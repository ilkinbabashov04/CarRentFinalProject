using Microsoft.AspNetCore.Mvc;

namespace Ui.ViewComponents.DefaultViewComponents
{
    public class DefaultStatisticsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
