using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.DataTransferObjects
{
    public class UserAuthenticationDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
