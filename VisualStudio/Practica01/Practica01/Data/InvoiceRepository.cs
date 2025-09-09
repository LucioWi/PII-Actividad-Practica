using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica01.Domain;

namespace Practica01.Data
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public bool Delete(int id)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter()
                {
                    Name = "@codigo",
                    Valor = id
                }
            };

            return DataHelper.GetInstance().ExecuteSpDml("SP_REGISTRAR_BAJA_PRODUCTO", param);
        }

        public List<Invoice> GetAll()
        {
            throw new NotImplementedException();
        }

        public Invoice GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save(Invoice invoice)
        {
            throw new NotImplementedException();
        }
    }
}
