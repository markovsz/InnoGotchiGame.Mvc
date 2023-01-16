using InnoGotchiGame.Mvc.Models.Reading;
using System.Collections.Generic;

namespace InnoGotchiGame.Mvc.Models.ViewModels
{
    public class FarmDetailsResponseModel
    {
        public FarmReadingDto Farm { get; set; }
        public IEnumerable<string> BodyPics { get; set; }
        public IEnumerable<string> EyesPics { get; set; }
        public IEnumerable<string> MouthPics { get; set; }
        public IEnumerable<string> NosePics { get; set; }
    }
}
