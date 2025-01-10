using Microsoft.AspNetCore.Mvc;
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
            });
        }
    }
}
