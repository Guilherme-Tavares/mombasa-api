using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MombasaAPI.DataContexts;
using MombasaAPI.Dtos.EstoqueMedicamento;
using MombasaAPI.Exceptions;
using MombasaAPI.Models;

namespace MombasaAPI.Services;

public class EstoqueMedicamentoService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public EstoqueMedicamentoService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<EstoqueMedicamentoResponseDto>> FindAll()
    {
        var estoques = await _context.EstoquesMedicamentos.ToListAsync();
        return _mapper.Map<ICollection<EstoqueMedicamentoResponseDto>>(estoques);
    }

    public async Task<EstoqueMedicamentoResponseDto> FindById(string id)
    {
        var estoque = await GetById(id);
        return _mapper.Map<EstoqueMedicamentoResponseDto>(estoque);
    }

    public async Task<EstoqueMedicamentoResponseDto> Create(EstoqueMedicamentoCreateDto data)
    {
        await ValidarUnicidade(data.PropriedadeId, data.MedicamentoId, excludeId: null);

        var estoque = _mapper.Map<EstoqueMedicamento>(data);
        await _context.EstoquesMedicamentos.AddAsync(estoque);
        await _context.SaveChangesAsync();

        return _mapper.Map<EstoqueMedicamentoResponseDto>(estoque);
    }

    public async Task<EstoqueMedicamentoResponseDto> Update(string id, EstoqueMedicamentoUpdateDto data)
    {
        var estoque = await GetById(id);

        await ValidarUnicidade(data.PropriedadeId, data.MedicamentoId, excludeId: id);

        _mapper.Map(data, estoque);
        _context.EstoquesMedicamentos.Update(estoque);
        await _context.SaveChangesAsync();

        return _mapper.Map<EstoqueMedicamentoResponseDto>(estoque);
    }

    public async Task Remove(string id)
    {
        var estoque = await GetById(id);
        _context.EstoquesMedicamentos.Remove(estoque);
        await _context.SaveChangesAsync();
    }

    // Busca a entidade ou lança 404
    private async Task<EstoqueMedicamento> GetById(string id)
    {
        var estoque = await _context.EstoquesMedicamentos.FirstOrDefaultAsync(e => e.Id == id);

        if (estoque is null)
            throw new ServiceException(
                $"Estoque #{id} não encontrado",
                c => c.NotFound(new { message = $"Estoque #{id} não encontrado" })
            );

        return estoque;
    }

    // Índice único composto: (PropriedadeId, MedicamentoId)
    private async Task ValidarUnicidade(string propriedadeId, string medicamentoId, string? excludeId)
    {
        var emUso = await _context.EstoquesMedicamentos
            .AnyAsync(e => e.PropriedadeId == propriedadeId && e.MedicamentoId == medicamentoId && e.Id != excludeId);

        if (emUso)
            throw new ServiceException(
                "Esta propriedade já possui um estoque cadastrado para este medicamento",
                c => c.Conflict(new { message = "Esta propriedade já possui um estoque cadastrado para este medicamento" })
            );
    }
}
