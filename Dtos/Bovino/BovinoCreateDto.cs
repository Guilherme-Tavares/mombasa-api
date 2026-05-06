using System.ComponentModel.DataAnnotations;
using MombasaAPI.Models;

namespace MombasaAPI.Dtos.Bovino
{
    public class BovinoCreateDto
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
    }
}
