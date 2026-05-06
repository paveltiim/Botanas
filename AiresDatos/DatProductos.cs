using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresDatos
{
    public class DatProductos : DatAbstracta
    {
        public DataTable obtieneProducto(int ProductoId)
        {
            try
            {
                com = new SqlCommand("selObtieneProducto", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtieneProductos(int EmpresaId)
        {
            try
            {
                com = new SqlCommand("selObtieneProductosPorEmpresa", con);
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
        public DataTable obtieneProductosPorTipo(int EmpresaId, int TipoProductoId)
        {
            try
            {
                com = new SqlCommand("selObtieneProductosPorTipoProductoEmpresa", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("TipoProductoId", TipoProductoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneProductosPorAlmacen(int CompañiaId, int ProductoId, int AlmacenId)
        {
            try
            {
                com = new SqlCommand("spExistenciaProductos", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", CompañiaId);
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("AlmacenId", AlmacenId);
                com.Parameters.AddWithValue("Tipo", 1);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneProductosPorAlmacen(int EmpresaId, int ProductoId, int AlmacenId, int TipoProductoId)
        {
            try
            {
                com = new SqlCommand("spExistenciaProductos", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("AlmacenId", AlmacenId);
                com.Parameters.AddWithValue("Tipo", 1);//INCLUYE SIN EXISTENCIA
                com.Parameters.AddWithValue("TipoProductoId", TipoProductoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneProductosPorAlmacenConPreciosVenta(int ProductoId, int EstablecimientoId, int TipoProductoId,
                                                                   int ClienteId)
        {
            try
            {
                com = new SqlCommand("spProductosVenta", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
                com.Parameters.AddWithValue("Tipo", 2);
                com.Parameters.AddWithValue("TipoProductoId", TipoProductoId);
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
        public DataTable obtieneProductosExistentesPorAlmacen(int EmpresaId, int ProductoId, int AlmacenId, int TipoProductoId)
        {
            try
            {
                com = new SqlCommand("spExistenciaProductos", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("AlmacenId", AlmacenId);
                com.Parameters.AddWithValue("Tipo", 2);//SOLO CON EXISTENCIAS
                com.Parameters.AddWithValue("TipoProductoId", TipoProductoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneProductosExistentesPorAlmacenConPreciosVenta(int ProductoId, int EstablecimientoId, int TipoProductoId, 
                                                                            int ClienteId)
        {
            try
            {
                com = new SqlCommand("spExistenciaProductosVenta", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
                com.Parameters.AddWithValue("Tipo", 2);
                com.Parameters.AddWithValue("TipoProductoId", TipoProductoId);
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
        public DataTable obtieneProductosExistentesMinMax(int EmpresaId, int AlmacenId)
        {
            try
            {
                com = new SqlCommand("spExistenciaStockMaxMin", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("AlmacenId", AlmacenId);
                com.Parameters.AddWithValue("TipoProductoId", 1);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        
        public DataTable obtieneProductosExistenciaConsigna(int EmpresaId, int TrabajadorId)
        {
            try
            {
                com = new SqlCommand("spExistenciaProductosConsigna", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProductoId", 0);
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("UsuarioId", TrabajadorId);
                com.Parameters.AddWithValue("Tipo", 1);//INCLUYE SIN EXISTENCIA
                com.Parameters.AddWithValue("TipoProductoId", 1);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtieneListaPreciosProducto(int EstablecimientoId, int ProductoId)
        {
            try
            {
                com = new SqlCommand("spGetPrecioProductoEstablecimiento", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtienePrecioProductoPorCantidad(int EstablecimientoId, int ProductoId, decimal Cantidad)
        {
            try
            {
                com = new SqlCommand("[spGetPrecioProductoCantidad]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
                com.Parameters.AddWithValue("Cantidad", Cantidad);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtieneProductosProveedor(int EmpresaId, int EstatusId)
        {
            try
            {
                com = new SqlCommand("selObtieneProductosProveedorPorEmpresaEstatus", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("EstatusId", EstatusId);
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
        /// SOLO CON ESTATUSID=1
        /// </summary>
        /// <returns></returns>
        public DataTable obtieneProductosDetalle()
        {
            try
            {
                com = new SqlCommand("selObtieneProductosDetalleActivos", con);
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
        /// SOLO CON ESTATUSID=1
        /// </summary>
        /// <returns></returns>
        public DataTable obtieneProductosDetalle(int EmpresaId)
        {
            try
            {
                com = new SqlCommand("selObtieneProductosDetalleActivosPorEmpresa", con);
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

        public DataTable obtieneProductosDetalle(int EmpresaId, int EstatusId)
        {
            try
            {
                com = new SqlCommand("selObtieneProductosDetallePorEmpresaEstatus", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("EstatusId", EstatusId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtieneProductosDetalleTodos()
        {
            try
            {
                com = new SqlCommand("selObtieneProductosDetalle", con);
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
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneProductosPorPedidoPreVenta(int PedidoPreVentaId)
        {
            try
            {
                com = new SqlCommand("[selObtieneProductosPorPedidoPreVenta]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoPreVentaId", PedidoPreVentaId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneProductosDetallePorPedido(int PedidoId)
        {
            try
            {
                com = new SqlCommand("selObtieneProductosDetallePorPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("PedidoId", PedidoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneProductosDetallePorIngreso(int IngresoId)
        {
            try
            {
                com = new SqlCommand("selObtieneProductosDetallePorIngreso", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("IngresoId", IngresoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneProductosPorIngreso(int IngresoId)
        {
            try
            {
                com = new SqlCommand("selObtieneProductosPorIngreso", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("IngresoId", IngresoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneProductosPorFechaIngreso(DateTime FechaDesde, DateTime FehaHasta)
        {
            try
            {
                com = new SqlCommand("selObtieneProductosPorFechaIngreso", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FehaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneProductosDetallePorFechaIngreso(DateTime FechaDesde, DateTime FehaHasta)
        {
            try
            {
                com = new SqlCommand("selObtieneProductosDetallePorFechaIngreso", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FehaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneProductosPorFechaPedido(DateTime FechaDesde, DateTime FehaHasta)
        {
            try
            {
                com = new SqlCommand("selObtieneProductosPorFechaPedido", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FehaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneProductosPorFechaPedidoCliente(DateTime FechaDesde, DateTime FehaHasta,int ClienteId)
        {
            try
            {
                com = new SqlCommand("selObtieneProductosPorFechaPedidoCliente", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FehaHasta);
                com.Parameters.AddWithValue("ClienteId", ClienteId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        
        public DataTable obtieneProductosHastaFecha(DateTime FehaHasta)
        {
            try
            {
                com = new SqlCommand("selObtieneProductosHastaFecha", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FechaHasta", FehaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable selObtieneProductosDetalleHastaFecha(DateTime FehaHasta)
        {
            try
            {
                com = new SqlCommand("selObtieneProductosDetalleHastaFecha", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FechaHasta", FehaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable selObtieneProductosDetalleHastaFecha(int EmpresaId, DateTime FehaHasta)
        {
            try
            {
                com = new SqlCommand("selObtieneProductosDetalleHastaFechaPorEmpresa", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("FechaHasta", FehaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable selObtieneHistorialProductosPrecioCosto(int EmpresaId, DateTime FehaDesde, DateTime FehaHasta)
        {
            try
            {
                com = new SqlCommand("selObtieneHistorialProductosPrecioCostoPorEmpresa", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("FechaDesde", FehaDesde);
                com.Parameters.AddWithValue("FechaHasta", FehaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable selObtieneHistorialProductos(int EmpresaId, DateTime FehaDesde, DateTime FehaHasta)
        {
            try
            {
                com = new SqlCommand("selObtieneHistorialProductosPorEmpresa", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("FechaDesde", FehaDesde);
                com.Parameters.AddWithValue("FechaHasta", FehaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable ObtieneHistorialProductosDetalle(int EmpresaId, int ProductoId, DateTime FehaDesde, DateTime FehaHasta)
        {
            try
            {
                com = new SqlCommand("selObtieneAuxiliarEntradasSalidasPorProductoEmpresa", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("FechaDesde", FehaDesde);
                com.Parameters.AddWithValue("FechaHasta", FehaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable ObtieneHistorialProductosDetallePrecioCosto(int EmpresaId, int ProductoId, DateTime FehaDesde, DateTime FehaHasta)
        {
            try
            {
                com = new SqlCommand("selObtieneAuxiliarEntradasSalidasPorProductoPrecioCostoEmpresa", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("FechaDesde", FehaDesde);
                com.Parameters.AddWithValue("FechaHasta", FehaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable selObtieneProductoDetalleHistorial(string Serie)
        {
            try
            {
                com = new SqlCommand("selObtieneProductoDetallePorSerie", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("Serie", Serie);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtieneMovimientosProductosPorFechas(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta,
                                                                int Orientacion, int AlmacenId)
        {
            try
            {
                com = new SqlCommand("[spGetMovimientoInventario]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("FechaInicio", FechaDesde);
                com.Parameters.AddWithValue("FechaFin", FechaHasta);
                com.Parameters.AddWithValue("Orientacion", Orientacion);
                com.Parameters.AddWithValue("Almacen", AlmacenId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneMovimientosProductosPorFechas(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta,
                                                                int Orientacion, int AlmacenId, int TipoMovimientoId)
        {
            try
            {
                com = new SqlCommand("[spGetMovimientoInventarioTipo]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("FechaInicio", FechaDesde);
                com.Parameters.AddWithValue("FechaFin", FechaHasta);
                com.Parameters.AddWithValue("Orientacion", Orientacion);
                com.Parameters.AddWithValue("Almacen", AlmacenId);
                com.Parameters.AddWithValue("TipoMovimientoId", TipoMovimientoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneMovimientosDetalleProductosPorFechas(int MovimientoId)
        {
            try
            {
                com = new SqlCommand("[spGetMovimientoInventarioDetalle]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("MovimientoId", MovimientoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtieneMovimientosTraspasosRecepcionProductosPorFechas(int EmpresaId, 
                                                                                DateTime FechaDesde, DateTime FechaHasta,
                                                                                int Estado, int AlmacenId)
        {
            try
            {
                com = new SqlCommand("[spGetMovimientoTraspasoDestino]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("FechaInicio", FechaDesde);
                com.Parameters.AddWithValue("FechaFin", FechaHasta);
                com.Parameters.AddWithValue("Estado", Estado);
                com.Parameters.AddWithValue("Almacen", AlmacenId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneMovimientosTraspasosEnvioProductosPorFechas(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta,
                                                            int Estado, int AlmacenId)
        {
            try
            {
                com = new SqlCommand("[spGetMovimientoTraspasoOrigen]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("FechaInicio", FechaDesde);
                com.Parameters.AddWithValue("FechaFin", FechaHasta);
                com.Parameters.AddWithValue("Estado", Estado);
                com.Parameters.AddWithValue("Almacen", AlmacenId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneMovimientosDetalleTraspasoProductosPorFechas(int TraspasoId)
        {
            try
            {
                com = new SqlCommand("[spGetMovimientoTraspasoDetalle]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("TraspasoId", TraspasoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public DataTable obtieneIngreso(int IngresoId)
        {
            try
            {
                com = new SqlCommand("selObtieneIngresoPorId", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("IngresoId", IngresoId);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneIngreso(int EmpresaId, string Serie)
        {
            try
            {
                com = new SqlCommand("selObtieneIngresoPorSerieProducto", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("Serie", Serie);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable obtieneIngresosProductosPorFechas(DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("selObtieneIngresosPorFechas", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("FechaDesde", FechaDesde);
                com.Parameters.AddWithValue("FechaHasta", FechaHasta);
                da = new SqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public DataTable obtieneIngresosProductosPorFechas(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                com = new SqlCommand("selObtieneIngresosPorFechasPorEmpresa", con);
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

        //public DataTable obtieneProductosPorTipo(int TipoProductoId)
        //{
        //    try
        //    {
        //        com = new SqlCommand("selObtieneProductosPorTipo", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("TipoProductoId", TipoProductoId);
        //        da = new SqlDataAdapter(com);
        //        dt = new DataTable();
        //        da.Fill(dt);
        //        //if (dt.Rows.Count == 0)
        //        //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
        //        return dt;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}

        //public DataTable obtieneProducto(int Id)
        //{
        //    try
        //    {
        //        com = new SqlCommand("selObtieneProducto", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("Id", Id);
        //        da = new SqlDataAdapter(com);
        //        dt = new DataTable();
        //        da.Fill(dt);
        //        //if (dt.Rows.Count == 0)
        //        //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
        //        return dt;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}

        //public DataTable obtieneProductosPorPedido(int PedidoId)
        //{
        //    try
        //    {
        //        com = new SqlCommand("selObtieneProductosPorPedido", con);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("PedidoId", PedidoId);
        //        da = new SqlDataAdapter(com);
        //        dt = new DataTable();
        //        da.Fill(dt);
        //        //if (dt.Rows.Count == 0)
        //        //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
        //        return dt;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}

        public int agregaProducto(int EmpresaId, int TipoProductoId, string Codigo, string CodigoBarra, string Descripcion, 

                                string Marca, string Modelo,
                                int TipoProductoServicioId, int TipoUnidadId, decimal PrecioCosto, bool IncluyeIEPS)
        {
            try
            {
                int Id = 0;
                com = new SqlCommand("insAgregaProductoConIEPScodigo", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("TipoProductoId", TipoProductoId);
                com.Parameters.AddWithValue("Codigo", Codigo);
                com.Parameters.AddWithValue("Descripcion", Descripcion);
                com.Parameters.AddWithValue("Marca", Marca);
                com.Parameters.AddWithValue("Modelo", Modelo);
                com.Parameters.AddWithValue("TipoProductoServicioId", TipoProductoServicioId);
                com.Parameters.AddWithValue("TipoUnidadId", TipoUnidadId);
                com.Parameters.AddWithValue("PrecioCosto", PrecioCosto);
                com.Parameters.AddWithValue("IncluyeIEPS", IncluyeIEPS);
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
        public int agregaProductoIngreso(int EmpresaId, int IngresoId, int ProductoId,
                                        decimal ProductoCantidad,
                                        decimal ProductoPrecioCosto, decimal ProductoIEPS, decimal ProductoIVA)
        {
            try
            {
                int Id = 0;
                com = new SqlCommand("[insAgregaProductoIngresoNeue]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("IngresoId", IngresoId);
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("ProductoCantidad", ProductoCantidad);
                com.Parameters.AddWithValue("ProductoPrecioCosto", ProductoPrecioCosto);
                com.Parameters.AddWithValue("ProductoIEPS", ProductoIEPS);
                com.Parameters.AddWithValue("ProductoIVA", ProductoIVA);
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
        public int agregaProductoDetalle(int ProductoId, int IngresoId, int EmpresaId, 
                                        string Marca, string Modelo, string Serie, decimal PrecioCosto, decimal PrecioVenta, decimal PrecioVenta2, decimal PrecioEspecial, DateTime Fecha)
        {
            try
            {
                int Id = 0;
                com = new SqlCommand("[insAgregaProductoDetalleNeue]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("IngresoId", IngresoId);
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("Marca", Marca);
                com.Parameters.AddWithValue("Modelo", Modelo);
                com.Parameters.AddWithValue("Serie", Serie);
                com.Parameters.AddWithValue("PrecioCosto", PrecioCosto);
                com.Parameters.AddWithValue("PrecioVenta", PrecioVenta);
                com.Parameters.AddWithValue("PrecioVenta2", PrecioVenta2);
                com.Parameters.AddWithValue("PrecioEspecial", PrecioEspecial);
                com.Parameters.AddWithValue("FechaRegistro", Fecha);
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
        public int agregaProductoDetalle(int Id, int ProductoId, int IngresoId, int EmpresaId, string Serie, decimal PrecioCosto, decimal PrecioVenta, decimal PrecioVenta2, decimal PrecioEspecial, DateTime Fecha)
        {
            try
            {
                com = new SqlCommand("insAgregaProductoDetalleConId", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("IngresoId", IngresoId);
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("Serie", Serie);
                com.Parameters.AddWithValue("PrecioCosto", PrecioCosto);
                com.Parameters.AddWithValue("PrecioVenta", PrecioVenta);
                com.Parameters.AddWithValue("PrecioVenta2", PrecioVenta2);
                com.Parameters.AddWithValue("PrecioEspecial", PrecioEspecial);
                com.Parameters.AddWithValue("Id", Id);
                //SqlParameter parm = new SqlParameter("Id", Id);
                //parm.Direction = ParameterDirection.InputOutput;
                //com.Parameters.Add(parm);
                con.Open();
                com.ExecuteNonQuery();
                return Id;//Convert.ToInt32(com.Parameters["Id"].Value);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        string Host = Environment.MachineName;
        public int agregaListaPreciosProducto(int EstablecimientoId, int ProductoId, 
                decimal PrecioMenudeo, decimal PrecioMayoreoLocal, decimal PrecioSonora, 
                decimal PrecioCuliacan, decimal PrecioEspecial, decimal PrecioEspecial2, decimal PrecioEspecial3,
                decimal PrecioEspecial4, decimal PrecioEspecial5,
                decimal PrecioEspecialDetalle, decimal PrecioEspecialDetalleM, decimal PrecioEspecialComercial,
                string Usuario)
        {
            try
            {
                com = new SqlCommand("spEstablecimientoProductoListaPrecio", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EstablecimientoId", EstablecimientoId);
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("Menudeo", PrecioMenudeo);
                com.Parameters.AddWithValue("MayoreoLocal", PrecioMayoreoLocal);
                com.Parameters.AddWithValue("Sonora", PrecioSonora);
                com.Parameters.AddWithValue("Culiacan", PrecioCuliacan);
                com.Parameters.AddWithValue("Especial", PrecioEspecial);
                com.Parameters.AddWithValue("Cabo", PrecioEspecial2);
                com.Parameters.AddWithValue("Conveniencia", PrecioEspecial3);
                com.Parameters.AddWithValue("Eventual", PrecioEspecial4);
                com.Parameters.AddWithValue("Hermosillo", PrecioEspecial5);
                com.Parameters.AddWithValue("Detalle", PrecioEspecialDetalle);
                com.Parameters.AddWithValue("DetalleMayoreo", PrecioEspecialDetalleM);
                com.Parameters.AddWithValue("CadenaComercial", PrecioEspecialComercial);
                com.Parameters.AddWithValue("Usuario", Usuario);
                com.Parameters.AddWithValue("Host", this.Host);

                con.Open();

                //com.ExecuteNonQuery();
                DataTable dtCambios = new DataTable();
                DataTable dtSinCambios = new DataTable();
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    // Primer SELECT → cambios y nuevos
                    dtCambios.Load(reader);

                    // Segundo SELECT → sin cambios
                    if (reader.NextResult())
                        dtSinCambios.Load(reader);
                }

                return ProductoId;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public void actualizaProducto(int ProductoId, int TipoProductoId, string Codigo, string CodigoBarra, string Descripcion,
                                    string Marca, string Modelo,
                                    int TipoProductoServicioId, int TipoUnidadId, decimal PrecioCosto, bool IncluyeIEPS)
        {
            try
            {
                com = new SqlCommand("[updActualizaProductoConIEPScodigo]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("TipoProductoId", TipoProductoId);
                com.Parameters.AddWithValue("Codigo", Codigo);
                com.Parameters.AddWithValue("CodigoBarra", CodigoBarra);
                com.Parameters.AddWithValue("Descripcion", Descripcion);
                com.Parameters.AddWithValue("Marca", Marca);
                com.Parameters.AddWithValue("Modelo", Modelo);

                com.Parameters.AddWithValue("TipoProductoServicioId", TipoProductoServicioId);
                com.Parameters.AddWithValue("TipoUnidadId", TipoUnidadId);
                com.Parameters.AddWithValue("PrecioCosto", PrecioCosto);
                com.Parameters.AddWithValue("IncluyeIEPS", IncluyeIEPS);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaProductoMinMax(int ProductoId, decimal ExistenciaMinima, decimal ExistenciaMaxima)
        {
            try
            {
                com = new SqlCommand("[updActualizaProductoMinMax]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("ExistenciaMinima", ExistenciaMinima);
                com.Parameters.AddWithValue("ExistenciaMaxima", ExistenciaMaxima);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaProductoDetalle(int ProductoDetalleId, int EmpresaId, int IngresoId, string Serie, decimal PrecioCosto)
        {
            try
            {
                com = new SqlCommand("updActualizaProductoDetalle", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProductoDetalleId", ProductoDetalleId);
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("IngresoId", IngresoId);
                com.Parameters.AddWithValue("Serie", Serie);
                com.Parameters.AddWithValue("PrecioCosto", PrecioCosto);

                con.Open();
                com.ExecuteNonQuery();
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public void aumentaProducto(int ProductoId, decimal CantidadAumenta)
        {
            try
            {
                com = new SqlCommand("updAumentaProducto", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("CantidadAumenta", CantidadAumenta);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void aumentaProductoDetalle(int ProductoDetalleId, int CantidadAumenta)
        {
            try
            {
                com = new SqlCommand("updAumentaProductoDetalle", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProductoDetalleId", ProductoDetalleId);
                com.Parameters.AddWithValue("CantidadAumenta", CantidadAumenta);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public void actualizaProductosDetalle(int EmpresaId, int ProductoId, decimal PrecioCosto)
        {
            try
            {
                com = new SqlCommand("updActualizaProductosDetallePrecioCostoPorProducto", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("PrecioCosto", PrecioCosto);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public void actualizaEstatusProducto(int ProductoId, bool Estatus)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusProducto", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("Estatus", Estatus);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaEstatusProductoDetalle(int ProductoDetalleId, int EstatusId)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusProductoDetalle", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProductoDetalleId", ProductoDetalleId);
                com.Parameters.AddWithValue("EstatusId", EstatusId);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaEstatusProductoDetalle(int ProductoDetalleId, int EstatusId, string Observacion)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusProductoDetalleConObservacion", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProductoDetalleId", ProductoDetalleId);
                com.Parameters.AddWithValue("EstatusId", EstatusId);
                com.Parameters.AddWithValue("Observacion", Observacion);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaEstatusProductoDetallePedido(int ProductoDetallePedidoId, bool Estatus)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusProductoDetalle", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProductoDetallePedidoId", ProductoDetallePedidoId);
                com.Parameters.AddWithValue("Estatus", Estatus);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaEstatusProductoDetallePorIngreso(int IngresoId, int EstatusId)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusProductoDetallePorIngreso", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("IngresoId", IngresoId);
                com.Parameters.AddWithValue("EstatusId", EstatusId);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public int agregaIngresoProducto(int EmpresaId, int ProveedorId, string Descripcion, DateTime Fecha, int UsuarioIngresoId)
        {
            try
            {
                int Id = 0;
                com = new SqlCommand("insAgregaIngreso", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("ProveedorId", ProveedorId);
                com.Parameters.AddWithValue("Descripcion", Descripcion);
                com.Parameters.AddWithValue("Fecha", Fecha);
                com.Parameters.AddWithValue("UsuarioIngresoId", UsuarioIngresoId);
                //com.Parameters.AddWithValue("Id", Id);
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
        public int agregaIngresoProducto(int Id, int ProveedorId, string Descripcion, DateTime Fecha)
        {
            try
            {
                //int Id = 0;
                com = new SqlCommand("insAgregaIngresoConId", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProveedorId", ProveedorId);
                com.Parameters.AddWithValue("Descripcion", Descripcion);
                com.Parameters.AddWithValue("Fecha", Fecha);
                com.Parameters.AddWithValue("Id", Id);
                //SqlParameter parm = new SqlParameter("Id", Id);
                //parm.Direction = ParameterDirection.InputOutput;
                //com.Parameters.Add(parm);
                con.Open();
                com.ExecuteNonQuery();
                return Id;// Convert.ToInt32(com.Parameters["Id"].Value);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public void actualizaEstatusIngreso(int IngresoId, bool Estatus)
        {
            try
            {
                com = new SqlCommand("updActualizaEstatusIngreso", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("IngresoId", IngresoId);
                com.Parameters.AddWithValue("Estatus", Estatus);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaIngreso(int IngresoId, string Descripcion, DateTime Fecha, bool Estatus)
        {
            try
            {
                com = new SqlCommand("updActualizaIngreso", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("IngresoId", IngresoId);
                com.Parameters.AddWithValue("Descripcion", Descripcion);
                com.Parameters.AddWithValue("Fecha", Fecha);
                com.Parameters.AddWithValue("Estatus", Estatus);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public int agregaMovimiento(int IngresoAnteriorId, int IngresoNuevoId, int TipoMovimientoId, DateTime Fecha)
        {
            try
            {
                int Id = 0;
                com = new SqlCommand("insAgregaMovimiento", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("IngresoAnteriorId", IngresoAnteriorId);
                com.Parameters.AddWithValue("IngresoNuevoId", IngresoNuevoId);
                com.Parameters.AddWithValue("TipoMovimientoId", TipoMovimientoId);
                com.Parameters.AddWithValue("Fecha", Fecha);
                //com.Parameters.AddWithValue("Id", Id);
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
