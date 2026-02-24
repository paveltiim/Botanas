using AiresEntidades;
using AiresNegocio;
using AiresUtilerias;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Aires.Pantallas
{
    public partial class SeleccionaComplemento : FormBase
    {
        public int FacturaId;
        public EntFactura FacturaComplementoSeleccionada { get { return ObtieneFacturaFromGV(gvFacturasComplementos); } }
        public List<EntFactura> ListaFacturasComplemento;
        public EntPedido PedidoFactura;

        public SeleccionaComplemento(EntPedido PedidoFactura)
        {
            InitializeComponent();
            this.PedidoFactura = PedidoFactura;
        }
        public SeleccionaComplemento(List<EntFactura> ListaFacturasComplemento)
        {
            InitializeComponent();
            this.ListaFacturasComplemento = ListaFacturasComplemento;
        }

        public void CargaComplementos()
        {
            this.ListaFacturasComplemento = new BusFacturas().ObtieneComplementos(this.FacturaId, "", 0);
            //lst.Add(Pedido);
            gvFacturasComplementos.DataSource = this.ListaFacturasComplemento;
            gvFacturasComplementos.Refresh();
        }
        public void CargaComplementos(List<EntFactura> ListaFacturas)
        {
            //ListaPedidos = new BusPedidos().ObtienePedidosClientesPorCliente(ClienteId);
            //lst.Add(Pedido);
            gvFacturasComplementos.DataSource = ListaFacturas;
            gvFacturasComplementos.Refresh();
        }

        /// <summary>
        /// Actualiza el Pago que se encuentre en la Fecha y con la cantidad Pago solicitada dentro de los pagos del Pedido solicitado.
        /// </summary>
        /// <param name="PedidoId"></param>
        /// <param name="FechaPago"></param>
        /// <param name="Pago"></param>
        /// <param name="Estatus"></param>
        void ActualizaEstatusPago(int PedidoId, DateTime FechaPago, decimal Pago, bool Estatus)
        {
            new BusPedidos().ActualizaEstatusPago(PedidoId, FechaPago, Pago, Estatus);
        }


        private void SeleccionaComplemento_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.ListaFacturasComplemento == null)
                    CargaComplementos();
                else
                    CargaComplementos(this.ListaFacturasComplemento);


                DataGridView gv = gvFacturasComplementos;
                if (Program.UsuarioSeleccionado.Supervisor)                
                    gv.Columns["gvColumnPago"].ReadOnly = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gvPagos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                    {
                        gvFacturasComplementos.DataSource = ((List<EntFactura>)((DataGridView)sender).DataSource).OrderBy(P => P.PedidoId).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        gvFacturasComplementos.DataSource = ((List<EntFactura>)((DataGridView)sender).DataSource).OrderByDescending(P => P.PedidoId).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                    {
                        gvFacturasComplementos.DataSource = ((List<EntFactura>)((DataGridView)sender).DataSource).OrderBy(P => P.Fecha).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        gvFacturasComplementos.DataSource = ((List<EntFactura>)((DataGridView)sender).DataSource).OrderByDescending(P => P.Fecha).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                }
                else if (e.ColumnIndex == 2)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                    {
                        gvFacturasComplementos.DataSource = ((List<EntFactura>)((DataGridView)sender).DataSource).OrderBy(P => P.Pago).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        gvFacturasComplementos.DataSource = ((List<EntFactura>)((DataGridView)sender).DataSource).OrderByDescending(P => P.Pago).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                }

                else if (e.ColumnIndex == 3)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                    {
                        gvFacturasComplementos.DataSource = ((List<EntFactura>)((DataGridView)sender).DataSource).OrderBy(P => P.Total).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        gvFacturasComplementos.DataSource = ((List<EntFactura>)((DataGridView)sender).DataSource).OrderByDescending(P => P.Total).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                }
                else if (e.ColumnIndex == 4)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                    {
                        gvFacturasComplementos.DataSource = ((List<EntFactura>)((DataGridView)sender).DataSource).OrderBy(P => P.NumeroFactura).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        gvFacturasComplementos.DataSource = ((List<EntFactura>)((DataGridView)sender).DataSource).OrderByDescending(P => P.NumeroFactura).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void gvPagos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnAgregar.PerformClick();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }


        private void gvFacturasComplementos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try {
                if (e.RowIndex >= 0)
                {
                    DataGridView gv = gvFacturasComplementos;
                    if (e.ColumnIndex == gv.Columns["gvColumnPago"].Index)
                    {
                        if (MuestraMensajeYesNo(string.Format("¿Desea guardar los cambios? "), "CONFIRMACIÓN") == DialogResult.Yes)
                        {
                            EntFactura complementoSeleccionado = ObtieneFacturaFromGV(gvFacturasComplementos);
                            new BusPedidos().CorrigePago(complementoSeleccionado.PagoId, complementoSeleccionado.PagoFactura, complementoSeleccionado.Fecha);
                        }
                    }
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            EntFactura facturaComplementoSeleccionada = new BusFacturas().ObtieneComplementoPago(this.FacturaComplementoSeleccionada.NumeroComplemento, Program.EmpresaSeleccionada.Id);
            facturaComplementoSeleccionada.Nombre = this.PedidoFactura.Cliente;
            facturaComplementoSeleccionada.NumeroFactura = this.FacturaComplementoSeleccionada.NumeroFactura;

            if (base.VerificaReAsignarFactura(facturaComplementoSeleccionada))
            {
                base.VerificaExistenArchivosFactura(facturaComplementoSeleccionada.Ruta,
                                               "CP" + facturaComplementoSeleccionada.NumeroComplemento + " FAC-" + facturaComplementoSeleccionada.NumeroFactura,
                                               facturaComplementoSeleccionada.PDF,
                                               facturaComplementoSeleccionada.XML);
                base.MuestraArchivo(facturaComplementoSeleccionada.Ruta, EncuentraArchivo(facturaComplementoSeleccionada.Ruta, ".pdf"));
            }
        }
        
        private void btnCancelaComplemento_Click(object sender, EventArgs e)
        {
            try
            {
                EntFactura complementoSeleccionado = ObtieneFacturaFromGV(gvFacturasComplementos);
                if (MuestraMensajeYesNo(string.Format("¿Seguro desea CANCELAR el Complemento seleccionado? \n UUID: {0}", complementoSeleccionado.UUID), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    base.SetWaitCursor();

                    base.TimbraCancelacion(new EntPedido() { UUID=complementoSeleccionado.UUID, ClienteId=this.PedidoFactura.ClienteId, ClienteRFC= this.PedidoFactura.ClienteRFC, Total=0});

                    //int numComplemento = ConvierteTextoAInteger(complementoSeleccionado.NumeroComplemento);
                    //ActualizaEstatusComplemento(numComplemento, false);
                    new BusFacturas().ActualizaEstatusComplementoPago(Program.EmpresaSeleccionada.Id, complementoSeleccionado.NumeroComplemento, false);

                    List<EntFactura> facturasPorComp = new BusFacturas().ObtieneFacturasPedidosPorComplemento(complementoSeleccionado.NumeroComplemento,Program.EmpresaSeleccionada.Id);

                    foreach (EntFactura f in facturasPorComp)
                    {
                        //SE BUSCA EL PAGO POR LA FECHA Y LA CANTIDAD DE PAGO
                        //ActualizaEstatusPago(PedidoFactura.Id, complementoSeleccionado.Fecha, complementoSeleccionado.Pago, false);
                        //new MuestraPagos(0).AumentaPagoPedido(PedidoFactura.Id, -complementoSeleccionado.Pago);
                        ActualizaEstatusPago(f.PedidoId, complementoSeleccionado.Fecha, f.PagoFactura, false);
                        //new MuestraPagos(0).AumentaPagoPedido(f.PedidoId, -f.Pago);
                        //new SalidasVentas().EliminaPago(f.PedidoId, pagoSeleccionado.Id, pagoSeleccionado.Cantidad * -1);
                        new BusPedidos().AumentaPagoPedido(f.PedidoId, -f.PagoFactura);

                    }

                    //CargaPedidos(Program.EmpresaSeleccionada.Id);
                    CargaComplementos();

                    MuestraMensaje("¡Complemento de Pago Cancelado!", "CANCELACIÓN CONFIRMADA");
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        EntFactura LeeCpXML(string RutaCarpeta)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(RutaCarpeta);
            if (dir.GetFiles().Length == 0)
                MandaExcepcion("NO se encuentran archivos en la carpeta seleccionada.");

            EntFactura CP40 = new EntFactura();

            foreach (System.IO.FileInfo file in dir.GetFiles())
            {
                string rutaArchivo = file.FullName;
                //if (file.Extension != ".xml")
                //    MuestraMensaje("El archivo no es el correcto. \n Debe ser archivo XML (.xml)");
                //else
                if (file.Extension == ".xml")
                {
                    EntFactura fac = new EntFactura();
                    EntFactura cp40 = new EntFactura();
                    EntFactura fac40ad = new EntFactura();
                    fac.Id = new Random().Next();
                    fac.NumeroFactura = "";
                    fac.SerieFactura = "";
                    fac.EmpresaId = Program.EmpresaSeleccionada.Id;
                    fac.UsuarioId = Program.UsuarioSeleccionado.Id;
                    fac.Ruta = "";
                    fac.PDF = new byte[1];
                    fac.XML = new byte[1];
                    fac.TipoCambio = 1;

                    XmlTextReader reader = new XmlTextReader(rutaArchivo);
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element: // The node is an element.
                                                      //Console.Write("<" + reader.Name);
                                if (reader.Name.ToUpper() == "cfdi:Comprobante".ToUpper())
                                {
                                    while (reader.MoveToNextAttribute())
                                    { // Read the attributes.
                                      //Console.Write(" " + reader.Name + "='" + reader.Value + "'");
                                        switch (reader.Name.ToUpper())
                                        {
                                            case "CERTIFICADO":
                                                fac.NumeroComplemento = reader.Value;
                                                break;
                                            case "VERSION":
                                                fac.VersionCFDI = reader.Value;
                                                //if (fac.VersionCFDI == "3.3")
                                                //    fac.VersionCFDIId = 1;
                                                //else if (fac.VersionCFDI == "4.0")
                                                //    fac.VersionCFDIId = 2;
                                                break;
                                            case "FECHA":
                                                fac.Fecha = Convert.ToDateTime(reader.Value);
                                                break;
                                            case "TOTAL":
                                                fac.Total = Convert.ToDecimal(reader.Value);
                                                break;
                                            case "DESCUENTO":
                                                fac.Descuento = Convert.ToDecimal(reader.Value);
                                                break;
                                            case "SUBTOTAL":
                                                fac.SubTotal = Convert.ToDecimal(reader.Value);
                                                break;
                                            case "SERIE":
                                                fac.SerieFactura = reader.Value;
                                                break;
                                            case "FOLIO":
                                                fac.NumeroFactura = reader.Value;
                                                break;
                                            case "MONEDA":
                                                //fac.Moneda = reader.Value.ToUpper();
                                                //if (reader.Value.ToUpper() == "USD")
                                                //    fac.MonedaId = 2;
                                                //else
                                                //    fac.MonedaId = 1;
                                                break;
                                            case "TIPOCAMBIO":
                                                fac.TipoCambio = ConvierteTextoADecimal(reader.Value);
                                                break;
                                            case "TIPODECOMPROBANTE":
                                                //fac.TipoComprobante = reader.Value;
                                                //switch (fac.TipoComprobante)
                                                //{
                                                //    case "I":
                                                //        fac.TipoComprobanteId = 1;
                                                //        break;
                                                //    case "E":
                                                //        fac.TipoComprobanteId = 2;
                                                //        break;
                                                //    case "T":
                                                //        fac.TipoComprobanteId = 3;
                                                //        break;
                                                //    case "N":
                                                //        fac.TipoComprobanteId = 4;
                                                //        break;
                                                //    case "P":
                                                //        fac.TipoComprobanteId = 5;
                                                //        break;
                                                //}
                                                break;

                                            case "FORMAPAGO":
                                                fac.FormaPago = reader.Value;
                                                switch (fac.FormaPago)
                                                {
                                                    case "01":
                                                        fac.FormaPagoId = 1;
                                                        break;
                                                    case "02":
                                                        fac.FormaPagoId = 2;
                                                        break;
                                                    case "03":
                                                        fac.FormaPagoId = 3;
                                                        break;
                                                    case "04":
                                                        fac.FormaPagoId = 4;
                                                        break;
                                                    case "05":
                                                        fac.FormaPagoId = 5;
                                                        break;
                                                    case "06":
                                                        fac.FormaPagoId = 6;
                                                        break;
                                                    case "08":
                                                        fac.FormaPagoId = 8;
                                                        break;
                                                    case "12":
                                                        fac.FormaPagoId = 12;
                                                        break;
                                                    case "13":
                                                        fac.FormaPagoId = 13;
                                                        break;
                                                    case "14":
                                                        fac.FormaPagoId = 14;
                                                        break;
                                                    case "15":
                                                        fac.FormaPagoId = 15;
                                                        break;
                                                    case "17":
                                                        fac.FormaPagoId = 17;
                                                        break;
                                                    case "23":
                                                        fac.FormaPagoId = 23;
                                                        break;
                                                    case "24":
                                                        fac.FormaPagoId = 24;
                                                        break;
                                                    case "25":
                                                        fac.FormaPagoId = 25;
                                                        break;
                                                    case "26":
                                                        fac.FormaPagoId = 26;
                                                        break;
                                                    case "27":
                                                        fac.FormaPagoId = 27;
                                                        break;
                                                    case "28":
                                                        fac.FormaPagoId = 28;
                                                        break;
                                                    case "29":
                                                        fac.FormaPagoId = 29;
                                                        break;
                                                    case "30":
                                                        fac.FormaPagoId = 30;
                                                        break;
                                                    case "99":
                                                        fac.FormaPagoId = 99;
                                                        break;
                                                }
                                                break;
                                            case "METODOPAGO":
                                                fac.MetodoPago = reader.Value;
                                                switch (fac.MetodoPago)
                                                {
                                                    case "PUE":
                                                        fac.MetodoPagoId = 1;
                                                        break;
                                                    case "PPD":
                                                        fac.MetodoPagoId = 2;
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                }

                                if (reader.Name.ToUpper() == "cfdi:Concepto".ToUpper())
                                {
                                    while (reader.MoveToNextAttribute())
                                    { // Read the attributes.
                                        Console.Write(" " + reader.Name + "='" + reader.Value + "'");
                                        switch (reader.Name.ToUpper())
                                        {
                                            case "DESCRIPCION":
                                                fac.Descripcion += reader.Value + "|";
                                                break;
                                        }
                                    }
                                }
                                
                                //TODOS CFDI'S
                                if (reader.Name.ToUpper() == "tfd:TimbreFiscalDigital".ToUpper())
                                {
                                    while (reader.MoveToNextAttribute())
                                    { // Read the attributes.
                                      //Console.Write(" " + reader.Name + "='" + reader.Value + "'");
                                        switch (reader.Name.ToUpper())
                                        {
                                            case "UUID":
                                                fac.UUID = reader.Value.ToUpper();
                                                break;
                                            case "SELLOCFD":
                                                fac.VersionCFDI = reader.Value.ToUpper();
                                                break;
                                            case "SELLOSAT":
                                                fac.Ruta = reader.Value.ToUpper();
                                                break;

                                        }
                                    }
                                }
                                //Console.WriteLine(">");
                                break;
                            case XmlNodeType.Text: //Display the text in each element.
                                                   //Console.WriteLine(reader.Value);
                                break;
                            case XmlNodeType.EndElement: //Display the end of the element.
                                                         //Console.Write("</" + reader.Name);
                                                         //Console.WriteLine(">");
                                break;
                        }
                    }

                    CP40 = fac;
                }
            }

            return CP40;
        }
        string [] LeeFacturasDescribeResumen(EntFactura ComplementoPago, List<EntFactura> ListaFacturas)
        {
            string[] resumen = new string[4];
            //DESCRIPCION RESUMEN FACTURAS PAGADAS
            StringBuilder sbFacturas = new StringBuilder();
            StringBuilder sbFacturasRelacionadas = new StringBuilder();
            foreach (EntFactura f in ListaFacturas)
            {
                //DESCRIPCION RESUMEN FACTURAS PAGADAS
                sbFacturas.Append("  " + f.SerieFactura + f.NumeroFactura
                    + "    |   $" + f.Saldo.ToString("0,0.00").PadRight(12)
                    + "|   $" + f.PagoFactura.ToString("0,0.00").PadRight(12)
                    + " |   $" + (f.Saldo - f.PagoFactura).ToString("0,0.00").PadRight(12)
                    + " |   " + f.UUID);
                sbFacturasRelacionadas.Append(f.SerieFactura + f.NumeroFactura + " | ");
            }

            string observacion = sbFacturasRelacionadas.ToString();
            string observacion2 = " FACTURA       DEUDA         PAGO        SALDO PENDIENTE       UUID";
            string observacion3 = sbFacturas.ToString();
            string observacion4 = "> FECHA PAGO: " + ComplementoPago.Fecha.Date.ToString("yyyy-MM-dd")
            + "         FORMA PAGO: " + ComplementoPago.FormaPagoId.ToString().PadLeft(2, '0')
            + "             CANTIDAD TOTAL PAGO: $" + ComplementoPago.Pago.ToString("0,0.00")
            + "             CANTIDAD FACTURAS: " + ListaFacturas.Count.ToString() + " <";
            
            resumen[0] = observacion;
            resumen[1] = observacion2;
            resumen[2] = observacion3;
            resumen[3] = observacion4;

            return resumen;
        }
        private void btnVerVersion2_Click(object sender, EventArgs e)
        {
            try
            {
                UtiPDF modiPDF = new UtiPDF();

                EntFactura complementoSeleccionado = new BusFacturas().ObtieneComplementoPago(ObtieneFacturaFromGV(gvFacturasComplementos).NumeroComplemento, Program.EmpresaSeleccionada.Id); //ObtieneFacturaFromGV(gvFacturasComplementos);
                complementoSeleccionado.Nombre = this.PedidoFactura.Cliente;
                complementoSeleccionado.NumeroFactura = this.FacturaComplementoSeleccionada.NumeroFactura;

                string rutaNeue = complementoSeleccionado.Ruta + "\\CPV2";
                int versionArchivo = CuentaArchivos(rutaNeue, ".pdf");

                if (versionArchivo > 0)
                    MuestraArchivo(rutaNeue +"\\"+ EncuentraArchivoOriginal(rutaNeue, ".pdf"), true);
                else
                {
                    if (base.VerificaReAsignarFactura(complementoSeleccionado))
                    {
                        base.VerificaExistenArchivosFactura(complementoSeleccionado.Ruta,
                                                       "CP" + complementoSeleccionado.NumeroComplemento + " FAC-" + complementoSeleccionado.NumeroFactura,
                                                       complementoSeleccionado.PDF,
                                                       complementoSeleccionado.XML);
                    }

                    //List<EntFactura> facturasPorComp = new BusFacturas().ObtieneFacturasPedidosPorComplemento(complementoSeleccionado.NumeroComplemento, Program.EmpresaSeleccionada.Id);
                    List<EntFactura> facturasPorComp = new BusFacturas().ObtieneFacturasPorComplemento(complementoSeleccionado.NumeroComplemento, Program.EmpresaSeleccionada.Id);

                    string[]resumen = LeeFacturasDescribeResumen(complementoSeleccionado, facturasPorComp);

                    EntFactura cpXML = LeeCpXML(complementoSeleccionado.Ruta);
                    string certificado = cpXML.NumeroComplemento;
                    string selloCFD = cpXML.VersionCFDI;
                    string selloSAT = cpXML.Ruta;
                    facturasPorComp[0].VersionCFDI = selloCFD;
                    facturasPorComp[0].Ruta = selloSAT;

                    string rutaArchivo = complementoSeleccionado.Ruta + "\\" + EncuentraArchivoOriginal(complementoSeleccionado.Ruta, ".pdf");
                     modiPDF.ModificaCpPDF(resumen[0], resumen[1], resumen[2], resumen[3],
                                rutaArchivo, "V2",
                                facturasPorComp, 40);

                    string rutaArchivoNeue = rutaNeue + "\\" + rutaArchivo.Replace(complementoSeleccionado.Ruta + "\\", "").Replace(".pdf", "") + "-V2.pdf";
                    MuestraArchivo(rutaArchivoNeue, true);

                    //MuestraMensaje("ARCHIVO CREADO");
                    //EliminaArchivosDireccion(rutaNeue);
                    //}
                }
                //this.DialogResult = DialogResult.Retry;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
    }
}
