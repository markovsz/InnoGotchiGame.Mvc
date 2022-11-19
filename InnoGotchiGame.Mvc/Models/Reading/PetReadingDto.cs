using System;

namespace Application.Services.DataTransferObjects.Reading
{
    public class PetReadingDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string HungerLevel { get; set; }
        public string ThirstLevel { get; set; }
        public int HappinessDaysCount { get; set; }
        public string IsAlive { get; set; }
    }
}
