using AiresEntidades;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Aires.Pantallas
{
    public partial class ImpresionEntradasConAjustes : FormBase
    {
        public ImpresionEntradasConAjustes(List<EntReporteEntradasAjustes> listaReporte)
        {
            InitializeComponent();
            this.ListaReporte = listaReporte;
        }

        List<EntReporteEntradasAjustes> ListaReporte { get; set; }

        public void CargaReporte(List<EntReporteEntradasAjustes> listaReporte)
        {
            base.SetWaitCursor();
            EntReporteEntradasAjustesBindingSource.DataMember = "";
            EntReporteEntradasAjustesBindingSource.DataSource = listaReporte;
            rvEntradasAjustes.RefreshReport();
        }

        private void ImpresionEntradasConAjustes_Load(object sender, EventArgs e)
        {
            try
            {
                CargaReporte(this.ListaReporte);
            }
            catch (Exception ex)
            {
                MuestraMensaje(ex.Message);
            }
            finally { base.SetDefaultCursor(); }
        }
    }
}
