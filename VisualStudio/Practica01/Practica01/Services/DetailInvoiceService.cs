using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica01.Data;
using Practica01.Domain;

namespace Practica01.Services
{
    public class DetailInvoiceService
    {
        private IDetailRepository _repository;
        public DetailInvoiceService()
        {
            _repository = new DetailRepository();
        }

        public List<DetailInvoice> GetProducts()
        {
            return _repository.GetAll();
        }

        public DetailInvoice? GetProductById(int id)
        {
            return _repository.GetById(id);
        }

        public bool SaveProduct(DetailInvoice detail)
        {
            if (detail.Cantidad < 0)
            {
                return false;
            }

            return _repository.Save(detail);
        }

        public bool DeleteProduct(int id)
        {

            var detailInBD = _repository.GetById(id);

            return detailInBD != null ? _repository.Delete(id) : false;

        }

        public bool ExecuteTransaction(DetailInvoice detail)
        {
            return DataHelper.GetInstance().ExecuteTransaction(detail);
        }
    }
}
