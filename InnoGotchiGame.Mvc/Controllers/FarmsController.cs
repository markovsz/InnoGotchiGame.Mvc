using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        [HttpGet("farms/overview")]
        public IActionResult FarmsOverview()
        {
            return View("~/Views/FarmsOverview.cshtml");
        }

        [HttpGet("farms/farm/{farmId}")]
        public IActionResult FarmDetails()
        {
            return View("~/Views/FarmDetails.cshtml");
        }
    }
}
