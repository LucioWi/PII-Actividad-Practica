using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Practica01.Domain
{
    public class Invoice // La BD esta en español
    {
        public int NroFactura { get; set; }
        public string Fecha { get; set; }
        public PaymentMethod FormaPago { get; set; }
        public string Cliente { get; set; }

        public override string ToString()
        {
            return NroFactura + " - " + Fecha + " - " + FormaPago;
        }
    }
}
