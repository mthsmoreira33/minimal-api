using minimal_api.Dominio.Entidades;

namespace minimal_api.Dominio.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(Administrador administrador);
    }
}
