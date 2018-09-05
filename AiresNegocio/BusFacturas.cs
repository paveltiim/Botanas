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
    }
}
