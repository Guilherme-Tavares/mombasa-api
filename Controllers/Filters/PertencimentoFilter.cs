using MombasaAPI.Helpers.Paginated;

namespace MombasaAPI.Controllers.Filters;

public class PertencimentoFilter : PaginatedFilter
{
    public string? BovinoId { get; set; } = null;
    public string? RebanhoId { get; set; } = null;
}