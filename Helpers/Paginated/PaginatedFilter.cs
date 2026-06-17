using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MombasaAPI.Helpers.Paginated;

public class PaginatedFilter : IPaginatedFilter
{
    public string? Search { get; set; } = null;

    [DefaultValue(1)]
    public int Page { get; set; } = 1;

    [DefaultValue(10)]
    [Range(1, 50)]
    public int Limit { get; set; } = 10;
}