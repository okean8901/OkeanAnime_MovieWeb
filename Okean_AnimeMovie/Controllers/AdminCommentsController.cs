using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Okean_AnimeMovie.Core.Entities;
using Okean_AnimeMovie.Infrastructure.Data;

namespace Okean_AnimeMovie.Controllers;

[Authorize(Roles = "Admin")]
public class AdminCommentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminCommentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int? animeId, string? userName, bool? onlyHidden)
    {
        var q = _context.Comments
            .Include(c => c.User)
            .Include(c => c.Anime)
            .AsQueryable();

        if (animeId.HasValue)
        {
            q = q.Where(c => c.AnimeId == animeId.Value);
        }
        if (!string.IsNullOrWhiteSpace(userName))
        {
            q = q.Where(c => c.User.UserName!.Contains(userName));
        }
        if (onlyHidden == true)
        {
            q = q.Where(c => !c.IsActive);
        }

        var vm = await q
            .OrderByDescending(c => c.CreatedAt)
            .Take(500)
            .ToListAsync();

        ViewBag.Animes = await _context.Animes.OrderBy(a => a.Title).ToListAsync();
        ViewBag.FilterAnimeId = animeId;
        ViewBag.FilterUserName = userName;
        ViewBag.OnlyHidden = onlyHidden == true;
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Hide(int id)
    {
        var c = await _context.Comments.FindAsync(id);
        if (c == null) return NotFound();
        c.IsActive = false;
        c.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { animeId = c.AnimeId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Show(int id)
    {
        var c = await _context.Comments.FindAsync(id);
        if (c == null) return NotFound();
        c.IsActive = true;
        c.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { animeId = c.AnimeId, onlyHidden = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var c = await _context.Comments.FindAsync(id);
        if (c == null) return NotFound();
        c.IsActive = false;
        c.Content = "[Đã xóa bởi quản trị viên]";
        c.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { animeId = c.AnimeId });
    }
}
