using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MombasaAPI.Models;

[Table("forragem"), PrimaryKey(nameof(Id))]
public class Forragem
{
    [Column("id_forragem")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("fk_id_divisao")]
    public required string DivisaoId { get; set; }

    // Tipo de forragem é texto livre no banco (VARCHAR), não ENUM
    [Column("tipo")]
    public required string Tipo { get; set; }

    [Column("data_plantio")]
    public DateOnly? DataPlantio { get; set; }

    [Column("ativa")]
    public bool Ativa { get; set; } = true;

    // Navegação
    public virtual Divisao Divisao { get; set; } = null!;
}
