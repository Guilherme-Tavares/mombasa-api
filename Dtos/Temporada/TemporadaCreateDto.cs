using System.ComponentModel.DataAnnotations;
using MombasaAPI.Models;

namespace MombasaAPI.Dtos.Temporada;

public class TemporadaCreateDto
{
    [Required, MaxLength(100)]
    public required string Nome { get; set; }

    [Required]
    public TemporadaTipo Tipo { get; set; }

    [Required]
    public DateOnly DataInicio { get; set; }

    [Required]
    public DateOnly DataFim { get; set; }
}
