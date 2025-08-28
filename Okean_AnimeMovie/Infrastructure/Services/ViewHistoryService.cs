using Microsoft.EntityFrameworkCore;
using Okean_AnimeMovie.Core.DTOs;
using Okean_AnimeMovie.Core.Entities;
using Okean_AnimeMovie.Core.Interfaces;
using Okean_AnimeMovie.Infrastructure.Data;

namespace Okean_AnimeMovie.Infrastructure.Services;

public class ViewHistoryService : IViewHistoryService
{
    private readonly ApplicationDbContext _context;

    public ViewHistoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ViewHistoryDto>> GetUserViewHistoryAsync(string userId, int page = 1, int pageSize = 20)
    {
        var skip = (page - 1) * pageSize;
        
        var viewHistories = await _context.ViewHistories
            .Where(vh => vh.UserId == userId)
            .Include(vh => vh.Anime)
            .Include(vh => vh.Episode)
            .OrderByDescending(vh => vh.WatchedAt)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        return viewHistories.Select(MapToDto);
    }

    public async Task<ViewHistoryDto?> GetViewHistoryAsync(int id)
    {
        var viewHistory = await _context.ViewHistories
            .Include(vh => vh.Anime)
            .Include(vh => vh.Episode)
            .FirstOrDefaultAsync(vh => vh.Id == id);

        return viewHistory != null ? MapToDto(viewHistory) : null;
    }

    public async Task<ViewHistoryDto> CreateViewHistoryAsync(string userId, CreateViewHistoryDto dto)
    {
        // Kiểm tra xem đã có lịch sử xem cho episode này chưa
        var existingHistory = await _context.ViewHistories
            .FirstOrDefaultAsync(vh => vh.UserId == userId && vh.EpisodeId == dto.EpisodeId);

        if (existingHistory != null)
        {
            // Cập nhật lịch sử hiện có
            existingHistory.WatchedAt = DateTime.UtcNow;
            existingHistory.WatchDuration = dto.WatchDuration;
            existingHistory.IsCompleted = dto.IsCompleted;
            
            await _context.SaveChangesAsync();
            return await GetViewHistoryAsync(existingHistory.Id) ?? throw new InvalidOperationException("Failed to retrieve updated view history");
        }

        // Tạo lịch sử mới
        var viewHistory = new ViewHistory
        {
            UserId = userId,
            AnimeId = dto.AnimeId,
            EpisodeId = dto.EpisodeId,
            WatchDuration = dto.WatchDuration,
            IsCompleted = dto.IsCompleted,
            WatchedAt = DateTime.UtcNow
        };

        _context.ViewHistories.Add(viewHistory);
        await _context.SaveChangesAsync();

        return await GetViewHistoryAsync(viewHistory.Id) ?? throw new InvalidOperationException("Failed to retrieve created view history");
    }

    public async Task<ViewHistoryDto> UpdateViewHistoryAsync(string userId, UpdateViewHistoryDto dto)
    {
        var viewHistory = await _context.ViewHistories
            .FirstOrDefaultAsync(vh => vh.Id == dto.Id && vh.UserId == userId);

        if (viewHistory == null)
            throw new InvalidOperationException("View history not found or access denied");

        viewHistory.WatchDuration = dto.WatchDuration;
        viewHistory.IsCompleted = dto.IsCompleted;
        viewHistory.WatchedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return await GetViewHistoryAsync(viewHistory.Id) ?? throw new InvalidOperationException("Failed to retrieve updated view history");
    }

    public async Task<bool> DeleteViewHistoryAsync(string userId, int id)
    {
        var viewHistory = await _context.ViewHistories
            .FirstOrDefaultAsync(vh => vh.Id == id && vh.UserId == userId);

        if (viewHistory == null)
            return false;

        _context.ViewHistories.Remove(viewHistory);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ClearUserViewHistoryAsync(string userId)
    {
        var userHistories = await _context.ViewHistories
            .Where(vh => vh.UserId == userId)
            .ToListAsync();

        _context.ViewHistories.RemoveRange(userHistories);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetUserViewHistoryCountAsync(string userId)
    {
        return await _context.ViewHistories
            .CountAsync(vh => vh.UserId == userId);
    }

    public async Task<IEnumerable<ViewHistoryDto>> GetRecentWatchedAsync(string userId, int count = 10)
    {
        var recentHistories = await _context.ViewHistories
            .Where(vh => vh.UserId == userId)
            .Include(vh => vh.Anime)
            .Include(vh => vh.Episode)
            .OrderByDescending(vh => vh.WatchedAt)
            .Take(count)
            .ToListAsync();

        return recentHistories.Select(MapToDto);
    }

    public async Task<IEnumerable<ViewHistoryDto>> GetWatchedByAnimeAsync(string userId, int animeId)
    {
        var animeHistories = await _context.ViewHistories
            .Where(vh => vh.UserId == userId && vh.AnimeId == animeId)
            .Include(vh => vh.Anime)
            .Include(vh => vh.Episode)
            .OrderByDescending(vh => vh.WatchedAt)
            .ToListAsync();

        return animeHistories.Select(MapToDto);
    }

    private static ViewHistoryDto MapToDto(ViewHistory viewHistory)
    {
        return new ViewHistoryDto
        {
            Id = viewHistory.Id,
            AnimeId = viewHistory.AnimeId,
            AnimeTitle = viewHistory.Anime.Title,
            AnimePoster = viewHistory.Anime.Poster ?? string.Empty,
            EpisodeId = viewHistory.EpisodeId,
            EpisodeTitle = viewHistory.Episode.Title,
            EpisodeNumber = viewHistory.Episode.EpisodeNumber,
            WatchedAt = viewHistory.WatchedAt,
            WatchDuration = viewHistory.WatchDuration,
            IsCompleted = viewHistory.IsCompleted,
            FormattedWatchDuration = FormatDuration(viewHistory.WatchDuration),
            FormattedWatchedAt = FormatDateTime(viewHistory.WatchedAt)
        };
    }

    private static string FormatDuration(int seconds)
    {
        if (seconds < 60)
            return $"{seconds}s";
        
        var minutes = seconds / 60;
        var remainingSeconds = seconds % 60;
        
        if (minutes < 60)
            return $"{minutes}m {remainingSeconds}s";
        
        var hours = minutes / 60;
        var remainingMinutes = minutes % 60;
        
        return $"{hours}h {remainingMinutes}m {remainingSeconds}s";
    }

    private static string FormatDateTime(DateTime dateTime)
    {
        var now = DateTime.UtcNow;
        var diff = now - dateTime;

        if (diff.TotalDays >= 1)
            return $"{(int)diff.TotalDays} ngày trước";
        
        if (diff.TotalHours >= 1)
            return $"{(int)diff.TotalHours} giờ trước";
        
        if (diff.TotalMinutes >= 1)
            return $"{(int)diff.TotalMinutes} phút trước";
        
        return "Vừa xem";
    }
}
