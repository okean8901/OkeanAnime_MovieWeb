using Okean_AnimeMovie.Core.Entities;
using Okean_AnimeMovie.Core.DTOs;

namespace Okean_AnimeMovie.Application.Services.Analytics;

public interface IAnimeAnalyticsService
{
    Task<AnimeViewStats> GetAnimeViewStatsAsync(int animeId, DateTime? startDate = null, DateTime? endDate = null);
    Task<UserWatchStats> GetUserWatchStatsAsync(string userId, DateTime? startDate = null, DateTime? endDate = null);
    Task<GenrePopularityStats> GetGenrePopularityStatsAsync(DateTime? startDate = null, DateTime? endDate = null);
    Task<AnimeRatingStats> GetAnimeRatingStatsAsync(int animeId);
    Task<OverallStats> GetOverallStatsAsync();
    Task LogAnimeViewAsync(int animeId, int episodeNumber, string userId);
    Task LogAnimeRatingAsync(int animeId, string userId, int rating);
    Task LogUserActivityAsync(string userId, string activityType, string? details = null);
}
