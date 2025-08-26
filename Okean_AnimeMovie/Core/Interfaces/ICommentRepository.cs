using Okean_AnimeMovie.Core.Entities;
using Okean_AnimeMovie.Core.DTOs;

namespace Okean_AnimeMovie.Core.Interfaces;

public interface ICommentRepository : IGenericRepository<Comment>
{
    Task<IEnumerable<CommentDto>> GetAnimeCommentsAsync(int animeId, int page = 1, int pageSize = 20);
    Task<CommentDto?> GetCommentByIdAsync(int commentId, string? userId = null);
    Task<IEnumerable<CommentDto>> GetCommentRepliesAsync(int commentId, int page = 1, int pageSize = 10);
    Task<int> GetCommentCountAsync(int animeId);
    Task<int> GetReplyCountAsync(int commentId);
    Task<bool> HasUserLikedCommentAsync(string userId, int commentId);
    Task<bool> HasUserDislikedCommentAsync(string userId, int commentId);
    Task<IEnumerable<CommentDto>> GetUserCommentsAsync(string userId, int page = 1, int pageSize = 20);
    Task<IEnumerable<CommentDto>> GetRecentCommentsAsync(int count = 10);
    Task<int> SaveChangesAsync();
}
