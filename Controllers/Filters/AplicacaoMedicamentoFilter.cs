using MombasaAPI.Helpers.Paginated;

namespace MombasaAPI.Controllers.Filters;

public class AplicacaoMedicamentoFilter : PaginatedFilter
{
    public string? BovinoId { get; set; } = null;
    public string? MedicamentoId { get; set; } = null;
}