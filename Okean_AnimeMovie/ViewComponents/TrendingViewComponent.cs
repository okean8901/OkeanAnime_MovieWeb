using Microsoft.AspNetCore.Mvc;
using Okean_AnimeMovie.Core.DTOs;
using Okean_AnimeMovie.Core.Interfaces;

namespace Okean_AnimeMovie.ViewComponents;

public class TrendingViewComponent : ViewComponent
{
    private readonly ITrendingRepository _trendingRepository;

    public TrendingViewComponent(ITrendingRepository trendingRepository)
    {
        _trendingRepository = trendingRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync(TrendingType type = TrendingType.MostViewed, int count = 10)
    {
        var trendingAnime = type switch
        {
            TrendingType.MostViewed => await _trendingRepository.GetMostViewedAnimeAsync(count),
            TrendingType.TopRated => await _trendingRepository.GetTopRatedAnimeAsync(count),
            TrendingType.MostCommented => await _trendingRepository.GetMostCommentedAnimeAsync(count),
            TrendingType.MostFavorited => await _trendingRepository.GetMostFavoritedAnimeAsync(count),
            TrendingType.RecentlyAdded => await _trendingRepository.GetRecentlyAddedAnimeAsync(count),
            TrendingType.RecentlyUpdated => await _trendingRepository.GetRecentlyUpdatedAnimeAsync(count),
            _ => await _trendingRepository.GetMostViewedAnimeAsync(count)
        };

        var viewModel = new TrendingViewModel
        {
            Anime = trendingAnime.ToList(),
            Type = type,
            Title = GetTitle(type)
        };

        return View(viewModel);
    }

    private string GetTitle(TrendingType type)
    {
        return type switch
        {
            TrendingType.MostViewed => "Xem nhiều nhất",
            TrendingType.TopRated => "Đánh giá cao nhất",
            TrendingType.MostCommented => "Bình luận nhiều nhất",
            TrendingType.MostFavorited => "Yêu thích nhiều nhất",
            TrendingType.RecentlyAdded => "Mới thêm gần đây",
            TrendingType.RecentlyUpdated => "Cập nhật gần đây",
            _ => "Thịnh hành"
        };
    }
}

public class TrendingViewModel
{
    public List<TrendingDto> Anime { get; set; } = new();
    public TrendingType Type { get; set; }
    public string Title { get; set; } = string.Empty;
}
