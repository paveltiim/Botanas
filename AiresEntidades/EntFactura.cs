using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresEntidades
{
    public class EntFactura:EntAbstracta
    {
        public int PedidoId { get; set; }
        public string SerieFactura { get; set; }
        public string NumeroFactura { get; set; }
        public string UUID { get; set; }
        public string Ruta { get; set; }


        public int TipoComprobanteId { get; set; }
        public int FormaPagoId { get; set; }
        public int MetodoPagoId { get; set; }
        public int UsoCFDIId { get; set; }

    }
}
