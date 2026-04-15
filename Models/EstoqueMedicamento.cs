using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MombasaAPI.Models;

[Table("estoque_medicamento"), PrimaryKey(nameof(Id))]
public class EstoqueMedicamento
{
    [Column("id_estoque_med")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("fk_id_propriedade")]
    public required string PropriedadeId { get; set; }

    [Column("fk_id_medicamento")]
    public required string MedicamentoId { get; set; }

    [Column("quantidade")]
    public required decimal Quantidade { get; set; }

    [Column("unidade")]
    public UnidadeEstoque Unidade { get; set; }

    [Column("data_entrada")]
    public DateOnly DataEntrada { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    [Column("estoque_minimo")]
    public decimal EstoqueMinimo { get; set; } = 0;

    // Navegação
    public virtual Propriedade Propriedade { get; set; } = null!;
    public virtual Medicamento Medicamento { get; set; } = null!;
}

public enum UnidadeEstoque { Ml, L, G, Kg, Doses, Frascos }
