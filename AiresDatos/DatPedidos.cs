using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresDatos
{
    public class DatPedidos : DatAbstracta
    {
        public DataTable obtieneReporteComprasVentas(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("[selReporteComprasVentasPorFechasPorEmpresa]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FechaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneReporteComprasVentas(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta, int ProductoId)
        {
            try
            {
                com = new SqlCommand("[selReporteComprasVentasPorFechasProductoPorEmpresa]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FechaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtienePedido(int PedidoId)
        {
            try
            {
                com = new SqlCommand("selObtienePedidoPorId", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtienePedidosPorEstablecimiento(int EstablecimientoId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("selObtienePedidosPorEstablecimientoFechasNeue", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FechaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtienePedidosPorEstablecimiento(int EstablecimientoId, DateTime FechaDesde, DateTime FechaHasta,
                                                          int TipoPedidoId)
        {
            try
            {
                com = new SqlCommand("selObtienePedidosPorEstablecimientoFechasTipoPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FechaHasta);
                com.Parameters.AddWithValue("TipoPedidoId", TipoPedidoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtienePedidosPreVenta(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("[selObtienePedidosPreVentaPorEmpresaFechas]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FechaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtienePedidoConPagos(int PedidoId)
        {
            try
            {
                com = new SqlCommand("[selObtienePedidoConPagos]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtieneCorte(int EstablecimientoId, int UsuarioId)
        {
            try
            {
                //4,1-- establecimientoid usuarioid
                com = new SqlCommand("spGetCorteAll", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
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
        public DataTable obtieneCorteMayoreo(int EstablecimientoId, int UsuarioId)
        {
            try
            {
                //4,1-- establecimientoid usuarioid
                com = new SqlCommand("spGetCorteMayoreoAll", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
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
        public DataTable obtieneCorteDetalle(int EstablecimientoId, int UsuarioId)
        {
            try
            {
                //4,1-- establecimientoid usuarioid
                com = new SqlCommand("spGetCorteDetalleAll", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
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
        public DataTable obtieneCorteDetalleMayoreo(int EstablecimientoId, int UsuarioId)
        {
            try
            {
                //4,1-- establecimientoid usuarioid
                com = new SqlCommand("spGetCorteDetalleMayoreoAll", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
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
        public DataTable agregaCorte(int EstablecimientoId, int UsuarioId)
        {
            try
            {
                //4,1-- establecimientoid usuarioid
                com = new SqlCommand("spCorteAll", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
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
        public DataTable agregaCorteMayoreo(int EstablecimientoId, int UsuarioId)
        {
            try
            {
                //4,1-- establecimientoid usuarioid
                com = new SqlCommand("spCorteMayoreoAll", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
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

        public DataTable obtienePedidosClientesConCostosPorProducto(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("selObtienePedidosClientesPorFechasPorEmpresaConCostosPorProducto", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FechaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneProductosPedidosPreventa(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("[selObtieneProductosPedidosPreVentaPorEmpresaFechas]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FechaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtienePedidosClientesCredito()
        {
            try
            {
                com = new SqlCommand("[selObtienePedidosClientesDeuda]", con);
                com.CommandType = CommandType.StoredProcedure;
                //com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtienePedidosClientesCredito(string ClienteRFC)
        {
            try
            {
                com = new SqlCommand("[selObtienePedidosClientesDeudaPorRFC]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("RFC", ClienteRFC);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtienePedidosClientesCredito(int EstablecimientoId)
        {
            try
            {
                com = new SqlCommand("[selObtienePedidosClientesDeudaPorEstablecimientoNeue]", con);
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
        public DataTable obtienePedidosClientesFacturaCredito(int EstablecimientoId)
        {
            try
            {
                com = new SqlCommand("[selObtienePedidosClientesFacturaDeudaPorEstablecimiento]", con);
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
        /// <summary>
        /// MEJORA DE selObtienePedidosClientesDeudaPorEstablecimientoNeue
        /// INCLUYE DIASDECREDITO,FECHAVENCIMIENTO,ETC.
        /// </summary>
        /// <param name="EstablecimientoId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public DataTable obtienePedidosClientesCreditoDiasCredito(int EstablecimientoId)
        {
            try
            {
                //com = new SqlCommand("[selObtienePedidosClientesDeudaPorEstablecimientoCliente]", con);
                com = new SqlCommand("[selObtienePedidosClientesDeuda_Establecimiento]", con);
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
        /// <summary>
        /// INCLUYE DIASDECREDITO,FECHAVENCIMIENTO,ETC.
        /// </summary>
        /// <param name="EstablecimientoId"></param>
        /// <param name="ClienteId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public DataTable obtienePedidosClientesCredito(int EstablecimientoId, int ClienteId)
        {
            try
            {
                //com = new SqlCommand("[selObtienePedidosClientesDeudaPorEstablecimientoCliente]", con);
                com = new SqlCommand("[selObtienePedidosClientesDeuda_EstablecimientoCliente]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
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

        /// <summary>
        /// OBTIENE SOLO FACTURAS CON FAC_CLIENTEFACTURAID > 0.
        /// </summary>
        /// <param name="ClienteId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public DataTable obtienePedidosClienteFacturaCredito()
        {
            try
            {
                com = new SqlCommand("[selObtienePedidosClientesDeuda_ClienteFacturaTodos]", con);
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
        /// <summary>
        /// OBTIENE SOLO FACTURAS CON FAC_CLIENTEFACTURAID.
        /// </summary>
        /// <param name="ClienteId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public DataTable obtienePedidosClienteFacturaCredito(int ClienteId)
        {
            try
            {
                com = new SqlCommand("[selObtienePedidosClientesDeuda_ClienteFactura]", con);
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

        /// <summary>
        /// SOLO SE USA EN REPORTES
        /// </summary>
        /// <param name="EmpresaId"></param>
        /// <param name="FechaDesde"></param>
        /// <param name="FechaHasta"></param>
        /// <returns></returns>
        public DataTable obtienePedidosClientes(int EmpresaId,DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("[selObtienePedidosClientesPorFechasPorEmpresaConCostoNeue]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FechaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
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
        public DataTable obtienePedidosClientes(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta,
                                                int EstatusId)
        {
            try
            {
                com = new SqlCommand("[selObtienePedidosClientesPorFechasEstatusPorEmpresa]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FechaHasta);
                com.Parameters.AddWithValue("EstatusId", EstatusId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtienePedidosPorFactura(int FacturaId)
        {
            try
            {
                com = new SqlCommand("[selObtienePedidosPorFactura]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FacturaId", FacturaId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtienePedidosClientes(int ClienteId)
        {
            try
            {
                com = new SqlCommand("selObtienePedidosClientesPorCliente", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ClienteId", ClienteId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtieneSiguienteFactura(int EmpresaId, int PedidoId, string SerieFactura)
        {
            try
            {
                com = new SqlCommand("[selObtieneSiguienteFacturaPorEmpresa]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("SerieFactura", SerieFactura);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneUltimaFactura(int EmpresaId)
        {
            try
            {
                com = new SqlCommand("selObtieneUltimaFacturaPorEmpresa", con);
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
        public DataTable obtieneUltimaNotaCredito(int EmpresaId)
        {
            try
            {
                com = new SqlCommand("selObtieneUltimaNotaCreditoPorEmpresa", con);
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

        public DataTable obtieneNotasCreditoPedidos(int ClienteId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("selObtieneNotasCreditoPedidosPorClienteFechas", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ClienteId", ClienteId);
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FechaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// SE USA PARA PEDIDOS QUE NO SON NECESARIAMENTE DE VENTA. COMO SALIDAS POR AJUSTE O SALIDA DE INSUMOS.
        /// </summary>
        /// <param name="TipoPedidoId"></param>
        /// <param name="ClienteId"></param>
        /// <param name="Detalle"></param>
        /// <param name="Solicitud"></param>
        /// <param name="Observaciones"></param>
        /// <param name="Total"></param>
        /// <param name="Pago"></param>
        /// <param name="Fecha"></param>
        /// <param name="FechaEntrega"></param>
        /// <param name="EmpleadoId"></param>
        /// <param name="Facturado"></param>
        /// <param name="EstatusId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int agregaPedido(int TipoPedidoId, int ClienteId, string Detalle, string Solicitud, string Observaciones, decimal Total, decimal Pago, DateTime Fecha, DateTime FechaEntrega, int EmpleadoId, bool Facturado, int EstatusId)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("insAgregaPedidoNeue", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("TipoPedidoId", TipoPedidoId);
                com.Parameters.AddWithValue("ClienteId", ClienteId);
                com.Parameters.AddWithValue("Detalle", Detalle);
                com.Parameters.AddWithValue("Solicitud", Solicitud);
                com.Parameters.AddWithValue("Observaciones", Observaciones);
                com.Parameters.AddWithValue("Total", Total);
                com.Parameters.AddWithValue("Pago", Pago);
                com.Parameters.AddWithValue("Fecha", Fecha);
                com.Parameters.AddWithValue("FechaEntrega", FechaEntrega);
                com.Parameters.AddWithValue("EmpleadoId", EmpleadoId);
                com.Parameters.AddWithValue("Facturado", Facturado);
                com.Parameters.AddWithValue("EstatusId", EstatusId);
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
        /// <summary>
        /// SE USA EXCLUSIVAMENTE PARA PEDIDOS DE SALIDA DE VENTAS. (VENTAS PG, VENTAS MAYOREO, ETC.)
        /// </summary>
        /// <param name="EstablecimientoId"></param>
        /// <param name="EmpresaId"></param>
        /// <param name="TipoPedidoId"></param>
        /// <param name="ClienteId"></param>
        /// <param name="Detalle"></param>
        /// <param name="Observaciones"></param>
        /// <param name="IEPS"></param>
        /// <param name="Total"></param>
        /// <param name="Pago"></param>
        /// <param name="Fecha"></param>
        /// <param name="FechaEntrega"></param>
        /// <param name="EmpleadoId"></param>
        /// <param name="Facturado"></param>
        /// <param name="TrabajadorId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int agregaPedidoVenta(int EstablecimientoId, int EmpresaId, int TipoPedidoId, int ClienteId, string Detalle, 
                                        string Observaciones, decimal IEPS, decimal Total, decimal Pago, 
                                        DateTime Fecha, DateTime FechaEntrega, int EmpleadoId, bool Facturado,
                                        int TrabajadorId)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("insAgregaPedidoVenta", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("TipoPedidoId", TipoPedidoId);
                com.Parameters.AddWithValue("ClienteId", ClienteId);
                com.Parameters.AddWithValue("Detalle", Detalle);
                com.Parameters.AddWithValue("Observaciones", Observaciones);
                com.Parameters.AddWithValue("IEPS", IEPS);
                com.Parameters.AddWithValue("Total", Total);
                com.Parameters.AddWithValue("Pago", Pago);
                com.Parameters.AddWithValue("Fecha", Fecha);
                com.Parameters.AddWithValue("FechaEntrega", FechaEntrega);
                com.Parameters.AddWithValue("EmpleadoId", EmpleadoId);
                com.Parameters.AddWithValue("Facturado", Facturado);
                com.Parameters.AddWithValue("TrabajadorId", TrabajadorId);
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
        public int agregaPedidoPreVenta(int EmpresaId, int NumeroPedido, int PedidoRelacionId, 
                                        int TipoPedidoId, int ClienteId, 
                                        int CondicionPagoId, string Detalle, string Observaciones,
                                        decimal Total, decimal IVA, decimal IEPS, decimal IvaRetenido, decimal IsrRetenido,
                                        DateTime Fecha, DateTime FechaEntrega, int TrabajadorId,
                                        int UsuarioId)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("[insAgregaPedidoPreVenta]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("NumeroPedido", NumeroPedido);
                com.Parameters.AddWithValue("PedidoRelacionId", PedidoRelacionId);
                com.Parameters.AddWithValue("TipoPedidoId", TipoPedidoId);
                com.Parameters.AddWithValue("ClienteId", ClienteId);
                com.Parameters.AddWithValue("CondicionPagoId", CondicionPagoId);
                com.Parameters.AddWithValue("Detalle", Detalle);
                com.Parameters.AddWithValue("Observaciones", Observaciones);
                com.Parameters.AddWithValue("Total", Total);
                com.Parameters.AddWithValue("IVA", IVA);
                com.Parameters.AddWithValue("IEPS", IEPS);
                com.Parameters.AddWithValue("IvaRetenido", IvaRetenido);
                com.Parameters.AddWithValue("IsrRetenido", IsrRetenido);
                com.Parameters.AddWithValue("Fecha", Fecha);
                com.Parameters.AddWithValue("FechaEntrega", FechaEntrega);
                com.Parameters.AddWithValue("TrabajadorId", TrabajadorId);
                com.Parameters.AddWithValue("UsuarioId", UsuarioId);
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

        public int agregaMovimientoMaster(string Comentario, int TipoMovimientoId, int Orientacion, int AlmacenId, int PedidoId, 
                                            int UsuarioId, int ProveedorId = 0)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("spMovimientoMaster", con);
                //com = new SqlCommand("AgregaPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("Comentario", Comentario);
                com.Parameters.AddWithValue("TIPOMOVIMIENTOINVENTARIOID", TipoMovimientoId);
                com.Parameters.AddWithValue("Orientacion", Orientacion);
                com.Parameters.AddWithValue("AlmacenId", AlmacenId);
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("UsuarioId", UsuarioId);
                com.Parameters.AddWithValue("ProveedorId", ProveedorId);
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
        public int agregaMovimientoMaster(string Comentario, int TipoMovimientoId, int Orientacion, int AlmacenId, int PedidoId,
                                           DateTime FechaMovimiento,
                                           int UsuarioId, int ProveedorId = 0)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("spMovimientoMasterFecha", con);
                //com = new SqlCommand("AgregaPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("Comentario", Comentario);
                com.Parameters.AddWithValue("TIPOMOVIMIENTOINVENTARIOID", TipoMovimientoId);
                com.Parameters.AddWithValue("Orientacion", Orientacion);
                com.Parameters.AddWithValue("AlmacenId", AlmacenId);
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("UsuarioId", UsuarioId);
                com.Parameters.AddWithValue("ProveedorId", ProveedorId);
                com.Parameters.AddWithValue("FechaMovimiento", FechaMovimiento);
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
        public int agregaMovimientoDetalle(int MovimientoInventarioId, int ProductoId, decimal Cantidad, decimal Total)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("spMovimientoDetalle", con);
                //com = new SqlCommand("AgregaPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("MovimientoInventarioId", MovimientoInventarioId);
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("Cantidad", Cantidad);
                com.Parameters.AddWithValue("Total", Total);
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
        public int agregaMovimientoLote(int MovimientoId, int Orientacion)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("spMovimientoLote", con);
                //com = new SqlCommand("AgregaPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("MovimientoId", MovimientoId);
                com.Parameters.AddWithValue("Orientacion", Orientacion);
                con.Open();
                com.ExecuteNonQuery();

                return Id;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public int agregaMovimientoTraspaso(int TraspasoId, int MovimientoId, int AlmacenOrigenId, int AlmacenDestinoId, 
                                            string Comentario, int Estado, int UsuarioId)
        {
            try
            {
                com = new SqlCommand("spMovimientoTraspaso", con);
                //com = new SqlCommand("AgregaPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("TraspasoId", TraspasoId);
                com.Parameters.AddWithValue("MovimientoId", MovimientoId);
                com.Parameters.AddWithValue("AlmacenOrigenId", AlmacenOrigenId);
                com.Parameters.AddWithValue("AlmacenDestinoId", AlmacenDestinoId);
                com.Parameters.AddWithValue("Comentario", Comentario);
                com.Parameters.AddWithValue("Estado", Estado);
                com.Parameters.AddWithValue("UsuarioId", UsuarioId);
                //SqlParameter parm = new SqlParameter("Id", Id);
                //parm.Direction = ParameterDirection.InputOutput;
                //com.Parameters.Add(parm);
                con.Open();
                com.ExecuteNonQuery();

                return Convert.ToInt32(com.Parameters["TraspasoId"].Value);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public int agregaMovimientoTraspasoConsigna(int TraspasoId, int MovimientoId, int AlmacenOrigenId, int UsuarioDestinoId,
                                            string Comentario, int Estado, int UsuarioId)
        {
            try
            {
                com = new SqlCommand("spMovimientoTraspasoConsigna", con);
                //com = new SqlCommand("AgregaPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("TraspasoId", TraspasoId);
                com.Parameters.AddWithValue("MovimientoId", MovimientoId);
                com.Parameters.AddWithValue("AlmacenOrigenId", AlmacenOrigenId);
                com.Parameters.AddWithValue("UsuarioDestinoId", UsuarioDestinoId);
                com.Parameters.AddWithValue("Comentario", Comentario);
                com.Parameters.AddWithValue("Estado", Estado);
                com.Parameters.AddWithValue("UsuarioId", UsuarioId);
                //SqlParameter parm = new SqlParameter("Id", Id);
                //parm.Direction = ParameterDirection.InputOutput;
                //com.Parameters.Add(parm);
                con.Open();
                com.ExecuteNonQuery();

                return Convert.ToInt32(com.Parameters["TraspasoId"].Value);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }



        public void actualizaEstatusPago(int PedidoId, DateTime FechaPago, decimal Pago, bool Estatus)
        {
            try
            {
                com = new SqlCommand("[updActualizaEstatusPagoPorFechaPagoPedido]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("Pago", Pago);
                com.Parameters.AddWithValue("FechaPago", FechaPago);
                com.Parameters.AddWithValue("Estatus", Estatus);
                con.Open();
                com.ExecuteNonQuery();
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void agregaProductoPedido(int PedidoId, int ProductoId, decimal ProductoCantidad, decimal ProductoPrecio, string Detalle)
        {
            try
            {
                com = new SqlCommand("insAgregaProductoPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("ProductoCantidad", ProductoCantidad);
                com.Parameters.AddWithValue("ProductoPrecio", ProductoPrecio);
                com.Parameters.AddWithValue("Detalle", Detalle);
                con.Open();
                com.ExecuteNonQuery();

                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public void agregaProductoDetallePedido(int PedidoId, int ProductoId, decimal Cantidad, decimal PrecioCosto, decimal PrecioVenta)
        {
            try
            {
                com = new SqlCommand("insAgregaProductosDetallePedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("Cantidad", Cantidad);
                com.Parameters.AddWithValue("PrecioCosto", PrecioCosto);
                com.Parameters.AddWithValue("PrecioVenta", PrecioVenta);
                con.Open();
                com.ExecuteNonQuery();

                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void agregaProductoPedidoPreVenta(int PedidoPreVentaId, int ProductoId, 
                                                decimal Cantidad, decimal IEPS, decimal IVA, decimal PrecioVenta,
                                                string Descripcion)
        {
            try
            {
                com = new SqlCommand("insAgregaProductosPedidoPreVenta", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoPreVentaId", PedidoPreVentaId);
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("Cantidad", Cantidad);
                com.Parameters.AddWithValue("IEPS", IEPS);
                com.Parameters.AddWithValue("IVA", IVA);
                com.Parameters.AddWithValue("PrecioVenta", PrecioVenta);
                com.Parameters.AddWithValue("Descripcion", Descripcion);
                con.Open();
                com.ExecuteNonQuery();

                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        //MOVER A DatPagos
        public DataTable obtienePagosPorCliente(int ClienteId)
        {
            try
            {
                com = new SqlCommand("[selObtienePagosPorCliente]", con);
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
        public int agregaPago(int PedidoId, int TipoPagoId, decimal Pago, DateTime FechaPago, int FormaPagoId, string FormaPago, int UsuarioId)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("AgregaPagoNeue", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("TipoPagoId", TipoPagoId);
                com.Parameters.AddWithValue("Pago", Pago);
                com.Parameters.AddWithValue("FechaPago", FechaPago);
                com.Parameters.AddWithValue("FormaPagoId", FormaPagoId);
                com.Parameters.AddWithValue("FormaPago", FormaPago);
                com.Parameters.AddWithValue("UsuarioId", UsuarioId);
                SqlParameter parm = new SqlParameter("Id", Id);
                parm.Direction = ParameterDirection.InputOutput;
                com.Parameters.Add(parm);
                con.Open();
                com.ExecuteNonQuery();

                return Convert.ToInt32(com.Parameters["Id"].Value);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public int agregaPagoPedido(int EstablecimientoId, int PedidoId, int TipoPagoId, decimal Pago, DateTime FechaPago, int FormaPagoId, string FormaPago, int UsuarioId)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("AgregaPagoPedidoEstablecimiento", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("TipoPagoId", TipoPagoId);
                com.Parameters.AddWithValue("Pago", Pago);
                com.Parameters.AddWithValue("FechaPago", FechaPago);
                com.Parameters.AddWithValue("FormaPagoId", FormaPagoId);
                com.Parameters.AddWithValue("FormaPago", FormaPago);
                com.Parameters.AddWithValue("UsuarioId", UsuarioId);
                SqlParameter parm = new SqlParameter("Id", Id);
                parm.Direction = ParameterDirection.InputOutput;
                com.Parameters.Add(parm);
                con.Open();
                com.ExecuteNonQuery();

                return Convert.ToInt32(com.Parameters["Id"].Value);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public DataTable obtienePagosClientes(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("selObtienePagosClientesPorFechaPagoPorEmpresa", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FechaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtienePagosClientesSinFactura(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("[selObtienePagosClientesPorFechaPagoPorEmpresaSinFactura]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FechaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtienePagosClientesSinDeposito()
        {
            try
            {
                com = new SqlCommand("selObtienePagosClientesSinDeposito", con);
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
        public void actualizaPago(int PagoId, decimal Pago, DateTime FechaPago, bool Estatus)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusPago", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PagoId", PagoId);
                com.Parameters.AddWithValue("Pago", Pago);
                com.Parameters.AddWithValue("FechaPago", FechaPago);
                com.Parameters.AddWithValue("Estatus", Estatus);
                con.Open();
                com.ExecuteNonQuery();
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaEstatusPago(int PagoId, bool Estatus)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusPago", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PagoId", PagoId);
                com.Parameters.AddWithValue("Estatus", Estatus);
                con.Open();
                com.ExecuteNonQuery();
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void corrigePago(int PagoId, decimal Pago, DateTime FechaPago)
        {
            try
            {
                com = new SqlCommand("[updCorrigePago]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PagoId", PagoId);
                com.Parameters.AddWithValue("Pago", Pago);
                com.Parameters.AddWithValue("FechaPago", FechaPago);
                con.Open();
                com.ExecuteNonQuery();
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        //MOVER A DatFactura
        //public int agregaFactura(int PedidoId, string UUID,
        //                            int TipoComprobanteId, int FormaPagoId, int MetodoPagoId, int UsoCFDIId,
        //                            DateTime FechaPago, string Ruta)
        //{
        //    try
        //    {
        //        int Id = 0;

        //        com = new SqlCommand("insAgregaFacturaPedido33", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("PedidoId", PedidoId);
        //        com.Parameters.AddWithValue("UUID", UUID);

        //        com.Parameters.AddWithValue("TipoComprobanteId", TipoComprobanteId);
        //        com.Parameters.AddWithValue("FormaPagoId", FormaPagoId);
        //        com.Parameters.AddWithValue("MetodoPagoId", MetodoPagoId);
        //        com.Parameters.AddWithValue("UsoCFDIId", UsoCFDIId);

        //        com.Parameters.AddWithValue("FechaPago", FechaPago);
        //        com.Parameters.AddWithValue("Ruta", Ruta);
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
        public void actualizaEstatusFacturaPedido(int FacturaId, int EstatusId)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusFacturaPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FacturaId", FacturaId);
                com.Parameters.AddWithValue("EstatusId", EstatusId);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public DataTable obtieneFacturasPorPedido(int PedidoId)
        {
            try
            {
                com = new SqlCommand("selObtieneFacturasPorPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public void actualizaPedido(int PedidoId, string Detalle, decimal Total, decimal Pago, DateTime FechaEntrega, int EstatusId)
        {
            try
            {
                com = new SqlCommand("updActualizaPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("Detalle", Detalle);
                com.Parameters.AddWithValue("Total", Total);
                com.Parameters.AddWithValue("Pago", Pago);
                com.Parameters.AddWithValue("FechaEntrega", FechaEntrega);
                com.Parameters.AddWithValue("EstatusId", EstatusId);
                con.Open();
                com.ExecuteNonQuery();
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaPedido(int PedidoId, bool Facturado)
        {
            try
            {
                com = new SqlCommand("updActualizaPedidoFacturado", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("Facturado", Facturado);
                con.Open();
                com.ExecuteNonQuery();
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaDetallePedido(int PedidoId, string Detalle)
        {
            try
            {
                com = new SqlCommand("updActualizaDetallePedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("Detalle", Detalle);
                con.Open();
                com.ExecuteNonQuery();
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaProductoDetallePedido(int ProductoDetalleId, decimal PrecioCosto, decimal PrecioVenta)
        {
            try
            {
                com = new SqlCommand("updActualizaProductoDetallePedidoPorProductoDetalle", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProductoDetalleId", ProductoDetalleId);
                com.Parameters.AddWithValue("PrecioCosto", PrecioCosto);
                com.Parameters.AddWithValue("PrecioVenta", PrecioVenta);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public void actualizaPedidoPreVenta(int PedidoPreVentaId, int PedidoRelacionId, int @TipoPedidoId,
                                            string Detalle, string Observaciones, int @TrabajadorId, int EstatusId)
        {
            try
            {
                com = new SqlCommand("[updActualizaPedidoPreVenta]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoPreVentaId", PedidoPreVentaId);
                com.Parameters.AddWithValue("PedidoRelacionId", PedidoRelacionId);
                com.Parameters.AddWithValue("TipoPedidoId", @TipoPedidoId);
                com.Parameters.AddWithValue("Detalle", Detalle);
                com.Parameters.AddWithValue("Observaciones", Observaciones);
                com.Parameters.AddWithValue("TrabajadorId", @TrabajadorId);
                com.Parameters.AddWithValue("EstatusId", EstatusId);
                con.Open();
                com.ExecuteNonQuery();
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public void actualizaEstatusPedido(int PedidoId, int EstatusId)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("EstatusId", EstatusId);
                con.Open();
                com.ExecuteNonQuery();
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaEstatusPedidoPreVenta(int PedidoPreVentaId, int EstatusId)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusPedidoPreVenta", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoPreVentaId", PedidoPreVentaId);
                com.Parameters.AddWithValue("EstatusId", EstatusId);
                con.Open();
                com.ExecuteNonQuery();
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaEstatusProductoDetallePedido(int PedidoId, bool Estatus)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusProductoDetallePedidoPorPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("Estatus", Estatus);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaEstatusProductoPedidoPreVenta(int @PedidoPreVentaId, int @EstatusId)
        {
            try
            {
                com = new SqlCommand("[updActualizaEstatusProductosPedidoPreVentaPorPedido]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoPreVentaId", @PedidoPreVentaId);
                com.Parameters.AddWithValue("EstatusId", @EstatusId);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public void aumentaPagoEnPedido(int PedidoId, decimal Pago)
        {
            try
            {
                com = new SqlCommand("[AumentaPagoEnPedido]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("Pago", Pago);
                con.Open();
                com.ExecuteNonQuery();
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void aumentaPagoPedido(int PedidoId, decimal Pago)
        {
            try
            {
                com = new SqlCommand("updAumentaPagoPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("Pago", Pago);
                con.Open();
                com.ExecuteNonQuery();
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void aumentaNotaCreditoPedido(int PedidoId, decimal Pago, int UsuarioId)
        {
            try
            {
                com = new SqlCommand("[updAumentaNotaCreditoPedido]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("Pago", Pago);
                com.Parameters.AddWithValue("UsuarioId", UsuarioId);
                con.Open();
                com.ExecuteNonQuery();
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public int agregaNotaCredito(int EmpresaId, int PedidoId, int FacturaId, int TipoId, string Descripcion, 
                                    string Serie, string Numero, DateTime Fecha, decimal Monto, string UUID, 
                                    string Ruta, byte[] PDF, byte[] XML,
                                    int FormaPagoId, int MonedaId, decimal TipoCambio, int UsuarioId)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("[insAgregaNotaDeCreditoPedidoNeue]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("FacturaId", FacturaId);
                com.Parameters.AddWithValue("TipoId", TipoId);
                com.Parameters.AddWithValue("Descripcion", Descripcion);
                com.Parameters.AddWithValue("Serie", Serie);
                com.Parameters.AddWithValue("Numero", Numero);
                com.Parameters.AddWithValue("Fecha", Fecha);
                com.Parameters.AddWithValue("Monto", Monto);
                com.Parameters.AddWithValue("UUID", UUID);
                com.Parameters.AddWithValue("Ruta", Ruta);
                com.Parameters.AddWithValue("PDF", PDF);
                com.Parameters.AddWithValue("XML", XML);
                com.Parameters.AddWithValue("FormaPagoId", FormaPagoId);
                com.Parameters.AddWithValue("MonedaId", MonedaId);
                com.Parameters.AddWithValue("TipoCambio", TipoCambio);
                com.Parameters.AddWithValue("UsuarioId", UsuarioId);
                SqlParameter parm = new SqlParameter("Id", Id);
                parm.Direction = ParameterDirection.InputOutput;
                com.Parameters.Add(parm);
                con.Open();
                com.ExecuteNonQuery();

                return Convert.ToInt32(com.Parameters["Id"].Value);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }


        /// <summary>
        /// CANCELA PAGOS, PRODUCTOSDETALLEPEDIDO, PEDIDO
        /// </summary>
        /// <param name="PedidoId"></param>
        public void CancelaPedidoTodo(int PedidoId)
        {
            try
            {
                com = new SqlCommand("[updCancelaPedidoTodo]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                con.Open();
                com.ExecuteNonQuery();
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        /// <summary>
        /// ELIMINA PAGOS, PRODUCTOSDETALLEPEDIDO, PEDIDO
        /// </summary>
        /// <param name="PedidoId"></param>
        public void eliminaPedidoTodo(int PedidoId)
        {
            try
            {
                com = new SqlCommand("[delEliminaPedidoTodo]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                con.Open();
                com.ExecuteNonQuery();
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void eliminaPedidoPreVentaTodo(int PedidoPreVentaId)
        {
            try
            {
                com = new SqlCommand("[delEliminaPedidoPreVentaTodo]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoPreVentaId", PedidoPreVentaId);
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
