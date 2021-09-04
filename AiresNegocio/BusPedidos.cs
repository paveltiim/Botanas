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
        public List<EntPedido> ObtieneReporteComprasVentas(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta, int ProductoId)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtieneReporteComprasVentas(EmpresaId, FechaDesde, FechaHasta, ProductoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PRODET_ID"]);
                    p.FechaEntrega = Convert.ToDateTime(r["FECHACOMPRA"]);
                    p.Descripcion = r["FACTURACOMPRA"].ToString();
                    p.FormaPagoId = Convert.ToInt32(r["PRO_ID"]);
                    p.Detalle = r["PRO_CODIGO"].ToString() + " - " + r["PRO_DESCRIPCION"].ToString();

                    p.UUID = r["PRO_MARCA"].ToString();
                    p.Observaciones = r["PRO_MODELO"].ToString();

                    p.RutaFactura = r["PRODET_SERIE"].ToString();

                    p.Total = Convert.ToDecimal(r["PRODET_PRECIOCOSTO"]);

                    p.ClienteId = Convert.ToInt32(r["CLI_ID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString() + " - " + r["CLI_RFC"].ToString();

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

        public List<EntPedido> ObtienePedidos(int EmpresaId, int Pendientes, int Pagados, int Entregados, int Cancelados, int Presupuesto)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePedidos(EmpresaId,Pendientes, Pagados, Entregados, Cancelados, Presupuesto);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PED_ID"]);
                    p.Detalle = r["PED_DETALLE"].ToString();
                    p.Observaciones = r["PED_OBSERVACIONES"].ToString();
                    p.Cliente = r["CLI_NOMBRE"].ToString();
                    p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.Pago = Convert.ToDecimal(r["PED_PAGO"]);
                    //p.PagoTotal = Convert.ToDecimal(r["PED_PAGO"]);
                    //p.Descuento = p.Total - p.PagoTotal;

                    p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    p.FechaCorta = Convert.ToDateTime(r["PED_FECHA"]).ToShortDateString();
                    p.EmpleadoId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    //p.Empleado = r["EMP_NOMBRE"].ToString();
                    //p.Facturado = Convert.ToBoolean(r["PED_FACTURADO"]);
                    p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    p.FacturaId = Convert.ToInt32(r["FAC_ID"]);
                    p.Facturado = Convert.ToBoolean(r["FACTURADO"]);
                    p.UUID = r["FAC_UUID"].ToString();
                    p.RutaFactura = r["FAC_RUTA"].ToString();
                    p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();//"AA" + r["FAC_NUMEROFACTURA"].ToString();
                    if (p.Facturado)
                    {
                        p.EstatusDescripcion = "VIGENTE";
                        p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                        p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                    }
                    else if (!string.IsNullOrWhiteSpace(p.UUID))
                    {
                        p.EstatusDescripcion = "CANCELADO";
                        p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                        p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();
                    }
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

        public List<EntPedido> ObtienePedidosClientesDeuda(int EmpresaId)
        {
            try
            {
                List<EntPedido> lst = new List<EntPedido>();
                dt = new DatPedidos().obtienePedidosClientesCredito(EmpresaId);
                foreach (DataRow r in dt.Rows)
                {
                    EntPedido p = new EntPedido();
                    p.Id = Convert.ToInt32(r["PED_ID"]);
                    p.Detalle = r["PED_DETALLE"].ToString();

                    p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();

                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.Pago = Convert.ToDecimal(r["PED_PAGO"]);
                    p.NotasCredito = Convert.ToDecimal(r["PED_NOTASCREDITO"]);
                    //if (Convert.ToInt32(r["CLI_TIPOPERSONAID"]) == 1)
                        p.IVARetencion = Convert.ToDecimal(r["RETENCION"]);
                    //p.Debe= Convert.ToDecimal(r["DEBE"]);

                    p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    p.FacturaId = Convert.ToInt32(r["FAC_ID"]);
                    p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                    p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                    p.FechaCorta = Convert.ToDateTime(r["FAC_FECHA"]).ToShortDateString();

                    p.Facturado = Convert.ToBoolean(r["FAC_ESTATUSID"]);
                    p.UUID = r["FAC_UUID"].ToString();
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
                    p.Detalle = r["PED_DETALLE"].ToString();
                    p.Solicitud = r["CLI_BANCO"].ToString();
                    p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();

                    p.Pago = Convert.ToDecimal(r["PAG_PAGO"]);
                    //p.NotasCredito = Convert.ToDecimal(r["NOTASCREDITO"]);
                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.FechaCorta = Convert.ToDateTime(r["PED_FECHA"]).ToShortDateString();
                    p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    //p.Facturado = Convert.ToBoolean(r["FAC_ID"]);
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

                    p.Pago = Convert.ToDecimal(r["PAG_PAGO"]);
                    //p.NotasCredito = Convert.ToDecimal(r["NOTASCREDITO"]);
                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.FechaCorta = Convert.ToDateTime(r["PED_FECHA"]).ToShortDateString();
                    p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                    p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();

                    //p.Facturado = Convert.ToBoolean(r["FAC_ID"]);
                    //if (p.Facturado)
                    //    p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();//"AA" + r["FAC_NUMEROFACTURA"].ToString();
                    //else
                    //    p.Factura = "S/F";
                    //p.UUID = r["FAC_UUID"].ToString();
                    //p.RutaFactura = r["FAC_RUTA"].ToString();
                    //p.FechaEntrega = Convert.ToDateTime(r["FAC_FECHA"]);

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
                    p.Detalle = r["PED_DETALLE"].ToString();
                    //p.Observaciones = r["PED_OBSERVACIONES"].ToString();
                    p.ClienteId = Convert.ToInt32(r["CLI_ID"]);
                    p.Cliente = r["CLI_NOMBRE"].ToString();

                    p.Pago = Convert.ToDecimal(r["PAG_PAGO"]);
                    //p.Debe = Convert.ToDecimal(r["DEBE"]);
                    p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                    p.FechaCorta = Convert.ToDateTime(r["PED_FECHA"]).ToShortDateString();
                    p.Fecha = Convert.ToDateTime(r["PED_FECHA"]); 
                    p.FechaPago= Convert.ToDateTime(r["PAG_FECHAPAGO"]);

                    p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();
                    //if (!string.IsNullOrWhiteSpace(r["FAC_ID"].ToString()))
                    //{
                    //    p.Factura = r["FAC_ID"].ToString();
                    //    p.Fecha= Convert.ToDateTime(r["FAC_FECHA"]);
                    //}
                    //else
                    //    p.Factura = p.Detalle;

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
                return new DatPedidos().agregaPedido(Pedido.ClienteId, Pedido.Detalle, Pedido.Solicitud, Pedido.Observaciones, Pedido.Total, Pedido.Pago, Pedido.Fecha, Pedido.FechaEntrega, Pedido.EmpleadoId, Pedido.Facturado, Pedido.EstatusId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int AgregaMovimientoMaster(string Comentario, int TipoMovimientoId, int Orientacion, int AlmacenId, int PedidoId,
                                            int UsuarioId)
        {
            try
            {
                return new DatPedidos().agregaMovimientoMaster(Comentario, TipoMovimientoId, Orientacion, AlmacenId, PedidoId, UsuarioId);
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
                new DatPedidos().agregaProductoDetallePedido(Pedido.Id, Producto.Id, Producto.Cantidad, Producto.PrecioCosto, Producto.PrecioVenta, Pedido.Fecha);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int AgregaNotaCredito(EntPago NotaCredito)
        {
            try
            {
                return new DatPedidos().agregaNotaCredito(NotaCredito.EmpresaId,NotaCredito.PedidoId, NotaCredito.Descripcion, NotaCredito.Cantidad, NotaCredito.Fecha);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        //MOVER A DatPagos
        /// <summary>
        /// Actualiza el Pago del Pedido.
        /// </summary>
        /// <param name="Pago">
        /// Propiedades Necesarias: Id, Pago.
        /// </param>
        public int AgregaPagoPedido(EntPago Pago)
        {
            try
            {
               return new DatPedidos().agregaPagoPedido(Pago.PedidoId,Pago.TipoPagoId, Pago.Cantidad,Pago.FechaPago);
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
        public void ActualizaEstatusPago(int PedidoId, DateTime FechaPago, decimal Pago, bool Estatus)
        {
            try
            {
                new DatPedidos().actualizaEstatusPago(PedidoId, FechaPago, Pago, Estatus);
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
                    p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    p.Descripcion = r["PED_DETALLE"].ToString();
                    p.Estatus = Convert.ToBoolean(r["PAG_ESTATUS"]);
                    p.Factura = r["FAC_SERIEFACTURA"].ToString() + r["FAC_NUMEROFACTURA"].ToString();//"AA" + r["FAC_NUMEROFACTURA"].ToString();
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

        public EntFactura ObtieneUltimaFactura()
        {
            try
            {
                dt = new DatPedidos().obtieneUltimaFactura();
                EntFactura p = new EntFactura();
                foreach (DataRow r in dt.Rows)
                {
                    p.Id = Convert.ToInt32(r["FAC_ID"]);
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
        public void ActualizaEstatusProductoDetallePedido(EntPedido Pedido)
        {
            try
            {
                new DatPedidos().actualizaEstatusProductoDetallePedido(Pedido.Id, Pedido.Estatus);
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


        public EntFactura ObtieneUltimoComplementoPago()
        {
            try
            {
                dt = new DatPedidos().obtieneUltimoComplemento();
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
                new DatPedidos().agregaComplementoPago(Complemento.Id, Complemento.Fecha,
                                                        Complemento.TipoComprobanteId, Complemento.FormaPagoId, Complemento.Ruta);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

    }
}
