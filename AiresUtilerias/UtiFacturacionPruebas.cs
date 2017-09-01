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
        string NombreLegal;
        string RegimenFiscal;
        string Calle;
        string NumeroExterior;
        string NumeroInterior;
        string Colonia;
        string Localidad;
        string Municipio;
        string Estado;
        string Pais;
        string CodigoPostal;

        string CalleSucursal;
        string NumeroExteriorSucursal;
        string NumeroInteriorSucursal;
        string ColoniaSucursal;
        string LocalidadSucursal;
        string MunicipioSucursal;
        string EstadoSucursal;
        string PaisSucursal;
        string CodigoPostalSucursal;
        public decimal IVA, IEPS;
        public decimal ISR = 0.10m;

        void CargaDatosFacturacionEmpresa()
        {
            //RFC = "CATS8706298F6";
            //NombreLegal = "SERGIO PATRICIO CAZARES TARÍN";
            //RegimenFiscal = "Persona Física con Actividad Empresarial y Profesional";
            ////COMENTAR EN PRODUCCION
            ////NumeroCertificadoSelloDigital = "20001000000300022815";
            ////COMENTAR EN PRODUCCION
            //NumeroCertificadoSelloDigital = "00001000000400591711";//Distribuidora LM

            CalleSucursal = "";
            NumeroExteriorSucursal = "";
            NumeroInteriorSucursal = "";
            ColoniaSucursal = "";
            LocalidadSucursal = "";
            MunicipioSucursal = "";
            EstadoSucursal = "";
            PaisSucursal = "";
            CodigoPostalSucursal = "";
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
                                decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS)
        {
            string respuesta;

            //XAXX010101000
            Emisor.RFC = "LAN7008173R5";

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
            st.WriteLine("observaciones|"+ string.Format("POR ESTE CONDUCTO CEDO LOS DERECHOS DE LA PRESENTE FACTURA A BANOBRAS S.N.C., INSTITUCIÓN FIDUCIARIA EN EL FIDEICOMISO 728 FIPATERM QUEDANDO BANOBRAS S.N.C. EN PODER DE DICHA FACTURA HASTA EN TANTO NO SEA CUBIERTO EN SU TOTALIDAD EL FINANCIAMIENTO QUE SE ME HA OTORGADO.{0} ENDOSO IGUALMENTE LA FACTURA Y AUTORIZO A BANOBRAS S.N.C. INSTITUCIÓN FIDUCIARIA EN EL FIDEICOMISO 728 FIPATERM PARA SI DEJO DE PAGAR PARCIAL O TOTALMENTE MI ADEUDO (CREDITO) Y ESTE SEA PAGADO POR MI AVAL, POR EL PROVEEDOR O POR CUALQUIER PERSONA FISICA O MORAL, BANOBRAS S.N.C. INSITUCIÓN FIDUICIARIA EN EL FIDEICOMISO 728 FIPATERM PODRÁ ENTREGAR ESTA FACTURA A QUIEN RALICE EL PAGO POR MI.{1} FIRMA DEL USUARIO _____________________________","           ","            "));
            st.WriteLine("emailCliente|" + Cliente.Email);

            st.WriteLine("[Emisor]");
            st.WriteLine("rfc|" + Emisor.RFC);
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
                st.WriteLine("unidad|" + p.TipoUnidad);
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
            reqt.emisorRFC = Emisor.RFC;// "LAN7008173R5";
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
        public string FacturarNotaCredito(decimal Total, string FolioFactura, EntCliente Cliente, DateTime FechaFactura, string FormaPago, string MedioPago, string CondicionPago, string NumeroCuenta, string PathGuardaArchivos)

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
            st.WriteLine("serie|NC");
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
            st.WriteLine("logotipo|C:\\TIIM\\Sistema\\LOGOTIPOMIRAGE.png");
            st.WriteLine("observaciones|");
            st.WriteLine("emailCliente|" + Cliente.Email);

            st.WriteLine("[Emisor]");
            st.WriteLine("rfc|" + RFC);
            st.WriteLine("nombre|" + NombreLegal);
            st.WriteLine("RegimenFiscal|" + RegimenFiscal);

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
            st.WriteLine("rfc|" + Cliente.RFC);
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
            st.WriteLine("descripcion|Descuento por pornto pago.");
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
            reqt.emisorRFC = Emisor.RFC;// "LAN7008173R5";
            reqt.urlTimbrado = "https://t2.facturacionmoderna.com/timbrado/soap";
            //https://t1demo.facturacionmoderna.com/timbrado/soap
            reqt.generarPDF = true;

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

            Emisor.RFC = "LAN7008173R5";
            requestCancelarCFDI reqc = new requestCancelarCFDI();
            reqc.UserID = UserID;// "UsuarioPruebasWS";
            reqc.UserPass = UserPass;// "b9ec2afa3361a59af4b4d102d3f704eabdf097d4";
            reqc.emisorRFC = Emisor.RFC;
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
    }
}
