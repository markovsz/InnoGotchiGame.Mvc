namespace Domain.Interfaces.RequestParameters
{
    public class PetParameters : PaginationParameters
    {
        public int Age { get; set; }
        public float? MinHungerLevel { get; set; }
        public float? MaxHungerLevel { get; set; }
        public float? MinThirstLevel { get; set; }
        public float? MaxThirstLevel { get; set; }
    }
}
