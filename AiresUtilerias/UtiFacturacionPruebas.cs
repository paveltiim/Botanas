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
        //int CuentaCaracteresHastaEspacio(string Descripcion)
        //{
        //    if (char.IsWhiteSpace(Descripcion[Descripcion.Length - 1]))
        //        return 1;
        //    else
        //        return 1 + CuentaCaracteresHastaEspacio(Descripcion.Remove(Descripcion.Length - 1));
        //}
        ///// <summary>
        ///// Método recursivo que divide Descripcion en renglones dependiendo de la longitud de la Descripcion 
        ///// y de la LongitudRenglon.
        ///// </summary>
        ///// <param name="Descripcion"></param>
        ///// <param name="Graphic"></param>
        ///// <param name="Font"></param>
        ///// <param name="FontHeight"></param>
        ///// <param name="Pen"></param>
        ///// <param name="StartX"></param>
        ///// <param name="StartY"></param>
        ///// <param name="Offset"></param>
        ///// <param name="LongitudRenglon">Longitud límite del renglon</param>
        ///// <returns></returns>
        //int EscribeRenglonesDescripciones(string Descripcion, int Offset, int LongitudRenglon)
        //{
        //    int caracteresHastaEspacio = 1;
        //    if (Descripcion.Length >= LongitudRenglon)
        //    {
        //        if (char.IsWhiteSpace(Descripcion[LongitudRenglon - 1]))                                                 
        //            Descripcion.Remove(LongitudRenglon - 1);
        //        else
        //        {
        //            caracteresHastaEspacio = CuentaCaracteresHastaEspacio(Descripcion.Remove(LongitudRenglon - caracteresHastaEspacio));
        //            Descripcion.Remove(LongitudRenglon - caracteresHastaEspacio);
        //        }                
        //        Offset = EscribeRenglonesDescripciones(Descripcion.Remove(0, LongitudRenglon - caracteresHastaEspacio), Offset, LongitudRenglon);
        //    }
        //    else
        //    {
        //        Offset += 10;
        //    }
        //    return Offset;
        //}
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
            string pagare = "Por medio de este PAGARÉ me obligo a pagar  incondicionalmente  a  la  orden  de "+ Emisor.NombreFiscal + ",  la  cantidad  de  "+ Pedido.Total.ToString("$0.00") + " correspondiente al Pedido estipulado en esta FACTURA, en esta ciudad. La cantidad que ampara este PAGARÉ causará intereses al tipo de 10 % mensual en caso de mora. Este PAGARÉ es mercantil no domiciliario y se rige por lo estipulado en la última parte del art. 173 de la ley general de títulos y operaciones de Crédito.  Firma: "+Cliente.NombreFiscal+" ____________________________";

            System.IO.StreamWriter st = new System.IO.StreamWriter(LayoutFile);//@"C:\Users\TIIM\Documents\TIIM\FacturaPruebaCreada3.txt" //20001000000200000278

            st.WriteLine("[ComprobanteFiscalDigital]");
            st.WriteLine("Version=3.3");
            st.WriteLine("Serie=AA");
            st.WriteLine("Folio=" + FolioFactura);
            st.WriteLine("Fecha=" + FechaFactura.AddHours(-8).ToString("yyyy-MM-ddTHH:mm:ss"));
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
                    decimal ivaRetenidoProd = importe*0.06m;
                    st.WriteLine("Impuestos.Retenciones.Base = [" + importe.ToString("0.00") +"]");//p.PrecioSinIVA.ToString("0.00") + "]");
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

            if (IVARetenido > 0) {
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
    }
}
