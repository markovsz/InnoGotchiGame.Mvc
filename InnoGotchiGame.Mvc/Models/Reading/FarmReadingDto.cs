using System;
using System.Collections.Generic;

namespace Application.Services.DataTransferObjects.Reading
{
    public class FarmReadingDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int DeadPetsCount { get; set; }
        public int AlivePetsCount { get; set; }
        public TimeSpan AverageFeedingTime { get; set; }
        public TimeSpan AverageThirstQuenchingTime { get; set; }
        public int AverageHappinessDaysCount { get; set; }
        public int AveragePetsAge { get; set; }

        public IEnumerable<PetReadingDto> Pets { get; set; }
        public IEnumerable<FarmFriendReadingDto> FarmFriends { get; set; }
    }
}
