namespace Okean_AnimeMovie.Core.Entities;

public class Favorite
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int AnimeId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual ApplicationUser User { get; set; } = null!;
    public virtual Anime Anime { get; set; } = null!;
}
