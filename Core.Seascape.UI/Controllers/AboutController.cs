using Core.Seascape.UI.Models.Layouts;
using Microsoft.AspNetCore.Mvc;

namespace Core.Seascape.UI.Controllers
{
    public class AboutController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View(
            new LayoutModel
            {
                Title = "About Seascape"
            });
        }

    }
}
