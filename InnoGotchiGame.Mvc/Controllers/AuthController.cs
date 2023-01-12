using Microsoft.AspNetCore.Mvc;

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
