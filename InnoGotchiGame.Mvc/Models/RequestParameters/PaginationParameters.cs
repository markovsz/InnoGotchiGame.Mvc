using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchiGame.Mvc.Models.RequestParameters
{
    public abstract class PaginationParameters
    {
        public int PageNumber { get; set; }
    }
}
