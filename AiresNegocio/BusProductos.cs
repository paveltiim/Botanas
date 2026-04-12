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
        public EntProducto ObtieneProducto(int ProductoId)
        {
            try
            {
                EntProducto p = new EntProducto();
                dt = new DatProductos().obtieneProducto(ProductoId);
                foreach (DataRow r in dt.Rows)
                {
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.CodigoBarra = r["PRO_CODIGOBARRA"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
                    p.TipoProducto = r["TIPPRO_DESCRIPCION"].ToString();
                    
                    p.ProductoServicioId = Convert.ToInt32(r["PRO_TIPOPRODUCTOSERVICIOID"]);
                    p.ProductoServicio = r["CATPRO_DESCRIPCION"].ToString();
                    p.ClaveProductoServicio = r["CATPRO_CLAVE"].ToString();
                    p.UnidadId = Convert.ToInt32(r["PRO_TIPOUNIDADID"]);
                    p.Unidad = r["CATUNI_DESCRIPCION"].ToString();
                    p.ClaveUnidad = r["CATUNI_CLAVE"].ToString();

                    p.IncluyeIeps = Convert.ToBoolean(r["PRO_INCLUYEIEPS"]);
                }
                return p;
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

                    p.Marca = r["PRO_MARCA"].ToString();
                    p.Modelo = r["PRO_MODELO"].ToString();

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
        public List<EntProducto> ObtieneProductosPorTipo(int EmpresaId, int TipoProductoId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosPorTipo(EmpresaId, TipoProductoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.CodigoBarra = r["PRO_CODIGOBARRA"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
                    p.TipoProducto = r["TIPPRO_DESCRIPCION"].ToString();
                    p.PrecioCosto = Convert.ToDecimal(r["PRO_PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PRO_PRECIOVENTA"]);
                    p.PrecioVenta2 = Convert.ToDecimal(r["PRO_PRECIOVENTA2"]);
                    p.PrecioEspecial = Convert.ToDecimal(r["PRO_PRECIOESPECIAL"]); 
                    p.ExistenciaMinima = Convert.ToInt32(r["PRO_EXISTENCIAMINIMA"]);
                    p.ExistenciaMaxima = Convert.ToInt32(r["PRO_EXISTENCIAMAXIMA"]);
                    p.Existencia = Convert.ToInt32(r["PRO_EXISTENCIA"]);
                    p.Cantidad = p.Existencia;

                    p.Marca = r["PRO_MARCA"].ToString();
                    p.Modelo = r["PRO_MODELO"].ToString();

                    p.ProductoServicioId = Convert.ToInt32(r["PRO_TIPOPRODUCTOSERVICIOID"]);
                    p.ProductoServicio = r["CATPRO_DESCRIPCION"].ToString();
                    p.ClaveProductoServicio = r["CATPRO_CLAVE"].ToString();
                    p.UnidadId = Convert.ToInt32(r["PRO_TIPOUNIDADID"]);
                    p.Unidad = r["CATUNI_DESCRIPCION"].ToString();
                    p.ClaveUnidad = r["CATUNI_CLAVE"].ToString();

                    p.IncluyeIeps = Convert.ToBoolean(r["PRO_INCLUYEIEPS"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<EntProducto> ObtieneProductosPorAlmacen(int CompañiaId, int ProductoId, int AlmacenId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosPorAlmacen(CompañiaId, ProductoId, AlmacenId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.PrecioCosto = Convert.ToDecimal(r["PRO_PRECIOCOSTO"]);
                    p.Existencia = Convert.ToInt32(r["EXISTENCIA"]);
                    if(!string.IsNullOrWhiteSpace(r["ALM_ID"].ToString()))
                        p.AlmacenId = Convert.ToInt32(r["ALM_ID"]);
                    p.Almacen = r["ALM_NOMBRE"].ToString();
                    p.Cantidad = p.Existencia;
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntProducto> ObtieneProductosPorAlmacen(int EmpresaId, int ProductoId, int AlmacenId, int TipoProductoId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosPorAlmacen(EmpresaId, ProductoId, AlmacenId, TipoProductoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.CodigoBarra = r["PRO_CODIGOBARRA"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.PrecioCosto = Convert.ToDecimal(r["PRO_PRECIOCOSTO"]);
                    p.Existencia = Convert.ToInt32(r["EXISTENCIA"]);
                    if (!string.IsNullOrWhiteSpace(r["ALM_ID"].ToString()))
                        p.AlmacenId = Convert.ToInt32(r["ALM_ID"]);
                    p.Almacen = r["ALM_NOMBRE"].ToString();
                    p.Cantidad = p.Existencia;
                    
                    //*****//
                    p.IncluyeIeps = true;
                    if (p.TipoProductoId == 3)//DONT KNOW WHY
                        p.IncluyeIeps = false;
                    //*****//
                    p.IncluyeIeps = Convert.ToBoolean(r["PRO_INCLUYEIEPS"]);

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        /// <summary>
        /// PRODUCTOS (CON O SIN EXISTENCIA) CON PRECIO VENTA CORRESPONDIENTE.
        /// </summary>
        /// <param name="ProductoId"></param>
        /// <param name="EstablecimientoId"></param>
        /// <param name="TipoProductoId"></param>
        /// <param name="ClienteId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<EntProducto> ObtieneProductosPorAlmacenConPreciosVenta(int ProductoId, int EstablecimientoId,
                                                                                    int TipoProductoId, int ClienteId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosPorAlmacenConPreciosVenta(ProductoId, EstablecimientoId,
                                                                                        TipoProductoId, ClienteId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.Existencia = Convert.ToInt32(r["EXISTENCIA"]);
                    p.AlmacenId = Convert.ToInt32(r["ALM_ID"]);
                    p.Almacen = r["ALM_NOMBRE"].ToString();
                    p.Cantidad = p.Existencia;
                    p.PrecioCosto = Convert.ToDecimal(r["PRO_PRECIOCOSTO"]);
                    p.IncluyeIeps = Convert.ToBoolean(r["IEPS"]);
                    p.PrecioVentaSinIVA = Convert.ToDecimal(r["PRECIO"]);
                    p.IEPS = Convert.ToDecimal(r["IEPS"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntProducto> ObtieneProductosExistenciaPorAlmacen(int EmpresaId, int ProductoId, int AlmacenId, int TipoProductoId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosExistentesPorAlmacen(EmpresaId, ProductoId, AlmacenId, TipoProductoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.CodigoBarra = r["PRO_CODIGOBARRA"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.PrecioCosto = Convert.ToDecimal(r["PRO_PRECIOCOSTO"]);
                    p.Existencia = Convert.ToInt32(r["EXISTENCIA"]);
                    p.AlmacenId = Convert.ToInt32(r["ALM_ID"]);
                    p.Almacen = r["ALM_NOMBRE"].ToString();
                    p.Cantidad = p.Existencia;
                    
                    p.IncluyeIeps = true;
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntProducto> ObtieneProductosExistenciaPorAlmacenConPreciosVenta(int ProductoId, int EstablecimientoId, 
                                                                                    int TipoProductoId, int ClienteId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosExistentesPorAlmacenConPreciosVenta(ProductoId, EstablecimientoId, 
                                                                                        TipoProductoId, ClienteId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    //p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
                    //p.TipoProducto = r["TIPPRO_DESCRIPCION"].ToString();
                    p.Existencia = Convert.ToInt32(r["EXISTENCIA"]);
                    p.AlmacenId = Convert.ToInt32(r["ALM_ID"]);
                    p.Almacen = r["ALM_NOMBRE"].ToString();
                    p.Cantidad = p.Existencia;
                    p.PrecioCosto = Convert.ToDecimal(r["PRO_PRECIOCOSTO"]);
                    p.IncluyeIeps = Convert.ToBoolean(r["IEPS"]);
                    p.PrecioVentaSinIVA = Convert.ToDecimal(r["PRECIO"]);
                    p.IEPS = Convert.ToDecimal(r["IEPS"]);

                    //p.ProductoServicioId = Convert.ToInt32(r["PRO_TIPOPRODUCTOSERVICIOID"]);
                    //p.ProductoServicio = r["CATPRO_DESCRIPCION"].ToString();
                    //p.ClaveProductoServicio = r["CATPRO_CLAVE"].ToString();
                    //p.UnidadId = Convert.ToInt32(r["PRO_TIPOUNIDADID"]);
                    //p.Unidad = r["CATUNI_DESCRIPCION"].ToString();
                    //p.ClaveUnidad = r["CATUNI_CLAVE"].ToString();
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntProducto> ObtieneProductosExistenciaMinMax(int EmpresaId, int EstablecimientoId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosExistentesMinMax(EmpresaId, EstablecimientoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    
                    p.Existencia = Convert.ToInt32(r["EXISTENCIA"]);
                    p.ExistenciaMinima = Convert.ToDecimal(r["Minimo"]);
                    p.ExistenciaMaxima = Convert.ToDecimal(r["Maximo"]);

                    //if (p.Existencia < p.ExistenciaMinima)
                    //    p.Cantidad = p.ExistenciaMinima - p.Existencia;// Convert.ToInt32(r["Pedido"]); 
                    //else if (p.Existencia > p.ExistenciaMaxima)
                    p.Cantidad = p.ExistenciaMaxima - p.Existencia;
                    //p.Cantidad = Convert.ToInt32(r["Pedido"]);

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<EntProducto> ObtieneProductosExistenciaConsigna(int EmpresaId, int TrabajadorId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                if (TrabajadorId < 0)
                    TrabajadorId = 0;
                dt = new DatProductos().obtieneProductosExistenciaConsigna(EmpresaId, TrabajadorId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.CodigoBarra = r["PRO_CODIGOBARRA"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.PrecioCosto = Convert.ToDecimal(r["PRO_PRECIOCOSTO"]);
                    p.Existencia = Convert.ToInt32(r["EXISTENCIA"]);
                    if (!string.IsNullOrWhiteSpace(r["TRA_ID"].ToString()))
                        p.AlmacenId = Convert.ToInt32(r["TRA_ID"]);
                    p.Almacen = r["TRA_NOMBRE"].ToString();
                    p.Cantidad = p.Existencia;

                    //*****//
                    p.IncluyeIeps = true;
                    if (p.TipoProductoId == 3)//DONT KNOW WHY
                        p.IncluyeIeps = false;
                    //*****//
                    p.IncluyeIeps = Convert.ToBoolean(r["PRO_INCLUYEIEPS"]);

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<EntProducto> ObtieneListaPreciosProducto(int EstablecimientoId, int ProductoId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneListaPreciosProducto(EstablecimientoId, ProductoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    //p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    //p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
                    //p.TipoProducto = r["TIPPRO_DESCRIPCION"].ToString();
                    //p.PrecioCosto = Convert.ToDecimal(r["PRO_PRECIOCOSTO"]);
                    p.PrecioVenta = Convert.ToDecimal(r["MENUDEO"]);
                    p.PrecioVenta2 = Convert.ToDecimal(r["MAYOREOLOCAL"]);
                    p.PrecioVenta3 = Convert.ToDecimal(r["SONORA"]);
                    p.PrecioVenta4 = Convert.ToDecimal(r["CULIACAN"]);
                    p.PrecioEspecial = Convert.ToDecimal(r["ESPECIAL"]);
                    p.PrecioEspecial2 = Convert.ToDecimal(r["CABO"]);
                    p.PrecioEspecial3 = Convert.ToDecimal(r["CONVENIENCIA"]);
                    p.PrecioEspecial4 = Convert.ToDecimal(r["EVENTUAL"]);
                    p.PrecioEspecial5 = Convert.ToDecimal(r["HERMOSILLO"]);
                    p.PrecioEspecialDetalle = Convert.ToDecimal(r["DETALLE"]);
                    p.PrecioEspecialDetalleM = Convert.ToDecimal(r["DETALLEMAYOREO"]);
                    p.PrecioEspecialComercial = Convert.ToDecimal(r["CADENACOMERCIAL"]);
                    //p.Existencia = Convert.ToInt32(r["PRO_EXISTENCIA"]);
                    //p.Cantidad = p.Existencia;
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public EntProducto ObtienePrecioProductoCantidad(int EstablecimientoId, int ProductoId, decimal Cantidad)
        {
            try
            {
                EntProducto p = new EntProducto();
                dt = new DatProductos().obtienePrecioProductoPorCantidad(EstablecimientoId, ProductoId, Cantidad);
                foreach (DataRow r in dt.Rows)
                {
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    p.PrecioVenta = Convert.ToDecimal(r["PRECAN_PRECIO"]);
                    p.Cantidad = Convert.ToDecimal(r["PRECAN_CANTIDAD"]);
                }
                return p;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

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
                    p.Codigo= r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();

                    //p.Unidad = "PIEZA";
                    p.ProductoServicioId = Convert.ToInt32(r["PRO_TIPOPRODUCTOSERVICIOID"]);
                    p.ProductoServicio = r["CATPRO_DESCRIPCION"].ToString();
                    p.ClaveProductoServicio = r["CATPRO_CLAVE"].ToString();
                    p.UnidadId = Convert.ToInt32(r["PRO_TIPOUNIDADID"]);
                    p.Unidad = r["CATUNI_DESCRIPCION"].ToString();
                    p.ClaveUnidad = r["CATUNI_CLAVE"].ToString();

                    p.PrecioCosto = Convert.ToDecimal(r["PROPED_PRECIOCOSTO"]);
                    p.Cantidad = Convert.ToInt32(r["CANTIDAD"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PROPED_PRECIOVENTA"]);
                    p.IncluyeIeps= Convert.ToBoolean(r["PRO_INCLUYEIEPS"]);
                    if (p.IncluyeIeps)
                    {
                        p.PrecioVentaSinIVA = Math.Round(p.PrecioVenta / 1.08m, 2);
                        p.IEPS = p.PrecioVenta - p.PrecioVentaSinIVA;
                    }
                    else
                        p.PrecioVentaSinIVA = p.PrecioVenta;

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntProducto> ObtieneProductosPorPedidoPreVenta(int PedidoPreVentaId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneProductosPorPedidoPreVenta(PedidoPreVentaId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.CodigoBarra = r["PRO_CODIGOBARRA"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();

                    p.ProductoServicioId = Convert.ToInt32(r["PRO_TIPOPRODUCTOSERVICIOID"]);
                    p.ProductoServicio = r["CATPRO_DESCRIPCION"].ToString();
                    p.ClaveProductoServicio = r["CATPRO_CLAVE"].ToString();
                    p.UnidadId = Convert.ToInt32(r["PRO_TIPOUNIDADID"]);
                    p.Unidad = r["CATUNI_DESCRIPCION"].ToString();
                    p.ClaveUnidad = r["CATUNI_CLAVE"].ToString();

                    //p.PrecioCosto = Convert.ToDecimal(r["PROPED_PRECIOCOSTO"]);
                    p.Cantidad = Convert.ToInt32(r["PROPEDPRE_CANTIDAD"]);
                    p.IEPS = Convert.ToDecimal(r["PROPEDPRE_IEPS"]);
                    p.IVA = Convert.ToDecimal(r["PROPEDPRE_IVA"]);
                    p.PrecioVenta = Convert.ToDecimal(r["PROPEDPRE_PRECIOVENTA"]);
                    p.PrecioVentaSinIVA = p.PrecioVenta - p.IEPS - p.IVA;
                    //subtotal = Math.Round(total, 2) / (1 + IVA);
                    p.IncluyeIeps = Convert.ToBoolean(r["PRO_INCLUYEIEPS"]);
                    //if (p.IncluyeIeps)
                    //{
                    //    p.PrecioVentaSinIVA = Math.Round(p.PrecioVenta / 1.08m, 2);
                    //    p.IEPS = p.PrecioVenta - p.PrecioVentaSinIVA;
                    //}
                    //else
                    //    p.PrecioVentaSinIVA = p.PrecioVenta;

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

        public List<EntProducto> ObtieneHistorialProductosPrecioCosto(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta, 
                                                                        string Empresa)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().selObtieneHistorialProductosPrecioCosto(EmpresaId, FechaDesde, FechaHasta);

                string fechaDesde = FechaDesde.ToString(" MM - yyyy ");
                string fechaHasta = FechaHasta.ToString(" MM - yyyy ");

                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString().ToString().Trim();
                    p.Ingreso = Empresa;

                    if (fechaDesde == fechaHasta)
                        p.FechaCorta = fechaDesde;
                    else
                        p.FechaCorta = fechaDesde + "  |  " + fechaHasta;
                    p.PrecioCosto = Convert.ToDecimal(r["EXISTENCIA"]);
                    p.PrecioEspecial = Convert.ToDecimal(r["ENTRADAS"]);
                    //p.PrecioEspecial2 = Convert.ToDecimal(r["ENTRADASTRASPASOS"]);
                    p.PrecioVenta = Convert.ToDecimal(r["SALIDAS"]);
                    //p.PrecioVentaSinIVA = Convert.ToDecimal(r["SALIDASTRASPASOS"]);
                    p.PrecioVenta2 = Convert.ToDecimal(r["EFINAL"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntProducto> ObtieneHistorialProductosPrecioCostoCodigo(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta,
                                                                        string Empresa)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().selObtieneHistorialProductosPrecioCosto(EmpresaId, FechaDesde, FechaHasta);

                string fechaDesde = FechaDesde.ToString(" MM - yyyy ");
                string fechaHasta = FechaHasta.ToString(" MM - yyyy ");

                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString().ToString().Trim();
                    p.Descripcion = r["PRO_CODIGO"].ToString() + "-" + r["PRO_DESCRIPCION"].ToString();
                    p.Ingreso = Empresa;

                    if (fechaDesde == fechaHasta)
                        p.FechaCorta = fechaDesde;
                    else
                        p.FechaCorta = fechaDesde + "  |  " + fechaHasta;
                    p.PrecioCosto = Convert.ToDecimal(r["EXISTENCIA"]);
                    p.PrecioEspecial = Convert.ToDecimal(r["ENTRADAS"]);
                    //p.PrecioEspecial2 = Convert.ToDecimal(r["ENTRADASTRASPASOS"]);
                    p.PrecioVenta = Convert.ToDecimal(r["SALIDAS"]);
                    //p.PrecioVentaSinIVA = Convert.ToDecimal(r["SALIDASTRASPASOS"]);
                    p.PrecioVenta2 = Convert.ToDecimal(r["EFINAL"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntProducto> ObtieneHistorialProductos(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta,
                                                                        string Empresa)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().selObtieneHistorialProductos(EmpresaId, FechaDesde, FechaHasta);

                string fechaDesde = FechaDesde.ToString(" MM - yyyy ");
                string fechaHasta = FechaHasta.ToString(" MM - yyyy ");

                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString().ToString().Trim();
                    p.Ingreso = Empresa;

                    if (fechaDesde == fechaHasta)
                        p.FechaCorta = fechaDesde;
                    else
                        p.FechaCorta = fechaDesde + "  |  " + fechaHasta;
                    
                    p.Existencia = Convert.ToInt32(r["EXISTENCIA"]);
                    p.IngresoId = Convert.ToInt32(r["ENTRADAS"]);
                    //p.EstatusId = Convert.ToInt32(r["ENTRADASTRASPASOS"]);
                    p.ProductoId = Convert.ToInt32(r["SALIDAS"]);
                    //p.TipoProductoId = Convert.ToInt32(r["SALIDASTRASPASOS"]);
                    p.Cantidad = Convert.ToInt32(r["EFINAL"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntProducto> ObtieneHistorialProductosCodigo(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta,
                                                                        string Empresa)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().selObtieneHistorialProductos(EmpresaId, FechaDesde, FechaHasta);

                string fechaDesde = FechaDesde.ToString(" MM - yyyy ");
                string fechaHasta = FechaHasta.ToString(" MM - yyyy ");

                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["PRO_ID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString().ToString().Trim();
                    p.Descripcion = r["PRO_CODIGO"].ToString() + "-" + r["PRO_DESCRIPCION"].ToString();
                    p.Ingreso = Empresa;

                    if (fechaDesde == fechaHasta)
                        p.FechaCorta = fechaDesde;
                    else
                        p.FechaCorta = fechaDesde + "  |  " + fechaHasta;

                    p.Existencia = Convert.ToInt32(r["EXISTENCIA"]);
                    p.IngresoId = Convert.ToInt32(r["ENTRADAS"]);
                    //p.EstatusId = Convert.ToInt32(r["ENTRADASTRASPASOS"]);
                    p.ProductoId = Convert.ToInt32(r["SALIDAS"]);
                    //p.TipoProductoId = Convert.ToInt32(r["SALIDASTRASPASOS"]);
                    p.Cantidad = Convert.ToInt32(r["EFINAL"]);
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<EntProducto> ObtieneHistorialProductosDetalle(int EmpresaId, int ProductoId, int ExistenciaI, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                int existenciaF = ExistenciaI;
                dt = new DatProductos().ObtieneHistorialProductosDetalle(EmpresaId,ProductoId, FechaDesde, FechaHasta);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();

                    p.Fecha = Convert.ToDateTime(r["FECHA"]);
                    p.TipoProductoId = Convert.ToInt32(r["TIPO"]);
                    p.IngresoId = Convert.ToInt32(r["ENTRADAS"]);
                    p.ProductoId = Convert.ToInt32(r["SALIDAS"]);
                    p.ProductoServicioId = Convert.ToInt32(r["TRASPASOS"]);

                    if (p.TipoProductoId == 2)//ENTRADA
                        existenciaF += p.IngresoId;
                    else if (p.TipoProductoId == 3)//SALIDA
                        existenciaF -= p.ProductoId;
                    else if (p.TipoProductoId == 4)//SALIDA
                        existenciaF -= p.ProductoServicioId;

                    p.Existencia = existenciaF;

                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntProducto> ObtieneHistorialProductosDetallePrecioCosto(int EmpresaId, int ProductoId, decimal ExistenciaI, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                decimal existenciaF = ExistenciaI;
                dt = new DatProductos().ObtieneHistorialProductosDetallePrecioCosto(EmpresaId,ProductoId, FechaDesde, FechaHasta);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();

                    p.Fecha = Convert.ToDateTime(r["FECHA"]);
                    p.TipoProductoId = Convert.ToInt32(r["TIPO"]);
                    p.PrecioCosto = Convert.ToInt32(r["ENTRADAS"]);
                    p.PrecioVenta = Convert.ToInt32(r["SALIDAS"]);
                    p.PrecioVentaSinIVA = Convert.ToInt32(r["TRASPASOS"]);

                    if (p.TipoProductoId == 2)//ENTRADA
                        existenciaF += p.PrecioCosto;
                    else if (p.TipoProductoId == 3)//SALIDA
                        existenciaF -= p.PrecioVenta;
                    else if (p.TipoProductoId == 4)//TRASPASO
                        existenciaF -= p.PrecioVentaSinIVA;

                    p.PrecioVenta2 = existenciaF;

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
        /// p.Id = Convert.ToInt32(r["FACING_ID"]);
        ///p.PedidoId = Convert.ToInt32(r["ING_ID"]);
        ///p.Descripcion = r["ING_DESCRIPCION"].ToString();
        ///p.Fecha = Convert.ToDateTime(r["ING_FECHA"]);
        ///p.Nombre = r["PROV_NOMBRE"].ToString();
        ///p.NumeroFactura = r["FACING_NUMEROFACTURA"].ToString();
        ///p.Total = Convert.ToDecimal(r["FACING_TOTAL"]);
        ///p.MetodoPago = r["CATMET_DESCRIPCION"].ToString();
        ///p.FormaPago = r["CATFOR_DESCRIPCION"].ToString();
        /// </summary>
        /// <param name="IngresoId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public EntFactura ObtieneFacturaIngresoPorIngreso(int IngresoId)
        {
            try
            {
                EntFactura p = new EntFactura();
                dt = new DatProductos().obtieneIngreso(IngresoId);
                foreach (DataRow r in dt.Rows)
                {
                    p.Id = Convert.ToInt32(r["FACING_ID"]);
                    p.PedidoId = Convert.ToInt32(r["ING_ID"]);
                    p.Descripcion = r["ING_DESCRIPCION"].ToString();
                    //p.Fecha = Convert.ToDateTime(r["FACING_FECHAFACTURA"]);
                    p.Fecha = Convert.ToDateTime(r["ING_FECHA"]);
                    p.Nombre = r["PROV_NOMBRE"].ToString();
                    p.NumeroFactura = r["FACING_NUMEROFACTURA"].ToString();
                    p.Total = Convert.ToDecimal(r["FACING_TOTAL"]);
                    p.MetodoPago = r["CATMET_DESCRIPCION"].ToString();
                    p.FormaPago = r["CATFOR_DESCRIPCION"].ToString();
                }
                return p;
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

        public List<EntCatalogoGenerico> ObtieneMovimientosProductos(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta,
                                                                                int AlmacenId)
        {
            try
            {
                List<EntCatalogoGenerico> lst = new List<EntCatalogoGenerico>();
                dt = new DatProductos().obtieneMovimientosProductosPorFechas(EmpresaId, FechaDesde, FechaHasta, 3, AlmacenId);
                foreach (DataRow r in dt.Rows)
                {
                    EntCatalogoGenerico p = new EntCatalogoGenerico();
                    p.Id = Convert.ToInt32(r["MOVINV_ID"]);
                    p.ClaveId = Convert.ToInt32(r["MOVINV_ORIENTACION"]);
                    p.Clave = r["MOVINV_ORIENTACIONTXT"].ToString();
                    p.Descripcion = r["MOVINV_COMENTARIO"].ToString();
                    p.Detalle = r["MOVINV_PREVIEW"].ToString();
                    p.Fecha = Convert.ToDateTime(r["MOVINV_FECHA"]);
                    lst.Add(p);
                }
                return lst.OrderByDescending(P => P.Fecha).ToList();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntCatalogoGenerico> ObtieneMovimientosEntradasProductos(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta,
                                                                             int AlmacenId)
        {
            try
            {
                List<EntCatalogoGenerico> lst = new List<EntCatalogoGenerico>();
                dt = new DatProductos().obtieneMovimientosProductosPorFechas(EmpresaId, FechaDesde, FechaHasta, 1, AlmacenId);
                foreach (DataRow r in dt.Rows)
                {
                    EntCatalogoGenerico p = new EntCatalogoGenerico();
                    p.Id = Convert.ToInt32(r["MOVINV_ID"]);
                    p.ClaveId = 1;
                    p.Clave = r["MOVINV_ORIENTACIONTXT"].ToString();
                    p.Descripcion = r["MOVINV_COMENTARIO"].ToString();
                    p.Detalle= r["MOVINV_PREVIEW"].ToString();
                    p.Fecha = Convert.ToDateTime(r["MOVINV_FECHA"]);
                    p.IdSecundario = Convert.ToInt32(r["MOVINV_PEDIDOID"]);//EN ENTRADAS ES INGRESOID;
                    p.TipoId = Convert.ToInt32(r["MOINV_TIPOMOVIMIENTOINVENTARIOID"]);
                    p.Usuario= r["USU_NOMBRE"].ToString();
                    p.Usuario +=" | "+ r["tipmovinv_nombre"].ToString();
                    lst.Add(p);
                }
                return lst.OrderByDescending(P => P.Fecha).ToList();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntCatalogoGenerico> ObtieneMovimientosEntradasProductos(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta,
                                                                                int AlmacenId, int TipoMovimientoId)
        {
            try
            {
                List<EntCatalogoGenerico> lst = new List<EntCatalogoGenerico>();
                dt = new DatProductos().obtieneMovimientosProductosPorFechas(EmpresaId, FechaDesde, FechaHasta, 1, 
                                                                                AlmacenId, TipoMovimientoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntCatalogoGenerico p = new EntCatalogoGenerico();
                    p.Id = Convert.ToInt32(r["MOVINV_ID"]);
                    p.ClaveId = 1;
                    p.Clave = r["MOVINV_ORIENTACIONTXT"].ToString();
                    p.Descripcion = r["MOVINV_COMENTARIO"].ToString();
                    p.Detalle = r["MOVINV_PREVIEW"].ToString();
                    p.Fecha = Convert.ToDateTime(r["MOVINV_FECHA"]);
                    lst.Add(p);
                }
                return lst.OrderByDescending(P => P.Fecha).ToList();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        
        public List<EntCatalogoGenerico> ObtieneMovimientosSalidasProductos(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta,
                                                                                int AlmacenId)
        {
            try
            {
                List<EntCatalogoGenerico> lst = new List<EntCatalogoGenerico>();
                dt = new DatProductos().obtieneMovimientosProductosPorFechas(EmpresaId, FechaDesde, FechaHasta, 2, AlmacenId);
                foreach (DataRow r in dt.Rows)
                {
                    EntCatalogoGenerico p = new EntCatalogoGenerico();
                    p.Id = Convert.ToInt32(r["MOVINV_ID"]);
                    p.IdSecundario = Convert.ToInt32(r["MOVINV_PEDIDOID"]);
                    p.ClaveId = 2;
                    p.Clave = r["MOVINV_ORIENTACIONTXT"].ToString();
                    p.Descripcion = r["MOVINV_COMENTARIO"].ToString();
                    p.Detalle = r["MOVINV_PREVIEW"].ToString();
                    p.Fecha = Convert.ToDateTime(r["MOVINV_FECHA"]);
                    lst.Add(p);
                }
                return lst.OrderByDescending(P => P.Fecha).ToList();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntCatalogoGenerico> ObtieneMovimientosSalidasProductos(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta,
                                                                           int AlmacenId, int TipoMovimientoId)
        {
            try
            {
                List<EntCatalogoGenerico> lst = new List<EntCatalogoGenerico>();
                dt = new DatProductos().obtieneMovimientosProductosPorFechas(EmpresaId, FechaDesde, FechaHasta, 2, AlmacenId, TipoMovimientoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntCatalogoGenerico p = new EntCatalogoGenerico();
                    p.Id = Convert.ToInt32(r["MOVINV_ID"]);
                    p.IdSecundario = Convert.ToInt32(r["MOVINV_PEDIDOID"]);
                    p.ClaveId = 2;
                    p.Clave = r["MOVINV_ORIENTACIONTXT"].ToString();
                    p.Descripcion = r["MOVINV_COMENTARIO"].ToString();
                    p.Detalle = r["MOVINV_PREVIEW"].ToString();
                    p.Fecha = Convert.ToDateTime(r["MOVINV_FECHA"]);
                    p.EstatusDescripcion=r["ESTADO"].ToString();
                    lst.Add(p);
                }
                return lst.OrderByDescending(P => P.Fecha).ToList();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<EntProducto> ObtieneMovimientosDetalleProductos(int TipoMovimientoId, int MovimientoId, int IngresoId=0)
        {
            try
            {
                EntFactura facturaIngreso = ObtieneFacturaIngresoPorIngreso(IngresoId);
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneMovimientosDetalleProductosPorFechas(MovimientoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["MOVINVDET_ID"]);
                    p.ProductoServicioId = Convert.ToInt32(r["MOVINV_PEDIDOID"]);
                    p.IngresoId = MovimientoId;
                    p.ProductoId = Convert.ToInt32(r["MOVINVDET_PRODUCTOID"]);
                    //p.Descripcion = r["MOVINV_ORIENTACION"].ToString();
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    //p.Fecha = Convert.ToDateTime(r["MOVINV_FECHA"]);
                    p.Cantidad= Convert.ToDecimal(r["MOVINVDET_CANTIDAD"]);

                    try 
                    {
                        if (TipoMovimientoId == 1)
                            p.PrecioCosto = Convert.ToDecimal(r["MOVINVDET_TOTAL"]) / p.Cantidad;
                        else
                            p.PrecioVenta = Convert.ToDecimal(r["MOVINVDET_TOTAL"]) / p.Cantidad;
                    }catch(DivideByZeroException dEx){ }
                    
                    p.Marca = facturaIngreso.NumeroFactura;
                    p.Modelo = facturaIngreso.Nombre;
                    p.Fecha = facturaIngreso.Fecha;
                    p.FechaCorta = facturaIngreso.Fecha.ToShortDateString();
                    lst.Add(p);
                }

                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntProducto> ObtieneMovimientosDetalleProductosSinDatosFactura(int TipoMovimientoId, int MovimientoId, int IngresoId = 0)
        {
            try
            {
                //EntFactura facturaIngreso = ObtieneFacturaIngresoPorIngreso(IngresoId);
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneMovimientosDetalleProductosPorFechas(MovimientoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    p.Id = Convert.ToInt32(r["MOVINVDET_ID"]);
                    //p.IngresoId = MovimientoId;
                    p.IngresoId = Convert.ToInt32(r["MOVINV_PEDIDOID"]);
                    p.ProductoId = Convert.ToInt32(r["MOVINVDET_PRODUCTOID"]);
                    p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = p.ProductoId.ToString()+"-"+ r["PRO_DESCRIPCION"].ToString();
                    p.Cantidad = Convert.ToDecimal(r["MOVINVDET_CANTIDAD"]);
                    try { 
                        if (TipoMovimientoId == 1)
                            p.PrecioCosto = Convert.ToDecimal(r["MOVINVDET_TOTAL"]) / p.Cantidad;
                        else
                            p.PrecioVenta = Convert.ToDecimal(r["MOVINVDET_TOTAL"]) / p.Cantidad;
                    }
                    catch (DivideByZeroException dEx) { }
                    
                    //p.Marca = facturaIngreso.NumeroFactura;
                    //p.Modelo = facturaIngreso.Nombre;
                    //p.Fecha = facturaIngreso.Fecha;
                    //p.FechaCorta = facturaIngreso.Fecha.ToShortDateString();
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public List<EntCatalogoGenerico> ObtieneMovimientosTraspasosProductos(int EmpresaId,int Orientacion, DateTime FechaDesde, DateTime FechaHasta,
                                                                                int Estado, int AlmacenId)
        {
            try
            {
                List<EntCatalogoGenerico> lst = new List<EntCatalogoGenerico>();
                if(Orientacion==1)//RECIBE
                    dt = new DatProductos().obtieneMovimientosTraspasosRecepcionProductosPorFechas(EmpresaId, FechaDesde, FechaHasta, Estado, AlmacenId);
                else if (Orientacion == 2)//ENVIA
                    dt = new DatProductos().obtieneMovimientosTraspasosEnvioProductosPorFechas(EmpresaId, FechaDesde, FechaHasta, Estado, AlmacenId);
                foreach (DataRow r in dt.Rows)
                {
                    EntCatalogoGenerico p = new EntCatalogoGenerico();
                    p.Id = Convert.ToInt32(r["MOVINV_ID"]);
                    p.IdSecundario = Convert.ToInt32(r["TRA_ID"]);
                    p.ClaveId = Convert.ToInt32(r["ALMACENORIGENID"]);
                    p.Clave = r["ALMACENORIGEN"].ToString();
                    p.Descripcion = r["MOVINV_COMENTARIO"].ToString();
                    p.Detalle = r["MOVINV_PREVIEW"].ToString();
                    p.Fecha = Convert.ToDateTime(r["MOVINV_FECHA"]);
                    lst.Add(p);
                }
                return lst.OrderByDescending(P => P.Fecha).ToList();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<EntProducto> ObtieneMovimientosDetalleTraspasoProductos(int TraspasoId)
        {
            try
            {
                List<EntProducto> lst = new List<EntProducto>();
                dt = new DatProductos().obtieneMovimientosDetalleTraspasoProductosPorFechas(TraspasoId);
                foreach (DataRow r in dt.Rows)
                {
                    EntProducto p = new EntProducto();
                    //p.Id = Convert.ToInt32(r["MOVINVDET_ID"]);
                    p.ProductoId = Convert.ToInt32(r["TRADET_PRODUCTOID"]);
                    //p.Descripcion = r["MOVINV_ORIENTACION"].ToString();
                    //p.Codigo = r["PRO_CODIGO"].ToString();
                    p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                    //p.Fecha = Convert.ToDateTime(r["MOVINV_FECHA"]);
                    p.Cantidad = Convert.ToDecimal(r["TRADET_CANTIDADENVIADA"]);
                    //if (TipoMovimientoId == 1)
                        //p.PrecioCosto = Convert.ToDecimal(r["MOVINVDET_TOTAL"]) / p.Cantidad;
                    //else
                    //    p.PrecioVenta = Convert.ToDecimal(r["MOVINVDET_TOTAL"]) / p.Cantidad;
                    lst.Add(p);
                }
                return lst;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Agrega nuevo Producto.
        /// </summary>
        /// <param name="producto">
        /// Propiedades Necesarias: TipoProductoId ,Descripcion, PrecioCosto.
        /// </param>
        public int AgregaProducto(EntProducto producto)
        {
            try
            {
                return new DatProductos().agregaProducto(producto.EmpresaId, producto.TipoProductoId, producto.Codigo, producto.CodigoBarra, producto.Descripcion,
                                                        producto.Marca, producto.Modelo,
                                                        producto.ProductoServicioId, producto.UnidadId, 
                                                        producto.PrecioCosto, producto.IncluyeIeps);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public int AgregaProductoIngreso(int IngresoId, EntProducto producto)
        {
            try
            {
                return new DatProductos().agregaProductoIngreso(producto.EmpresaId, IngresoId, producto.Id,
                                                        producto.Cantidad, producto.PrecioCosto, producto.IEPS, producto.IVA);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public int AgregaListaPreciosProducto(EntProducto producto, string Usuario)
        {
            try
            {
                return new DatProductos().agregaListaPreciosProducto(0, producto.Id, producto.PrecioVenta, producto.PrecioVenta2, 
                                                            producto.PrecioVenta3, producto.PrecioVenta4, 
                                                            producto.PrecioEspecial, producto.PrecioEspecial2, producto.PrecioEspecial3,
                                                            producto.PrecioEspecial4, producto.PrecioEspecial5,
                                                            producto.PrecioEspecialDetalle,
                                                            producto.PrecioEspecialDetalleM, producto.PrecioEspecialComercial, Usuario);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Agrega nuevo Ingreso.
        /// </summary>
        /// <param name="producto">
        /// Propiedades Necesarias: Id servira para EmpresaId.
        /// </param>
        public int AgregaIngreso(EntIngreso Ingreso)
        {
            try
            {
                return new DatProductos().agregaIngresoProducto(Ingreso.EmpresaId, Ingreso.ProveedorId, 
                                                                Ingreso.Descripcion, Ingreso.Fecha, Ingreso.UsuarioId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Actualiza campos de Producto.
        /// </summary>
        /// <param name="producto">
        /// Propiedades Necesarias: Id=Id del Producto a Actualizar;Campos a Actualizar: TipoProductoId, Descripcion, PrecioCosto.
        /// </param>
        public void ActualizaProducto(EntProducto producto)
        {
            try
            {
                new DatProductos().actualizaProducto(producto.Id, producto.TipoProductoId, producto.Codigo, producto.CodigoBarra, producto.Descripcion,                                                      producto.Marca, producto.Modelo,
                                                     producto.ProductoServicioId, producto.UnidadId, producto.PrecioCosto,
                                                     producto.IncluyeIeps);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void ActualizaProductoMinMax(EntProducto producto)
        {
            try
            {
                new DatProductos().actualizaProductoMinMax(producto.Id, producto.ExistenciaMinima, producto.ExistenciaMaxima);
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

        public void ActualizaProductosDetalle(int EmpresaId, int ProductoId, decimal PrecioCosto)
        {
            try
            {
                new DatProductos().actualizaProductosDetalle(EmpresaId, ProductoId, PrecioCosto);
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
        public void AumentaProducto(int ProductoId, decimal CantidadAumenta)
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
        public void ActualizaEstatusProductoDetalle(int ProductoId, int EstatusId)
        {
            try
            {
                new DatProductos().actualizaEstatusProductoDetalle(ProductoId, EstatusId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void ActualizaEstatusProductoDetalle(EntProducto producto, string Observacion)
        {
            try
            {
                new DatProductos().actualizaEstatusProductoDetalle(producto.Id, producto.EstatusId, Observacion);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

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
