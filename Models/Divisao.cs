using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MombasaAPI.Models;

[Table("divisao"), PrimaryKey(nameof(Id))]
public class Divisao
{
    [Column("id_divisao")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("fk_id_propriedade")]
    public required string PropriedadeId { get; set; }

    [Column("nome")]
    public required string Nome { get; set; }

    [Column("tipo")]
    public DivisaoTipo Tipo { get; set; } = DivisaoTipo.Pasto;

    [Column("area_hectares")]
    public decimal? AreaHectares { get; set; }

    [Column("ativa")]
    public bool Ativa { get; set; } = true;

    // Navegação
    public virtual Propriedade Propriedade { get; set; } = null!;
    public virtual ICollection<Forragem> Forragens { get; set; } = [];
    public virtual ICollection<Cocho> Cochos { get; set; } = [];
    public virtual ICollection<Lotacao> Lotacoes { get; set; } = [];
}

public enum DivisaoTipo { Pasto, Reserva, Instalacao }
