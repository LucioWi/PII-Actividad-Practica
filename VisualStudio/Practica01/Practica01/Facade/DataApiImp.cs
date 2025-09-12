using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica01.Data;
using Practica01.Domain;

namespace Practica01.Facade
{
    public class DataApiImp
    {
        private IDetailRepository repositoryDetail;
        private IInvoiceRepository repositoryInvoice;

        public DataApiImp()
        {
            repositoryDetail = new DetailRepository();
        }

        public List<DetailInvoice> GetProductos()
        {
            return repositoryDetail.GetAll();
        }

        public bool SaveInvoice(Invoice invoice)
        {
            return repositoryInvoice.Save(invoice);
        }
    }
}
