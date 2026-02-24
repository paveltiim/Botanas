using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresEntidades
{
    public class EntProveedor:EntAbstracta
    {
        public string Nombre { get; set; }
        public string NombreFiscal { get; set; }
        public string RFC { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Telefono2 { get; set; }
        public string Email { get; set; }
        public string Contacto { get; set; }
        public string TelefonoContacto { get; set; }
        public string Banco { get; set; }
        public string NumeroCuenta { get; set; }
        public string Sucursal { get; set; }
        public string CLABE { get; set; }
        public string NumeroReferencia { get; set; }

        public int GastoId { get; set; }
        public DateTime FechaFactura { get; set; }
        public string UUIDFactura { get; set; }
        public string NumeroFactura { get; set; }
        public int FormaPagoId { get; set; }

        public decimal Deuda { get; set; }
        public decimal Pago { get; set; }
        public decimal NotasCredito { get; set; }
        public decimal Saldo { get { return Deuda - Pago - NotasCredito; } }
    }
}
