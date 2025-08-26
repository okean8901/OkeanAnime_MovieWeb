namespace Okean_AnimeMovie.Core.Entities;

public class ViewCount
{
    public int Id { get; set; }
    public int AnimeId { get; set; }
    public string? UserId { get; set; } // Null for anonymous users
    public string IpAddress { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public DateTime ViewedAt { get; set; } = DateTime.UtcNow;
    public string? Referrer { get; set; }
    
    // Navigation properties
    public virtual Anime Anime { get; set; } = null!;
    public virtual ApplicationUser? User { get; set; }
}
