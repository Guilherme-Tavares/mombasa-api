using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MombasaAPI.DataContexts;
using MombasaAPI.Dtos.Pertencimento;
using MombasaAPI.Exceptions;
using MombasaAPI.Models;

namespace MombasaAPI.Services;

public class PertencimentoService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PertencimentoService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<PertencimentoResponseDto>> FindAll()
    {
        var pertencimentos = await _context.Pertencimentos.ToListAsync();
        return _mapper.Map<ICollection<PertencimentoResponseDto>>(pertencimentos);
    }

    public async Task<PertencimentoResponseDto> FindById(string id)
    {
        var pertencimento = await GetById(id);
        return _mapper.Map<PertencimentoResponseDto>(pertencimento);
    }

    public async Task<PertencimentoResponseDto> Create(PertencimentoCreateDto data)
    {
        await ValidarUnicidade(data.BovinoId, data.RebanhoId, data.DataEntrada, excludeId: null);

        var pertencimento = _mapper.Map<Pertencimento>(data);
        await _context.Pertencimentos.AddAsync(pertencimento);
        await _context.SaveChangesAsync();

        return _mapper.Map<PertencimentoResponseDto>(pertencimento);
    }

    public async Task<PertencimentoResponseDto> Update(string id, PertencimentoUpdateDto data)
    {
        var pertencimento = await GetById(id);

        await ValidarUnicidade(data.BovinoId, data.RebanhoId, data.DataEntrada, excludeId: id);

        _mapper.Map(data, pertencimento);
        _context.Pertencimentos.Update(pertencimento);
        await _context.SaveChangesAsync();

        return _mapper.Map<PertencimentoResponseDto>(pertencimento);
    }

    public async Task Remove(string id)
    {
        var pertencimento = await GetById(id);
        _context.Pertencimentos.Remove(pertencimento);
        await _context.SaveChangesAsync();
    }

    // Busca a entidade ou lança 404
    private async Task<Pertencimento> GetById(string id)
    {
        var pertencimento = await _context.Pertencimentos.FirstOrDefaultAsync(p => p.Id == id);

        if (pertencimento is null)
            throw new ServiceException(
                $"Pertencimento #{id} não encontrado",
                c => c.NotFound(new { message = $"Pertencimento #{id} não encontrado" })
            );

        return pertencimento;
    }

    // Índice único composto: (BovinoId, RebanhoId, DataEntrada)
    private async Task ValidarUnicidade(string bovinoId, string rebanhoId, DateOnly dataEntrada, string? excludeId)
    {
        var emUso = await _context.Pertencimentos
            .AnyAsync(p => p.BovinoId == bovinoId && p.RebanhoId == rebanhoId
                        && p.DataEntrada == dataEntrada && p.Id != excludeId);

        if (emUso)
            throw new ServiceException(
                "Já existe um pertencimento para este bovino e rebanho nesta data de entrada",
                c => c.Conflict(new { message = "Já existe um pertencimento para este bovino e rebanho nesta data de entrada" })
            );
    }
}
