using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Okean_AnimeMovie.Core.Entities;
using Okean_AnimeMovie.Core.Interfaces;
using Okean_AnimeMovie.Core.Services;
using Okean_AnimeMovie.Infrastructure.Data;
using Okean_AnimeMovie.Infrastructure.Repositories;
using Okean_AnimeMovie.Infrastructure.Services;

namespace Okean_AnimeMovie
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add Entity Framework
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                // Railway MySQL environment variables
                var dbHost = Environment.GetEnvironmentVariable("MYSQLHOST") ?? 
                            Environment.GetEnvironmentVariable("DB_HOST") ?? 
                            "mysql.railway.internal";
                var dbName = Environment.GetEnvironmentVariable("MYSQLDATABASE") ?? 
                            Environment.GetEnvironmentVariable("DB_NAME") ?? 
                            "railway";
                var dbUser = Environment.GetEnvironmentVariable("MYSQLUSER") ?? 
                            Environment.GetEnvironmentVariable("DB_USER") ?? 
                            "root";
                var dbPassword = Environment.GetEnvironmentVariable("MYSQLPASSWORD") ?? 
                                Environment.GetEnvironmentVariable("DB_PASSWORD") ?? 
                                "vVBagZYpROAhKhnFmCUINweFZtCCITmy";
                var dbPort = Environment.GetEnvironmentVariable("MYSQLPORT") ?? 
                            Environment.GetEnvironmentVariable("DB_PORT") ?? 
                            "3306";
                
                connectionString = $"Server={dbHost};Database={dbName};User={dbUser};Password={dbPassword};Port={dbPort};CharSet=utf8mb4;SslMode=Preferred;";
                
                // Log connection info (without password)
                Console.WriteLine($"Database Connection: Server={dbHost}, Database={dbName}, User={dbUser}, Port={dbPort}");
                Console.WriteLine($"Using Railway MySQL environment variables");
            }
            else
            {
                Console.WriteLine("Using connection string from configuration");
            }
            
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, 
                    ServerVersion.AutoDetect(connectionString)));

            // Add Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Add JWT Authentication
            var jwtSecret = builder.Configuration["Jwt:Secret"] ?? 
                           Environment.GetEnvironmentVariable("JWT_SECRET") ?? 
                           "YourSuperSecretKeyHere123456789012345678901234567890";
            
            builder.Services.AddAuthentication(options =>
            {
                // Use Identity cookie as the default scheme so MVC sign-in works
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "OkeanAnimeMovie",
                    ValidAudience = builder.Configuration["Jwt:Audience"] ?? "OkeanAnimeMovieUsers",
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSecret))
                };
            });

            // Add Repositories
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();
            builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<ITrendingRepository, TrendingRepository>();

            // Add Services
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IViewHistoryService, ViewHistoryService>();

            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Configure for Railway
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            app.Urls.Add($"http://0.0.0.0:{port}");
            
            // Disable HTTPS redirect in production for Railway
            if (app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Initialize database with sample data
            try
            {
                await DbInitializer.InitializeAsync(app.Services);
            }
            catch (Exception ex)
            {
                // Log the error but don't crash the application
                Console.WriteLine($"Database initialization failed: {ex.Message}");
                // In production, you might want to log this to a proper logging service
            }

            app.Run();
        }
    }
}
