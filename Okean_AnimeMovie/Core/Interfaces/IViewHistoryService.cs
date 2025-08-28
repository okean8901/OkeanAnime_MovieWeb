using Okean_AnimeMovie.Core.DTOs;

namespace Okean_AnimeMovie.Core.Interfaces;

public interface IViewHistoryService
{
    Task<IEnumerable<ViewHistoryDto>> GetUserViewHistoryAsync(string userId, int page = 1, int pageSize = 20);
    Task<ViewHistoryDto?> GetViewHistoryAsync(int id);
    Task<ViewHistoryDto> CreateViewHistoryAsync(string userId, CreateViewHistoryDto dto);
    Task<ViewHistoryDto> UpdateViewHistoryAsync(string userId, UpdateViewHistoryDto dto);
    Task<bool> DeleteViewHistoryAsync(string userId, int id);
    Task<bool> ClearUserViewHistoryAsync(string userId);
    Task<int> GetUserViewHistoryCountAsync(string userId);
    Task<IEnumerable<ViewHistoryDto>> GetRecentWatchedAsync(string userId, int count = 10);
    Task<IEnumerable<ViewHistoryDto>> GetWatchedByAnimeAsync(string userId, int animeId);
}
