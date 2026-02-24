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
    public partial class AgregaDeposito : Form
    {
        public AgregaDeposito()
        {
            InitializeComponent();
        }

        public string CantidadPago { get { return txtCantidadPaga.Text; } set { txtCantidadPaga.Text = value; } }
        public DateTime FechaPago { get { return dtpFechaPagoGasto.Value; } }
        public bool CantidadEnabled { set { txtCantidadPaga.Enabled = value; } }

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

        private void AgregaDeposito_Load(object sender, EventArgs e)
        {

        }
    }
}
