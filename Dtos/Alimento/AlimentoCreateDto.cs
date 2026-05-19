using System.ComponentModel.DataAnnotations;
using MombasaAPI.Models;

namespace MombasaAPI.Dtos.Alimento;

public class AlimentoCreateDto
{
    [Required, MaxLength(100)]
    public required string Nome { get; set; }

    [Required]
    public AlimentoTipo Tipo { get; set; }
}
