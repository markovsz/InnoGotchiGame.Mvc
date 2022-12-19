using InnoGotchiGame.Mvc.Models.Reading;
using System;

namespace Application.Services.DataTransferObjects.Reading
{
    public class FarmMinReadingDto
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
    }
}
