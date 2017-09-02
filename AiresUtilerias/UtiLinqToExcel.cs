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
        public List<EntProducto> ListaProductos(string PathExcel)
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
                                 Fecha = row["FECHAINGRESO"].Cast<DateTime>()                             }
                             select item).ToList();

            book.Dispose();
            lst = resultado;
            return lst;
        }
    }
}
