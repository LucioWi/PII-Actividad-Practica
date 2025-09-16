using WebApi_EF2.Models;

namespace WebApi_EF2.Repositories
{
    public interface IRepository
    {
        public List<Order> GetByFilters(string client);
        public Order GetById(int id);
    }
}
