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
                if (string.IsNullOrWhiteSpace(veiculoDTO.Nome) || string.IsNullOrWhiteSpace(veiculoDTO.Marca)) return Results.BadRequest();

                veiculoService.Incluir(veiculo);
                return Results.Created($"/veiculos/{veiculo.Id}", veiculo);
            }).WithTags("Veiculos");

            // Put de veículos
            veiculosRoutes.MapPut("/{id}", ([FromRoute] int id, [FromBody] AddVeiculoDTO veiculoDTO, IVeiculoService veiculoService) =>
            {
                var veiculo = veiculoService.BuscaPorId(id);

                if (veiculo == null) return Results.NotFound();
                if (string.IsNullOrWhiteSpace(veiculoDTO.Nome) || string.IsNullOrWhiteSpace(veiculoDTO.Marca)) return Results.BadRequest();

                veiculo.Nome = veiculoDTO.Nome;
                veiculo.Marca = veiculoDTO.Marca;


                veiculoService.Atualizar(veiculo);
                return Results.Ok();
            }).WithTags("Veiculos");

            // Delete de Veículos
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
