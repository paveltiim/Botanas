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
    public partial class SeleccionaFacturaProveedor : Form
    {
        public SeleccionaFacturaProveedor(List<AiresEntidades.EntProveedor> ProveedorGastos)
        {
            InitializeComponent();

            gvGastosProveedor.DataSource = ProveedorGastos;
        }

        public EntProveedor ObtieneProveedorFromGV(DataGridView GridViewProveedores)
        {
            if (GridViewProveedores.CurrentRow == null)
                return null;
            return (EntProveedor)((List<EntProveedor>)GridViewProveedores.DataSource)[GridViewProveedores.CurrentRow.Index];
        }

        public AiresEntidades.EntProveedor ProveedorGastoSeleccionado { get { return ObtieneProveedorFromGV(gvGastosProveedor); } }
        

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
            btnAgregar.PerformClick();
        }
    }
}
