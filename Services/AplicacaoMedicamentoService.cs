using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MombasaAPI.DataContexts;
using MombasaAPI.Dtos.AplicacaoMedicamento;
using MombasaAPI.Exceptions;
using MombasaAPI.Models;

namespace MombasaAPI.Services;

public class AplicacaoMedicamentoService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AplicacaoMedicamentoService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<AplicacaoMedicamentoResponseDto>> FindAll()
    {
        var aplicacoes = await _context.AplicacoesMedicamento.ToListAsync();
        return _mapper.Map<ICollection<AplicacaoMedicamentoResponseDto>>(aplicacoes);
    }

    public async Task<AplicacaoMedicamentoResponseDto> FindById(string id)
    {
        var aplicacao = await GetById(id);
        return _mapper.Map<AplicacaoMedicamentoResponseDto>(aplicacao);
    }

    public async Task<AplicacaoMedicamentoResponseDto> Create(AplicacaoMedicamentoCreateDto data)
    {
        var aplicacao = _mapper.Map<AplicacaoMedicamento>(data);
        await _context.AplicacoesMedicamento.AddAsync(aplicacao);
        await _context.SaveChangesAsync();

        return _mapper.Map<AplicacaoMedicamentoResponseDto>(aplicacao);
    }

    public async Task<AplicacaoMedicamentoResponseDto> Update(string id, AplicacaoMedicamentoUpdateDto data)
    {
        var aplicacao = await GetById(id);

        _mapper.Map(data, aplicacao);
        _context.AplicacoesMedicamento.Update(aplicacao);
        await _context.SaveChangesAsync();

        return _mapper.Map<AplicacaoMedicamentoResponseDto>(aplicacao);
    }

    public async Task Remove(string id)
    {
        var aplicacao = await GetById(id);
        _context.AplicacoesMedicamento.Remove(aplicacao);
        await _context.SaveChangesAsync();
    }

    // Busca a entidade ou lança 404
    private async Task<AplicacaoMedicamento> GetById(string id)
    {
        var aplicacao = await _context.AplicacoesMedicamento.FirstOrDefaultAsync(a => a.Id == id);

        if (aplicacao is null)
            throw new ServiceException(
                $"Aplicação de medicamento #{id} não encontrada",
                c => c.NotFound(new { message = $"Aplicação de medicamento #{id} não encontrada" })
            );

        return aplicacao;
    }
}
