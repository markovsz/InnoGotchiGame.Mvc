using InnoGotchiGame.Mvc.Models.Reading;
using InnoGotchiGame.Mvc.Models.ViewModels;
using InnoGotchiGame.Mvc.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
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

        public async Task<MyProfileDetailsResponseModel> GetMyProfile(string jwtToken)
        {
            var myProfileDetailsResponseModel = new MyProfileDetailsResponseModel();

            var rootAddress = _configuration.GetSection("BackendRootAddress").Value;
            var userInfoRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            "api/Users/my-profile")
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
            myProfileDetailsResponseModel.ProfileDetails = JsonConvert.DeserializeObject<UserReadingDto>(userInfoJson);

            return myProfileDetailsResponseModel;
        }
    }
}
