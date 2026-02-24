using AiresEntidades;
using AiresNegocio;
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
        int longituRenglonDescripcionProducto = 50;

        List<EntProducto> ListaProductos;
        List<EntPedido> ListaPedidosImprime;

        EntPedido PedidoImprime;
        public void AsignaValoresParametrosImpresion(EntPedido PedidoSeleccionado, List<EntProducto> ListaProductos)
        {
            this.PedidoImprime = PedidoSeleccionado;
            this.ListaProductos = ListaProductos;
        }
        public void ImprimeRecibo(Graphics Graphic, Image pbLogo)
        {
            Font font = new Font("Courier New", 7);
            Font fontPequeña = new Font("Courier New", 6);
            Font fontMini = new Font("Courier New", 5);
            Font fontBodoni = new Font("Bodoni MT Condensed", 8);
            Font fontBodoniMini = new Font("Bodoni MT Condensed", 5);
            Font fontTitle = new Font("Book Antiqua", 12);
            Font fontSubTitle = new Font("Book Antiqua", 8, FontStyle.Bold);
            Pen pen = new Pen(Color.Black, 1);
            float fontHeight = font.GetHeight();
            float fontPequeñaHeight = fontPequeña.GetHeight();
            float fontBodoniHeight = fontBodoni.GetHeight();

            int startX = 5;
            int startY = 5;
            int offset = 20;
            int offsetXLimite = 200;
            int offsetXpequeña = 10;
            int offsetXmedio = 50;

            int longituRenglonGeneral = 60;
            int longituRenglonDescripcion = 30;

            int positionCenter = startX + offsetXmedio + (offsetXpequeña*2) ;
            int positionCenterShort = positionCenter + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio;

            Graphic.DrawImage(pbLogo, startX + (offsetXpequeña*8), startY);
            offset = offset + (int)pbLogo.Height-20;
            //Graphic.DrawString("Botanas JAVY", fontTitle, new SolidBrush(Color.Black), positionCenter - (offsetXpequeña*2), startY);
            //offset = offset + (int)fontHeight;
            Graphic.DrawString("CALLE DANIEL CASTILLO #2150", fontBodoni, new SolidBrush(Color.Black), 
                positionCenter + (offsetXpequeña * 1), startY + offset);
            offset = offset + (int)fontBodoniHeight;
            Graphic.DrawString("COLONIA EJIDO MORELOS", fontBodoni, new SolidBrush(Color.Black), 
                positionCenter + (offsetXpequeña * 2), startY + offset);
            offset = offset + (int)fontBodoniHeight;
            Graphic.DrawString("LOS MOCHIS, SIN.", fontBodoni, new SolidBrush(Color.Black), 
                positionCenter + (offsetXpequeña * 3)+5, startY + offset);
            offset = offset + (int)fontBodoniHeight;
            Graphic.DrawString("TEL. 668 165 95 43", fontBodoni, new SolidBrush(Color.Black), 
                positionCenter + (offsetXpequeña * 3), startY + offset);
            offset = offset + (int)fontHeight;
            //Graphic.DrawString("669-255-97-61", fontBodoni, new SolidBrush(Color.Black), positionCenter + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //offset = offset + (int)fontHeight;
            // Create rectangle.
            //Rectangle rect = new Rectangle(startX + offsetXpequeña + offsetXpequeña + (offsetXpequeña), startY, 130, 40);
            // Draw rectangle to screen.
            //Graphic.DrawRectangle(pen, rect);

            offset = offset + (int)fontHeight;
            Graphic.DrawString("Los Mochis, Sin.", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight;
            Graphic.DrawString("Fecha: " + PedidoImprime.Fecha.ToShortDateString(), font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight;
            offset = offset + (int)fontHeight;
            Graphic.DrawLine(pen, startX, offset, startX + offsetXLimite + offsetXmedio + offsetXmedio, offset);
            offset = offset + (int)fontHeight;
            Graphic.DrawString("RECIBO: " + PedidoImprime.Id.ToString().PadLeft(5,'0'), fontSubTitle, new SolidBrush(Color.Black), 
                positionCenter + (offsetXpequeña * 4), startY + offset - 10);
            offset = offset + (int)fontHeight;
            pen.DashPattern = new float[] { 1, 1 };
            Graphic.DrawLine(pen, startX, offset, startX + offsetXLimite + offsetXmedio + offsetXmedio, offset);
            offset = offset + (int)fontPequeñaHeight;
            //Graphic.DrawString("Fecha y Hora: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"), fontPequeña, new SolidBrush(Color.Black), startX, startY + offset - 10);
            //offset = offset + (int)fontPequeñaHeight;
            ////Condision si cliente mide mas de 38..
            //if (cmbClientes.Text.Length > 30)
            //    Graphic.DrawString("Cliente: " + Cliente.Remove(30), fontPequeña, new SolidBrush(Color.Black), startX, startY + offset - 10);
            //else
            //    Graphic.DrawString("Cliente: " + Cliente, fontPequeña, new SolidBrush(Color.Black), startX, startY + offset - 10);
            offset = EscribeRenglonesDescripciones("Cobrador: " + PedidoImprime.Empleado, Graphic, fontPequeña, (int)fontHeight, pen, startX, startY + offset, offset, longituRenglonGeneral);
            offset = offset + (int)fontPequeñaHeight;
            offset = EscribeRenglonesDescripciones("Cliente: " + PedidoImprime.Cliente, Graphic, fontPequeña, (int)fontHeight, pen, startX, startY + offset, offset, longituRenglonGeneral);
            //offset = EscribeRenglonesDescripciones("Dirección: " + PedidoImprime.DireccionCliente.Split(',')[0], Graphic, fontPequeña, (int)fontHeight, pen, startX, startY + offset, offset, longituRenglonGeneral);
            //offset = EscribeRenglonesDescripciones("Colonia: " + PedidoImprime.DireccionCliente.Split(',')[1], Graphic, fontPequeña, (int)fontHeight, pen, startX, startY + offset, offset, longituRenglonGeneral);
            //offset = offset + (int)fontPequeñaHeight;
            offset = offset + (int)fontPequeñaHeight;

            Graphic.DrawLine(pen, startX, offset, startX + offsetXLimite + offsetXmedio + offsetXmedio, offset);
            offset = offset + (int)fontPequeñaHeight;
            //Graphic.DrawString("Lavadora: " + PedidoImprime.Folio, font, new SolidBrush(Color.Black), startX, startY + offset);
            //offset = offset + (int)fontHeight;
            //switch (PedidoImprime.PlazoPagoId)
            //{
            //    case 1:
            //        PedidoImprime.FechaEntrega = PedidoImprime.FechaAbono.AddDays(7);
            //        break;
            //    case 2:
            //        PedidoImprime.FechaEntrega = PedidoImprime.FechaAbono.AddDays(1);
            //        break;
            //    case 3:
            //        PedidoImprime.FechaEntrega = PedidoImprime.FechaAbono.AddDays(2);
            //        break;
            //    case 4:
            //        PedidoImprime.FechaEntrega = PedidoImprime.FechaAbono.AddDays(3);
            //        break;
            //}
            //Graphic.DrawString("Fecha Vencimiento: " + PedidoImprime.FechaEntrega.ToShortDateString(), font, new SolidBrush(Color.Black), startX, startY + offset);
            //offset = offset + (int)fontHeight;
            //offset = offset + (int)fontHeight;
            //offset = offset + (int)fontHeight;
            Graphic.DrawString("Descripción", fontBodoni, new SolidBrush(Color.Black), startX, startY + offset - 10);
            Graphic.DrawString("Precio/u", fontBodoni, new SolidBrush(Color.Black), 
                startX + offsetXLimite - offsetXmedio - (offsetXpequeña*0), startY + offset - 10);
            Graphic.DrawString("Cant.", fontBodoni, new SolidBrush(Color.Black), 
                startX + offsetXLimite - offsetXpequeña + (offsetXpequeña*1), startY + offset - 10);
            Graphic.DrawString("Importe", fontBodoni, new SolidBrush(Color.Black), 
                startX + offsetXLimite + offsetXmedio - (offsetXpequeña*1), startY + offset - 10);
            offset = offset + (int)fontHeight;

            Graphic.DrawLine(pen, startX, offset, startX + offsetXLimite + offsetXmedio + offsetXmedio, offset);

            foreach (EntProducto p in ListaProductos)
            {
                Graphic.DrawString(FormatoMoney(p.PrecioVenta), font, new SolidBrush(Color.Black), 
                    startX + offsetXLimite - (offsetXpequeña*1)+5 - offsetXmedio, startY + offset);
                Graphic.DrawString(p.Cantidad.ToString(), font, new SolidBrush(Color.Black), 
                    startX + offsetXLimite - (offsetXpequeña * 0)+5, startY + offset);
                Graphic.DrawString(FormatoMoney(p.Precio), font, new SolidBrush(Color.Black), 
                    startX + offsetXLimite - (offsetXpequeña * 2) + offsetXmedio, startY + offset);
                 offset = EscribeRenglonesDescripciones(p.Descripcion,Graphic, font, (int)fontHeight, pen, startX, startY + offset, 
                                                        offset, longituRenglonDescripcion)+2;
               //offset = offset + (int)fontHeight;
            }

            offset = offset + (int)fontPequeñaHeight;
            offset = offset + (int)fontPequeñaHeight;
            Graphic.DrawLine(pen, startX, offset, startX + offsetXLimite + offsetXmedio + offsetXmedio, offset);
            offset = offset + (int)fontPequeñaHeight;

            //Graphic.DrawString("Monto: " + FormatoMoney(PedidoImprime.Pago), fontSubTitle, new SolidBrush(Color.Black), positionCenterShort - offsetXpequeña, startY + offset);
            //offset = offset + (int)fontPequeñaHeight;
            //offset = offset + (int)fontPequeñaHeight;
            //offset = EscribeRenglonesDescripciones("RECIBI DE LAVADORAS LEON UBICADO EN AVE. GARDENIA COL. FLORES MAGÓN 210-A EN MAZATLÁN, SIN.,
            //UNA LAVADORA CON VALOR DE $10,000 EN CALIDAD DE RENTA. ME COMPROMETO HACER BUEN USO DE ELLA Y HACERME RESPONSABLE EN CASO DE ROBO O
            //EXTRAVIO PAGANDO EL TOTAL DEL COSTO DE LA MISMA.", Graphic, fontPequeña, (int)fontHeight, pen, startX, startY + offset, offset, longituRenglonGeneral);

            Graphic.DrawString("Total: " + FormatoMoney(PedidoImprime.Total), fontSubTitle, new SolidBrush(Color.Black), 
                                startX + offsetXLimite ,
                                startY + offset - 10);
            offset = offset + (int)fontPequeñaHeight;
            offset = offset + 2;
            pen = new Pen(Color.Black, 1);
            Graphic.DrawLine(pen, positionCenter - (offsetXpequeña * 2), offset, positionCenter + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, offset);
            //Graphic.DrawString("FIRMA", font, new SolidBrush(Color.Black), positionCenterShort, startY + offset);
            //offset = offset + (int)fontPequeñaHeight;
            //offset = offset + (int)fontHeight;
            Graphic.DrawString("¡GRACIAS POR SU PREFERENCIA!", font, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXpequeña, startY + offset);
        }
        public void ImprimeCorte(Graphics Graphic)
        {
            Font font = new Font("Courier New", 7);
            Font fontPequeña = new Font("Courier New", 6);
            Font fontMini = new Font("Courier New", 5);
            Font fontBodoni = new Font("Bodoni MT Condensed", 6);
            Font fontBodoniMini = new Font("Bodoni MT Condensed", 5);
            Font fontTitle = new Font("Book Antiqua", 12);
            Font fontSubTitle = new Font("Book Antiqua", 8, FontStyle.Bold);
            Pen pen = new Pen(Color.Black, 1);
            float fontHeight = font.GetHeight();
            float fontPequeñaHeight = fontPequeña.GetHeight();

            int startX = 5;
            int startY = 5;
            int offset = 20;
            int offsetXLimite = 200;
            int offsetXpequeña = 10;
            int offsetXmedio = 50;

            int longituRenglonGeneral = 40;

            int positionCenter = startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio;
            int positionCenterShort = positionCenter + offsetXpequeña + offsetXpequeña + offsetXpequeña;

            //Graphic.DrawImage(pbLogo.Image, startX + offsetXpequeña + offsetXpequeña + (offsetXpequeña), startY);
            Graphic.DrawString("Botanas JAVY", fontTitle, new SolidBrush(Color.Black), positionCenter - offsetXpequeña, startY);
            offset = offset + (int)fontHeight;
            Graphic.DrawString("CALLE DANIEL CASTILLO #2150", fontBodoni, new SolidBrush(Color.Black), positionCenter + offsetXmedio - offsetXpequeña - offsetXpequeña - offsetXpequeña - offsetXpequeña - offsetXpequeña, startY + offset);
            offset = offset + (int)fontHeight;
            Graphic.DrawString("COLONIA EJIDO MORELOS", fontBodoni, new SolidBrush(Color.Black), positionCenter - offsetXpequeña - offsetXpequeña - offsetXpequeña - offsetXpequeña + offsetXmedio, startY + offset);
            offset = offset + (int)fontHeight;
            Graphic.DrawString("LOS MOCHIS, SIN.", fontBodoni, new SolidBrush(Color.Black), positionCenter - offsetXpequeña - offsetXpequeña - offsetXpequeña + offsetXmedio, startY + offset);
            offset = offset + (int)fontHeight;
            Graphic.DrawString("TEL. 668 165 95 43", fontBodoni, new SolidBrush(Color.Black), positionCenter - offsetXpequeña - offsetXpequeña - offsetXpequeña + offsetXmedio -5, startY + offset);
            offset = offset + (int)fontHeight;
            //Graphic.DrawString("669-255-97-61", fontBodoni, new SolidBrush(Color.Black), positionCenter + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //offset = offset + (int)fontHeight;
            // Create rectangle.
            //Rectangle rect = new Rectangle(startX + offsetXpequeña + offsetXpequeña + (offsetXpequeña), startY, 130, 40);
            // Draw rectangle to screen.
            //Graphic.DrawRectangle(pen, rect);

            offset = offset + (int)fontHeight;
            Graphic.DrawString("Los Mochis, Sin.", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight;
            Graphic.DrawString("Fecha: " + DateTime.Today.ToShortDateString(), font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight;
            offset = offset + (int)fontHeight;
            Graphic.DrawLine(pen, startX, offset, startX + offsetXLimite + offsetXmedio, offset);
            offset = offset + (int)fontHeight;
            //Graphic.DrawString("RECIBO: " + PedidoImprime.Id.ToString().PadLeft(5, '0'), fontSubTitle, new SolidBrush(Color.Black), positionCenter + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset - 10);
            //offset = offset + (int)fontHeight;

            pen.DashPattern = new float[] { 1, 1 };
            Graphic.DrawLine(pen, startX, offset, startX + offsetXLimite + offsetXmedio + offsetXmedio, offset);
            offset = offset + (int)fontPequeñaHeight;
            Graphic.DrawString("CORTE DEL DÍA", fontTitle, new SolidBrush(Color.Black), positionCenter - offsetXpequeña - offsetXpequeña, startY + offset);
            offset = offset + (int)fontHeight;
            //Graphic.DrawString("Fecha y Hora: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"), fontPequeña, new SolidBrush(Color.Black), startX, startY + offset - 10);
            //offset = offset + (int)fontPequeñaHeight;
            ////Condision si cliente mide mas de 38..
            //if (cmbClientes.Text.Length > 30)
            //    Graphic.DrawString("Cliente: " + Cliente.Remove(30), fontPequeña, new SolidBrush(Color.Black), startX, startY + offset - 10);
            //else
            //    Graphic.DrawString("Cliente: " + Cliente, fontPequeña, new SolidBrush(Color.Black), startX, startY + offset - 10);
            //offset = EscribeRenglonesDescripciones("Cobrador: " + PedidoImprime.Empleado, Graphic, fontPequeña, (int)fontHeight, pen, startX, startY + offset, offset, longituRenglonGeneral);
            //offset = offset + (int)fontPequeñaHeight;
            //offset = EscribeRenglonesDescripciones("Cliente: " + PedidoImprime.Cliente, Graphic, fontPequeña, (int)fontHeight, pen, startX, startY + offset, offset, longituRenglonGeneral);
            //offset = EscribeRenglonesDescripciones("Dirección: " + PedidoImprime.DireccionCliente.Split(',')[0], Graphic, fontPequeña, (int)fontHeight, pen, startX, startY + offset, offset, longituRenglonGeneral);
            //offset = EscribeRenglonesDescripciones("Colonia: " + PedidoImprime.DireccionCliente.Split(',')[1], Graphic, fontPequeña, (int)fontHeight, pen, startX, startY + offset, offset, longituRenglonGeneral);
            //offset = offset + (int)fontPequeñaHeight;
            offset = offset + (int)fontPequeñaHeight;
            offset = offset + (int)fontPequeñaHeight;

            Graphic.DrawLine(pen, startX, offset, startX + offsetXLimite + offsetXmedio + offsetXmedio, offset);
            offset = offset + (int)fontPequeñaHeight;
            //Graphic.DrawString("Lavadora: " + PedidoImprime.Folio, font, new SolidBrush(Color.Black), startX, startY + offset);
            //offset = offset + (int)fontHeight;
            //switch (PedidoImprime.PlazoPagoId)
            //{
            //    case 1:
            //        PedidoImprime.FechaEntrega = PedidoImprime.FechaAbono.AddDays(7);
            //        break;
            //    case 2:
            //        PedidoImprime.FechaEntrega = PedidoImprime.FechaAbono.AddDays(1);
            //        break;
            //    case 3:
            //        PedidoImprime.FechaEntrega = PedidoImprime.FechaAbono.AddDays(2);
            //        break;
            //    case 4:
            //        PedidoImprime.FechaEntrega = PedidoImprime.FechaAbono.AddDays(3);
            //        break;
            //}
            //Graphic.DrawString("Fecha Vencimiento: " + PedidoImprime.FechaEntrega.ToShortDateString(), font, new SolidBrush(Color.Black), startX, startY + offset);
            //offset = offset + (int)fontHeight;
            //offset = offset + (int)fontHeight;
            //offset = offset + (int)fontHeight;
            Graphic.DrawString("Descripción", fontBodoni, new SolidBrush(Color.Black), startX, startY + offset - 10);
            Graphic.DrawString("Precio", fontBodoni, new SolidBrush(Color.Black), startX + offsetXLimite - offsetXmedio, startY + offset - 10);
            offset = offset + (int)fontHeight;

            Graphic.DrawLine(pen, startX, offset, startX + offsetXLimite, offset);

            decimal total = 0;
            foreach (EntPedido ped in this.ListaPedidosImprime)
            {
                total += ped.Total;
                //foreach (EntProducto p in new BusProductos().ObtieneProductosPorPedido(ped.Id))
                //{
                offset = EscribeRenglonesDescripciones(ped.Descripcion, Graphic, fontPequeña, (int)fontHeight, pen, startX, startY + offset, offset, longituRenglonDescripcionProducto);
                //Graphic.DrawString(ped.Descripcion, font, new SolidBrush(Color.Black), startX, startY + offset);
                    Graphic.DrawString(FormatoMoney(ped.Total), font, new SolidBrush(Color.Black), startX + offsetXLimite - offsetXpequeña - offsetXmedio, startY + offset);
                    offset = offset + (int)fontHeight;
                //}
            }

            offset = offset + (int)fontPequeñaHeight;
            offset = offset + (int)fontPequeñaHeight;
            Graphic.DrawLine(pen, startX, offset, startX + offsetXLimite, offset);
            offset = offset + (int)fontPequeñaHeight;

            //Graphic.DrawString("Monto: " + FormatoMoney(PedidoImprime.Pago), fontSubTitle, new SolidBrush(Color.Black), positionCenterShort - offsetXpequeña, startY + offset);
            //offset = offset + (int)fontPequeñaHeight;
            //offset = offset + (int)fontPequeñaHeight;
            //offset = EscribeRenglonesDescripciones("RECIBI DE LAVADORAS LEON UBICADO EN AVE. GARDENIA COL. FLORES MAGÓN 210-A EN MAZATLÁN, SIN., UNA LAVADORA CON VALOR DE $10,000 EN CALIDAD DE RENTA. ME COMPROMETO HACER BUEN USO DE ELLA Y HACERME RESPONSABLE EN CASO DE ROBO O EXTRAVIO PAGANDO EL TOTAL DEL COSTO DE LA MISMA.", Graphic, fontPequeña, (int)fontHeight, pen, startX, startY + offset, offset, longituRenglonGeneral);

            //Graphic.DrawString("Total: " + FormatoMoney(PedidoImprime.Total), fontSubTitle, new SolidBrush(Color.Black), startX + offsetXLimite - offsetXmedio - offsetXmedio,
            //                    startY + offset - 10);
            offset = offset + (int)fontPequeñaHeight;
            offset = offset + 2;
            pen = new Pen(Color.Black, 1);
            Graphic.DrawLine(pen, positionCenter, offset, positionCenter + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, offset);
            //Graphic.DrawString("FIRMA", font, new SolidBrush(Color.Black), positionCenterShort, startY + offset);
            //offset = offset + (int)fontPequeñaHeight;
            //offset = offset + (int)fontHeight;
        }

        public void ImprimirNotaVentaSoloProductos(string Titulo, EntEmpresa Empresa, EntCliente Cliente,
                                        EntPedido Pedido,
                                        List<EntProducto> ListaProductos,
                                        decimal TasaIVA,
                                        Image LogoEmpresa, Image Leyenda, Image Firma, Graphics Graphic)
        {
            Font fontBoldTitulo = new System.Drawing.Font("Microsoft Tai Le", 14, FontStyle.Bold);
            Font font = new System.Drawing.Font("Microsoft Tai Le", 10);
            Font fontBold = new System.Drawing.Font("Microsoft Tai Le", 10, FontStyle.Bold);
            Font fontBodoni = new Font("Bodoni MT Condensed", 8);
            Font fontBodoniBold = new Font("Bodoni MT Condensed", 8, FontStyle.Bold);
            Font fontPequeña = new System.Drawing.Font("Microsoft Tai Le", 8);
            Font fontPequeñaBold = new System.Drawing.Font("Microsoft Tai Le", 8, FontStyle.Bold);
            Font fontPequeñaBold2 = new System.Drawing.Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            Font fontMini = new System.Drawing.Font("Microsoft Tai Le", 7);
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
            int positionCenter = startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio;

            Graphic.DrawImage(LogoEmpresa, positionCenter, startY - 100);


            Graphic.DrawString(Titulo, fontBold, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio - offsetXpequeña - offsetXpequeña - offsetXpequeña, startY);

            Graphic.DrawString(Pedido.NumOrden, font, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;

            Graphic.DrawString("Fecha", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;

            Graphic.DrawString(Pedido.Fecha.ToShortDateString(), fontPequeña, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);

            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("CALLE DANIEL CASTILLO #2150", fontBodoniBold, new SolidBrush(Color.Black), positionCenter + offsetXmedio + offsetXmedio, startY + offset);
            offset = offset + (int)fontHeight;
            Graphic.DrawString("COLONIA EJIDO MORELOS", fontBodoniBold, new SolidBrush(Color.Black), positionCenter + offsetXpequeña + offsetXmedio + offsetXmedio, startY + offset);
            offset = offset + (int)fontHeight;
            Graphic.DrawString("LOS MOCHIS, SIN.", fontBodoniBold, new SolidBrush(Color.Black), positionCenter + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            offset = offset + (int)fontHeight;
            Graphic.DrawString("TEL. 668 165 95 43", fontBodoniBold, new SolidBrush(Color.Black), positionCenter + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            offset = offset + (int)fontHeight;

            Graphic.DrawString("Datos del Cliente", fontBold, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 8;
            Graphic.DrawString("Num. Cliente:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(Cliente.Id.ToString().PadLeft(4, '0'), font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 8;
            //if (Cliente.Nombre == null)
            //    Cliente.Nombre = "";
            //if (Cliente.Nombre.Split('-').Length > 2)
            //    Graphic.DrawString(Cliente.Nombre.Split('-')[0], font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset);
            //else
            Graphic.DrawString("Cliente:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(Cliente.Nombre, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("Razon Social:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(Cliente.NombreFiscal, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("R.F.C:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(Cliente.RFC, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset + 1);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("SUCURSAL: ", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(Pedido.Sucursal.ToUpper(), font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset + 1);

            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;

            int iniciaTabla = startY + offset - 5;
            Graphic.DrawLine(pen, startX - 60, startY + offset - 5, offsetXLimite + 418, startY + offset - 5);
            Graphic.DrawString("Cantidad", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña - offsetXmedio, startY + offset);
            Graphic.DrawString("Código", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString("Descricpción", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXmedio + offsetXmedio, startY + offset);
            //Graphic.DrawString("Unidad", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXmedio + offsetXLimite - offsetXpequeña, startY + offset);
            Graphic.DrawString("Precio Unitario", fontPequeñaBold, new SolidBrush(Color.Black),
                                startX + offsetXLimite + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //Graphic.DrawString("Precio Neto", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString("Importe", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXmedio + offsetXpequeña, startY + offset);

            inicioY = startY + offset + (int)fontPequeñaHeight + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            Graphic.DrawLine(pen, startX - 60, startY + offset + 2, offsetXLimite + 418, startY + offset + 2);
            offset = offset + (int)fontPequeñaHeight + 2;
            foreach (EntProducto p in ListaProductos)
            {
                Graphic.DrawString(p.Cantidad.ToString(), fontPequeña, new SolidBrush(Color.Black), startX - offsetXpequeña - offsetXpequeña - offsetXpequeña, startY + offset);
                Graphic.DrawString(FormatoMoney(p.PrecioVenta), fontPequeña, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio, startY + offset);
                //Graphic.DrawString(FormatoMoney(p.PrecioVenta), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio -offsetXpequeña, startY + offset);
                Graphic.DrawString(FormatoMoney(p.Precio), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXmedio + offsetXpequeña + offsetXpequeña, startY + offset);

                Graphic.DrawString(p.Codigo, fontMini, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
                offset = EscribeRenglonesDescripciones(p.Descripcion, Graphic, fontPequeña, (int)fontHeight, pen, startX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXmedio, startY + offset, offset + 2, longituRenglonDescripcionProducto);
                //Graphic.DrawLine(pen, startX, startY + offset, startX + endX, startY + offset);
            }
            Graphic.DrawLine(pen, startX - 60, iniciaTabla, startX - 60, startY + offset + 10);//LINEA 1 VERTICAL
            Graphic.DrawLine(pen, startX + offsetXpequeña + offsetXpequeña, iniciaTabla, startX + offsetXpequeña + offsetXpequeña, startY + offset + 10);
            Graphic.DrawLine(pen, startX + offsetXpequeña + offsetXmedio + offsetXmedio - 10, iniciaTabla,
                                  startX + offsetXpequeña + offsetXmedio + offsetXmedio - 10, startY + offset + 10);
            Graphic.DrawLine(pen, startX + offsetXLimite + offsetXpequeña, iniciaTabla,
                                  startX + offsetXLimite + offsetXpequeña, startY + offset + 10);
            Graphic.DrawLine(pen,
                startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio - offsetXpequeña - offsetXpequeña,
                iniciaTabla,
                startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio - offsetXpequeña - offsetXpequeña,
                startY + offset + 10); //PENULTIMA LINEA VERTICAL
            Graphic.DrawLine(pen, offsetXLimite + offsetXLimite + offsetXpequeña + 8,
                                iniciaTabla,
                                offsetXLimite + offsetXLimite + offsetXpequeña + 8,
                                startY + offset + 10);//ULTIMA LINEA VERTICAL

            offset = offset + (int)fontPequeñaHeight + 2;
            Graphic.DrawLine(pen, startX - offsetXpequeña - offsetXpequeña - offsetXpequeña - offsetXpequeña - offsetXpequeña - 10,
                startY + offset - 5, offsetXLimite + 418, startY + offset - 5);//ULTIMA LINEA HORIZONTAL
            offset = offset + (int)fontPequeñaHeight;

            //Graphic.DrawString("SUBTOTAL", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //Graphic.DrawString(FormatoMoney(Pedido.SubTotal), fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //offset = offset + (int)fontHeight + 2;
            //Graphic.DrawString("IVA " + TasaIVA.ToString("0.00%"), fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //Graphic.DrawString(FormatoMoney(Pedido.IVA), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //offset = offset + (int)fontHeight + 2;
            //Graphic.DrawString("IEPS ", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //Graphic.DrawString(FormatoMoney(Pedido.IEPS), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //offset = offset + (int)fontHeight + 2;

            //Graphic.DrawString("TOTAL", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //Graphic.DrawString(FormatoMoney(Pedido.Total), fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //offset = offset + (int)fontHeight;

            //if (!string.IsNullOrWhiteSpace(Pedido.Observaciones))
            //{
            //    Graphic.DrawString("Observaciones: ", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            //    if (Pedido.Observaciones.Split('\n').Length > 0)
            //    {
            //        foreach (string d in Pedido.Observaciones.Split('\n'))
            //            offset = EscribeRenglonesDescripciones(d, Graphic, fontPequeña, (int)fontHeight, pen, startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset, offset + 2,
            //                                                    longituRenglonDescripcionProducto);
            //    }
            //    else
            //        offset = EscribeRenglonesDescripciones(Pedido.Observaciones, Graphic, fontPequeña, (int)fontHeight, pen, startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset, offset + 2,
            //                                                longituRenglonDescripcionProducto);
            //}

            //offset = offset + (int)fontHeight;
            //offset = offset + (int)fontHeight;
            //offset = offset + (int)fontHeight;
            //Graphic.DrawImage(Leyenda, offsetXmedio, startY + offset);

            //offset = offset + (int)fontHeight;
            //offset = offset + (int)fontHeight;
            //offset = offset + (int)fontHeight;
            //offset = offset + (int)fontHeight;
            //offset = offset + (int)fontHeight;
            //offset = offset + (int)fontHeight;
            //offset = offset + (int)fontHeight;
            //offset = offset + (int)fontHeight;
            //Graphic.DrawImage(Firma, offsetXmedio, startY + offset);
        }
        public void ImprimirNotaVenta(string Titulo, EntEmpresa Empresa, EntCliente Cliente,
                                        EntPedido Pedido,
                                        List<EntProducto> ListaProductos,
                                        decimal TasaIVA,
                                        Image LogoEmpresa, Image Leyenda, Image Firma, Graphics Graphic)
        {
            Font fontBoldTitulo = new System.Drawing.Font("Microsoft Tai Le", 14, FontStyle.Bold);
            Font font = new System.Drawing.Font("Microsoft Tai Le", 10);
            Font fontBold = new System.Drawing.Font("Microsoft Tai Le", 10, FontStyle.Bold);
            Font fontMedia = new System.Drawing.Font("Microsoft Tai Le", 12);
            Font fontBodoni = new Font("Bodoni MT Condensed", 8);
            Font fontBodoniBold = new Font("Bodoni MT Condensed", 12, FontStyle.Bold);
            Font fontPequeña = new System.Drawing.Font("Microsoft Tai Le", 8);
            Font fontPequeñaBold = new System.Drawing.Font("Microsoft Tai Le", 8, FontStyle.Bold);
            Font fontPequeñaBold2 = new System.Drawing.Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            Font fontMini = new System.Drawing.Font("Microsoft Tai Le", 7);
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
            int positionCenter = startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio;

            Graphic.DrawImage(LogoEmpresa, positionCenter, startY - 100);


            Graphic.DrawString(Titulo, fontBold, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio - offsetXpequeña - offsetXpequeña - offsetXpequeña, startY);

            Graphic.DrawString(Pedido.NumOrden, font, new SolidBrush(Color.Black), startX + offsetXLimite + (offsetXmedio * 4)+5, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;

            Graphic.DrawString("Fecha", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;

            Graphic.DrawString(Pedido.Fecha.ToShortDateString(), fontPequeña, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);

            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("CALLE DANIEL CASTILLO #2150", fontBodoniBold, new SolidBrush(Color.Black), positionCenter + (offsetXmedio * 2) + (offsetXpequeña * 3), startY + offset);
            offset = offset + (int)fontHeight;
            Graphic.DrawString("COLONIA EJIDO MORELOS", fontBodoniBold, new SolidBrush(Color.Black), positionCenter + (offsetXmedio * 2) + (offsetXpequeña*4), startY + offset);
            offset = offset + (int)fontHeight;
            Graphic.DrawString("LOS MOCHIS, SIN.", fontBodoniBold, new SolidBrush(Color.Black), positionCenter + offsetXmedio + offsetXmedio + offsetXmedio + (offsetXpequeña * 1), startY + offset);
            offset = offset + (int)fontHeight;
            Graphic.DrawString("TEL. 668 165 95 43", fontBodoniBold, new SolidBrush(Color.Black), positionCenter + offsetXmedio + offsetXmedio + offsetXmedio + 5, startY + offset);
            offset = offset + (int)fontHeight;

            Graphic.DrawString("Datos del Cliente", fontBold, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 8;
            Graphic.DrawString("Num. Cliente:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(Cliente.Id.ToString().PadLeft(4, '0'), font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 8;
            //if (Cliente.Nombre == null)
            //    Cliente.Nombre = "";
            //if (Cliente.Nombre.Split('-').Length > 2)
            //    Graphic.DrawString(Cliente.Nombre.Split('-')[0], font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset);
            //else
            Graphic.DrawString("Cliente:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(Cliente.Nombre, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("Razon Social:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(Cliente.NombreFiscal, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("R.F.C:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(Cliente.RFC, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset + 1);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("SUCURSAL: ", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(Pedido.Sucursal.ToUpper(), font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset + 1);

            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;

            int iniciaTabla = startY + offset - 5;
            Graphic.DrawLine(pen, startX - 60, startY + offset - 5, offsetXLimite + 418, startY + offset - 5);

            Graphic.DrawString("Cantidad", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña - offsetXmedio, startY + offset);
            Graphic.DrawString("Código", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString("Descricpción", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXmedio + offsetXmedio, startY + offset);
            //Graphic.DrawString("Unidad", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXmedio + offsetXLimite - offsetXpequeña, startY + offset);
            Graphic.DrawString("Precio c/u", fontPequeñaBold, new SolidBrush(Color.Black),
                                startX + offsetXLimite + (offsetXpequeña * 2) + 5, startY + offset);
            Graphic.DrawString("IEPS", fontPequeñaBold, new SolidBrush(Color.Black),
                                startX + offsetXLimite + (offsetXpequeña * 14), startY + offset);
            //Graphic.DrawString("Precio Neto", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString("Importe", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXLimite + (offsetXmedio * 4) + (offsetXpequeña * 3) + 5, startY + offset);

            inicioY = startY + offset + (int)fontPequeñaHeight + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            Graphic.DrawLine(pen, startX - 60, startY + offset + 2, offsetXLimite + 418, startY + offset + 2);
            offset = offset + (int)fontPequeñaHeight + 2;
            foreach (EntProducto p in ListaProductos)
            {
                Graphic.DrawString(p.Cantidad.ToString(), fontMedia, new SolidBrush(Color.Black), startX - offsetXpequeña - offsetXpequeña - offsetXpequeña, startY + offset);
                Graphic.DrawString(FormatoMoney(p.PrecioVentaSinIVA), fontMedia, new SolidBrush(Color.Black), startX + offsetXLimite + (offsetXpequeña * 2) + 5, startY + offset);
                Graphic.DrawString(FormatoMoney(p.IEPS), fontMedia, new SolidBrush(Color.Black), startX + offsetXLimite + (offsetXpequeña * 13), startY + offset);
                //Graphic.DrawString(FormatoMoney(p.PrecioVenta), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio -offsetXpequeña, startY + offset);
                Graphic.DrawString(FormatoMoney(p.PrecioSinIVA), fontMedia, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXmedio + offsetXpequeña + offsetXpequeña, startY + offset);

                Graphic.DrawString(p.Codigo.PadLeft(3, '0'), fontMedia, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
                offset = EscribeRenglonesDescripciones(p.Descripcion, Graphic, fontMedia, (int)fontHeight, pen, startX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXmedio, startY + offset, offset + 8, longituRenglonDescripcionProducto);
                //Graphic.DrawLine(pen, startX, startY + offset, startX + endX, startY + offset);
            }
            //LINEA 1 VERTICAL
            Graphic.DrawLine(pen, startX - 60, iniciaTabla, startX - 60, startY + offset + 10);
            //LINEA 2 VERTICAL
            Graphic.DrawLine(pen, startX + offsetXpequeña + offsetXpequeña, iniciaTabla, startX + offsetXpequeña + offsetXpequeña, startY + offset + 10);
            //LINEA 3 VERTICAL
            Graphic.DrawLine(pen, startX + offsetXpequeña + offsetXmedio + offsetXmedio - 10, iniciaTabla,
                                  startX + offsetXpequeña + offsetXmedio + offsetXmedio - 10, startY + offset + 10);
            //LINEA 4 VERTICAL
            Graphic.DrawLine(pen, startX + offsetXLimite + offsetXpequeña, iniciaTabla,
            startX + offsetXLimite + offsetXpequeña, startY + offset + 10);
            //LINEA 5 VERTICAL
            Graphic.DrawLine(pen, startX + offsetXLimite + (offsetXpequeña * 11), iniciaTabla,
            startX + offsetXLimite + (offsetXpequeña * 11), startY + offset + 10);
            //PENULTIMA LINEA VERTICAL
            Graphic.DrawLine(pen,
                startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio - offsetXpequeña + 5,
                iniciaTabla,
                startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio - offsetXpequeña + 5,
                startY + offset + 10);
            Graphic.DrawLine(pen, offsetXLimite + offsetXLimite + offsetXpequeña + 8,
                                iniciaTabla,
                                offsetXLimite + offsetXLimite + offsetXpequeña + 8,
                                startY + offset + 10);//ULTIMA LINEA VERTICAL

            offset = offset + (int)fontPequeñaHeight + 2;
            Graphic.DrawLine(pen, startX - offsetXpequeña - offsetXpequeña - offsetXpequeña - offsetXpequeña - offsetXpequeña - 10,
                startY + offset - 5, offsetXLimite + 418, startY + offset - 5);//ULTIMA LINEA HORIZONTAL
            offset = offset + (int)fontPequeñaHeight;

            Graphic.DrawString("SUBTOTAL", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString(FormatoMoney(Pedido.SubTotal), fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            offset = offset + (int)fontHeight + 2;
            Graphic.DrawString("IVA " + TasaIVA.ToString("0.00%"), fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString(FormatoMoney(Pedido.IVA), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            offset = offset + (int)fontHeight + 2;
            Graphic.DrawString("IEPS ", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString(FormatoMoney(Pedido.IEPS), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            offset = offset + (int)fontHeight + 2;

            //if (Pedido.TipoMonedaId == 2)
            //{
            //    Graphic.DrawString("TOTAL (USD)", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);

            //    Graphic.DrawString(FormatoMoney(Pedido.Total), fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //}
            //else
            //{
            Graphic.DrawString("TOTAL", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString(FormatoMoney(Pedido.Total), fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //}
            offset = offset + (int)fontHeight;
            if (Pedido.TrabajadorId > 0)
            {
                Graphic.DrawString("Vendedor: "+ Pedido.Trabajador, fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
                offset = offset + (int)fontHeight;
            }
            if (!string.IsNullOrWhiteSpace(Pedido.Observaciones))
            {
                Graphic.DrawString("Observaciones: ", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
                if (Pedido.Observaciones.Split('\n').Length > 0)
                {
                    foreach (string d in Pedido.Observaciones.Split('\n'))
                        offset = EscribeRenglonesDescripciones(d, Graphic, fontPequeña, (int)fontHeight, pen, startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset, offset + 2,
                                                                longituRenglonDescripcionProducto);
                }
                else
                    offset = EscribeRenglonesDescripciones(Pedido.Observaciones, Graphic, fontPequeña, (int)fontHeight, pen, startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset, offset + 2,
                                                            longituRenglonDescripcionProducto);
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
        
        public void ImprimirSalida(EntEmpresa Empresa, EntCliente Cliente, EntPedido Pedido, 
                                    List<EntProducto> ListaProductos,
                                    decimal TasaIVA,
                                    Image LogoEmpresa, Image Leyenda, Image Firma, Graphics Graphic, string TipoImpresion="SALIDA")
        {
            System.Drawing.Font fontBoldTitulo = new System.Drawing.Font("Microsoft Tai Le", 14, FontStyle.Bold);
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

            Graphic.DrawImage(LogoEmpresa, offsetXmedio *(3), startY - 100);      
            Graphic.DrawString(" - "+TipoImpresion+" - ", fontBold, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio *(4) - offsetXpequeña*(3), startY);
            Graphic.DrawString(Pedido.NumOrden, font, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio *(4) - offsetXpequeña * (1), startY + offset);

            offset = offset + (int)fontPequeñaHeight + 4;
            offset = offset + (int)fontPequeñaHeight + 2; 
            Graphic.DrawString("Fecha", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña, startY + offset);

            offset = offset + (int)fontPequeñaHeight + 2; 
            Graphic.DrawString(DateTime.Today.ToShortDateString(), fontPequeña, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);

      
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
          
            Graphic.DrawString(Empresa.Nombre, fontBoldTitulo, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXmedio, startY + offset - 5);
            //Graphic.DrawString("R.F.C:"
            Graphic.DrawString("", fontBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString(Empresa.RFC, fontPequeña, new SolidBrush(Color.Black), startX + offsetXpequeña *(5) + offsetXmedio *(4), startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;

            //DIRECCION
            Graphic.DrawString(Empresa.Direccion, fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            //offset = offset + (int)fontPequeñaHeight + 4;
         
            //CIUDAD
            Graphic.DrawString("" + Empresa.CP + ". " + Empresa.Localidad + ", " + Empresa.Estado, fontPequeña, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;

            //Graphic.DrawString("TEL:"
            Graphic.DrawString("", fontBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            Graphic.DrawString(Empresa.Telefono, fontPequeña, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXpequeña, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;


              Graphic.DrawString("Datos del Cliente", fontBold, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 8;
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

            int iniciaTabla = startY + offset - 5;
            Graphic.DrawLine(pen, startX - 60, startY + offset - 5, offsetXLimite + 500, startY + offset - 5);

            Graphic.DrawString("Cantidad", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña - offsetXmedio, startY + offset);
            Graphic.DrawString("Código", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString("Descricpción", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXmedio + offsetXmedio, startY + offset);
            //Graphic.DrawString("Unidad", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXmedio + offsetXLimite - offsetXpequeña, startY + offset);
            Graphic.DrawString("Precio Costo", fontPequeñaBold, new SolidBrush(Color.Black),
                                startX + offsetXLimite + offsetXpequeña * (1), startY + offset);
            Graphic.DrawString("Precio Unitario", fontPequeñaBold, new SolidBrush(Color.Black),
                                startX + offsetXLimite + ((offsetXpequeña) * 12), startY + offset);
            //Graphic.DrawString("Precio Neto", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString("Importe", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXmedio + offsetXpequeña, startY + offset);

            inicioY = startY + offset + (int)fontPequeñaHeight + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            Graphic.DrawLine(pen, startX - 60, startY + offset + 2, offsetXLimite + 500, startY + offset + 2);
            offset = offset + (int)fontPequeñaHeight + 2;
            foreach (EntProducto p in ListaProductos)
            {
                Graphic.DrawString(p.Cantidad.ToString(), fontPequeña, new SolidBrush(Color.Black), startX - offsetXpequeña - offsetXpequeña - offsetXpequeña, startY + offset);
                Graphic.DrawString(FormatoMoney(p.PrecioCosto), fontPequeña, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXpequeña*(2), startY + offset);
                Graphic.DrawString(FormatoMoney(p.PrecioVenta), fontPequeña, new SolidBrush(Color.Black), startX + offsetXLimite + (offsetXmedio*(3)), startY + offset);
                //Graphic.DrawString(FormatoMoney(p.PrecioVenta), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio -offsetXpequeña, startY + offset);
                Graphic.DrawString(FormatoMoney(p.Precio), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXmedio + offsetXpequeña + offsetXpequeña, startY + offset);

                Graphic.DrawString(p.Codigo, fontMini, new SolidBrush(Color.Black), startX + offsetXpequeña *(4), startY + offset);
                offset = EscribeRenglonesDescripciones(p.Descripcion, Graphic, fontPequeña, (int)fontHeight, pen, startX + offsetXmedio + offsetXpequeña + offsetXmedio, startY + offset, offset + 2, longituRenglonDescripcionProducto);
                //Graphic.DrawLine(pen, startX, startY + offset, startX + endX, startY + offset);
            }
            Graphic.DrawLine(pen, startX - 60, iniciaTabla, startX - 60, startY + offset + 10);
            Graphic.DrawLine(pen, startX + offsetXpequeña + offsetXpequeña, iniciaTabla, startX + offsetXpequeña + offsetXpequeña, startY + offset + 10);
            Graphic.DrawLine(pen, startX + offsetXpequeña + offsetXmedio + offsetXmedio - 10, iniciaTabla,
                                  startX + offsetXpequeña + offsetXmedio + offsetXmedio - 10, startY + offset + 10);
            Graphic.DrawLine(pen, startX + offsetXLimite , iniciaTabla,
                                  startX + offsetXLimite , startY + offset + 10);
            Graphic.DrawLine(pen, startX + offsetXmedio + offsetXLimite + offsetXmedio , iniciaTabla,
                startX + offsetXmedio + offsetXLimite + offsetXmedio , startY + offset + 10);
            Graphic.DrawLine(pen, offsetXLimite + offsetXLimite + offsetXpequeña + 8,
                                iniciaTabla,
                                offsetXLimite + offsetXLimite + offsetXpequeña + 8,
                                startY + offset + 10);

            offset = offset + (int)fontPequeñaHeight + 2;
            Graphic.DrawLine(pen, startX - offsetXpequeña - offsetXpequeña - offsetXpequeña - offsetXpequeña - offsetXpequeña - 10,
                startY + offset - 5, offsetXLimite + 500, startY + offset - 5);
            offset = offset + (int)fontPequeñaHeight;

            Graphic.DrawString("SUBTOTAL", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString(FormatoMoney(Pedido.SubTotal), fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            offset = offset + (int)fontHeight + 2;
            Graphic.DrawString("IVA " + TasaIVA.ToString("0.00%"), fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString(FormatoMoney(Pedido.IVA), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            offset = offset + (int)fontHeight + 2;
            Graphic.DrawString("IEPS " + 0.08.ToString("0.00%"), fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString(FormatoMoney(Pedido.IEPS), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            offset = offset + (int)fontHeight + 2;

            //if (Pedido.TipoMonedaId == 2)
            //{
            //    Graphic.DrawString("TOTAL (USD)", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);

            //    Graphic.DrawString(FormatoMoney(Pedido.Total), fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //}
            //else
            //{
            Graphic.DrawString("TOTAL", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
                Graphic.DrawString(FormatoMoney(Pedido.Total), fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //}
            offset = offset + (int)fontHeight;

            if (!string.IsNullOrWhiteSpace(Pedido.Observaciones))
            {
                Graphic.DrawString("Observaciones: ", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
                if (Pedido.Observaciones.Split('\n').Length > 0)
                {
                    foreach (string d in Pedido.Observaciones.Split('\n'))
                        offset = EscribeRenglonesDescripciones(d, Graphic, fontPequeña, (int)fontHeight, pen, startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset, offset + 2,
                                                                longituRenglonDescripcionProducto);
                }
                else
                    offset = EscribeRenglonesDescripciones(Pedido.Observaciones, Graphic, fontPequeña, (int)fontHeight, pen, startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset, offset + 2,
                                                            longituRenglonDescripcionProducto);
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
        public void ImprimirEntrada(EntEmpresa Empresa, EntCliente Proveedor, EntPedido Pedido,
                                    List<EntProducto> ListaProductos,
                                    decimal TasaIVA,
                                    Image LogoEmpresa, Image Leyenda, Image Firma, Graphics Graphic)
        {
            System.Drawing.Font fontBoldTitulo = new System.Drawing.Font("Microsoft Tai Le", 14, FontStyle.Bold);
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

            Graphic.DrawImage(LogoEmpresa, offsetXmedio*(3), startY - 100);
            //Graphic.DrawString(Empresa.Nombre, fontBoldTitulo, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXmedio, startY + offset - 5);
            Graphic.DrawString(" - ENTRADA - ", fontBold, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio - offsetXpequeña, startY -20);
            Graphic.DrawString(Pedido.NumOrden, font, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY-20 + offset);

            offset = offset + (int)fontPequeñaHeight + 4;
            offset = offset + (int)fontPequeñaHeight + 4;
           
            Graphic.DrawString("", fontBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;
            offset = offset + (int)fontPequeñaHeight + 4;
            offset = offset + (int)fontPequeñaHeight + 4;
            offset = offset + (int)fontPequeñaHeight + 4;
            //offset = offset + (int)fontPequeñaHeight + 4;

            //Graphic.DrawString(Empresa.RFC, fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio *(5) - 5, startY + offset);
            //offset = offset + (int)fontPequeñaHeight + 4;
            //DIRECCION
            Graphic.DrawString(Empresa.Direccion, fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            //CIUDAD
            //Graphic.DrawString("      CP: " 
            //Graphic.DrawString("" + Empresa.CP + ". " + Empresa.Localidad + ", " + Empresa.Estado, fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            //offset = offset + (int)fontPequeñaHeight + 4;

            //Graphic.DrawString("TEL:"
            Graphic.DrawString("", fontBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            //Graphic.DrawString(Empresa.Telefono, fontPequeña, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXpequeña, startY + offset);
            //offset = offset + (int)fontPequeñaHeight + 4;

           
            Graphic.DrawString("Datos del Proveedor", fontBold, new SolidBrush(Color.Black), startX, startY + offset);
            Graphic.DrawString("Fecha", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString(DateTime.Today.ToShortDateString(), fontPequeña, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXmedio, startY + offset);
            //offset = offset + (int)fontPequeñaHeight + 8;
            Graphic.DrawString("Proveedor:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            if (Proveedor.Nombre == null)
                Proveedor.Nombre = "";
            if (Proveedor.Nombre.Split('-').Length > 2)
                Graphic.DrawString(Proveedor.Nombre.Split('-')[0], font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset);
            else
                Graphic.DrawString(Proveedor.Nombre, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("Razon Social:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(Proveedor.NombreFiscal, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("R.F.C:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(Proveedor.RFC, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset + 1);
            offset = offset + (int)fontPequeñaHeight + 4;
            Graphic.DrawString("Dirección:", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
            Graphic.DrawString(Proveedor.Direccion, font, new SolidBrush(Color.Black), startX + offsetXmedio, startY + offset + 1);

            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;

            int iniciaTabla = startY + offset - 5;
            Graphic.DrawLine(pen, startX - 60, startY + offset - 5, offsetXLimite + 500, startY + offset - 5);

            Graphic.DrawString("Cantidad", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña - offsetXmedio, startY + offset);
            Graphic.DrawString("Código", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString("Descricpción", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXmedio + offsetXmedio, startY + offset);
            //Graphic.DrawString("Unidad", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXpequeña + offsetXmedio + offsetXLimite - offsetXpequeña, startY + offset);
            Graphic.DrawString("Precio Unitario", fontPequeñaBold, new SolidBrush(Color.Black),
                                startX + offsetXLimite + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //Graphic.DrawString("Precio Neto", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString("Importe", fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXmedio + offsetXpequeña, startY + offset);

            inicioY = startY + offset + (int)fontPequeñaHeight + (int)fontPequeñaHeight + 2;
            offset = offset + (int)fontPequeñaHeight + 2;
            Graphic.DrawLine(pen, startX - 60, startY + offset + 2, offsetXLimite + 500, startY + offset + 2);
            offset = offset + (int)fontPequeñaHeight + 2;
            foreach (EntProducto p in ListaProductos)
            {
                Graphic.DrawString(p.Cantidad.ToString(), fontPequeña, new SolidBrush(Color.Black), startX - offsetXpequeña - offsetXpequeña - offsetXpequeña, startY + offset);
                Graphic.DrawString(FormatoMoney(p.PrecioCosto), fontPequeña, new SolidBrush(Color.Black), startX + offsetXLimite + offsetXmedio, startY + offset);
                //Graphic.DrawString(FormatoMoney(p.PrecioVenta), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio -offsetXpequeña, startY + offset);
                Graphic.DrawString(FormatoMoney(p.PrecioC), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXmedio + offsetXpequeña + offsetXpequeña, startY + offset);

                Graphic.DrawString(p.Codigo, fontMini, new SolidBrush(Color.Black), startX + offsetXpequeña *(4), startY + offset);
                offset = EscribeRenglonesDescripciones(p.Descripcion, Graphic, fontPequeña, (int)fontHeight, pen, startX + offsetXmedio + offsetXpequeña + offsetXmedio, startY + offset, offset + 2, longituRenglonDescripcionProducto);
                //Graphic.DrawLine(pen, startX, startY + offset, startX + endX, startY + offset);
            }
            Graphic.DrawLine(pen, startX - 60, iniciaTabla, startX - 60, startY + offset + 10);
            Graphic.DrawLine(pen, startX + offsetXpequeña + offsetXpequeña, iniciaTabla, startX + offsetXpequeña + offsetXpequeña, startY + offset + 10);
            Graphic.DrawLine(pen, startX + offsetXpequeña + offsetXmedio + offsetXmedio - 10, iniciaTabla,
                                  startX + offsetXpequeña + offsetXmedio + offsetXmedio - 10, startY + offset + 10);
            Graphic.DrawLine(pen, startX + offsetXLimite + offsetXpequeña + offsetXpequeña, iniciaTabla,
                                  startX + offsetXLimite + offsetXpequeña + offsetXpequeña, startY + offset + 10);
            Graphic.DrawLine(pen, startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXmedio, iniciaTabla,
                startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXmedio, startY + offset + 10);
            Graphic.DrawLine(pen, offsetXLimite + offsetXLimite + offsetXpequeña + 8,
                                iniciaTabla,
                                offsetXLimite + offsetXLimite + offsetXpequeña + 8,
                                startY + offset + 10);

            offset = offset + (int)fontPequeñaHeight + 2;
            Graphic.DrawLine(pen, startX - offsetXpequeña - offsetXpequeña - offsetXpequeña - offsetXpequeña - offsetXpequeña - 10,
                startY + offset - 5, offsetXLimite + 500, startY + offset - 5);
            offset = offset + (int)fontPequeñaHeight;

            Graphic.DrawString("SUBTOTAL", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString(FormatoMoney(Pedido.SubTotal), fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            offset = offset + (int)fontHeight + 2;
            Graphic.DrawString("IVA " + TasaIVA.ToString("0.00%"), fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString(FormatoMoney(Pedido.IVA), fontPequeña, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            offset = offset + (int)fontHeight + 2;

            //if (Pedido.TipoMonedaId == 2)
            //{
            //    Graphic.DrawString("TOTAL (USD)", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);

            //    Graphic.DrawString(FormatoMoney(Pedido.Total), fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //}
            //else
            //{
            Graphic.DrawString("TOTAL", fontBold, new SolidBrush(Color.Black), startX + offsetX + offsetX + offsetX + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            Graphic.DrawString(FormatoMoney(Pedido.Total), fontPequeñaBold, new SolidBrush(Color.Black), startX + offsetXmedio + offsetXLimite + offsetXmedio + offsetXmedio + offsetXmedio + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset);
            //}
            offset = offset + (int)fontHeight;

            if (!string.IsNullOrWhiteSpace(Pedido.Observaciones))
            {
                Graphic.DrawString("Observaciones: ", fontPequeñaBold, new SolidBrush(Color.Black), startX - offsetXmedio, startY + offset);
                if (Pedido.Observaciones.Split('\n').Length > 0)
                {
                    foreach (string d in Pedido.Observaciones.Split('\n'))
                        offset = EscribeRenglonesDescripciones(d, Graphic, fontPequeña, (int)fontHeight, pen, startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset, offset + 2,
                                                                longituRenglonDescripcionProducto);
                }
                else
                    offset = EscribeRenglonesDescripciones(Pedido.Observaciones, Graphic, fontPequeña, (int)fontHeight, pen, startX + offsetXpequeña + offsetXpequeña + offsetXpequeña + offsetXpequeña, startY + offset, offset + 2,
                                                            longituRenglonDescripcionProducto);
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
