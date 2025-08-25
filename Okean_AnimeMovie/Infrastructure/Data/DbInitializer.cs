using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Okean_AnimeMovie.Core.Entities;

namespace Okean_AnimeMovie.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Ensure database is created
        await context.Database.EnsureCreatedAsync();

        // Seed roles
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new IdentityRole("User"));
        }

        // Seed admin user
        var adminEmail = "admin@okeananime.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = adminEmail,
                FirstName = "Admin",
                LastName = "User",
                EmailConfirmed = true,
                IsActive = true
            };

            var result = await userManager.CreateAsync(adminUser, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        // Seed genres
        if (!await context.Genres.AnyAsync())
        {
            var genres = new List<Genre>
            {
                new() { Name = "Action", Description = "Action-packed anime with fighting and adventure" },
                new() { Name = "Adventure", Description = "Epic journeys and exploration" },
                new() { Name = "Comedy", Description = "Humorous and light-hearted content" },
                new() { Name = "Drama", Description = "Emotional and character-driven stories" },
                new() { Name = "Fantasy", Description = "Magical worlds and supernatural elements" },
                new() { Name = "Horror", Description = "Scary and suspenseful content" },
                new() { Name = "Mystery", Description = "Puzzle-solving and detective work" },
                new() { Name = "Romance", Description = "Love stories and relationships" },
                new() { Name = "Sci-Fi", Description = "Science fiction and futuristic themes" },
                new() { Name = "Slice of Life", Description = "Everyday life and realistic situations" }
            };

            await context.Genres.AddRangeAsync(genres);
            await context.SaveChangesAsync();
        }

        // Seed sample anime
        if (!await context.Animes.AnyAsync())
        {
            var genres = await context.Genres.ToListAsync();
            
            var animes = new List<Anime>
            {
                new()
                {
                    Title = "Demon Slayer",
                    AlternativeTitle = "Kimetsu no Yaiba",
                    Description = "A young boy becomes a demon slayer to save his sister and avenge his family.",
                    ReleaseYear = 2019,
                    TotalEpisodes = 26,
                    Status = "Completed",
                    Type = "TV",
                    Rating = 8.5,
                    ViewCount = 1000000
                },
                new()
                {
                    Title = "Attack on Titan",
                    AlternativeTitle = "Shingeki no Kyojin",
                    Description = "Humanity fights for survival against giant humanoid creatures called Titans.",
                    ReleaseYear = 2013,
                    TotalEpisodes = 25,
                    Status = "Completed",
                    Type = "TV",
                    Rating = 9.0,
                    ViewCount = 1500000
                },
                new()
                {
                    Title = "My Hero Academia",
                    AlternativeTitle = "Boku no Hero Academia",
                    Description = "A world where people have superpowers and a boy without powers strives to become a hero.",
                    ReleaseYear = 2016,
                    TotalEpisodes = 13,
                    Status = "Completed",
                    Type = "TV",
                    Rating = 8.2,
                    ViewCount = 800000
                }
            };

            await context.Animes.AddRangeAsync(animes);
            await context.SaveChangesAsync();

            // Add genres to anime
            var animeGenres = new List<AnimeGenre>();
            var demonSlayer = animes[0];
            var attackOnTitan = animes[1];
            var myHeroAcademia = animes[2];

            // Demon Slayer - Action, Fantasy
            animeGenres.AddRange(new[]
            {
                new AnimeGenre { AnimeId = demonSlayer.Id, GenreId = genres.First(g => g.Name == "Action").Id },
                new AnimeGenre { AnimeId = demonSlayer.Id, GenreId = genres.First(g => g.Name == "Fantasy").Id }
            });

            // Attack on Titan - Action, Drama, Mystery
            animeGenres.AddRange(new[]
            {
                new AnimeGenre { AnimeId = attackOnTitan.Id, GenreId = genres.First(g => g.Name == "Action").Id },
                new AnimeGenre { AnimeId = attackOnTitan.Id, GenreId = genres.First(g => g.Name == "Drama").Id },
                new AnimeGenre { AnimeId = attackOnTitan.Id, GenreId = genres.First(g => g.Name == "Mystery").Id }
            });

            // My Hero Academia - Action, Comedy
            animeGenres.AddRange(new[]
            {
                new AnimeGenre { AnimeId = myHeroAcademia.Id, GenreId = genres.First(g => g.Name == "Action").Id },
                new AnimeGenre { AnimeId = myHeroAcademia.Id, GenreId = genres.First(g => g.Name == "Comedy").Id }
            });

            await context.AnimeGenres.AddRangeAsync(animeGenres);
            await context.SaveChangesAsync();

            // Add episodes
            var episodes = new List<Episode>();
            for (int i = 1; i <= 3; i++)
            {
                episodes.Add(new Episode
                {
                    AnimeId = demonSlayer.Id,
                    EpisodeNumber = i,
                    Title = $"Episode {i}",
                    VideoUrl = $"https://example.com/demon-slayer-ep{i}",
                    VideoType = "Embed",
                    Duration = 1440 // 24 minutes
                });
            }

            await context.Episodes.AddRangeAsync(episodes);
            await context.SaveChangesAsync();
        }
    }
}
