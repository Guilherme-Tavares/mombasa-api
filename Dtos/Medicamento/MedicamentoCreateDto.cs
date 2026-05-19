using System.ComponentModel.DataAnnotations;
using MombasaAPI.Models;

namespace MombasaAPI.Dtos.Medicamento;

public class MedicamentoCreateDto
{
    [Required, MaxLength(150)]
    public required string NomeComercial { get; set; }

    [MaxLength(150)]
    public string? PrincipioAtivo { get; set; }

    [Required]
    public MedicamentoTipo Tipo { get; set; }
}
