using AiresEntidades;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WSConecFM;

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
        string Contraseña = "tiimFac10!";//GENERAL//"Fuckoo04!";//TIIM

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
            if (Cliente.RFC == "XAXX010101000" && Cliente.NombreFiscal == "PUBLICO EN GENERAL")
                Cliente.NombreFiscal = "PUBLICO EN GENERAL.";

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
                                "|" + importe.ToString("0.0000") + "|002|" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") + "|" + ivas.ToString().PadRight(8, '0') + "|";
                    //BOTANAS TODO A TASA 0% IVA
                    //if (Emisor.TipoFactorId > 1 || p.PrecioVentaSinIVA == 0)// CUOTA|EXENTO|
                    textoCSV += "0.0000";
                    //else
                    //    textoCSV += cantidadIva.ToString("0.0000");
                    textoCSV += "|\n";

                    if (cantidadIeps > 0)
                        textoCSV += "CIMT|" + importe.ToString("0.0000") + "|003|Tasa|" + IEPS.ToString().PadRight(8, '0') + "|" + cantidadIeps.ToString("0.0000") + "|\n";
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
                                               decimal CantidadIVA,
                                               string Observaciones)
        {
            mx.pade.timbrado.IntegracionCfdi wPade = new mx.pade.timbrado.IntegracionCfdi();

            //// Aumentamos el timeout de la operación
            wPade.Timeout = 600000;

            IVA = Emisor.TasaOCuota;
            IEPS = 0.08m;

            this.Contrato = Emisor.NumeroReferencia;
            if (Cliente.RFC == "XAXX010101000" && Cliente.NombreFiscal == "PUBLICO EN GENERAL")
                Cliente.NombreFiscal = "PUBLICO EN GENERAL.";

            decimal subtotal = Math.Round(Total, 2) / (1 + IVA); //Math.Round(total / (1 + IVA), 2);
            string usoCFDI = "G02";//UsoCFDI| G02 Devoluciones, descuentos y bonificacione //st.WriteLine("UsoCFDI=" + Emisor.UsoCDFI);

            //Emisor.RFC = "PIRP871204DN5";
            //Emisor.NombreFiscal = "PAVEL URIEL PINEDA RUIZ";
            // Aquí declaramos el CSV que queremos timbrar (sólo se permite un comprobante por transacción)
            string textoCSV = @"CFDI|4.0|" + Serie + "|" + FolioNotaCredito + "|" + FechaNotaCredito.AddHours(-1).ToString("yyyy-MM-ddTHH:mm:ss") + "||" + FormaPago + "|" +
                                "|" +
                                "|" + CondicionPago + "" +
                                "|" + subtotal.ToString("0.00") + "" +
                                "||MXN||" + Total.ToString("0.00") + "|E|01|" + MetodoPago + "|" + Emisor.CP + "||\n" +
                                "EMIS" +
                                "|" + Emisor.RFC + "|" + Emisor.NombreFiscal + "|" + Emisor.RegimenFiscalId + "||\n" +
                                "RECE" +
                                "|" + Cliente.RFC + "|" + Cliente.NombreFiscal + "|" + Cliente.CP + "|||" + Cliente.RegimenFiscalId.ToString() + "|" + usoCFDI + "|\n";//" + UsoCFDI + "

            decimal ivas = IVA;
            decimal restaCantidaSinIEPS = 0;

            decimal cantidadIeps = 0;

            decimal importe = Math.Round((Math.Round(Total, 2) / (1 + IVA)), 4);
            decimal importeSinRedondeo = (Math.Round(Total, 2) / (1 + IVA));

            string claveProdServ = "84111506";
            string claveUnidad = "ACT";
            string unidad = "Actividad";
            //CONC | ClaveProdServ | NoIdentificacion | Cantidad | ClaveUnidad | Unidad | Descripcion | ValorUnitario | Importe | Descuento | ObjetoImp |
            textoCSV += "CONC" +
                            "|" + claveProdServ + "||1|" + claveUnidad + "|" + unidad + "|" + Descripcion + "" +
                            "|" + (importe / 1).ToString("0.0000") + "|" + importe.ToString("0.0000") + "||02|\n";
            if (cantidadIeps > 0)
            {
                textoCSV += "CIMT" +
                            "|" + importe.ToString("0.0000") + "|002|" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") + "|" + ivas.ToString().PadRight(8, '0') + "|";
                //BOTANAS TODO A TASA 0% IVA
                //if (Emisor.TipoFactorId > 1 || p.PrecioVentaSinIVA == 0)// CUOTA|EXENTO|
                textoCSV += "0.0000";
                //else
                //    textoCSV += cantidadIva.ToString("0.0000");
                textoCSV += "|\n";

                //if (cantidadIeps > 0)
                //    textoCSV += "CIMT|" + importe.ToString("0.0000") + "|003|Tasa|" + IEPS.ToString().PadRight(8, '0') + "|" + cantidadIeps.ToString("0.0000") + "|\n";
                //else
                //    restaCantidaSinIEPS += importe;
            }
            else
            {
                //BOTANAS TODO A TASA 0% IVA
                textoCSV += "CIMT" +
                            "|" + importe.ToString("0.0000") + "|002|" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") +
                            "|" + ivas.ToString().PadRight(8, '0') + "|0.0000|\n";
            }

            textoCSV += "IMPU|0.00|" + (CantidadIVA + cantidadIeps).ToString("0.00") + "|\n";
            //BOTANAS NO RETIENE
            //if (ImpuestoRetenido > 0)
            //{
            //    if (IVARetenido > 0)
            //        textoCSV += "IMPR|002|" + IVARetenido.ToString("0.00") + "|\n";
            //    if (ISRRetenido > 0)
            //        textoCSV += "IMPR|001|" + ISRRetenido.ToString("0.00") + "|\n";
            //}

            textoCSV += "IMPT|" + subtotal.ToString("0.00") + "|002|Tasa|" + IVA.ToString().PadRight(8, '0') + "|" + CantidadIVA.ToString("0.00");

            //if (incluyeSinIVA && ListaProductos.Where(P => P.PrecioVentaSinIVA > 0).Count() > 0)
            //{
            //    textoCSV += "|\nIMPT|" + Pedido.SubTotal.ToString("0.00") + "|002|Tasa|" + "0.00".PadRight(8, '0') + "|" + ListaProductos.Where(P => P.PrecioVentaSinIVA > 0).Sum(P => P.PrecioVentaSinIVA).ToString("0.00");
            //}

            //if (cantidadIeps > 0)//SI LLEVA CON IEPS SE PONEN LOS DOS SINO SOLO IVA
            //{
            //    textoCSV += "|\nIMPT|" + (subtotal - restaCantidaSinIEPS).ToString("0.00") + "|003|Tasa|" + IEPS.ToString().PadRight(8, '0') + "|" + cantidadIeps.ToString("0.00");
            //}

            // La opción PLUGIN_PROCESO es Obligatoria, 
            Observaciones += "\n\n Facturación Versión 4.0 - TIIM Tecnología - www.tiimtecnologia.com -";
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
            
        public string FacturarNeue(EntEmpresa Emisor, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente,
                                    string SerieFactura, DateTime FechaFactura, string FormaPago, string MetodoPago, string CondicionPago,
                                    string NumeroCuenta,
                                    decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS, 
                                    decimal ImpuestoRetenido,
                                    string PathGuardaArchivos, string UsoCFDI)
        {
            string respuesta;

            //3.3: LAN7008173R5 YA FUNCIONA MSE061107IA8 CON SU CERTIFICADO
            //3.2: MSE061107IA8

            UsoCFDI = "G01";
            //EN 3.3 EL RFC DEL CLIENTE TDVIA NO ESTA PERMITIDO LIBRE. SERA EL MISMO QUE DEL EMISOR (DE PRUEBA: LAN O MSE)
            RFC = "TCM970625MB1";// "MSE061107IA8";// "LAN7008173R5"; // 
            //NumeroCertificadoSelloDigital = "20001000000300022759";//PRUEBAS LAN7|"20001000000300022815";
            RegimenFiscalId = 601;//pruebas
            IVA = Emisor.TasaOCuota;
            IEPS = 0.08m;

            string FolioFactura = Pedido.Factura;
            string pagare = "Por medio de este PAGARÉ me obligo a pagar  incondicionalmente  a  la  orden  de "+ Emisor.NombreFiscal + ",  la  cantidad  de  "+ Pedido.Total.ToString("$0.00") + " correspondiente al Pedido estipulado en esta FACTURA, en esta ciudad. La cantidad que ampara este PAGARÉ causará intereses al tipo de 10 % mensual en caso de mora. Este PAGARÉ es mercantil no domiciliario y se rige por lo estipulado en la última parte del art. 173 de la ley general de títulos y operaciones de Crédito.  Firma: "+Cliente.NombreFiscal+" ____________________________";

            System.IO.StreamWriter st = new System.IO.StreamWriter(LayoutFile);//@"C:\Users\TIIM\Documents\TIIM\FacturaPruebaCreada3.txt" //20001000000200000278

            st.WriteLine("[ComprobanteFiscalDigital]");
            st.WriteLine("Version=3.3");
            st.WriteLine("Serie=" + SerieFactura);
            st.WriteLine("Folio=" + FolioFactura);
            st.WriteLine("Fecha=" + FechaFactura.ToString("yyyy-MM-ddTHH:mm:ss"));
            st.WriteLine("FormaPago=" + FormaPago);
            //st.WriteLine("NoCertificado=" + NumeroCertificadoSelloDigital);
            st.WriteLine("CondicionesDePago=" + CondicionPago);
            st.WriteLine("SubTotal="  +Pedido.SubTotal.ToString("0.00"));
            st.WriteLine("Descuento=0");
            st.WriteLine("Total=" + Pedido.Total.ToString("0.00"));
            st.WriteLine("TipoDeComprobante=I");
            st.WriteLine("MetodoPago=" + MetodoPago); //st.WriteLine("MetodoPago=" + MedioPago);
            st.WriteLine("LugarExpedicion=" + Emisor.CP);

            st.WriteLine("[DatosAdicionales]");
            st.WriteLine("tipoDocumento=FACTURA");
            //st.WriteLine("observaciones=");
            st.WriteLine("observaciones= Por el presente pagaré el suscrito reconozco deber y me obligo a pagar incondicionalmente a la orden  de " +
                "" + Emisor.NombreFiscal + ", la cantidad de $" + Pedido.Total.ToString("0.00") + ". ************* " + Cliente.Nombre + " ________________________________"); //Facturación Versión 3.3 - TIIM Tecnología | www.tiimtecnologia.com |");            
            //********************************************************************************************************************************
            st.WriteLine("platillaPDF=clasic");//gti_clasica");// clasic");// custom");
            //st.WriteLine("colorLetraE=000000");
            //st.WriteLine("colorPlantillaHex=000000");
            st.WriteLine("logotipo=" + Emisor.Logo);//lg_a0f8b790af59b2e36be4cf1//TIIM 


            st.WriteLine("[Emisor]");
            st.WriteLine("Rfc=" + RFC);//RFC PRUEBAS.
            st.WriteLine("Nombre=" + Emisor.NombreFiscal);
            st.WriteLine("RegimenFiscal=" + RegimenFiscalId);

            st.WriteLine("[Receptor]");
            st.WriteLine("Rfc=" + RFC);//Cliente.RFC//EN 3.3 EL RFC DEL CLIENTE TDVIA NO ESTA PERMITIDO LIBRE. SERA EL MISMO QUE DEL EMISOR (DE PRUEBA: LAN O MSE);
            st.WriteLine("Nombre=" + Cliente.NombreFiscal);
            st.WriteLine("UsoCFDI=" + UsoCFDI); //UsoCFDI| TIIM | I04:Equipo de computo y accesorios    //st.WriteLine("UsoCFDI=" + Emisor.UsoCDFI);

            int cont = 1;
            bool incluyeSinIVA = false;
            decimal ivas = IVA;
            
            //A PRUEBA
            //CantidadIEPS = 0;
            
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

                    cantidadIeps = (Math.Round(p.Precio, 2) * (IEPS * 100)) / porcentaje;
                    importe = Math.Round(p.Precio, 2) - cantidadIeps;
                }
                else
                {
                    ivas = 0.00m;
                    importe = Math.Round(Math.Round(p.Precio, 2), 4);
                    incluyeSinIVA = true;
                }

                st.WriteLine("[Concepto#" + cont + "]");
                st.WriteLine("ClaveProdServ=" + p.ClaveProductoServicio);//TIIM | 81161501 CLAVE PRODUCTO SERVICIO//
                st.WriteLine("NoIdentificacion=" + p.Codigo);//TTIM | MENS01*
                st.WriteLine("Cantidad=" + p.Cantidad);
                st.WriteLine("ClaveUnidad=" + p.ClaveUnidad); //TIIM | E48 Unidad de servicio //st.WriteLine("ClaveUnidad=" + p.ClaveUnidad);
                st.WriteLine("Unidad=");//+ p.Unidad //st.WriteLine("Unidad=" + p.TipoUnidad);
                st.WriteLine("Descripcion=" + p.Descripcion.Replace('|', '-'));

                st.WriteLine("ValorUnitario=" + (importe / p.Cantidad).ToString("0.0000"));
                st.WriteLine("Importe=" + importe.ToString("0.0000"));
                st.WriteLine("Descuento=0.0000");            

                if (cantidadIeps > 0)//SI LLEVA CON IEPS SE PONEN LOS DOS SINO SOLO IVA
                {
                    st.WriteLine("Impuestos.Traslados.Base = [" + importe.ToString("0.0000") + "," 
                                                                + importe.ToString("0.0000") + "]");//p.PrecioSinIVA.ToString("0.00") + "]");
                    st.WriteLine("Impuestos.Traslados.Impuesto =[002,003]");//003 IEPS
                    st.WriteLine("Impuestos.Traslados.TipoFactor =["+Emisor.TipoFactor+",Tasa]");//Tasa|Cuota|Exento
                    st.WriteLine("Impuestos.Traslados.TasaOCuota =[" + ivas.ToString().PadRight(8, '0') + "," 
                                                                     + IEPS.ToString().PadRight(8, '0') + "]");// 0.160000
                    st.WriteLine("Impuestos.Traslados.Importe =[" + ivas.ToString().PadRight(8, '0') + "," 
                                                                  + cantidadIeps.ToString("0.0000") + "]");
                    //A PRUEBA
                    //CantidadIEPS += Math.Round(cantidadIeps,4);
                }
                else
                {
                    st.WriteLine("Impuestos.Traslados.Base = [" + importe.ToString("0.0000") + "]");//p.PrecioSinIVA.ToString("0.00") + "]");
                    st.WriteLine("Impuestos.Traslados.Impuesto =[002]");//002
                    st.WriteLine("Impuestos.Traslados.TipoFactor =[" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") + "]");//Tasa|Cuota|Exento
                    st.WriteLine("Impuestos.Traslados.TasaOCuota =[" + ivas.ToString().PadRight(8, '0') + "]");// 0.160000
                    if (Emisor.TipoFactorId > 1)// CUOTA|EXENTO|
                        st.WriteLine("Impuestos.Traslados.Importe =[0.0000]");
                    else
                        st.WriteLine("Impuestos.Traslados.Importe =[" + cantidadIva.ToString("0.0000") + "]");
                }
                
                if (IVARetenido > 0)
                {
                    decimal importeSinRedondeo = (Math.Round(p.Precio, 2) / (1 + IVA));
                    decimal iva = (Math.Round(p.Precio, 2) - Math.Round(importeSinRedondeo, 4));
                    decimal ivaRetenido = Math.Round(Math.Round(importeSinRedondeo, 2) * 0.06m, 2); //importe * 0.06;

                    st.WriteLine("Impuestos.Retenciones.Base = [" + importe.ToString("0.00") + "]");//p.PrecioSinIVA.ToString("0.00") + "]");
                    st.WriteLine("Impuestos.Retenciones.Impuesto =[002]");//IVA e ISR
                    st.WriteLine("Impuestos.Retenciones.TipoFactor =[Tasa]");//Tasa| NO HAY (Cuota|Exento) PARA IEPS 
                    st.WriteLine("Impuestos.Retenciones.TasaOCuota =[0.060000]");// [0.106667,0.100000]
                    st.WriteLine("Impuestos.Retenciones.Importe =[" + ivaRetenido.ToString("0.00") + "]");//IVARetenido.ToString
                }

                cont++;
            }

            if (incluyeSinIVA && CantidadIEPS > 0 && ListaProductos.Where(P => P.PrecioVentaSinIVA > 0).Count() > 0)
            {
                st.WriteLine("[Traslados]");
                st.WriteLine("TotalImpuestosTrasladados=" + CantidadIEPS.ToString("0.00"));
                st.WriteLine("Impuesto=[002,003]");
                st.WriteLine("TipoFactor=[" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") + ",Tasa]");
                st.WriteLine("TasaOCuota=[" + IVA.ToString().PadRight(8, '0') + "," + IEPS.ToString().PadRight(8, '0') + "]");
                st.WriteLine("Importe=[0.00," + CantidadIEPS.ToString("0.00") + "]");
            }
            else
            {
                //TRASLADOS TOTALES
                if (CantidadIEPS > 0)//SI LLEVA CON IEPS SE PONEN LOS DOS SINO SOLO IVA
                {
                    st.WriteLine("[Traslados]");
                    st.WriteLine("TotalImpuestosTrasladados=" + (CantidadIVA + CantidadIEPS).ToString("0.00"));
                    st.WriteLine("Impuesto=[002,003]");
                    //Tasa| NO HAY (Cuota|Exento) PARA IEPS 
                    st.WriteLine("TipoFactor=[" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") + ",Tasa]");
                    st.WriteLine("TasaOCuota=[" + IVA.ToString().PadRight(8, '0') + "," + IEPS.ToString().PadRight(8, '0') + "]");
                    st.WriteLine("Importe=[" + CantidadIVA.ToString("0.00") + "," + CantidadIEPS.ToString("0.00") + "]");
                }
                else
                {
                    st.WriteLine("[Traslados]");
                    st.WriteLine("TotalImpuestosTrasladados=" + CantidadIVA.ToString("0.00"));
                    st.WriteLine("Impuesto=[002]");
                    st.WriteLine("TipoFactor=[" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") + "]");
                    st.WriteLine("TasaOCuota=[" + ivas.ToString().PadRight(8, '0') + "]");
                    st.WriteLine("Importe=[" + CantidadIVA.ToString("0.00") + "]");
                }
            }

            //}
            if (IVARetenido > 0)
            {
                st.WriteLine("[Retenciones]");
                st.WriteLine("TotalImpuestosRetenidos=" + (IVARetenido).ToString("0.00"));
                st.WriteLine("Impuesto=[002]");//IVA,ISR
                st.WriteLine("Importe=[" + IVARetenido.ToString("0.00") + "]");
            }

            st.Close();

            WSConecFM.Resultados r_wsconect = new Resultados();//activation.Activacion(activarC);

            requestTimbrarCFDI reqt = new requestTimbrarCFDI();
            reqt.UserID = UserID;// "UsuarioPruebasWS";
            reqt.UserPass = UserPass;// "b9ec2afa3361a59af4b4d102d3f704eabdf097d4";
                                     //reqt.emisorRFC = emisorRFC;// "LAN7008173R5";
                                     //reqt.urlTimbrado = "https://t2.facturacionmoderna.com/timbrado/soap";


            reqt.urlTimbrado = "https://t1demo.facturacionmoderna.com/timbrado/soap";
            reqt.emisorRFC = RFC;// "MSE061107IA8";//"LAN7008173R5";
            reqt.generarPDF = true;
            //TUEM470405V4A

            Timbrado timbra = new Timbrado();

            //DESCOMENTAR EN PRODUCCION
            r_wsconect = timbra.Timbrar(LayoutFile, reqt);
            //COMENTAR EN PRODUCCION
            //r_wsconect.status = false;
            if (!r_wsconect.status)
            {
                respuesta = r_wsconect.message; //MessageBox.Show(r_wsconect.message);
                throw new Exception("ERROR EN TIMBRADO-" + respuesta);// Environment.Exit(-1);
            }
            byte[] byteXML = System.Convert.FromBase64String(r_wsconect.xmlBase64);
            System.IO.FileStream swxml = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (r_wsconect.uuid + ".xml"))), System.IO.FileMode.Create);
            swxml.Write(byteXML, 0, byteXML.Length);
            swxml.Close();
            if (reqt.generarCBB)
            {
                byte[] byteCBB = System.Convert.FromBase64String(r_wsconect.cbbBase64);
                System.IO.FileStream swcbb = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (r_wsconect.uuid + ".png"))), System.IO.FileMode.Create);
                swcbb.Write(byteCBB, 0, byteCBB.Length);
                swcbb.Close();
            }
            if (reqt.generarPDF)
            {
                byte[] bytePDF = System.Convert.FromBase64String(r_wsconect.pdfBase64);
                System.IO.FileStream swpdf = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (r_wsconect.uuid + ".pdf"))), System.IO.FileMode.Create);
                swpdf.Write(bytePDF, 0, bytePDF.Length);
                swpdf.Close();
            }
            if (reqt.generarTXT)
            {
                byte[] byteTXT = System.Convert.FromBase64String(r_wsconect.txtBase64);
                System.IO.FileStream swtxt = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (r_wsconect.uuid + ".txt"))), System.IO.FileMode.Create);
                swtxt.Write(byteTXT, 0, byteTXT.Length);
                swtxt.Close();
            }

            respuesta = "Comprobante guardado en " + PathGuardaArchivos + "\\";//MessageBox.Show("Comprobante guardado en " + path + "\\");
            //Cursor.Current = Cursors.Default;
            return r_wsconect.uuid;
        }
        public string FacturarNeueRecalculo(EntEmpresa Emisor, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente,
                            string SerieFactura, DateTime FechaFactura,
                            string FormaPago, string MetodoPago, string CondicionPago,
                            string NumeroCuenta,
                            decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS,
                            decimal ImpuestoRetenido,
                            string PathGuardaArchivos, string UsoCFDI)
        {
            string respuesta;
            
            //3.3: LAN7008173R5 YA FUNCIONA MSE061107IA8 CON SU CERTIFICADO
            //3.2: MSE061107IA8

            UsoCFDI = "G01";
            //EN 3.3 EL RFC DEL CLIENTE TDVIA NO ESTA PERMITIDO LIBRE. SERA EL MISMO QUE DEL EMISOR (DE PRUEBA: LAN O MSE)
            RFC = "TCM970625MB1";// "MSE061107IA8";// "LAN7008173R5"; // 
            //NumeroCertificadoSelloDigital = "20001000000300022759";//PRUEBAS LAN7|"20001000000300022815";
            RegimenFiscalId = 601;//pruebas
            IVA = Emisor.TasaOCuota;
            IEPS = 0.08m;

            string FolioFactura = Pedido.Factura;
            string pagare = "Por medio de este PAGARÉ me obligo a pagar  incondicionalmente  a  la  orden  de "+ Emisor.NombreFiscal + ",  la  cantidad  de  "+ Pedido.Total.ToString("$0.00") + " correspondiente al Pedido estipulado en esta FACTURA, en esta ciudad. La cantidad que ampara este PAGARÉ causará intereses al tipo de 10 % mensual en caso de mora. Este PAGARÉ es mercantil no domiciliario y se rige por lo estipulado en la última parte del art. 173 de la ley general de títulos y operaciones de Crédito.  Firma: "+Cliente.NombreFiscal+" ____________________________";

            System.IO.StreamWriter st = new System.IO.StreamWriter(LayoutFile);//@"C:\Users\TIIM\Documents\TIIM\FacturaPruebaCreada3.txt" //20001000000200000278

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
                    //REVISAR. DEBERIA CALCULAR IVA AL 16%. PRODUCTOS SIN IEPS DEBEN LLEVAR IVA APARENTEMENTE.
                    //ivas = IVA;
                    //porcentaje += (ivas) * 100;
                    //cantidadIva = (Math.Round(p.Precio, 2) * (ivas * 100)) / porcentaje;
                    //importe = Math.Round(p.Precio, 2) - cantidadIva;

                    ivas = 0.00m;
                    importe = Math.Round(Math.Round(p.Precio, 2), 4);
                    incluyeSinIVA = true;
                }
                importeSubtotal += importe;
                iepsTotal += cantidadIeps;
            }
            importeTotal = importeSubtotal + iepsTotal;

            st.WriteLine("[ComprobanteFiscalDigital]");
            st.WriteLine("Version=3.3");
            st.WriteLine("Serie=" + SerieFactura);
            st.WriteLine("Folio=" + FolioFactura);
            st.WriteLine("Fecha=" + FechaFactura.ToString("yyyy-MM-ddTHH:mm:ss"));
            st.WriteLine("FormaPago=" + FormaPago);
            //st.WriteLine("NoCertificado=" + NumeroCertificadoSelloDigital);
            st.WriteLine("CondicionesDePago=" + CondicionPago);
            st.WriteLine("SubTotal=" + importeSubtotal.ToString("0.00"));
            st.WriteLine("Descuento=0");
            st.WriteLine("Moneda=MXN");
            st.WriteLine("Total=" + importeTotal.ToString("0.00"));
            st.WriteLine("TipoDeComprobante=I");
            st.WriteLine("MetodoPago=" + MetodoPago); //st.WriteLine("MetodoPago=" + MedioPago);
            st.WriteLine("LugarExpedicion=" + Emisor.CP);

            st.WriteLine("[DatosAdicionales]");
            st.WriteLine("tipoDocumento=FACTURA");
            //st.WriteLine("observaciones=");
            st.WriteLine("observaciones= Por el presente pagaré el suscrito reconozco deber y me obligo a pagar incondicionalmente a la orden  de " +
                "" + Emisor.NombreFiscal + ", la cantidad de $" + importeTotal.ToString("0.00") + ". ************* " + Cliente.Nombre + " ________________________________"); //Facturación Versión 3.3 - TIIM Tecnología | www.tiimtecnologia.com |");            
            //********************************************************************************************************************************
            st.WriteLine("platillaPDF=clasic");//gti_clasica");// clasic");// custom");
            //st.WriteLine("colorLetraE=000000");
            //st.WriteLine("colorPlantillaHex=000000");
            st.WriteLine("logotipo=" + Emisor.Logo);//lg_a0f8b790af59b2e36be4cf1//TIIM 

            st.WriteLine("[Emisor]");
            st.WriteLine("Rfc=" + RFC);//RFC PRUEBAS.
            st.WriteLine("Nombre=" + Emisor.NombreFiscal);
            st.WriteLine("RegimenFiscal=" + RegimenFiscalId);

            st.WriteLine("[Receptor]");
            st.WriteLine("Rfc=" + RFC);//Cliente.RFC//EN 3.3 EL RFC DEL CLIENTE TDVIA NO ESTA PERMITIDO LIBRE. SERA EL MISMO QUE DEL EMISOR (DE PRUEBA: LAN O MSE);
            st.WriteLine("Nombre=" + Cliente.NombreFiscal);
            st.WriteLine("UsoCFDI=" + UsoCFDI); //UsoCFDI| TIIM | I04:Equipo de computo y accesorios    //st.WriteLine("UsoCFDI=" + Emisor.UsoCDFI);

            int cont = 1;

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

                st.WriteLine("[Concepto#" + cont + "]");
                st.WriteLine("ClaveProdServ=" + p.ClaveProductoServicio);//TIIM | 81161501 CLAVE PRODUCTO SERVICIO//
                st.WriteLine("NoIdentificacion=" + p.Codigo);//TTIM | MENS01*
                st.WriteLine("Cantidad=" + p.Cantidad);
                st.WriteLine("ClaveUnidad=" + p.ClaveUnidad); //TIIM | E48 Unidad de servicio //st.WriteLine("ClaveUnidad=" + p.ClaveUnidad);
                st.WriteLine("Unidad=");//+ p.Unidad //st.WriteLine("Unidad=" + p.TipoUnidad);
                st.WriteLine("Descripcion=" + p.Descripcion.Replace('|', '-'));

                st.WriteLine("ValorUnitario=" + (importe / p.Cantidad).ToString("0.0000"));
                st.WriteLine("Importe=" + importe.ToString("0.0000"));
                st.WriteLine("Descuento=0.0000");

                if (cantidadIeps > 0)//SI LLEVA CON IEPS SE PONEN LOS DOS SINO SOLO IVA
                {
                    st.WriteLine("Impuestos.Traslados.Base = [" + importe.ToString("0.0000") + ","
                                                                + importe.ToString("0.0000") + "]");//p.PrecioSinIVA.ToString("0.00") + "]");
                    st.WriteLine("Impuestos.Traslados.Impuesto =[002,003]");//003 IEPS
                    st.WriteLine("Impuestos.Traslados.TipoFactor =[" + Emisor.TipoFactor + ",Tasa]");//Tasa|Cuota|Exento
                    st.WriteLine("Impuestos.Traslados.TasaOCuota =[" + ivas.ToString().PadRight(8, '0') + ","// 0.160000
                                                                     + IEPS.ToString().PadRight(8, '0') + "]");
                    st.WriteLine("Impuestos.Traslados.Importe =[" + ivas.ToString().PadRight(8, '0') + ","
                                                                  + cantidadIeps.ToString("0.0000") + "]");
                }
                else
                {
                    st.WriteLine("Impuestos.Traslados.Base = [" + importe.ToString("0.0000") + "]");//p.PrecioSinIVA.ToString("0.00") + "]");
                    st.WriteLine("Impuestos.Traslados.Impuesto =[002]");//002
                    st.WriteLine("Impuestos.Traslados.TipoFactor =[" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") + "]");//Tasa|Cuota|Exento
                    st.WriteLine("Impuestos.Traslados.TasaOCuota =[" + ivas.ToString().PadRight(8, '0') + "]");// 0.160000
                    if (Emisor.TipoFactorId > 1)// CUOTA|EXENTO|
                        st.WriteLine("Impuestos.Traslados.Importe =[0.0000]");
                    else
                        st.WriteLine("Impuestos.Traslados.Importe =[" + cantidadIva.ToString("0.0000") + "]");
                }

                if (IVARetenido > 0)
                {
                    decimal importeSinRedondeo = (Math.Round(p.Precio, 2) / (1 + IVA));
                    decimal iva = (Math.Round(p.Precio, 2) - Math.Round(importeSinRedondeo, 4));
                    decimal ivaRetenido = Math.Round(Math.Round(importeSinRedondeo, 2) * 0.06m, 2); //importe * 0.06;

                    st.WriteLine("Impuestos.Retenciones.Base = [" + importe.ToString("0.00") + "]");//p.PrecioSinIVA.ToString("0.00") + "]");
                    st.WriteLine("Impuestos.Retenciones.Impuesto =[002]");//IVA e ISR
                    st.WriteLine("Impuestos.Retenciones.TipoFactor =[Tasa]");//Tasa| NO HAY (Cuota|Exento) PARA IEPS 
                    st.WriteLine("Impuestos.Retenciones.TasaOCuota =[0.060000]");// [0.106667,0.100000]
                    st.WriteLine("Impuestos.Retenciones.Importe =[" + ivaRetenido.ToString("0.00") + "]");//IVARetenido.ToString
                }

                cont++;
            }

            //REVISAR. DEBERIA CALCULAR IVA AL 16%. PRODUCTOS SIN IEPS DEBEN LLEVAR IVA APARENTEMENTE.
            if (incluyeSinIVA && CantidadIEPS > 0 && ListaProductos.Where(P => P.PrecioVentaSinIVA > 0).Count() > 0)
            {
                st.WriteLine("[Traslados]");
                st.WriteLine("TotalImpuestosTrasladados=" + iepsTotal.ToString("0.00"));
                st.WriteLine("Impuesto=[002,003]");
                st.WriteLine("TipoFactor=[" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") + ",Tasa]");
                st.WriteLine("TasaOCuota=[" + IVA.ToString().PadRight(8, '0') + "," + IEPS.ToString().PadRight(8, '0') + "]");
                st.WriteLine("Importe=[0.00," + iepsTotal.ToString("0.00") + "]");
            }
            else
            {
                //TRASLADOS TOTALES
                if (CantidadIEPS > 0)//SI LLEVA CON IEPS SE PONEN LOS DOS SINO SOLO IVA
                {
                    st.WriteLine("[Traslados]");
                    st.WriteLine("TotalImpuestosTrasladados=" + (CantidadIVA + iepsTotal).ToString("0.00"));
                    st.WriteLine("Impuesto=[002,003]");
                    //Tasa| NO HAY (Cuota|Exento) PARA IEPS 
                    st.WriteLine("TipoFactor=[" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") + ",Tasa]");
                    st.WriteLine("TasaOCuota=[" + IVA.ToString().PadRight(8, '0') + "," + IEPS.ToString().PadRight(8, '0') + "]");
                    st.WriteLine("Importe=[" + CantidadIVA.ToString("0.00") + "," + iepsTotal.ToString("0.00") + "]");
                }
                else
                {
                    st.WriteLine("[Traslados]");
                    st.WriteLine("TotalImpuestosTrasladados=" + CantidadIVA.ToString("0.00"));
                    st.WriteLine("Impuesto=[002]");
                    st.WriteLine("TipoFactor=[" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") + "]");
                    st.WriteLine("TasaOCuota=[" + ivas.ToString().PadRight(8, '0') + "]");
                    st.WriteLine("Importe=[" + CantidadIVA.ToString("0.00") + "]");
                }
            }

            //}
            if (IVARetenido > 0)
            {
                st.WriteLine("[Retenciones]");
                st.WriteLine("TotalImpuestosRetenidos=" + (IVARetenido).ToString("0.00"));
                st.WriteLine("Impuesto=[002]");//IVA,ISR
                st.WriteLine("Importe=[" + IVARetenido.ToString("0.00") + "]");
            }

            st.Close();

            WSConecFM.Resultados r_wsconect = new Resultados();//activation.Activacion(activarC);

            requestTimbrarCFDI reqt = new requestTimbrarCFDI();
            reqt.UserID = UserID;// "UsuarioPruebasWS";
            reqt.UserPass = UserPass;// "b9ec2afa3361a59af4b4d102d3f704eabdf097d4";
                                     //reqt.emisorRFC = emisorRFC;// "LAN7008173R5";
                                     //reqt.urlTimbrado = "https://t2.facturacionmoderna.com/timbrado/soap";


            reqt.urlTimbrado = "https://t1demo.facturacionmoderna.com/timbrado/soap";
            reqt.emisorRFC = RFC;// "MSE061107IA8";//"LAN7008173R5";
            reqt.generarPDF = true;
            //TUEM470405V4A

            Timbrado timbra = new Timbrado();
            r_wsconect = timbra.Timbrar(LayoutFile, reqt);

            //r_wsconect.status = false;
            if (!r_wsconect.status)
            {
                respuesta = r_wsconect.message; //MessageBox.Show(r_wsconect.message);
                throw new Exception("ERROR EN TIMBRADO-" + respuesta);// Environment.Exit(-1);
            }
            byte[] byteXML = System.Convert.FromBase64String(r_wsconect.xmlBase64);
            System.IO.FileStream swxml = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (SerieFactura + FolioFactura + ".xml"))), System.IO.FileMode.Create);
            swxml.Write(byteXML, 0, byteXML.Length);
            swxml.Close();
            if (reqt.generarCBB)
            {
                byte[] byteCBB = System.Convert.FromBase64String(r_wsconect.cbbBase64);
                System.IO.FileStream swcbb = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (SerieFactura + FolioFactura + ".png"))), System.IO.FileMode.Create);
                swcbb.Write(byteCBB, 0, byteCBB.Length);
                swcbb.Close();
            }
            if (reqt.generarPDF)
            {
                byte[] bytePDF = System.Convert.FromBase64String(r_wsconect.pdfBase64);
                System.IO.FileStream swpdf = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (SerieFactura + FolioFactura + ".pdf"))), System.IO.FileMode.Create);
                swpdf.Write(bytePDF, 0, bytePDF.Length);
                swpdf.Close();
            }
            if (reqt.generarTXT)
            {
                byte[] byteTXT = System.Convert.FromBase64String(r_wsconect.txtBase64);
                System.IO.FileStream swtxt = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (SerieFactura + FolioFactura + ".txt"))), System.IO.FileMode.Create);
                swtxt.Write(byteTXT, 0, byteTXT.Length);
                swtxt.Close();
            }

            respuesta = "Comprobante guardado en " + PathGuardaArchivos + "\\";//MessageBox.Show("Comprobante guardado en " + path + "\\");
            //Cursor.Current = Cursors.Default;
            return r_wsconect.uuid;
        }
        
        public string Facturar33conRelacion(EntEmpresa Emisor, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente,
                            string FolioFactura, DateTime FechaFactura,
                            string TipoComprobante, string UsoCFDI,
                            string FormaPago, string MetodoPago, string CondicionPago,
                            string NumeroCuenta, string PathGuardaArchivos,
                            decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS,
                            string Observaciones,
                            string TipoRelacion, string UUIDRelacionado)
        {
            string respuesta;


            //EN FACTURACION 3.3 LAN SI FUNCIONA EN 3.2 NO. SOLO MSE.
            //3.3: LAN7008173R5 YA FUNCIONA MSE061107IA8 CON SU CERTIFICADO
            //3.2: MSE061107IA8

            //EN 3.3 EL RFC DEL CLIENTE TDVIA NO ESTA PERMITIDO LIBRE. SERA EL MISMO QUE DEL EMISOR (DE PRUEBA: LAN O MSE)

            UsoCFDI = "G01";

            //RFC = "MSE061107IA8";// "LAN7008173R5"; // 
            //CERTIFICADO  MSE061107IA8: 20001000000300022759
            RFC = "LAN7008173R5";
            //int RegimenFiscalId = 601; //Pruebas Ley General|601; TIIM RIF|621; Profesional|612
            RegimenFiscalId = 601;//pruebas

            NumeroCertificadoSelloDigital = "20001000000300022815";//PRUEBAS LAN7|"20001000000300022815";

            IVA = Emisor.TasaOCuota;
            IEPS = 0.08m;
            string pagare = "Por medio de este PAGARÉ me obligo a pagar  incondicionalmente  a  la  orden  de " + Emisor.NombreFiscal + ",  la  cantidad  de  " + Pedido.Total.ToString("$0.00") + " correspondiente al Pedido estipulado en esta FACTURA, en esta ciudad. La cantidad que ampara este PAGARÉ causará intereses al tipo de 10 % mensual en caso de mora. Este PAGARÉ es mercantil no domiciliario y se rige por lo estipulado en la última parte del art. 173 de la ley general de títulos y operaciones de Crédito.  Firma: " + Cliente.NombreFiscal + " ____________________________";

            System.IO.StreamWriter st = new System.IO.StreamWriter(LayoutFile);//@"C:\Users\TIIM\Documents\TIIM\FacturaPruebaCreada3.txt" //20001000000200000278

            st.WriteLine("[ComprobanteFiscalDigital]");
            st.WriteLine("Version=3.3");
            st.WriteLine("Serie=AA");
            st.WriteLine("Folio=" + FolioFactura);
            st.WriteLine("Fecha=" + FechaFactura.AddHours(-8).ToString("yyyy-MM-ddTHH:mm:ss"));
            st.WriteLine("FormaPago=" + FormaPago);
            st.WriteLine("NoCertificado=" + NumeroCertificadoSelloDigital);
            st.WriteLine("CondicionesDePago=" + CondicionPago);
            st.WriteLine("SubTotal=" + Pedido.SubTotal.ToString("0.00"));
            st.WriteLine("Descuento=0");
            st.WriteLine("Moneda=MXN");
            st.WriteLine("Total=" + Pedido.Total.ToString("0.00"));
            st.WriteLine("TipoDeComprobante=I");
            st.WriteLine("MetodoPago=" + MetodoPago); //st.WriteLine("MetodoPago=" + MedioPago);
            st.WriteLine("LugarExpedicion=" + Emisor.CP);

            st.WriteLine("[CfdiRelacionados]");
            st.WriteLine("TipoRelacion=" + TipoRelacion);
            st.WriteLine("UUID=[" + UUIDRelacionado + "]");

            st.WriteLine("[DatosAdicionales]");
            st.WriteLine("tipoDocumento=FACTURA");
            //st.WriteLine("observaciones=");
            st.WriteLine("observaciones=|||||||---- ESTO ES UNA FACTURA DE PRUEBA, NO CUENTA COMO COMPROBANTE FISCAL LEGAL ---------------------- " + Observaciones.Replace("\r", " ").Replace("\n", " ").PadRight(110, '-') + "||||||||||||||---  Facturación Versión 3.3 - TIIM Tecnología | www.tiimtecnologia.com |----------------------------------------- "); //Facturación Versión 3.3 - TIIM Tecnología | www.tiimtecnologia.com |");
            //********************************************************************************************************************************
            st.WriteLine("platillaPDF=clasic");//gti_clasica");// clasic");// custom");
            //st.WriteLine("colorLetraE=000000");
            //st.WriteLine("colorPlantillaHex=000000");
            st.WriteLine("logotipo=lg_27cb5900e159233c421067e");//lg_a0f8b790af59b2e36be4cf1//TIIM 


            st.WriteLine("[Emisor]");
            st.WriteLine("Rfc=" + RFC);//RFC PRUEBAS.
            st.WriteLine("Nombre=" + Emisor.NombreFiscal);
            st.WriteLine("RegimenFiscal=" + RegimenFiscalId);

            st.WriteLine("[Receptor]");
            st.WriteLine("Rfc=" + RFC);//Cliente.RFC//EN 3.3 EL RFC DEL CLIENTE TDVIA NO ESTA PERMITIDO LIBRE. SERA EL MISMO QUE DEL EMISOR (DE PRUEBA: LAN O MSE);
            st.WriteLine("Nombre=" + Cliente.NombreFiscal);
            st.WriteLine("UsoCFDI=" + UsoCFDI); //UsoCFDI| TIIM | I04:Equipo de computo y accesorios    //st.WriteLine("UsoCFDI=" + Emisor.UsoCDFI);


            int cont = 1;
            foreach (EntProducto p in ListaProductos)
            {
                st.WriteLine("[Concepto#" + cont + "]");
                st.WriteLine("ClaveProdServ=" + p.ClaveProductoServicio);//TIIM | 81161501 CLAVE PRODUCTO SERVICIO//st.WriteLine("ClaveProdServ=81161501" + p.ClaveProdServ);
                st.WriteLine("NoIdentificacion=" + p.Id.ToString().Replace('|', '-').Replace("\r", "").Replace("\n", ""));//TTIM | MENS01*
                st.WriteLine("Cantidad=" + p.Cantidad);
                st.WriteLine("ClaveUnidad=" + p.ClaveUnidad); //TIIM | E48 Unidad de servicio //st.WriteLine("ClaveUnidad=" + p.ClaveUnidad);
                if (p.Unidad.Length > 20)
                    st.WriteLine("Unidad=" + p.Unidad.Remove(20));//+ p.Unidad //st.WriteLine("Unidad=" + p.TipoUnidad);
                else
                    st.WriteLine("Unidad=" + p.Unidad);//+ p.Unidad //st.WriteLine("Unidad=" + p.TipoUnidad);

                st.WriteLine("Descripcion=" + p.Descripcion.Replace('|', '-').Replace("\r", "").Replace("\n", ""));

                decimal importe = Math.Round((Math.Round(p.Precio, 2) / (1 + IVA)), 4);

                decimal importeSinRedondeo = (Math.Round(p.Precio, 2) / (1 + IVA));
                st.WriteLine("ValorUnitario=" + (importe / p.Cantidad).ToString("0.0000"));//p.PrecioVentaSinIVA.ToString("0.00"));
                st.WriteLine("Importe=" + importe.ToString("0.0000"));//p.PrecioSinIVA.ToString("0.00"));
                st.WriteLine("Descuento=0.00");

                st.WriteLine("Impuestos.Traslados.Base = [" + importe.ToString("0.0000") + "]");//p.PrecioSinIVA.ToString("0.00") + "]");
                st.WriteLine("Impuestos.Traslados.Impuesto =[002]");//002
                st.WriteLine("Impuestos.Traslados.TipoFactor =[" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") + "]");//Tasa|Cuota|Exento
                st.WriteLine("Impuestos.Traslados.TasaOCuota =[" + IVA.ToString().PadRight(8, '0') + "]");// 0.160000
                if (Emisor.TipoFactorId > 1)// CUOTA|EXENTO|
                    st.WriteLine("Impuestos.Traslados.Importe =[0.0000]");
                else
                    st.WriteLine("Impuestos.Traslados.Importe =[" + (Math.Round(p.Precio, 2) - Math.Round(importeSinRedondeo, 4)).ToString("0.0000") + "]");//p.PrecioSinIVA).ToString("0.00") + "]");
                                                                                                                                                            //st.WriteLine("Impuestos.Traslados.Importe =[" + (p.Precio - Math.Round(importeSinRedondeo, 4)).ToString("0.0000") + "]");//p.PrecioSinIVA).ToString("0.00") + "]");
                                                                                                                                                            //st.WriteLine("Impuestos.Traslados.Importe =[" + (p.Precio - p.PrecioSinIVA).ToString("0.00") + "]");

                if (IVARetenido > 0)//Retención IVA 6%
                {
                    //string IVARetenidoS = IVARetenido.ToString().Remove(IVARetenido.ToString().IndexOf('.') + 3);
                    //IVARetenido = Convert.ToDecimal(IVARetenidoS);
                    decimal ivaRetenidoProd = importe * 0.06m;
                    st.WriteLine("Impuestos.Retenciones.Base = [" + importe.ToString("0.00") + "]");//p.PrecioSinIVA.ToString("0.00") + "]");
                    st.WriteLine("Impuestos.Retenciones.Impuesto =[002]");//IVA 
                    st.WriteLine("Impuestos.Retenciones.TipoFactor =[Tasa]");//Tasa| NO HAY (Cuota|Exento) PARA IEPS 
                    st.WriteLine("Impuestos.Retenciones.TasaOCuota =[0.060000]");// [0.106667,0.100000]
                    st.WriteLine("Impuestos.Retenciones.Importe =[" + ivaRetenidoProd + "]");//IVARetenido.ToString("0.000000") + "," + ISRRetenido.ToString("0.000000") + "]");
                }
                cont++;
            }

            //TRASLADOS TOTALES
            if (CantidadIEPS > 0)//SI LLEVA CON IEPS SE PONEN LOS DOS SINO SOLO IVA
            {
                st.WriteLine("[Traslados]");
                st.WriteLine("TotalImpuestosTrasladados=" + (CantidadIVA + CantidadIEPS).ToString("0.00"));
                st.WriteLine("Impuesto=[002,003]");
                st.WriteLine("TipoFactor=[" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") + ",Tasa]");//Tasa| NO HAY (Cuota|Exento) PARA IEPS 
                st.WriteLine("TasaOCuota=[" + IVA.ToString().PadRight(8, '0') + "," + IEPS.ToString().PadRight(8, '0') + "]");
                st.WriteLine("Importe=[" + CantidadIVA.ToString("0.00") + "," + CantidadIEPS.ToString("0.00") + "]");
            }
            else if (Emisor.TipoFactorId != 3)
            {
                st.WriteLine("[Traslados]");
                st.WriteLine("TotalImpuestosTrasladados=" + CantidadIVA.ToString("0.00"));
                st.WriteLine("Impuesto=[002]");
                st.WriteLine("TipoFactor=[" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") + "]");
                st.WriteLine("TasaOCuota=[" + IVA.ToString().PadRight(8, '0') + "]");
                st.WriteLine("Importe=[" + CantidadIVA.ToString("0.00") + "]");
            }

            if (IVARetenido > 0)
            {
                //st.WriteLine("Impuestos.Retenciones.Base = [" + Pedido.SubTotal.ToString("0.00") + "]");//p.PrecioSinIVA.ToString("0.00") + "]");
                //st.WriteLine("Impuestos.Retenciones.Impuesto =[002]");//IVA 
                //st.WriteLine("Impuestos.Retenciones.TipoFactor =[Tasa]");//Tasa| NO HAY (Cuota|Exento) PARA IEPS 
                //st.WriteLine("Impuestos.Retenciones.TasaOCuota =[0.060000]");// [0.106667,0.100000]
                //st.WriteLine("Impuestos.Retenciones.Importe =[" + IVARetenido +"]");//IVARetenido.ToString("0.000000") + "," + ISRRetenido.ToString("0.000000") + "]");
                //string ivaretenidoS = IVARetenido.ToString().Remove(IVARetenido.ToString().IndexOf('.') + 3);
                //ivaretenidoS = IVARetenido.ToString("0.00");
                st.WriteLine("[Retenciones]");
                st.WriteLine("TotalImpuestosRetenidos=" + IVARetenido.ToString("0.00"));
                st.WriteLine("Impuesto=[002]");//ISR
                st.WriteLine("Importe=[" + IVARetenido.ToString("0.00") + "]");
            }
            st.Close();

            WSConecFM.Resultados r_wsconect = new Resultados();//activation.Activacion(activarC);

            requestTimbrarCFDI reqt = new requestTimbrarCFDI();
            reqt.UserID = UserID;// "UsuarioPruebasWS";
            reqt.UserPass = UserPass;// "b9ec2afa3361a59af4b4d102d3f704eabdf097d4";
                                     //reqt.emisorRFC = emisorRFC;// "LAN7008173R5";
                                     //reqt.urlTimbrado = "https://t2.facturacionmoderna.com/timbrado/soap";


            reqt.urlTimbrado = "https://t1demo.facturacionmoderna.com/timbrado/soap";
            reqt.emisorRFC = RFC;// "MSE061107IA8";//"LAN7008173R5";
            ////
            reqt.generarPDF = true;
            //TUEM470405V4A

            Timbrado timbra = new Timbrado();

            //DESCOMENTAR EN PRODUCCION
            r_wsconect = timbra.Timbrar(LayoutFile, reqt);
            //COMENTAR EN PRODUCCION
            //r_wsconect.status = false;
            if (!r_wsconect.status)
            {
                respuesta = r_wsconect.message; //MessageBox.Show(r_wsconect.message);
                throw new Exception("ERROR EN TIMBRADO-" + respuesta);// Environment.Exit(-1);
            }
            byte[] byteXML = System.Convert.FromBase64String(r_wsconect.xmlBase64);
            System.IO.FileStream swxml = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (r_wsconect.uuid + ".xml"))), System.IO.FileMode.Create);
            swxml.Write(byteXML, 0, byteXML.Length);
            swxml.Close();
            if (reqt.generarCBB)
            {
                byte[] byteCBB = System.Convert.FromBase64String(r_wsconect.cbbBase64);
                System.IO.FileStream swcbb = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (r_wsconect.uuid + ".png"))), System.IO.FileMode.Create);
                swcbb.Write(byteCBB, 0, byteCBB.Length);
                swcbb.Close();
            }
            if (reqt.generarPDF)
            {
                byte[] bytePDF = System.Convert.FromBase64String(r_wsconect.pdfBase64);
                System.IO.FileStream swpdf = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (r_wsconect.uuid + ".pdf"))), System.IO.FileMode.Create);
                swpdf.Write(bytePDF, 0, bytePDF.Length);
                swpdf.Close();
            }
            if (reqt.generarTXT)
            {
                byte[] byteTXT = System.Convert.FromBase64String(r_wsconect.txtBase64);
                System.IO.FileStream swtxt = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (r_wsconect.uuid + ".txt"))), System.IO.FileMode.Create);
                swtxt.Write(byteTXT, 0, byteTXT.Length);
                swtxt.Close();
            }

            respuesta = "Comprobante guardado en " + PathGuardaArchivos + "\\";//MessageBox.Show("Comprobante guardado en " + path + "\\");
            //Cursor.Current = Cursors.Default;
            return r_wsconect.uuid;
        }

        public string FacturarComplementoPago(EntEmpresa Emisor, EntCliente Cliente, string FolioComplemento, DateTime FechaComplemento,
                                                DateTime FechaPago, string FormaPago, decimal Monto, string Moneda, decimal TipoCambio,
                                                List<EntFactura> ListaFacturasRelacionadas, string FacturasRelacionadas,
                                                string PathGuardaArchivos)
        {
            string respuesta;

            string layoutFile = @"C:\TIIM\Facturacion\FacturaComplePagoBase.txt";

            CargaDatosFacturacionEmpresa();
            CargaDatosFacturacionCliente(Cliente);
            
            RFC = "LAN7008173R5";
            //NumeroCertificadoSelloDigital = "20001000000200000240";
            NumeroCertificadoSelloDigital = "20001000000300022815";//PRUEBAS LAN7|"20001000000300022815";

            IVA = 0.16m;

            System.IO.StreamWriter st = new System.IO.StreamWriter(layoutFile);//@"C:\Users\TIIM\Documents\TIIM\FacturaPruebaCreada3.txt" //20001000000200000278
            
            st.WriteLine("[ReciboPagos]");
            st.WriteLine("Serie=CP");
            st.WriteLine("Folio=" + FolioComplemento);
            st.WriteLine("Fecha=" + FechaComplemento.ToString("yyyy-MM-ddTHH:mm:ss"));
            st.WriteLine("NoCertificado=" + NumeroCertificadoSelloDigital);
            st.WriteLine("LugarExpedicion=81200");

            st.WriteLine("[DatosAdicionales]");
            st.WriteLine("tipoDocumento=RECIBO DE PAGO");
            
            st.WriteLine("observaciones = =||||||||| ESTO ES UNA FACTURA DE PRUEBA, NO CUENTA COMO COMPROBANTE FISCAL LEGAL ||||||||| " +
                        "Complemento de Pago relacionado a la(s) Factura(s): " + FacturasRelacionadas
                                    + " Monto: " + Monto.ToString("$0.00")
                                    + "| Facturación Versión 3.3 - TIIM Tecnología | www.tiimtecnologia.com |".PadLeft(200, '|').PadRight(150, '|'));//Facturación Versión 3.3 - TIIM

            st.WriteLine("platillaPDF=custom");//gti_clasica");// clasic");// custom");
            st.WriteLine("logotipo=lg_de198d9f5c5960f7ac72e6f");//lg_de198d9f5c5960f7ac72e6f-SERDAN //lg_a0f8b790af59b2e36be4cf1//TIIM 
            
            st.WriteLine("[Emisor]");
            st.WriteLine("Rfc=" + RFC);//RFC PRUEBAS.
            st.WriteLine("Nombre=" + Emisor.Nombre);
            st.WriteLine("RegimenFiscal=" + RegimenFiscalId);

            st.WriteLine("[Receptor]");
            st.WriteLine("Rfc=" + RFC);//Cliente.RFC;
            st.WriteLine("Nombre=" + Cliente.NombreFiscal);
            //st.WriteLine("UsoCFDI=" + UsoCFDI); //UsoCFDI| TIIM | I04:Equipo de computo y accesorios    //st.WriteLine("UsoCFDI=" + Emisor.UsoCDFI);

            st.WriteLine("[ComplementoPagos]");
            st.WriteLine("Version=1.0");//;

            int cont = 1;
            foreach (EntFactura f in ListaFacturasRelacionadas)
            {
                st.WriteLine("[Pago#" + cont.ToString() + "]");
                cont++;
                st.WriteLine("FechaPago=" + FechaPago.ToString("yyyy-MM-ddTHH:mm:ss"));
                st.WriteLine("FormaDePagoP=" + FormaPago);


                st.WriteLine("MonedaP=" + Moneda);
                if (Moneda != "MXN" && Moneda != "XXX")
                {
                    st.WriteLine("TipoCambioP=" + TipoCambio.ToString("0.0000"));
                }
                else
                    st.WriteLine("TipoCambioP=");
                st.WriteLine("Monto=" + f.Pago.ToString("0.00")); // + Monto.ToString("0.00"));

                st.WriteLine("NumOperacion=");
                st.WriteLine("RfcEmisorCtaOrd=");
                st.WriteLine("NomBancoOrdExt=");
                st.WriteLine("CtaOrdenante=");
                st.WriteLine("RfcEmisorCtaBen=");
                st.WriteLine("CtaBeneficiario=");
                st.WriteLine("TipoCadPago=");
                st.WriteLine("CertPago=");
                st.WriteLine("CadPago=");
                st.WriteLine("SelloPago=");

                st.WriteLine("DoctoRelacionado.IdDocumento =[" + f.UUID + "]");
                st.WriteLine("DoctoRelacionado.Serie =[" + f.NumeroFactura.Remove(2) + "]");
                st.WriteLine("DoctoRelacionado.Folio =[" + f.NumeroFactura.Remove(0, 2) + "]");
                st.WriteLine("DoctoRelacionado.MonedaDR =[MXN]");
                st.WriteLine("DoctoRelacionado.TipoCambioDR =[]");
                st.WriteLine("DoctoRelacionado.MetodoDePagoDR =[PPD]");
                st.WriteLine("DoctoRelacionado.NumParcialidad =[" + f.Parcialidad + "]");
                st.WriteLine("DoctoRelacionado.ImpSaldoAnt =[" + f.Saldo.ToString("0.00") + "]");
                st.WriteLine("DoctoRelacionado.ImpPagado =[" + f.Pago.ToString("0.00") + "]");
                st.WriteLine("DoctoRelacionado.ImpSaldoInsoluto =[" + (f.Saldo - f.Pago).ToString("0.00") + "]");
            }

            st.Close();

            WSConecFM.Resultados r_wsconect = new Resultados();//activation.Activacion(activarC);

            requestTimbrarCFDI reqt = new requestTimbrarCFDI();
            reqt.UserID = UserID;// "UsuarioPruebasWS";
            reqt.UserPass = UserPass;// "b9ec2afa3361a59af4b4d102d3f704eabdf097d4";
            reqt.urlTimbrado = "https://t1demo.facturacionmoderna.com/timbrado/soap";
            reqt.emisorRFC = RFC;// "LAN7008173R5";
            reqt.generarPDF = true;
            //TUEM470405V4A

            Timbrado timbra = new Timbrado();

            r_wsconect = timbra.Timbrar(layoutFile, reqt);
            //COMENTAR EN PRODUCCION
            //r_wsconect.status = false;
            if (!r_wsconect.status)
            {
                respuesta = r_wsconect.message; //MessageBox.Show(r_wsconect.message);
                throw new Exception("ERROR EN TIMBRADO-" + respuesta);// Environment.Exit(-1);
            }
            byte[] byteXML = System.Convert.FromBase64String(r_wsconect.xmlBase64);
            System.IO.FileStream swxml = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (r_wsconect.uuid + ".xml"))), System.IO.FileMode.Create);
            swxml.Write(byteXML, 0, byteXML.Length);
            swxml.Close();
            if (reqt.generarCBB)
            {
                byte[] byteCBB = System.Convert.FromBase64String(r_wsconect.cbbBase64);
                System.IO.FileStream swcbb = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (r_wsconect.uuid + ".png"))), System.IO.FileMode.Create);
                swcbb.Write(byteCBB, 0, byteCBB.Length);
                swcbb.Close();
            }
            if (reqt.generarPDF)
            {
                byte[] bytePDF = System.Convert.FromBase64String(r_wsconect.pdfBase64);
                System.IO.FileStream swpdf = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (r_wsconect.uuid + ".pdf"))), System.IO.FileMode.Create);
                swpdf.Write(bytePDF, 0, bytePDF.Length);
                swpdf.Close();
            }
            if (reqt.generarTXT)
            {
                byte[] byteTXT = System.Convert.FromBase64String(r_wsconect.txtBase64);
                System.IO.FileStream swtxt = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (r_wsconect.uuid + ".txt"))), System.IO.FileMode.Create);
                swtxt.Write(byteTXT, 0, byteTXT.Length);
                swtxt.Close();
            }

            respuesta = "Comprobante guardado en " + PathGuardaArchivos + "\\";//MessageBox.Show("Comprobante guardado en " + path + "\\");
            //Cursor.Current = Cursors.Default;
            return r_wsconect.uuid;
        }

        public string FacturarNotaCredito33(EntEmpresa Emisor, decimal Total, string Descripcion, EntCliente Cliente,
                               string UUIDFactura,
                               string Serie, string FolioNotaCredito, DateTime FechaNotaCredito,
                               string FormaPago, string MetodoPago, string CondicionPago, string NumeroCuenta,
                               string PathGuardaArchivos,
                               decimal CantidadIVA,
                               string Observaciones)
        {
            string respuesta;

            string UsoCFDI = "G01";

            //RFC = "MSE061107IA8";// "LAN7008173R5"; // 
            //CERTIFICADO  MSE061107IA8: 20001000000300022759
            RFC = "LAN7008173R5";
            //int RegimenFiscalId = 601; //Pruebas Ley General|601; TIIM RIF|621; Profesional|612
            RegimenFiscalId = 601;//pruebas

            NumeroCertificadoSelloDigital = "20001000000300022815";//PRUEBAS LAN7|"20001000000300022815";

            IVA = Emisor.TasaOCuota;
            IEPS = 0.08m;

            //System.IO.StreamWriter st = new System.IO.StreamWriter(PathCreaArchivoBaseTxt);//@"C:\Users\TIIM\Documents\TIIM\FacturaPruebaCreada3.txt" //20001000000200000278
            System.IO.StreamWriter st = new System.IO.StreamWriter(LayoutFileNC);//@"C:\Users\TIIM\Documents\TIIM\FacturaPruebaCreada3.txt" //20001000000200000278

            //decimal subtotal = Total / (1 + IVA);
            decimal subtotal = Math.Round(Total, 2) / (1 + IVA); //Math.Round(total / (1 + IVA), 2);
            //cantidadIva = Math.Round(total, 2) - subtotal;


            //decimal cantidadIva = subtotal * IVA;

            st.WriteLine("[ComprobanteFiscalDigital]");
            st.WriteLine("Version=3.3");
            st.WriteLine("Serie=" + Serie);
            st.WriteLine("Folio=" + FolioNotaCredito);
            st.WriteLine("Fecha=" + FechaNotaCredito.ToString("yyyy-MM-ddTHH:mm:ss"));
            st.WriteLine("FormaPago=" + FormaPago);
            st.WriteLine("NoCertificado=" + NumeroCertificadoSelloDigital);
            st.WriteLine("CondicionesDePago=" + CondicionPago);
            st.WriteLine("SubTotal=" + subtotal.ToString("0.00"));
            st.WriteLine("Descuento=0");
            st.WriteLine("Moneda=MXN");
            st.WriteLine("Total=" + Total.ToString("0.00"));
            st.WriteLine("TipoDeComprobante=E");
            st.WriteLine("MetodoPago=" + MetodoPago); //st.WriteLine("MetodoPago=" + MedioPago);
            st.WriteLine("LugarExpedicion=" + Emisor.CP);

            st.WriteLine("[CfdiRelacionados]");
            st.WriteLine("TipoRelacion=01");
            st.WriteLine("UUID=[" + UUIDFactura + "]");

            //; Atributo requerido para indicar la clave de la relación que existe entre éste CFDI y los CFDI previos.(Conforme al catálogo c_TipoRelacion)

            st.WriteLine("[DatosAdicionales]");
            st.WriteLine("tipoDocumento=NOTA DE CRÉDITO");
            st.WriteLine("observaciones=" + Observaciones.Replace("\r", " ").Replace("\n", " ") + " Facturación Versión 3.3 - TIIM Tecnología | www.tiimtecnologia.com |"); //Facturación Versión 3.3 - TIIM Tecnología | www.tiimtecnologia.com |");
            st.WriteLine("platillaPDF=custom");//gti_clasica");// clasic");// custom");
            st.WriteLine("colorLetraE=000000");
            st.WriteLine("colorPlantillaHex=000000");
            st.WriteLine("logotipo=lg_de198d9f5c5960f7ac72e6f"); //lg_de198d9f5c5960f7ac72e6f - Serdan //lg_a0f8b790af59b2e36be4cf1//TIIM 


            st.WriteLine("[Emisor]");
            st.WriteLine("Rfc=" + RFC);
            st.WriteLine("Nombre=" + Emisor.NombreFiscal);
            st.WriteLine("RegimenFiscal=" + RegimenFiscalId);

            st.WriteLine("[Receptor]");
            st.WriteLine("Rfc=" + RFC);
            st.WriteLine("Nombre=" + Cliente.NombreFiscal);
            st.WriteLine("UsoCFDI=G02"); //UsoCFDI| G02 Devoluciones, descuentos y bonificacione //st.WriteLine("UsoCFDI=" + Emisor.UsoCDFI);


            int cont = 1;
            //foreach (EntProducto p in ListaProductos)
            //{
            st.WriteLine("[Concepto#" + cont + "]");
            st.WriteLine("ClaveProdServ=84111506");//TIIM | 81161501 CLAVE PRODUCTO SERVICIO//st.WriteLine("ClaveProdServ=81161501" + p.ClaveProdServ);
            st.WriteLine("NoIdentificacion=");//TTIM | MENS01*
            st.WriteLine("Cantidad=1");
            st.WriteLine("ClaveUnidad=ACT"); //TIIM | E48 Unidad de servicio //st.WriteLine("ClaveUnidad=" + p.ClaveUnidad);
                                             //if (p.Unidad.Length > 20)
            st.WriteLine("Unidad=Actividad");//+ p.Unidad //st.WriteLine("Unidad=" + p.TipoUnidad);
                                             //else
                                             //    st.WriteLine("Unidad=" + p.Unidad);//+ p.Unidad //st.WriteLine("Unidad=" + p.TipoUnidad);

            st.WriteLine("Descripcion=" + Descripcion);

            decimal importe = Math.Round((Math.Round(Total, 2) / (1 + IVA)), 4);

            decimal importeSinRedondeo = (Math.Round(Total, 2) / (1 + IVA));
            st.WriteLine("ValorUnitario=" + (importe / 1).ToString("0.0000"));//p.PrecioVentaSinIVA.ToString("0.00"));
            st.WriteLine("Importe=" + importe.ToString("0.0000"));//p.PrecioSinIVA.ToString("0.00"));
            st.WriteLine("Descuento=0.00");

            st.WriteLine("Impuestos.Traslados.Base = [" + importe.ToString("0.0000") + "]");//p.PrecioSinIVA.ToString("0.00") + "]");
            st.WriteLine("Impuestos.Traslados.Impuesto =[002]");//002
            st.WriteLine("Impuestos.Traslados.TipoFactor =[" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") + "]");//Tasa|Cuota|Exento
            st.WriteLine("Impuestos.Traslados.TasaOCuota =[" + IVA.ToString().PadRight(8, '0') + "]");// 0.160000
            if (Emisor.TipoFactorId > 1)// CUOTA|EXENTO|
                st.WriteLine("Impuestos.Traslados.Importe =[0.0000]");
            else
                st.WriteLine("Impuestos.Traslados.Importe =[" + (Math.Round(Total, 2) - Math.Round(importeSinRedondeo, 4)).ToString("0.0000") + "]");//p.PrecioSinIVA).ToString("0.00") + "]");
                                                                                                                                                     //st.WriteLine("Impuestos.Traslados.Importe =[" + (p.Precio - Math.Round(importeSinRedondeo, 4)).ToString("0.0000") + "]");//p.PrecioSinIVA).ToString("0.00") + "]");
                                                                                                                                                     //st.WriteLine("Impuestos.Traslados.Importe =[" + (p.Precio - p.PrecioSinIVA).ToString("0.00") + "]");

            cont++;
            //}

            //TRASLADOS TOTALES
            //if (CantidadIEPS > 0)//SI LLEVA CON IEPS SE PONEN LOS DOS SINO SOLO IVA
            //{
            //    st.WriteLine("[Traslados]");
            //    st.WriteLine("TotalImpuestosTrasladados=" + (CantidadIVA + CantidadIEPS).ToString("0.00"));
            //    st.WriteLine("Impuesto=[002,003]");
            //    st.WriteLine("TipoFactor=[" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") + ",Tasa]");//Tasa| NO HAY (Cuota|Exento) PARA IEPS 
            //    st.WriteLine("TasaOCuota=[" + IVA.ToString().PadRight(8, '0') + "," + IEPS.ToString().PadRight(8, '0') + "]");
            //    st.WriteLine("Importe=[" + CantidadIVA.ToString("0.00") + "," + CantidadIEPS.ToString("0.00") + "]");
            //}
            //else 
            if (Emisor.TipoFactorId != 3)
            {
                st.WriteLine("[Traslados]");
                st.WriteLine("TotalImpuestosTrasladados=" + CantidadIVA.ToString("0.00"));
                st.WriteLine("Impuesto=[002]");
                st.WriteLine("TipoFactor=[" + Emisor.TipoFactor.Replace("\r", "").Replace("\n", "") + "]");
                st.WriteLine("TasaOCuota=[" + IVA.ToString().PadRight(8, '0') + "]");
                st.WriteLine("Importe=[" + CantidadIVA.ToString("0.00") + "]");
            }


            st.Close();

            WSConecFM.Resultados r_wsconect = new Resultados();//activation.Activacion(activarC);

            requestTimbrarCFDI reqt = new requestTimbrarCFDI();
            reqt.UserID = UserID;// "UsuarioPruebasWS";
            reqt.UserPass = UserPass;// "b9ec2afa3361a59af4b4d102d3f704eabdf097d4";
                                        //reqt.urlTimbrado = "https://t2.facturacionmoderna.com/timbrado/soap";
            reqt.urlTimbrado = "https://t1demo.facturacionmoderna.com/timbrado/soap";
            reqt.emisorRFC = RFC;// "LAN7008173R5";

            reqt.generarPDF = true;

            Timbrado timbra = new Timbrado();
            r_wsconect = timbra.Timbrar(LayoutFileNC, reqt);

            //r_wsconect.status = false;
            if (!r_wsconect.status)
            {
                respuesta = r_wsconect.message; //MessageBox.Show(r_wsconect.message);
                throw new Exception("ERROR EN TIMBRADO-" + respuesta);// Environment.Exit(-1);
            }
            byte[] byteXML = System.Convert.FromBase64String(r_wsconect.xmlBase64);
            System.IO.FileStream swxml = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (r_wsconect.uuid + ".xml"))), System.IO.FileMode.Create);
            swxml.Write(byteXML, 0, byteXML.Length);
            swxml.Close();
            if (reqt.generarCBB)
            {
                byte[] byteCBB = System.Convert.FromBase64String(r_wsconect.cbbBase64);
                System.IO.FileStream swcbb = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (r_wsconect.uuid + ".png"))), System.IO.FileMode.Create);
                swcbb.Write(byteCBB, 0, byteCBB.Length);
                swcbb.Close();
            }
            if (reqt.generarPDF)
            {
                byte[] bytePDF = System.Convert.FromBase64String(r_wsconect.pdfBase64);
                System.IO.FileStream swpdf = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (r_wsconect.uuid + ".pdf"))), System.IO.FileMode.Create);
                swpdf.Write(bytePDF, 0, bytePDF.Length);
                swpdf.Close();
            }
            if (reqt.generarTXT)
            {
                byte[] byteTXT = System.Convert.FromBase64String(r_wsconect.txtBase64);
                System.IO.FileStream swtxt = new System.IO.FileStream((PathGuardaArchivos + ("\\" + (r_wsconect.uuid + ".txt"))), System.IO.FileMode.Create);
                swtxt.Write(byteTXT, 0, byteTXT.Length);
                swtxt.Close();
            }

            respuesta = "Comprobante guardado en " + PathGuardaArchivos + "\\";//MessageBox.Show("Comprobante guardado en " + path + "\\");
            //Cursor.Current = Cursors.Default;
            return r_wsconect.uuid;
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
