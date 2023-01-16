using InnoGotchiGame.Mvc.Models.Reading;
using InnoGotchiGame.Mvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InnoGotchiGame.Mvc.Services.Interfaces
{
    public interface IFarmsService
    {
        Task<FarmsOverviewResponseModel> GetFarmsOverview(string jwtToken);
        Task<FarmDetailsResponseModel> GetFarmDetails(Guid farmId, string jwtToken);
    }
}
