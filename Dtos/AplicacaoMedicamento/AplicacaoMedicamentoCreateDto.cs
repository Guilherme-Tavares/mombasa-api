using System.ComponentModel.DataAnnotations;
using MombasaAPI.Models;

namespace MombasaAPI.Dtos.AplicacaoMedicamento;

public class AplicacaoMedicamentoCreateDto
{
    [Required, MaxLength(36)]
    public required string BovinoId { get; set; }

    [Required, MaxLength(36)]
    public required string MedicamentoId { get; set; }

    [Required]
    public DateTime DataAplicacao { get; set; }

    [Required]
    public decimal Dose { get; set; }

    [Required]
    public UnidadeDose UnidadeDose { get; set; }
}
