using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MombasaAPI.Models;

// Tabela N:N: bovino <-> rebanho
[Table("pertencimento"), PrimaryKey(nameof(Id))]
public class Pertencimento
{
    [Column("id_pertencimento")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("fk_id_bovino")]
    public required string BovinoId { get; set; }

    [Column("fk_id_rebanho")]
    public required string RebanhoId { get; set; }

    [Column("data_entrada")]
    public required DateOnly DataEntrada { get; set; }

    [Column("data_saida")]
    public DateOnly? DataSaida { get; set; }

    [Column("peso_entrada_kg")]
    public decimal? PesoEntradaKg { get; set; }

    // Navegação
    public virtual Bovino Bovino { get; set; } = null!;
    public virtual Rebanho Rebanho { get; set; } = null!;
}
