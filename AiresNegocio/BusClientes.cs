using AiresDatos;
using AiresEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresNegocio
{
    public class BusClientes : BusAbstracta
    {
        public List<EntCliente> ObtieneClientes()
        {
            try
            {
                List<EntCliente> lst = new List<EntCliente>();
                dt = new DatClientes().obtieneClientes();
                foreach (DataRow r in dt.Rows)
                {
                    EntCliente c = new EntCliente();
                    c.Id = Convert.ToInt32(r["CLI_ID"]);
                    c.Nombre = r["CLI_NOMBRE"].ToString();
                    c.NombreFiscal = r["CLI_NOMBREFISCAL"].ToString();
                    c.Direccion= r["CLI_DIRECCION"].ToString();
                    c.Calle = r["CLI_CALLE"].ToString();
                    c.NoExterior= r["CLI_NOEXTERIOR"].ToString();
                    c.NoInterior= r["CLI_NOINTERIOR"].ToString();
                    c.Colonia = r["CLI_COLONIA"].ToString();
                    c.Localidad= r["CLI_LOCALIDAD"].ToString();
                    c.Municipio= r["CLI_MUNICIPIO"].ToString();
                    c.Estado= r["CLI_ESTADO"].ToString();
                    c.CP= r["CLI_CP"].ToString();
                    c.Telefono = r["CLI_TELEFONO"].ToString();
                    c.Telefono2 = r["CLI_TELEFONO2"].ToString();
                    c.Celular = r["CLI_CELULAR"].ToString();
                    c.RFC = r["CLI_RFC"].ToString();
                    c.Email = r["CLI_EMAIL"].ToString();
                    c.Email2 = r["CLI_EMAIL2"].ToString();
                    c.Email3 = r["CLI_EMAIL3"].ToString();
                    c.Banco = r["CLI_BANCO"].ToString();
                    c.NumeroCuenta = r["CLI_NUMEROCUENTA"].ToString();
                    c.Sucursal = r["CLI_SUCURSAL"].ToString();
                    c.CLABE = r["CLI_CLABE"].ToString();
                    c.NumeroReferencia = r["CLI_NUMEROREFERENCIA"].ToString();
                    c.IncluyeKit =Convert.ToBoolean( r["CLI_INCLUYEKIT"]);
                    c.Fecha = Convert.ToDateTime(r["CLI_FECHAREGISTRO"]);
                    c.Estatus = Convert.ToBoolean(r["CLI_ESTATUS"]);
                    lst.Add(c);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntCliente> ObtieneClientes(int EmpresaId)
        {
            try
            {
                List<EntCliente> lst = new List<EntCliente>();
                dt = new DatClientes().obtieneClientes(EmpresaId);
                foreach (DataRow r in dt.Rows)
                {
                    EntCliente c = new EntCliente();
                    c.Id = Convert.ToInt32(r["CLI_ID"]);
                    c.Nombre = r["CLI_NOMBRE"].ToString();
                    c.NombreFiscal = r["CLI_NOMBREFISCAL"].ToString();
                    c.Direccion = r["CLI_DIRECCION"].ToString();
                    c.Calle = r["CLI_CALLE"].ToString();
                    c.NoExterior = r["CLI_NOEXTERIOR"].ToString();
                    c.NoInterior = r["CLI_NOINTERIOR"].ToString();
                    c.Colonia = r["CLI_COLONIA"].ToString();
                    c.Localidad = r["CLI_LOCALIDAD"].ToString();
                    c.Municipio = r["CLI_MUNICIPIO"].ToString();
                    c.Estado = r["CLI_ESTADO"].ToString();
                    c.CP = r["CLI_CP"].ToString();
                    c.Telefono = r["CLI_TELEFONO"].ToString();
                    c.Telefono2 = r["CLI_TELEFONO2"].ToString();
                    c.Celular = r["CLI_CELULAR"].ToString();
                    c.RFC = r["CLI_RFC"].ToString();
                    c.Email = r["CLI_EMAIL"].ToString();
                    c.Email2 = r["CLI_EMAIL2"].ToString();
                    c.Email3 = r["CLI_EMAIL3"].ToString();
                    c.Banco = r["CLI_BANCO"].ToString();
                    c.NumeroCuenta = r["CLI_NUMEROCUENTA"].ToString();
                    c.Sucursal = r["CLI_SUCURSAL"].ToString();
                    c.CLABE = r["CLI_CLABE"].ToString();
                    c.NumeroReferencia = r["CLI_NUMEROREFERENCIA"].ToString();
                    c.FormaPagoId = Convert.ToInt32(r["CLI_FORMAPAGOID"]);
                    c.IncluyeKit = Convert.ToBoolean(r["CLI_INCLUYEKIT"]);

                    c.EmpresaId = Convert.ToInt32(r["CLI_EMPRESAASOCIADAID"]);

                    c.Fecha = Convert.ToDateTime(r["CLI_FECHAREGISTRO"]);
                    c.Estatus = Convert.ToBoolean(r["CLI_ESTATUS"]);
                    lst.Add(c);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public EntCliente ObtieneCliente(int ClienteId)
        {
            try
            {
                EntCliente c = new EntCliente();
                dt = new DatClientes().obtieneCliente(ClienteId);
                foreach (DataRow r in dt.Rows)
                {
                    c.Id = Convert.ToInt32(r["CLI_ID"]);
                    c.Nombre = r["CLI_NOMBRE"].ToString();
                    c.NombreFiscal = r["CLI_NOMBREFISCAL"].ToString();
                    c.Direccion = r["CLI_DIRECCION"].ToString();
                    c.Calle = r["CLI_CALLE"].ToString();
                    c.NoExterior = r["CLI_NOEXTERIOR"].ToString();
                    c.NoInterior = r["CLI_NOINTERIOR"].ToString();
                    c.Colonia = r["CLI_COLONIA"].ToString();
                    c.Localidad = r["CLI_LOCALIDAD"].ToString();
                    c.Municipio = r["CLI_MUNICIPIO"].ToString();
                    c.Estado = r["CLI_ESTADO"].ToString();
                    c.CP = r["CLI_CP"].ToString();
                    c.Telefono = r["CLI_TELEFONO"].ToString();
                    c.Telefono2 = r["CLI_TELEFONO2"].ToString();
                    c.Celular = r["CLI_CELULAR"].ToString();
                    c.RFC = r["CLI_RFC"].ToString();
                    c.Email = r["CLI_EMAIL"].ToString();

                    c.Email2 = r["CLI_EMAIL2"].ToString();
                    c.Email3 = r["CLI_EMAIL3"].ToString();

                    c.Banco = r["CLI_BANCO"].ToString();
                    c.NumeroCuenta = r["CLI_NUMEROCUENTA"].ToString();
                    c.Sucursal = r["CLI_SUCURSAL"].ToString();
                    c.CLABE = r["CLI_CLABE"].ToString();
                    c.NumeroReferencia = r["CLI_NUMEROREFERENCIA"].ToString();
                    c.Fecha = Convert.ToDateTime(r["CLI_FECHAREGISTRO"]);
                    c.Estatus = Convert.ToBoolean(r["CLI_ESTATUS"]);
                }
                return c;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntCliente> ObtieneClientesCredito()
        {
            try
            {
                List<EntCliente> lst = new List<EntCliente>();
                dt = new DatClientes().obtieneClientesCredito();
                foreach (DataRow r in dt.Rows)
                {
                    EntCliente c = new EntCliente();
                    c.Id = Convert.ToInt32(r["CLI_ID"]);
                    c.Nombre = r["CLI_NOMBRE"].ToString();
                    c.NombreFiscal = r["CLI_NOMBREFISCAL"].ToString();
                    c.RFC = r["CLI_RFC"].ToString();
                    c.Telefono = r["CLI_TELEFONO"].ToString();
                    c.Celular = r["CLI_CELULAR"].ToString();
                    c.Email = r["CLI_EMAIL"].ToString();
                    c.Total = Convert.ToDecimal(r["TOTAL"]);
                    c.Pago = Convert.ToDecimal(r["PED_PAGO"]);
                    //c.NotasCredito = Convert.ToDecimal(r["NOTASCREDITO"]);
                    c.Debe = Convert.ToDecimal(r["DEBE"]);
                    lst.Add(c);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Agrega nuevo Cliente.
        /// </summary>
        /// <param name="cliente">
        /// Propiedades Necesarias:Nombre, NombreFiscal, Direccion, Calle, NoExterior, NoInterior, Colonia, Localidad, Municipio, Estado, CP, Telefono, Telefono2, Celular, RFC, Email, Banco, NumeroCuenta, Sucursal, CLABE, NumeroReferencia, Fecha.
        /// </param>
        public int AgregaCliente(EntCliente cliente)
        {
            try
            {
                //return new DatClientes().agregaCliente(cliente.EmpresaId, cliente.TipoPersonaId, cliente.Nombre, cliente.NombreFiscal, cliente.Direccion, cliente.Calle, cliente.NoExterior, cliente.NoInterior, cliente.Colonia, cliente.Localidad, cliente.Municipio, cliente.Estado, cliente.CP, cliente.Telefono, cliente.Telefono2, cliente.Celular, cliente.RFC, cliente.Email, cliente.Email2, cliente.Email3, cliente.Banco, cliente.NumeroCuenta, cliente.Sucursal, cliente.CLABE, cliente.NumeroReferencia, cliente.FormaPagoId, cliente.IncluyeKit, cliente.Fecha);
                return new DatClientes().agregaClientePorEmpresa(cliente.EmpresaId, cliente.TipoPersonaId, cliente.Nombre, cliente.NombreFiscal, cliente.Direccion, cliente.Calle, cliente.NoExterior, cliente.NoInterior, cliente.Colonia, cliente.Localidad, cliente.Municipio, cliente.Estado, cliente.CP, cliente.Telefono, cliente.Telefono2, cliente.Celular, cliente.RFC, cliente.Email, cliente.Email2, cliente.Email3, cliente.Banco, cliente.NumeroCuenta, cliente.Sucursal, cliente.CLABE, cliente.NumeroReferencia, cliente.FormaPagoId, cliente.IncluyeKit, cliente.Fecha);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public int AgregaCliente(int ClienteId, EntCliente cliente)
        {
            try
            {
                //return new DatClientes().agregaCliente(cliente.EmpresaId, cliente.TipoPersonaId, cliente.Nombre, cliente.NombreFiscal, cliente.Direccion, cliente.Calle, cliente.NoExterior, cliente.NoInterior, cliente.Colonia, cliente.Localidad, cliente.Municipio, cliente.Estado, cliente.CP, cliente.Telefono, cliente.Telefono2, cliente.Celular, cliente.RFC, cliente.Email, cliente.Email2, cliente.Email3, cliente.Banco, cliente.NumeroCuenta, cliente.Sucursal, cliente.CLABE, cliente.NumeroReferencia, cliente.FormaPagoId, cliente.IncluyeKit, cliente.Fecha);
                return new DatClientes().agregaCliente(ClienteId,cliente.EmpresaId, cliente.TipoPersonaId, cliente.Nombre, cliente.NombreFiscal, cliente.Direccion, cliente.Calle, cliente.NoExterior, cliente.NoInterior, cliente.Colonia, cliente.Localidad, cliente.Municipio, cliente.Estado, cliente.CP, cliente.Telefono, cliente.Telefono2, cliente.Celular, cliente.RFC, cliente.Email, cliente.Email2, cliente.Email3, cliente.Banco, cliente.NumeroCuenta, cliente.Sucursal, cliente.CLABE, cliente.NumeroReferencia, cliente.FormaPagoId, cliente.IncluyeKit, cliente.Fecha);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Actualiza nuevo Cliente.
        /// </summary>
        /// <param name="cliente">
        /// Propiedades Necesarias:Id, Nombre, NombreFiscal, Direccion, Calle, NoExterior, NoInterior, Colonia, Localidad, Municipio, Estado, CP, Telefono, Telefono2, Celular, RFC, Email, Banco, NumeroCuenta, Sucursal, CLABE, NumeroReferencia, Fecha.
        /// </param>
        public void ActualizaCliente(EntCliente cliente)
        {
            try
            {
                new DatClientes().actualizaCliente(cliente.Id, cliente.EmpresaId,cliente.TipoPersonaId, cliente.Nombre, cliente.NombreFiscal, cliente.Direccion, cliente.Calle, cliente.NoExterior, cliente.NoInterior, cliente.Colonia, cliente.Localidad, cliente.Municipio, cliente.Estado, cliente.CP, cliente.Telefono, cliente.Telefono2, cliente.Celular, cliente.RFC, cliente.Email, cliente.Email2, cliente.Email3, cliente.Banco, cliente.NumeroCuenta, cliente.Sucursal, cliente.CLABE, cliente.NumeroReferencia, cliente.FormaPagoId, cliente.IncluyeKit);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Actualiza el Estatus del Cliente solicitado.
        /// </summary>
        /// <param name="Cliente">
        /// Propiedades Necesarias: Id, Estatus.
        /// </param>
        public void ActualizaEstatusCliente(EntCliente Cliente)
        {
            try
            {
                new DatClientes().actualizaEstatusCliente(Cliente.Id, Cliente.Estatus);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
