using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresDatos
{
   public class DatGastos:DatAbstracta
    {
       public int agregaGasto(int EmpresaId, int TipoGastoId, string NumeroFactura, decimal Cantidad, decimal Pago, DateTime FechaFactura, string Observaciones, DateTime FechaRegistro)
       {
           try
           {
               int Id = 0;

               com = new SqlCommand("insAgregaGasto", con);
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.AddWithValue("EmpresaId", EmpresaId);
               com.Parameters.AddWithValue("TipoGastoId", TipoGastoId);
               com.Parameters.AddWithValue("NumeroFactura", NumeroFactura);
               com.Parameters.AddWithValue("Cantidad", Cantidad);
               com.Parameters.AddWithValue("Pago", Pago);
               com.Parameters.AddWithValue("FechaFactura", FechaFactura);
               com.Parameters.AddWithValue("Observaciones", Observaciones);
               com.Parameters.AddWithValue("FechaRegistro", FechaRegistro);
               SqlParameter parm = new SqlParameter("Id", Id);
               parm.Direction = ParameterDirection.InputOutput;
               com.Parameters.Add(parm);
               con.Open();
               com.ExecuteNonQuery();

               return Convert.ToInt32(com.Parameters["Id"].Value);
               //if (dt.Rows.Count == 0)
               //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
           }
           catch (Exception ex) { throw new Exception(ex.Message); }
           finally { con.Close(); }
       }
       public int agregaPagoGasto(int GastoId, int TipoPagoId, decimal Pago, DateTime FechaPago)
       {
           try
           {
               int Id = 0;

               com = new SqlCommand("insAgregaPagoGasto", con);
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.AddWithValue("GastoId", GastoId);
               com.Parameters.AddWithValue("TipoPagoId", TipoPagoId);
               com.Parameters.AddWithValue("Pago", Pago);
               com.Parameters.AddWithValue("FechaPago", FechaPago);
               SqlParameter parm = new SqlParameter("Id", Id);
               parm.Direction = ParameterDirection.InputOutput;
               com.Parameters.Add(parm);
               con.Open();
               com.ExecuteNonQuery();

               return Convert.ToInt32(com.Parameters["Id"].Value);
           }
           catch (Exception ex) { throw new Exception(ex.Message); }
           finally { con.Close(); }
       }
       public void actualizaGasto(int GastoId, int EmpresaId, string NumeroFactura, decimal Cantidad, DateTime FechaFactura, string Observaciones, int EstatusId)
       {
           try
           {
               com = new SqlCommand("updActualizaGasto", con);
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.AddWithValue("GastoId", GastoId);
               com.Parameters.AddWithValue("EmpresaId", EmpresaId);
               com.Parameters.AddWithValue("NumeroFactura", NumeroFactura);
               com.Parameters.AddWithValue("Cantidad", Cantidad);
               com.Parameters.AddWithValue("FechaFactura", FechaFactura);
               com.Parameters.AddWithValue("Observaciones", Observaciones);
               com.Parameters.AddWithValue("EstatusId", EstatusId);
               con.Open();
               com.ExecuteNonQuery();
           }
           catch (Exception ex) { throw new Exception(ex.Message); }
           finally { con.Close(); }
       }
        public DataTable ObtieneGastos(int TipoGastoId, int EmpresaId)
        {
            try
            {
                com = new SqlCommand("selObtieneGastosPorTipoGastoEmpresa", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("TipoGastoId", TipoGastoId);
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void aumentaPagoGasto(int GastoId, decimal Pago)
       {
           try
           {
               com = new SqlCommand("updAumentaPagoGasto", con);
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.AddWithValue("GastoId", GastoId);
               com.Parameters.AddWithValue("Pago", Pago);
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
