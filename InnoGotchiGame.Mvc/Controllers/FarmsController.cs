using Application.Services.DataTransferObjects.Reading;
using InnoGotchiGame.Mvc.Models.Reading;
using InnoGotchiGame.Mvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

            (var myFarm, var friendFarms) = await _farmsService.GetFarmsOverview(jwtToken);

            ViewBag.MyFarm = myFarm;
            ViewBag.FriendFarms = friendFarms;

            return View("~/Views/FarmsOverview.cshtml");
        }

        [HttpGet]

        [Route("farms/farm/{farmId}")]
        public async Task<IActionResult> FarmDetails(Guid farmId)
        {
            var jwtToken = Request.Cookies["jwtToken"];

            (var farm, var bodyPics, var eyesPics, var mouthPics, var nosePics) = await _farmsService.GetFarmDetails(farmId, jwtToken);

            ViewBag.Farm = farm;
            ViewBag.BodyPics = bodyPics;
            ViewBag.EyesPics = eyesPics;
            ViewBag.MouthPics = mouthPics;
            ViewBag.NosePics = nosePics;
            
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
