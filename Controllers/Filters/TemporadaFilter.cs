using MombasaAPI.Helpers.Paginated;

namespace MombasaAPI.Controllers.Filters;

public class TemporadaFilter : PaginatedFilter
{
    public string? Tipo { get; set; } = null;
}