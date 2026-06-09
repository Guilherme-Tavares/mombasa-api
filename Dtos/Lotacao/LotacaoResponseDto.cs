namespace MombasaAPI.Dtos.Lotacao;

public class LotacaoResponseDto
{
    public required string Id { get; set; }
    public required string RebanhoId { get; set; }
    public required string DivisaoId { get; set; }
    public DateOnly DataEntrada { get; set; }
    public DateOnly? DataSaida { get; set; }
    public int NumeroCabecas { get; set; }
}
