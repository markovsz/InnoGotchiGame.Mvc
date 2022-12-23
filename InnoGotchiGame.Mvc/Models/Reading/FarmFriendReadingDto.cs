using System;

namespace Application.Services.DataTransferObjects.Reading
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
