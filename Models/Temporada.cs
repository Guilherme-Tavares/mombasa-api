using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MombasaAPI.Models;

[Table("temporada"), PrimaryKey(nameof(Id))]
public class Temporada
{
    [Column("id_temporada")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("nome")]
    public required string Nome { get; set; }

    [Column("tipo")]
    public TemporadaTipo Tipo { get; set; }

    [Column("data_inicio")]
    public required DateOnly DataInicio { get; set; }

    [Column("data_fim")]
    public required DateOnly DataFim { get; set; }

    // Navegação
    public virtual ICollection<PassagemTemporada> PassagensTemporada { get; set; } = [];
}

public enum TemporadaTipo { Aguas, Seca, Transicao }
