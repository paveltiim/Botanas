using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresDatos
{
    public class DatProveedores : DatAbstracta
    {
        public DataTable obtieneProveedores()
        {
            try
            {
                com = new SqlCommand("selObtieneProveedores", con);
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
        public DataTable obtieneProveedores(int EmpresaId)
        {
            try
            {
                com = new SqlCommand("selObtieneProveedoresPorEmpresa", con);
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
        //public DataTable obtieneEmpresaGastosPorEmpresa(int EmpresaId)
        //{
        //    try
        //    {
        //        com = new SqlCommand("selObtieneEmpresasGastosDeudaPorEmpresa", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("EmpresaId", EmpresaId);
        //        da = new SqlDataAdapter(com);
        //        dt = new DataTable();
        //        da.Fill(dt);
        //        //if (dt.Rows.Count == 0)
        //        //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
        //        return dt;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}
        //public DataTable obtieneEmpresasGastosDeuda()
        //{
        //    try
        //    {
        //        com = new SqlCommand("selObtieneEmpresasGastosDeuda", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        da = new SqlDataAdapter(com);
        //        dt = new DataTable();
        //        da.Fill(dt);
        //        //if (dt.Rows.Count == 0)
        //        //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
        //        return dt;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}
        //public DataTable obtieneEmpresasGastosDeuda(DateTime FechaDesde, DateTime FechaHasta)
        //{
        //    try
        //    {
        //        com = new SqlCommand("selObtieneEmpresasGastosDeudaPorFechas", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("FechaDesde", FechaDesde);
        //        com.Parameters.AddWithValue("FechaHasta", FechaHasta);
        //        da = new SqlDataAdapter(com);
        //        dt = new DataTable();
        //        da.Fill(dt);
        //        //if (dt.Rows.Count == 0)
        //        //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
        //        return dt;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}
        //public DataTable obtieneEmpresasGastos(DateTime FechaDesde, DateTime FechaHasta)
        //{
        //    try
        //    {
        //        com = new SqlCommand("selObtieneEmpresasGastosPorFechas", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("FechaDesde", FechaDesde);
        //        com.Parameters.AddWithValue("FechaHasta", FechaHasta);
        //        da = new SqlDataAdapter(com);
        //        dt = new DataTable();
        //        da.Fill(dt);
        //        //if (dt.Rows.Count == 0)
        //        //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
        //        return dt;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}

        //public DataTable obtienePagosEmpresas(DateTime FechaDesde, DateTime FechaHasta)
        //{
        //    try
        //    {
        //        com = new SqlCommand("selObtienePagosEmpresasPorFechaPago", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("FechaDesde", FechaDesde);
        //        com.Parameters.AddWithValue("FechaHasta", FechaHasta);
        //        da = new SqlDataAdapter(com);
        //        dt = new DataTable();
        //        da.Fill(dt);
        //        //if (dt.Rows.Count == 0)
        //        //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
        //        return dt;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}
        //public DataTable obtieneNotasCreditoEmpresas(DateTime FechaDesde, DateTime FechaHasta)
        //{
        //    try
        //    {
        //        com = new SqlCommand("selObtieneNotasCreditoEmpresasPorFecha", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("FechaDesde", FechaDesde);
        //        com.Parameters.AddWithValue("FechaHasta", FechaHasta);
        //        da = new SqlDataAdapter(com);
        //        dt = new DataTable();
        //        da.Fill(dt);
        //        //if (dt.Rows.Count == 0)
        //        //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
        //        return dt;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}

        public int agregaProveedores(int EmpresaId, string Nombre, string NombreFiscal, string RFC, string Direccion, string Telefono, string Telefono2, string Email, string Contacto, string TelefonoContacto, string Banco, string NumeroCuenta, string Sucursal, string CLABE, string NumeroReferencia, DateTime Fecha)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("insAgregaProveedor", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("Nombre", Nombre);
                com.Parameters.AddWithValue("NombreFiscal", NombreFiscal);
                com.Parameters.AddWithValue("RFC", RFC);
                com.Parameters.AddWithValue("Direccion", Direccion);
                com.Parameters.AddWithValue("Telefono", Telefono);
                com.Parameters.AddWithValue("Telefono2", Telefono2);
                com.Parameters.AddWithValue("Email", Email);
                com.Parameters.AddWithValue("Contacto", Contacto);
                com.Parameters.AddWithValue("TelefonoContacto", TelefonoContacto);
                com.Parameters.AddWithValue("Banco", Banco);
                com.Parameters.AddWithValue("NumeroCuenta", NumeroCuenta);
                com.Parameters.AddWithValue("Sucursal", Sucursal);
                com.Parameters.AddWithValue("CLABE", CLABE);
                com.Parameters.AddWithValue("NumeroReferencia", NumeroReferencia);
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
        //public int agregaEmpresaRentas(string Nombre, string NombreFiscal, string Direccion, string Telefono, string Telefono2, string Email, string Contacto, string TelefonoContacto, string Banco, string NumeroCuenta, string Sucursal, string CLABE, string NumeroReferencia, DateTime Fecha)
        //{
        //    try
        //    {
        //        int Id = 0;

        //        com = new SqlCommand("insAgregaEmpresaRentas", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("Nombre", Nombre);
        //        com.Parameters.AddWithValue("NombreFiscal", NombreFiscal);
        //        com.Parameters.AddWithValue("Direccion", Direccion);
        //        com.Parameters.AddWithValue("Telefono", Telefono);
        //        com.Parameters.AddWithValue("Telefono2", Telefono2);
        //        com.Parameters.AddWithValue("Email", Email);
        //        com.Parameters.AddWithValue("Contacto", Contacto);
        //        com.Parameters.AddWithValue("TelefonoContacto", TelefonoContacto);
        //        com.Parameters.AddWithValue("Banco", Banco);
        //        com.Parameters.AddWithValue("NumeroCuenta", NumeroCuenta);
        //        com.Parameters.AddWithValue("Sucursal", Sucursal);
        //        com.Parameters.AddWithValue("CLABE", CLABE);
        //        com.Parameters.AddWithValue("NumeroReferencia", NumeroReferencia);
        //        com.Parameters.AddWithValue("FechaRegistro", Fecha);
        //        SqlParameter parm = new SqlParameter("Id", Id);
        //        parm.Direction = ParameterDirection.InputOutput;
        //        com.Parameters.Add(parm);
        //        con.Open();
        //        com.ExecuteNonQuery();

        //        return Convert.ToInt32(com.Parameters["Id"].Value);
        //        //if (dt.Rows.Count == 0)
        //        //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //    finally { con.Close(); }
        //}
        //public int agregaNotaCredito(int GastoId, decimal Cantidad, DateTime Fecha)
        //{
        //    try
        //    {
        //        int Id = 0;

        //        com = new SqlCommand("insAgregaNotaDeCredito", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("GastoId", GastoId);
        //        com.Parameters.AddWithValue("Cantidad", Cantidad);
        //        com.Parameters.AddWithValue("Fecha", Fecha);
        //        SqlParameter parm = new SqlParameter("Id", Id);
        //        parm.Direction = ParameterDirection.InputOutput;
        //        com.Parameters.Add(parm);
        //        con.Open();
        //        com.ExecuteNonQuery();

        //        return Convert.ToInt32(com.Parameters["Id"].Value);
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //    finally { con.Close(); }
        //}
        public int actualizaProveedor(int ProveedorId, int EmpresaId, string Nombre, string NombreFiscal, string Direccion, string Telefono, string Telefono2, string Email, string Contacto, string TelefonoContacto, string Banco, string NumeroCuenta, string Sucursal, string CLABE, string NumeroReferencia)
        {
            try
            {
                int Id = ProveedorId;

                com = new SqlCommand("updActualizaProveedor", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProveedorId", ProveedorId);
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("Nombre", Nombre);
                com.Parameters.AddWithValue("NombreFiscal", NombreFiscal);
                com.Parameters.AddWithValue("Direccion", Direccion);
                com.Parameters.AddWithValue("Telefono", Telefono);
                com.Parameters.AddWithValue("Telefono2", Telefono2);
                com.Parameters.AddWithValue("Email", Email);
                com.Parameters.AddWithValue("Contacto", Contacto);
                com.Parameters.AddWithValue("TelefonoContacto", TelefonoContacto);
                com.Parameters.AddWithValue("Banco", Banco);
                com.Parameters.AddWithValue("NumeroCuenta", NumeroCuenta);
                com.Parameters.AddWithValue("Sucursal", Sucursal);
                com.Parameters.AddWithValue("CLABE", CLABE);
                com.Parameters.AddWithValue("NumeroReferencia", NumeroReferencia);
                con.Open();
                com.ExecuteNonQuery();

                return Id;
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public int actualizaEstatusProveedor(int ProveedorId, bool Estatus)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusProveedor", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProveedorId", ProveedorId);
                com.Parameters.AddWithValue("Estatus", Estatus);
                con.Open();
                com.ExecuteNonQuery();

                return ProveedorId;
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
    }
}
