using Microsoft.AspNetCore.Mvc;

namespace Ui.ViewComponents.BlogViewComponents
{
    public class BlogDetailSideBarSearchBoxComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
