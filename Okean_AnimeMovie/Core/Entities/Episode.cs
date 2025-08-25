using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Okean_AnimeMovie.Core.Entities;

public class Episode
{
    public int Id { get; set; }

    [Required]
    public int AnimeId { get; set; }

    [Range(1, 100000)]
    public int EpisodeNumber { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Url]
    [StringLength(500)]
    public string? Thumbnail { get; set; }

    [Required]
    [StringLength(1000)]
    public string VideoUrl { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string VideoType { get; set; } = "Embed"; // Embed, HLS, Direct

    [Range(0, 1000000)]
    public int Duration { get; set; } // in seconds

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    [ValidateNever]
    public virtual Anime? Anime { get; set; }
}
