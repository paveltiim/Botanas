using AiresEntidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Aires.Pantallas
{
    public partial class ImpresionEntradasConAjustes : FormBase
    {
        public ImpresionEntradasConAjustes(
            List<EntReporteEntradasAjustes> listaReporte,
            string empresa,
            string fechaDesde,
            string fechaHasta,
            string almacen,
            string usuario)
        {
            InitializeComponent();
            this.ListaReporte = listaReporte;
            this.Empresa = empresa;
            this.FechaDesde = fechaDesde;
            this.FechaHasta = fechaHasta;
            this.Almacen = almacen;
            this.Usuario = usuario;
        }

        List<EntReporteEntradasAjustes> ListaReporte { get; set; }
        string Empresa { get; set; }
        string FechaDesde { get; set; }
        string FechaHasta { get; set; }
        string Almacen { get; set; }
        string Usuario { get; set; }

        public void CargaReporte(List<EntReporteEntradasAjustes> listaReporte)
        {
            base.SetWaitCursor();
            EntReporteEntradasAjustesBindingSource.DataMember = "";
            EntReporteEntradasAjustesBindingSource.DataSource = listaReporte;
            rvEntradasAjustes.LocalReport.SetParameters(new ReportParameter("Empresa", this.Empresa ?? ""));
            rvEntradasAjustes.LocalReport.SetParameters(new ReportParameter("FechaDesde", this.FechaDesde ?? ""));
            rvEntradasAjustes.LocalReport.SetParameters(new ReportParameter("FechaHasta", this.FechaHasta ?? ""));
            rvEntradasAjustes.LocalReport.SetParameters(new ReportParameter("Almacen", this.Almacen ?? ""));
            rvEntradasAjustes.LocalReport.SetParameters(new ReportParameter("Usuario", this.Usuario ?? ""));
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
