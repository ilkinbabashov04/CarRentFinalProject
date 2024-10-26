using Microsoft.AspNetCore.Mvc;

namespace Ui.ViewComponents.UILayoutViewComponents
{
    public class NavbarUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
