using MombasaAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace MombasaAPI.Dtos.Bovino
{
    public class BovinoUpdateDto
    {
        [MaxLength(50)]
        public string? Brinco { get; set; }

        [Required(ErrorMessage = "O nome do bovino é obrigatório.")]
        [MaxLength(100)]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "O sexo do bovino é obrigatório.")]
        public Sexo? Sexo { get; set; }

        [MaxLength(80)]
        public string? Raca { get; set; }

        public DateOnly? DataNascimento { get; set; }

        [Range(0, 2000, ErrorMessage = "Peso deve estar entre 0 e 2000 kg.")]
        public decimal? PesoAtualKg { get; set; }

        public DateOnly? DataUltimaPesagem { get; set; }

        public BovinoOrigem? BovinoOrigem { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
