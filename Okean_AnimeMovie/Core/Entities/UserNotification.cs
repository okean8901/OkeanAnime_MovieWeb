using System.ComponentModel.DataAnnotations;

namespace Okean_AnimeMovie.Core.Entities;

public class UserNotification
{
    public int Id { get; set; }
    
    [Required]
    public string UserId { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Message { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Type { get; set; } = string.Empty; // "episode", "update", "welcome", "password_reset"
    
    public int? AnimeId { get; set; }
    public int? EpisodeNumber { get; set; }
    
    public bool IsRead { get; set; } = false;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ReadAt { get; set; }
    
    // Navigation properties
    public ApplicationUser User { get; set; } = null!;
    public Anime? Anime { get; set; }
}
