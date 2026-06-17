using MombasaAPI.Helpers.Paginated;

namespace MombasaAPI.Controllers.Filters;

public class DivisaoFilter : PaginatedFilter
{
    public string? Tipo { get; set; } = null;
    public bool? Ativa { get; set; } = null;
}