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
            var genres = await context.Genres.ToListAsync();
            
            var animes = new List<Anime>
            {
                new()
                {
                    Title = "Ragna Crimson",
                    AlternativeTitle = "Ragna Crimson",
                    Description = "Trong một thế giới mà loài rồng thống trị từ trên trời đến dưới biển sâu, những người muốn đấu tranh chống lại chúng buộc phải bứt phá được giới hạn vốn có của con người. Với ý chí quyết thắng bằng mọi giá, thợ săn rồng Ragna đã hợp sức với vua rồng Crimson. Tuy rằng động cơ của Crimson vẫn còn là bí ẩn, nhưng mục tiêu của hai người họ chỉ có một, đó là diệt trừ loài rồng. ",
                    Poster = "https://www.nautiljon.com/images/anime/00/44/ragna_crimson_11044.webp",
                    Trailer = "https://youtu.be/U_grtjdstks?si=dhBub8xmDH8Zna4g",
                    ReleaseYear = 2023,
                    TotalEpisodes = 24,
                    Status = "Completed",
                    Type = "TV",
                    Rating = 8.0,
                    ViewCount = 600000

                },
                new()
                {
                    Title = "Chuyển sinh thành đệ thất hoàng tử, tôi quyết định trau dồi ma thuật",
                    AlternativeTitle = "Chuyển sinh thành đệ thất hoàng tử, tôi quyết định trau dồi ma thuật",
                    Description = "Dòng dõi, năng khiếu và sự nỗ lực, đó là những điều cần thiết để học phép thuật Một pháp sư bình thường rất yêu phép thuật nhưng lại không ban cho dòng dõi và năng khiếu đã gặp một kết cục bi thảm. Khi cận kề cái chết, cậu có một khao khát mãnh liệt là có thể được học phép thuật nhiều hơn để hoàn thiện nó. Sau khi tỉnh dậy, cậu nhất ra mình đã chuyển sinh thành Lloyd, đệ thất hoàng tử của vương quốc Saloum, đồng thời là dòng dõi pháp sư mạnh mẽ.Được chuyển sinh cùng với ký ức và kỹ năng của tiền kiếp, nay lại ở trong một dòng dõi hoàn hảo, cậu tận hưởng một cuộc sống vô tư để hoàn thiện phép thuật của mình với lượng ma lực dư thừa.Bộ truyện tranh được dựa trên Light Novel và được xuất bản trên App Manga của Kodansha, “Magazine Pocket”, và trở thành tác phẩm được bán nhiều nhất trong App với hơn 3 triệu bản. Bộ truyện tranh chuyển sinh nổi tiếng này nay đã được chuyển thể thành anime.Câu chuyện lấy bối cảnh tại một thế giới khác nơi có quái vật và quỷ. Nhân vật chính là Lloyd sở hữu lượng ma lực khổng lồ mà bất cứ ai cũng phải nể sợ. Chúng ta sẽ dõi theo hành trình phát triển không ngừng của cậu trong việc học phép thuật và cố gắng hoàn thiện nó.Câu chuyện chuyển sinh của đệ thất hoàng tử với những trận chiến phép thuật đầy phấn khích và ấn tượng đã bắt đầu.",
                    Poster = "https://vn.e-muse.com.tw/wp-content/uploads/2024/03/%E8%BD%89%E7%94%9F%E7%82%BA%E7%AC%AC%E4%B8%83%E7%8E%8B%E5%AD%90%EF%BC%8C%E9%9A%A8%E5%BF%83%E6%89%80%E6%AC%B2%E7%9A%84%E9%AD%94%E6%B3%95%E5%AD%B8%E7%BF%92%E4%B9%8B%E8%B7%AF_%E8%B6%8A%E5%AE%98%E7%B6%B2.jpg",
                    Trailer = "https://youtu.be/CpZcllLRDnU?si=nY52tAllDz-zSdr1",
                    ReleaseYear = 2024,
                    TotalEpisodes = 12,
                    Status = "Completed",
                    Type = "TV",
                    Rating = 8.0,
                    ViewCount = 600000
                },
                new()
                {
                    Title = "Câu chuyện về Senpai đáng ghét của tôi",
                    AlternativeTitle = "Câu chuyện về Senpai đáng ghét của tôi",
                    Description = "Futaba Igarashi, một nhân viên văn phòng nhỏ nhắn và hay gây sự, thường bị nhầm là trẻ con, liên tục phàn nàn về người đồng nghiệp to con và ồn ào của mình, Harumi Takeda. Tuy nhiên, bạn bè và đồng nghiệp của Futaba đều nhận ra cô đang âm thầm nuôi dưỡng tình cảm với Takeda mà cô đang phải đấu tranh.",
                    Poster = "https://pic.bstarstatic.com/ogv/0df0b6052e939779c037a01ede8d4a74a5c7d395.jpg@600w_800h_1e_1c_1f.webp",
                    Trailer = "https://youtu.be/yNJ8ih3FKtU?si=c7fF-et_gFRtUKmC",
                    ReleaseYear = 2021,
                    TotalEpisodes = 12,
                    Status = "Completed",
                    Type = "TV",
                    Rating = 8.0,
                    ViewCount = 600000
                },
                new()
                {
                    Title = "Hành trình của Elaina",
                    AlternativeTitle = "Majo no Tabitabi",
                    Description = "Một ngày nọ, cô bé Elaina đọc được quyển sách 'Những cuộc phiêu lưu của Nike', và rồi ấp ủ ước mơ trở thành phù thủy, được đi chu du đây đó. Sau khi đỗ kì thi phù thủy, cô chỉ còn phải xin làm đệ tử cho một phù thủy nào đó. Tuy nhiên, ai cũng từ chối cô. Cuối cùng, cô đành đến thỉnh cầu 'Phù thủy Bụi Sao' thu nhận mình.",
                    Poster = "https://image.tmdb.org/t/p/original/eCIsneDyB1KD2kQN4cUAJMXj7mt.jpg",
                    Trailer = "https://youtu.be/CJO-94le5cc?si=mJ0wGnwpLDCIPoEb",
                    ReleaseYear = 2016,
                    TotalEpisodes = 12,
                    Status = "Completed",
                    Type = "TV",
                    Rating = 8.0,
                    ViewCount = 600000
                },
                new()
                {
                    Title = "SPY×FAMILY ",
                    AlternativeTitle = "SPY×FAMILY ",
                    Description = "Điệp viên Twilight nhận nhiệm vụ thực hiện chiến dịch Strix nhằm bảo vệ hòa bình Đông Tây và thế giới. Để hoàn thành nhiệm vụ, Twilight lấy danh tính giả là bác sĩ tâm thần Loid Forger, kết hôn với công chức Yor Briar và nhận nuôi Anya, cho bé theo học trường Eden. Với những mục tiêu riêng, ba người đồng lòng trở thành gia đình. Dù sống chung dưới một mái nhà, họ đều che giấu danh tính thật của mình.",
                    Poster = "https://vn.e-muse.com.tw/wp-content/uploads/2022/09/%E9%96%93%E8%AB%9C%E5%AE%B6%E5%AE%B6%E9%85%92_%E5%BE%8C%E5%8D%8A_%E8%8B%B1%E7%9B%B4.jpg",
                    Trailer = "https://youtu.be/pJOdT0Odxak?si=_cmtINN0TqsyDmRX",
                    ReleaseYear = 2022,
                    TotalEpisodes = 12,
                    Status = "Completed",
                    Type = "TV",
                    Rating = 8.0,
                    ViewCount = 600000
                },
                new()
                {
                    Title = "Cạo râu xong, tôi nhặt gái về nhà",
                    AlternativeTitle = "Cạo râu xong, tôi nhặt gái về nhà",
                    Description = "Câu chuyện mở đầu với Yoshida, một nam lập trình viên 26 tuổi, đang thất tình vì bị từ chối tình cảm mà anh luôn ấp ủ suốt 5 năm với đồng nghiệp. Chán nản và say xỉn, trên đường về Yoshida bắt gặp Sayu, một nữ sinh cao trung lang thang, đang ngồi cạnh cột điện của phố nhà mình. Sayu đã bỏ nhà đi suốt 6 tháng và chọn cách làm tình với nhiều người đàn ông khác nhau để được tá túc ở nhà họ. Thoạt đầu Sayu đề nghị Yoshida cho cô qua đêm đổi lại việc cô để anh làm tình với mình, nhưng Yoshida chỉ đồng ý chứa chấp cô mà không cần điều kiện trao đổi đó. Sau khi hỏi rõ về xuất thân và hoàn cảnh của Sayu, Yoshida quyết định để cô cùng sống trong nhà mình cho đến khi cô tìm ra được giá trị sống của bản thân và trở nên trưởng thành hơn.",
                    Poster = "https://upload.wikimedia.org/wikipedia/vi/d/d5/Hige_o_Soru._Soshite_Joshi_K%C5%8Dsei_o_Hirou._volume_1_cover.jpg",
                    Trailer = "https://youtu.be/C7j0pMHgFDI?si=mgnbKJUpnjUhta6g",
                    ReleaseYear = 2018,
                    TotalEpisodes = 12,
                    Status = "Completed",
                    Type = "TV",
                    Rating = 8.0,
                    ViewCount = 600000
                },
                new()
                {
                    Title = "Chào mừng đến với lớp học đề cao thực lực",
                    AlternativeTitle = "Chào mừng đến với lớp học đề cao thực lực",
                    Description = "Trường THPT Đào tạo Nâng cao Tokyo là ngôi trường danh tiếng hàng đầu với các cơ sở hiện đại, nơi có 100% học sinh đậu đại học hoặc tìm được việc làm danh giá sau khi tốt nghiệp. Các học sinh dường như có quyền tự do mặc bất cứ gì và để bất kỳ kiểu tóc nào. Nhiều người xem ngôi trường này là thiên đường, nhưng nam sinh Ayanokoji Kiyotaka biết rằng chỉ những học sinh giỏi nhất mới được đối xử ưu ái và nơi đây coi trọng thành tích đến thế nào.",
                    Poster = "https://upload.wikimedia.org/wikipedia/vi/5/52/Y%C5%8Dkoso_Jitsuryoku_Shij%C5%8D_Shugi_no_Ky%C5%8Dshitsu_e%2C_Volume_1.jpg",
                    Trailer = "https://youtu.be/85LZ15Ta05g?si=Lb88c2DTbTzmbhkK",
                    ReleaseYear = 2017 ,
                    TotalEpisodes = 12,
                    Status = "Completed",
                    Type = "TV",
                    Rating = 8.0,
                    ViewCount = 600000
                },
                new()
                {
                    Title = "Vì con gái, tôi có thể đánh bại cả ma vương",
                    AlternativeTitle = "Vì con gái, tôi có thể đánh bại cả ma vương",
                    Description = "Dale là một mạo hiểm giả đã đạt được những thành tựu dù còn rất trẻ. Trong một lần thực hiện nhiệm vụ, cậu đã gặp Latina – một cô nhóc thuộc Quỷ Nhân tộc – bị bỏ lại trong rừng. Thương cảm cho số phận của Latina, cậu đã quyết định nhận nuôi cô.",
                    Poster = "https://upload.wikimedia.org/wikipedia/vi/1/14/V%C3%AC_con_g%C3%A1i%2C_t%C3%B4i_c%C3%B3_th%E1%BB%83_%C4%91%C3%A1nh_b%E1%BA%A1i_c%E1%BA%A3_Ma_v%C6%B0%C6%A1ng.jpg",
                    Trailer = "https://youtu.be/p5PzSkP4pyQ?si=hjUW1r4LrxAfYes_",
                    ReleaseYear = 2019,
                    TotalEpisodes = 12,
                    Status = "Completed",
                    Type = "TV",
                    Rating = 8.0,
                    ViewCount = 600000
                }

            };

            await context.Animes.AddRangeAsync(animes);
            await context.SaveChangesAsync();

            // Add genres to anime
            var animeGenres = new List<AnimeGenre>();

                           // Ragna Crimson - Action, Fantasy, Supernatural, Demons
              animeGenres.AddRange(new[]
              {
                  new AnimeGenre { AnimeId = animes[0].Id, GenreId = genres.First(g => g.Name == "Action").Id },
                  new AnimeGenre { AnimeId = animes[0].Id, GenreId = genres.First(g => g.Name == "Fantasy").Id },
                  new AnimeGenre { AnimeId = animes[0].Id, GenreId = genres.First(g => g.Name == "Supernatural").Id },
                  new AnimeGenre { AnimeId = animes[0].Id, GenreId = genres.First(g => g.Name == "Demons").Id }
              });

              // Chuyển sinh thành đệ thất hoàng tử - Action, Fantasy, Isekai, School
              animeGenres.AddRange(new[]
              {
                  new AnimeGenre { AnimeId = animes[1].Id, GenreId = genres.First(g => g.Name == "Action").Id },
                  new AnimeGenre { AnimeId = animes[1].Id, GenreId = genres.First(g => g.Name == "Fantasy").Id },
                  new AnimeGenre { AnimeId = animes[1].Id, GenreId = genres.First(g => g.Name == "Isekai").Id },
                  new AnimeGenre { AnimeId = animes[1].Id, GenreId = genres.First(g => g.Name == "School").Id }
              });

              // Câu chuyện về Senpai đáng ghét - Comedy, Drama, Romance, Slice of Life, Shoujo
              animeGenres.AddRange(new[]
              {
                  new AnimeGenre { AnimeId = animes[2].Id, GenreId = genres.First(g => g.Name == "Comedy").Id },
                  new AnimeGenre { AnimeId = animes[2].Id, GenreId = genres.First(g => g.Name == "Drama").Id },
                  new AnimeGenre { AnimeId = animes[2].Id, GenreId = genres.First(g => g.Name == "Romance").Id },
                  new AnimeGenre { AnimeId = animes[2].Id, GenreId = genres.First(g => g.Name == "Slice of Life").Id },
                  new AnimeGenre { AnimeId = animes[2].Id, GenreId = genres.First(g => g.Name == "Shoujo").Id }
              });

              // Hành trình của Elaina - Adventure, Fantasy, Slice of Life, Supernatural
              animeGenres.AddRange(new[]
              {
                  new AnimeGenre { AnimeId = animes[3].Id, GenreId = genres.First(g => g.Name == "Adventure").Id },
                  new AnimeGenre { AnimeId = animes[3].Id, GenreId = genres.First(g => g.Name == "Fantasy").Id },
                  new AnimeGenre { AnimeId = animes[3].Id, GenreId = genres.First(g => g.Name == "Slice of Life").Id },
                  new AnimeGenre { AnimeId = animes[3].Id, GenreId = genres.First(g => g.Name == "Supernatural").Id }
              });

              // SPY×FAMILY - Action, Comedy, Drama, Mystery, Romance, Sci-Fi, Thriller
              animeGenres.AddRange(new[]
              {
                  new AnimeGenre { AnimeId = animes[4].Id, GenreId = genres.First(g => g.Name == "Action").Id },
                  new AnimeGenre { AnimeId = animes[4].Id, GenreId = genres.First(g => g.Name == "Comedy").Id },
                  new AnimeGenre { AnimeId = animes[4].Id, GenreId = genres.First(g => g.Name == "Drama").Id },
                  new AnimeGenre { AnimeId = animes[4].Id, GenreId = genres.First(g => g.Name == "Mystery").Id },
                  new AnimeGenre { AnimeId = animes[4].Id, GenreId = genres.First(g => g.Name == "Romance").Id },
                  new AnimeGenre { AnimeId = animes[4].Id, GenreId = genres.First(g => g.Name == "Sci-Fi").Id },
                  new AnimeGenre { AnimeId = animes[4].Id, GenreId = genres.First(g => g.Name == "Thriller").Id }
              });

              // Cạo râu xong, tôi nhặt gái về nhà - Comedy, Drama, Romance, Slice of Life, Josei
            animeGenres.AddRange(new[]
            {
                  new AnimeGenre { AnimeId = animes[5].Id, GenreId = genres.First(g => g.Name == "Comedy").Id },
                  new AnimeGenre { AnimeId = animes[5].Id, GenreId = genres.First(g => g.Name == "Drama").Id },
                  new AnimeGenre { AnimeId = animes[5].Id, GenreId = genres.First(g => g.Name == "Romance").Id },
                  new AnimeGenre { AnimeId = animes[5].Id, GenreId = genres.First(g => g.Name == "Slice of Life").Id },
                  new AnimeGenre { AnimeId = animes[5].Id, GenreId = genres.First(g => g.Name == "Josei").Id }
              });

              // Chào mừng đến với lớp học đề cao thực lực - Comedy, Drama, Romance, Slice of Life, School, Psychological
            animeGenres.AddRange(new[]
            {
                  new AnimeGenre { AnimeId = animes[6].Id, GenreId = genres.First(g => g.Name == "Comedy").Id },
                  new AnimeGenre { AnimeId = animes[6].Id, GenreId = genres.First(g => g.Name == "Drama").Id },
                  new AnimeGenre { AnimeId = animes[6].Id, GenreId = genres.First(g => g.Name == "Romance").Id },
                  new AnimeGenre { AnimeId = animes[6].Id, GenreId = genres.First(g => g.Name == "Slice of Life").Id },
                  new AnimeGenre { AnimeId = animes[6].Id, GenreId = genres.First(g => g.Name == "School").Id },
                  new AnimeGenre { AnimeId = animes[6].Id, GenreId = genres.First(g => g.Name == "Psychological").Id }
              });

              // Vì con gái, tôi có thể đánh bại cả ma vương - Action, Adventure, Fantasy, Drama, Mystery, Romance, Sci-Fi, Supernatural
            animeGenres.AddRange(new[]
            {
                  new AnimeGenre { AnimeId = animes[7].Id, GenreId = genres.First(g => g.Name == "Action").Id },
                  new AnimeGenre { AnimeId = animes[7].Id, GenreId = genres.First(g => g.Name == "Adventure").Id },
                  new AnimeGenre { AnimeId = animes[7].Id, GenreId = genres.First(g => g.Name == "Fantasy").Id },
                  new AnimeGenre { AnimeId = animes[7].Id, GenreId = genres.First(g => g.Name == "Drama").Id },
                  new AnimeGenre { AnimeId = animes[7].Id, GenreId = genres.First(g => g.Name == "Mystery").Id },
                  new AnimeGenre { AnimeId = animes[7].Id, GenreId = genres.First(g => g.Name == "Romance").Id },
                  new AnimeGenre { AnimeId = animes[7].Id, GenreId = genres.First(g => g.Name == "Sci-Fi").Id },
                  new AnimeGenre { AnimeId = animes[7].Id, GenreId = genres.First(g => g.Name == "Supernatural").Id }
            });

            await context.AnimeGenres.AddRangeAsync(animeGenres);
            await context.SaveChangesAsync();

            // Add episodes
            var episodes = new List<Episode>();
             
             // Ragna Crimson episodes (24 tập)
             for (int i = 1; i <= 24; i++)
             {
                 episodes.Add(new Episode
                 {
                     AnimeId = animes[0].Id,
                     EpisodeNumber = i,
                     Title = $"Ragna Crimson - Tập {i:00}",
                     VideoUrl = $"https://youtu.be/JgVK5o6eo1E?si=B_XYOjsaRF0sAsyb",
                     VideoType = "Embed",
                     Duration = 1440 // 24 minutes
                 });
             }
             
             // Chuyển sinh thành đệ thất hoàng tử episodes (12 tập)
             for (int i = 1; i <= 12; i++)
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
             
             // Câu chuyện về Senpai đáng ghét episodes (12 tập)
             for (int i = 1; i <= 12; i++)
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
             
             // Hành trình của Elaina episodes (12 tập)
             for (int i = 1; i <= 12; i++)
             {
                 episodes.Add(new Episode
                 {
                     AnimeId = animes[3].Id,
                     EpisodeNumber = i,
                     Title = $"Hành trình của Elaina - Tập {i:00}",
                     VideoUrl = $"https://youtu.be/FdWFB2oVXkg?si=qwnIXg31Y3r24ZDQ",
                     VideoType = "Embed",
                     Duration = 1440 // 24 minutes
                 });
             }
             
             // SPY×FAMILY episodes (12 tập)
             for (int i = 1; i <= 12; i++)
             {
                 episodes.Add(new Episode
                 {
                     AnimeId = animes[4].Id,
                     EpisodeNumber = i,
                     Title = $"SPY×FAMILY - Tập {i:00}",
                     VideoUrl = $"https://youtu.be/hLxCjEcFcMM?si=EyLb6bay-rC728tC",
                     VideoType = "Embed",
                     Duration = 1440 // 24 minutes
                 });
             }
             
             // Cạo râu xong, tôi nhặt gái về nhà episodes (12 tập)
             for (int i = 1; i <= 12; i++)
             {
                 episodes.Add(new Episode
                 {
                     AnimeId = animes[5].Id,
                     EpisodeNumber = i,
                     Title = $"Cạo râu xong, tôi nhặt gái về nhà - Tập {i:00}",
                     VideoUrl = $"https://youtu.be/-zqBNThB-cw?si=5Hb5knY8wT5PzKx9",
                     VideoType = "Embed",
                     Duration = 1440 // 24 minutes
                 });
             }
             
             // Chào mừng đến với lớp học đề cao thực lực episodes (12 tập)
             for (int i = 1; i <= 12; i++)
             {
                 episodes.Add(new Episode
                 {
                     AnimeId = animes[6].Id,
                     EpisodeNumber = i,
                     Title = $"Chào mừng đến với lớp học đề cao thực lực - Tập {i:00}",
                     VideoUrl = $"https://youtu.be/kkjZITaiivU?si=X2WruIVOqrSCcbrS",
                     VideoType = "Embed",
                     Duration = 1440 // 24 minutes
                 });
             }
             
             // Vì con gái, tôi có thể đánh bại cả ma vương episodes (12 tập)
             for (int i = 1; i <= 12; i++)
            {
                episodes.Add(new Episode
                {
                     AnimeId = animes[7].Id,
                    EpisodeNumber = i,
                     Title = $"Vì con gái, tôi có thể đánh bại cả ma vương - Tập {i:00}",
                     VideoUrl = $"https://youtu.be/rzq2TGm3FVw?si=OYhc0fu-UlE3mAql",
                    VideoType = "Embed",
                    Duration = 1440 // 24 minutes
                });
            }

            await context.Episodes.AddRangeAsync(episodes);
            await context.SaveChangesAsync();
        }
    }
}
