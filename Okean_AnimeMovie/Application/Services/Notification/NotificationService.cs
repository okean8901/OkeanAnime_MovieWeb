using Microsoft.EntityFrameworkCore;
using Okean_AnimeMovie.Core.Entities;
using Okean_AnimeMovie.Infrastructure.Data;

namespace Okean_AnimeMovie.Application.Services.Notification;

public class NotificationService : INotificationService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(ApplicationDbContext context, ILogger<NotificationService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SendNewEpisodeNotificationAsync(int animeId, int episodeNumber)
    {
        try
        {
            var anime = await _context.Animes.FindAsync(animeId);
            if (anime == null) return;

            var users = await _context.Users.ToListAsync();
            var notifications = users.Select(user => new UserNotification
            {
                UserId = user.Id,
                Title = $"Tập mới: {anime.Title}",
                Message = $"Tập {episodeNumber} của {anime.Title} đã được cập nhật!",
                Type = "episode",
                AnimeId = animeId,
                EpisodeNumber = episodeNumber,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            }).ToList();

            await _context.UserNotifications.AddRangeAsync(notifications);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Sent {notifications.Count} new episode notifications for anime {animeId}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending new episode notifications");
        }
    }

    public async Task SendAnimeUpdateNotificationAsync(int animeId, string updateType)
    {
        try
        {
            var anime = await _context.Animes.FindAsync(animeId);
            if (anime == null) return;

            var users = await _context.Users.ToListAsync();
            var notifications = users.Select(user => new UserNotification
            {
                UserId = user.Id,
                Title = $"Cập nhật: {anime.Title}",
                Message = $"{anime.Title} đã được cập nhật: {updateType}",
                Type = "update",
                AnimeId = animeId,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            }).ToList();

            await _context.UserNotifications.AddRangeAsync(notifications);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Sent {notifications.Count} update notifications for anime {animeId}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending anime update notifications");
        }
    }

    public async Task SendUserWelcomeNotificationAsync(string userId)
    {
        try
        {
            var notification = new UserNotification
            {
                UserId = userId,
                Title = "Chào mừng bạn đến với Okean Anime!",
                Message = "Cảm ơn bạn đã tham gia cộng đồng anime của chúng tôi. Hãy khám phá những bộ anime tuyệt vời!",
                Type = "welcome",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            await _context.UserNotifications.AddAsync(notification);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Sent welcome notification to user {userId}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending welcome notification");
        }
    }

    public async Task SendPasswordResetNotificationAsync(string email)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return;

            var notification = new UserNotification
            {
                UserId = user.Id,
                Title = "Đặt lại mật khẩu",
                Message = "Yêu cầu đặt lại mật khẩu của bạn đã được xử lý. Vui lòng kiểm tra email để biết thêm chi tiết.",
                Type = "password_reset",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            await _context.UserNotifications.AddAsync(notification);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Sent password reset notification to user {user.Id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending password reset notification");
        }
    }

    public async Task<IEnumerable<UserNotification>> GetUserNotificationsAsync(string userId, int page = 1, int pageSize = 20)
    {
        try
        {
            return await _context.UserNotifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(n => n.Anime)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user notifications");
            return new List<UserNotification>();
        }
    }

    public async Task MarkNotificationAsReadAsync(int notificationId)
    {
        try
        {
            var notification = await _context.UserNotifications.FindAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                notification.ReadAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error marking notification as read");
        }
    }

    public async Task MarkAllNotificationsAsReadAsync(string userId)
    {
        try
        {
            var notifications = await _context.UserNotifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
                notification.ReadAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error marking all notifications as read");
        }
    }

    public async Task<int> GetUnreadNotificationCountAsync(string userId)
    {
        try
        {
            return await _context.UserNotifications
                .CountAsync(n => n.UserId == userId && !n.IsRead);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting unread notification count");
            return 0;
        }
    }
}
