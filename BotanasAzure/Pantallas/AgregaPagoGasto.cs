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
    public partial class AgregaPagoGasto : Form
    {
        public AgregaPagoGasto()
        {
            InitializeComponent();
        }

        public string CantidadPago { get { return txtCantidadPaga.Text; } }
        public DateTime FechaPago { get { return dtpFechaPagoGasto.Value; } }

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

        private void AgregaPagoGasto_Load(object sender, EventArgs e)
        {

        }
    }
}
