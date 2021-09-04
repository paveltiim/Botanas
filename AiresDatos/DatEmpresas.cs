using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresDatos
{
    public class DatEmpresas : DatAbstracta
    {
        public DataTable obtieneEmpresas()
        {
            try
            {
                com = new SqlCommand("selObtieneEmpresas", con);
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

        public int agregaEmpresa(int TipoPersonaId, string Nombre, string NombreFiscal, string RegimenFiscal, string Direccion, string Calle, string NoExterior, string NoInterior, string Colonia,
            string Localidad, string Municipio, string Estado, string CP, string Telefono, string Telefono2, string RFC, string Email, string Banco, string NumeroCuenta,
            string Sucursal, string CLABE, string NumeroReferencia, string Certificado, string Key, string Clave, int TipoTasaIVAId, string NoCertificado)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("insAgregaEmpresa", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("TipoPersonaId", TipoPersonaId);
                com.Parameters.AddWithValue("Nombre", Nombre);
                com.Parameters.AddWithValue("NombreFiscal", NombreFiscal);
                com.Parameters.AddWithValue("RegimenFiscal", RegimenFiscal);
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
                com.Parameters.AddWithValue("RFC", RFC);
                com.Parameters.AddWithValue("Email", Email);
                //com.Parameters.AddWithValue("Contacto", Contacto);
                //com.Parameters.AddWithValue("TelefonoContacto", TelefonoContacto);
                com.Parameters.AddWithValue("Banco", Banco);
                com.Parameters.AddWithValue("NumeroCuenta", NumeroCuenta);
                com.Parameters.AddWithValue("Sucursal", Sucursal);
                com.Parameters.AddWithValue("CLABE", CLABE);
                com.Parameters.AddWithValue("NumeroReferencia", NumeroReferencia);
                com.Parameters.AddWithValue("CertificadoRuta", Certificado);
                com.Parameters.AddWithValue("KeyRuta", Key);
                com.Parameters.AddWithValue("Clave", Clave);
                com.Parameters.AddWithValue("TipoTasaIVAId", TipoTasaIVAId);
                com.Parameters.AddWithValue("NoCertificado", NoCertificado);
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

        public int actualizaEmpresa(int EmpresaId, int TipoPersonaId, string Nombre, string NombreFiscal, string RegimenFiscal, string Direccion, string Calle, string NoExterior, string NoInterior, string Colonia,
            string Localidad, string Municipio, string Estado, string CP, string Telefono, string Telefono2, string RFC, string Email, string Banco, string NumeroCuenta,
            string Sucursal, string CLABE, string NumeroReferencia, string Certificado, string Key, string Clave,
            int TipoTasaIVAId, string NoCertificado, int RegimenFiscalId, int TipoFactorId, decimal TasaOCuota, int UsoCFDIId)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("updActualizaEmpresa", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("TipoPersonaId", TipoPersonaId);
                com.Parameters.AddWithValue("Nombre", Nombre);
                com.Parameters.AddWithValue("NombreFiscal", NombreFiscal);

                com.Parameters.AddWithValue("RegimenFiscalId", RegimenFiscalId);

                com.Parameters.AddWithValue("RegimenFiscal", RegimenFiscal);
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
                com.Parameters.AddWithValue("RFC", RFC);
                com.Parameters.AddWithValue("Email", Email);
                //com.Parameters.AddWithValue("Contacto", Contacto);
                //com.Parameters.AddWithValue("TelefonoContacto", TelefonoContacto);
                com.Parameters.AddWithValue("Banco", Banco);
                com.Parameters.AddWithValue("NumeroCuenta", NumeroCuenta);
                com.Parameters.AddWithValue("Sucursal", Sucursal);
                com.Parameters.AddWithValue("CLABE", CLABE);
                com.Parameters.AddWithValue("NumeroReferencia", NumeroReferencia);
                com.Parameters.AddWithValue("CertificadoRuta", Certificado);
                com.Parameters.AddWithValue("KeyRuta", Key);
                com.Parameters.AddWithValue("Clave", Clave);
                com.Parameters.AddWithValue("TipoTasaIVAId", TipoTasaIVAId);
                com.Parameters.AddWithValue("NoCertificado", NoCertificado);

                com.Parameters.AddWithValue("TipoFactorId", TipoFactorId);
                com.Parameters.AddWithValue("TasaOCuota", TasaOCuota);
                com.Parameters.AddWithValue("UsoCFDIId", UsoCFDIId);
                con.Open();
                com.ExecuteNonQuery();

                return EmpresaId;
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        
        public int actualizaEstatusEmpresa(int EmpresaId, bool Estatus)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusEmpresa", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("Estatus", Estatus);
                con.Open();
                com.ExecuteNonQuery();

                return EmpresaId;
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public int aumentaTimbresEmpresa(int EmpresaId)
        {
            try
            {
                com = new SqlCommand("AumentaTimbreEmpresa", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                con.Open();
                com.ExecuteNonQuery();

                return EmpresaId;
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public DataTable obtieneProductosServicios()
        {
            try
            {
                com = new SqlCommand("selProductoServicio", con);
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
        public DataTable obtieneUnidades()
        {
            try
            {
                com = new SqlCommand("selUnidades", con);
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

        public DataTable obtieneCatalogoRegimen()
        {
            try
            {
                com = new SqlCommand("selCatalogoRegimenFiscal", con);
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
        public DataTable obtieneCatalogoUsoCFDI()
        {
            try
            {
                com = new SqlCommand("selCatalogoUsoCFDI", con);
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

        public DataTable obtieneEstablecimientos(int EmpresaId)
        {
            try
            {
                com = new SqlCommand("spGetEstablecimiento", con);
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
        public DataTable obtieneAlmacenes(int EmpresaId, int UsuarioId)
        {
            try
            {
                com = new SqlCommand("spGetUsuarioAlmacenes", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("UsuarioId", UsuarioId);
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
