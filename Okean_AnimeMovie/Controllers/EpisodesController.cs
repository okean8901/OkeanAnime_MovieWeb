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
}
