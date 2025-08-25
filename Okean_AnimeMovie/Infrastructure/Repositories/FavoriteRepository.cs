using Microsoft.EntityFrameworkCore;
using Okean_AnimeMovie.Core.Entities;
using Okean_AnimeMovie.Core.Interfaces;
using Okean_AnimeMovie.Infrastructure.Data;

namespace Okean_AnimeMovie.Infrastructure.Repositories;

public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
{
    public FavoriteRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Anime>> GetUserFavoritesAsync(string userId, int page = 1, int pageSize = 20)
    {
        return await _context.Set<Favorite>()
            .Where(f => f.UserId == userId)
            .Include(f => f.Anime)
                .ThenInclude(a => a.AnimeGenres)
                    .ThenInclude(ag => ag.Genre)
            .OrderByDescending(f => f.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(f => f.Anime)
            .ToListAsync();
    }

    public async Task<bool> IsFavoriteAsync(string userId, int animeId)
    {
        return await _context.Set<Favorite>().AnyAsync(f => f.UserId == userId && f.AnimeId == animeId);
    }

    public async Task AddToFavoritesAsync(string userId, int animeId)
    {
        if (await IsFavoriteAsync(userId, animeId)) return;
        await _context.Set<Favorite>().AddAsync(new Favorite { UserId = userId, AnimeId = animeId });
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFromFavoritesAsync(string userId, int animeId)
    {
        var fav = await _context.Set<Favorite>().FirstOrDefaultAsync(f => f.UserId == userId && f.AnimeId == animeId);
        if (fav != null)
        {
            _context.Set<Favorite>().Remove(fav);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetFavoriteCountAsync(string userId)
    {
        return await _context.Set<Favorite>().CountAsync(f => f.UserId == userId);
    }
}
