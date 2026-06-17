using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MombasaAPI.DataContexts;
using MombasaAPI.Controllers.Filters;
using MombasaAPI.Dtos.Propriedade;
using MombasaAPI.Helpers.Paginated;
using MombasaAPI.Exceptions;
using MombasaAPI.Models;

namespace MombasaAPI.Services;

public class PropriedadeService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PropriedadeService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<PropriedadeResponseDto>> FindAll()
    {
        var propriedades = await _context.Propriedades.ToListAsync();
        return _mapper.Map<ICollection<PropriedadeResponseDto>>(propriedades);
    }

    public async Task<PropriedadeResponseDto> FindById(string id)
    {
        var propriedade = await GetById(id);
        return _mapper.Map<PropriedadeResponseDto>(propriedade);
    }

    public async Task<PropriedadeResponseDto> Create(PropriedadeCreateDto data)
    {
        await ValidarNomeUnicoNaPropriedade(data.Nome, data.ProdutorId, excludeId: null);

        var propriedade = _mapper.Map<Propriedade>(data);
        await _context.Propriedades.AddAsync(propriedade);
        await _context.SaveChangesAsync();

        return _mapper.Map<PropriedadeResponseDto>(propriedade);
    }

    public async Task<PropriedadeResponseDto> Update(string id, PropriedadeUpdateDto data)
    {
        var propriedade = await GetById(id);

        await ValidarNomeUnicoNaPropriedade(data.Nome, data.ProdutorId, excludeId: id);

        _mapper.Map(data, propriedade);
        _context.Propriedades.Update(propriedade);
        await _context.SaveChangesAsync();

        return _mapper.Map<PropriedadeResponseDto>(propriedade);
    }

    public async Task Remove(string id)
    {
        var propriedade = await GetById(id);
        _context.Propriedades.Remove(propriedade);
        await _context.SaveChangesAsync();
    }

    // Busca a entidade ou lança 404

    public async Task<PaginatedResponse<PropriedadeResponseDto>> FindAllV2(PropriedadeFilter filter)
    {
        var query = _context.Propriedades.AsQueryable();

        if (filter.Search is not null)
            query = query.Where(p => p.Nome.Contains(filter.Search)
                                  || (p.Municipio != null && p.Municipio.Contains(filter.Search)));

        if (filter.Estado is not null)
            query = query.Where(p => p.Estado == filter.Estado);

        if (filter.Ativa is not null)
            query = query.Where(p => p.Ativa == filter.Ativa);

        return await Paginate<Propriedade>.Set<PropriedadeResponseDto>(query, filter, _mapper);
    }

    private async Task<Propriedade> GetById(string id)
    {
        var propriedade = await _context.Propriedades.FirstOrDefaultAsync(p => p.Id == id);

        if (propriedade is null)
            throw new ServiceException(
                $"Propriedade #{id} não encontrada",
                c => c.NotFound(new { message = $"Propriedade #{id} não encontrada" })
            );

        return propriedade;
    }

    // Um mesmo produtor não pode ter duas propriedades com o mesmo nome
    private async Task ValidarNomeUnicoNaPropriedade(string nome, string produtorId, string? excludeId)
    {
        var emUso = await _context.Propriedades
            .AnyAsync(p => p.Nome == nome && p.ProdutorId == produtorId && p.Id != excludeId);

        if (emUso)
            throw new ServiceException(
                $"Propriedade '{nome}' já cadastrada para este produtor",
                c => c.Conflict(new { message = $"Este produtor já possui uma propriedade com o nome '{nome}'" })
            );
    }
}
