using Microsoft.EntityFrameworkCore;
using minimal_api.Domain.DTOs;
using minimal_api.Domain.Entities;
using minimal_api.Domain.Interfaces;
using minimal_api.Infra.Db;

namespace minimal_api.Domain.Services
{
    public class AdminService(AppDbContext db) : IAdminService
    {
        private readonly AppDbContext _db = db;

        public Admin? Login(LoginDTO loginDTO)
        {
            return (_db.Admins.Where(a => a.Email == loginDTO.Email && a.Password == loginDTO.Password).FirstOrDefault());
        }

        public void Store(AdminDTO adminDTO)
        {
            var admin = new Admin
            {
                Email = adminDTO.Email,
                Password = adminDTO.Password,
                Role = adminDTO.Role
            };

            _db.Admins.Add(admin);
            _db.SaveChanges();
        }

        public bool HasAny(AdminDTO adminDTO)
        {
            return (_db.Admins.Where(a => a.Email == adminDTO.Email).Any());
        }
    }
}
