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
    public partial class AgregaMotivoCancelacion : FormBase
    {
        public AgregaMotivoCancelacion()
        {
            InitializeComponent();
        }

        public AgregaMotivoCancelacion(List<AiresEntidades.EntPedido> PedidosFactura)
        {
            InitializeComponent();

            CargaDatosFacturas(PedidosFactura);
        }
        public AgregaMotivoCancelacion(EntFactura Factura, bool MuestraFormaFechaPago)
        {
            InitializeComponent();

            CargaDatosFactura(Factura);
            gbFormaFechaPago.Visible = MuestraFormaFechaPago;
        }

        void CargaDatosFactura(EntFactura Factura)
        {
            txtFactura.Text = Factura.NumeroFactura;
            txtTotal.Text = FormatoMoney(Factura.Total);
        }
        void CargaDatosFacturas(List<AiresEntidades.EntPedido> PedidosFactura)
        {
            foreach (AiresEntidades.EntPedido p in PedidosFactura)
            {
                txtFactura.Text += p.Factura + " | ";
            }
            txtFactura.Text = txtFactura.Text.Remove(txtFactura.Text.Length - 2);

            txtTotal.Text = FormatoMoney(PedidosFactura.Sum(P => P.Total));
        }

        public string MotivoCancelacion { get { return cmbMotivoCancelacion.Text.Remove(0, 4); } }
        public string MotivoCancelacionId { get { return cmbMotivoCancelacion.Text.Remove(2); } }
        public string FolioSustituye { get { return txtFolioSustitucion.Text; } }



        private void AgregaPago_Load(object sender, EventArgs e)
        {
            cmbMotivoCancelacion.SelectedIndex = 1;
        }

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

        private void AgregaPago_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool cierra = true;
            if (this.DialogResult == DialogResult.OK)
            {
                e.Cancel = !cierra;
            }
        }

        private void cmbMotivoCancelacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(cmbMotivoCancelacion.SelectedIndex==0)
            txtFolioSustitucion.ReadOnly = Convert.ToBoolean(cmbMotivoCancelacion.SelectedIndex);
        }
    }
}