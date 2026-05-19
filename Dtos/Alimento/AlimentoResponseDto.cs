using MombasaAPI.Models;

namespace MombasaAPI.Dtos.Alimento;

public class AlimentoResponseDto
{
    public required string Id { get; set; }
    public required string Nome { get; set; }
    public AlimentoTipo Tipo { get; set; }
}
