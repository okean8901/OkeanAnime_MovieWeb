using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Okean_AnimeMovie.Core.Entities;

namespace Okean_AnimeMovie.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Anime> Animes { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<AnimeGenre> AnimeGenres { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<ViewCount> ViewCounts { get; set; }
    public DbSet<ViewHistory> ViewHistories { get; set; }
    public DbSet<UserNotification> UserNotifications { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure AnimeGenre as many-to-many relationship
        builder.Entity<AnimeGenre>()
            .HasKey(ag => new { ag.AnimeId, ag.GenreId });

        builder.Entity<AnimeGenre>()
            .HasOne(ag => ag.Anime)
            .WithMany(a => a.AnimeGenres)
            .HasForeignKey(ag => ag.AnimeId);

        builder.Entity<AnimeGenre>()
            .HasOne(ag => ag.Genre)
            .WithMany(g => g.AnimeGenres)
            .HasForeignKey(ag => ag.GenreId);

        // Configure Anime entity
        builder.Entity<Anime>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.AlternativeTitle).HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Poster).HasMaxLength(500);
            entity.Property(e => e.Trailer).HasMaxLength(500);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Type).HasMaxLength(20);
            entity.Property(e => e.Rating).HasPrecision(3, 2);
        });

        // Configure Genre entity
        builder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        // Configure Episode entity
        builder.Entity<Episode>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Thumbnail).HasMaxLength(500);
            entity.Property(e => e.VideoUrl).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.VideoType).HasMaxLength(20);
        });

        // Configure Favorite entity
        builder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.UserId, e.AnimeId }).IsUnique();
        });

        // Configure Comment entity
        builder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Content).IsRequired().HasMaxLength(1000);
        });

        // Configure Rating entity
        builder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.UserId, e.AnimeId }).IsUnique();
            entity.Property(e => e.Score);
        });

        // Configure ViewCount entity
        builder.Entity<ViewCount>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.IpAddress).IsRequired().HasMaxLength(45);
            entity.Property(e => e.UserAgent).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Referrer).HasMaxLength(500);
            entity.HasIndex(e => new { e.AnimeId, e.IpAddress, e.ViewedAt }).IsUnique();
        });

        // Configure ViewHistory entity
        builder.Entity<ViewHistory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.UserId, e.AnimeId, e.EpisodeId, e.WatchedAt }).IsUnique();
            entity.Property(e => e.WatchDuration).HasDefaultValue(0);
            entity.Property(e => e.IsCompleted).HasDefaultValue(false);
            
            // Configure foreign key relationships with NO ACTION
            entity.HasOne(vh => vh.User)
                .WithMany(u => u.ViewHistories)
                .HasForeignKey(vh => vh.UserId)
                .OnDelete(DeleteBehavior.NoAction);
                
            entity.HasOne(vh => vh.Anime)
                .WithMany()
                .HasForeignKey(vh => vh.AnimeId)
                .OnDelete(DeleteBehavior.NoAction);
                
            entity.HasOne(vh => vh.Episode)
                .WithMany()
                .HasForeignKey(vh => vh.EpisodeId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        // Configure ApplicationUser entity
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Avatar).HasMaxLength(500);
        });

        // Configure UserNotification entity
        builder.Entity<UserNotification>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            
            entity.HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasOne(n => n.Anime)
                .WithMany()
                .HasForeignKey(n => n.AnimeId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }
}
