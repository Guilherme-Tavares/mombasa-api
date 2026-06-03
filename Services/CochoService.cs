using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MombasaAPI.DataContexts;
using MombasaAPI.Dtos.Cocho;
using MombasaAPI.Exceptions;
using MombasaAPI.Models;

namespace MombasaAPI.Services;

public class CochoService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CochoService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<CochoResponseDto>> FindAll()
    {
        var cochos = await _context.Cochos.ToListAsync();
        return _mapper.Map<ICollection<CochoResponseDto>>(cochos);
    }

    public async Task<CochoResponseDto> FindById(string id)
    {
        var cocho = await GetById(id);
        return _mapper.Map<CochoResponseDto>(cocho);
    }

    public async Task<CochoResponseDto> Create(CochoCreateDto data)
    {
        await ValidarIdentificacaoUnicaNaDivisao(data.Identificacao, data.DivisaoId, excludeId: null);

        var cocho = _mapper.Map<Cocho>(data);
        await _context.Cochos.AddAsync(cocho);
        await _context.SaveChangesAsync();

        return _mapper.Map<CochoResponseDto>(cocho);
    }

    public async Task<CochoResponseDto> Update(string id, CochoUpdateDto data)
    {
        var cocho = await GetById(id);

        await ValidarIdentificacaoUnicaNaDivisao(data.Identificacao, data.DivisaoId, excludeId: id);

        _mapper.Map(data, cocho);
        _context.Cochos.Update(cocho);
        await _context.SaveChangesAsync();

        return _mapper.Map<CochoResponseDto>(cocho);
    }

    public async Task Remove(string id)
    {
        var cocho = await GetById(id);
        _context.Cochos.Remove(cocho);
        await _context.SaveChangesAsync();
    }

    // Busca a entidade ou lança 404
    private async Task<Cocho> GetById(string id)
    {
        var cocho = await _context.Cochos.FirstOrDefaultAsync(c => c.Id == id);

        if (cocho is null)
            throw new ServiceException(
                $"Cocho #{id} não encontrado",
                c => c.NotFound(new { message = $"Cocho #{id} não encontrado" })
            );

        return cocho;
    }

    // Identificação é opcional mas única por divisão quando informada
    private async Task ValidarIdentificacaoUnicaNaDivisao(string? identificacao, string divisaoId, string? excludeId)
    {
        if (identificacao is null) return;

        var emUso = await _context.Cochos
            .AnyAsync(c => c.Identificacao == identificacao && c.DivisaoId == divisaoId && c.Id != excludeId);

        if (emUso)
            throw new ServiceException(
                $"Identificação '{identificacao}' já em uso nesta divisão",
                c => c.Conflict(new { message = $"Esta divisão já possui um cocho com a identificação '{identificacao}'" })
            );
    }
}
