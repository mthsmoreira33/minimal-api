using minimal_api.Domain.Entities;

namespace minimal_api.Domain.Interfaces
{
    public interface IVeicleService
    {
        List<Veicle> GetAll(int pagina = 1, string? nome = null, string? marca = null);
        Veicle SearchById(int id);
        void Store(Veicle veiculo);
        void Amend(Veicle veiculo);
        void Erase(Veicle veiculo);
    }
}
