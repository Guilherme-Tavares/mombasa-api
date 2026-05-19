using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MombasaAPI.DataContexts;
using MombasaAPI.Dtos.Temporada;
using MombasaAPI.Exceptions;
using MombasaAPI.Models;

namespace MombasaAPI.Services;

public class TemporadaService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TemporadaService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<TemporadaResponseDto>> FindAll()
    {
        var temporadas = await _context.Temporadas.ToListAsync();
        return _mapper.Map<ICollection<TemporadaResponseDto>>(temporadas);
    }

    public async Task<TemporadaResponseDto> FindById(string id)
    {
        var temporada = await GetById(id);
        return _mapper.Map<TemporadaResponseDto>(temporada);
    }

    public async Task<TemporadaResponseDto> Create(TemporadaCreateDto data)
    {
        var temporada = _mapper.Map<Temporada>(data);
        await _context.Temporadas.AddAsync(temporada);
        await _context.SaveChangesAsync();

        return _mapper.Map<TemporadaResponseDto>(temporada);
    }

    public async Task<TemporadaResponseDto> Update(string id, TemporadaUpdateDto data)
    {
        var temporada = await GetById(id);

        _mapper.Map(data, temporada);
        _context.Temporadas.Update(temporada);
        await _context.SaveChangesAsync();

        return _mapper.Map<TemporadaResponseDto>(temporada);
    }

    public async Task Remove(string id)
    {
        var temporada = await GetById(id);
        _context.Temporadas.Remove(temporada);
        await _context.SaveChangesAsync();
    }

    // Busca a entidade ou lança 404
    private async Task<Temporada> GetById(string id)
    {
        var temporada = await _context.Temporadas.FirstOrDefaultAsync(t => t.Id == id);

        if (temporada is null)
            throw new ServiceException(
                $"Temporada #{id} não encontrada",
                c => c.NotFound(new { message = $"Temporada #{id} não encontrada" })
            );

        return temporada;
    }
}
