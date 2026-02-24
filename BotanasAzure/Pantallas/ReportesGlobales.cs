using AiresEntidades;
using AiresNegocio;
using AiresUtilerias;
using iTextSharp.text;
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
    public partial class ReportesGlobales : FormBase
    {
        List<EntEmpresa> ListaEmpresas;
        public void CargaEmpresas()
        {
            this.ListaEmpresas = new BusEmpresas().ObtieneEmpresas();

            Program.CambiaEmpresa = false;
            cmbEmpresas.DataSource = this.ListaEmpresas;
            Program.CambiaEmpresa = true;
        }
        
        public ReportesGlobales()
        {
            InitializeComponent();
        }

        #region Metodos Ventas
        List<EntPedido> ListaPedidos;
        List<EntPedido> ListaPedidosComparativo;
        List<EntProducto> ListaProductosVentas;

        bool ReporteEntradasCargado, ReporteDeudaClientesCargado, ReporteDeudaProveedoresCargado, ReporteInventarioCargado, ReporteVentasCargado,
            ReporteAnaliticoCargado, ReporteAuxiliarCargado;

        public void CargaRvVentas(EntEmpresa EmpresaSeleccionada, DateTime FechaDesde, DateTime FechaHasta)
        {
            ReportParameter parmEmpresa;
            //parmEmpresa = new ReportParameter("Empresa", EmpresaSeleccionada.Nombre);
            //if (tcReportesVentas.SelectedIndex < 3)
            //{
            //    this.ListaPedidos = new BusPedidos().ObtienePedidosClientesPorFechas(EmpresaSeleccionada.Id, 
            //                                                                                            FechaDesde, FechaHasta);                
            //    entPedidoBindingSource.DataSource = this.ListaPedidos;

            //    FiltraVentas(txtProductoDescripcionFiltroVentas.Text, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""), chkSoloDevoluciones.Checked);
            //}
            //else
            //{
            EntCatalogoGenerico periodo = ObtieneCatalogoGenericoFromCmb(cmbPeriodoVentas);
            DateTime fechaDesde = DateTime.Today;
            DateTime fechaHasta = DateTime.Today;
            AsignaFechaDesdeFechaHastaFromCmbPeriodos(cmbPeriodoVentas, 1, ref fechaDesde, ref fechaHasta);

            if (!chkSinComparativo.Checked)
            {
                this.ListaPedidosComparativo = new BusPedidos().ObtienePedidosClientesPorFechasConCostosPorProducto(1,
                                                                                                        fechaDesde, fechaHasta);
                this.ListaPedidosComparativo.AddRange(new BusPedidos().ObtienePedidosClientesPorFechasConCostosPorProducto(2,
                                                                                                        fechaDesde, fechaHasta));
            }
            else
                this.ListaPedidosComparativo = new List<EntPedido>();
            this.ListaPedidos = new BusPedidos().ObtienePedidosClientesPorFechasConCostosPorProducto(1,
                                                                                                    FechaDesde, FechaHasta);
            this.ListaPedidos.AddRange(new BusPedidos().ObtienePedidosClientesPorFechasConCostosPorProducto(2,
                                                                                                    FechaDesde, FechaHasta));
            entPedidoBindingSource1.DataSource = this.ListaPedidos;

            AplicaFiltroVentasPorProducto();
            //}

        }

        void FiltraVentas(string ProductoFiltroDescripcion, int ClienteFiltroId, string ClienteFiltro, string TrabajadorFiltro, bool SoloDevoluciones)
        {
            ReportParameter parmEmpresa;
            parmEmpresa = new ReportParameter("Empresa", Program.EmpresaSeleccionada.Nombre);

            List<EntPedido> lstFiltro = this.ListaPedidos;

            if (SoloDevoluciones)
                lstFiltro = lstFiltro.Where(P => P.TipoPedidoId == (int)TipoPedido.DEVOLUCIONCORTESIA).ToList();

            if (ProductoFiltroDescripcion.Length > 0)
                lstFiltro = lstFiltro.Where(P => P.Detalle.Contains(ProductoFiltroDescripcion)).ToList();
            if (ClienteFiltroId > 0)
                lstFiltro = lstFiltro.Where(P => P.ClienteId == ClienteFiltroId).ToList();
            if (!string.IsNullOrWhiteSpace(ClienteFiltro))
                lstFiltro = lstFiltro.Where(P => (P.NumCliente + " " + P.Cliente).ToUpper().Contains(ClienteFiltro.ToUpper())).ToList();

            if (!string.IsNullOrWhiteSpace(TrabajadorFiltro))
                lstFiltro = lstFiltro.Where(P => P.Trabajador.ToUpper().Contains(TrabajadorFiltro.ToUpper())).ToList();

            this.entPedidoBindingSource.DataSource = lstFiltro;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Lista1"></param>
        /// <param name="Lista2">LISTA COMPARATIVA DE PERIODOS ANTERIORES</param>
        void CalcularDiferenciasYPorcentajes(ref List<EntPedido> Lista1,ref List<EntPedido> Lista2)
        {
            //SOLO SE HACE EL RECORRIDO SOBRE LA LISTA 2(O CUALQUIERA) PARA HACER LA RESTA 
            foreach (var ent1 in Lista1)
            {
                //FALTARIA GLOBALIZAR. AQUI SIEMPRE RESTARIA CONTRA EL PRIMER REGISTRO DEL PRODUCTO
                var ent2 = Lista2.FirstOrDefault(P => 
                                                    //P.EmpresaId == ent1.EmpresaId && 
                                                    P.Sucursal == ent1.Sucursal && P.ProductoId == ent1.ProductoId);
                if (ent2 != null)
                {
                    ent1.Descuento = (ent1.Total - ent2.Total);/*Math.Abs(ent2.Total - ent1.Total);*/

                    if (ent2.Total != 0)
                        ent1.Saldo = (ent1.Descuento / ent2.Total); //(ent1.Descuento / ent1.Total) * 100;
                    else
                        ent1.Saldo = 1m;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Lista1"></param>
        /// <param name="Lista2">LISTA COMPARATIVA DE PERIODOS ANTERIORES</param>
        void IgualarListas(List<EntPedido> Lista1, List<EntPedido> Lista2, bool CodigosBajos)
        {
            var items = Lista1;
            //// Agrupar por campo Category
            //var groupedItems = items.GroupBy(i => i.ClienteNombreFiscal + "-" + i.Sucursal + "-"+ i.ProductoId.ToString() )
            //    .Select(productoGroup => new { Key = productoGroup.Key,
            //        Empresa = productoGroup.Max(p => p.ClienteNombreFiscal),
            //        Sucursal =productoGroup.Max(p => p.Sucursal),
            //        Productoid=productoGroup.Max(p=>p.ProductoId),
            //        Producto = productoGroup.Max(p => p.Descripcion),
            //        Cantidad = productoGroup.Sum(p => p.SubTotal),
            //        VentasTotales = productoGroup.Sum(p => p.Total*p.SubTotal) })
            //    .OrderBy(P=>P.Empresa).ThenBy(P=>P.Sucursal).ThenBy(P=>P.Producto).ToList();

            ////COMPARATIVA
            //var groupedItemsComparativoAnt = Lista2.GroupBy(i => i.ClienteNombreFiscal + "-" + i.Sucursal + "-" + i.ProductoId.ToString())
            //    .Select(productoGroup => new {
            //        Key = productoGroup.Key,
            //        Empresa = productoGroup.Max(p => p.ClienteNombreFiscal),
            //        Sucursal = productoGroup.Max(p => p.Sucursal),
            //        Productoid = productoGroup.Max(p => p.ProductoId),
            //        Producto = productoGroup.Max(p => p.Descripcion),
            //        Cantidad = productoGroup.Sum(p => p.SubTotal),
            //        VentasTotales = productoGroup.Sum(p => p.Total * p.SubTotal)
            //    })
            //    .OrderBy(P => P.Empresa).ThenBy(P => P.Sucursal).ThenBy(P => P.Producto).ToList();

            //// Imprimir resultados
            //foreach (var group in groupedItems)
            //{
            //    Console.WriteLine($"Category: {group.Key}");
            //    //foreach (var item in group)
            //    //{
            //    //    Console.WriteLine($" {item.ProductoId}");
            //    //}
            //}

            ////---------------------------------------------------------------------------------//
            //// Agrupar por Sucursal y luego por Nombre del Producto
            //var grupos = items.GroupBy(p => p.Sucursal)
            //            .Select(sucursalGroup => new { Sucursal = sucursalGroup.Key, Productos = sucursalGroup.GroupBy(p => p.ProductoId) 
            //                                        .Select(productoGroup => new { Producto = productoGroup.Key, VentasTotales = productoGroup.Sum(p => p.Total) })
            //                                        .ToList() })
            //            .ToList();

            //// Imprimir resultados
            //foreach (var sucursal in grupos) { 
            //    Console.WriteLine($"Sucursal: {sucursal.Sucursal}"); 
            //    foreach (var producto in sucursal.Productos) { 
            //        Console.WriteLine($" Producto: {producto.Producto}, VentasTotales: {producto.VentasTotales}"); 
            //    } 
            //}
            //_---------------------------------------------------------------------------------//

            //var enLista2NoEnLista1 = Lista2.Where(ent2 => !Lista1.Any(ent1 => ent1.ProductoId == ent2.ProductoId))
            //                                .Select(ent => new EntPedido { ProductoId = ent.ProductoId, Descripcion = "NA-"+ent.Descripcion }).ToList();
            var enLista2NoEnLista1 = Lista2.Where(ent2 => !Lista1.Any(ent1 => 
                                                                    //ent1.EmpresaId == ent2.EmpresaId && 
                                                                    ent1.Sucursal == ent2.Sucursal && ent1.ProductoId == ent2.ProductoId))
                                           .Select(ent => new EntPedido { ClienteNombreFiscal=ent.ClienteNombreFiscal, Sucursal=ent.Sucursal, ProductoId = ent.ProductoId, Descripcion = ent.Descripcion + " - NA"})
                                           .OrderBy(P => P.ClienteNombreFiscal).ThenBy(P => P.Sucursal).ThenBy(P => P.ProductoId).ToList();
            var enLista2NoEnLista1grouped = enLista2NoEnLista1.GroupBy(i => 
                                                                        //i.ClienteNombreFiscal + "-" +
                                                                        i.Sucursal + "-" + i.ProductoId.ToString())
                .Select(productoGroup => new {
                    Key = productoGroup.Key,
                    Empresaid = productoGroup.Max(p => p.EmpresaId),
                    Empresa = productoGroup.Max(p => p.ClienteNombreFiscal),
                    Sucursal = productoGroup.Max(p => p.Sucursal),
                    Productoid = productoGroup.Max(p => p.ProductoId),
                    Producto = productoGroup.Max(p => p.Descripcion),
                    Cantidad = productoGroup.Sum(p => p.SubTotal),
                    VentasTotales = productoGroup.Sum(p => p.Total * p.SubTotal)
                })
                .OrderBy(P => P.Empresa).ThenBy(P => P.Sucursal).ThenBy(P => P.Producto).ToList();

            var enLista1NoEnLista2 = Lista1.Where(ent1 => !Lista2.Any(ent2 => 
                                                                    //ent2.EmpresaId == ent1.EmpresaId &&
                                                                    ent2.Sucursal == ent1.Sucursal && ent2.ProductoId == ent1.ProductoId))
                                            .Select(ent => new EntPedido { ClienteNombreFiscal = ent.ClienteNombreFiscal, Sucursal = ent.Sucursal, ProductoId = ent.ProductoId, Descripcion = ent.Descripcion+" - NA" }).ToList();
            var enLista1NoEnLista2grouped = enLista1NoEnLista2.GroupBy(i => 
                                                                        //i.ClienteNombreFiscal + "-" +
                                                                        i.Sucursal + "-" + i.ProductoId.ToString())
                .Select(productoGroup => new {
                    Key = productoGroup.Key,
                    Empresaid = productoGroup.Max(p => p.EmpresaId),
                    Empresa = productoGroup.Max(p => p.ClienteNombreFiscal),
                    Sucursal = productoGroup.Max(p => p.Sucursal),
                    Productoid = productoGroup.Max(p => p.ProductoId),
                    Producto = productoGroup.Max(p => p.Descripcion),
                    Cantidad = productoGroup.Sum(p => p.SubTotal),
                    VentasTotales = productoGroup.Sum(p => p.Total * p.SubTotal)
                })
                .OrderBy(P => P.Empresa).ThenBy(P => P.Sucursal).ThenBy(P => P.Producto).ToList();

            var lista1grouped = Lista1.GroupBy(i => 
                                                //i.ClienteNombreFiscal + "-" +
                                                i.Sucursal + "-" + i.ProductoId.ToString())
                                            .Select(productoGroup => new {
                                                Key = productoGroup.Key,
                                                Empresaid = productoGroup.Max(p => p.EmpresaId),
                                                Empresa = productoGroup.Max(p => p.ClienteNombreFiscal),
                                                Sucursal = productoGroup.Max(p => p.Sucursal),
                                                Productoid = productoGroup.Max(p => p.ProductoId),
                                                Producto = productoGroup.Max(p => p.Descripcion),
                                                Cantidad = productoGroup.Sum(p => p.SubTotal),
                                                VentasTotales = productoGroup.Sum(p => p.Total * p.SubTotal)
                                            })
                                            .OrderBy(P => P.Empresa).ThenBy(P => P.Sucursal).ThenBy(P => P.Producto).ToList();
            var lista2grouped = Lista2.GroupBy(i => 
                                                //i.ClienteNombreFiscal + "-" +
                                                i.Sucursal + "-" + i.ProductoId.ToString())
                                      .Select(productoGroup => new {
                                          Key = productoGroup.Key,
                                          Empresaid = productoGroup.Max(p => p.EmpresaId),
                                          Empresa = productoGroup.Max(p => p.ClienteNombreFiscal),
                                          Sucursal = productoGroup.Max(p => p.Sucursal),
                                          Productoid = productoGroup.Max(p => p.ProductoId),
                                          Producto = productoGroup.Max(p => p.Descripcion),
                                          Cantidad = productoGroup.Sum(p => p.SubTotal),
                                          VentasTotales = productoGroup.Sum(p => p.Total * p.SubTotal)
                                      })
                                      .OrderBy(P => P.Empresa).ThenBy(P => P.Sucursal).ThenBy(P => P.Producto).ToList();

            //REGISTROS POR PEDIDO(VENTA)
            DateTime fechaC=Lista2.First().Fecha, fecha=Lista1.First().Fecha;
            Lista1.Clear();
            foreach (var p in lista1grouped)
            {
                Lista1.Add(new EntPedido() { EmpresaId = p.Empresaid, ClienteNombreFiscal = p.Empresa, Sucursal = p.Sucursal, ProductoId = p.Productoid, Descripcion = p.Producto,
                                             SubTotal=p.Cantidad, Total=p.VentasTotales,
                                             Fecha=fecha, Estatus=true});
            }
            foreach (var p in enLista2NoEnLista1grouped)
            {
                Lista1.Add(new EntPedido() { EmpresaId = p.Empresaid, ClienteNombreFiscal=p.Empresa, Sucursal=p.Sucursal, ProductoId=p.Productoid, Descripcion=p.Producto,
                                             Fecha=fecha, Estatus = true
                });
            }
            Lista2.Clear();
            foreach (var p in lista2grouped)
            {
                Lista2.Add(new EntPedido(){ EmpresaId = p.Empresaid, ClienteNombreFiscal = p.Empresa, Sucursal = p.Sucursal, ProductoId = p.Productoid, Descripcion = p.Producto,
                                            SubTotal = p.Cantidad, Total = p.VentasTotales, 
                                            Fecha=fechaC, Estatus = true
                });
            }
            foreach (var p in enLista1NoEnLista2grouped)
            {
                Lista2.Add(new EntPedido() { EmpresaId = p.Empresaid, ClienteNombreFiscal = p.Empresa, Sucursal = p.Sucursal, ProductoId = p.Productoid, Descripcion = p.Producto,
                                            Fecha = fechaC, Estatus = true});
            }

            //foreach (var p in enLista2NoEnLista1grouped)
            //{
            //    lista1grouped.Add(new
            //    {
            //        Key = p.Key,
            //        Empresa = p.Empresa,
            //        Sucursal = p.Sucursal,
            //        Productoid = p.Productoid,
            //        Producto = p.Producto,
            //        Cantidad = p.Cantidad,
            //        VentasTotales = p.VentasTotales
            //    });
            //}
            //foreach (var p in enLista1NoEnLista2grouped)
            //{
            //    lista2grouped.Add(new
            //    {
            //        Key = p.Key,
            //        Empresa = p.Empresa,
            //        Sucursal = p.Sucursal,
            //        Productoid = p.Productoid,
            //        Producto = p.Producto,
            //        Cantidad = p.Cantidad,
            //        VentasTotales = p.VentasTotales
            //    });
            //}

            //// Crear un diccionario para almacenar las ventas totales de cada producto en lista1
            //var diccionarioVentasLista1 = Lista1.ToDictionary(p => p.ProductoId.ToString() + "-" + p.Sucursal, p => p.Total); 
            //// Crear un diccionario para almacenar las ventas totales de cada producto en lista2
            //var diccionarioVentasLista2 = Lista2.ToDictionary(p => p.ProductoId + "-" + p.Sucursal, p => p.Total); 
            //// Iterar sobre lista1 para agregar productos de lista1 que no estén en lista2 con ventas iniciales de 0
            //foreach (var producto in Lista1) {
            //    if (!Lista2.Any(p => p.ProductoId == producto.ProductoId))
            //    {
            //        Lista2.Add(new EntPedido { ProductoId = producto.ProductoId, Total = 0 });
            //    }
            //} 
            //// Iterar sobre lista2 para agregar productos de lista2 que no estén en lista1 con ventas iniciales de 0
            //foreach (var producto in Lista2) { 
            //    if (!Lista1.Any(p => p.ProductoId == producto.ProductoId)) { 
            //        Lista1.Add(new EntPedido { ProductoId = producto.ProductoId, Total = 0 }); 
            //    } 
            //}
            //// Actualizar las diferencias en ventas sin modificar las ventas totales en lista2
            //foreach (var producto in Lista2)
            //{
            //    if (diccionarioVentasLista1.ContainsKey(producto.ProductoId.ToString() + "-" + producto.Sucursal)) 
            //    { 
            //        producto.Saldo = diccionarioVentasLista1[producto.ProductoId.ToString() + "-" + producto.Sucursal] - producto.Total; 
            //    }
            //    else { producto.Saldo = -producto.Total; }
            //}

            CalcularDiferenciasYPorcentajes(ref Lista1, ref Lista2);
            //if (CodigosBajos)
            //{
            //    Lista1 = Lista1.Where(P => P.Saldo > 0).ToList();
            //    Lista2 = Lista2.Where(P => P.Saldo > 0).ToList();
            //}
        }
        void AplicaFiltroVentasPorProducto()
        {
            FiltraVentasPorProducto(this.ProductoFiltroId, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""), chkSoloDevoluciones.Checked,
                                    ObtieneListaProductosFromGV(gvProductosFiltro).Where(P => !P.Estatus).ToList(),
                                    cmbAlmacenes.Text, chkCodigosBajos.Checked);
        }
        void FiltraVentasPorProducto(int ProductoId, int ClienteFiltroId, string ClienteFiltro, string TrabajadorFiltro, bool SoloDevoluciones,
                                    List<EntProducto> ProductosFiltraRemueve, string Almacen, bool CodigosBajos)
        {
            ReportParameter parmEmpresa;
            parmEmpresa = new ReportParameter("Empresa", Program.EmpresaSeleccionada.Nombre);

            List<EntPedido> lstFiltro = this.ListaPedidos;
            List<EntPedido> lstFiltroComparativo = this.ListaPedidosComparativo;

            foreach (EntProducto p in ProductosFiltraRemueve)
            {
                lstFiltro.RemoveAll(P => P.ProductoId == p.Id);
                lstFiltroComparativo.RemoveAll(P => P.ProductoId == p.Id);
            }

            if (SoloDevoluciones)
                lstFiltro = lstFiltro.Where(P => P.TipoPedidoId == (int)TipoPedido.DEVOLUCIONCORTESIA).ToList();

            if (Almacen != "-TODOS-")
            {
                lstFiltro = lstFiltro.Where(P => P.Sucursal == Almacen).ToList();
                lstFiltroComparativo = lstFiltroComparativo.Where(P => P.Sucursal == Almacen).ToList();
            }
            if (ProductoId > 0)
            {
                lstFiltro = lstFiltro.Where(P => P.ProductoId == ProductoId).ToList();
                lstFiltroComparativo = lstFiltroComparativo.Where(P => P.ProductoId == ProductoId).ToList();
            }
            if (ClienteFiltroId > 0)
            {
                lstFiltro = lstFiltro.Where(P => P.ClienteId == ClienteFiltroId).ToList();
                lstFiltroComparativo = lstFiltroComparativo.Where(P => P.ClienteId == ClienteFiltroId).ToList();
            }
            if (!string.IsNullOrWhiteSpace(ClienteFiltro))
            {
                lstFiltro = lstFiltro.Where(P => (P.NumCliente + " " + P.Cliente).ToUpper().Contains(ClienteFiltro.ToUpper())).ToList();
                lstFiltroComparativo = lstFiltroComparativo.Where(P => (P.NumCliente + " " + P.Cliente).ToUpper().Contains(ClienteFiltro.ToUpper())).ToList();
            }
            if (!string.IsNullOrWhiteSpace(TrabajadorFiltro))
            {
                lstFiltro = lstFiltro.Where(P => P.Trabajador.ToUpper().Contains(TrabajadorFiltro.ToUpper())).ToList();
                lstFiltroComparativo = lstFiltroComparativo.Where(P => P.Trabajador.ToUpper().Contains(TrabajadorFiltro.ToUpper())).ToList();
            }

            this.rvVentasPorProductoGlobal.LocalReport.DataSources.Clear();
            this.rvVentasPorProductoGlobal.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas", lstFiltroComparativo));
            this.rvVentasPorProductoGlobal.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas2", lstFiltro));
            this.rvVentasPorClienteGlobal.LocalReport.DataSources.Clear();
            this.rvVentasPorClienteGlobal.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas", lstFiltroComparativo));
            this.rvVentasPorClienteGlobal.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas2", lstFiltro));

            //decimal totalComparativo, total,dif,difP;
            //totalComparativo = lstFiltroComparativo.Sum(P => P.Total);
            //total = lstFiltro.Sum(P => P.Total);
            //dif = total - totalComparativo;
            //difP = (total / totalComparativo)-1;
            decimal totalComparativo = lstFiltroComparativo.Sum(P => P.Total * P.SubTotal);
            decimal total = lstFiltro.Sum(P => P.Total * P.SubTotal);
            this.rvVentasPorProductoGlobal.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas3",
                                                new List<EntPedido>() { new EntPedido() { SubTotal = totalComparativo, Total = total } }));
            this.rvVentasPorProductoGlobal.LocalReport.SetParameters(parmEmpresa);
            this.rvVentasPorProductoGlobal.RefreshReport();
            this.rvVentasPorClienteGlobal.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas3",
                                                new List<EntPedido>() { new EntPedido() { SubTotal = totalComparativo, Total = total } }));
            this.rvVentasPorClienteGlobal.LocalReport.SetParameters(parmEmpresa);
            this.rvVentasPorClienteGlobal.RefreshReport();

            if (tcReportesGlobalesGeneral.SelectedIndex == tcReportesGlobalesGeneral.TabPages.IndexOf(tpReporteAumento))
            {
                List<EntPedido> lstReporte = lstFiltro;
                //&& (P.ProductoId == 7
                // || P.ProductoId == 8
                // || P.ProductoId == 9
                // || P.ProductoId == 2177
                // || P.ProductoId == 2202)).ToList();

                List<EntPedido> lstReporteComparativo = lstFiltroComparativo;
                //&& (P.ProductoId == 7
                // || P.ProductoId == 8
                // || P.ProductoId == 9
                // || P.ProductoId == 2177
                // || P.ProductoId == 2202)).ToList();
                IgualarListas(lstReporte, lstReporteComparativo, CodigosBajos);

                if (CodigosBajos)
                {
                    foreach (EntPedido p in lstReporte.Where(P => P.Saldo > 0).ToList())
                    {
                        p.Estatus = false;
                    }
                }

                this.rvVentasPorProductoAlmacenGlobal.LocalReport.DataSources.Clear();
                this.rvVentasPorProductoAlmacenGlobal.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas", lstReporteComparativo));
                this.rvVentasPorProductoAlmacenGlobal.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas2", lstReporte.Where(P => P.Estatus).ToList()));
                rvVentasPorProductoAlmacenGlobal.LocalReport.SetParameters(parmEmpresa);
                rvVentasPorProductoAlmacenGlobal.RefreshReport();
            }
            else
            {
                this.rvVentasPorProductoAlmacenGlobal.LocalReport.DataSources.Clear();
                rvVentasPorProductoAlmacenGlobal.LocalReport.SetParameters(parmEmpresa);
                rvVentasPorProductoAlmacenGlobal.RefreshReport();
            }
            AgruparPorCodigoMuestraEnGV(lstFiltro, lstFiltroComparativo, gvProductosFiltro);
        } 

        void AgruparPorCodigoMuestraEnGV(List<EntPedido> lstFiltro, List<EntPedido> lstFiltroComparativo,
                                            DataGridView GvMuestra)
        {
            List<EntPedido> lstFiltroTodos = new List<EntPedido>();
            //AGREGA LAS DOS LISTAS EN UNA SOLA
            lstFiltroTodos.AddRange(lstFiltro);
            lstFiltroTodos.AddRange(lstFiltroComparativo);

            //AGRUPA POR CODIGO
            var gruposPorCodigo = lstFiltroTodos.GroupBy(p => p.ProductoId.ToString() + " - " + p.Descripcion).ToList();//r["PRO_CODIGO"].ToString()+" - "+ r["PRO_DESCRIPCION"].ToString();

            //GENERA UNA LISTA DONDE INGRESE NUEVO PRODUCTO AGRUPADO ANTERIORMENTE
            List<EntProducto> lstProductosFiltrados = new List<EntProducto>();
            foreach (var p in gruposPorCodigo)
            {
                int productoId = ConvierteTextoAInteger(p.Key.Split('-')[0]);
                if (lstProductosFiltrados.Where(P => P.Id == productoId).Count() <= 0)
                    lstProductosFiltrados.Add(new EntProducto() { Estatus = true, Id = productoId, Codigo = p.Key.Split('-')[1], Descripcion = p.Key.Split('-')[2] });
            }
            
            GvMuestra.DataSource = lstProductosFiltrados.OrderBy(P => P.Codigo).ToList();
            GvMuestra.Refresh();
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

        #endregion


        void CargaAlmacenes()
        {
            List<EntCatalogoGenerico> almacenes = new BusEmpresas().ObtieneAlmacenes(Program.UsuarioSeleccionado.CompañiaId, Program.UsuarioSeleccionado.Id);
            if (Program.UsuarioSeleccionado.Administrador)
                almacenes.Insert(0, new EntCatalogoGenerico() { Id = 0, Descripcion = "-TODOS-" });
            cmbAlmacenes.DataSource = almacenes;
            cmbAlmacenes.SelectedIndex = 0;
        }

        private void Reportes_Load(object sender, EventArgs e)
        {
            try
            {
                base.CargaPeriodosCmb(cmbPeriodoVentas, 1);//1:MESES
                CargaEmpresas();
                cmbEmpresas.SelectedIndex = 0;//((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);


                CargaAñosCmb(cmbAñosVentas);
                cmbMesesVentas.SelectedIndex = DateTime.Today.Month - 1;

                base.CargaTrabajadoresPorEmpresa(1, cmbTrabajadores); //EMPRESAID=1:BOTANAS

                tcReportes.TabPages.Remove(tpAnalitico);
                tcReportes.TabPages.Remove(tpAuxiliar);

                gvProductosFiltro.DataSource = null;

                CargaAlmacenes();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tcReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tcReportes.SelectedTab.Text == "Ventas")//PESTAÑA VENTAS
                {
                    if (!ReporteVentasCargado)
                    {
                        CargaAñosCmb(cmbAñosVentas);
                        cmbMesesVentas.SelectedIndex = DateTime.Today.Month - 1;

                        ReporteVentasCargado = true;
                    }
                }
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
                base.SetWaitCursor();
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
            finally { base.SetDefaultCursor(); }
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
                    if (tcReportes.SelectedIndex == 1)//PESTAÑA VENTAS
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

        int ProductoFiltroId { get; set; }
        void CargaDatosProductos(EntProducto Producto)
        {
            if (Producto == null)
                Producto = new EntProducto();
            txtProductoCodigoFiltroVentas.Text = Producto.Codigo;
            txtProductoDescripcionFiltroVentas.Text = Producto.Descripcion;
            this.ProductoFiltroId = Producto.Id;
        }
        
        private void btnBuscaProductoVentas_Click(object sender, EventArgs e)
        {
            try
            {
                int compañiaId = Program.UsuarioSeleccionado.CompañiaId;
                SeleccionaProducto vProd = new SeleccionaProducto(new BusProductos()
                                                                    .ObtieneProductosPorTipo(compañiaId, 1));
                if (vProd.ShowDialog() == DialogResult.OK)
                {
                    CargaDatosProductos(vProd.ProductoSeleccionado);
                    //FiltroVentas(this.ListaPedidos, this.ProductoFiltroId);
                    if(tcReportesVentas.SelectedIndex<3)
                        FiltraVentas(txtProductoDescripcionFiltroVentas.Text, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""), chkSoloDevoluciones.Checked);
                    else
                        AplicaFiltroVentasPorProducto();
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
                        FiltraVentas(txtProductoDescripcionFiltroVentas.Text, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""), chkSoloDevoluciones.Checked);
                    else
                        AplicaFiltroVentasPorProducto();
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
                        FiltraVentas(txtProductoDescripcionFiltroVentas.Text, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""), chkSoloDevoluciones.Checked);
                    else
                        AplicaFiltroVentasPorProducto();
                }

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtFiltroClientes_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tcReportesVentas.SelectedIndex < 3)
                    FiltraVentas(txtProductoDescripcionFiltroVentas.Text, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""), chkSoloDevoluciones.Checked);
                else
                    AplicaFiltroVentasPorProducto();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbTrabajadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tcReportesVentas.SelectedIndex < 3)
                    FiltraVentas(txtProductoDescripcionFiltroVentas.Text, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""), chkSoloDevoluciones.Checked);
                else
                    AplicaFiltroVentasPorProducto();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        int tabAnterior = 0;

        private void tcReportesVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabAnterior == 3 && tcReportesVentas.SelectedIndex < 3 || tcReportesVentas.SelectedIndex == 3)
                    btnRefrescarVentas.PerformClick();

                tabAnterior = tcReportesVentas.SelectedIndex;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductosFiltro_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void rdoReportePorProductoGeneral_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                rvVentasPorProducto.Visible = rdoReportePorProductoGeneral.Checked;
                rvVentasPorProductoAlmacen.Visible = !rdoReportePorProductoGeneral.Checked;
                if (chkSoloTotales.Checked)
                {
                    rvVentasPorProducto.Visible = false;
                    rvVentasPorProductoAlmacen.Visible = false;
                    rvVentasPorProductoGlobal.Visible = !rdoReportePorProductoGeneral.Checked;
                    rvVentasPorProductoTotales.Visible = rdoReportePorProductoGeneral.Checked;
                }
                else
                {
                    rvVentasPorProductoGlobal.Visible = false;
                    rvVentasPorProductoTotales.Visible = false;
                }
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
                    FiltraVentas(txtProductoDescripcionFiltroVentas.Text, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""), chkSoloDevoluciones.Checked);
                else
                    AplicaFiltroVentasPorProducto();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void chkSinComparativo_CheckedChanged(object sender, EventArgs e)
        {
            pnlPeriodoComparativo.Enabled = !chkSinComparativo.Checked;
        }

        private void btnLimpiaFiltroClienteVentas_Click(object sender, EventArgs e)
        {
            try
            {
                txtFiltroClientes.Clear();

                if (tcReportesVentas.SelectedIndex < 3)
                    FiltraVentas(txtProductoDescripcionFiltroVentas.Text, 0, txtFiltroClientes.Text, cmbTrabajadores.Text.Replace("-SELECCIONE-", ""), chkSoloDevoluciones.Checked);
                else
                    AplicaFiltroVentasPorProducto();
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

        private void gvProductosFiltro_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    //List<EntPedido> lstFiltro = this.ListaPedidos;
                    //List<EntPedido> lstFiltroComparativo = this.ListaPedidosComparativo;


                    ////if (SoloDevoluciones)
                    ////    lstFiltro = lstFiltro.Where(P => P.TipoPedidoId == (int)TipoPedido.DEVOLUCIONCORTESIA).ToList();

                    ////if (ProductoId > 0)
                    ////    lstFiltro = lstFiltro.Where(P => P.ProductoId == ProductoId).ToList();
                    ////if (ClienteFiltroId > 0)
                    ////    lstFiltro = lstFiltro.Where(P => P.ClienteId == ClienteFiltroId).ToList();
                    ////if (!string.IsNullOrWhiteSpace(ClienteFiltro))
                    ////    lstFiltro = lstFiltro.Where(P => (P.NumCliente + " " + P.Cliente).ToUpper().Contains(ClienteFiltro.ToUpper())).ToList();

                    ////if (!string.IsNullOrWhiteSpace(TrabajadorFiltro))
                    ////    lstFiltro = lstFiltro.Where(P => P.Trabajador.ToUpper().Contains(TrabajadorFiltro.ToUpper())).ToList();

                    //foreach (EntProducto p in ObtieneListaProductosFromGV(gvProductosFiltro).Where(P => !P.Estatus).ToList())
                    //{
                    //    lstFiltro.RemoveAll(P => P.ProductoId == p.ProductoId);
                    //    lstFiltroComparativo.RemoveAll(P => P.ProductoId == p.ProductoId);
                    //}

                    //this.rvVentasPorProductoAlmacenTotales.LocalReport.DataSources.Clear();
                    //this.rvVentasPorProductoAlmacenTotales.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas", lstFiltroComparativo));
                    //this.rvVentasPorProductoAlmacenTotales.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas2", lstFiltro));
                    //this.rvVentasPorProductoAlmacenTotales.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas3", new List<EntPedido>()));
                    ////rvVentasPorProductoAlmacenTotales.LocalReport.SetParameters(parmEmpresa);
                    //rvVentasPorProductoAlmacenTotales.RefreshReport();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void chkSeleccionaTodos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (EntProducto p in ObtieneListaProductosFromGV(gvProductosFiltro)) { 
                    p.Estatus = chkSeleccionaTodos.Checked;
                }
                gvProductosFiltro.Refresh();

                if (chkSeleccionaTodos.Checked)
                    btnRefrescarVentas.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        private void btnFiltrarProductos_Click(object sender, EventArgs e)
        {
            AplicaFiltroVentasPorProducto();

            //List<EntPedido> lstFiltro = this.ListaPedidos;
            //List<EntPedido> lstFiltroComparativo = this.ListaPedidosComparativo;

            ////if (SoloDevoluciones)
            ////    lstFiltro = lstFiltro.Where(P => P.TipoPedidoId == (int)TipoPedido.DEVOLUCIONCORTESIA).ToList();

            ////if (ProductoId > 0)
            ////    lstFiltro = lstFiltro.Where(P => P.ProductoId == ProductoId).ToList();
            ////if (ClienteFiltroId > 0)
            ////    lstFiltro = lstFiltro.Where(P => P.ClienteId == ClienteFiltroId).ToList();
            ////if (!string.IsNullOrWhiteSpace(ClienteFiltro))
            ////    lstFiltro = lstFiltro.Where(P => (P.NumCliente + " " + P.Cliente).ToUpper().Contains(ClienteFiltro.ToUpper())).ToList();

            ////if (!string.IsNullOrWhiteSpace(TrabajadorFiltro))
            ////    lstFiltro = lstFiltro.Where(P => P.Trabajador.ToUpper().Contains(TrabajadorFiltro.ToUpper())).ToList();

            //foreach (EntProducto p in ObtieneListaProductosFromGV(gvProductosFiltro).Where(P => !P.Estatus).ToList())
            //{
            //    lstFiltro.RemoveAll(P => P.ProductoId == p.Id);
            //    lstFiltroComparativo.RemoveAll(P => P.ProductoId == p.Id);
            //}

            //this.rvVentasPorProductoGlobal.LocalReport.DataSources.Clear();
            //this.rvVentasPorProductoGlobal.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas", lstFiltroComparativo));
            //this.rvVentasPorProductoGlobal.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas2", lstFiltro));
            //decimal totalComparativo = lstFiltroComparativo.Sum(P => P.Total * P.SubTotal);
            //decimal total = lstFiltro.Sum(P => P.Total * P.SubTotal);
            //this.rvVentasPorProductoGlobal.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas3", new List<EntPedido>() { new EntPedido() { SubTotal = totalComparativo, Total = total } }));
            //rvVentasPorProductoGlobal.RefreshReport();

            //this.rvVentasPorProductoAlmacenGlobal.LocalReport.DataSources.Clear();
            //this.rvVentasPorProductoAlmacenGlobal.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas", lstFiltroComparativo));
            //this.rvVentasPorProductoAlmacenGlobal.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas2", lstFiltro));
            //rvVentasPorProductoAlmacenGlobal.RefreshReport();


        }


        private void rdoPorProductoGlobal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                rvVentasPorProductoGlobal.Visible = rdoPorProductoGlobal.Checked;
                rvVentasPorClienteGlobal.Visible = rdoPorClienteGlobal.Checked;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

    }
}
