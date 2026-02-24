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
        List<EntEmpresa> ListaEmpresas;
        public void CargaEmpresas()
        {
            this.ListaEmpresas = new BusEmpresas().ObtieneEmpresas();

            Program.CambiaEmpresa = false;
            cmbEmpresas.DataSource = this.ListaEmpresas;
            Program.CambiaEmpresa = true;
        }
        public void VerificaEmpresa()
        {
            cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }
        public ReportesGenerales()
        {
            InitializeComponent();
        }

        #region Metodos Ventas
        List<EntPedido> ListaPedidos;
        List<EntProducto> ListaProductosVentas;

        bool ReporteEntradasCargado, ReporteDeudaClientesCargado, ReporteDeudaProveedoresCargado, ReporteInventarioCargado, ReporteVentasCargado,
            ReporteAnaliticoCargado, ReporteAuxiliarCargado;

        public void CargaRvComprasVentas(EntEmpresa EmpresaSeleccionada, DateTime FechaDesde, DateTime FechaHasta, string Producto)
        {
            this.ListaPedidos = new BusPedidos().ObtieneReporteComprasVentas(EmpresaSeleccionada.Id, FechaDesde, FechaHasta);
            
            entPedidoBindingSource.DataSource = this.ListaPedidos.Where(P=>P.Detalle.ToUpper().Contains(Producto.ToUpper())).ToList();
            ReportParameter parmEmpresa;
            parmEmpresa = new ReportParameter("Empresa", EmpresaSeleccionada.Nombre);

            rvComprasVentas.LocalReport.SetParameters(parmEmpresa);
            rvComprasVentas.RefreshReport();
            rvComprasVentas.RefreshReport();
        }

        public void CargaRvVentas(EntEmpresa EmpresaSeleccionada, DateTime FechaDesde, DateTime FechaHasta)
        {
            ReportParameter parmEmpresa;
            parmEmpresa = new ReportParameter("Empresa", EmpresaSeleccionada.Nombre);
            if (tcReportesVentas.SelectedIndex < 3)
            {
                this.ListaPedidos = new BusPedidos().ObtienePedidosClientesPorFechas(EmpresaSeleccionada.Id, 
                                                                                                        FechaDesde, FechaHasta);                
                entPedidoBindingSource.DataSource = this.ListaPedidos;
                rvVentas.LocalReport.SetParameters(parmEmpresa);
                rvVentas.RefreshReport();
                rvVentasPorCliente.RefreshReport();
                FiltraVentas(txtProductoDescripcionFiltroVentas.Text, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""));
            }
            else
            {
                this.ListaPedidos = new BusPedidos().ObtienePedidosClientesPorFechasConCostosPorProducto(EmpresaSeleccionada.Id,
                                                                                                        FechaDesde, FechaHasta);
                entPedidoBindingSource1.DataSource = this.ListaPedidos;
                FiltraVentasPorProducto(this.ProductoFiltroId, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""));
            }

            //this.rvVentasPorTrabajador.LocalReport.DataSources.Clear();
            //this.rvVentasPorTrabajador.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas", this.ListaPedidos));
            //rvVentasPorTrabajador.RefreshReport();

            //this.rvVentasPorProducto.LocalReport.DataSources.Clear();
            //this.rvVentasPorProducto.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas", this.ListaPedidos));
            //rvVentasPorProducto.LocalReport.SetParameters(parmEmpresa);
            //rvVentasPorProducto.RefreshReport();

            //this.rvVentasClientesPorProducto.LocalReport.DataSources.Clear();
            //this.rvVentasClientesPorProducto.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas", this.ListaPedidos));
            //rvVentasClientesPorProducto.LocalReport.SetParameters(parmEmpresa);
            //rvVentasClientesPorProducto.RefreshReport();

            //this.rvVentasProductosPorClientes.LocalReport.DataSources.Clear();
            //this.rvVentasProductosPorClientes.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas", this.ListaPedidos));
            //rvVentasProductosPorClientes.LocalReport.SetParameters(parmEmpresa);
            //rvVentasProductosPorClientes.RefreshReport();

        }

        void FiltraVentas(string ProductoFiltroDescripcion, int ClienteFiltroId, string ClienteFiltro, string TrabajadorFiltro)
        {
            ReportParameter parmEmpresa;
            parmEmpresa = new ReportParameter("Empresa", Program.EmpresaSeleccionada.Nombre);

            List<EntPedido> lstFiltro = this.ListaPedidos;

            if (ProductoFiltroDescripcion.Length > 0)
                lstFiltro = lstFiltro.Where(P => P.Detalle.Contains(ProductoFiltroDescripcion)).ToList();
            if (ClienteFiltroId > 0)
                lstFiltro = lstFiltro.Where(P => P.ClienteId == ClienteFiltroId).ToList();
            if (!string.IsNullOrWhiteSpace(ClienteFiltro))
                lstFiltro = lstFiltro.Where(P => (P.NumCliente + " " + P.Cliente).ToUpper().Contains(ClienteFiltro.ToUpper())).ToList();

            if (!string.IsNullOrWhiteSpace(TrabajadorFiltro))
                lstFiltro = lstFiltro.Where(P => P.Trabajador.ToUpper().Contains(TrabajadorFiltro.ToUpper())).ToList();


            this.entPedidoBindingSource.DataSource = lstFiltro;
            rvVentas.LocalReport.SetParameters(parmEmpresa);
            rvVentas.RefreshReport();
            rvVentasPorCliente.RefreshReport();
            this.rvVentasPorTrabajador.LocalReport.DataSources.Clear();
            this.rvVentasPorTrabajador.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas", lstFiltro));
            rvVentasPorTrabajador.RefreshReport();

            //this.rvVentasPorProducto.LocalReport.DataSources.Clear();
            //this.rvVentasPorProducto.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas", lstFiltro));
            //rvVentasPorProducto.LocalReport.SetParameters(parmEmpresa);
            //rvVentasPorProducto.RefreshReport();

            //this.rvVentasClientesPorProducto.LocalReport.DataSources.Clear();
            //this.rvVentasClientesPorProducto.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas", lstFiltro));
            //rvVentasClientesPorProducto.LocalReport.SetParameters(parmEmpresa);
            //rvVentasClientesPorProducto.RefreshReport();

            //this.rvVentasProductosPorClientes.LocalReport.DataSources.Clear();
            //this.rvVentasProductosPorClientes.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas", lstFiltro));
            //rvVentasProductosPorClientes.LocalReport.SetParameters(parmEmpresa);
            //rvVentasProductosPorClientes.RefreshReport();
        }
        void FiltraVentasPorProducto(int ProductoId, int ClienteFiltroId, string ClienteFiltro, string TrabajadorFiltro)
        {
            ReportParameter parmEmpresa;
            parmEmpresa = new ReportParameter("Empresa", Program.EmpresaSeleccionada.Nombre);

            List<EntPedido> lstFiltro = this.ListaPedidos;

            if (ProductoId > 0)
                lstFiltro = lstFiltro.Where(P => P.ProductoId == ProductoId).ToList();
            if (ClienteFiltroId > 0)
                lstFiltro = lstFiltro.Where(P => P.ClienteId == ClienteFiltroId).ToList();
            if (!string.IsNullOrWhiteSpace(ClienteFiltro))
                lstFiltro = lstFiltro.Where(P => (P.NumCliente + " " + P.Cliente).ToUpper().Contains(ClienteFiltro.ToUpper())).ToList();

            if (!string.IsNullOrWhiteSpace(TrabajadorFiltro))
                lstFiltro = lstFiltro.Where(P => P.Trabajador.ToUpper().Contains(TrabajadorFiltro.ToUpper())).ToList();

            this.rvVentasPorProducto.LocalReport.DataSources.Clear();
            this.rvVentasPorProducto.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas", lstFiltro));
            rvVentasPorProducto.LocalReport.SetParameters(parmEmpresa);
            rvVentasPorProducto.RefreshReport();

            this.rvVentasClientesPorProducto.LocalReport.DataSources.Clear();
            this.rvVentasClientesPorProducto.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas", lstFiltro));
            rvVentasClientesPorProducto.LocalReport.SetParameters(parmEmpresa);
            rvVentasClientesPorProducto.RefreshReport();

            this.rvVentasProductosPorClientes.LocalReport.DataSources.Clear();
            this.rvVentasProductosPorClientes.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas", lstFiltro));
            rvVentasProductosPorClientes.LocalReport.SetParameters(parmEmpresa);
            rvVentasProductosPorClientes.RefreshReport();
        }

        public void CargaRvAnalitico(EntEmpresa EmpresaSeleccionada, DateTime FechaDesde, DateTime FechaHasta, int TipoReporte)
        {
            if (TipoReporte == 1)
            {
                EntProductoBindingSource.DataSource = new BusProductos().ObtieneHistorialProductosPrecioCosto(EmpresaSeleccionada.Id,
                                                                            FechaDesde, FechaHasta, EmpresaSeleccionada.Nombre);
                rvAnaliticoPorCostos.RefreshReport();
            }
            else
            {
                EntProductoBindingSource.DataSource = new BusProductos().ObtieneHistorialProductos(EmpresaSeleccionada.Id,
                                                                            FechaDesde, FechaHasta, EmpresaSeleccionada.Nombre);
                rvAnaliticoPorUnidad.RefreshReport();
            }
        }
        public void CargaRvAuxiliar(EntEmpresa EmpresaSeleccionada, EntProducto Producto, DateTime FechaDesde, DateTime FechaHasta, int TipoReporte)
        {
            ReportParameter parm = new ReportParameter("Codigo", Producto.Codigo);
            ReportParameter parm2 = new ReportParameter("Descripcion", Producto.Descripcion);

            if (TipoReporte == 1)//COSTO
            {
                EntProductoBindingSource.DataSource = new BusProductos().ObtieneHistorialProductosDetallePrecioCosto(EmpresaSeleccionada.Id, Producto.Id, Producto.PrecioCosto, FechaDesde, FechaHasta);

                ReportParameter parm3 = new ReportParameter("Existencia", Producto.PrecioCosto.ToString());
                rvAuxiliarPorCostos.LocalReport.SetParameters(parm);
                rvAuxiliarPorCostos.LocalReport.SetParameters(parm2);
                rvAuxiliarPorCostos.LocalReport.SetParameters(parm3);
                rvAuxiliarPorCostos.RefreshReport();
            }
            else
            {
                EntProductoBindingSource.DataSource = new BusProductos().ObtieneHistorialProductosDetalle(EmpresaSeleccionada.Id, Producto.Id, Producto.Existencia, FechaDesde, FechaHasta);

                ReportParameter parm3 = new ReportParameter("Existencia", Producto.Existencia.ToString());
                rvAuxiliarPorUnidad.LocalReport.SetParameters(parm);
                rvAuxiliarPorUnidad.LocalReport.SetParameters(parm2);
                rvAuxiliarPorUnidad.LocalReport.SetParameters(parm3);
                rvAuxiliarPorUnidad.RefreshReport();
            }
        }

        public void CargaRvVentasProductos(List<EntProducto> ListaProductosVentas, int FiltroProductoId)
        {
            EntProductoBindingSource.DataSource = ListaProductosVentas.Where(P => P.Id == FiltroProductoId);
            //rvGraficaVentasProductos.RefreshReport();
            //rvVentasPorProducto.RefreshReport();
        }
        public void CargaRvVentasProductos(List<EntProducto> ListaProductosVentas)
        {
            EntProductoBindingSource.DataSource = ListaProductosVentas;
            //rvGraficaVentasProductos.RefreshReport();
            //rvVentasPorProducto.RefreshReport();
        }
        #endregion
        #region Metodos Compras
        public void CargaRvCompras(DateTime FechaDesde, DateTime FechaHasta)
        {
        }
        #endregion
        #region Metodos Entradas
        public void CargaCmbProductos()
        {
            //List<EntProducto> lst = new BusProductos().ObtieneProductos().OrderBy(P => P.Descripcion).ToList();
            //lst.Insert(0, new EntProducto() { Id = -1, Descripcion = "-TODOS-" });
            //cmbProductosVentas.DataSource = lst;
        }
        public void CargaCmbProductos(int TipoReporte,DateTime FechaDesde, DateTime FechaHasta)
        {
            List<EntProducto> ListaProductosAnaliticos;
            if (TipoReporte==1)//COSTO
                ListaProductosAnaliticos = new BusProductos().ObtieneHistorialProductosPrecioCostoCodigo(Program.EmpresaSeleccionada.Id,FechaDesde, FechaHasta,Program.EmpresaSeleccionada.Nombre);
            else 
                ListaProductosAnaliticos = new BusProductos().ObtieneHistorialProductosCodigo(Program.EmpresaSeleccionada.Id, FechaDesde, FechaHasta, Program.EmpresaSeleccionada.Nombre);

            List<EntProducto> lst = ListaProductosAnaliticos.ToList();
            lst.Insert(0, new EntProducto() { Id = -1, Descripcion = "  -SELECCIONE PRODUCTO-", Codigo = "" });
            cmbProductosHistorialPorProducto.DataSource = lst.OrderBy(P => P.Descripcion).ToList();
        }
        List<EntProducto> ListaProductosEntradas;
        public void CargaRvEntradas(DateTime FechaDesde, DateTime FechaHasta)
        {
            ListaProductosEntradas = new BusProductos().ObtieneProductosPorFechaIngreso(FechaDesde, FechaHasta);
            EntProductoBindingSource.DataSource = ListaProductosEntradas;
            //rvEntradas.RefreshReport();
            //rvEntradas.RefreshReport();

            EntProductoBindingSource.DataSource = new BusProductos().ObtieneProductosDetallePorFechaIngreso(FechaDesde, FechaHasta); ;
            //rvEntradasDetalle.RefreshReport();
        }
        public void CargaRvEntradas(List<EntProducto> ListaProductosEntradas, int FiltroProductoId)
        {
            EntProductoBindingSource.DataSource = ListaProductosEntradas.Where(P => P.Id == FiltroProductoId);
            //rvEntradas.RefreshReport();
        }
        public void CargaRvEntradas(List<EntProducto> ListaProductosEntradas)
        {
            EntProductoBindingSource.DataSource = ListaProductosEntradas;
            //rvEntradas.RefreshReport();
        }
        #endregion
        #region Metodos Clientes
        void CargaRvDeudaClientes()
        {
            entPedidoBindingSource.DataSource = new BusPedidos().ObtienePedidosClientesDeuda();
            rvDeudaCliente.RefreshReport();
        }
        void CargaRvDeudaClientes(DateTime FechaDesde, DateTime FechaHasta)
        {
            entPedidoBindingSource.DataSource = new BusPedidos().ObtienePedidosClientesDeuda()
                                                                                    .Where(P =>P.Fecha >=FechaDesde && P.Fecha <=FechaHasta)
                                                                                                                                                .ToList();
            rvDeudaCliente.RefreshReport();
        }
        void CargaRvDeudaClientes(DateTime FechaLimite)
        {
            entPedidoBindingSource.DataSource = new BusPedidos().ObtienePedidosClientesDeuda()
                                                                                        .Where(P => P.Fecha <= FechaLimite).ToList();
            rvDeudaCliente.RefreshReport();
        }
        void CargaRvPagosClientes(DateTime FechaDesde, DateTime FechaHasta)
        {
            entPedidoBindingSource.DataSource = new BusPedidos().ObtienePagosClientes(Program.EmpresaSeleccionada.Id,FechaDesde, FechaHasta);
            rvPagosClientes.RefreshReport();
        }

        #endregion
        public void CargaRvInventario(int EmpresaId, DateTime FechaHasta)
        {
            this.ListaProductosEntradas = new BusProductos().ObtieneProductosDetalleHastaFecha(EmpresaId, FechaHasta).Where(P => P.TipoProductoId != 2).ToList().OrderBy(P => P.Descripcion).ToList();

            EntProductoBindingSource.DataSource = this.ListaProductosEntradas;
            rvProductosHastaFecha.RefreshReport();
        }

        public void CargaRvInventario(DateTime FechaHasta, string FiltroCodigoProducto, string FiltroProducto)
        {
            List<EntProducto> listaProductos = new BusProductos().ObtieneProductosDetalleHastaFecha(FechaHasta).Where(P => P.Codigo.ToUpper().Contains(FiltroCodigoProducto.ToUpper()) && P.Descripcion.ToUpper().Contains(FiltroProducto.ToUpper())).OrderBy(P => P.Descripcion).ToList();
            EntProductoBindingSource.DataSource = listaProductos;
            rvProductosHastaFecha.RefreshReport();
        }
        public void FiltroInventario(List<EntProducto> ListaProductos,  string FiltroCodigoProducto, string FiltroProducto)
        {
            List<EntProducto> listaProductos = ListaProductos.Where(P => P.Codigo.ToUpper().Contains(FiltroCodigoProducto.ToUpper()) && P.Descripcion.ToUpper().Contains(FiltroProducto.ToUpper())).OrderBy(P => P.Descripcion).ToList();
            
            EntProductoBindingSource.DataSource = listaProductos;
            rvProductosHastaFecha.RefreshReport();
        }
        public void FiltroCompraVenta(List<EntPedido> ListaProductos, string FiltroCodigoProducto, string FiltroProducto)
        {
            List<EntPedido> listaProductos = ListaProductos.Where(P => P.Detalle.ToUpper().Replace("-", "").Trim()
            .Contains((FiltroCodigoProducto.ToUpper().Replace("-", "")
                        + " " + FiltroProducto.ToUpper().Replace("-", "")).Trim())).ToList();

            entPedidoBindingSource.DataSource = listaProductos;
            rvComprasVentas.RefreshReport();
        }
        public void FiltroCompraVenta(List<EntPedido> ListaProductos, string FiltroSerie)
        {
            List<EntPedido> listaProductos = ListaProductos.Where(P => P.RutaFactura.ToUpper().Replace("-", "").Trim()
            .Contains((FiltroSerie.ToUpper().Replace("-", "")).Trim())).ToList();

            entPedidoBindingSource.DataSource = listaProductos;
            rvComprasVentas.RefreshReport();
        }



        private void Reportes_Load(object sender, EventArgs e)
        {
            try
            {
                CargaEmpresas();

                if (Program.EmpresaSeleccionada == null)
                    Program.EmpresaSeleccionada = SeleccionaEmpresa();

                cmbEmpresas.SelectedIndex = 0;//((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
                                
                CargaAñosCmb(cmbAñosInventario);
                CargaAñosCmb(cmbAñosDeudaClientes);
                //CargaAñosCmb(cmbAñosVentas);
                //CargaAñosCmb(cmbAñosAnalitico);
                cmbMesesInventario.SelectedIndex = DateTime.Today.Month - 1;
                cmbMesesDeudaCliente.SelectedIndex = DateTime.Today.Month - 1;
                //cmbMesesVentas.SelectedIndex = DateTime.Today.Month - 1;
                //cmbMesesAnalitico.SelectedIndex = DateTime.Today.Month - 1;

                //tcReportes.TabPages.Remove(tpVentas);
                tcReportes.TabPages.Remove(tpEntradas);
                tcReportes.TabPages.Remove(tpInvenarios);
                //tcReportes.TabPages.Remove(tpClientes);
                tcReportes.TabPages.Remove(tpProveedores);
                cmbProductosFiltroReporteComprasVentas.SelectedIndex = -1;

                base.CargaTrabajadoresPorEmpresa(1, cmbTrabajadores); //EMPRESAID=1:BOTANAS

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tcReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (tcReportes.SelectedIndex == 0)//PESTAÑA ENTRADAS
                //{
                //    if (!ReporteEntradasCargado)
                //    {
                //        CargaAñosCmb(cmbAñoEntradas);
                //        cmbMesesEntradas.SelectedIndex = DateTime.Today.Month - 1;

                //        btnRefrescaEntradas.PerformClick();
                //        ReporteEntradasCargado = true;
                //    }
                //}
                //else 

                //ConvierteTextoAInteger(tcReportes.TabPages["tcReportes"].Tag.ToString())
                if (tcReportes.SelectedTab.Text == "Inventario")//PESTAÑA INVENTARIO
                {
                    if (!ReporteInventarioCargado)
                    {
                        CargaAñosCmb(cmbAñosInventario);
                        cmbMesesInventario.SelectedIndex = DateTime.Today.Month - 1;

                        ReporteInventarioCargado = true;
                    }
                }
                else if (tcReportes.SelectedTab.Text == "Ventas")//PESTAÑA VENTAS
                {
                    if (!ReporteVentasCargado)
                    {
                        CargaAñosCmb(cmbAñosVentas);
                        cmbMesesVentas.SelectedIndex = DateTime.Today.Month - 1;

                        ReporteVentasCargado = true;
                    }
                }
                //else if (tcReportes.SelectedIndex == 3)//PESTAÑA CLIENTES
                //{
                //    if (!ReporteDeudaProveedoresCargado)
                //    {
                //        CargaAñosCmb(cmbAñosDeudaProveedores);
                //        cmbMesesDeudaProveedores.SelectedIndex = DateTime.Today.Month - 1;

                //        ReporteDeudaProveedoresCargado = true;
                //    }
                //}
                //else if (tcReportes.SelectedIndex == 4)//PESTAÑA PROVEEDORES
                //{
                //    if (!ReporteDeudaProveedoresCargado)
                //    {
                //        CargaAñosCmb(cmbAñosDeudaProveedores);
                //        cmbMesesDeudaProveedores.SelectedIndex = DateTime.Today.Month - 1;

                //        ReporteDeudaProveedoresCargado = true;
                //    }
                //}
                else if (tcReportes.SelectedTab.Text == "Analítico")//PESTAÑA ANALITICO
                {
                    if (!ReporteAnaliticoCargado)
                    {
                        CargaAñosCmb(cmbAñosAnalitico);
                        cmbMesesAnalitico.SelectedIndex = DateTime.Today.Month - 1;

                        ReporteAnaliticoCargado = true;
                    }
                }
                else if (tcReportes.SelectedTab.Text == "Auxiliar")//PESTAÑA AUXILIAR
                {
                    if (!ReporteAuxiliarCargado)
                    {
                        CargaAñosCmb(cmbAñosAuxiliar);
                        cmbMesesAuxiliar.SelectedIndex = DateTime.Today.Month - 1;
                        CargaAñosCmb(cmbAñosAuxiliarHasta);
                        cmbMesesAuxiliarHasta.SelectedIndex = DateTime.Today.Month - 1;

                        CargaCmbProductos(tcReportesAuxiliares.SelectedIndex + 1, ObtieneFechaPrimerDiaMes(cmbMesesAuxiliar, cmbAñosAuxiliar),
                        ObtieneFechaUltimoDiaMes(cmbMesesAuxiliar, cmbAñosAuxiliar));
                        ReporteAuxiliarCargado = true;
                    }
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        #region Eventos Pestaña Ventas

        private void btnRefrescarVentas_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdoPorMesVentas.Checked)
                {
                    if (cmbMesesVentas.SelectedIndex >= 0)
                        CargaRvVentas(Program.EmpresaSeleccionada,
                                    ObtieneFechaPrimerDiaMes(cmbMesesVentas, cmbAñosVentas),
                                    ObtieneFechaUltimoDiaMes(cmbMesesVentas, cmbAñosVentas));
                }
                else if (rdoPorDiaVentas.Checked)
                    CargaRvVentas(Program.EmpresaSeleccionada,
                        dtpFechaDiaVentas.Value.Date,
                        dtpFechaDiaVentas.Value.Date);
                else if (rdoPorFechasVentas.Checked)
                    CargaRvVentas(Program.EmpresaSeleccionada,
                                    dtpFechaDesdeVentas.Value.Date,
                                    dtpFechaHastaVentas.Value.Date);
                
                //if (this.ListaPedidos != null)
                //{
                //    CargaCmbProductos();
                //    CargaCmbClientesFromPedidos(this.ListaPedidos);
                //}
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        private void rdoPorMesVentas_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //if (((RadioButton)sender).Checked)
                //{
                pnlPorMesVentas.Enabled = ((RadioButton)sender).Checked;
                pnlPorDiaVentas.Enabled = !((RadioButton)sender).Checked;
                pnlPorFechasVentas.Enabled = !((RadioButton)sender).Checked;

                btnRefrescarVentas.PerformClick();
                //}
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void rdoPorDiaVentas_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //if (((RadioButton)sender).Checked)
                //{
                    pnlPorMesVentas.Enabled = !((RadioButton)sender).Checked;
                    pnlPorDiaVentas.Enabled = ((RadioButton)sender).Checked;
                    pnlPorFechasVentas.Enabled = !((RadioButton)sender).Checked;

                    btnRefrescarVentas.PerformClick();
                //}
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void rdoPorFechasVentas_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //if (((RadioButton)sender).Checked)
                //{
                    pnlPorMesVentas.Enabled = !((RadioButton)sender).Checked;
                    pnlPorDiaVentas.Enabled = !((RadioButton)sender).Checked;
                    pnlPorFechasVentas.Enabled = ((RadioButton)sender).Checked;

                    btnRefrescarVentas.PerformClick();
                //}
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
                        if (tcClientesCxC.SelectedIndex == 0)//Deuda
                            CargaRvDeudaClientes(new DateTime(ConvierteTextoAInteger(cmbAñosDeudaClientes.Text), cmbMesesDeudaCliente.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñosDeudaClientes.Text), cmbMesesDeudaCliente.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñosDeudaClientes.Text), cmbMesesDeudaCliente.SelectedIndex + 1)));
                        else if (tcClientesCxC.SelectedIndex == 1)
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

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbClientesVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(tcReportes.SelectedIndex== 0) //tcReportes.TabPages["Inventario"])
                {
                    btnRefrescarInventario.PerformClick();
                }
                //else if (tcReportes.SelectedIndex == 1) //tcReportes.TabPages["Ventas"])
                //{
                //    if (ReporteVentasCargado)
                //    {
                //        if (this.ListaPedidos != null)
                //        {
                //            FiltraVentas(Convert.ToInt32(cmbProductosVentas.SelectedValue),
                //                                           cmbProductosVentas.SelectedText,
                //                         Convert.ToInt32(cmbClientesVentas.SelectedValue));
                //        }
                //    }
                //}

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
                    {
                        CargaRvInventario(Program.EmpresaSeleccionada.Id, ObtieneFechaUltimoDiaMes(cmbMesesInventario, cmbAñosInventario));
                        CargaRvComprasVentas(Program.EmpresaSeleccionada, new DateTime(AñoInicio, 1, 1),
                            ObtieneFechaUltimoDiaMes(cmbMesesInventario, cmbAñosInventario),
                            txtBuscaProductoCodigo.Text + " - " + txtBuscaProductoCodigo.Text);
                        cmbProductosFiltroReporteComprasVentas.SelectedIndex = -1;
                    }
                }
                //else if (rdoTotalDeudaCliente.Checked)
                //    CargaRvDeudaClientes();
                else
                {
                    CargaRvInventario(Program.EmpresaSeleccionada.Id, dtpAlDiaInventario.Value.Date);
                    CargaRvComprasVentas(Program.EmpresaSeleccionada, new DateTime(AñoInicio, 1, 1),
                            dtpAlDiaInventario.Value.Date,
                            txtBuscaProductoCodigo.Text + " - " + txtBuscaProductoCodigo.Text);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tcClientesCxC.SelectedIndex == 0)
                {
                    pnlAlDiaClientes.Enabled = true;
                    rdoAlDiaClientes.Enabled = true;
                    rdoTotalDeudaCliente.Enabled = true;
                }
                else
                    if (tcClientesCxC.SelectedIndex == 1)
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

        DateTime ObtieneFechaPrimerDiaMes(int Mes, int Año)
        {
            return new DateTime(Año, Mes, 1);
        }
        DateTime ObtieneFechaUltimoDiaMes(int Mes, int Año)
        {
            return new DateTime(Año, Mes, DateTime.DaysInMonth(Año, Mes));
        }
        DateTime ObtieneFechaPrimerDiaMes(ComboBox ComboMeses, ComboBox ComboAños)
        {
            int mes = ComboMeses.SelectedIndex + 1;
            int año = ConvierteTextoAInteger(ComboAños.Text);

            return new DateTime(año, mes, 1);
        }
        DateTime ObtieneFechaUltimoDiaMes(ComboBox ComboMeses, ComboBox ComboAños)
        {
            int mes = ComboMeses.SelectedIndex + 1;
            int año = ConvierteTextoAInteger(ComboAños.Text);

            return new DateTime(año, mes, DateTime.DaysInMonth(año, mes));
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtBuscaProductoCodigo.Text) || !string.IsNullOrWhiteSpace(txtBuscaProducto.Text))
                {
                    FiltroInventario(this.ListaProductosEntradas, txtBuscaProductoCodigo.Text, txtBuscaProducto.Text);
                    FiltroCompraVenta(this.ListaPedidos, txtBuscaProductoCodigo.Text, txtBuscaProducto.Text);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
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
                    if (tcReportes.SelectedIndex == 0)//PESTAÑA INVENTARIO
                    {
                        btnRefrescarInventario.PerformClick();
                    }
                    else if (tcReportes.SelectedIndex == 1)//PESTAÑA VENTAS
                    {
                        btnRefrescarVentas.PerformClick();
                    }
                    else if (tcReportes.SelectedIndex == 2)//PESTAÑA ANALITICO
                    {
                        btnRefrescarAnalitico.PerformClick();
                    }
                    //btnRefrescarInventario.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnRefrescaAnalitico_Click(object sender, EventArgs e)
        {
            try
            {
                int tipoReporte = tcReportesAnaliticos.SelectedIndex + 1;
                if (rdoPorMesAnalitico.Checked)
                {
                    if (cmbMesesAnalitico.SelectedIndex >= 0)
                        CargaRvAnalitico(Program.EmpresaSeleccionada, ObtieneFechaPrimerDiaMes(cmbMesesAnalitico, cmbAñosAnalitico), ObtieneFechaUltimoDiaMes(cmbMesesAnalitico, cmbAñosAnalitico),
                            tcReportesAnaliticos.SelectedIndex+1);
                }
                else
                    CargaRvAnalitico(Program.EmpresaSeleccionada, 
                                    dtpFechaDesdeAnalitico.Value.Date, dtpFechaHastaAnalitico.Value.Date, 
                                    tcReportesAnaliticos.SelectedIndex + 1);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void rdoPorMesAnalitico_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pnlPorMesAnalitico.Enabled = ((RadioButton)sender).Checked;
                pnlPorDiaAnalitico.Enabled = !((RadioButton)sender).Checked;
                pnlPorFechasAnalitico.Enabled = !((RadioButton)sender).Checked;

                btnRefrescarAnalitico.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnRefrescarAuxiliar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbMesesAuxiliar.SelectedIndex >= 0)
                    CargaRvAuxiliar(Program.EmpresaSeleccionada, 
                            ObtieneProductoFromCmb(cmbProductosHistorialPorProducto),
                            ObtieneFechaPrimerDiaMes(cmbMesesAuxiliar, cmbAñosAuxiliar), ObtieneFechaUltimoDiaMes(cmbMesesAuxiliarHasta, cmbAñosAuxiliarHasta),
                            tcReportesAuxiliares.SelectedIndex + 1);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbMesesAuxiliar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ReporteAuxiliarCargado)
                CargaCmbProductos(tcReportesAuxiliares.SelectedIndex + 1, ObtieneFechaPrimerDiaMes(cmbMesesAuxiliar, cmbAñosAuxiliar),
                    ObtieneFechaUltimoDiaMes(cmbMesesAuxiliarHasta, cmbAñosAuxiliarHasta));
        }

        private void cmbMesesAuxiliarHasta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ReporteAuxiliarCargado)
                    CargaCmbProductos(tcReportesAuxiliares.SelectedIndex + 1, ObtieneFechaPrimerDiaMes(cmbMesesAuxiliar, cmbAñosAuxiliar),
                        ObtieneFechaUltimoDiaMes(cmbMesesAuxiliarHasta, cmbAñosAuxiliarHasta));
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnBuscaClienteVentasPorFecha_Click(object sender, EventArgs e)
        {
            try
            {
                //FiltroClientes vClientes = new FiltroClientes();
                //if (vClientes.ShowDialog() == DialogResult.OK)
                //{
                //    cmbClientesVentas.SelectedIndex = ((List<EntPedido>)cmbClientesVentas.DataSource).FindIndex(P => P.ClienteId == vClientes.ClienteSeleccionado.Id);
                //}
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbProductosFiltroReporteComprasVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbProductosFiltroReporteComprasVentas.SelectedIndex>=0)
                {
                    FiltroInventario(this.ListaProductosEntradas, "", cmbProductosFiltroReporteComprasVentas.Text);
                    FiltroCompraVenta(this.ListaPedidos, "", cmbProductosFiltroReporteComprasVentas.Text);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbProductosFiltroReporteComprasVentas_Click(object sender, EventArgs e)
        {
            try
            {
                entPedidoBindingSource.DataSource = this.ListaPedidos;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtSerieFiltroReporteComprasVentas_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FiltroCompraVenta(this.ListaPedidos, txtSerieFiltroReporteComprasVentas.Text);
                cmbProductosFiltroReporteComprasVentas.SelectedIndex = -1;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        int ProductoFiltroId { get; set; }
        void CargaDatosProductos(EntProducto Producto)
        {
            if (Producto == null)
                Producto = new EntProducto();
            txtProductoCodigoFiltroVentas.Text = Producto.Codigo;
            txtProductoDescripcionFiltroVentas.Text = Producto.Descripcion;
            this.ProductoFiltroId = Producto.Id;
        }
        void CargaDatosProductos(EntProducto Producto, TextBox TxtProductoCodigo, TextBox TxtProductoDescripcion)
        {
            if (Producto == null)
                Producto = new EntProducto();
            TxtProductoCodigo.Text = Producto.Codigo;
            TxtProductoDescripcion.Text = Producto.Descripcion;
            this.ProductoFiltroId = Producto.Id;
        }

        private void btnBuscaProductoVentas_Click(object sender, EventArgs e)
        {
            try
            {
                int compañiaId = Program.UsuarioSeleccionado.EmpresaId;
                SeleccionaProducto vProd = new SeleccionaProducto(new BusProductos()
                                                                    .ObtieneProductos(compañiaId, 1));
                if (vProd.ShowDialog() == DialogResult.OK)
                {
                    CargaDatosProductos(vProd.ProductoSeleccionado);
                    //FiltroVentas(this.ListaPedidos, this.ProductoFiltroId);
                    if(tcReportesVentas.SelectedIndex<3)
                        FiltraVentas(txtProductoDescripcionFiltroVentas.Text, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""));
                    else
                        FiltraVentasPorProducto(this.ProductoFiltroId, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""));
                }
                else
                    this.ProductoFiltroId = 0;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtProductoCodigoFiltroVentas_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (int)Keys.Enter)
                {
                    int compañiaId = Program.UsuarioSeleccionado.EmpresaId;
                    List<EntProducto> productosEncontrados = new BusProductos().ObtieneProductos(compañiaId).Where(P => P.Codigo == txtProductoCodigoFiltroVentas.Text).ToList();
                    if (productosEncontrados.Count == 0)
                    {
                        MuestraMensajeError("PRODUCTO NO ENCONTRADO");
                        txtProductoDescripcionFiltroVentas.Clear();
                        this.ProductoFiltroId = 0;
                    }
                    else
                        CargaDatosProductos(productosEncontrados.First());
                    if (tcReportesVentas.SelectedIndex < 3)
                        FiltraVentas(txtProductoDescripcionFiltroVentas.Text, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""));
                    else
                        FiltraVentasPorProducto(this.ProductoFiltroId, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""));
                }
            }
            catch(Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnBuscaCliente_Click(object sender, EventArgs e)
        {
            try
            {
                FiltroClientes vCli = new FiltroClientes();
                if (vCli.ShowDialog() == DialogResult.OK)
                {
                    txtFiltroClientes.Text = vCli.ClienteSeleccionado.Nombre;// + " " + vCli.ProveedorSeleccionado.NombreFiscal;
                                                                             //btnRefrescarVentas.PerformClick();
                                                                             //FiltraVentas(0,"",0,txtFiltroClientes.Text);
                    if (tcReportesVentas.SelectedIndex < 3)
                        FiltraVentas(txtProductoDescripcionFiltroVentas.Text, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""));
                    else
                        FiltraVentasPorProducto(this.ProductoFiltroId, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""));
                }

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtFiltroClientes_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tcReportesVentas.SelectedIndex < 3)
                    FiltraVentas(txtProductoDescripcionFiltroVentas.Text, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""));
                else
                    FiltraVentasPorProducto(this.ProductoFiltroId, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""));
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbTrabajadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tcReportesVentas.SelectedIndex < 3)
                    FiltraVentas(txtProductoDescripcionFiltroVentas.Text, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""));
                else
                    FiltraVentasPorProducto(this.ProductoFiltroId, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""));
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnBuscaProductoVentasPorTrabajador_Click(object sender, EventArgs e)
        {
            try
            {
                int compañiaId = Program.UsuarioSeleccionado.EmpresaId;
                SeleccionaProducto vProd = new SeleccionaProducto(new BusProductos()
                                                                    .ObtieneProductos(compañiaId, 1));
                if (vProd.ShowDialog() == DialogResult.OK)
                {
                    CargaDatosProductos(vProd.ProductoSeleccionado);

                    if (tcReportesVentas.SelectedIndex < 3)
                        FiltraVentas(txtProductoDescripcionFiltroVentas.Text, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""));
                    else
                        FiltraVentasPorProducto(this.ProductoFiltroId, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""));
                }
                else
                    this.ProductoFiltroId = 0;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        int tabAnterior = 0;
        private void tcReportesVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabAnterior==3 && tcReportesVentas.SelectedIndex < 3 || tcReportesVentas.SelectedIndex == 3)
                    btnRefrescarVentas.PerformClick();

                tabAnterior = tcReportesVentas.SelectedIndex;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnLimpiaFiltroProducto_Click(object sender, EventArgs e)
        {
            try
            {
                this.ProductoFiltroId = 0;
                txtProductoCodigoFiltroVentas.Clear();
                txtProductoDescripcionFiltroVentas.Clear();
                
                if (tcReportesVentas.SelectedIndex < 3)
                        FiltraVentas(txtProductoDescripcionFiltroVentas.Text, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""));
                else
                    FiltraVentasPorProducto(this.ProductoFiltroId, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""));
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnLimpiaFiltroClienteVentas_Click(object sender, EventArgs e)
        {
            try
            {
                txtFiltroClientes.Clear();

                if (tcReportesVentas.SelectedIndex < 3)
                    FiltraVentas(txtProductoDescripcionFiltroVentas.Text, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""));
                else
                    FiltraVentasPorProducto(this.ProductoFiltroId, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""));
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void rdoPorFechasAnalitico_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pnlPorMesAnalitico.Enabled = !((RadioButton)sender).Checked;
                pnlPorDiaAnalitico.Enabled = !((RadioButton)sender).Checked;
                pnlPorFechasAnalitico.Enabled = ((RadioButton)sender).Checked;

                btnRefrescarAnalitico.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

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
