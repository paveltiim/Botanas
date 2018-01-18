using AiresEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //void CargaDatosFacturacionEmpresa()
        //{
        //    //RFC = "CATS8706298F6";
        //    //NombreLegal = "SERGIO PATRICIO CAZARES TARÍN";
        //    //RegimenFiscal = "Persona Física con Actividad Empresarial y Profesional";
        //    ////COMENTAR EN PRODUCCION
        //    ////NumeroCertificadoSelloDigital = "20001000000300022815";
        //    ////COMENTAR EN PRODUCCION
        //    //NumeroCertificadoSelloDigital = "00001000000400591711";//Distribuidora LM

        //    CalleSucursal = "";
        //    NumeroExteriorSucursal = "";
        //    NumeroInteriorSucursal = "";
        //    ColoniaSucursal = "";
        //    LocalidadSucursal = "";
        //    MunicipioSucursal = "";
        //    EstadoSucursal = "";
        //    PaisSucursal = "";
        //    CodigoPostalSucursal = "";
        //}
        //void CargaDatosFacturacionCliente(EntCliente Cliente)
        //{
        //    Calle = Cliente.Calle;
        //    NumeroExterior = Cliente.NoExterior;
        //    NumeroInterior = Cliente.NoInterior;
        //    Colonia = Cliente.Colonia;
        //    Localidad = Cliente.Localidad;
        //    Municipio = Cliente.Municipio;
        //    Estado = Cliente.Estado;
        //    Pais = "MÉXICO";
        //    CodigoPostal = Cliente.CP;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Pedido"></param>
        /// <param name="ListaProductos"></param>
        /// <param name="FechaFactura"></param>
        /// <param name="FormaPago">Contado-Crédito</param>
        /// <param name="MedioPago">Efectivo-Tarjeta-Cheque</param>
        /// <param name="CondicionPago">Pago en una sola Exhibición</param>
        /// <param name="NumeroCuenta">No identificado</param>
        /// <param name="NumeroCertificadoSelloDigital">CSD</param>
        /// <returns></returns>
        public string Facturar(EntEmpresa Emisor, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente,
                                string FolioFactura, DateTime FechaFactura, string FormaPago, string MedioPago, string CondicionPago,
                                string NumeroCuenta, string PathGuardaArchivos,
                                decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS,
                                string Observaciones)
        {
            string respuesta;
            string rfc;
            //XAXX010101000
            //Emisor.RFC = "LAN7008173R5";
            rfc= "MSE061107IA8";

            IEPS = 0.08m;
            switch (Emisor.TipoTasaIVAId)
            {
                case 1: //16%
                    IVA = 0.16m;
                    break;
                case 2: //0%
                    IVA = 0m;
                    break;
                case 3: //Exento
                    IVA = 0;
                    break;
            }

           System.IO.StreamWriter st = new System.IO.StreamWriter(LayoutFile);//@"C:\Users\TIIM\Documents\TIIM\FacturaPruebaCreada3.txt" //20001000000200000278
            st.WriteLine("[Encabezado]");
            st.WriteLine("serie|AA");
            st.WriteLine("fecha|" + FechaFactura.ToString("yyyy-MM-ddTHH:mm:ss"));
            st.WriteLine("folio|" + FolioFactura);
            st.WriteLine("tipoDeComprobante|ingreso");
            st.WriteLine("formaDePago|" + FormaPago);
            st.WriteLine("metodoDePago|" + MedioPago);
            st.WriteLine("condicionesDePago|" + CondicionPago);
            st.WriteLine("NumCtaPago|" + NumeroCuenta);
            st.WriteLine("subTotal|" + Pedido.SubTotal.ToString("0.00"));
            st.WriteLine("descuento|0");
            st.WriteLine("total|" + Pedido.Total.ToString("0.00"));
            st.WriteLine("Moneda|MXN");
            st.WriteLine("TipoCambio|0.00");
            st.WriteLine("noCertificado|" + Emisor.NoCertificado);
            st.WriteLine("LugarExpedicion|Los Mochis, Sinaloa");

            st.WriteLine("[DatosAdicionales]");
            st.WriteLine("tipoDocumento|Factura");
            st.WriteLine("plantillaPDF|custom");//gti_clasica");// clasic");// custom");
            st.WriteLine("colorLetraE|000000");
            st.WriteLine("colorPlantillaHex|DCE3E6");
            st.WriteLine("logotipo|"); //lg_27cb5900e159233c421067e //GREE
            st.WriteLine("observaciones|"+ Observaciones.Replace("\r", " ").Replace("\n", " "));
            //st.WriteLine("observaciones|"+ string.Format("{0} POR ESTE CONDUCTO CEDO LOS DERECHOS DE LA PRESENTE FACTURA A BANOBRAS S.N.C., INSTITUCIÓN FIDUCIARIA EN EL FIDEICOMISO 728 FIPATERM QUEDANDO BANOBRAS S.N.C. EN PODER DE DICHA FACTURA HASTA EN TANTO NO SEA CUBIERTO EN SU TOTALIDAD EL FINANCIAMIENTO QUE SE ME HA OTORGADO.{1} ENDOSO IGUALMENTE LA FACTURA Y AUTORIZO A BANOBRAS S.N.C. INSTITUCIÓN FIDUCIARIA EN EL FIDEICOMISO 728 FIPATERM PARA SI DEJO DE PAGAR PARCIAL O TOTALMENTE MI ADEUDO (CREDITO) Y ESTE SEA PAGADO POR MI AVAL, POR EL PROVEEDOR O POR CUALQUIER PERSONA FISICA O MORAL, BANOBRAS S.N.C. INSITUCIÓN FIDUICIARIA EN EL FIDEICOMISO 728 FIPATERM PODRÁ ENTREGAR ESTA FACTURA A QUIEN RALICE EL PAGO POR MI.{2} FIRMA DEL USUARIO _____________________________", "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", " ", " . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"));
            st.WriteLine("emailCliente|" + Cliente.Email);

            st.WriteLine("[Emisor]");
            //st.WriteLine("rfc|" + Emisor.RFC);
            st.WriteLine("rfc|" + rfc);
            st.WriteLine("nombre|" + Emisor.NombreFiscal);
            st.WriteLine("RegimenFiscal|" + Emisor.RegimenFiscal);

            st.WriteLine("[DomicilioFiscal]");
            st.WriteLine("calle|" + Emisor.Calle);
            st.WriteLine("noExterior|" + Emisor.NoExterior);
            st.WriteLine("noInterior| " + Emisor.NoInterior);
            st.WriteLine("colonia|" + Emisor.Colonia);
            st.WriteLine("localidad|" + Emisor.Localidad);
            st.WriteLine("municipio|" + Emisor.Municipio);
            st.WriteLine("estado|" + Emisor.Estado);
            st.WriteLine("pais|México");
            st.WriteLine("codigoPostal|" + Emisor.CP);

            // [ExpedidoEn]
            //calle|CERRADA DE AZUCENAS
            //noExterior|109
            //noInterior|
            //colonia|REFORMA
            //localidad|
            //municipio|OAXACA DE JUAREZ
            //estado|OAXACA
            //pais|México
            //codigoPostal|68050
            //st.WriteLine(CalleSucursal + "||||||||");

            st.WriteLine("[Receptor]");
            st.WriteLine("rfc|" + Cliente.RFC);
            st.WriteLine("nombre|" + Cliente.NombreFiscal);

            st.WriteLine("[Domicilio]");
            st.WriteLine("calle|" + Cliente.Calle);
            st.WriteLine("noExterior|" + Cliente.NoExterior);
            st.WriteLine("noInterior|" + Cliente.NoInterior);
            st.WriteLine("colonia|" + Cliente.Colonia);
            st.WriteLine("localidad|" + Cliente.Localidad);
            st.WriteLine("municipio|" + Cliente.Municipio);
            st.WriteLine("estado|" + Cliente.Estado);
            st.WriteLine("pais|MÉXICO");
            st.WriteLine("codigoPostal|" + Cliente.CP);

            foreach (EntProducto p in ListaProductos)
            {
                st.WriteLine("[Concepto]");
                st.WriteLine("cantidad|" + p.Cantidad);
                st.WriteLine("unidad|" + p.Unidad);
                st.WriteLine("noIdentificacion|");
                st.WriteLine("descripcion|" + p.Descripcion);
                st.WriteLine("valorUnitario|" + (p.PrecioVenta / (1 + IVA)).ToString("0.00"));
                st.WriteLine("importe|" + (p.Precio / (1 + IVA)).ToString("0.00"));
                //st.WriteLine("valorUnitario|" + p.PrecioVentaSinIVA.ToString("0.00"));
                //st.WriteLine("importe|" + p.PrecioSinIVA.ToString("0.00"));
            }

            st.WriteLine("[ImpuestoTrasladado]");
            st.WriteLine("impuesto|IVA");
            //st.WriteLine("importe|" + cantidadIva.ToString("0.00"));
            st.WriteLine("importe|" + CantidadIVA.ToString("0.00"));
            st.WriteLine("tasa|" + 100 * IVA);

            if (CantidadIEPS > 0)
            {
                st.WriteLine("[ImpuestoTrasladado]");
                st.WriteLine("impuesto|IEPS");
                //st.WriteLine("importe|" + cantidadIva.ToString("0.00"));
                st.WriteLine("importe|" + CantidadIEPS.ToString("0.00"));
                st.WriteLine("tasa|" + 100 * IEPS);
            }
            //decimal cantidadIvaRetenido=0;
            if (Cliente.TipoPersonaId == 2)//PERSONA MORAL
            {
                if (Emisor.TipoTasaIVAId == 1)
                {
                    //cantidadIvaRetenido = (cantidadIva / 3) * 2;
                    st.WriteLine("[ImpuestoRetenido]");
                    st.WriteLine("impuesto|IVA");
                    //st.WriteLine("importe|" + cantidadIvaRetenido.ToString("0.00"));
                    st.WriteLine("importe|" + IVARetenido.ToString("0.00"));
                }
                //cantidadISR = subtotal * ISR;
                st.WriteLine("[ImpuestoRetenido]");
                st.WriteLine("impuesto|ISR");
                //st.WriteLine("importe|" + cantidadISR.ToString("0.00"));
                st.WriteLine("importe|" + ISRRetenido.ToString("0.00"));
            }
            st.Close();

            ////string keyfile = @"C:\TIIM\FacturacionModerna\CertificadosDemo-FacturacionModernaNeue\CertificadosDemo-FacturacionModerna\TUEM470405V4A\TUEM470405V4A.key";
            ////string certfile = @"C:\TIIM\FacturacionModerna\CertificadosDemo-FacturacionModernaNeue\CertificadosDemo-FacturacionModerna\TUEM470405V4A\TUEM470405V4A.cer";
            ////string password = "12345678a";
            ///*  CREAR LA CONFIGURACION DE CONEXION CON EL SERVICIO SOAP
            // * *    Los parametros configurables son:
            // * *    1.- string UserID; Nombre de usuario que se utiliza para la conexion con SOAP
            // * *    2.- string UserPass; Contraseña del usuario para conectarse a SOAP
            // * *    3.- string emisorRFC; RFC del contribuyente
            // * *    4.- string archivoKey; archivo llave
            // * *    5.- string strCer; archivo de certificado
            // * *    6.- string clave; contraseña del archivo llave del certificado
            // * *    7.- string urlActivarCancelacion; URL de la conexion con SOAP
            // * La configuracion inicial es para el ambiente de pruebas
            //*/
            //activarCancelacion activarC = new activarCancelacion();
            ///*
            // * Si desea cambiar alguna configuracion realizarla solo realizar lo siguiente
            // * activarC.UserID = "Miusuario";  Por poner un ejemplo
            //*/
            //activarC.archivoCer = certfile;
            //activarC.archivoKey = keyfile;
            //activarC.clave = password;

            //activarC.UserID = UserID;// "UsuarioPruebasWS";
            //activarC.UserPass = UserPass;// "b9ec2afa3361a59af4b4d102d3f704eabdf097d4";
            //activarC.emisorRFC = emisorRFC;// "LAN7008173R5";

            //ActivarCancelado activation = new ActivarCancelado();
            ////WSConecFM.Resultados r_wsconect = new WSConecFM.Resultados();
            WSConecFM.Resultados r_wsconect = new Resultados();//activation.Activacion(activarC);

            // WSConecFMNeue.Timbrado_ManagerPortClient fm = new WSConecFMNeue.Timbrado_ManagerPortClient();

            // Crear instancia, para los para metros enviados a requestTimbradoCFDI
            requestTimbrarCFDI reqt = new requestTimbrarCFDI();
            reqt.UserID = UserID;// "UsuarioPruebasWS";
            reqt.UserPass = UserPass;// "b9ec2afa3361a59af4b4d102d3f704eabdf097d4";
            reqt.emisorRFC = rfc;//Emisor.RFC;// "LAN7008173R5";
            //DESCOMENTAR EN PRODUCCION
            //reqt.urlTimbrado = "https://t2.facturacionmoderna.com/timbrado/soap";
            //COMENTAR EN PRODUCCION
            reqt.urlTimbrado = "https://t1demo.facturacionmoderna.com/timbrado/soap";

            //reqt.generarCBB = true;
            //reqt.generarTXT = true;
            reqt.generarPDF = true;
            //TUEM470405V4A

            Timbrado timbra = new Timbrado();

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
        public string FacturarNotaCredito(EntEmpresa Emisor, decimal Total, string Descripcion, string FolioFactura, EntCliente Cliente, DateTime FechaFactura, string FormaPago, string MedioPago, string CondicionPago, string NumeroCuenta, string PathGuardaArchivos)

        {
            string respuesta;

            //CargaDatosFacturacionEmpresa();
            //CargaDatosFacturacionCliente(Cliente);

            IVA = 0.16m;

            //System.IO.StreamWriter st = new System.IO.StreamWriter(PathCreaArchivoBaseTxt);//@"C:\Users\TIIM\Documents\TIIM\FacturaPruebaCreada3.txt" //20001000000200000278
            System.IO.StreamWriter st = new System.IO.StreamWriter(LayoutFileNC);//@"C:\Users\TIIM\Documents\TIIM\FacturaPruebaCreada3.txt" //20001000000200000278

            decimal subtotal = Total / (1 + IVA);
            //subtotal = (Pedido.Total - (Pedido.Total * 0.16m));

            decimal cantidadIva = subtotal * IVA;
            //cantidadIva = Pedido.Total - subtotal;

            st.WriteLine("[Encabezado]");
            st.WriteLine("serie|NC-");
            st.WriteLine("fecha|" + FechaFactura.ToString("yyyy-MM-ddTHH:mm:ss"));
            st.WriteLine("folio|" + FolioFactura);
            st.WriteLine("tipoDeComprobante|egreso");
            st.WriteLine("formaDePago|" + FormaPago);
            st.WriteLine("metodoDePago|NA");
            st.WriteLine("condicionesDePago|" + CondicionPago);
            st.WriteLine("NumCtaPago|" + NumeroCuenta);
            st.WriteLine("subTotal|" + subtotal.ToString("0.00"));
            st.WriteLine("descuento|0");
            st.WriteLine("total|" + Total.ToString("0.00"));
            st.WriteLine("Moneda|MXN");
            st.WriteLine("TipoCambio|0.00");
            st.WriteLine("noCertificado|" + NumeroCertificadoSelloDigital);
            st.WriteLine("LugarExpedicion|Los Mochis, Sinaloa");

            st.WriteLine("[DatosAdicionales]");
            st.WriteLine("tipoDocumento|Nota de crédito");
            st.WriteLine("plantillaPDF|custom");
            st.WriteLine("colorLetraE|000000");
            st.WriteLine("colorPlantillaHex|2ECCFA");
            st.WriteLine("logotipo|");
            st.WriteLine("observaciones|");
            st.WriteLine("emailCliente|" + Cliente.Email);

            st.WriteLine("[Emisor]");
            st.WriteLine("rfc|" + "LAN7008173R5");
            st.WriteLine("nombre|" + Emisor.NombreFiscal);
            st.WriteLine("RegimenFiscal|" + Emisor.RegimenFiscal);

            st.WriteLine("[DomicilioFiscal]");
            st.WriteLine("calle|CARRANZA Y GUILLERMO PRIETO");
            st.WriteLine("noExterior|101");
            st.WriteLine("noInterior|");
            st.WriteLine("colonia|Primer Cuadro (Centro)");
            st.WriteLine("localidad|LOS MOCHIS");
            st.WriteLine("municipio|AHOME");
            st.WriteLine("estado|SINALOA");
            st.WriteLine("pais|México");
            st.WriteLine("codigoPostal|81200");

            // [ExpedidoEn]
            //calle|CERRADA DE AZUCENAS
            //noExterior|109
            //noInterior|
            //colonia|REFORMA
            //localidad|
            //municipio|OAXACA DE JUAREZ
            //estado|OAXACA
            //pais|México
            //codigoPostal|68050
            //st.WriteLine(CalleSucursal + "||||||||");

            st.WriteLine("[Receptor]");
            st.WriteLine("rfc|" + Cliente.RFC);// LAN7008173R5");//" + Cliente.RFC);
            st.WriteLine("nombre|" + Cliente.NombreFiscal);

            st.WriteLine("[Domicilio]");
            st.WriteLine("calle|" + Calle);
            st.WriteLine("noExterior|" + NumeroExterior);
            st.WriteLine("noInterior|" + NumeroInterior);
            st.WriteLine("colonia|" + Colonia);
            st.WriteLine("localidad|" + Localidad);
            st.WriteLine("municipio|" + Municipio);
            st.WriteLine("estado|" + Estado);
            st.WriteLine("pais|" + Pais);
            st.WriteLine("codigoPostal|" + CodigoPostal);

            st.WriteLine("[Concepto]");
            st.WriteLine("cantidad|1");
            st.WriteLine("unidad|No aplica");
            st.WriteLine("noIdentificacion|");
            st.WriteLine("descripcion|"+Descripcion);//NOTA DE CRÉDITO.
            st.WriteLine("valorUnitario|" + subtotal.ToString("0.00"));
            st.WriteLine("importe|" + subtotal.ToString("0.00"));

            st.WriteLine("[ImpuestoTrasladado]");
            st.WriteLine("impuesto|IVA");
            st.WriteLine("importe|" + cantidadIva.ToString("0.00"));
            st.WriteLine("tasa|" + 100 * IVA);

            st.Close();


            WSConecFM.Resultados r_wsconect = new Resultados();//activation.Activacion(activarC);

            // Crear instancia, para los para metros enviados a requestTimbradoCFDI
            requestTimbrarCFDI reqt = new requestTimbrarCFDI();
            reqt.UserID = UserID;// "UsuarioPruebasWS";
            reqt.UserPass = UserPass;// "b9ec2afa3361a59af4b4d102d3f704eabdf097d4";
            //NOTACREDITO
            reqt.emisorRFC = "LAN7008173R5";// Emisor.RFC;// "LAN7008173R5";
            reqt.urlTimbrado = "https://t1demo.facturacionmoderna.com/timbrado/soap";
            //"https://t2.facturacionmoderna.com/timbrado/soap";
            //reqt.generarPDF = true;

            Timbrado timbra = new Timbrado();

            r_wsconect = timbra.Timbrar(LayoutFileNC, reqt);
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
        public string Cancelar(EntEmpresa Emisor, string UUID)
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

            //ActivarCancelado activation = new ActivarCancelado();
            WSConecFM.Resultados r_wsconect = new Resultados();// activation.Activacion(activarC);

            string rfc= "LAN7008173R5";
            rfc = "MSE061107IA8";
            //Emisor.RFC = "LAN7008173R5";
            requestCancelarCFDI reqc = new requestCancelarCFDI();
            reqc.UserID = UserID;// "UsuarioPruebasWS";
            reqc.UserPass = UserPass;// "b9ec2afa3361a59af4b4d102d3f704eabdf097d4";
            reqc.emisorRFC = rfc;// Emisor.RFC;
            reqc.urlCancelado = "https://t1demo.facturacionmoderna.com/timbrado/soap";
            reqc.uuid = UUID;

            //CancelarCFDI cancela = new CancelarCFDI();
            Cancelado cancela = new Cancelado();
            r_wsconect = cancela.Cancelar(reqc, UUID);

            if (!r_wsconect.status)
            {
                respuesta = r_wsconect.message; //MessageBox.Show(r_wsconect.message);
                throw new Exception("ERROR AL CANCELAR-" + respuesta);
                //Environment.Exit(-1);
            }

            return respuesta;
        }

        public string Facturar33(EntEmpresa Emisor, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente,
                                string FolioFactura, DateTime FechaFactura,
                                string TipoComprobante, string UsoCFDI,
                                string FormaPago, string MetodoPago, string CondicionPago,
                                string NumeroCuenta, string PathGuardaArchivos,
                                decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS,
                                string Observaciones)
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

            System.IO.StreamWriter st = new System.IO.StreamWriter(LayoutFile);//@"C:\Users\TIIM\Documents\TIIM\FacturaPruebaCreada3.txt" //20001000000200000278

            st.WriteLine("[ComprobanteFiscalDigital]");
            st.WriteLine("Version=3.3");
            st.WriteLine("Serie=AA");
            st.WriteLine("Folio=" + FolioFactura);
            st.WriteLine("Fecha=" + FechaFactura.ToString("yyyy-MM-ddTHH:mm:ss"));
            st.WriteLine("FormaPago=" + FormaPago);
            st.WriteLine("NoCertificado=" + NumeroCertificadoSelloDigital);
            st.WriteLine("CondicionesDePago=" + CondicionPago);
            st.WriteLine("SubTotal="  +Pedido.SubTotal.ToString("0.00"));
            st.WriteLine("Descuento=0");
            st.WriteLine("Moneda=MXN");
            st.WriteLine("Total=" + Pedido.Total.ToString("0.00"));
            st.WriteLine("TipoDeComprobante=I");
            st.WriteLine("MetodoPago=" + MetodoPago); //st.WriteLine("MetodoPago=" + MedioPago);
            st.WriteLine("LugarExpedicion="+Emisor.CP);

            st.WriteLine("[DatosAdicionales]");
            st.WriteLine("tipoDocumento=FACTURA");
            st.WriteLine("observaciones=" + Observaciones.Replace("\r", " ").Replace("\n", " ")+" Facturación Versión 3.3 - TIIM Tecnología | www.tiimtecnologia.com |"); //Facturación Versión 3.3 - TIIM Tecnología | www.tiimtecnologia.com |");
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
                st.WriteLine("NoIdentificacion=" + p.Id.ToString());//TTIM | MENS01*
                st.WriteLine("Cantidad=" + p.Cantidad);
                st.WriteLine("ClaveUnidad=" + p.ClaveUnidad); //TIIM | E48 Unidad de servicio //st.WriteLine("ClaveUnidad=" + p.ClaveUnidad);
                if (p.Unidad.Length > 20)
                    st.WriteLine("Unidad=" + p.Unidad.Remove(20));//+ p.Unidad //st.WriteLine("Unidad=" + p.TipoUnidad);
                else
                    st.WriteLine("Unidad=" + p.Unidad);//+ p.Unidad //st.WriteLine("Unidad=" + p.TipoUnidad);

                st.WriteLine("Descripcion=" + p.Descripcion.Replace('|', '-'));

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
                                            DateTime FechaPago, string FormaPago, decimal Monto,
                                            string UUID, string FolioFactura, decimal SaldoAnterior,
                                            string PathGuardaArchivos)
        {
            string respuesta;

            string layoutFile = @"C:\TIIM\Facturacion\FacturaComplePagoBase.txt";

            //CargaDatosFacturacionEmpresa();
            //CargaDatosFacturacionCliente(Cliente);


            RFC = "LAN7008173R5";
            //NumeroCertificadoSelloDigital = "20001000000200000240";
            NumeroCertificadoSelloDigital = "20001000000300022815";//PRUEBAS LAN7|"20001000000300022815";

            IVA = 0.16m;

            System.IO.StreamWriter st = new System.IO.StreamWriter(layoutFile);//@"C:\Users\TIIM\Documents\TIIM\FacturaPruebaCreada3.txt" //20001000000200000278


            st.WriteLine("[ReciboPagos]");
            //st.WriteLine("Version=3.3");
            st.WriteLine("Serie=CP");
            st.WriteLine("Folio=" + FolioComplemento);
            st.WriteLine("Fecha=" + FechaComplemento.ToString("yyyy-MM-ddTHH:mm:ss"));
            //st.WriteLine("FormaPago=" + FormaPago);
            st.WriteLine("NoCertificado=" + NumeroCertificadoSelloDigital);
            //st.WriteLine("CondicionesDePago=" + CondicionPago);
            //st.WriteLine("SubTotal=" + subtotal.ToString("0.00"));
            //st.WriteLine("Descuento=0");
            //st.WriteLine("Moneda=MXN");
            //st.WriteLine("Total=" + Pedido.Total.ToString("0.00"));
            //st.WriteLine("TipoDeComprobante=I");
            //st.WriteLine("MetodoPago=" + MedioPago); //st.WriteLine("MetodoPago=" + MedioPago);
            st.WriteLine("LugarExpedicion=81200");

            st.WriteLine("[DatosAdicionales]");
            st.WriteLine("tipoDocumento=RECIBO DE PAGO");
            st.WriteLine("observaciones=Factura: AA" + FolioFactura + "- SALDO ANTERIOR: " + SaldoAnterior.ToString("0.00") + " MONTO PAGADO: " + Monto.ToString("0.00") + ".          Facturación Versión 3.3 - TIIM Tecnología | www.tiimtecnologia.com |");//Facturación Versión 3.3 - TIIM Tecnología | www.tiimtecnologia.com |");
            st.WriteLine("platillaPDF=clasic");//gti_clasica");// clasic");// custom");
            //st.WriteLine("colorLetraE=000000");
            //st.WriteLine("colorPlantillaHex=000000");
            st.WriteLine("logotipo=lg_a0f8b790af59b2e36be4cf1");//lg_a0f8b790af59b2e36be4cf1//TIIM 


            st.WriteLine("[Emisor]");
            st.WriteLine("Rfc=" + RFC);//RFC PRUEBAS.
            st.WriteLine("Nombre=" + Emisor.NombreFiscal);
            st.WriteLine("RegimenFiscal=" + RegimenFiscalId);

            st.WriteLine("[Receptor]");
            st.WriteLine("Rfc=" + RFC);//Cliente.RFC;
            st.WriteLine("Nombre=" + Cliente.NombreFiscal);
            //st.WriteLine("UsoCFDI=" + UsoCFDI); //UsoCFDI| TIIM | I04:Equipo de computo y accesorios    //st.WriteLine("UsoCFDI=" + Emisor.UsoCDFI);

            st.WriteLine("[ComplementoPagos]");
            st.WriteLine("Version=1.0");//;

            st.WriteLine("[Pago#1]");
            st.WriteLine("FechaPago=" + FechaPago.ToString("yyyy-MM-ddTHH:mm:ss"));
            st.WriteLine("FormaDePagoP=" + FormaPago);
            st.WriteLine("MonedaP=MXN");
            st.WriteLine("TipoCambioP=");
            st.WriteLine("Monto=" + Monto.ToString("0.00"));

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

            st.WriteLine("DoctoRelacionado.IdDocumento =[" + UUID + "]");
            st.WriteLine("DoctoRelacionado.Serie =[AA]");
            st.WriteLine("DoctoRelacionado.Folio =[" + FolioFactura + "]");
            st.WriteLine("DoctoRelacionado.MonedaDR =[MXN]");
            st.WriteLine("DoctoRelacionado.TipoCambioDR =[]");
            st.WriteLine("DoctoRelacionado.MetodoDePagoDR =[PPD]");
            st.WriteLine("DoctoRelacionado.NumParcialidad =[1]");
            st.WriteLine("DoctoRelacionado.ImpSaldoAnt =[" + SaldoAnterior.ToString("0.00") + "]");
            st.WriteLine("DoctoRelacionado.ImpPagado =[" + Monto.ToString("0.00") + "]");
            st.WriteLine("DoctoRelacionado.ImpSaldoInsoluto =[" + (SaldoAnterior - Monto).ToString("0.00") + "]");

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


    }
}
