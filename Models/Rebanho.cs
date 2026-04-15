using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MombasaAPI.Models;

[Table("rebanho"), PrimaryKey(nameof(Id))]
public class Rebanho
{
    [Column("id_rebanho")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("fk_id_propriedade")]
    public required string PropriedadeId { get; set; }

    [Column("nome")]
    public required string Nome { get; set; }

    [Column("finalidade")]
    public RebanhoFinalidade Finalidade { get; set; }

    [Column("data_formacao")]
    public required DateOnly DataFormacao { get; set; }

    [Column("ativo")]
    public bool Ativo { get; set; } = true;

    // Navegação
    public virtual Propriedade Propriedade { get; set; } = null!;
    public virtual ICollection<Pertencimento> Pertencimentos { get; set; } = [];
    public virtual ICollection<Lotacao> Lotacoes { get; set; } = [];
    public virtual ICollection<PassagemTemporada> PassagensTemporada { get; set; } = [];
}

public enum RebanhoFinalidade { Recria, Engorda, Misto }
