using AiresEntidades;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresUtilerias
{
    public class UtiLinqToExcel
    {
        public List<EntProducto> ExcelToListaProductos(string PathExcel)
        {
            List<EntProducto> lst;
            var book = new ExcelQueryFactory(PathExcel);
            var resultado = (from row in book.Worksheet("Hoja1")
                             let item = new EntProducto
                             {
                                 Id = row["ID"].Cast<Int32>(),
                                 ProductoId= row["PRODUCTOID"].Cast<Int32>(),
                                 Codigo= row["CODIGO"].Cast<string>(),
                                 Descripcion = row["DESCRIPCION"].Cast<string>(),
                                 TipoProductoId= row["TIPOPRODUCTOID"].Cast<Int32>(),
                                 TipoProducto = row["TIPOPRODUCTO"].Cast<string>(),
                                 Serie = row["SERIE"].Cast<string>(),
                                 PrecioCosto = row["PRECIOCOSTO"].Cast<decimal>(),
                                 PrecioVenta = row["PRECIOVENTA"].Cast<decimal>(),
                                 IngresoId = row["INGRESOID"].Cast<Int32>(),
                                 Ingreso = row["INGRESO"].Cast<string>(),
                                 Fecha = row["FECHAINGRESO"].Cast<DateTime>(),
                                 EmpresaId = row["EMPRESAID"].Cast<Int32>(),
                                 EstatusId=1
                             }
                             select item).ToList();

            book.Dispose();
            lst = resultado;
            return lst;
        }
        public List<EntPedido> ExcelToListaPedidos(string PathExcel)
        {
            List<EntPedido> lst;
            var book = new ExcelQueryFactory(PathExcel);
            var resultado = (from row in book.Worksheet("Hoja1")
                             let item = new EntPedido
                             {
                                 //CLIENTEID  CLIENTE PEDIDODETALLE   TOTAL   PAGO    FECHA   FACTURADO   PEDIDOESTATUSID 
                                 //PRODUCTODETALLEID   CANTIDAD    PRECIOCOSTO PRECIOVENTA TIPOPRODUCTOID  PRODUCTOID     
                                 //NUMEROFACTURA    UUID    FECHAFACTURA    RUTAFACTURA

                                 ClienteId = row["CLIENTEID"].Cast<Int32>(),
                                 Cliente = row["CLIENTE"].Cast<string>(),
                                 Id = row["PEDIDOID"].Cast<Int32>(),
                                 Detalle = row["PEDIDODETALLE"].Cast<string>(),
                                 Total = row["TOTAL"].Cast<Int32>(),
                                 Pago = row["PAGO"].Cast<decimal>(),
                                 Fecha = row["FECHA"].Cast<DateTime>(),
                                 Facturado = row["FACTURADO"].Cast<bool>(),
                                 EstatusId = row["PEDIDOESTATUSID"].Cast<Int32>(),
                                 ProductoPedido = new EntProducto() { Id = row["PRODUCTODETALLEID"].Cast<Int32>()
                                                                    , Cantidad = row["CANTIDAD"].Cast<Int32>()
                                                                    , PrecioCosto= row["PRECIOCOSTO"].Cast<decimal>()
                                                                    , PrecioVenta= row["PRECIOVENTA"].Cast<decimal>()
                                                                    , TipoProductoId= row["TIPOPRODUCTOID"].Cast<Int32>()
                                                                    , ProductoId= row["PRODUCTOID"].Cast<Int32>()},
                                  
                                 Factura = row["NUMEROFACTURA"].Cast<string>(),
                                 UUID = row["UUID"].Cast<string>(),
                                 FechaEntrega = row["FECHAFACTURA"].Cast<DateTime>(),
                                 RutaFactura = row["RUTAFACTURA"].Cast<string>(),
                             }
                             select item).ToList();

            book.Dispose();
            lst = resultado;
            return lst;
        }
    }
}
