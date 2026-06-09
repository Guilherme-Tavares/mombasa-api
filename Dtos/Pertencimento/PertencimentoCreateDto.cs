using System.ComponentModel.DataAnnotations;

namespace MombasaAPI.Dtos.Pertencimento;

public class PertencimentoCreateDto
{
    [Required, MaxLength(36)]
    public required string BovinoId { get; set; }

    [Required, MaxLength(36)]
    public required string RebanhoId { get; set; }

    [Required]
    public DateOnly DataEntrada { get; set; }

    public DateOnly? DataSaida { get; set; }

    public decimal? PesoEntradaKg { get; set; }
}
