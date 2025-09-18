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

        public bool ExecuteTransaction(Invoice invoice)
        {
            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();

            try
            {
                // Parseo de fecha (si tu Invoice.Fecha es string)
                DateTime fechaParsed;
                string fechaStr = Convert.ToString(invoice.Fecha);
                if (!DateTime.TryParse(fechaStr, out fechaParsed))
                {
                    transaction.Rollback();
                    return false;
                }

                // Insertar factura (padre) y obtener nro generado
                using (var cmd = new SqlCommand("SP_Save_Invoice", _connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@fecha", System.Data.SqlDbType.DateTime) { Value = fechaParsed });
                    cmd.Parameters.AddWithValue("@idFormaPago", invoice.IdFormaPago);
                    cmd.Parameters.AddWithValue("@cliente", invoice.Cliente);

                    var outParam = new SqlParameter("@invoiceNumber", System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outParam);

                    cmd.ExecuteNonQuery(); // con NOCOUNT ON puede devolver -1, ignoramos ese valor

                    int invoiceNumber = (outParam.Value == DBNull.Value) ? 0 : Convert.ToInt32(outParam.Value);
                    if (invoiceNumber <= 0)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    // Insertar detalles (hijos)
                    if (invoice.Details != null)
                    {
                        foreach (DetailInvoice d in invoice.Details)
                        {
                            using (var cmdDetalle = new SqlCommand("SP_Save_Detail", _connection, transaction))
                            {
                                cmdDetalle.CommandType = CommandType.StoredProcedure;

                                cmdDetalle.Parameters.AddWithValue("@invoiceNumber", invoiceNumber);
                                cmdDetalle.Parameters.AddWithValue("@articleId", d.IdArticulo);
                                cmdDetalle.Parameters.AddWithValue("@quantity", d.Cantidad);

                                // NO comprobar affectedRows porque SP usa SET NOCOUNT ON (ExecuteNonQuery -> -1)
                                cmdDetalle.ExecuteNonQuery();
                                // si querés una comprobación extra: podrías validar que no se lanzó excepción
                            }
                        }
                    }

                    transaction.Commit();
                    return true;
                }
            }
            catch
            {
                try { transaction.Rollback(); } catch { }
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
