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
    public class BusFacturas : BusAbstracta
    {
        public List<EntFactura> ObtieneFacturasPorPedido(int PedidoId)
        {
            try
            {
                List<EntFactura> lst = new List<EntFactura>();
                dt = new DatFacturas().obtieneFacturasPorPedido(PedidoId);
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
        public List<EntFactura> ObtieneFacturasPorComplemento(string Complemento)
        {
            try
            {
                List<EntFactura> lst = new List<EntFactura>();
                dt = new DatFacturas().obtieneFacturasPorComplemento(Complemento);
                foreach (DataRow r in dt.Rows)
                {
                    EntFactura p = new EntFactura();
                    p.Id = Convert.ToInt32(r["FAC_ID"]);
                    p.PedidoId = Convert.ToInt32(r["FAC_PEDIDOID"]);
                    p.NumeroFactura = r["FAC_NUMEROFACTURA"].ToString();
                    p.Pago = Convert.ToDecimal(r["COMPAG_PAGOFACTURA"]);
                    p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                    p.EstatusId = Convert.ToInt32(r["FAC_ESTATUSID"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public EntFactura ObtieneUltimaFactura()
        {
            try
            {
                dt = new DatFacturas().obtieneUltimaFactura();
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
        public EntFactura ObtieneUltimaFactura(int EmpresaId)
        {
            try
            {
                dt = new DatFacturas().obtieneUltimaFactura(EmpresaId);
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

        public List<EntFactura> ObtieneComplementos(int FacturaId, string NumeroFactura, decimal Total)
        {
            try
            {
                dt = new DatFacturas().obtieneComplementos(FacturaId);
                List<EntFactura> lst = new List<EntFactura>();
                int numParc = dt.Rows.Count;
                foreach (DataRow r in dt.Rows)
                {
                    EntFactura p = new EntFactura();
                    //p.Id = Convert.ToInt32(r["COMPAG_ID"]);
                    p.Id = Convert.ToInt32(r["COMPAG_NUMEROCOMPLEMENTO"]);
                    p.Fecha = Convert.ToDateTime(r["COMPAG_FECHA"]);
                    p.Pago = Convert.ToDecimal(r["COMPAG_PAGO"]);
                    p.UUID = r["COMPAG_UUID"].ToString();
                    p.Ruta = r["COMPAG_RUTA"].ToString();
                    p.FormaPagoId = Convert.ToInt32(r["COMPAG_FORMAPAGOID"]);

                    p.NumeroFactura = NumeroFactura;
                    p.Total = Total;
                    p.PedidoId = numParc;

                    p.Estatus = Convert.ToBoolean(r["COMPAG_ESTATUS"]);

                    if (p.Estatus)
                        p.Descripcion = "VIGENTE";
                    else
                        p.Descripcion = "CANCELADO";

                    p.PedidoId = numParc;

                    numParc--;

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public EntFactura ObtieneUltimoComplementoPago()
        {
            try
            {
                dt = new DatFacturas().obtieneUltimoComplemento();
                EntFactura p = new EntFactura();
                foreach (DataRow r in dt.Rows)
                {
                    p.Id = Convert.ToInt32(r["COMPAG_ID"]);
                    if (p.Id > 0)
                    {
                        p.Fecha = Convert.ToDateTime(r["COMPAG_FECHA"]);
                        p.Estatus = Convert.ToBoolean(r["COMPAG_ESTATUS"]);
                    }
                }
                return p;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public int ObtieneNumeroParcialidades(int FacturaId)
        {
            try
            {
                int numParcialdad = 0;
                dt = new DatFacturas().obtieneNumeroParcialidades(FacturaId);
                //EntFactura p = new EntFactura();
                foreach (DataRow r in dt.Rows)
                {
                    //p.Id = Convert.ToInt32(r["NOTCRE_ID"]);
                    numParcialdad = Convert.ToInt32(r["NUMPAGOS"]);
                }
                //numParcialdad++;
                return numParcialdad;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        /// <summary>
        /// Actualiza una Factura.
        /// </summary>
        /// <param name="factura">
        /// Propiedades Necesarias: PedidoId, NumeroFactura, UUID, Fecha, Ruta.
        /// </param>
        public void AgregaFactura(EntFactura Factura)
        {
            try
            {
                new DatFacturas().agregaFactura(Factura.EmpresaId, Factura.PedidoId, Factura.SerieFactura, Factura.NumeroFactura, Factura.UUID,
                                                Factura.TipoComprobanteId, Factura.FormaPagoId, Factura.MetodoPagoId, Factura.UsoCFDIId,
                                                Factura.Fecha, Factura.Ruta);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// .
        /// </summary>
        /// <param name="Complemento">
        /// Propiedades Necesarias: Id(FacturaId), Fecha, TipoComprobanteId,
        ///                         FormaPagoId, Ruta.
        /// </param>
        public void AgregaComplementePago(EntFactura Complemento)
        {
            try
            {
                new DatFacturas().agregaComplementoPago(Complemento.Id, Complemento.Fecha, Complemento.Pago,
                                                        Complemento.TipoComprobanteId, Complemento.FormaPagoId,
                                                        Complemento.NumeroFactura, Complemento.Saldo, Complemento.UUID, Complemento.Ruta);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Actualiza Estatus de la Factura.
        /// </summary>
        /// <param name="Factura">
        /// Propiedades Necesarias: Id, EstatusId.
        /// </param>
        public void ActualizaEstatusFacturaPedido(EntFactura Factura)
        {
            try
            {
                new DatFacturas().actualizaEstatusFacturaPedido(Factura.Id, Factura.EstatusId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void ActualizaEstatusComplementoPago(EntFactura Factura)
        {
            try
            {
                new DatFacturas().actualizaEstatusComplementoPago(Factura.Id, Factura.Estatus);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
