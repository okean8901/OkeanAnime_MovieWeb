using Microsoft.AspNetCore.Mvc;
using Okean_AnimeMovie.Core.DTOs;
using Okean_AnimeMovie.Core.Interfaces;

namespace Okean_AnimeMovie.ViewComponents;

public class CommentViewComponent : ViewComponent
{
    private readonly ICommentRepository _commentRepository;

    public CommentViewComponent(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync(int animeId, string? userId = null)
    {
        try
        {
            var comments = await _commentRepository.GetAnimeCommentsAsync(animeId, 1, 50);
            var commentCount = await _commentRepository.GetCommentCountAsync(animeId);
            
            var viewModel = new CommentViewModel
            {
                AnimeId = animeId,
                Comments = comments.ToList(),
                CommentCount = commentCount,
                UserId = userId
            };

            return View(viewModel);
        }
        catch (Exception)
        {
            // Return empty model if there's an error
            var viewModel = new CommentViewModel
            {
                AnimeId = animeId,
                Comments = new List<CommentDto>(),
                CommentCount = 0,
                UserId = userId
            };

            return View(viewModel);
        }
    }
}

public class CommentViewModel
{
    public int AnimeId { get; set; }
    public List<CommentDto> Comments { get; set; } = new();
    public int CommentCount { get; set; }
    public string? UserId { get; set; }
}
