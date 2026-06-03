using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MombasaAPI.DataContexts;
using MombasaAPI.Dtos.Rebanho;
using MombasaAPI.Exceptions;
using MombasaAPI.Models;

namespace MombasaAPI.Services;

public class RebanhoService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public RebanhoService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<RebanhoResponseDto>> FindAll()
    {
        var rebanhos = await _context.Rebanhos.ToListAsync();
        return _mapper.Map<ICollection<RebanhoResponseDto>>(rebanhos);
    }

    public async Task<RebanhoResponseDto> FindById(string id)
    {
        var rebanho = await GetById(id);
        return _mapper.Map<RebanhoResponseDto>(rebanho);
    }

    public async Task<RebanhoResponseDto> Create(RebanhoCreateDto data)
    {
        await ValidarNomeUnicoNaPropriedade(data.Nome, data.PropriedadeId, excludeId: null);

        var rebanho = _mapper.Map<Rebanho>(data);
        await _context.Rebanhos.AddAsync(rebanho);
        await _context.SaveChangesAsync();

        return _mapper.Map<RebanhoResponseDto>(rebanho);
    }

    public async Task<RebanhoResponseDto> Update(string id, RebanhoUpdateDto data)
    {
        var rebanho = await GetById(id);

        await ValidarNomeUnicoNaPropriedade(data.Nome, data.PropriedadeId, excludeId: id);

        _mapper.Map(data, rebanho);
        _context.Rebanhos.Update(rebanho);
        await _context.SaveChangesAsync();

        return _mapper.Map<RebanhoResponseDto>(rebanho);
    }

    public async Task Remove(string id)
    {
        var rebanho = await GetById(id);
        _context.Rebanhos.Remove(rebanho);
        await _context.SaveChangesAsync();
    }

    // Busca a entidade ou lança 404
    private async Task<Rebanho> GetById(string id)
    {
        var rebanho = await _context.Rebanhos.FirstOrDefaultAsync(r => r.Id == id);

        if (rebanho is null)
            throw new ServiceException(
                $"Rebanho #{id} não encontrado",
                c => c.NotFound(new { message = $"Rebanho #{id} não encontrado" })
            );

        return rebanho;
    }

    // Uma mesma propriedade não pode ter dois rebanhos com o mesmo nome
    private async Task ValidarNomeUnicoNaPropriedade(string nome, string propriedadeId, string? excludeId)
    {
        var emUso = await _context.Rebanhos
            .AnyAsync(r => r.Nome == nome && r.PropriedadeId == propriedadeId && r.Id != excludeId);

        if (emUso)
            throw new ServiceException(
                $"Rebanho '{nome}' já cadastrado nesta propriedade",
                c => c.Conflict(new { message = $"Esta propriedade já possui um rebanho com o nome '{nome}'" })
            );
    }
}
