using InnoGotchiGame.Mvc.Models.RequestParameters;
using InnoGotchiGame.Mvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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

            var petOverviewResponseModel = await _petsService.GetPetOverview(petId, jwtToken);
            ViewBag.Pet = petOverviewResponseModel.Pet;

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

            var paginationResponseModel = await _petsService.GetPetsOverview(parameters, jwtToken);

            ViewBag.Pets = paginationResponseModel.Pets;
            ViewBag.PrevPageParameters = paginationResponseModel.PrevPageParametersStr;
            ViewBag.IsPrevPageAvailable = paginationResponseModel.IsPrevPageAvailable;
            ViewBag.CurrentPage = parameters.PageNumber;
            ViewBag.NextPageParameters = paginationResponseModel.NextPageParametersStr;
            ViewBag.IsNextPageAvailable = paginationResponseModel.IsNextPageAvailable;

            return View("~/Views/AllInnogotchiList.cshtml");
        }
    }
}
