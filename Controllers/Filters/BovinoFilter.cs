using MombasaAPI.Helpers.Paginated;

namespace MombasaAPI.Controllers.Filters;

public class BovinoFilter : PaginatedFilter
{
    public string? Sexo { get; set; } = null;
    public bool? Ativo { get; set; } = null;
}