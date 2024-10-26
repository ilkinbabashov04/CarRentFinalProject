using Microsoft.AspNetCore.Mvc;

namespace Ui.ViewComponents.AboutViewComponents
{
	public class BecomeADriverComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
