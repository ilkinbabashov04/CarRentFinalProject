using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
	public class AboutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
