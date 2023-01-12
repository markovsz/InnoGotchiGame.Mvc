using System;

namespace InnoGotchiGame.Mvc.Models.Reading
{
    public class FarmFriendReadingDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string PictureSrc { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
