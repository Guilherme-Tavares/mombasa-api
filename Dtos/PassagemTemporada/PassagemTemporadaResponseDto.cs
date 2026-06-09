namespace MombasaAPI.Dtos.PassagemTemporada;

public class PassagemTemporadaResponseDto
{
    public required string Id { get; set; }
    public required string RebanhoId { get; set; }
    public required string TemporadaId { get; set; }
    public decimal? PesoMedioInicialKg { get; set; }
    public decimal? PesoMedioFinalKg { get; set; }
    public decimal? GmdMedioKg { get; set; }
}
