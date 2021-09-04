using AiresEntidades;
using AiresNegocio;
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
    public partial class MuestraPagos : FormBase
    {
        public int ClienteId;
        public MuestraPagos(int ClienteId)
        {
            InitializeComponent();

            this.ClienteId = ClienteId;
        }
        List<EntPago> ListaPagos = new List<EntPago>();
        void CargaPagosPorCliente(int ClienteId)
        {
            //if (Program.IsTiim)
            //    gvPagos.DataSource = new BusPedidos().ObtienePagosPorClienteNube(ClienteId, "AA");
            //else
            this.ListaPagos = new BusPedidos().ObtienePagosPorCliente(ClienteId);
                gvPagos.DataSource = this.ListaPagos;
            gvPagos.Refresh();
        }
        void ActualizaEstatusPago(EntPago Pago, bool Estatus)
        {
            Pago.Estatus = Estatus;
            //if (this.IsTiim)
            //    new BusPedidos().ActualizaEstatusPagoNube(Pago);
            //else
                new BusPedidos().ActualizaEstatusPago(Pago);
        }
        public void AumentaPagoPedido(int PedidoId, decimal Pago)
        {
            EntPedido pedido = new EntPedido()
            {
                Id = PedidoId,
                Pago = Pago
            };
            //if (this.IsTiim)
            //    new BusPedidos().AumentaPagoEnPedidoNube(pedido);
            //else
                new BusPedidos().AumentaPagoEnPedido(pedido);
        }


        private void SeleccionaFactura_Load(object sender, EventArgs e)
        {
            try
            {
                CargaPagosPorCliente(ClienteId);
                if (Program.UsuarioSeleccionado.Administrador)
                    btnEliminaPago.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                EntPago pagoSeleccionado = ObtienePagoFromGV(gvPagos);
                if (MuestraMensajeYesNo(string.Format("¿Seguro desea ELIMINAR el pago seleccionado? \n Fecha Pago:{0} \n Cantidad Pago:{1}", pagoSeleccionado.FechaPago.ToShortDateString(), FormatoMoney(pagoSeleccionado.Cantidad)), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    ActualizaEstatusPago(pagoSeleccionado, false);
                    AumentaPagoPedido(pagoSeleccionado.PedidoId, pagoSeleccionado.Cantidad * -1);
                    CargaPagosPorCliente(ClienteId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gvPagos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MuestraMensajeYesNo("¿Desea guardar los cambios realizados?") == DialogResult.Yes)
                {
                    List<EntPago> pagosSeleccionados = ObtieneListaPagosFromGV(gvPagos);
                    foreach (EntPago p in pagosSeleccionados)
                    {
                        //if (this.IsTiim)
                        //    new BusPedidos().ActualizaPagoNube(p);
                        //else
                            new BusPedidos().ActualizaPago(p);
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
            try {
                gvPagos.DataSource = this.ListaPagos.Where(P => P.Factura.Contains(txtFactura.Text));
                gvPagos.Refresh();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
