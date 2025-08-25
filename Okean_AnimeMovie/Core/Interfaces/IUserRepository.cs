using Okean_AnimeMovie.Core.Entities;

namespace Okean_AnimeMovie.Core.Interfaces;

public interface IUserRepository : IGenericRepository<ApplicationUser>
{
    Task<ApplicationUser?> GetByEmailAsync(string email);
    Task<ApplicationUser?> GetByUsernameAsync(string username);
    Task<IEnumerable<ApplicationUser>> GetUsersAsync(int page = 1, int pageSize = 20);
    Task<bool> IsEmailUniqueAsync(string email);
    Task<bool> IsUsernameUniqueAsync(string username);
    Task UpdateLastLoginAsync(string userId);
    Task ToggleUserStatusAsync(string userId);
}
