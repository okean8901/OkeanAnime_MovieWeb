namespace Okean_AnimeMovie.Core.Entities;

public class AnimeGenre
{
    public int AnimeId { get; set; }
    public int GenreId { get; set; }
    
    // Navigation properties
    public virtual Anime Anime { get; set; } = null!;
    public virtual Genre Genre { get; set; } = null!;
}
