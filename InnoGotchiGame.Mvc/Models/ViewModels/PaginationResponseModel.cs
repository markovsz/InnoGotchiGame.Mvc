using System.Collections.Generic;

namespace InnoGotchiGame.Mvc.Models.ViewModels
{
    public class PaginationResponseModel<TEntity>
    {
        public IEnumerable<TEntity> Pets { get; set; }
        public string PrevPageParametersStr { get; set; }
        public bool IsPrevPageAvailable { get; set; }
        public int PageNumber { get; set; }
        public string NextPageParametersStr { get; set; }
        public bool IsNextPageAvailable { get; set; }
    }
}
