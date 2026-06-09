using System.ComponentModel.DataAnnotations;

namespace MombasaAPI.Dtos.AbastecimentoCocho;

public class AbastecimentoCochoCreateDto
{
    [Required, MaxLength(36)]
    public required string CochoId { get; set; }

    [Required, MaxLength(36)]
    public required string AlimentoId { get; set; }

    [Required]
    public DateTime DataAbastecimento { get; set; }

    [Required]
    public decimal QuantidadeInicialKg { get; set; }

    [Required]
    public decimal QuantidadeRestanteKg { get; set; }
}
