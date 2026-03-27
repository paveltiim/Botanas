using AiresEntidades;
using AiresNegocio;
using AiresUtilerias;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.Reporting.WinForms;
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
    public partial class PreVentasMayoreo : FormBase
    {
        public PreVentasMayoreo()
        {
            InitializeComponent();
        }

        void AsignaPrecioVenta(List<EntProducto> ListaProductoAsignaPrecio)
        {            
            if (ListaProductoAsignaPrecio is null)
            {
                throw new ArgumentNullException(nameof(ListaProductoAsignaPrecio));
            }
            //foreach (var ProductoAsignaPrecio in ListaProductoAsignaPrecio)
            //{
            if (cmbFormaPago.Text.Contains("04") || cmbFormaPago.Text.Contains("28"))
                foreach (var ProductoAsignaPrecio in ListaProductoAsignaPrecio)
                {
                    ProductoAsignaPrecio.PrecioVentaSinIVA = ProductoAsignaPrecio.PrecioEspecial;
                    ProductoAsignaPrecio.PrecioVenta = Math.Round((ProductoAsignaPrecio.PrecioVentaSinIVA + ProductoAsignaPrecio.IEPS) * 1.03m, 2);
                }
            else
                foreach (var ProductoAsignaPrecio in ListaProductoAsignaPrecio)
                {
                    ProductoAsignaPrecio.PrecioVentaSinIVA = ProductoAsignaPrecio.PrecioEspecial;
                    ProductoAsignaPrecio.PrecioVenta = ProductoAsignaPrecio.PrecioVentaSinIVA + ProductoAsignaPrecio.IEPS;
                }

            if (chk5Descuento.Checked)
                foreach (var ProductoAsignaPrecio in ListaProductoAsignaPrecio)
                {
                    ProductoAsignaPrecio.PrecioVentaSinIVA = ProductoAsignaPrecio.PrecioEspecial;
                    ProductoAsignaPrecio.PrecioVenta = Math.Round((ProductoAsignaPrecio.PrecioVenta) / 1.05m, 2);
                }
            //}
            CalculaSumaTotal(ListaProductoAsignaPrecio, txtSubtotal, txtIEPS, txtTotal);
        }

        /// <summary>
        /// ASIGNA EN ProductoAsignaPrecio.PrecioVenta EL PRECIOVENTA + IEPS + 3% PAGO CON TARJETA.
        /// </summary>
        /// <param name="ProductoAsignaPrecio"></param>
        /// <exception cref="ArgumentNullException"></exception>
        void AsignaPrecioVenta(EntProducto ProductoAsignaPrecio)
        {
            if (ProductoAsignaPrecio is null)
            {
                throw new ArgumentNullException(nameof(ProductoAsignaPrecio));
            }

            if (cmbFormaPago.Text.Contains("04") || cmbFormaPago.Text.Contains("28"))
                ProductoAsignaPrecio.PrecioVenta = Math.Round((ProductoAsignaPrecio.PrecioVentaSinIVA + ProductoAsignaPrecio.IEPS) * 1.03m, 2);
            else
                ProductoAsignaPrecio.PrecioVenta = ProductoAsignaPrecio.PrecioVentaSinIVA + ProductoAsignaPrecio.IEPS;

            if (chk5Descuento.Checked)
                ProductoAsignaPrecio.PrecioVenta = Math.Round((ProductoAsignaPrecio.PrecioVenta) / 1.05m, 2);
        }
        void AgregaProductoEnPedido(EntProducto ProductoSeleccionado, decimal CantidadAgrega)
        {
            List<EntProducto> productosPedido = ObtieneListaProductosFromGV(gvProductosPedido);

            if (CantidadAgrega > 0)
            {
                if (productosPedido == null)
                    productosPedido = new List<EntProducto>();

                //PEDIDO MINIMO 6 PIEZAS PARA ESTOS ALMACENES
                if (this.AlmacenId == 6 || this.AlmacenId == 16 || this.AlmacenId == 21 || this.AlmacenId == 15)//15:PRUEBAS
                {
                    //EN PREVENTA NO TOMA EN CUENTA QUE CUENTE CON EXISTENCIA
                    //if (ProductoSeleccionado.Existencia < 6)
                    //    MandaExcepcion("Existencia insuficiente. \n\n Existencia: " + ProductoSeleccionado.Existencia);
                    CantidadAgrega = 6;//CULIACAN; MAZATLAN; SIEMPRE 6
                }

                ProductoSeleccionado.Cantidad = CantidadAgrega;

                //**EN PREVENTA NO ES NECESARIO**//
                //EntProducto productoClaves = new BusProductos().ObtieneProducto(ProductoSeleccionado.Id);
                //ProductoSeleccionado.ProductoServicioId = productoClaves.ProductoServicioId;
                //ProductoSeleccionado.ProductoServicio = productoClaves.ProductoServicio;
                //ProductoSeleccionado.ClaveProductoServicio = productoClaves.ClaveProductoServicio;
                //ProductoSeleccionado.ClaveUnidad = productoClaves.ClaveUnidad;
                //ProductoSeleccionado.UnidadId = productoClaves.UnidadId;
                //ProductoSeleccionado.Unidad = productoClaves.Unidad;

                if (chkDevolucionCortesia.Checked || chkCortesia.Checked)
                {
                    ProductoSeleccionado.PrecioVentaSinIVA = 0;
                    ProductoSeleccionado.PrecioVenta = 0;
                }
                else
                {
                    EntProducto productoPrecio = new BusProductos().ObtieneProductosPorAlmacenConPreciosVenta(
                                                                        ProductoSeleccionado.Id, this.AlmacenId,
                                                                        ProductoSeleccionado.TipoProductoId, this.ClienteSeleccionado.Id).Last();
                    ProductoSeleccionado.PrecioEspecial = productoPrecio.PrecioVentaSinIVA;
                    ProductoSeleccionado.PrecioVentaSinIVA = productoPrecio.PrecioVentaSinIVA;
                    ProductoSeleccionado.IncluyeIeps = productoPrecio.IncluyeIeps;
                    ProductoSeleccionado.IEPS = productoPrecio.IEPS;
                    //EN VALIDACION
                    ProductoSeleccionado.Existencia = productoPrecio.Existencia;
                    AsignaPrecioVenta(ProductoSeleccionado);
                }

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

            CalculaSumaTotal(productosPedido, txtSubtotal, txtIEPS, txtTotal);
            lbContadorSeries.Text = productosPedido.Count.ToString();
        }

        /// <summary>
        /// Muestra el resultado de ListaProductos.Sum(P=>P.Precio) en TxtMuestraTotal.Text.
        /// </summary>
        /// <param name="CantidadSumar"></param>
        void CalculaSumaTotal(List<EntProducto> ListaProductos, TextBox TxtMuestraSubTotal, TextBox TxtMuestraIEPS, TextBox TxtMuestraTotal)
        {
            decimal total, subtotal, cantidadIEPS;
            decimal cantidadIVARetenido = 0;
            decimal cantidadISRRetenido = 0;
            decimal IvaRetenidoPorcentaje = 0, IsrRetenidoPorcentaje = 0;
            EntEmpresa empresaSeleccionada = Program.EmpresaSeleccionada;
            decimal iva = empresaSeleccionada.TasaOCuota * 100;
            decimal ieps = empresaSeleccionada.TasaIEPS * 100;

            decimal porcentaje;
            porcentaje = 100 - IvaRetenidoPorcentaje - IsrRetenidoPorcentaje;
            porcentaje = porcentaje + iva + ieps;

            total = ListaProductos.Sum(P => P.Precio);//PRECIO: YA INCLUYE IMPUESTOS
            decimal totalConIEPS = ListaProductos.Where(P => P.IncluyeIeps).Sum(P => P.Precio);
            cantidadIEPS = (totalConIEPS * ieps) / porcentaje;
            subtotal = total - cantidadIEPS;

            TxtMuestraSubTotal.Text = FormatoMoney(subtotal);
            //txtIVA.Text = FormatoMoney(cantidadIva);
            TxtMuestraIEPS.Text = FormatoMoney(cantidadIEPS);
            TxtMuestraTotal.Text = FormatoMoney(total - cantidadIVARetenido - cantidadISRRetenido);
        }

#region EMPRESAS
        public void VerificaEmpresa()
        {
            cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
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

        private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Program.CambiaEmpresa)
                {
                    Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                    //btnCancelar.PerformClick();

                    //CargaProductos(this.AlmacenId);
                    //btnCancelar.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        #endregion

#region DECLARACION PARAMETROS

        EntPedido PedidoAgrega { get; set; }
        EntCliente ClienteSeleccionado = new EntCliente();
        EntProducto ProductoSeleccionado;
        List<EntProducto> ListaProductos = new List<EntProducto>();
        List<EntCliente> ListaClientes = new List<EntCliente>();
        bool ClientePublicoGeneral;

        /// <summary>
        /// ObtieneListaProductosFromGV(gvProductosPedido)
        /// </summary>
        List<EntProducto> ListaProductosPedido { get { return ObtieneListaProductosFromGV(gvProductosPedido); } }
        string detallePedido;
        /// <summary>
        /// foreach (EntProducto p in this.ListaProductosPedido) detallePedido += p.Cantidad + " " + p.Descripcion + " | ";
        /// </summary>
        public string DetallePedido
        {
            get
            {
                detallePedido = ("SUC: "+txtSucursal.Text.Trim()+" | ").Replace("SUC:  | ","");
                foreach (EntProducto p in this.ListaProductosPedido)
                    detallePedido += p.Cantidad + " " + p.Descripcion + " | ";
                return detallePedido.Remove(detallePedido.Length - 2);
            }
        }

        /// <summary>
        /// ObtieneCatalogoGenericoFromCmb(cmbEstablecimientos).Id; 
        /// </summary>
        int EstablecimientoId { get { return ObtieneCatalogoGenericoFromCmb(cmbEstablecimientos).Id; } }      

        int AlmacenId { get; set; }
        int EstablecimientoClientesId { get; set; }
        /// <summary>
        /// return cmbTipoProductoFiltro.SelectedIndex + 1; 
        /// </summary>
        int TipoProductoId { get { return cmbTipoProductoFiltro.SelectedIndex + 1; } }

        int IndexFormaPago = 0;

        #endregion

#region METODOS CARGAS
        void CargaEstablecimientos()
        {
            List<EntCatalogoGenerico> establecimientos = new BusEmpresas().ObtieneAlmacenes(Program.UsuarioSeleccionado.CompañiaId, Program.UsuarioSeleccionado.Id);//USUARIO PRINCIPAL
            cmbEstablecimientos.DataSource = establecimientos;
            cmbEstablecimientos.SelectedIndex = 0;
        }
        public void CargaClientes()
        {
            this.ListaClientes = new BusClientes().ObtieneClientesPorEstablecimiento(this.EstablecimientoClientesId);
            gvClientes.DataSource = this.ListaClientes;
            LimpiaDatosCliente();
        }
        public void CargaTrabajadores(int EstablecimientoId)
        {
            List<EntTrabajador> lst = new BusTrabajadores().ObtieneTrabajadores(EstablecimientoId);
            lst.Insert(0, new EntTrabajador() { Id = -1, Nombre = "-SELECCIONE-" });
            cmbTrabajadores.DataSource = lst;
            cmbTrabajadores.SelectedIndex = 0;
        }
        /// <summary>
        /// CARGA PRODUCTOS TODOS. NO IMPORTA EXISTENCIA.
        /// </summary>
        /// <param name="AlmacenId"></param>
        /// <param name="TipoProductoId"></param>
        public void CargaProductos(int AlmacenId, int TipoProductoId)
        {
            this.ListaProductos = new BusProductos().ObtieneProductosPorAlmacen(Program.UsuarioSeleccionado.CompañiaId, 0, AlmacenId,TipoProductoId)
                                                                                        .OrderBy(P => P.Descripcion).ToList();
            gvProductosBusqueda.DataSource = this.ListaProductos;
        }
        public void CargaProductos(List<EntProducto> ListaProductos)
            {
                this.ListaProductos = ListaProductos;
                gvProductosBusqueda.DataSource = ListaProductos;
            }
        public void CargaProductosEnPedido(List<EntProducto> ListaProductosPedido)
        {
            foreach (EntProducto p in ListaProductosPedido)
            {
                EntProducto productoEncontrado = this.ListaProductos.Find(P => P.Id == p.Id);
                p.TipoProductoId = productoEncontrado.TipoProductoId;
                p.PrecioCosto = productoEncontrado.PrecioCosto;
                p.Existencia = productoEncontrado.Existencia;
                this.ListaProductos.Remove(productoEncontrado);
            }
            CargaProductos(this.ListaProductos);

            gvProductosPedido.DataSource = ListaProductosPedido;
        }
        public void CargaSalidas(DateTime FechaDesde, DateTime FechaHasta, int AlmacenId)
        {
            
        }
        void CargaDatosCliente(EntCliente Cliente)
        {
            btnCancelar.PerformClick();
            this.ClienteSeleccionado = Cliente;//HAY QUE REASIGNAR POR NULLIFICAR EN btnCancelar
            Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(this.ClienteSeleccionado.EmpresaId);
            txtNombre.Text = Cliente.Nombre;
            txtNombreFiscal.Text = Cliente.NombreFiscal;
            txtRFC.Text = Cliente.RFC;
            txtEmail.Text = Cliente.Email;
            txtDireccion.Text = Cliente.Direccion;
            txtSucursal.Text = Cliente.Sucursal;
            txtCP.Text = Cliente.CP;

            if (Cliente.Credito)
                cmbMetodoPago.SelectedIndex = 0;
            cmbMetodoPago.Enabled = Cliente.Credito;
            cmbFormaPago.SelectedIndex = Cliente.FormaPagoId-1;
            //((List<EntCatalogoGenerico>)cmbFormaPago.DataSource)
            //                                .FindIndex(P => P.Id == Cliente.FormaPagoId);

            //chkPagada.Enabled = Cliente.Credito;

            cmbRegimenFiscal.SelectedIndex = ((List<EntCatalogoGenerico>)cmbRegimenFiscal.DataSource).FindIndex(P => P.Id == Cliente.RegimenFiscalId);
            if (Cliente.RFC == base.RfcPublicoGeneral)
            {
                cmbUsoCFDI.SelectedIndex = cmbUsoCFDI.Items.Count - 1;

                txtCP.Text = Program.EmpresaSeleccionada.CP; //OBLIGATORIO.
                txtCP.ReadOnly = true;
                if (Cliente.RegimenFiscalId != 616)//SIN OBLIGACIONES FISCALES.
                {
                    Cliente.RegimenFiscalId = 616; //Sin Obligaciones Fiscales      
                    MuestraMensaje("EL RFC -" + txtRFC.Text + "- DEBE USAR OBLIGATORIAMENTE EL REGIMEN FISCAL: 616 - SIN OBLIGACIONES FISCALES." +
                        "\nSE DEBE ACTUALIZAR LOS DATOS DEL CLIENTE PARA CAMBIAR SU REGIMEN FISCAL.");
                    cmbRegimenFiscal.SelectedIndex = ((List<EntCatalogoGenerico>)cmbRegimenFiscal.DataSource).FindIndex(P => P.Id == 616);//SIN OBLIGACIONES FISCALES
                }
                cmbRegimenFiscal.Enabled = false;
            }
            else
            {
                cmbRegimenFiscal.Enabled = true;
                txtCP.ReadOnly = false;
            }

            pnlProductos.Enabled = true;
            txtBuscaProductoCodigo.Focus();
        }
        #endregion

#region METODOS BD
        EntCliente ObtieneCliente(int ClienteId)
        {
            List<EntCliente> clientes = this.ListaClientes.Where(P => P.Id == ClienteId).ToList();

            if (clientes.Count == 0)
                throw new Exception("Cliente NO encontrado");

            return clientes[0];
        }
        public EntPedido AgregarPedidoPreVenta(int TipoPedidoId, int ClienteId,
                                        int CondicionPagoId, string Detalle, string Observaciones,
                                        decimal Total, decimal SubTotal, decimal IVA, decimal IEPS, decimal IvaRetencion, decimal IsrRetencion, 
                                        DateTime Fecha, DateTime FechaEntrega, int TrabajadorId, string NumOrden = "0")
        {
            EntPedido pedido = new EntPedido()
            {
                NumOrden=NumOrden,
                PedidoRelacionadoId = 0,
                TipoPedidoId = TipoPedidoId,
                ClienteId = ClienteId,
                MetodoPagoId = CondicionPagoId,
                Detalle = Detalle,
                Observaciones = Observaciones,
                SubTotal = SubTotal,
                IEPS = IEPS,
                Total = Total,
                Fecha = Fecha,
                FechaEntrega = FechaEntrega,
                TrabajadorId = TrabajadorId
            };
            pedido.Id = new BusPedidos().AgregaPedidoPreVenta(Program.EmpresaSeleccionada.Id, pedido, Program.UsuarioSeleccionado.Id);
            return pedido;
        }
        public void AgregarProductoPedidoPreVenta(int PedidoId, List<EntProducto> ProductosSeleccionados)
        {
            foreach (EntProducto p in ProductosSeleccionados)
            {
                EntPedido pedido = new EntPedido()
                {
                    Id = PedidoId
                };

                EntProducto producto = new EntProducto()
                {
                    Id = p.Id,
                    Cantidad = p.Cantidad,
                    PrecioCosto = p.PrecioCosto,
                    IEPS = p.IEPS,
                    IVA = p.IVA,
                    PrecioVenta = p.PrecioVenta,
                    Descripcion=p.Descripcion
                };
                new BusPedidos().AgregaProductoPedidoPreVenta(pedido, producto);
            }
        }

        /// <summary>
        /// Agrega nuevo registro del Pedido solicitado.
        /// </summary>
        /// <param name="pedido"></param>
        public EntPedido AgregarPedido(int EstablecimientoId, int TipoPedidoId, int ClienteId,
                                        string Detalle, string Observaciones, decimal SubTotal, decimal IEPS, decimal Total, decimal Pago,
                                        DateTime Fecha, DateTime FechaEntrega, bool Facturado, int TrabajadorId)
        {
            EntPedido pedido = new EntPedido()
            {
                TipoPedidoId = TipoPedidoId,
                ClienteId = ClienteId,
                Detalle = Detalle,
                Observaciones = Observaciones,
                SubTotal = SubTotal,
                IEPS = IEPS,
                Total = Total,
                Pago = Pago,
                Fecha = Fecha,
                FechaEntrega = FechaEntrega,
                Facturado = Facturado,
                EmpleadoId = Program.UsuarioSeleccionado.Id,
                TrabajadorId = TrabajadorId
            };
            pedido.Id = new BusPedidos().AgregaPedidoVenta(EstablecimientoId, Program.EmpresaSeleccionada.Id, pedido);
            return pedido;
        }
        /// <summary>
        /// Agrega nueva relación de Producto con el Pedido.
        /// </summary>
        /// <param name="pedido"></param>
        public void AgregarProductoDetallePedido(int PedidoId, List<EntProducto> ProductosSeleccionados)
        {
            foreach (EntProducto p in ProductosSeleccionados)
            {
                EntPedido pedido = new EntPedido()
                {
                    Id = PedidoId,
                    Fecha = DateTime.Now
                };

                EntProducto producto = new EntProducto()
                {
                    Id = p.Id,
                    Cantidad = p.Cantidad,
                    PrecioCosto = p.PrecioCosto,
                    PrecioVenta = p.PrecioVenta
                };
                new BusPedidos().AgregaProductoDetallePedido(pedido, producto);
            }
        }
        #endregion

#region METODOS IMPRESION

        void ImprimirNotaVenta(string TituloImpresion,EntCliente Cliente, EntPedido Pedido, List<EntProducto> ProductosSeleccionados)
        {
            string rutaGuardaArchivosOrden = base.RutaImpresion + "\\" + Cliente.Nombre + "\\" + Pedido.NumOrden;
            base.VerificaRutas(base.RutaImpresion, Cliente.Nombre + "\\" + Pedido.NumOrden);
            
            List<string> lstRutas = new List<string>();
            if (ProductosSeleccionados.Count > 20)
                lstRutas = base.CreaImagenesBMPnotaVenta(rutaGuardaArchivosOrden,
                                    pbImpresionFondoBlanco.Image, pbLogo.Image, pbLeyendaCotizacion.Image, pbImpresionFondoBlanco.Image,
                                    TituloImpresion, Pedido.NumOrden,
                                    Program.EmpresaSeleccionada, Cliente, Pedido, ProductosSeleccionados);
            else
            {
                base.CreaImagenBMPnotaVenta(rutaGuardaArchivosOrden,
                                    pbImpresionFondoBlanco.Image, pbLogo.Image, pbLeyendaCotizacion.Image, pbImpresionFondoBlanco.Image,
                                    TituloImpresion, Pedido.NumOrden,
                                    Program.EmpresaSeleccionada, Cliente, Pedido, ProductosSeleccionados);
                lstRutas.Add(rutaGuardaArchivosOrden + "\\" + Pedido.NumOrden + ".bmp");
            }

            try
            {
                new UtiPDF().CreaPDF(rutaGuardaArchivosOrden + "\\" + Pedido.NumOrden + ".bmp",
                                        rutaGuardaArchivosOrden + "\\NOTAVENTA-M-" + Pedido.NumOrden + ".pdf");
            }
            catch (Exception ex) { }
            finally
            {
                string nombreArchivo = EncuentraArchivo(rutaGuardaArchivosOrden, ".pdf");
                MuestraArchivo(rutaGuardaArchivosOrden, nombreArchivo);
            }
        }
        void MandarImprimirNotaVenta(string TituloImpresion, string TipoMovimiento, EntPedido PedidoAgrega, string Observaciones)
        {
            if (MuestraMensajeYesNo("¿Desea Imprimir " + TipoMovimiento + "?") == DialogResult.Yes)
            {
                EntPedido pedidoImprime = new EntPedido();
                pedidoImprime.NumOrden = PedidoAgrega.NumOrden.PadLeft(6, '0');
                pedidoImprime.Fecha = PedidoAgrega.Fecha;
                pedidoImprime.SubTotal = PedidoAgrega.SubTotal;
                pedidoImprime.IVA = PedidoAgrega.IVA;
                pedidoImprime.IEPS = PedidoAgrega.IEPS;
                pedidoImprime.Total = PedidoAgrega.Total;
                pedidoImprime.Observaciones = Observaciones;
                pedidoImprime.Sucursal = PedidoAgrega.Sucursal;
                pedidoImprime.Trabajador = PedidoAgrega.Trabajador;
                ImprimirNotaVenta(TituloImpresion, this.ClienteSeleccionado, pedidoImprime, this.ListaProductosPedido);
            }
        }
        #endregion

        /// <summary>
        /// LimpiaTextBox(pnlDatosCliente); this.ClienteSeleccionado = null;
        /// </summary>
        void LimpiaDatosCliente()
        {
            LimpiaTextBox(pnlDatosCliente);
            this.ClienteSeleccionado = null;
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

        string ObtieneDetallePedido(string DetallePedidoSucursal,List<EntProducto> ProductosSeleccionados)
        {
            detallePedido = "";
            if (DetallePedidoSucursal.Contains("SUC:"))
                detallePedido = ("SUC: " + DetallePedidoSucursal.Split('|')[0].Replace("SUC: ", "") + " | ").Replace("SUC:  | ", "");
                foreach (EntProducto p in ProductosSeleccionados)
                    detallePedido += p.Cantidad + " " + p.Descripcion + " | ";
                return detallePedido.Remove(detallePedido.Length - 2);
        }

        void InicializaPantalla()
        {
            if (Program.EmpresaSeleccionada != null && cmbEmpresas.Items.Count > 0)
                cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);

            cmbMesesEntradas.SelectedIndex = DateTime.Today.Month - 1;
            CargaAñosCmb(cmbAñoEntradas);
            CargaCatalogoRegimen(cmbRegimenFiscal);
            CargaCatalogoUsoCFDI(cmbUsoCFDI);
            cmbFormaPago.SelectedIndex = 0;
            cmbMetodoPago.SelectedIndex = 0;

            cmbTipoProductoFiltro.SelectedIndex = 0;

            cmbDescuentos.SelectedIndex = 0;
            if (Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.ADMINISTRADORPUNTOVENTA
                || Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.PUNTOVENTA
                || Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.PUNTOVENTAMENUDEO)
            {
                btnEliminar.Visible = false;
            }
            //else if (Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.MASTER)
            //    btnEliminar.Visible = true;
        }


        private void Ventas_Load(object sender, EventArgs e)
        {
            try
            {
                InicializaPantalla();
             
                this.ClienteSeleccionado = null;
                gvProductosPedido.DataSource = null;

                CargaEstablecimientos();
                this.EstablecimientoClientesId = Program.UsuarioSeleccionado.EstablecimientoClientesId; //5;//PUNTO VENTA MOCHIS
                this.AlmacenId = Program.UsuarioSeleccionado.AlmacenMayoristaId;//AQUI SE GUARDA EL ALMACEN MAYORISTA DEL USUARIO.

                CargaClientes();
                CargaTrabajadores(this.EstablecimientoId);
                CargaProductos(Program.UsuarioSeleccionado.AlmacenMayoristaId, this.TipoProductoId);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void cmbAlmacenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                //gvProductosPedido.DataSource = null;
                //CargaProductos(this.AlmacenId, this.TipoProductoId);
                //CargaClientes();
                CargaTrabajadores(this.EstablecimientoId);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
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
                FiltroClientes vClientes = new FiltroClientes(new BusClientes().ObtieneClientesPorEstablecimiento(this.EstablecimientoClientesId));
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

#region EVENTOS GV
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
        private void gvProductosBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                if (gvProductosBusqueda.CurrentRow.Index != gvProductosBusqueda.Rows.Count - 1)
                    this.ProductoSeleccionado = ObtieneProductoAnteriorFromGV(gvProductosBusqueda);
                else
                    this.ProductoSeleccionado = ObtieneProductoFromGV(gvProductosBusqueda);

                AgregaProductoEnPedido(this.ProductoSeleccionado, 1);

                this.ListaProductos.Remove(this.ProductoSeleccionado);
                CargaProductos(this.ListaProductos);

                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProducto);
                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProductoCodigo);
                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProductoSerie);
            }
        }

        private void gvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.ProductoSeleccionado = ObtieneProductoFromGV(gvProductosBusqueda);
                AgregaProductoEnPedido(this.ProductoSeleccionado, 1);

                this.ListaProductos.Remove(this.ProductoSeleccionado);
                CargaProductos(this.ListaProductos);

                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProducto);
                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProductoCodigo);
                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProductoSerie);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void gvProductosPedido_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CalculaSumaTotal(ObtieneListaProductosFromGV(gvProductosPedido), txtSubtotal, txtIEPS, txtTotal);
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
                if ((e.ColumnIndex == cantidadDataGridViewTextBoxColumn.Index 
                  || e.ColumnIndex == precioVentaSinIvaGvProductosColumn.Index 
                  || e.ColumnIndex == precioVentaGvProductosColumn.Index) && e.RowIndex > -1)//CANTIDAD | PRECIO SIN IVA | PRECIO VENTA
                {
                    EntProducto productoSeleccionado = ObtieneProductoFromGV(gvProductosPedido);
                    decimal iva = 0;
                    decimal ieps = 1.08m;
                    if (e.ColumnIndex == 3)//CANTIDAD 
                    {
                        if (productoSeleccionado.Cantidad <= 0)
                            productoSeleccionado.Cantidad = 0;
                        else if (this.AlmacenId == 6 || this.AlmacenId == 16 || this.AlmacenId == 21 || this.AlmacenId == 15)//15:PRUEBAS
                        {
                            if (productoSeleccionado.Cantidad < 6)//POR LOGICA EN AGREGADO SOLO HABRA PRODUCTOS CON EXISTENCIA MAYOR A 6.
                                productoSeleccionado.Cantidad = 6;//CULIACAN; MAZATLAN; SIEMPRE 6
                        }

                        if (productoSeleccionado.Cantidad > productoSeleccionado.Existencia)
                        {
                            MuestraMensaje("¡ADVERTENCIA! \n\nLa Cantidad solicitada es mayor a la Existente. \n Existencia: "
                                                   + productoSeleccionado.Existencia, "ERROR");
                            //productoSeleccionado.Cantidad = productoSeleccionado.Existencia;
                            base.CambiaBackColorGv(2, (DataGridView)sender, e.RowIndex);
                        }
                        //CALCULO PRECIO MAYOREO MENUDEO
                        //if (!chkDevolucionCortesia.Checked)//SI ES DEVOLUCION POR CORTESIA EL PRECIO DEBE SER SIEMPRE $0.
                        //{
                        //    if (this.ClienteSeleccionado.TipoPersonaId == 1 || this.ClienteSeleccionado.TipoPersonaId == 2) //MENUDEO (PUEDE PASAR A MAYOREO)
                        //    {
                        //        if (productoSeleccionado.Cantidad >= base.CantidadLimiteMayoreo)
                        //        {
                        //            decimal precioPorCantidad = productoSeleccionado.PrecioVentaSinIVA;
                        //            precioPorCantidad = new BusProductos().ObtienePrecioProductoCantidad(0, productoSeleccionado.Id, base.CantidadLimiteMayoreo).PrecioVenta;
                        //            if (precioPorCantidad > 0)
                        //            {
                        //                productoSeleccionado.PrecioVentaSinIVA = precioPorCantidad;
                        //                if (productoSeleccionado.IncluyeIeps)
                        //                    productoSeleccionado.IEPS = productoSeleccionado.PrecioVentaSinIVA * (ieps - 1);
                        //                ProductoSeleccionado.PrecioVenta = ProductoSeleccionado.PrecioVentaSinIVA + ProductoSeleccionado.IEPS;
                        //            }
                        //        } else //if (productoSeleccionado.Cantidad < base.CantidadLimiteMayoreo)
                        //        {
                        //            decimal precioPorCantidad = productoSeleccionado.PrecioVentaSinIVA;
                        //            precioPorCantidad = new BusProductos().ObtienePrecioProductoCantidad(0, productoSeleccionado.Id, 1).PrecioVenta;
                        //            if (precioPorCantidad > 0)
                        //            {
                        //                productoSeleccionado.PrecioVentaSinIVA = precioPorCantidad;
                        //                if (productoSeleccionado.IncluyeIeps)
                        //                    productoSeleccionado.IEPS = productoSeleccionado.PrecioVentaSinIVA * (ieps - 1);
                        //                ProductoSeleccionado.PrecioVenta = ProductoSeleccionado.PrecioVentaSinIVA + ProductoSeleccionado.IEPS;
                        //            }
                        //        }
                        //    }
                        //}
                    }
                    else if (e.ColumnIndex == precioVentaSinIvaGvProductosColumn.Index) //PRECIO SIN IEPS
                    {
                        if (productoSeleccionado.IncluyeIeps)
                        {
                            decimal precioSinIEPS = productoSeleccionado.PrecioVentaSinIVA; // (decimal)gvProductosPedido.CurrentRow.Cells[4].Value;
                            decimal precioIEPS = Math.Round(precioSinIEPS * ieps, 2);
                            productoSeleccionado.PrecioVenta = precioIEPS;
                            //gvProductosPedido.CurrentRow.Cells[5].Value = precioIVA;
                        }
                        else
                            productoSeleccionado.PrecioVenta = productoSeleccionado.PrecioVentaSinIVA;
                        //gvProductosPedido.CurrentRow.Cells[5].Value = (decimal)gvProductosPedido.CurrentRow.Cells[4].Value;
                    }
                    else if (e.ColumnIndex == precioVentaGvProductosColumn.Index)
                    {
                        if (productoSeleccionado.IncluyeIeps)
                        {
                            decimal precioIEPS = productoSeleccionado.PrecioVenta; //(decimal)gvProductosPedido.CurrentRow.Cells[5].Value;
                            decimal precioSinIEPS = Math.Round(precioIEPS / ieps, 2);
                            productoSeleccionado.PrecioVentaSinIVA = precioSinIEPS;
                            //gvProductosPedido.CurrentRow.Cells[4].Value = precioSinIVA;
                        }
                        else
                            productoSeleccionado.PrecioVentaSinIVA = productoSeleccionado.PrecioVenta;
                        //gvProductosPedido.CurrentRow.Cells[4].Value = (decimal)gvProductosPedido.CurrentRow.Cells[5].Value;
                    }

                    gvProductosPedido.Refresh();
                    //gvProductosPedido.CurrentRow.Cells[gvProductosPedido.ColumnCount - 1].Selected = true;
                    CalculaSumaTotal(this.ListaProductosPedido, txtSubtotal, txtIEPS, txtTotal);
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

#endregion
        private void txtCodigoProductoBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
                    gvProductosBusqueda.Visible = false;
                else
                {
                    gvProductosBusqueda.DataSource = this.ListaProductos.Where(P => P.Codigo.ToUpper() == ((TextBox)sender).Text.ToUpper() ||
                                                                                    P.CodigoBarra.ToUpper() == ((TextBox)sender).Text.ToUpper()).ToList();
                    //gvProductosBusqueda.DataSource = this.ListaProductos.Where(P => P.Codigo.ToUpper() == ((TextBox)sender).Text.ToUpper()).ToList();
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
                    gvProductosBusqueda.DataSource = this.ListaProductos.Where(P => P.Serie.ToUpper().Contains(((TextBox)sender).Text.ToUpper())).ToList();
                    gvProductosBusqueda.Visible = true;
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
                CargaProductos(Program.UsuarioSeleccionado.AlmacenMayoristaId, this.TipoProductoId);
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
                    this.ProductoSeleccionado = ObtieneProductoFromGV(gvProductosBusqueda);

                    if (this.ProductoSeleccionado != null)
                    {
                        AgregaProductoEnPedido(this.ProductoSeleccionado, 1);

                        this.ListaProductos.Remove(this.ProductoSeleccionado);
                        CargaProductos(this.ListaProductos);
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
                    gvProductosBusqueda.DataSource = this.ListaProductos.Where(P => P.Descripcion.ToUpper().Contains(txtBuscaProducto.Text.ToUpper())).ToList();
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
                    
                    this.ListaProductos.Remove(this.ProductoSeleccionado);
                    CargaProductos(this.ListaProductos);
                    //CargaProductos(this.AlmacenId, this.TipoProductoId);

                    OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProducto);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtDescuento_Leave(object sender, EventArgs e)
        {
            TextBoxDecimalMoney_Leave(sender, e);
        }

#region FACTURACION
        public string ObtieneUltimaFactura(int EmpresaId)
        {
            int ultimaFactura = ConvierteTextoAInteger(new BusPedidos().ObtieneUltimaFactura(EmpresaId).NumeroFactura);
            ultimaFactura++;

            return ultimaFactura.ToString();
        }
        
        string PathClienteDirectorioFacturas { get; set; }
        
        EntFactura TimbrarFactura(EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente, DateTime FechaFactura,
                                string FormaPago, string MedioPago, string CondicionPago, string NumeroCuenta,
                                decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS, 
                                decimal ImpuestoRetenido,
                                string Observaciones,
                                string TipoComprobante,
                                string UsoCFDI, bool Timbrar)
        {
            if (Cliente == null)
                Cliente = new EntCliente();
            Cliente.Nombre = txtNombre.Text;
            Cliente.NombreFiscal = txtNombreFiscal.Text;
            Cliente.RFC = txtRFC.Text;                        
            Cliente.Email = txtEmail.Text;
            //Cliente.Email2 = txtEmail2.Text;
            //Cliente.Email3 = txtEmail3.Text;
            Cliente.CP = txtCP.Text;
            Cliente.RegimenFiscalId = ObtieneCatalogoGenericoFromCmb(cmbRegimenFiscal).Id;

            UtiFacturacion factura = new UtiFacturacion();
            UtiFacturacionPruebas facturaPruebas = new UtiFacturacionPruebas();
            
            string uuid = "";

            ////EN PRUEBA PARA CAMBIAR ULTIMA FACTURA
            //EntFactura siguienteF = new EntFactura();
            //if (Pedido.SiguienteFacturaId == 0)
            //{
            //    siguienteF = ObtieneSiguienteFactura(Program.EmpresaSeleccionada.Id, Pedido.Id, Program.EmpresaSeleccionada.SerieFactura);
            //    //siguienteF.NumeroFactura = "1";
            //    Pedido.Factura = siguienteF.NumeroFactura;
            //    Pedido.SiguienteFacturaId = siguienteF.Id;
            //}
            if (Program.ConexionIdActual == 1 && Timbrar && Program.EmpresaSeleccionada.Facturacion)//PRODUCCION
            {
                string pathFacturasBase = @"C:\TIIM\Facturacion\Facturas";
                this.PathClienteDirectorioFacturas = base.CreaPathClienteDirectorioFacturas(pathFacturasBase, Cliente.Nombre, Pedido.Factura);

                //EN PRUEBA PARA CAMBIAR ULTIMA FACTURA
                //Pedido.Factura = ObtieneUltimaFactura(Program.EmpresaSeleccionada.Id);
                
                uuid = factura.Facturar40(Program.EmpresaSeleccionada, Pedido, ListaProductos, Cliente,
                                            Program.EmpresaSeleccionada.SerieFactura, Pedido.Factura, FechaFactura,
                                            FormaPago, MedioPago, CondicionPago,
                                            CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, ImpuestoRetenido, "",
                                            UsoCFDI, this.PathClienteDirectorioFacturas);
            }
            else 
            {
                string pathFacturasBase = @"C:\TIIM\Facturacion\PreFacturas";
                this.PathClienteDirectorioFacturas = base.CreaPathClienteDirectorioFacturas(pathFacturasBase, Cliente.Nombre, Pedido.Factura);
                
                MuestraMensaje("FACTURACIÓN DE PRUEBA");
                //uuid = facturaPruebas.FacturarNeue(Program.EmpresaSeleccionada, Pedido, ListaProductos, Cliente,
                //                            Program.EmpresaSeleccionada.SerieFactura, FechaFactura,
                //                            FormaPago, MedioPago, CondicionPago, NumeroCuenta,
                //                            CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, ImpuestoRetenido,
                //                            this.PathClienteDirectorioFacturas,
                //                            UsoCFDI);
                uuid = facturaPruebas.Facturar40(Program.EmpresaSeleccionada, Pedido, ListaProductos, Cliente,
                                            Program.EmpresaSeleccionada.SerieFactura, FechaFactura,
                                            FormaPago, MedioPago, CondicionPago, NumeroCuenta,
                                            CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, ImpuestoRetenido,
                                            this.PathClienteDirectorioFacturas,
                                            UsoCFDI);
            }
            EntFactura fact = new EntFactura()
            {
                PedidoId = Pedido.Id,
                SerieFactura = Program.EmpresaSeleccionada.SerieFactura,
                NumeroFactura = Pedido.Factura,
                UUID = uuid,
                Ruta = this.PathClienteDirectorioFacturas,
                Fecha = DateTime.Today,
                SiguienteFacturaId = Pedido.SiguienteFacturaId
            };
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(this.PathClienteDirectorioFacturas);
            foreach (System.IO.FileInfo file in dir.GetFiles())
            {
                if (file.Extension == ".pdf")
                {
                    UtiPDF modiPDF = new UtiPDF();
                    string nota1 = "VENDEDOR: " + cmbTrabajadores.Text.Replace("-SELECCIONE-", "");
                    string nota2 = "SUCURSAL: " + txtSucursal.Text;
                    string nota3 = "";// "Dirección: " + txtDireccion.Text;
                    string nota4 = "NOTA: FAVOR DE SOLICITAR LAS DEVOLUCIONES AL MOMENTO DE HACER UN PEDIDO Y NO AL MOMENTO DE QUE ESTEN ENTREGANDO PRODUCTO. " 
                                    +"NO HABRÁ DEVOLUCIONES DESPUÉS DE 8 DÍAS DE PEDIDO.";// "Dirección: " + txtDireccion.Text;
                
                    if (ListaProductos.Count > 40)
                        nota4 = "";
                    if (chkSinSucursal.Checked)
                        nota2 = "";
                
                    string rutaArchivoPDF = file.FullName;
                    modiPDF.ModificaPDF(nota1, nota2, nota3, nota4, rutaArchivoPDF, "1", 1);
                    fact.PDF = System.IO.File.ReadAllBytes(file.FullName);
                }
                else
                    fact.XML = System.IO.File.ReadAllBytes(file.FullName);
            }
            return fact;// pathClienteDirectorioFacturas;
        }
        bool Facturar(EntPedido PedidoAgrega, List<EntProducto> ProductosSeleccionados,
                        decimal CantidadIVA, decimal CantidadIVARetenido, decimal CantidadISRRetenido, decimal CantidadIEPS, 
                        decimal ImpuestoRetenido, 
                        string Observaciones, string TipoComprobante, bool Timbrar, bool PreFactura)
        {
            bool facturado = false;
            EntFactura factura = new EntFactura();

            try
            {
                //EN PRUEBA PARA CAMBIAR ULTIMA FACTURA
                //PedidoAgrega.Factura = ObtieneUltimaFactura(Program.EmpresaSeleccionada.Id);
                this.siguienteFacturaId = AsignaSiguienteFacturaEnPedido(this.siguienteFacturaId, PedidoAgrega, Program.EmpresaSeleccionada.Id, Program.EmpresaSeleccionada.SerieFactura);

                txtFormaPago.Text = cmbFormaPago.Text.Remove(2, cmbFormaPago.Text.Length - 2);
                txtMetodoPago.Text = cmbMetodoPago.Text.Remove(3);
                txtUsoCFDI.Text = cmbUsoCFDI.Text.Remove(3);

                factura = TimbrarFactura(PedidoAgrega, ProductosSeleccionados, ClienteSeleccionado, DateTime.Now,
                                    txtFormaPago.Text, txtMetodoPago.Text, txtCondicionesPago.Text, txtNumeroCuenta.Text,
                                    CantidadIVA, CantidadIVARetenido, CantidadISRRetenido, CantidadIEPS, ImpuestoRetenido,
                                    Observaciones, TipoComprobante,
                                    txtUsoCFDI.Text, Timbrar);
                factura.EmpresaId = Program.EmpresaSeleccionada.Id;
                factura.TipoComprobanteId = 1;//I-INGRESO.
                factura.FormaPagoId = ConvierteTextoAInteger(txtFormaPago.Text);
                factura.MetodoPagoId = cmbMetodoPago.SelectedIndex + 1;
                factura.UsoCFDIId = ObtieneCatalogoGenericoFromCmb(cmbUsoCFDI).Id;
                factura.VersionCFDI = "4.0";

                facturado = true;
            }
            catch (Exception ex)
            {
                MandaExcepcion("ERROR EN TIMBRADO\n" + ex.Message);
            }

            if (facturado)
            {
                if (!PreFactura)
                {
                    MuestraMensaje("¡El Pedido fue FACTURADO satisfactoriamente!", "CONFIRMACIÓN PEDIDO FACTURADO");
                    base.SetWaitCursor();
                    try
                    {
                        new BusFacturas().AgregaFactura(factura);
                        //EN PRUEBA PARA CAMBIAR ULTIMA FACTURA -- SE PUSO EN NEGOCIO EN METODO AgregaFactura(factura).
                    }
                    catch (Exception ex)
                    {
                        MuestraExcepcion(ex, "ERROR AL GUARDAR FACTURA EN SISTEMA. LA FACTURA SI FUE TIMBRADA ANTE SAT. " +
                                             " \nFAVOR DE COMUNICARSE CON EL ADMINISTRADOR DE SISTEMA");
                    }
                    finally { base.SetDefaultCursor(); }
                    try
                    {                        
                        if (MuestraMensajeYesNo("¿Desea mostrar Factura?") == DialogResult.Yes)
                        {
                            string nombreArchivo = EncuentraArchivo(PathClienteDirectorioFacturas, ".pdf");
                            MuestraArchivo(PathClienteDirectorioFacturas, nombreArchivo);
                        }
                    }
                    catch (Exception ex) { MuestraExcepcion(ex, "ERROR AL MOSTRAR FACTURA"); }

                    try
                    {
                        base.SetWaitCursor();
                        base.EnviaCorreo("FACTURA", Program.EmpresaSeleccionada, PedidoAgrega, this.ClienteSeleccionado, 
                                            this.PathClienteDirectorioFacturas, "","",
                                            factura.UUID);
                    }
                    catch (Exception ex)
                    {
                        MuestraExcepcion(ex, "Correo NO enviado.");
                    }
                    finally { base.SetDefaultCursor(); }
                }
                else
                {
                    string nombreArchivo = EncuentraArchivo(PathClienteDirectorioFacturas, ".pdf");
                    MuestraArchivo(PathClienteDirectorioFacturas, nombreArchivo);
                }
            }

            return facturado;
        }

        public void EliminaPedidoPreVenta(int PedidoId)
        {
            new BusPedidos().EliminaPedidoPreVentaTodo(PedidoId);
        }
        int siguienteFacturaId { get; set; }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                btnAgregar.Enabled = false;
                string mensaje, conexion = "";

                VerificaClienteSeleccionado();
                VerificaProductosSeleccionados(this.ListaProductosPedido);

                mensaje = "Se guardará PreVenta\n ¿Correcto? ";
                
                if (MuestraMensajeYesNo(mensaje, "CONFIRMAR") == DialogResult.Yes)
                {
                    base.SetWaitCursor();

                    Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(this.ClienteSeleccionado.EmpresaId);

                    EntTrabajador trabajador = ObtieneTrabajadorFromCmb(cmbTrabajadores);
                
                    int tipoPedidoId = (int)TipoPedido.PREVENTA;
                    if (chkDevolucionCortesia.Checked)
                        tipoPedidoId = (int)TipoPedido.PREVENTADEVOLUCION;// 14:DEVOLUCION.
                    else if (chkCortesia.Checked)
                        tipoPedidoId = (int)TipoPedido.PREVENTACORTESIA;//14:CORTESIA.

                    //NO EXISTE EN PREVENTA, POR NO SER MOVIMIENTO DE INVENTARIO
                    //int tipoMovimientoId = (int)TipoMovimiento.VENTA;
                    //string tipoMovimiento = "N/A";

                    EntCatalogoGenerico establecimiento = ObtieneCatalogoGenericoFromCmb(cmbEstablecimientos);

                    decimal cantidadIEPS = ConvierteTextoADecimal(txtIEPS);

                    //PEDIDO PRECARGADO PARA EDITAR.
                    if (this.PedidoAgrega.Id > 0)
                    {
                        //ELIMINA TODOS LOS PRODUCTOS REGISTRADOS ANTERIORMENTE(SIRVE COMO HISTORIAL DE COMO FUE LA ORDEN INICIALMENTE).
                        new BusPedidos().ActualizaEstatusProductoPedidoPreVenta(this.PedidoAgrega.Id, 0);
                        try
                        {
                            AgregarProductoPedidoPreVenta(this.PedidoAgrega.Id, this.ListaProductosPedido);
                        }
                        catch (Exception ex)
                        {
                            MandaExcepcion("ERROR AL AGREGAR PRODUCTOS\n\n"+ex.Message);
                        }
                        new BusPedidos().ActualizaPedidoPreVenta(this.PedidoAgrega.Id, this.PedidoAgrega.PedidoRelacionadoId, tipoPedidoId, 
                                                                 this.DetallePedido, txtComentario.Text, trabajador.Id, (int)EstatusPedidoPreVenta.PREVENTA);
                    }
                    else
                    {
                        this.PedidoAgrega = AgregarPedidoPreVenta(tipoPedidoId, this.ClienteSeleccionado.Id, 0,
                                                this.DetallePedido, txtComentario.Text,
                                                ConvierteTextoADecimal(txtTotal), ConvierteTextoADecimal(txtSubtotal), 0, ConvierteTextoADecimal(txtIEPS),
                                                0, 0,
                                                DateTime.Today, DateTime.Now,
                                                trabajador.Id);
                        this.PedidoAgrega.Cliente = this.ClienteSeleccionado.Nombre;
                        this.PedidoAgrega.Observaciones = txtComentario.Text;
                        this.PedidoAgrega.Direccion = txtDireccion.Text;
                        this.PedidoAgrega.Sucursal = txtSucursal.Text;
                        this.PedidoAgrega.Trabajador = cmbTrabajadores.Text;

                        try
                        {
                            AgregarProductoPedidoPreVenta(this.PedidoAgrega.Id, this.ListaProductosPedido);
                        }
                        catch (Exception ex)
                        {
                            EliminaPedidoPreVenta(this.PedidoAgrega.Id);
                            MandaExcepcion(ex.Message);
                        }
                    }

                    string tituloImpresion = ObtieneTituloImpresion(this.PedidoAgrega.TipoPedidoId);
                    MandarImprimirNotaVenta(tituloImpresion, tituloImpresion, this.PedidoAgrega, txtComentario.Text);
                    MuestraMensaje("¡" + tituloImpresion + " Registrada!");
                    btnCancelar.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { btnAgregar.Enabled = true; base.SetDefaultCursor(); }
        }
        string ObtieneTituloImpresion(int TipoPedidoId)
        {
            string tituloImpresion = " - PRE VENTA - ";
            if (TipoPedidoId == (int)TipoPedido.PREVENTADEVOLUCION || TipoPedidoId == (int)TipoPedido.PREVENTACORTESIA)//DEVOLUCION/CORTESIA
                tituloImpresion = "PRE VENTA \nDEV/COR";
            return tituloImpresion;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                
                this.LimpiaDatosCliente();
                base.LimpiaTextBox(gbDatosFacturacion);
                base.LimpiaTextBox(pnlAgrega);

                this.ListaProductos.AddRange(this.ListaProductosPedido);
                gvProductosPedido.DataSource = null;
                lbContadorSeries.Text = "0";
                txtDireccion.Clear();
                txtSucursal.Clear();
                chkSinSucursal.Checked = false;
                txtCambio.Clear();
                txtComentario.Clear();

                gvProductosBusqueda.Visible = false;
                pnlProductos.Enabled = false;

                gbComentario.SendToBack();

                chkDevolucionCortesia.Enabled = true;
                chkCortesia.Enabled = true; 
                chkDevolucionCortesia.Checked = false;
                chkCortesia.Checked = false;

                cmbUsoCFDI.SelectedIndex = 0;

                chk5Descuento.Checked = false;

                pnlDatosCliente.Enabled = true;

                this.PedidoAgrega = new EntPedido();

                //CargaProductos(Program.UsuarioSeleccionado.AlmacenMayoristaId, this.TipoProductoId);
                //CargaClientes();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnPreFactura_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCP.Text.Trim()))
                    MandaExcepcion("Ingrese C.P. del Cliente");
                if (cmbRegimenFiscal.SelectedIndex < 0)
                    MandaExcepcion("Seleccione Régimen Fiscal");

                if (MuestraMensajeYesNo("¿Desea mostrar Pre-Factura?") == DialogResult.Yes)
                {
                    base.SetWaitCursor();
                    EntPedido pedidoAgrega = new EntPedido();
                    pedidoAgrega.SubTotal = ConvierteTextoADecimal(txtSubtotal.Text);
                    pedidoAgrega.IVA = 0;
                    pedidoAgrega.Total = ConvierteTextoADecimal(txtTotal.Text);
                    pedidoAgrega.Factura = ObtieneUltimaFactura(Program.EmpresaSeleccionada.Id);

                    Facturar(pedidoAgrega, this.ListaProductosPedido, 0, 0, 0, ConvierteTextoADecimal(txtIEPS), 0, "", "I", false, true);

                    string nombreArchivo = EncuentraArchivo(this.PathClienteDirectorioFacturas, ".pdf");
                    MuestraArchivo(this.PathClienteDirectorioFacturas, nombreArchivo);
                }
            }catch(Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        void ActualizaEstatusFacturaPedido(int FacturaId, int EstatusId)
        {
            EntFactura fac = new EntFactura()
            {
                Id = FacturaId,
                EstatusId = EstatusId
            };
            new BusFacturas().ActualizaEstatusFacturaPedido(fac);
        }

        private void chkFacturar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                gbDatosFacturacion.Enabled = chkFacturar.Checked;
                //chkPagada.Enabled = !chkFacturar.Checked;

                if (chkFacturar.Checked)
                {
                    if (chkDevolucionCortesia.Checked || chkCortesia.Checked)
                    {
                        chkDevolucionCortesia.Checked = false;
                        chkCortesia.Checked = false;
                        //btnCancelar.PerformClick();

                        AsignaPrecioVenta(this.ListaProductosPedido);
                        gvProductosPedido.Refresh();
                    }

                    gbComentario.SendToBack();
                    txtComentario.Clear();                    
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void chkPagada_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!chkPagada.Checked && !this.ClienteSeleccionado.Credito)
                {
                    chkPagada.Checked = true;
                    MandaExcepcion("CLIENTE NO TIENE PERMITIDO CRÉDITO");
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void cmbMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (IndexFormaPago == 0)
                {
                    if (cmbMetodoPago.SelectedIndex == 1)//PARCIALIDADES
                    {
                        cmbFormaPago.SelectedIndex = cmbFormaPago.Items.Count - 1;
                        chkPagada.Checked = false;
                    }
                    else
                    {
                        cmbFormaPago.SelectedIndex = 0;
                        //chkPagada.Checked = true;
                    }
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        void AplicaAumentoDescuento()
        {
            decimal aumentoPagoT = 1;
            lbDescuento.Visible = false;
            lbAumentoPagoT.Visible = false;
            if (cmbFormaPago.Text.Contains("04") || cmbFormaPago.Text.Contains("28"))
            {
                lbAumentoPagoT.Visible = true;
                aumentoPagoT = 1.03m;
            }

            if (cmbDescuentos.SelectedIndex > 0)
            {
                lbDescuento.Visible = true;
                char[] dtChar = cmbDescuentos.Text.Take(2).ToArray();
                decimal dto = ConvierteTextoADecimal(dtChar[0].ToString() + dtChar[1].ToString().Replace("%", ""));

                decimal descuento = dto / 100;
                lbDescuento.Text="* "+dto.ToString()+"% Descuento";
                foreach (EntProducto p in this.ListaProductosPedido)
                {
                    //PRIMERO SE APLICA AUMENTO POR FORMAPAGO SI ES REQUERIDO.
                    p.PrecioVenta = Math.Round((p.PrecioVentaSinIVA + p.IEPS) * aumentoPagoT, 4);
                    //DESPUES SE AGREGA EL DESCUENTO
                    p.PrecioVenta = Math.Round(p.PrecioVenta / (1 + descuento), 2);
                }
            }
            else
            {
                //EN CASO DE NO HABER DESCUENTO SOLO SE APLICA AUMENTO POR FORMAPAGO SI ES REQUERIDO
                foreach (EntProducto p in this.ListaProductosPedido)
                {
                    p.PrecioVenta = Math.Round((p.PrecioVentaSinIVA + p.IEPS) * aumentoPagoT, 2);
                }
             }
            gvProductosPedido.Refresh();
            CalculaSumaTotal(this.ListaProductosPedido, txtSubtotal, txtIEPS, txtTotal);
        }
        private void cmbFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                IndexFormaPago = cmbFormaPago.SelectedIndex;
                if ((cmbFormaPago.SelectedIndex == cmbFormaPago.Items.Count - 1) && !this.ClienteSeleccionado.Credito)
                {
                    cmbFormaPago.SelectedIndex = this.ClienteSeleccionado.FormaPagoId - 1;
                    MandaExcepcion("CLIENTE NO TIENE PERMITIDO CRÉDITO");
                }
                if (cmbFormaPago.SelectedIndex == cmbFormaPago.Items.Count - 1)
                    cmbMetodoPago.SelectedIndex = 1;//PARCIALIDADES
                else
                    cmbMetodoPago.SelectedIndex = 0;

                IndexFormaPago = 0;

                if (gvProductosPedido.Rows.Count > 0)
                {
                    AplicaAumentoDescuento();
                    ////SE AGREGARA PORCENTAJE SOBRE PRECIOS SIN DESUENTO
                    //if (cmbFormaPago.Text.Contains("04") || cmbFormaPago.Text.Contains("28"))
                    //{
                    //    //SE QUITA EL DESCUENTO POR DEFAULT. SE PUEDE VOLVER A SELECCIONAR DESPUES DE SELECIONAR FORMADEPAGO
                    //    cmbDescuentos.SelectedIndex = 0;
                    //    foreach (EntProducto p in this.ListaProductosPedido)
                    //    {
                    //        p.PrecioVenta = Math.Round((p.PrecioVentaSinIVA + p.IEPS) * 1.03m, 2);
                    //    }
                    //}
                    //else
                    //{
                    //    foreach (EntProducto p in this.ListaProductosPedido)
                    //    {
                    //        p.PrecioVenta = p.PrecioVentaSinIVA + p.IEPS;
                    //    }
                    //}
                    //gvProductosPedido.Refresh();
                    //CalculaSumaTotal(this.ListaProductosPedido, txtSubtotal, txtIEPS, txtTotal);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void chk5Descuento_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ListaProductosPedido.Count > 0)
            {
                if (chk5Descuento.Checked)
                {
                    foreach (EntProducto p in this.ListaProductosPedido)
                    {
                        //(p.PrecioVentaSinIVA + p.IEPS)
                        p.PrecioVenta = Math.Round(p.PrecioVenta / 1.05m, 2);
                    }
                }
                else
                {
                    foreach (EntProducto p in this.ListaProductosPedido)
                    {
                        EntProducto productoPrecio = new BusProductos().ObtieneProductosExistenciaPorAlmacenConPreciosVenta(
                                                                    p.Id, this.AlmacenId,
                                                                    p.TipoProductoId, this.ClienteSeleccionado.Id).First();
                        p.PrecioVentaSinIVA = productoPrecio.PrecioVentaSinIVA;
                        p.IncluyeIeps = productoPrecio.IncluyeIeps;
                        p.IEPS = productoPrecio.IEPS;

                        if (cmbFormaPago.Text.Contains("04") || cmbFormaPago.Text.Contains("28"))
                            p.PrecioVenta = Math.Round((p.PrecioVentaSinIVA + p.IEPS) * 1.03m, 2);
                        else
                            p.PrecioVenta = p.PrecioVentaSinIVA + p.IEPS;
                    }
                }
                CalculaSumaTotal(this.ListaProductosPedido, txtSubtotal, txtIEPS, txtTotal);
                gvProductosPedido.Refresh();
            }
        }

        private void cmbDescuentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbDescuento.Visible = false;
            if (((ComboBox)sender).Focused)
            {
                if (this.ListaProductosPedido.Count > 0)
                {
                    AplicaAumentoDescuento();
                    //if (cmbDescuentos.SelectedIndex > 0)
                    //{
                    //    char[] dtChar = cmbDescuentos.Text.Take(2).ToArray();
                    //    decimal dto = ConvierteTextoADecimal(dtChar[0].ToString() + dtChar[1].ToString().Replace("%", ""));

                    //    decimal descuento = dto / 100;
                    //    decimal aumentoPagoT = 1;
                    //    if (cmbFormaPago.Text.Contains("04") || cmbFormaPago.Text.Contains("28"))
                    //        aumentoPagoT = 1.03m;

                    //    foreach (EntProducto p in this.ListaProductosPedido)
                    //    {
                    //        p.PrecioVenta = Math.Round((p.PrecioVentaSinIVA + p.IEPS) * aumentoPagoT, 4);

                    //        p.PrecioVenta = Math.Round(p.PrecioVenta / (1 + descuento), 2);
                    //    }
                    //}
                    //else
                    //{
                    //    foreach (EntProducto p in this.ListaProductosPedido)
                    //    {
                    //        EntProducto productoPrecio = new BusProductos().ObtieneProductosExistenciaPorAlmacenConPreciosVenta(
                    //                                                    p.Id, this.AlmacenId,
                    //                                                    p.TipoProductoId, this.ClienteSeleccionado.Id).First();
                    //        p.PrecioVentaSinIVA = productoPrecio.PrecioVentaSinIVA;
                    //        p.IncluyeIeps = productoPrecio.IncluyeIeps;
                    //        p.IEPS = productoPrecio.IEPS;

                    //        if (cmbFormaPago.Text.Contains("04") || cmbFormaPago.Text.Contains("28"))
                    //            p.PrecioVenta = Math.Round((p.PrecioVentaSinIVA + p.IEPS) * 1.03m, 2);
                    //        else
                    //            p.PrecioVenta = p.PrecioVentaSinIVA + p.IEPS;
                    //    }
                    //}
                    //CalculaSumaTotal(this.ListaProductosPedido, txtSubtotal, txtIEPS, txtTotal);
                    //gvProductosPedido.Refresh();
                }
            }
        }

#endregion

#region REGISTROS

        List<EntPedido> ListaPedidos = new List<EntPedido>();
        List<EntPedido> ListaProductosPedidosPreventa = new List<EntPedido>();
        public void CargaPedidosPreVenta(DateTime FechaDesde, DateTime FechaHasta)
        {
            this.ListaPedidos = new BusPedidos().ObtienePedidosPreVenta(Program.EmpresaSeleccionada.Id, FechaDesde, FechaHasta);
            //if (chkVerDevoluciones.Checked)
            //    this.ListaPedidos.AddRange(new BusPedidos().ObtienePedidosPorEstablecimiento(EstablecimientoId, FechaDesde, FechaHasta,
            //                                                                          (int)TipoPedido.DEVOLUCIONCORTESIA));
            //if (chkVerCortesias.Checked)
            //    this.ListaPedidos.AddRange(new BusPedidos().ObtienePedidosPorEstablecimiento(EstablecimientoId, FechaDesde, FechaHasta,
            //                                                                          (int)TipoPedido.CORTESIA));
            //if (!chkVerFacturasCanceladas.Checked)
            //    this.ListaPedidos = this.ListaPedidos.Where(P => !P.EstatusDescripcion.Contains("CANCELA")).ToList();

            gvPedidos.DataSource = this.ListaPedidos;
            txtTotalPedidos.Text = FormatoMoney(this.ListaPedidos.Sum(P => P.Total));

            // Obtener lista única de trabajadores
            var listaTrabajadores = this.ListaPedidos
                .Select(p => p.Trabajador)
                .Distinct()
                .OrderBy(nombre => nombre)
                .Select(nombre => new EntTrabajador { Nombre = nombre })
                .ToList();

            listaTrabajadores.Insert(0, new EntTrabajador { Id=-1, Nombre = "-TODOS-" });
            int index=cmbTrabjadoresFiltro.SelectedIndex;
            if (index < 0)
                index = 0;
            cmbTrabjadoresFiltro.DataSource = listaTrabajadores;
            cmbTrabjadoresFiltro.SelectedIndex = index;
        }

        private void cmbMesesEntradas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                if (cmbMesesEntradas.Focused)
                {
                    EntCatalogoGenerico almacen = ObtieneCatalogoGenericoFromCmb(cmbEstablecimientos);
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
                EntCatalogoGenerico establecimiento = ObtieneCatalogoGenericoFromCmb(cmbEstablecimientos);
            
                if (rdoPorMesVentas.Checked)
                {
                    if (cmbMesesEntradas.SelectedIndex >= 0)
                    {
                        CargaPedidosPreVenta(FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                                             FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas));
                    }
                }
                else if (rdoPorFechas.Checked)
                {
                    CargaPedidosPreVenta(dtpFechaDesdeVentas.Value.Date,
                                         dtpFechaHastaVentas.Value.Date);
                }
                btnFiltrarPedidos.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void rdoPorFechas_CheckedChanged(object sender, EventArgs e)
        {
            try {
                pnlEntradasPorMes.Enabled = !rdoPorFechas.Checked;
                pnlFechasVentas.Enabled = rdoPorFechas.Checked;
                btnRefrescarVentas.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnEnviaCorreo_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);

                if (!pedidoSeleccionado.Facturado)
                    MandaExcepcion("El Pedido NO está Facturado o ha sido Cancelado.");
                else if (pedidoSeleccionado.EstatusDescripcion == "CANCELADO")
                    MandaExcepcion("El Pedido ha sido Cancelado.");

                EntCliente cliente = ObtieneCliente(pedidoSeleccionado.ClienteId);
                SeleccionaEmail vCorreo = new SeleccionaEmail(cliente);//LE ASIGNA EL CORREO QUE SE ESCRIBE AL CLIENTE.

                if (vCorreo.ShowDialog() == DialogResult.OK)
                {
                    if (MuestraMensajeYesNo(string.Format("¿Seguro desea enviar la FACTURA al correo seleccionado? \n Cliente:{0} \n Email:{1}", pedidoSeleccionado.Cliente, cliente.Email), "CONFIRMACIÓN") == DialogResult.Yes)
                    {
                        base.SetWaitCursor();
                        try
                        {
                            List<EntFactura> facturasEncontradas = new BusFacturas().ObtieneFacturasPorPedido(pedidoSeleccionado.Id);
                            if (facturasEncontradas.Count > 0)
                            {
                                VerificaExistenArchivosFactura(facturasEncontradas[0].Ruta,
                                                                facturasEncontradas[0].SerieFactura
                                                                            + facturasEncontradas[0].NumeroFactura,
                                                                facturasEncontradas[0].PDF,
                                                                facturasEncontradas[0].XML);
                                base.EnviaCorreo("FACTURA", Program.EmpresaSeleccionada, pedidoSeleccionado, cliente,
                                                pedidoSeleccionado.RutaFactura, "", "",
                                                facturasEncontradas[0].UUID);
                            }
                            else
                                MuestraMensajeError("NO SE ENCONTRÓ FACTURA");
                        }
                        catch (Exception ex)
                        {
                            MuestraExcepcion(ex, "Correo NO enviado.");
                        }
                    }
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }
        
        private void btnCancelaFactura_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (MuestraMensajeYesNo(string.Format("¿Seguro desea CANCELAR la Factura seleccionada? \n " +
                                         "UUID: {0}", pedidoSeleccionado.UUID), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    SeleccionaAlmacen vAlmacen = new SeleccionaAlmacen();
                    if (vAlmacen.ShowDialog() == DialogResult.OK)
                    {
                        base.SetWaitCursor();

                        base.TimbraCancelacion(pedidoSeleccionado);

                        ActualizaEstatusFacturaPedido(pedidoSeleccionado.FacturaId, 0);

                        new RegistrosNeue().ReingresoDeProducto(vAlmacen.AlmacenSeleccionado.Id, pedidoSeleccionado.Id, pedidoSeleccionado.Factura);

                        btnRefrescarVentas.PerformClick();

                        MuestraMensaje("¡Factura Cancelada!", "CANCELACIÓN DE FACTURA");
                    }
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnComplementoPago_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedido = ObtienePedidoFromGV(gvPedidos);
                List<EntPedido> facturasCliente = ObtieneListaPedidosFromGV(gvPedidos).Where(P => P.ClienteId == pedido.ClienteId
                                                                                             && P.Facturado
                                                                                             && P.EstatusId == 1).ToList();
                EntCliente cliente = new BusClientes().ObtieneCliente(pedido.ClienteId);
                SeleccionaFacturas vSelFac = new SeleccionaFacturas(facturasCliente);
                if (vSelFac.ShowDialog() == DialogResult.OK)
                {
                    List<EntPedido> pedidosfacturasSeleccionadas = vSelFac.FacturasPedidoSeleccionados;
                    foreach (EntPedido pf in pedidosfacturasSeleccionadas)
                    {
                        //SOLO AGREGA EL PAGO QUE DEBE TENER CADA FACTURA
                        AgregaPago vAgregaPago = new AgregaPago(pf);//pedidosDeudaCliente);
                        if (vAgregaPago.ShowDialog() == DialogResult.OK)
                        {
                            decimal cantidadPaga = ConvierteTextoADecimal(vAgregaPago.CantidadPago);
                            //SE USA INSTEAD OF PAGO PARA NO ALTERAR DEBE
                            pf.PagoTotal = cantidadPaga;
                            pf.FechaPago = vAgregaPago.FechaPago;
                        }
                        else
                            return;
                    }

                    AgregaComplementoPago vComplePago = new AgregaComplementoPago(ConvierteListaPedidosEnFacturas(pedidosfacturasSeleccionadas),
                                                                                    cliente,
                                                                                    pedidosfacturasSeleccionadas.Sum(P => P.Debe),
                                                                                    pedidosfacturasSeleccionadas.Sum(P => P.PagoTotal));
                    if (vComplePago.ShowDialog() == DialogResult.OK)
                    {
                        base.SetWaitCursor();
                        bool pagoAgregado = false;

                        //DescuentaTimbre(Program.EmpresaSeleccionada);

                        foreach (EntPedido p in pedidosfacturasSeleccionadas)
                        {
                            SalidasVentas vVen = new SalidasVentas();
                            int pagoId = 0;
                            pagoAgregado = true;
                            try
                            {
                                pagoId = vVen.AgregarPagoPedidoBD(this.EstablecimientoId, p.Id, p.PagoTotal, vComplePago.FormaPagoId, vComplePago.FormaPago, vComplePago.FechaPago);
                            }
                            catch (Exception ex)
                            {
                                MuestraMensajeError("NO SE LOGRO GUARDAR TODOS LOS PAGOS ASIGNADOS A LAS FACTURAS \n\n" + ex.Message, "ERROR AL AGREGAR PAGO");
                            }
                            try
                            {
                                vComplePago.AgregarComplementoPago(p.FacturaId, pagoId, DateTime.Today, p.PagoTotal,
                                                                    vComplePago.FormaPagoId,
                                                                    vComplePago.ComplementoPago.NumeroFactura,
                                                                    vComplePago.CantidadPago,
                                                                    vComplePago.ComplementoPago.UUID,
                                                                    vComplePago.ComplementoPago.Ruta,
                                                                    vComplePago.ComplementoPago.PDF,
                                                                    vComplePago.ComplementoPago.XML);
                            }
                            catch (Exception ex)
                            {
                                MuestraMensajeError("NO SE LOGRO RELACIONAR TODAS LAS FACTURAS AL COMPLEMNTO \n\n" + ex.Message, "ERROR - COMPLEMENTO SI FUE TIMBRADO");
                            }
                        }

                        if (pagoAgregado)
                            MuestraMensaje("¡Pago Agregado!", "CONFIRMACIÓN PAGO");
                        else
                            MuestraMensajeError("Error al agregar el Pago", "ERROR PAGO-COMPLEMENTO");
                    }
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnVerComplemento_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                List<EntFactura> facturasComplementos = new BusFacturas().ObtieneComplementos(pedidoSeleccionado.FacturaId, pedidoSeleccionado.Factura, pedidoSeleccionado.Total);

                SeleccionaComplemento vSelFactura = new SeleccionaComplemento(pedidoSeleccionado);
                vSelFactura.ListaFacturasComplemento = facturasComplementos;
                if (vSelFactura.ShowDialog() == DialogResult.OK)
                {
                    vSelFactura.FacturaComplementoSeleccionada.Nombre = pedidoSeleccionado.Cliente;
                    //if (VerificaReAsignarFactura(vSelFactura.FacturaComplementoSeleccionada))
                    //{
                    VerificaExistenArchivosFactura(vSelFactura.FacturaComplementoSeleccionada.Ruta,
                                                   "CP" + vSelFactura.FacturaComplementoSeleccionada.NumeroComplemento + " FAC-" + vSelFactura.FacturaComplementoSeleccionada.NumeroFactura,
                                                   vSelFactura.FacturaComplementoSeleccionada.PDF,
                                                   vSelFactura.FacturaComplementoSeleccionada.XML);
                    MuestraArchivo(vSelFactura.FacturaComplementoSeleccionada.Ruta, EncuentraArchivo(vSelFactura.FacturaComplementoSeleccionada.Ruta, ".pdf"));
                    //}
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvPedidos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedido = ObtienePedidoFromGV(gvPedidos);
                btnEnviarVenta.Enabled = true;
                btnFacturar.Visible = false;
                btnEditar.Visible = false;
                btnDividirVenta.Visible = true;
                //btnEnviarVenta.Enabled = !pedido.Facturado;
                //btnCancelaFactura.Enabled = pedido.Facturado;
                //btnComplementoPago.Visible = pedido.Facturado;
                //btnComplementoPago.Enabled = pedido.Facturado;
                //btnVerComplemento.Visible = pedido.Facturado;
                //btnVerComplemento.Enabled = pedido.Facturado;

                EstatusPedidoPreVenta estatus= (EstatusPedidoPreVenta)pedido.EstatusId;
                //if(estatus==EstatusPedidoPreVenta.NOTAVENTA)
                //    btnFacturar.Visible = true;
                if (estatus == EstatusPedidoPreVenta.PREVENTA)
                    btnEditar.Visible = true;
                else
                {
                    btnEnviarVenta.Enabled = false;
                    btnDividirVenta.Visible = false;
                }
                if (pedido.TipoPedidoId == (int)TipoPedido.PREVENTADIVIDIDA || pedido.TipoPedidoId == (int)TipoPedido.PREVENTACORTESIA || pedido.TipoPedidoId == (int)TipoPedido.PREVENTADEVOLUCION)
                    btnDividirVenta.Visible = false;
                btnEliminar.Visible = Convert.ToBoolean(pedido.EstatusId);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtEfectivo_TextChanged(object sender, EventArgs e)
        {
            try {
                decimal efectivo = ConvierteTextoADecimal(txtEfectivo);
                decimal total = ConvierteTextoADecimal(txtTotal);
                decimal cambio = efectivo - total;
                txtCambio.Text = FormatoMoney(cambio);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void txtEfectivo_Leave(object sender, EventArgs e)
        {
            try
            {
                //TextBoxDecimalMoney_Leave(sender, e);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void chkDevolucionCortesia_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkDevolucionCortesia.Checked)
                {
                    chkCortesia.Checked = false;
                    chkFacturar.Checked = false;
                    gbComentario.BringToFront();

                    if (this.ListaProductosPedido != null)
                    {
                        foreach (EntProducto p in this.ListaProductosPedido)
                        {
                            p.PrecioVenta = 0;
                            p.PrecioVentaSinIVA = 0;
                        }
                        gvProductosPedido.Refresh();
                        CalculaSumaTotal(this.ListaProductosPedido, txtSubtotal, txtIEPS, txtTotal);
                    }
                }
                else if(chkDevolucionCortesia.Focused)
                {
                    gbComentario.SendToBack();
                    txtComentario.Clear();
                    btnCancelar.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        private void chkCortesia_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkCortesia.Checked)
                {
                    chkDevolucionCortesia.Checked = false;
                    chkFacturar.Checked = false;
                    gbComentario.BringToFront();

                    if (this.ListaProductosPedido != null)
                    {
                        foreach (EntProducto p in this.ListaProductosPedido)
                        {
                            p.PrecioVenta = 0;
                            p.PrecioVentaSinIVA = 0;
                        }
                        gvProductosPedido.Refresh();
                        CalculaSumaTotal(this.ListaProductosPedido, txtSubtotal, txtIEPS, txtTotal);
                    }
                }
                else if (chkCortesia.Focused)
                {
                    gbComentario.SendToBack();
                    txtComentario.Clear();
                    btnCancelar.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnAgregaMovimientos_Click(object sender, EventArgs e)
        {
            try {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                int tipoMovimientoId = (int)TipoMovimiento.VENTA;
                int tipoPedidoId = tipoMovimientoId - 1; //LOS TIPOS DE PEDIDOS SON CON ID MENOR(-1).
                SalidasVentas vVen = new SalidasVentas(); 
                vVen.AgregaMovimientosVenta(tipoMovimientoId, this.AlmacenId, "VENTA:" + pedidoSeleccionado.Factura, 
                    pedidoSeleccionado.Id, new BusProductos().ObtieneProductosPorPedido(pedidoSeleccionado.Id));
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void gvProductosPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (gvProductosPedido.CurrentCell != null)
                {
                    if (e.KeyChar == (char)Keys.Enter && gvProductosPedido.CurrentCell.ColumnIndex == 3)
                    {
                        txtBuscaProductoCodigo.Focus();
                    }
                }
                //((TextBox)sender).Focus();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnRefrescarClientes_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                CargaClientes();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnRefrescaTrabajadores_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                CargaTrabajadores(this.EstablecimientoId);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
            finally { base.SetDefaultCursor(); }
        }

        void FiltrarPedidosPreVenta(List<EntPedido> ListaPedidos, string Id, string NombreCliente, string Descripcion, 
                                    string NumOrden, string Trabajador)
        {
            //List<EntCliente> clientes = (List<EntCliente>)gvClientes.DataSource;

            var pedidosFiltro = from c in ListaPedidos
                                where c.Cliente.ToUpper().Contains(NombreCliente.ToUpper()) ||
                                      c.ClienteNombreFiscal.ToUpper().Contains(NombreCliente.ToUpper())
                                select c;

            if (!string.IsNullOrWhiteSpace(Id))
            {
                pedidosFiltro = from c in pedidosFiltro
                                where c.Id.ToString().Contains(Id.ToUpper())
                                select c;
            }

            if (!string.IsNullOrWhiteSpace(Descripcion))
            {
                pedidosFiltro = from c in pedidosFiltro
                                where c.Detalle.ToUpper().Contains(Descripcion.ToUpper())
                                select c;
            }

            if (!string.IsNullOrWhiteSpace(NumOrden))
            {
                pedidosFiltro = from c in pedidosFiltro
                                where c.NumOrden.ToUpper().Contains(NumOrden.ToUpper())
                                select c;
            }
            if (!string.IsNullOrWhiteSpace(Trabajador.Replace("-TODOS-","")))
            {
                pedidosFiltro = from c in pedidosFiltro
                                where c.Trabajador.ToUpper().Equals(Trabajador.ToUpper())
                                select c;
            }

            gvPedidos.DataSource = null;
            gvPedidos.DataSource = pedidosFiltro.ToList();
            txtTotalPedidos.Text = FormatoMoney(pedidosFiltro.Sum(P => P.Total));
        }
        private void btnFiltrarPedidos_Click(object sender, EventArgs e)
        {
            try
            {
                FiltrarPedidosPreVenta(this.ListaPedidos, txtNumPedidoFiltro.Text, txtClienteFiltro.Text, txtDescripcionFiltro.Text, txtFacturaFiltro.Text, cmbTrabjadoresFiltro.Text);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void cmbTipoProductoFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(((ComboBox)sender).Focused)
                    CargaProductos(Program.UsuarioSeleccionado.AlmacenMayoristaId, this.TipoProductoId);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

#endregion

        private void tcGeneral_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.PedidoAgrega != null)
                {
                    if (this.PedidoAgrega.Id > 0)
                    {
                        tcGeneral.SelectedIndex = 0;
                        MuestraMensajeError("Pedido en Edición");
                    }
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnEnviarAVenta_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (pedidoSeleccionado.Facturado)
                    MandaExcepcion("PEDIDO YA FACTURADO");
                if (MuestraMensajeYesNo("¿Desea Enviar a Venta la PreVenta seleccionada?\n Num. Orden: " + pedidoSeleccionado.NumOrden) == DialogResult.Yes)
                {
                    ////    Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(pedidoSeleccionado.EmpresaId);
                    ////    Facturar(pedidoSeleccionado, new BusProductos().ObtieneProductosPorPedido(pedidoSeleccionado.Id),
                    ////                0, 0, 0, pedidoSeleccionado.IEPS, 0, "", "I",
                    ////                Program.EmpresaSeleccionada.Facturacion, false);
                    //AgregaFactura vFacturar = new AgregaFactura(pedidoSeleccionado,
                    //                                        new BusClientes().ObtieneCliente(pedidoSeleccionado.ClienteId), this.EstablecimientoId);
                    //vFacturar.ShowDialog();

                    SalidasVentasMayoreo vVentaMayoreo = new SalidasVentasMayoreo(1300, 800);
                    vVentaMayoreo.CargaPedidoPreVenta(pedidoSeleccionado);
                    if (vVentaMayoreo.ShowDialog() == DialogResult.OK)
                    {
                        int estatusPedidoPreId = (int)EstatusPedidoPreVenta.NOTAVENTA;
                        if (vVentaMayoreo.PedidoFacturado)
                            estatusPedidoPreId = (int)EstatusPedidoPreVenta.FACTURA;

                        new BusPedidos().ActualizaPedidoPreVenta(pedidoSeleccionado.Id, vVentaMayoreo.PedidoAgrega.Id, pedidoSeleccionado.TipoPedidoId,
                                                                 pedidoSeleccionado.Detalle, vVentaMayoreo.PedidoAgrega.Observaciones, pedidoSeleccionado.TrabajadorId, estatusPedidoPreId);
                    }
                    //else {
                    //    MuestraMensaje(vVentaMayoreo.DialogResult.ToString());
                    //    //int estatusPedidoPreId = (int)EstatusPedidoPreVenta.NOTAVENTA;
                    //    //if (vVentaMayoreo.PedidoFacturado)
                    //    //    estatusPedidoPreId = (int)EstatusPedidoPreVenta.FACTURA;

                    //    //new BusPedidos().ActualizaPedidoPreVenta(pedidoSeleccionado.Id, vVentaMayoreo.PedidoAgrega.Id, vVentaMayoreo.PedidoAgrega.TipoPedidoId,
                    //    //                                         pedidoSeleccionado.Detalle, vVentaMayoreo.PedidoAgrega.Observaciones, estatusPedidoPreId);
                    //}
                    btnRefrescarVentas.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        public void CargaPedidoPreVenta(EntPedido PedidoPreVenta)
        {
            tcGeneral.SelectedIndex = 0;

            this.ClienteSeleccionado = new BusClientes().ObtieneCliente(PedidoPreVenta.ClienteId);
            this.ClienteSeleccionado.Sucursal = ObtieneSucursalFromPedidoDetalle(PedidoPreVenta.Detalle);

            CargaDatosCliente(this.ClienteSeleccionado);//AQUI ACCIONA CLICK DE CANCELAR. REINICIA 

            txtNumOrden.Text = PedidoPreVenta.NumOrden;
            pnlDatosCliente.Enabled = false;

            txtComentario.Text = PedidoPreVenta.Observaciones;
            //SERIA ENCONTRAR EL ESTABLECIMIENTO DEL TRABAJOR.
            //cmbEstablecimientos.SelectedIndex = ((List<EntCatalogoGenerico>)cmbEstablecimientos.DataSource).FindIndex(P => P.Id == PedidoPreVenta.Sucursal);
            cmbTrabajadores.SelectedIndex = ((List<EntTrabajador>)cmbTrabajadores.DataSource).FindIndex(P => P.Id == PedidoPreVenta.TrabajadorId);

            List<EntProducto> productosPedidoPreVenta = new BusProductos().ObtieneProductosPorPedidoPreVenta(PedidoPreVenta.Id);
            CargaProductosEnPedido(productosPedidoPreVenta);

            CalculaSumaTotal(productosPedidoPreVenta, txtSubtotal, txtIEPS, txtTotal);

            if (PedidoPreVenta.TipoPedidoId == (int)TipoPedido.PREVENTADEVOLUCION)
                chkDevolucionCortesia.Checked = true;
            else if (PedidoPreVenta.TipoPedidoId == (int)TipoPedido.PREVENTACORTESIA)
                chkCortesia.Checked = true;
            chkDevolucionCortesia.Enabled = false;
            chkCortesia.Enabled = false;

            this.PedidoAgrega = PedidoPreVenta;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (pedidoSeleccionado.EstatusId != (int)EstatusPedidoPreVenta.PREVENTA)
                    MandaExcepcion("NO SE PUEDE EDITAR PREVENTA QUE YA PASO A NOTA DE VENTA O FACTURA.");
                
                if (MuestraMensajeYesNo("¿Desea Editar PreVenta seleccionada?\n Num. Orden: " + pedidoSeleccionado.NumOrden) == DialogResult.Yes)
                {
                    CargaPedidoPreVenta(pedidoSeleccionado);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (pedidoSeleccionado.EstatusId != (int)EstatusPedidoPreVenta.PREVENTA && pedidoSeleccionado.EstatusId != (int)EstatusPedidoPreVenta.PREVENTA_DIVIDIDA)
                    MandaExcepcion("NO SE PUEDE ELIMINAR PREVENTA QUE SE ENVIÓ A \n" +
                                   "NOTA DE VENTA O FACTURA.");

                if (MuestraMensajeYesNo(string.Format("¿Seguro desea ELIMINAR la PreVenta seleccionada? \n "), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    base.SetWaitCursor();

                    new BusPedidos().ActualizaPedidoPreVenta(pedidoSeleccionado.Id,pedidoSeleccionado.PedidoRelacionadoId, pedidoSeleccionado.TipoPedidoId, 
                                                             pedidoSeleccionado.Detalle, pedidoSeleccionado.Observaciones, pedidoSeleccionado.TrabajadorId, (int)EstatusPedidoPreVenta.CANCELADA);

                    btnRefrescarVentas.PerformClick();

                    MuestraMensaje("¡PreVenta Eliminada!", "ELIMINACIÓN VENTA");
                }
            }
            catch (Exception ex) {
                MuestraExcepcion(ex);
            }
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            try
            {
                //NO EXISTE CONSULTA DE PEDIDO POR ID
                //EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                //AgregaFactura vFacturar = new AgregaFactura(new BusPedidos().ObtienePedidosPorEstablecimiento,
                //                                           new BusClientes().ObtieneCliente(pedidoSeleccionado.ClienteId), this.AlmacenId);
                //vFacturar.ShowDialog();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        public class FacturaDividida
        {
            public List<EntProducto> Productos { get; set; } = new List<EntProducto>();
            public decimal Total => Productos.Sum(p => p.Precio);
            public decimal SubTotal => Productos.Sum(p => p.PrecioSinIVA);
            public decimal IEPS => Productos.Sum(p => p.IEPS);
        }

        public List<FacturaDividida> DividirEnFacturas(List<EntProducto> productos, decimal limiteFactura)
        {
            var facturas = new List<FacturaDividida>();
            var productosPendientes = new Queue<EntProducto>(productos);

            while (productosPendientes.Count > 0)
            {
                var factura = new FacturaDividida();
                decimal totalActual = 0;

                var tempQueue = new Queue<EntProducto>();

                while (productosPendientes.Count > 0)
                {
                    var producto = productosPendientes.Dequeue();
                    decimal cantidadDisponible = producto.Cantidad;

                    while (cantidadDisponible > 0)
                    {
                        decimal subtotal = producto.PrecioVenta;

                        if (totalActual + subtotal <= limiteFactura)
                        {
                            factura.Productos.Add(new EntProducto
                            {
                                Descripcion = producto.Descripcion,
                                PrecioVenta = producto.PrecioVenta,
                                Cantidad = 1
                            });

                            totalActual += subtotal;
                            cantidadDisponible--;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (cantidadDisponible > 0)
                    {
                        producto.Cantidad = cantidadDisponible;
                        tempQueue.Enqueue(producto);
                    }
                }

                facturas.Add(factura);
                productosPendientes = tempQueue;
            }

            return facturas;
        }
        public List<FacturaDividida> DividirFacturas(List<EntProducto> productos, decimal limiteFactura)
        {
            var facturas = new List<FacturaDividida>();
            var pendientes = new Queue<EntProducto>(productos);

            while (pendientes.Count > 0)
            {
                var factura = new FacturaDividida();
                decimal totalActual = 0;

                var tempQueue = new Queue<EntProducto>();

                while (pendientes.Count > 0)
                {
                    var producto = pendientes.Dequeue();
                    decimal cantidadRestante = producto.Cantidad;

                    while (cantidadRestante > 0)
                    {
                        decimal subtotal = producto.PrecioVenta;

                        if (totalActual + subtotal <= limiteFactura)
                        {
                            var existente = factura.Productos.FirstOrDefault(i => i.Id == producto.Id);
                            if (existente != null)
                            {
                                existente.Cantidad += 1;
                            }
                            else
                            {
                                factura.Productos.Add(new EntProducto
                                {
                                    Id = producto.Id,
                                    Descripcion= producto.Descripcion,
                                    PrecioCosto = producto.PrecioCosto,
                                    PrecioVenta = producto.PrecioVenta,
                                    PrecioVentaSinIVA = producto.PrecioVentaSinIVA,
                                    IEPS = producto.IEPS,
                                    IVA=producto.IVA,
                                    Cantidad = 1
                                });
                            }
                    
                            totalActual += subtotal;
                            cantidadRestante--;
                        }
                        else break;
                    }

                    if (cantidadRestante > 0)
                    {
                        producto.Cantidad = cantidadRestante;
                        tempQueue.Enqueue(producto);
                    }
                }

                facturas.Add(factura);
                pendientes = tempQueue;
            }

            return facturas;
        }

        private void btnDividirVenta_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                List<EntProducto> productosPedidoPreVenta = new BusProductos().ObtieneProductosPorPedidoPreVenta(pedidoSeleccionado.Id);
                //List<FacturaDividida> lstFacDiv = DividirEnFacturas(productosPedidoPreVenta,2000);
                //productosPedidoPreVenta = new BusProductos().ObtieneProductosPorPedidoPreVenta(pedidoSeleccionado.Id);
                AgregaCantidad vCantidad = new AgregaCantidad(2000);
                if (vCantidad.ShowDialog() == DialogResult.OK)
                {
                    List<FacturaDividida> lstFacDiv = DividirFacturas(productosPedidoPreVenta, vCantidad.CantidadPago);
                    foreach (FacturaDividida f in lstFacDiv)
                    {
                        MuestraMensaje(f.Total.ToString());
                        EntPedido pedidoAgrega = AgregarPedidoPreVenta((int)TipoPedido.PREVENTADIVIDIDA, pedidoSeleccionado.ClienteId, 0,
                                                        ObtieneDetallePedido(pedidoSeleccionado.Detalle, f.Productos), pedidoSeleccionado.Observaciones,
                                                        f.Total, f.SubTotal, 0, f.IEPS,
                                                        0, 0,
                                                        DateTime.Today, DateTime.Now,
                                                        pedidoSeleccionado.TrabajadorId,
                                                        pedidoSeleccionado.NumOrden);
                        try
                        {
                            AgregarProductoPedidoPreVenta(pedidoAgrega.Id, f.Productos);
                        }
                        catch (Exception ex)
                        {
                            EliminaPedidoPreVenta(pedidoAgrega.Id);
                            MandaExcepcion(ex.Message);
                        }
                    }

                    pedidoSeleccionado.EstatusId = (int)EstatusPedidoPreVenta.PREVENTA_DIVIDIDA;
                    new BusPedidos().ActualizaEstatusPedidoPreVenta(pedidoSeleccionado);
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnImprimeTicket_Click(object sender, EventArgs e)
        {
            try
            {
                this.PedidoAgrega = ObtienePedidoFromGV(gvPedidos);
                this.PedidoAgrega.NumOrden = this.PedidoAgrega.NumOrden.PadLeft(5, '0');
                this.IVA = this.PedidoAgrega.IVA;
                this.ClienteSeleccionado = new BusClientes().ObtieneCliente(this.PedidoAgrega.ClienteId);

                ImprimirNotaVenta(this.PedidoAgrega.TipoPedido.PadLeft(13, ' '), this.ClienteSeleccionado, this.PedidoAgrega,
                                    new BusProductos().ObtieneProductosPorPedidoPreVenta(this.PedidoAgrega.Id));

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnVerFactura_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (pedidoSeleccionado.EstatusId != (int)EstatusPedidoPreVenta.FACTURA)
                    MandaExcepcion("PreVenta Sin Facturar");


                List<EntFactura> facturasEncontradas = new BusFacturas().ObtieneFacturasPorPedido(pedidoSeleccionado.PedidoRelacionadoId);
                if (facturasEncontradas.Count > 0)
                {
                    VerificaExistenArchivosFactura(facturasEncontradas[0].Ruta,
                                                    //facturasEncontradas[0].SerieFactura + facturasEncontradas[0].NumeroFactura,
                                                    facturasEncontradas[0].SerieFactura
                                                                + facturasEncontradas[0].NumeroFactura,
                                                    facturasEncontradas[0].PDF,
                                                    facturasEncontradas[0].XML);

                    //VerificaExistenArchivosFactura(pedidoSeleccionado);                    
                    MuestraArchivo(facturasEncontradas[0].Ruta);
                }

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnImprimeNotaVenta_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (pedidoSeleccionado.PedidoRelacionadoId <=0)
                    MandaExcepcion("PreVenta Sin Nota de Venta");

                pedidoSeleccionado.NumOrden = pedidoSeleccionado.PedidoRelacionadoId.ToString().PadLeft(5, '0');
                this.IVA = pedidoSeleccionado.IVA;
                this.ClienteSeleccionado = new BusClientes().ObtieneCliente(pedidoSeleccionado.ClienteId);

                ImprimirNotaVenta(pedidoSeleccionado.TipoPedido.PadLeft(13, ' '), this.ClienteSeleccionado, new BusPedidos().ObtienePedido(pedidoSeleccionado.PedidoRelacionadoId),
                                    new BusProductos().ObtieneProductosPorPedido(pedidoSeleccionado.PedidoRelacionadoId));

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnRefrescarProductosPreventas_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaDesde = FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas);
                DateTime fechaHasta = FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1);
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (rdoPorMesVentas.Checked)
                {
                    if (cmbMesesEntradas.SelectedIndex >= 0)
                    {
                        fechaDesde = FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas);
                        fechaHasta = FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1);
                    }
                }
                else if (rdoPorFechas.Checked)
                {
                    fechaDesde = dtpFechaDesdeVentas.Value.Date;
                    fechaDesde = dtpFechaHastaVentas.Value.Date;
                }

                this.ListaProductosPedidosPreventa = new BusPedidos().ObtieneProductosPedidosPreventa(
                                                                            Program.EmpresaSeleccionada.Id,
                                                                            fechaDesde, fechaHasta);
                ReportParameter parmEmpresa;
                parmEmpresa = new ReportParameter("Empresa", Program.EmpresaSeleccionada.Nombre);

                List<EntPedido> lstFiltro = this.ListaProductosPedidosPreventa;

                this.rvProductosPreventa.LocalReport.DataSources.Clear();
                this.rvProductosPreventa.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("dsVentas", lstFiltro));
                rvProductosPreventa.LocalReport.SetParameters(parmEmpresa);
                rvProductosPreventa.RefreshReport();
                rvProductosPreventa.Refresh();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
    }
}
