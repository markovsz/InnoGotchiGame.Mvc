using InnoGotchiGame.Mvc.Models.Reading;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InnoGotchiGame.Mvc.Services.Interfaces
{
    public interface IFarmsService
    {
        Task<(FarmMinReadingDto, IEnumerable<FarmMinReadingDto>)> GetFarmsOverview(string jwtToken);
        Task<(FarmReadingDto, IEnumerable<string>, IEnumerable<string>, IEnumerable<string>, IEnumerable<string>)> GetFarmDetails(Guid farmId, string jwtToken);
    }
}
