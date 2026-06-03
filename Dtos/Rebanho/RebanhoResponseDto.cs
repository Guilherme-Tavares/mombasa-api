using MombasaAPI.Models;

namespace MombasaAPI.Dtos.Rebanho;

public class RebanhoResponseDto
{
    public required string Id { get; set; }
    public required string PropriedadeId { get; set; }
    public required string Nome { get; set; }
    public RebanhoFinalidade Finalidade { get; set; }
    public DateOnly DataFormacao { get; set; }
    public bool Ativo { get; set; }
}
