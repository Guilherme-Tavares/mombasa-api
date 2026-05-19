using MombasaAPI.Models;

namespace MombasaAPI.Dtos.Medicamento;

public class MedicamentoResponseDto
{
    public required string Id { get; set; }
    public required string NomeComercial { get; set; }
    public string? PrincipioAtivo { get; set; }
    public MedicamentoTipo Tipo { get; set; }
}
