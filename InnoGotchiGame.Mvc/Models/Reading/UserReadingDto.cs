using System;

namespace Application.Services.DataTransferObjects.Reading
{
    public class UserReadingDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureSrc { get; set; }
    }
}
