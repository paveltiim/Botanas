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
    public partial class AgregaObservacion : Form
    {
        public AgregaObservacion()
        {
            InitializeComponent();
        }

        public string Observacion { get { return txtObservaciones.Text; } }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        
        private void AgregaPagoGasto_Load(object sender, EventArgs e)
        {
            txtObservaciones.Focus();
        }

        private void txtObservaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                btnAgregar.PerformClick();
            }
        }
    }
}
