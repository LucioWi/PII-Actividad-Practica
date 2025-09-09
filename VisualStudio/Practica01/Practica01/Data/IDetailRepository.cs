using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica01.Domain;

namespace Practica01.Data
{
    public interface IDetailRepository
    {
        List<DetailInvoice> GetAll();
        DetailInvoice? GetById(int id);
        bool Save(DetailInvoice detail);
        bool Delete(int id);
    }
}
