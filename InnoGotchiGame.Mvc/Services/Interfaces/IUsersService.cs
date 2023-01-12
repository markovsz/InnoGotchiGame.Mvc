using InnoGotchiGame.Mvc.Models.Reading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InnoGotchiGame.Mvc.Services.Interfaces
{
    public interface IUsersService
    {
        Task<UserReadingDto> GetMyProfile(string jwtToken);
    }
}
