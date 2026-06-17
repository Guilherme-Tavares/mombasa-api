using MombasaAPI.Helpers.Paginated;

namespace MombasaAPI.Controllers.Filters;

public class AlimentoFilter : PaginatedFilter
{
    public string? Tipo { get; set; } = null;
}