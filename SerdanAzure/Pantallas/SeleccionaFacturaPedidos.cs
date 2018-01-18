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
    public partial class SeleccionaFacturaPedidos : Form
    {
        public SeleccionaFacturaPedidos(List<AiresEntidades.EntEmpresa> EmpresaGastos)
        {
            InitializeComponent();

            gvGastosEmpresa.DataSource = EmpresaGastos;
        }

        public EntEmpresa ObtieneEmpresaFromGV(DataGridView GridViewEmpresas)
        {
            if (GridViewEmpresas.CurrentRow == null)
                return null;
            return (EntEmpresa)((List<EntEmpresa>)GridViewEmpresas.DataSource)[GridViewEmpresas.CurrentRow.Index];
        }

        public AiresEntidades.EntEmpresa EmpresaGastoSeleccionada { get { return ObtieneEmpresaFromGV(gvGastosEmpresa); } }
        

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
