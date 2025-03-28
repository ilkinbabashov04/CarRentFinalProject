using Microsoft.AspNetCore.Mvc;

namespace Ui.ViewComponents.RentACarFilterComponents
{
    public class RentACarFilterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(string v)
        {
            TempData["value"] = v;
            return View();
        }
    }
}
