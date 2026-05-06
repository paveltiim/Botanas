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
    public partial class AgregaPago : FormBase
    {
        private System.Windows.Forms.NumericUpDown numIEPSPorcentaje;

        public AgregaPago(EntPedido PedidoFactura)
        {
            InitializeComponent();
            InitIEPSControl();
            CargaDatosFactura(PedidoFactura);
        }
        public AgregaPago(EntPedido PedidoFactura, bool MuestraFechaFormaPago)
        {
            InitializeComponent();
            InitIEPSControl();
            CargaDatosFactura(PedidoFactura);
            pnlFechaFormaPago.Visible = MuestraFechaFormaPago;
        }

        /// <summary>
        /// Porcentaje del IEPS de la factura a incluir en el complemento de pago.
        /// 0 = automático (comportamiento previo). 100 = 100% del IEPS.
        /// </summary>
        public decimal IEPSManualPorcentaje { get { return numIEPSPorcentaje.Value / 100m; } }
        public string CantidadPago { get { return txtCantidadPaga.Text; } }
        public decimal CantidadPagoDecimal { get { return ConvierteTextoADecimal(txtCantidadPaga); } }
        public string FormaPago { get { return cmbFormaPago.Text.Remove(0, 4); } }
        public int FormaPagoId { get { return ConvierteTextoAInteger(cmbFormaPago.Text.Remove(2)); } }
        public DateTime FechaPago { get { return dtpFechaPago.Value; } }

        private void InitIEPSControl()
        {
            var lblIEPS = new System.Windows.Forms.Label();
            lblIEPS.AutoSize = true;
            lblIEPS.Location = new System.Drawing.Point(200, 110);
            lblIEPS.Text = "% IEPS (0=auto)";

            numIEPSPorcentaje = new System.Windows.Forms.NumericUpDown();
            numIEPSPorcentaje.Location = new System.Drawing.Point(315, 107);
            numIEPSPorcentaje.Minimum = 0;
            numIEPSPorcentaje.Maximum = 100;
            numIEPSPorcentaje.DecimalPlaces = 0;
            numIEPSPorcentaje.Value = 0;
            numIEPSPorcentaje.Size = new System.Drawing.Size(63, 25);
            numIEPSPorcentaje.TabIndex = 5;

            this.Controls.Add(lblIEPS);
            this.Controls.Add(numIEPSPorcentaje);
        }

        void CargaDatosFactura(AiresEntidades.EntPedido PedidoFactura)
        {
            txtFactura.Text = PedidoFactura.Factura;
            txtDeuda.Text = FormatoMoney(PedidoFactura.Debe);
            txtTotal.Text = FormatoMoney(PedidoFactura.Total);
            txtCantidadPaga.Text = FormatoMoney(PedidoFactura.Debe);
        }
        void CargaDatosFacturas(List<EntPedido> PedidosFactura)
        {
            foreach (EntPedido p in PedidosFactura)
            {
                txtFactura.Text += p.Factura + " | ";
            }
            txtFactura.Text = txtFactura.Text.Remove(txtFactura.Text.Length - 2);

            txtTotal.Text = FormatoMoney(PedidosFactura.Sum(P => P.Total));
            txtDeuda.Text = FormatoMoney(PedidosFactura.Sum(P => P.Debe));
            dtpFechaPago.MinDate = PedidosFactura[0].Fecha;
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

        private void AgregaPago_Load(object sender, EventArgs e)
        {
            cmbFormaPago.SelectedIndex = 0;
        }

        private void AgregaPago_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool cierra = true;
            if (this.DialogResult == DialogResult.OK)
            {
                if (CantidadPagoDecimal <= 0)
                {
                    MuestraMensaje("La cantidad a Pagar debe ser mayor a 0.");
                    cierra = false;
                }
                else if (CantidadPagoDecimal > ConvierteTextoADecimal(txtDeuda))
                {
                    MuestraMensaje("La cantidad a Pagar debe ser menor o igual a la cantidad que se Debe de la factura.");
                    cierra = false;
                }
                e.Cancel = !cierra;
            }
        }
    }
}
