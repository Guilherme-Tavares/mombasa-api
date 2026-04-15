using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MombasaAPI.Models;

// Tabela N:N: bovino <-> medicamento
[Table("aplicacao_medicamento"), PrimaryKey(nameof(Id))]
public class AplicacaoMedicamento
{
    [Column("id_aplicacao")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("fk_id_bovino")]
    public required string BovinoId { get; set; }

    [Column("fk_id_medicamento")]
    public required string MedicamentoId { get; set; }

    [Column("data_aplicacao")]
    public required DateTime DataAplicacao { get; set; }

    [Column("dose")]
    public required decimal Dose { get; set; }

    [Column("unidade_dose")]
    public UnidadeDose UnidadeDose { get; set; }

    // Navegação
    public virtual Bovino Bovino { get; set; } = null!;
    public virtual Medicamento Medicamento { get; set; } = null!;
}

public enum UnidadeDose { Ml, G, Doses }
