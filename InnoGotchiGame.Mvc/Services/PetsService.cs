using InnoGotchiGame.Mvc.Models.Reading;
using InnoGotchiGame.Mvc.Models.RequestParameters;
using InnoGotchiGame.Mvc.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace InnoGotchiGame.Mvc.Services
{
    public class PetsService : IPetsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public PetsService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<PetReadingDto> GetPetOverview(Guid petId, string jwtToken)
        {
            var petsRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"api/Pets/pet/{petId}")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };

            var httpClientName = _configuration.GetSection("HttpClientName").Value;
            var httpClient = _httpClientFactory.CreateClient(httpClientName);
            var petsResponseMessage = await httpClient.SendAsync(petsRequestMessage);
            var petsJson = await petsResponseMessage.Content.ReadAsStringAsync();
            var pet = JsonConvert.DeserializeObject<PetReadingDto>(petsJson);
            return pet;
        }

        public async Task<(IEnumerable<PetMinReadingDto>, string, bool, string, bool)> GetPetsOverview(PetParameters parameters, string jwtToken)
        {
            string prevPageParametersStr = "";
            string nextPageParametersStr = "";
            var parametersStr = BuildRequestParametersString(parameters);

            var petsRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"api/Pets/all?{parametersStr}")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };

            var httpClientName = _configuration.GetSection("HttpClientName").Value;
            var httpClient = _httpClientFactory.CreateClient(httpClientName);
            var petsResponseMessage = await httpClient.SendAsync(petsRequestMessage);
            var petsJson = await petsResponseMessage.Content.ReadAsStringAsync();
            var pets = JsonConvert.DeserializeObject<IEnumerable<PetMinReadingDto>>(petsJson);

            var paginationHeaderValues = petsResponseMessage.Headers.GetValues("X-Pagination-Page-Count");
            var pagesCount = Int32.Parse(paginationHeaderValues.FirstOrDefault());

            bool isPrevPageAvailable = true;
            if (parameters.PageNumber > 1)
            {
                var prevParameters = (PetParameters) parameters.Clone();
                --prevParameters.PageNumber;
                prevPageParametersStr = BuildRequestParametersString(prevParameters);
            }
            else isPrevPageAvailable = false;

            bool isNextPageAvailable = true;
            if (parameters.PageNumber < pagesCount)
            {
                var nextParameters = (PetParameters) parameters.Clone();
                ++nextParameters.PageNumber;
                nextPageParametersStr = BuildRequestParametersString(nextParameters);
            }
            else isNextPageAvailable = false;
            

            return (pets, prevPageParametersStr, isPrevPageAvailable, nextPageParametersStr, isNextPageAvailable);
        }

        private string BuildRequestParametersString(PetParameters parameters)
        {
            var parametersJson = JsonConvert.SerializeObject(parameters);
            var parametersDictionary = JsonConvert.DeserializeObject<IDictionary<string, string>>(parametersJson);
            var parametersList = parametersDictionary.Select(x => HttpUtility.UrlEncode(x.Key) + "=" + HttpUtility.UrlEncode(x.Value));
            var parametersStr = string.Join("&", parametersList);
            return parametersStr;
        }
    }
}
