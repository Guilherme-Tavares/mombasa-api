using MombasaAPI.Helpers.Paginated;

namespace MombasaAPI.Controllers.Filters;

public class AbastecimentoCochoFilter : PaginatedFilter
{
    public string? CochoId { get; set; } = null;
    public bool? Esgotado { get; set; } = null;
}