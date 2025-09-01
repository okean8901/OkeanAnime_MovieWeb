using Microsoft.EntityFrameworkCore;
using Okean_AnimeMovie.Core.Entities;
using Okean_AnimeMovie.Core.Interfaces;
using Okean_AnimeMovie.Infrastructure.Data;

namespace Okean_AnimeMovie.Infrastructure.Repositories;

public class AnimeRepository : GenericRepository<Anime>, IAnimeRepository
{
    public AnimeRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Anime>> GetTrendingAnimeAsync(int take = 10)
    {
        return await _dbSet
            .Include(a => a.AnimeGenres)
            .ThenInclude(ag => ag.Genre)
            .OrderByDescending(a => a.ViewCount)
            .Take(take)
            .ToListAsync();
    }

    public async Task<IEnumerable<Anime>> SearchAnimeAsync(string searchTerm, int? genreId = null, int? year = null, string? sortBy = null, int page = 1, int pageSize = 20)
    {
        var query = _dbSet
            .Include(a => a.AnimeGenres)
            .ThenInclude(ag => ag.Genre)
            .Include(a => a.Ratings)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(a => a.Title.Contains(searchTerm) || 
                                   a.AlternativeTitle!.Contains(searchTerm) ||
                                   a.Description!.Contains(searchTerm));
        }

        if (genreId.HasValue)
        {
            query = query.Where(a => a.AnimeGenres.Any(ag => ag.GenreId == genreId.Value));
        }

        if (year.HasValue)
        {
            query = query.Where(a => a.ReleaseYear == year.Value);
        }

        // Apply sorting
        query = sortBy switch
        {
            "title" => query.OrderBy(a => a.Title),
            "title_desc" => query.OrderByDescending(a => a.Title),
            "year" => query.OrderByDescending(a => a.ReleaseYear),
            "rating" => query.OrderByDescending(a => a.Ratings.Average(r => r.Score)),
            "views" => query.OrderByDescending(a => a.ViewCount),
            "newest" => query.OrderByDescending(a => a.CreatedAt),
            _ => query.OrderByDescending(a => a.ViewCount)
        };

        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Anime?> GetAnimeWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(a => a.AnimeGenres)
            .ThenInclude(ag => ag.Genre)
            .Include(a => a.Episodes.OrderBy(e => e.EpisodeNumber))
            .Include(a => a.Comments.Where(c => c.IsActive).OrderByDescending(c => c.CreatedAt))
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Anime>> GetAnimeByGenreAsync(int genreId, int page = 1, int pageSize = 20)
    {
        return await _dbSet
            .Include(a => a.AnimeGenres)
            .ThenInclude(ag => ag.Genre)
            .Where(a => a.AnimeGenres.Any(ag => ag.GenreId == genreId))
            .OrderByDescending(a => a.ViewCount)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalAnimeCountAsync(string? searchTerm = null, int? genreId = null, int? year = null)
    {
        var query = _dbSet.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(a => a.Title.Contains(searchTerm) || 
                                   a.AlternativeTitle!.Contains(searchTerm) ||
                                   a.Description!.Contains(searchTerm));
        }

        if (genreId.HasValue)
        {
            query = query.Where(a => a.AnimeGenres.Any(ag => ag.GenreId == genreId.Value));
        }

        if (year.HasValue)
        {
            query = query.Where(a => a.ReleaseYear == year.Value);
        }

        return await query.CountAsync();
    }

    public async Task IncrementViewCountAsync(int animeId)
    {
        var anime = await _dbSet.FindAsync(animeId);
        if (anime != null)
        {
            anime.ViewCount++;
            await _context.SaveChangesAsync();
        }
    }
}
