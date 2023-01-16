using InnoGotchiGame.Mvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
            var myProfileDetailsResponseModel = await _usersService.GetMyProfile(jwtToken);
            ViewBag.UserInfo = myProfileDetailsResponseModel.ProfileDetails;
            return View("~/Views/AccountDetails.cshtml");
        }
    }
}
