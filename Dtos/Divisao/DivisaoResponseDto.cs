using MombasaAPI.Models;

namespace MombasaAPI.Dtos.Divisao;

public class DivisaoResponseDto
{
    public required string Id { get; set; }
    public required string PropriedadeId { get; set; }
    public required string Nome { get; set; }
    public DivisaoTipo Tipo { get; set; }
    public decimal? AreaHectares { get; set; }
    public bool Ativa { get; set; }
}
