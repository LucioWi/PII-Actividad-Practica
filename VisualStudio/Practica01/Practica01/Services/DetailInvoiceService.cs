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

        public List<DetailInvoice> GetDetail()
        {
            return _repository.GetAll();
        }

        public DetailInvoice? GetDetailById(int id)
        {
            return _repository.GetById(id);
        }

        public bool SaveDetail(DetailInvoice detail)
        {
            if (detail.Cantidad < 0)
            {
                return false;
            }

            return _repository.Save(detail);
        }
    }
}
