using Microsoft.AspNetCore.Mvc;

namespace Ui.ViewComponents.UILayoutViewComponents
{
    public class HeadUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
