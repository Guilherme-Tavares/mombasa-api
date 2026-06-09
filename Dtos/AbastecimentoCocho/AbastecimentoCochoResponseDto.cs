namespace MombasaAPI.Dtos.AbastecimentoCocho;

public class AbastecimentoCochoResponseDto
{
    public required string Id { get; set; }
    public required string CochoId { get; set; }
    public required string AlimentoId { get; set; }
    public DateTime DataAbastecimento { get; set; }
    public decimal QuantidadeInicialKg { get; set; }
    public decimal QuantidadeRestanteKg { get; set; }
    public bool Esgotado { get; set; }
}
