using System.ComponentModel.DataAnnotations;

namespace MombasaAPI.Dtos.Lotacao;

public class LotacaoCreateDto
{
    [Required, MaxLength(36)]
    public required string RebanhoId { get; set; }

    [Required, MaxLength(36)]
    public required string DivisaoId { get; set; }

    [Required]
    public DateOnly DataEntrada { get; set; }

    public DateOnly? DataSaida { get; set; }

    [Required]
    public int NumeroCabecas { get; set; }
}
