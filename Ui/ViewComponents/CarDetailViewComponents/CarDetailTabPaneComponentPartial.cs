using Microsoft.AspNetCore.Mvc;

namespace Ui.ViewComponents.CarDetailViewComponents
{
	public class CarDetailTabPaneComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
