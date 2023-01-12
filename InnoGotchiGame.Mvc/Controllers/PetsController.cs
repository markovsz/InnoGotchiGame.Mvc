using InnoGotchiGame.Mvc.Models.RequestParameters;
using InnoGotchiGame.Mvc.Services.Interfaces;
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
        private readonly IPetsService _petsService;

        public PetsController(IPetsService petsService)
        {
            _petsService = petsService;
        }

        [HttpGet]
        [Route("pets/pet/{petId}/details")]
        public async Task<IActionResult> PetOverview(Guid petId)
        {
            var jwtToken = Request.Cookies["jwtToken"];

            var pet = await _petsService.GetPetOverview(petId, jwtToken);
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

            (var pets, var prevPageParametersStr, var isPrevPageAvailable, var nextPageParametersStr, var isNextPageAvailable) = await _petsService.GetPetsOverview(parameters, jwtToken);

            ViewBag.Pets = pets;
            ViewBag.PrevPageParameters = prevPageParametersStr;
            ViewBag.IsPrevPageAvailable = isPrevPageAvailable;
            ViewBag.CurrentPage = parameters.PageNumber;
            ViewBag.NextPageParameters = nextPageParametersStr;
            ViewBag.IsNextPageAvailable = isNextPageAvailable;

            return View("~/Views/AllInnogotchiList.cshtml");
        }
    }
}
