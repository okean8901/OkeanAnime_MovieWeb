using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Okean_AnimeMovie.Core.DTOs;
using Okean_AnimeMovie.Core.Entities;
using Okean_AnimeMovie.Core.Interfaces;

namespace Okean_AnimeMovie.Controllers;

public class AnimeController : Controller
{
    private readonly IAnimeRepository _animeRepository;
    private readonly IGenericRepository<Genre> _genreRepository;
    private readonly ILogger<AnimeController> _logger;
    private readonly IFavoriteRepository _favoriteRepository;

    public AnimeController(
        IAnimeRepository animeRepository,
        IGenericRepository<Genre> genreRepository,
        ILogger<AnimeController> logger,
        IFavoriteRepository favoriteRepository)
    {
        _animeRepository = animeRepository;
        _genreRepository = genreRepository;
        _logger = logger;
        _favoriteRepository = favoriteRepository;
    }

    public async Task<IActionResult> Index(string searchTerm, int? genreId, int? year, int page = 1)
    {
        try
        {
            var animes = await _animeRepository.SearchAnimeAsync(searchTerm ?? "", genreId, year, page, 20);
            var totalCount = await _animeRepository.GetTotalAnimeCountAsync(searchTerm, genreId, year);
            var genres = await _genreRepository.GetAllAsync();

            ViewBag.SearchTerm = searchTerm;
            ViewBag.SelectedGenreId = genreId;
            ViewBag.SelectedYear = year;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / 20);
            ViewBag.Genres = genres;

            return View(animes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading anime list");
            return View("Error");
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var anime = await _animeRepository.GetAnimeWithDetailsAsync(id);
            if (anime == null)
            {
                return NotFound();
            }

            if (User?.Identity?.IsAuthenticated == true)
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    var isFav = await _favoriteRepository.IsFavoriteAsync(userId, id);
                    ViewBag.IsFavorite = isFav;
                }
            }

            // Increment view count
            await _animeRepository.IncrementViewCountAsync(id);

            return View(anime);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading anime details for ID: {AnimeId}", id);
            return View("Error");
        }
    }

    public async Task<IActionResult> Watch(int animeId, int episodeNumber)
    {
        try
        {
            var anime = await _animeRepository.GetAnimeWithDetailsAsync(animeId);
            if (anime == null)
            {
                return NotFound();
            }

            var episode = anime.Episodes.FirstOrDefault(e => e.EpisodeNumber == episodeNumber);
            if (episode == null)
            {
                return NotFound();
            }

            ViewBag.Anime = anime;
            return View(episode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading episode for anime ID: {AnimeId}, episode: {EpisodeNumber}", animeId, episodeNumber);
            return View("Error");
        }
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        var genres = await _genreRepository.GetAllAsync();
        ViewBag.Genres = genres;
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateAnimeDto model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var anime = new Anime
                {
                    Title = model.Title,
                    AlternativeTitle = model.AlternativeTitle,
                    Description = model.Description,
                    Poster = model.Poster,
                    Trailer = model.Trailer,
                    ReleaseYear = model.ReleaseYear,
                    TotalEpisodes = model.TotalEpisodes,
                    Status = model.Status,
                    Type = model.Type,
                    Rating = 0,
                    ViewCount = 0,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    AnimeGenres = new List<AnimeGenre>()
                };

                // Map selected genres
                if (model.GenreIds != null && model.GenreIds.Any())
                {
                    foreach (var gid in model.GenreIds.Distinct())
                    {
                        anime.AnimeGenres.Add(new AnimeGenre { GenreId = gid });
                    }
                }

                await _animeRepository.AddAsync(anime);
                TempData["Success"] = "Anime created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating anime");
                ModelState.AddModelError(string.Empty, "An error occurred while creating the anime.");
            }
        }

        var genres = await _genreRepository.GetAllAsync();
        ViewBag.Genres = genres;
        return View(model);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var anime = await _animeRepository.GetAnimeWithDetailsAsync(id);
            if (anime == null)
            {
                return NotFound();
            }

            var genres = await _genreRepository.GetAllAsync();
            ViewBag.Genres = genres;

            var dto = new UpdateAnimeDto
            {
                Id = anime.Id,
                Title = anime.Title,
                AlternativeTitle = anime.AlternativeTitle,
                Description = anime.Description,
                Poster = anime.Poster,
                Trailer = anime.Trailer,
                ReleaseYear = anime.ReleaseYear,
                TotalEpisodes = anime.TotalEpisodes,
                Status = anime.Status,
                Type = anime.Type,
                GenreIds = anime.AnimeGenres.Select(ag => ag.GenreId).ToList()
            };

            return View(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading anime for edit, ID: {AnimeId}", id);
            return View("Error");
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateAnimeDto model)
    {
        if (id != model.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var anime = await _animeRepository.GetAnimeWithDetailsAsync(id);
                if (anime == null)
                {
                    return NotFound();
                }

                anime.Title = model.Title;
                anime.AlternativeTitle = model.AlternativeTitle;
                anime.Description = model.Description;
                anime.Poster = model.Poster;
                anime.Trailer = model.Trailer;
                anime.ReleaseYear = model.ReleaseYear;
                anime.TotalEpisodes = model.TotalEpisodes;
                anime.Status = model.Status;
                anime.Type = model.Type;
                anime.UpdatedAt = DateTime.UtcNow;

                // Update genres
                var newGenreIds = new HashSet<int>(model.GenreIds ?? new List<int>());
                anime.AnimeGenres = anime.AnimeGenres
                    .Where(ag => newGenreIds.Contains(ag.GenreId))
                    .ToList();
                foreach (var gid in newGenreIds)
                {
                    if (!anime.AnimeGenres.Any(ag => ag.GenreId == gid))
                    {
                        anime.AnimeGenres.Add(new AnimeGenre { GenreId = gid, AnimeId = anime.Id });
                    }
                }

                await _animeRepository.UpdateAsync(anime);
                TempData["Success"] = "Anime updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating anime, ID: {AnimeId}", id);
                ModelState.AddModelError(string.Empty, "An error occurred while updating the anime.");
            }
        }

        var genres = await _genreRepository.GetAllAsync();
        ViewBag.Genres = genres;
        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var anime = await _animeRepository.GetByIdAsync(id);
            if (anime == null)
            {
                return NotFound();
            }

            await _animeRepository.DeleteAsync(anime);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting anime, ID: {AnimeId}", id);
            return View("Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Trending()
    {
        try
        {
            var trendingAnimes = await _animeRepository.GetTrendingAnimeAsync(10);
            return View(trendingAnimes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading trending anime");
            return View("Error");
        }
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToFavorites(int id)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            await _favoriteRepository.AddToFavoritesAsync(userId, id);
        }
        return RedirectToAction("Details", new { id });
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveFromFavorites(int id)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            await _favoriteRepository.RemoveFromFavoritesAsync(userId, id);
        }
        return RedirectToAction("Details", new { id });
    }
}
