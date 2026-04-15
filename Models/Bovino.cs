using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MombasaAPI.Models;

[Table("bovino"), PrimaryKey(nameof(Id))]
public class Bovino
{
    [Column("id_bovino")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("brinco")]
    public string? Brinco { get; set; }

    [Column("nome")]
    public required string Nome { get; set; }

    [Column("sexo")]
    public Sexo Sexo { get; set; }

    [Column("raca")]
    public string? Raca { get; set; }

    [Column("data_nascimento")]
    public DateOnly? DataNascimento { get; set; }

    [Column("peso_atual_kg")]
    public decimal? PesoAtualKg { get; set; }

    [Column("data_ultima_pesagem")]
    public DateOnly? DataUltimaPesagem { get; set; }

    [Column("origem")]
    public BovinoOrigem? Origem { get; set; }

    [Column("ativo")]
    public bool Ativo { get; set; } = true;

    // Navegação
    public virtual ICollection<Pertencimento> Pertencimentos { get; set; } = [];
    public virtual ICollection<AplicacaoMedicamento> AplicacoesMedicamento { get; set; } = [];
}

// Sexo armazenado como 'M'/'F' no MySQL — converter preserva uppercase
public enum Sexo { M, F }

public enum BovinoOrigem { Comprado, Doacao }
