namespace Okean_AnimeMovie.Core.Entities;

public class Rating
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int AnimeId { get; set; }
    public int Score { get; set; } // 1-10
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    public virtual ApplicationUser User { get; set; } = null!;
    public virtual Anime Anime { get; set; } = null!;
}
