using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MombasaAPI.DataContexts;
using MombasaAPI.Profiles;
using MombasaAPI.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// GuidFormat=None instrui o MySqlConnector a devolver CHAR(36) como string,
// evitando que colunas UUID sejam retornadas como System.Guid no driver
var connectionString = builder.Configuration.GetConnectionString("mysql")!;
if (!connectionString.Contains("GuidFormat=", StringComparison.OrdinalIgnoreCase))
    connectionString = connectionString.TrimEnd(';') + ";GuidFormat=None";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32)))
           .UseSnakeCaseNamingConvention()
);

// Evita ciclos de referencia; enums serializados como strings (ex: "M", "Comprado")
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "Agroware Mombasa API v1" });
    options.SwaggerDoc("v2", new OpenApiInfo { Version = "v2", Title = "Agroware Mombasa API v2" });
});

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<BovinoProfile>();
    config.AddProfile<ProdutorProfile>();
    config.AddProfile<TemporadaProfile>();
    config.AddProfile<MedicamentoProfile>();
    config.AddProfile<AlimentoProfile>();
    config.AddProfile<PropriedadeProfile>();
    config.AddProfile<DivisaoProfile>();
    config.AddProfile<RebanhoProfile>();
    config.AddProfile<CochoProfile>();
    config.AddProfile<ForragemProfile>();
    config.AddProfile<LotacaoProfile>();
    config.AddProfile<PertencimentoProfile>();
    config.AddProfile<PassagemTemporadaProfile>();
    config.AddProfile<AplicacaoMedicamentoProfile>();
    config.AddProfile<AbastecimentoCochoProfile>();
    config.AddProfile<EstoqueMedicamentoProfile>();
});

builder.Services.AddScoped<BovinoService>();
builder.Services.AddScoped<ProdutorService>();
builder.Services.AddScoped<TemporadaService>();
builder.Services.AddScoped<MedicamentoService>();
builder.Services.AddScoped<AlimentoService>();
builder.Services.AddScoped<PropriedadeService>();
builder.Services.AddScoped<DivisaoService>();
builder.Services.AddScoped<RebanhoService>();
builder.Services.AddScoped<CochoService>();
builder.Services.AddScoped<ForragemService>();
builder.Services.AddScoped<LotacaoService>();
builder.Services.AddScoped<PertencimentoService>();
builder.Services.AddScoped<PassagemTemporadaService>();
builder.Services.AddScoped<AplicacaoMedicamentoService>();
builder.Services.AddScoped<AbastecimentoCochoService>();
builder.Services.AddScoped<EstoqueMedicamentoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Agroware Mombasa API v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "Agroware Mombasa API v2");
    });
}

app.UseAuthorization();
app.MapControllers();
app.Run();