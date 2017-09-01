using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresDatos
{
    public class DatDivisas:DatAbstracta
    {
        public DataTable obtieneDivisa(int DivisaId)
        {
            try
            {
                com = new SqlCommand("selObtieneDivisaPorId", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("DivisaId", DivisaId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

    }
}
