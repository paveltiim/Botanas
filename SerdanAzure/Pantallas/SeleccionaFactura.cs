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
    public partial class SeleccionaFactura : Form
    {
        public SeleccionaFactura(List<AiresEntidades.EntPedido> FacturasPedido)
        {
            InitializeComponent();

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
        public List<EntPedido> FacturasPedidoSeleccionados { get { return ObtieneListaPedidosFromGV(gvFacturasPedido).Where(P => P.Estatus).ToList(); } }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void txtCantidadPaga_Leave(object sender, EventArgs e)
        {

        }

        private void txtCantidadPaga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                btnAgregar.PerformClick();
            }
        }

        private void SeleccionaFactura_Load(object sender, EventArgs e)
        {
            
        }

        private void gvGastosEmpresa_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //btnAgregar.PerformClick();
        }

        private void gvFacturasPedido_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ObtienePedidoFromGV(gvFacturasPedido).Estatus = !ObtienePedidoFromGV(gvFacturasPedido).Estatus;
                gvFacturasPedido.Refresh();
                //this.FacturasPedidoSeleccionados = ObtieneListaPedidosFromGV(gvFacturasPedido);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
