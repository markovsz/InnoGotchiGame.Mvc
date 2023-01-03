using Application.Services.DataTransferObjects.Reading;
using Domain.Interfaces.RequestParameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

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

            var userIdStr = HttpContext.User.Claims
                                         .Where(e => e.Type.Equals(ClaimTypes.NameIdentifier))
                                         .FirstOrDefault().Value;

            Guid userId;
            Guid.TryParse(userIdStr, out userId);

            ViewBag.CurrentUserId = userId;
            return View("~/Views/InnogotchiDetails.cshtml");
        }

        [HttpGet]
        [Route("pets/list")]
        public async Task<IActionResult> PetsOverview(PetParameters parameters)
        {
            var jwtToken = Request.Cookies["jwtToken"];

            var parametersJson = JsonConvert.SerializeObject(parameters);
            var parametersDictionary = JsonConvert.DeserializeObject<IDictionary<string, string>>(parametersJson);
            var parametersList = parametersDictionary.Select(x => HttpUtility.UrlEncode(x.Key) + "=" + HttpUtility.UrlEncode(x.Value));
            var parametersStr = string.Join("&", parametersList);

            var petsRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://localhost:44336/api/Pets/all?{parametersStr}")
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

            var paginationHeaderValues = petsResponseMessage.Headers.GetValues("X-Pagination-Page-Count");
            var pagesCount = Int32.Parse(paginationHeaderValues.FirstOrDefault());

            bool isPrevPageAvailable = true;
            if (parameters.PageNumber > 1) --parameters.PageNumber;
            else isPrevPageAvailable = false;
            var prevPageParametersJson = JsonConvert.SerializeObject(parameters);
            var prevPageParametersDictionary = JsonConvert.DeserializeObject<IDictionary<string, string>>(prevPageParametersJson);
            var prevPageParametersList = prevPageParametersDictionary.Select(x => HttpUtility.UrlEncode(x.Key) + "=" + HttpUtility.UrlEncode(x.Value));
            var prevPageParametersStr = string.Join("&", prevPageParametersList);
            if (isPrevPageAvailable) ++parameters.PageNumber;

            bool isNextPageAvailable = true;
            if (parameters.PageNumber < pagesCount) ++parameters.PageNumber;
            else isNextPageAvailable = false;
            var nextPageParametersJson = JsonConvert.SerializeObject(parameters);
            var nextPageParametersDictionary = JsonConvert.DeserializeObject<IDictionary<string, string>>(nextPageParametersJson);
            var nextPageParametersList = nextPageParametersDictionary.Select(x => HttpUtility.UrlEncode(x.Key) + "=" + HttpUtility.UrlEncode(x.Value));
            var nextPageParametersStr = string.Join("&", nextPageParametersList);
            if (isNextPageAvailable) --parameters.PageNumber;

            ViewBag.PrevPageParameters = prevPageParametersStr;
            ViewBag.IsPrevPageAvailable = isPrevPageAvailable;
            ViewBag.CurrentPage = parameters.PageNumber;
            ViewBag.NextPageParameters = nextPageParametersStr;
            ViewBag.IsNextPageAvailable = isNextPageAvailable;

            return View("~/Views/AllInnogotchiList.cshtml");
        }
    }
}
