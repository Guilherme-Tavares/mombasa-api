using MombasaAPI.DataContexts;
using MombasaAPI.Profiles;
using MombasaAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// GuidFormat=None instrui o MySqlConnector a devolver CHAR(36) como string,
// evitando que colunas UUID sejam retornadas como System.Guid no driver
var connectionString = builder.Configuration.GetConnectionString("mysql")!;
if (!connectionString.Contains("GuidFormat=", StringComparison.OrdinalIgnoreCase))
    connectionString = connectionString.TrimEnd(';') + ";GuidFormat=None";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32)))
);

// Evita ciclos de referência; enums serializados como strings (ex: "M", "Comprado")
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<BovinoProfile>();
    config.AddProfile<ProdutorProfile>();
});

builder.Services.AddScoped<BovinoService>();
builder.Services.AddScoped<ProdutorService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
