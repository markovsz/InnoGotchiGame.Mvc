using Application.Services.DataTransferObjects.Reading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InnoGotchiGame.Mvc.Controllers
{
    public class PetsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PetsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("pets/pet/{petId}/details")]
        public async Task<IActionResult> PetOverview(Guid petId)
        {
            var jwtToken = Request.Cookies["jwtToken"];
            var petsRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://localhost:44336/api/Pets/pet/{petId}")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };


            var httpClient = _httpClientFactory.CreateClient();
            var petsResponseMessage = await httpClient.SendAsync(petsRequestMessage);
            var petsJson = await petsResponseMessage.Content.ReadAsStringAsync();
            var pet = JsonConvert.DeserializeObject<PetReadingDto>(petsJson);
            ViewBag.Pet = pet;
            return View("~/Views/InnogotchiDetails.cshtml");
        }

        [HttpGet]
        [Route("pets/list")]
        public async Task<IActionResult> PetsOverview()
        {
            var jwtToken = Request.Cookies["jwtToken"];
            var petsRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            "https://localhost:44336/api/Pets/all")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };


            var httpClient = _httpClientFactory.CreateClient();
            var petsResponseMessage = await httpClient.SendAsync(petsRequestMessage);
            var petsJson = await petsResponseMessage.Content.ReadAsStringAsync();
            var pets = JsonConvert.DeserializeObject<IEnumerable<PetMinReadingDto>>(petsJson);
            ViewBag.Pets = pets;
            return View("~/Views/AllInnogotchiList.cshtml");
        }
    }
}
