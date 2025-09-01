namespace Okean_AnimeMovie.Core.Entities;

public class Anime
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? AlternativeTitle { get; set; }
    public string? Description { get; set; }
    public string? Poster { get; set; }
    public string? Trailer { get; set; }
    public int ReleaseYear { get; set; }
    public int TotalEpisodes { get; set; }
    public string Status { get; set; } = "Ongoing"; // Ongoing, Completed, Upcoming
    public string Type { get; set; } = "TV"; // TV, Movie, OVA, Special
    public decimal Rating { get; set; }
    public int ViewCount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    public virtual ICollection<AnimeGenre> AnimeGenres { get; set; } = new List<AnimeGenre>();
    public virtual ICollection<Episode> Episodes { get; set; } = new List<Episode>();
    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
