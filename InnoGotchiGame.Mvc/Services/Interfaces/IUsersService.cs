using InnoGotchiGame.Mvc.Models.Reading;
using System.Threading.Tasks;

namespace InnoGotchiGame.Mvc.Services.Interfaces
{
    public interface IUsersService
    {
        Task<UserReadingDto> GetMyProfile(string jwtToken);
    }
}
