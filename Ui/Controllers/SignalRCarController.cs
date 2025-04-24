using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
	public class SignalRCarController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
