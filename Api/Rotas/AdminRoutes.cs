using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using minimal_api.Domain.DTOs;
using minimal_api.Domain.Entities;
using minimal_api.Domain.Interfaces;
using minimal_api.Domain.ModelView;

namespace minimal_api.Rotas
{
    public static class AdminRoutes
    {
        public static void MapAdminRoutes(this WebApplication app)
        {
            var adminRoutes = app.MapGroup("/admin");

            // Login de Admin
            adminRoutes.MapPost("/login", ([FromBody] LoginDTO loginDTO, IAdminService adminService, ITokenService tokenService) =>
            {
                var admin = adminService.Login(loginDTO);

                if (admin != null)
                {
                    string token = tokenService.GenerateJwtToken(admin);
                    return Results.Ok(new LoggedAdmin 
                    { 
                        Email = admin.Email, 
                        Role = admin.Role, 
                        Token = token 
                    });
                }

                else
                {
                    return Results.Unauthorized();
                }
            })
                .AllowAnonymous()
                .WithTags("Administrador");

            //Post de Admin
            adminRoutes.MapPost("/", ([FromBody] AdminDTO adminDTO, IAdminService adminService) =>
            {
                if (string.IsNullOrEmpty(adminDTO.Email) || string.IsNullOrEmpty(adminDTO.Password))
                {
                    return Results.BadRequest("Inclua email ou senha");
                }
                if (adminService.HasAny(adminDTO))
                {
                    return Results.BadRequest("Admin já cadastrado");
                }

                var admin = new Admin
                {
                    Email = adminDTO.Email,
                    Password = adminDTO.Password,
                    Role = adminDTO.Role
                };

                adminService.Store(admin);

                return Results.Created();
            })
                .RequireAuthorization()
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
                .WithTags("Administrador");
        }
    }
}
