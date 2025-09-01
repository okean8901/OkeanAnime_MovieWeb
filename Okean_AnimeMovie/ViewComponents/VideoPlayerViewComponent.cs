using Microsoft.AspNetCore.Mvc;

namespace Okean_AnimeMovie.ViewComponents;

public class VideoPlayerViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string videoUrl)
    {
        return View(videoUrl);
    }
}
