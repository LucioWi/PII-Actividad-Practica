using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica01.Domain;

namespace Practica01.Data
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public bool Delete(int id)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter()
                {
                    Name = "@nroFactura",
                    Valor = id
                }
            };

            return DataHelper.GetInstance().ExecuteSpDml("SP_Get_Invoice", param);
        }

        public List<Invoice> GetAll()
        {
            List<Invoice> lst = new List<Invoice>();

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_Get_Invoice");

            foreach (DataRow row in dt.Rows)
            {
                Invoice i = new Invoice();
                i.NroFactura = (int)row["nroFactura"];
                i.Fecha = (string)row["fecha"];
                i.FormaPago = (List<PaymentMethod>)row["idFormaPago"];
                i.Cliente = (string)row["cliente"];

                lst.Add(i);
            }

            return lst;
        }

        public Invoice GetById(int id)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter()
                {
                    Name = "@nroFactura",
                    Valor = id
                }
            };

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_Get_Invoice_By_Id");

            if (dt != null && dt.Rows.Count > 0)
            {
                Invoice di = new Invoice()
                {
                    NroFactura = (int)dt.Rows[0]["nroFactura"],
                    Fecha = (string)dt.Rows[0]["fecha"],
                    FormaPago = (List<PaymentMethod>)dt.Rows[0]["idFormaPago"],
                    Cliente = (string)dt.Rows[0]["cliente"],
                };

                return di;
            }

            return null;
        }

        public bool Save(Invoice invoice)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter("@nroFactura", invoice.NroFactura),
                new SpParameter("@fecha", invoice.Fecha),
                new SpParameter("@idFormaPago", invoice.FormaPago),
                new SpParameter("@cliente", invoice.Cliente)
            };

            return DataHelper.GetInstance().ExecuteSpDml("SP_Save_Detail", param);
        }
    }
}
