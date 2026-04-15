using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MombasaAPI.Models;

[Table("cocho"), PrimaryKey(nameof(Id))]
public class Cocho
{
    [Column("id_cocho")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("fk_id_divisao")]
    public required string DivisaoId { get; set; }

    [Column("identificacao")]
    public string? Identificacao { get; set; }

    [Column("tipo_material")]
    public CochoTipoMaterial? TipoMaterial { get; set; }

    [Column("capacidade_kg")]
    public required decimal CapacidadeKg { get; set; }

    [Column("ativo")]
    public bool Ativo { get; set; } = true;

    // Navegação
    public virtual Divisao Divisao { get; set; } = null!;
    public virtual ICollection<AbastecimentoCocho> Abastecimentos { get; set; } = [];
}

public enum CochoTipoMaterial { Madeira, Concreto, Plastico, Metal }
