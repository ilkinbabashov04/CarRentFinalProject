﻿using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
	public class AboutController : Controller
	{
		public IActionResult Index()
		{
			ViewBag.v1 = "About Us";
			ViewBag.v2 = "About Us";

            return View();
		}
	}
}
