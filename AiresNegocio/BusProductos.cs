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
    public class BusProductos : BusAbstracta
    {
        /// <summary>
        /// Obtiene todos los Productos registrados. Con Estatus=1
        /// </summary>
        /// <returns></returns>
        public List<EntProducto> ObtieneProductos()
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductos();
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
                    p.TipoProducto = r["TIPPRO_DESCRIPCION"].ToString();
                    p.PrecioCosto = Convert.ToDecimal(r["PRO_PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PRO_PRECIOVENTA"]);
                    p.PrecioVenta2 = Convert.ToDecimal(r["PRO_PRECIOVENTA2"]);
                    p.PrecioEspecial = Convert.ToDecimal(r["PRO_PRECIOESPECIAL"]);
                    p.Existencia = Convert.ToInt32(r["PRO_EXISTENCIA"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Obtiene todos los Productos registrados. Con Estatus=1
        /// </summary>
        /// <returns></returns>
        public List<EntProducto> ObtieneProductos(int EmpresaId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductos(EmpresaId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
                    p.TipoProducto = r["TIPPRO_DESCRIPCION"].ToString();
                    p.PrecioCosto = Convert.ToDecimal(r["PRO_PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PRO_PRECIOVENTA"]);
                    p.PrecioVenta2 = Convert.ToDecimal(r["PRO_PRECIOVENTA2"]);
                    p.PrecioEspecial = Convert.ToDecimal(r["PRO_PRECIOESPECIAL"]);
                    p.Existencia = Convert.ToInt32(r["PRO_EXISTENCIA"]);
                    p.Cantidad = p.Existencia;


                    p.ProductoServicioId = Convert.ToInt32(r["PRO_TIPOPRODUCTOSERVICIOID"]);
                    p.ProductoServicio = r["CATPRO_DESCRIPCION"].ToString();
                    p.ClaveProductoServicio = r["CATPRO_CLAVE"].ToString();
                    p.UnidadId = Convert.ToInt32(r["PRO_TIPOUNIDADID"]);
                    p.Unidad = r["CATUNI_DESCRIPCION"].ToString();
                    p.ClaveUnidad = r["CATUNI_CLAVE"].ToString();
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntProducto> ObtieneProductos(int EmpresaId, int EstatusId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductos(EmpresaId, EstatusId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
                    p.TipoProducto = r["TIPPRO_DESCRIPCION"].ToString();
                    p.PrecioCosto = Convert.ToDecimal(r["PRO_PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PRO_PRECIOVENTA"]);
                    p.PrecioVenta2 = Convert.ToDecimal(r["PRO_PRECIOVENTA2"]);
                    p.PrecioEspecial = Convert.ToDecimal(r["PRO_PRECIOESPECIAL"]);
                    p.Existencia = Convert.ToInt32(r["PRO_EXISTENCIA"]);
                    p.Cantidad = p.Existencia;
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntProducto> ObtieneProductosCodigos()
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductos();
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_CODIGO"].ToString() +"-" + r["PRO_DESCRIPCION"].ToString();
                    p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
                    p.TipoProducto = r["TIPPRO_DESCRIPCION"].ToString();
                    p.PrecioCosto = Convert.ToDecimal(r["PRO_PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PRO_PRECIOVENTA"]);
                    p.PrecioVenta2 = Convert.ToDecimal(r["PRO_PRECIOVENTA2"]);
                    p.PrecioEspecial = Convert.ToDecimal(r["PRO_PRECIOESPECIAL"]);
                    p.Existencia = Convert.ToInt32(r["PRO_EXISTENCIA"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        ///// <summary>
        ///// Agrega las Series en Descripcion
        ///// </summary>
        ///// <returns></returns>
        //public List<EntProducto> ObtieneProductosSerie()
        //{
        //    try
        //    {
        //        List<EntProducto> lst = new List<EntProducto>();
        //        dt = new DatProductos().obtieneProductos();
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            EntProducto p = new EntProducto();
        //            p.Id = Convert.ToInt32(r["PRO_ID"]);
        //            p.Codigo = r["PRO_CODIGO"].ToString();
        //            p.Descripcion = r["PRO_DESCRIPCION"].ToString();
        //            p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
        //            p.TipoProducto = r["TIPPRO_DESCRIPCION"].ToString();
        //            p.PrecioCosto = Convert.ToDecimal(r["PRO_PRECIOCOSTO"]);
        //            p.PrecioVenta = Convert.ToDecimal(r["PRO_PRECIOVENTA"]);
        //            p.PrecioVenta2 = Convert.ToDecimal(r["PRO_PRECIOVENTA2"]);
        //            p.PrecioEspecial = Convert.ToDecimal(r["PRO_PRECIOESPECIAL"]);
        //            p.Existencia = Convert.ToInt32(r["PRO_EXISTENCIA"]);

        //            lst.Add(p);
        //        }
        //        return lst;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}

        /// <summary>
        /// Obtiene solo los ProductosDetalle Activos. Con Estatus=1
        /// </summary>
        /// <returns></returns>
        public List<EntProducto> ObtieneProductosDetalle()
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosDetalle();
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRODET_ID"]);
                    p.ProductoId = Convert.ToInt32(r["PRO_ID"]);

                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
                    p.TipoProducto = r["TIPPRO_DESCRIPCION"].ToString();
                    p.EmpresaId = Convert.ToInt32(r["PRODET_EMPRESAID"]);
                    p.Serie = r["PRODET_SERIE"].ToString();

                    p.Unidad = "PIEZA";

                    p.Cantidad = Convert.ToInt32(r["PRODET_EXISTENCIA"]);
                    p.PrecioCosto = Convert.ToDecimal(r["PRODET_PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PRODET_PRECIOVENTA"]);
                    p.PrecioVenta2 = Convert.ToDecimal(r["PRODET_PRECIOVENTA2"]);
                    p.PrecioEspecial = Convert.ToDecimal(r["PRODET_PRECIOESPECIAL"]);
                    p.Existencia = Convert.ToInt32(r["PRO_EXISTENCIA"]);
                    p.EstatusId = Convert.ToInt32(r["PRODET_ESTATUSID"]);

                    p.Fecha = Convert.ToDateTime(r["PRODET_FECHAREGISTRO"]);
                    p.FechaCorta = Convert.ToDateTime(r["PRODET_FECHAREGISTRO"]).ToShortDateString();
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Obtiene solo los ProductosDetalle Activos. Con Estatus=1
        /// </summary>
        /// <returns></returns>
        public List<EntProducto> ObtieneProductosDetalle(int EmpresaId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosDetalle(EmpresaId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRODET_ID"]);
                    p.ProductoId = Convert.ToInt32(r["PRO_ID"]);

                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
                    p.TipoProducto = r["TIPPRO_DESCRIPCION"].ToString();
                    p.EmpresaId = Convert.ToInt32(r["PRODET_EMPRESAID"]);
                    p.Serie = r["PRODET_SERIE"].ToString();

                    //p.Unidad = "PIEZA";

                    p.IngresoId = Convert.ToInt32(r["PRODET_INGRESOID"]);
                    p.Ingreso = r["ING_DESCRIPCION"].ToString();
                    p.Fecha = Convert.ToDateTime(r["ING_FECHA"]);

                    p.Cantidad = Convert.ToInt32(r["PRODET_EXISTENCIA"]);
                    p.PrecioCosto = Convert.ToDecimal(r["PRODET_PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PRODET_PRECIOVENTA"]);
                    p.PrecioVenta2 = Convert.ToDecimal(r["PRODET_PRECIOVENTA2"]);
                    p.PrecioEspecial = Convert.ToDecimal(r["PRODET_PRECIOESPECIAL"]);

                    p.Existencia = Convert.ToInt32(r["PRO_EXISTENCIA"]);
                    p.Existencia = Convert.ToInt32(p.Cantidad);

                    p.EstatusId = Convert.ToInt32(r["PRODET_ESTATUSID"]);


                    p.ProductoServicioId = Convert.ToInt32(r["PRO_TIPOPRODUCTOSERVICIOID"]);
                    p.ProductoServicio = r["CATPRO_DESCRIPCION"].ToString();
                    p.ClaveProductoServicio = r["CATPRO_CLAVE"].ToString();
                    p.UnidadId = Convert.ToInt32(r["PRO_TIPOUNIDADID"]);
                    p.Unidad = r["CATUNI_DESCRIPCION"].ToString();
                    p.ClaveUnidad = r["CATUNI_CLAVE"].ToString();

                    p.FechaCorta = Convert.ToDateTime(r["PRODET_FECHAREGISTRO"]).ToShortDateString();
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Obtiene solo los ProductosDetalle Activos. Con Estatus=1
        /// </summary>
        /// <returns></returns>
        public List<EntProducto> ObtieneProductosDetalle(int EmpresaId, int EstatusId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosDetalle(EmpresaId, EstatusId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRODET_ID"]);
                    p.ProductoId = Convert.ToInt32(r["PRO_ID"]);

                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
                    p.TipoProducto = r["TIPPRO_DESCRIPCION"].ToString();
                    p.EmpresaId = Convert.ToInt32(r["PRODET_EMPRESAID"]);
                    p.Serie = r["PRODET_SERIE"].ToString();

                    p.Unidad = "PIEZA";

                    p.IngresoId = Convert.ToInt32(r["PRODET_INGRESOID"]);
                    p.Ingreso = r["ING_DESCRIPCION"].ToString();
                    p.Fecha = Convert.ToDateTime(r["ING_FECHA"]);

                    p.Cantidad = Convert.ToInt32(r["PRODET_EXISTENCIA"]);
                    p.PrecioCosto = Convert.ToDecimal(r["PRODET_PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PRODET_PRECIOVENTA"]);
                    p.PrecioVenta2 = Convert.ToDecimal(r["PRODET_PRECIOVENTA2"]);
                    p.PrecioEspecial = Convert.ToDecimal(r["PRODET_PRECIOESPECIAL"]);

                    p.Existencia = Convert.ToInt32(r["PRO_EXISTENCIA"]);
                    p.Existencia = Convert.ToInt32(p.Cantidad);

                    p.EstatusId = Convert.ToInt32(r["PRODET_ESTATUSID"]);

                    p.FechaCorta = Convert.ToDateTime(r["PRODET_FECHAREGISTRO"]).ToShortDateString();
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Obtiene los ProductosDetalle Activos o Vendidos. Con Estatus=1 o Estatus=2 
        /// </summary>
        /// <returns></returns>
        public List<EntProducto> ObtieneProductosDetalleTodos()
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosDetalleTodos();
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRODET_ID"]);
                    p.ProductoId = Convert.ToInt32(r["PRO_ID"]);

                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
                    p.TipoProducto = r["TIPPRO_DESCRIPCION"].ToString();
                    //p.Empresa
                    p.IngresoId =Convert.ToInt32(r["PRODET_INGRESOID"]);
                    p.Serie = r["PRODET_SERIE"].ToString();

                    p.Unidad = "PIEZA";

                    p.Cantidad = Convert.ToInt32(r["PRODET_EXISTENCIA"]);
                    p.PrecioCosto = Convert.ToDecimal(r["PRODET_PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PRODET_PRECIOVENTA"]);
                    p.PrecioVenta2 = Convert.ToDecimal(r["PRODET_PRECIOVENTA2"]);
                    p.PrecioEspecial = Convert.ToDecimal(r["PRODET_PRECIOESPECIAL"]);
                    p.Existencia = Convert.ToInt32(r["PRODET_EXISTENCIA"]);
                    p.EstatusId = Convert.ToInt32(r["PRODET_ESTATUSID"]);

                    p.Fecha = Convert.ToDateTime(r["PRODET_FECHAREGISTRO"]);
                    p.FechaCorta = Convert.ToDateTime(r["PRODET_FECHAREGISTRO"]).ToShortDateString();
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TipoProductoId"></param>
        /// <returns></returns>
        public List<EntProducto> ObtieneProductosPorPedido(int PedidoId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosPorPedido(PedidoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    //p.Id = Convert.ToInt32(r["PROPED_ID"]);
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();


                    //p.Unidad = "PIEZA";
                    p.ProductoServicioId = Convert.ToInt32(r["PRO_TIPOPRODUCTOSERVICIOID"]);
                    p.ProductoServicio = r["CATPRO_DESCRIPCION"].ToString();
                    p.ClaveProductoServicio = r["CATPRO_CLAVE"].ToString();
                    p.UnidadId = Convert.ToInt32(r["PRO_TIPOUNIDADID"]);
                    p.Unidad = r["CATUNI_DESCRIPCION"].ToString();
                    p.ClaveUnidad = r["CATUNI_CLAVE"].ToString();


                    p.Cantidad = Convert.ToInt32(r["CANTIDAD"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PROPED_PRECIOVENTA"]);
                    
                    p.PrecioVentaSinIVA = Math.Round(p.PrecioVenta/1.16m,2);
                    //subtotal = Math.Round(total, 2) / (1 + IVA);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TipoProductoId"></param>
        /// <returns></returns>
        public List<EntProducto> ObtieneProductosDetallePorPedido(int PedidoId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosDetallePorPedido(PedidoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRODET_ID"]);
                    p.ProductoId = Convert.ToInt32(r["PRO_ID"]);
                    p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.Cantidad = Convert.ToInt32(r["PROPED_CANTIDAD"]);
                    p.PrecioCosto = Convert.ToDecimal(r["PROPED_PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PROPED_PRECIOVENTA"]);
                    p.Serie = r["PRODET_SERIE"].ToString(); 
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TipoProductoId"></param>
        /// <returns></returns>
        public List<EntProducto> ObtieneProductosDetallePorIngreso(int IngresoId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosDetallePorIngreso(IngresoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRODET_ID"]);
                    p.ProductoId = Convert.ToInt32(r["PRO_ID"]);

                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
                    p.TipoProducto = r["TIPPRO_DESCRIPCION"].ToString();

                    p.EmpresaId = Convert.ToInt32(r["PRODET_EMPRESAID"]);

                    p.IngresoId = Convert.ToInt32(r["PRODET_INGRESOID"]);
                    p.Ingreso = r["ING_DESCRIPCION"].ToString();
                    p.Fecha = Convert.ToDateTime(r["ING_FECHA"]);

                    p.Serie = r["PRODET_SERIE"].ToString();
                    p.Cantidad = Convert.ToInt32(r["PRODET_EXISTENCIA"]);
                    p.PrecioCosto = Convert.ToDecimal(r["PRODET_PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PRODET_PRECIOVENTA"]);
                    p.PrecioVenta2 = Convert.ToDecimal(r["PRODET_PRECIOVENTA2"]);
                    p.PrecioEspecial = Convert.ToDecimal(r["PRODET_PRECIOESPECIAL"]);
                    p.Existencia = Convert.ToInt32(r["PRODET_EXISTENCIA"]);
                    p.EstatusId = Convert.ToInt32(r["PRODET_ESTATUSID"]);

                    p.FechaCorta = Convert.ToDateTime(r["PRODET_FECHAREGISTRO"]).ToShortDateString();
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TipoProductoId"></param>
        /// <returns></returns>
        public List<EntProducto> ObtieneProductosPorIngreso(int IngresoId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosPorIngreso(IngresoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.Cantidad = Convert.ToInt32(r["CANTIDAD"]);
                    p.PrecioCosto = Convert.ToDecimal(r["PRODET_PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PRODET_PRECIOVENTA"]);
                    p.IngresoId = Convert.ToInt32(r["PRODET_INGRESOID"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<EntProducto> ObtieneProductosPorFechaIngreso(DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosPorFechaIngreso(FechaDesde,FechaHasta);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.Cantidad = Convert.ToInt32(r["CANTIDAD"]);
                    p.PrecioCosto = Convert.ToDecimal(r["PRODET_PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PRODET_PRECIOVENTA"]);
                    p.IngresoId = Convert.ToInt32(r["PRODET_INGRESOID"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntProducto> ObtieneProductosDetallePorFechaIngreso(DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosDetallePorFechaIngreso(FechaDesde, FechaHasta);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.Serie = r["PRODET_SERIE"].ToString();
                    p.Ingreso = r["ING_DESCRIPCION"].ToString();
                    p.Cantidad = Convert.ToInt32(r["CANTIDAD"]);
                    p.PrecioCosto = Convert.ToDecimal(r["PRODET_PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PRODET_PRECIOVENTA"]);
                    p.IngresoId = Convert.ToInt32(r["PRODET_INGRESOID"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntProducto> ObtieneProductosPorFechaPedido(DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosPorFechaPedido(FechaDesde, FechaHasta);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    //p.Id = Convert.ToInt32(r["PROPED_ID"]);
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    //p.Cantidad = Convert.ToInt32(r["CANTIDAD"]);
                    p.Cantidad = Convert.ToInt32(r["PROPED_CANTIDAD"]);
                    p.Serie = r["PRODET_SERIE"].ToString();
                    p.PrecioCosto= Convert.ToDecimal(r["PRODET_PRECIOCOSTO"]);
                    //p.PrecioVenta = Convert.ToDecimal(r["PROPED_PRECIOVENTA"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PRECIOVENTA"]);
                    p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntProducto> ObtieneProductosPorFechaPedidoCliente(DateTime FechaDesde, DateTime FechaHasta, int ClienteId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosPorFechaPedidoCliente(FechaDesde, FechaHasta, ClienteId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    //p.Id = Convert.ToInt32(r["PROPED_ID"]);
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.Cantidad = Convert.ToInt32(r["CANTIDAD"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PROPED_PRECIOVENTA"]);
                    
                    p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Productos sin vender Hasta Fecha
        /// </summary>
        /// <param name="FechaDesde"></param>
        /// <param name="FechaHasta"></param>
        /// <param name="ClienteId"></param>
        /// <returns></returns>
        public List<EntProducto> ObtieneProductosHastaFecha(DateTime FechaHasta)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosHastaFecha(FechaHasta);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    //p.Id = Convert.ToInt32(r["PROPED_ID"]);
                    //p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.Cantidad = Convert.ToInt32(r["CANTIDAD_RESTANTES"]);
                    p.PrecioCosto = Convert.ToDecimal(r["PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PRECIOVENTA"]);

                    //p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Productos sin vender Hasta Fecha
        /// </summary>
        /// <param name="FechaDesde"></param>
        /// <param name="FechaHasta"></param>
        /// <param name="ClienteId"></param>
        /// <returns></returns>
        public List<EntProducto> ObtieneProductosDetalleHastaFecha(DateTime FechaHasta)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().selObtieneProductosDetalleHastaFecha(FechaHasta);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    //p.Id = Convert.ToInt32(r["PROPED_ID"]);
                    //p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.Cantidad = 1;
                    p.PrecioCosto = Convert.ToDecimal(r["PRODET_PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PRODET_PRECIOVENTA"]);

                    //p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Productos sin vender Hasta Fecha
        /// </summary>
        /// <param name="FechaDesde"></param>
        /// <param name="FechaHasta"></param>
        /// <param name="ClienteId"></param>
        /// <returns></returns>
        public List<EntProducto> ObtieneProductosDetalleHastaFecha(int EmpresaId, DateTime FechaHasta)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().selObtieneProductosDetalleHastaFecha(EmpresaId, FechaHasta);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    //p.Id = Convert.ToInt32(r["PROPED_ID"]);
                    //p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.Cantidad = 1;
                    p.PrecioCosto = Convert.ToDecimal(r["PRODET_PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PRODET_PRECIOVENTA"]);

                    //p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<EntProducto> ObtieneProductoDetalleHistorial(string Serie)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().selObtieneProductoDetalleHistorial(Serie);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    //p.Id = Convert.ToInt32(r["PROPED_ID"]);
                    //p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.Cantidad = 1;
                    p.PrecioCosto = Convert.ToDecimal(r["PRODET_PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PRODET_PRECIOVENTA"]);
                    p.Serie = r["PRODET_SERIE"].ToString();

                    p.IngresoId = Convert.ToInt32(r["ING_ID"]);
                    p.TipoProducto = r["ING_DESCRIPCION"].ToString();

                    p.EmpresaId = Convert.ToInt32(r["FAC_ID"]);
                    p.FechaCorta = Convert.ToDateTime(r["ING_FECHA"]).ToShortDateString();
                    p.Fecha = Convert.ToDateTime(r["FAC_FECHA"]);
                    //p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TipoProductoId"></param>
        /// <returns></returns>
        public EntCatalogoGenerico ObtieneIngreso(int IngresoId)
        {
            try
            {
                //List<EntCatalogoGenerico> lst = new List<EntCatalogoGenerico>();
                dt = new DatProductos().obtieneIngreso(IngresoId);
                EntCatalogoGenerico p = new EntCatalogoGenerico();
                foreach (DataRow r in dt.Rows)
                {
                    p = new EntCatalogoGenerico();
                    p.Id = Convert.ToInt32(r["ING_ID"]);
                    p.Descripcion = r["ING_DESCRIPCION"].ToString();
                    p.Fecha = Convert.ToDateTime(r["ING_FECHA"]);
                    //lst.Add(p);
                }
                return p;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public EntCatalogoGenerico ObtieneIngreso(int EmpresaId, string Serie)
        {
            try
            {
                //List<EntCatalogoGenerico> lst = new List<EntCatalogoGenerico>();
                dt = new DatProductos().obtieneIngreso(EmpresaId, Serie);
                EntCatalogoGenerico p = new EntCatalogoGenerico();
                foreach (DataRow r in dt.Rows)
                {
                    p = new EntCatalogoGenerico();
                    p.Id = Convert.ToInt32(r["ING_ID"]);
                    p.Descripcion = r["ING_DESCRIPCION"].ToString();
                    p.Fecha = Convert.ToDateTime(r["ING_FECHA"]);
                    //lst.Add(p);
                }
                return p;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TipoProductoId"></param>
        /// <returns></returns>
        public List<EntCatalogoGenerico> ObtieneIngresosProductos(DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                List<EntCatalogoGenerico> lst = new List<EntCatalogoGenerico>();
                dt = new DatProductos().obtieneIngresosProductosPorFechas(FechaDesde,FechaHasta);
                foreach (DataRow r in dt.Rows)
                {
                    EntCatalogoGenerico p = new EntCatalogoGenerico();
                    p.Id = Convert.ToInt32(r["ING_ID"]);
                    p.Descripcion = r["ING_DESCRIPCION"].ToString();
                    p.Fecha = Convert.ToDateTime(r["ING_FECHA"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntCatalogoGenerico> ObtieneIngresosProductos(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                List<EntCatalogoGenerico> lst = new List<EntCatalogoGenerico>();
                dt = new DatProductos().obtieneIngresosProductosPorFechas(EmpresaId ,FechaDesde, FechaHasta);
                foreach (DataRow r in dt.Rows)
                {
                    EntCatalogoGenerico p = new EntCatalogoGenerico();
                    p.Id = Convert.ToInt32(r["ING_ID"]);
                    p.Descripcion = r["ING_DESCRIPCION"].ToString();
                    p.Fecha = Convert.ToDateTime(r["ING_FECHA"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public List<EntMaterial> ObtieneMaterialesPorProducto(int ProductoId)
        {
            try
            {
                List<EntMaterial> lst = new List<EntMaterial>();
                //dt = new DatProductos().ObtieneMaterialesPorProducto(ProductoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntMaterial p = new EntMaterial();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    //p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    //p.Cantidad = Convert.ToInt32(r["CANTIDAD"]);
                    p.PrecioCosto = Convert.ToDecimal(r["PRODET_PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PRODET_PRECIOVENTA"]);
                    //p.IngresoId = Convert.ToInt32(r["PRODET_INGRESOID"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        ///// <summary>
        ///// Obtiene todos los Productos registrados del TipoProducto solicitado. Con Estatus=1
        ///// </summary>
        ///// <param name="TipoProductoId"></param>
        ///// <returns></returns>
        //public List<EntProducto> ObtieneProductosPorTipo(int TipoProductoId)
        //{
        //    try
        //    {
        //        List<EntProducto> lst = new List<EntProducto>();
        //        dt = new DatProductos().obtieneProductosPorTipo(TipoProductoId);
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            EntProducto p = new EntProducto();
        //            p.Id = Convert.ToInt32(r["PRO_ID"]);
        //            p.Descripcion = r["PRO_DESCRIPCION"].ToString();
        //            p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
        //            p.TipoProducto = r["TIPPRO_DESCRIPCION"].ToString();
        //            p.PrecioCosto = Convert.ToDecimal(r["PRO_PRECIOCOSTO"]);
        //            p.PrecioVenta = Convert.ToDecimal(r["PRO_PRECIOVENTA"]);
        //            //p.PrecioVenta2 = Convert.ToDecimal(r["PRO_PRECIOVENTA2"]);
        //            p.PrecioEspecial = Convert.ToDecimal(r["PRO_PRECIOESPECIAL"]);
        //            p.Existencia = Convert.ToInt32(r["PRO_EXISTENCIA"]);
        //            lst.Add(p);
        //        }
        //        return lst;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}

        //public EntProducto ObtieneProducto(int Id)
        //{
        //    try
        //    {
        //        dt = new DatProductos().obtieneProducto(Id);
        //        EntProducto p = new EntProducto();
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            p.Id = Convert.ToInt32(r["PRO_ID"]);
        //            p.Descripcion = r["PRO_DESCRIPCION"].ToString();
        //            p.PrecioCosto = Convert.ToDecimal(r["PRO_PRECIOCOSTO"]);
        //            p.PrecioVenta = Convert.ToDecimal(r["PRO_PRECIOVENTA"]);
        //            p.PrecioEspecial = Convert.ToDecimal(r["PRO_PRECIOESPECIAL"]);
        //        }
        //        return p;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="TipoProductoId"></param>
        ///// <returns></returns>
        //public List<EntProducto> ObtieneProductosPorPedido(int PedidoId)
        //{
        //    try
        //    {
        //        List<EntProducto> lst = new List<EntProducto>();
        //        dt = new DatProductos().obtieneProductosPorPedido(PedidoId);
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            EntProducto p = new EntProducto();
        //            p.Id = Convert.ToInt32(r["PROPED_ID"]);
        //            p.Descripcion = r["PRO_DESCRIPCION"].ToString();
        //            p.Cantidad = Convert.ToInt32(r["PROPED_PRODUCTOCANTIDAD"]);
        //            p.PrecioVenta = Convert.ToDecimal(r["PROPED_PRODUCTOPRECIO"]);
        //            lst.Add(p);
        //        }
        //        return lst;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}

        /// <summary>
        /// Agrega nuevo Producto.
        /// </summary>
        /// <param name="producto">
        /// Propiedades Necesarias: TipoProductoId ,Descripcion, PrecioCosto, PrecioVenta, PrecioEspecial.
        /// </param>
        public int AgregaProducto(EntProducto producto)
        {
            try
            {
                return new DatProductos().agregaProducto(producto.TipoProductoId, producto.Codigo, producto.Descripcion, 
                                                        producto.ProductoServicioId, producto.UnidadId, 
                                                        producto.Fecha);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public int AgregaProducto(int Id, EntProducto producto)
        {
            try
            {
                return new DatProductos().agregaProducto(Id, producto.TipoProductoId, producto.Codigo, producto.Descripcion);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Agrega nuevo ProductoDetalle.
        /// </summary>
        /// <param name="producto">
        /// Propiedades Necesarias: TipoProductoId ,Descripcion, PrecioCosto, PrecioVenta, PrecioEspecial.
        /// </param>
        public int AgregaProductoDetalle(EntProducto producto)
        {
            try
            {
                return new DatProductos().agregaProductoDetalle(producto.Id, producto.IngresoId,producto.EmpresaId,producto.Serie, producto.PrecioCosto, producto.PrecioVenta, producto.PrecioVenta2, producto.PrecioEspecial, producto.Fecha);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Agrega nuevo ProductoDetalle.
        /// </summary>
        /// <param name="producto">
        /// Propiedades Necesarias: TipoProductoId ,Descripcion, PrecioCosto, PrecioVenta, PrecioEspecial.
        /// </param>
        public int AgregaProductoDetalle(int Id, EntProducto producto)
        {
            try
            {
                return new DatProductos().agregaProductoDetalle(Id, producto.ProductoId, producto.IngresoId, producto.EmpresaId, producto.Serie, producto.PrecioCosto, producto.PrecioVenta, producto.PrecioVenta2, producto.PrecioEspecial, producto.Fecha);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Agrega nuevo Ingreso.
        /// </summary>
        /// <param name="producto">
        /// Propiedades Necesarias: Id servira para EmpresaId.
        /// </param>
        public int AgregaIngreso(EntCatalogoGenerico Ingreso)
        {
            try
            {
                return new DatProductos().agregaIngresoProducto(Ingreso.Id,Ingreso.Descripcion, Ingreso.Fecha);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public int AgregaIngreso(int IngresoId, EntCatalogoGenerico Ingreso)
        {
            try
            {
                return new DatProductos().agregaIngresoProducto(IngresoId, Ingreso.Id, Ingreso.Descripcion, Ingreso.Fecha);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Actualiza campos de Producto.
        /// </summary>
        /// <param name="producto">
        /// Propiedades Necesarias: Id=Id del Producto a Actualizar;Campos a Actualizar: TipoProductoId, Descripcion, PrecioCosto, PrecioVenta, PrecioEspecial.
        /// </param>
        public void ActualizaProducto(EntProducto producto)
        {
            try
            {
                new DatProductos().actualizaProducto(producto.Id, producto.TipoProductoId, producto.Codigo, producto.Descripcion,
                                                    producto.ProductoServicioId, producto.UnidadId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public void ActualizaProductoDetalle(EntProducto producto)
        {
            try
            {
                new DatProductos().actualizaProductoDetalle(producto.Id, producto.EmpresaId, producto.IngresoId, producto.Serie, producto.PrecioCosto);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public void ActualizaProductoDetallePedido(EntProducto Producto)
        {
            try
            {
                new DatPedidos().actualizaProductoDetallePedido(Producto.Id, Producto.PrecioCosto, Producto.PrecioVenta);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Actualiza campos de Producto.
        /// </summary>
        /// <param name="producto">
        /// Propiedades Necesarias: Id=Id del Producto a Actualizar;Campos a Actualizar: TipoProductoId, Descripcion, PrecioCosto, PrecioVenta, PrecioEspecial, Cantidad.
        /// </param>
        public void AumentaProducto(int ProductoId, int CantidadAumenta)
        {
            try
            {
                new DatProductos().aumentaProducto(ProductoId, CantidadAumenta);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Actualiza campos de Producto.
        /// </summary>
        /// <param name="producto">
        /// Propiedades Necesarias: Id=Id del Producto a Actualizar;Campos a Actualizar: TipoProductoId, Descripcion, PrecioCosto, PrecioVenta, PrecioEspecial, Cantidad.
        /// </param>
        //public void AumentaProductoDetalle(EntProducto producto, int CantidadAumenta)
        //{
        //    try
        //    {
        //        new DatProductos().aumentaProductoDetalle(producto.Id, CantidadAumenta);
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}

        /// <summary>
        /// Actualiza campo Estatus de Producto.
        /// </summary>
        /// <param name="producto">
        /// Propiedades Necesarias: Id=Id del Producto a Actualizar;Campos a Actualizar: Estatus.
        /// </param>
        public void ActualizaEstatusProducto(EntProducto producto)
        {
            try
            {
                new DatProductos().actualizaEstatusProducto(producto.Id, producto.Estatus);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void ActualizaEstatusProducto(int ProductoId, bool Estatus)
        {
            try
            {
                new DatProductos().actualizaEstatusProducto(ProductoId, Estatus);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// Actualiza campo Estatus de Producto.
        /// </summary>
        /// <param name="producto">
        /// Propiedades Necesarias: Id=Id del Producto a Actualizar;Campos a Actualizar: Estatus.
        /// </param>
        public void ActualizaEstatusProductoDetalle(EntProducto producto)
        {
            try
            {
                new DatProductos().actualizaEstatusProductoDetalle(producto.Id, producto.EstatusId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        //public void ActualizaEstatusProductoDetallePedido(EntProducto producto)
        //{
        //    try
        //    {
        //        new DatProductos().actualizaEstatusProductoDetallePedido(producto.Id, producto.Estatus);
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}
        public void ActualizaEstatusProductoDetalle(EntCatalogoGenerico Ingreso)
        {
            try
            {
                new DatProductos().actualizaEstatusProductoDetallePorIngreso(Ingreso.Id, Ingreso.EstatusId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public void ActualizaEstatusIngreso(EntCatalogoGenerico ingreso)
        {
            try
            {
                new DatProductos().actualizaEstatusIngreso(ingreso.Id, ingreso.Estatus);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void ActualizaIngreso(EntCatalogoGenerico ingreso)
        {
            try
            {
                new DatProductos().actualizaIngreso(ingreso.Id, ingreso.Descripcion,ingreso.Fecha, ingreso.Estatus);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Actualiza campos de Producto.
        /// </summary>
        /// <param name="producto">
        /// Propiedades Necesarias: IngresoId, FechaMovimiento
        /// Campos a Actualizar: TipoProductoId, Descripcion, PrecioCosto, PrecioVenta, PrecioEspecial.
        /// </param>
        /// <param name="TipoMovimientoId">
        /// TipoMovimientoId; 1-ENTRADA, 2-VENTA, 3-TRASPASO, 4-DEVOLUCION, 
        /// </param>
        public void AgregaMovimiento(EntProducto producto, int IngresoNuevoId, int TipoMovimientoId, DateTime Fecha)
        {
            try
            {
                new DatProductos().agregaMovimiento(producto.IngresoId,IngresoNuevoId, TipoMovimientoId, Fecha);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
