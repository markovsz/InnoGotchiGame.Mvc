using System;

namespace InnoGotchiGame.Mvc.Models.Reading
{
    public class PetMinReadingDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int HappinessDaysCount { get; set; }
    }
}
