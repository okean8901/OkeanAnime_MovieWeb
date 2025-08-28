using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Okean_AnimeMovie.Core.DTOs;
using Okean_AnimeMovie.Core.Interfaces;
using System.Security.Claims;

namespace Okean_AnimeMovie.Controllers;

[Authorize]
public class ViewHistoryController : Controller
{
    private readonly IViewHistoryService _viewHistoryService;

    public ViewHistoryController(IViewHistoryService viewHistoryService)
    {
        _viewHistoryService = viewHistoryService;
    }

    // GET: /ViewHistory
    public async Task<IActionResult> Index(int page = 1)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return RedirectToAction("Login", "Account");

        var pageSize = 20;
        var viewHistories = await _viewHistoryService.GetUserViewHistoryAsync(userId, page, pageSize);
        var totalCount = await _viewHistoryService.GetUserViewHistoryCountAsync(userId);
        
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        ViewBag.TotalCount = totalCount;

        return View(viewHistories);
    }

    // GET: /ViewHistory/Recent
    public async Task<IActionResult> Recent()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return RedirectToAction("Login", "Account");

        var recentHistories = await _viewHistoryService.GetRecentWatchedAsync(userId, 10);
        return View(recentHistories);
    }

    // POST: /ViewHistory/Create
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateViewHistoryDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var viewHistory = await _viewHistoryService.CreateViewHistoryAsync(userId, dto);
            return Json(new { success = true, data = viewHistory });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    // PUT: /ViewHistory/Update
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateViewHistoryDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var viewHistory = await _viewHistoryService.UpdateViewHistoryAsync(userId, dto);
            return Json(new { success = true, data = viewHistory });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    // DELETE: /ViewHistory/Delete/5
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _viewHistoryService.DeleteViewHistoryAsync(userId, id);
            if (result)
                return Json(new { success = true });
            else
                return Json(new { success = false, message = "Không tìm thấy lịch sử xem phim" });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    // POST: /ViewHistory/Clear
    [HttpPost]
    public async Task<IActionResult> Clear()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            await _viewHistoryService.ClearUserViewHistoryAsync(userId);
            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    // GET: /ViewHistory/Anime/5
    public async Task<IActionResult> Anime(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return RedirectToAction("Login", "Account");

        var animeHistories = await _viewHistoryService.GetWatchedByAnimeAsync(userId, id);
        return View(animeHistories);
    }


}
