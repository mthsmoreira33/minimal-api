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

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/login", ([FromBody] LoginDTO loginDTO, IAdministradorService administradorService) => {
    if (administradorService.Login(loginDTO) != null) Results.Ok("Login Success");
    else Results.Unauthorized();
});

app.Run();
