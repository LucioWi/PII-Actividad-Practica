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

        public Invoice? GetInvoice(int id)
        {
            return _repository.GetById(id);
        }

        public bool SaveIngredient(Invoice invoice)
        {
            return _repository.Save(invoice);
        }
    }
}
