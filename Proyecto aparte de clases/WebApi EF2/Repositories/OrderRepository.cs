using Microsoft.EntityFrameworkCore;
using WebApi_EF2.Models;

namespace WebApi_EF2.Repositories
{
    public class OrderRepository : IRepository
    {
        private OrdersContext _context;

        public List<Order> GetByFilters(string client)
        {
            return _context.Orders.Include(x => x.Items).Where(x => x.Cliente.Contains(client)).ToList();
        }

        public Order GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
