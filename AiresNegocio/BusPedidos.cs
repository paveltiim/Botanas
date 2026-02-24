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
    public class BusPedidos : BusAbstracta
    {

        public List<EntPedido> ObtieneReporteComprasVentas(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtieneReporteComprasVentas(EmpresaId, FechaDesde, FechaHasta);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PRODET_ID"]);
                    p.FechaEntrega = Convert.ToDateTime(r["FECHACOMPRA"]);
                    p.Descripcion = r["FACTURACOMPRA"].ToString();
                    if (p.Descripcion.ToUpper().Contains("FAC"))
                    {
                        int index = p.Descripcion.IndexOf("FAC");
                        string fac = p.Descripcion.Substring(index);
                        p.Descripcion = fac;
                    }

                    p.FormaPagoId = Convert.ToInt32(r["PRO_ID"]);
                    p.Detalle = r["PRO_CODIGO"].ToString() + " - " + r["PRO_DESCRIPCION"].ToString();

                    p.UUID = r["PRO_MARCA"].ToString();
                    p.Observaciones = r["PRO_MODELO"].ToString();

                    p.RutaFactura = r["PRODET_SERIE"].ToString();

                    p.SubTotal = Convert.ToDecimal(r["PRODET_PRECIOCOSTO"]);
                    p.Total = Convert.ToDecimal(r["PROPED_PRECIOVENTA"]);

                    p.ClienteId = Convert.ToInt32(r["CLI_ID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString()+ " - "+r["CLI_RFC"].ToString();

                    p.FacturaId = Convert.ToInt32(r["FAC_ID"]);
                    p.Fecha = Convert.ToDateTime(r["FECHAVENTA"]); 
                    p.Factura = r["FACTURAVENTA"].ToString();

                    p.Solicitud = r["PED_SOLICITUD"].ToString();

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public EntPedido ObtienePedido(int PedidoId)
        {
            try
            {
                EntPedido p = new EntPedido();
                dt = new DatPedidos().obtienePedido(PedidoId);
                foreach (DataRow r in dt.Rows)
                {
                    p.Id = Convert.ToInt32(r["PED_ID"]);
                    p.TipoPedidoId = Convert.ToInt32(r["PED_TIPOPEDIDOID"]);
                    p.Sucursal = "";
                    p.Detalle = r["PED_DETALLE"].ToString();
                    p.Observaciones = r["PED_OBSERVACIONES"].ToString();
                    p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();
                    p.ClienteNombreFiscal = r["CLI_NOMBREFISCAL"].ToString();
                    p.ClienteRFC = r["CLI_RFC"].ToString();

                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.Pago = Convert.ToDecimal(r["PED_PAGO"]);
                    p.NotasCredito = Convert.ToDecimal(r["PED_NOTASCREDITO"]);

                    p.IVA = Convert.ToDecimal(r["PED_IVA"]);
                    p.IEPS = Convert.ToDecimal(r["PED_IEPS"]);
                    p.IVARetencion = Convert.ToDecimal(r["PED_IVARETENIDO"]);
                    p.ISRRetencion = Convert.ToDecimal(r["PED_ISRRETENIDO"]);
                    p.SubTotal = p.Total + p.IVARetencion + p.ISRRetencion - p.IVA - p.IEPS;

                    p.FechaCorta = Convert.ToDateTime(r["PED_FECHA"]).ToShortDateString();
                    p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    p.FacturaId = Convert.ToInt32(r["FAC_ID"]);
                    p.Facturado = Convert.ToBoolean(r["FAC_ESTATUSID"]);
                    p.Factura = "";
                    if (p.Facturado)
                    {
                        p.EstatusDescripcion = "VIGENTE / " + p.EstatusDescripcion;
                        p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                        p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                        p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                        p.UUID = r["FAC_UUID"].ToString();
                        p.RutaFactura = r["FAC_RUTA"].ToString();
                        p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                    }
                    else if (Convert.ToBoolean(r["PED_FACTURADO"]))
                    {
                        p.Facturado = true;
                        p.EstatusDescripcion = "CANCELADO";
                        p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                        p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                        p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                        p.UUID = r["FAC_UUID"].ToString();
                        p.RutaFactura = r["FAC_RUTA"].ToString();
                        p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                    }
                    else
                        p.EstatusDescripcion = "SIN FACTURAR";
                }
                return p;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntPedido> ObtienePedidosPorEstablecimiento(int EstablecimientoId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePedidosPorEstablecimiento(EstablecimientoId, FechaDesde, FechaHasta);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PED_ID"]);
                    p.TipoPedidoId = Convert.ToInt32(r["PED_TIPOPEDIDOID"]);
                    p.TrabajadorId = Convert.ToInt32(r["PED_TRABAJADORID"]);
                    p.Trabajador = r["TRA_NOMBRE"].ToString();
                    p.Sucursal = "";
                    p.Detalle = r["PED_DETALLE"].ToString();
                    p.Observaciones = r["PED_OBSERVACIONES"].ToString();
                    p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();
                    p.ClienteNombreFiscal = r["CLI_NOMBREFISCAL"].ToString();
                    p.ClienteRFC = r["CLI_RFC"].ToString();

                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.Pago = Convert.ToDecimal(r["PED_PAGO"]);
                    p.NotasCredito= Convert.ToDecimal(r["PED_NOTASCREDITO"]);

                    p.IVA = Convert.ToDecimal(r["PED_IVA"]);
                    p.IEPS = Convert.ToDecimal(r["PED_IEPS"]);
                    p.IVARetencion = Convert.ToDecimal(r["PED_IVARETENIDO"]);
                    p.ISRRetencion = Convert.ToDecimal(r["PED_ISRRETENIDO"]);
                    p.SubTotal = p.Total + p.IVARetencion + p.ISRRetencion - p.IVA - p.IEPS;

                    p.FechaCorta = Convert.ToDateTime(r["PED_FECHA"]).ToShortDateString();
                    p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    p.FacturaId = Convert.ToInt32(r["FAC_ID"]);
                    p.Facturado = Convert.ToBoolean(r["FAC_ESTATUSID"]);
                    //p.Facturado = Convert.ToBoolean(r["FACTURADO"]);
                    p.Factura = "";
                    if (p.Facturado)
                    {
                        p.EstatusDescripcion = "VIGENTE / "+ p.EstatusDescripcion;
                        p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                        p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                        p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                        p.UUID = r["FAC_UUID"].ToString();
                        p.RutaFactura = r["FAC_RUTA"].ToString();
                        p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                    }
                    else if (Convert.ToBoolean(r["PED_FACTURADO"]))
                    {
                        p.Facturado = true;
                        p.EstatusDescripcion = "CANCELADO";
                        p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                        p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                        p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                        p.UUID = r["FAC_UUID"].ToString();
                        p.RutaFactura = r["FAC_RUTA"].ToString();
                        p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                    }
                    else
                        p.EstatusDescripcion = "SIN FACTURAR";

                    //if (!(r["FAC_PDF"] is DBNull))
                    //{
                    //    p.PDF = (byte[])r["FAC_PDF"];
                    //    p.XML = (byte[])r["FAC_XML"];
                    //}
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntPedido> ObtienePedidosPorEstablecimiento(int EstablecimientoId, DateTime FechaDesde, DateTime FechaHasta,
                                                                int TipoPedidoId)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePedidosPorEstablecimiento(EstablecimientoId, FechaDesde, FechaHasta, TipoPedidoId);
                foreach (DataRow r in dt.Rows)
                {

                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PED_ID"]);
                    p.TipoPedidoId = Convert.ToInt32(r["PED_TIPOPEDIDOID"]);
                    p.TipoPedido = r["TIPPED_DESCRIPCION"].ToString();
                    p.Sucursal = "";
                    p.Detalle = r["PED_DETALLE"].ToString();
                    p.Observaciones = r["PED_OBSERVACIONES"].ToString();
                    p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();
                    p.ClienteNombreFiscal = r["CLI_NOMBREFISCAL"].ToString();
                    p.ClienteRFC = r["CLI_RFC"].ToString();

                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.Pago = Convert.ToDecimal(r["PED_PAGO"]);
                    p.NotasCredito = Convert.ToDecimal(r["PED_NOTASCREDITO"]);

                    p.IVA = Convert.ToDecimal(r["PED_IVA"]);
                    p.IEPS = Convert.ToDecimal(r["PED_IEPS"]);
                    p.IVARetencion = Convert.ToDecimal(r["PED_IVARETENIDO"]);
                    p.ISRRetencion = Convert.ToDecimal(r["PED_ISRRETENIDO"]);
                    p.SubTotal = p.Total + p.IVARetencion + p.ISRRetencion - p.IVA - p.IEPS;

                    p.FechaCorta = Convert.ToDateTime(r["PED_FECHA"]).ToShortDateString();
                    p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    p.FacturaId = Convert.ToInt32(r["FAC_ID"]);
                    p.Facturado = Convert.ToBoolean(r["FAC_ESTATUSID"]);
                    //p.Facturado = Convert.ToBoolean(r["FACTURADO"]);
                    p.Factura = "";
                    if (p.Facturado)
                    {
                        p.EstatusDescripcion = "VIGENTE / " + p.EstatusDescripcion;
                        p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                        p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                        p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                        p.UUID = r["FAC_UUID"].ToString();
                        p.RutaFactura = r["FAC_RUTA"].ToString();
                        p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                    }
                    else if (Convert.ToBoolean(r["PED_FACTURADO"]))
                    {
                        p.Facturado = true;
                        p.EstatusDescripcion = "CANCELADO";
                        p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                        p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                        p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                        p.UUID = r["FAC_UUID"].ToString();
                        p.RutaFactura = r["FAC_RUTA"].ToString();
                        p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                    }
                    else
                        p.EstatusDescripcion = p.TipoPedido+"/SIN FACTURAR";

                    //if (!(r["FAC_PDF"] is DBNull))
                    //{
                    //    p.PDF = (byte[])r["FAC_PDF"];
                    //    p.XML = (byte[])r["FAC_XML"];
                    //}
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntPedido> ObtienePedidosPreVenta(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePedidosPreVenta(EmpresaId ,FechaDesde, FechaHasta);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PEDPRE_ID"]);
                    p.NumOrden = Convert.ToInt32(r["PEDPRE_NUMEROPEDIDO"]).ToString();
                    p.PedidoRelacionadoId = Convert.ToInt32(r["PEDPRE_PEDIDORELACIONID"]);

                    p.TipoPedidoId = Convert.ToInt32(r["PEDPRE_TIPOPEDIDOID"]);
                    p.TipoPedido = r["TIPPED_DESCRIPCION"].ToString();
                    p.Sucursal = "";
                    p.Detalle = r["PEDPRE_DETALLE"].ToString();
                    p.Observaciones = r["PEDPRE_OBSERVACIONES"].ToString();
                    p.ClienteId = Convert.ToInt32(r["PEDPRE_CLIENTEID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();
                    p.ClienteNombreFiscal = r["CLI_NOMBREFISCAL"].ToString();
                    p.ClienteRFC = r["CLI_RFC"].ToString();

                    p.Total = Convert.ToDecimal(r["PEDPRE_TOTAL"]);
                    p.IVA = Convert.ToDecimal(r["PEDPRE_IVA"]);
                    p.IEPS = Convert.ToDecimal(r["PEDPRE_IEPS"]);
                    p.IVARetencion = Convert.ToDecimal(r["PEDPRE_IVARETENIDO"]);
                    p.ISRRetencion = Convert.ToDecimal(r["PEDPRE_ISRRETENIDO"]);
                    p.SubTotal = p.Total + p.IVARetencion + p.ISRRetencion - p.IVA - p.IEPS;

                    p.FechaCorta = Convert.ToDateTime(r["PEDPRE_FECHA"]).ToShortDateString();
                    p.Fecha = Convert.ToDateTime(r["PEDPRE_FECHA"]);

                    p.TrabajadorId = Convert.ToInt32(r["PEDPRE_TRABAJADORID"]);
                    p.Trabajador = r["TRA_NOMBRE"].ToString();

                    p.EstatusId = Convert.ToInt32(r["PEDPRE_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPEDPRE_DESCRIPCION"].ToString();

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<EntPedido> ObtieneCorte(int EstablecimientoId, int UsuarioId)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtieneCorte(EstablecimientoId, UsuarioId);
                foreach (DataRow r in dt.Rows)
                {

                    EntPedido p = new EntPedido();
                    //p.Id = Convert.ToInt32(r["PED_ID"]);
                    //p.Detalle = r["PED_DETALLE"].ToString();
                    //p.Observaciones = r["PED_OBSERVACIONES"].ToString();
                    //p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    //p.Cliente = r["CLI_NOMBRE"].ToString();
                    //p.ClienteNombreFiscal = r["CLI_NOMBREFISCAL"].ToString();
                    //p.ClienteRFC = r["CLI_RFC"].ToString();

                    p.Total = Convert.ToDecimal(r["TOTAL"]);                    
                    p.Descripcion = r["PAG_FORMAPAGO"].ToString();

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<EntPedido> ObtieneCorteMayoreo(int EstablecimientoId, int UsuarioId)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtieneCorteMayoreo(EstablecimientoId, UsuarioId);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Total = Convert.ToDecimal(r["TOTAL"]);
                    p.Descripcion = r["PAG_FORMAPAGO"].ToString();
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntPedido> ObtieneCorteDetalle(int EstablecimientoId, int UsuarioId)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtieneCorteDetalle(EstablecimientoId, UsuarioId);
                foreach (DataRow r in dt.Rows)
                {

                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PED_ID"]);
                       p.Total = Convert.ToDecimal(r["TOTAL"]);
                    p.Descripcion = r["PAG_FORMAPAGO"].ToString();
                    p.Detalle = "ID: " + p.Id .ToString()+" - "+r["PAG_FORMAPAGO"].ToString();

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntPedido> ObtieneCorteDetalleMayoreo(int EstablecimientoId, int UsuarioId)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtieneCorteDetalleMayoreo(EstablecimientoId, UsuarioId);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PED_ID"]);
                    p.Total = Convert.ToDecimal(r["TOTAL"]);
                    p.Descripcion = r["PAG_FORMAPAGO"].ToString();
                    p.Detalle = "ID: " + p.Id.ToString() + " - " + r["PAG_FORMAPAGO"].ToString();
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntPedido> AgregaCorte(int EstablecimientoId, int UsuarioId)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().agregaCorte(EstablecimientoId, UsuarioId);
                foreach (DataRow r in dt.Rows)
                {

                    EntPedido p = new EntPedido();
                    //p.Id = Convert.ToInt32(r["PED_ID"]);
                    //p.Detalle = r["PED_DETALLE"].ToString();
                    //p.Observaciones = r["PED_OBSERVACIONES"].ToString();
                    //p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    //p.Cliente = r["CLI_NOMBRE"].ToString();
                    //p.ClienteNombreFiscal = r["CLI_NOMBREFISCAL"].ToString();
                    //p.ClienteRFC = r["CLI_RFC"].ToString();

                    p.Total = Convert.ToDecimal(r["TOTAL"]);
                    p.Descripcion = r["PAG_FORMAPAGO"].ToString();

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntPedido> AgregaCorteMayoreo(int EstablecimientoId, int UsuarioId)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().agregaCorteMayoreo(EstablecimientoId, UsuarioId);
                foreach (DataRow r in dt.Rows)
                {

                    EntPedido p = new EntPedido();
                    p.Total = Convert.ToDecimal(r["TOTAL"]);
                    p.Descripcion = r["PAG_FORMAPAGO"].ToString();

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<EntPedido> ObtienePedidosClientesDeuda()
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePedidosClientesCredito();
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PED_ID"]);
                    p.Detalle = r["PED_DETALLE"].ToString();

                    p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();
                    p.ClienteRFC = r["CLI_RFC"].ToString();

                    p.IEPS = Convert.ToDecimal(r["PED_IEPS"]);
                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.Pago = Convert.ToDecimal(r["PED_PAGO"]);
                    p.NotasCredito = Convert.ToDecimal(r["PED_NOTASCREDITO"]);
                    //p.Debe= Convert.ToDecimal(r["DEBE"]);

                    p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    p.FacturaId = Convert.ToInt32(r["FAC_ID"]);
                    p.Facturado = Convert.ToBoolean(r["FAC_ESTATUSID"]);
                    //p.Facturado = Convert.ToBoolean(r["FACTURADO"]);
                    if (p.Facturado)
                    {
                        p.EstatusDescripcion = "VIGENTE";
                        p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                        p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                        p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                        p.UUID = r["FAC_UUID"].ToString();
                        p.RutaFactura = r["FAC_RUTA"].ToString();
                        p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                    }
                    else if (Convert.ToBoolean(r["PED_FACTURADO"]))
                    {
                        p.Facturado = true;
                        p.EstatusDescripcion = "CANCELADO";
                        p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                        p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                        p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                        p.UUID = r["FAC_UUID"].ToString();
                        p.RutaFactura = r["FAC_RUTA"].ToString();
                        p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                    }
                    else
                        p.EstatusDescripcion = "SIN FACTURAR";
                    p.EmpresaId = Convert.ToInt32(r["PED_EMPRESAID"]);

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntPedido> ObtienePedidosClientesDeuda(string ClienteRFC)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePedidosClientesCredito(ClienteRFC);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PED_ID"]);
                    p.Detalle = r["PED_DETALLE"].ToString();

                    p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();
                    p.ClienteRFC = r["CLI_RFC"].ToString();

                    p.IEPS = Convert.ToDecimal(r["PED_IEPS"]);
                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.Pago = Convert.ToDecimal(r["PED_PAGO"]);
                    p.NotasCredito = Convert.ToDecimal(r["PED_NOTASCREDITO"]);
                    //p.Debe= Convert.ToDecimal(r["DEBE"]);

                    p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    p.FacturaId = Convert.ToInt32(r["FAC_ID"]);
                    p.Facturado = Convert.ToBoolean(r["FAC_ESTATUSID"]);
                    //p.Facturado = Convert.ToBoolean(r["FACTURADO"]);
                    if (p.Facturado)
                    {
                        p.EstatusDescripcion = "VIGENTE";
                        p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                        p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                        p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                        p.UUID = r["FAC_UUID"].ToString();
                        p.RutaFactura = r["FAC_RUTA"].ToString();
                        p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                    }
                    else if (Convert.ToBoolean(r["PED_FACTURADO"]))
                    {
                        p.Facturado = true;
                        p.EstatusDescripcion = "CANCELADO";
                        p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                        p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                        p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                        p.UUID = r["FAC_UUID"].ToString();
                        p.RutaFactura = r["FAC_RUTA"].ToString();
                        p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                    }
                    else
                        p.EstatusDescripcion = "SIN FACTURAR";
                    p.EmpresaId = Convert.ToInt32(r["PED_EMPRESAID"]);

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntPedido> ObtienePedidosPorFactura(int FacturaId)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePedidosPorFactura(FacturaId);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PED_ID"]);
                    p.Detalle = r["PED_DETALLE"].ToString();

                    p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();
                    p.ClienteRFC = r["CLI_RFC"].ToString();

                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.Pago = Convert.ToDecimal(r["PED_PAGO"]);
                    p.NotasCredito = Convert.ToDecimal(r["PED_NOTASCREDITO"]);
                    p.PagoTotal = p.Total - p.Pago - p.NotasCredito;// SE OBTIENE LA DEUDA A PAGAR.
                    //p.Debe= Convert.ToDecimal(r["DEBE"]);

                    p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    p.FacturaId = Convert.ToInt32(r["FAC_ID"]);
                    p.Facturado = Convert.ToBoolean(r["FAC_ESTATUSID"]);
                    //p.Facturado = Convert.ToBoolean(r["FACTURADO"]);
                    if (p.Facturado)
                    {
                        p.EstatusDescripcion = "VIGENTE";
                        p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                        p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                        p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                        p.UUID = r["FAC_UUID"].ToString();
                        p.RutaFactura = r["FAC_RUTA"].ToString();
                        p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                    }
                    else if (Convert.ToBoolean(r["PED_FACTURADO"]))
                    {
                        p.Facturado = true;
                        p.EstatusDescripcion = "CANCELADO";
                        p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                        p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                        p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                        p.UUID = r["FAC_UUID"].ToString();
                        p.RutaFactura = r["FAC_RUTA"].ToString();
                        p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                    }
                    else
                        p.EstatusDescripcion = "SIN FACTURAR";
                    p.EmpresaId = Convert.ToInt32(r["PED_EMPRESAID"]);

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// SOLO SE USA EN CLIENTES CREDITO
        /// </summary>
        /// <param name="EstablecimientoId"></param>
        /// <returns></returns>
        public List<EntPedido> ObtienePedidosClientesDeuda(int EstablecimientoId)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePedidosClientesCredito(EstablecimientoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PED_ID"]);
                    p.Detalle = r["PED_DETALLE"].ToString();

                    p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();
                    p.ClienteRFC = r["CLI_RFC"].ToString();

                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.Pago = Convert.ToDecimal(r["PED_PAGO"]);
                    p.NotasCredito = Convert.ToDecimal(r["PED_NOTASCREDITO"]);
                    p.DebeFacturas = Convert.ToDecimal(r["DEBE_FACTURAS"]);
                    //p.Debe= Convert.ToDecimal(r["DEBE"]);

                    p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    p.FacturaId = Convert.ToInt32(r["FAC_ID"]);
                    //p.Facturado = Convert.ToBoolean(r["FAC_ESTATUSID"]);
                    p.Facturado = Convert.ToBoolean(r["PED_FACTURADO"]);
                    if (p.Facturado)
                    {
                        if (Convert.ToInt32(r["FAC_ESTATUSID"]) > 0)
                        {
                            p.EstatusDescripcion = "VIGENTE";
                            p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                            p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                            p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                            p.UUID = r["FAC_UUID"].ToString();
                            p.RutaFactura = r["FAC_RUTA"].ToString();
                            p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                        }
                        else 
                        {
                            p.Facturado = true;
                            p.EstatusDescripcion = "CANCELADO";
                            p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                            p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                            p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                            p.UUID = r["FAC_UUID"].ToString();
                            p.RutaFactura = r["FAC_RUTA"].ToString();
                            p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                        }
                    }
                    else
                    {
                        p.EstatusDescripcion = "SIN FACTURAR";
                        p.Factura = "VE-" + p.Id.ToString();
                        p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    }
                    p.EmpresaId= Convert.ToInt32(r["PED_EMPRESAID"]);

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<EntPedido> ObtienePedidosClienteFacturaDeuda()
        {
            try
            {
                DateTime fechaToday = DateTime.Today;
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePedidosClienteFacturaCredito();
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PED_ID"]);
                    p.Detalle = r["PED_DETALLE"].ToString();

                    p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();
                    p.ClienteRFC = r["CLI_RFC"].ToString();

                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.Pago = Convert.ToDecimal(r["PED_PAGO"]);
                    p.NotasCredito = Convert.ToDecimal(r["PED_NOTASCREDITO"]);

                    p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    p.FacturaId = Convert.ToInt32(r["FAC_ID"]);
                    p.Facturado = Convert.ToBoolean(r["PED_FACTURADO"]);
                    if (p.Facturado)
                    {
                        if (Convert.ToInt32(r["FAC_ESTATUSID"]) > 0)
                        {
                            p.EstatusDescripcion = "VIGENTE";
                            p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                            p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                            p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                            p.UUID = r["FAC_UUID"].ToString();
                            //p.RutaFactura = r["FAC_RUTA"].ToString();
                            //p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                        }
                        else
                        {
                            p.Facturado = true;
                            p.EstatusDescripcion = "CANCELADO";
                            p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                            p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                            p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                            p.UUID = r["FAC_UUID"].ToString();
                            //p.RutaFactura = r["FAC_RUTA"].ToString();
                            //p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                        }
                    }
                    else
                    {
                        p.EstatusDescripcion = "SIN FACTURAR";
                        p.Factura = "VE-" + p.Id.ToString();
                        p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    }
                    p.EmpresaId = Convert.ToInt32(r["PED_EMPRESAID"]);

                    p.DiasCredito = Convert.ToInt32(r["DIASCREDITO"]);
                    p.FechaEntrega = Convert.ToDateTime(r["FECHA"]);//FECHA REFERENCIA VENCIMIENTO
                    p.FechaPago = Convert.ToDateTime(r["ULTIMAFECHAPAGO"]);
                    p.FechaVencimiento = Convert.ToDateTime(r["FECHAVENCIMIENTO"]);
                    TimeSpan ts = fechaToday - p.FechaVencimiento;
                    p.DiasVencidos = ts.Days;

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntPedido> ObtienePedidosClienteFacturaDeuda(int ClienteId)
        {
            try
            {
                DateTime fechaToday = DateTime.Today;
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePedidosClienteFacturaCredito(ClienteId);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PED_ID"]);
                    p.Detalle = r["PED_DETALLE"].ToString();

                    p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();
                    p.ClienteRFC = r["CLI_RFC"].ToString();

                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.Pago = Convert.ToDecimal(r["PED_PAGO"]);
                    p.NotasCredito = Convert.ToDecimal(r["PED_NOTASCREDITO"]);

                    p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    p.FacturaId = Convert.ToInt32(r["FAC_ID"]);
                    p.Facturado = Convert.ToBoolean(r["PED_FACTURADO"]);
                    if (p.Facturado)
                    {
                        if (Convert.ToInt32(r["FAC_ESTATUSID"]) > 0)
                        {
                            p.EstatusDescripcion = "VIGENTE";
                            p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                            p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                            p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                            p.UUID = r["FAC_UUID"].ToString();
                            //p.RutaFactura = r["FAC_RUTA"].ToString();
                            //p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                        }
                        else
                        {
                            p.Facturado = true;
                            p.EstatusDescripcion = "CANCELADO";
                            p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                            p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                            p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                            p.UUID = r["FAC_UUID"].ToString();
                            //p.RutaFactura = r["FAC_RUTA"].ToString();
                            //p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                        }
                    }
                    else
                    {
                        p.EstatusDescripcion = "SIN FACTURAR";
                        p.Factura = "VE-" + p.Id.ToString();
                        p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    }
                    p.EmpresaId = Convert.ToInt32(r["PED_EMPRESAID"]);

                    p.DiasCredito = Convert.ToInt32(r["DIASCREDITO"]);
                    p.FechaEntrega = Convert.ToDateTime(r["FECHA"]);//FECHA REFERENCIA VENCIMIENTO
                    p.FechaPago = Convert.ToDateTime(r["ULTIMAFECHAPAGO"]);
                    p.FechaVencimiento = Convert.ToDateTime(r["FECHAVENCIMIENTO"]);
                    TimeSpan ts = fechaToday - p.FechaVencimiento;
                    p.DiasVencidos = ts.Days;

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// SOLO SE USA EN REPORTE DIAS VENCIMIENTOS
        /// </summary>
        /// <param name="EstablecimientoId"></param>
        /// <param name="ClienteId"></param>
        /// <returns></returns>
        public List<EntPedido> ObtienePedidosClientesDeudaDiasCredito(int EstablecimientoId)
        {
            try
            {
                DateTime fechaToday = DateTime.Today;
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePedidosClientesCreditoDiasCredito(EstablecimientoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PED_ID"]);
                    p.Detalle = r["PED_DETALLE"].ToString();

                    p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();
                    p.ClienteRFC = r["CLI_RFC"].ToString();

                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.Pago = Convert.ToDecimal(r["PED_PAGO"]);
                    p.NotasCredito = Convert.ToDecimal(r["PED_NOTASCREDITO"]);

                    p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    p.FacturaId = Convert.ToInt32(r["FAC_ID"]);
                    p.Facturado = Convert.ToBoolean(r["PED_FACTURADO"]);
                    if (p.Facturado)
                    {
                        if (Convert.ToInt32(r["FAC_ESTATUSID"]) > 0)
                        {
                            p.EstatusDescripcion = "VIGENTE";
                            p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                            p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                            p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                            p.UUID = r["FAC_UUID"].ToString();
                            //p.RutaFactura = r["FAC_RUTA"].ToString();
                            //p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                        }
                        else
                        {
                            p.Facturado = true;
                            p.EstatusDescripcion = "CANCELADO";
                            p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                            p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                            p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                            p.UUID = r["FAC_UUID"].ToString();
                            //p.RutaFactura = r["FAC_RUTA"].ToString();
                            //p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                        }
                    }
                    else
                    {
                        p.EstatusDescripcion = "SIN FACTURAR";
                        p.Factura = "VE-" + p.Id.ToString();
                        p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    }
                    p.EmpresaId = Convert.ToInt32(r["PED_EMPRESAID"]);

                    p.DiasCredito = Convert.ToInt32(r["DIASCREDITO"]);
                    p.FechaEntrega = Convert.ToDateTime(r["FECHA"]);//FECHA REFERENCIA VENCIMIENTO
                    p.FechaPago = Convert.ToDateTime(r["ULTIMAFECHAPAGO"]);
                    p.FechaVencimiento = Convert.ToDateTime(r["FECHAVENCIMIENTO"]);
                    TimeSpan ts = fechaToday - p.FechaVencimiento;
                    p.DiasVencidos = ts.Days;

                    lst.Add(p);
                }

                List<EntPedido> lstClienteFactura = this.ObtienePedidosClienteFacturaDeuda();
                lst.AddRange(lstClienteFactura);
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// SOLO SE USA EN ESTADO DE CUENTA
        /// </summary>
        /// <param name="EstablecimientoId"></param>
        /// <param name="ClienteId"></param>
        /// <returns></returns>
        public List<EntPedido> ObtienePedidosClientesDeuda(int EstablecimientoId, int ClienteId)
        {
            try
            {
                DateTime fechaToday = DateTime.Today;
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePedidosClientesCredito(EstablecimientoId, ClienteId);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PED_ID"]);
                    p.Detalle = r["PED_DETALLE"].ToString();

                    p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();
                    p.ClienteRFC = r["CLI_RFC"].ToString();

                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.Pago = Convert.ToDecimal(r["PED_PAGO"]);
                    p.NotasCredito = Convert.ToDecimal(r["PED_NOTASCREDITO"]);

                    p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    p.FacturaId = Convert.ToInt32(r["FAC_ID"]);
                    p.Facturado = Convert.ToBoolean(r["PED_FACTURADO"]);
                    if (p.Facturado)
                    {
                        if (Convert.ToInt32(r["FAC_ESTATUSID"]) > 0)
                        {
                            p.EstatusDescripcion = "VIGENTE";
                            p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                            p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                            p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                            p.UUID = r["FAC_UUID"].ToString();
                            //p.RutaFactura = r["FAC_RUTA"].ToString();
                            //p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                        }
                        else
                        {
                            p.Facturado = true;
                            p.EstatusDescripcion = "CANCELADO";
                            p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                            p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                            p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                            p.UUID = r["FAC_UUID"].ToString();
                            //p.RutaFactura = r["FAC_RUTA"].ToString();
                            //p.VersionCFDI = r["FAC_VERSIONCFDI"].ToString();
                        }
                    }
                    else
                    {
                        p.EstatusDescripcion = "SIN FACTURAR";
                        p.Factura = "VE-" + p.Id.ToString();
                        p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    }
                    p.EmpresaId = Convert.ToInt32(r["PED_EMPRESAID"]);

                    p.DiasCredito = Convert.ToInt32(r["DIASCREDITO"]);
                    p.FechaEntrega = Convert.ToDateTime(r["FECHA"]);//FECHA REFERENCIA VENCIMIENTO
                    p.FechaPago = Convert.ToDateTime(r["ULTIMAFECHAPAGO"]);
                    p.FechaVencimiento = Convert.ToDateTime(r["FECHAVENCIMIENTO"]);
                    TimeSpan ts = fechaToday - p.FechaVencimiento;
                    p.DiasVencidos = ts.Days;

                    lst.Add(p);
                }

                List<EntPedido> lstClienteFactura = this.ObtienePedidosClienteFacturaDeuda(ClienteId);
                lst.AddRange(lstClienteFactura);
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// SOLO SE USA EN REPORTES
        /// </summary>
        /// <param name="EmpresaId"></param>
        /// <param name="FechaDesde"></param>
        /// <param name="FechaHasta"></param>
        /// <returns></returns>
        public List<EntPedido> ObtienePedidosClientesPorFechas(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePedidosClientes(EmpresaId,FechaDesde,FechaHasta);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PED_ID"]);
                    p.Sucursal = r["EST_NOMBRE"].ToString();
                    p.TipoPedidoId = Convert.ToInt32(r["PED_TIPOPEDIDOID"]);
                    p.Detalle = r["PED_DETALLE"].ToString();
                    p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();

                    //p.Pago = Convert.ToDecimal(r["PAG_PAGO"]);
                    p.NotasCredito = Convert.ToDecimal(r["PED_NOTASCREDITO"]);
                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.PagoTotal = Convert.ToDecimal(r["PROPED_PRECIOCOSTO"]);//COSTO
                    p.Descuento = p.Total - p.PagoTotal;//UTILIDAD
                    p.FechaCorta = Convert.ToDateTime(r["PED_FECHA"]).ToShortDateString();
                    p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    p.TrabajadorId = Convert.ToInt32(r["PED_TRABAJADORID"]);
                    p.Trabajador = r["TRA_NOMBRE"].ToString();
                    p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    p.Facturado = Convert.ToBoolean(r["PED_FACTURADO"]);
                    //if (p.Facturado)
                    //    p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();//"AA" + r["FAC_NUMEROFACTURA"].ToString();
                    //else
                    //    p.Factura = "S/F";
                    //p.UUID = r["FAC_UUID"].ToString();
                    //p.RutaFactura = r["FAC_RUTA"].ToString();
                    //p.FechaEntrega = Convert.ToDateTime(r["FAC_FECHA"]);

                    p.Facturado = Convert.ToBoolean(r["FAC_ESTATUSID"]);
                    p.UUID = r["FAC_UUID"].ToString();
                    p.RutaFactura = r["FAC_RUTA"].ToString();
                    p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();//"AA" + r["FAC_NUMEROFACTURA"].ToString();
                    if (p.Facturado)
                        p.EstatusDescripcion = "VIGENTE";
                    else if (!string.IsNullOrWhiteSpace(p.UUID.Replace("VENTA","").Replace(" CONSIGNA", "").Replace("DEVOLUCION","")))
                        p.EstatusDescripcion = "CANCELADO";
                    else
                    {
                        p.EstatusDescripcion = "SIN FACTURAR";
                        p.Factura = "VE-" + p.Id.ToString();
                        p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    }
                    p.EmpresaId = Convert.ToInt32(r["PED_EMPRESAID"]);

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// SOLO SE USA EN REPORTES
        /// </summary>
        /// <param name="EmpresaId"></param>
        /// <param name="FechaDesde"></param>
        /// <param name="FechaHasta"></param>
        /// <returns></returns>
        public List<EntPedido> ObtienePedidosClientesPorFechas(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta,
                                                               int EstatusId)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePedidosClientes(EmpresaId, FechaDesde, FechaHasta, EstatusId);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PED_ID"]);
                    p.Sucursal = r["EST_NOMBRE"].ToString();
                    p.TipoPedidoId = Convert.ToInt32(r["PED_TIPOPEDIDOID"]);
                    p.Detalle = r["PED_DETALLE"].ToString();
                    p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();

                    //p.Pago = Convert.ToDecimal(r["PAG_PAGO"]);
                    p.NotasCredito = Convert.ToDecimal(r["PED_NOTASCREDITO"]);
                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.PagoTotal = Convert.ToDecimal(r["PROPED_PRECIOCOSTO"]);//COSTO
                    p.Descuento = p.Total - p.PagoTotal;//UTILIDAD
                    p.FechaCorta = Convert.ToDateTime(r["PED_FECHA"]).ToShortDateString();
                    p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    p.TrabajadorId = Convert.ToInt32(r["PED_TRABAJADORID"]);
                    p.Trabajador = r["TRA_NOMBRE"].ToString();
                    p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    p.Facturado = Convert.ToBoolean(r["PED_FACTURADO"]);
                    //if (p.Facturado)
                    //    p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();//"AA" + r["FAC_NUMEROFACTURA"].ToString();
                    //else
                    //    p.Factura = "S/F";
                    //p.UUID = r["FAC_UUID"].ToString();
                    //p.RutaFactura = r["FAC_RUTA"].ToString();
                    //p.FechaEntrega = Convert.ToDateTime(r["FAC_FECHA"]);

                    p.Facturado = Convert.ToBoolean(r["FAC_ESTATUSID"]);
                    p.UUID = r["FAC_UUID"].ToString();
                    p.RutaFactura = r["FAC_RUTA"].ToString();
                    p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();//"AA" + r["FAC_NUMEROFACTURA"].ToString();
                    if (p.Facturado)
                        p.EstatusDescripcion = "VIGENTE";
                    else if (!string.IsNullOrWhiteSpace(p.UUID.Replace("VENTA", "").Replace(" CONSIGNA", "").Replace("DEVOLUCION", "")))
                        p.EstatusDescripcion = "CANCELADO";
                    else
                    {
                        p.EstatusDescripcion = "SIN FACTURAR";
                        p.Factura = "VE-" + p.Id.ToString();
                        p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    }
                    p.EmpresaId = Convert.ToInt32(r["PED_EMPRESAID"]);

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntPedido> ObtienePedidosClientesPorFechasConCostosPorProducto(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePedidosClientesConCostosPorProducto(EmpresaId, FechaDesde, FechaHasta);
                EntEmpresa empresa = new BusEmpresas().ObtieneEmpresa(EmpresaId);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.ClienteNombreFiscal = empresa.Nombre;

                    p.Id = Convert.ToInt32(r["PED_ID"]);
                    p.TipoPedidoId = Convert.ToInt32(r["PED_TIPOPEDIDOID"]);
                    p.Detalle = r["PED_DETALLE"].ToString();
                    //p.Solicitud = r["CLI_BANCO"].ToString();
                    p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();
                    p.TrabajadorId = Convert.ToInt32(r["PED_TRABAJADORID"]);
                    p.Trabajador = r["TRA_NOMBRE"].ToString();
                    //p.EmpresaId = Convert.ToInt32(r["PED_ESTABLECIMIENTOID"]);
                    p.Sucursal = r["EST_NOMBRE"].ToString();

                    //p.Pago = Convert.ToDecimal(r["PAG_PAGO"]);
                    //p.NotasCredito = Convert.ToDecimal(r["NOTASCREDITO"]);
                    p.ProductoId = Convert.ToInt32(r["PRO_ID"]);
                    p.Descripcion = r["PRO_CODIGO"].ToString()+" - "+ r["PRO_DESCRIPCION"].ToString();
                    p.SubTotal = Convert.ToDecimal(r["PROPED_CANTIDAD"]);
                    p.ProductoPrecio = Convert.ToDecimal(r["PROPED_PRECIOCOSTO"]);
                    p.Total = Convert.ToDecimal(r["PROPED_PRECIOVENTA"]);

                    p.FechaCorta = Convert.ToDateTime(r["PED_FECHA"]).ToShortDateString();
                    p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    p.Facturado = Convert.ToBoolean(r["FAC_ESTATUSID"]);
                    p.UUID = r["FAC_UUID"].ToString();
                    p.RutaFactura = r["FAC_RUTA"].ToString();
                    p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();//"AA" + r["FAC_NUMEROFACTURA"].ToString();
                    if (p.Facturado)
                        p.EstatusDescripcion = "VIGENTE";
                    else if (!string.IsNullOrWhiteSpace(p.UUID))
                        p.EstatusDescripcion = "CANCELADO";
                    else
                    {
                        p.EstatusDescripcion = "SIN FACTURAR";
                        p.Factura = "VE-" + p.Id.ToString();
                        p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    }
                    p.EmpresaId = Convert.ToInt32(r["PED_EMPRESAID"]);


                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntPedido> ObtieneProductosPedidosPreventa(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtieneProductosPedidosPreventa(EmpresaId, FechaDesde, FechaHasta);
                EntEmpresa empresa = new BusEmpresas().ObtieneEmpresa(EmpresaId);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.ClienteNombreFiscal = empresa.Nombre;

                    p.Id = Convert.ToInt32(r["PEDPRE_ID"]);
                    //p.TipoPedidoId = Convert.ToInt32(r["PEDPRE_TIPOPEDIDOID"]);
                    p.Detalle = r["PEDPRE_DETALLE"].ToString();
                    //p.Solicitud = r["CLI_BANCO"].ToString();
                    p.ClienteId = Convert.ToInt32(r["PEDPRE_CLIENTEID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();
                    p.TrabajadorId = Convert.ToInt32(r["PEDPRE_TRABAJADORID"]);
                    p.Trabajador = r["TRA_NOMBRE"].ToString();
                    //p.Sucursal = r["ESTPEDPRE_DESCRIPCION"].ToString();

                    p.ProductoId = Convert.ToInt32(r["PRO_ID"]);
                    p.Descripcion = r["PRO_CODIGO"].ToString() + " - " + r["PRO_DESCRIPCION"].ToString();
                    p.SubTotal = Convert.ToDecimal(r["PROPEDPRE_CANTIDAD"]);
                    //p.ProductoPrecio = Convert.ToDecimal(r["PROPED_PRECIOCOSTO"]);
                    p.Total = Convert.ToDecimal(r["PROPEDPRE_PRECIOVENTA"]);

                    p.FechaCorta = Convert.ToDateTime(r["PEDPRE_FECHA"]).ToShortDateString();
                    p.Fecha = Convert.ToDateTime(r["PEDPRE_FECHA"]);
                    p.EstatusId = Convert.ToInt32(r["PEDPRE_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPEDPRE_DESCRIPCION"].ToString();

                    p.EstatusDescripcion = "SIN FACTURAR";
                    p.Factura = "VE-" + p.Id.ToString();
                    p.Fecha = Convert.ToDateTime(r["PEDPRE_FECHA"]);
                    p.EmpresaId = EmpresaId;// Convert.ToInt32(r["PEDPRE_EMPRESAID"]);


                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntPedido> ObtienePedidosClientesPorCliente(int ClienteId)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePedidosClientes(ClienteId);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PED_ID"]);
                    p.Detalle = r["PED_DETALLE"].ToString();
                    p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();

                    p.NotasCredito = Convert.ToDecimal(r["NOTASCREDITO"]);
                    p.Pago = Convert.ToDecimal(r["PAG_PAGO"]);
                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.FechaCorta = Convert.ToDateTime(r["PED_FECHA"]).ToShortDateString();
                    p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    p.FacturaId = Convert.ToInt32(r["FAC_ID"]);
                    p.EmpresaId= Convert.ToInt32(r["FAC_EMPRESAID"]);
                    p.Facturado = Convert.ToBoolean(r["FAC_ESTATUSID"]);
                    p.UUID = r["FAC_UUID"].ToString();
                    p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();//"AA" + r["FAC_NUMEROFACTURA"].ToString();
                    p.FormaPagoId= Convert.ToInt32(r["FAC_FORMAPAGOID"]);
                    p.RutaFactura = r["FAC_RUTA"].ToString();
                    if (p.Facturado)
                        p.EstatusDescripcion = "VIGENTE";
                    else if (!string.IsNullOrWhiteSpace(p.UUID))
                        p.EstatusDescripcion = "CANCELADO";
                    else
                    {
                        p.EstatusDescripcion = "SIN FACTURAR";
                        p.Factura = "";
                    }

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<EntPedido> ObtienePagosClientes(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePagosClientes(EmpresaId,FechaDesde, FechaHasta);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    //p.Id = Convert.ToInt32(r["PED_ID"]);
                    //p.Observaciones = r["PED_OBSERVACIONES"].ToString();
                    p.ClienteId = Convert.ToInt32(r["CLI_ID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();

                    //p.Debe = Convert.ToDecimal(r["DEBE"]);
                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.FechaCorta = Convert.ToDateTime(r["PED_FECHA"]).ToShortDateString(); 

                    p.Pago = Convert.ToDecimal(r["PAG_PAGO"]);
                    p.FechaPago= Convert.ToDateTime(r["PAG_FECHAPAGO"]);
                    p.FormaPagoId = Convert.ToInt32(r["PAG_FORMAPAGOID"].ToString());
                    p.Detalle = r["PAG_FORMAPAGO"].ToString();

                    p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]); 
                    p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                    p.UUID = r["FAC_UUID"].ToString();

                    p.NumOrden= r["COMPAG_NUMEROCOMPLEMENTO"].ToString();

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntPedido> ObtienePagosClientesSinFactura(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePagosClientesSinFactura(EmpresaId, FechaDesde, FechaHasta);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    //p.Id = Convert.ToInt32(r["PED_ID"]);
                    //p.Observaciones = r["PED_OBSERVACIONES"].ToString();
                    p.ClienteId = Convert.ToInt32(r["CLI_ID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();

                    //p.Debe = Convert.ToDecimal(r["DEBE"]);
                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.FechaCorta = Convert.ToDateTime(r["PED_FECHA"]).ToShortDateString();

                    p.Pago = Convert.ToDecimal(r["PAG_PAGO"]);
                    p.FechaPago = Convert.ToDateTime(r["PAG_FECHAPAGO"]);
                    p.FormaPagoId = Convert.ToInt32(r["PAG_FORMAPAGOID"].ToString());
                    p.Detalle = r["PAG_FORMAPAGO"].ToString();

                    p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                    p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                    p.UUID = r["FAC_UUID"].ToString();

                    p.NumOrden = r["COMPAG_NUMEROCOMPLEMENTO"].ToString();

                    p.Sucursal = r["EST_NOMBRE"].ToString(); 
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntPedido> ObtienePagosClientesSinDeposito()
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePagosClientesSinDeposito();
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PAG_ID"]);
                    p.Detalle = r["PED_DETALLE"].ToString();
                    //p.Observaciones = r["PED_OBSERVACIONES"].ToString();
                    p.ClienteId = Convert.ToInt32(r["CLI_ID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();

                    p.Pago = Convert.ToDecimal(r["PAG_PAGO"]);
                    //p.Debe = Convert.ToDecimal(r["DEBE"]);
                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.FechaCorta = Convert.ToDateTime(r["PED_FECHA"]).ToShortDateString();
                    p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    p.FechaPago = Convert.ToDateTime(r["PAG_FECHAPAGO"]);
                    //p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    //p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    if (!string.IsNullOrWhiteSpace(r["FAC_ID"].ToString()))
                    {
                        p.Factura = r["FAC_ID"].ToString();
                        p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                    }
                    else
                        p.Factura = p.Detalle;

                    //if (p.Factura == "0")
                    //    p.Factura = "";
                    //else
                    //    p.Factura = "AA " + p.Factura;

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<EntPedido> ObtieneNotasCreditoPedidos(int ClienteId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtieneNotasCreditoPedidos(ClienteId, FechaDesde, FechaHasta);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["NOTCRE_ID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();
                    p.ClienteRFC = r["CLI_RFC"].ToString();
                    //if (p.Cliente.Contains('-'))
                    //    p.CodigoCliente = p.Cliente.Split('-')[0];

                    p.PedidoRelacionadoId = Convert.ToInt32(r["NOTCRE_PEDIDOID"]);

                    p.TipoPedidoId = Convert.ToInt32(r["NOTCRE_TIPOID"]);
                    p.NumOrden = r["NOTCRE_SERIE"].ToString() + r["NOTCRE_NUMERO"].ToString();
                    p.Descripcion = r["NOTCRE_DESCRIPCION"].ToString();
                    p.UUID = r["NOTCRE_UUID"].ToString();
                    p.NotasCredito = Convert.ToDecimal(r["NOTCRE_CANTIDAD"]);
                    p.Pago = p.NotasCredito;
                    p.FechaPago = Convert.ToDateTime(r["NOTCRE_FECHA"]);
                    //DateTime fechaRegistro = Convert.ToDateTime(r["NOTCRE_FECHAREGISTRO"]);
                    //p.FechaPago = p.FechaPago.AddHours(fechaRegistro.Hour).AddMinutes(fechaRegistro.Minute).AddSeconds(fechaRegistro.Second);
                    //p.FechaCorta = fechaRegistro.ToShortTimeString();

                    p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                    p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();// + " | NC-" + r["NOTCRE_NUMERO"].ToString();
                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    //p.TotalUSD = p.Total;

                    p.Estatus = Convert.ToBoolean(r["NOTCRE_ESTATUS"]);
                    if (p.Estatus)
                        p.EstatusDescripcion = "VIGENTE";
                    else
                        p.EstatusDescripcion = "CANCELADA";

                    //p.Usuario = r["USU_NOMBRE"].ToString();
                    p.EmpresaId = Convert.ToInt32(r["NOTCRE_EMPRESAID"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntPedido> ObtieneComplementosPago(int EmpresaId, int EstablecimientoId)
        {
            try
            {
                dt = new DatFacturas().obtieneComplementosPagoPorEmpresa(EmpresaId, EstablecimientoId);
                List<EntPedido> lst = new List<EntPedido>();
                int numParc = dt.Rows.Count;
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["COMPAG_ID"]);
                    p.Descripcion = "CP-" + r["COMPAG_NUMEROCOMPLEMENTO"].ToString();
                    p.Fecha = Convert.ToDateTime(r["COMPAG_FECHA"]);
                    p.FechaCorta = Convert.ToDateTime(r["PED_FECHA"]).ToString();
                    p.FechaEntrega = Convert.ToDateTime(r["FAC_FECHA"]);
                    p.FechaPago = Convert.ToDateTime(r["PAG_FECHAPAGO"]);

                    p.Pago = Convert.ToDecimal(r["COMPAG_PAGO"]);
                    p.UUID = r["COMPAG_UUID"].ToString();
                    p.RutaFactura = r["COMPAG_RUTA"].ToString();
                    p.FormaPagoId = Convert.ToInt32(r["COMPAG_FORMAPAGOID"]);

                    p.Total = Convert.ToDecimal(r["COMPAG_PAGOFACTURA"]);

                    p.Estatus = Convert.ToBoolean(r["COMPAG_ESTATUS"]);

                    if (p.Estatus)
                        p.EstatusDescripcion = "VIGENTE";
                    else
                        p.EstatusDescripcion = "CANCELADO";

                    p.PedidoRelacionadoId = Convert.ToInt32(r["PED_ID"]);
                    p.FacturaId = Convert.ToInt32(r["FAC_ID"]);
                    p.Factura = r["FAC_SERIEFACTURA"].ToString() + "-" + r["FAC_NUMEROFACTURA"].ToString();
                    p.ClienteId = Convert.ToInt32(r["CLI_ID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();
                    p.ClienteNombreFiscal = r["CLI_NOMBREFISCAL"].ToString();
                    p.ClienteRFC = r["CLI_RFC"].ToString();

                    p.EmpleadoId = numParc;

                    numParc--;

                    lst.Add(p);
                }

                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntPedido> ObtieneComplementosPago(int EmpresaId, int EstablecimientoId,
                                                        DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                dt = new DatFacturas().obtieneComplementosPagoPorEmpresa(EmpresaId, EstablecimientoId,
                                                                          FechaDesde, FechaHasta);
                List<EntPedido> lst = new List<EntPedido>();
                int numParc = dt.Rows.Count;
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["COMPAG_ID"]);
                    p.Descripcion = "CP-" + r["COMPAG_NUMEROCOMPLEMENTO"].ToString();
                    p.Fecha = Convert.ToDateTime(r["COMPAG_FECHA"]);
                    p.FechaCorta = Convert.ToDateTime(r["PED_FECHA"]).ToShortDateString();
                    p.FechaEntrega = Convert.ToDateTime(r["FAC_FECHA"]);
                    p.FechaPago = Convert.ToDateTime(r["PAG_FECHAPAGO"]);

                    p.Pago = Convert.ToDecimal(r["COMPAG_PAGO"]);
                    p.UUID = r["COMPAG_UUID"].ToString();
                    p.RutaFactura = r["COMPAG_RUTA"].ToString();
                    p.FormaPagoId = Convert.ToInt32(r["COMPAG_FORMAPAGOID"]);

                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);

                    p.Estatus = Convert.ToBoolean(r["COMPAG_ESTATUS"]);

                    if (p.Estatus)
                        p.EstatusDescripcion = "VIGENTE";
                    else
                        p.EstatusDescripcion = "CANCELADO";

                    p.PedidoRelacionadoId = Convert.ToInt32(r["PED_ID"]);
                    p.FacturaId = Convert.ToInt32(r["FAC_ID"]);
                    p.Factura = r["FAC_SERIEFACTURA"].ToString() + "-" + r["FAC_NUMEROFACTURA"].ToString();
                    p.ClienteId = Convert.ToInt32(r["CLI_ID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();
                    p.ClienteNombreFiscal = r["CLI_NOMBREFISCAL"].ToString();
                    p.ClienteRFC = r["CLI_RFC"].ToString();

                    p.EmpleadoId = numParc;

                    numParc--;

                    lst.Add(p);
                }

                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntPedido> ObtieneComplementosPagoPorFechaPago(int EmpresaId, int EstablecimientoId,
                                                        DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                dt = new DatFacturas().obtieneComplementosPagoPorEmpresaFechaPago(EmpresaId, EstablecimientoId,
                                                                          FechaDesde, FechaHasta);
                List<EntPedido> lst = new List<EntPedido>();
                int numParc = dt.Rows.Count;
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["COMPAG_ID"]);
                    p.Descripcion = "CP-" + r["COMPAG_NUMEROCOMPLEMENTO"].ToString();
                    p.Fecha = Convert.ToDateTime(r["COMPAG_FECHA"]);
                    p.FechaCorta = Convert.ToDateTime(r["PED_FECHA"]).ToString();
                    p.FechaEntrega = Convert.ToDateTime(r["FAC_FECHA"]);
                    p.FechaPago = Convert.ToDateTime(r["PAG_FECHAPAGO"]);

                    p.Pago = Convert.ToDecimal(r["COMPAG_PAGO"]);
                    p.UUID = r["COMPAG_UUID"].ToString();
                    p.RutaFactura = r["COMPAG_RUTA"].ToString();
                    p.FormaPagoId = Convert.ToInt32(r["COMPAG_FORMAPAGOID"]);

                    p.Total = Convert.ToDecimal(r["COMPAG_PAGOFACTURA"]);

                    p.Estatus = Convert.ToBoolean(r["COMPAG_ESTATUS"]);

                    if (p.Estatus)
                        p.EstatusDescripcion = "VIGENTE";
                    else
                        p.EstatusDescripcion = "CANCELADO";

                    p.PedidoRelacionadoId = Convert.ToInt32(r["PED_ID"]);
                    p.FacturaId = Convert.ToInt32(r["FAC_ID"]);
                    p.Factura = r["FAC_SERIEFACTURA"].ToString() + "-" + r["FAC_NUMEROFACTURA"].ToString();
                    p.ClienteId = Convert.ToInt32(r["CLI_ID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();
                    p.ClienteNombreFiscal = r["CLI_NOMBREFISCAL"].ToString();
                    p.ClienteRFC = r["CLI_RFC"].ToString();

                    p.EmpleadoId = numParc;

                    numParc--;

                    lst.Add(p);
                }

                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Agrega nuevo registro de Pedido
        /// </summary>
        /// <param name="Pedido">
        /// Propiedades Necesarias: ClienteId, Detalle, Observaciones, Total, Pago, Fecha, FechaEntrega, EmpleadoId, Facturado, EstatusId.
        /// </param>
        public int AgregaPedido(EntPedido Pedido)
        {
            try
            {
                return new DatPedidos().agregaPedido(Pedido.TipoPedidoId, Pedido.ClienteId, Pedido.Detalle, Pedido.Solicitud, Pedido.Observaciones, Pedido.Total, Pedido.Pago, Pedido.Fecha, Pedido.FechaEntrega, Pedido.EmpleadoId, Pedido.Facturado, Pedido.EstatusId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public int AgregaPedidoVenta(int EstablecimientoId, int EmpresaId, EntPedido Pedido)
        {
            try
            {
                return new DatPedidos().agregaPedidoVenta(EstablecimientoId, EmpresaId, Pedido.TipoPedidoId, Pedido.ClienteId, 
                                                    Pedido.Detalle, Pedido.Observaciones, Pedido.IEPS, Pedido.Total, Pedido.Pago, 
                                                    Pedido.Fecha, Pedido.FechaEntrega, Pedido.EmpleadoId, Pedido.Facturado,
                                                    Pedido.TrabajadorId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// EmpresaId, Convert.ToInt32(Pedido.NumOrden), Pedido.PedidoRelacionadoId, Pedido.TipoPedidoId, 
        ///Pedido.ClienteId, Pedido.MetodoPagoId, Pedido.Detalle, Pedido.Observaciones,
        ///Pedido.Total, Pedido.IVA, Pedido.IEPS, Pedido.IVARetencion, Pedido.ISRRetencion,
        ///Pedido.Fecha, Pedido.FechaEntrega, Pedido.TrabajadorId,
        ///UsuarioId
        /// </summary>
        /// <param name="EmpresaId"></param>
        /// <param name="Pedido"></param>
        /// <param name="UsuarioId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int AgregaPedidoPreVenta(int EmpresaId, EntPedido Pedido, int UsuarioId)
        {
            try
            {
                return new DatPedidos().agregaPedidoPreVenta(EmpresaId, Convert.ToInt32(Pedido.NumOrden), Pedido.PedidoRelacionadoId, Pedido.TipoPedidoId, 
                                                    Pedido.ClienteId, Pedido.MetodoPagoId, Pedido.Detalle, Pedido.Observaciones,
                                                    Pedido.Total, Pedido.IVA, Pedido.IEPS, Pedido.IVARetencion, Pedido.ISRRetencion,
                                                    Pedido.Fecha, Pedido.FechaEntrega, Pedido.TrabajadorId,
                                                    UsuarioId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int AgregaMovimientoMaster(string Comentario, int TipoMovimientoId, int Orientacion, int AlmacenId, int PedidoId,
                                            int UsuarioId, int ProveedorId = 0)
        {
            try
            {
                return new DatPedidos().agregaMovimientoMaster(Comentario, TipoMovimientoId, Orientacion, AlmacenId, PedidoId,
                                                                UsuarioId, ProveedorId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public int AgregaMovimientoMaster(string Comentario, int TipoMovimientoId, int Orientacion, int AlmacenId, int PedidoId,
                                            DateTime FechaMovimiento,
                                            int UsuarioId, int ProveedorId = 0)
        {
            try
            {
                return new DatPedidos().agregaMovimientoMaster(Comentario, TipoMovimientoId, Orientacion, AlmacenId, PedidoId,
                                                                FechaMovimiento,
                                                                UsuarioId, ProveedorId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int AgregaMovimientoDetalle(int MovimientoInventarioId, int ProductoId, decimal Cantidad, decimal Total)
        {
            try
            {
                return new DatPedidos().agregaMovimientoDetalle(MovimientoInventarioId, ProductoId, Cantidad, Total);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int AgregaMovimientoLote(int MovimientoId, int Orientacion)
        {
            try
            {
                return new DatPedidos().agregaMovimientoLote(MovimientoId, Orientacion);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public int AgregaMovimientoTraspaso(int TraspasoId, int MovimientoId, int AlmacenOrigenId, int AlmacenDestinoId, 
                                            string Comentario, int EstadoId,int UsuarioId)
        {
            try
            {
                return new DatPedidos().agregaMovimientoTraspaso(TraspasoId ,MovimientoId, AlmacenOrigenId, AlmacenDestinoId, Comentario, EstadoId, UsuarioId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public int AgregaMovimientoTraspasoConsigna(int TraspasoId, int MovimientoId, int AlmacenOrigenId, int UsuarioDestinoId,
                                            string Comentario, int EstadoId, int UsuarioId)
        {
            try
            {
                return new DatPedidos().agregaMovimientoTraspasoConsigna(TraspasoId, MovimientoId, AlmacenOrigenId, UsuarioDestinoId, Comentario, EstadoId, UsuarioId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Agrega nueva relacion de  Producto con Pedido
        /// </summary>
        /// <param name="Pedido">
        /// Propiedades Necesarias: Id, ProductoId, Cantidad, PrecioVenta, Detalle.
        /// </param>
        public void AgregaProductoPedido(EntPedido Pedido, EntProducto Producto)
        {
            try
            {
                new DatPedidos().agregaProductoPedido(Pedido.Id, Producto.Id, Producto.Cantidad, Producto.PrecioVenta, Pedido.Detalle);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Agrega nueva relacion de  Producto con Pedido
        /// </summary>
        /// <param name="Pedido">
        /// Propiedades Necesarias: Id, ProductoId, Cantidad, PrecioVenta, Detalle.
        /// </param>
        public void AgregaProductoDetallePedido(EntPedido Pedido, EntProducto Producto)
        {
            try
            {
                new DatPedidos().agregaProductoDetallePedido(Pedido.Id, Producto.Id, Producto.Cantidad, Producto.PrecioCosto, Producto.PrecioVenta);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void AgregaProductoPedidoPreVenta(EntPedido Pedido, EntProducto Producto)
        {
            try
            {
                new DatPedidos().agregaProductoPedidoPreVenta(Pedido.Id, Producto.Id, 
                                                            Producto.Cantidad, Producto.IEPS, Producto.IVA, Producto.PrecioVenta,
                                                            Producto.Descripcion);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        //MOVER A DatPagos
        /// <summary>
        /// Solo Agrega registro de Pago.
        /// </summary>
        /// <param name="Pago">
        /// Propiedades Necesarias: Id, Pago.
        /// </param>
        public int AgregaPago(EntPago Pago)
        {
            try
            {
               return new DatPedidos().agregaPago(Pago.PedidoId,Pago.TipoPagoId, Pago.Cantidad,Pago.FechaPago,
                                                        Pago.FormaPagoId, Pago.FormaPago, Pago.UsuarioId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Agrega Registro de Pago y Actualiza EstatusId de Pedido si es necesario.
        /// </summary>
        /// <param name="Pago">
        /// Propiedades Necesarias: Id, Pago.
        /// </param>
        public int AgregaPagoPedido(int EstablecimientoId, EntPago Pago)
        {
            try
            {
                return new DatPedidos().agregaPagoPedido(EstablecimientoId, Pago.PedidoId, Pago.TipoPagoId, Pago.Cantidad, Pago.FechaPago,
                                                         Pago.FormaPagoId, Pago.FormaPago, Pago.UsuarioId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void ActualizaPago(EntPago Pago)
        {
            try
            {
                new DatPedidos().actualizaPago(Pago.Id, Pago.Cantidad, Pago.FechaPago, Pago.Estatus);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void ActualizaEstatusPago(EntPago Pago)
        {
            try
            {
                new DatPedidos().actualizaEstatusPago(Pago.Id, Pago.Estatus);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void ActualizaEstatusPago(int PagoId, bool Estatus)
        {
            try
            {
                new DatPedidos().actualizaEstatusPago(PagoId, Estatus);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void ActualizaEstatusPago(int PedidoId, DateTime FechaPago, decimal Pago, bool Estatus)
        {
            try
            {
                new DatPedidos().actualizaEstatusPago(PedidoId, FechaPago, Pago, Estatus);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public void CorrigePago(int PagoId, decimal Pago, DateTime FechaPago)
        {
            try
            {
                new DatPedidos().corrigePago(PagoId, Pago,FechaPago);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        //MOVER A DatPagos

        public List<EntPago> ObtienePagosPorCliente(int ClienteId)
        {
            try
            {
                List<EntPago> lst = new List<EntPago>();
                dt = new DatPedidos().obtienePagosPorCliente(ClienteId);
                foreach (DataRow r in dt.Rows)
                {
                    EntPago p = new EntPago();
                    p.Id = Convert.ToInt32(r["PAG_ID"]);
                    p.PedidoId = Convert.ToInt32(r["PAG_PEDIDOID"]);
                    p.Cantidad = Convert.ToDecimal(r["PAG_PAGO"]);
                    p.FechaPago = Convert.ToDateTime(r["PAG_FECHAPAGO"]);
                    p.FormaPago = r["PAG_FORMAPAGO"].ToString();
                    p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    p.Descripcion = "VE-"+p.PedidoId.ToString()+" | "+ r["PED_DETALLE"].ToString();
                    p.Estatus = Convert.ToBoolean(r["PAG_ESTATUS"]);
                    p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();//"AA" + r["FAC_NUMEROFACTURA"].ToString();

                    p.EmpresaId = Convert.ToInt32(r["CLI_ID"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntFactura> ObtieneFacturasPorPedido(int PedidoId)
        {
            try
            {
                List<EntFactura> lst = new List<EntFactura>();
                dt = new DatPedidos().obtieneFacturasPorPedido(PedidoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntFactura p = new EntFactura();
                    p.Id = Convert.ToInt32(r["FAC_ID"]);
                    p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                    p.EstatusId = Convert.ToInt32(r["FAC_ESTATUSID"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public EntFactura ObtieneSiguienteFactura(int EmpresaId, int PedidoId, string SerieFactura)
        {
            try
            {
                dt = new DatPedidos().obtieneSiguienteFactura(EmpresaId, PedidoId, SerieFactura);
                EntFactura p = new EntFactura();
                foreach (DataRow r in dt.Rows)
                {
                    p.Id = Convert.ToInt32(r["NUMFAC_ID"]);
                    p.SerieFactura= r["NUMFAC_SERIE"].ToString();
                    p.NumeroFactura = r["NUMFAC_NUMERO"].ToString();
                }
                return p;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public EntFactura ObtieneUltimaFactura(int EmpresaId)
        {
            try
            {
                dt = new DatPedidos().obtieneUltimaFactura(EmpresaId);
                EntFactura p = new EntFactura();
                foreach (DataRow r in dt.Rows)
                {
                    p.Id = Convert.ToInt32(r["FAC_ID"]);
                    p.NumeroFactura = r["FAC_NUMEROFACTURA"].ToString();
                    p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                    p.EstatusId = Convert.ToInt32(r["FAC_ESTATUSID"]);
                }
                return p;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public EntFactura ObtieneUltimaNotaCredito(int EmpresaId)
        {
            try
            {
                dt = new DatPedidos().obtieneUltimaNotaCredito(EmpresaId);
                EntFactura p = new EntFactura();
                foreach (DataRow r in dt.Rows)
                {
                    p.Id = Convert.ToInt32(r["NOTCRE_ID"]);
                    p.NumeroFactura = r["NOTCRE_NUMERO"].ToString();
                    p.Fecha = Convert.ToDateTime(r["NOTCRE_FECHA"]);
                    p.EstatusId = Convert.ToInt32(r["NOTCRE_ESTATUS"]);
                }
                return p;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Actualiza el Pago del Pedido.
        /// </summary>
        /// <param name="Factura">
        /// Propiedades Necesarias: Id, Pago.
        /// </param>
        //public void AgregaFactura(EntFactura Factura)
        //{
        //    try
        //    {
        //        new DatPedidos().agregaFactura(Factura.PedidoId, Factura.UUID,
        //                                        Factura.TipoComprobanteId, Factura.FormaPagoId, Factura.MetodoPagoId, Factura.UsoCFDIId,
        //                                        Factura.Fecha, Factura.Ruta);
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}
        /// <summary>
        /// Actualiza Estatus del Pedido.
        /// </summary>
        /// <param name="Pedido">
        /// Propiedades Necesarias: Id, EstatusId.
        /// </param>
        public void ActualizaEstatusFacturaPedido(EntFactura Factura)
        {
            try
            {
                new DatPedidos().actualizaEstatusFacturaPedido(Factura.Id, Factura.EstatusId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Actualiza Estatus del Pedido.
        /// </summary>
        /// <param name="Pedido">
        /// Propiedades Necesarias: Id, EstatusId.
        /// </param>
        public void ActualizaEstatusPedido(EntPedido Pedido)
        {
            try
            {
                new DatPedidos().actualizaEstatusPedido(Pedido.Id, Pedido.EstatusId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void ActualizaEstatusPedidoPreVenta(EntPedido Pedido)
        {
            try
            {
                new DatPedidos().actualizaEstatusPedidoPreVenta(Pedido.Id, Pedido.EstatusId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void ActualizaEstatusProductoDetallePedido(EntPedido Pedido)
        {
            try
            {
                new DatPedidos().actualizaEstatusProductoDetallePedido(Pedido.Id, Pedido.Estatus);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public void ActualizaEstatusProductoPedidoPreVenta(int PedidoId, int EstatusId)
        {
            try
            {
                new DatPedidos().actualizaEstatusProductoPedidoPreVenta(PedidoId, EstatusId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Actualiza Pedido.
        /// </summary>
        /// <param name="Pedido">
        /// Propiedades Necesarias: Id, Detalle, Total, Pago, FechaEntrega, EstatusId.
        /// </param>
        public void ActualizaPedido(EntPedido Pedido)
        {
            try
            {
                new DatPedidos().actualizaPedido(Pedido.Id, Pedido.Detalle, Pedido.Total, Pedido.Pago, Pedido.FechaEntrega, Pedido.EstatusId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void ActualizaPedido(int PedidoId, bool Facturado)
        {
            try
            {
                new DatPedidos().actualizaPedido(PedidoId, Facturado);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void ActualizaDetallePedido(int PedidoId, string Detalle)
        {
            try
            {
                new DatPedidos().actualizaDetallePedido(PedidoId, Detalle);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public void ActualizaPedidoPreVenta(int @PedidoPreVentaId, int @PedidoRelacionId, int @TipoPedidoId,
                                            string @Detalle, string @Observaciones, int @TrajaadorId, int @EstatusId)
        {
            try
            {
                new DatPedidos().actualizaPedidoPreVenta(@PedidoPreVentaId, @PedidoRelacionId, @TipoPedidoId, 
                                                         Detalle, Observaciones, TrajaadorId, EstatusId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Actualiza el Pago del Pedido.
        /// </summary>
        /// <param name="Pedido">
        /// Propiedades Necesarias: Id, Pago.
        /// </param>
        public void AumentaPagoEnPedido(EntPedido Pedido)
        {
            try
            {
                new DatPedidos().aumentaPagoEnPedido(Pedido.Id, Pedido.Pago);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Actualiza el Pago del Pedido.
        /// </summary>
        /// <param name="Pedido">
        /// Propiedades Necesarias: Id, Pago.
        /// </param>
        public void AumentaPagoPedido(EntPedido Pedido)
        {
            try
            {
                new DatPedidos().aumentaPagoPedido(Pedido.Id, Pedido.Pago);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Actualiza el Pago del Pedido. ACTUALIZA ESTATUS DE PEDIDO DEPENDIENDO A PAGADO O VIGENTE.
        /// </summary>
        /// <param name="Pedido">
        /// Propiedades Necesarias: Id, Pago.
        /// </param>
        public void AumentaPagoPedido(int PedidoId, decimal Pago)
        {
            try
            {
                new DatPedidos().aumentaPagoPedido(PedidoId, Pago);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public void AumentaNotaCreditoPedido(int PedidoId, decimal Pago, int UsuarioId)
        {
            try
            {
                new DatPedidos().aumentaNotaCreditoPedido(PedidoId, Pago, UsuarioId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// CANCELA PAGOS, PRODUCTOSDETALLEPEDIDO, PEDIDO
        /// </summary>
        /// <param name="PedidoId"></param>
        public void CancelaPedidoTodo(int PedidoId)
        {
            try
            {
                new DatPedidos().CancelaPedidoTodo(PedidoId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// ELIMINA PAGOS, PRODUCTOSDETALLEPEDIDO, PEDIDO
        /// </summary>
        /// <param name="PedidoId"></param>
        public void EliminaPedidoTodo(int PedidoId)
        {
            try
            {
                new DatPedidos().eliminaPedidoTodo(PedidoId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// PRODUCTOPEDIDOPREVENTA, PEDIDOPREVENTA
        /// </summary>
        /// <param name="PedidoPreVentaId"></param>
        /// <exception cref="Exception"></exception>
        public void EliminaPedidoPreVentaTodo(int PedidoPreVentaId)
        {
            try
            {
                new DatPedidos().eliminaPedidoPreVentaTodo(PedidoPreVentaId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
