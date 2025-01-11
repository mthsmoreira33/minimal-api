using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
            administradorRoutes.MapPost("/login", ([FromBody] LoginDTO loginDTO, IAdministradorService administradorService) =>
            {
                if (administradorService.Login(loginDTO) != null) Results.Ok("Login Success");
                else Results.Unauthorized();
            }).WithTags("Administrador");

            //Post de Administrador
            administradorRoutes.MapPost("/", ([FromBody] AdministradorDTO administradorDTO, IAdministradorService administradorService) =>
            {
                if (string.IsNullOrEmpty(administradorDTO.Email) || string.IsNullOrEmpty(administradorDTO.Senha)) Results.BadRequest("Inclua email ou senha");
                if (administradorService.VerificarAdministradorExistente(administradorDTO)) Results.BadRequest("Administrador já cadastrado");
                administradorService.Incluir(administradorDTO);
            });


        }
    }
}
