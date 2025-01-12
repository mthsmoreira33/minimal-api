using minimal_api.Domain.Entities;

namespace minimal_api.Domain.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(Admin administrador);
    }
}
