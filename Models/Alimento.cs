using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MombasaAPI.Models;

[Table("alimento"), PrimaryKey(nameof(Id))]
public class Alimento
{
    [Column("id_alimento")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("nome")]
    public required string Nome { get; set; }

    [Column("tipo")]
    public AlimentoTipo Tipo { get; set; }

    // Navegação
    public virtual ICollection<AbastecimentoCocho> Abastecimentos { get; set; } = [];
}

public enum AlimentoTipo
{
    Racao,
    // O valor no MySQL é 'sal_mineral' — o Description informa o converter
    [Description("sal_mineral")]
    SalMineral,
    Silagem,
    Farelo,
    Suplemento,
    Outro
}
