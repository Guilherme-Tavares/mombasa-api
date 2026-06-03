using System.ComponentModel.DataAnnotations;
using MombasaAPI.Models;

namespace MombasaAPI.Dtos.Rebanho;

public class RebanhoCreateDto
{
    [Required, MaxLength(36)]
    public required string PropriedadeId { get; set; }

    [Required, MaxLength(100)]
    public required string Nome { get; set; }

    [Required]
    public RebanhoFinalidade Finalidade { get; set; }

    [Required]
    public DateOnly DataFormacao { get; set; }
}
