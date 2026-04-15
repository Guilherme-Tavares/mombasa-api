using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MombasaAPI.Models;

// Tabela N:N: rebanho <-> divisao (com data de entrada como chave composta)
[Table("lotacao"), PrimaryKey(nameof(Id))]
public class Lotacao
{
    [Column("id_lotacao")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("fk_id_rebanho")]
    public required string RebanhoId { get; set; }

    [Column("fk_id_divisao")]
    public required string DivisaoId { get; set; }

    [Column("data_entrada")]
    public required DateOnly DataEntrada { get; set; }

    [Column("data_saida")]
    public DateOnly? DataSaida { get; set; }

    [Column("numero_cabecas")]
    public required int NumeroCabecas { get; set; }

    // Navegação
    public virtual Rebanho Rebanho { get; set; } = null!;
    public virtual Divisao Divisao { get; set; } = null!;
}
