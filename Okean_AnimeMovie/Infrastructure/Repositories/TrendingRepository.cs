using Microsoft.EntityFrameworkCore;
using Okean_AnimeMovie.Core.Entities;
using Okean_AnimeMovie.Core.DTOs;
using Okean_AnimeMovie.Core.Interfaces;
using Okean_AnimeMovie.Infrastructure.Data;

namespace Okean_AnimeMovie.Infrastructure.Repositories;

public class TrendingRepository : ITrendingRepository
{
    private readonly ApplicationDbContext _context;

    public TrendingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TrendingDto>> GetTrendingAnimeAsync(TrendingFilterDto filter)
    {
        var query = _context.Animes
            .Include(a => a.AnimeGenres)
            .ThenInclude(ag => ag.Genre)
            .AsQueryable();

        // Apply filters
        if (!string.IsNullOrEmpty(filter.SearchTerm))
        {
            query = query.Where(a => a.Title.Contains(filter.SearchTerm) || 
                                   a.Description!.Contains(filter.SearchTerm));
        }

        if (!string.IsNullOrEmpty(filter.Genre))
        {
            query = query.Where(a => a.AnimeGenres.Any(ag => ag.Genre.Name == filter.Genre));
        }

        if (!string.IsNullOrEmpty(filter.Year))
        {
            if (int.TryParse(filter.Year, out int year))
            {
                query = query.Where(a => a.ReleaseYear == year);
            }
        }

        // Apply trending type filter and select with counts
        var queryWithCounts = query.Select(a => new
        {
            Anime = a,
            ViewCount = _context.ViewCounts.Count(vc => vc.AnimeId == a.Id),
            RatingCount = _context.Ratings.Count(r => r.AnimeId == a.Id),
            CommentCount = _context.Comments.Count(c => c.AnimeId == a.Id && c.IsActive),
            FavoriteCount = _context.Favorites.Count(f => f.AnimeId == a.Id)
        });

        var orderedQuery = filter.Type switch
        {
            TrendingType.MostViewed => queryWithCounts.OrderByDescending(x => x.ViewCount),
            TrendingType.TopRated => queryWithCounts.Where(x => x.Anime.Rating > 0).OrderByDescending(x => x.Anime.Rating).ThenByDescending(x => x.RatingCount),
            TrendingType.MostCommented => queryWithCounts.OrderByDescending(x => x.CommentCount),
            TrendingType.MostFavorited => queryWithCounts.OrderByDescending(x => x.FavoriteCount),
            TrendingType.RecentlyAdded => queryWithCounts.OrderByDescending(x => x.Anime.CreatedAt),
            TrendingType.RecentlyUpdated => queryWithCounts.OrderByDescending(x => x.Anime.UpdatedAt ?? x.Anime.CreatedAt),
            _ => queryWithCounts.OrderByDescending(x => x.Anime.CreatedAt)
        };

        var skip = (filter.Page - 1) * filter.PageSize;
        var animes = await orderedQuery.Skip(skip).Take(filter.PageSize).ToListAsync();

        return animes.Select(x => new TrendingDto
        {
            Id = x.Anime.Id,
            Title = x.Anime.Title,
            Description = x.Anime.Description,
            CoverImage = x.Anime.Poster,
            Type = x.Anime.Type,
            ReleaseYear = x.Anime.ReleaseYear,
            Rating = x.Anime.Rating,
            ViewCount = x.ViewCount,
            RatingCount = x.RatingCount,
            CommentCount = x.CommentCount,
            FavoriteCount = x.FavoriteCount,
            LastUpdated = x.Anime.UpdatedAt ?? x.Anime.CreatedAt,
            Genres = x.Anime.AnimeGenres.Select(ag => ag.Genre.Name).ToArray(),
            TrendingType = filter.Type ?? TrendingType.MostViewed,
            TrendingScore = (x.ViewCount * 1) + (x.RatingCount * 2) + (x.CommentCount * 3) + (x.FavoriteCount * 4)
        });
    }

    public async Task<TrendingSummaryDto> GetTrendingSummaryAsync(int count = 10)
    {
        return new TrendingSummaryDto
        {
            MostViewed = (await GetMostViewedAnimeAsync(count)).ToList(),
            TopRated = (await GetTopRatedAnimeAsync(count)).ToList(),
            MostCommented = (await GetMostCommentedAnimeAsync(count)).ToList(),
            RecentlyAdded = (await GetRecentlyAddedAnimeAsync(count)).ToList(),
            TrendingThisWeek = (await GetTrendingThisWeekAsync(count)).ToList()
        };
    }

    public async Task<IEnumerable<TrendingDto>> GetMostViewedAnimeAsync(int count = 20)
    {
        var animes = await _context.Animes
            .Include(a => a.AnimeGenres)
            .ThenInclude(ag => ag.Genre)
            .Select(a => new
            {
                Anime = a,
                ViewCount = _context.ViewCounts.Count(vc => vc.AnimeId == a.Id),
                RatingCount = _context.Ratings.Count(r => r.AnimeId == a.Id),
                CommentCount = _context.Comments.Count(c => c.AnimeId == a.Id && c.IsActive),
                FavoriteCount = _context.Favorites.Count(f => f.AnimeId == a.Id)
            })
            .OrderByDescending(x => x.ViewCount)
            .Take(count)
            .ToListAsync();

        return animes.Select(x => new TrendingDto
        {
            Id = x.Anime.Id,
            Title = x.Anime.Title,
            Description = x.Anime.Description,
            CoverImage = x.Anime.Poster,
            Type = x.Anime.Type,
            ReleaseYear = x.Anime.ReleaseYear,
            Rating = x.Anime.Rating,
            ViewCount = x.ViewCount,
            RatingCount = x.RatingCount,
            CommentCount = x.CommentCount,
            FavoriteCount = x.FavoriteCount,
            LastUpdated = x.Anime.UpdatedAt ?? x.Anime.CreatedAt,
            Genres = x.Anime.AnimeGenres.Select(ag => ag.Genre.Name).ToArray(),
            TrendingType = TrendingType.MostViewed,
            TrendingScore = (x.ViewCount * 1) + (x.RatingCount * 2) + (x.CommentCount * 3) + (x.FavoriteCount * 4)
        });
    }

    public async Task<IEnumerable<TrendingDto>> GetTopRatedAnimeAsync(int count = 20)
    {
        var animes = await _context.Animes
            .Include(a => a.AnimeGenres)
            .ThenInclude(ag => ag.Genre)
            .Where(a => a.Rating > 0)
            .Select(a => new
            {
                Anime = a,
                ViewCount = _context.ViewCounts.Count(vc => vc.AnimeId == a.Id),
                RatingCount = _context.Ratings.Count(r => r.AnimeId == a.Id),
                CommentCount = _context.Comments.Count(c => c.AnimeId == a.Id && c.IsActive),
                FavoriteCount = _context.Favorites.Count(f => f.AnimeId == a.Id)
            })
            .OrderByDescending(x => x.Anime.Rating)
            .ThenByDescending(x => x.RatingCount)
            .Take(count)
            .ToListAsync();

        return animes.Select(x => new TrendingDto
        {
            Id = x.Anime.Id,
            Title = x.Anime.Title,
            Description = x.Anime.Description,
            CoverImage = x.Anime.Poster,
            Type = x.Anime.Type,
            ReleaseYear = x.Anime.ReleaseYear,
            Rating = x.Anime.Rating,
            ViewCount = x.ViewCount,
            RatingCount = x.RatingCount,
            CommentCount = x.CommentCount,
            FavoriteCount = x.FavoriteCount,
            LastUpdated = x.Anime.UpdatedAt ?? x.Anime.CreatedAt,
            Genres = x.Anime.AnimeGenres.Select(ag => ag.Genre.Name).ToArray(),
            TrendingType = TrendingType.TopRated,
            TrendingScore = (x.ViewCount * 1) + (x.RatingCount * 2) + (x.CommentCount * 3) + (x.FavoriteCount * 4)
        });
    }

    public async Task<IEnumerable<TrendingDto>> GetMostCommentedAnimeAsync(int count = 20)
    {
        var animes = await _context.Animes
            .Include(a => a.AnimeGenres)
            .ThenInclude(ag => ag.Genre)
            .Select(a => new
            {
                Anime = a,
                ViewCount = _context.ViewCounts.Count(vc => vc.AnimeId == a.Id),
                RatingCount = _context.Ratings.Count(r => r.AnimeId == a.Id),
                CommentCount = _context.Comments.Count(c => c.AnimeId == a.Id && c.IsActive),
                FavoriteCount = _context.Favorites.Count(f => f.AnimeId == a.Id)
            })
            .OrderByDescending(x => x.CommentCount)
            .Take(count)
            .ToListAsync();

        return animes.Select(x => new TrendingDto
        {
            Id = x.Anime.Id,
            Title = x.Anime.Title,
            Description = x.Anime.Description,
            CoverImage = x.Anime.Poster,
            Type = x.Anime.Type,
            ReleaseYear = x.Anime.ReleaseYear,
            Rating = x.Anime.Rating,
            ViewCount = x.ViewCount,
            RatingCount = x.RatingCount,
            CommentCount = x.CommentCount,
            FavoriteCount = x.FavoriteCount,
            LastUpdated = x.Anime.UpdatedAt ?? x.Anime.CreatedAt,
            Genres = x.Anime.AnimeGenres.Select(ag => ag.Genre.Name).ToArray(),
            TrendingType = TrendingType.MostCommented,
            TrendingScore = (x.ViewCount * 1) + (x.RatingCount * 2) + (x.CommentCount * 3) + (x.FavoriteCount * 4)
        });
    }

    public async Task<IEnumerable<TrendingDto>> GetMostFavoritedAnimeAsync(int count = 20)
    {
        var animes = await _context.Animes
            .Include(a => a.AnimeGenres)
            .ThenInclude(ag => ag.Genre)
            .Select(a => new
            {
                Anime = a,
                ViewCount = _context.ViewCounts.Count(vc => vc.AnimeId == a.Id),
                RatingCount = _context.Ratings.Count(r => r.AnimeId == a.Id),
                CommentCount = _context.Comments.Count(c => c.AnimeId == a.Id && c.IsActive),
                FavoriteCount = _context.Favorites.Count(f => f.AnimeId == a.Id)
            })
            .OrderByDescending(x => x.FavoriteCount)
            .Take(count)
            .ToListAsync();

        return animes.Select(x => new TrendingDto
        {
            Id = x.Anime.Id,
            Title = x.Anime.Title,
            Description = x.Anime.Description,
            CoverImage = x.Anime.Poster,
            Type = x.Anime.Type,
            ReleaseYear = x.Anime.ReleaseYear,
            Rating = x.Anime.Rating,
            ViewCount = x.ViewCount,
            RatingCount = x.RatingCount,
            CommentCount = x.CommentCount,
            FavoriteCount = x.FavoriteCount,
            LastUpdated = x.Anime.UpdatedAt ?? x.Anime.CreatedAt,
            Genres = x.Anime.AnimeGenres.Select(ag => ag.Genre.Name).ToArray(),
            TrendingType = TrendingType.MostFavorited,
            TrendingScore = (x.ViewCount * 1) + (x.RatingCount * 2) + (x.CommentCount * 3) + (x.FavoriteCount * 4)
        });
    }

    public async Task<IEnumerable<TrendingDto>> GetRecentlyAddedAnimeAsync(int count = 20)
    {
        var animes = await _context.Animes
            .Include(a => a.AnimeGenres)
            .ThenInclude(ag => ag.Genre)
            .Select(a => new
            {
                Anime = a,
                ViewCount = _context.ViewCounts.Count(vc => vc.AnimeId == a.Id),
                RatingCount = _context.Ratings.Count(r => r.AnimeId == a.Id),
                CommentCount = _context.Comments.Count(c => c.AnimeId == a.Id && c.IsActive),
                FavoriteCount = _context.Favorites.Count(f => f.AnimeId == a.Id)
            })
            .OrderByDescending(x => x.Anime.CreatedAt)
            .Take(count)
            .ToListAsync();

        return animes.Select(x => new TrendingDto
        {
            Id = x.Anime.Id,
            Title = x.Anime.Title,
            Description = x.Anime.Description,
            CoverImage = x.Anime.Poster,
            Type = x.Anime.Type,
            ReleaseYear = x.Anime.ReleaseYear,
            Rating = x.Anime.Rating,
            ViewCount = x.ViewCount,
            RatingCount = x.RatingCount,
            CommentCount = x.CommentCount,
            FavoriteCount = x.FavoriteCount,
            LastUpdated = x.Anime.UpdatedAt ?? x.Anime.CreatedAt,
            Genres = x.Anime.AnimeGenres.Select(ag => ag.Genre.Name).ToArray(),
            TrendingType = TrendingType.RecentlyAdded,
            TrendingScore = (x.ViewCount * 1) + (x.RatingCount * 2) + (x.CommentCount * 3) + (x.FavoriteCount * 4)
        });
    }

    public async Task<IEnumerable<TrendingDto>> GetRecentlyUpdatedAnimeAsync(int count = 20)
    {
        var animes = await _context.Animes
            .Include(a => a.AnimeGenres)
            .ThenInclude(ag => ag.Genre)
            .Select(a => new
            {
                Anime = a,
                ViewCount = _context.ViewCounts.Count(vc => vc.AnimeId == a.Id),
                RatingCount = _context.Ratings.Count(r => r.AnimeId == a.Id),
                CommentCount = _context.Comments.Count(c => c.AnimeId == a.Id && c.IsActive),
                FavoriteCount = _context.Favorites.Count(f => f.AnimeId == a.Id)
            })
            .OrderByDescending(x => x.Anime.UpdatedAt ?? x.Anime.CreatedAt)
            .Take(count)
            .ToListAsync();

        return animes.Select(x => new TrendingDto
        {
            Id = x.Anime.Id,
            Title = x.Anime.Title,
            Description = x.Anime.Description,
            CoverImage = x.Anime.Poster,
            Type = x.Anime.Type,
            ReleaseYear = x.Anime.ReleaseYear,
            Rating = x.Anime.Rating,
            ViewCount = x.ViewCount,
            RatingCount = x.RatingCount,
            CommentCount = x.CommentCount,
            FavoriteCount = x.FavoriteCount,
            LastUpdated = x.Anime.UpdatedAt ?? x.Anime.CreatedAt,
            Genres = x.Anime.AnimeGenres.Select(ag => ag.Genre.Name).ToArray(),
            TrendingType = TrendingType.RecentlyUpdated,
            TrendingScore = (x.ViewCount * 1) + (x.RatingCount * 2) + (x.CommentCount * 3) + (x.FavoriteCount * 4)
        });
    }

    public async Task<IEnumerable<TrendingDto>> GetTrendingThisWeekAsync(int count = 20)
    {
        var weekAgo = DateTime.UtcNow.AddDays(-7);
        
        var animes = await _context.Animes
            .Include(a => a.AnimeGenres)
            .ThenInclude(ag => ag.Genre)
            .Select(a => new
            {
                Anime = a,
                ViewCount = _context.ViewCounts.Count(vc => vc.AnimeId == a.Id),
                RatingCount = _context.Ratings.Count(r => r.AnimeId == a.Id),
                CommentCount = _context.Comments.Count(c => c.AnimeId == a.Id && c.IsActive),
                FavoriteCount = _context.Favorites.Count(f => f.AnimeId == a.Id)
            })
            .OrderByDescending(x => (x.ViewCount * 1) + (x.RatingCount * 2) + (x.CommentCount * 3) + (x.FavoriteCount * 4))
            .Take(count)
            .ToListAsync();

        return animes.Select(x => new TrendingDto
        {
            Id = x.Anime.Id,
            Title = x.Anime.Title,
            Description = x.Anime.Description,
            CoverImage = x.Anime.Poster,
            Type = x.Anime.Type,
            ReleaseYear = x.Anime.ReleaseYear,
            Rating = x.Anime.Rating,
            ViewCount = x.ViewCount,
            RatingCount = x.RatingCount,
            CommentCount = x.CommentCount,
            FavoriteCount = x.FavoriteCount,
            LastUpdated = x.Anime.UpdatedAt ?? x.Anime.CreatedAt,
            Genres = x.Anime.AnimeGenres.Select(ag => ag.Genre.Name).ToArray(),
            TrendingType = TrendingType.MostViewed,
            TrendingScore = (x.ViewCount * 1) + (x.RatingCount * 2) + (x.CommentCount * 3) + (x.FavoriteCount * 4)
        });
    }

    public async Task<IEnumerable<TrendingDto>> GetTrendingByGenreAsync(string genre, int count = 20)
    {
        var animes = await _context.Animes
            .Include(a => a.AnimeGenres)
            .ThenInclude(ag => ag.Genre)
            .Where(a => a.AnimeGenres.Any(ag => ag.Genre.Name == genre))
            .Select(a => new
            {
                Anime = a,
                ViewCount = _context.ViewCounts.Count(vc => vc.AnimeId == a.Id),
                RatingCount = _context.Ratings.Count(r => r.AnimeId == a.Id),
                CommentCount = _context.Comments.Count(c => c.AnimeId == a.Id && c.IsActive),
                FavoriteCount = _context.Favorites.Count(f => f.AnimeId == a.Id)
            })
            .OrderByDescending(x => (x.ViewCount * 1) + (x.RatingCount * 2) + (x.CommentCount * 3) + (x.FavoriteCount * 4))
            .Take(count)
            .ToListAsync();

        return animes.Select(x => new TrendingDto
        {
            Id = x.Anime.Id,
            Title = x.Anime.Title,
            Description = x.Anime.Description,
            CoverImage = x.Anime.Poster,
            Type = x.Anime.Type,
            ReleaseYear = x.Anime.ReleaseYear,
            Rating = x.Anime.Rating,
            ViewCount = x.ViewCount,
            RatingCount = x.RatingCount,
            CommentCount = x.CommentCount,
            FavoriteCount = x.FavoriteCount,
            LastUpdated = x.Anime.UpdatedAt ?? x.Anime.CreatedAt,
            Genres = x.Anime.AnimeGenres.Select(ag => ag.Genre.Name).ToArray(),
            TrendingType = TrendingType.MostViewed,
            TrendingScore = (x.ViewCount * 1) + (x.RatingCount * 2) + (x.CommentCount * 3) + (x.FavoriteCount * 4)
        });
    }

    public async Task<IEnumerable<TrendingDto>> GetTrendingByYearAsync(int year, int count = 20)
    {
        var animes = await _context.Animes
            .Include(a => a.AnimeGenres)
            .ThenInclude(ag => ag.Genre)
            .Where(a => a.ReleaseYear == year)
            .Select(a => new
            {
                Anime = a,
                ViewCount = _context.ViewCounts.Count(vc => vc.AnimeId == a.Id),
                RatingCount = _context.Ratings.Count(r => r.AnimeId == a.Id),
                CommentCount = _context.Comments.Count(c => c.AnimeId == a.Id && c.IsActive),
                FavoriteCount = _context.Favorites.Count(f => f.AnimeId == a.Id)
            })
            .OrderByDescending(x => (x.ViewCount * 1) + (x.RatingCount * 2) + (x.CommentCount * 3) + (x.FavoriteCount * 4))
            .Take(count)
            .ToListAsync();

        return animes.Select(x => new TrendingDto
        {
            Id = x.Anime.Id,
            Title = x.Anime.Title,
            Description = x.Anime.Description,
            CoverImage = x.Anime.Poster,
            Type = x.Anime.Type,
            ReleaseYear = x.Anime.ReleaseYear,
            Rating = x.Anime.Rating,
            ViewCount = x.ViewCount,
            RatingCount = x.RatingCount,
            CommentCount = x.CommentCount,
            FavoriteCount = x.FavoriteCount,
            LastUpdated = x.Anime.UpdatedAt ?? x.Anime.CreatedAt,
            Genres = x.Anime.AnimeGenres.Select(ag => ag.Genre.Name).ToArray(),
            TrendingType = TrendingType.MostViewed,
            TrendingScore = (x.ViewCount * 1) + (x.RatingCount * 2) + (x.CommentCount * 3) + (x.FavoriteCount * 4)
        });
    }

    public async Task<int> GetTrendingCountAsync(TrendingFilterDto filter)
    {
        var query = _context.Animes.AsQueryable();

        if (!string.IsNullOrEmpty(filter.SearchTerm))
        {
            query = query.Where(a => a.Title.Contains(filter.SearchTerm) || 
                                   a.Description!.Contains(filter.SearchTerm));
        }

        if (!string.IsNullOrEmpty(filter.Genre))
        {
            query = query.Where(a => a.AnimeGenres.Any(ag => ag.Genre.Name == filter.Genre));
        }

        if (!string.IsNullOrEmpty(filter.Year))
        {
            if (int.TryParse(filter.Year, out int year))
            {
                query = query.Where(a => a.ReleaseYear == year);
            }
        }

        return await query.CountAsync();
    }

    public async Task UpdateAnimeViewCountAsync(int animeId)
    {
        // This method will be called when a user views an anime
        // Implementation will be in the controller
    }

    public async Task UpdateAnimeTrendingScoreAsync(int animeId)
    {
        // This method will be called periodically to update trending scores
        // Implementation will be in a background service
    }


}
