using AiresEntidades;
using AiresNegocio;
using AiresUtilerias;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
//using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static iTextSharp.text.pdf.parser.LocationTextExtractionStrategy;

namespace Aires.Pantallas
{
    public partial class Entradas : FormBase
    {
        public void VerificaEmpresa()
        {
            cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }

        public Entradas()
        {
            InitializeComponent();
        }

        public Entradas(EntEmpresa Empresa, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente
                     ,int FormaPagoId, int MedioPagoId, string CondicionPago, string NumeroCuenta)
        {
            InitializeComponent();
            CargaProductosPedido(ListaProductos);
            CalculaSumaTotal(ListaProductos, txtTotal);
        }

        EntCliente ClienteSeleccionado = new EntCliente();
        EntProducto ProductoSeleccionado;
        List<EntProducto> ListaProductos = new List<EntProducto>();
        List<EntCliente> ListaClientes = new List<EntCliente>();

        List<EntProducto> ListaProductosPedido { get { return ObtieneListaProductosFromGV(gvProductosPedido); } }
        EntProducto ProductoPedidoSeleccionado { get { return ObtieneProductoFromGV(gvProductosPedido); } }
        /// <summary>
        /// ObtieneCatalogoGenericoFromCmb(cmbAlmacenes).Id
        /// </summary>
        int almacenId { get { return ObtieneCatalogoGenericoFromCmb(cmbAlmacenes).Id; } }

        #region Metodos


        void CargaAlmacenes()
        {
            List<EntCatalogoGenerico> almacenes = new BusEmpresas().ObtieneAlmacenes(Program.UsuarioSeleccionado.CompañiaId, Program.UsuarioSeleccionado.Id);
            cmbAlmacenes.DataSource = almacenes;
            cmbAlmacenes.SelectedIndex = 0;
        }

        public void CargaProductos(int AlmacenId)
        {
            this.ListaProductos = new BusProductos().ObtieneProductosPorAlmacen(
                        Program.EmpresaSeleccionada.CompañiaId, 
                        0, 
                        almacenId)
                        .OrderBy(P => P.Descripcion).ToList();
            gvProductosBusqueda.DataSource = this.ListaProductos;
        }
        public void CargaProductos(List<EntProducto> ListaProductos)
            {
                this.ListaProductos = ListaProductos;
                gvProductosBusqueda.DataSource = this.ListaProductos;
            }
        public void CargaProductosPedido(List<EntProducto> ListaProductosPedido)
        {
            gvProductosPedido.DataSource = ListaProductosPedido;
        }

        void AgregaProductoEnPedido(EntProducto ProductoSeleccionado, decimal CantidadAgrega)
        {
            List<EntProducto> productosPedido = ObtieneListaProductosFromGV(gvProductosPedido);

            if (CantidadAgrega > 0)
            {
                if (productosPedido == null)
                    productosPedido = new List<EntProducto>();

                //ProductoSeleccionado = ObtieneProductosFromGV(gvProductos);
                ProductoSeleccionado.Cantidad = CantidadAgrega;
                productosPedido.Add(ProductoSeleccionado);
            }
            else if (CantidadAgrega < 0)
            {
                productosPedido.Remove(ProductoSeleccionado);
                this.ListaProductos.Add(ProductoSeleccionado);
            }

            gvProductosPedido.DataSource = null;
            gvProductosPedido.DataSource = productosPedido;
            if (productosPedido.Count > 0)
            {
                gvProductosPedido.CurrentCell = gvProductosPedido[3, gvProductosPedido.Rows.Count - 1];
                gvProductosPedido.BeginEdit(true);
            }
            else
                txtBuscaProductoCodigo.Focus();

            CalculaSumaTotal(productosPedido, txtTotal);
            lbContadorSeries.Text = productosPedido.Count.ToString();

            //productosPedido[productosPedido.Count - 1].Descripcion=productosPedido[productosPedido.Count - 1].Descripcion.Replace("Solicitud:".PadLeft(5, '-') + txtSolicitud.Text,"");
            //productosPedido[productosPedido.Count - 1].Descripcion += "Solicitud:".PadLeft(5, '-') + txtSolicitud.Text;

        }

        /// <summary>
        /// Muestra el resultado de ListaProductos.Sum(P=>P.Precio) en TxtMuestraTotal.Text.
        /// </summary>
        /// <param name="CantidadSumar"></param>
        void CalculaSumaTotal(List<EntProducto> ListaProductos, TextBox TxtMuestraTotal)
        {
            decimal total, subtotal, cantidadIva, descuento;
            decimal cantidadIVARetenido = 0;

            if (this.ListaProductos == null)
            {
                total = 0;
                subtotal = 0;
                cantidadIva = 0;
            }
            else
            {
                total = ListaProductos.Sum(P => P.PrecioC);
                //subtotal = Math.Round(total, 2) / (1 + IVA); //Math.Round(total / (1 + IVA), 2);
                //cantidadIva = subtotal * this.IVA;
                //cantidadIva = Math.Round(total, 2) - subtotal;
            }
         
            descuento = ConvierteTextoADecimal(txtDescuento);
            TxtMuestraTotal.Text = FormatoMoney(total-descuento);
        }

        #endregion
        
        void VerificaDescripcion(TextBox TxtDescripcionEntrada)
        {
            if (string.IsNullOrWhiteSpace(TxtDescripcionEntrada.Text))
            {
                TxtDescripcionEntrada.Focus();
                throw new Exception("Agregue una Descripción");
            }
        }
        void VerificaProductosSeleccionados(List<EntProducto> ProductosSeleccionados)
        {
            if (ProductosSeleccionados == null || ProductosSeleccionados.Count == 0)
                throw new Exception("Agregue al menos un producto.");
        }
        public void CargaEntradas(DateTime FechaDesde, DateTime FechaHasta, int AlmacenId)
        {
            gvProductos.DataSource = null;
            gvIngresos.DataSource = new BusProductos().ObtieneMovimientosEntradasProductos(Program.UsuarioSeleccionado.CompañiaId,
                                                                                           FechaDesde, FechaHasta,
                                                                                           AlmacenId);
        }
        public void CargaProductosMovimientoEntrada(EntCatalogoGenerico Movimiento, int IngresoId)
        {
            List<EntProducto> listaProductos = new BusProductos().ObtieneMovimientosDetalleProductos(1, Movimiento.Id, IngresoId);//TIPOMOVIMIENTOID = 1: ENTRADAS
            entProductoBindingSource.DataSource = listaProductos;
            gvProductos.DataSource = listaProductos;//.OrderByDescending(P => P.Fecha).ToList();
            
            foreach (EntProducto c in listaProductos)
            {
                if (IngresoId == 0)
                {
                    c.Modelo = Movimiento.Descripcion;
                    c.Fecha = Movimiento.Fecha;
                    c.FechaCorta = Movimiento.Fecha.ToShortDateString();
                }
                List<EntProducto> precios = new BusProductos().ObtieneListaPreciosProducto(0, c.ProductoId);
                //decimal precioPorCantidad = new BusProductos().ObtienePrecioProductoCantidad(0, c.ProductoId, 6).PrecioVenta;
                c.PrecioVenta = precios.First().PrecioVenta2;// precioPorCantidad;
            }

            decimal cantidad = listaProductos.Sum(P => P.Cantidad);
            txtCantidadTotalEntradas.Text = cantidad.ToString();
            txtPrecioCostoTotalEntradas.Text = FormatoMoney(listaProductos.Sum(P => P.PrecioC));
            rvEntradas.RefreshReport();
        }
        void InicializaPantalla()
        {
            if (Program.EmpresaSeleccionada != null && cmbEmpresas.Items.Count > 0)
                cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);

            gvIngresos.DataSource = new List<EntCatalogoGenerico>();
            cmbMesesEntradas.SelectedIndex = DateTime.Today.Month - 1;
            CargaAñosCmb(cmbAñoEntradas);
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            try
            {
                InicializaPantalla();
                //CargaEmpresas();

                //if (Program.EmpresaSeleccionada == null)
                //    Program.EmpresaSeleccionada = SeleccionaEmpresa();
                //if (Program.EmpresaSeleccionada != null)
                //{
                //cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
                //CargaProductos(Program.EmpresaSeleccionada.Id);

                ClienteSeleccionado = null;
                gvProductosPedido.DataSource = null;

                CargaAlmacenes();
                if ((TipoUsuario)Program.UsuarioSeleccionado.TipoUsuarioId == TipoUsuario.GERENTEALMACEN)
                {
                    tcGeneral.TabPages.Remove(tpAgregaEntrada);
                }
                //}
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }
        
        private void txtProductoBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    ProductoSeleccionado = ObtieneProductoFromGV(gvProductosBusqueda);

                    if (ProductoSeleccionado != null)
                    {
                        AgregaProductoEnPedido(ProductoSeleccionado, 1);

                        this.ListaProductos.Remove(ProductoSeleccionado);
                        //CargaProductos(ListaProductos);
                        CargaProductos(ListaProductos);
                    }

                    gvProductosBusqueda.Visible = false;
                    LimpiaTextBox(pnlProductos);
                }
                else if (e.KeyChar == (char)Keys.Escape)
                {
                    gvProductosBusqueda.Visible = false;
                    LimpiaTextBox(pnlProductos);
                }

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ProductoSeleccionado=ObtieneProductoFromGV(gvProductosBusqueda);
                AgregaProductoEnPedido(ProductoSeleccionado, 1);

                ListaProductos.Remove(ProductoSeleccionado);
                //CargaProductos(ListaProductos);
                CargaProductos(ListaProductos);

                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProducto);
                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProductoCodigo);
                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProductoSerie);
                //gvProductos.Visible = false;
                //txtProductoBusqueda.Text = "";
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void txtProductoBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBuscaProducto.Text))
                {
                    gvProductosBusqueda.Visible = false;
                    //gvProductos.DataSource = ListaProductos;
                }
                else
                {
                    gvProductosBusqueda.DataSource = ListaProductos.Where(P => P.Descripcion.ToUpper().Contains(txtBuscaProducto.Text.ToUpper())).ToList();
                    gvProductosBusqueda.Visible = true;
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                FiltroProductos vProveedores = new FiltroProductos();
                vProveedores.CargaProductosDetalle(this.ListaProductos);
                if (vProveedores.ShowDialog() == DialogResult.OK)
                {
                    if (vProveedores.ProductoSeleccionado == null)
                        throw new Exception("Producto NO encontrado");

                    ProductoSeleccionado = vProveedores.ProductoSeleccionado;

                    AgregaProductoEnPedido(ProductoSeleccionado, 1);
                    this.ListaProductos.Remove(vProveedores.ProductoSeleccionado);

                    CargaProductos(this.ListaProductos);

                    OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProducto);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductosPedido_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CalculaSumaTotal(ObtieneListaProductosFromGV(gvProductosPedido), txtTotal);
            }catch(Exception ex) { MuestraMensaje(ex.Message, "ERROR"); }
        }
        bool ClientePublicoGeneral;

        private void gvProductosPedido_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                try
            {
                if (e.ColumnIndex == 0)
                {
                    EntProducto productoRemueve = ObtieneProductoFromGV(gvProductosPedido);

                    AgregaProductoEnPedido(productoRemueve, -productoRemueve.Cantidad);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductosPedido_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.ColumnIndex == 3 || e.ColumnIndex == 4 ) && e.RowIndex > -1)//CANTIDAD | PRECIO SIN IVA
                {
                    EntProducto productoSeleccionado = ObtieneProductoFromGV(gvProductosPedido);
                    decimal iva = 1.16m;
                    if (e.ColumnIndex == 4)//CANTIDAD 
                    {
                        //if (productoSeleccionado.TipoProductoId == 1)//1:PRODUCTO | 2:SERVICIO
                        //{
                            //if (!string.IsNullOrWhiteSpace(productoSeleccionado.Serie))
                            //{
                            //    productoSeleccionado.Cantidad = 1;
                            //    MandaExcepcion("PRODUCTOS CON SERIE NO SE PUEDEN VENDER EN CANTIDADES MAYORES A 1.");
                            //}

                            //List<EntProducto> listaProductosSeleccionados = this.ListaProductos.Where(P => P.ProductoId == productoSeleccionado.ProductoId).ToList();
                            //int existenciaProducto = listaProductosSeleccionados.Count() + 1;//COMPENSACION POR PRODUCTOSELECCIONADO
                            //int cantidadProductos = Convert.ToInt32(productoSeleccionado.Cantidad);
                            //if (productoSeleccionado.Cantidad > existenciaProducto)
                            //{
                            //    MuestraMensajeError("La Cantidad solicitada es mayor a la Existente. \n Existencia: " + existenciaProducto, "ERROR");
                            //    cantidadProductos = existenciaProducto;
                            //}
                            //for (int x = 0; x < cantidadProductos - 1; x++)
                            //{
                            //    AgregaRemueveProductoEnPedido(listaProductosSeleccionados[x], 1);
                            //}
                            //productoSeleccionado.Cantidad = 1;
                        //}
                    }
                    else if (e.ColumnIndex == 5)//PRECIO COSTO
                    {
                        //decimal precioSinIVA = productoSeleccionado.PrecioVentaSinIVA; // ConvierteTextoADecimal(gvProductosPedido.CurrentRow.Cells[6].Value.ToString());
                        //decimal precioIVA = Math.Round(precioSinIVA * iva, 2);

                        //productoSeleccionado.PrecioVenta = precioIVA;
                        ////gvProductosPedido.CurrentRow.Cells[7].Value = precioIVA;                    }
                    }
                    //gvProductosPedido.CurrentCell = gvProductosPedido[e.ColumnIndex + 1, gvProductosPedido.Rows.Count - 1];
                    //gvProductosPedido.BeginEdit(true);
                    gvProductosPedido.Refresh();
                    CalculaSumaTotal(this.ListaProductosPedido, txtTotal);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductosPedido_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string mensaje;

                EntEmpresa empresaSeleccionada = Program.EmpresaSeleccionada;
                empresaSeleccionada.RFC = Program.EmpresaSeleccionada.RFC;

                VerificaDescripcion(txtDescripcionEntrada);
                //Pendiente
                //VerificaClienteSeleccionado();
                VerificaProductosSeleccionados(this.ListaProductosPedido);

                mensaje = "¿Desea agregar Entrada de Productos? ";

                if (MuestraMensajeYesNo(mensaje, "CONFIRMAR") == DialogResult.Yes)
                {
                    base.SetWaitCursor();

                    string detallePedido = "";
                    EntPedido pedidoAgrega = new EntPedido();

                    List<EntProducto> productosSeleccionados = this.ListaProductosPedido;

                    foreach (EntProducto p in productosSeleccionados)
                        detallePedido += p.Cantidad + " " + p.Descripcion + " | ";

                    int orientacion = 1;
                    int almacenId = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes).Id;
                    int movimientoId = new BusPedidos().AgregaMovimientoMaster(txtDescripcionEntrada.Text,
                                                                                (int)TipoMovimiento.PRODUCCION, orientacion, 
                                                                                almacenId, 0,
                                                                                dtpFechaEntrada.Value, Program.UsuarioSeleccionado.Id);

                    Productos vProd = new Productos();
                    foreach (EntProducto p in productosSeleccionados) {
                        new BusPedidos().AgregaMovimientoDetalle(movimientoId, p.Id, p.Cantidad, p.PrecioC); 
                        vProd.AumentaProducto(p.Id, p.Cantidad);
                    }

                    new BusPedidos().AgregaMovimientoLote(movimientoId, orientacion);

                    btnCancelar.PerformClick();

                    MuestraMensaje("¡Entrada Registrada!");
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                ClienteSeleccionado = null;

                gvProductosPedido.DataSource = null;
                lbContadorSeries.Text = "0";
                txtDescripcionEntrada.Clear();

                CargaProductos(almacenId);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void txtCodigoProductoBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
                    gvProductosBusqueda.Visible = false;
                else
                {
                    gvProductosBusqueda.DataSource = ListaProductos.Where(P => P.Codigo.ToUpper()==((TextBox)sender).Text.ToUpper()).ToList();
                    gvProductosBusqueda.Visible = true;
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtBuscaProductoSerie_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
                    gvProductosBusqueda.Visible = false;
                else
                {
                    gvProductosBusqueda.DataSource = ListaProductos.Where(P => P.Serie.ToUpper().Contains(((TextBox)sender).Text.ToUpper())).ToList();
                    gvProductosBusqueda.Visible = true;
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductosBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                if (gvProductosBusqueda.CurrentRow.Index != gvProductosBusqueda.Rows.Count-1)
                                    ProductoSeleccionado = ObtieneProductoAnteriorFromGV(gvProductosBusqueda);
                                    else 
                    ProductoSeleccionado = ObtieneProductoFromGV(gvProductosBusqueda);
                
                AgregaProductoEnPedido(ProductoSeleccionado, 1);

                ListaProductos.Remove(ProductoSeleccionado);
                //CargaProductos(ListaProductos);
                CargaProductos(ListaProductos);

                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProducto);
                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProductoCodigo);
                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProductoSerie);
            }
        }

        private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Program.CambiaEmpresa)
                {
                    Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                    //btnCancelar.PerformClick();

                    CargaProductos(almacenId);
                    btnCancelar.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnRefrescarProductos_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CargaProductos(almacenId);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }


        private void btnRefrescaEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnBuscaEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                SeleccionaEmpresa vSeleccionaEmp = new Pantallas.SeleccionaEmpresa();
                if (vSeleccionaEmp.ShowDialog() == DialogResult.OK)
                {
                    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == vSeleccionaEmp.EmpresaSeleccionada.Id);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtDescuento_Leave(object sender, EventArgs e)
        {
            TextBoxDecimalMoney_Leave(sender, e);
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            try {
                CalculaSumaTotal(ListaProductosPedido, txtTotal);
            } catch(Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbAlmacenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                gvProductosPedido.DataSource = null;
                CargaProductos(almacenId);
                
                EntCatalogoGenerico almacen = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);
                if (cmbMesesEntradas.SelectedIndex >= 0)
                    CargaEntradas(FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                                    FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1),
                                    almacen.Id);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void cmbMesesEntradas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                if (cmbMesesEntradas.Focused)
                {
                    EntCatalogoGenerico almacen = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);
                    //if (rdoEntradasPorDia.Checked)
                    //    CargaEntradas(dtpEntradasFechaDia.Value.Date, dtpEntradasFechaDia.Value.Date.AddDays(1), almacen.Id);
                    //else if (rdoEntradasPorMes.Checked)
                    //{
                        if (cmbMesesEntradas.SelectedIndex >= 0)
                            CargaEntradas(FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                                          FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1),
                                          almacen.Id);
                    //}
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        void RefrescaEntradas()
        {
            EntCatalogoGenerico almacen = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);
            if (rdoEntradasPorMes.Checked)
            {
                if (cmbMesesEntradas.SelectedIndex >= 0)
                    CargaEntradas(FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                                  FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1),
                                    almacen.Id);
            }
            else
            {
                CargaEntradas(dtpFechaEntradasFiltro.Value.Date,
                              dtpFechaEntradasFiltro.Value.Date.AddDays(1),
                                almacen.Id);
            }
        }
        private void btnRefrescarEntradas_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                //EntCatalogoGenerico almacen = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);
                ////if (rdoEntradasPorDia.Checked)
                ////    CargaEntradas(dtpEntradasFechaDia.Value.Date, dtpEntradasFechaDia.Value.Date.AddDays(1), almacen.Id);
                ////else if (rdoEntradasPorMes.Checked)
                ////{
                //    if (cmbMesesEntradas.SelectedIndex >= 0)
                //        CargaEntradas(FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                //                        FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1),
                //                        almacen.Id);
                ////}
                RefrescaEntradas();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void gvIngresos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                EntCatalogoGenerico entrada = ObtieneCatalogoGenericoFromGV(gvIngresos);
                CargaProductosMovimientoEntrada(entrada, entrada.IdSecundario);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void rdoEntradasPorMes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pnlEntradasPorMes.Enabled = rdoEntradasPorMes.Checked;
                pnlPorDiaEntradas.Enabled = !rdoEntradasPorMes.Checked;
                RefrescaEntradas();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        private void btnExportaEntradas_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                
                List<EntProducto> listaProductos = new List<EntProducto>();
                List<EntCatalogoGenerico> listaEntradasSeleccionadas = ObtieneListaGenericaFromGV(gvIngresos).Where(P => P.Estatus).ToList();
                if (listaEntradasSeleccionadas.Count <= 0)
                    MandaExcepcion("SELECCIONE AL MENOS UNA ENTRADA");

                foreach (EntCatalogoGenerico en in listaEntradasSeleccionadas)
                {
                    List<EntProducto> listaProductosIngreso = new BusProductos().ObtieneMovimientosDetalleProductosSinDatosFactura(1, en.Id, en.IdSecundario);//MOVIMIENTOID | INGRESOID
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
                ImpresionEntradas vImprimeEntradas = new ImpresionEntradas(listaProductos);
                vImprimeEntradas.Show();

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }
        private void btnExportaEntradasPorMes_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();

                // Determine date range and almacen used to load the current entries grid
                EntCatalogoGenerico almacen = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);
                DateTime fechaDesde;
                DateTime fechaHasta;
                if (rdoEntradasPorMes.Checked)
                {
                    fechaDesde = FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas);
                    fechaHasta = FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1);
                }
                else
                {
                    fechaDesde = dtpFechaEntradasFiltro.Value.Date;
                    fechaHasta = dtpFechaEntradasFiltro.Value.Date.AddDays(1);
                }

                // Collect entry product details from the selected entries
                List<EntProducto> listaProductosEntrada = new List<EntProducto>();
                List<EntCatalogoGenerico> listaEntradasSeleccionadas = ObtieneListaGenericaFromGV(gvIngresos);
                foreach (EntCatalogoGenerico en in listaEntradasSeleccionadas)
                {
                    List<EntProducto> listaProductosIngreso = new BusProductos().ObtieneMovimientosDetalleProductosSinDatosFactura(1, en.Id, en.IdSecundario);
                    listaProductosEntrada.AddRange(listaProductosIngreso);
                }

                // Collect adjustment (TipoMovimientoId = 3) product details for the same period
                List<EntProducto> listaProductosAjuste = new List<EntProducto>();
                List<EntCatalogoGenerico> listaAjustes = new BusProductos().ObtieneMovimientosSalidasProductos(
                    Program.UsuarioSeleccionado.CompañiaId, fechaDesde, fechaHasta, almacen.Id, 3);
                foreach (EntCatalogoGenerico aj in listaAjustes)
                {
                    List<EntProducto> detalleAjuste = new BusProductos().ObtieneMovimientosDetalleProductosSinDatosFactura(1, aj.Id, 0);//SE PONE tipoMovimientoId=1 PARA PONER PRECIO EN COSTO
                    listaProductosAjuste.AddRange(detalleAjuste);
                }

                // Build a consolidated summary per product
                Dictionary<int, EntReporteEntradasAjustes> resumen = new Dictionary<int, EntReporteEntradasAjustes>();

                foreach (EntProducto ep in listaProductosEntrada)
                {
                    if (!resumen.ContainsKey(ep.ProductoId))
                    {
                        resumen[ep.ProductoId] = new EntReporteEntradasAjustes
                        {
                            ProductoId = ep.ProductoId,
                            Codigo = ep.Codigo,
                            Descripcion = ep.Descripcion
                        };
                    }
                    EntReporteEntradasAjustes fila = resumen[ep.ProductoId];
                    fila.CantidadEntrada += ep.Cantidad;
                    fila.TotalCostoEntrada += ep.Cantidad * ep.PrecioCosto;
                }

                // Compute weighted average unit cost for each entry product
                foreach (EntReporteEntradasAjustes fila in resumen.Values)
                {
                    fila.CostoEntrada = fila.CantidadEntrada > 0
                        ? Math.Round(fila.TotalCostoEntrada / fila.CantidadEntrada, 2)
                        : 0m;
                }

                foreach (EntProducto ep in listaProductosAjuste)
                {
                    if (!resumen.ContainsKey(ep.ProductoId))
                    {
                        resumen[ep.ProductoId] = new EntReporteEntradasAjustes
                        {
                            ProductoId = ep.ProductoId,
                            Codigo = ep.Codigo,
                            Descripcion = ep.Descripcion
                        };
                    }
                    EntReporteEntradasAjustes fila = resumen[ep.ProductoId];
                    fila.CantidadSalidaAjuste += ep.Cantidad;
                    fila.TotalCostoSalidaAjuste += ep.Cantidad * ep.PrecioCosto;
                }

                // Compute weighted average unit cost for each adjustment
                foreach (EntReporteEntradasAjustes fila in resumen.Values)
                {
                    fila.CostoSalidaAjuste = fila.CantidadSalidaAjuste > 0
                        ? Math.Round(fila.TotalCostoSalidaAjuste / fila.CantidadSalidaAjuste, 2)
                        : 0m;
                }

                List<EntReporteEntradasAjustes> listaReporte = resumen.Values
                    .OrderBy(r => r.Descripcion)
                    .ToList();

                ImpresionEntradasConAjustes vImprime = new ImpresionEntradasConAjustes(
                    listaReporte,
                    Program.EmpresaSeleccionada != null ? Program.EmpresaSeleccionada.Nombre : "",
                    fechaDesde.ToString("dd/MM/yyyy"),
                    fechaHasta.AddDays(-1).ToString("dd/MM/yyyy"), // fechaHasta was pushed +1 day for inclusive query; display original last day
                    almacen.Descripcion,
                    Program.UsuarioSeleccionado != null ? Program.UsuarioSeleccionado.Descripcion : "");
                vImprime.Show();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void chkSeleccionaTodasEntradas_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                List<EntCatalogoGenerico> listaEntradasSeleccionadas = ObtieneListaGenericaFromGV(gvIngresos);
                foreach (EntCatalogoGenerico en in listaEntradasSeleccionadas)
                {
                    en.Estatus = chkSeleccionaTodasEntradas.Checked;
                }
                gvIngresos.Refresh();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void dtpFechaEntrada_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
