using ParcialWebApi.Models;

namespace ParcialWebApi.Repositories
{
    public class CryptoRepository : ICryptoRepository
    {
        private CriptoContext _context;

        public CryptoRepository(CriptoContext context) 
        { 
            _context = context;
        }
        public void Create(Criptomoneda criptomoneda)
        {
            throw new NotImplementedException();
        }
        public List<Criptomoneda> GetAll()
        {
            return _context.Criptomonedas.ToList();
        }
        public Criptomoneda? GetById(int id)
        {
            return _context.Criptomonedas.Find(id);
        }
        public void Update(Criptomoneda criptomoneda)
        {
            if (criptomoneda != null)
            {
                var exist = _context.Criptomonedas.Find(criptomoneda.Id);
                if (exist != null)
                {
                    exist.Nombre = criptomoneda.Nombre;
                    exist.Simbolo = criptomoneda.Simbolo;
                    exist.ValorActual = criptomoneda.ValorActual;
                    exist.UltimaActualizacion = criptomoneda.UltimaActualizacion;
                    exist.Categoria = criptomoneda.Categoria;
                    exist.Estado = criptomoneda.Estado;

                    _context.SaveChanges();
                }
            }
        }
        public void Delete(int id)
        {
            var CriptoDeleted = GetById(id);
            if (CriptoDeleted != null)
            {
                _context.Criptomonedas.Remove(CriptoDeleted);
                _context.SaveChanges();
            }

        }

        public List<Criptomoneda> GetByCategory(string categoria)
        {
            var unDiaAtras = DateTime.Now.AddDays(-1);

            return _context.Criptomonedas.Where(x => x.CategoriaNavigation.Nombre.Equals(categoria)).ToList();
        }

        public bool UpdateValorBySimb(string simbolo, double nuevoValor)
        {
            var cripto = _context.Criptomonedas.FirstOrDefault(c => c.Simbolo.ToLower() == simbolo.ToLower());

            if (cripto == null)
                return false;

            cripto.ValorActual = nuevoValor;
            cripto.UltimaActualizacion = DateTime.Now;

            _context.SaveChanges();
            return true;
        }
    }
}
