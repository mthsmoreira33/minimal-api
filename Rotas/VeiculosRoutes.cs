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

            // Get de todos os veículos
            veiculosRoutes.MapGet("/", ([FromQuery] int pagina, [FromQuery] string? nome, [FromQuery] string? marca, IVeiculoService veiculoService) =>
            {
                return Results.Ok(veiculoService.Todos(pagina, nome, marca));
            }).WithTags("Veiculos");

            // Get de todos os veículos no Id
            veiculosRoutes.MapGet("/{id}", ([FromRoute] int id, IVeiculoService veiculoService) =>
            {
                var veiculo = veiculoService.BuscaPorId(id);

                if (veiculo == null) return Results.NotFound();

                return Results.Ok(veiculo);
            }).WithTags("Veiculos");

            // Post de veículos
            veiculosRoutes.MapPost("/", ([FromBody] AddVeiculoDTO veiculoDTO, IVeiculoService veiculoService) =>
            {
                var veiculo = new Veiculo { Nome = veiculoDTO.Nome, Marca = veiculoDTO.Marca};

                veiculoService.Incluir(veiculo);
                return Results.Created();
            }).WithTags("Veiculos");

            // Put de veículos
            veiculosRoutes.MapPut("/{id}", ([FromRoute] int id, [FromBody] AddVeiculoDTO veiculoDTO, IVeiculoService veiculoService) =>
            {
                var veiculo = veiculoService.BuscaPorId(id);

                if (veiculo == null) return Results.NotFound();

                veiculo.Nome = veiculoDTO.Nome;
                veiculo.Marca = veiculoDTO.Marca;

                veiculoService.Atualizar(veiculo);
                return Results.Ok();
            }).WithTags("Veiculos");

            veiculosRoutes.MapDelete("/{id}", ([FromRoute] int id, IVeiculoService veiculoService) =>
            {
                var veiculo = veiculoService.BuscaPorId(id);

                if (veiculo == null) return Results.NotFound();

                veiculoService.Apagar(veiculo);
                return Results.NoContent();
            }).WithTags("Veiculos");
        }
    }
}
