using System;

namespace InnoGotchiGame.Mvc.Models.RequestParameters
{
    public class PetParameters : PaginationParameters, ICloneable
    {
        public int Age { get; set; }
        public float? MinHungerLevel { get; set; }
        public float? MaxHungerLevel { get; set; }
        public float? MinThirstLevel { get; set; }
        public float? MaxThirstLevel { get; set; }

        public object Clone()
        {
            return new PetParameters() {
                Age = Age,
                MinHungerLevel = MinHungerLevel,
                MaxHungerLevel = MaxHungerLevel,
                MinThirstLevel = MinThirstLevel,
                MaxThirstLevel = MaxThirstLevel,
                PageNumber = PageNumber
            };
        }
    }
}
