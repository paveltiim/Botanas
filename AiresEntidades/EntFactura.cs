using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresEntidades
{
    public class EntFactura:EntAbstracta
    {
        public int SiguienteFacturaId { get; set; }
        public int PedidoId { get; set; }
        public int PagoId { get; set; }
        public string Nombre { get; set; }
        public string RFC { get; set; }
        public string SerieFactura { get; set; }
        public string NumeroFactura { get; set; }
        public string NumeroComplemento { get; set; }
        public string UUID { get; set; }
        public string Ruta { get; set; }
        public byte[] PDF { get; set; }
        public byte[] XML { get; set; }

        public decimal TipoCambio{ get; set; }
        public int MonedaId { get; set; }
        public decimal Saldo { get; set; }
        public decimal Pago { get; set; }
        public decimal PagoFactura { get; set; }
        public decimal Descuento { get; set; }
        public decimal SubTotal { get; set; }
        public decimal IEPS { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
        public decimal TotalUSD { get; set; }
        public decimal Retenciones { get; set; }
        public int TipoComprobanteId { get; set; }
        public int MetodoPagoId { get; set; }
        public string MetodoPago { get; set; }
        public int FormaPagoId { get; set; }
        public string FormaPago { get; set; }
        public int UsoCFDIId { get; set; }
        public int Parcialidad { get; set; }
        public string VersionCFDI { get; set; }

        public List<EntProducto> Productos { get; set; }
    }
}
