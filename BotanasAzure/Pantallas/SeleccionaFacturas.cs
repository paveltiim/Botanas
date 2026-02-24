using AiresEntidades;
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
    public partial class SeleccionaFacturas : Form
    {
        List<EntPedido> PedidosFacturas;
        public SeleccionaFacturas(List<AiresEntidades.EntPedido> FacturasPedido)
        {
            InitializeComponent();
            this.PedidosFacturas = FacturasPedido;
            gvFacturasPedido.DataSource = FacturasPedido;
        }

        public EntPedido ObtienePedidoFromGV(DataGridView GridViewPedidos)
        {
            if (GridViewPedidos.CurrentRow == null)
                return null;
            return (EntPedido)((List<EntPedido>)GridViewPedidos.DataSource)[GridViewPedidos.CurrentRow.Index];
        }
        public List<EntPedido> ObtieneListaPedidosFromGV(DataGridView GridViewPedidos)
        {
            if (GridViewPedidos.DataSource == null)
                return new List<EntPedido>();
            return ((List<EntPedido>)GridViewPedidos.DataSource).ToList();
        }
        public List<EntPedido> ObtieneListaPedidosFromGV(DataGridView GridViewPedidos, bool Estatus)
        {
            if (GridViewPedidos.DataSource == null)
                return new List<EntPedido>();
            return ((List<EntPedido>)GridViewPedidos.DataSource).Where(P=>P.Estatus=Estatus).ToList();
        }
        public AiresEntidades.EntPedido FacturaPedidoSeleccionado { get { return ObtienePedidoFromGV(gvFacturasPedido); } }

        /// <summary>
        /// ObtieneListaPedidosFromGV(gvFacturasPedido).Where(P => P.Estatus).ToList(); 
        /// </summary>
        public List<EntPedido> FacturasPedidoSeleccionados { get {
                txtFacturaFiltro.Clear();
                return ObtieneListaPedidosFromGV(gvFacturasPedido).Where(P => P.Estatus).ToList(); } }
        public List<EntPedido> FacturasPedidosSeleccionados { get { return ObtieneListaPedidosFromGV(gvFacturasPedido).Where(P => P.Estatus).ToList(); } }

        string versionCFDI = "";


        private void SeleccionaFactura_Load(object sender, EventArgs e)
        {
            gvFacturasPedido.ClearSelection();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                versionCFDI = "";
                foreach (EntPedido p in FacturasPedidosSeleccionados)
                {
                    if (versionCFDI == "")
                        versionCFDI = p.VersionCFDI;
                    else if (versionCFDI != p.VersionCFDI)
                    {
                        this.DialogResult = DialogResult.Abort;
                        MessageBox.Show("POR REGLAMENTO DE SAT, NO SE PERMITE HACER COMPLEMENTO DE FACTURAS CON DIFERENTES VERSIONES DE CFDI");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void gvFacturasPedido_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (versionCFDI == "")
                    versionCFDI = ObtienePedidoFromGV(gvFacturasPedido).VersionCFDI;
                else if (versionCFDI != ObtienePedidoFromGV(gvFacturasPedido).VersionCFDI)
                    MessageBox.Show("POR REGLAMENTO DE SAT, NO SE PERMITE HACER COMPLEMENTO DE FACTURAS CON DIFERENTES VERSIONES DE CFDI");
                else
                {
                    ObtienePedidoFromGV(gvFacturasPedido).Estatus = !ObtienePedidoFromGV(gvFacturasPedido).Estatus;

                    if (this.FacturasPedidoSeleccionados.Count == 0)
                        this.versionCFDI = "";
                }
                gvFacturasPedido.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtFacturaFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                gvFacturasPedido.DataSource = this.PedidosFacturas.Where(P => P.Factura.ToUpper().Contains(txtFacturaFiltro.Text.ToUpper())).ToList();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void gvFacturasPedido_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //NO FUNCIONA AQUI
                //if (e.RowIndex >= 0)
                //{
                //    if (this.FacturasPedidoSeleccionados.Count == 0)
                //        this.versionCFDI = "";
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
