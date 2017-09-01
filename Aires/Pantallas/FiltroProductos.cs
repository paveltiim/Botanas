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
    public partial class FiltroProductos : Form
    {
        public FiltroProductos()
        {
            InitializeComponent();
        }
        public List<EntProducto> ListaProductos;
        bool SelectedFromGV = false;

        public EntProducto ProductoSeleccionado;
        //public bool EmpresasRentas = false;

        public void CargaProductosDetalle()
        {
            ListaProductos = new BusProductos().ObtieneProductosDetalle().OrderBy(P => P.Descripcion).ToList();
            gvProductosDetalle.DataSource = ListaProductos;
        }
        public void CargaProductosDetalle(List<EntProducto> ListaProductos)
        {
            this.ListaProductos = ListaProductos;
            //ListaProductos = new BusProductos().ObtieneProductosDetalle().OrderBy(P => P.Descripcion).ToList();
            gvProductosDetalle.DataSource = null;
            gvProductosDetalle.DataSource = ListaProductos;
        }

        private void FiltroProductos_Load(object sender, EventArgs e)
        {
            try
            {
                if (ListaProductos == null)
                    CargaProductosDetalle();

                txtFiltroSerieProducto.Focus();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void FiltroProductos_Activated(object sender, EventArgs e)
        {

            txtFiltroSerieProducto.Focus();
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFiltroCodigoProducto.Text)
                    && string.IsNullOrWhiteSpace(txtFiltroDescripcionProducto.Text)
                    && string.IsNullOrWhiteSpace(txtFiltroSerieProducto.Text))
                    gvProductosDetalle.DataSource = ListaProductos;
                else
                {
                    if(txtFiltroCodigoProducto.Focused)
                        gvProductosDetalle.DataSource = ListaProductos.Where(P => P.Codigo.ToUpper().Contains(txtFiltroCodigoProducto.Text.ToUpper())).ToList();
                    else if (txtFiltroDescripcionProducto.Focused)
                        gvProductosDetalle.DataSource = ListaProductos.Where(P => P.Descripcion.ToUpper().Contains(txtFiltroDescripcionProducto.Text.ToUpper())).ToList();
                    else if (txtFiltroSerieProducto.Focused)
                        gvProductosDetalle.DataSource = ListaProductos.Where(P => P.Serie.ToUpper().Contains(txtFiltroSerieProducto.Text.ToUpper())).ToList();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        
        }

        private void txtFiltroEmpresas_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                    btnSeleccionaEmpresa.PerformClick();
                else if (e.KeyChar == (char)Keys.Down)
                    gvProductosDetalle.Focus();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnSeleccionaEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvProductosDetalle.CurrentRow != null)
                {
                    int index = gvProductosDetalle.CurrentRow.Index;
                    if (SelectedFromGV && gvProductosDetalle.RowCount > 1 && gvProductosDetalle.CurrentRow.Index != gvProductosDetalle.RowCount - 1)
                        --index;
                    ProductoSeleccionado = ((List<EntProducto>)gvProductosDetalle.DataSource)[index];
                }
                else
                    this.DialogResult = DialogResult.Abort;
                //ListaProductos.Remove(ProductoSeleccionado);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void gvProductosDetalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                SelectedFromGV = true;
                btnSeleccionaEmpresa.PerformClick();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void gvProductosDetalle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnSeleccionaEmpresa.PerformClick();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        
        private void FiltroProductos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try {
                if(this.DialogResult==DialogResult.Abort)
                    e.Cancel = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
