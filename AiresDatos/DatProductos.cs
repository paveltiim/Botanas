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
        public DataTable obtieneProductos()
        {
            try
            {
                com = new SqlCommand("selObtieneProductos", con);
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

        public int agregaProducto(int TipoProductoId, string Codigo, string Descripcion, DateTime Fecha)
        {
            try
            {
                int Id = 0;
                com = new SqlCommand("insAgregaProducto", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("TipoProductoId", TipoProductoId);
                com.Parameters.AddWithValue("Codigo", Codigo);
                com.Parameters.AddWithValue("Descripcion", Descripcion);
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
        public int agregaProductoDetalle(int ProductoId, int IngresoId, int EmpresaId, string Serie, decimal PrecioCosto, decimal PrecioVenta, decimal PrecioVenta2, decimal PrecioEspecial, DateTime Fecha)
        {
            try
            {
                int Id = 0;
                com = new SqlCommand("insAgregaProductoDetalle", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("IngresoId", IngresoId);
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
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
        public int agregaIngresoProducto(int EmpresaId,string Descripcion, DateTime Fecha)
        {
            try
            {
                int Id = 0;
                com = new SqlCommand("insAgregaIngreso", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("EmpresaId", EmpresaId);
                com.Parameters.AddWithValue("Descripcion", Descripcion);
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
        
        public void actualizaProducto(int ProductoId, int TipoProductoId, string Codigo, string Descripcion)
        {
            try
            {
                com = new SqlCommand("updActualizaProducto", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("ProductoId", ProductoId);
                com.Parameters.AddWithValue("TipoProductoId", TipoProductoId);
                com.Parameters.AddWithValue("Codigo", Codigo);
                com.Parameters.AddWithValue("Descripcion", Descripcion);
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }
        public void actualizaProductoDetalle(int ProductoDetalleId, int EmpresaId, int IngresoId, string Serie, decimal PrecioCosto, int EstatusId)
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
                com.Parameters.AddWithValue("EstatusId", EstatusId);

                con.Open();
                com.ExecuteNonQuery();
                //if (dt.Rows.Count == 0)
                //    throw new Exception("Usuario y/o Contraseña Inválido(s)");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { con.Close(); }
        }

        public void aumentaProducto(int ProductoId, int CantidadAumenta)
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
    }
}
