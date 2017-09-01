using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresEntidades
{
    public class EntProducto : EntAbstracta
    {
        public int ProductoId { get; set; }
        public int IngresoId { get; set; }
        public string Ingreso { get; set; }

        public int TipoUnidadId { get; set; }
        public string TipoUnidad { get; set; }

        public string Codigo { get; set; }
        public string Serie { get; set; }
        public int TipoProductoId { get; set; }
        public string TipoProducto { get; set; }
        public int Cantidad { get; set; }
        public int Existencia { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioVentaSinIVA { get; set; }
        public decimal PrecioVenta2 { get; set; }
        public decimal PrecioEspecial { get; set; }

        /// <summary>
        /// Cantidad X PrecioVenta 
        /// </summary>
        public decimal PrecioC { get { return Cantidad * PrecioCosto; } }
        /// <summary>
        /// Cantidad X PrecioVenta 
        /// </summary>
        public decimal Precio { get { return Cantidad * PrecioVenta; } }

        /// <summary>
        /// Cantidad X PrecioVenta2 
        /// </summary>
        public decimal Precio2 { get { return Cantidad * PrecioVenta2; } }

        /// <summary>
        /// Cantidad X PrecioVentaSinIVA 
        /// </summary>
        public decimal PrecioSinIVA { get { return Cantidad * PrecioVentaSinIVA; } }

        public int EmpresaId { get; set; }
        public string FechaCorta { get; set; }
    }
}
