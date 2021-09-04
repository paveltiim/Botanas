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
        public AgregaPago()
        {
            InitializeComponent();
        }
        public AgregaPago(List<EntPedido> PedidosFactura)
        {
            InitializeComponent();

            CargaDatosFacturas(PedidosFactura);
        }
        
        public AgregaPago(AiresEntidades.EntPedido PedidoFactura)
        {
            InitializeComponent();

            CargaDatosFactura(PedidoFactura);
        }

        public string CantidadPago { get { return txtCantidadPaga.Text; } }
        public decimal CantidadPagoDecimal { get { return ConvierteTextoADecimal(txtCantidadPaga); } }
        public DateTime FechaPago { get { return dtpFechaPago.Value; } }

        void CargaDatosFactura(AiresEntidades.EntPedido PedidoFactura)
        {
            txtFactura.Text = PedidoFactura.Factura;
            txtDeuda.Text = FormatoMoney(PedidoFactura.Debe);
            txtTotal.Text = FormatoMoney(PedidoFactura.Total);
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
