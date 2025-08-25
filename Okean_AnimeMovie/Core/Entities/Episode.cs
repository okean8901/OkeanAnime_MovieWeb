namespace Okean_AnimeMovie.Core.Entities;

public class Episode
{
    public int Id { get; set; }
    public int AnimeId { get; set; }
    public int EpisodeNumber { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Thumbnail { get; set; }
    public string VideoUrl { get; set; } = string.Empty;
    public string VideoType { get; set; } = "Embed"; // Embed, HLS, Direct
    public int Duration { get; set; } // in seconds
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    public virtual Anime Anime { get; set; } = null!;
}
