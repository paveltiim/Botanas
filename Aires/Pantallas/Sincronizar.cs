using AiresEntidades;
using AiresNegocio;
using AiresUtilerias;
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
    public partial class Sincronizacion : FormBase
    {
        public void VerificaEmpresa()
        {
            cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }
        public Sincronizacion()
        {
            InitializeComponent();
        }

        List<EntPedido> ListaPedidos;
        List<EntEmpresa> ListaEmpresas;
        
        #region Metodos
        
            string PathClienteDirectorioFacturas;
       
        
        #endregion
        
        int DiasPorSemana = 6;
        void InicializaPantalla()
        {

            if (Program.UsuarioSeleccionado.Id > 1)
            {
                tpImportaVentas.Hide();
                
            }
            //if(Program.EmpresaSeleccionada!=null)
            //    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }
        public void CargaEmpresas()
        {
            ListaEmpresas = new BusEmpresas().ObtieneEmpresas();

            Program.CambiaEmpresa = false;
            cmbEmpresas.DataSource = ListaEmpresas;
            Program.CambiaEmpresa = true;

            //CargaClientesEnPantallas();
        }


        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void gvProductosDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        bool EncuentraProductoDetalleEnListaProductos(List<EntProducto> ListaProductos, int ProductoId)
        {
            if (ListaProductos.Where(P => P.Id == ProductoId).Count() > 0)
                return true;
            else
                return false;
        }
        EntProducto ObtieneProductoDetalleEnListaProductos(List<EntProducto> ListaProductos, int ProductoId)
        {
            List<EntProducto> lst = ListaProductos.Where(P => P.Id == ProductoId).ToList();
            if (lst.Count > 0)
                return lst[0];

            return null;
        }
        int CuentaProductosEnListaProductos(List<EntProducto> ListaProductos, int ProductoId)
        {
            return ListaProductos.Where(P => P.ProductoId == ProductoId).Count();
        }
        List<EntProducto> ObtieneProductosEnListaProductos(List<EntProducto> ListaProductos, int ProductoId)
        {
            return ListaProductos.Where(P => P.ProductoId == ProductoId).ToList();
        }
        void AgregaProductoDetalle(int Id, int ProductoId, int IngresoId, int EmpresaId, string Serie, decimal PrecioCosto, decimal PrecioVenta, decimal PrecioVenta2, decimal PrecioEspecial)
        {
            EntProducto producto = new EntProducto()
            {
                Id = Id,
                ProductoId = ProductoId,
                IngresoId = IngresoId,
                EmpresaId = EmpresaId,
                Serie = Serie,
                PrecioCosto = PrecioCosto,
                PrecioVenta = PrecioVenta,
                PrecioVenta2 = PrecioVenta2,
                PrecioEspecial = PrecioEspecial,
                Fecha = DateTime.Now
            };
            producto.Id = new BusProductos().AgregaProductoDetalle(Id, producto);
        }
        void ActualizaProductoDetalle(int Id, int ProductoId, int IngresoId, int EmpresaId, string Serie, decimal PrecioCosto, decimal PrecioVenta, decimal PrecioVenta2, decimal PrecioEspecial)
        {
            EntProducto producto = new EntProducto()
            {
                Id = Id,
                ProductoId = ProductoId,
                IngresoId = IngresoId,
                EmpresaId = EmpresaId,
                Serie = Serie,
                PrecioCosto = PrecioCosto,
                PrecioVenta = PrecioVenta,
                PrecioVenta2 = PrecioVenta2,
                PrecioEspecial = PrecioEspecial,
                Fecha = DateTime.Now
            };
            new BusProductos().ActualizaProductoDetalle(producto);
        }
        void RevisaAgregaIngreso(EntProducto ProductoIngresa)
        {
            string descripcion = "";
            if (!string.IsNullOrWhiteSpace(ProductoIngresa.Ingreso))
                descripcion = ProductoIngresa.Ingreso;
            else
                descripcion = "IMPORTACION " + ProductoIngresa.Fecha.ToShortDateString();

            if (new BusProductos().ObtieneIngreso(ProductoIngresa.IngresoId).Id == 0)
                new BusProductos().AgregaIngreso(ProductoIngresa.IngresoId, new EntCatalogoGenerico() { Descripcion = descripcion, Fecha = ProductoIngresa.Fecha });
        }
        void RevisaAgregaProducto(List<EntProducto> ListaProductosDetalleRevisa, EntProducto ProductoDetalleRevisa)
        {
            int cantidadProductos = CuentaProductosEnListaProductos(ListaProductosDetalleRevisa, ProductoDetalleRevisa.ProductoId);
            if (cantidadProductos == 0)
                new Productos().AgregaProducto(ProductoDetalleRevisa.TipoProductoId, ProductoDetalleRevisa.Codigo, ProductoDetalleRevisa.Descripcion);
            else
            {
                new Productos().ActualizaProducto(ProductoDetalleRevisa.ProductoId, ProductoDetalleRevisa.TipoProductoId, ProductoDetalleRevisa.Codigo, ProductoDetalleRevisa.Descripcion);
                new BusProductos().ActualizaEstatusProducto(ProductoDetalleRevisa.ProductoId, true);
            }
            if (ProductoDetalleRevisa.TipoProductoId == 1)//PRODUCTO
                new Productos().AumentaProducto(ProductoDetalleRevisa.ProductoId, cantidadProductos);
            else//SERVICIO
                new Productos().AumentaProducto(ProductoDetalleRevisa.ProductoId, 1);
        }


        private void Reportes_Load(object sender, EventArgs e)
        {
            try
            {
                InicializaPantalla();
                CargaEmpresas();

                if (Program.EmpresaSeleccionada == null)
                    Program.EmpresaSeleccionada = SeleccionaEmpresa();

                if (Program.EmpresaSeleccionada !=null)
                {
                    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);

                    //CargaGvPedidos(Program.EmpresaSeleccionada.Id);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        
        #region Eventos Pestaña Impresion

        private void tcPedidosGrids_SelectedIndexChanged(object sender, EventArgs e)
            {
                
            }
           
        #endregion

        private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Program.CambiaEmpresa)
                {
                    Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                    ////CargaGvPedidos(Program.EmpresaSeleccionada.Id);
                    //btnRefrescar.PerformClick();
                    //btnRefrescarReporteVentas.PerformClick();
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
     
        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                gvProductosDetalle.DataSource = null;
                gvProductosDetalle.Refresh();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnVerFactura_Click(object sender, EventArgs e)
        {
            try
            {
                //EntPedido pedidoSeleccionado= ObtienePedidoFromGV(gvPedidos);
                //if (!pedidoSeleccionado.Facturado)
                //    throw new Exception("Pedido Sin Facturar");

                //MuestraArchivo(pedidoSeleccionado.RutaFactura);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }


        private void btnBuscaArchivoImportarEntradas_Click(object sender, EventArgs e)
        {
            try
            {
                string rutaArchivo = SeleccionaArchivo();
                System.IO.FileInfo fi = new FileInfo(rutaArchivo);
                if (fi != null)
                {
                    if (fi.Extension != ".xls")
                        MandaExcepcion("El archivo no es el correcto. \n Debe ser archivo de Excel (.xls)");

                    txtDescripcionFiltro.Text = rutaArchivo; //fi.FullName;
                    List<EntProducto> lst = new UtiLinqToExcel().ExcelToListaProductos(rutaArchivo);

                    gvProductosDetalle.DataSource = lst;
                    txtCantidadVentas.Text = lst.Count.ToString();
                }
                else
                    MandaExcepcion("ARCHIVO NO ENCONTRADO");
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }
        private void btnImportarEntradas_Click(object sender, EventArgs e)
        {
            try
            {
                //REVISAR FECHA DE IMPORTACION PARA EVITAR DUPLICAR
                //PENDIENTE TABLA DE REGISTRO DE IMPORTACIONES
                EntEmpresa empresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                List<EntProducto> listaProductosTodos = new BusProductos().ObtieneProductosDetalleTodos();
                List<EntProducto> listaProductosIngresar = ObtieneListaProductosFromGV(gvProductosDetalle);
                string seriesNoImportadas = "";

                int productoId = 0;
                int IngresoId = 0;
                bool productoRepetido = false;

                foreach (EntProducto p in listaProductosIngresar)
                {
                    if (!EncuentraProductoDetalleEnListaProductos(listaProductosTodos, p.Id))
                    {
                        if (IngresoId != p.IngresoId)
                        {
                            IngresoId = p.IngresoId;
                            RevisaAgregaIngreso(p);
                        }
                        if (productoId != p.ProductoId)
                        {
                            productoId = p.ProductoId;
                            RevisaAgregaProducto(listaProductosTodos, p);
                        }
                        AgregaProductoDetalle(p.Id, p.ProductoId, p.IngresoId, empresaSeleccionada.Id, p.Serie, p.PrecioCosto, p.PrecioVenta, p.PrecioVenta2, p.PrecioEspecial);
                    }
                    else
                    {
                        productoRepetido = true;

                        //ACTUALIZAR AL ACTUALIZAR EL EXCEL CON EMPRESAID
                        EntProducto productoEntonctrado = ObtieneProductoDetalleEnListaProductos(listaProductosTodos, p.Id);
                        if (productoEntonctrado.IngresoId != p.IngresoId)
                        {
                            RevisaAgregaIngreso(p);
                            ActualizaProductoDetalle(p.Id, p.ProductoId, productoEntonctrado.IngresoId, p.EmpresaId, p.Serie, p.PrecioCosto, p.PrecioVenta, p.PrecioVenta2, p.PrecioEspecial);
                        }
                        if (productoEntonctrado.ProductoId != p.ProductoId)
                        {
                            RevisaAgregaProducto(listaProductosTodos, p);
                        }

                        seriesNoImportadas += p.Serie + "\n";
                    }
                }

                if (productoRepetido)
                    MuestraMensaje(string.Format("Se encontraron Productos previamente importados:\n{0} \n¡IMPORTACIÓN COMPLETA!", seriesNoImportadas), "ALERTA");
                else
                    MuestraMensaje("¡IMPORTACIÓN COMPLETA!", "CONFIRMACIÓN");
                txtDescripcionFiltro.Clear();
                gvProductosDetalle.DataSource = null;
                gvProductosDetalle.Refresh();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        
        private void btnBuscaArchivoImportaVentas_Click(object sender, EventArgs e)
        {
            try
            {
                string rutaArchivo = SeleccionaArchivo();
                System.IO.FileInfo fi = new FileInfo(rutaArchivo);
                if (fi != null)
                {
                    if (fi.Extension != ".xls")
                        MandaExcepcion("El archivo no es el correcto. \n Debe ser archivo de Excel (.xls)");

                    txtRutaArchivoImportaVentas.Text = rutaArchivo; //fi.FullName;
                    List<EntPedido> lst = new UtiLinqToExcel().ExcelToListaPedidos(rutaArchivo);

                    gvPedidosImporta.DataSource = lst;
                    txtCantidadVentas.Text = lst.Count.ToString();
                }
                else
                    MandaExcepcion("ARCHIVO NO ENCONTRADO");
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }
        private void btnImportaVentas_Click(object sender, EventArgs e)
        {
            try
            {
                //REVISAR FECHA DE IMPORTACION PARA EVITAR DUPLICAR
                //PENDIENTE TABLA DE REGISTRO DE IMPORTACIONES
                Cursor.Current = Cursors.WaitCursor;

                EntEmpresa empresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                
                List<EntPedido> listaPedidosIngresar = ObtieneListaPedidosFromGV(gvPedidosImporta);
                EntPedido pedidoIngresar = new EntPedido();

                int pedidoId = 0;
                
                Ventas vVentas = new Pantallas.Ventas();
                foreach (EntPedido pe in listaPedidosIngresar)
                {
                    EntProducto productoPedido = pe.ProductoPedido;
                    if (pedidoId != pe.Id)
                    {
                        pedidoId = pe.Id;

                        EntCliente clientePedido = new BusClientes().ObtieneCliente(pe.ClienteId);
                        if (clientePedido.Id == 0)
                            new Clientes().AgregaCliente(pe.ClienteId,empresaSeleccionada.Id, pe.Cliente);
                        else
                            new Clientes().ActualizaCliente(clientePedido, pe.Cliente);
                        pedidoIngresar = vVentas.AgregarPedido(pe.ClienteId, pe.Detalle, "", pe.Total,pe.Total, pe.Fecha, pe.Fecha, 0, pe.Facturado, pe.EstatusId);

                        decimal pago = pe.Pago;
                        if (pago > 0)
                        {
                            vVentas.AgregarPago(pedidoIngresar.Id, pago);
                            vVentas.AumentaPagoPedido(pedidoIngresar.Id, 0);//SOLO PARA CAMBIAR ESTATUS. VERIFICA SI EL TOTAL DEL PEDIDO ESTA PAGADO, CAMBIA ESTATUS DE SER ASI.
                        }
                        if (pe.Facturado)
                        {
                            EntFactura factura = new EntFactura() { PedidoId = pedidoIngresar.Id, NumeroFactura = pe.Factura.Remove(0,2), UUID = pe.UUID, Ruta = pe.RutaFactura, Fecha = pe.FechaEntrega };
                            vVentas.AgregarFacturaPedido(factura);
                        }
                    }
                    vVentas.AgregarProductoDetallePedido(pedidoIngresar.Id, pe.ProductoPedido.Id, Convert.ToInt32(pe.ProductoPedido.Cantidad), pe.ProductoPedido.PrecioCosto, pe.ProductoPedido.PrecioVenta);
                    Productos vProd = new Productos();
                    if (pe.ProductoPedido.TipoProductoId == 1)
                    {
                        vProd.ActualizaEstatusProductoDetalle(pe.ProductoPedido, 2);//ESTATUS:2=ENTREGADO
                        vProd.AumentaProducto(pe.ProductoPedido.ProductoId, -Convert.ToInt32(pe.ProductoPedido.Cantidad));
                    }
                }

                MuestraMensaje("¡IMPORTACIÓN COMPLETA!", "CONFIRMACIÓN");

                txtRutaArchivoImportaVentas.Clear();
                txtCantidadPedidosImporta.Clear();
                gvPedidosImporta.DataSource = null;
                gvPedidosImporta.Refresh();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

    }
}
