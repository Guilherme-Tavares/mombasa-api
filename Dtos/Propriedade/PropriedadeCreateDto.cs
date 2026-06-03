using System.ComponentModel.DataAnnotations;

namespace MombasaAPI.Dtos.Propriedade;

public class PropriedadeCreateDto
{
    [Required, MaxLength(36)]
    public required string ProdutorId { get; set; }

    [Required, MaxLength(100)]
    public required string Nome { get; set; }

    public decimal? AreaTotalHectares { get; set; }

    [MaxLength(100)]
    public string? Municipio { get; set; }

    [MaxLength(2)]
    public string? Estado { get; set; }
}
