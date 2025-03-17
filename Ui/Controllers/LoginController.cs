using Entities.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginDTO loginDto)
        {
            var client = _httpClientFactory.CreateClient();
            return View();
        }
    }
}
