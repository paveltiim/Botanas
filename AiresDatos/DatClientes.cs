using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresDatos
{
    public class DatClientes : DatAbstracta
    {
        public DataTable obtieneClientes()
        {
            try
            {
                com = new SqlCommand("selObtieneClientes", con);
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
        public DataTable obtieneClientes(int EmpresaId)
        {
            try
            {
                com = new SqlCommand("selObtieneClientesPorEmpresa", con);
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

        public DataTable obtieneCliente(int ClienteId)
        {
            try
            {
                com = new SqlCommand("selObtieneClientePorId", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ClienteId", ClienteId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtieneClientesCredito(int EmpresaId)
        {
            try
            {
                com = new SqlCommand("[selObtieneClientesDeudaPorEmpresa]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int agregaCliente(int EmpresaId, int TipoPersonaId, string Nombre, string NombreFiscal, string Direccion, string Calle, string NoExterior, string NoInterior, string Colonia,
            string Localidad, string Municipio, string Estado, string CP, string Telefono, string Telefono2, string Celular, string RFC, string Email, string Email2, string Email3, string Banco, string NumeroCuenta, 
            string Sucursal, string CLABE, string NumeroReferencia, int FormaPagoId, bool IncluyeKit,  DateTime FechaRegistro)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("insAgregaCliente", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("TipoPersonaId", TipoPersonaId);
                com.Parameters.AddWithValue("Nombre", Nombre);
                com.Parameters.AddWithValue("NombreFiscal", NombreFiscal);
                com.Parameters.AddWithValue("Direccion", Direccion);
                com.Parameters.AddWithValue("Calle", Calle);
                com.Parameters.AddWithValue("NoExterior", NoExterior);
                com.Parameters.AddWithValue("NoInterior", NoInterior);
                com.Parameters.AddWithValue("Colonia", Colonia);
                com.Parameters.AddWithValue("Localidad", Localidad);
                com.Parameters.AddWithValue("Municipio", Municipio);
                com.Parameters.AddWithValue("Estado", Estado);
                com.Parameters.AddWithValue("CP", CP);
                com.Parameters.AddWithValue("Telefono", Telefono);
                com.Parameters.AddWithValue("Telefono2", Telefono2);
                com.Parameters.AddWithValue("Celular", Celular);
                com.Parameters.AddWithValue("RFC", RFC);
                com.Parameters.AddWithValue("Email", Email);
                com.Parameters.AddWithValue("Email2", Email2);
                com.Parameters.AddWithValue("Email3", Email3);
                com.Parameters.AddWithValue("Banco", Banco);
                com.Parameters.AddWithValue("NumeroCuenta", NumeroCuenta);
                com.Parameters.AddWithValue("Sucursal", Sucursal);
                com.Parameters.AddWithValue("CLABE", CLABE);
                com.Parameters.AddWithValue("NumeroReferencia", NumeroReferencia);
                com.Parameters.AddWithValue("FormaPagoId", FormaPagoId);
                com.Parameters.AddWithValue("IncluyeKit", IncluyeKit);
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

        public int agregaCliente(int Id, int EmpresaId, int TipoPersonaId, string Nombre, string NombreFiscal, string Direccion, string Calle, string NoExterior, string NoInterior, string Colonia,
            string Localidad, string Municipio, string Estado, string CP, string Telefono, string Telefono2, string Celular, string RFC, string Email, string Email2, string Email3, string Banco, string NumeroCuenta,
            string Sucursal, string CLABE, string NumeroReferencia, int FormaPagoId, bool IncluyeKit, DateTime FechaRegistro)
        {
            try
            {
                com = new SqlCommand("insAgregaClienteConId", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("Id", Id);
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("TipoPersonaId", TipoPersonaId);
                com.Parameters.AddWithValue("Nombre", Nombre);
                com.Parameters.AddWithValue("NombreFiscal", NombreFiscal);
                com.Parameters.AddWithValue("Direccion", Direccion);
                com.Parameters.AddWithValue("Calle", Calle);
                com.Parameters.AddWithValue("NoExterior", NoExterior);
                com.Parameters.AddWithValue("NoInterior", NoInterior);
                com.Parameters.AddWithValue("Colonia", Colonia);
                com.Parameters.AddWithValue("Localidad", Localidad);
                com.Parameters.AddWithValue("Municipio", Municipio);
                com.Parameters.AddWithValue("Estado", Estado);
                com.Parameters.AddWithValue("CP", CP);
                com.Parameters.AddWithValue("Telefono", Telefono);
                com.Parameters.AddWithValue("Telefono2", Telefono2);
                com.Parameters.AddWithValue("Celular", Celular);
                com.Parameters.AddWithValue("RFC", RFC);
                com.Parameters.AddWithValue("Email", Email);
                com.Parameters.AddWithValue("Email2", Email2);
                com.Parameters.AddWithValue("Email3", Email3);
                com.Parameters.AddWithValue("Banco", Banco);
                com.Parameters.AddWithValue("NumeroCuenta", NumeroCuenta);
                com.Parameters.AddWithValue("Sucursal", Sucursal);
                com.Parameters.AddWithValue("CLABE", CLABE);
                com.Parameters.AddWithValue("NumeroReferencia", NumeroReferencia);
                com.Parameters.AddWithValue("FormaPagoId", FormaPagoId);
                com.Parameters.AddWithValue("IncluyeKit", IncluyeKit);
                com.Parameters.AddWithValue("FechaRegistro", FechaRegistro);
                //SqlParameter parm = new SqlParameter("Id", Id);
                //parm.Direction = ParameterDirection.InputOutput;
                //com.Parameters.Add(parm);

                con.Open();
                com.ExecuteNonQuery();

                return Id;//Convert.ToInt32(com.Parameters["Id"].Value);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public int agregaClientePorEmpresa(int EmpresaId, int TipoPersonaId, string Nombre, string NombreFiscal, string Direccion, string Calle, string NoExterior, string NoInterior, string Colonia,
            string Localidad, string Municipio, string Estado, string CP, string Telefono, string Telefono2, string Celular, string RFC, string Email, string Email2, string Email3, string Banco, string NumeroCuenta,
            string Sucursal, string CLABE, string NumeroReferencia, int FormaPagoId, bool IncluyeKit, DateTime FechaRegistro)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("insAgregaClientePorEmpresa", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("TipoPersonaId", TipoPersonaId);
                com.Parameters.AddWithValue("Nombre", Nombre);
                com.Parameters.AddWithValue("NombreFiscal", NombreFiscal);
                com.Parameters.AddWithValue("Direccion", Direccion);
                com.Parameters.AddWithValue("Calle", Calle);
                com.Parameters.AddWithValue("NoExterior", NoExterior);
                com.Parameters.AddWithValue("NoInterior", NoInterior);
                com.Parameters.AddWithValue("Colonia", Colonia);
                com.Parameters.AddWithValue("Localidad", Localidad);
                com.Parameters.AddWithValue("Municipio", Municipio);
                com.Parameters.AddWithValue("Estado", Estado);
                com.Parameters.AddWithValue("CP", CP);
                com.Parameters.AddWithValue("Telefono", Telefono);
                com.Parameters.AddWithValue("Telefono2", Telefono2);
                com.Parameters.AddWithValue("Celular", Celular);
                com.Parameters.AddWithValue("RFC", RFC);
                com.Parameters.AddWithValue("Email", Email);
                com.Parameters.AddWithValue("Email2", Email2);
                com.Parameters.AddWithValue("Email3", Email3);
                com.Parameters.AddWithValue("Banco", Banco);
                com.Parameters.AddWithValue("NumeroCuenta", NumeroCuenta);
                com.Parameters.AddWithValue("Sucursal", Sucursal);
                com.Parameters.AddWithValue("CLABE", CLABE);
                com.Parameters.AddWithValue("NumeroReferencia", NumeroReferencia);
                com.Parameters.AddWithValue("FormaPagoId", FormaPagoId);
                com.Parameters.AddWithValue("IncluyeKit", IncluyeKit);
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

        public void actualizaCliente(int ClienteId, int EmpresaId, int TipoPersonaId, string Nombre, string NombreFiscal, string Direccion, string Calle, string NoExterior, string NoInterior, string Colonia,
            string Localidad, string Municipio, string Estado, string CP, string Telefono, string Telefono2, string Celular, string RFC, string Email, string Email2, string Email3, string Banco, string NumeroCuenta,
            string Sucursal, string CLABE, string NumeroReferencia, int FormaPagoId, bool IncluyeKit)
        {
            try
            {
                com = new SqlCommand("updActualizaCliente", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ClienteId", ClienteId);
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("TipoPersonaId", TipoPersonaId);
                com.Parameters.AddWithValue("Nombre", Nombre);
                com.Parameters.AddWithValue("NombreFiscal", NombreFiscal);
                com.Parameters.AddWithValue("Direccion", Direccion);
                com.Parameters.AddWithValue("Calle", Calle);
                com.Parameters.AddWithValue("NoExterior", NoExterior);
                com.Parameters.AddWithValue("NoInterior", NoInterior);
                com.Parameters.AddWithValue("Colonia", Colonia);
                com.Parameters.AddWithValue("Localidad", Localidad);
                com.Parameters.AddWithValue("Municipio", Municipio);
                com.Parameters.AddWithValue("Estado", Estado);
                com.Parameters.AddWithValue("CP", CP);
                com.Parameters.AddWithValue("Telefono", Telefono);
                com.Parameters.AddWithValue("Telefono2", Telefono2);
                com.Parameters.AddWithValue("Celular", Celular);
                com.Parameters.AddWithValue("RFC", RFC);
                com.Parameters.AddWithValue("Email", Email);
                com.Parameters.AddWithValue("Email2", Email2);
                com.Parameters.AddWithValue("Email3", Email3);
                com.Parameters.AddWithValue("Banco", Banco);
                com.Parameters.AddWithValue("NumeroCuenta", NumeroCuenta);
                com.Parameters.AddWithValue("Sucursal", Sucursal);
                com.Parameters.AddWithValue("CLABE", CLABE);
                com.Parameters.AddWithValue("NumeroReferencia", NumeroReferencia);
                com.Parameters.AddWithValue("FormaPagoId", FormaPagoId);
                com.Parameters.AddWithValue("IncluyeKit", IncluyeKit);
                con.Open();
                com.ExecuteNonQuery();

                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        
        public void actualizaEstatusCliente(int ClienteId, bool Estatus)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusCliente", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ClienteId", ClienteId);
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
