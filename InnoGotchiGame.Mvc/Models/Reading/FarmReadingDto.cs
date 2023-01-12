using System;
using System.Collections.Generic;

namespace InnoGotchiGame.Mvc.Models.Reading
{
    public class FarmReadingDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int DeadPetsCount { get; set; }
        public int AlivePetsCount { get; set; }
        public float AverageFeedingTime { get; set; }
        public float AverageThirstQuenchingTime { get; set; }
        public int AverageHappinessDaysCount { get; set; }
        public int AveragePetsAge { get; set; }
        public UserMinReadingDto UserInfo { get; set; }

        public IEnumerable<PetReadingDto> Pets { get; set; }
        public IEnumerable<FarmFriendReadingDto> FarmFriends { get; set; }
    }
}
