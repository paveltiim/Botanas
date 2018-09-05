using AiresEntidades;
using AiresNegocio;
using AiresUtilerias;
using Microsoft.Reporting.WinForms;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aires.Pantallas
{
    public partial class ReportesGenerales : FormBase
    {
        public ReportesGenerales()
        {
            InitializeComponent();
        }

        void CargaAñosCmb(ComboBox cmbAños)
        {
            List<EntCatalogoGenerico> años = new List<EntCatalogoGenerico>();
            for (int x = DateTime.Today.Year; x >= AñoInicio; x--)
            {
                EntCatalogoGenerico año = new EntCatalogoGenerico();
                año.Descripcion = x.ToString();
                años.Add(año);
            }
            cmbAños.DataSource = años;
        }
        public void CargaCmbClientes()
        {
            List<EntCliente> lst = new BusClientes().ObtieneClientes().OrderBy(P => P.Nombre).ToList();
            lst.Insert(0, new EntCliente() { Id = -1, Nombre = "-TODOS-" });
            cmbClientesVentas.DataSource = lst;

            //CargaClientesEnPantallas();
        }

        #region Metodos Ventas
        List<EntPedido> ListaPedidos;
        ///// <summary>
        ///// Carga cmbAñoGastos,cmbAñosManoDeObra,cmbAñosMateriales.
        ///// </summary>
        //void CargaAños()
        //{
        //    List<EntCatalogoGenerico> años = new List<EntCatalogoGenerico>();
        //    for (int x = DateTime.Today.Year; x >= AñoInicio; x--)
        //    {
        //        EntCatalogoGenerico año = new EntCatalogoGenerico();
        //        año.Descripcion = x.ToString();
        //        años.Add(año);
        //    }
        //    cmbAñoVentas.DataSource = años;
        //    ////cmbAñoDepositos.DataSource = años;
        //    //EntCatalogoGenerico[] añosCopy = new EntCatalogoGenerico[años.Count];
        //    //años.CopyTo(añosCopy);
        //    //cmbAñoDepositos.DataSource = añosCopy;
        //    //cmbAñosManoDeObra.DataSource = añosCopy;
        //    //cmbAñosMateriales.DataSource = añosCopy;
        //}

        public void CargaRvVentas(DateTime FechaDesde, DateTime FechaHasta)
        {
            //ListaPedidos = new BusPedidos().ObtienePedidosClientesPorFechas(FechaDesde,FechaHasta);
            entPedidoBindingSource.DataSource = ListaPedidos;
            rvVentas.RefreshReport();
            rvVentasPorCliente.RefreshReport();

            ReportParameter parm = new ReportParameter("Total", "0");
            rvGraficoVentasClientes.LocalReport.SetParameters(parm);

            rvGraficoVentasClientes.RefreshReport();

            CargaRvVentasProductos(FechaDesde, FechaHasta);
        }

        public void CargaRvVentas(DateTime FechaDesde, DateTime FechaHasta, int FiltroClienteId)
        {
            entPedidoBindingSource.DataSource = ListaPedidos.Where(P => P.ClienteId == FiltroClienteId);
            ReportParameter parm = new ReportParameter("Total", "0");
            rvGraficoVentasClientes.LocalReport.SetParameters(parm);

            rvGraficoVentasClientes.RefreshReport();

            CargaRvVentasProductos(FechaDesde, FechaHasta, FiltroClienteId);
        }
        List<EntProducto> ListaProductosVentas;
        public void CargaRvVentasProductos(DateTime FechaDesde, DateTime FechaHasta)
        {
            ListaProductosVentas = new BusProductos().ObtieneProductosPorFechaPedido(FechaDesde, FechaHasta);
            EntProductoBindingSource.DataSource = ListaProductosVentas;
            rvGraficaVentasProductos.RefreshReport();
            rvVentasPorProducto.RefreshReport();
        }
        public void CargaRvVentasProductos(DateTime FechaDesde, DateTime FechaHasta, int FiltroClienteId)
        {
            List<EntProducto> listaProducto = new BusProductos().ObtieneProductosPorFechaPedidoCliente(FechaDesde, FechaHasta, FiltroClienteId);
            EntProductoBindingSource.DataSource = listaProducto;
            rvGraficaVentasProductos.RefreshReport();
            rvVentasPorProducto.RefreshReport();
        }
        public void CargaRvVentasProductos(List<EntProducto> ListaProductosVentas, int FiltroProductoId)
        {
            EntProductoBindingSource.DataSource = ListaProductosVentas.Where(P => P.Id == FiltroProductoId);
            rvGraficaVentasProductos.RefreshReport();
            rvVentasPorProducto.RefreshReport();
        }
        public void CargaRvVentasProductos(List<EntProducto> ListaProductosVentas)
        {
            EntProductoBindingSource.DataSource = ListaProductosVentas;
            rvGraficaVentasProductos.RefreshReport();
            rvVentasPorProducto.RefreshReport();
        }
        #endregion
        #region Metodos Compras
        ///// <summary>
        ///// Carga cmbAñoGastos,cmbAñosManoDeObra,cmbAñosMateriales.
        ///// </summary>
        //void CargaAñosCompras()
        //{
        //    List<EntCatalogoGenerico> años = new List<EntCatalogoGenerico>();
        //    for (int x = DateTime.Today.Year; x >= AñoInicio; x--)
        //    {
        //        EntCatalogoGenerico año = new EntCatalogoGenerico();
        //        año.Descripcion = x.ToString();
        //        años.Add(año);
        //    }
        //    cmbAñosCompras.DataSource = años;
        //}
        public void CargaRvCompras(DateTime FechaDesde, DateTime FechaHasta)
        {
            ////List<EntEmpresa> ListaEmpresasGastos = new BusEmpresas().ObtieneEmpresasGastos(FechaDesde, FechaHasta);
            ////EntEmpresaBindingSource.DataSource = ListaEmpresasGastos;
            //rvCompras.RefreshReport();
            //rvComprasPorProveedor.RefreshReport();
        }
        #endregion
        #region Metodos Entradas
        //void CargaAñosEntradas()
        //{
        //    List<EntCatalogoGenerico> años = new List<EntCatalogoGenerico>();
        //    for (int x = DateTime.Today.Year; x >= AñoInicio; x--)
        //    {
        //        EntCatalogoGenerico año = new EntCatalogoGenerico();
        //        año.Descripcion = x.ToString();
        //        años.Add(año);
        //    }
        //    cmbAñoEntradas.DataSource = años;
        //}
        public void CargaCmbProductos()
        {
            List<EntProducto> lst = new BusProductos().ObtieneProductosCodigos().OrderBy(P => P.Descripcion).ToList();
            lst.Insert(0, new EntProducto() { Id = -1, Descripcion = "-TODOS-" });
            cmbFiltroProductosEntradas.DataSource = lst;
            cmbFiltroProductosVentas.DataSource = lst;
        }
        List<EntProducto> ListaProductosEntradas;
        public void CargaRvEntradas(DateTime FechaDesde, DateTime FechaHasta)
        {
            ListaProductosEntradas = new BusProductos().ObtieneProductosPorFechaIngreso(FechaDesde, FechaHasta);
            EntProductoBindingSource.DataSource = ListaProductosEntradas;
            rvEntradas.RefreshReport();
            rvEntradas.RefreshReport();

            EntProductoBindingSource.DataSource = new BusProductos().ObtieneProductosDetallePorFechaIngreso(FechaDesde, FechaHasta); ;
            rvEntradasDetalle.RefreshReport();
        }
        public void CargaRvEntradas(List<EntProducto> ListaProductosEntradas, int FiltroProductoId)
        {
            EntProductoBindingSource.DataSource = ListaProductosEntradas.Where(P => P.Id == FiltroProductoId);
            rvEntradas.RefreshReport();
        }
        public void CargaRvEntradas(List<EntProducto> ListaProductosEntradas)
        {
            EntProductoBindingSource.DataSource = ListaProductosEntradas;
            rvEntradas.RefreshReport();
        }
        #endregion
        #region Metodos Clientes
        void CargaRvDeudaClientes()
        {
            //List<EntPedido> list = new BusPedidos().ObtienePedidosClientesCredito();
            //entPedidoBindingSource.DataSource = list;
            //rvDeudaClientes.RefreshReport();
        }
        void CargaRvDeudaClientes(DateTime FechaDesde, DateTime FechaHasta)
        {
            //List<EntPedido> list = new BusPedidos().ObtienePedidosClientesCredito(FechaDesde,FechaHasta);
            //entPedidoBindingSource.DataSource = list;
            //rvDeudaClientes.RefreshReport();

            ////CargaRvPagosClientes(FechaDesde, FechaHasta);
        }
        void CargaRvDeudaClientes(DateTime FechaLimite)
        {
            //List<EntPedido> list = new BusPedidos().ObtienePedidosClientesCredito(FechaLimite);
            //entPedidoBindingSource.DataSource = list;
            //rvDeudaClientes.RefreshReport();

            ////CargaRvPagosClientes(new DateTime(2016,1,1), FechaLimite);
        }
        void CargaRvPagosClientes(DateTime FechaDesde, DateTime FechaHasta)
        {
            entPedidoBindingSource.DataSource = new BusPedidos().ObtienePagosClientes(FechaDesde, FechaHasta);
            rvPagosClientes.RefreshReport();
        }

        #endregion
        #region Metodos Depositos
        public void CargaRvDepositos(DateTime FechaDesde, DateTime FechaHasta)
        {
            //List<EntDeposito> listaDepositos= new BusDepositos().ObtieneDepositos(FechaDesde, FechaHasta);
            //EntDepositoBindingSource.DataSource = listaDepositos;
            //rvDepositos.RefreshReport();
        }
        #endregion
        public void CargaRvInventario(int EmpresaId, DateTime FechaHasta)
        {
            List<EntProducto> listaProductos = new BusProductos().ObtieneProductosDetalleHastaFecha(EmpresaId, FechaHasta).OrderBy(P => P.Descripcion).ToList();
            EntProductoBindingSource.DataSource = listaProductos;
            rvProductosHastaFecha.RefreshReport();

            //int EmpresaId = Program.EmpresaSeleccionada.Id;
            //ReportParameter parmEmpresa;
            //parmEmpresa = new ReportParameter("Empresa", Program.EmpresaSeleccionada.Nombre);

            //if (tcReportesInventario.SelectedIndex == 0)
            //{
            //    List<EntProducto> listaProductos;
            //    if (chkSoloConExistencia.Checked)
            //        listaProductos = new BusProductos().ObtieneProductosDetalle(EmpresaId);
            //    else
            //        listaProductos = new BusProductos().ObtieneProductos(EmpresaId);
            //    //gvPedidos.DataSource = ListaPedidos;
            //    EntProductoBindingSource.DataSource = listaProductos;

            //    rvInventario.LocalReport.SetParameters(parmEmpresa);
            //    rvInventario.RefreshReport();
            //}
            //else if (tcReportesInventario.SelectedIndex == 1)
            //{
            //    EntProductoBindingSource.DataSource = new BusProductos().ObtieneProductosDetalle(EmpresaId);

            //    rvInventarioDetalle.LocalReport.SetParameters(parmEmpresa);
            //    rvInventarioDetalle.RefreshReport();
        }

        public void CargaRvInventario(DateTime FechaHasta, string FiltroCodigoProducto, string FiltroProducto)
        {
            List<EntProducto> listaProductos = new BusProductos().ObtieneProductosDetalleHastaFecha(FechaHasta).Where(P => P.Codigo.ToUpper().Contains(FiltroCodigoProducto.ToUpper()) && P.Descripcion.ToUpper().Contains(FiltroProducto.ToUpper())).OrderBy(P => P.Descripcion).ToList();
            EntProductoBindingSource.DataSource = listaProductos;
            rvProductosHastaFecha.RefreshReport();
        }

        public void CargaRvDeudaProveedores()
        {
            List<EntEmpresa> list = new BusEmpresas().ObtieneEmpresasGastosDeuda();
            EntEmpresaBindingSource.DataSource = list;
            rvDeudaProveedores.RefreshReport();
        }
        public void CargaRvDeudaProveedores(DateTime FechaDesde, DateTime FechaHasta)
        {
            List<EntEmpresa> list = new BusEmpresas().ObtieneEmpresasGastosDeuda(FechaDesde, FechaHasta);
            EntEmpresaBindingSource.DataSource = list;
            rvDeudaProveedores.RefreshReport();

            //CargaRvPagosProveedores(FechaDesde, FechaHasta);
        }
        void CargaRvPagosProveedores(DateTime FechaDesde, DateTime FechaHasta)
        {
            EntEmpresaBindingSource.DataSource = new BusEmpresas().ObtienePagosEmpresas(FechaDesde, FechaHasta);
            rvPagosProveedores.RefreshReport();
        }
        void CargaRvNotasCreditoProveedores(DateTime FechaDesde, DateTime FechaHasta)
        {
            //EntEmpresaBindingSource.DataSource = new BusEmpresas().ObtieneNotasCreditoEmpresas(FechaDesde, FechaHasta);
            //rvNotasCreditoProveedores.RefreshReport();
        }

        //void CargaEmpresas()
        //{
        //    List<EntEmpresa> lst = new BusEmpresas().ObtieneEmpresas();
        //    lst.Add(new EntEmpresa() { Id = -1, Descripcion = "-TODAS-" });
        //    cmbEmpresasVentas.DataSource = lst;
        //}
        public void CargaEmpresas()
        {
            if (Program.UsuarioSeleccionado.Id > 1)
                ListaEmpresas = new BusEmpresas().ObtieneEmpresas().Where(P => P.UsuarioId == Program.UsuarioSeleccionado.Id).ToList();
            else
                ListaEmpresas = new BusEmpresas().ObtieneEmpresas();

            Program.CambiaEmpresa = false;
            cmbEmpresas.DataSource = ListaEmpresas;
            Program.CambiaEmpresa = true;

            //CargaClientesEnPantallas();
        }
        List<EntEmpresa> ListaEmpresas;

        private void Reportes_Load(object sender, EventArgs e)
        {
            try
            {
                CargaEmpresas();
                //CargaCmbClientes();

                if (Program.EmpresaSeleccionada == null)
                    Program.EmpresaSeleccionada = SeleccionaEmpresa();

                cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);

                //CargaAñosCmb(cmbAñoVentas);
                //cmbMesesVentas.SelectedIndex = DateTime.Today.Month - 1;
                CargaAñosCmb(cmbAñosInventario);
                cmbMesesInventario.SelectedIndex = DateTime.Today.Month - 1;

                //CargaRvVentas(new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1)));

                //CargaCmbProductos();

                tcReportes.TabPages.Remove(tpVentas);
                tcReportes.TabPages.Remove(tpEntradas);
                tcReportes.TabPages.Remove(tpClientes);
                tcReportes.TabPages.Remove(tpProveedores);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            this.rvGraficoVentasClientes.RefreshReport();
            this.rvGraficaVentasProductos.RefreshReport();
        }
        private void tcReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (tcReportes.SelectedIndex == 1)//PESTAÑA COMPRAS
                //{
                //    if (!ReporteComprasCargado)
                //    {
                //        CargaAñosCmb(cmbAñosCompras);
                //        cmbMesesCompras.SelectedIndex = DateTime.Today.Month - 1;

                //        ReporteComprasCargado = true;
                //    }
                //}
                //else 
                if (tcReportes.SelectedIndex == 1)//PESTAÑA ENTRADAS
                {
                    if (!ReporteEntradasCargado)
                    {
                        CargaAñosCmb(cmbAñoEntradas);
                        cmbMesesEntradas.SelectedIndex = DateTime.Today.Month - 1;

                        btnRefrescaEntradas.PerformClick();
                        ReporteEntradasCargado = true;
                    }
                }
                else if (tcReportes.SelectedIndex == 2)//PESTAÑA INVENTARIO
                {
                    if (!ReporteInventarioCargado)
                    {
                        CargaAñosCmb(cmbAñosInventario);
                        cmbMesesInventario.SelectedIndex = DateTime.Today.Month - 1;

                        ReporteInventarioCargado = true;
                    }
                }
                else if (tcReportes.SelectedIndex == 3)//PESTAÑA DEUDA CLIENTES
                {
                    if (!ReporteDeudaClientesCargado)
                    {
                        CargaAñosCmb(cmbAñosDeudaClientes);
                        cmbMesesDeudaCliente.SelectedIndex = DateTime.Today.Month - 1;

                        ReporteDeudaClientesCargado = true;
                    }
                }
                else if (tcReportes.SelectedIndex == 4)//PESTAÑA DEUDA PROVEEDORES
                {
                    if (!ReporteDeudaProveedoresCargado)
                    {
                        CargaAñosCmb(cmbAñosDeudaProveedores);
                        cmbMesesDeudaProveedores.SelectedIndex = DateTime.Today.Month - 1;

                        ReporteDeudaProveedoresCargado = true;
                    }
                }
                //else if (tcReportes.SelectedIndex == 6)//PESTAÑA DEPÖSITOS
                //{
                //    if (!ReporteDepositosCargado)
                //    {
                //        CargaAñosCmb(cmbAñosDepositos);
                //        cmbMesesDepositos.SelectedIndex = DateTime.Today.Month - 1;

                //        ReporteDepositosCargado = true;
                //    }
                //}
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        bool ReporteComprasCargado, ReporteEntradasCargado, ReporteDeudaClientesCargado, ReporteDeudaProveedoresCargado, ReporteInventarioCargado, ReporteDepositosCargado;

        #region Eventos Pestaña Ventas
        DateTime FechaDesdeVentas
        {
            get
            {
                if (rdoPorMesVentas.Checked)
                    return new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), ConvierteTextoAInteger(cmbMesesVentas.SelectedIndex.ToString()) + 1, 1);
                else
                    return dtpFechaDesdeVentas.Value.Date;
            }
        }
        DateTime FechaDesdeHasta
        {
            get
            {
                if (rdoPorMesVentas.Checked)
                    return new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1));
                else
                    return dtpFechaDesdeVentas.Value.Date;
            }
        }
        private void btnRefrescarReporteVentas_Click(object sender, EventArgs e)
        {
            if (rdoPorMesVentas.Checked)
            {
                if (cmbMesesVentas.SelectedIndex >= 0)
                    CargaRvVentas(new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1)));
            }
            else
                CargaRvVentas(dtpFechaDesdeVentas.Value.Date, dtpFechaDesdeVentas.Value.Date);
        }
        private void rdoPorMesVentas_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((RadioButton)sender).Checked)
                {
                    rdoPorDiaVentas.Checked = false;
                    pnlPorMesVentas.Enabled = true;
                    pnlPorDiaVentas.Enabled = false;

                    CargaRvVentas(new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1)));
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void rdoPorSemanaVentas_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((RadioButton)sender).Checked)
                {
                    rdoPorMesVentas.Checked = false;
                    pnlPorMesVentas.Enabled = false;
                    pnlPorDiaVentas.Enabled = true;
                    CargaRvVentas(dtpFechaDesdeVentas.Value.Date, dtpFechaDesdeVentas.Value.Date);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        #endregion
        #region Eventos Pestaña Compras
        //private void rdoPorMesCompras_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (((RadioButton)sender).Checked)
        //        {
        //            rdoPorDiaCompras.Checked = false;
        //            pnlPorMesCompras.Enabled = true;
        //            pnlPorDiaCompras.Enabled = false;

        //            CargaRvCompras(new DateTime(ConvierteTextoAInteger(cmbAñosCompras.Text), cmbMesesCompras.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñosCompras.Text), cmbMesesCompras.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñosCompras.Text), cmbMesesCompras.SelectedIndex + 1)));
        //        }
        //    }
        //    catch (Exception ex) { MuestraExcepcion(ex); }
        //}

        //private void rdoPorDiaCompras_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (((RadioButton)sender).Checked)
        //        {
        //            rdoPorMesCompras.Checked = false;
        //            pnlPorMesCompras.Enabled = false;
        //            pnlPorDiaCompras.Enabled = true;
        //            CargaRvCompras(dtpFechaCompras.Value.Date, dtpFechaCompras.Value.Date);
        //        }
        //    }
        //    catch (Exception ex) { MuestraExcepcion(ex); }
        //}
        //private void btnRefrescaCompras_Click(object sender, EventArgs e)
        //{
        //    if (rdoPorMesCompras.Checked)
        //    {
        //        if (cmbMesesCompras.SelectedIndex >= 0)
        //            CargaRvCompras(new DateTime(ConvierteTextoAInteger(cmbAñosCompras.Text), cmbMesesCompras.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñosCompras.Text), cmbMesesCompras.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñosCompras.Text), cmbMesesCompras.SelectedIndex + 1)));
        //    }
        //    else
        //        CargaRvCompras(dtpFechaCompras.Value.Date, dtpFechaCompras.Value.Date);
        //}


        #endregion
        #region Eventos Pestaña Entradas
        private void btnRefrescaEntradas_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdoPorMesEntradas.Checked)
                {
                    if (cmbMesesEntradas.SelectedIndex >= 0)
                        CargaRvEntradas(new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1)));
                }
                else
                    CargaRvEntradas(dtpFechaEntradas.Value.Date, dtpFechaEntradas.Value.Date);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void rdoPorMesEntradas_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((RadioButton)sender).Checked)
                {
                    rdoPorDiaEntradas.Checked = false;
                    pnlPorMesEntradas.Enabled = true;
                    pnlPorDiaEntradas.Enabled = false;

                    CargaRvEntradas(new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoEntradas.Text), cmbMesesEntradas.SelectedIndex + 1)));
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void rdoPorDiaEntradas_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((RadioButton)sender).Checked)
                {
                    rdoPorMesEntradas.Checked = false;
                    pnlPorMesEntradas.Enabled = false;
                    pnlPorDiaEntradas.Enabled = true;
                    CargaRvEntradas(dtpFechaEntradas.Value.Date, dtpFechaEntradas.Value.Date);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        #endregion
        #region Pestaña Clientes
        private void btnRefrescarDeudaClientes_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdoPorMesDeudaClientes.Checked)
                {
                    if (cmbMesesDeudaCliente.SelectedIndex >= 0)
                    {
                        if (tabControl3.SelectedIndex == 0)//Deuda
                            CargaRvDeudaClientes(new DateTime(ConvierteTextoAInteger(cmbAñosDeudaClientes.Text), cmbMesesDeudaCliente.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñosDeudaClientes.Text), cmbMesesDeudaCliente.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñosDeudaClientes.Text), cmbMesesDeudaCliente.SelectedIndex + 1)));
                        else if (tabControl3.SelectedIndex == 1)
                            CargaRvPagosClientes(new DateTime(ConvierteTextoAInteger(cmbAñosDeudaClientes.Text), cmbMesesDeudaCliente.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñosDeudaClientes.Text), cmbMesesDeudaCliente.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñosDeudaClientes.Text), cmbMesesDeudaCliente.SelectedIndex + 1)));
                    }
                }
                else if (rdoTotalDeudaCliente.Checked)
                    CargaRvDeudaClientes();
                else
                    CargaRvDeudaClientes(dtpFechaLimiteClientes.Value.Date);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbMesesDeudaCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnRefrescarDeudaClientes.PerformClick();
                //if (rdoPorMesDeudaClientes.Checked)
                //{
                //    if (cmbMesesDeudaCliente.SelectedIndex >= 0)
                //        CargaRvDeudaClientes(new DateTime(ConvierteTextoAInteger(cmbAñosDeudaClientes.Text), cmbMesesDeudaCliente.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñosDeudaClientes.Text), cmbMesesDeudaCliente.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñosDeudaClientes.Text), cmbMesesDeudaCliente.SelectedIndex + 1)));
                //}
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void rdoTotalDeudaCliente_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((RadioButton)sender).Checked)
                {
                    rdoPorMesDeudaClientes.Checked = false;
                    pnlPorMesDeudaClientes.Enabled = false;
                    rdoAlDiaClientes.Checked = false;
                    pnlAlDiaClientes.Enabled = false;
                    CargaRvDeudaClientes();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void rdoPorMesDeudaClientes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((RadioButton)sender).Checked)
                {
                    rdoTotalDeudaCliente.Checked = false;
                    pnlPorMesDeudaClientes.Enabled = true;
                    rdoAlDiaClientes.Checked = false;
                    pnlAlDiaClientes.Enabled = false;

                    //CargaRvDeudaClientes(new DateTime(ConvierteTextoAInteger(cmbAñosDeudaClientes.Text), cmbMesesDeudaCliente.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñosDeudaClientes.Text), cmbMesesDeudaCliente.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñosDeudaClientes.Text), cmbMesesDeudaCliente.SelectedIndex + 1)));
                    btnRefrescarDeudaClientes.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        #endregion
        #region Eventos Pestaña Inventarios

        private void rdoPorMesInventario_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((RadioButton)sender).Checked)
                {
                    rdoAlDiaInventario.Checked = false;
                    pnlPorMesInventario.Enabled = true;
                    pnlAlDiaInventario.Enabled = false;

                    btnRefrescarInventario.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void rdoAlDiaInventario_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((RadioButton)sender).Checked)
                {
                    rdoPorMesInventario.Checked = false;
                    pnlPorMesInventario.Enabled = false;
                    pnlAlDiaInventario.Enabled = true;

                    btnRefrescarInventario.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void dtpAlDiaInventario_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                CargaRvInventario(Program.EmpresaSeleccionada.Id, dtpAlDiaInventario.Value.Date);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        #endregion
        private void btnRefrescaDeudaProveedores_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdoPorMesDeudaProveedores.Checked)
                {
                    if (cmbMesesDeudaProveedores.SelectedIndex >= 0)
                    {
                        if (tabControl5.SelectedIndex == 0)
                            CargaRvDeudaProveedores(new DateTime(ConvierteTextoAInteger(cmbAñosDeudaProveedores.Text), cmbMesesDeudaProveedores.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñosDeudaProveedores.Text), cmbMesesDeudaProveedores.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñosDeudaProveedores.Text), cmbMesesDeudaProveedores.SelectedIndex + 1)));

                        else if (tabControl5.SelectedIndex == 1)
                            CargaRvPagosProveedores(new DateTime(ConvierteTextoAInteger(cmbAñosDeudaProveedores.Text), cmbMesesDeudaProveedores.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñosDeudaProveedores.Text), cmbMesesDeudaProveedores.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñosDeudaProveedores.Text), cmbMesesDeudaProveedores.SelectedIndex + 1)));
                        else if (tabControl5.SelectedIndex == 2)
                            CargaRvNotasCreditoProveedores(new DateTime(ConvierteTextoAInteger(cmbAñosDeudaProveedores.Text), cmbMesesDeudaProveedores.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñosDeudaProveedores.Text), cmbMesesDeudaProveedores.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñosDeudaProveedores.Text), cmbMesesDeudaProveedores.SelectedIndex + 1)));
                    }
                }
                else
                    CargaRvDeudaProveedores();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbEmpresasVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //EntEmpresa empresaSeleccionada= ObtieneEmpresaFromCmb(cmbEmpresasVentas);

                //if (empresaSeleccionada.Id >= 0)
                //{
                //    CargaRvVentasProductos
                //}
                if (!string.IsNullOrEmpty(cmbAñoVentas.Text))
                {
                    EntCliente clienteSeleccionado = ObtieneClienteFromCmb(cmbClientesVentas);

                    if (clienteSeleccionado.Id >= 0)
                    {
                        CargaRvVentas(FechaDesdeVentas, FechaDesdeHasta, clienteSeleccionado.Id);
                    }
                    else
                        CargaRvVentas(FechaDesdeVentas, FechaDesdeHasta);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbFiltroProductosEntradas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tcReportes.SelectedIndex == 2)//VENTAS
                {
                    EntProducto productoSeleccionado = ObtieneProductoFromCmb(cmbFiltroProductosEntradas);
                    if (productoSeleccionado.Id > -1)
                        CargaRvEntradas(ListaProductosEntradas, productoSeleccionado.Id);
                    else
                        CargaRvEntradas(ListaProductosEntradas);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void rdoAlDiaClientes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((RadioButton)sender).Checked)
                {
                    rdoTotalDeudaCliente.Checked = false;
                    rdoPorMesDeudaClientes.Checked = false;
                    pnlPorMesDeudaClientes.Enabled = false;
                    pnlAlDiaClientes.Enabled = true;

                    CargaRvDeudaClientes(dtpFechaLimiteClientes.Value.Date);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnRefrescarInventario_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdoPorMesInventario.Checked)
                {
                    if (cmbMesesInventario.SelectedIndex >= 0)
                        CargaRvInventario(Program.EmpresaSeleccionada.Id,new DateTime(ConvierteTextoAInteger(cmbAñosInventario.Text), cmbMesesInventario.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñosInventario.Text), cmbMesesInventario.SelectedIndex + 1)));
                }
                //else if (rdoTotalDeudaCliente.Checked)
                //    CargaRvDeudaClientes();
                else
                    CargaRvInventario(Program.EmpresaSeleccionada.Id, dtpAlDiaInventario.Value.Date);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControl3.SelectedIndex == 0)
                {
                    pnlAlDiaClientes.Enabled = true;
                    rdoAlDiaClientes.Enabled = true;
                    rdoTotalDeudaCliente.Enabled = true;
                }
                else
                    if (tabControl3.SelectedIndex == 1)
                {
                    rdoPorMesDeudaClientes.Checked = true;
                    pnlAlDiaClientes.Enabled = false;
                    rdoAlDiaClientes.Enabled = false;
                    rdoTotalDeudaCliente.Enabled = false;
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void tabControl5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControl5.SelectedIndex == 0)
                {
                    rdoTotalDeudaProveedores.Enabled = true;
                }
                else
                    if (tabControl5.SelectedIndex == 1)
                {
                    rdoPorMesDeudaProveedores.Checked = true;
                    rdoTotalDeudaProveedores.Enabled = false;
                }
                else
                    if (tabControl5.SelectedIndex == 2)
                {
                    rdoPorMesDeudaProveedores.Checked = true;
                    rdoTotalDeudaProveedores.Enabled = false;
                }
                btnRefrescaDeudaProveedores.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        DateTime ObtieneFechaUltimoDiaMes(int Mes, int Año)
        {
            return new DateTime(Año, Mes, DateTime.DaysInMonth(Año, Mes));
        }
        DateTime ObtieneFechaPrimerDiaMes(int Mes, int Año)
        {
            return new DateTime(Año, Mes, 1);
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtBuscaProductoCodigo.Text) || !string.IsNullOrWhiteSpace(txtBuscaProducto.Text))
                    CargaRvInventario(ObtieneFechaUltimoDiaMes(cmbMesesInventario.SelectedIndex + 1, ConvierteTextoAInteger(cmbAñosInventario.Text)), txtBuscaProductoCodigo.Text, txtBuscaProducto.Text);
                else
                    btnRefrescarInventario.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbFiltroProductosVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tcReportes.SelectedIndex == 0)
                {
                    //List<EntProducto> ListaProducto = new BusProductos().ObtieneProductosPorFechaPedido(FechaDesde, FechaHasta);
                    EntProducto productoSeleccionado = ObtieneProductoFromCmb(cmbFiltroProductosVentas);
                    if (productoSeleccionado.Id > -1)
                        CargaRvVentasProductos(ListaProductosVentas, productoSeleccionado.Id);
                    else
                        CargaRvVentasProductos(ListaProductosVentas);
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

        private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Program.CambiaEmpresa)
                {
                    Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                    btnRefrescarInventario.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        //private void btnRefrescaDepositos_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (rdoPorMesDepositos.Checked)
        //        {
        //            if (cmbMesesDepositos.SelectedIndex >= 0)
        //                CargaRvDepositos(ObtieneFechaPrimerDiaMes(cmbMesesDepositos.SelectedIndex + 1, ConvierteTextoAInteger(cmbAñosDepositos.Text)), ObtieneFechaUltimoDiaMes(cmbMesesDepositos.SelectedIndex + 1, ConvierteTextoAInteger(cmbAñosDepositos.Text)));
        //        }
        //        //else if (rdoTotalDeudaCliente.Checked)
        //        //    CargaRvDeudaClientes();
        //        //else
        //        //    CargaRvDeudaClientes(dtpFechaLimiteClientes.Value.Date);
        //    }
        //    catch (Exception ex) { MuestraExcepcion(ex); }
        //}

        private void txtBuscaProductoCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((Keys)e.KeyChar == Keys.Enter)
                {
                    btnBuscarProducto.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }

        }

        private void dtpFechaLimiteClientes_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoAlDiaClientes.Checked)
                {
                    CargaRvDeudaClientes(dtpFechaLimiteClientes.Value.Date);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbMesesDeudaProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (rdoPorMesDeudaProveedores.Checked)
                //{
                //    if (cmbMesesDeudaProveedores.SelectedIndex >= 0)
                //        CargaRvDeudaProveedores(new DateTime(ConvierteTextoAInteger(cmbAñosDeudaProveedores.Text), cmbMesesDeudaProveedores.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñosDeudaProveedores.Text), cmbMesesDeudaProveedores.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñosDeudaProveedores.Text), cmbMesesDeudaProveedores.SelectedIndex + 1)));
                //}
                btnRefrescaDeudaProveedores.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void rdoTotalDeudaProveedores_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((RadioButton)sender).Checked)
                {
                    rdoPorMesDeudaProveedores.Checked = false;
                    pnlPorMesDeudaProveedores.Enabled = false;
                    CargaRvDeudaProveedores();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void rdoPorMesDeudaProveedores_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((RadioButton)sender).Checked)
                {
                    rdoTotalDeudaProveedores.Checked = false;
                    pnlPorMesDeudaProveedores.Enabled = true;

                    //CargaRvDeudaProveedores(new DateTime(ConvierteTextoAInteger(cmbAñosDeudaProveedores.Text), cmbMesesDeudaProveedores.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñosDeudaProveedores.Text), cmbMesesDeudaProveedores.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñosDeudaProveedores.Text), cmbMesesDeudaProveedores.SelectedIndex + 1)));
                    btnRefrescaDeudaProveedores.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
    }
}
