using AiresEntidades;
using AiresNegocio;
using AiresUtilerias;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aires.Pantallas
{
    public partial class MuestraPagosNC : FormBase
    {
        public int ClienteId;
        //public List<EntPedido> ListaNotasCredito { get; set; }
        public MuestraPagosNC(int ClienteId)
        {
            InitializeComponent();

            this.ClienteId = ClienteId;
        }

        void CargaNotasCreditoPorCliente(int ClienteId)
        {
            List<EntPedido> lst = new BusPedidos().ObtieneNotasCreditoPedidos(ClienteId, ObtieneFechaInicial(), DateTime.Today);
            gvPagos.DataSource = lst;
            gvPagos.Refresh();
            EntPedidoBindingSource.DataSource = lst; //base.ConvierteListaPagosEnPedidos(lst);
            rvNotasCreditoClientes.RefreshReport();
        }


        private void MuestraPagos_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.ClienteId > 0)
                    CargaNotasCreditoPorCliente(this.ClienteId);
                //if (Program.UsuarioSeleccionado.TipoUsuarioId == (int)TiposUsuario.MASTER || this.UsuarioLogin.TipoUsuarioId == (int)TiposUsuario.CONTADOR)
                //    btnEliminaPago.Visible = true;
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido notaSeleccionado = ObtienePedidoFromGV(gvPagos);
                //if (!notaSeleccionado.Estatus)
                //    MandaExcepcion("LA NOTA DE CRÉDITO YA SE ENCUENTRA CANCELADA");

                if (MuestraMensajeYesNo(string.Format("¿Desea Cancelar Nota de Crédito Seleccionada? \n\n Nota Crédito: {0} \n Fecha: {1} \n Cantidad: {2}", 
                    notaSeleccionado.NumOrden, 
                    notaSeleccionado.FechaPago.ToShortDateString(), 
                    FormatoMoney(notaSeleccionado.Pago)), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    EntFactura factuaNC = new BusFacturas().ObtieneNotaCredito(notaSeleccionado.Id);

                    Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(factuaNC.EmpresaId);

                    UtiFacturacion facturar = new UtiFacturacion();
                    UtiFacturacionPruebas facturaPruebas = new UtiFacturacionPruebas();
                    if (Program.EmpresaSeleccionada.Facturacion)
                        facturar.CancelarPADE(Program.EmpresaSeleccionada, factuaNC.UUID, "02", "",notaSeleccionado.ClienteRFC, notaSeleccionado.Total);
                    //facturar.Cancelar(Program.EmpresaSeleccionada, factuaNC.UUID, "02", "");
                    else
                    {
                        MuestraMensaje("CANCELACIÓN PRUEBA");
                        facturaPruebas.Cancelar(Program.EmpresaSeleccionada, factuaNC.UUID, "02", "");
                    }

                    //facturasEncontradas[0].Id = notaSeleccionado.Id;
                    factuaNC.Estatus = false;
                    new BusFacturas().ActualizaEstatusNotaCredito(factuaNC, Program.UsuarioSeleccionado.Id);
                    //new BusPedidos().AumentaNotaCreditoPedido(facturasEncontradas[0].PedidoId, -facturasEncontradas[0].Total, this.UsuarioLogin.Id);
                    new BusPedidos().AumentaNotaCreditoPedido(notaSeleccionado.PedidoRelacionadoId, -notaSeleccionado.NotasCredito, Program.UsuarioSeleccionado.Id);
                    if (this.ClienteId > 0)
                        CargaNotasCreditoPorCliente(ClienteId);
                    else
                    {
                        MuestraMensaje("¡Nota de Crédito Cancelada!", "CANCELACIÓN Nota de Crédito");
                        this.Close();
                    }
                   
                        MuestraMensaje("¡Nota de Crédito Cancelada!", "CANCELACIÓN Nota de Crédito");
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
            finally { SetDefaultCursor(); }
        }

        private void btnActualizaPago_Click(object sender, EventArgs e)
        {
            try
            {
                if (MuestraMensajeYesNo("¿Desea guardar los cambios realizados?") == DialogResult.Yes)
                {
                    List<EntPago> pagosSeleccionados = ObtieneListaPagosFromGV(gvPagos);
                    foreach (EntPago p in pagosSeleccionados)
                    {
                        new BusPedidos().ActualizaPago(p);
                    }
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        void ReAsignaArchivosComplementoPago(EntFactura Factura)
        {
            if (Factura.XML.Length == new byte[1].Length)
            {
                string rutaArchivoXml = SeleccionaArchivoConFiltro(".xml");
                if (!string.IsNullOrWhiteSpace(rutaArchivoXml))
                {
                    Factura.XML = System.IO.File.ReadAllBytes(rutaArchivoXml);
                    if (!string.IsNullOrWhiteSpace(EncuentraArchivo(Factura.Ruta, ".xml"))) //ELIMINA ARCHIVO "VACIO" o con ERROR.
                        File.Delete(Factura.Ruta + "\\" + EncuentraArchivo(Factura.Ruta, ".xml"));
                }
                else
                    return;
            }

            if (Factura.PDF.Length == new byte[1].Length)
            {
                string rutaArchivoPDF = SeleccionaArchivoConFiltro(".pdf");
                if (!string.IsNullOrWhiteSpace(rutaArchivoPDF))
                {
                    Factura.PDF = System.IO.File.ReadAllBytes(rutaArchivoPDF);
                    if (!string.IsNullOrWhiteSpace(EncuentraArchivo(Factura.Ruta, ".pdf")))
                        File.Delete(Factura.Ruta + "\\" + EncuentraArchivo(Factura.Ruta, ".pdf"));
                }
                else
                    return;
            }

            //Factura.Ruta = this.PathFacturas + "\\" + Factura.Nombre + "\\CP" + Factura.NumeroComplemento + " " + Factura.NumeroFactura;
            new BusFacturas().ActualizaPDFXMLEnNotaCredito(Factura);

            //VerificaExistenArchivosFactura(Factura.Ruta, Factura.SerieFactura + Factura.NumeroFactura + " FAC-" + Factura.Factura, Factura.PDF, Factura.XML);
            //MuestraArchivo(facturaEncontrada.Ruta);
        }
        bool VerificaReAsignarFactura(EntFactura Factura)
        {
            if (string.IsNullOrWhiteSpace(Factura.Ruta) || Factura.PDF.Length == new byte[1].Length || Factura.XML.Length == new byte[1].Length)
            // || pedidoSeleccionado.RutaFactura == null)
            {
                if (MuestraMensajeYesNo("NO SE ENCONTRARON ARCHIVOS PARA MOSTRAR. \n ¿Desea asignar archivos PDF y XML?") == DialogResult.Yes)
                {
                    if (string.IsNullOrWhiteSpace(Factura.Ruta))
                    {
                        Factura.Ruta = base.PathFacturas + "\\" + Factura.Nombre;
                        if (!System.IO.Directory.Exists(Factura.Ruta))
                            System.IO.Directory.CreateDirectory(Factura.Ruta);
                    }
                    ReAsignaArchivosComplementoPago(Factura);
                }
                else
                    return false;
            }
            return true;
        }
        private void gvPagos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == gvPagos.Columns.Count - 1)
                    {
                        EntPedido pedidoNC = ObtienePedidoFromGV(gvPagos);
                
                        //List<EntFactura> facturasEncontradas = new BusFacturas().ObtieneNotaCredito(nota.Id);
                        //if (facturasEncontradas.Count > 0)
                        //{
                            EntFactura facturaNC = new BusFacturas().ObtieneNotaCredito(pedidoNC.Id);
                            if (VerificaReAsignarFactura(facturaNC))
                            {
                                VerificaExistenArchivosFactura(facturaNC.Ruta,
                                                               facturaNC.SerieFactura + facturaNC.NumeroFactura + " FAC-" + pedidoNC.Factura,
                                                               facturaNC.PDF,
                                                               facturaNC.XML);
                                MuestraArchivo(facturaNC.Ruta, EncuentraArchivo(facturaNC.Ruta, ".pdf"));
                            }
                            //VerificaExistenArchivosFactura(facturasEncontradas[0].Ruta, facturasEncontradas[0].UUID,
                            //                                facturasEncontradas[0].PDF,
                            //                                facturasEncontradas[0].XML);
                            //MuestraArchivo(facturasEncontradas[0].Ruta);
                        //}
                        //else
                        //    MuestraMensajeError("NO SE ENCONTRÓ ARCHIVO A MOSTRAR");
                    }
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvPagos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                    {
                        gvPagos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderBy(P => P.FechaPago).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        gvPagos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderByDescending(P => P.FechaPago).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    //if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                    //{
                    //    gvPagos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderBy(P => P.Cliente).ToList();
                    //    ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    //}
                    //else
                    //{
                    //    gvPagos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderByDescending(P => P.Cliente).ToList();
                    //    ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    //}
                }
                else if (e.ColumnIndex == 2)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                    {
                        gvPagos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderBy(P => P.Fecha).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        gvPagos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderByDescending(P => P.Fecha).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                }

                else if (e.ColumnIndex == 3)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                    {
                        gvPagos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderBy(P => P.Factura).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        gvPagos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderByDescending(P => P.Factura).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                }
                else if (e.ColumnIndex == 4)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                    {
                        gvPagos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderBy(P => P.Descripcion).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        gvPagos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderByDescending(P => P.Descripcion).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtFactura_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
