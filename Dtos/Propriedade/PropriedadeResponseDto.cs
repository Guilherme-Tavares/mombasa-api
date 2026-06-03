namespace MombasaAPI.Dtos.Propriedade;

public class PropriedadeResponseDto
{
    public required string Id { get; set; }
    public required string ProdutorId { get; set; }
    public required string Nome { get; set; }
    public decimal? AreaTotalHectares { get; set; }
    public string? Municipio { get; set; }
    public string? Estado { get; set; }
    public DateTime DataCadastro { get; set; }
    public bool Ativa { get; set; }
}
