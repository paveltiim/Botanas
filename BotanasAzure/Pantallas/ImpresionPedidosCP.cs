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
    public partial class ImpresionPedidosCP : FormBase
    {
        public ImpresionPedidosCP(List<EntPedido> ListaPedidos)
        {
            InitializeComponent();

            this.ListaPedidos = ListaPedidos;
        }
        List<EntPedido> ListaPedidos { get; set; }

        public void Carga(List<EntPedido> ListaPedidos)
        {
            base.SetWaitCursor();

            EntClienteBindingSource.DataSource = ListaPedidos;
            this.rvEntradas.LocalReport.DataSources.Clear();
            this.rvEntradas.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas", ListaPedidos));
            rvEntradas.RefreshReport();
        }


        private void SeleccionaFactura_Load(object sender, EventArgs e)
        {
            try
            {
                Carga(this.ListaPedidos);
            }
            catch (Exception ex)
            {
                MuestraMensaje(ex.Message);
            }
            finally { base.SetDefaultCursor(); }
        }

    }
}
