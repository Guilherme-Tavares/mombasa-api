using System.ComponentModel;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MombasaAPI.Models;

namespace MombasaAPI.DataContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Entidades principais
    public DbSet<Produtor> Produtores { get; set; }
    public DbSet<Propriedade> Propriedades { get; set; }
    public DbSet<Divisao> Divisoes { get; set; }
    public DbSet<Rebanho> Rebanhos { get; set; }
    public DbSet<Bovino> Bovinos { get; set; }
    public DbSet<Temporada> Temporadas { get; set; }
    public DbSet<Forragem> Forragens { get; set; }
    public DbSet<Cocho> Cochos { get; set; }
    public DbSet<Medicamento> Medicamentos { get; set; }
    public DbSet<Alimento> Alimentos { get; set; }
    public DbSet<EstoqueMedicamento> EstoquesMedicamentos { get; set; }

    // Tabelas N:N
    public DbSet<Lotacao> Lotacoes { get; set; }
    public DbSet<Pertencimento> Pertencimentos { get; set; }
    public DbSet<PassagemTemporada> PassagensTemporada { get; set; }
    public DbSet<AplicacaoMedicamento> AplicacoesMedicamento { get; set; }
    public DbSet<AbastecimentoCocho> AbastecimentosCochos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Converte todos os enums C# para string no banco de dados,
        // respeitando o atributo [Description] quando o nome do valor
        // difere do ENUM definido no MySQL (ex: SalMineral -> 'sal_mineral')
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                var clrType = property.ClrType;
                var enumType = clrType.IsEnum
                    ? clrType
                    : Nullable.GetUnderlyingType(clrType) is { IsEnum: true } u ? u : null;

                if (enumType is null) continue;

                var converterMethod = typeof(AppDbContext)
                    .GetMethod(nameof(CreateEnumConverter), BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(enumType);

                property.SetValueConverter((ValueConverter)converterMethod.Invoke(null, null)!);
            }
        }

        // Índices únicos compostos das tabelas N:N
        modelBuilder.Entity<EstoqueMedicamento>()
            .HasIndex(e => new { e.PropriedadeId, e.MedicamentoId })
            .IsUnique();

        modelBuilder.Entity<Lotacao>()
            .HasIndex(e => new { e.RebanhoId, e.DivisaoId, e.DataEntrada })
            .IsUnique();

        modelBuilder.Entity<Pertencimento>()
            .HasIndex(e => new { e.BovinoId, e.RebanhoId, e.DataEntrada })
            .IsUnique();

        modelBuilder.Entity<PassagemTemporada>()
            .HasIndex(e => new { e.RebanhoId, e.TemporadaId })
            .IsUnique();
    }

    // Cria um ValueConverter que serializa o enum como string lowercase,
    // usando [Description] quando presente (ex: SalMineral -> "sal_mineral")
    private static ValueConverter<TEnum, string> CreateEnumConverter<TEnum>()
        where TEnum : struct, Enum
    {
        return new ValueConverter<TEnum, string>(
            v => EnumToDbString(v),
            v => DbStringToEnum<TEnum>(v)
        );
    }

    private static string EnumToDbString<TEnum>(TEnum value) where TEnum : struct, Enum
    {
        var member = typeof(TEnum).GetMember(value.ToString()).FirstOrDefault();
        var description = member?.GetCustomAttribute<DescriptionAttribute>()?.Description;
        return description ?? value.ToString().ToLower();
    }

    private static TEnum DbStringToEnum<TEnum>(string value) where TEnum : struct, Enum
    {
        // Verifica primeiro se algum membro possui [Description] correspondente
        foreach (var member in typeof(TEnum).GetMembers(BindingFlags.Public | BindingFlags.Static))
        {
            var description = member.GetCustomAttribute<DescriptionAttribute>()?.Description;
            if (description != null && string.Equals(description, value, StringComparison.OrdinalIgnoreCase))
                return Enum.Parse<TEnum>(member.Name);
        }

        // Fallback: parse case-insensitive pelo nome do enum
        return Enum.Parse<TEnum>(value, ignoreCase: true);
    }
}
