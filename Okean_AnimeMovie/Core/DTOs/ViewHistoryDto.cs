namespace Okean_AnimeMovie.Core.DTOs;

public class ViewHistoryDto
{
    public int Id { get; set; }
    public int AnimeId { get; set; }
    public string AnimeTitle { get; set; } = string.Empty;
    public string AnimePoster { get; set; } = string.Empty;
    public int EpisodeId { get; set; }
    public string EpisodeTitle { get; set; } = string.Empty;
    public int EpisodeNumber { get; set; }
    public DateTime WatchedAt { get; set; }
    public int WatchDuration { get; set; }
    public bool IsCompleted { get; set; }
    public string FormattedWatchDuration { get; set; } = string.Empty;
    public string FormattedWatchedAt { get; set; } = string.Empty;
}

public class CreateViewHistoryDto
{
    public int AnimeId { get; set; }
    public int EpisodeId { get; set; }
    public int WatchDuration { get; set; }
    public bool IsCompleted { get; set; }
}

public class UpdateViewHistoryDto
{
    public int Id { get; set; }
    public int WatchDuration { get; set; }
    public bool IsCompleted { get; set; }
}
