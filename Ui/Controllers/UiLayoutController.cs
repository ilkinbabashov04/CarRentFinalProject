using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
	public class UiLayoutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
