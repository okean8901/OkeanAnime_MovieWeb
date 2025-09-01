using Okean_AnimeMovie.Core.Entities;

namespace Okean_AnimeMovie.Application.Services.Cache;

public interface IAnimeCacheService
{
    Task<IEnumerable<Anime>?> GetTrendingAnimeAsync(int take = 10);
    Task SetTrendingAnimeAsync(IEnumerable<Anime> animes, int take = 10);
    Task<IEnumerable<Anime>?> GetAnimeByGenreAsync(int genreId, int page = 1, int pageSize = 20);
    Task SetAnimeByGenreAsync(int genreId, int page, int pageSize, IEnumerable<Anime> animes);
    Task<Anime?> GetAnimeByIdAsync(int id);
    Task SetAnimeByIdAsync(int id, Anime anime);
    Task ClearAnimeCacheAsync(int? animeId = null);
    Task ClearGenreCacheAsync(int? genreId = null);
}
