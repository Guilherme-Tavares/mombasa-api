using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MombasaAPI.Models;

[Table("propriedade"), PrimaryKey(nameof(Id))]
public class Propriedade
{
    [Column("id_propriedade")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("fk_id_produtor")]
    public required string ProdutorId { get; set; }

    [Column("nome")]
    public required string Nome { get; set; }

    [Column("area_total_hectares")]
    public decimal? AreaTotalHectares { get; set; }

    [Column("municipio")]
    public string? Municipio { get; set; }

    [Column("estado")]
    public string? Estado { get; set; }

    [Column("data_cadastro")]
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    [Column("ativa")]
    public bool Ativa { get; set; } = true;

    // Navegação
    public virtual Produtor Produtor { get; set; } = null!;
    public virtual ICollection<Divisao> Divisoes { get; set; } = [];
    public virtual ICollection<Rebanho> Rebanhos { get; set; } = [];
    public virtual ICollection<EstoqueMedicamento> EstoquesMedicamentos { get; set; } = [];
}
