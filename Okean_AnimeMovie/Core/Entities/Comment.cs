namespace Okean_AnimeMovie.Core.Entities;

public class Comment
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int AnimeId { get; set; }
    public int? ParentCommentId { get; set; } // For replies
    public string Content { get; set; } = string.Empty;
    public bool IsEdited { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public int LikeCount { get; set; } = 0;
    public int DislikeCount { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    public virtual ApplicationUser User { get; set; } = null!;
    public virtual Anime Anime { get; set; } = null!;
    public virtual Comment? ParentComment { get; set; }
    public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();
}
