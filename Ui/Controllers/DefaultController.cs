using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
