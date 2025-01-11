using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Interfaces;
using minimal_api.Infraestrutura.Db;

namespace minimal_api.Dominio.Servicos
{
    public class AdministradorService : IAdministradorService
    {
        private readonly AppDbContext _db;
        public AdministradorService(AppDbContext db)
        {
            _db = db;
        }
        public Administrador? Login(LoginDTO loginDTO)
        {
            return (_db.Administradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Password).FirstOrDefault());
        }

        public void Incluir(AdministradorDTO administradorDTO)
        {
            var administrador = new Administrador
            {
                Email = administradorDTO.Email,
                Senha = administradorDTO.Senha,
                Perfil = administradorDTO.Perfil
            };

            _db.Administradores.Add(administrador);
            _db.SaveChanges();
        }

        public bool VerificarAdministradorExistente(AdministradorDTO administradorDTO)
        {
            return (_db.Administradores.Where(a => a.Email == administradorDTO.Email).Any());
        }
    }
}
