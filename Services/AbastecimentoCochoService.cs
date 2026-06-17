using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MombasaAPI.DataContexts;
using MombasaAPI.Controllers.Filters;
using MombasaAPI.Dtos.AbastecimentoCocho;
using MombasaAPI.Helpers.Paginated;
using MombasaAPI.Exceptions;
using MombasaAPI.Models;

namespace MombasaAPI.Services;

public class AbastecimentoCochoService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AbastecimentoCochoService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<AbastecimentoCochoResponseDto>> FindAll()
    {
        var abastecimentos = await _context.AbastecimentosCochos.ToListAsync();
        return _mapper.Map<ICollection<AbastecimentoCochoResponseDto>>(abastecimentos);
    }

    public async Task<AbastecimentoCochoResponseDto> FindById(string id)
    {
        var abastecimento = await GetById(id);
        return _mapper.Map<AbastecimentoCochoResponseDto>(abastecimento);
    }

    public async Task<AbastecimentoCochoResponseDto> Create(AbastecimentoCochoCreateDto data)
    {
        var abastecimento = _mapper.Map<AbastecimentoCocho>(data);
        await _context.AbastecimentosCochos.AddAsync(abastecimento);
        await _context.SaveChangesAsync();

        return _mapper.Map<AbastecimentoCochoResponseDto>(abastecimento);
    }

    public async Task<AbastecimentoCochoResponseDto> Update(string id, AbastecimentoCochoUpdateDto data)
    {
        var abastecimento = await GetById(id);

        _mapper.Map(data, abastecimento);
        _context.AbastecimentosCochos.Update(abastecimento);
        await _context.SaveChangesAsync();

        return _mapper.Map<AbastecimentoCochoResponseDto>(abastecimento);
    }

    public async Task Remove(string id)
    {
        var abastecimento = await GetById(id);
        _context.AbastecimentosCochos.Remove(abastecimento);
        await _context.SaveChangesAsync();
    }

    // Busca a entidade ou lança 404

    public async Task<PaginatedResponse<AbastecimentoCochoResponseDto>> FindAllV2(AbastecimentoCochoFilter filter)
    {
        var query = _context.AbastecimentosCochos.AsQueryable();

        if (filter.CochoId is not null)
            query = query.Where(a => a.CochoId == filter.CochoId);

        if (filter.Esgotado is not null)
            query = query.Where(a => a.Esgotado == filter.Esgotado);

        return await Paginate<AbastecimentoCocho>.Set<AbastecimentoCochoResponseDto>(query, filter, _mapper);
    }

    private async Task<AbastecimentoCocho> GetById(string id)
    {
        var abastecimento = await _context.AbastecimentosCochos.FirstOrDefaultAsync(a => a.Id == id);

        if (abastecimento is null)
            throw new ServiceException(
                $"Abastecimento #{id} não encontrado",
                c => c.NotFound(new { message = $"Abastecimento #{id} não encontrado" })
            );

        return abastecimento;
    }
}
