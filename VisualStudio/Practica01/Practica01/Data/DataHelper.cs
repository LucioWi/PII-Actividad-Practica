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
                // Abrimos la conexión
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Agregamos parámetros si los hay
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
                // En caso de error, retornamos false
                result = false;
            }
            finally
            {
                // Cerramos la conexión
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

            // Agregamos los parámetros
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

                foreach (Invoice i in DetailInvoice)
                {
                    // Para cada elemento de la lista tenemos que:
                    // - Crear un comando
                    SqlCommand cmdDetalle = new SqlCommand("SP_GUARDAR_INGREDIENTE", _connection, transaction);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;

                    // --------------------
                    // POR AHORA, HARDCODEAMOS EL CÓDIGO DEL PRODUCTO
                    // LO IDEAL ES OBTENERLO A PARTIR DE UN PARÁMETRO DE SALIDA
                    int codigoProducto = 1;
                    // --------------------

                    // - Asignar los parámetros
                    cmdDetalle.Parameters.AddWithValue("@codigo_producto", codigoProducto);
                    cmdDetalle.Parameters.AddWithValue("@nombre", i.Nombre);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", i.Cantidad);
                    cmdDetalle.Parameters.AddWithValue("@unidad", i.Unidad);

                    // - Ejecutar el comando
                    int affectedRowsDetalle = cmdDetalle.ExecuteNonQuery();

                    // - Validar el resultado y revertir en caso de que sea necesario
                    if (affectedRowsDetalle <= 0)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }

                // Ya insertamos el maestro y todos sus detalles sin problemas -> COMMIT
                // Se confirma la transacción y se retorna true
                transaction.Commit();
                return true;
            }
        }
    }
}
