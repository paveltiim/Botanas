using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresEntidades
{
    public class EntPago : EntAbstracta
    {
        public int PedidoId { get; set; }
        public int GastoId { get; set; }
        public int TipoPagoId { get; set; }
        public decimal Cantidad { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal TipoCambio { get; set; }
        public int MonedaId { get; set; }
        public int FormaPagoId { get; set; }
        public string FormaPago { get; set; }
        public string Factura { get; set; }
        public int FacturaId { get; set; }
    }
}
