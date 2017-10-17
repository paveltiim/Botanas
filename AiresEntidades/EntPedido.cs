using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresEntidades
{
    public class EntPedido:EntAbstracta
    {
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public decimal TotalSinIVA { get { return Total - IVA; } }
        public decimal Pago { get; set; }
        public decimal TotalSinRetenciones { get { return Total - IVARetencion - ISRRetencion; } }
        public decimal IVA { get; set; }
        public decimal IVARetencion { get; set; }
        public decimal ISRRetencion { get; set; }
        public decimal Debe { get { return Total - Pago; } }
        public decimal Descuento { get; set; }

        public int ClienteId { get; set; }
        public string Cliente { get; set; }
        public string NumCliente { get { return ClienteId.ToString().PadLeft(4, '0'); } }

        public string Detalle { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaPago { get; set; }
        public string FechaCorta { get; set; }
        public string EstatusDescripcion { get; set; }

        public int EmpleadoId { get; set; }
        public string Empleado { get; set; }

        public bool Facturado { get; set; }
        public string UUID { get; set; }
        public string RutaFactura { get; set; }
        public string Factura { get; set; }

        public EntProducto ProductoPedido { get; set; }
    }
}
