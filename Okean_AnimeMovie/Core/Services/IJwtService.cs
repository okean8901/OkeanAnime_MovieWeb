using Okean_AnimeMovie.Core.Entities;

namespace Okean_AnimeMovie.Core.Services;

public interface IJwtService
{
    string GenerateToken(ApplicationUser user, IList<string> roles);
    bool ValidateToken(string token);
    string? GetUserIdFromToken(string token);
}
