using MombasaAPI.Models;

namespace MombasaAPI.Dtos.AplicacaoMedicamento;

public class AplicacaoMedicamentoResponseDto
{
    public required string Id { get; set; }
    public required string BovinoId { get; set; }
    public required string MedicamentoId { get; set; }
    public DateTime DataAplicacao { get; set; }
    public decimal Dose { get; set; }
    public UnidadeDose UnidadeDose { get; set; }
}
