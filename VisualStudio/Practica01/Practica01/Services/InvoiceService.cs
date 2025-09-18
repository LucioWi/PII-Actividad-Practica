using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica01.Data;
using Practica01.Domain;

namespace Practica01.Services
{
    public class InvoiceService
    {
        private IInvoiceRepository _repository;
        public InvoiceService()
        {
            _repository = new InvoiceRepository();
        }

        public List<Invoice> GetInvoice()
        {
            return _repository.GetAll();
        }

        public Invoice? GetInvoiceById(int id)
        {
            return _repository.GetById(id);
        }

        public bool SaveInvoice(Invoice invoice)
        {
            return _repository.Save(invoice);
        }
        public bool DeleteInvoice(int id)
        {
            return _repository.Delete(id);
        }
        public bool ExecuteTransaction(Invoice invoice)
        {
            return DataHelper.GetInstance().ExecuteTransaction(invoice);
        }
    }
}
