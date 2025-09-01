using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Okean_AnimeMovie.Core.Entities;
using Okean_AnimeMovie.Core.Interfaces;
using Okean_AnimeMovie.Core.Services;
using Okean_AnimeMovie.Infrastructure.Data;
using Okean_AnimeMovie.Infrastructure.Repositories;
using Okean_AnimeMovie.Infrastructure.Services;
using Okean_AnimeMovie.Application.Services.Email;
using Okean_AnimeMovie.Application.Services.Cache;
using Okean_AnimeMovie.Application.Services.Notification;
using Okean_AnimeMovie.Middleware;

namespace Okean_AnimeMovie
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddMemoryCache();

            // Add Entity Framework
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

            // Add Authentication
            builder.Services.AddAuthentication(options =>
            {
                // Use Identity cookie as the default scheme so MVC sign-in works
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!))
                };
            })
            .AddGoogle(options =>
            {
                var clientId = builder.Configuration["Authentication:Google:ClientId"];
                var clientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                
                // Log để debug
                Console.WriteLine($"Google OAuth ClientId: {clientId}");
                Console.WriteLine($"Google OAuth ClientSecret: {clientSecret?.Substring(0, Math.Min(10, clientSecret?.Length ?? 0))}...");
                
                if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
                {
                    throw new InvalidOperationException("Google OAuth credentials are not configured properly");
                }
                
                options.ClientId = clientId;
                options.ClientSecret = clientSecret;
                options.CallbackPath = "/signin-google";
                
                // Cấu hình bổ sung
                options.SaveTokens = true;
                
                // Thêm scopes
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
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
            builder.Services.AddScoped<IAnimeCacheService, AnimeCacheService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            
            // Configure Email
            var emailSettings = new EmailSettings();
            builder.Configuration.GetSection("Email").Bind(emailSettings);
            builder.Services.AddSingleton(emailSettings);
            builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();

            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

                    app.UseAuthentication();
        app.UseAuthorization();
        
        // Add Rate Limiting Middleware
        app.UseMiddleware<RateLimitingMiddleware>();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Initialize database with sample data
            await DbInitializer.InitializeAsync(app.Services);

            app.Run();
        }
    }
}
