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
                DetailInvoice i = new DetailInvoice();
                i.Codigo = (int)row["codigo"];
                i.Nombre = (string)row["n_ingrediente"];
                i.Codigo_Producto = (int)row["codigo_producto"];
                i.Cantidad = (double)row["cantidad"];
                i.Unidad = (string)row["unidad"];

                lst.Add(i);
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
