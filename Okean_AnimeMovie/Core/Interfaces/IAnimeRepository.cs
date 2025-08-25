using Okean_AnimeMovie.Core.Entities;

namespace Okean_AnimeMovie.Core.Interfaces;

public interface IAnimeRepository : IGenericRepository<Anime>
{
    Task<IEnumerable<Anime>> GetTrendingAnimeAsync(int take = 10);
    Task<IEnumerable<Anime>> SearchAnimeAsync(string searchTerm, int? genreId = null, int? year = null, int page = 1, int pageSize = 20);
    Task<Anime?> GetAnimeWithDetailsAsync(int id);
    Task<IEnumerable<Anime>> GetAnimeByGenreAsync(int genreId, int page = 1, int pageSize = 20);
    Task<int> GetTotalAnimeCountAsync(string? searchTerm = null, int? genreId = null, int? year = null);
    Task IncrementViewCountAsync(int animeId);
}
