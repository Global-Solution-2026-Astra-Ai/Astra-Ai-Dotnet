using Microsoft.EntityFrameworkCore;
using AstraAiDotnet.Data;
using AstraAiDotnet.ClientesPremium.Services;
using AstraAiDotnet.Leiloes.Services;
using AstraAiDotnet.ClientesPremium.Repositories.Interfaces;
using AstraAiDotnet.ClientesPremium.Repositories.Implementations;
using AstraAiDotnet.Leiloes.Repositories.Interfaces;
using AstraAiDotnet.Leiloes.Repositories.Implementations;
using AstraAiDotnet.LogTransacoes.Repositories.Interfaces;
using AstraAiDotnet.LogTransacoes.Repositories.Implementations;
using AstraAiDotnet.LogTransacoes.Services;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");

// Add services to the container.
builder.Services.AddControllers();

// Configurar DbContext
var connectionString = builder.Configuration.GetConnectionString("OracleConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(connectionString,b => b.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19)));

builder.Services.AddScoped<IClientePremiumRepository, ClientePremiumRepository>();
builder.Services.AddScoped<ClientePremiumService>();

builder.Services.AddScoped<ILeilaoRepository, LeilaoRepository>();
builder.Services.AddScoped<LeilaoService>();

builder.Services.AddScoped<ILogTransacaoRepository, LogTransacaoRepository>();
builder.Services.AddScoped<LogTransacaoService>();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
