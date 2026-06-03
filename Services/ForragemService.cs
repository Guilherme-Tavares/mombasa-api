using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MombasaAPI.DataContexts;
using MombasaAPI.Dtos.Forragem;
using MombasaAPI.Exceptions;
using MombasaAPI.Models;

namespace MombasaAPI.Services;

public class ForragemService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ForragemService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<ForragemResponseDto>> FindAll()
    {
        var forragens = await _context.Forragens.ToListAsync();
        return _mapper.Map<ICollection<ForragemResponseDto>>(forragens);
    }

    public async Task<ForragemResponseDto> FindById(string id)
    {
        var forragem = await GetById(id);
        return _mapper.Map<ForragemResponseDto>(forragem);
    }

    public async Task<ForragemResponseDto> Create(ForragemCreateDto data)
    {
        var forragem = _mapper.Map<Forragem>(data);
        await _context.Forragens.AddAsync(forragem);
        await _context.SaveChangesAsync();

        return _mapper.Map<ForragemResponseDto>(forragem);
    }

    public async Task<ForragemResponseDto> Update(string id, ForragemUpdateDto data)
    {
        var forragem = await GetById(id);

        _mapper.Map(data, forragem);
        _context.Forragens.Update(forragem);
        await _context.SaveChangesAsync();

        return _mapper.Map<ForragemResponseDto>(forragem);
    }

    public async Task Remove(string id)
    {
        var forragem = await GetById(id);
        _context.Forragens.Remove(forragem);
        await _context.SaveChangesAsync();
    }

    // Busca a entidade ou lança 404
    private async Task<Forragem> GetById(string id)
    {
        var forragem = await _context.Forragens.FirstOrDefaultAsync(f => f.Id == id);

        if (forragem is null)
            throw new ServiceException(
                $"Forragem #{id} não encontrada",
                c => c.NotFound(new { message = $"Forragem #{id} não encontrada" })
            );

        return forragem;
    }
}
