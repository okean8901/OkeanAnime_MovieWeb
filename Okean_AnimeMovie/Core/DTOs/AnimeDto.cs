using System.ComponentModel.DataAnnotations;

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
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(200)]
    public string? AlternativeTitle { get; set; }

    [StringLength(2000)]
    public string? Description { get; set; }

    [Url]
    [StringLength(500)]
    public string? Poster { get; set; }

    [Url]
    [StringLength(500)]
    public string? Trailer { get; set; }

    [Range(1900, 2100)]
    public int ReleaseYear { get; set; }

    [Range(0, 10000)]
    public int TotalEpisodes { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Ongoing";

    [Required]
    [StringLength(20)]
    public string Type { get; set; } = "TV";

    public List<int> GenreIds { get; set; } = new List<int>();
}

public class UpdateAnimeDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(200)]
    public string? AlternativeTitle { get; set; }

    [StringLength(2000)]
    public string? Description { get; set; }

    [Url]
    [StringLength(500)]
    public string? Poster { get; set; }

    [Url]
    [StringLength(500)]
    public string? Trailer { get; set; }

    [Range(1900, 2100)]
    public int ReleaseYear { get; set; }

    [Range(0, 10000)]
    public int TotalEpisodes { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Ongoing";

    [Required]
    [StringLength(20)]
    public string Type { get; set; } = "TV";

    public List<int> GenreIds { get; set; } = new List<int>();
}
