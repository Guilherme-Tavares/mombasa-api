using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MombasaAPI.Models;

[Table("medicamento"), PrimaryKey(nameof(Id))]
public class Medicamento
{
    [Column("id_medicamento")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("nome_comercial")]
    public required string NomeComercial { get; set; }

    [Column("principio_ativo")]
    public string? PrincipioAtivo { get; set; }

    [Column("tipo")]
    public MedicamentoTipo Tipo { get; set; }

    // Navegação
    public virtual ICollection<EstoqueMedicamento> Estoques { get; set; } = [];
    public virtual ICollection<AplicacaoMedicamento> Aplicacoes { get; set; } = [];
}

public enum MedicamentoTipo { Antibiotico, Antiparasitario, Vitamina, Vacina, Outro }
