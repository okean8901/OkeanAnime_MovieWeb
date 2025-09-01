namespace Okean_AnimeMovie.Core.DTOs;

public class TrendingDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? CoverImage { get; set; }
    public string Type { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public decimal Rating { get; set; }
    public int ViewCount { get; set; }
    public int RatingCount { get; set; }
    public int CommentCount { get; set; }
    public int FavoriteCount { get; set; }
    public DateTime LastUpdated { get; set; }
    public string[] Genres { get; set; } = Array.Empty<string>();
    public TrendingType TrendingType { get; set; }
    public int TrendingScore { get; set; }
}

public enum TrendingType
{
    MostViewed,      // Xem nhiều nhất
    TopRated,        // Đánh giá cao nhất
    MostCommented,   // Bình luận nhiều nhất
    MostFavorited,   // Yêu thích nhiều nhất
    RecentlyAdded,   // Mới thêm gần đây
    RecentlyUpdated  // Cập nhật gần đây
}

public class TrendingFilterDto
{
    public TrendingType? Type { get; set; }
    public string? Genre { get; set; }
    public string? Year { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? SearchTerm { get; set; }
}

public class TrendingSummaryDto
{
    public List<TrendingDto> MostViewed { get; set; } = new();
    public List<TrendingDto> TopRated { get; set; } = new();
    public List<TrendingDto> MostCommented { get; set; } = new();
    public List<TrendingDto> RecentlyAdded { get; set; } = new();
    public List<TrendingDto> TrendingThisWeek { get; set; } = new();
}
