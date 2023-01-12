using Application.Services.DataTransferObjects.Reading;
using InnoGotchiGame.Mvc.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace InnoGotchiGame.Mvc.Services
{
    public class UsersService : IUsersService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public UsersService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<UserReadingDto> GetMyProfile(string jwtToken)
        {
            var rootAddress = _configuration.GetSection("BackendRootAddress").Value;
            var userInfoRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            "api/my-profile")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwtToken}" }
                }
            };


            var httpClientName = _configuration.GetSection("HttpClientName").Value;
            var httpClient = _httpClientFactory.CreateClient(httpClientName);
            var userInfoResponseMessage = await httpClient.SendAsync(userInfoRequestMessage);
            var userInfoJson = await userInfoResponseMessage.Content.ReadAsStringAsync();
            var userInfo = JsonConvert.DeserializeObject<UserReadingDto>(userInfoJson);

            return userInfo;
        }
    }
}
