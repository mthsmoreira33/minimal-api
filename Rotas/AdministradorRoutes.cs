using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Interfaces;

namespace minimal_api.Rotas
{
    public static class AdministradorRoutes
    {
        public static void MapAdministradorRoutes(this WebApplication app)
        {
            var administradorRoutes = app.MapGroup("/administrador");

            // Login de Administrador
            administradorRoutes.MapPost("/login", ([FromBody] LoginDTO loginDTO, IAdministradorService administradorService, ITokenService tokenService) =>
            {
                var administrador = administradorService.Login(loginDTO);

                if (administrador != null)
                {
                    string token = tokenService.GenerateJwtToken(administrador);
                    return Results.Ok(new { administrador.Email, administrador.Perfil, Token = token });
                }

                else
                {
                    return Results.Unauthorized();
                }
            })
                .WithTags("Administrador");

            //Post de Administrador
            administradorRoutes.MapPost("/", ([FromBody] AdministradorDTO administradorDTO, IAdministradorService administradorService) =>
            {
                if (string.IsNullOrEmpty(administradorDTO.Email) || string.IsNullOrEmpty(administradorDTO.Senha))
                {
                    return Results.BadRequest("Inclua email ou senha");
                }
                if (administradorService.VerificarAdministradorExistente(administradorDTO))
                {
                    return Results.BadRequest("Administrador já cadastrado");
                }
                administradorService.Incluir(administradorDTO);
                return Results.Created();
            })
                .RequireAuthorization()
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
                .WithTags("Administrador");


        }
    }
}
