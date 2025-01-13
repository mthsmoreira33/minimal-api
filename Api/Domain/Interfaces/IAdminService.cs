using minimal_api.Domain.DTOs;
using minimal_api.Domain.Entities;

namespace minimal_api.Domain.Interfaces
{
    public interface IAdminService
    {
        public Admin? SearchById(int id);
        Admin? Login(LoginDTO loginDTO);
        Admin Store(Admin admin);
        bool HasAny(AdminDTO adminDTO);

        public List<Admin> GetAll(int? page);
    }

}