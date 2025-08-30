using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Okean_AnimeMovie.Core.Entities;

namespace Okean_AnimeMovie.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Test database connection first
            if (!await context.Database.CanConnectAsync())
            {
                Console.WriteLine("Cannot connect to database. Skipping initialization.");
                return;
            }

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
                    new() { Name = "Action", Description = "Phim hành động với các cảnh đánh nhau và phiêu lưu gay cấn" },
                    new() { Name = "Adventure", Description = "Những cuộc hành trình và khám phá hoành tráng" },
                    new() { Name = "Comedy", Description = "Nội dung hài hước và vui nhộn" },
                    new() { Name = "Drama", Description = "Những câu chuyện đầy cảm xúc và tập trung vào nhân vật" },
                    new() { Name = "Fantasy", Description = "Thế giới phép thuật và các yếu tố siêu nhiên" },
                    new() { Name = "Horror", Description = "Nội dung đáng sợ và hồi hộp" },
                    new() { Name = "Mystery", Description = "Giải đố và công việc thám tử" },
                    new() { Name = "Romance", Description = "Câu chuyện tình yêu và các mối quan hệ" },
                    new() { Name = "Sci-Fi", Description = "Khoa học viễn tưởng và chủ đề tương lai" },
                    new() { Name = "Slice of Life", Description = "Cuộc sống hàng ngày và tình huống thực tế" },
                    new() { Name = "Thriller", Description = "Phim giật gân với cốt truyện căng thẳng" },
                    new() { Name = "Psychological", Description = "Phim tâm lý với các yếu tố tâm thần phức tạp" },
                    new() { Name = "Supernatural", Description = "Các hiện tượng siêu nhiên và quyền năng đặc biệt" },
                    new() { Name = "Mecha", Description = "Phim về robot khổng lồ và công nghệ cơ khí" },
                    new() { Name = "Sports", Description = "Phim về thể thao và thi đấu" },
                    new() { Name = "Music", Description = "Phim về âm nhạc và biểu diễn nghệ thuật" },
                    new() { Name = "Historical", Description = "Phim lịch sử và các sự kiện quá khứ" },
                    new() { Name = "Military", Description = "Phim về quân đội và chiến tranh" },
                    new() { Name = "Parody", Description = "Phim châm biếm và hài hước chế giễu" },
                    new() { Name = "Demons", Description = "Phim về quỷ dữ và thế giới ma quỷ" },
                    new() { Name = "Martial Arts", Description = "Phim võ thuật và các kỹ năng chiến đấu" },
                    new() { Name = "Samurai", Description = "Phim về samurai và thời kỳ Edo" },
                    new() { Name = "Super Power", Description = "Phim về siêu năng lực và sức mạnh đặc biệt" },
                    new() { Name = "Vampire", Description = "Phim về ma cà rồng và thế giới bóng đêm" },
                    new() { Name = "Yaoi", Description = "Phim tình yêu nam-nam" },
                    new() { Name = "Yuri", Description = "Phim tình yêu nữ-nữ" },
                    new() { Name = "Harem", Description = "Phim về một nam chính và nhiều nữ chính" },
                    new() { Name = "Reverse Harem", Description = "Phim về một nữ chính và nhiều nam chính" },
                    new() { Name = "Isekai", Description = "Phim về nhân vật bị đưa đến thế giới khác" },
                    new() { Name = "School", Description = "Phim về cuộc sống học đường" },
                    new() { Name = "Shounen", Description = "Phim dành cho nam thiếu niên" },
                    new() { Name = "Shoujo", Description = "Phim dành cho nữ thiếu niên" },
                    new() { Name = "Seinen", Description = "Phim dành cho nam thanh niên" },
                    new() { Name = "Josei", Description = "Phim dành cho nữ thanh niên" }
                };

                await context.Genres.AddRangeAsync(genres);
                await context.SaveChangesAsync();
            }

            // Seed sample anime
            if (!await context.Animes.AnyAsync())
            {
                var animes = new List<Anime>
                {
                    new()
                    {
                        Title = "One Piece",
                        Description = "Câu chuyện về Monkey D. Luffy và băng hải tặc Mũ Rơm trong hành trình tìm kiếm kho báu One Piece.",
                        Poster = "https://via.placeholder.com/300x400/FF6B6B/FFFFFF?text=One+Piece",
                        ReleaseYear = 1999,
                        Status = "Ongoing",
                        Rating = 9.5,
                        TotalEpisodes = 1000,
                        Type = "TV",
                        ViewCount = 1000000
                    },
                    new()
                    {
                        Title = "Chuyển sinh thành đệ thất hoàng tử",
                        Description = "Một cậu bé bị chết trong một vụ tai nạn và được chuyển sinh thành hoàng tử thứ 7 của vương quốc.",
                        Poster = "https://via.placeholder.com/300x400/4ECDC4/FFFFFF?text=Chuyển+sinh",
                        ReleaseYear = 2021,
                        Status = "Completed",
                        Rating = 8.2,
                        TotalEpisodes = 12,
                        Type = "TV",
                        ViewCount = 500000
                    },
                    new()
                    {
                        Title = "Câu chuyện về Senpai đáng ghét",
                        Description = "Câu chuyện tình yêu học đường giữa một cô gái và senpai của cô.",
                        Poster = "https://via.placeholder.com/300x400/45B7D1/FFFFFF?text=Senpai",
                        ReleaseYear = 2021,
                        Status = "Completed",
                        Rating = 8.0,
                        TotalEpisodes = 12,
                        Type = "TV",
                        ViewCount = 300000
                    }
                };

                await context.Animes.AddRangeAsync(animes);
                await context.SaveChangesAsync();

                // Add genre relationships
                var animeGenres = new List<AnimeGenre>();
                var genresList = await context.Genres.ToListAsync();

                // One Piece - Action, Adventure, Comedy
                animeGenres.AddRange(new[]
                {
                    new AnimeGenre { AnimeId = animes[0].Id, GenreId = genresList.First(g => g.Name == "Action").Id },
                    new AnimeGenre { AnimeId = animes[0].Id, GenreId = genresList.First(g => g.Name == "Adventure").Id },
                    new AnimeGenre { AnimeId = animes[0].Id, GenreId = genresList.First(g => g.Name == "Comedy").Id }
                });

                // Chuyển sinh - Fantasy, Adventure
                animeGenres.AddRange(new[]
                {
                    new AnimeGenre { AnimeId = animes[1].Id, GenreId = genresList.First(g => g.Name == "Fantasy").Id },
                    new AnimeGenre { AnimeId = animes[1].Id, GenreId = genresList.First(g => g.Name == "Adventure").Id }
                });

                // Senpai - Romance, Comedy, School
                animeGenres.AddRange(new[]
                {
                    new AnimeGenre { AnimeId = animes[2].Id, GenreId = genresList.First(g => g.Name == "Romance").Id },
                    new AnimeGenre { AnimeId = animes[2].Id, GenreId = genresList.First(g => g.Name == "Comedy").Id },
                    new AnimeGenre { AnimeId = animes[2].Id, GenreId = genresList.First(g => g.Name == "School").Id }
                });

                await context.AnimeGenres.AddRangeAsync(animeGenres);
                await context.SaveChangesAsync();

                // Add episodes
                var episodes = new List<Episode>();

                // One Piece episodes (sample 5 episodes)
                for (int i = 1; i <= 5; i++)
                {
                    episodes.Add(new Episode
                    {
                        AnimeId = animes[0].Id,
                        EpisodeNumber = i,
                        Title = $"One Piece - Tập {i:00}",
                        VideoUrl = $"https://youtu.be/dQw4w9WgXcQ?si=1phNFUa7cVKU6UH8",
                        VideoType = "Embed",
                        Duration = 1440 // 24 minutes
                    });
                }

                // Chuyển sinh thành đệ thất hoàng tử episodes (5 tập)
                for (int i = 1; i <= 5; i++)
                {
                    episodes.Add(new Episode
                    {
                        AnimeId = animes[1].Id,
                        EpisodeNumber = i,
                        Title = $"Chuyển sinh thành đệ thất hoàng tử - Tập {i:00}",
                        VideoUrl = $"https://youtu.be/NiYIDJ7MGAM?si=1phNFUa7cVKU6UH8",
                        VideoType = "Embed",
                        Duration = 1440 // 24 minutes
                    });
                }

                // Câu chuyện về Senpai đáng ghét episodes (5 tập)
                for (int i = 1; i <= 5; i++)
                {
                    episodes.Add(new Episode
                    {
                        AnimeId = animes[2].Id,
                        EpisodeNumber = i,
                        Title = $"Câu chuyện về Senpai đáng ghét - Tập {i:00}",
                        VideoUrl = $"https://youtu.be/INZy5fa5GuQ?si=WPH6BmIQrhHRmXhe",
                        VideoType = "Embed",
                        Duration = 1440 // 24 minutes
                    });
                }

                await context.Episodes.AddRangeAsync(episodes);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database initialization error: {ex.Message}");
            // Don't throw the exception, just log it
        }
    }
}
