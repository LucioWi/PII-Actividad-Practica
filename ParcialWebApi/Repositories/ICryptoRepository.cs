using ParcialWebApi.Models;

namespace ParcialWebApi.Repositories
{
    public interface ICryptoRepository
    {
        void Create(Criptomoneda criptomoneda);
        List<Criptomoneda> GetAll();
        Criptomoneda? GetById(int id);
        void Update(Criptomoneda criptomoneda);
        void Delete(int id);
        List<Criptomoneda> GetByCategory(string categoria);
        bool UpdateValorBySimb(string simbolo, double nuevoValor);
    }
}
