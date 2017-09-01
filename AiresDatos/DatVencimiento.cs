using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresDatos
{
    public class DatVencimiento : DatAbstracta
    {
        public DataTable obtieneVencimientoActivo()
        {
            try
            {
                com = new SqlCommand("selObtieneVencimiento", con);
                com.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public void verificaVencimiento()
        {
            try
            {
                com = new SqlCommand("VerificaVencimiento", con);
                com.CommandType = CommandType.StoredProcedure;
                con.Open();
                com.ExecuteNonQuery();

                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
    }
}
