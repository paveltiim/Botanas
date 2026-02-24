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
    public partial class EntradasCompras : FormBase
    {
        public EntradasCompras()
        {
            InitializeComponent();
        }

        public EntradasCompras(EntEmpresa Empresa, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente
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

        /// <summary>
        /// return ObtieneListaProductosFromGV(gvProductosPedido)
        /// </summary>
        List<EntProducto> ListaProductosPedido { get { return ObtieneListaProductosFromGV(gvProductosPedido); } }
        EntProducto ProductoPedidoSeleccionado { get { return ObtieneProductoFromGV(gvProductosPedido); } }
        int almacenId { get { return cmbAlmacenes.SelectedIndex + 1; } }

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
        }

        public void CargaProductos(int AlmacenId)
        {
            this.ListaProductos = new BusProductos().ObtieneProductosPorAlmacen(
                Program.UsuarioSeleccionado.CompañiaId, 0, almacenId,2)
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
        public void CargaEntradas(DateTime FechaDesde, DateTime FechaHasta, int AlmacenId)
        {
            gvProductos.DataSource = null;
            gvIngresos.DataSource = new BusProductos()
                        .ObtieneMovimientosEntradasProductos(
                            Program.UsuarioSeleccionado.EmpresaId,
                            FechaDesde, FechaHasta, AlmacenId);
        }
        public void CargaProductosMovimientoEntrada(EntCatalogoGenerico Movimiento, int IngresoId)
        {
            List<EntProducto> listaProductos = new BusProductos().ObtieneMovimientosDetalleProductos(1, Movimiento.Id, IngresoId);
            entProductoBindingSource.DataSource = listaProductos;
            gvProductos.DataSource = listaProductos;//.OrderByDescending(P => P.Fecha).ToList();

            if (IngresoId == 0)
            {
                foreach (EntProducto c in listaProductos)
                {
                    c.Modelo = Movimiento.Descripcion;
                    c.Fecha = Movimiento.Fecha;
                    c.FechaCorta = Movimiento.Fecha.ToShortDateString();
                }
                //List<EntProducto> precios = new BusProductos().ObtieneListaPreciosProducto(0, c.ProductoId);
                ////decimal precioPorCantidad = new BusProductos().ObtienePrecioProductoCantidad(0, c.ProductoId, 6).PrecioVenta;
                //c.PrecioVenta = precios.First().PrecioVenta2;// precioPorCantidad;
            }

            decimal cantidad = listaProductos.Sum(P => P.Cantidad);
            txtCantidadTotalEntradas.Text = cantidad.ToString();
            txtPrecioCostoTotalEntradas.Text = FormatoMoney(listaProductos.Sum(P => P.PrecioC));
            rvEntradas.RefreshReport();
        }

        void AgregaMovimientos(int IngresoId, int ProveedorId, List<EntProducto> ProductosSeleccionados)
        {
            int orientacion = 1;
            int almacenId = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes).Id;
            int movimientoId = new BusPedidos().AgregaMovimientoMaster(txtDescripcionEntrada.Text,
                                                                    (int)TipoMovimiento.COMPRA, orientacion,
                                                                    almacenId, IngresoId, Program.UsuarioSeleccionado.Id, ProveedorId);

            Productos vProd = new Productos();
            foreach (EntProducto p in ProductosSeleccionados)
            {
                new BusPedidos().AgregaMovimientoDetalle(movimientoId, p.Id, p.Cantidad, p.PrecioC);
                vProd.AumentaProducto(p.Id, p.Cantidad);
            }

            new BusPedidos().AgregaMovimientoLote(movimientoId, orientacion);
        }

        public EntIngreso AgregaIngresoBD(int EmpresaId, int ProveedorId, string Descripcion, DateTime Fecha, decimal TipoCambio)
        {
            EntIngreso ingreso = new EntIngreso()
            {
                EmpresaId = EmpresaId,
                ProveedorId = ProveedorId,
                Descripcion = Descripcion,
                Fecha = Fecha,
                //TipoCambio = TipoCambio,
                UsuarioId = Program.UsuarioSeleccionado.Id
            };
            ingreso.Id = new BusProductos().AgregaIngreso(ingreso);
            return ingreso;
        }
        int AgregaIngresoProductos(List<EntProducto> ProductosIngresa)
        {
            int ingresoProductoId = AgregaIngresoBD(Program.UsuarioSeleccionado.EmpresaId,
                                                       this.ProveedorSeleccionado.Id,
                                                       txtDescripcionEntrada.Text, dtpFechaIngreso.Value.Date,
                                                       0).Id;

            foreach (EntProducto p in ProductosIngresa)
            {
                new BusProductos().AgregaProductoIngreso(ingresoProductoId, new EntProducto()
                {
                    EmpresaId = Program.UsuarioSeleccionado.EmpresaId,
                    IngresoId = ingresoProductoId,
                    Id = p.Id,
                    Cantidad = p.Cantidad,
                    PrecioCosto = p.PrecioCosto,
                    IEPS = p.IEPS,
                    IVA = p.IVA
                });
            }
            return ingresoProductoId;
        }
        
        public EntFactura AgregaFacturaIngresoBD(int EmpresaId, int IngresoId, int ProveedorId,
                                                int PedidoCompraId, int TipoCompraId,
                                                EntFactura FacturaIngresa)
        {
            EntFactura factura = new EntFactura()
            {
                PedidoId = PedidoCompraId,
                TipoComprobanteId = TipoCompraId,
                SerieFactura = "",
                NumeroFactura = FacturaIngresa.NumeroFactura,
                IEPS = FacturaIngresa.IEPS,
                IVA = FacturaIngresa.IVA,
                Total = FacturaIngresa.Total,
                Pago = 0,
                Fecha = FacturaIngresa.Fecha,
                Ruta = FacturaIngresa.Ruta,
                MetodoPagoId = FacturaIngresa.MetodoPagoId,
                FormaPagoId = FacturaIngresa.FormaPagoId,
                MonedaId = FacturaIngresa.MonedaId,
                TipoCambio = FacturaIngresa.TipoCambio,
                Descripcion = FacturaIngresa.Descripcion,
                PDF = FacturaIngresa.PDF,
                XML = FacturaIngresa.XML,
                UsuarioId = Program.UsuarioSeleccionado.Id
            };
            factura.Id = new BusFacturas().AgregaFacturaIngreso(EmpresaId, IngresoId, ProveedorId, factura);
            return factura;
        }
        /// <summary>
        /// Agrega nuevo registro del Pedido solicitado.
        /// </summary>
        /// <param name="pedido"></param>
        public void AgregarPagoCompraBD(int PedidoId, int FacturaId, decimal Cantidad, DateTime FechaPago,
                                        int FormaPagoId, int MonedaId, decimal TipoCambio)
        {
            EntPago pago = new EntPago()
            {
                PedidoId = PedidoId,
                FormaPagoId = FormaPagoId,
                MonedaId = MonedaId,
                TipoCambio = TipoCambio,
                TipoPagoId = 1,
                Cantidad = Cantidad,
                FechaPago = FechaPago,
                UsuarioId = Program.UsuarioSeleccionado.Id
            };
            new BusFacturas().AgregaPagoCompra(pago, FacturaId);
        }
        int AgregaFacturaIngreso(int IngresoId)
        {
            try
            {
                EntFactura facturaIngresa = new EntFactura();
                facturaIngresa.SerieFactura = "";
                facturaIngresa.NumeroFactura = txtNumeroFactura.Text;
                facturaIngresa.Descripcion = txtDescripcionEntrada.Text;
                facturaIngresa.IEPS = ConvierteTextoADecimal(txtIepsFactura);
                facturaIngresa.IVA = ConvierteTextoADecimal(txtIvaFactura);
                facturaIngresa.Total = ConvierteTextoADecimal(txtTotalFactura);
                facturaIngresa.Ruta = this.PathFacturas + "\\INGRESOS\\"
                                                        + this.ProveedorSeleccionado.Nombre.Trim() + "\\"
                                                        + facturaIngresa.NumeroFactura.Trim();
                facturaIngresa.MonedaId = 1;
                facturaIngresa.TipoCambio = 0;
                facturaIngresa.Fecha = dtpFechaFactura.Value.Date;
                facturaIngresa.MetodoPagoId = cmbCondicionesPago.SelectedIndex + 1;
                if (facturaIngresa.MetodoPagoId == 1)
                    facturaIngresa.FormaPagoId = ConvierteTextoAInteger(txtFormaPago);
                else
                    facturaIngresa.FormaPagoId = 0;
                facturaIngresa.PDF = this.PDF;
                facturaIngresa.XML = this.XML;
                facturaIngresa.UsuarioId = Program.UsuarioSeleccionado.Id;

                VerificaExistenArchivosFactura(facturaIngresa.Ruta, facturaIngresa.SerieFactura + facturaIngresa.NumeroFactura,
                                                this.PDF, this.XML);

                int facturaIngresoId = AgregaFacturaIngresoBD(Program.UsuarioSeleccionado.EmpresaId, IngresoId,
                                                            this.ProveedorSeleccionado.Id, 0, 1, facturaIngresa).Id;
                if (facturaIngresa.MetodoPagoId == 1)
                {
                    try
                    {
                        facturaIngresa.Pago = ConvierteTextoADecimal(txtTotalFactura);

                        AgregarPagoCompraBD(0, facturaIngresoId, facturaIngresa.Pago, dtpFechaPago.Value,
                                            facturaIngresa.FormaPagoId, facturaIngresa.MonedaId, facturaIngresa.TipoCambio);
                        //new BusFacturas().AumentaPagoEnFacturaIngreso(factura);
                    }
                    catch (Exception ex) { MuestraExcepcion(ex, "FACTURA INGRESADA CORRECTAMENTE. ERROR AL AGREGAR PAGO."); }
                }
                return facturaIngresoId;
            }
            catch (Exception ex)
            {
                MuestraMensajeError("ERROR AL GUARDAR FACTURA");
            }
            return 0;
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

            //descuento = ConvierteTextoADecimal(txtDescuento);
            TxtMuestraTotal.Text = FormatoMoney(total); //FormatoMoney(total-descuento);
        }

        void VerificaProductosSeleccionados(List<EntProducto> ProductosSeleccionados)
        {
            if (ProductosSeleccionados == null || ProductosSeleccionados.Count == 0)
                MandaExcepcion("Agregue al menos un producto.");
        }
        void VerificaProveedorSeleccionado()
        {
            if (cmbProveedores.SelectedIndex == 0)
            {
                cmbProveedores.Focus();
                MandaExcepcion("SELECCIONE PROVEEDOR");
            }
        }
        void InicializaPantalla()
        {
            if (Program.EmpresaSeleccionada != null && cmbEmpresas.Items.Count > 0)
                cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);

            gvIngresos.DataSource = new List<EntCatalogoGenerico>();
            cmbMesesEntradas.SelectedIndex = DateTime.Today.Month - 1;
            CargaAñosCmb(cmbAñoEntradas);

            this.PDF = new byte[1];
            this.XML = new byte[1];

            cmbCondicionesPago.SelectedIndex = 1;
            cmbFormaPago.SelectedIndex = 0;
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
                CargaProveedores();
                //}
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        #region SELECCION EMPRESA
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
        private void btnRefrescaEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        #endregion

        private void cmbAlmacenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                gvProductosPedido.DataSource = null;
                CargaProductos(this.almacenId);

                EntCatalogoGenerico almacen = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);
                if (cmbMesesEntradas.SelectedIndex >= 0)
                    CargaEntradas(FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                                    FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1),
                                    almacen.Id);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void cmbProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.ProveedorSeleccionado = ObtieneProveedorFromCmb(cmbProveedores);
                txtProveedor.Text = this.ProveedorSeleccionado.Nombre + "-" + this.ProveedorSeleccionado.RFC;
                gbDatosFactura.Enabled = Convert.ToBoolean(cmbProveedores.SelectedIndex);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
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
                CargaProductos(ListaProductos);

                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProducto);
                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProductoCodigo);
                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProductoSerie);
            }
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

        private void btnRefrescarProductos_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                CargaProductos(almacenId);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally
            {
                base.SetDefaultCursor();
            }
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

        List<EntProveedor> ListaProveedores = new List<EntProveedor>();
        EntProveedor ProveedorSeleccionado;
        public void CargaProveedores()
        {
            this.ListaProveedores = new BusProveedores().ObtieneProveedores(Program.UsuarioSeleccionado.EmpresaId);
            this.ListaProveedores.Insert(0, new EntProveedor() { Nombre = "-SELECCIONAR PROVEEDOR-", Id = -1 });
            cmbProveedores.DataSource = this.ListaProveedores;
            cmbProveedores.SelectedIndex = 0;
        }

        void CargaDatosProveedor(EntProveedor Proveedor)
        {
            if (Proveedor == null)
                Proveedor = new EntProveedor();

            //txtNombre.Text = Proveedor.Nombre;
            //txtNombreFiscal.Text = Proveedor.NombreFiscal;
            cmbProveedores.SelectedIndex = ((List<EntProveedor>)cmbProveedores.DataSource).FindIndex(P => P.Id == Proveedor.Id);
        }

        private void btnAgregaProveedor_Click(object sender, EventArgs e)
        {
            try
            {
                AgregaProveedor vAgregaProveedor = new AgregaProveedor(Program.UsuarioSeleccionado.EmpresaId);
                if (vAgregaProveedor.ShowDialog() == DialogResult.OK)
                {
                    CargaProveedores();
                    
                    this.ProveedorSeleccionado = vAgregaProveedor.ProveedorAgregado;
                    CargaDatosProveedor(ProveedorSeleccionado);
                    //OcultaBuscadorGrid(gvProveedoresBusqueda, txtProveedorBusqueda);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void chkSinArchivos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSinArchivos.Checked)
                {
                    chkXML.Checked = false;
                    chkPDF.Checked = false;

                    gvProductosPedido.DataSource = null;

                    //this.ProveedorSeleccionado = new BusProveedores().ObtieneProveedores().Where(P => P.Id == pedido.ProveedorId).ToList()[0];
                    //this.ProveedorSeleccionadoId = pedido.ProveedorId;
                    //txtProveedor.Text = this.ProveedorSeleccionado.NombreFiscal;

                    txtTotalFactura.Text = FormatoMoney(0);
                    txtIepsFactura.Text = FormatoMoney(0);
                    txtIvaFactura.Text = FormatoMoney(0);
                    txtSubtotalFactura.Text = FormatoMoney(0);
                }

                chkXML.Enabled = !chkSinArchivos.Checked;
                chkPDF.Enabled = !chkSinArchivos.Checked;
                txtNumeroFactura.ReadOnly = !chkSinArchivos.Checked;
                dtpFechaFactura.Enabled = chkSinArchivos.Checked;
                txtSubtotalFactura.ReadOnly = !chkSinArchivos.Checked;
                txtIepsFactura.ReadOnly = !chkSinArchivos.Checked;
                txtIvaFactura.ReadOnly = !chkSinArchivos.Checked;
                txtTotalFactura.ReadOnly = !chkSinArchivos.Checked;
                //btnBuscarProveedor.Visible = chkSinArchivos.Checked;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        public EntFactura FacturaCargada { get; set; }
        public byte[] PDF { get; set; }
        public byte[] XML { get; set; }

        /// <summary>
        /// SI ENCUENTRA FACTURA REGRESA TRUE; DE LO CONTRARIO REGRESA FALSE.
        /// </summary>
        /// <param name="ProveedorId"></param>
        /// <param name="TextBoxNumeroFactura"></param>
        /// <returns></returns>
        bool VerificaNumeroFacturaIngresoRegistrada(int ProveedorId, TextBox TextBoxNumeroFactura)
        {
            if (string.IsNullOrWhiteSpace(TextBoxNumeroFactura.Text))
                return false;

            List<EntFactura> lst = new BusFacturas().ObtieneFacturasIngresoVigentes(Program.UsuarioSeleccionado.EmpresaId, ProveedorId,
                                                                                    TextBoxNumeroFactura.Text);
            if (lst.Count > 0)
            {
                TextBoxNumeroFactura.Focus();
                MuestraMensajeError("Número de Factura ya Registrada \n\n Factura: " + TextBoxNumeroFactura.Text);
                TextBoxNumeroFactura.Clear();
                return true;
            }
            return false;
        }
        private void chkXML_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((CheckBox)sender).Checked)
                {
                    string rutaArchivo = SeleccionaArchivoConFiltro(".xml");
                    if (!string.IsNullOrWhiteSpace(rutaArchivo))
                    {
                        base.SetWaitCursor();
                        this.XML = System.IO.File.ReadAllBytes(rutaArchivo);

                        this.FacturaCargada = base.LeeXMLFactura(new System.IO.FileInfo(rutaArchivo), false);

                        if (this.FacturaCargada.NumeroFactura == null)
                            this.FacturaCargada.NumeroFactura = "";
                        if (this.FacturaCargada.SerieFactura == null)
                            this.FacturaCargada.SerieFactura = "";
                        this.FacturaCargada.NumeroFactura = this.FacturaCargada.NumeroFactura.Replace('/', '-');
                        txtNumeroFactura.Text = this.FacturaCargada.SerieFactura.Replace('/', '-') + this.FacturaCargada.NumeroFactura;
                        dtpFechaFactura.Value = this.FacturaCargada.Fecha;

                        if (VerificaNumeroFacturaIngresoRegistrada(this.ProveedorSeleccionado.Id, txtNumeroFactura))
                        {
                            txtNumeroFactura.Focus();
                            ((CheckBox)sender).Checked = false;
                        }
                        else
                        {
                            txtSubtotalFactura.Text = FormatoMoney(this.FacturaCargada.SubTotal);
                            txtIepsFactura.Text = FormatoMoney(this.FacturaCargada.IEPS);
                            txtIvaFactura.Text = FormatoMoney(this.FacturaCargada.IVA);
                            txtTotalFactura.Text = FormatoMoney(this.FacturaCargada.Total);
                            //EntProveedor prove;
                            //try
                            //{
                            //    base.SetWaitCursor();
                            //    prove = ObtieneListaProveedoresFromCmb(cmbProveedores).Where(P => P.RFC == this.FacturaCargada.RFC 
                            //                                                       && P.NombreFiscal == this.FacturaCargada.Nombre).ToList()[0];
                            //    txtProveedor.Text = this.FacturaCargada.Nombre + " - " + this.FacturaCargada.RFC;
                            //}
                            //catch (Exception ex)
                            //{
                            //MuestraMensaje("PROVEEDOR NO ENCONTRADO");
                            //FiltroProveedores vProve = new FiltroProveedores();
                            //if (vProve.ShowDialog() == DialogResult.OK)
                            //{
                            //    prove = vProve.ProveedorSeleccionado;
                            //txtProveedor.Text = this.ProveedorSeleccionado.NombreFiscal; //+ " - " + this.ProveedorSeleccionado.RFC;
                            //}
                            //else
                            //{
                            //    prove = new EntProveedor();
                            //    txtProveedor.Text = "SIN PROVEEDOR";
                            //    this.ProveedorSeleccionadoId = -1;
                            //}
                            //}
                            //finally
                            //{
                            //    base.SetDefaultCursor();
                            //}
                            //this.ProveedorSeleccionadoId = prove.Id;
                            //this.ProveedorSeleccionado = prove;
                            //txtDescripcionIngreso.Text += " Factura: " + txtNumeroFactura.Text;

                            //if (this.FacturaCargada.MonedaId > 0)
                            //    cmbMoneda.SelectedIndex = this.FacturaCargada.MonedaId - 1;

                            //txtSubtotalFactura.Text = FormatoMoney(this.FacturaCargada.SubTotal);
                            //txtIvaFactura.Text = FormatoMoney(this.FacturaCargada.IVA);
                            //txtTotalFactura.Text = FormatoMoney(this.FacturaCargada.Total);
                            //else
                            //    gvProductosPedido.DataSource = this.FacturaCargada.Productos;
                        }
                    }
                    else
                        ((CheckBox)sender).Checked = false;
                }
                else
                {
                    this.XML = new byte[1];
                    this.FacturaCargada = new EntFactura();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally
            {
                base.SetDefaultCursor();
            }
        }

        private void chkPDF_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((CheckBox)sender).Checked)
                {
                    string rutaArchivo = SeleccionaArchivoConFiltro(".pdf");
                    if (!string.IsNullOrWhiteSpace(rutaArchivo))
                        this.PDF = System.IO.File.ReadAllBytes(rutaArchivo);
                    else
                        ((CheckBox)sender).Checked = false;
                }
                else
                    this.PDF = new byte[1];

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtNumeroFactura_Leave(object sender, EventArgs e)
        {
            try
            {
                VerificaProveedorSeleccionado();
                if (VerificaNumeroFacturaIngresoRegistrada(this.ProveedorSeleccionado.Id, (TextBox)sender))
                    return;
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void cmbCondicionesPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gbFormaFechaPago.Visible = !Convert.ToBoolean(cmbCondicionesPago.SelectedIndex);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                btnAgregar.Enabled = false;
                string mensaje;

                if (string.IsNullOrWhiteSpace(txtNumeroFactura.Text))
                {
                    txtNumeroFactura.Focus();
                    MandaExcepcion("Ingrese Número de Factura");
                }
                VerificaProveedorSeleccionado();
                VerificaProductosSeleccionados(this.ListaProductosPedido);

                mensaje = "¿Desea ingresar Entrada de Productos con Factura? ";

                if (MuestraMensajeYesNo(mensaje, "CONFIRMAR") == DialogResult.Yes)
                {
                    base.SetWaitCursor();

                    string detallePedido = "";

                    foreach (EntProducto p in this.ListaProductosPedido)
                        detallePedido += p.Cantidad + " " + p.Descripcion + " | ";

                    int ingresoId = AgregaIngresoProductos(this.ListaProductosPedido);
                    AgregaFacturaIngreso(ingresoId);

                    AgregaMovimientos(ingresoId, this.ProveedorSeleccionado.Id, this.ListaProductosPedido);

                    MuestraMensaje("¡Entrada Registrada!");


                    btnCancelar.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally
            {
                base.SetDefaultCursor();
            }
        }
        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                btnAgregar.Enabled = true;
                ClienteSeleccionado = null;

                cmbProveedores.SelectedIndex = 0;
                gvProductosPedido.DataSource = null;
                lbContadorSeries.Text = "0";
                txtDescripcionEntrada.Clear();
                txtTotal.Clear();
                
                this.PDF = new byte[1];
                this.XML = new byte[1];

                LimpiaTextBox(gbDatosFactura, true, true);
                cmbCondicionesPago.SelectedIndex = 1;
                
                CargaProductos(almacenId);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }


        #region PESTAÑA REGISTRO ENTRADAS
        private void cmbMesesEntradas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                if (cmbMesesEntradas.Focused)
                {
                    EntCatalogoGenerico almacen = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);
                    if (cmbMesesEntradas.SelectedIndex >= 0)
                        CargaEntradas(FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                                      FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1),
                                      almacen.Id);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        void RefrescaEntradasInsumos()
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
                CargaEntradas(dtpFechaEntradas.Value.Date,
                              dtpFechaEntradas.Value.Date.AddDays(1),
                                almacen.Id);
            }
        } 

        private void btnRefrescarEntradas_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                RefrescaEntradasInsumos();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        void CargaDatosFacturaEntrada(EntFactura FacturaEntrada) {
            gbDatosFacturaEntrada.Text = "Datos de Factura - " + FacturaEntrada.Id.ToString();
            txtNumeroFacturaEntrada.Text = FacturaEntrada.NumeroFactura;
            txtProveedorFacturaEntrada.Text = FacturaEntrada.Nombre;
            dtpFechaEntradas.Value = FacturaEntrada.Fecha;
            txtTotalFacturaEntrada.Text = FormatoMoney(FacturaEntrada.Total);
            chkFacturaEntradaXML.Tag = FacturaEntrada.Id;
            chkFacturaEntradaPDF.Tag = FacturaEntrada.Id;
        }

        private void gvIngresos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                base.SetWaitCursor();

                if (e.ColumnIndex == gvEntradasColumnImagen.Index)
                {
                    EntCatalogoGenerico entrada = ObtieneCatalogoGenericoFromGV(gvIngresos);
                    EntPedido pedidoImprime = new EntPedido();
                    pedidoImprime.NumOrden = "IN-" + entrada.Id.ToString().PadLeft(6, '0') + "\n";
                    pedidoImprime.Fecha = entrada.Fecha;
                    pedidoImprime.Observaciones = entrada.Descripcion;

                    EntFactura facturaEntrada = new BusProductos().ObtieneFacturaIngresoPorIngreso(entrada.IdSecundario);
                    pedidoImprime.NumOrden += facturaEntrada.NumeroFactura;//"FAC-" + facturaEntrada.NumeroFactura;
                    //p.MetodoPago = r["CATMET_DESCRIPCION"].ToString();
                    //p.FormaPago = r["CATFOR_DESCRIPCION"].ToString();

                    //CargaDatosFacturaEntrada(facturaEntrada);
                    //CargaProductosMovimientoEntrada(entrada.Id, entrada.IdSecundario);//MOVIMIENTOID; INGRESOID
                    List<EntProducto> listaProductos = new BusProductos().ObtieneMovimientosDetalleProductos(1, entrada.Id, entrada.IdSecundario);

                    decimal cantidad = listaProductos.Sum(P => P.Cantidad);
                    pedidoImprime.SubTotal = listaProductos.Sum(P => P.PrecioC);
                    pedidoImprime.IVA = 0;
                    pedidoImprime.Total = listaProductos.Sum(P => P.PrecioC);
                    //pedidoImprime.TotalUSD = pedidoImprime.Total;

                    EntCliente proveedor = new EntCliente() { Nombre = facturaEntrada.Nombre };
                    if (string.IsNullOrWhiteSpace(proveedor.Nombre))
                        proveedor.Nombre = "PROVEEDOR";
                    ImprimirEntrada(proveedor, pedidoImprime, listaProductos,
                                    pbImpresionFondoBlanco.Image, pbLogo.Image, pbLeyendaCotizacion.Image);
                }
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
                entrada.Estatus = true;

                EntFactura facturaEntrada = new BusProductos().ObtieneFacturaIngresoPorIngreso(entrada.IdSecundario);
                CargaDatosFacturaEntrada(facturaEntrada);
               
                CargaProductosMovimientoEntrada(entrada, entrada.IdSecundario);//MOVIMIENTOID; INGRESOID

                gvIngresos.Refresh();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }
        #endregion

        private void cmbFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { 
                txtFormaPago.Text = cmbFormaPago.Text.Remove(2);//, cmbFormaPago.Text.Length - 2
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtTotalFactura_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (((TextBox)sender).Focused)
                {
                    decimal total = ConvierteTextoADecimal(txtTotalFactura);
                    decimal subtotal=0, ieps= ConvierteTextoADecimal(txtIepsFactura), iva= ConvierteTextoADecimal(txtIvaFactura);

                    subtotal = total - ieps - iva;
                    txtSubtotalFactura.Text = FormatoMoney(subtotal);
                    //if (total > 0)
                    //{
                    //    subtotal = total / (1 + base.IVA);
                    //    txtSubtotalFactura.Text = FormatoMoney(subtotal);
                    //    txtIvaFactura.Text = FormatoMoney(total - subtotal);
                    //}
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtSubtotalFactura_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (((TextBox)sender).Focused)
                {
                    decimal subtotal = ConvierteTextoADecimal(txtSubtotalFactura);
                    decimal total = 0, ieps = ConvierteTextoADecimal(txtIepsFactura), iva = ConvierteTextoADecimal(txtIvaFactura);

                    total = subtotal + ieps + iva;
                    txtTotalFactura.Text = FormatoMoney(total);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtIepsFactura_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (((TextBox)sender).Focused)
                {
                    decimal subtotal = ConvierteTextoADecimal(txtSubtotalFactura);
                    decimal total = ConvierteTextoADecimal(txtTotalFactura), 
                            ieps = ConvierteTextoADecimal(txtIepsFactura), iva = ConvierteTextoADecimal(txtIvaFactura);
                    if (subtotal > 0)
                    {
                        total = subtotal + ieps + iva;
                        txtTotalFactura.Text = FormatoMoney(total);
                    }else if(total > 0)
                    {
                        subtotal = total - ieps - iva;
                        txtSubtotalFactura.Text = FormatoMoney(subtotal);
                    }
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void rdoEntradasPorMes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pnlEntradasPorMes.Enabled = rdoEntradasPorMes.Checked;
                pnlPorDiaEntradas.Enabled = !rdoEntradasPorMes.Checked;
                RefrescaEntradasInsumos();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void dtpFechaEntradas_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                RefrescaEntradasInsumos();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnExportaEntradas_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                //EntCatalogoGenerico entrada = ObtieneCatalogoGenericoFromGV(gvIngresos);
                //EntFactura facturaEntrada = new BusProductos().ObtieneFacturaIngresoPorIngreso(entrada.IdSecundario);
                //CargaDatosFacturaEntrada(facturaEntrada);
                //CargaProductosMovimientoEntrada(entrada.Id, entrada.IdSecundario);//MOVIMIENTOID; INGRESOID

                List<EntProducto> listaProductos = new List<EntProducto>();
                List<EntCatalogoGenerico> listaEntradasSeleccionadas = ObtieneListaGenericaFromGV(gvIngresos).Where(P=>P.Estatus).ToList();
                if (listaEntradasSeleccionadas.Count <= 0)
                    MandaExcepcion("SELECCIONE AL MENOS UNA ENTRADA");

                foreach (EntCatalogoGenerico en in listaEntradasSeleccionadas)
                {
                    List<EntProducto> listaProductosIngreso = new BusProductos().ObtieneMovimientosDetalleProductos(1, en.Id, en.IdSecundario);//MOVIMIENTOID| INGRESOID
                    if (en.IdSecundario == 0)
                    {
                        foreach (EntProducto c in listaProductosIngreso)
                        {
                            c.Modelo = en.Descripcion;
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

        private void chkSeleccionaTodasEntradas_CheckedChanged(object sender, EventArgs e)
        {
            try {
                List<EntCatalogoGenerico> listaEntradasSeleccionadas = ObtieneListaGenericaFromGV(gvIngresos);
                foreach (EntCatalogoGenerico en in listaEntradasSeleccionadas)
                {
                    en.Estatus=chkSeleccionaTodasEntradas.Checked;
                }
                gvIngresos.Refresh();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }
    }
}
