using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ui.Helper;

namespace Ui.Controllers
{
    public class ServiceController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
