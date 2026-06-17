using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MombasaAPI.DataContexts;
using MombasaAPI.Controllers.Filters;
using MombasaAPI.Dtos.PassagemTemporada;
using MombasaAPI.Helpers.Paginated;
using MombasaAPI.Exceptions;
using MombasaAPI.Models;

namespace MombasaAPI.Services;

public class PassagemTemporadaService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PassagemTemporadaService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<PassagemTemporadaResponseDto>> FindAll()
    {
        var passagens = await _context.PassagensTemporada.ToListAsync();
        return _mapper.Map<ICollection<PassagemTemporadaResponseDto>>(passagens);
    }

    public async Task<PassagemTemporadaResponseDto> FindById(string id)
    {
        var passagem = await GetById(id);
        return _mapper.Map<PassagemTemporadaResponseDto>(passagem);
    }

    public async Task<PassagemTemporadaResponseDto> Create(PassagemTemporadaCreateDto data)
    {
        await ValidarUnicidade(data.RebanhoId, data.TemporadaId, excludeId: null);

        var passagem = _mapper.Map<PassagemTemporada>(data);
        await _context.PassagensTemporada.AddAsync(passagem);
        await _context.SaveChangesAsync();

        return _mapper.Map<PassagemTemporadaResponseDto>(passagem);
    }

    public async Task<PassagemTemporadaResponseDto> Update(string id, PassagemTemporadaUpdateDto data)
    {
        var passagem = await GetById(id);

        await ValidarUnicidade(data.RebanhoId, data.TemporadaId, excludeId: id);

        _mapper.Map(data, passagem);
        _context.PassagensTemporada.Update(passagem);
        await _context.SaveChangesAsync();

        return _mapper.Map<PassagemTemporadaResponseDto>(passagem);
    }

    public async Task Remove(string id)
    {
        var passagem = await GetById(id);
        _context.PassagensTemporada.Remove(passagem);
        await _context.SaveChangesAsync();
    }

    // Busca a entidade ou lança 404

    public async Task<PaginatedResponse<PassagemTemporadaResponseDto>> FindAllV2(PassagemTemporadaFilter filter)
    {
        var query = _context.PassagensTemporada.AsQueryable();

        if (filter.RebanhoId is not null)
            query = query.Where(p => p.RebanhoId == filter.RebanhoId);

        if (filter.TemporadaId is not null)
            query = query.Where(p => p.TemporadaId == filter.TemporadaId);

        return await Paginate<PassagemTemporada>.Set<PassagemTemporadaResponseDto>(query, filter, _mapper);
    }

    private async Task<PassagemTemporada> GetById(string id)
    {
        var passagem = await _context.PassagensTemporada.FirstOrDefaultAsync(p => p.Id == id);

        if (passagem is null)
            throw new ServiceException(
                $"Passagem de temporada #{id} não encontrada",
                c => c.NotFound(new { message = $"Passagem de temporada #{id} não encontrada" })
            );

        return passagem;
    }

    // Índice único composto: (RebanhoId, TemporadaId)
    private async Task ValidarUnicidade(string rebanhoId, string temporadaId, string? excludeId)
    {
        var emUso = await _context.PassagensTemporada
            .AnyAsync(p => p.RebanhoId == rebanhoId && p.TemporadaId == temporadaId && p.Id != excludeId);

        if (emUso)
            throw new ServiceException(
                "Este rebanho já possui uma passagem registrada para esta temporada",
                c => c.Conflict(new { message = "Este rebanho já possui uma passagem registrada para esta temporada" })
            );
    }
}
