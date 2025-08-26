using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Okean_AnimeMovie.Core.Entities;
using Okean_AnimeMovie.Core.Interfaces;

namespace Okean_AnimeMovie.Controllers;

[Authorize(Roles = "Admin")]
public class EpisodesController : Controller
{
    private readonly IGenericRepository<Episode> _episodeRepository;
    private readonly IGenericRepository<Anime> _animeRepository;

    public EpisodesController(IGenericRepository<Episode> episodeRepository, IGenericRepository<Anime> animeRepository)
    {
        _episodeRepository = episodeRepository;
        _animeRepository = animeRepository;
    }

    public async Task<IActionResult> Index(int animeId)
    {
        var anime = await _animeRepository.GetByIdAsync(animeId);
        if (anime == null) return NotFound();
        ViewBag.Anime = anime;
        var all = await _episodeRepository.GetAllAsync();
        var list = all.Where(e => e.AnimeId == animeId).OrderBy(e => e.EpisodeNumber);
        return View(list);
    }

    [HttpGet]
    public async Task<IActionResult> BulkAdd(int animeId)
    {
        var anime = await _animeRepository.GetByIdAsync(animeId);
        if (anime == null) return NotFound();
        ViewBag.Anime = anime;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BulkAdd(int animeId, string payload, string? defaultVideoType)
    {
        var anime = await _animeRepository.GetByIdAsync(animeId);
        if (anime == null) return NotFound();
        ViewBag.Anime = anime;

        if (string.IsNullOrWhiteSpace(payload))
        {
            ModelState.AddModelError(string.Empty, "Vui lòng nhập dữ liệu tập phim.");
            return View();
        }

        var all = await _episodeRepository.GetAllAsync();
        var existing = all.Where(e => e.AnimeId == animeId).ToDictionary(e => e.EpisodeNumber, e => e);

        // Supported formats per line (comma or tab separated):
        // 1,Title,https://video,Embed,Https://thumb.jpg,1200
        // Columns: EpisodeNumber, Title, VideoUrl, [VideoType], [Thumbnail], [DurationSeconds]
        int created = 0, updated = 0, skipped = 0;
        var lines = payload.Replace("\r", string.Empty).Split('\n', StringSplitOptions.RemoveEmptyEntries);
        foreach (var raw in lines)
        {
            var line = raw.Trim();
            if (line.Length == 0) continue;
            var parts = line.Contains('\t') ? line.Split('\t') : line.Split(',');
            if (parts.Length < 3) { skipped++; continue; }

            if (!int.TryParse(parts[0].Trim(), out int epNo)) { skipped++; continue; }
            var title = parts[1].Trim();
            var videoUrl = parts[2].Trim();
            var videoType = (parts.Length >= 4 && !string.IsNullOrWhiteSpace(parts[3])) ? parts[3].Trim() : (defaultVideoType ?? "Embed");
            var thumb = (parts.Length >= 5) ? parts[4].Trim() : null;
            int? duration = null;
            if (parts.Length >= 6 && int.TryParse(parts[5].Trim(), out int d)) duration = d;

            if (existing.TryGetValue(epNo, out var ep))
            {
                ep.Title = title;
                ep.VideoUrl = videoUrl;
                ep.VideoType = videoType;
                ep.Thumbnail = thumb;
                if (duration.HasValue) ep.Duration = duration.Value;
                await _episodeRepository.UpdateAsync(ep);
                updated++;
            }
            else
            {
                var newEpisode = new Episode
                {
                    AnimeId = animeId,
                    EpisodeNumber = epNo,
                    Title = title,
                    VideoUrl = videoUrl,
                    VideoType = videoType,
                    Thumbnail = thumb,
                    Duration = duration ?? 0,
                    CreatedAt = DateTime.UtcNow,
                };
                await _episodeRepository.AddAsync(newEpisode);
                created++;
            }
        }

        TempData["Success"] = $"Đã thêm {created}, cập nhật {updated}, bỏ qua {skipped}.";
        return RedirectToAction(nameof(Index), new { animeId });
    }

    [HttpGet]
    public async Task<IActionResult> Create(int animeId)
    {
        var anime = await _animeRepository.GetByIdAsync(animeId);
        if (anime == null) return NotFound();
        ViewBag.Anime = anime;
        return View(new Episode { AnimeId = animeId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Episode model)
    {
        var anime = await _animeRepository.GetByIdAsync(model.AnimeId);
        if (anime == null) return NotFound();
        ViewBag.Anime = anime;

        if (!ModelState.IsValid) return View(model);
        await _episodeRepository.AddAsync(model);
        TempData["Success"] = "Episode created.";
        return RedirectToAction(nameof(Index), new { animeId = model.AnimeId });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var ep = await _episodeRepository.GetByIdAsync(id);
        if (ep == null) return NotFound();
        var anime = await _animeRepository.GetByIdAsync(ep.AnimeId);
        if (anime == null) return NotFound();
        ViewBag.Anime = anime;
        return View(ep);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Episode model)
    {
        if (id != model.Id) return BadRequest();
        var anime = await _animeRepository.GetByIdAsync(model.AnimeId);
        if (anime == null) return NotFound();
        ViewBag.Anime = anime;

        if (!ModelState.IsValid) return View(model);
        await _episodeRepository.UpdateAsync(model);
        TempData["Success"] = "Episode updated.";
        return RedirectToAction(nameof(Index), new { animeId = model.AnimeId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var ep = await _episodeRepository.GetByIdAsync(id);
        if (ep == null) return NotFound();
        await _episodeRepository.DeleteAsync(ep);
        TempData["Success"] = "Episode deleted.";
        return RedirectToAction(nameof(Index), new { animeId = ep.AnimeId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MoveUp(int id)
    {
        var current = await _episodeRepository.GetByIdAsync(id);
        if (current == null) return NotFound();
        var all = await _episodeRepository.GetAllAsync();
        var prev = all.Where(e => e.AnimeId == current.AnimeId && e.EpisodeNumber < current.EpisodeNumber)
                      .OrderByDescending(e => e.EpisodeNumber)
                      .FirstOrDefault();
        if (prev != null)
        {
            var tmp = current.EpisodeNumber;
            current.EpisodeNumber = prev.EpisodeNumber;
            prev.EpisodeNumber = tmp;
            await _episodeRepository.UpdateAsync(prev);
            await _episodeRepository.UpdateAsync(current);
            TempData["Success"] = $"Moved episode {current.Title} up.";
        }
        return RedirectToAction(nameof(Index), new { animeId = current.AnimeId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MoveDown(int id)
    {
        var current = await _episodeRepository.GetByIdAsync(id);
        if (current == null) return NotFound();
        var all = await _episodeRepository.GetAllAsync();
        var next = all.Where(e => e.AnimeId == current.AnimeId && e.EpisodeNumber > current.EpisodeNumber)
                      .OrderBy(e => e.EpisodeNumber)
                      .FirstOrDefault();
        if (next != null)
        {
            var tmp = current.EpisodeNumber;
            current.EpisodeNumber = next.EpisodeNumber;
            next.EpisodeNumber = tmp;
            await _episodeRepository.UpdateAsync(next);
            await _episodeRepository.UpdateAsync(current);
            TempData["Success"] = $"Moved episode {current.Title} down.";
        }
        return RedirectToAction(nameof(Index), new { animeId = current.AnimeId });
    }
}
