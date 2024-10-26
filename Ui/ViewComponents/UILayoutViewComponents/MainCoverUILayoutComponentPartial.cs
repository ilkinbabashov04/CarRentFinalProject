using Microsoft.AspNetCore.Mvc;

namespace Ui.ViewComponents.UILayoutViewComponents
{
    public class MainCoverUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
