using MombasaAPI.Helpers.Paginated;

namespace MombasaAPI.Controllers.Filters;

public class MedicamentoFilter : PaginatedFilter
{
    public string? Tipo { get; set; } = null;
}