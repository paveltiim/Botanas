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

namespace Aires.Pantallas
{
    public partial class Inventarios : FormBase
    {
        public Inventarios()
        {
            InitializeComponent();
        }
        public void VerificaEmpresa()
        {
            cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }

        List<EntEmpresa> ListaEmpresas;
        /// <summary>
        /// cmbTipoProductoFiltro.SelectedIndex + 1
        /// </summary>
        int TipoProductoId { get { return cmbTipoProductoFiltro.SelectedIndex + 1; } }

        void CargaAlmacenes()
        {
            List<EntCatalogoGenerico> almacenes = new BusEmpresas().ObtieneAlmacenes(Program.UsuarioSeleccionado.CompañiaId, Program.UsuarioSeleccionado.Id);
            if(Program.UsuarioSeleccionado.Administrador)
                almacenes.Insert(0, new EntCatalogoGenerico() { Id = 0, Descripcion = "-TODAS-" });
            cmbAlmacenes.DataSource = almacenes;
            cmbAlmacenes.SelectedIndex = 0;
        }

        public void CargaInventario(int AlmacenId, int TipoProductoId)
        {
            int EmpresaId = Program.EmpresaSeleccionada.Id;
            ReportParameter parmEmpresa;
            parmEmpresa = new ReportParameter("Empresa", Program.EmpresaSeleccionada.Nombre);

            if (tcReportesInventario.SelectedIndex == 0)
            {
                List<EntProducto> listaProductos;
                if (chkSoloConExistencia.Checked)
                    listaProductos = new BusProductos().ObtieneProductosExistenciaPorAlmacen(Program.UsuarioSeleccionado.CompañiaId, 0, AlmacenId, TipoProductoId);
                else
                    listaProductos = new BusProductos().ObtieneProductosPorAlmacen(Program.UsuarioSeleccionado.CompañiaId, 0, AlmacenId, TipoProductoId);
                //gvPedidos.DataSource = ListaPedidos;
                EntProductoBindingSource.DataSource = listaProductos;

                rvInventario.LocalReport.SetParameters(parmEmpresa);
                rvInventario.RefreshReport();
            }
        }

        List<EntCatalogoGenerico> ListaMovimientos { get; set; }
        public void CargaMovimientos(int TipoMovimientoId, DateTime FechaDesde, DateTime FechaHasta, int AlmacenId)
        {
            gvProductos.DataSource = null;
            if (TipoMovimientoId == 1)
                this.ListaMovimientos = new BusProductos().ObtieneMovimientosEntradasProductos(Program.EmpresaSeleccionada.Id,
                                                                                            FechaDesde, FechaHasta, AlmacenId);
            else
                this.ListaMovimientos = new BusProductos().ObtieneMovimientosSalidasProductos(Program.EmpresaSeleccionada.Id,
                                                                                                FechaDesde, FechaHasta, AlmacenId);
            gvIngresos.DataSource = this.ListaMovimientos;
        }
        public void CargaProductosMovimiento(int TipoMovimientoId, int MovimientoId)
        {
            //List<EntProducto> listaProductos = new BusProductos().ObtieneProductosPorIngreso(IngresoId);
            List<EntProducto> listaProductos = new BusProductos().ObtieneMovimientosDetalleProductos(TipoMovimientoId, MovimientoId);
            //gvPedidos.DataSource = ListaPedidos;
            EntProductoBindingSource.DataSource = listaProductos;
            gvProductos.DataSource = listaProductos;//.OrderByDescending(P => P.Fecha).ToList();

            decimal cantidad = listaProductos.Sum(P => P.Cantidad);
            txtCantidadTotalEntradas.Text = cantidad.ToString();
            txtPrecioCostoTotalEntradas.Text = FormatoMoney(listaProductos.Sum(P => P.PrecioC));
            txtPrecioVentaTotalEntradas.Text = FormatoMoney(listaProductos.Sum(P => P.Precio));
            rvEntradas.RefreshReport();
        }

        public void CargaIngresosProductos(DateTime FechaDesde, DateTime FechaHasta)
        {
            int EmpresaId = Program.EmpresaSeleccionada.Id;

            List<EntCatalogoGenerico> listaIngresosProductos = new BusProductos().ObtieneIngresosProductos(EmpresaId, FechaDesde, FechaHasta);
            gvIngresos.DataSource = listaIngresosProductos;

            gvProductos.DataSource = null;
        }

        public void CargaEmpresas()
        {
            if (Program.UsuarioSeleccionado.Id > 1)
                ListaEmpresas = new BusEmpresas().ObtieneEmpresas().Where(P => P.UsuarioId == Program.UsuarioSeleccionado.Id).ToList();
            else
                ListaEmpresas = new BusEmpresas().ObtieneEmpresas();

            Program.CambiaEmpresa = false;
            cmbEmpresas.DataSource = ListaEmpresas;
            Program.CambiaEmpresa = true;
        }
        void InicializaPantalla()
        {
            gvIngresos.DataSource = new List<EntCatalogoGenerico>();
            cmbMesesEntradas.SelectedIndex = DateTime.Today.Month - 1;
            CargaAñosCmb(cmbAñoEntradas);
            cmbTipoMovimiento.SelectedIndex = 0;
            cmbTipoProductoFiltro.SelectedIndex = 0;

            if(Program.UsuarioSeleccionado.TipoUsuarioId==(int)TipoUsuario.ADMINISTRADORINSUMOS
                || Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.MASTER)
            {
                lbTipoProductoFiltro.Visible = true;
                cmbTipoProductoFiltro.Visible = true;
            }
        }


        private void Inventarios_Load(object sender, EventArgs e)
        {
            try
            {
                InicializaPantalla();
                //CargaEmpresas();

                //if (Program.EmpresaSeleccionada == null)
                //    Program.EmpresaSeleccionada = SeleccionaEmpresa();

                //if (Program.EmpresaSeleccionada != null)
                //{
                //    CargaAños();

                //    cmbMesesEntradas.SelectedIndex = DateTime.Today.Month - 1;
                //    dtpEntradasFechaDia.Value = DateTime.Today;

                //    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);

                //    ////CargaEntradas(new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1)));
                //    //CargaIngresosProductos(new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1)));
                //    //CargaInventario();

                //    ////this.rvInventario.RefreshReport();
                //    ////this.rvEntradas.RefreshReport();
                //}

                pnlBotonesGenerales.Enabled = Program.UsuarioSeleccionado.Administrador;

                //if (Program.UsuarioSeleccionado.Id > 1)
                //{
                //    gvProductos.Columns[3].Visible = false;
                //    gvProductos.Columns[5].Visible = false;
                //}
                CargaAlmacenes();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        EntCatalogoGenerico AlmacenSeleccionado { get { return ObtieneCatalogoGenericoFromCmb(cmbAlmacenes); } }
        private void btnRefrescarEntradas_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                EntCatalogoGenerico almacen = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);
                if (rdoEntradasPorDia.Checked)
                    CargaMovimientos(cmbTipoMovimiento.SelectedIndex+1,
                                     dtpEntradasFechaDia.Value.Date, dtpEntradasFechaDia.Value.Date.AddDays(1), 
                                     almacen.Id);
                else if (rdoEntradasPorMes.Checked)
                {
                    if (cmbMesesEntradas.SelectedIndex >= 0)
                        CargaMovimientos(cmbTipoMovimiento.SelectedIndex + 1,
                                        FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                                        FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1),
                                        almacen.Id);
                }
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
                    //CargaIngresosProductos(new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1)));
                    CargaMovimientos(cmbTipoMovimiento.SelectedIndex + 1,
                                        FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                                        FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1),
                                        this.AlmacenSeleccionado.Id);
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
                    CargaMovimientos(cmbTipoMovimiento.SelectedIndex + 1, 
                                     dtpEntradasFechaDia.Value.Date, dtpEntradasFechaDia.Value.Date.AddDays(1), 
                                     this.AlmacenSeleccionado.Id);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbMesesEntradas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                if (cmbMesesEntradas.Focused)
                {
                    EntCatalogoGenerico almacen = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);
                    if (rdoEntradasPorDia.Checked)
                        CargaMovimientos(cmbTipoMovimiento.SelectedIndex + 1,dtpEntradasFechaDia.Value.Date, dtpEntradasFechaDia.Value.Date.AddDays(1), almacen.Id);
                    else if (rdoEntradasPorMes.Checked)
                    {
                        if (cmbMesesEntradas.SelectedIndex >= 0)
                            CargaMovimientos(cmbTipoMovimiento.SelectedIndex + 1,FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                                          FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1),
                                          almacen.Id);
                    }
                }
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

        private void gvIngresos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                EntCatalogoGenerico entrada = ObtieneCatalogoGenericoFromGV(gvIngresos);
                CargaProductosMovimiento(entrada.ClaveId, entrada.Id);
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
                EntCatalogoGenerico ingresoSeleccionado = ObtieneCatalogoGenericoFromGV(gvIngresos);
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
                EntCatalogoGenerico ingresoSeleccionado = ObtieneCatalogoGenericoFromGV(gvIngresos);
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

        private void btnEliminarProductosDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                //EntCatalogoGenerico ingresoSeleccionado = ObtieneCatalogoGenericoFromGV(gvIngresos);
                //EntProducto productoSeleccionado = ObtieneProductoFromGV(gvProductos);

                //if (MuestraMensajeYesNo(string.Format("¿Desea Eliminar el Producto seleccionado: {0} - {1}?", productoSeleccionado.Codigo, productoSeleccionado.Descripcion), "CONFIRMACIÓN ELIMINAR") == DialogResult.Yes)
                //{
                //    List<EntProducto> productosDetalle = new BusProductos().ObtieneProductosDetallePorIngreso(ingresoSeleccionado.Id).Where(P => P.ProductoId == productoSeleccionado.Id).ToList();

                //    foreach (EntProducto p in productosDetalle)
                //    {
                //        p.EstatusId = 0;
                //        new BusProductos().ActualizaEstatusProductoDetalle(p);
                //    }
                //    new BusProductos().AumentaProducto(productoSeleccionado.Id, -Convert.ToInt32(productoSeleccionado.Cantidad));

                //    CargaProductosEntrada(ingresoSeleccionado.Id);
                //}
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnMueveAIngreso_Click(object sender, EventArgs e)
        {
            try
            {
                SeleccionaIngreso vIngresos = new Pantallas.SeleccionaIngreso(Program.EmpresaSeleccionada);
                if (vIngresos.ShowDialog() == DialogResult.OK)
                {
                    List<EntProducto> productosDetalle = new BusProductos().ObtieneProductosDetallePorIngreso(ObtieneCatalogoGenericoFromGV(gvIngresos).Id).Where(P => P.ProductoId == ObtieneProductoFromGV(gvProductos).Id).ToList();

                    foreach (EntProducto p in productosDetalle)
                    {
                        p.IngresoId = vIngresos.IngresoSeleccionado.Id;

                        new BusProductos().ActualizaProductoDetalle(p);
                    }
                    btnRefrescarEntradas.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Program.CambiaEmpresa)
                {
                    Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                    if (tcInventarios.SelectedIndex == 0)
                        btnRefrescarInventario.PerformClick();
                    else if (tcInventarios.SelectedIndex == 1)
                        btnRefrescarEntradas.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnBuscaEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                SeleccionaEmpresa vSeleccionaEmp = new Pantallas.SeleccionaEmpresa(ListaEmpresas);
                if (vSeleccionaEmp.ShowDialog() == DialogResult.OK)
                {
                    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == vSeleccionaEmp.EmpresaSeleccionada.Id);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnRefrescarInventario_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                EntCatalogoGenerico almacen = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);
                CargaInventario(almacen.Id, this.TipoProductoId);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        public void ExportaProductos(List<EntProducto> ListaProductos) {
            //if (ListaProductos.Count == 0)
            //    MandaExcepcion("NO SE SELECCIONARON PRODUCTOS A EXPORTAR");

            //this.Cursor = Cursors.WaitCursor;
            //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            //if (xlApp == null)
            //    MandaExcepcion("Excel NO esta instalado apropiadamente!!");

            //SeleccionaEmail vEmail = new Pantallas.SeleccionaEmail();
            //if (vEmail.ShowDialog() == DialogResult.OK)
            //{

            //    Workbook xlWorkBook;
            //    Worksheet xlWorkSheet;

            //    object misValue = System.Reflection.Missing.Value;
            //    xlWorkBook = xlApp.Workbooks.Add(misValue);
            //    xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //    //--------------REN|COL----------//
            //    xlWorkSheet.Cells[1, 1] = "ID";
            //    xlWorkSheet.Cells[1, 2] = "PRODUCTOID";
            //    xlWorkSheet.Cells[1, 3] = "CODIGO";
            //    xlWorkSheet.Cells[1, 4] = "DESCRIPCION";
            //    xlWorkSheet.Cells[1, 5] = "TIPOPRODUCTOID";
            //    xlWorkSheet.Cells[1, 6] = "TIPOPRODUCTO";
            //    xlWorkSheet.Cells[1, 7] = "SERIE";
            //    xlWorkSheet.Cells[1, 8] = "PRECIOCOSTO";
            //    xlWorkSheet.Cells[1, 9] = "PRECIOVENTA";

            //    xlWorkSheet.Cells[1, 10] = "INGRESOID";
            //    xlWorkSheet.Cells[1, 11] = "INGRESO";
            //    xlWorkSheet.Cells[1, 12] = "FECHAINGRESO";

            //    xlWorkSheet.Cells[1, 13] = "EMPRESAID";
            //    //xlWorkSheet.Cells[1, 14] = "PROVEEDORID";

            //    int ren = 2;
            //    //foreach (EntCatalogoGenerico i in ObtieneListaGenericaFromGV(gvIngresos))
            //    //{

            //        //List<EntProducto> listaProductos = new BusProductos().ObtieneProductosDetallePorIngreso(i.Id);

            //        foreach (EntProducto p in ListaProductos)
            //        {
            //            xlWorkSheet.Cells[ren, 1] = p.Id;           // "ID";
            //            xlWorkSheet.Cells[ren, 2] = p.ProductoId;   // "PRODUCTOID";
            //            xlWorkSheet.Cells[ren, 3] = p.Codigo;       // "CODIGO";
            //            xlWorkSheet.Cells[ren, 4] = p.Descripcion;  // "DESCRIPCION";

            //            xlWorkSheet.Cells[ren, 5] = p.TipoProductoId;   //"TIPOPRODUCTOID";
            //            xlWorkSheet.Cells[ren, 6] = p.TipoProducto;     //"TIPOPRODUCTO";
            //            xlWorkSheet.Cells[ren, 7] = p.Serie;            // "SERIE";
            //            xlWorkSheet.Cells[ren, 8] = p.PrecioCosto;      // "PRECIOCOSTO";
            //            xlWorkSheet.Cells[ren, 9] = p.PrecioVenta;      // "PRECIOVENTA";
            //            xlWorkSheet.Cells[ren, 10] = p.IngresoId;       // "INGRESOID";
            //            xlWorkSheet.Cells[ren, 11] = p.Ingreso;         // "INGRESO";
            //            xlWorkSheet.Cells[ren, 12] = p.Fecha;           // "INGRESOFECHA";

            //            xlWorkSheet.Cells[ren, 13] = p.EmpresaId;           // "EMPRESAID";
            //        ren++;
            //        }
            //    //}

            //    //EntCatalogoGenerico ingreso = ObtieneCatalogoGenericoFromGV(gvIngresos);
            //    string rutaExportacion = string.Format(@"c:\TIIM\EXPORTACIONES\Entradas {0:yyyy-MM-dd}.xls", ListaProductos[0].Fecha);

            //    try
            //    {
            //        xlWorkBook.SaveAs(rutaExportacion, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //        xlWorkBook.Close(true, misValue, misValue);
            //        xlApp.Quit();

            //        Marshal.ReleaseComObject(xlWorkSheet);
            //        Marshal.ReleaseComObject(xlWorkBook);
            //        Marshal.ReleaseComObject(xlApp);

            //        EnviaCorreoArchivoCompras(vEmail.EmailSeleccionado, ListaProductos[0].Fecha, rutaExportacion);
            //    }
            //    catch (Exception ex)
            //    {
            //        xlWorkBook.Close(true, misValue, misValue);
            //        xlApp.Quit();

            //        Marshal.ReleaseComObject(xlWorkSheet);
            //        Marshal.ReleaseComObject(xlWorkBook);
            //        Marshal.ReleaseComObject(xlApp);
            //        MandaExcepcion(ex.Message);
            //    }
            //    MuestraMensaje("¡EXPORTACIÓN ENVIADA!", "CONFIRMACIÓN");
            //    //MessageBox.Show("Excel file created , you can find the file d:\\csharp-Excel.xls");
            //    this.Cursor = Cursors.Default;
            //}
            //this.Cursor = Cursors.Default;
        }
        public void ExportaProductos(List<EntProducto> ListaProductos, int EmpresaId)
        {
            //if (ListaProductos.Count == 0)
            //    MandaExcepcion("NO SE SELECCIONARON PRODUCTOS A EXPORTAR");

            //this.Cursor = Cursors.WaitCursor;
            //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            //if (xlApp == null)
            //    MandaExcepcion("Excel NO esta instalado apropiadamente!!");

            //SeleccionaEmail vEmail = new Pantallas.SeleccionaEmail();
            //if (vEmail.ShowDialog() == DialogResult.OK)
            //{

            //    Workbook xlWorkBook;
            //    Worksheet xlWorkSheet;

            //    object misValue = System.Reflection.Missing.Value;
            //    xlWorkBook = xlApp.Workbooks.Add(misValue);
            //    xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //    //--------------REN|COL----------//
            //    xlWorkSheet.Cells[1, 1] = "ID";
            //    xlWorkSheet.Cells[1, 2] = "PRODUCTOID";
            //    xlWorkSheet.Cells[1, 3] = "CODIGO";
            //    xlWorkSheet.Cells[1, 4] = "DESCRIPCION";
            //    xlWorkSheet.Cells[1, 5] = "TIPOPRODUCTOID";
            //    xlWorkSheet.Cells[1, 6] = "TIPOPRODUCTO";
            //    xlWorkSheet.Cells[1, 7] = "SERIE";
            //    xlWorkSheet.Cells[1, 8] = "PRECIOCOSTO";
            //    xlWorkSheet.Cells[1, 9] = "PRECIOVENTA";

            //    xlWorkSheet.Cells[1, 10] = "INGRESOID";
            //    xlWorkSheet.Cells[1, 11] = "INGRESO";
            //    xlWorkSheet.Cells[1, 12] = "FECHAINGRESO";

            //    xlWorkSheet.Cells[1, 13] = "EMPRESAID";
            //    //xlWorkSheet.Cells[1, 14] = "PROVEEDORID";

            //    int ren = 2;
            //    //foreach (EntCatalogoGenerico i in ObtieneListaGenericaFromGV(gvIngresos))
            //    //{

            //    //List<EntProducto> listaProductos = new BusProductos().ObtieneProductosDetallePorIngreso(i.Id);

            //    foreach (EntProducto p in ListaProductos)
            //    {
            //        xlWorkSheet.Cells[ren, 1] = p.Id;           // "ID";
            //        xlWorkSheet.Cells[ren, 2] = p.ProductoId;   // "PRODUCTOID";
            //        xlWorkSheet.Cells[ren, 3] = p.Codigo;       // "CODIGO";
            //        xlWorkSheet.Cells[ren, 4] = p.Descripcion;  // "DESCRIPCION";

            //        xlWorkSheet.Cells[ren, 5] = p.TipoProductoId;   //"TIPOPRODUCTOID";
            //        xlWorkSheet.Cells[ren, 6] = p.TipoProducto;     //"TIPOPRODUCTO";
            //        xlWorkSheet.Cells[ren, 7] = p.Serie;            // "SERIE";
            //        xlWorkSheet.Cells[ren, 8] = p.PrecioCosto;      // "PRECIOCOSTO";
            //        xlWorkSheet.Cells[ren, 9] = p.PrecioVenta;      // "PRECIOVENTA";
            //        xlWorkSheet.Cells[ren, 10] = p.IngresoId;       // "INGRESOID";
            //        xlWorkSheet.Cells[ren, 11] = p.Ingreso;         // "INGRESO";
            //        xlWorkSheet.Cells[ren, 12] = p.Fecha;           // "INGRESOFECHA";

            //        xlWorkSheet.Cells[ren, 13] = EmpresaId;           // "EMPRESAID";
            //        ren++;
            //    }
            //    //}

            //    //EntCatalogoGenerico ingreso = ObtieneCatalogoGenericoFromGV(gvIngresos);
            //    string rutaExportacion = string.Format(@"c:\TIIM\EXPORTACIONES\Entradas {0:yyyy-MM-dd}.xls", ListaProductos[0].Fecha);

            //    try
            //    {
            //        xlWorkBook.SaveAs(rutaExportacion, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //        xlWorkBook.Close(true, misValue, misValue);
            //        xlApp.Quit();

            //        Marshal.ReleaseComObject(xlWorkSheet);
            //        Marshal.ReleaseComObject(xlWorkBook);
            //        Marshal.ReleaseComObject(xlApp);

            //        EnviaCorreoArchivoCompras(vEmail.EmailSeleccionado, ListaProductos[0].Fecha, rutaExportacion);
            //    }
            //    catch (Exception ex)
            //    {
            //        xlWorkBook.Close(true, misValue, misValue);
            //        xlApp.Quit();

            //        Marshal.ReleaseComObject(xlWorkSheet);
            //        Marshal.ReleaseComObject(xlWorkBook);
            //        Marshal.ReleaseComObject(xlApp);
            //        MandaExcepcion(ex.Message);
            //    }
            //    MuestraMensaje("¡EXPORTACIÓN ENVIADA!", "CONFIRMACIÓN");
            //    //MessageBox.Show("Excel file created , you can find the file d:\\csharp-Excel.xls");
            //    this.Cursor = Cursors.Default;
            //}
            //this.Cursor = Cursors.Default;
        }
        public void ExportaIngresos(List<EntCatalogoGenerico> ListaIngresos)
        {
            //if (ListaIngresos.Count == 0)
            //    MandaExcepcion("NO SE SELECCIONARON INGRESOS A EXPORTAR");

            //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            //if (xlApp == null)
            //    MandaExcepcion("Excel NO esta instalado apropiadamente!!");

            //SeleccionaEmail vEmail = new Pantallas.SeleccionaEmail();
            //if (vEmail.ShowDialog() == DialogResult.OK)
            //{
            //    this.Cursor = Cursors.WaitCursor;

            //    Workbook xlWorkBook;
            //    Worksheet xlWorkSheet;

            //    object misValue = System.Reflection.Missing.Value;
            //    xlWorkBook = xlApp.Workbooks.Add(misValue);
            //    xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //    //--------------REN|COL----------//
            //    xlWorkSheet.Cells[1, 1] = "ID";
            //    xlWorkSheet.Cells[1, 2] = "PRODUCTOID";
            //    xlWorkSheet.Cells[1, 3] = "CODIGO";
            //    xlWorkSheet.Cells[1, 4] = "DESCRIPCION";
            //    xlWorkSheet.Cells[1, 5] = "TIPOPRODUCTOID";
            //    xlWorkSheet.Cells[1, 6] = "TIPOPRODUCTO";
            //    xlWorkSheet.Cells[1, 7] = "SERIE";
            //    xlWorkSheet.Cells[1, 8] = "PRECIOCOSTO";
            //    xlWorkSheet.Cells[1, 9] = "PRECIOVENTA";

            //    xlWorkSheet.Cells[1, 10] = "INGRESOID";
            //    xlWorkSheet.Cells[1, 11] = "INGRESO";
            //    xlWorkSheet.Cells[1, 12] = "FECHAINGRESO";

            //    xlWorkSheet.Cells[1, 13] = "EMPRESAID";
            //    //xlWorkSheet.Cells[1, 14] = "PROVEEDORID";

            //    int ren = 2;
            //    foreach (EntCatalogoGenerico i in ListaIngresos)
            //    {
            //        //EntCatalogoGenerico ingreso = ObtieneListaGenericaFromGV(gvIngresos)[gvIngresos.CurrentRow.Index];
            //        //CargaEntradas(ingreso.Id);

            //        List<EntProducto> listaProductos = new BusProductos().ObtieneProductosDetallePorIngreso(i.Id);
            //        //foreach (EntProducto p in listaProductos)
            //        //{
            //        //    new BusProductos().ObtieneProductosDetallePorIngreso(ingreso.Id).Where(P => P.ProductoId == p.Id).ToList();
            //        //}


            //        foreach (EntProducto p in listaProductos)
            //        {
            //            xlWorkSheet.Cells[ren, 1] = p.Id;           // "ID";
            //            xlWorkSheet.Cells[ren, 2] = p.ProductoId;   // "PRODUCTOID";
            //            xlWorkSheet.Cells[ren, 3] = p.Codigo;       // "CODIGO";
            //            xlWorkSheet.Cells[ren, 4] = p.Descripcion;  // "DESCRIPCION";

            //            xlWorkSheet.Cells[ren, 5] = p.TipoProductoId;   //"TIPOPRODUCTOID";
            //            xlWorkSheet.Cells[ren, 6] = p.TipoProducto;     //"TIPOPRODUCTO";
            //            xlWorkSheet.Cells[ren, 7] = p.Serie;            // "SERIE";
            //            xlWorkSheet.Cells[ren, 8] = p.PrecioCosto;      // "PRECIOCOSTO";
            //            xlWorkSheet.Cells[ren, 9] = p.PrecioVenta;      // "PRECIOVENTA";
            //            xlWorkSheet.Cells[ren, 10] = p.IngresoId;       // "INGRESOID";
            //            xlWorkSheet.Cells[ren, 11] = i.Descripcion;     // "INGRESO";
            //            xlWorkSheet.Cells[ren, 12] = i.Fecha;           // "INGRESOFECHA";
            //            xlWorkSheet.Cells[ren, 13] = p.EmpresaId;       // "EMPRESAID";
            //            ren++;
            //        }
            //    }

            //    EntCatalogoGenerico ingreso = ListaIngresos[0];
            //    string rutaExportacion = string.Format(@"c:\TIIM\EXPORTACIONES\Entradas {0:yyyy-MM-dd}.xls", ingreso.Fecha);

            //    try
            //    {
            //        xlWorkBook.SaveAs(rutaExportacion, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //        xlWorkBook.Close(true, misValue, misValue);
            //        xlApp.Quit();

            //        Marshal.ReleaseComObject(xlWorkSheet);
            //        Marshal.ReleaseComObject(xlWorkBook);
            //        Marshal.ReleaseComObject(xlApp);

            //        EnviaCorreoArchivoCompras(vEmail.EmailSeleccionado, ingreso.Fecha, rutaExportacion);
            //    }
            //    catch (Exception ex)
            //    {
            //        xlWorkBook.Close(true, misValue, misValue);
            //        xlApp.Quit();

            //        Marshal.ReleaseComObject(xlWorkSheet);
            //        Marshal.ReleaseComObject(xlWorkBook);
            //        Marshal.ReleaseComObject(xlApp);
            //        MandaExcepcion(ex.Message);
            //    }
            //    MuestraMensaje("¡EXPORTACIÓN ENVIADA!", "CONFIRMACIÓN");
            //    //MessageBox.Show("Excel file created , you can find the file d:\\csharp-Excel.xls");
            //    this.Cursor = Cursors.Default;
            //}
            //this.Cursor = Cursors.Default;
        }
        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                ExportaIngresos(ObtieneListaGenericaFromGV(gvIngresos).Where(P => P.Estatus).ToList());
                //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                //if (xlApp == null)
                //    MandaExcepcion("Excel NO esta instalado apropiadamente!!");

                //SeleccionaEmail vEmail = new Pantallas.SeleccionaEmail();
                //if (vEmail.ShowDialog() == DialogResult.OK)
                //{
                //    this.Cursor = Cursors.WaitCursor;

                //    Workbook xlWorkBook;
                //    Worksheet xlWorkSheet;

                //    object misValue = System.Reflection.Missing.Value;
                //    xlWorkBook = xlApp.Workbooks.Add(misValue);
                //    xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                //    //--------------REN|COL----------//
                //    xlWorkSheet.Cells[1, 1] = "ID";
                //    xlWorkSheet.Cells[1, 2] = "PRODUCTOID";
                //    xlWorkSheet.Cells[1, 3] = "CODIGO";
                //    xlWorkSheet.Cells[1, 4] = "DESCRIPCION";
                //    xlWorkSheet.Cells[1, 5] = "TIPOPRODUCTOID";
                //    xlWorkSheet.Cells[1, 6] = "TIPOPRODUCTO";
                //    xlWorkSheet.Cells[1, 7] = "SERIE";
                //    xlWorkSheet.Cells[1, 8] = "PRECIOCOSTO";
                //    xlWorkSheet.Cells[1, 9] = "PRECIOVENTA";

                //    xlWorkSheet.Cells[1, 10] = "INGRESOID";
                //    xlWorkSheet.Cells[1, 11] = "INGRESO";
                //    xlWorkSheet.Cells[1, 12] = "FECHAINGRESO";

                //    int ren = 2;
                //    foreach (EntCatalogoGenerico i in ObtieneListaGenericaFromGV(gvIngresos).Where(P => P.Estatus).ToList())
                //    {
                //        //EntCatalogoGenerico ingreso = ObtieneListaGenericaFromGV(gvIngresos)[gvIngresos.CurrentRow.Index];
                //        //CargaEntradas(ingreso.Id);

                //        List<EntProducto> listaProductos = new BusProductos().ObtieneProductosDetallePorIngreso(i.Id);
                //        //foreach (EntProducto p in listaProductos)
                //        //{
                //        //    new BusProductos().ObtieneProductosDetallePorIngreso(ingreso.Id).Where(P => P.ProductoId == p.Id).ToList();
                //        //}


                //        foreach (EntProducto p in listaProductos)
                //        {
                //            xlWorkSheet.Cells[ren, 1] = p.Id;           // "ID";
                //            xlWorkSheet.Cells[ren, 2] = p.ProductoId;   // "PRODUCTOID";
                //            xlWorkSheet.Cells[ren, 3] = p.Codigo;       // "CODIGO";
                //            xlWorkSheet.Cells[ren, 4] = p.Descripcion;  // "DESCRIPCION";

                //            xlWorkSheet.Cells[ren, 5] = p.TipoProductoId;  //"TIPOPRODUCTOID";
                //            xlWorkSheet.Cells[ren, 6] = p.TipoProducto;  //"TIPOPRODUCTO";
                //            xlWorkSheet.Cells[ren, 7] = p.Serie;        // "SERIE";
                //            xlWorkSheet.Cells[ren, 8] = p.PrecioCosto;  // "PRECIOCOSTO";
                //            xlWorkSheet.Cells[ren, 9] = p.PrecioVenta;  // "PRECIOVENTA";
                //            xlWorkSheet.Cells[ren, 10] = p.IngresoId;    // "INGRESOID";
                //            xlWorkSheet.Cells[ren, 11] = i.Descripcion;  // "INGRESO";
                //            xlWorkSheet.Cells[ren, 12] = i.Fecha;       // "INGRESOFECHA";
                //            ren++;
                //        }
                //    }

                //    EntCatalogoGenerico ingreso = ObtieneCatalogoGenericoFromGV(gvIngresos);
                //    string rutaExportacion = string.Format(@"c:\TIIM\EXPORTACIONES\Entradas {0:yyyy-MM-dd}.xls", ingreso.Fecha);

                //    try
                //    {
                //        xlWorkBook.SaveAs(rutaExportacion, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                //        xlWorkBook.Close(true, misValue, misValue);
                //        xlApp.Quit();

                //        Marshal.ReleaseComObject(xlWorkSheet);
                //        Marshal.ReleaseComObject(xlWorkBook);
                //        Marshal.ReleaseComObject(xlApp);

                //        EnviaCorreoArchivo(vEmail.EmailSeleccionado, ingreso.Fecha, rutaExportacion);
                //    }
                //    catch (Exception ex)
                //    {
                //        xlWorkBook.Close(true, misValue, misValue);
                //        xlApp.Quit();

                //        Marshal.ReleaseComObject(xlWorkSheet);
                //        Marshal.ReleaseComObject(xlWorkBook);
                //        Marshal.ReleaseComObject(xlApp);
                //        MandaExcepcion(ex.Message);
                //    }
                //    MuestraMensaje("¡EXPORTACIÓN ENVIADA!", "CONFIRMACIÓN");
                //    //MessageBox.Show("Excel file created , you can find the file d:\\csharp-Excel.xls");
                //    this.Cursor = Cursors.Default;
                //}
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnExportaProducto_Click(object sender, EventArgs e)
        {
            try
            {
                EntCatalogoGenerico ingreso = ObtieneListaGenericaFromGV(gvIngresos)[gvIngresos.CurrentRow.Index];
                EntProducto productoSeleccionado = ObtieneProductoFromGV(gvProductos);

                ExportaProductos(new BusProductos().ObtieneProductosDetallePorIngreso(ingreso.Id).Where(P => P.ProductoId == productoSeleccionado.Id).ToList());
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnBuscaSerie_Click(object sender, EventArgs e)
        {
            try
            {
                //EntCatalogoGenerico ingreso=new BusProductos().ObtieneIngreso(Program.EmpresaSeleccionada.Id,txtBuscaSerie.Text);
                //if (ingreso.Id <= 0)
                //    MandaExcepcion("NO SE ENCONTRO LA SERIE EN LA EMPRESA: " + Program.EmpresaSeleccionada.Nombre);

                //rdoEntradasPorDia.Checked = true;
                //dtpEntradasFechaDia.Value = ingreso.Fecha;
                //List<EntCatalogoGenerico> lstIngresos = new List<EntCatalogoGenerico>();
                //lstIngresos.Add(ingreso);

                //gvIngresos.DataSource = lstIngresos;
                //CargaProductosEntrada(ingreso.Id);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbTipoMovimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                if (cmbTipoMovimiento.Focused)
                    btnRefrescarEntradas.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
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

        private void cmbTipoProductoFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbTipoProductoFiltro.Focus())
                    btnRefrescarInventario.PerformClick();
                CargaProductos(Program.EmpresaSeleccionada.CompañiaId, this.TipoProductoId);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtDetalle_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (gvIngresos.Rows.Count > 0)
                    gvIngresos.DataSource = this.ListaMovimientos.Where(P => P.Detalle.ToUpper().Contains(txtDetalle.Text.ToUpper())).ToList();
                else
                    gvIngresos.DataSource = this.ListaMovimientos;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        List<EntProducto> ListaProductos = new List<EntProducto>();
        EntProducto ProductoSeleccionado;
        public void CargaProductos(int EmpresaId, int TipoProductoId)
        {
            this.ListaProductos = new BusProductos().ObtieneProductosPorTipo(EmpresaId, TipoProductoId)
                                                                        .OrderBy(P => P.Descripcion).ToList();
            gvProductos.DataSource = this.ListaProductos;
        }
        void MuestraDatosProductoKardex(EntProducto ProductoMuestra)
        {
            lbProductoKardex.Text = ProductoMuestra.Codigo + " - " + ProductoMuestra.Descripcion;
        }
        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                FiltroProductos vProducto = new FiltroProductos();
                vProducto.CargaProductosDetalle(this.ListaProductos);
                if (vProducto.ShowDialog() == DialogResult.OK)
                {
                    if (vProducto.ProductoSeleccionado == null)
                        throw new Exception("Producto NO encontrado");

                    this.ProductoSeleccionado = vProducto.ProductoSeleccionado;
                    MuestraDatosProductoKardex(this.ProductoSeleccionado);

                    OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProducto);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnRefrescarProductos_Click(object sender, EventArgs e)
        {
            try
            {
                CargaProductos(Program.EmpresaSeleccionada.CompañiaId, this.TipoProductoId);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtBuscaProductoCodigo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
                    gvProductosBusqueda.Visible = false;
                else
                {
                    gvProductosBusqueda.DataSource = this.ListaProductos.Where(P => P.Codigo.ToUpper().Contains(((TextBox)sender).Text.ToUpper())).ToList();
                    gvProductosBusqueda.Visible = true;
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtBuscaProducto_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBuscaProductoCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    this.ProductoSeleccionado = ObtieneProductoFromGV(gvProductosBusqueda);

                    if (this.ProductoSeleccionado != null)
                        MuestraDatosProductoKardex(this.ProductoSeleccionado);

                    gvProductosBusqueda.Visible = false;
                    LimpiaTextBox(tpKardex);
                }
                else if (e.KeyChar == (char)Keys.Escape)
                {
                    gvProductosBusqueda.Visible = false;
                    LimpiaTextBox(tpKardex);
                }

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
    }
}
