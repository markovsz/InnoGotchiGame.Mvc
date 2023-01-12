using Application.Services.DataTransferObjects.Reading;
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

        public async Task<(FarmMinReadingDto, IEnumerable<FarmMinReadingDto>)> GetFarmsOverview(string jwtToken)
        {
            FarmMinReadingDto myFarm = null;
            IEnumerable<FarmMinReadingDto> friendFarms = null;

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
                myFarm = JsonConvert.DeserializeObject<FarmMinReadingDto>(myFarmJson);
            }
            else if (myFarmResponseMessage.StatusCode.Equals(HttpStatusCode.NotFound))
                myFarm = null;
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
                friendFarms = JsonConvert.DeserializeObject<IEnumerable<FarmMinReadingDto>>(friendFarmsJson);
            }

            return (myFarm, friendFarms);
        }

        public async Task<(FarmReadingDto, IEnumerable<string>, IEnumerable<string>, IEnumerable<string>, IEnumerable<string>)> GetFarmDetails(Guid farmId, string jwtToken)
        {
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
            var farm = JsonConvert.DeserializeObject<FarmReadingDto>(farmJson);


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
            var bodyPics = JsonConvert.DeserializeObject<IEnumerable<string>>(bodyPicsJson);


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
            var eyesPics = JsonConvert.DeserializeObject<IEnumerable<string>>(eyesPicsJson);


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
            var mouthPics = JsonConvert.DeserializeObject<IEnumerable<string>>(mouthPicsJson);


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
            var nosePics = JsonConvert.DeserializeObject<IEnumerable<string>>(nosePicsJson);

            return (farm, bodyPics, eyesPics, mouthPics, nosePics);
        }
    }
}
