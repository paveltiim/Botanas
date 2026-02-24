using AiresEntidades;
using AiresNegocio;
using AiresUtilerias;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Microsoft.Office.Interop.Excel;

using System.Runtime.InteropServices;
using System.Globalization;

namespace Aires.Pantallas
{
    public partial class InventariosMovil : FormBase
    {
        public InventariosMovil()
        {
            InitializeComponent();
        }

        List<EntEmpresa> ListaEmpresas;

        public void CargaInventarioMovil(int TrabajadorId)
        {
            int EmpresaId = Program.EmpresaSeleccionada.Id;
            ReportParameter parmEmpresa;
            parmEmpresa = new ReportParameter("Empresa", Program.EmpresaSeleccionada.Nombre);

            if (tcReportesInventario.SelectedIndex == 0)
            {
                List<EntProducto> listaProductos = new BusProductos().ObtieneProductosExistenciaConsigna(Program.UsuarioSeleccionado.CompañiaId, TrabajadorId)
                                                                                                .Where(P => P.AlmacenId > 0).ToList();//ESTE FILTRO ES PARA QUE TRAIGA SOLO LOS ASIGNADOS A TRABAJADOR(DETALLE)
                //if (chkSoloConExistencia.Checked)
                //    listaProductos = new BusProductos().ObtieneProductosExistenciaPorAlmacen(Program.UsuarioSeleccionado.CompañiaId, 0, AlmacenId, TipoProductoId);

                EntProductoBindingSource.DataSource = listaProductos;

                rvInventario.LocalReport.SetParameters(parmEmpresa);
                rvInventario.RefreshReport();
            }
        }

        List<EntCatalogoGenerico> ListaMovimientos { get; set; }
        public void CargaSalidasTraspaso(DateTime FechaDesde, DateTime FechaHasta, int AlmacenId)
        {
            gvProductos.DataSource = null;
            gvSalidasTraspasos.DataSource = new BusProductos().ObtieneMovimientosSalidasProductos(Program.UsuarioSeleccionado.CompañiaId,
                                                                                            FechaDesde, FechaHasta,
                                                                                            AlmacenId, (int)TipoMovimiento.TRASPASOCONSIGNA);
        }

        public void CargaMovimientos(int TipoMovimientoId, DateTime FechaDesde, DateTime FechaHasta, int AlmacenId)
        {
            gvProductos.DataSource = null;
            if (TipoMovimientoId == 1)
                this.ListaMovimientos = new BusProductos().ObtieneMovimientosEntradasProductos(Program.EmpresaSeleccionada.Id,
                                                                                            FechaDesde, FechaHasta, AlmacenId);
            else
                this.ListaMovimientos = new BusProductos().ObtieneMovimientosSalidasProductos(Program.EmpresaSeleccionada.Id,
                                                                                                FechaDesde, FechaHasta, AlmacenId);
            gvSalidasTraspasos.DataSource = this.ListaMovimientos;
        }
        public void CargaProductosMovimientoSalida(int MovimientoId)
        {
            List<EntProducto> listaProductos = new BusProductos().ObtieneMovimientosDetalleProductos(2, MovimientoId);
            EntProductoBindingSource.DataSource = listaProductos;
            gvProductos.DataSource = listaProductos;

            decimal cantidad = listaProductos.Sum(P => P.Cantidad);
            txtCantidadTotalEntradas.Text = cantidad.ToString();
            txtPrecioCostoTotalEntradas.Text = FormatoMoney(listaProductos.Sum(P => P.Precio));
            rvEntradas.RefreshReport();
        }
        void InicializaPantalla()
        {
            gvSalidasTraspasos.DataSource = new List<EntCatalogoGenerico>();
            cmbMesesEntradas.SelectedIndex = DateTime.Today.Month - 1;
            CargaAñosCmb(cmbAñoEntradas);
        }


        private void Inventarios_Load(object sender, EventArgs e)
        {
            try
            {
                InicializaPantalla();

                base.CargaTrabajadoresPorEmpresa(Program.UsuarioSeleccionado.CompañiaId, 2, cmbTrabajadoresInventarioMovil);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnRefrescarEntradas_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                //if (rdoEntradasPorDia.Checked)
                //    CargaMovimientos(cmbTipoMovimiento.SelectedIndex+1,
                //                     dtpEntradasFechaDia.Value.Date, dtpEntradasFechaDia.Value.Date.AddDays(1), 
                //                     almacen.Id);
                //else if (rdoEntradasPorMes.Checked)
                //{
                //    if (cmbMesesEntradas.SelectedIndex >= 0)
                //        CargaMovimientos(cmbTipoMovimiento.SelectedIndex + 1,
                //                        FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                //                        FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1),
                //                        almacen.Id);
                //}
                DateTime fechaDesde = DateTime.Today;
                DateTime fechaHasta = DateTime.Today;
                if (rdoEntradasPorMes.Checked)
                {
                    if (cmbMesesEntradas.SelectedIndex >= 0)
                    {
                        fechaDesde = FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas);
                        fechaHasta = FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1);
                    }
                }
                else
                {
                    fechaDesde = dtpEntradasFechaDia.Value.Date;
                    fechaHasta = dtpEntradasFechaDia.Value.Date.AddDays(1);
                }
                CargaSalidasTraspaso(fechaDesde, fechaHasta, 0);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void rdoEntradasPorMes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((RadioButton)sender).Checked)
                {
                    rdoEntradasPorDia.Checked = false;
                    pnlEntradasPorMes.Enabled = true;
                    pnlEntradasPorSemana.Enabled = false;

                    CargaSalidasTraspaso(FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                                         FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1),
                                         0);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void rdoEntradasPorDia_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((RadioButton)sender).Checked)
                {
                    rdoEntradasPorMes.Checked = false;
                    pnlEntradasPorMes.Enabled = false;
                    pnlEntradasPorSemana.Enabled = true;

                    CargaSalidasTraspaso(dtpEntradasFechaDia.Value.Date, dtpEntradasFechaDia.Value.Date.AddDays(1),
                                         0);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbMesesEntradas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                //if (cmbMesesEntradas.Focused)
                //{
                //    EntCatalogoGenerico almacen = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);
                //    if (rdoEntradasPorDia.Checked)
                //        CargaMovimientos(cmbTipoMovimiento.SelectedIndex + 1,dtpEntradasFechaDia.Value.Date, dtpEntradasFechaDia.Value.Date.AddDays(1), almacen.Id);
                //    else if (rdoEntradasPorMes.Checked)
                //    {
                //        if (cmbMesesEntradas.SelectedIndex >= 0)
                //            CargaMovimientos(cmbTipoMovimiento.SelectedIndex + 1,FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                //                          FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1),
                //                          almacen.Id);
                //    }
                //}
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void dtpEntradasFechaDia_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                if (dtpEntradasFechaDia.Focused)
                    btnRefrescarEntradas.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void tcInventarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tcInventarios.SelectedIndex == 1)//Inventario Consignacion
                {
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //EntCatalogoGenerico ingreso = ObtieneListaGenericaFromGV(gvIngresos)[gvIngresos.CurrentRow.Index];

                //EntProducto productoSeleccionado = ObtieneProductoFromGV(gvProductos);

                //MuestraProductosDetalle vProductosDetalles = new MuestraProductosDetalle(new BusProductos().ObtieneProductosDetallePorIngreso(ingreso.Id).Where(P => P.ProductoId == productoSeleccionado.Id).ToList());
                //vProductosDetalles.Show();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                EntCatalogoGenerico ingresoSeleccionado = ObtieneCatalogoGenericoFromGV(gvSalidasTraspasos);
                MuestraIngreso vMuestraIngreso = new Pantallas.MuestraIngreso(ingresoSeleccionado);
                if (vMuestraIngreso.ShowDialog() == DialogResult.OK)
                {
                    ingresoSeleccionado.Descripcion = vMuestraIngreso.Descripcion;
                    ingresoSeleccionado.Fecha = vMuestraIngreso.Fecha;
                    ingresoSeleccionado.Estatus = true;
                    new BusProductos().ActualizaIngreso(ingresoSeleccionado);

                    btnRefrescarEntradas.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                EntCatalogoGenerico ingresoSeleccionado = ObtieneCatalogoGenericoFromGV(gvSalidasTraspasos);
                if (MuestraMensajeYesNo(string.Format("¿Desea Eliminar el Ingreso: {0} - {1}? \n Se Eliminarán todos los Productos relacionados a este Ingreso.", ingresoSeleccionado.Descripcion, ingresoSeleccionado.Fecha.ToShortDateString()), "CONFIRMACIÓN ELIMINAR") == DialogResult.Yes)
                {
                    ingresoSeleccionado.Estatus = false;
                    ingresoSeleccionado.EstatusId = 0;
                    new BusProductos().ActualizaEstatusIngreso(ingresoSeleccionado);
                    new BusProductos().ActualizaEstatusProductoDetalle(ingresoSeleccionado);

                    List<EntProducto> productos = new BusProductos().ObtieneProductosPorIngreso(ingresoSeleccionado.Id);
                    foreach (EntProducto p in productos)
                    {
                        new BusProductos().AumentaProducto(p.Id, -Convert.ToInt32(p.Cantidad));
                    }

                    btnRefrescarEntradas.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnRefrescarInventario_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                CargaInventarioMovil(ObtieneTrabajadorFromCmb(cmbTrabajadoresInventarioMovil).Id);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        public void ExportaProductos(List<EntProducto> ListaProductos)
        {

        }

        public void ExportaIngresos(List<EntCatalogoGenerico> ListaIngresos)
        {

        }

        private void gvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbAñoEntradas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                if (cmbAñoEntradas.Focused)
                    btnRefrescarEntradas.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void gvSalidasTraspasos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                EntCatalogoGenerico entrada = ObtieneCatalogoGenericoFromGV(gvSalidasTraspasos);
                CargaProductosMovimientoSalida(entrada.Id);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void chkSeleccionaTodasSalidasTraspasos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                List<EntCatalogoGenerico> listaEntradasSeleccionadas = ObtieneListaGenericaFromGV(gvSalidasTraspasos);
                foreach (EntCatalogoGenerico en in listaEntradasSeleccionadas)
                {
                    en.Estatus = chkSeleccionaTodasSalidasTraspasos.Checked;
                }
                gvSalidasTraspasos.Refresh();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnExportaEntradasPorMes_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();

                List<EntProducto> listaProductos = new List<EntProducto>();

                List<EntCatalogoGenerico> listaEntradasSeleccionadas = ObtieneListaGenericaFromGV(gvSalidasTraspasos).Where(P => P.Estatus).ToList();
                if (listaEntradasSeleccionadas.Count <= 0)
                    MandaExcepcion("SELECCIONE AL MENOS UNA SALIDA TRASPASO");

                foreach (EntCatalogoGenerico en in listaEntradasSeleccionadas)
                {
                    List<EntProducto> listaProductosIngreso = new BusProductos().ObtieneMovimientosDetalleProductosSinDatosFactura(2, en.Id, en.IdSecundario);//MOVIMIENTOID | INGRESOID
                    if (en.IdSecundario == 0)
                    {
                        foreach (EntProducto c in listaProductosIngreso)
                        {
                            //c.Modelo = en.Descripcion;
                            c.Fecha = en.Fecha;
                            c.FechaCorta = en.Fecha.ToShortDateString();
                        }
                    }

                    listaProductos.AddRange(listaProductosIngreso);
                }
                ImpresionSalidas vImprimeEntradas = new ImpresionSalidas(listaProductos);
                vImprimeEntradas.Show();

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnExportaEntradas_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();

                List<EntProducto> listaProductos = new List<EntProducto>();

                List<EntCatalogoGenerico> listaEntradasSeleccionadas = ObtieneListaGenericaFromGV(gvSalidasTraspasos);
                //if (listaEntradasSeleccionadas.Count <= 0)
                //    MandaExcepcion("SELECCIONE AL MENOS UNA ENTRADA");

                string dia = "";
                CultureInfo culturaEspanol = new CultureInfo("es-ES");
                if (rdoEntradasPorDia.Checked)
                    dia = "\n" + culturaEspanol.DateTimeFormat.GetDayName(dtpEntradasFechaDia.Value.DayOfWeek) + " " + dtpEntradasFechaDia.Value.Day.ToString().PadLeft(2, '0');
                foreach (EntCatalogoGenerico en in listaEntradasSeleccionadas)
                {
                    List<EntProducto> listaProductosIngreso = new BusProductos().ObtieneMovimientosDetalleProductosSinDatosFactura(2, en.Id, en.IdSecundario);//MOVIMIENTOID | INGRESOID
                    if (en.IdSecundario == 0)
                    {
                        foreach (EntProducto c in listaProductosIngreso)
                        {
                            //c.Modelo = en.Descripcion;
                            c.Fecha = en.Fecha;
                            c.FechaCorta = en.Fecha.ToString("yyyy - MMM").ToUpper() + dia;
                        }
                    }

                    listaProductos.AddRange(listaProductosIngreso);
                }
                ImpresionSalidas vImprimeEntradas = new ImpresionSalidas(listaProductos);
                vImprimeEntradas.Show();

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }
    }
}
