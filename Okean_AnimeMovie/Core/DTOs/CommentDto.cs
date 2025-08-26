namespace Okean_AnimeMovie.Core.DTOs;

public class CommentDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int AnimeId { get; set; }
    public int? ParentCommentId { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsEdited { get; set; }
    public bool IsActive { get; set; }
    public int LikeCount { get; set; }
    public int DislikeCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    // User info
    public string UserName { get; set; } = string.Empty;
    public string? UserAvatar { get; set; }
    
    // Reply info
    public List<CommentDto> Replies { get; set; } = new();
    public int ReplyCount { get; set; }
    
    // User interaction
    public bool IsLikedByUser { get; set; }
    public bool IsDislikedByUser { get; set; }
}

public class CreateCommentDto
{
    public int AnimeId { get; set; }
    public int? ParentCommentId { get; set; }
    public string Content { get; set; } = string.Empty;
}

public class UpdateCommentDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
}

public class CommentReactionDto
{
    public int CommentId { get; set; }
    public string ReactionType { get; set; } = string.Empty; // "like" or "dislike"
}
