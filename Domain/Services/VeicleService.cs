using minimal_api.Domain.Interfaces;
using minimal_api.Domain.Entities;
using minimal_api.Infra.Db;

namespace minimal_api.Domain.Services
{
    public class VeicleService(AppDbContext db) : IVeicleService
    {
        private readonly AppDbContext _db = db;

        public List<Veicle> GetAll(int pages = 1, string? name = null, string? brand = null)
        {
            var veiculos = _db.Veicles.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name)) 
                veiculos = veiculos.Where(v => v.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(brand)) 
                veiculos = veiculos.Where(v => v.Brand.Contains(brand, StringComparison.CurrentCultureIgnoreCase));
            return [.. veiculos.Skip((pages - 1) * 10).Take(10)];
        }

        public Veicle SearchById(int id)
        {
            return _db.Veicles.Find(id);
        }

        public void Store(Veicle veiculo)
        {
            _db.Veicles.Add(veiculo);
            _db.SaveChanges();
        }

        public void Amend(Veicle veiculo)
        {
            _db.Veicles.Update(veiculo);
            _db.SaveChanges();
        }

        public void Erase(Veicle veiculo)
        {
            _db.Veicles.Remove(veiculo);
            _db.SaveChanges();
        }
    }
}
