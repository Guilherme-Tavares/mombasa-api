using System.ComponentModel.DataAnnotations;

namespace MombasaAPI.Dtos.Forragem;

public class ForragemCreateDto
{
    [Required, MaxLength(36)]
    public required string DivisaoId { get; set; }

    [Required, MaxLength(100)]
    public required string Tipo { get; set; }

    public DateOnly? DataPlantio { get; set; }
}
