using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Interfaces;
using minimal_api.Dominio.Servicos;
using minimal_api.Infraestrutura.Db;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlserver")));

builder.Services.AddScoped<IAdministradorService, AdministradorService>();
builder.Services.AddScoped<IVeiculoService, VeiculoService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", (HttpContext context) =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.MapPost("/login", ([FromBody] LoginDTO loginDTO, IAdministradorService administradorService) => {
    if (administradorService.Login(loginDTO) != null) Results.Ok("Login Success");
    else Results.Unauthorized();
});

app.MapGet("/veiculos", ([FromQuery] int pagina, [FromQuery] string? nome, [FromQuery] string? marca, IVeiculoService veiculoService) =>
{
    return Results.Ok(veiculoService.Todos(pagina, nome, marca));
});

app.Run();
