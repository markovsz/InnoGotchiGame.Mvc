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
        public AuthController()
        {
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
