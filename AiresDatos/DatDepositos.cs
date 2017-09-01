using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresDatos
{
    public class DatDepositos:DatAbstracta
    {
        public int agregaDeposito(decimal Total, DateTime FechaDeposito)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("insAgregaDeposito", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("Total", Total);
                com.Parameters.AddWithValue("Fecha", FechaDeposito);
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
        public int agregaPagoDeposito(int DepositoId, int PagoId)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("insAgregaPagoDeposito", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("DepositoId", DepositoId);
                com.Parameters.AddWithValue("PagoId", PagoId);
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
        public void actualizaDeposito(int DepositoId, decimal Total, DateTime FechaDeposito)
        {
            try
            {
                com = new SqlCommand("updActualizaDeposito", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("DepositoId", DepositoId);
                com.Parameters.AddWithValue("Total", Total);
                com.Parameters.AddWithValue("Fecha", FechaDeposito);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public DataTable ObtieneDepositos(int EmpresaId)
        {
            try
            {
                com = new SqlCommand("selObtieneDepositosEmpresa", con);
                com.CommandType = CommandType.StoredProcedure;
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
        public DataTable ObtieneDepositos(DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("selObtieneDepositosPagosPorFechas", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FechaHasta);
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
