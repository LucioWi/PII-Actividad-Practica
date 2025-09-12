using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica01.Domain;

namespace Practica01.Facade
{
    public interface IDataApi
    {
        public List<DetailInvoice> GetDetail();
        public bool SaveInvoice(Invoice invoice);
    }
}
