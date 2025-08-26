using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Okean_AnimeMovie.Core.Entities;
using Okean_AnimeMovie.Infrastructure.Data;

namespace Okean_AnimeMovie.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        var stats = new AdminDashboardViewModel
        {
            TotalUsers = await _context.Users.CountAsync(),
            TotalAnime = await _context.Animes.CountAsync(),
            TotalEpisodes = await _context.Episodes.CountAsync(),
            TotalComments = await _context.Comments.CountAsync(),
            TotalFavorites = await _context.Favorites.CountAsync()
        };
        return View(stats);
    }

    [HttpGet]
    public async Task<IActionResult> Users()
    {
        var users = await _context.Users
            .OrderBy(u => u.UserName)
            .Select(u => new AdminUserRow
            {
                Id = u.Id,
                UserName = u.UserName!,
                Email = u.Email!,
                FirstName = u.FirstName,
                LastName = u.LastName,
                IsAdmin = false
            }).ToListAsync();

        // load admin flags
        for (int i = 0; i < users.Count; i++)
        {
            users[i].IsAdmin = await _userManager.IsInRoleAsync(await _userManager.FindByIdAsync(users[i].Id), "Admin");
        }

        return View(users);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Promote(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return RedirectToAction(nameof(Users));
        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
        }
        await _userManager.AddToRoleAsync(user, "Admin");
        return RedirectToAction(nameof(Users));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Demote(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return RedirectToAction(nameof(Users));
        await _userManager.RemoveFromRoleAsync(user, "Admin");
        return RedirectToAction(nameof(Users));
    }
}

public class AdminDashboardViewModel
{
    public int TotalUsers { get; set; }
    public int TotalAnime { get; set; }
    public int TotalEpisodes { get; set; }
    public int TotalComments { get; set; }
    public int TotalFavorites { get; set; }
}

public class AdminUserRow
{
    public string Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool IsAdmin { get; set; }
}
