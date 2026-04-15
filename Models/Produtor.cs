using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MombasaAPI.Models;

[Table("produtor"), PrimaryKey(nameof(Id))]
public class Produtor
{
    [Column("id_produtor")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("nome")]
    public required string Nome { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("telefone")]
    public string? Telefone { get; set; }

    [Column("senha")]
    public string? Senha { get; set; }

    [Column("data_cadastro")]
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    // Navegação
    public virtual ICollection<Propriedade> Propriedades { get; set; } = [];
}
