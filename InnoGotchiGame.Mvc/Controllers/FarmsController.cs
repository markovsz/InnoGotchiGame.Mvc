using InnoGotchiGame.Mvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InnoGotchiGame.Mvc.Controllers
{
    public class FarmsController : Controller
    {
        private readonly IFarmsService _farmsService;

        public FarmsController(IFarmsService farmsService)
        {
            _farmsService = farmsService;
        }

        [HttpGet]
        [Route("farms/overview")]
        public async Task<IActionResult> FarmsOverview()
        {
            var jwtToken = Request.Cookies["jwtToken"];

            var farmsOverviewResponseModel = await _farmsService.GetFarmsOverview(jwtToken);

            ViewBag.MyFarm = farmsOverviewResponseModel.MyFarm;
            ViewBag.FriendFarms = farmsOverviewResponseModel.FriendFarms;

            return View("~/Views/FarmsOverview.cshtml");
        }

        [HttpGet]

        [Route("farms/farm/{farmId}")]
        public async Task<IActionResult> FarmDetails(Guid farmId)
        {
            var jwtToken = Request.Cookies["jwtToken"];

            var farmDetailsResponseModel = await _farmsService.GetFarmDetails(farmId, jwtToken);

            ViewBag.Farm = farmDetailsResponseModel.Farm;
            ViewBag.BodyPics = farmDetailsResponseModel.BodyPics;
            ViewBag.EyesPics = farmDetailsResponseModel.EyesPics;
            ViewBag.MouthPics = farmDetailsResponseModel.MouthPics;
            ViewBag.NosePics = farmDetailsResponseModel.NosePics;
            
            var userIdStr = HttpContext.User.Claims
                                         .Where(e => e.Type.Equals(ClaimTypes.NameIdentifier))
                                         .FirstOrDefault().Value;
            
            Guid userId;
            Guid.TryParse(userIdStr, out userId);

            ViewBag.CurrentUserId = userId;
            return View("~/Views/FarmDetails.cshtml");
        }
    }
}
