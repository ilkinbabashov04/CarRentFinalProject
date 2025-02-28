using Microsoft.AspNetCore.Mvc;

namespace Ui.ViewComponents.BlogViewComponents
{
    public class BlogDetailsParagraphComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
