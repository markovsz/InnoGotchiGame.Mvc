using Application.Services.DataTransferObjects.Reading;
using Domain.Interfaces.RequestParameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InnoGotchiGame.Mvc.Services.Interfaces
{
    public interface IPetsService
    {
        Task<PetReadingDto> GetPetOverview(Guid petId, string jwtToken);
        Task<(IEnumerable<PetMinReadingDto>, string, bool, string, bool)> GetPetsOverview(PetParameters parameters, string jwtToken);
    }
}
