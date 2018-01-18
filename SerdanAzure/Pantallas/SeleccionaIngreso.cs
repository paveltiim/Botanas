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
    public partial class SeleccionaIngreso : FormBase
    {
        public SeleccionaIngreso()
        {
            InitializeComponent();
        }
        public SeleccionaIngreso(EntEmpresa EmpresaSeleccionada)
        {
            InitializeComponent();
            this.EmpresaSeleccionada = EmpresaSeleccionada;
        }
        EntEmpresa EmpresaSeleccionada;

        public EntCatalogoGenerico IngresoSeleccionado { get { return ObtieneCatalogoGenericoFromGV(gvIngresos); } }
        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        void CargaAños()
        {
            List<EntCatalogoGenerico> años = new List<EntCatalogoGenerico>();
            for (int x = DateTime.Today.Year; x >= AñoInicio; x--)
            {
                EntCatalogoGenerico año = new EntCatalogoGenerico();
                año.Descripcion = x.ToString();
                años.Add(año);
            }
            cmbAñoEntradas.DataSource = años;
        }

        public void CargaIngresos(DateTime FechaDesde, DateTime FechaHasta)
        {
            List<EntCatalogoGenerico> listaIngresos = new BusProductos().ObtieneIngresosProductos(EmpresaSeleccionada.Id,FechaDesde, FechaHasta);
            gvIngresos.DataSource = listaIngresos;
        }


        private void SeleccionaFactura_Load(object sender, EventArgs e)
        {
            try
            {
                CargaAños();

                cmbMesesEntradas.SelectedIndex = DateTime.Today.Month - 1;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void gvGastosEmpresa_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAgregar.PerformClick();
        }
        private void cmbMesesEntradas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbMesesEntradas.SelectedIndex >= 0)
                    CargaIngresos(new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1)));
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbAñoEntradas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbMesesEntradas.SelectedIndex >= 0)
                    CargaIngresos(new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1)));
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
    }
}
