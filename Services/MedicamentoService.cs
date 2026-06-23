using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MombasaAPI.DataContexts;
using MombasaAPI.Controllers.Filters;
using MombasaAPI.Dtos.Medicamento;
using MombasaAPI.Helpers.Paginated;
using MombasaAPI.Exceptions;
using MombasaAPI.Models;

namespace MombasaAPI.Services;

public class MedicamentoService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public MedicamentoService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<MedicamentoResponseDto>> FindAll()
    {
        var medicamentos = await _context.Medicamentos.ToListAsync();
        return _mapper.Map<ICollection<MedicamentoResponseDto>>(medicamentos);
    }

    public async Task<MedicamentoResponseDto> FindById(string id)
    {
        var medicamento = await GetById(id);
        return _mapper.Map<MedicamentoResponseDto>(medicamento);
    }

    public async Task<MedicamentoResponseDto> Create(MedicamentoCreateDto data)
    {
        await ValidarNomeComercialUnico(data.NomeComercial, excludeId: null);

        var medicamento = _mapper.Map<Medicamento>(data);
        await _context.Medicamentos.AddAsync(medicamento);
        await _context.SaveChangesAsync();

        return _mapper.Map<MedicamentoResponseDto>(medicamento);
    }

    public async Task<MedicamentoResponseDto> Update(string id, MedicamentoUpdateDto data)
    {
        var medicamento = await GetById(id);

        await ValidarNomeComercialUnico(data.NomeComercial, excludeId: id);

        _mapper.Map(data, medicamento);
        _context.Medicamentos.Update(medicamento);
        await _context.SaveChangesAsync();

        return _mapper.Map<MedicamentoResponseDto>(medicamento);
    }

    public async Task Remove(string id)
    {
        var medicamento = await GetById(id);
        _context.Medicamentos.Remove(medicamento);
        await _context.SaveChangesAsync();
    }

    // Busca a entidade ou lança 404

    public async Task<PaginatedResponse<MedicamentoResponseDto>> FindAllV2(MedicamentoFilter filter)
    {
        var query = _context.Medicamentos.AsQueryable();

        if (filter.Search is not null)
            query = query.Where(m => m.NomeComercial.Contains(filter.Search)
                                  || (m.PrincipioAtivo != null && m.PrincipioAtivo.Contains(filter.Search)));

        if (filter.Tipo is not null
            && Enum.TryParse<MedicamentoTipo>(filter.Tipo, ignoreCase: true, out var tipo))
            query = query.Where(m => m.Tipo == tipo);

        return await Paginate<Medicamento>.Set<MedicamentoResponseDto>(query, filter, _mapper);
    }

    private async Task<Medicamento> GetById(string id)
    {
        var medicamento = await _context.Medicamentos.FirstOrDefaultAsync(m => m.Id == id);

        if (medicamento is null)
            throw new ServiceException(
                $"Medicamento #{id} não encontrado",
                c => c.NotFound(new { message = $"Medicamento #{id} não encontrado" })
            );

        return medicamento;
    }

    // Nome comercial deve ser único na base
    private async Task ValidarNomeComercialUnico(string nomeComercial, string? excludeId)
    {
        var emUso = await _context.Medicamentos
            .AnyAsync(m => m.NomeComercial == nomeComercial && m.Id != excludeId);

        if (emUso)
            throw new ServiceException(
                $"Medicamento '{nomeComercial}' já cadastrado",
                c => c.Conflict(new { message = $"Nome comercial '{nomeComercial}' já está em uso" })
            );
    }
}
