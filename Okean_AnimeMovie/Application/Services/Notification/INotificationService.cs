using Okean_AnimeMovie.Core.Entities;

namespace Okean_AnimeMovie.Application.Services.Notification;

public interface INotificationService
{
    Task SendNewEpisodeNotificationAsync(int animeId, int episodeNumber);
    Task SendAnimeUpdateNotificationAsync(int animeId, string updateType);
    Task SendUserWelcomeNotificationAsync(string userId);
    Task SendPasswordResetNotificationAsync(string email);
    Task<IEnumerable<UserNotification>> GetUserNotificationsAsync(string userId, int page = 1, int pageSize = 20);
    Task MarkNotificationAsReadAsync(int notificationId);
    Task MarkAllNotificationsAsReadAsync(string userId);
    Task<int> GetUnreadNotificationCountAsync(string userId);
}
