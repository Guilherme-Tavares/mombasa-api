using MombasaAPI.Models;

namespace MombasaAPI.Dtos.Cocho;

public class CochoResponseDto
{
    public required string Id { get; set; }
    public required string DivisaoId { get; set; }
    public string? Identificacao { get; set; }
    public CochoTipoMaterial? TipoMaterial { get; set; }
    public decimal CapacidadeKg { get; set; }
    public bool Ativo { get; set; }
}
