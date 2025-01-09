using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Interfaces;
using minimal_api.Infraestrutura.Db;

namespace minimal_api.Dominio.Servicos
{
    public class VeiculoService : IVeiculoService
    {
        private readonly AppDbContext _db;
        public VeiculoService(AppDbContext db) {
            _db = db;
        }

        public List<Veiculo> Todos(int pagina = 1, string? nome = null, string? marca = null)
        {
            var veiculos = _db.Veiculos.AsQueryable();
            if (!string.IsNullOrWhiteSpace(nome)) 
                veiculos = veiculos.Where(v => v.Nome.ToLower().Contains(nome));
            if (!string.IsNullOrWhiteSpace(marca)) 
                veiculos = veiculos.Where(v => v.Marca.ToLower().Contains(marca));
            return [.. veiculos.Skip((pagina - 1) * 10).Take(10)];
        }

        public Veiculo BuscaPorId(int id)
        {
            return _db.Veiculos.Find(id);
        }

        public void Incluir(Veiculo veiculo)
        {
            _db.Veiculos.Add(veiculo);
            _db.SaveChanges();
        }

        public void Atualizar(Veiculo veiculo)
        {
            _db.Veiculos.Update(veiculo);
            _db.SaveChanges();
        }

        public void Apagar(Veiculo veiculo)
        {
            _db.Veiculos.Remove(veiculo);
            _db.SaveChanges();
        }
    }
}
