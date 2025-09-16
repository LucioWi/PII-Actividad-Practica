using Microsoft.EntityFrameworkCore;

namespace WebApi_EF2.Models
{
    public class OrdersContext : DbContext // Contexto para el EF
    {
        public DbSet<Order> Orders { get; set; } // Un DbSet por cada model
        public DbSet<Product> Products { get; set; }
        public DbSet<ItemOrden> ItemsOrder { get; set; }
        public OrdersContext(DbContextOptions<OrdersContext>options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // Ctrl + . -> Generar invalidaciones -> OnModelCreating
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
