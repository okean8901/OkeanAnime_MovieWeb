using Okean_AnimeMovie.Core.DTOs;

namespace Okean_AnimeMovie.Core.Interfaces;

public interface ITrendingRepository
{
    Task<IEnumerable<TrendingDto>> GetTrendingAnimeAsync(TrendingFilterDto filter);
    Task<TrendingSummaryDto> GetTrendingSummaryAsync(int count = 10);
    Task<IEnumerable<TrendingDto>> GetMostViewedAnimeAsync(int count = 20);
    Task<IEnumerable<TrendingDto>> GetTopRatedAnimeAsync(int count = 20);
    Task<IEnumerable<TrendingDto>> GetMostCommentedAnimeAsync(int count = 20);
    Task<IEnumerable<TrendingDto>> GetMostFavoritedAnimeAsync(int count = 20);
    Task<IEnumerable<TrendingDto>> GetRecentlyAddedAnimeAsync(int count = 20);
    Task<IEnumerable<TrendingDto>> GetRecentlyUpdatedAnimeAsync(int count = 20);
    Task<IEnumerable<TrendingDto>> GetTrendingThisWeekAsync(int count = 20);
    Task<IEnumerable<TrendingDto>> GetTrendingByGenreAsync(string genre, int count = 20);
    Task<IEnumerable<TrendingDto>> GetTrendingByYearAsync(int year, int count = 20);
    Task<int> GetTrendingCountAsync(TrendingFilterDto filter);
    Task UpdateAnimeViewCountAsync(int animeId);
    Task UpdateAnimeTrendingScoreAsync(int animeId);
}
