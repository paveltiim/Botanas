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
using Microsoft.Office.Interop.Excel;

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
        /// 
        /// 
        /// </summary>
        public void CargaInventario()
        {
            int EmpresaId = Program.EmpresaSeleccionada.Id;
            ReportParameter parmEmpresa;
            parmEmpresa = new ReportParameter("Empresa", Program.EmpresaSeleccionada.Nombre);

            if (tcReportesInventario.SelectedIndex == 0)
            {
                List<EntProducto> listaProductos;
                if (chkSoloConExistencia.Checked)
                    listaProductos = new BusProductos().ObtieneProductosDetalle(EmpresaId);
                else
                    listaProductos = new BusProductos().ObtieneProductos(EmpresaId);
                //gvPedidos.DataSource = ListaPedidos;
                EntProductoBindingSource.DataSource = listaProductos;

                rvInventario.LocalReport.SetParameters(parmEmpresa);
                rvInventario.RefreshReport();
            }
            else if (tcReportesInventario.SelectedIndex == 1)
            {
                EntProductoBindingSource.DataSource = new BusProductos().ObtieneProductosDetalle(EmpresaId);

                rvInventarioDetalle.LocalReport.SetParameters(parmEmpresa);
                rvInventarioDetalle.RefreshReport();
            }
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        public void CargaEntradas(DateTime FechaDesde, DateTime FechaHasta)
        {
            List<EntProducto> listaProductos = new BusProductos().ObtieneProductosDetalleTodos();
            listaProductos = listaProductos.Where(P => P.Fecha >= FechaDesde && P.Fecha <= FechaHasta.AddDays(1)).ToList();
            //gvPedidos.DataSource = ListaPedidos;
            EntProductoBindingSource.DataSource = listaProductos;
            gvProductos.DataSource = listaProductos.OrderByDescending(P => P.Fecha).ToList();
            txtCantidadTotalEntradas.Text = listaProductos.Sum(P => P.Cantidad).ToString();
            txtPrecioCostoTotalEntradas.Text = FormatoMoney(listaProductos.Sum(P => P.PrecioCosto));
            txtPrecioVentaTotalEntradas.Text = FormatoMoney(listaProductos.Sum(P => P.Precio));
            rvEntradas.RefreshReport();
        }
        public void CargaEntradas(int IngresoId)
        {
            //List<EntProducto> listaProductos = new BusProductos().ObtieneProductosDetalleTodos();
            List<EntProducto> listaProductos = new BusProductos().ObtieneProductosPorIngreso(IngresoId);
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

        /// <summary>
        /// Carga cmbAñoGastos,cmbAñosManoDeObra,cmbAñosMateriales.
        /// </summary>
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
        public void CargaEmpresas()
        {
            ListaEmpresas = new BusEmpresas().ObtieneEmpresas();

            Program.CambiaEmpresa = false;
            cmbEmpresas.DataSource = ListaEmpresas;
            Program.CambiaEmpresa = true;
        }

        private void Inventarios_Load(object sender, EventArgs e)
        {
            try
            {
                //InicializaPantalla();
                CargaEmpresas();

                if (Program.EmpresaSeleccionada == null)
                    Program.EmpresaSeleccionada = SeleccionaEmpresa();

                if (Program.EmpresaSeleccionada != null)
                {
                    CargaAños();

                    cmbMesesEntradas.SelectedIndex = DateTime.Today.Month - 1;
                    dtpEntradasFechaDesde.Value = DateTime.Today;

                    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);

                    ////CargaEntradas(new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1)));
                    //CargaIngresosProductos(new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1)));
                    //CargaInventario();

                    ////this.rvInventario.RefreshReport();
                    ////this.rvEntradas.RefreshReport();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                CargaInventario();

                if (rdoEntradasPorMes.Checked)
                    CargaIngresosProductos(new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1)));
                else
                    CargaIngresosProductos(dtpEntradasFechaDesde.Value.Date, dtpEntradasFechaDesde.Value.Date);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void rdoEntradasPorMes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((RadioButton)sender).Checked)
                {
                    rdoEntradasPorSemana.Checked = false;
                    pnlEntradasPorMes.Enabled = true;
                    pnlEntradasPorSemana.Enabled = false;
                    //CargaEntradas(new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1)));
                    CargaIngresosProductos(new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1)));
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void rdoEntradasPorSemana_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((RadioButton)sender).Checked)
                {
                    rdoEntradasPorMes.Checked = false;
                    pnlEntradasPorMes.Enabled = false;
                    pnlEntradasPorSemana.Enabled = true;
                    //CargaEntradas(dtpEntradasFechaDesde.Value.Date, dtpEntradasFechaDesde.Value.Date);
                    CargaIngresosProductos(dtpEntradasFechaDesde.Value.Date, dtpEntradasFechaDesde.Value.Date);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbMesesGastos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (rdoEntradasPorSemana.Checked)
                //    CargaEntradas(dtpEntradasFechaDesde.Value.Date, dtpEntradasFechaDesde.Value.Date);
                //else if (rdoEntradasPorMes.Checked)
                //{
                //    if (cmbMesesEntradas.SelectedIndex >= 0)
                //        CargaEntradas(new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1)));
                //}
                //else
                //    CargaEntradas(new DateTime(1990, 1, 1), new DateTime(2990, 1, 1));
                if (rdoEntradasPorSemana.Checked)
                    CargaIngresosProductos(dtpEntradasFechaDesde.Value.Date, dtpEntradasFechaDesde.Value.Date);
                else if (rdoEntradasPorMes.Checked)
                {
                    if (cmbMesesEntradas.SelectedIndex >= 0)
                        CargaIngresosProductos(new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1)));
                }
                else
                    CargaIngresosProductos(new DateTime(1990, 1, 1), new DateTime(2990, 1, 1));
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbAñoGastos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (rdoEntradasPorSemana.Checked)
                //    CargaEntradas(dtpEntradasFechaDesde.Value.Date, dtpEntradasFechaDesde.Value.Date);
                //else if (rdoEntradasPorMes.Checked)
                //{
                //    if (cmbMesesEntradas.SelectedIndex >= 0)
                //        CargaEntradas(new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1)));
                //}
                //else
                //    CargaEntradas(new DateTime(1990, 1, 1), new DateTime(2990, 1, 1));
                if (rdoEntradasPorSemana.Checked)
                    CargaIngresosProductos(dtpEntradasFechaDesde.Value.Date, dtpEntradasFechaDesde.Value.Date);
                else if (rdoEntradasPorMes.Checked)
                {
                    if (cmbMesesEntradas.SelectedIndex >= 0)
                        CargaIngresosProductos(new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1)));
                }
                else
                    CargaIngresosProductos(new DateTime(1990, 1, 1), new DateTime(2990, 1, 1));
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void dtpGastosFechaDesde_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoEntradasPorSemana.Checked)
                    CargaIngresosProductos(dtpEntradasFechaDesde.Value.Date, dtpEntradasFechaDesde.Value.Date);
                //CargaEntradas(dtpEntradasFechaDesde.Value.Date, dtpEntradasFechaDesde.Value.Date);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void tcInventarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try {
            //    if (tcInventarios.SelectedIndex == 1)
            //    {
            //        CargaInventario();
            //    }
            //}
            //catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvIngresos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                EntCatalogoGenerico ingreso = ObtieneListaGenericaFromGV(gvIngresos)[gvIngresos.CurrentRow.Index];
                CargaEntradas(ingreso.Id);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                EntCatalogoGenerico ingreso = ObtieneListaGenericaFromGV(gvIngresos)[gvIngresos.CurrentRow.Index];

                EntProducto productoSeleccionado = ObtieneProductoFromGV(gvProductos);

                MuestraProductosDetalle vProductosDetalles = new MuestraProductosDetalle(new BusProductos().ObtieneProductosDetallePorIngreso(ingreso.Id).Where(P => P.ProductoId == productoSeleccionado.Id).ToList());
                vProductosDetalles.Show();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtBusquedaSerieHistorial_TextChanged(object sender, EventArgs e)
        {
            try
            {
                gvProductosDetalle.DataSource = new BusProductos().ObtieneProductoDetalleHistorial(txtBusquedaSerieHistorial.Text);
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

                    btnRefrescar.PerformClick();
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

                    btnRefrescar.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnEliminarProductosDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                EntCatalogoGenerico ingresoSeleccionado = ObtieneCatalogoGenericoFromGV(gvIngresos);
                EntProducto productoSeleccionado = ObtieneProductoFromGV(gvProductos);

                if (MuestraMensajeYesNo(string.Format("¿Desea Eliminar el Producto seleccionado: {0} - {1}?", productoSeleccionado.Codigo, productoSeleccionado.Descripcion), "CONFIRMACIÓN ELIMINAR") == DialogResult.Yes)
                {
                    List<EntProducto> productosDetalle = new BusProductos().ObtieneProductosDetallePorIngreso(ingresoSeleccionado.Id).Where(P => P.ProductoId == productoSeleccionado.Id).ToList();

                    foreach (EntProducto p in productosDetalle)
                    {
                        p.EstatusId = 0;
                        new BusProductos().ActualizaEstatusProductoDetalle(p);
                    }
                    new BusProductos().AumentaProducto(productoSeleccionado.Id, -Convert.ToInt32(productoSeleccionado.Cantidad));

                    CargaEntradas(ingresoSeleccionado.Id);
                }
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
                    btnRefrescar.PerformClick();
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
                        btnRefrescar.PerformClick();
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
                CargaInventario();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        /// <summary>
        /// Muestra Ventana emergente para Confirmar Envio de Correo, llama los métodos Imprime.AsignaValoresParametrosImpresionDatosCliente y Imprime.AsignaValoresParametrosImpresion.
        /// Envia correo electronico por medio de la clase UtiCorreo.
        /// </summary>
        void EnviaCorreo(string Email, DateTime Fecha, string PathArchivos)
        {
            Cursor.Current = Cursors.WaitCursor;

            List<string> archivosAdjuntos = new List<string>();

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PathArchivos);
            foreach (System.IO.FileInfo file in dir.GetFiles())
            {
                archivosAdjuntos.Add(file.FullName);
            }

            EntEmpresa empresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
            string asunto = "COMPRAS DE PRODUCTOS -" + Fecha.ToString("dd MMM yyyy");
            string mensaje = "IMPORTAR ARCHIVO AL SISTEMA 'SERDAN SOFTWARE'";
            new UtiCorreo().EnviaCorreo("" + asunto, new List<string>() { Email }, mensaje, archivosAdjuntos);

            MessageBox.Show("El Correo se ha Enviado correctamente, a la dirección -" + Email + "-");
            //}
        }
        void EnviaCorreoArchivo(string Email, DateTime Fecha, string PathArchivo)
        {
            Cursor.Current = Cursors.WaitCursor;

            List<string> archivosAdjuntos = new List<string>();

            //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PathArchivos);
            //foreach (System.IO.FileInfo file in dir.GetFiles())
            //{
            //    archivosAdjuntos.Add(file.FullName);
            //}
            System.IO.FileInfo file = new System.IO.FileInfo(PathArchivo);
            archivosAdjuntos.Add(file.FullName);

            string asunto = "COMPRAS DE PRODUCTOS -" + Fecha.ToString("dd MMM yyyy");
            string mensaje = "IMPORTAR ARCHIVO AL SISTEMA 'SERDAN SOFTWARE'. \n\n Abrir sistema 'SERDAN SOFTWARE'27-->Ir a 'Sincronización' en menu-->En Pestaña 'Importar Entradas'-->Seleccionar archivo descargado desde correo (archivo Excel adjunto). Se mostrarán productos a Importar-->Presionar botón Importar";
            new UtiCorreo().EnviaCorreo("" + asunto, new List<string>() { Email }, mensaje, archivosAdjuntos);

            MessageBox.Show("El Correo se ha Enviado correctamente, a la dirección -" + Email + "-");
            //}
        }
        public void ExportaProductos(List<EntProducto> ListaProductos) {
            if (ListaProductos.Count == 0)
                MandaExcepcion("NO SE SELECCIONARON PRODUCTOS A EXPORTAR");

            this.Cursor = Cursors.WaitCursor;
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
                MandaExcepcion("Excel NO esta instalado apropiadamente!!");

            SeleccionaEmail vEmail = new Pantallas.SeleccionaEmail();
            if (vEmail.ShowDialog() == DialogResult.OK)
            {

                Workbook xlWorkBook;
                Worksheet xlWorkSheet;

                object misValue = System.Reflection.Missing.Value;
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                //--------------REN|COL----------//
                xlWorkSheet.Cells[1, 1] = "ID";
                xlWorkSheet.Cells[1, 2] = "PRODUCTOID";
                xlWorkSheet.Cells[1, 3] = "CODIGO";
                xlWorkSheet.Cells[1, 4] = "DESCRIPCION";
                xlWorkSheet.Cells[1, 5] = "TIPOPRODUCTOID";
                xlWorkSheet.Cells[1, 6] = "TIPOPRODUCTO";
                xlWorkSheet.Cells[1, 7] = "SERIE";
                xlWorkSheet.Cells[1, 8] = "PRECIOCOSTO";
                xlWorkSheet.Cells[1, 9] = "PRECIOVENTA";

                xlWorkSheet.Cells[1, 10] = "INGRESOID";
                xlWorkSheet.Cells[1, 11] = "INGRESO";
                xlWorkSheet.Cells[1, 12] = "FECHAINGRESO";

                xlWorkSheet.Cells[1, 13] = "EMPRESAID";
                //xlWorkSheet.Cells[1, 14] = "PROVEEDORID";

                int ren = 2;
                //foreach (EntCatalogoGenerico i in ObtieneListaGenericaFromGV(gvIngresos))
                //{

                    //List<EntProducto> listaProductos = new BusProductos().ObtieneProductosDetallePorIngreso(i.Id);

                    foreach (EntProducto p in ListaProductos)
                    {
                        xlWorkSheet.Cells[ren, 1] = p.Id;           // "ID";
                        xlWorkSheet.Cells[ren, 2] = p.ProductoId;   // "PRODUCTOID";
                        xlWorkSheet.Cells[ren, 3] = p.Codigo;       // "CODIGO";
                        xlWorkSheet.Cells[ren, 4] = p.Descripcion;  // "DESCRIPCION";

                        xlWorkSheet.Cells[ren, 5] = p.TipoProductoId;   //"TIPOPRODUCTOID";
                        xlWorkSheet.Cells[ren, 6] = p.TipoProducto;     //"TIPOPRODUCTO";
                        xlWorkSheet.Cells[ren, 7] = p.Serie;            // "SERIE";
                        xlWorkSheet.Cells[ren, 8] = p.PrecioCosto;      // "PRECIOCOSTO";
                        xlWorkSheet.Cells[ren, 9] = p.PrecioVenta;      // "PRECIOVENTA";
                        xlWorkSheet.Cells[ren, 10] = p.IngresoId;       // "INGRESOID";
                        xlWorkSheet.Cells[ren, 11] = p.Ingreso;         // "INGRESO";
                        xlWorkSheet.Cells[ren, 12] = p.Fecha;           // "INGRESOFECHA";

                        xlWorkSheet.Cells[ren, 13] = p.EmpresaId;           // "EMPRESAID";
                    ren++;
                    }
                //}

                //EntCatalogoGenerico ingreso = ObtieneCatalogoGenericoFromGV(gvIngresos);
                string rutaExportacion = string.Format(@"c:\TIIM\EXPORTACIONES\Entradas {0:yyyy-MM-dd}.xls", ListaProductos[0].Fecha);

                try
                {
                    xlWorkBook.SaveAs(rutaExportacion, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();

                    Marshal.ReleaseComObject(xlWorkSheet);
                    Marshal.ReleaseComObject(xlWorkBook);
                    Marshal.ReleaseComObject(xlApp);

                    EnviaCorreoArchivo(vEmail.EmailSeleccionado, ListaProductos[0].Fecha, rutaExportacion);
                }
                catch (Exception ex)
                {
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();

                    Marshal.ReleaseComObject(xlWorkSheet);
                    Marshal.ReleaseComObject(xlWorkBook);
                    Marshal.ReleaseComObject(xlApp);
                    MandaExcepcion(ex.Message);
                }
                MuestraMensaje("¡EXPORTACIÓN ENVIADA!", "CONFIRMACIÓN");
                //MessageBox.Show("Excel file created , you can find the file d:\\csharp-Excel.xls");
                this.Cursor = Cursors.Default;
            }
            this.Cursor = Cursors.Default;
        }
        public void ExportaProductos(List<EntProducto> ListaProductos, int EmpresaId)
        {
            if (ListaProductos.Count == 0)
                MandaExcepcion("NO SE SELECCIONARON PRODUCTOS A EXPORTAR");

            this.Cursor = Cursors.WaitCursor;
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
                MandaExcepcion("Excel NO esta instalado apropiadamente!!");

            SeleccionaEmail vEmail = new Pantallas.SeleccionaEmail();
            if (vEmail.ShowDialog() == DialogResult.OK)
            {

                Workbook xlWorkBook;
                Worksheet xlWorkSheet;

                object misValue = System.Reflection.Missing.Value;
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                //--------------REN|COL----------//
                xlWorkSheet.Cells[1, 1] = "ID";
                xlWorkSheet.Cells[1, 2] = "PRODUCTOID";
                xlWorkSheet.Cells[1, 3] = "CODIGO";
                xlWorkSheet.Cells[1, 4] = "DESCRIPCION";
                xlWorkSheet.Cells[1, 5] = "TIPOPRODUCTOID";
                xlWorkSheet.Cells[1, 6] = "TIPOPRODUCTO";
                xlWorkSheet.Cells[1, 7] = "SERIE";
                xlWorkSheet.Cells[1, 8] = "PRECIOCOSTO";
                xlWorkSheet.Cells[1, 9] = "PRECIOVENTA";

                xlWorkSheet.Cells[1, 10] = "INGRESOID";
                xlWorkSheet.Cells[1, 11] = "INGRESO";
                xlWorkSheet.Cells[1, 12] = "FECHAINGRESO";

                xlWorkSheet.Cells[1, 13] = "EMPRESAID";
                //xlWorkSheet.Cells[1, 14] = "PROVEEDORID";

                int ren = 2;
                //foreach (EntCatalogoGenerico i in ObtieneListaGenericaFromGV(gvIngresos))
                //{

                //List<EntProducto> listaProductos = new BusProductos().ObtieneProductosDetallePorIngreso(i.Id);

                foreach (EntProducto p in ListaProductos)
                {
                    xlWorkSheet.Cells[ren, 1] = p.Id;           // "ID";
                    xlWorkSheet.Cells[ren, 2] = p.ProductoId;   // "PRODUCTOID";
                    xlWorkSheet.Cells[ren, 3] = p.Codigo;       // "CODIGO";
                    xlWorkSheet.Cells[ren, 4] = p.Descripcion;  // "DESCRIPCION";

                    xlWorkSheet.Cells[ren, 5] = p.TipoProductoId;   //"TIPOPRODUCTOID";
                    xlWorkSheet.Cells[ren, 6] = p.TipoProducto;     //"TIPOPRODUCTO";
                    xlWorkSheet.Cells[ren, 7] = p.Serie;            // "SERIE";
                    xlWorkSheet.Cells[ren, 8] = p.PrecioCosto;      // "PRECIOCOSTO";
                    xlWorkSheet.Cells[ren, 9] = p.PrecioVenta;      // "PRECIOVENTA";
                    xlWorkSheet.Cells[ren, 10] = p.IngresoId;       // "INGRESOID";
                    xlWorkSheet.Cells[ren, 11] = p.Ingreso;         // "INGRESO";
                    xlWorkSheet.Cells[ren, 12] = p.Fecha;           // "INGRESOFECHA";

                    xlWorkSheet.Cells[ren, 13] = EmpresaId;           // "EMPRESAID";
                    ren++;
                }
                //}

                //EntCatalogoGenerico ingreso = ObtieneCatalogoGenericoFromGV(gvIngresos);
                string rutaExportacion = string.Format(@"c:\TIIM\EXPORTACIONES\Entradas {0:yyyy-MM-dd}.xls", ListaProductos[0].Fecha);

                try
                {
                    xlWorkBook.SaveAs(rutaExportacion, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();

                    Marshal.ReleaseComObject(xlWorkSheet);
                    Marshal.ReleaseComObject(xlWorkBook);
                    Marshal.ReleaseComObject(xlApp);

                    EnviaCorreoArchivo(vEmail.EmailSeleccionado, ListaProductos[0].Fecha, rutaExportacion);
                }
                catch (Exception ex)
                {
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();

                    Marshal.ReleaseComObject(xlWorkSheet);
                    Marshal.ReleaseComObject(xlWorkBook);
                    Marshal.ReleaseComObject(xlApp);
                    MandaExcepcion(ex.Message);
                }
                MuestraMensaje("¡EXPORTACIÓN ENVIADA!", "CONFIRMACIÓN");
                //MessageBox.Show("Excel file created , you can find the file d:\\csharp-Excel.xls");
                this.Cursor = Cursors.Default;
            }
            this.Cursor = Cursors.Default;
        }
        public void ExportaIngresos(List<EntCatalogoGenerico> ListaIngresos)
        {
            if (ListaIngresos.Count == 0)
                MandaExcepcion("NO SE SELECCIONARON INGRESOS A EXPORTAR");

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
                MandaExcepcion("Excel NO esta instalado apropiadamente!!");

            SeleccionaEmail vEmail = new Pantallas.SeleccionaEmail();
            if (vEmail.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;

                Workbook xlWorkBook;
                Worksheet xlWorkSheet;

                object misValue = System.Reflection.Missing.Value;
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                //--------------REN|COL----------//
                xlWorkSheet.Cells[1, 1] = "ID";
                xlWorkSheet.Cells[1, 2] = "PRODUCTOID";
                xlWorkSheet.Cells[1, 3] = "CODIGO";
                xlWorkSheet.Cells[1, 4] = "DESCRIPCION";
                xlWorkSheet.Cells[1, 5] = "TIPOPRODUCTOID";
                xlWorkSheet.Cells[1, 6] = "TIPOPRODUCTO";
                xlWorkSheet.Cells[1, 7] = "SERIE";
                xlWorkSheet.Cells[1, 8] = "PRECIOCOSTO";
                xlWorkSheet.Cells[1, 9] = "PRECIOVENTA";

                xlWorkSheet.Cells[1, 10] = "INGRESOID";
                xlWorkSheet.Cells[1, 11] = "INGRESO";
                xlWorkSheet.Cells[1, 12] = "FECHAINGRESO";

                xlWorkSheet.Cells[1, 13] = "EMPRESAID";
                //xlWorkSheet.Cells[1, 14] = "PROVEEDORID";

                int ren = 2;
                foreach (EntCatalogoGenerico i in ListaIngresos)
                {
                    //EntCatalogoGenerico ingreso = ObtieneListaGenericaFromGV(gvIngresos)[gvIngresos.CurrentRow.Index];
                    //CargaEntradas(ingreso.Id);

                    List<EntProducto> listaProductos = new BusProductos().ObtieneProductosDetallePorIngreso(i.Id);
                    //foreach (EntProducto p in listaProductos)
                    //{
                    //    new BusProductos().ObtieneProductosDetallePorIngreso(ingreso.Id).Where(P => P.ProductoId == p.Id).ToList();
                    //}


                    foreach (EntProducto p in listaProductos)
                    {
                        xlWorkSheet.Cells[ren, 1] = p.Id;           // "ID";
                        xlWorkSheet.Cells[ren, 2] = p.ProductoId;   // "PRODUCTOID";
                        xlWorkSheet.Cells[ren, 3] = p.Codigo;       // "CODIGO";
                        xlWorkSheet.Cells[ren, 4] = p.Descripcion;  // "DESCRIPCION";

                        xlWorkSheet.Cells[ren, 5] = p.TipoProductoId;   //"TIPOPRODUCTOID";
                        xlWorkSheet.Cells[ren, 6] = p.TipoProducto;     //"TIPOPRODUCTO";
                        xlWorkSheet.Cells[ren, 7] = p.Serie;            // "SERIE";
                        xlWorkSheet.Cells[ren, 8] = p.PrecioCosto;      // "PRECIOCOSTO";
                        xlWorkSheet.Cells[ren, 9] = p.PrecioVenta;      // "PRECIOVENTA";
                        xlWorkSheet.Cells[ren, 10] = p.IngresoId;       // "INGRESOID";
                        xlWorkSheet.Cells[ren, 11] = i.Descripcion;     // "INGRESO";
                        xlWorkSheet.Cells[ren, 12] = i.Fecha;           // "INGRESOFECHA";
                        xlWorkSheet.Cells[ren, 13] = p.EmpresaId;       // "EMPRESAID";
                        ren++;
                    }
                }

                EntCatalogoGenerico ingreso = ListaIngresos[0];
                string rutaExportacion = string.Format(@"c:\TIIM\EXPORTACIONES\Entradas {0:yyyy-MM-dd}.xls", ingreso.Fecha);

                try
                {
                    xlWorkBook.SaveAs(rutaExportacion, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();

                    Marshal.ReleaseComObject(xlWorkSheet);
                    Marshal.ReleaseComObject(xlWorkBook);
                    Marshal.ReleaseComObject(xlApp);

                    EnviaCorreoArchivo(vEmail.EmailSeleccionado, ingreso.Fecha, rutaExportacion);
                }
                catch (Exception ex)
                {
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();

                    Marshal.ReleaseComObject(xlWorkSheet);
                    Marshal.ReleaseComObject(xlWorkBook);
                    Marshal.ReleaseComObject(xlApp);
                    MandaExcepcion(ex.Message);
                }
                MuestraMensaje("¡EXPORTACIÓN ENVIADA!", "CONFIRMACIÓN");
                //MessageBox.Show("Excel file created , you can find the file d:\\csharp-Excel.xls");
                this.Cursor = Cursors.Default;
            }
            this.Cursor = Cursors.Default;
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
                EntCatalogoGenerico ingreso=new BusProductos().ObtieneIngreso(Program.EmpresaSeleccionada.Id,txtBuscaSerie.Text);
                if (ingreso.Id <= 0)
                    MandaExcepcion("NO SE ENCONTRO LA SERIE EN LA EMPRESA: " + Program.EmpresaSeleccionada.Nombre);

                rdoEntradasPorSemana.Checked = true;
                dtpEntradasFechaDesde.Value = ingreso.Fecha;
                List<EntCatalogoGenerico> lstIngresos = new List<EntCatalogoGenerico>();
                lstIngresos.Add(ingreso);

                gvIngresos.DataSource = lstIngresos;
                CargaEntradas(ingreso.Id);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
    }
}
