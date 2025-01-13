using minimal_api.Domain.DTOs;
using minimal_api.Domain.Entities;
using minimal_api.Domain.Interfaces;

namespace Test.Mocks
{
    public class AdminServiceMock : IAdminService
    {
        public static readonly List<Admin> admins =
        [
            new Admin {
                Id = 1,
                Email = "adm@teste.com",
                Password = "123456",
                Role = "Adm"
            },
            new Admin {
                Id = 2,
                Email = "editor@teste.com",
                Password = "123456",
                Role = "Editor"
            }
        ];

        public Admin? SearchById(int id)
        {
            return admins.Where(v => v.Id == id).FirstOrDefault();
        }
        public Admin Store(Admin admin)
        {
            admins.Add(admin);
            return admin;
        }
        public bool HasAny(AdminDTO adminDTO)
        {
            return (admins.Where(a => a.Email == adminDTO.Email).Any());
        }
        public List<Admin> GetAll(int? page)
        {
            return admins;
        }

        public Admin? Login(LoginDTO loginDTO)
        {
            return (admins.Where(a => a.Email == loginDTO.Email && a.Password == loginDTO.Password).FirstOrDefault());
        }
    }
}
