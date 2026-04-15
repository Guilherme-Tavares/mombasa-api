using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MombasaAPI.Models;

// Tabela N:N: cocho <-> alimento
[Table("abastecimento_cocho"), PrimaryKey(nameof(Id))]
public class AbastecimentoCocho
{
    [Column("id_abastecimento")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("fk_id_cocho")]
    public required string CochoId { get; set; }

    [Column("fk_id_alimento")]
    public required string AlimentoId { get; set; }

    [Column("data_abastecimento")]
    public required DateTime DataAbastecimento { get; set; }

    [Column("quantidade_inicial_kg")]
    public required decimal QuantidadeInicialKg { get; set; }

    [Column("quantidade_restante_kg")]
    public required decimal QuantidadeRestanteKg { get; set; }

    [Column("esgotado")]
    public bool Esgotado { get; set; } = false;

    // Navegação
    public virtual Cocho Cocho { get; set; } = null!;
    public virtual Alimento Alimento { get; set; } = null!;
}
