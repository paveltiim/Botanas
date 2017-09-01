using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresEntidades
{
    public class EntCliente : EntAbstracta
    {
        public int EmpresaId { get; set; }
        public string Nombre { get; set; }
        public string NombreLegal { get; set; }
        public string Celular { get; set; }
        public string RFC { get; set; }

        public string NombreFiscal { get; set; }
        public string Direccion { get; set; }
        public string Calle { get; set; }
        public string NoExterior { get; set; }
        public string NoInterior { get; set; }
        public string Colonia { get; set; }
        public string Localidad { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string CP { get; set; }
        public string Telefono { get; set; }
        public string Telefono2 { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Contacto { get; set; }
        public string TelefonoContacto { get; set; }
        public string Banco { get; set; }
        public string NumeroCuenta { get; set; }
        public string Sucursal { get; set; }
        public string CLABE { get; set; }
        public string NumeroReferencia { get; set; }

        public int TipoPersonaId { get; set; }

        public int FormaPagoId { get; set; }

        public bool IncluyeKit { get; set; }

        public string NumCliente { get { return Id.ToString().PadLeft(4, '0'); } }

        public decimal Total { get; set; }
        public decimal Pago { get; set; }
        public decimal Debe { get; set; }
    }
}
