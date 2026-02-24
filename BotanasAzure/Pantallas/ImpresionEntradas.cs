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
    public partial class ImpresionEntradas : FormBase
    {
        public ImpresionEntradas(List<EntProducto> ListaProductos)
        {
            InitializeComponent();

            this.ListaProductos = ListaProductos;
        }
        List<EntProducto> ListaProductos { get; set; }

        public void CargaCorte(List<EntProducto> ListaProductos)
        {
            base.SetWaitCursor();

            EntProductoBindingSource.DataSource = ListaProductos;
            rvEntradas.RefreshReport();
        }


        private void SeleccionaFactura_Load(object sender, EventArgs e)
        {
            try
            {
                CargaCorte(this.ListaProductos);
            }
            catch (Exception ex)
            {
                MuestraMensaje(ex.Message);
            }
            finally { base.SetDefaultCursor(); }
        }

    }
}
