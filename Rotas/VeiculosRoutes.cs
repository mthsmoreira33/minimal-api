using Microsoft.AspNetCore.Mvc;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Interfaces;

namespace minimal_api.Rotas
{
    public static class VeiculosRoutes
    {
        public static void MapVeiculosRoutes(this WebApplication app)
        {
            var veiculosRoutes = app.MapGroup("/veiculos");

            veiculosRoutes.MapGet("/", ([FromQuery] int pagina, [FromQuery] string? nome, [FromQuery] string? marca, IVeiculoService veiculoService) =>
            {
                return Results.Ok(veiculoService.Todos(pagina, nome, marca));
            }).WithTags("Veiculos");

            veiculosRoutes.MapGet("/{id}", ([FromRoute] int id, IVeiculoService veiculoService) =>
            {
                return Results.Ok(veiculoService.BuscaPorId(id));
            }).WithTags("Veiculos");



            veiculosRoutes.MapPost("/", ([FromBody] AddVeiculoDTO veiculoDTO, IVeiculoService veiculoService) =>
            {
                var veiculo = new Veiculo { Nome = veiculoDTO.Nome, Marca = veiculoDTO.Marca};
                veiculoService.Incluir(veiculo);
                return Results.Created();
            }).WithTags("Veiculos");
        }
    }
}
