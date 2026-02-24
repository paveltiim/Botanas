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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static iTextSharp.text.pdf.parser.LocationTextExtractionStrategy;

namespace Aires.Pantallas
{
    public partial class Salidas : FormBase
    {
        public void VerificaEmpresa()
        {
            cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }

        public Salidas()
        {
            InitializeComponent();
        }

        public Salidas(EntEmpresa Empresa, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente
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
        bool ClientePublicoGeneral;

        /// <summary>
        /// ObtieneCatalogoGenericoFromCmb(cmbAlmacenes).Id; 
        /// </summary>
        int AlmacenId { get { return ObtieneCatalogoGenericoFromCmb(cmbAlmacenes).Id; } }

        #region Metodos

        public void CargaEmpresas()
        {
            Program.CambiaEmpresa = false;
        if (Program.UsuarioSeleccionado.Id > 1)
            cmbEmpresas.DataSource = new BusEmpresas().ObtieneEmpresas().Where(P => P.UsuarioId == Program.UsuarioSeleccionado.Id).ToList();
        else
            cmbEmpresas.DataSource = new BusEmpresas().ObtieneEmpresas();
        //cmbEmpresas.DataSource = new BusEmpresas().ObtieneEmpresas();
            Program.CambiaEmpresa = true;
        }
        void CargaAlmacenes()
        {
            List<EntCatalogoGenerico> almacenes = new BusEmpresas().ObtieneAlmacenes(Program.UsuarioSeleccionado.CompañiaId, Program.UsuarioSeleccionado.Id);
            cmbAlmacenes.DataSource = almacenes;
            cmbAlmacenes.SelectedIndex = 0;

            List<EntCatalogoGenerico> almacenesEntradas = new BusEmpresas().ObtieneAlmacenes(Program.UsuarioSeleccionado.CompañiaId, Program.UsuarioSeleccionado.Id);
            cmbAlmacenesEntrada.DataSource = almacenesEntradas;
            cmbAlmacenesEntrada.SelectedIndex = almacenesEntradas.Count-1;
        }
        void CargaVendedores()
        {
            List<EntTrabajador> vendedores = new List<EntTrabajador>();
            vendedores.Add(new EntTrabajador() { Id = 1, Descripcion = "VENDEDOR RUTA 1" });
            //List<EntCatalogoGenerico> almacenes = new BusTrabajadores().ObtieneTrabajadores(1, Program.UsuarioSeleccionado.Id);
            cmbTrabajadoresDestino.DataSource = vendedores;
            cmbTrabajadoresDestino.SelectedIndex = 0;
        }
        void CargaTipoSalidas()
        {
            List<EntCatalogoGenerico> salidas = new List<EntCatalogoGenerico>();
            //ID SE USA PARA TIPOMOVIMIENTO; IDSECUNDARIO SE USA PARA TIPOPEDIDO
            salidas.Add(new EntCatalogoGenerico() { Id=2, IdSecundario=1, Descripcion ="VENTA"});
            salidas.Add(new EntCatalogoGenerico() { Id = 3, IdSecundario=2, Descripcion = "AJUSTE" });
            salidas.Add(new EntCatalogoGenerico() { Id = 4, IdSecundario=3, Descripcion = "TRASPASO" });
            salidas.Add(new EntCatalogoGenerico() { Id = 8, IdSecundario=8, Descripcion = "TRASPASO VENTA MÓVIL" });
            cmbTipoSalidas.DataSource = salidas;
            cmbTipoSalidas.SelectedIndex = 0;
        }
        public void CargaClientes()
        {
            this.ListaClientes = new BusClientes().ObtieneClientes(Program.EmpresaSeleccionada.Id);
            gvClientes.DataSource = this.ListaClientes;
            this.ClienteSeleccionado = null;
        }

        public void CargaProductos(int AlmacenId, int TipoProductoId)
        {
            this.ListaProductos = new BusProductos().ObtieneProductosExistenciaPorAlmacen(Program.UsuarioSeleccionado.CompañiaId, 0, AlmacenId, TipoProductoId)
                                                                                        .OrderBy(P => P.Descripcion).ToList();
            gvProductosBusqueda.DataSource = this.ListaProductos;
        }
        public void CargaProductos(List<EntProducto> ListaProductos)
            {
                this.ListaProductos = ListaProductos;
                gvProductosBusqueda.DataSource = ListaProductos;
            }
        public void CargaProductosPedido(List<EntProducto> ListaProductosPedido)
        {
            gvProductosPedido.DataSource = ListaProductosPedido;
        }

        void CargaDatosCliente(EntCliente Cliente)
        {
            txtNombre.Text = Cliente.Nombre;
            txtNombreFiscal.Text = Cliente.NombreFiscal;
            txtRFC.Text = Cliente.RFC;
        }

        void AgregaProductoEnPedido(EntProducto ProductoSeleccionado, decimal CantidadAgrega)
        {
            List<EntProducto> productosPedido = ObtieneListaProductosFromGV(gvProductosPedido);

            if (CantidadAgrega > 0)
            {
                if (productosPedido == null)
                    productosPedido = new List<EntProducto>();

                //ProductoSeleccionado = ObtieneProductosFromGV(gvProductos);
                ProductoSeleccionado.Descripcion = ProductoSeleccionado.Descripcion.Replace(" MARCA: " + ProductoSeleccionado.Marca + " MODELO: " + ProductoSeleccionado.Modelo, "");
                if(!string.IsNullOrWhiteSpace(ProductoSeleccionado.Marca))
                    ProductoSeleccionado.Descripcion += " | MARCA: " + ProductoSeleccionado.Marca;
                if (!string.IsNullOrWhiteSpace(ProductoSeleccionado.Modelo))
                    ProductoSeleccionado.Descripcion += " | MODELO: " + ProductoSeleccionado.Modelo;
                ProductoSeleccionado.Cantidad = CantidadAgrega;
                ProductoSeleccionado.PrecioVenta = ProductoSeleccionado.PrecioCosto;
                productosPedido.Add(ProductoSeleccionado);
            }
            else if (CantidadAgrega < 0)
            {
                productosPedido.Remove(ProductoSeleccionado);
                this.ListaProductos.Add(ProductoSeleccionado);
            }

            gvProductosPedido.DataSource = null;
            gvProductosPedido.DataSource = productosPedido;

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

            if (ListaProductos == null)
            {
                total = 0;
                subtotal = 0;
                cantidadIva = 0;
            }
            else
            {
                total = ListaProductos.Sum(P => P.Precio);

                //subtotal = Math.Round(total, 2) / (1 + IVA); //Math.Round(total / (1 + IVA), 2);
                //cantidadIva = subtotal * this.IVA;
                //cantidadIva = Math.Round(total, 2) - subtotal;
            }
         
            descuento = ConvierteTextoADecimal(txtDescuento);
            TxtMuestraTotal.Text = FormatoMoney(total-descuento);
        }

        #endregion
        public void CargaSalidas(DateTime FechaDesde, DateTime FechaHasta, int AlmacenId)
        {
            gvDetalleProductosSalida.DataSource = null;
            gvSalidas.DataSource = new BusProductos().ObtieneMovimientosSalidasProductos(Program.UsuarioSeleccionado.CompañiaId,
                                                                                            FechaDesde, FechaHasta,
                                                                                            AlmacenId);
        }

        void VerificaDescripcion(TextBox TxtDescripcion)
        {
            if (string.IsNullOrWhiteSpace(TxtDescripcion.Text))
            {
                TxtDescripcion.Focus();
                throw new Exception("Agregue una Descripción");
            }
        }
        /// <summary>
        /// Verifica que se halla seleccionado cliente.
        /// Revisa el SelectedIndex de cmbClientes. Si su valor es 0 o -1, Manda excepción y Focus() en cmbClientes.
        /// </summary>
        void VerificaClienteSeleccionado()
        {
            if (this.ClienteSeleccionado == null)
                throw new Exception("Seleccione un Cliente");
        }
        void VerificaProductosSeleccionados(List<EntProducto> ProductosSeleccionados)
        {
            if (ProductosSeleccionados == null || ProductosSeleccionados.Count == 0)
                throw new Exception("Agregue al menos un producto.");
        }
        public void CargaProductosMovimientoSalida(int MovimientoId)
        {
            List<EntProducto> listaProductos = new BusProductos().ObtieneMovimientosDetalleProductos(2, MovimientoId);
            entProductoBindingSource.DataSource = listaProductos;
            gvDetalleProductosSalida.DataSource = listaProductos;//.OrderByDescending(P => P.Fecha).ToList();

            decimal cantidad = listaProductos.Sum(P => P.Cantidad);
            txtCantidadTotalEntradas.Text = cantidad.ToString();
            txtPrecioTotalEntradas.Text = FormatoMoney(listaProductos.Sum(P => P.Precio));
            rvSalidas.RefreshReport();
        }
        void InicializaPantalla()
        {
            if (Program.EmpresaSeleccionada != null && cmbEmpresas.Items.Count > 0)
                cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);

            gvSalidas.DataSource = new List<EntCatalogoGenerico>();
            cmbMesesEntradas.SelectedIndex = DateTime.Today.Month - 1;
            CargaAñosCmb(cmbAñoEntradas);

            if (Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.MASTER)
                btnCancelarSalida.Visible = true;

            cmbTipoProductoFiltro.SelectedIndex = 0;
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
                //CargaVendedores();
                CargaTipoSalidas();

                base.CargaTrabajadoresPorEmpresa(Program.UsuarioSeleccionado.CompañiaId, 2, cmbTrabajadoresDestino);
                
                if ((TipoUsuario)Program.UsuarioSeleccionado.TipoUsuarioId == TipoUsuario.GERENTEALMACEN)
                {
                    tcGeneral.TabPages.Remove(tpAgregaSalida);
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void cmbTipoSalidas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pnlAlmacenEntrada.Visible = false;
                pnlVendedoresDestino.Visible = false;
                pnlDatosCliente.Visible = false;
                txtDescripcionSalida.Text = cmbTipoSalidas.Text;
                if (ObtieneCatalogoGenericoFromCmb(cmbTipoSalidas).Id == (int)TipoMovimiento.VENTA)
                {
                    pnlDatosCliente.Visible = true;
                    pnlDatosCliente.Enabled = true;
                    CargaClientes();
                }
                else if (ObtieneCatalogoGenericoFromCmb(cmbTipoSalidas).Id == (int)TipoMovimiento.TRASPASO)
                {
                    pnlAlmacenEntrada.Visible = true;
                }
                else if (ObtieneCatalogoGenericoFromCmb(cmbTipoSalidas).Id == (int)TipoMovimiento.TRASPASOCONSIGNA)
                {
                    pnlVendedoresDestino.Visible = true;
                }
                else
                {
                    pnlDatosCliente.Enabled = false;
                    LimpiaTextBox(pnlDatosCliente);
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void txtClienteBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtClienteBusqueda.Text))
                    gvClientes.Visible = false;
                else
                {
                    List<EntCliente> clientesEncontrados = this.ListaClientes.Where(P => P.Nombre.ToUpper()
                                                                                            .Contains(txtClienteBusqueda.Text.ToUpper())).ToList();
                    gvClientes.DataSource = clientesEncontrados;
                    gvClientes.Visible = true;
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtClienteBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    this.ClienteSeleccionado = ObtieneClienteFromGV(gvClientes);

                    if (this.ClienteSeleccionado != null)
                    {
                        CargaDatosCliente(this.ClienteSeleccionado);
                    }

                    OcultaBuscadorGrid(gvClientes, txtClienteBusqueda);
                }
                else if (e.KeyChar == (char)Keys.Escape)
                {
                    OcultaBuscadorGrid(gvClientes, txtClienteBusqueda);
                }

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtClienteBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == (int)Keys.Down)
                {
                    gvClientes.Focus();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                FiltroClientes vClientes = new FiltroClientes();
                if (vClientes.ShowDialog() == DialogResult.OK)
                {
                    this.ClienteSeleccionado = vClientes.ClienteSeleccionado;
                    if (this.ClienteSeleccionado != null)
                        CargaDatosCliente(this.ClienteSeleccionado);
                    OcultaBuscadorGrid(gvClientes, txtClienteBusqueda);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.ClienteSeleccionado = ObtieneClienteFromGV(gvClientes);

                if (this.ClienteSeleccionado != null)
                    CargaDatosCliente(this.ClienteSeleccionado);

                OcultaBuscadorGrid(gvClientes, txtClienteBusqueda);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void gvClientes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                if (gvClientes.CurrentRow.Index != gvClientes.Rows.Count - 1)
                    this.ClienteSeleccionado = ObtieneClienteAnteriorFromGV(gvClientes);
                else
                    this.ClienteSeleccionado = ObtieneClienteFromGV(gvClientes);

                if (this.ClienteSeleccionado != null)
                    CargaDatosCliente(this.ClienteSeleccionado);

                OcultaBuscadorGrid(gvClientes, txtClienteBusqueda);
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
                ProductoSeleccionado = ObtieneProductoFromGV(gvProductosBusqueda);
                AgregaProductoEnPedido(this.ProductoSeleccionado, 1);

                this.ListaProductos.Remove(this.ProductoSeleccionado);
                //CargaProductos(ListaProductos);
                CargaProductos(this.ListaProductos);

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
                FiltroProductos vProducto = new FiltroProductos();
                vProducto.CargaProductosDetalle(this.ListaProductos);
                if (vProducto.ShowDialog() == DialogResult.OK)
                {
                    if (vProducto.ProductoSeleccionado == null)
                        throw new Exception("Producto NO encontrado");

                    this.ProductoSeleccionado = vProducto.ProductoSeleccionado;

                    AgregaProductoEnPedido(this.ProductoSeleccionado, 1);
                    this.ListaProductos.Remove(vProducto.ProductoSeleccionado);

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
                    if (e.ColumnIndex == 3)//CANTIDAD 
                    {
                        if (productoSeleccionado.Cantidad < 0)
                            productoSeleccionado.Cantidad = 0;

                        if (productoSeleccionado.Cantidad > productoSeleccionado.Existencia)
                        {
                            MuestraMensajeError("La Cantidad solicitada es mayor a la Existente. \n Existencia: "
                                                   + productoSeleccionado.Existencia, "ERROR");
                            productoSeleccionado.Cantidad = productoSeleccionado.Existencia;
                        }
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
        /// <summary>
        /// Agrega nuevo registro del Pedido solicitado.
        /// </summary>
        /// <param name="pedido"></param>
        public EntPedido AgregarPedido(int TipoPedidoId, int ClienteId, string Detalle, string Solicitud, string Observaciones, decimal Total, decimal Pago, DateTime Fecha, DateTime FechaEntrega, int EmpleadoId, bool Facturado, int EstatusId)
        {
            EntPedido pedido = new EntPedido()
            {
                TipoPedidoId = TipoPedidoId,
                ClienteId = ClienteId,
                Detalle = Detalle,
                Solicitud = Solicitud,
                Observaciones = Observaciones,
                Total = Total,
                Pago = Pago,
                Fecha = Fecha,
                FechaEntrega = FechaEntrega,
                Facturado = Facturado,
                EmpleadoId = EmpleadoId,
                EstatusId = EstatusId
            };
            pedido.Id = new BusPedidos().AgregaPedido(pedido);
            return pedido;
        }
        /// <summary>
        /// Agrega nueva relación de Producto con el Pedido.
        /// </summary>
        /// <param name="pedido"></param>
        public void AgregarProductoDetallePedido(int PedidoId, int ProductoId, decimal Cantidad, decimal PrecioCosto, decimal PrecioVenta)
        {
            EntPedido pedido = new EntPedido()
            {
                Id = PedidoId,
                Fecha = DateTime.Now
            };

            EntProducto producto = new EntProducto()
            {
                Id = ProductoId,
                Cantidad = Cantidad,
                PrecioCosto = PrecioCosto,
                PrecioVenta = PrecioVenta
            };
            new BusPedidos().AgregaProductoDetallePedido(pedido, producto);
        }
        
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string mensaje;

                EntEmpresa empresaSeleccionada = Program.EmpresaSeleccionada;
                VerificaDescripcion(txtDescripcionSalida);

                int tipoMovimientoId = ObtieneCatalogoGenericoFromCmb(cmbTipoSalidas).Id;
                int tipoPedidoId = ObtieneCatalogoGenericoFromCmb(cmbTipoSalidas).IdSecundario;//tipoMovimientoId - 1; //LOS TIPOS DE PEDIDOS SON CON ID MENOR(-1). EXCEPTO EL TRASPASO VENTA DETALLE
                if (tipoMovimientoId == (int)TipoMovimiento.VENTA)
                    VerificaClienteSeleccionado();
                else
                    this.ClienteSeleccionado = new EntCliente() { Id = 0, Nombre = "-NA-" };
                
                VerificaProductosSeleccionados(ListaProductosPedido);

                mensaje = "¿Desea dar Salida a los Productos seleccionados? ";

                if (MuestraMensajeYesNo(mensaje, "CONFIRMAR") == DialogResult.Yes)
                {
                    base.SetWaitCursor();

                    string detallePedido = "";
                    string tipoMovimiento = "-"+cmbTipoSalidas.Text+"-";
                    EntPedido pedidoAgrega = new EntPedido();
                    List<EntProducto> productosSeleccionados = ListaProductosPedido;
                    EntCatalogoGenerico almacenOrigen = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);

                    foreach (EntProducto p in productosSeleccionados)
                        detallePedido += p.Cantidad + " " + p.Descripcion + " | ";

                    pedidoAgrega = AgregarPedido(tipoPedidoId, this.ClienteSeleccionado.Id, 
                                                detallePedido.Remove(detallePedido.Length - 2), "", "",
                                                ConvierteTextoADecimal(txtTotal.Text), 0, DateTime.Now, DateTime.Today,
                                                Program.UsuarioSeleccionado.Id, false, 1);
                    Productos vProd = new Productos();
                    foreach (EntProducto p in productosSeleccionados)
                    {
                        AgregarProductoDetallePedido(pedidoAgrega.Id, p.Id, p.Cantidad, p.PrecioCosto, p.PrecioVenta);
                        vProd.AumentaProducto(p.Id, -p.Cantidad);
                    }

                    int orientacion = 2;
                    int movimientoId = new BusPedidos().AgregaMovimientoMaster(txtDescripcionSalida.Text, tipoMovimientoId, orientacion, almacenOrigen.Id, pedidoAgrega.Id, Program.UsuarioSeleccionado.Id);

                    foreach (EntProducto p in productosSeleccionados) {
                        new BusPedidos().AgregaMovimientoDetalle(movimientoId, p.Id, p.Cantidad, p.Precio);
                    }

                    new BusPedidos().AgregaMovimientoLote(movimientoId, orientacion);

                    if (tipoMovimientoId == (int)TipoMovimiento.TRASPASO)
                    {
                        EntCatalogoGenerico almacenDestino= ObtieneCatalogoGenericoFromCmb(cmbAlmacenesEntrada);
                        new BusPedidos().AgregaMovimientoTraspaso(0, movimientoId, almacenOrigen.Id,almacenDestino.Id,"", 1,Program.UsuarioSeleccionado.Id);
                    }
                    else if (tipoMovimientoId == (int)TipoMovimiento.TRASPASOCONSIGNA)
                    {
                        EntTrabajador trabajadorDestino = ObtieneTrabajadorFromCmb(cmbTrabajadoresDestino);
                        new BusPedidos().AgregaMovimientoTraspasoConsigna(0, movimientoId, almacenOrigen.Id, trabajadorDestino.Id, "", 1, Program.UsuarioSeleccionado.Id);
                    }

                    MuestraMensaje("¡Salida "+tipoMovimiento+" Registrada!");


                    if (MuestraMensajeYesNo("¿Desea Imprimir Salida "+ tipoMovimiento+ "?") == DialogResult.Yes)
                    {
                        EntPedido pedidoImprime = new EntPedido();
                        pedidoImprime.NumOrden = cmbTipoSalidas.Text.Remove(2)+"-"+ pedidoAgrega.Id.ToString().PadLeft(6,'0');
                        pedidoImprime.SubTotal = ConvierteTextoADecimal(txtTotal);
                        pedidoImprime.IVA = 0;
                        pedidoImprime.Total = ConvierteTextoADecimal(txtTotal);
                        //pedidoImprime.TotalUSD = pedidoImprime.Total;
                        pedidoImprime.Observaciones = txtDescripcionSalida.Text;
                        ImprimirSalida(this.ClienteSeleccionado, pedidoImprime, productosSeleccionados,
                                        pbImpresionFondoBlanco.Image, pbLogo.Image, pbLeyendaCotizacion.Image);
                    }

                    btnCancelar.PerformClick();

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
                this.ClienteSeleccionado = null;

                gvProductosPedido.DataSource = null;
                lbContadorSeries.Text = "0";
                txtTotal.Text = FormatoMoney(0);
                txtDescripcionSalida.Clear();
                CargaProductos(AlmacenId, cmbTipoProductoFiltro.SelectedIndex+1);                
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
                    gvProductosBusqueda.DataSource = this.ListaProductos.Where(P => P.Codigo.ToUpper().Contains(((TextBox)sender).Text.ToUpper())).ToList();
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

                    CargaProductos(AlmacenId, cmbTipoProductoFiltro.SelectedIndex + 1);
                    btnCancelar.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnRefrescarProductos_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                gvProductosPedido.DataSource = null;
                CargaProductos(AlmacenId, cmbTipoProductoFiltro.SelectedIndex + 1);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
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
                CargaProductos(this.AlmacenId, cmbTipoProductoFiltro.SelectedIndex + 1);
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
                        CargaSalidas(FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                                      FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1),
                                      almacen.Id);
                    //}
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnRefrescarEntradas_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                EntCatalogoGenerico almacen = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);
                //if (rdoEntradasPorDia.Checked)
                //    CargaEntradas(dtpEntradasFechaDia.Value.Date, dtpEntradasFechaDia.Value.Date.AddDays(1), almacen.Id);
                //else if (rdoEntradasPorMes.Checked)
                //{
                if (cmbMesesEntradas.SelectedIndex >= 0)
                    CargaSalidas(FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                                    FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1),
                                    almacen.Id);
                //}
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void gvIngresos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                EntCatalogoGenerico entrada = ObtieneCatalogoGenericoFromGV(gvSalidas);
                CargaProductosMovimientoSalida(entrada.Id);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        public void ReingresoDeProducto(int AlmacenId, string TipoMovimientoRegresa, string IdentificadorMovimiento, 
                                        List<EntProducto> ProductosReingresa)
        {
            int orientacion = 1;
            int movimientoId = new BusPedidos().AgregaMovimientoMaster("REINGRESO POR CANCELACIÓN DE " + TipoMovimientoRegresa + ". " +
                " "+ TipoMovimientoRegresa + ": " + IdentificadorMovimiento,
                (int)TipoMovimiento.CANCELACION, orientacion, AlmacenId, 0, Program.UsuarioSeleccionado.Id);

            Productos vProd = new Productos();
            foreach (EntProducto p in ProductosReingresa)
            {
                new BusPedidos().AgregaMovimientoDetalle(movimientoId, p.ProductoId, p.Cantidad, p.PrecioC);
                vProd.AumentaProducto(p.Id, p.Cantidad);
            }

            new BusPedidos().AgregaMovimientoLote(movimientoId, orientacion);
        }

        private void btnCancelarSalida_Click(object sender, EventArgs e)
        {
            try
            {
                EntCatalogoGenerico traspasoSeleccionado = ObtieneCatalogoGenericoFromGV(gvSalidas);
                CargaProductosMovimientoSalida(traspasoSeleccionado.Id);

                if (MuestraMensajeYesNo(string.Format("¿Seguro desea REINGRESAR la Salida seleccionada? \n "), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    base.SetWaitCursor();
                    string detallePedido = "";

                    List<EntProducto> productosSeleccionados = ObtieneListaProductosFromGV(gvDetalleProductosSalida);

                    foreach (EntProducto p in productosSeleccionados)
                        detallePedido += p.Cantidad + " " + p.Descripcion + " | ";

                    //SeleccionaAlmacen vAlmacen = new SeleccionaAlmacen();
                    //if (vAlmacen.ShowDialog() == DialogResult.OK)
                    //{
                    //new RegistrosNeue().ReingresoDeProducto(vAlmacen.AlmacenSeleccionado.Id, pedidoSeleccionado.Id, pedidoSeleccionado.Factura);

                    //new BusPedidos().CancelaPedidoTodo(pedidoSeleccionado.Id);
                    ReingresoDeProducto(this.AlmacenId,"SALIDA", traspasoSeleccionado.Id.ToString().PadLeft(5,'0'),productosSeleccionados);

                    btnRefrescarSalidas.PerformClick();

                    MuestraMensaje("¡Salida Reingresada!", "CANCELACIÓN VENTA");
                    //}
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnCancelarSinDevolucion_Click(object sender, EventArgs e)
        {
            try
            {
                EntCatalogoGenerico traspasoSeleccionado = ObtieneCatalogoGenericoFromGV(gvSalidas);
                CargaProductosMovimientoSalida(traspasoSeleccionado.Id);

                if (MuestraMensajeYesNo(string.Format("¿Seguro desea Cancelar la Salida seleccionada? \n "), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    base.SetWaitCursor();
                    //string detallePedido = "";

                    List<EntProducto> productosSeleccionados = ObtieneListaProductosFromGV(gvDetalleProductosSalida);

                    //foreach (EntProducto p in productosSeleccionados)
                    //    detallePedido += p.Cantidad + " " + p.Descripcion + " | ";
                    ReingresoDeProducto(this.AlmacenId, "SALIDA", traspasoSeleccionado.Id.ToString().PadLeft(5, '0'), productosSeleccionados);

                    btnRefrescarSalidas.PerformClick();

                    MuestraMensaje("¡Salida Cancelada Sin Reingreso!", "CANCELACIÓN VENTA");
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnImprimeSalida_Click(object sender, EventArgs e)
        {
            try {
                EntCatalogoGenerico traspasoSeleccionado = ObtieneCatalogoGenericoFromGV(gvSalidas);
                EntPedido pedidoImprime = new BusPedidos().ObtienePedido(traspasoSeleccionado.IdSecundario);
                if(!string.IsNullOrWhiteSpace(traspasoSeleccionado.Descripcion))
                    pedidoImprime.NumOrden = traspasoSeleccionado.Descripcion.Remove(2) + "-" + pedidoImprime.Id.ToString().PadLeft(6, '0');
                else
                    pedidoImprime.NumOrden = pedidoImprime.Id.ToString().PadLeft(6, '0');
                //pedidoImprime.SubTotal = ConvierteTextoADecimal(pedidoImprime.SubTotal;
                pedidoImprime.IVA = 0;
                //pedidoImprime.Total = ConvierteTextoADecimal(txtTotal);
                //pedidoImprime.Observaciones = txtDescripcionSalida.Text;

                CargaProductosMovimientoSalida(traspasoSeleccionado.Id);
                List<EntProducto> productosSeleccionados = new BusProductos().ObtieneProductosPorPedido(pedidoImprime.Id);//ObtieneListaProductosFromGV(gvDetalleProductosSalida);
                ImprimirSalida(new BusClientes().ObtieneCliente(pedidoImprime.ClienteId), pedidoImprime, productosSeleccionados,
                                pbImpresionFondoBlanco.Image, pbLogo.Image, pbLeyendaCotizacion.Image);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void gvProductosBusqueda_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
