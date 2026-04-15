using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MombasaAPI.Models;

// Tabela N:N: rebanho <-> temporada (contém indicadores zootécnicos do período)
[Table("passagem_temporada"), PrimaryKey(nameof(Id))]
public class PassagemTemporada
{
    [Column("id_passagem")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("fk_id_rebanho")]
    public required string RebanhoId { get; set; }

    [Column("fk_id_temporada")]
    public required string TemporadaId { get; set; }

    [Column("peso_medio_inicial_kg")]
    public decimal? PesoMedioInicialKg { get; set; }

    [Column("peso_medio_final_kg")]
    public decimal? PesoMedioFinalKg { get; set; }

    // GMD = Ganho Médio Diário
    [Column("gmd_medio_kg")]
    public decimal? GmdMedioKg { get; set; }

    // Navegação
    public virtual Rebanho Rebanho { get; set; } = null!;
    public virtual Temporada Temporada { get; set; } = null!;
}
