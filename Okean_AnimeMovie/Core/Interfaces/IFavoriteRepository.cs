using Okean_AnimeMovie.Core.Entities;

namespace Okean_AnimeMovie.Core.Interfaces;

public interface IFavoriteRepository : IGenericRepository<Favorite>
{
    Task<IEnumerable<Anime>> GetUserFavoritesAsync(string userId, int page = 1, int pageSize = 20);
    Task<bool> IsFavoriteAsync(string userId, int animeId);
    Task AddToFavoritesAsync(string userId, int animeId);
    Task RemoveFromFavoritesAsync(string userId, int animeId);
    Task<int> GetFavoriteCountAsync(string userId);
}
