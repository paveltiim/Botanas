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
        public DataTable obtienePedidos(int EmpresaId, int Pendientes, int Pagados, int Entregados, int Cancelados, int Presupuesto)
        {
            try
            {
                com = new SqlCommand("selObtienePedidosFiltrosPorEmpresa", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("Pendientes", Pendientes);
                com.Parameters.AddWithValue("Pagados", Pagados);
                com.Parameters.AddWithValue("Entregados", Entregados);
                com.Parameters.AddWithValue("Cancelados", Cancelados);
                com.Parameters.AddWithValue("Presupuesto", Presupuesto);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtienePedidosPorFechas(int Pendientes, int Pagados, int Entregados, int Cancelados, int Presupuesto, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("selObtienePedidosFiltrosPorFechas", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("Pendientes", Pendientes);
                com.Parameters.AddWithValue("Pagados", Pagados);
                com.Parameters.AddWithValue("Entregados", Entregados);
                com.Parameters.AddWithValue("Cancelados", Cancelados);
                com.Parameters.AddWithValue("Presupuesto", Presupuesto);
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

        public DataTable obtienePedidosPorFechasEntrega(int Pendientes, int Pagados, int Entregados, int Cancelados, int Presupuesto, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("selObtienePedidosFiltrosPorFechasEntrega", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("Pendientes", Pendientes);
                com.Parameters.AddWithValue("Pagados", Pagados);
                com.Parameters.AddWithValue("Entregados", Entregados);
                com.Parameters.AddWithValue("Cancelados", Cancelados);
                com.Parameters.AddWithValue("Presupuesto", Presupuesto);
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

        public DataTable obtieneProductosPorPedido(int PedidoId)
        {
            try
            {
                com = new SqlCommand("selObtieneProductosPorPedido", con);
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

        public DataTable obtienePedidosPorCliente(int ClienteId)
        {
            try
            {
                com = new SqlCommand("selObtienePedidosPorCliente", con);
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

        public DataTable obtienePedidosFacturado()
        {
            try
            {
                com = new SqlCommand("selObtienePedidosFacturado", con);
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

        public DataTable obtienePedidosClientesCredito()
        {
            try
            {
                com = new SqlCommand("selObtienePedidosClientesDeuda", con);
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
        public DataTable obtienePedidosClientesCredito(int ClienteId)
        {
            try
            {
                com = new SqlCommand("selObtienePedidosClienteDeuda", con);
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
        public DataTable obtienePedidosClientesCredito(DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("selObtienePedidosClientesDeudaPorFechas", con);
                com.CommandType = CommandType.StoredProcedure;
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
        public DataTable obtienePedidosClientesCredito(DateTime FechaLimite)
        {
            try
            {
                com = new SqlCommand("selObtienePedidosClientesDeudaAFecha", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FechaLimite", FechaLimite);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtienePedidosClientes(int EmpresaId,DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("selObtienePedidosClientesPorFechasPorEmpresa", con);
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
        public DataTable obtieneUltimaFactura()
        {
            try
            {
                com = new SqlCommand("selObtieneUltimaFactura", con);
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

        public int agregaPedido(int ClienteId, string Detalle, string Observaciones, decimal Total, decimal Pago, DateTime Fecha, DateTime FechaEntrega, int EmpleadoId, bool Facturado, int EstatusId)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("insAgregaPedido", con);
                //com = new SqlCommand("AgregaPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ClienteId", ClienteId);
                com.Parameters.AddWithValue("Detalle", Detalle);
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

        public void agregaProductoDetallePedido(int PedidoId, int ProductoId, decimal Cantidad, decimal PrecioCosto, decimal PrecioVenta, DateTime FechaRegistro)
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
                com.Parameters.AddWithValue("FechaRegistro", FechaRegistro);
                con.Open();
                com.ExecuteNonQuery();

                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public int agregaNotaCredito(int EmpresaId, int PedidoId, string Numero, decimal Cantidad, DateTime Fecha)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("insAgregaNotaDeCreditoPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("Numero", Numero);
                com.Parameters.AddWithValue("Cantidad", Cantidad);
                com.Parameters.AddWithValue("Fecha", Fecha);
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


        //MOVER A DatPagos
        public int agregaPagoPedido(int PedidoId, int TipoPagoId, decimal Pago, DateTime FechaPago)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("AgregaPago", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                com.Parameters.AddWithValue("TipoPagoId", TipoPagoId);
                com.Parameters.AddWithValue("Pago", Pago);
                com.Parameters.AddWithValue("FechaPago", FechaPago);
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
        public DataTable obtienePagosPorCliente(int ClienteId)
        {
            try
            {
                com = new SqlCommand("selObtienePagosPorCliente", con);
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
        public DataTable obtienePagosClientes(DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("selObtienePagosClientesPorFechaPago", con);
                com.CommandType = CommandType.StoredProcedure;
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


        public DataTable obtieneUltimoComplemento()
        {
            try
            {
                com = new SqlCommand("selObtieneUltimoComplementoPago", con);
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
        public int agregaComplementoPago(int FacturaId, DateTime Fecha,
                                        int TipoComprobanteId, int FormaPagoId, string Ruta)
        {
            try
            {
                int Id = 0;

                com = new SqlCommand("insAgregaComplementoPagoFactura", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FacturaId", FacturaId);
                com.Parameters.AddWithValue("Fecha", Fecha);
                com.Parameters.AddWithValue("TipoComprobanteId", TipoComprobanteId);
                com.Parameters.AddWithValue("FormaPagoId", FormaPagoId);
                com.Parameters.AddWithValue("Ruta", Ruta);
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
    }
}
