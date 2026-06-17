using MombasaAPI.Helpers.Paginated;

namespace MombasaAPI.Controllers.Filters;

public class EstoqueMedicamentoFilter : PaginatedFilter
{
    public string? PropriedadeId { get; set; } = null;
    public string? MedicamentoId { get; set; } = null;
}