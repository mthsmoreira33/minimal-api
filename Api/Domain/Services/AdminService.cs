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

        public Admin? SearchById(int id)
        {
            return _db.Admins.Where(v => v.Id == id).FirstOrDefault();
        }
        public Admin? Login(LoginDTO loginDTO)
        {
            return (_db.Admins.Where(a => a.Email == loginDTO.Email && a.Password == loginDTO.Password).FirstOrDefault());
        }

        public Admin Store(Admin admin)
        {
            _db.Admins.Add(admin);
            _db.SaveChanges();

            return admin;
        }

        public bool HasAny(AdminDTO adminDTO)
        {
            return (_db.Admins.Where(a => a.Email == adminDTO.Email).Any());
        }

        public List<Admin> GetAll(int? page)
        {
            var query = _db.Admins.AsQueryable();

            int pageSize = 10;

            if (page == null)
            {
                query = query.Skip((int)page - 1 * pageSize).Take(pageSize);
            }

            return query.ToList();
        }
    }
}
