namespace Okean_AnimeMovie.Core.DTOs;

public class AnimeViewStats
{
    public int AnimeId { get; set; }
    public string AnimeTitle { get; set; } = string.Empty;
    public int TotalViews { get; set; }
    public int UniqueViewers { get; set; }
    public double AverageWatchTime { get; set; }
    public List<DailyViewData> DailyViews { get; set; } = new();
    public List<EpisodeViewData> EpisodeViews { get; set; } = new();
}

public class DailyViewData
{
    public DateTime Date { get; set; }
    public int Views { get; set; }
    public int UniqueViewers { get; set; }
}

public class EpisodeViewData
{
    public int EpisodeNumber { get; set; }
    public int Views { get; set; }
    public double CompletionRate { get; set; }
}

public class UserWatchStats
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public int TotalWatchTime { get; set; }
    public int TotalEpisodesWatched { get; set; }
    public int TotalAnimeWatched { get; set; }
    public List<AnimeWatchData> TopAnime { get; set; } = new();
    public List<GenrePreferenceData> GenrePreferences { get; set; } = new();
}

public class AnimeWatchData
{
    public int AnimeId { get; set; }
    public string AnimeTitle { get; set; } = string.Empty;
    public int EpisodesWatched { get; set; }
    public int TotalWatchTime { get; set; }
    public DateTime LastWatched { get; set; }
}

public class GenrePreferenceData
{
    public int GenreId { get; set; }
    public string GenreName { get; set; } = string.Empty;
    public int WatchCount { get; set; }
    public double Percentage { get; set; }
}

public class GenrePopularityStats
{
    public List<GenrePopularityData> GenrePopularity { get; set; } = new();
    public int TotalViews { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
}

public class GenrePopularityData
{
    public int GenreId { get; set; }
    public string GenreName { get; set; } = string.Empty;
    public int TotalViews { get; set; }
    public int UniqueViewers { get; set; }
    public double AverageRating { get; set; }
    public double PercentageOfTotal { get; set; }
}

public class AnimeRatingStats
{
    public int AnimeId { get; set; }
    public string AnimeTitle { get; set; } = string.Empty;
    public double AverageRating { get; set; }
    public int TotalRatings { get; set; }
    public List<RatingDistributionData> RatingDistribution { get; set; } = new();
    public List<RecentRatingData> RecentRatings { get; set; } = new();
}

public class RatingDistributionData
{
    public int Rating { get; set; }
    public int Count { get; set; }
    public double Percentage { get; set; }
}

public class RecentRatingData
{
    public string UserName { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime RatedAt { get; set; }
}

public class OverallStats
{
    public int TotalUsers { get; set; }
    public int TotalAnime { get; set; }
    public int TotalEpisodes { get; set; }
    public int TotalViews { get; set; }
    public double AverageRating { get; set; }
    public int TotalComments { get; set; }
    public int TotalFavorites { get; set; }
    public List<TopAnimeData> TopAnime { get; set; } = new();
    public List<TopGenreData> TopGenres { get; set; } = new();
    public List<ActiveUserData> MostActiveUsers { get; set; } = new();
}

public class TopAnimeData
{
    public int AnimeId { get; set; }
    public string AnimeTitle { get; set; } = string.Empty;
    public int Views { get; set; }
    public double Rating { get; set; }
    public int Episodes { get; set; }
}

public class TopGenreData
{
    public int GenreId { get; set; }
    public string GenreName { get; set; } = string.Empty;
    public int Views { get; set; }
    public int AnimeCount { get; set; }
}

public class ActiveUserData
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public int WatchTime { get; set; }
    public int EpisodesWatched { get; set; }
    public int CommentsPosted { get; set; }
}
