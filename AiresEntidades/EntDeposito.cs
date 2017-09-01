using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresEntidades
{
    public  class EntDeposito:EntAbstracta
    {
        public decimal Total { get; set; }
        public int PagoId { get; set; }
        public decimal Pago { get; set; }
        public DateTime FechaPago { get; set; }
        public string Factura { get; set; }
        public DateTime FechaFactura { get; set; }
    }
}
