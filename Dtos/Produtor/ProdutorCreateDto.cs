using System.ComponentModel.DataAnnotations;

namespace MombasaAPI.Dtos.Produtor
{
    public class ProdutorCreateDto
    {
        [Required(ErrorMessage = "O nome do produtor é obrigatório.")]
        [MaxLength(100)]
        public required string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        [MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(20)]
        public string? Telefone { get; set; }

        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        [MaxLength(255)]
        public string? Senha { get; set; }
    }
}
