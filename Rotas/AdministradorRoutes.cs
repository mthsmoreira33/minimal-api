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

            administradorRoutes.MapPost("/login", ([FromBody] LoginDTO loginDTO, IAdministradorService administradorService) =>
            {
                if (administradorService.Login(loginDTO) != null) Results.Ok("Login Success");
                else Results.Unauthorized();
            });
        }
    }
}
