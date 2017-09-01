using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresEntidades
{
    public class EntGasto : EntAbstracta
    {
        public int EmpresaId { get; set; }
        public int ObraId { get; set; }
        public int TrabajadorId { get; set; }
        public string Trabajador { get; set; }
        public int TipoGastoId { get; set; }
        public int TipoPagoId { get; set; }
        public string Empresa { get; set; }
        public string Obra { get; set; }
        public string TipoPago { get; set; }
        public string NumeroFactura { get; set; }
        public string NumeroCheque { get; set; }
        public string Observaciones { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Pago { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime FechaFactura { get; set; }
        public DateTime FechaRegistro { get; set; }

    }
}
