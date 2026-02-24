using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresDatos
{
    public class DatTrabajadores : DatAbstracta
    {
        public DataTable obtieneTrabajadoresPorEmpresa(int EmpresaId)
        {
            try
            {
                com = new SqlCommand("selObtieneTrabajadoresPorEmpresa", con);
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
        public DataTable obtieneTrabajadoresPorEmpresa(int EmpresaId, int TipoTrabajadorId)
        {
            try
            {
                com = new SqlCommand("selObtieneTrabajadoresPorEmpresaTipo", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("TipoTrabajadorId", TipoTrabajadorId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneTrabajadores(int EstablecimientoId)
        {
            try
            {
                com = new SqlCommand("selObtieneTrabajadoresPorEstablecimiento", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneTrabajadores(int RutaId, int TipoTrabajadorId)
        {
            try
            {
                com = new SqlCommand("selObtieneTrabajadoresPorRutaTipo", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("RutaId", RutaId);
                com.Parameters.AddWithValue("TipoTrabajadorId", TipoTrabajadorId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtieneTrabajador(int TrabajadorId)
        {
            try
            {
                com = new SqlCommand("selObtieneTrabajadorPorId", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("TrabajadorId", TrabajadorId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtieneCobradorPorRuta(int RutaId)
        {
            try
            {
                com = new SqlCommand("selObtieneTrabajadoresPorRutaTipo", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("RutaId", RutaId);
                com.Parameters.AddWithValue("TipoTrabajadorId", 1);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int agregaTrabajador(int EmpresaId, int EstablecimientoId, int TipoTrabajadorId, string Nombre,
            string Direccion, string Calle, string NoExterior, string NoInterior, string Colonia, string CP,
            string Telefono, string Telefono2, string Celular, string RFC, string Curp, string Credencial,
            string Email, string Email2, string Email3,
            int RutaId)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("insAgregaTrabajador", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
                com.Parameters.AddWithValue("TipoTrabajadorId", TipoTrabajadorId);
                com.Parameters.AddWithValue("Nombre", Nombre);
                com.Parameters.AddWithValue("Direccion", Direccion);
                com.Parameters.AddWithValue("Telefono", Telefono);
                com.Parameters.AddWithValue("Telefono2", Telefono2);
                com.Parameters.AddWithValue("Celular", Celular);
                com.Parameters.AddWithValue("Curp", Curp);
                com.Parameters.AddWithValue("Credencial", Credencial);
                com.Parameters.AddWithValue("RutaId", RutaId);
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

        public void actualizaTrabajador(int TrabajadorId, int TipoTrabajadorId, string Nombre,
            string Direccion, string Calle, string NoExterior, string NoInterior, string Colonia, string CP,
            string Telefono, string Telefono2, string Celular, string RFC, string Curp, string Credencial,
            string Email, string Email2, string Email3,
            int RutaId)
        {
            try
            {
                com = new SqlCommand("updActualizaTrabajador", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("TrabajadorId", TrabajadorId);
                //com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("TipoTrabajadorId", TipoTrabajadorId);
                com.Parameters.AddWithValue("Nombre", Nombre);
                com.Parameters.AddWithValue("Direccion", Direccion);
                //com.Parameters.AddWithValue("Calle", Calle);
                //com.Parameters.AddWithValue("NoExterior", NoExterior);
                //com.Parameters.AddWithValue("NoInterior", NoInterior);
                //com.Parameters.AddWithValue("Colonia", Colonia);
                //com.Parameters.AddWithValue("CP", CP);
                com.Parameters.AddWithValue("Telefono", Telefono);
                com.Parameters.AddWithValue("Telefono2", Telefono2);
                com.Parameters.AddWithValue("Celular", Celular);
                //com.Parameters.AddWithValue("RFC", RFC);
                com.Parameters.AddWithValue("Curp", Curp);
                com.Parameters.AddWithValue("Credencial", Credencial);
                //com.Parameters.AddWithValue("Email", Email);
                //com.Parameters.AddWithValue("Email2", Email2);
                //com.Parameters.AddWithValue("Email3", Email3);
                com.Parameters.AddWithValue("RutaId", RutaId);
                con.Open();
                com.ExecuteNonQuery();

                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public void actualizaEstatusTrabajador(int TrabajadorId, bool Estatus)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusTrabajador", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("TrabajadorId", TrabajadorId);
                com.Parameters.AddWithValue("Estatus", Estatus);
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
