using MombasaAPI.Helpers.Paginated;

namespace MombasaAPI.Controllers.Filters;

public class ForragemFilter : PaginatedFilter
{
    public bool? Ativa { get; set; } = null;
}