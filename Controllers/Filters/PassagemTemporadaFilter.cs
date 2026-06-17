using MombasaAPI.Helpers.Paginated;

namespace MombasaAPI.Controllers.Filters;

public class PassagemTemporadaFilter : PaginatedFilter
{
    public string? RebanhoId { get; set; } = null;
    public string? TemporadaId { get; set; } = null;
}