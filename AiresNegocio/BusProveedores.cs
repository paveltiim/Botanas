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
    public class BusProveedores : BusAbstracta
    {
        public List<EntProveedor> ObtieneProveedores()
        {
            try
            {
                List<EntProveedor> lst = new List<EntProveedor>();
                dt = new DatProveedores().obtieneProveedores();
                foreach (DataRow r in dt.Rows)
                {
                    EntProveedor m = new EntProveedor();
                    m.Id = Convert.ToInt32(r["PRO_ID"]);

                    m.Descripcion = r["PRO_NOMBRE"].ToString();
                    m.Nombre = r["PRO_NOMBRE"].ToString();
                    m.NombreFiscal = r["PRO_NOMBREFISCAL"].ToString();
                    m.Direccion = r["PRO_DIRECCION"].ToString();
                    m.Telefono = r["PRO_TELEFONO"].ToString();
                    m.Telefono2 = r["PRO_TELEFONO2"].ToString();
                    m.Email = r["PRO_EMAIL"].ToString();
                    m.Contacto = r["PRO_CONTACTO"].ToString();
                    m.TelefonoContacto = r["PRO_TELEFONOCONTACTO"].ToString();
                    m.Banco = r["PRO_BANCO"].ToString();
                    m.NumeroCuenta = r["PRO_NUMEROCUENTA"].ToString();
                    m.Sucursal = r["PRO_SUCURSAL"].ToString();
                    m.CLABE = r["PRO_CLABE"].ToString();
                    m.NumeroReferencia = r["PRO_NUMEROREFERENCIA"].ToString();

                    m.Deuda = Convert.ToDecimal(r["GAS_CANTIDAD"]);
                    m.Pago = Convert.ToDecimal(r["PAG_PAGO"]);
                    m.NotasCredito = Convert.ToDecimal(r["NOTASCREDITO"]);

                    m.Fecha = Convert.ToDateTime(r["PRO_FECHAREGISTRO"]);
                    lst.Add(m);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntProveedor> ObtieneProveedores(int EmpresaId)
        {
            try
            {
                List<EntProveedor> lst = new List<EntProveedor>();
                dt = new DatProveedores().obtieneProveedores(EmpresaId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProveedor m = new EntProveedor();
                    m.Id = Convert.ToInt32(r["PRO_ID"]);

                    m.Descripcion = r["PRO_NOMBRE"].ToString();
                    m.Nombre = r["PRO_NOMBRE"].ToString();
                    m.NombreFiscal = r["PRO_NOMBREFISCAL"].ToString();
                    m.Direccion = r["PRO_DIRECCION"].ToString();
                    m.Telefono = r["PRO_TELEFONO"].ToString();
                    m.Telefono2 = r["PRO_TELEFONO2"].ToString();
                    m.Email = r["PRO_EMAIL"].ToString();
                    m.Contacto = r["PRO_CONTACTO"].ToString();
                    m.TelefonoContacto = r["PRO_TELEFONOCONTACTO"].ToString();
                    m.Banco = r["PRO_BANCO"].ToString();
                    m.NumeroCuenta = r["PRO_NUMEROCUENTA"].ToString();
                    m.Sucursal = r["PRO_SUCURSAL"].ToString();
                    m.CLABE = r["PRO_CLABE"].ToString();
                    m.NumeroReferencia = r["PRO_NUMEROREFERENCIA"].ToString();

                    m.Deuda = Convert.ToDecimal(r["GAS_CANTIDAD"]);
                    m.Pago = Convert.ToDecimal(r["PAG_PAGO"]);
                    m.NotasCredito = Convert.ToDecimal(r["NOTASCREDITO"]);

                    //m.Fecha = Convert.ToDateTime(r["PRO_FECHAREGISTRO"]);
                    lst.Add(m);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        ///// <summary>
        ///// Gastos con ESTATUSID=1
        ///// </summary>
        ///// <returns></returns>
        //public List<EntEmpresa> ObtieneEmpresaGastosPorEmpresa(int EmpresaId)
        //{
        //    try
        //    {
        //        List<EntEmpresa> lst = new List<EntEmpresa>();
        //        dt = new DatEmpresas().obtieneEmpresaGastosPorEmpresa(EmpresaId);
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            EntEmpresa m = new EntEmpresa();
        //            m.Id = Convert.ToInt32(r["EMP_ID"]);

        //            m.Descripcion = r["EMP_NOMBRE"].ToString();
        //            m.Nombre = r["EMP_NOMBRE"].ToString();
        //            m.NombreFiscal = r["EMP_NOMBREFISCAL"].ToString();
        //            m.Direccion = r["EMP_DIRECCION"].ToString();
        //            m.Telefono = r["EMP_TELEFONO"].ToString();
        //            m.Telefono2 = r["EMP_TELEFONO2"].ToString();
        //            m.Email = r["EMP_EMAIL"].ToString();
        //            m.Contacto = r["EMP_CONTACTO"].ToString();
        //            m.TelefonoContacto = r["EMP_TELEFONOCONTACTO"].ToString();
        //            m.Banco = r["EMP_BANCO"].ToString();
        //            m.NumeroCuenta = r["EMP_NUMEROCUENTA"].ToString();
        //            m.Sucursal = r["EMP_SUCURSAL"].ToString();
        //            m.CLABE = r["EMP_CLABE"].ToString();
        //            m.NumeroReferencia = r["EMP_NUMEROREFERENCIA"].ToString();

        //            m.GastoId = Convert.ToInt32(r["GAS_ID"]);
        //            m.NumeroFactura = r["GAS_NUMEROFACTURA"].ToString();
        //            m.FechaFactura = Convert.ToDateTime(r["GAS_FECHAFACTURA"]);
        //            m.Deuda = Convert.ToDecimal(r["GAS_CANTIDAD"]);
        //            m.Pago = Convert.ToDecimal(r["PAG_PAGO"]);
        //            m.NotasCredito = Convert.ToDecimal(r["NOTASCREDITO"]);

        //            m.Fecha = Convert.ToDateTime(r["EMP_FECHAREGISTRO"]);
        //            lst.Add(m);
        //        }
        //        return lst;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}
        ///// <summary>
        ///// Gastos con ESTATUSID=1
        ///// </summary>
        ///// <returns></returns>
        //public List<EntEmpresa> ObtieneEmpresasGastosDeuda()
        //{
        //    try
        //    {
        //        List<EntEmpresa> lst = new List<EntEmpresa>();
        //        dt = new DatEmpresas().obtieneEmpresasGastosDeuda();
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            EntEmpresa m = new EntEmpresa();
        //            m.Id = Convert.ToInt32(r["EMP_ID"]);

        //            m.Descripcion = r["EMP_NOMBRE"].ToString();
        //            m.Nombre = r["EMP_NOMBRE"].ToString();
        //            m.NombreFiscal = r["EMP_NOMBREFISCAL"].ToString();
        //            m.Direccion = r["EMP_DIRECCION"].ToString();
        //            m.Telefono = r["EMP_TELEFONO"].ToString();
        //            m.Telefono2 = r["EMP_TELEFONO2"].ToString();
        //            m.Email = r["EMP_EMAIL"].ToString();
        //            m.Contacto = r["EMP_CONTACTO"].ToString();
        //            m.TelefonoContacto = r["EMP_TELEFONOCONTACTO"].ToString();
        //            m.Banco = r["EMP_BANCO"].ToString();
        //            m.NumeroCuenta = r["EMP_NUMEROCUENTA"].ToString();
        //            m.Sucursal = r["EMP_SUCURSAL"].ToString();
        //            m.CLABE = r["EMP_CLABE"].ToString();
        //            m.NumeroReferencia = r["EMP_NUMEROREFERENCIA"].ToString();

        //            m.GastoId = Convert.ToInt32(r["GAS_ID"]);
        //            m.NumeroFactura = r["GAS_NUMEROFACTURA"].ToString();
        //            m.FechaFactura = Convert.ToDateTime(r["GAS_FECHAFACTURA"]);
        //            m.Deuda = Convert.ToDecimal(r["GAS_CANTIDAD"]);
        //            m.Pago = Convert.ToDecimal(r["PAG_PAGO"]);

        //            m.Fecha = Convert.ToDateTime(r["EMP_FECHAREGISTRO"]);
        //            lst.Add(m);
        //        }
        //        return lst;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}
        ///// <summary>
        ///// Gastos con ESTATUSID=1
        ///// </summary>
        ///// <returns></returns>
        //public List<EntEmpresa> ObtieneEmpresasGastosDeuda(DateTime FechaDesde, DateTime FechaHasta)
        //{
        //    try
        //    {
        //        List<EntEmpresa> lst = new List<EntEmpresa>();
        //        dt = new DatEmpresas().obtieneEmpresasGastosDeuda(FechaDesde, FechaHasta);
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            EntEmpresa m = new EntEmpresa();
        //            m.Id = Convert.ToInt32(r["EMP_ID"]);

        //            m.Descripcion = r["EMP_NOMBRE"].ToString();
        //            m.Nombre = r["EMP_NOMBRE"].ToString();
        //            m.NombreFiscal = r["EMP_NOMBREFISCAL"].ToString();
        //            m.Direccion = r["EMP_DIRECCION"].ToString();
        //            m.Telefono = r["EMP_TELEFONO"].ToString();
        //            m.Telefono2 = r["EMP_TELEFONO2"].ToString();
        //            m.Email = r["EMP_EMAIL"].ToString();
        //            m.Contacto = r["EMP_CONTACTO"].ToString();
        //            m.TelefonoContacto = r["EMP_TELEFONOCONTACTO"].ToString();
        //            m.Banco = r["EMP_BANCO"].ToString();
        //            m.NumeroCuenta = r["EMP_NUMEROCUENTA"].ToString();
        //            m.Sucursal = r["EMP_SUCURSAL"].ToString();
        //            m.CLABE = r["EMP_CLABE"].ToString();
        //            m.NumeroReferencia = r["EMP_NUMEROREFERENCIA"].ToString();

        //            m.GastoId = Convert.ToInt32(r["GAS_ID"]);
        //            m.NumeroFactura = r["GAS_NUMEROFACTURA"].ToString();
        //            m.FechaFactura = Convert.ToDateTime(r["GAS_FECHAFACTURA"]);
        //            m.Deuda = Convert.ToDecimal(r["GAS_CANTIDAD"]);
        //            m.Pago = Convert.ToDecimal(r["PAG_PAGO"]);
        //            m.NotasCredito = Convert.ToDecimal(r["NOTASCREDITO"]);

        //            m.Fecha = Convert.ToDateTime(r["EMP_FECHAREGISTRO"]);
        //            lst.Add(m);
        //        }
        //        return lst;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}
        ///// <summary>
        ///// Gastos con ESTATUSID=1 o ESTATUSID=2
        ///// </summary>
        ///// <returns></returns>
        //public List<EntEmpresa> ObtieneEmpresasGastos(DateTime FechaDesde, DateTime FechaHasta)
        //{
        //    try
        //    {
        //        List<EntEmpresa> lst = new List<EntEmpresa>();
        //        dt = new DatEmpresas().obtieneEmpresasGastos(FechaDesde, FechaHasta);
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            EntEmpresa m = new EntEmpresa();
        //            m.Id = Convert.ToInt32(r["EMP_ID"]);

        //            m.Descripcion = r["EMP_NOMBRE"].ToString();
        //            m.Nombre = r["EMP_NOMBRE"].ToString();
        //            m.NombreFiscal = r["EMP_NOMBREFISCAL"].ToString();
        //            m.Direccion = r["EMP_DIRECCION"].ToString();
        //            m.Telefono = r["EMP_TELEFONO"].ToString();
        //            m.Telefono2 = r["EMP_TELEFONO2"].ToString();
        //            m.Email = r["EMP_EMAIL"].ToString();
        //            m.Contacto = r["EMP_CONTACTO"].ToString();
        //            m.TelefonoContacto = r["EMP_TELEFONOCONTACTO"].ToString();
        //            m.Banco = r["EMP_BANCO"].ToString();
        //            m.NumeroCuenta = r["EMP_NUMEROCUENTA"].ToString();
        //            m.Sucursal = r["EMP_SUCURSAL"].ToString();
        //            m.CLABE = r["EMP_CLABE"].ToString();
        //            m.NumeroReferencia = r["EMP_NUMEROREFERENCIA"].ToString();

        //            m.GastoId = Convert.ToInt32(r["GAS_ID"]);
        //            m.NumeroFactura = r["GAS_NUMEROFACTURA"].ToString();
        //            m.FechaFactura = Convert.ToDateTime(r["GAS_FECHAFACTURA"]);
        //            m.Deuda = Convert.ToDecimal(r["GAS_CANTIDAD"]);
        //            //m.Pago = Convert.ToDecimal(r["PAG_PAGO"]);

        //            m.Fecha = Convert.ToDateTime(r["EMP_FECHAREGISTRO"]);
        //            lst.Add(m);
        //        }
        //        return lst;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}

        //public List<EntEmpresa> ObtienePagosEmpresas(DateTime FechaDesde, DateTime FechaHasta)
        //{
        //    try
        //    {
        //        List<EntEmpresa> lst = new List<EntEmpresa>();
        //        dt = new DatEmpresas().obtienePagosEmpresas(FechaDesde, FechaHasta);
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            EntEmpresa p = new EntEmpresa();
        //            p.Id = Convert.ToInt32(r["EMP_ID"]);
        //            p.Nombre = r["EMP_NOMBRE"].ToString();

        //            p.Pago = Convert.ToDecimal(r["PAG_PAGO"]);
        //            p.Fecha = Convert.ToDateTime(r["PAG_FECHAPAGO"]);
        //            p.FechaFactura = Convert.ToDateTime(r["GAS_FECHAFACTURA"]);
        //            p.NumeroFactura = r["GAS_NUMEROFACTURA"].ToString();
        //            lst.Add(p);
        //        }
        //        return lst;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}
        //public List<EntEmpresa> ObtieneNotasCreditoEmpresas(DateTime FechaDesde, DateTime FechaHasta)
        //{
        //    try
        //    {
        //        List<EntEmpresa> lst = new List<EntEmpresa>();
        //        dt = new DatEmpresas().obtieneNotasCreditoEmpresas(FechaDesde, FechaHasta);
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            EntEmpresa p = new EntEmpresa();
        //            p.Id = Convert.ToInt32(r["EMP_ID"]);
        //            p.Nombre = r["EMP_NOMBRE"].ToString();

        //            p.Pago = Convert.ToDecimal(r["NOTCRE_CANTIDAD"]);
        //            p.Fecha = Convert.ToDateTime(r["NOTCRE_FECHA"]);
        //            p.FechaFactura = Convert.ToDateTime(r["GAS_FECHAFACTURA"]);
        //            p.NumeroFactura = r["GAS_NUMEROFACTURA"].ToString();
        //            lst.Add(p);
        //        }
        //        return lst;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}

        /// <summary>
        /// Agrega una Empresa.
        /// </summary>
        /// <param name="empresa">
        /// Propiedades Necesarias: Nombre, NombreFiscal, Direccion, Telefono, Telefono2, Email, Contacto, TelefonoContacto, Banco, NumeroCuenta, Sucursal, CLABE, NumeroReferencia, Fecha.
        /// </param>
        public int AgregaProveedor(EntProveedor Proveedor)
        {
            try
            {
                return new DatProveedores().agregaProveedores(Proveedor.EmpresaId,Proveedor.Nombre, Proveedor.NombreFiscal, Proveedor.Direccion, Proveedor.Telefono, Proveedor.Telefono2, Proveedor.Email, Proveedor.Contacto, Proveedor.TelefonoContacto, Proveedor.Banco, Proveedor.NumeroCuenta, Proveedor.Sucursal, Proveedor.CLABE, Proveedor.NumeroReferencia, Proveedor.Fecha);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        ///// <summary>
        ///// Agrega una Nota de Credito.
        ///// </summary>
        ///// <param name="empresa">
        ///// Propiedades Necesarias: GastoId, Cantidad, Fecha.
        ///// </param>
        //public int AgregaNotaCredito(EntPago NotaCredito)
        //{
        //    try
        //    {
        //        return new DatEmpresas().agregaNotaCredito(NotaCredito.GastoId, NotaCredito.Cantidad, NotaCredito.Fecha);
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}
        /// <summary>
        /// Agrega una Empresa.
        /// </summary>
        /// <param name="empresa">
        /// Propiedades Necesarias: Id, Nombre, NombreFiscal, Direccion, Telefono, Telefono2, Email, Contacto, TelefonoContacto, Banco, NumeroCuenta, Sucursal, CLABE, NumeroReferencia.
        /// </param>
        public int ActualizaProveedor(EntProveedor Proveedor)
        {
            try
            {
                return new DatProveedores().actualizaProveedor(Proveedor.Id, Proveedor.EmpresaId, Proveedor.Nombre, Proveedor.NombreFiscal, Proveedor.Direccion, Proveedor.Telefono, Proveedor.Telefono2, Proveedor.Email, Proveedor.Contacto, Proveedor.TelefonoContacto, Proveedor.Banco, Proveedor.NumeroCuenta, Proveedor.Sucursal, Proveedor.CLABE, Proveedor.NumeroReferencia);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Actualiza el Estatus de la Empresa solicitada.
        /// </summary>
        /// <param name="Proveedor">
        /// Propiedades Necesarias: Id, Estatus.
        /// </param>
        public int ActualizaEstatusProveedor(EntProveedor Proveedor)
        {
            try
            {
                return new DatProveedores().actualizaEstatusProveedor(Proveedor.Id, Proveedor.Estatus);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
