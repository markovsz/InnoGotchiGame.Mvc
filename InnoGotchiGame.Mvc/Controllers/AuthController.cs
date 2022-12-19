using Application.Services.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InnoGotchiGame.Mvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("login")]
        public IActionResult GetLogInPage()
        {
            return View("~/Views/LogIn.cshtml");
        }

        [HttpGet("reg")]
        public IActionResult GetRegPage()
        {
            return View("~/Views/Reg.cshtml");
        }
    }
}
