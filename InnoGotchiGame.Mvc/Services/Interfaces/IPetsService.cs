using InnoGotchiGame.Mvc.Models.Reading;
using InnoGotchiGame.Mvc.Models.RequestParameters;
using InnoGotchiGame.Mvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InnoGotchiGame.Mvc.Services.Interfaces
{
    public interface IPetsService
    {
        Task<PetOverviewResponseModel> GetPetOverview(Guid petId, string jwtToken);
        Task<PaginationResponseModel<PetMinReadingDto>> GetPetsOverview(PetParameters parameters, string jwtToken);
    }
}
