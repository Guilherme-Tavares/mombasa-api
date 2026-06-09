using System.ComponentModel.DataAnnotations;
using MombasaAPI.Models;

namespace MombasaAPI.Dtos.EstoqueMedicamento;

public class EstoqueMedicamentoCreateDto
{
    [Required, MaxLength(36)]
    public required string PropriedadeId { get; set; }

    [Required, MaxLength(36)]
    public required string MedicamentoId { get; set; }

    [Required]
    public decimal Quantidade { get; set; }

    [Required]
    public UnidadeEstoque Unidade { get; set; }

    public decimal EstoqueMinimo { get; set; } = 0;
}
