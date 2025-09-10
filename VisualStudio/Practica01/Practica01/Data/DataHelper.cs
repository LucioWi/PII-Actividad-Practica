using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Practica01.Domain;

namespace Practica01.Data
{
    public class DataHelper
    {
        private static DataHelper _instance;
        private SqlConnection _connection;

        private DataHelper() 
        {
            _connection = new SqlConnection(Properties.Resources.LocalConnection); // UTNServerConnection en caso de estar en la UTN.
        }
        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }
        public DataTable ExecuteSPQuery(string sp)
        {
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;
                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException ex)
            {
                throw ex;
                dt = null;
            }
            finally
            {
                _connection.Close();
            }
            return dt;
        }
        public bool ExecuteSpDml(string sp, List<SpParameter>? param = null)
        {
            bool result;
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (param != null)
                {
                    foreach (SpParameter p in param)
                    {
                        cmd.Parameters.AddWithValue(p.Name, p.Valor);
                    }
                }

                int affectedRows = cmd.ExecuteNonQuery();

                result = affectedRows > 0;
            }
            catch (SqlException ex)
            {
                result = false;
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public bool ExecuteTransaction(DetailInvoice detailInvoice)
        {
            _connection.Open();

            SqlTransaction transaction = _connection.BeginTransaction();

            var cmd = new SqlCommand("SP_Save_DetailInvoice", _connection, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idDetalle", detailInvoice.Id);
            cmd.Parameters.AddWithValue("@idArticulo", detailInvoice.IdArticulo);
            cmd.Parameters.AddWithValue("@cantidad", detailInvoice.Cantidad);

            int affectedRows = cmd.ExecuteNonQuery();
            if (affectedRows <= 0)
            {
                transaction.Rollback();
                return false;
            }
            else
            {

                foreach (Invoice i in detailInvoice.NroFactura)
                {

                    SqlCommand cmdDetalle = new SqlCommand("SP_Save_Invoice", _connection, transaction);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;

                    int codigoFactura = 1;

                    cmdDetalle.Parameters.AddWithValue("@nroFactura", i.NroFactura);
                    cmdDetalle.Parameters.AddWithValue("@fecha", i.Fecha);
                    cmdDetalle.Parameters.AddWithValue("@idFormaPago", i.FormaPago);
                    cmdDetalle.Parameters.AddWithValue("@cliente", i.Cliente);

                    int affectedRowsDetalle = cmdDetalle.ExecuteNonQuery();

                    if (affectedRowsDetalle <= 0)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }

                transaction.Commit();
                return true;
            }
        }
    }
}
