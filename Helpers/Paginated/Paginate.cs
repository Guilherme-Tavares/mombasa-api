using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace MombasaAPI.Helpers.Paginated;

public class Paginate<T>
{
    public static async Task<PaginatedResponse<TDto>> Set<TDto>(
        IQueryable<T> query, IPaginatedFilter paginate, IMapper mapper)
    {
        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)paginate.Limit);

        var list = await query
            .Skip((paginate.Page - 1) * paginate.Limit)
            .Take(paginate.Limit)
            .ToListAsync();

        return new PaginatedResponse<TDto>
        {
            TotalItems = totalItems,
            TotalPages = totalPages,
            Limit = paginate.Limit,
            Page = paginate.Page,
            Data = mapper.Map<IEnumerable<TDto>>(list)
        };
    }
}