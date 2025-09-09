using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica01.Domain;

namespace Practica01.Data
{
    public class DetailRepository : IDetailRepository
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<DetailInvoice> GetAll()
        {
            List<DetailInvoice> lst = new List<DetailInvoice>();

            // Traer registros de la BD
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_Get_DetailInvoice");

            // Mapear cada DataRow a un Ingredient
            foreach (DataRow row in dt.Rows)
            {
                DetailInvoice dv = new DetailInvoice();
                dv.Id = (int)row["idDetalleFactura"];
                dv.NroFactura = (int)row["nroFactura"];
                dv.IdArticulo = (List<Article>)row["idArticulo"];
                dv.Cantidad = (int)row["cantidad"];

                lst.Add(dv);
            }

            return lst;
        }

        public DetailInvoice? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save(DetailInvoice detail)
        {
            throw new NotImplementedException();
        }
    }
}
