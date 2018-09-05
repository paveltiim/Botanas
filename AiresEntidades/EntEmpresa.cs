using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresEntidades
{
    public class EntEmpresa : EntAbstracta
    {
        public string Nombre { get; set; }
        public string NombreFiscal { get; set; }
        public string RegimenFiscal { get; set; }
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
        public string NumeroFactura { get; set; }

        public decimal Deuda { get; set; }
        public decimal Pago { get; set; }
        public decimal NotasCredito { get; set; }
        public decimal Saldo { get { return Deuda - Pago - NotasCredito; } }

        public string NombreLegal { get; set; }
        public string Celular { get; set; }
        public string RFC { get; set; }

        public string Calle { get; set; }
        public string NoExterior { get; set; }
        public string NoInterior { get; set; }
        public string Colonia { get; set; }
        public string Localidad { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string CP { get; set; }
        public int TipoPersonaId { get; set; }
        public string Certificado { get; set; }
        public string Key { get; set; }
        public string Clave { get; set; }
        public int TipoTasaIVAId { get; set; }
        public string NoCertificado { get; set; }
        public bool Facturacion { get; set; }

        public int RegimenFiscalId { get; set; }
        public int TipoFactorId { get; set; }
        public string TipoFactor { get; set; }
        public decimal TasaOCuota { get; set; }
        public int UsoCFDIId { get; set; }

        public int Timbres { get; set; }
        public int TimbresUsados { get; set; }
        public int TimbresRestantes { get; set; }

        public string SerieFactura
        {
            get
            {
                string[] nombreSplit = Nombre.Trim().Split(' ');
                try
                {
                    if (nombreSplit.Length == 0)
                        return "AA";
                    else if (nombreSplit.Length > 1)
                        return (nombreSplit[0][0].ToString() + nombreSplit[nombreSplit.Length - 1][0].ToString()).ToUpper();
                    else
                        return (nombreSplit[0][0].ToString() + nombreSplit[1][0].ToString()).ToUpper();
                }catch(Exception ex) { return ""; }
            }
        }


    }
}
