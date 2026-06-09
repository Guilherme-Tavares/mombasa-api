using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MombasaAPI.DataContexts;
using MombasaAPI.Dtos.Lotacao;
using MombasaAPI.Exceptions;
using MombasaAPI.Models;

namespace MombasaAPI.Services;

public class LotacaoService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public LotacaoService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<LotacaoResponseDto>> FindAll()
    {
        var lotacoes = await _context.Lotacoes.ToListAsync();
        return _mapper.Map<ICollection<LotacaoResponseDto>>(lotacoes);
    }

    public async Task<LotacaoResponseDto> FindById(string id)
    {
        var lotacao = await GetById(id);
        return _mapper.Map<LotacaoResponseDto>(lotacao);
    }

    public async Task<LotacaoResponseDto> Create(LotacaoCreateDto data)
    {
        await ValidarUnicidade(data.RebanhoId, data.DivisaoId, data.DataEntrada, excludeId: null);

        var lotacao = _mapper.Map<Lotacao>(data);
        await _context.Lotacoes.AddAsync(lotacao);
        await _context.SaveChangesAsync();

        return _mapper.Map<LotacaoResponseDto>(lotacao);
    }

    public async Task<LotacaoResponseDto> Update(string id, LotacaoUpdateDto data)
    {
        var lotacao = await GetById(id);

        await ValidarUnicidade(data.RebanhoId, data.DivisaoId, data.DataEntrada, excludeId: id);

        _mapper.Map(data, lotacao);
        _context.Lotacoes.Update(lotacao);
        await _context.SaveChangesAsync();

        return _mapper.Map<LotacaoResponseDto>(lotacao);
    }

    public async Task Remove(string id)
    {
        var lotacao = await GetById(id);
        _context.Lotacoes.Remove(lotacao);
        await _context.SaveChangesAsync();
    }

    // Busca a entidade ou lança 404
    private async Task<Lotacao> GetById(string id)
    {
        var lotacao = await _context.Lotacoes.FirstOrDefaultAsync(l => l.Id == id);

        if (lotacao is null)
            throw new ServiceException(
                $"Lotação #{id} não encontrada",
                c => c.NotFound(new { message = $"Lotação #{id} não encontrada" })
            );

        return lotacao;
    }

    // Índice único composto: (RebanhoId, DivisaoId, DataEntrada)
    private async Task ValidarUnicidade(string rebanhoId, string divisaoId, DateOnly dataEntrada, string? excludeId)
    {
        var emUso = await _context.Lotacoes
            .AnyAsync(l => l.RebanhoId == rebanhoId && l.DivisaoId == divisaoId
                        && l.DataEntrada == dataEntrada && l.Id != excludeId);

        if (emUso)
            throw new ServiceException(
                "Já existe uma lotação para este rebanho e divisão nesta data de entrada",
                c => c.Conflict(new { message = "Já existe uma lotação para este rebanho e divisão nesta data de entrada" })
            );
    }
}
