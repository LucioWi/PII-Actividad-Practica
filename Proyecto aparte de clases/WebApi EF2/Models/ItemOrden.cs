namespace WebApi_EF2.Models
{
    public class ItemOrden
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }

        //Opcional
        public int OrderId { get; set; }
        public Order Order { get; set; }

        // ------------------------

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
