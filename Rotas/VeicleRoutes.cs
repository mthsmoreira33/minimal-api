using Microsoft.AspNetCore.Mvc;
using minimal_api.Domain.DTOs;
using minimal_api.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using minimal_api.Domain.Entities;

namespace minimal_api.Rotas
{
    public static class VeicleRoutes
    {
        public static void MapVeicleRoutes(this WebApplication app)
        {
            var veiclesRoutes = app.MapGroup("/veicles");

            // Get de todos os veículos
            veiclesRoutes.MapGet("/", ([FromQuery] int pagina, [FromQuery] string? nome, [FromQuery] string? marca, IVeicleService veicleService) =>
            {
                return Results.Ok(veicleService.GetAll(pagina, nome, marca));
            })
                .RequireAuthorization()
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm, Editor" })
                .WithTags("Veículos");

            // Get de um veículo
            veiclesRoutes.MapGet("/{id}", ([FromRoute] int id, IVeicleService veicleService) =>
            {
                var veicle = veicleService.SearchById(id);

                if (veicle == null) return Results.NotFound();

                return Results.Ok(veicle);
            })
                .RequireAuthorization()
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm, Editor" })
                .WithTags("Veículos");

            // Post de veículos
            veiclesRoutes.MapPost("/", ([FromBody] AddVeicleDTO veicleDTO, IVeicleService veicleService) =>
            {
                var veicle = new Veicle { Name = veicleDTO.Name, Brand = veicleDTO.Brand, Year = veicleDTO.Year };
                if (string.IsNullOrWhiteSpace(veicleDTO.Name) || string.IsNullOrWhiteSpace(veicleDTO.Brand)) return Results.BadRequest();

                veicleService.Store(veicle);
                return Results.Created($"/veicles/{veicle.Id}", veicle);
            })
                .RequireAuthorization()
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
                .WithTags("Veículos");

            // Patch de veículos
            veiclesRoutes.MapPatch("/{id}", ([FromRoute] int id, [FromBody] EditVeicleDTO veicleDTO, IVeicleService veicleService) =>
            {
                var veicle = veicleService.SearchById(id);

                if (veicle == null) return Results.NotFound("Veículo não encontrado");

                if (string.IsNullOrWhiteSpace(veicleDTO.Name) && string.IsNullOrWhiteSpace(veicleDTO.Brand) && veicleDTO.Year == 0)
                    return Results.BadRequest("Por favor insira o Name, Brand ou Year para atualizar");
                
                if (!string.IsNullOrWhiteSpace(veicleDTO.Name))
                    veicle.Name = veicleDTO.Name;

                if (!string.IsNullOrWhiteSpace(veicleDTO.Brand))
                    veicle.Brand = veicleDTO.Brand;

                var currentYear = DateTime.Now.Year;
                var dataMinima = 1950;

                if (veicleDTO.Year < dataMinima)
                    return Results.BadRequest($"Veículo muito antigo, o ano mínimo é {dataMinima}");

                if (veicleDTO.Year > currentYear)
                    return Results.BadRequest($"Veículo mais novo do que o ano atual, o ano máximo é {currentYear}");

                veicle.Year = (int)veicleDTO.Year;

                veicleService.Amend(veicle);
                return Results.Ok();
            })
                .RequireAuthorization()
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
                .WithTags("Veículos");

            // Delete de Veículos
            veiclesRoutes.MapDelete("/{id}", ([FromRoute] int id, IVeicleService veicleService) =>
            {
                var veicle = veicleService.SearchById(id);

                if (veicle == null) return Results.NotFound();

                veicleService.Erase(veicle);
                return Results.NoContent();
            })
                .RequireAuthorization()
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
                .WithTags("Veículos");
        }
    }
}
