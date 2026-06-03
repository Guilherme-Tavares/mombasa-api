using System.ComponentModel.DataAnnotations;
using MombasaAPI.Models;

namespace MombasaAPI.Dtos.Cocho;

public class CochoCreateDto
{
    [Required, MaxLength(36)]
    public required string DivisaoId { get; set; }

    [MaxLength(50)]
    public string? Identificacao { get; set; }

    public CochoTipoMaterial? TipoMaterial { get; set; }

    [Required]
    public decimal CapacidadeKg { get; set; }
}
