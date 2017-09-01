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
    public partial class AgregaPago : Form
    {
        public AgregaPago()
        {
            InitializeComponent();
        }

        public AgregaPago(List<AiresEntidades.EntPedido> PedidosFactura)
        {
            InitializeComponent();

            CargaDatosFacturas(PedidosFactura);
        }
        public AgregaPago(AiresEntidades.EntPedido PedidoFactura)
        {
            InitializeComponent();

            CargaDatosFactura(PedidoFactura);
        }

        /// <summary>
        /// Regresa el valor con formato {0:c}.
        /// </summary>
        /// <param name="Valor"></param>
        /// <returns></returns>
        public string FormatoMoney(decimal Valor)
        {
            return string.Format("{0:c}", Valor);
        }
        void CargaDatosFactura(AiresEntidades.EntPedido PedidoFactura)
        {
            txtFactura.Text = PedidoFactura.Factura;
            txtTotal.Text = FormatoMoney(PedidoFactura.Total);
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

        public string CantidadPago { get { return txtCantidadPaga.Text; } }
        public DateTime FechaPago { get { return dtpFechaPago.Value; } }

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
    }
}
