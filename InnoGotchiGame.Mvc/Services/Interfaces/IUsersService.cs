using InnoGotchiGame.Mvc.Models.Reading;
using InnoGotchiGame.Mvc.Models.ViewModels;
using System.Threading.Tasks;

namespace InnoGotchiGame.Mvc.Services.Interfaces
{
    public interface IUsersService
    {
        Task<MyProfileDetailsResponseModel> GetMyProfile(string jwtToken);
    }
}
