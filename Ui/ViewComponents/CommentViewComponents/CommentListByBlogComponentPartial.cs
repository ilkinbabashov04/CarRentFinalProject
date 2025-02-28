using Microsoft.AspNetCore.Mvc;

namespace Ui.ViewComponents.CommentViewComponents
{
    public class CommentListByBlogComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
