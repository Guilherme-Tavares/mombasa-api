using MombasaAPI.Helpers.Paginated;

namespace MombasaAPI.Controllers.Filters;

public class PropriedadeFilter : PaginatedFilter
{
    public string? Estado { get; set; } = null;
    public bool? Ativa { get; set; } = null;
}