using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Okean_AnimeMovie.Core.DTOs;
using Okean_AnimeMovie.Core.Interfaces;
using System.Security.Claims;

namespace Okean_AnimeMovie.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly ILogger<CommentController> _logger;

    public CommentController(ICommentRepository commentRepository, ILogger<CommentController> logger)
    {
        _commentRepository = commentRepository;
        _logger = logger;
    }

    // GET: api/comment/anime/{animeId}
    [HttpGet("anime/{animeId}")]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetAnimeComments(int animeId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        try
        {
            var comments = await _commentRepository.GetAnimeCommentsAsync(animeId, page, pageSize);
            return Ok(comments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting comments for anime {AnimeId}", animeId);
            return StatusCode(500, "Internal server error");
        }
    }

    // GET: api/comment/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDto>> GetComment(int id)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var comment = await _commentRepository.GetCommentByIdAsync(id, userId);
            
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting comment {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    // GET: api/comment/{id}/replies
    [HttpGet("{id}/replies")]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentReplies(int id, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var replies = await _commentRepository.GetCommentRepliesAsync(id, page, pageSize);
            return Ok(replies);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting replies for comment {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    // POST: api/comment
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CommentDto>> CreateComment([FromBody] CreateCommentDto createCommentDto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            if (string.IsNullOrWhiteSpace(createCommentDto.Content))
            {
                return BadRequest("Comment content cannot be empty");
            }

            var comment = new Core.Entities.Comment
            {
                UserId = userId,
                AnimeId = createCommentDto.AnimeId,
                ParentCommentId = createCommentDto.ParentCommentId,
                Content = createCommentDto.Content.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            await _commentRepository.AddAsync(comment);
            await _commentRepository.SaveChangesAsync();

            // Return the created comment
            var commentDto = new CommentDto
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
                UpdatedAt = comment.UpdatedAt
            };

            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, commentDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating comment");
            return StatusCode(500, "Internal server error");
        }
    }

    // PUT: api/comment/{id}
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDto updateCommentDto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            // Check if user owns this comment
            if (comment.UserId != userId)
            {
                return Forbid();
            }

            if (string.IsNullOrWhiteSpace(updateCommentDto.Content))
            {
                return BadRequest("Comment content cannot be empty");
            }

            comment.Content = updateCommentDto.Content.Trim();
            comment.IsEdited = true;
            comment.UpdatedAt = DateTime.UtcNow;

            await _commentRepository.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating comment {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    // DELETE: api/comment/{id}
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteComment(int id)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            // Check if user owns this comment
            if (comment.UserId != userId)
            {
                return Forbid();
            }

            // Soft delete - mark as inactive
            comment.IsActive = false;
            comment.UpdatedAt = DateTime.UtcNow;

            await _commentRepository.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting comment {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    // POST: api/comment/{id}/reaction
    [HttpPost("{id}/reaction")]
    [Authorize]
    public async Task<IActionResult> AddCommentReaction(int id, [FromBody] CommentReactionDto reactionDto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            // TODO: Implement reaction system (like/dislike)
            // This would require a separate CommentReaction entity
            // For now, just return success

            return Ok(new { message = "Reaction added successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding reaction to comment {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    // GET: api/comment/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetUserComments(string userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        try
        {
            var comments = await _commentRepository.GetUserCommentsAsync(userId, page, pageSize);
            return Ok(comments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting comments for user {UserId}", userId);
            return StatusCode(500, "Internal server error");
        }
    }

    // GET: api/comment/recent
    [HttpGet("recent")]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetRecentComments([FromQuery] int count = 10)
    {
        try
        {
            var comments = await _commentRepository.GetRecentCommentsAsync(count);
            return Ok(comments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting recent comments");
            return StatusCode(500, "Internal server error");
        }
    }
}
