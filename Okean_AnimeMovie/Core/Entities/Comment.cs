namespace Okean_AnimeMovie.Core.Entities;

public class Comment
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int AnimeId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public virtual ApplicationUser User { get; set; } = null!;
    public virtual Anime Anime { get; set; } = null!;
}
