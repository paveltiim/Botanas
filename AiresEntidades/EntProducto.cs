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


        public int TipoId { get; set; }
        public string Tipo { get; set; }

        public int ProductoServicioId { get; set; }
        public string ClaveProductoServicio { get; set; }
        public string ProductoServicio { get; set; }

        public int UnidadId { get; set; }
        public string ClaveUnidad { get; set; }
        public string Unidad { get; set; }

        public string Codigo { get; set; }
        public string CodigoBarra { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public int TipoProductoId { get; set; }
        public string TipoProducto { get; set; }
        public int AlmacenId { get; set; }
        public string Almacen { get; set; }

        public bool IncluyeIeps { get; set; }
        public decimal Cantidad { get; set; }
        public decimal ExistenciaMinima { get; set; }
        public decimal ExistenciaMaxima { get; set; }
        public int Existencia { get; set; }
        public decimal IEPS { get; set; }
        public decimal IVA { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioVentaSinIVA { get; set; }
        public decimal PrecioVenta2 { get; set; }
        public decimal PrecioVenta3 { get; set; }
        public decimal PrecioVenta4 { get; set; }
        public decimal PrecioEspecial { get; set; }
        public decimal PrecioEspecial2 { get; set; }
        public decimal PrecioEspecial3 { get; set; }
        public decimal PrecioEspecial4 { get; set; }
        public decimal PrecioEspecial5 { get; set; }
        public decimal PrecioEspecialDetalle { get; set; }
        public decimal PrecioEspecialDetalleM { get; set; }
        public decimal PrecioEspecialComercial { get; set; }

        /// <summary>
        /// Cantidad X PrecioCosto
        /// </summary>
        public decimal PrecioC { get { return Cantidad * PrecioCosto; } }
        /// <summary>
        /// Cantidad X PrecioVenta 
        /// </summary>
        public decimal Precio { get { return Math.Round(Cantidad * PrecioVenta,2); } }

        /// <summary>
        /// Cantidad X PrecioVenta2 
        /// </summary>
        public decimal Precio2 { get { return Cantidad * PrecioVenta2; } }

        /// <summary>
        /// Cantidad X PrecioVentaSinIVA 
        /// </summary>
        public decimal PrecioSinIVA { get { return Cantidad * PrecioVentaSinIVA; } }

        public string FechaCorta { get; set; }
    }
}
