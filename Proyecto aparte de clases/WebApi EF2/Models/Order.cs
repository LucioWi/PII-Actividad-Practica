namespace WebApi_EF2.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public ICollection<ItemOrden> Items { get; set; }
    }
}
