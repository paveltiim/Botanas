using AiresEntidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiresUtilerias
{
    public class UtiImpresiones : UtiAbstracta
    {
        public string TotalImprime, SubTotalImprime, IVAImprime, PagoImprime, Cambio, Descuento, Observaciones, Pendiente, Vendedor, FechaEntregaImprime, Responsable, Cliente, Recibe, Folio, PagoTCmeses;
        public string Direccion, Colonia, Telefono, Celular, RFC, Email;
        bool MuestraInstalacionElectrica, MuestraGarantiaMotor, Anticipo60;
        string DiaEntregaDesde, DiaEntregaHasta, PorcentajePagoTarjeta;

        int longituRenglonLargo = 90;
        int longituRenglonMediano = 80;
        int longituRenglonDescripcionProducto = 48;

        List<EntProducto> ListaProductos;
        public void AsignaValoresParametrosImpresion(List<EntProducto> ListaProductos, string TotalImporte, string SubTotalImporte, string IVAImporte, string PagoImporte, string Cambio, string Observaciones, string Pendiente, string Responsable, string Cliente, string Recibe, string FechaEntrega, string Folio)
        {
            this.ListaProductos = ListaProductos;
            this.TotalImprime = TotalImporte;
            this.SubTotalImprime = SubTotalImporte;
            this.IVAImprime = IVAImporte;
            this.PagoImprime = PagoImporte;
            this.Cambio = Cambio;
            this.Observaciones = Observaciones;
            this.Pendiente = Pendiente;
            this.Responsable = Responsable;
            this.Cliente = Cliente;
            this.Recibe = Recibe;
            this.FechaEntregaImprime = FechaEntrega;
            this.Folio = Folio;
        }

        public void AsignaValoresParametrosImpresionDatosCliente(string Cliente, string Direccion, string Colonia, string Telefono, string Celular, string RFC, string Email, string Folio)
        {
            this.Cliente = Cliente;
            this.Direccion = Direccion;
            this.Colonia = Colonia;
            this.Telefono = Telefono;
            this.Celular = Celular;
            this.RFC = RFC;
            this.Email = Email;
            this.Folio = Folio;
        }

        public void AsignaValoresParametrosImpresionPresupuesto(bool MuestraInstalacionElectrica, bool MuestraGarantiaMotor, bool Anticipo60, string DiaEntregaDesde, string DiaEntregaHasta, string PorcentajePagoTarjeta, string PagoTCmeses)
        {
            this.MuestraInstalacionElectrica = MuestraInstalacionElectrica;
            this.MuestraGarantiaMotor = MuestraGarantiaMotor;
            this.Anticipo60 = Anticipo60;
            this.DiaEntregaDesde = DiaEntregaDesde;
            this.DiaEntregaHasta = DiaEntregaHasta;
            this.PorcentajePagoTarjeta = PorcentajePagoTarjeta;
            this.PagoTCmeses = PagoTCmeses;
        }

        public void ImprimirCotizacion(EntEmpresa Empresa, EntCliente Cliente, EntPedido Pedido, List<EntProducto> ListaProductos,
                                    decimal TasaIVA,
                                    Image LogoEmpresa, Image Leyenda, Image Firma, Graphics Graphic)
        {
            System.Drawing.Font font = new System.Drawing.Font("Microsoft Tai Le", 10);
            System.Drawing.Font fontBold = new System.Drawing.Font("Microsoft Tai Le", 10, FontStyle.Bold);
            System.Drawing.Font fontPequeña = new System.Drawing.Font("Microsoft Tai Le", 8);
            System.Drawing.Font fontPequeñaBold = new System.Drawing.Font("Microsoft Tai Le", 8, FontStyle.Bold);
            System.Drawing.Font fontPequeñaBold2 = new System.Drawing.Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            System.Drawing.Font fontMini = new System.Drawing.Font("Microsoft Tai Le", 7);
            Pen pen = new Pen(Color.Black, 1);
            float fontHeight = font.GetHeight();
            float fontPequeñaHeight = fontPequeña.GetHeight();

            int startX = 100;
            int startY = 40;
            int offset = 20;
            int offsetXLimite = 400;
            int offsetX = 135;
            int offsetXpequeña = 10;
            int offsetXmedio = 50;
            int endX = 650;
            int inicioY;

            Graphic.DrawImage(LogoEmpresa, offsetXmedio, startY);

            Graphic.DrawString(Empresa.NombreFiscal, fontBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXmedio, startY + offset);
            Graphic.DrawString("COTIZACIÓN", fontBold, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio - offsetXpequeña, startY);
            //
            Graphic.DrawString(Pedido.NumOrden, font, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("R.F.C:", fontBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            Graphic.DrawString(Empresa.RFC, fontPequeña, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña - 5, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;

            Graphic.DrawString("Fecha", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña, startY + offset);

            Graphic.DrawString("LUGAR DE EXPEDICIÓN:", fontBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            Graphic.DrawString(Empresa.CP, fontPequeña, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXpequeña, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;

            Graphic.DrawString(DateTime.Today.ToShortDateString(), fontPequeña, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);

            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            Graphic.DrawString("Datos del Cliente", fontBold, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("Num. Cliente:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            if (Cliente.Nombre == null)
                Cliente.Nombre = "";
            if (Cliente.Nombre.Split('-').Length > 2)
                Graphic.DrawString(Cliente.Nombre.Split('-')[0], font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset);
            else
                Graphic.DrawString(Cliente.Nombre, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("Razon Social:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(Cliente.NombreFiscal, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("R.F.C:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(Cliente.RFC, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset + 1);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("Dirección:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(Cliente.Direccion, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset + 1);

            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;

            Graphic.DrawString("Cantidad", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña - offsetXmedio, startY + offset);
            Graphic.DrawString("Código", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString("Descricpción", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXmedio + offsetXmedio, startY + offset);
            //Graphic.DrawString("Unidad", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXmedio + offsetXLimite - offsetXpequeña, startY + offset);
            Graphic.DrawString("Precio Unitario", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXpequeña + offsetXpequeña, startY + offset);
            //Graphic.DrawString("Precio Neto", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString("Importe", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXmedio + offsetXpequeña, startY + offset);

            inicioY = startY + offset + (int)fontPequeñaHeight + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            foreach (EntProducto p in ListaProductos)
            {
                Graphic.DrawString(p.Cantidad.ToString(), fontPequeña, new SolidBrush(Color.Black), startX - offsetXpequeña - offsetXpequeña - offsetXpequeña, startY + offset);
                Graphic.DrawString(FormatoMoney(p.PrecioVentaSinIVA), fontPequeña, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio, startY + offset);
                //Graphic.DrawString(FormatoMoney(p.PrecioVenta), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio -offsetXpequeña, startY + offset);
                Graphic.DrawString(FormatoMoney(p.PrecioSinIVA), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXmedio + offsetXpequeña + offsetXpequeña, startY + offset);

                Graphic.DrawString(p.Codigo, fontMini, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña, startY + offset);
                offset = EscribeRenglonesDescripciones(p.Descripcion, Graphic, fontPequeña, (int)fontHeight, pen, startX + offsetXmedio + offsetXpequeña + offsetXmedio, startY + offset, offset + 2, longituRenglonDescripcionProducto);
                //Graphic.DrawLine(pen, startX, startY + offset, startX + endX, startY + offset);
            }
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            Graphic.DrawString("SUBTOTAL", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString(FormatoMoney(Pedido.SubTotal), fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            offset = offset + (int)fontHeight + 2;
            Graphic.DrawString("IVA " + TasaIVA.ToString("0.00%"), fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString(FormatoMoney(Pedido.IVA), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            offset = offset + (int)fontHeight + 2;

            //if (Pedido.TipoMonedaId == 2)
            //{
            //    Graphic.DrawString("TOTAL (USD)", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);

            //    Graphic.DrawString(FormatoMoney(Pedido.TotalUSD), fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //}
            //else
            //{
                Graphic.DrawString("TOTAL", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
                Graphic.DrawString(FormatoMoney(Pedido.Total), fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //}
            offset = offset + (int)fontHeight;

            if (!string.IsNullOrWhiteSpace(Pedido.Observaciones))
            {
                Pedido.Observaciones = Pedido.Observaciones.Trim();
                Graphic.DrawString("Observaciones: ", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
                if (Pedido.Observaciones.Split('\n').Length > 0)
                {
                    foreach (string d in Pedido.Observaciones.Split('\n'))
                        offset = EscribeRenglonesDescripciones(d, Graphic, fontPequeña, (int)fontHeight, pen, startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset, offset + 2,
                                                                longituRenglonDescripcionProducto + 60);
                }
                else
                    offset = EscribeRenglonesDescripciones(Pedido.Observaciones, Graphic, fontPequeña, (int)fontHeight, pen, startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset, offset + 2,
                                                            longituRenglonDescripcionProducto + 60);
            }

            offset = offset + (int)fontHeight;
            offset = offset + (int)fontHeight;
            offset = offset + (int)fontHeight;
            Graphic.DrawImage(Leyenda, offsetXmedio, startY + offset);

            offset = offset + (int)fontHeight;
            offset = offset + (int)fontHeight;
            offset = offset + (int)fontHeight;
            offset = offset + (int)fontHeight;
            offset = offset + (int)fontHeight;
            offset = offset + (int)fontHeight;
            offset = offset + (int)fontHeight;
            offset = offset + (int)fontHeight;
            Graphic.DrawImage(Firma, offsetXmedio, startY + offset);
        }
        public void ImprimirOrdenSalidaConsignacionSoloProductos(EntEmpresa Empresa, EntCliente ClienteConsigna, EntPedido Pedido, 
                                    List<EntProducto> ListaProductos,
                                    decimal TasaIVA,
                                    Image LogoEmpresa, Image Leyenda, Image Firma, Graphics Graphic)
        {
            System.Drawing.Font font = new System.Drawing.Font("Microsoft Tai Le", 10);
            System.Drawing.Font fontBold = new System.Drawing.Font("Microsoft Tai Le", 10, FontStyle.Bold);
            System.Drawing.Font fontPequeña = new System.Drawing.Font("Microsoft Tai Le", 8);
            System.Drawing.Font fontPequeñaBold = new System.Drawing.Font("Microsoft Tai Le", 8, FontStyle.Bold);
            System.Drawing.Font fontPequeñaBold2 = new System.Drawing.Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            System.Drawing.Font fontMini = new System.Drawing.Font("Microsoft Sans Serif", 6);
            Pen pen = new Pen(Color.Black, 1);
            float fontHeight = font.GetHeight();
            float fontPequeñaHeight = fontPequeña.GetHeight();

            int startX = 100;
            int startY = 40;
            int offset = 20;
            int offsetXLimite = 400;
            int offsetX = 135;
            int offsetXpequeña = 10;
            int offsetXmedio = 50;
            int endX = 650;
            int inicioY;

            Graphic.DrawImage(LogoEmpresa, offsetXmedio, startY);

            Graphic.DrawString(Empresa.NombreFiscal, fontBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXmedio, startY + offset);
            Graphic.DrawString("ORD. SALIDA CONS.", fontBold, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio - offsetXpequeña, startY);
            Graphic.DrawString(Pedido.NumOrden, font, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("R.F.C:", fontBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            Graphic.DrawString(Empresa.RFC, fontPequeña, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña - 5, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;

            Graphic.DrawString("Fecha", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña, startY + offset);

            Graphic.DrawString("LUGAR DE EXPEDICIÓN:", fontBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            Graphic.DrawString(Empresa.CP, fontPequeña, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXpequeña, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;

            Graphic.DrawString(DateTime.Today.ToShortDateString(), fontPequeña, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);

            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            Graphic.DrawString("Datos del Cliente", fontBold, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("Razon Social:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(ClienteConsigna.NombreFiscal, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;
            //Graphic.DrawString("R.F.C:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            //Graphic.DrawString(Proveedor.RFC, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset + 1);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("Dirección:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(ClienteConsigna.Direccion, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset + 1);

            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;

            Graphic.DrawString("Cantidad", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio + offsetXpequeña, startY + offset);
            Graphic.DrawString("Clave - Descricpción", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXmedio - offsetXpequeña, startY + offset);
            Graphic.DrawString("Unidad", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXmedio + offsetXLimite - offsetXpequeña, startY + offset);
            Graphic.DrawString("Precio Unitario", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString("Importe", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXmedio + offsetXpequeña, startY + offset);

            inicioY = startY + offset + (int)fontPequeñaHeight + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            int index = 0;
            foreach (EntProducto p in ListaProductos)
            {
                if (index >= 0)//25
                {
                    Graphic.DrawString(p.Cantidad.ToString(), fontPequeña, new SolidBrush(Color.Black), startX - offsetXpequeña - offsetXpequeña - offsetXpequeña, startY + offset);
                    Graphic.DrawString(p.ClaveUnidad, fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXmedio + offsetXLimite - offsetXpequeña, startY + offset);
                    //if (Pedido.TipoMonedaId == 1)
                    //{
                        Graphic.DrawString(FormatoMoney(p.PrecioCosto), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXmedio + offsetXmedio + offsetXLimite, startY + offset);
                        Graphic.DrawString(FormatoMoney(p.PrecioC), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXLimite, startY + offset);
                    //}
                    //else if (Pedido.TipoMonedaId == 2)
                    //{
                    //    Graphic.DrawString(FormatoMoney(p.PrecioCostoUSD), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXmedio + offsetXmedio + offsetXLimite, startY + offset);
                    //    Graphic.DrawString(FormatoMoney(p.CantidadDecimal * p.PrecioCostoUSD), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXLimite, startY + offset);
                    //}

                    offset = EscribeRenglonesDescripciones(p.Codigo + " - " + p.Descripcion, Graphic, fontPequeña, (int)fontHeight, pen, startX + offsetXmedio - offsetXpequeña, startY + offset, offset + 2, longituRenglonDescripcionProducto);
                    //Graphic.DrawLine(pen, startX, startY + offset, startX + endX, startY + offset);
                }
                index++;
            }
        }
        public void ImprimirOrdenSalidaConsignacion(EntEmpresa Empresa, EntCliente ClienteConsigna, EntPedido Pedido, List<EntProducto> ListaProductos,
                                    decimal TasaIVA,
                                    Image LogoEmpresa, Image Leyenda, Image Firma, Graphics Graphic)
        {
            System.Drawing.Font font = new System.Drawing.Font("Microsoft Tai Le", 10);
            System.Drawing.Font fontBold = new System.Drawing.Font("Microsoft Tai Le", 10, FontStyle.Bold);
            System.Drawing.Font fontPequeña = new System.Drawing.Font("Microsoft Tai Le", 8);
            System.Drawing.Font fontPequeñaBold = new System.Drawing.Font("Microsoft Tai Le", 8, FontStyle.Bold);
            System.Drawing.Font fontPequeñaBold2 = new System.Drawing.Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            System.Drawing.Font fontMini = new System.Drawing.Font("Microsoft Sans Serif", 6);
            Pen pen = new Pen(Color.Black, 1);
            float fontHeight = font.GetHeight();
            float fontPequeñaHeight = fontPequeña.GetHeight();

            int startX = 100;
            int startY = 40;
            int offset = 20;
            int offsetXLimite = 400;
            int offsetX = 135;
            int offsetXpequeña = 10;
            int offsetXmedio = 50;
            int endX = 650;
            int inicioY;

            Graphic.DrawImage(LogoEmpresa, offsetXmedio, startY);

            Graphic.DrawString(Empresa.NombreFiscal, fontBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXmedio, startY + offset);
            Graphic.DrawString("ORD. SALIDA CONS.", fontBold, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio - offsetXpequeña, startY);
            Graphic.DrawString(Pedido.NumOrden, font, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("R.F.C:", fontBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            Graphic.DrawString(Empresa.RFC, fontPequeña, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña - 5, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;

            Graphic.DrawString("Fecha", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña, startY + offset);

            Graphic.DrawString("LUGAR DE EXPEDICIÓN:", fontBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            Graphic.DrawString(Empresa.CP, fontPequeña, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXpequeña, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;

            Graphic.DrawString(DateTime.Today.ToShortDateString(), fontPequeña, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);

            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            Graphic.DrawString("Datos del Cliente", fontBold, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("Razon Social:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(ClienteConsigna.NombreFiscal, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;
            //Graphic.DrawString("R.F.C:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            //Graphic.DrawString(Proveedor.RFC, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset + 1);
            //offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("Dirección:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(ClienteConsigna.Direccion, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset + 1);

            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;

            Graphic.DrawString("Cantidad", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio + offsetXpequeña, startY + offset);
            Graphic.DrawString("Clave - Descricpción", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXmedio - offsetXpequeña, startY + offset);
            Graphic.DrawString("Unidad", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXmedio + offsetXLimite - offsetXpequeña, startY + offset);
            Graphic.DrawString("Precio Unitario", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString("Importe", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXmedio + offsetXpequeña, startY + offset);

            inicioY = startY + offset + (int)fontPequeñaHeight + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            int index = 0;
            foreach (EntProducto p in ListaProductos)
            {
                if (index >= 0)//25
                {
                    Graphic.DrawString(p.Cantidad.ToString(), fontPequeña, new SolidBrush(Color.Black), startX - offsetXpequeña - offsetXpequeña - offsetXpequeña, startY + offset);
                    Graphic.DrawString(p.ClaveUnidad, fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXmedio + offsetXLimite - offsetXpequeña, startY + offset);
                    //if (Pedido.TipoMonedaId == 1)
                    //{
                    Graphic.DrawString(FormatoMoney(p.PrecioCosto), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXmedio + offsetXmedio + offsetXLimite, startY + offset);
                    Graphic.DrawString(FormatoMoney(p.PrecioC), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXLimite, startY + offset);
                    //Graphic.DrawString(FormatoMoney(p.PrecioDecimalC), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXLimite, startY + offset);
                    //}
                    //else if (Pedido.TipoMonedaId == 2)
                    //{
                    //    Graphic.DrawString(FormatoMoney(p.PrecioCostoUSD), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXmedio + offsetXmedio + offsetXLimite, startY + offset);
                    //    Graphic.DrawString(FormatoMoney(p.CantidadDecimal * p.PrecioCostoUSD), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXLimite, startY + offset);
                    //}

                    offset = EscribeRenglonesDescripciones(p.Codigo + " - " + p.Descripcion, Graphic, fontPequeña, (int)fontHeight, pen, startX + offsetXmedio - offsetXpequeña, startY + offset, offset + 2, longituRenglonDescripcionProducto);
                    //Graphic.DrawLine(pen, startX, startY + offset, startX + endX, startY + offset);
                }
                //index++;
            }

            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            Graphic.DrawString("SUBTOTAL", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString(FormatoMoney(Pedido.SubTotal), fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            offset = offset + (int)fontHeight + 2;
            Graphic.DrawString("IVA " + TasaIVA.ToString("0.00%"), fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString(FormatoMoney(Pedido.IVA), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            offset = offset + (int)fontHeight + 2;

            //if (Pedido.TipoMonedaId == 2)
            //{
            //    //Graphic.DrawString(" (USD)", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset + (int)fontHeight + 2);
            //    Graphic.DrawString("TOTAL (USD)", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);

            //    Graphic.DrawString(FormatoMoney(Pedido.TotalUSD), fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //}
            //else
            //{
                Graphic.DrawString("TOTAL ", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);

                Graphic.DrawString(FormatoMoney(Pedido.Total), fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //}
            offset = offset + (int)fontHeight;
            offset = offset + (int)fontHeight;
            offset = offset + (int)fontHeight;

            if (!string.IsNullOrWhiteSpace(Pedido.Observaciones.Trim()))
            {
                Graphic.DrawString("Observaciones: ", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
                //offset = EscribeRenglonesDescripciones(Pedido.Observaciones, Graphic, fontPequeña, (int)fontHeight, pen, startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset, offset + 20, longituRenglonDescripcionProducto + 80);
                if (Pedido.Observaciones.Split('\n').Length > 0)
                {
                    foreach (string d in Pedido.Observaciones.Split('\n'))
                        offset = EscribeRenglonesDescripciones(d, Graphic, fontPequeña, (int)fontHeight, pen, startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset, offset + 2,
                                                                longituRenglonDescripcionProducto + 60);
                }
                else
                    offset = EscribeRenglonesDescripciones(Pedido.Observaciones, Graphic, fontPequeña, (int)fontHeight, pen, startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset, offset + 2,
                                                            longituRenglonDescripcionProducto + 60);
            }

            offset = offset + (int)fontHeight;
            offset = offset + (int)fontHeight;
            offset = offset + (int)fontHeight;
            Graphic.DrawImage(Leyenda, offsetXmedio, startY + offset);

            offset = offset + (int)fontHeight;
            offset = offset + (int)fontHeight;
            offset = offset + (int)fontHeight;
            offset = offset + (int)fontHeight;
            Graphic.DrawImage(Firma, offsetXmedio, startY + offset);
        }

        int CuentaCaracteresHastaEspacio(string Descripcion)
        {
            //|| (int)Descripcion[Descripcion.Length - 1]==10
            if (Descripcion.Length == 0)
                return 0;
            if (char.IsWhiteSpace(Descripcion[Descripcion.Length - 1]))
                return 1;
            else
                return 1 + CuentaCaracteresHastaEspacio(Descripcion.Remove(Descripcion.Length - 1));
        }
        /// <summary>
        /// Método recursivo que divide Descripcion en renglones dependiendo de la longitud de la Descripcion 
        /// y de la LongitudRenglon.
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <param name="Graphic"></param>
        /// <param name="Font"></param>
        /// <param name="FontHeight"></param>
        /// <param name="Pen"></param>
        /// <param name="StartX"></param>
        /// <param name="StartY"></param>
        /// <param name="Offset"></param>
        /// <param name="LongitudRenglon">Longitud límite del renglon</param>
        /// <returns></returns>
        int EscribeRenglonesDescripciones(string Descripcion, Graphics Graphic, Font Font, int FontHeight, Pen Pen, int StartX, int StartY, int Offset, int LongitudRenglon)
        {
            int caracteresHastaEspacio = 1;
            if (Descripcion.Length >= LongitudRenglon)
            {
                //|| (int)Descripcion[LongitudRenglon - 1] == 10
                if (char.IsWhiteSpace(Descripcion[LongitudRenglon - 1]))                                                 //startX + offsetXmedio + offsetXpequeña //startY + offset
                    Graphic.DrawString(Descripcion.Remove(LongitudRenglon - 1), Font, new SolidBrush(Color.Black), StartX, StartY);
                else
                {
                    caracteresHastaEspacio = CuentaCaracteresHastaEspacio(Descripcion.Remove(LongitudRenglon - caracteresHastaEspacio));
                    Graphic.DrawString(Descripcion.Remove(LongitudRenglon - caracteresHastaEspacio), Font, new SolidBrush(Color.Black), StartX, StartY);
                    //Graphic.DrawString(Descripcion.Remove(LongitudRenglon - 1) + '-', Font, new SolidBrush(Color.Black), StartX, StartY);
                }
                StartY = StartY + FontHeight;
                Offset = EscribeRenglonesDescripciones(Descripcion.Remove(0, LongitudRenglon - caracteresHastaEspacio), Graphic, Font, FontHeight, Pen, StartX, StartY, Offset + FontHeight, LongitudRenglon);
            }
            else
            {
                Graphic.DrawString(Descripcion, Font, new SolidBrush(Color.Black), StartX, StartY);
                Offset += FontHeight;
            }
            return Offset;
        }

    }
}
