using MombasaAPI.Models;

namespace MombasaAPI.Dtos.Temporada;

public class TemporadaResponseDto
{
    public required string Id { get; set; }
    public required string Nome { get; set; }
    public TemporadaTipo Tipo { get; set; }
    public DateOnly DataInicio { get; set; }
    public DateOnly DataFim { get; set; }
}
