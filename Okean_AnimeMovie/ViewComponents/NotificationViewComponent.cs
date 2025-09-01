using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Okean_AnimeMovie.Core.Entities;
using Okean_AnimeMovie.Application.Services.Notification;
using System.Security.Claims;

namespace Okean_AnimeMovie.ViewComponents;

public class NotificationViewComponent : ViewComponent
{
    private readonly INotificationService _notificationService;
    private readonly UserManager<ApplicationUser> _userManager;

    public NotificationViewComponent(
        INotificationService notificationService,
        UserManager<ApplicationUser> userManager)
    {
        _notificationService = notificationService;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
        if (user != null)
        {
            var userId = user.Id;
            if (!string.IsNullOrEmpty(userId))
            {
                var unreadCount = await _notificationService.GetUnreadNotificationCountAsync(userId);
                var recentNotifications = await _notificationService.GetUserNotificationsAsync(userId, 1, 5);
                
                var viewModel = new NotificationViewModel
                {
                    UnreadCount = unreadCount,
                    RecentNotifications = recentNotifications
                };
                
                return View(viewModel);
            }
        }
        
        return View(new NotificationViewModel { UnreadCount = 0, RecentNotifications = new List<UserNotification>() });
    }
}

public class NotificationViewModel
{
    public int UnreadCount { get; set; }
    public IEnumerable<UserNotification> RecentNotifications { get; set; } = new List<UserNotification>();
}
