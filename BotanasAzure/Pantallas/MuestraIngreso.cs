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
    public partial class MuestraIngreso : Form
    {
        public MuestraIngreso(EntCatalogoGenerico Ingreso)
        {
            InitializeComponent();

            CargaIngreso(Ingreso);
        }
        void CargaIngreso(EntCatalogoGenerico Ingreso)
        {
            txtCantidadPaga.Text = Ingreso.Descripcion;
            dtpFechaPagoGasto.Value = Ingreso.Fecha;
        }
        public string Descripcion { get { return txtCantidadPaga.Text; } }
        public DateTime Fecha { get { return dtpFechaPagoGasto.Value; } }
        
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
