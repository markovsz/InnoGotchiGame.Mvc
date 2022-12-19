using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InnoGotchiGame.Mvc.Models.Reading
{
    public class UserMinReadingDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureSrc { get; set; }
    }
}
