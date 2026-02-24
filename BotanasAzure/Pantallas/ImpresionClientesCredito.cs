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
    public partial class ImpresionClientesCredito : FormBase
    {
        public ImpresionClientesCredito(List<EntCliente> ListaClientes)
        {
            InitializeComponent();

            this.ListaClientes = ListaClientes;
        }
        List<EntCliente> ListaClientes { get; set; }

        public void CargaCorte(List<EntCliente> ListaClientes)
        {
            base.SetWaitCursor();

            EntClienteBindingSource.DataSource = ListaClientes;
            rvEntradas.RefreshReport();
        }


        private void SeleccionaFactura_Load(object sender, EventArgs e)
        {
            try
            {
                CargaCorte(this.ListaClientes);
            }
            catch (Exception ex)
            {
                MuestraMensaje(ex.Message);
            }
            finally { base.SetDefaultCursor(); }
        }

    }
}
