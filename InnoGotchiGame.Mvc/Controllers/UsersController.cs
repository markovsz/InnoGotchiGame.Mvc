using Application.Services.DataTransferObjects.Reading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace InnoGotchiGame.Mvc.Controllers
{
    public class UsersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UsersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("my-profile")]
        public async Task<IActionResult> MyProfile()
        {
            var jwtToken = Request.Cookies["jwtToken"];
            var userInfoRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            "https://localhost:44336/api/Users/my-profile")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };


            var httpClient = _httpClientFactory.CreateClient();
            var userInfoResponseMessage = await httpClient.SendAsync(userInfoRequestMessage);
            var userInfoJson = await userInfoResponseMessage.Content.ReadAsStringAsync();
            var userInfo = JsonConvert.DeserializeObject<UserReadingDto>(userInfoJson);
            ViewBag.UserInfo = userInfo;
            return View("~/Views/AccountDetails.cshtml");
        }
    }
}
