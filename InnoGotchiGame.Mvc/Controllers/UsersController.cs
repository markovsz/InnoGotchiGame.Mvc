using Application.Services.DataTransferObjects.Reading;
using InnoGotchiGame.Mvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace InnoGotchiGame.Mvc.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [Route("my-profile")]
        public async Task<IActionResult> MyProfile()
        {
            var jwtToken = Request.Cookies["jwtToken"];
            var userInfo = await _usersService.GetMyProfile(jwtToken);
            ViewBag.UserInfo = userInfo;
            return View("~/Views/AccountDetails.cshtml");
        }
    }
}
