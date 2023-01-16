using InnoGotchiGame.Mvc.Models.Reading;
using System.Collections.Generic;

namespace InnoGotchiGame.Mvc.Models.ViewModels
{
    public class FarmsOverviewResponseModel
    {
        public FarmMinReadingDto MyFarm { get; set; }
        public IEnumerable<FarmMinReadingDto> FriendFarms { get; set; }
    }
}
