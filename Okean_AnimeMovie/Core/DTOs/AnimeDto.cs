namespace Okean_AnimeMovie.Core.DTOs;

public class AnimeDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? AlternativeTitle { get; set; }
    public string? Description { get; set; }
    public string? Poster { get; set; }
    public string? Trailer { get; set; }
    public int ReleaseYear { get; set; }
    public int TotalEpisodes { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public double Rating { get; set; }
    public int ViewCount { get; set; }
    public List<string> Genres { get; set; } = new List<string>();
    public bool IsFavorite { get; set; }
    public int UserRating { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateAnimeDto
{
    public string Title { get; set; } = string.Empty;
    public string? AlternativeTitle { get; set; }
    public string? Description { get; set; }
    public string? Poster { get; set; }
    public string? Trailer { get; set; }
    public int ReleaseYear { get; set; }
    public int TotalEpisodes { get; set; }
    public string Status { get; set; } = "Ongoing";
    public string Type { get; set; } = "TV";
    public List<int> GenreIds { get; set; } = new List<int>();
}

public class UpdateAnimeDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? AlternativeTitle { get; set; }
    public string? Description { get; set; }
    public string? Poster { get; set; }
    public string? Trailer { get; set; }
    public int ReleaseYear { get; set; }
    public int TotalEpisodes { get; set; }
    public string Status { get; set; } = "Ongoing";
    public string Type { get; set; } = "TV";
    public List<int> GenreIds { get; set; } = new List<int>();
}
