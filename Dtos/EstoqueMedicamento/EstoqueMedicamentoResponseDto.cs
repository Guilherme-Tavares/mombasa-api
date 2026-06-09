using MombasaAPI.Models;

namespace MombasaAPI.Dtos.EstoqueMedicamento;

public class EstoqueMedicamentoResponseDto
{
    public required string Id { get; set; }
    public required string PropriedadeId { get; set; }
    public required string MedicamentoId { get; set; }
    public decimal Quantidade { get; set; }
    public UnidadeEstoque Unidade { get; set; }
    public DateOnly DataEntrada { get; set; }
    public decimal EstoqueMinimo { get; set; }
}
