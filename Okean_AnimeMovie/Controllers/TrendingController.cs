using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Okean_AnimeMovie.Core.DTOs;
using Okean_AnimeMovie.Core.Interfaces;
using Okean_AnimeMovie.Infrastructure.Data;
using Okean_AnimeMovie.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Okean_AnimeMovie.Controllers;

public class TrendingController : Controller
{
    private readonly ITrendingRepository _trendingRepository;
    private readonly ApplicationDbContext _context;

    public TrendingController(ITrendingRepository trendingRepository, ApplicationDbContext context)
    {
        _trendingRepository = trendingRepository;
        _context = context;
    }

    // GET: Trending
    public async Task<IActionResult> Index(TrendingFilterDto? filter = null)
    {
        filter ??= new TrendingFilterDto();
        
        var trendingAnime = await _trendingRepository.GetTrendingAnimeAsync(filter);
        var totalCount = await _trendingRepository.GetTrendingCountAsync(filter);
        
        ViewBag.Filter = filter;
        ViewBag.TotalCount = totalCount;
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize);
        ViewBag.CurrentPage = filter.Page;
        
        return View(trendingAnime);
    }

    // GET: Trending/Summary
    public async Task<IActionResult> Summary()
    {
        var summary = await _trendingRepository.GetTrendingSummaryAsync(10);
        return View(summary);
    }

    // GET: Trending/MostViewed
    public async Task<IActionResult> MostViewed(int page = 1, int pageSize = 20)
    {
        var filter = new TrendingFilterDto
        {
            Type = TrendingType.MostViewed,
            Page = page,
            PageSize = pageSize
        };
        
        var anime = await _trendingRepository.GetTrendingAnimeAsync(filter);
        var totalCount = await _trendingRepository.GetTrendingCountAsync(filter);
        
        ViewBag.Title = "Xem nhiều nhất";
        ViewBag.TotalCount = totalCount;
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        ViewBag.CurrentPage = page;
        
        return View("Index", anime);
    }

    // GET: Trending/TopRated
    public async Task<IActionResult> TopRated(int page = 1, int pageSize = 20)
    {
        var filter = new TrendingFilterDto
        {
            Type = TrendingType.TopRated,
            Page = page,
            PageSize = pageSize
        };
        
        var anime = await _trendingRepository.GetTrendingAnimeAsync(filter);
        var totalCount = await _trendingRepository.GetTrendingCountAsync(filter);
        
        ViewBag.Title = "Đánh giá cao nhất";
        ViewBag.TotalCount = totalCount;
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        ViewBag.CurrentPage = page;
        
        return View("Index", anime);
    }

    // GET: Trending/MostCommented
    public async Task<IActionResult> MostCommented(int page = 1, int pageSize = 20)
    {
        var filter = new TrendingFilterDto
        {
            Type = TrendingType.MostCommented,
            Page = page,
            PageSize = pageSize
        };
        
        var anime = await _trendingRepository.GetTrendingAnimeAsync(filter);
        var totalCount = await _trendingRepository.GetTrendingCountAsync(filter);
        
        ViewBag.Title = "Bình luận nhiều nhất";
        ViewBag.TotalCount = totalCount;
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        ViewBag.CurrentPage = page;
        
        return View("Index", anime);
    }

    // GET: Trending/MostFavorited
    public async Task<IActionResult> MostFavorited(int page = 1, int pageSize = 20)
    {
        var filter = new TrendingFilterDto
        {
            Type = TrendingType.MostFavorited,
            Page = page,
            PageSize = pageSize
        };
        
        var anime = await _trendingRepository.GetTrendingAnimeAsync(filter);
        var totalCount = await _trendingRepository.GetTrendingCountAsync(filter);
        
        ViewBag.Title = "Yêu thích nhiều nhất";
        ViewBag.TotalCount = totalCount;
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        ViewBag.CurrentPage = page;
        
        return View("Index", anime);
    }

    // GET: Trending/RecentlyAdded
    public async Task<IActionResult> RecentlyAdded(int page = 1, int pageSize = 20)
    {
        var filter = new TrendingFilterDto
        {
            Type = TrendingType.RecentlyAdded,
            Page = page,
            PageSize = pageSize
        };
        
        var anime = await _trendingRepository.GetTrendingAnimeAsync(filter);
        var totalCount = await _trendingRepository.GetTrendingCountAsync(filter);
        
        ViewBag.Title = "Mới thêm gần đây";
        ViewBag.TotalCount = totalCount;
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        ViewBag.CurrentPage = page;
        
        return View("Index", anime);
    }

    // GET: Trending/ByGenre/{genre}
    public async Task<IActionResult> ByGenre(string genre, int page = 1, int pageSize = 20)
    {
        var anime = await _trendingRepository.GetTrendingByGenreAsync(genre, pageSize);
        
        ViewBag.Title = $"Thịnh hành - {genre}";
        ViewBag.Genre = genre;
        ViewBag.CurrentPage = page;
        
        return View("Index", anime);
    }

    // GET: Trending/ByYear/{year}
    public async Task<IActionResult> ByYear(int year, int page = 1, int pageSize = 20)
    {
        var anime = await _trendingRepository.GetTrendingByYearAsync(year, pageSize);
        
        ViewBag.Title = $"Thịnh hành năm {year}";
        ViewBag.Year = year;
        ViewBag.CurrentPage = page;
        
        return View("Index", anime);
    }

    // POST: Trending/RecordView
    [HttpPost]
    public async Task<IActionResult> RecordView(int animeId)
    {
        try
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();
            var referrer = HttpContext.Request.Headers["Referer"].ToString();
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // Check if this view was already recorded recently (within 1 hour)
            var oneHourAgo = DateTime.UtcNow.AddHours(-1);
            var existingView = await _context.ViewCounts
                .FirstOrDefaultAsync(vc => vc.AnimeId == animeId && 
                                          vc.IpAddress == ipAddress && 
                                          vc.ViewedAt > oneHourAgo);

            if (existingView == null)
            {
                var viewCount = new ViewCount
                {
                    AnimeId = animeId,
                    UserId = userId,
                    IpAddress = ipAddress,
                    UserAgent = userAgent,
                    Referrer = referrer,
                    ViewedAt = DateTime.UtcNow
                };

                _context.ViewCounts.Add(viewCount);
                await _context.SaveChangesAsync();
            }

            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, error = ex.Message });
        }
    }

    // GET: Trending/GetGenres (for filter dropdown)
    [HttpGet]
    public async Task<IActionResult> GetGenres()
    {
        var genres = await _context.Genres
            .OrderBy(g => g.Name)
            .Select(g => g.Name)
            .ToListAsync();
        
        return Json(genres);
    }

    // GET: Trending/GetYears (for filter dropdown)
    [HttpGet]
    public async Task<IActionResult> GetYears()
    {
        var years = await _context.Animes
            .Select(a => a.ReleaseYear)
            .Distinct()
            .OrderByDescending(y => y)
            .ToListAsync();
        
        return Json(years);
    }
}
