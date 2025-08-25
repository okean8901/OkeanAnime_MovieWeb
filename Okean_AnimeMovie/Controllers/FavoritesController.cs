using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Okean_AnimeMovie.Core.Interfaces;
using System.Security.Claims;

namespace Okean_AnimeMovie.Controllers;

[Authorize]
public class FavoritesController : Controller
{
    private readonly IFavoriteRepository _favoriteRepository;

    public FavoritesController(IFavoriteRepository favoriteRepository)
    {
        _favoriteRepository = favoriteRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int page = 1)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var animes = await _favoriteRepository.GetUserFavoritesAsync(userId, page, 20);
        return View(animes);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int animeId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        await _favoriteRepository.AddToFavoritesAsync(userId, animeId);
        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(int animeId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        await _favoriteRepository.RemoveFromFavoritesAsync(userId, animeId);
        return Redirect(Request.Headers["Referer"].ToString());
    }
}
