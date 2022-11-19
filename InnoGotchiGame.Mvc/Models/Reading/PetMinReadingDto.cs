using System;

namespace Application.Services.DataTransferObjects.Reading
{
    public class PetMinReadingDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int HappinessDaysCount { get; set; }
    }
}
