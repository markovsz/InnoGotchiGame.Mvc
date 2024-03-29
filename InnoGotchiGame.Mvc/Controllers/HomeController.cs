﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InnoGotchiGame.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            if(HttpContext.User.Identity.IsAuthenticated)
                return RedirectPermanent("/farms/overview");
            return RedirectPermanent("/login");
        }
    }
}
