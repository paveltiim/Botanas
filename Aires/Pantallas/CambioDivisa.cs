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
    public partial class CambioDivisa : FormBase
    {
        public CambioDivisa()
        {
            InitializeComponent();
        }

        public string Cantidad { get { return txtCantidad.Text; } }
        public decimal Cambio { get { return ConvierteTextoADecimal(txtCambio); } }

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
        void CargaDivisa(int DivisaId)
        {
            EntDivisa div = new BusDivisas().ObtieneDivisa(DivisaId);
            txtDivisa.Text = div.Descripcion;
            txtValorDivisa.Text =FormatoMoney( div.Valor);
        }
        private void AgregaDeuda_Load(object sender, EventArgs e)
        {
            try
            {
                CargaDivisa(1);
                txtCantidad.Text = FormatoMoney(0);
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCambio.Text = FormatoMoney(ConvierteTextoADecimal(txtCantidad) * ConvierteTextoADecimal(txtValorDivisa));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
