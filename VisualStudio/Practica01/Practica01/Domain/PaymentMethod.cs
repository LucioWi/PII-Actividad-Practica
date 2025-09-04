using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Domain
{
    public class PaymentMethod
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }

        public override string ToString()
        {
            return "Tipo de pago: "+Nombre;
        }
    }
}
