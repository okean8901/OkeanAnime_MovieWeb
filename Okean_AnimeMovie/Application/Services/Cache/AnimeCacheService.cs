using Microsoft.Extensions.Caching.Memory;
using Okean_AnimeMovie.Core.Entities;

namespace Okean_AnimeMovie.Application.Services.Cache;

public class AnimeCacheService : IAnimeCacheService
{
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(30);

    public AnimeCacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<IEnumerable<Anime>?> GetTrendingAnimeAsync(int take = 10)
    {
        var cacheKey = $"trending_anime_{take}";
        return await Task.FromResult(_cache.Get<IEnumerable<Anime>>(cacheKey));
    }

    public async Task SetTrendingAnimeAsync(IEnumerable<Anime> animes, int take = 10)
    {
        var cacheKey = $"trending_anime_{take}";
        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _defaultExpiration,
            SlidingExpiration = TimeSpan.FromMinutes(15)
        };
        
        _cache.Set(cacheKey, animes, cacheOptions);
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<Anime>?> GetAnimeByGenreAsync(int genreId, int page = 1, int pageSize = 20)
    {
        var cacheKey = $"genre_anime_{genreId}_{page}_{pageSize}";
        return await Task.FromResult(_cache.Get<IEnumerable<Anime>>(cacheKey));
    }

    public async Task SetAnimeByGenreAsync(int genreId, int page, int pageSize, IEnumerable<Anime> animes)
    {
        var cacheKey = $"genre_anime_{genreId}_{page}_{pageSize}";
        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _defaultExpiration,
            SlidingExpiration = TimeSpan.FromMinutes(15)
        };
        
        _cache.Set(cacheKey, animes, cacheOptions);
        await Task.CompletedTask;
    }

    public async Task<Anime?> GetAnimeByIdAsync(int id)
    {
        var cacheKey = $"anime_{id}";
        return await Task.FromResult(_cache.Get<Anime>(cacheKey));
    }

    public async Task SetAnimeByIdAsync(int id, Anime anime)
    {
        var cacheKey = $"anime_{id}";
        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2),
            SlidingExpiration = TimeSpan.FromMinutes(30)
        };
        
        _cache.Set(cacheKey, anime, cacheOptions);
        await Task.CompletedTask;
    }

    public async Task ClearAnimeCacheAsync(int? animeId = null)
    {
        if (animeId.HasValue)
        {
            var cacheKey = $"anime_{animeId.Value}";
            _cache.Remove(cacheKey);
        }
        else
        {
            // Clear all anime-related cache
            var cacheKeys = new List<string>();
            for (int i = 1; i <= 100; i++) // Assuming max 100 anime
            {
                cacheKeys.Add($"anime_{i}");
            }
            
            foreach (var key in cacheKeys)
            {
                _cache.Remove(key);
            }
        }
        
        await Task.CompletedTask;
    }

    public async Task ClearGenreCacheAsync(int? genreId = null)
    {
        if (genreId.HasValue)
        {
            // Clear specific genre cache
            for (int page = 1; page <= 10; page++) // Assuming max 10 pages
            {
                for (int pageSize = 10; pageSize <= 50; pageSize += 10)
                {
                    var cacheKey = $"genre_anime_{genreId.Value}_{page}_{pageSize}";
                    _cache.Remove(cacheKey);
                }
            }
        }
        else
        {
            // Clear all genre cache
            for (int genre = 1; genre <= 50; genre++) // Assuming max 50 genres
            {
                for (int page = 1; page <= 10; page++)
                {
                    for (int pageSize = 10; pageSize <= 50; pageSize += 10)
                    {
                        var cacheKey = $"genre_anime_{genre}_{page}_{pageSize}";
                        _cache.Remove(cacheKey);
                    }
                }
            }
        }
        
        await Task.CompletedTask;
    }
}
