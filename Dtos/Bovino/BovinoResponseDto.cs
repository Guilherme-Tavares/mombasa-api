using MombasaAPI.Models;

namespace MombasaAPI.Dtos.Bovino
{
    public class BovinoResponseDto
    {
        public required string Id { get; set; }
        public string? Brinco { get; set; }
        public required string Nome { get; set; }
        public Sexo Sexo { get; set; }
        public string? Raca { get; set; }
        public DateOnly? DataNascimento { get; set; }
        public decimal? PesoAtualKg { get; set; }
        public DateOnly? DataUltimaPesagem { get; set; }
        public BovinoOrigem? Origem { get; set; }
        public bool Ativo { get; set; }
    }
}
