using MombasaAPI.Helpers.Paginated;

namespace MombasaAPI.Controllers.Filters;

public class CochoFilter : PaginatedFilter
{
    public string? TipoMaterial { get; set; } = null;
    public bool? Ativo { get; set; } = null;
}