using Microsoft.EntityFrameworkCore;
using Okean_AnimeMovie.Core.Entities;
using Okean_AnimeMovie.Core.DTOs;
using Okean_AnimeMovie.Core.Interfaces;
using Okean_AnimeMovie.Infrastructure.Data;

namespace Okean_AnimeMovie.Infrastructure.Repositories;

public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CommentDto>> GetAnimeCommentsAsync(int animeId, int page = 1, int pageSize = 20)
    {
        var skip = (page - 1) * pageSize;
        
        return await _context.Comments
            .Where(c => c.AnimeId == animeId && c.IsActive && c.ParentCommentId == null)
            .Include(c => c.User)
            .Include(c => c.Replies.Where(r => r.IsActive))
            .OrderByDescending(c => c.CreatedAt)
            .Skip(skip)
            .Take(pageSize)
            .Select(c => new CommentDto
            {
                Id = c.Id,
                UserId = c.UserId,
                AnimeId = c.AnimeId,
                ParentCommentId = c.ParentCommentId,
                Content = c.Content,
                IsEdited = c.IsEdited,
                IsActive = c.IsActive,
                LikeCount = c.LikeCount,
                DislikeCount = c.DislikeCount,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                UserName = c.User.UserName ?? c.User.Email ?? "Unknown",
                UserAvatar = null,
                ReplyCount = c.Replies.Count,
                Replies = c.Replies.Select(r => new CommentDto
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    AnimeId = r.AnimeId,
                    ParentCommentId = r.ParentCommentId,
                    Content = r.Content,
                    IsEdited = r.IsEdited,
                    IsActive = r.IsActive,
                    LikeCount = r.LikeCount,
                    DislikeCount = r.DislikeCount,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                    UserName = r.User.UserName ?? r.User.Email ?? "Unknown",
                    UserAvatar = null,
                    ReplyCount = 0
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<CommentDto?> GetCommentByIdAsync(int commentId, string? userId = null)
    {
        var comment = await _context.Comments
            .Where(c => c.Id == commentId && c.IsActive)
            .Include(c => c.User)
            .Include(c => c.Replies.Where(r => r.IsActive))
            .FirstOrDefaultAsync();

        if (comment == null) return null;

        return new CommentDto
        {
            Id = comment.Id,
            UserId = comment.UserId,
            AnimeId = comment.AnimeId,
            ParentCommentId = comment.ParentCommentId,
            Content = comment.Content,
            IsEdited = comment.IsEdited,
            IsActive = comment.IsActive,
            LikeCount = comment.LikeCount,
            DislikeCount = comment.DislikeCount,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            UserName = comment.User.UserName ?? comment.User.Email ?? "Unknown",
            UserAvatar = null,
            ReplyCount = comment.Replies.Count,
            IsLikedByUser = !string.IsNullOrEmpty(userId) && await HasUserLikedCommentAsync(userId, commentId),
            IsDislikedByUser = !string.IsNullOrEmpty(userId) && await HasUserDislikedCommentAsync(userId, commentId)
        };
    }

    public async Task<IEnumerable<CommentDto>> GetCommentRepliesAsync(int commentId, int page = 1, int pageSize = 10)
    {
        var skip = (page - 1) * pageSize;
        
        return await _context.Comments
            .Where(c => c.ParentCommentId == commentId && c.IsActive)
            .Include(c => c.User)
            .OrderBy(c => c.CreatedAt)
            .Skip(skip)
            .Take(pageSize)
            .Select(c => new CommentDto
            {
                Id = c.Id,
                UserId = c.UserId,
                AnimeId = c.AnimeId,
                ParentCommentId = c.ParentCommentId,
                Content = c.Content,
                IsEdited = c.IsEdited,
                IsActive = c.IsActive,
                LikeCount = c.LikeCount,
                DislikeCount = c.DislikeCount,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                UserName = c.User.UserName ?? c.User.Email ?? "Unknown",
                UserAvatar = null,
                ReplyCount = 0
            })
            .ToListAsync();
    }

    public async Task<int> GetCommentCountAsync(int animeId)
    {
        return await _context.Comments
            .CountAsync(c => c.AnimeId == animeId && c.IsActive && c.ParentCommentId == null);
    }

    public async Task<int> GetReplyCountAsync(int commentId)
    {
        return await _context.Comments
            .CountAsync(c => c.ParentCommentId == commentId && c.IsActive);
    }

    public async Task<bool> HasUserLikedCommentAsync(string userId, int commentId)
    {
        // TODO: Implement when CommentReaction entity is created
        return false;
    }

    public async Task<bool> HasUserDislikedCommentAsync(string userId, int commentId)
    {
        // TODO: Implement when CommentReaction entity is created
        return false;
    }

    public async Task<IEnumerable<CommentDto>> GetUserCommentsAsync(string userId, int page = 1, int pageSize = 20)
    {
        var skip = (page - 1) * pageSize;
        
        return await _context.Comments
            .Where(c => c.UserId == userId && c.IsActive)
            .Include(c => c.Anime)
            .OrderByDescending(c => c.CreatedAt)
            .Skip(skip)
            .Take(pageSize)
            .Select(c => new CommentDto
            {
                Id = c.Id,
                UserId = c.UserId,
                AnimeId = c.AnimeId,
                ParentCommentId = c.ParentCommentId,
                Content = c.Content,
                IsEdited = c.IsEdited,
                IsActive = c.IsActive,
                LikeCount = c.LikeCount,
                DislikeCount = c.DislikeCount,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                UserName = c.User.UserName ?? c.User.Email ?? "Unknown",
                UserAvatar = null,
                ReplyCount = 0
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<CommentDto>> GetRecentCommentsAsync(int count = 10)
    {
        return await _context.Comments
            .Where(c => c.IsActive && c.ParentCommentId == null)
            .Include(c => c.User)
            .Include(c => c.Anime)
            .OrderByDescending(c => c.CreatedAt)
            .Take(count)
            .Select(c => new CommentDto
            {
                Id = c.Id,
                UserId = c.UserId,
                AnimeId = c.AnimeId,
                ParentCommentId = c.ParentCommentId,
                Content = c.Content,
                IsEdited = c.IsEdited,
                IsActive = c.IsActive,
                LikeCount = c.LikeCount,
                DislikeCount = c.DislikeCount,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                UserName = c.User.UserName ?? c.User.Email ?? "Unknown",
                UserAvatar = null,
                ReplyCount = 0
            })
            .ToListAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
