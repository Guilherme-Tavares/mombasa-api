using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MombasaAPI.DataContexts;
using MombasaAPI.Dtos.Divisao;
using MombasaAPI.Exceptions;
using MombasaAPI.Models;

namespace MombasaAPI.Services;

public class DivisaoService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public DivisaoService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<DivisaoResponseDto>> FindAll()
    {
        var divisoes = await _context.Divisoes.ToListAsync();
        return _mapper.Map<ICollection<DivisaoResponseDto>>(divisoes);
    }

    public async Task<DivisaoResponseDto> FindById(string id)
    {
        var divisao = await GetById(id);
        return _mapper.Map<DivisaoResponseDto>(divisao);
    }

    public async Task<DivisaoResponseDto> Create(DivisaoCreateDto data)
    {
        await ValidarNomeUnicoNaPropriedade(data.Nome, data.PropriedadeId, excludeId: null);

        var divisao = _mapper.Map<Divisao>(data);
        await _context.Divisoes.AddAsync(divisao);
        await _context.SaveChangesAsync();

        return _mapper.Map<DivisaoResponseDto>(divisao);
    }

    public async Task<DivisaoResponseDto> Update(string id, DivisaoUpdateDto data)
    {
        var divisao = await GetById(id);

        await ValidarNomeUnicoNaPropriedade(data.Nome, data.PropriedadeId, excludeId: id);

        _mapper.Map(data, divisao);
        _context.Divisoes.Update(divisao);
        await _context.SaveChangesAsync();

        return _mapper.Map<DivisaoResponseDto>(divisao);
    }

    public async Task Remove(string id)
    {
        var divisao = await GetById(id);
        _context.Divisoes.Remove(divisao);
        await _context.SaveChangesAsync();
    }

    // Busca a entidade ou lança 404
    private async Task<Divisao> GetById(string id)
    {
        var divisao = await _context.Divisoes.FirstOrDefaultAsync(d => d.Id == id);

        if (divisao is null)
            throw new ServiceException(
                $"Divisão #{id} não encontrada",
                c => c.NotFound(new { message = $"Divisão #{id} não encontrada" })
            );

        return divisao;
    }

    // Uma mesma propriedade não pode ter duas divisões com o mesmo nome
    private async Task ValidarNomeUnicoNaPropriedade(string nome, string propriedadeId, string? excludeId)
    {
        var emUso = await _context.Divisoes
            .AnyAsync(d => d.Nome == nome && d.PropriedadeId == propriedadeId && d.Id != excludeId);

        if (emUso)
            throw new ServiceException(
                $"Divisão '{nome}' já cadastrada nesta propriedade",
                c => c.Conflict(new { message = $"Esta propriedade já possui uma divisão com o nome '{nome}'" })
            );
    }
}
