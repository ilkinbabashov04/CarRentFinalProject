using Microsoft.AspNetCore.Mvc;

namespace Ui.ViewComponents.UILayoutViewComponents
{
    public class ScriptUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
