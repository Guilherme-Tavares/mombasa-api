using System.ComponentModel.DataAnnotations;

namespace MombasaAPI.Dtos.PassagemTemporada;

public class PassagemTemporadaCreateDto
{
    [Required, MaxLength(36)]
    public required string RebanhoId { get; set; }

    [Required, MaxLength(36)]
    public required string TemporadaId { get; set; }

    public decimal? PesoMedioInicialKg { get; set; }
    public decimal? PesoMedioFinalKg { get; set; }
    public decimal? GmdMedioKg { get; set; }
}
