namespace MombasaAPI.Dtos.Pertencimento;

public class PertencimentoResponseDto
{
    public required string Id { get; set; }
    public required string BovinoId { get; set; }
    public required string RebanhoId { get; set; }
    public DateOnly DataEntrada { get; set; }
    public DateOnly? DataSaida { get; set; }
    public decimal? PesoEntradaKg { get; set; }
}
