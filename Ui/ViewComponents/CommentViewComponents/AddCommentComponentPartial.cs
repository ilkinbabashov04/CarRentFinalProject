using Microsoft.AspNetCore.Mvc;

namespace Ui.ViewComponents.CommentViewComponents
{
    public class AddCommentComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
