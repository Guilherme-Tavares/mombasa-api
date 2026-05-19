using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MombasaAPI.DataContexts;
using MombasaAPI.Dtos.Alimento;
using MombasaAPI.Exceptions;
using MombasaAPI.Models;

namespace MombasaAPI.Services;

public class AlimentoService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AlimentoService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<AlimentoResponseDto>> FindAll()
    {
        var alimentos = await _context.Alimentos.ToListAsync();
        return _mapper.Map<ICollection<AlimentoResponseDto>>(alimentos);
    }

    public async Task<AlimentoResponseDto> FindById(string id)
    {
        var alimento = await GetById(id);
        return _mapper.Map<AlimentoResponseDto>(alimento);
    }

    public async Task<AlimentoResponseDto> Create(AlimentoCreateDto data)
    {
        await ValidarNomeUnico(data.Nome, excludeId: null);

        var alimento = _mapper.Map<Alimento>(data);
        await _context.Alimentos.AddAsync(alimento);
        await _context.SaveChangesAsync();

        return _mapper.Map<AlimentoResponseDto>(alimento);
    }

    public async Task<AlimentoResponseDto> Update(string id, AlimentoUpdateDto data)
    {
        var alimento = await GetById(id);

        await ValidarNomeUnico(data.Nome, excludeId: id);

        _mapper.Map(data, alimento);
        _context.Alimentos.Update(alimento);
        await _context.SaveChangesAsync();

        return _mapper.Map<AlimentoResponseDto>(alimento);
    }

    public async Task Remove(string id)
    {
        var alimento = await GetById(id);
        _context.Alimentos.Remove(alimento);
        await _context.SaveChangesAsync();
    }

    // Busca a entidade ou lança 404
    private async Task<Alimento> GetById(string id)
    {
        var alimento = await _context.Alimentos.FirstOrDefaultAsync(a => a.Id == id);

        if (alimento is null)
            throw new ServiceException(
                $"Alimento #{id} não encontrado",
                c => c.NotFound(new { message = $"Alimento #{id} não encontrado" })
            );

        return alimento;
    }

    // Nome deve ser único na base
    private async Task ValidarNomeUnico(string nome, string? excludeId)
    {
        var emUso = await _context.Alimentos
            .AnyAsync(a => a.Nome == nome && a.Id != excludeId);

        if (emUso)
            throw new ServiceException(
                $"Alimento '{nome}' já cadastrado",
                c => c.Conflict(new { message = $"Alimento '{nome}' já está em uso" })
            );
    }
}
