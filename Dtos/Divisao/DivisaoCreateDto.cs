using System.ComponentModel.DataAnnotations;
using MombasaAPI.Models;

namespace MombasaAPI.Dtos.Divisao;

public class DivisaoCreateDto
{
    [Required, MaxLength(36)]
    public required string PropriedadeId { get; set; }

    [Required, MaxLength(100)]
    public required string Nome { get; set; }

    [Required]
    public DivisaoTipo Tipo { get; set; } = DivisaoTipo.Pasto;

    public decimal? AreaHectares { get; set; }
}
