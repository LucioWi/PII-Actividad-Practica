using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Domain
{
    public class DetailInvoice
    {
        public int Id { get; set; }
        public List<Invoice> NroFactura {  get; set; }
        public List<Article> IdArticulo { get; set; }
        public int Cantidad { get; set; }
    }
}
