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
    public partial class SalidasInsumo : FormBase
    {
        public void VerificaEmpresa()
        {
            cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }

        public SalidasInsumo()
        {
            InitializeComponent();
        }

        public SalidasInsumo(EntEmpresa Empresa, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente
                     , int FormaPagoId, int MedioPagoId, string CondicionPago, string NumeroCuenta)
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
            List<EntCatalogoGenerico> almacenes = new BusEmpresas().ObtieneAlmacenes(1, Program.UsuarioSeleccionado.Id);
            cmbAlmacenes.DataSource = almacenes;
            cmbAlmacenes.SelectedIndex = 0;
        }

        public void CargaProductosInsumos(int AlmacenId)
        {
            this.ListaProductos = new BusProductos().ObtieneProductosExistenciaPorAlmacen(Program.UsuarioSeleccionado.CompañiaId, 0, AlmacenId, 2)
                                                                                        .OrderBy(P => P.Descripcion).ToList();
            gvProductosBusqueda.DataSource = ListaProductos;
        }
        public void CargaProductosInsumos(List<EntProducto> ListaProductos)
        {
            this.ListaProductos = ListaProductos;
            gvProductosBusqueda.DataSource = ListaProductos;
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
                ProductoSeleccionado.Descripcion = ProductoSeleccionado.Descripcion.Replace(" MARCA: " + ProductoSeleccionado.Marca + " MODELO: " + ProductoSeleccionado.Modelo, "");
                if (!string.IsNullOrWhiteSpace(ProductoSeleccionado.Marca))
                    ProductoSeleccionado.Descripcion += " | MARCA: " + ProductoSeleccionado.Marca;
                if (!string.IsNullOrWhiteSpace(ProductoSeleccionado.Modelo))
                    ProductoSeleccionado.Descripcion += " | MODELO: " + ProductoSeleccionado.Modelo;
                ProductoSeleccionado.Cantidad = CantidadAgrega;
                productosPedido.Add(ProductoSeleccionado);
            }
            else if (CantidadAgrega < 0)
            {
                productosPedido.Remove(ProductoSeleccionado);
                ListaProductos.Add(ProductoSeleccionado);
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
                total = ListaProductos.Sum(P => P.PrecioC);

                //subtotal = Math.Round(total, 2) / (1 + IVA); //Math.Round(total / (1 + IVA), 2);
                //cantidadIva = subtotal * this.IVA;
                //cantidadIva = Math.Round(total, 2) - subtotal;
            }

            descuento = ConvierteTextoADecimal(txtDescuento);
            TxtMuestraTotal.Text = FormatoMoney(total - descuento);
        }

        #endregion
        public void CargaSalidas(DateTime FechaDesde, DateTime FechaHasta, int AlmacenId)
        {
            gvProductos.DataSource = null;
            gvIngresos.DataSource = new BusProductos().ObtieneMovimientosSalidasProductos(Program.EmpresaSeleccionada.Id,
                                                                                            FechaDesde, FechaHasta,
                                                                                            AlmacenId);
        }
        void VerificaProductosSeleccionados(List<EntProducto> ProductosSeleccionados)
        {
            if (ProductosSeleccionados == null || ProductosSeleccionados.Count == 0)
                throw new Exception("Agregue al menos un producto.");
        }
        public void CargaProductosMovimientoSalida(EntCatalogoGenerico Movimiento)
        {
            List<EntProducto> listaProductos = new BusProductos().ObtieneMovimientosDetalleProductos(2, Movimiento.Id);

            foreach (EntProducto c in listaProductos)
            {
                c.Modelo = Movimiento.Descripcion;
                c.Fecha = Movimiento.Fecha;
                c.FechaCorta = Movimiento.Fecha.ToShortDateString();
            }

            entProductoBindingSource.DataSource = listaProductos;
            gvProductos.DataSource = listaProductos;//.OrderByDescending(P => P.Fecha).ToList();

            decimal cantidad = listaProductos.Sum(P => P.Cantidad);
            txtCantidadTotalEntradas.Text = cantidad.ToString();
            txtPrecioTotalEntradas.Text = FormatoMoney(listaProductos.Sum(P => P.Precio));
            rvSalidas.RefreshReport();
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
                        CargaProductosInsumos(ListaProductos);
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
                AgregaProductoEnPedido(ProductoSeleccionado, 1);

                ListaProductos.Remove(ProductoSeleccionado);
                //CargaProductos(ListaProductos);
                CargaProductosInsumos(ListaProductos);

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

                    CargaProductosInsumos(this.ListaProductos);

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
            }
            catch (Exception ex) { MuestraMensaje(ex.Message, "ERROR"); }
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
                if ((e.ColumnIndex == 3 || e.ColumnIndex == 4) && e.RowIndex > -1)//CANTIDAD | PRECIO SIN IVA
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
        public EntPedido AgregarPedido(int ClienteId, string Detalle, string Solicitud, string Observaciones, decimal Total, decimal Pago, DateTime Fecha, DateTime FechaEntrega, int EmpleadoId, bool Facturado, int EstatusId)
        {
            EntPedido pedido = new EntPedido()
            {
                TipoPedidoId = 2,
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

                //Pendiente
                //VerificaClienteSeleccionado();
                VerificaProductosSeleccionados(ListaProductosPedido);

                mensaje = "¿Desea dar Salida a los Productos seleccionados? ";

                if (MuestraMensajeYesNo(mensaje, "CONFIRMAR") == DialogResult.Yes)
                {
                    base.SetWaitCursor();

                    string detallePedido = "";
                    EntPedido pedidoAgrega = new EntPedido();

                    List<EntProducto> productosSeleccionados = ListaProductosPedido;

                    foreach (EntProducto p in productosSeleccionados)
                        detallePedido += p.Cantidad + " " + p.Descripcion + " | ";

                    pedidoAgrega = AgregarPedido(0, detallePedido.Remove(detallePedido.Length - 2), "", "",
                                                ConvierteTextoADecimal(txtTotal.Text), 0, DateTime.Now, DateTime.Today,
                                                Program.UsuarioSeleccionado.Id, false, 1);
                    Productos vProd = new Productos();
                    foreach (EntProducto p in productosSeleccionados)
                    {
                        AgregarProductoDetallePedido(pedidoAgrega.Id, p.Id, p.Cantidad, p.PrecioCosto, p.PrecioVenta);
                        vProd.AumentaProducto(p.Id, -p.Cantidad);
                    }

                    int orientacion = 2;
                    int tipoSalidaId = (int)TipoMovimiento.PRODUCCION;
                    int movimientoId = new BusPedidos().AgregaMovimientoMaster(txtDescripcionSalida.Text, tipoSalidaId, 
                                                orientacion, this.AlmacenId, pedidoAgrega.Id, Program.UsuarioSeleccionado.Id);

                    foreach (EntProducto p in productosSeleccionados)
                    {
                        new BusPedidos().AgregaMovimientoDetalle(movimientoId, p.Id, p.Cantidad, p.PrecioC);
                    }

                    new BusPedidos().AgregaMovimientoLote(movimientoId, orientacion);

                    btnCancelar.PerformClick();

                    MuestraMensaje("¡Salida Registrada!");
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
                txtDescripcionSalida.Clear();
                CargaProductosInsumos(this.AlmacenId);
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
                if (gvProductosBusqueda.CurrentRow.Index != gvProductosBusqueda.Rows.Count - 1)
                    ProductoSeleccionado = ObtieneProductoAnteriorFromGV(gvProductosBusqueda);
                else
                    ProductoSeleccionado = ObtieneProductoFromGV(gvProductosBusqueda);

                AgregaProductoEnPedido(ProductoSeleccionado, 1);

                ListaProductos.Remove(ProductoSeleccionado);
                //CargaProductos(ListaProductos);
                CargaProductosInsumos(ListaProductos);

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

                    CargaProductosInsumos(this.AlmacenId);
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
                CargaProductosInsumos(this.AlmacenId);
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
            try
            {
                CalculaSumaTotal(ListaProductosPedido, txtTotal);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbAlmacenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                gvProductosPedido.DataSource = null;
                CargaProductosInsumos(this.AlmacenId);
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
                EntCatalogoGenerico entrada = ObtieneCatalogoGenericoFromGV(gvIngresos);
                CargaProductosMovimientoSalida(entrada);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnExportaSalidas_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
             
                List<EntProducto> listaProductos = new List<EntProducto>();
                List<EntCatalogoGenerico> listaSalidasSeleccionadas = ObtieneListaGenericaFromGV(gvIngresos).Where(P => P.Estatus).ToList();
                if (listaSalidasSeleccionadas.Count <= 0)
                    MandaExcepcion("SELECCIONE AL MENOS UNA SALIDA");

                foreach (EntCatalogoGenerico en in listaSalidasSeleccionadas)
                {
                    List<EntProducto> listaProductosIngreso = new BusProductos().ObtieneMovimientosDetalleProductos(2, en.Id);

                    foreach (EntProducto c in listaProductosIngreso)
                    {
                        c.Modelo = en.Descripcion;
                        c.Fecha = en.Fecha;
                        c.FechaCorta = en.Fecha.ToShortDateString();
                    }

                    listaProductos.AddRange(listaProductosIngreso);
                }
                ImpresionSalidasTotales vImprimeSalidas = new ImpresionSalidasTotales(listaProductos);
                vImprimeSalidas.Show();

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnExportaSalidasPorMes_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();

                List<EntProducto> listaProductos = new List<EntProducto>();
                List<EntCatalogoGenerico> listaSalidasSeleccionadas = ObtieneListaGenericaFromGV(gvIngresos);//.Where(P => P.Estatus).ToList();
                //if (listaSalidasSeleccionadas.Count <= 0)
                //    MandaExcepcion("SELECCIONE AL MENOS UNA SALIDA");

                foreach (EntCatalogoGenerico en in listaSalidasSeleccionadas)
                {
                    List<EntProducto> listaProductosIngreso = new BusProductos().ObtieneMovimientosDetalleProductosSinDatosFactura(2, en.Id);

                    foreach (EntProducto c in listaProductosIngreso)
                    {
                        //c.Modelo = en.Descripcion;
                        //c.Fecha = en.Fecha;
                        c.FechaCorta = en.Fecha.ToString("yyyy - MMM").ToUpper();
                    }

                    listaProductos.AddRange(listaProductosIngreso);
                }
                ImpresionSalidas vImprimeSalidas = new ImpresionSalidas(listaProductos);
                vImprimeSalidas.Show();

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void chkSeleccionaTodasSalidas_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                List<EntCatalogoGenerico> listaSalidasSeleccionadas = ObtieneListaGenericaFromGV(gvIngresos);
                foreach (EntCatalogoGenerico en in listaSalidasSeleccionadas)
                {
                    en.Estatus = chkSeleccionaTodasEntradas.Checked;
                }
                gvIngresos.Refresh();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

    }
}
