using MombasaAPI.Helpers.Paginated;

namespace MombasaAPI.Controllers.Filters;

public class LotacaoFilter : PaginatedFilter
{
    public string? RebanhoId { get; set; } = null;
    public string? DivisaoId { get; set; } = null;
}