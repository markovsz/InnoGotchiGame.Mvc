using InnoGotchiGame.Mvc.Models.Reading;
using InnoGotchiGame.Mvc.Models.ViewModels;
using InnoGotchiGame.Mvc.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace InnoGotchiGame.Mvc.Services
{
    public class FarmsService : IFarmsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FarmsService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<FarmsOverviewResponseModel> GetFarmsOverview(string jwtToken)
        {
            var farmsOverviewResponseModel = new FarmsOverviewResponseModel();
            var myFarmRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            "api/Farms/my-farm")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };

            var httpClientName = _configuration.GetSection("HttpClientName").Value;
            var httpClient = _httpClientFactory.CreateClient(httpClientName);

            var myFarmResponseMessage = await httpClient.SendAsync(myFarmRequestMessage);
            if (myFarmResponseMessage.IsSuccessStatusCode)
            {
                var myFarmJson = await myFarmResponseMessage.Content.ReadAsStringAsync();
                farmsOverviewResponseModel.MyFarm = JsonConvert.DeserializeObject<FarmMinReadingDto>(myFarmJson);
            }
            else if (myFarmResponseMessage.StatusCode.Equals(HttpStatusCode.NotFound))
                farmsOverviewResponseModel.MyFarm = null;
            else
                throw new HttpResponseException(myFarmResponseMessage.StatusCode);


            var friendFarmsRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            "api/Farms/friends")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };

            var friendFarmsResponseMessage = await httpClient.SendAsync(friendFarmsRequestMessage);
            if (friendFarmsResponseMessage.IsSuccessStatusCode)
            {
                var friendFarmsJson = await friendFarmsResponseMessage.Content.ReadAsStringAsync();
                farmsOverviewResponseModel.FriendFarms = JsonConvert.DeserializeObject<IEnumerable<FarmMinReadingDto>>(friendFarmsJson);
            }

            return farmsOverviewResponseModel;
        }

        public async Task<FarmDetailsResponseModel> GetFarmDetails(Guid farmId, string jwtToken)
        {
            var farmDetailsResponseModel = new FarmDetailsResponseModel();

            var farmRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"api/Farms/farm/{farmId}")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };
            var httpClientName = _configuration.GetSection("HttpClientName").Value;
            var httpClient = _httpClientFactory.CreateClient(httpClientName);
            var farmResponseMessage = await httpClient.SendAsync(farmRequestMessage);
            if (!farmResponseMessage.IsSuccessStatusCode)
                throw new Exception(farmResponseMessage.StatusCode.ToString());
            var farmJson = await farmResponseMessage.Content.ReadAsStringAsync();
            farmDetailsResponseModel.Farm = JsonConvert.DeserializeObject<FarmReadingDto>(farmJson);


            var bodyPicsRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"api/BodyParts/bodies")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };
            var bodyPicsResponseMessage = await httpClient.SendAsync(bodyPicsRequestMessage);
            if (!bodyPicsResponseMessage.IsSuccessStatusCode)
                throw new Exception(bodyPicsResponseMessage.StatusCode.ToString());
            var bodyPicsJson = await bodyPicsResponseMessage.Content.ReadAsStringAsync();
            farmDetailsResponseModel.BodyPics = JsonConvert.DeserializeObject<IEnumerable<string>>(bodyPicsJson);


            var eyesPicsRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"api/BodyParts/eyes")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };
            var eyesPicsResponseMessage = await httpClient.SendAsync(eyesPicsRequestMessage);
            if (!eyesPicsResponseMessage.IsSuccessStatusCode)
                throw new Exception(eyesPicsResponseMessage.StatusCode.ToString());
            var eyesPicsJson = await eyesPicsResponseMessage.Content.ReadAsStringAsync();
            farmDetailsResponseModel.EyesPics = JsonConvert.DeserializeObject<IEnumerable<string>>(eyesPicsJson);


            var mouthPicsRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"api/BodyParts/mouths")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };
            var mouthPicsResponseMessage = await httpClient.SendAsync(mouthPicsRequestMessage);
            if (!mouthPicsResponseMessage.IsSuccessStatusCode)
                throw new Exception(mouthPicsResponseMessage.StatusCode.ToString());
            var mouthPicsJson = await mouthPicsResponseMessage.Content.ReadAsStringAsync();
            farmDetailsResponseModel.MouthPics = JsonConvert.DeserializeObject<IEnumerable<string>>(mouthPicsJson);


            var nosePicsRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"api/BodyParts/noses")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };
            var nosePicsResponseMessage = await httpClient.SendAsync(nosePicsRequestMessage);
            if (!nosePicsResponseMessage.IsSuccessStatusCode)
                throw new Exception(nosePicsResponseMessage.StatusCode.ToString());
            var nosePicsJson = await nosePicsResponseMessage.Content.ReadAsStringAsync();
            farmDetailsResponseModel.NosePics = JsonConvert.DeserializeObject<IEnumerable<string>>(nosePicsJson);

            return farmDetailsResponseModel;
        }
    }
}
