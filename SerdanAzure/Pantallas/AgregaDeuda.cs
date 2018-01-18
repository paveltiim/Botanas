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
    public partial class AgregaDeuda : Form
    {
        public AgregaDeuda()
        {
            InitializeComponent();
        }

        public string Cantidad { get { return txtCantidad.Text; } }
        public string Descripcion { get { return txtDescripcion.Text; } }
        public DateTime Fecha { get { return dtpFecha.Value; } }

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
                this.GetNextControl((Control)sender, true).Focus();  
            }
        }

        private void AgregaDeuda_Load(object sender, EventArgs e)
        {

        }
    }
}
