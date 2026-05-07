using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresEntidades
{
    public class EntPedido:EntAbstracta
    {
        public int SiguienteFacturaId { get; set; }
        public int PedidoRelacionadoId { get; set; }
        public int TipoPedidoId { get; set; }
        public string TipoPedido { get; set; }
        public string NumOrden { get; set; }
        public string NumOrdenRelacion { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public decimal TotalSinIVA { get { return Total - IVA; } }
        public decimal Pago { get; set; }
        public decimal NotasCredito { get; set; }
        public decimal TotalSinRetenciones { get { return Total - IVARetencion - ISRRetencion; } }
        public decimal IEPS{ get; set; }
        /// <summary>
        /// Porcentaje manual de IEPS a aplicar en el complemento de pago (0 = cálculo automático).
        /// </summary>
        public decimal IEPSManualPorcentaje { get; set; } = 0;
        public decimal IVA { get; set; }
        public decimal IVARetencion { get; set; }
        public decimal ISRRetencion { get; set; }
        public decimal Debe { get { return Total - Pago - NotasCredito; } }
        public decimal DebeFacturas { get; set; }
        public decimal Descuento { get; set; }
        public decimal PagoTotal { get; set; }
        public decimal Saldo { get; set; }

        public int ClienteId { get; set; }
        public string Cliente { get; set; }
        public string ClienteCodigo { get; set; }
        public string ClienteRFC { get; set; }
        public string ClienteNombreFiscal { get; set; }
        public string NumCliente { get { return ClienteId.ToString().PadLeft(4, '0'); } }

        public string Detalle { get; set; }
        /// <summary>
        /// Propiedad calculada que extrae la sucursal desde el Detalle.
        /// Regla: si el texto inicia con "SUC:" entonces lo que sigue es la sucursal.
        /// Si no inicia con "SUC:", regresa null.
        /// </summary>
        public string SucursalDetalle
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Detalle))
                    return null;

                var txt = Detalle.Trim();

                if (!txt.StartsWith("SUC:", StringComparison.OrdinalIgnoreCase))
                    return null;

                // Remover "SUC:" y limpiar espacios
                string resto = txt.Trim();

                // Buscar el delimitador '|'
                int pos = resto.IndexOf('|');
                if (pos >= 0)
                {
                    // Extraer solo lo que está antes del '|'
                    resto = resto.Substring(0, pos).Trim();
                }

                return string.IsNullOrWhiteSpace(resto) ? null : resto;
            }
        }

        public string Solicitud { get; set; }
        public string Direccion { get; set; }
        public string Sucursal{ get; set; }
        public string Observaciones { get; set; }
 
        public int DiasCredito { get; set; }
        public int DiasVencidos { get; set; }
        public DateTime FechaVencimiento { get; set; }
        
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaPago { get; set; }
        public string FechaCorta { get; set; }

        public int EmpleadoId { get; set; }
        public string Empleado { get; set; }
        public int TrabajadorId { get; set; }
        public string Trabajador { get; set; }

        public int FacturaId { get; set; }
        public string Factura { get; set; }
        public bool Facturado { get; set; }
        public string UUID { get; set; }
        public int FormaPagoId { get; set; }
        public int MetodoPagoId { get; set; }

        public string RutaFactura { get; set; }

        public byte[] PDF { get; set; }
        public byte[] XML { get; set; }

        public string VersionCFDI { get; set; }

        public int ProductoId { get; set; }
        public decimal ProductoPrecio { get; set; }

        public int PagoId { get; set; }

        public EntProducto ProductoPedido { get; set; }
        public EntPedido Copiar()
        {
            return new EntPedido
            {
                // EntAbstracta
                Id = this.Id,
                UsuarioId = this.UsuarioId,
                Descripcion = this.Descripcion,
                Estatus = this.Estatus,
                EstatusDescripcion = this.EstatusDescripcion,
                EstatusId = this.EstatusId,
                Fecha = this.Fecha,
                EmpresaId = this.EmpresaId,

                // EntPedido
                SiguienteFacturaId = this.SiguienteFacturaId,
                PedidoRelacionadoId = this.PedidoRelacionadoId,
                TipoPedidoId = this.TipoPedidoId,
                TipoPedido = this.TipoPedido,
                NumOrden = this.NumOrden,
                NumOrdenRelacion = this.NumOrdenRelacion,
                SubTotal = this.SubTotal,
                Total = this.Total,
                Pago = this.Pago,
                NotasCredito = this.NotasCredito,
                IEPS = this.IEPS,
                IEPSManualPorcentaje = this.IEPSManualPorcentaje,
                IVA = this.IVA,
                IVARetencion = this.IVARetencion,
                ISRRetencion = this.ISRRetencion,
                Descuento = this.Descuento,
                PagoTotal = this.PagoTotal,
                Saldo = this.Saldo,

                ClienteId = this.ClienteId,
                Cliente = this.Cliente,
                ClienteCodigo = this.ClienteCodigo,
                ClienteRFC = this.ClienteRFC,
                ClienteNombreFiscal = this.ClienteNombreFiscal,

                Detalle = this.Detalle,
                Solicitud = this.Solicitud,
                Direccion = this.Direccion,
                Sucursal = this.Sucursal,
                Observaciones = this.Observaciones,

                DiasCredito = this.DiasCredito,
                DiasVencidos = this.DiasVencidos,
                FechaVencimiento = this.FechaVencimiento,
                FechaEntrega = this.FechaEntrega,
                FechaPago = this.FechaPago,
                FechaCorta = this.FechaCorta,

                EmpleadoId = this.EmpleadoId,
                Empleado = this.Empleado,
                TrabajadorId = this.TrabajadorId,
                Trabajador = this.Trabajador,

                FacturaId = this.FacturaId,
                Factura = this.Factura,
                Facturado = this.Facturado,
                UUID = this.UUID,
                FormaPagoId = this.FormaPagoId,
                MetodoPagoId = this.MetodoPagoId,

                RutaFactura = this.RutaFactura,
                PDF = this.PDF != null ? (byte[])this.PDF.Clone() : null,
                XML = this.XML != null ? (byte[])this.XML.Clone() : null,

                VersionCFDI = this.VersionCFDI,
                ProductoId = this.ProductoId,
                ProductoPrecio = this.ProductoPrecio,
                PagoId = this.PagoId,

                //ProductoPedido = this.ProductoPedido != null ? this.ProductoPedido.Copiar() : null
            };
        }
    }
}
