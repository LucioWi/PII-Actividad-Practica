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
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter()
                {
                    Name = "@idDetalle",
                    Valor = id
                }
            };

            // Eliminamos el registro correspondiente a través del SP
            return DataHelper.GetInstance().ExecuteSpDml("SP_Delete_Detail", param);
        }

        public List<DetailInvoice> GetAll()
        {
            List<DetailInvoice> lst = new List<DetailInvoice>();

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_Get_Detail");

            foreach (DataRow row in dt.Rows)
            {
                DetailInvoice dv = new DetailInvoice();
                dv.Id = (int)row["idDetalleFactura"];
                dv.NroFactura = (List<Invoice>)row["nroFactura"];
                dv.IdArticulo = (List<Article>)row["idArticulo"];
                dv.Cantidad = (int)row["cantidad"];

                lst.Add(dv);
            }

            return lst;
        }

        public DetailInvoice? GetById(int id)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter()
                {
                    Name = "@idDetalle",
                    Valor = id
                }
            };

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_Get_Detail_By_Id");

            if (dt != null && dt.Rows.Count > 0)
            {
                DetailInvoice di = new DetailInvoice()
                {
                    Id = (int)dt.Rows[0]["idDetalle"],
                    NroFactura = (List<Invoice>)dt.Rows[0]["nroFactura"],
                    IdArticulo = (List<Article>)dt.Rows[0]["idArticulo"],
                    Cantidad = (int)dt.Rows[0]["cantidad"],
                };

                return di;
            }

            return null;
        }

        public bool Save(DetailInvoice detail)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter("@idDetalle", detail.Id),
                new SpParameter("@nroFactura", detail.NroFactura),
                new SpParameter("@idArticulo", detail.IdArticulo),
                new SpParameter("@cantidad", detail.Cantidad)
            };

            return DataHelper.GetInstance().ExecuteSpDml("SP_Save_Detail", param);
        }
    }
}
