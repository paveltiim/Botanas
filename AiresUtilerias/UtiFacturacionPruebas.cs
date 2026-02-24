using AiresEntidades;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AiresUtilerias
{
    public class UtiFacturacionPruebas
    {
        //COMENTAR EN PRODUCCION
        string UserID = "UsuarioPruebasWS";
        string UserPass = "b9ec2afa3361a59af4b4d102d3f704eabdf097d4";
        //string emisorRFC = "LAN7008173R5";
        //DESCOMENTAR EN PRODUCCION
        //string UserID = "AAPE880825DD2";
        //string UserPass = "38c5256f5533ba27d26b80539cc18d7922df4627";

        /// <summary>
        /// PADE
        /// </summary>
        string Contrato = "ca2be778-3ba5-48f3-8fae-00b831faebea";//TIIM
        string Usuario = "pavel_tiim@hotmail.com";
        string Contraseña = "tiimFac10!";//GENERAL//"Fuckoo06!";//TIIM

        public EntEmpresa Emisor { get; set; }

        //string EmisorRFC;// = "CATS8706298F6";

        //string Keyfile;// = @"C:\TIIM\Facturacion\Certificados Distribuidora LM\EMPRESA.key";
        //string Certfile;// = @"C:\TIIM\Facturacion\Certificados Distribuidora LM\EMPRESA.cer";
        //string Password;// = new System.IO.FileInfo(@"C:\TIIM\Facturacion\Certificados Distribuidora LM\Clave.txt").OpenText().ReadLine();
        ////string password = "12345678a";

        string LayoutFile = @"C:\TIIM\Facturacion\FacturaBase.txt";

        string LayoutFileNC = @"C:\TIIM\Facturacion\FacturaBaseNC.txt";

        string PathCreaArchivoBaseTxt { get; set; }
        string NumeroCertificadoSelloDigital;
        string RFC;
        //string NombreLegal;
        //string RegimenFiscal;
        string Calle;
        string NumeroExterior;
        string NumeroInterior;
        string Colonia;
        string Localidad;
        string Municipio;
        string Estado;
        string Pais;
        string CodigoPostal;

        //string CalleSucursal;
        //string NumeroExteriorSucursal;
        //string NumeroInteriorSucursal;
        //string ColoniaSucursal;
        //string LocalidadSucursal;
        //string MunicipioSucursal;
        //string EstadoSucursal;
        //string PaisSucursal;
        //string CodigoPostalSucursal;
        public decimal IVA, IEPS;
        public decimal ISR = 0.10m;


        int RegimenFiscalId;
        int TipoPersonaId;
        int TipoFactorId;
        string TipoFactor;
        decimal TasaOCuota;
        int UsoCFDIId;

        void CargaDatosFacturacionEmpresa()
        {
            //RFC = "CATS8706298F6";
            //NombreLegal = "SERGIO PATRICIO CAZARES TARÍN";
            //RegimenFiscal = "Persona Física con Actividad Empresarial y Profesional";
            ////COMENTAR EN PRODUCCION
            ////NumeroCertificadoSelloDigital = "20001000000300022815";
            ////COMENTAR EN PRODUCCION
            //NumeroCertificadoSelloDigital = "00001000000400591711";//Distribuidora LM

            RegimenFiscalId = 601;//pruebas

            //CalleSucursal = "";
            //NumeroExteriorSucursal = "";
            //NumeroInteriorSucursal = "";
            //ColoniaSucursal = "";
            //LocalidadSucursal = "";
            //MunicipioSucursal = "";
            //EstadoSucursal = "";
            //PaisSucursal = "";
            //CodigoPostalSucursal = "";
        }
        void CargaDatosFacturacionCliente(EntCliente Cliente)
        {
            Calle = Cliente.Calle;
            NumeroExterior = Cliente.NoExterior;
            NumeroInterior = Cliente.NoInterior;
            Colonia = Cliente.Colonia;
            Localidad = Cliente.Localidad;
            Municipio = Cliente.Municipio;
            Estado = Cliente.Estado;
            Pais = "MÉXICO";
            CodigoPostal = Cliente.CP;
        }


        public string Facturar40(EntEmpresa Emisor, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente,
                                    string SerieFactura, DateTime FechaFactura, string FormaPago, string MetodoPago, string CondicionPago,
                                    string NumeroCuenta,
                                    decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS,
                                    decimal ImpuestoRetenido,
                                    string PathGuardaArchivos, string UsoCFDI)
        {
            mx.pade.timbrado.IntegracionCfdi wPade = new mx.pade.timbrado.IntegracionCfdi();

            //// Aumentamos el timeout de la operación
            wPade.Timeout = 600000;

            IVA = Emisor.TasaOCuota;
            IEPS = 0.08m;

            string FolioFactura = Pedido.Factura;
            
            this.Contrato = Emisor.NumeroReferencia;
            if (Cliente.RFC == "XAXX010101000")
            {
                if(Cliente.NombreFiscal == "PUBLICO EN GENERAL")
                    Cliente.NombreFiscal = "PUBLICO EN GENERAL.";
                Cliente.CP = Emisor.CP;
            }

            //Emisor.RFC = "PIRP871204DN5";
            //Emisor.NombreFiscal = "PAVEL URIEL PINEDA RUIZ";
            // Aquí declaramos el CSV que queremos timbrar (sólo se permite un comprobante por transacción)
            string textoCSV = @"CFDI|4.0|" + SerieFactura + "|" + FolioFactura + "|" + FechaFactura.AddHours(-1).ToString("yyyy-MM-ddTHH:mm:ss") + "||" + FormaPago + "|" +
                                "|" +
                                "|" + CondicionPago + "" +
                                "|" + Pedido.SubTotal.ToString("0.00") + "" +
                                "||MXN||" + Pedido.Total.ToString("0.00") + "|I|01|" + MetodoPago + "|" + Emisor.CP + "||\n" +
                                "EMIS" +
                                "|" + Emisor.RFC + "|" + Emisor.NombreFiscal + "|" + Emisor.RegimenFiscalId + "||\n" +
                                "RECE" +
                                "|" + Cliente.RFC + "|" + Cliente.NombreFiscal + "|" + Cliente.CP + "|||" + Cliente.RegimenFiscalId.ToString() + "|" + UsoCFDI + "|\n";//" + UsoCFDI + "

            int cont = 1;
            bool incluyeSinIVA = false;
            decimal ivas = IVA;
            decimal restaCantidaSinIEPS = 0;
            foreach (EntProducto p in ListaProductos)
            {

                decimal porcentaje = 100;
                decimal cantidadIva = 0;
                decimal cantidadIeps = 0;
                decimal importe = 0;
                if (p.IncluyeIeps)
                {
                    //ivas = IVA;
                    //porcentaje += (ivas) * 100;

                    //cantidadIva = (Math.Round(p.Precio, 2) * (ivas * 100)) / porcentaje;
                    //importe = Math.Round(p.Precio, 2) - cantidadIva;

                    porcentaje += (IEPS) * 100;

                    cantidadIeps = Math.Round((Math.Round(p.Precio, 2) * (IEPS * 100)) / porcentaje,4);
                    importe = Math.Round(p.Precio, 2) - cantidadIeps;
                }
                else
                {
                    ivas = 0.00m;
                    importe = Math.Round(Math.Round(p.Precio, 2), 4);
                    incluyeSinIVA = true;
                }
                //CONC | ClaveProdServ | NoIdentificacion | Cantidad | ClaveUnidad | Unidad | Descripcion | ValorUnitario | Importe | Descuento | ObjetoImp |
                textoCSV += "CONC" +
                                "|" + p.ClaveProductoServicio + "|" + p.Codigo + "|" + p.Cantidad + "|" + p.ClaveUnidad + "|" + p.Unidad + "|" + p.Descripcion + "" +
                                "|" + (importe / p.Cantidad).ToString("0.0000") + "|" + importe.ToString("0.0000") + "||02|\n";
                if (CantidadIEPS > 0)
                {
                    textoCSV += "CIMT" +
                                "|" + importe.ToString("0.0000") + "|002|" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") 
                                                                     + "|" + ivas.ToString().PadRight(8, '0') + "|";
                    //BOTANAS TODO A TASA 0% IVA
                    //if (Emisor.TipoFactorId > 1 || p.PrecioVentaSinIVA == 0)// CUOTA|EXENTO|
                    textoCSV += "0.0000";
                    //else
                    //    textoCSV += cantidadIva.ToString("0.0000");
                    textoCSV += "|\n";

                    if (cantidadIeps > 0)
                        textoCSV += "CIMT|" + importe.ToString("0.0000") + "|003|Tasa|" + IEPS.ToString().PadRight(8, '0') 
                                                                                  + "|" + cantidadIeps.ToString("0.0000") + "|\n";
                    else
                        restaCantidaSinIEPS += importe;
                }
                else
                {
                    //BOTANAS TODO A TASA 0% IVA
                    textoCSV += "CIMT" +
                                "|" + importe.ToString("0.0000") + "|002|" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") +
                                "|" + ivas.ToString().PadRight(8, '0') + "|0.0000|\n";
                }
            }

            textoCSV += "IMPU|" + ImpuestoRetenido.ToString("0.00") + "|" + (CantidadIVA + CantidadIEPS).ToString("0.00") + "|\n";
            //BOTANAS NO RETIENE
            //if (ImpuestoRetenido > 0)
            //{
            //    if (IVARetenido > 0)
            //        textoCSV += "IMPR|002|" + IVARetenido.ToString("0.00") + "|\n";
            //    if (ISRRetenido > 0)
            //        textoCSV += "IMPR|001|" + ISRRetenido.ToString("0.00") + "|\n";
            //}

            textoCSV += "IMPT|" + Pedido.SubTotal.ToString("0.00") + "|002|Tasa|" + IVA.ToString().PadRight(8, '0') + "|" + CantidadIVA.ToString("0.00");

            //if (incluyeSinIVA && ListaProductos.Where(P => P.PrecioVentaSinIVA > 0).Count() > 0)
            //{
            //    textoCSV += "|\nIMPT|" + Pedido.SubTotal.ToString("0.00") + "|002|Tasa|" + "0.00".PadRight(8, '0') + "|" + ListaProductos.Where(P => P.PrecioVentaSinIVA > 0).Sum(P => P.PrecioVentaSinIVA).ToString("0.00");
            //}

            if (CantidadIEPS > 0)//SI LLEVA CON IEPS SE PONEN LOS DOS SINO SOLO IVA
            {
                textoCSV += "|\nIMPT|" + (Pedido.SubTotal-restaCantidaSinIEPS).ToString("0.00") + "|003|Tasa|" + IEPS.ToString().PadRight(8, '0') + "|" + CantidadIEPS.ToString("0.00");
            }

            // La opción PLUGIN_PROCESO es Obligatoria, 
            string pagare = "Por medio de este PAGARÉ me obligo a pagar  incondicionalmente  a  la  orden  de " + Emisor.NombreFiscal + ", " +
                "la  cantidad  de  " + Pedido.Total.ToString("$0.00") + " correspondiente al Pedido estipulado en esta FACTURA, en esta ciudad. " +
                "La cantidad que ampara este PAGARÉ causará intereses al tipo de 10 % mensual en caso de mora. Este PAGARÉ es mercantil no domiciliario " +
                "y se rige por lo estipulado en la última parte del art. 173 de la ley general de títulos y operaciones de Crédito." +
                "\n\nFirma: " + Cliente.NombreFiscal + " ____________________________";
            string[] opciones = new string[6] { "PLUGIN_PROCESO:GENERICO_CFDI40:1.0", "CALCULAR_SELLO", "ESTABLECER_NO_CERTIFICADO",
                "GENERAR_PDF","CADENA_ORIGINAL", "OBSERVACIONES:"+System.Convert.ToBase64String(Encoding.UTF8.GetBytes(pagare))};

            this.Contrato = Emisor.NumeroReferencia;
            //this.Contrato = "ca2be778-3ba5-48f3-8fae-00b831faebea";//TIIM
            if (this.Contrato == "ca2be778-3ba5-48f3-8fae-00b831faebea")//TIIM
                this.Contraseña = "Fuckoo06!";

            // La siguiente línea hace la invocación al WebService
            //throw new Exception("EXCEPCION CREADA PROBAR CONEXION");
            string respuesta = wPade.procesarArchivo(this.Contrato, this.Usuario, this.Contraseña, true, textoCSV, opciones);
            string uuid = "";

            try
            {
                uuid = respuesta.Remove(0, respuesta.IndexOf("<UUID>")).Replace("<UUID>", "").Remove(36);

                //mx.pade.timbradoH.herramientas herramientas = new mx.pade.timbradoH.herramientas();
                //herramientas.obtenerXmlPdf(this.Contrato, this.Usuario, this.Contraseña, uuid, "");

                string xmlBase64 = respuesta.Remove(0, respuesta.IndexOf("<xmlBase64>")).Remove(respuesta.Remove(0, respuesta.IndexOf("<xmlBase64>")).IndexOf("</xmlBase64>")).Replace("<xmlBase64>", "");
                byte[] byteXML = System.Convert.FromBase64String(xmlBase64);
                System.IO.FileStream swxml = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (uuid + ".xml"))), System.IO.FileMode.Create);
                swxml.Write(byteXML, 0, byteXML.Length);
                swxml.Close();

                string pdfBase64 = respuesta.Remove(0, respuesta.IndexOf("<pdfBase64>")).Remove(respuesta.Remove(0, respuesta.IndexOf("<pdfBase64>")).IndexOf("</pdfBase64>")).Replace("<pdfBase64>", "");
                byte[] bytePDF = System.Convert.FromBase64String(pdfBase64);
                System.IO.FileStream swpdf = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (uuid + ".pdf"))), System.IO.FileMode.Create);
                swpdf.Write(bytePDF, 0, bytePDF.Length);
                swpdf.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("-ERROR EN TIMBRADO-\n" + respuesta + "\n\n" + textoCSV);
            }

            return uuid;
        }
        public string Facturar40Recalculo(EntEmpresa Emisor, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente,
                                    string SerieFactura, DateTime FechaFactura, string FormaPago, string MetodoPago, string CondicionPago,
                                    string NumeroCuenta,
                                    decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS,
                                    decimal ImpuestoRetenido,
                                    string PathGuardaArchivos, string UsoCFDI)
        {
            mx.pade.timbrado.IntegracionCfdi wPade = new mx.pade.timbrado.IntegracionCfdi();

            //// Aumentamos el timeout de la operación
            wPade.Timeout = 600000;

            IVA = Emisor.TasaOCuota;
            IEPS = 0.08m;

            string FolioFactura = Pedido.Factura;

            this.Contrato = Emisor.NumeroReferencia;
            if (Cliente.RFC == "XAXX010101000" && Cliente.NombreFiscal == "PUBLICO EN GENERAL")
                Cliente.NombreFiscal = "PUBLICO EN GENERAL.";

            //RECALCULO
            bool incluyeSinIVA = false;
            decimal ivas = IVA;

            decimal importeSubtotal = 0;
            decimal importeTotal = 0;
            decimal iepsTotal = 0;
            decimal ivaTotal = 0;
            foreach (EntProducto p in ListaProductos)
            {
                decimal porcentaje = 100;
                decimal cantidadIeps = 0;
                decimal importe = 0;
                if (p.IncluyeIeps)
                {
                    porcentaje += (IEPS) * 100;

                    cantidadIeps = (Math.Round(p.Precio, 2) * (IEPS * 100)) / porcentaje;
                    importe = Math.Round(p.Precio, 2) - cantidadIeps;
                }
                else
                {
                    ivas = 0.00m;
                    importe = Math.Round(Math.Round(p.Precio, 2), 4);
                    incluyeSinIVA = true;
                }
                importeSubtotal += Math.Round(importe,2);
                iepsTotal += Math.Round(cantidadIeps,2);
            }
            importeTotal = importeSubtotal + iepsTotal;

            //Emisor.RFC = "PIRP871204DN5";
            //Emisor.NombreFiscal = "PAVEL URIEL PINEDA RUIZ";
            // Aquí declaramos el CSV que queremos timbrar (sólo se permite un comprobante por transacción)
            string textoCSV = @"CFDI|4.0|" + SerieFactura + "|" + FolioFactura + "|" + FechaFactura.AddHours(-1).ToString("yyyy-MM-ddTHH:mm:ss") + "||" + FormaPago + "|" +
                                "|" +
                                "|" + CondicionPago + "" +
                                "|" + importeSubtotal.ToString("0.00") + "" +
                                "||MXN||" + importeTotal.ToString("0.00") + "|I|01|" + MetodoPago + "|" + Emisor.CP + "||\n" +
                                "EMIS" +
                                "|" + Emisor.RFC + "|" + Emisor.NombreFiscal + "|" + Emisor.RegimenFiscalId + "||\n" +
                                "RECE" +
                                "|" + Cliente.RFC + "|" + Cliente.NombreFiscal + "|" + Cliente.CP + "|||" + Cliente.RegimenFiscalId.ToString() + "|" + UsoCFDI + "|\n";//" + UsoCFDI + "

            int cont = 1;
            decimal restaCantidaSinIEPS = 0;
            foreach (EntProducto p in ListaProductos)
            {

                decimal porcentaje = 100;
                decimal cantidadIva = 0;
                decimal cantidadIeps = 0;
                decimal importe = 0;
                if (p.IncluyeIeps)
                {
                    //REVISAR. DEBERIA CALCULAR IVA AL 16%. PRODUCTOS SIN IEPS DEBEN LLEVAR IVA APARENTEMENTE.
                    //ivas = IVA;
                    //porcentaje += (ivas) * 100;
                    //cantidadIva = (Math.Round(p.Precio, 2) * (ivas * 100)) / porcentaje;
                    //importe = Math.Round(p.Precio, 2) - cantidadIva;

                    porcentaje += (IEPS) * 100;

                    cantidadIeps = (Math.Round(p.Precio, 2) * (IEPS * 100)) / porcentaje;
                    importe = Math.Round(p.Precio, 2) - cantidadIeps;
                }
                else
                {
                    ivas = 0.00m;
                    importe = Math.Round(Math.Round(p.Precio, 2), 4);
                    incluyeSinIVA = true;
                }
                //CONC | ClaveProdServ | NoIdentificacion | Cantidad | ClaveUnidad | Unidad | Descripcion | ValorUnitario | Importe | Descuento | ObjetoImp |
                textoCSV += "CONC" +
                                "|" + p.ClaveProductoServicio + "|" + p.Codigo + "|" + p.Cantidad + "|" + p.ClaveUnidad + "|" + p.Unidad + "|" + p.Descripcion + "" +
                                "|" + (importe / p.Cantidad).ToString("0.00") + "|" + importe.ToString("0.00") + "||02|\n";
                if (CantidadIEPS > 0)
                {
                    textoCSV += "CIMT" +
                                "|" + importe.ToString("0.00") + "|002|" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") + "|" + ivas.ToString().PadRight(8, '0') + "|";
                    //BOTANAS TODO A TASA 0% IVA
                    //if (Emisor.TipoFactorId > 1 || p.PrecioVentaSinIVA == 0)// CUOTA|EXENTO|
                    textoCSV += "0.00";
                    //else
                    //    textoCSV += cantidadIva.ToString("0.0000");
                    textoCSV += "|\n";

                    if (cantidadIeps > 0)
                        textoCSV += "CIMT|" + importe.ToString("0.00") + "|003|Tasa|" + IEPS.ToString().PadRight(8, '0') + "|" + cantidadIeps.ToString("0.00") + "|\n";
                    else
                        restaCantidaSinIEPS += importe;
                }
                else
                {
                    //BOTANAS TODO A TASA 0% IVA
                    textoCSV += "CIMT" +
                                "|" + importe.ToString("0.00") + "|002|" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") +
                                "|" + ivas.ToString().PadRight(8, '0') + "|0.00|\n";
                }
            }

            textoCSV += "IMPU|" + ImpuestoRetenido.ToString("0.00") + "|" + (CantidadIVA + iepsTotal).ToString("0.00") + "|\n";
            //BOTANAS NO RETIENE
            //if (ImpuestoRetenido > 0)
            //{
            //    if (IVARetenido > 0)
            //        textoCSV += "IMPR|002|" + IVARetenido.ToString("0.00") + "|\n";
            //    if (ISRRetenido > 0)
            //        textoCSV += "IMPR|001|" + ISRRetenido.ToString("0.00") + "|\n";
            //}

            textoCSV += "IMPT|" + importeSubtotal.ToString("0.00") + "|002|Tasa|" + IVA.ToString().PadRight(8, '0') + "|" + CantidadIVA.ToString("0.00");

            //if (incluyeSinIVA && ListaProductos.Where(P => P.PrecioVentaSinIVA > 0).Count() > 0)
            //{
            //    textoCSV += "|\nIMPT|" + Pedido.SubTotal.ToString("0.00") + "|002|Tasa|" + "0.00".PadRight(8, '0') + "|" + ListaProductos.Where(P => P.PrecioVentaSinIVA > 0).Sum(P => P.PrecioVentaSinIVA).ToString("0.00");
            //}

            if (CantidadIEPS > 0)//SI LLEVA CON IEPS SE PONEN LOS DOS SINO SOLO IVA
            {
                textoCSV += "|\nIMPT|" + (importeSubtotal-restaCantidaSinIEPS).ToString("0.00") + "|003|Tasa|" + IEPS.ToString().PadRight(8, '0') + "|" + iepsTotal.ToString("0.00");
            }

            // La opción PLUGIN_PROCESO es Obligatoria, 
            string pagare = "Por medio de este PAGARÉ me obligo a pagar  incondicionalmente  a  la  orden  de " + Emisor.NombreFiscal + ", " +
                "la  cantidad  de  " + Pedido.Total.ToString("$0.00") + " correspondiente al Pedido estipulado en esta FACTURA, en esta ciudad. " +
                "La cantidad que ampara este PAGARÉ causará intereses al tipo de 10 % mensual en caso de mora. Este PAGARÉ es mercantil no domiciliario " +
                "y se rige por lo estipulado en la última parte del art. 173 de la ley general de títulos y operaciones de Crédito." +
                "\n\nFirma: " + Cliente.NombreFiscal + " ____________________________";
            string[] opciones = new string[5] { "PLUGIN_PROCESO:GENERICO_CFDI40:1.0", "CALCULAR_SELLO", "ESTABLECER_NO_CERTIFICADO",
                "GENERAR_PDF", "OBSERVACIONES:"+System.Convert.ToBase64String(Encoding.UTF8.GetBytes(pagare))};

            this.Contrato = Emisor.NumeroReferencia;
            //this.Contrato = "ca2be778-3ba5-48f3-8fae-00b831faebea";//TIIM
            if (this.Contrato == "ca2be778-3ba5-48f3-8fae-00b831faebea")//TIIM
                this.Contraseña = "Fuckoo06!";

            // La siguiente línea hace la invocación al WebService
            string respuesta = wPade.procesarArchivo(this.Contrato, this.Usuario, this.Contraseña, true, textoCSV, opciones);
            string uuid = "";

            try
            {
                uuid = respuesta.Remove(0, respuesta.IndexOf("<UUID>")).Replace("<UUID>", "").Remove(36);

                //mx.pade.timbradoH.herramientas herramientas = new mx.pade.timbradoH.herramientas();
                //herramientas.obtenerXmlPdf(this.Contrato, this.Usuario, this.Contraseña, uuid, "");

                string xmlBase64 = respuesta.Remove(0, respuesta.IndexOf("<xmlBase64>")).Remove(respuesta.Remove(0, respuesta.IndexOf("<xmlBase64>")).IndexOf("</xmlBase64>")).Replace("<xmlBase64>", "");
                byte[] byteXML = System.Convert.FromBase64String(xmlBase64);
                System.IO.FileStream swxml = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (uuid + ".xml"))), System.IO.FileMode.Create);
                swxml.Write(byteXML, 0, byteXML.Length);
                swxml.Close();

                string pdfBase64 = respuesta.Remove(0, respuesta.IndexOf("<pdfBase64>")).Remove(respuesta.Remove(0, respuesta.IndexOf("<pdfBase64>")).IndexOf("</pdfBase64>")).Replace("<pdfBase64>", "");
                byte[] bytePDF = System.Convert.FromBase64String(pdfBase64);
                System.IO.FileStream swpdf = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (uuid + ".pdf"))), System.IO.FileMode.Create);
                swpdf.Write(bytePDF, 0, bytePDF.Length);
                swpdf.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("-ERROR EN TIMBRADO-\n" + respuesta + "\n\n" + textoCSV);
            }

            return uuid;
        }
        public string FacturarComplementoPago40PADE(EntEmpresa Emisor, EntCliente Cliente, string FolioComplemento, DateTime FechaComplemento,
                                                DateTime FechaPago, string FormaPago, decimal Monto, string Moneda, decimal TipoCambio,
                                                List<EntFactura> ListaFacturasRelacionadas, string FacturasRelacionadas,
                                                string PathGuardaArchivos)
        {
            mx.pade.timbrado.IntegracionCfdi wPade = new mx.pade.timbrado.IntegracionCfdi();

            //// Aumentamos el timeout de la operación
            wPade.Timeout = 600000;

            IVA = Emisor.TasaOCuota;
            IEPS = 0.08m;

            decimal porcentajeMonto = 0;
            decimal subtotal = 0;

            decimal totalFacturas = ListaFacturasRelacionadas.Sum(P => P.Total);

            porcentajeMonto = Math.Round(Monto / totalFacturas,4);
            decimal cantidadIvaMonto = Math.Round(ListaFacturasRelacionadas.Sum(P => P.IVA)*porcentajeMonto,2);
            decimal cantidadIepsMonto = Math.Round(ListaFacturasRelacionadas.Sum(P => P.IEPS) * porcentajeMonto, 2);

            subtotal = Monto - cantidadIvaMonto - cantidadIepsMonto;

            if (Cliente.RFC == "XAXX010101000" && Cliente.NombreFiscal == "PUBLICO EN GENERAL")
                Cliente.NombreFiscal = "PUBLICO EN GENERAL.";

            this.CodigoPostal = Emisor.CP;

            // Aquí declaramos el CSV que queremos timbrar (sólo se permite un comprobante por transacción)
            string textoCSV = @"CFDI|4.0|CP|" + FolioComplemento + "|" + FechaComplemento.Date.ToString("yyyy-MM-ddTHH:mm:ss") +
                                "||||||0||XXX||0|P|01||" + CodigoPostal + "||\n" +
                                "EMIS" +
                                "|" + Emisor.RFC + "|" + Emisor.NombreFiscal + "|" + Emisor.RegimenFiscalId + "||\n" +
                                "RECE" +
                                "|" + Cliente.RFC + "|" + Cliente.NombreFiscal + "|" + Cliente.CP + "|||" + Cliente.RegimenFiscalId.ToString() + "|CP01|\n";//" + UsoCFDI + "
            textoCSV += "CONC|84111506||1|ACT||Pago|0|0||01|\n";
            textoCSV += "COMP|\n";
            textoCSV += "CRPS20|2.0|" + "\n" +
                                "CTOT20||||" +
                                //TotalRetencionesIVA | TotalRetencionesISR | TotalRetencionesIEPS |
                                //TotalTrasladosBaseIVA16 |TotalTrasladosImpuestoIVA16|
                                //subtotal.ToString("0.00") + "|" + cantidadIvaMonto.ToString("0.00") + "|"+//NO APLICA PARA BOTANAS(TODO A 0%)
                                "||" +
                                //TotalTrasladosBaseIVA8|TotalTrasladosImpuestoIVA8|
                                "||" +
                                //TotalTrasladosBaseIVA0|TotalTrasladosImpuestoIVA0|
                                subtotal.ToString("0.00")+"|0.00|"+
                                //TotalTrasladosBaseIVAExento
                                "|" + Monto.ToString("0.00") + "\n";
                                 //MontoTotalPagos

            int cont = 1;
            decimal cantidadiepsTotal = 0;
            decimal subtotalTotal = 0;
            foreach (EntFactura f in ListaFacturasRelacionadas)
            {
                decimal subtotalF = f.Pago / (1 + IVA);
                decimal cantidadIvaF = subtotalF * IVA;
                decimal cantidadiepsF = 0;

                decimal porcentaje = 0;
                decimal totalCP = f.Pago; //f.Total;
                decimal totalFac = f.Total;
                if (totalFac > 0)
                {
                    porcentaje = Math.Round(totalCP / totalFac, 4);
                    //cp.SubTotal = fac.SubTotal * porcentaje;
                    //cp.IVA = f.IVA * porcentaje;
                    cantidadiepsF = Math.Round(f.IEPS * porcentaje, 2);
                    cantidadiepsTotal += cantidadiepsF;
                    //cp.IVARetenciones = fac.IVARetenciones * porcentaje;
                    //cp.ISRRetenciones = fac.ISRRetenciones * porcentaje;
                    //cp.Retenciones = fac.Retenciones * porcentaje;
                    subtotalF = (subtotalF - cantidadIvaF - cantidadiepsF);
                    subtotalTotal += subtotalF;
                }

                textoCSV += "CPAG20|" + FechaPago.Date.AddHours(-1).ToString("yyyy-MM-ddTHH:mm:ss") + "|" + FormaPago + "|MXN|1|" + f.Pago.ToString("0.00") + "|" +
                            //FechaPago | FormaDePagoP | MonedaP | TipoCambioP | Monto |
                            "|||||||||\n" +
                            //NumOperacion | RfcEmisorCtaOrd | NomBancoOrdExt | CtaOrdenante | RfcEmisorCtaBen | CtaBeneficiario | TipoCadPago | CertPago | CadPago | SelloPago
                            "CPDR20|" + f.UUID + "|" + f.NumeroFactura.Remove(2) + "|" + f.NumeroFactura.Replace("AA", "").Replace("BB", "") +
                            //IdDocumento | Serie | Folio |
                            "|MXN|1|" + f.Parcialidad + "|" + f.Saldo.ToString("0.00") + "|" + f.Pago.ToString("0.00") + "|" + (f.Saldo - f.Pago).ToString("0.00") + "|02|\n" +
                            //MonedaDR | EquivalenciaDR | NumParcialidad | ImpSaldoAnt | ImpPagado | ImpSaldoInsoluto | ObjetoImpDR
                            "CPIDR20|\n";
                textoCSV += "CPTDR20|" + subtotalF.ToString("0.00") + "|002|" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") +
                            //BaseDR | ImpuestoDR | TipoFactorDR | 
                            "|" + IVA.ToString().PadRight(8, '0') + "|" + cantidadIvaF.ToString("0.00") + "|\n";
                //TasaOCuotaDR | ImporteDR
                if (cantidadiepsF > 0)
                {
                    textoCSV += "CPTDR20|" + (cantidadiepsF/this.IEPS).ToString("0.00") + "|003|Tasa" +
                            //BaseDR | ImpuestoDR | TipoFactorDR | 
                            "|" + this.IEPS.ToString().PadRight(8, '0') + "|" + cantidadiepsF.ToString("0.00") + "|\n";
                }

                textoCSV += "CPIM20|\n";
                textoCSV += "CPIT20|" + subtotalF.ToString("0.00") + "|002|" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") +
                //BaseP | ImpuestoP | TipoFactorP | 
                "|" + IVA.ToString().PadRight(8, '0') + "|" + cantidadIvaF.ToString("0.00") + "\n";
                //TasaOCuotaP | ImporteP

                if (cantidadiepsTotal > 0)
                {
                    textoCSV += "CPIT20|" + (cantidadiepsF / this.IEPS).ToString("0.00") + "|003|Tasa" +
                                //BaseP | ImpuestoP | TipoFactorP | 
                                "|" + this.IEPS.ToString().PadRight(8, '0') + "|" + cantidadiepsF.ToString("0.00") + "\n";
                    //TasaOCuotaP | ImporteP
                }

            }            

            string observacion = "Complemento de Pago relacionado a la(s) Factura(s): " + FacturasRelacionadas + "\n " +
                //" > TOTAL: " + TotalFactura.ToString("$0.00") + " | SALDO ANTERIOR: " + SaldoAnterior.ToString("0.00") + "" +
                " > CANTIDAD PAGO: " + Monto.ToString("0.00") + " <\n\n " +
                " | Facturación Versión 4.0 - TIIM Tecnología | www.tiimtecnologia.com |";
            // La opción PLUGIN_PROCESO es Obligatoria, 
            string[] opciones = new string[5] { "PLUGIN_PROCESO:GENERICO_CFDI40:1.0", "CALCULAR_SELLO", "ESTABLECER_NO_CERTIFICADO",
                "GENERAR_PDF", "OBSERVACIONES:"+System.Convert.ToBase64String(Encoding.UTF8.GetBytes(observacion)) };

            this.Contrato = Emisor.NumeroReferencia;
            if (this.Contrato == "ca2be778-3ba5-48f3-8fae-00b831faebea")//TIIM
                this.Contraseña = "Fuckoo06!";

            // La siguiente línea hace la invocación al WebService
            string respuesta = wPade.procesarArchivo(this.Contrato, this.Usuario, this.Contraseña, true, textoCSV, opciones);
            string uuid = "";

            try
            {
                uuid = respuesta.Remove(0, respuesta.IndexOf("<UUID>")).Replace("<UUID>", "").Remove(36);

                //mx.pade.timbradoH.herramientas herramientas = new mx.pade.timbradoH.herramientas();
                //herramientas.obtenerXmlPdf(this.Contrato, this.Usuario, this.Contraseña, uuid, "");

                string xmlBase64 = respuesta.Remove(0, respuesta.IndexOf("<xmlBase64>")).Remove(respuesta.Remove(0, respuesta.IndexOf("<xmlBase64>")).IndexOf("</xmlBase64>")).Replace("<xmlBase64>", "");
                byte[] byteXML = System.Convert.FromBase64String(xmlBase64);
                System.IO.FileStream swxml = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (uuid + ".xml"))), System.IO.FileMode.Create);
                swxml.Write(byteXML, 0, byteXML.Length);
                swxml.Close();

                string pdfBase64 = respuesta.Remove(0, respuesta.IndexOf("<pdfBase64>")).Remove(respuesta.Remove(0, respuesta.IndexOf("<pdfBase64>")).IndexOf("</pdfBase64>")).Replace("<pdfBase64>", "");
                byte[] bytePDF = System.Convert.FromBase64String(pdfBase64);
                System.IO.FileStream swpdf = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (uuid + ".pdf"))), System.IO.FileMode.Create);
                swpdf.Write(bytePDF, 0, bytePDF.Length);
                swpdf.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("-ERROR EN TIMBRADO-\n" + respuesta + "\n\n" + textoCSV);
            }

            return uuid;
        }
        public string FacturarComplementoPago40PADERecalculo(EntEmpresa Emisor, EntCliente Cliente, string FolioComplemento, DateTime FechaComplemento,
                                                DateTime FechaPago, string FormaPago, decimal Monto, string Moneda, decimal TipoCambio,
                                                List<EntFactura> ListaFacturasRelacionadas, string FacturasRelacionadas,
                                                string PathGuardaArchivos)
        {
            mx.pade.timbrado.IntegracionCfdi wPade = new mx.pade.timbrado.IntegracionCfdi();

            //// Aumentamos el timeout de la operación
            wPade.Timeout = 600000;

            IVA = Emisor.TasaOCuota;
            IEPS = 0.08m;

            decimal porcentajeMonto = 0;
            decimal subtotal = 0;

            decimal totalFacturas = ListaFacturasRelacionadas.Sum(P => P.Total);

            porcentajeMonto = Math.Round(Monto / totalFacturas, 4);
            decimal cantidadIvaMonto = Math.Round(ListaFacturasRelacionadas.Sum(P => P.IVA) * porcentajeMonto, 2);
            decimal cantidadIepsMonto = Math.Round(ListaFacturasRelacionadas.Sum(P => P.IEPS) * porcentajeMonto, 2);

            subtotal = Monto - cantidadIvaMonto - cantidadIepsMonto;

            int cont = 1;
            decimal cantidadiepsTotal = 0;
            decimal subtotalTotal = 0;
            foreach (EntFactura f in ListaFacturasRelacionadas)
            {
                decimal subtotalF = 0;// f.Pago / (1 + IVA);
                decimal cantidadIvaF = 0;// subtotalF * IVA;
                decimal cantidadiepsF = 0;

                decimal porcentaje = 0;
                decimal totalCP = f.Pago; //f.Total;
                decimal totalFac = f.Total;
                if (totalFac > 0)
                {
                    porcentaje = Math.Round(totalCP / totalFac, 4);
                    cantidadiepsF = Math.Round(f.IEPS * porcentaje, 2);
                    cantidadiepsTotal += cantidadiepsF;
                    subtotalF = (f.Pago - cantidadIvaF - cantidadiepsF);
                    subtotalTotal += subtotalF;
                }
            }

            if (Cliente.RFC == "XAXX010101000" && Cliente.NombreFiscal == "PUBLICO EN GENERAL")
                Cliente.NombreFiscal = "PUBLICO EN GENERAL.";

            this.CodigoPostal = Emisor.CP;

            // Aquí declaramos el CSV que queremos timbrar (sólo se permite un comprobante por transacción)
            string textoCSV = @"CFDI|4.0|CP|" + FolioComplemento + "|" + FechaComplemento.Date.ToString("yyyy-MM-ddTHH:mm:ss") +
                                "||||||0||XXX||0|P|01||" + CodigoPostal + "||\n" +
                                "EMIS" +
                                "|" + Emisor.RFC + "|" + Emisor.NombreFiscal + "|" + Emisor.RegimenFiscalId + "||\n" +
                                "RECE" +
                                "|" + Cliente.RFC + "|" + Cliente.NombreFiscal + "|" + Cliente.CP + "|||" + Cliente.RegimenFiscalId.ToString() + "|CP01|\n";//" + UsoCFDI + "
            textoCSV += "CONC|84111506||1|ACT||Pago|0|0||01|\n";
            textoCSV += "COMP|\n";
            textoCSV += "CRPS20|2.0|" + "\n" +
                                "CTOT20||||" +
                                //TotalRetencionesIVA | TotalRetencionesISR | TotalRetencionesIEPS |
                                //TotalTrasladosBaseIVA16 |TotalTrasladosImpuestoIVA16|
                                //subtotal.ToString("0.00") + "|" + cantidadIvaMonto.ToString("0.00") + "|"+//NO APLICA PARA BOTANAS(TODO A 0%)
                                "||" +
                                //TotalTrasladosBaseIVA8|TotalTrasladosImpuestoIVA8|
                                "||" +
                                //TotalTrasladosBaseIVA0|TotalTrasladosImpuestoIVA0|
                                subtotalTotal.ToString("0.00") + "|0.00|" +
                                //TotalTrasladosBaseIVAExento
                                "|" + Monto.ToString("0.00") + "\n";
            //MontoTotalPagos

            StringBuilder sbFacturas= new StringBuilder();
            foreach (EntFactura f in ListaFacturasRelacionadas)
            {
                sbFacturas.AppendLine(f.SerieFactura+"-"+f.NumeroFactura + " |  $" + f.Saldo.ToString("0,0.00").PadRight(12) + " |  $" + f.Pago.ToString("0,0.00").PadRight(12) + " |  $" + (f.Saldo - f.Pago).ToString("0,0.00").PadRight(12) + "   | "+ f.UUID + "\n");
                
                decimal subtotalF = f.Pago / (1 + IVA);
                decimal cantidadIvaF = subtotalF * IVA;
                decimal cantidadiepsF = 0;

                decimal porcentaje = 0;
                decimal totalCP = f.Pago; //f.Total;
                decimal totalFac = f.Total;
                if (totalFac > 0)
                {
                    porcentaje = Math.Round(totalCP / totalFac, 4);
                    //cp.SubTotal = fac.SubTotal * porcentaje;
                    //cp.IVA = f.IVA * porcentaje;
                    cantidadiepsF = Math.Round(f.IEPS * porcentaje, 2);
                    cantidadiepsTotal += cantidadiepsF;
                    //cp.IVARetenciones = fac.IVARetenciones * porcentaje;
                    //cp.ISRRetenciones = fac.ISRRetenciones * porcentaje;
                    //cp.Retenciones = fac.Retenciones * porcentaje;
                    subtotalF = (subtotalF - cantidadIvaF - cantidadiepsF);
                    subtotalTotal += subtotalF;
                }

                textoCSV += "CPAG20|" + FechaPago.Date.ToString("yyyy-MM-ddTHH:mm:ss") + "|" + FormaPago + "|MXN|1|" + f.Pago.ToString("0.00") + "|" +
                            //FechaPago | FormaDePagoP | MonedaP | TipoCambioP | Monto |
                            "|||||||||\n" +
                            //NumOperacion | RfcEmisorCtaOrd | NomBancoOrdExt | CtaOrdenante | RfcEmisorCtaBen | CtaBeneficiario | TipoCadPago | CertPago | CadPago | SelloPago
                            "CPDR20|" + f.UUID + "|" + f.NumeroFactura.Remove(2) + "|" + f.NumeroFactura.Replace("AA", "").Replace("BB", "") +
                            //IdDocumento | Serie | Folio |
                            "|MXN|1|" + f.Parcialidad + "|" + f.Saldo.ToString("0.00") + "|" + f.Pago.ToString("0.00") + "|" + (f.Saldo - f.Pago).ToString("0.00") + "|02|\n" +
                            //MonedaDR | EquivalenciaDR | NumParcialidad | ImpSaldoAnt | ImpPagado | ImpSaldoInsoluto | ObjetoImpDR
                            "CPIDR20|\n";
                textoCSV += "CPTDR20|" + subtotalF.ToString("0.00") + "|002|" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") +
                            //BaseDR | ImpuestoDR | TipoFactorDR | 
                            "|" + IVA.ToString().PadRight(8, '0') + "|" + cantidadIvaF.ToString("0.00") + "|\n";
                //TasaOCuotaDR | ImporteDR
                if (cantidadiepsF > 0)
                {
                    textoCSV += "CPTDR20|" + (cantidadiepsF / this.IEPS).ToString("0.00") + "|003|Tasa" +
                            //BaseDR | ImpuestoDR | TipoFactorDR | 
                            "|" + this.IEPS.ToString().PadRight(8, '0') + "|" + cantidadiepsF.ToString("0.00") + "|\n";
                }

                textoCSV += "CPIM20|\n";
                textoCSV += "CPIT20|" + subtotalF.ToString("0.00") + "|002|" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") +
                //BaseP | ImpuestoP | TipoFactorP | 
                "|" + IVA.ToString().PadRight(8, '0') + "|" + cantidadIvaF.ToString("0.00") + "\n";
                //TasaOCuotaP | ImporteP

                if (cantidadiepsTotal > 0)
                {
                    textoCSV += "CPIT20|" + (cantidadiepsF / this.IEPS).ToString("0.00") + "|003|Tasa" +
                                //BaseP | ImpuestoP | TipoFactorP | 
                                "|" + this.IEPS.ToString().PadRight(8, '0') + "|" + cantidadiepsF.ToString("0.00") + "\n";
                    //TasaOCuotaP | ImporteP
                }

            }

            string observacion = " \nComplemento de Pago relacionado a la(s) Factura(s): \n" +
                " " + FacturasRelacionadas + "\n\n " +
                "FACTURA     DEUDA              PAGO      SALDO PENDIENTE     UUID       \n \n" +
                sbFacturas.ToString() +
                " > FECHA PAGO: " + FechaPago.Date.ToString("yyyy-MM-dd") + " < > FORMA PAGO: "+ FormaPago + " < > CANTIDAD FACTURAS: "+ListaFacturasRelacionadas.Count.ToString()+"\n" +
                " > CANTIDAD TOTAL PAGO: $" + Monto.ToString("0,0.00") + " <\n\n " +
                " | Facturación Versión 4.0 - TIIM Tecnología | www.tiimtecnologia.com |";
            // La opción PLUGIN_PROCESO es Obligatoria, 
            string[] opciones = new string[5] { "PLUGIN_PROCESO:GENERICO_CFDI40:1.0", "CALCULAR_SELLO", "ESTABLECER_NO_CERTIFICADO",
                "GENERAR_PDF", "OBSERVACIONES:"+System.Convert.ToBase64String(Encoding.UTF8.GetBytes(observacion)) };

            this.Contrato = Emisor.NumeroReferencia;
            if (this.Contrato == "ca2be778-3ba5-48f3-8fae-00b831faebea")//TIIM
                this.Contraseña = "Fuckoo06!";

            // La siguiente línea hace la invocación al WebService
            string respuesta = wPade.procesarArchivo(this.Contrato, this.Usuario, this.Contraseña, true, textoCSV, opciones);
            string uuid = "";

            try
            {
                uuid = respuesta.Remove(0, respuesta.IndexOf("<UUID>")).Replace("<UUID>", "").Remove(36);

                //mx.pade.timbradoH.herramientas herramientas = new mx.pade.timbradoH.herramientas();
                //herramientas.obtenerXmlPdf(this.Contrato, this.Usuario, this.Contraseña, uuid, "");

                string xmlBase64 = respuesta.Remove(0, respuesta.IndexOf("<xmlBase64>")).Remove(respuesta.Remove(0, respuesta.IndexOf("<xmlBase64>")).IndexOf("</xmlBase64>")).Replace("<xmlBase64>", "");
                byte[] byteXML = System.Convert.FromBase64String(xmlBase64);
                System.IO.FileStream swxml = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (uuid + ".xml"))), System.IO.FileMode.Create);
                swxml.Write(byteXML, 0, byteXML.Length);
                swxml.Close();

                string pdfBase64 = respuesta.Remove(0, respuesta.IndexOf("<pdfBase64>")).Remove(respuesta.Remove(0, respuesta.IndexOf("<pdfBase64>")).IndexOf("</pdfBase64>")).Replace("<pdfBase64>", "");
                byte[] bytePDF = System.Convert.FromBase64String(pdfBase64);
                System.IO.FileStream swpdf = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (uuid + ".pdf"))), System.IO.FileMode.Create);
                swpdf.Write(bytePDF, 0, bytePDF.Length);
                swpdf.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("-ERROR EN TIMBRADO-\n" + respuesta + "\n\n" + textoCSV);
            }

            return uuid;
        }
        public string FacturarComplementoPago33PADE(EntEmpresa Emisor, EntCliente Cliente, string FolioComplemento, DateTime FechaComplemento,
                                                DateTime FechaPago, string FormaPago, decimal Monto, string Moneda, decimal TipoCambio,
                                                List<EntFactura> ListaFacturasRelacionadas, string FacturasRelacionadas,
                                                string PathGuardaArchivos)
        {
            mx.pade.timbrado.IntegracionCfdi wPade = new mx.pade.timbrado.IntegracionCfdi();

            //// Aumentamos el timeout de la operación
            wPade.Timeout = 600000;

            IVA = Emisor.TasaOCuota;
            IEPS = 0.08m;

            decimal porcentajeMonto = 0;
            decimal subtotal = 0;

            decimal totalFacturas = ListaFacturasRelacionadas.Sum(P => P.Total);

            porcentajeMonto = Math.Round(Monto / totalFacturas, 4);
            decimal cantidadIvaMonto = Math.Round(ListaFacturasRelacionadas.Sum(P => P.IVA) * porcentajeMonto, 2);
            decimal cantidadIepsMonto = Math.Round(ListaFacturasRelacionadas.Sum(P => P.IEPS) * porcentajeMonto, 2);

            subtotal = Monto - cantidadIvaMonto - cantidadIepsMonto;

            //if (Cliente.RFC == "XAXX010101000" && Cliente.NombreFiscal == "PUBLICO EN GENERAL")
            //    Cliente.NombreFiscal = "PUBLICO EN GENERAL.";

            this.CodigoPostal = Emisor.CP;

            // Aquí declaramos el CSV que queremos timbrar (sólo se permite un comprobante por transacción)
            string textoCSV = @"CFDI|3.3|CP|" + FolioComplemento + "|" + FechaComplemento.Date.ToString("yyyy-MM-ddTHH:mm:ss") +
                                "||||||0||XXX||0|P|01||" + CodigoPostal + "||\n" +
                                "EMIS" +
                                "|" + Emisor.RFC + "|" + Emisor.NombreFiscal + "|" + Emisor.RegimenFiscalId + "|\n" +
                                "RECE" +
                                "|" + Cliente.RFC + "|" + Cliente.NombreFiscal + "|||P01|\n";//" + UsoCFDI + "
            //textoCSV += "CONC|84111506||1|ACT||Pago|0|0||01|\n";
            //textoCSV += "COMP|\n";
            textoCSV += "CRPS|1.0|" + "\n";

            int cont = 1;
            decimal cantidadiepsTotal = 0;
            decimal subtotalTotal = 0;
            foreach (EntFactura f in ListaFacturasRelacionadas)
            {
                decimal subtotalF = f.Pago / (1 + IVA);
                decimal cantidadIvaF = subtotalF * IVA;
                decimal cantidadiepsF = 0;

                decimal porcentaje = 0;
                decimal totalCP = f.Pago; //f.Total;
                decimal totalFac = f.Total;
                if (totalFac > 0)
                {
                    porcentaje = Math.Round(totalCP / totalFac, 4);
                    //cp.SubTotal = fac.SubTotal * porcentaje;
                    //cp.IVA = f.IVA * porcentaje;
                    cantidadiepsF = Math.Round(f.IEPS * porcentaje, 2);
                    cantidadiepsTotal += cantidadiepsF;
                    //cp.IVARetenciones = fac.IVARetenciones * porcentaje;
                    //cp.ISRRetenciones = fac.ISRRetenciones * porcentaje;
                    //cp.Retenciones = fac.Retenciones * porcentaje;
                    subtotalF = (subtotalF - cantidadIvaF - cantidadiepsF);
                    subtotalTotal += subtotalF;
                }

                textoCSV += "CPAG|" + FechaPago.Date.AddHours(-1).ToString("yyyy-MM-ddTHH:mm:ss") + "|" + FormaPago + "|MXN|1|" + f.Pago.ToString("0.00") + "|" +
                            //FechaPago | FormaDePagoP | MonedaP | TipoCambioP | Monto |
                            "|||||||||\n" +
                            //NumOperacion | RfcEmisorCtaOrd | NomBancoOrdExt | CtaOrdenante | RfcEmisorCtaBen | CtaBeneficiario | TipoCadPago | CertPago | CadPago | SelloPago
                            "CPDR|" + f.UUID + "|" + f.NumeroFactura.Remove(2) + "|" + f.NumeroFactura.Replace("AA", "").Replace("BB", "") +
                            //IdDocumento | Serie | Folio |
                            "|MXN|1|" + f.Parcialidad + "|" + f.Saldo.ToString("0.00") + "|" + f.Pago.ToString("0.00") + "|" + (f.Saldo - f.Pago).ToString("0.00") + "|02|\n";
                            //MonedaDR | EquivalenciaDR | NumParcialidad | ImpSaldoAnt | ImpPagado | ImpSaldoInsoluto | ObjetoImpDR
                //textoCSV += "CPIDR20|\n";
                //textoCSV += "CPTDR20|" + subtotalF.ToString("0.00") + "|002|" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") +
                //            //BaseDR | ImpuestoDR | TipoFactorDR | 
                //            "|" + IVA.ToString().PadRight(8, '0') + "|" + cantidadIvaF.ToString("0.00") + "|\n";
                ////TasaOCuotaDR | ImporteDR
                //if (cantidadiepsF > 0)
                //{
                //    textoCSV += "CPTDR20|" + subtotalF.ToString("0.00") + "|003|Tasa" +
                //            //BaseDR | ImpuestoDR | TipoFactorDR | 
                //            "|" + IEPS.ToString().PadRight(8, '0') + "|" + cantidadiepsF.ToString("0.00") + "|\n";
                //}
            }

            string observacion = "Complemento de Pago relacionado a la(s) Factura(s): " + FacturasRelacionadas + "\n " +
                //" > TOTAL: " + TotalFactura.ToString("$0.00") + " | SALDO ANTERIOR: " + SaldoAnterior.ToString("0.00") + "" +
                " > CANTIDAD PAGO: " + Monto.ToString("0.00") + " <\n\n " +
                " | Facturación Versión 4.0 - TIIM Tecnología | www.tiimtecnologia.com |";
            // La opción PLUGIN_PROCESO es Obligatoria, 
            string[] opciones = new string[5] { "PLUGIN_PROCESO:GENERICO_CFDI33:1.0", "CALCULAR_SELLO", "ESTABLECER_NO_CERTIFICADO",
                "GENERAR_PDF", "OBSERVACIONES:"+System.Convert.ToBase64String(Encoding.UTF8.GetBytes(observacion)) };

            this.Contrato = Emisor.NumeroReferencia;
            if (this.Contrato == "ca2be778-3ba5-48f3-8fae-00b831faebea")//TIIM
                this.Contraseña = "Fuckoo06!";

            // La siguiente línea hace la invocación al WebService
            string respuesta = wPade.procesarArchivo(this.Contrato, this.Usuario, this.Contraseña, true, textoCSV, opciones);
            string uuid = "";

            try
            {
                uuid = respuesta.Remove(0, respuesta.IndexOf("<UUID>")).Replace("<UUID>", "").Remove(36);

                //mx.pade.timbradoH.herramientas herramientas = new mx.pade.timbradoH.herramientas();
                //herramientas.obtenerXmlPdf(this.Contrato, this.Usuario, this.Contraseña, uuid, "");

                string xmlBase64 = respuesta.Remove(0, respuesta.IndexOf("<xmlBase64>")).Remove(respuesta.Remove(0, respuesta.IndexOf("<xmlBase64>")).IndexOf("</xmlBase64>")).Replace("<xmlBase64>", "");
                byte[] byteXML = System.Convert.FromBase64String(xmlBase64);
                System.IO.FileStream swxml = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (uuid + ".xml"))), System.IO.FileMode.Create);
                swxml.Write(byteXML, 0, byteXML.Length);
                swxml.Close();

                string pdfBase64 = respuesta.Remove(0, respuesta.IndexOf("<pdfBase64>")).Remove(respuesta.Remove(0, respuesta.IndexOf("<pdfBase64>")).IndexOf("</pdfBase64>")).Replace("<pdfBase64>", "");
                byte[] bytePDF = System.Convert.FromBase64String(pdfBase64);
                System.IO.FileStream swpdf = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (uuid + ".pdf"))), System.IO.FileMode.Create);
                swpdf.Write(bytePDF, 0, bytePDF.Length);
                swpdf.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("-ERROR EN TIMBRADO-\n" + respuesta + "\n\n" + textoCSV);
            }

            return uuid;
        }
        public string FacturarNotaDeCredito40PADE(EntEmpresa Emisor, decimal Total, string Descripcion, EntCliente Cliente,
                                               string UUIDFactura,
                                               string Serie, string FolioNotaCredito, DateTime FechaNotaCredito,
                                               string FormaPago, string MetodoPago, string CondicionPago, string NumeroCuenta,
                                               string PathGuardaArchivos,
                                               decimal CantidadIVA, decimal CantidadIEPS,
                                               string UsoCFDI, string TipoRelacion,
                                               string Observaciones)
        {
            mx.pade.timbrado.IntegracionCfdi wPade = new mx.pade.timbrado.IntegracionCfdi();

            //// Aumentamos el timeout de la operación
            wPade.Timeout = 600000;

            this.IVA = Emisor.TasaOCuota;
            this.IEPS = 0.08m;

            this.Contrato = Emisor.NumeroReferencia;
            if (Cliente.RFC == "XAXX010101000")
            {
                if (Cliente.NombreFiscal == "PUBLICO EN GENERAL")
                    Cliente.NombreFiscal = "PUBLICO EN GENERAL.";
                Cliente.CP = Emisor.CP;
                UsoCFDI = "S01";
            }

            //decimal subtotal = Math.Round(Total, 2) / (1 + IVA);
            decimal subtotal = Math.Round(Math.Round(Total, 2) / (1 + this.IEPS), 2);

            string usoCFDI = UsoCFDI; //| G02 Devoluciones, descuentos y bonificacione //st.WriteLine("UsoCFDI=" + Emisor.UsoCDFI);

            //Emisor.RFC = "PIRP871204DN5";
            //Emisor.NombreFiscal = "PAVEL URIEL PINEDA RUIZ";
            // Aquí declaramos el CSV que queremos timbrar (sólo se permite un comprobante por transacción)
            string textoCSV = @"CFDI|4.0|" + Serie + "|" + FolioNotaCredito + "|" + FechaNotaCredito.ToString("yyyy-MM-ddTHH:mm:ss") + "||" + FormaPago + "|" +
                                "|" +
                                "|" + CondicionPago + "" +
                                "|" + subtotal.ToString("0.00") + "" +
                                "||MXN||" + Total.ToString("0.00") + "|E|01|" + MetodoPago + "|" + Emisor.CP + "||\n" +
                                "CREL" +
                                "|" + TipoRelacion + "|\n" +
                                "CRUU" +
                                "|" + UUIDFactura + "|\n" +
                                "EMIS" +
                                "|" + Emisor.RFC + "|" + Emisor.NombreFiscal + "|" + Emisor.RegimenFiscalId + "||\n" +
                                "RECE" +
                                "|" + Cliente.RFC + "|" + Cliente.NombreFiscal + "|" + Cliente.CP + "|||" + Cliente.RegimenFiscalId.ToString() + "|" + usoCFDI + "|\n";//" + UsoCFDI + "

            decimal ivas = IVA;
            decimal restaCantidaSinIEPS = 0;

            decimal cantidadIeps = 0;

            //decimal importe = Math.Round((Math.Round(Total, 2) / (1 + IVA)), 4);
            //decimal importeSinRedondeo = (Math.Round(Total, 2) / (1 + IVA));
            decimal importe = Math.Round((Math.Round(Total, 2) / (1 + this.IEPS)), 4);
            decimal importeSinRedondeo = (Math.Round(Total, 2) / (1 + this.IEPS));

            //decimal porcentaje = 100;
            //porcentaje += (this.IEPS) * 100;

            //cantidadIeps = Math.Round((Math.Round(Total, 2) * (this.IEPS * 100)) / porcentaje, 2); 
            //decimal importe = Math.Round(Total, 2) - cantidadIeps;

            cantidadIeps = Math.Round(Total, 2) - Math.Round(importeSinRedondeo, 4);

            string claveProdServ = "84111506";
            string claveUnidad = "ACT";
            string unidad = "Actividad";
            //CONC | ClaveProdServ | NoIdentificacion | Cantidad | ClaveUnidad | Unidad | Descripcion | ValorUnitario | Importe | Descuento | ObjetoImp |
            textoCSV += "CONC" +
                            "|" + claveProdServ + "||1|" + claveUnidad + "|" + unidad + "|" + Descripcion + "" +
                            "|" + (importe / 1).ToString("0.0000") + "|" + importe.ToString("0.0000") + "||02|\n";

            if (CantidadIVA > 0 || cantidadIeps > 0)
            {
                if (CantidadIVA > 0)
                {
                    textoCSV += "CIMT" +
                                "|" + importe.ToString("0.0000") + "|002|" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") +
                                "|" + ivas.ToString().PadRight(8, '0') + "|";
                    //BOTANAS TODO A TASA 0% IVA
                    //if (Emisor.TipoFactorId > 1 || p.PrecioVentaSinIVA == 0)// CUOTA|EXENTO|
                    textoCSV += "0.0000";
                    //else
                    //    textoCSV += cantidadIva.ToString("0.0000");
                    textoCSV += "|\n";
                }
                else
                {
                    //BOTANAS TODO A TASA 0% IVA
                    textoCSV += "CIMT" +
                                "|" + importe.ToString("0.0000") + "|002|" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") +
                                "|" + ivas.ToString().PadRight(8, '0') + "|0.0000|\n";
                }

                if (cantidadIeps > 0)
                {
                    textoCSV += "CIMT" +
                            "|" + importe.ToString("0.0000") + "|003|Tasa" +
                            "|" + this.IEPS.ToString().PadRight(8, '0') + "|";
                    textoCSV += cantidadIeps.ToString("0.0000");
                    textoCSV += "|\n";
                }
                else
                    restaCantidaSinIEPS += importe;
            }
            else
            {
                //BOTANAS TODO A TASA 0% IVA
                textoCSV += "CIMT" +
                            "|" + importe.ToString("0.0000") + "|002|" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") +
                            "|" + ivas.ToString().PadRight(8, '0') + "|0.0000|\n";
            }

            textoCSV += "IMPU||" + (CantidadIVA + cantidadIeps).ToString("0.00") + "|\n";
            //textoCSV += "IMPU|0.00|" + (CantidadIVA + cantidadIeps).ToString("0.00") + "|\n";
            //BOTANAS NO RETIENE
            //if (ImpuestoRetenido > 0)
            //{
            //    if (IVARetenido > 0)
            //        textoCSV += "IMPR|002|" + IVARetenido.ToString("0.00") + "|\n";
            //    if (ISRRetenido > 0)
            //        textoCSV += "IMPR|001|" + ISRRetenido.ToString("0.00") + "|\n";
            //}

            textoCSV += "IMPT|" + subtotal.ToString("0.00") + "|002|Tasa|" + this.IVA.ToString().PadRight(8, '0') + "|" + CantidadIVA.ToString("0.00");

            //if (incluyeSinIVA && ListaProductos.Where(P => P.PrecioVentaSinIVA > 0).Count() > 0)
            //{
            //    textoCSV += "|\nIMPT|" + Pedido.SubTotal.ToString("0.00") + "|002|Tasa|" + "0.00".PadRight(8, '0') + "|" + ListaProductos.Where(P => P.PrecioVentaSinIVA > 0).Sum(P => P.PrecioVentaSinIVA).ToString("0.00");
            //}

            if (cantidadIeps > 0)//SI LLEVA CON IEPS SE PONEN LOS DOS SINO SOLO IVA
            {
                textoCSV += "|\nIMPT|" + (subtotal - restaCantidaSinIEPS).ToString("0.00") + "|003|Tasa|" + this.IEPS.ToString().PadRight(8, '0') + "|" + cantidadIeps.ToString("0.00");
            }

            // La opción PLUGIN_PROCESO es Obligatoria, 
            Observaciones = "DESCUENTO A: " + Observaciones;
            Observaciones += "\n\n\n\n Facturación Versión 4.0 - TIIM Tecnología - www.tiimtecnologia.com -";
            string[] opciones = new string[5] { "PLUGIN_PROCESO:GENERICO_CFDI40:1.0", "CALCULAR_SELLO", "ESTABLECER_NO_CERTIFICADO",
                "GENERAR_PDF", "OBSERVACIONES:"+System.Convert.ToBase64String(Encoding.UTF8.GetBytes(Observaciones))};

            this.Contrato = Emisor.NumeroReferencia;
            //this.Contrato = "ca2be778-3ba5-48f3-8fae-00b831faebea";//TIIM
            if (this.Contrato == "ca2be778-3ba5-48f3-8fae-00b831faebea")//TIIM
                this.Contraseña = "Fuckoo06!";

            // La siguiente línea hace la invocación al WebService
            string respuesta = wPade.procesarArchivo(this.Contrato, this.Usuario, this.Contraseña, true, textoCSV, opciones);
            string uuid = "";

            try
            {
                uuid = respuesta.Remove(0, respuesta.IndexOf("<UUID>")).Replace("<UUID>", "").Remove(36);

                //mx.pade.timbradoH.herramientas herramientas = new mx.pade.timbradoH.herramientas();
                //herramientas.obtenerXmlPdf(this.Contrato, this.Usuario, this.Contraseña, uuid, "");

                string xmlBase64 = respuesta.Remove(0, respuesta.IndexOf("<xmlBase64>")).Remove(respuesta.Remove(0, respuesta.IndexOf("<xmlBase64>")).IndexOf("</xmlBase64>")).Replace("<xmlBase64>", "");
                byte[] byteXML = System.Convert.FromBase64String(xmlBase64);
                System.IO.FileStream swxml = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (uuid + ".xml"))), System.IO.FileMode.Create);
                swxml.Write(byteXML, 0, byteXML.Length);
                swxml.Close();

                string pdfBase64 = respuesta.Remove(0, respuesta.IndexOf("<pdfBase64>")).Remove(respuesta.Remove(0, respuesta.IndexOf("<pdfBase64>")).IndexOf("</pdfBase64>")).Replace("<pdfBase64>", "");
                byte[] bytePDF = System.Convert.FromBase64String(pdfBase64);
                System.IO.FileStream swpdf = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (uuid + ".pdf"))), System.IO.FileMode.Create);
                swpdf.Write(bytePDF, 0, bytePDF.Length);
                swpdf.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("-ERROR EN TIMBRADO-\n" + respuesta + "\n\n" + textoCSV);
            }

            return uuid;
        }

        public string CancelarPADE(EntEmpresa Emisor, string UUID, string Motivo, string UUIDsustitucion, string RfcReceptor, decimal Total)
        {
            string respuesta = "";

            mx.pade.timbrado.IntegracionCfdi wPadeI = new mx.pade.timbrado.IntegracionCfdi();
            //// Aumentamos el timeout de la operación
            wPadeI.Timeout = 600000;

            string arregloS = UUID + "|" + RfcReceptor + "|" + Emisor.RFC + "|" + Total.ToString("0.00") + "|" + Motivo + "|" + UUIDsustitucion;
            string[] arreglo = new string[1] { arregloS };

            byte[] cerBytes = new byte[0] { };// System.Convert.FromBase64String(cerBase64);
            byte[] keyBytes = new byte[0] { };// System.Convert.FromBase64String(keyBase64);
            //string[] opciones = new string[2] { "MODO_PRUEBA:1", "CERT_DEFAULT" };
            string[] opciones = new string[2] { "MODO_PRUEBA:1", "CERT_DEFAULT" };

            //this.Contrato = "7dbf7f63-76fa-4c19-98f3-d1834db86063";//JUAN AYALA
            this.Contrato = Emisor.NumeroReferencia;
            //this.Contrato = "ca2be778-3ba5-48f3-8fae-00b831faebea";//TIIM
            if (this.Contrato == "ca2be778-3ba5-48f3-8fae-00b831faebea")//TIIM
                this.Contraseña = "Fuckoo06!";

            // La siguiente línea hace la invocación al WebService
            //respuesta = wPade.cancelarConOpciones(this.Contrato, this.Usuario, this.Contraseña, emisorRFC, arreglo, cerBytes,keyBytes, cerContraseña, opciones);
            respuesta = wPadeI.cancelarCfdiConOpciones(this.Contrato, this.Usuario, this.Contraseña, Emisor.RFC, arreglo, cerBytes, keyBytes, "", opciones);
            if (respuesta.Contains("false"))
            {
                throw new Exception("-ERROR AL CANCELAR-\n" + respuesta + "\n\n" + arregloS);
            }

            return respuesta;
        }
          
        public string Cancelar(EntEmpresa Emisor, string UUID, string Motivo, string FolioSustitucion)
        {
            string respuesta = "";

            //RFC PRUEBA**
            //RFC = "LAN7008173R5";
            //UUID = "ABC1147C-D41E-4596-9C3E-45629B097CDB";

            //activarCancelacion activarC = new activarCancelacion();
            //activarC.archivoCer = certfile;
            //activarC.archivoKey = keyfile;
            //activarC.clave = password;
            //activarC.UserID = UserID;
            //activarC.UserPass = UserPass;
            //activarC.emisorRFC = emisorRFC;

            ////ActivarCancelado activation = new ActivarCancelado();
            //WSConecFM.Resultados r_wsconect = new Resultados();// activation.Activacion(activarC);

            //string rfc= "LAN7008173R5";
            //rfc = "MSE061107IA8";
            ////Emisor.RFC = "LAN7008173R5";
            //requestCancelarCFDI reqc = new requestCancelarCFDI();
            //reqc.UserID = UserID;// "UsuarioPruebasWS";
            //reqc.UserPass = UserPass;// "b9ec2afa3361a59af4b4d102d3f704eabdf097d4";
            //reqc.emisorRFC = rfc;// Emisor.RFC;
            //reqc.urlCancelado = "https://t1demo.facturacionmoderna.com/timbrado/soap";
            //reqc.uuid = UUID;

            ////CancelarCFDI cancela = new CancelarCFDI();
            //Cancelado cancela = new Cancelado();
            //r_wsconect = cancela.Cancelar(reqc, UUID);

            //if (!r_wsconect.status)
            //{
            //    respuesta = r_wsconect.message; //MessageBox.Show(r_wsconect.message);
            //    throw new Exception("ERROR AL CANCELAR-" + respuesta);
            //    //Environment.Exit(-1);
            //}

            WebClient wb = new WebClient();
            NameValueCollection data = new NameValueCollection();
            string url = Emisor.LinkCancelacionFactura; //"http://44.234.130.5/alka/inicioApp/CancelarFac";
            data["RfcEmisor"] = "ESI920427886";
            data["UUID"] = "ABC1147C-D41E-4596-9C3E-45629B097CDB";
            data["Motivo"] = "02";
            data["UUIDRelacion"] = "";

            var response = wb.UploadValues(url, "POST", data);
            respuesta = Encoding.UTF8.GetString(response);
            if (respuesta.Contains('['))
            {
                throw new Exception("ERROR AL CANCELAR - " + respuesta);
            }

            return respuesta;
        }
        
    }
}
