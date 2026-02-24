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
    public partial class ImpresionCorteDetalle : FormBase
    {
        public ImpresionCorteDetalle(List<EntProducto> ListaCorte)
        {
            InitializeComponent();

            this.ListaCorte = ListaCorte;
        }
        List<EntProducto> ListaCorte { get; set; }

        public void CargaCorte(List<EntProducto> ListaCorte)
        {
            base.SetWaitCursor();

            EntProductoBindingSource.DataSource = ListaCorte;
            rvEntradas.RefreshReport();
        }


        private void SeleccionaFactura_Load(object sender, EventArgs e)
        {
            try
            {
                CargaCorte(this.ListaCorte);
            }
            catch (Exception ex)
            {
                MuestraMensaje(ex.Message);
            }
            finally { base.SetDefaultCursor(); }
        }

    }
}
