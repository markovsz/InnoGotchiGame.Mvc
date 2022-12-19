using System;

namespace Application.Services.DataTransferObjects.Reading //TODO: replace namespace to a local
{
    public class UserReadingDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureSrc { get; set; }
    }
}
