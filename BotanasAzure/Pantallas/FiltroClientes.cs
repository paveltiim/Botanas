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
    public partial class FiltroClientes : Form
    {
        public FiltroClientes()
        {
            InitializeComponent();
        }
        public FiltroClientes(List<EntCliente> ListaClientes)
        {
            InitializeComponent();
            this.ListaClientes = ListaClientes;
        }

        List<EntCliente> ListaClientes;
        bool SelectedFromGV = false;

        public EntCliente ClienteSeleccionado;

        public void CargaClientes()
        {
            if (this.ListaClientes == null)
                this.ListaClientes = new BusClientes().ObtieneClientes(Program.EmpresaSeleccionada.Id).OrderBy(P => P.Nombre).ToList();
            gvClientes.DataSource = this.ListaClientes;
        }

        private void FiltroClientes_Load(object sender, EventArgs e)
        {
            CargaClientes();
            txtFiltroClientes.Focus();
        }

        private void btnBuscarClientes_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFiltroClientes.Text))
                    gvClientes.DataSource = ListaClientes;
                else
                    gvClientes.DataSource = ListaClientes.Where(P => P.Nombre.ToUpper().Contains(txtFiltroClientes.Text.ToUpper())).ToList();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        
        }

        private void txtFiltroClientes_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                    btnSeleccionaCliente.PerformClick();
                else if (e.KeyChar == (char)Keys.Down)
                    gvClientes.Focus();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnSeleccionaCliente_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvClientes.CurrentRow != null)
                {
                    int index = gvClientes.CurrentRow.Index;
                    if (SelectedFromGV && gvClientes.RowCount > 1 && gvClientes.CurrentRow.Index != gvClientes.RowCount - 1)
                        --index;
                    ClienteSeleccionado = ((List<EntCliente>)gvClientes.DataSource)[index];
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void gvClientes_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                SelectedFromGV = true;
                btnSeleccionaCliente.PerformClick();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void gvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnSeleccionaCliente.PerformClick();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void FiltroClientes_Activated(object sender, EventArgs e)
        {
            txtFiltroClientes.Focus();
        }
    }
}
