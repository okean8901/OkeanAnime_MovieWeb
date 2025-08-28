namespace Okean_AnimeMovie.Core.Entities;

public class ViewHistory
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int AnimeId { get; set; }
    public int EpisodeId { get; set; }
    public DateTime WatchedAt { get; set; } = DateTime.UtcNow;
    public int WatchDuration { get; set; } // Thời gian xem (giây)
    public bool IsCompleted { get; set; } = false; // Đã xem hết tập chưa
    
    // Navigation properties
    public virtual ApplicationUser User { get; set; } = null!;
    public virtual Anime Anime { get; set; } = null!;
    public virtual Episode Episode { get; set; } = null!;
}
