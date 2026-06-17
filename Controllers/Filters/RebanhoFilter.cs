using MombasaAPI.Helpers.Paginated;

namespace MombasaAPI.Controllers.Filters;

public class RebanhoFilter : PaginatedFilter
{
    public string? Finalidade { get; set; } = null;
    public bool? Ativo { get; set; } = null;
}