using Application.Services.DataTransferObjects.Reading;
using InnoGotchiGame.Mvc.Models.Reading;
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
        private readonly IHttpClientFactory _httpClientFactory;

        public FarmsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("farms/overview")]
        public async Task<IActionResult> FarmsOverview()
        {
            var jwtToken = Request.Cookies["jwtToken"];
            var myFarmRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            "https://localhost:44336/api/Farms/my-farm")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };
            var friendFarmsRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            "https://localhost:44336/api/Farms/friends")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };


            var httpClient = _httpClientFactory.CreateClient();

            var myFarmResponseMessage = await httpClient.SendAsync(myFarmRequestMessage);
            if (myFarmResponseMessage.IsSuccessStatusCode)
            {
                var myFarmJson = await myFarmResponseMessage.Content.ReadAsStringAsync();
                var myFarm = JsonConvert.DeserializeObject<FarmMinReadingDto>(myFarmJson);
                ViewBag.MyFarm = myFarm;
            }
            else if (myFarmResponseMessage.StatusCode.Equals(HttpStatusCode.NotFound))
                ViewBag.MyFarm = null;
            else
                return View("~/Views/Error.cshtml", myFarmResponseMessage.StatusCode.ToString());


            var friendFarmsResponseMessage = await httpClient.SendAsync(friendFarmsRequestMessage);
            if (!friendFarmsResponseMessage.IsSuccessStatusCode) 
                return View("~/Views/Error.cshtml", friendFarmsResponseMessage.StatusCode.ToString());
            var friendFarmsJson = await friendFarmsResponseMessage.Content.ReadAsStringAsync();
            var friendFarms = JsonConvert.DeserializeObject<IEnumerable<FarmMinReadingDto>>(friendFarmsJson);
            
            ViewBag.FriendFarms = friendFarms;
            return View("~/Views/FarmsOverview.cshtml");
        }

        [HttpGet]

        [Route("farms/farm/{farmId}")]
        public async Task<IActionResult> FarmDetails(Guid farmId)
        {
            var jwtToken = Request.Cookies["jwtToken"];

            var farmRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://localhost:44336/api/Farms/farm/{farmId}")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };
            var httpClient = _httpClientFactory.CreateClient();
            var farmResponseMessage = await httpClient.SendAsync(farmRequestMessage);
            if (!farmResponseMessage.IsSuccessStatusCode)
                return View("~/Views/Error.cshtml", farmResponseMessage.StatusCode.ToString());
            var farmJson = await farmResponseMessage.Content.ReadAsStringAsync();
            var farm = JsonConvert.DeserializeObject<FarmReadingDto>(farmJson);
            ViewBag.Farm = farm;


            var bodyPicsRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://localhost:44336/api/BodyParts/bodies")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };
            var bodyPicsResponseMessage = await httpClient.SendAsync(bodyPicsRequestMessage);
            if (!bodyPicsResponseMessage.IsSuccessStatusCode)
                return View("~/Views/Error.cshtml", bodyPicsResponseMessage.StatusCode.ToString());
            var bodyPicsJson = await bodyPicsResponseMessage.Content.ReadAsStringAsync();
            var bodyPics = JsonConvert.DeserializeObject<IEnumerable<string>>(bodyPicsJson);
            ViewBag.BodyPics = bodyPics;


            var eyesPicsRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://localhost:44336/api/BodyParts/eyes")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };
            var eyesPicsResponseMessage = await httpClient.SendAsync(eyesPicsRequestMessage);
            if (!eyesPicsResponseMessage.IsSuccessStatusCode)
                return View("~/Views/Error.cshtml", eyesPicsResponseMessage.StatusCode.ToString());
            var eyesPicsJson = await eyesPicsResponseMessage.Content.ReadAsStringAsync();
            var eyesPics = JsonConvert.DeserializeObject<IEnumerable<string>>(eyesPicsJson);
            ViewBag.EyesPics = eyesPics;


            var mouthPicsRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://localhost:44336/api/BodyParts/mouths")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };
            var mouthPicsResponseMessage = await httpClient.SendAsync(mouthPicsRequestMessage);
            if (!mouthPicsResponseMessage.IsSuccessStatusCode)
                return View("~/Views/Error.cshtml", mouthPicsResponseMessage.StatusCode.ToString());
            var mouthPicsJson = await mouthPicsResponseMessage.Content.ReadAsStringAsync();
            var mouthPics = JsonConvert.DeserializeObject<IEnumerable<string>>(mouthPicsJson);
            ViewBag.MouthPics = mouthPics;


            var nosePicsRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://localhost:44336/api/BodyParts/noses")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };
            var nosePicsResponseMessage = await httpClient.SendAsync(nosePicsRequestMessage);
            if (!nosePicsResponseMessage.IsSuccessStatusCode)
                return View("~/Views/Error.cshtml", nosePicsResponseMessage.StatusCode.ToString());
            var nosePicsJson = await nosePicsResponseMessage.Content.ReadAsStringAsync();
            var nosePics = JsonConvert.DeserializeObject<IEnumerable<string>>(nosePicsJson);
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
