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
    public partial class ImpresionPagosCliente : FormBase
    {
        public ImpresionPagosCliente(List<EntPago> ListaPago)
        {
            InitializeComponent();

            this.ListaPago = ListaPago;
        }
        List<EntPago> ListaPago { get; set; }

        public void CargaCorte(List<EntPago> ListaPago)
        {
            base.SetWaitCursor();

            EntPedidoBindingSource.DataSource = ConvierteListaPagosEnPedidos(ListaPago);
            rvEntradas.RefreshReport();
        }


        private void SeleccionaFactura_Load(object sender, EventArgs e)
        {
            try
            {
                CargaCorte(this.ListaPago);
            }
            catch (Exception ex)
            {
                MuestraMensaje(ex.Message);
            }
            finally { base.SetDefaultCursor(); }
        }

    }
}
