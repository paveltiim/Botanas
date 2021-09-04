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

namespace Aires.Pantallas
{
    public partial class SeleccionaComplemento : FormBase
    {

        public int FacturaId;
        public EntFactura FacturaComplementoSeleccionada { get { return ObtieneFacturaFromGV(gvFacturasComplementos); } }
        public List<EntFactura> ListaFacturasComplemento;
        public EntPedido PedidoFactura;

        public SeleccionaComplemento(int FacturaId)
        {
            InitializeComponent();
            this.FacturaId = FacturaId;
        }
        public SeleccionaComplemento(EntPedido PedidoFactura)
        {
            InitializeComponent();
            this.PedidoFactura = PedidoFactura;
        }

        public void CargaComplementos()
        {
            ListaFacturasComplemento = new BusFacturas().ObtieneComplementos(FacturaId, "", 0);
            //lst.Add(Pedido);
            gvFacturasComplementos.DataSource = ListaFacturasComplemento;
            gvFacturasComplementos.Refresh();
        }
        public void CargaComplementos(List<EntFactura> ListaFacturas)
        {
            //ListaPedidos = new BusPedidos().ObtienePedidosClientesPorCliente(ClienteId);
            //lst.Add(Pedido);
            gvFacturasComplementos.DataSource = ListaFacturas;
            gvFacturasComplementos.Refresh();
        }


        private void SeleccionaFactura_Load(object sender, EventArgs e)
        {
            try
            {
                if (ListaFacturasComplemento == null)
                    CargaComplementos();
                else
                    CargaComplementos(ListaFacturasComplemento);
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

        private void gvFacturasComplementos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        void ActualizaEstatusComplemento(int ComplementoId, bool Estatus)
        {
            EntFactura fac = new EntFactura()
            {
                Id = ComplementoId,
                Estatus = Estatus
            };
            new BusFacturas().ActualizaEstatusComplementoPago(fac);
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
        private void btnCancelaComplemento_Click(object sender, EventArgs e)
        {
            try
            {
                EntFactura complementoSeleccionado = ObtieneFacturaFromGV(gvFacturasComplementos);
                if (MuestraMensajeYesNo(string.Format("¿Seguro desea CANCELAR el Complemento seleccionado? \n UUID: {0}", complementoSeleccionado.UUID), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (Program.ConexionIdActual == 1)//PRODUCCION
                    {
                        UtiFacturacion factura = new UtiFacturacion();
                        factura.Cancelar(Program.EmpresaSeleccionada, complementoSeleccionado.UUID);
                    }
                    else if (Program.ConexionIdActual == 2)
                    {
                        MessageBox.Show("CANCELACIÓN PRUEBA");
                        UtiFacturacionPruebas facturaPruebas = new UtiFacturacionPruebas();
                        facturaPruebas.Cancelar(new EntEmpresa() { RFC = "LAN7008173R5" }, complementoSeleccionado.UUID);
                    }

                    ActualizaEstatusComplemento(complementoSeleccionado.Id, false);

                    List<EntFactura> facturasPorComp = new BusFacturas().ObtieneFacturasPorComplemento(complementoSeleccionado.Id.ToString());

                    foreach (EntFactura f in facturasPorComp)
                    {
                        //SE BUSCA EL PAGO POR LA FECHA Y LA CANTIDAD DE PAGO
                        //ActualizaEstatusPago(PedidoFactura.Id, complementoSeleccionado.Fecha, complementoSeleccionado.Pago, false);
                        //new MuestraPagos(0).AumentaPagoPedido(PedidoFactura.Id, -complementoSeleccionado.Pago);
                        ActualizaEstatusPago(f.PedidoId, complementoSeleccionado.Fecha, f.Pago, false);
                        new MuestraPagos(0).AumentaPagoPedido(f.PedidoId, -f.Pago);
                    }

                    //CargaPedidos(Program.EmpresaSeleccionada.Id);
                    CargaComplementos();

                    Cursor.Current = Cursors.Default;
                    MuestraMensaje("¡Complemento de Pago Cancelado!", "CANCELACIÓN CONFIRMADA");
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
    }
}
