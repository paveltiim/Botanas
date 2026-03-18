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
    public partial class SalidasVentasMayoreo : FormBase
    {
        public SalidasVentasMayoreo()
        {
            InitializeComponent();
        }
        public SalidasVentasMayoreo(int SizeX, int SizeY)
        {
            InitializeComponent();
            this.Size = new Size(SizeX, SizeY);
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

                //if (this.ClienteSeleccionado.TipoPersonaId == 2) //NO HAY MAYOREO INICIAL
                //{
                //}

                if (chkDevolucionCortesia.Checked || chkCortesia.Checked)// NO VERIFICA CANTIDAD MINIMA NI PRECIO.
                {
                    ProductoSeleccionado.PrecioVentaSinIVA = 0;
                    ProductoSeleccionado.PrecioVenta = 0;
                }
                else
                {
                    //CANTIDAD MINIMA PARA ALMACENES CULIACAN Y MAZATLAN.
                if (this.AlmacenId == 6  //CULIACAN;
                 || this.AlmacenId == 16 //PUNTO DE VENTA MOVIL - CULIACAN
                 //|| this.AlmacenId == 21 //MAZATLAN;
                 || this.AlmacenId == 15)//15:PRUEBAS
                    {
                        if (ProductoSeleccionado.Existencia < 6)
                            MandaExcepcion("Existencia insuficiente. \n\n Existencia: " + ProductoSeleccionado.Existencia);
                        CantidadAgrega = 6;//CULIACAN;SIEMPRE 6
                    }

                    //ASIGNA PRECIO DEPENDIENDO DE CLIENTE.
                    EntProducto productoPrecio = new BusProductos().ObtieneProductosExistenciaPorAlmacenConPreciosVenta(
                                                                        ProductoSeleccionado.Id, this.AlmacenId,
                                                                        ProductoSeleccionado.TipoProductoId, this.ClienteSeleccionado.Id).First();
                    ProductoSeleccionado.PrecioEspecial = productoPrecio.PrecioVentaSinIVA;
                    ProductoSeleccionado.PrecioVentaSinIVA = productoPrecio.PrecioVentaSinIVA;
                    ProductoSeleccionado.IncluyeIeps = productoPrecio.IncluyeIeps;
                    ProductoSeleccionado.IEPS = productoPrecio.IEPS;

                    AsignaPrecioVenta(ProductoSeleccionado);
                }

                ProductoSeleccionado.Cantidad = CantidadAgrega;//PUEDE SER 6 Ó 1 DEPENDIENDO EL ALMACEN.

                EntProducto productoClaves = new BusProductos().ObtieneProducto(ProductoSeleccionado.Id);
                ProductoSeleccionado.ProductoServicioId = productoClaves.ProductoServicioId;
                ProductoSeleccionado.ProductoServicio = productoClaves.ProductoServicio;
                ProductoSeleccionado.ClaveProductoServicio = productoClaves.ClaveProductoServicio;
                ProductoSeleccionado.ClaveUnidad = productoClaves.ClaveUnidad;
                ProductoSeleccionado.UnidadId = productoClaves.UnidadId;
                ProductoSeleccionado.Unidad = productoClaves.Unidad;

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
            decimal total, subtotal, cantidadIva, cantidadIEPS, impuestoRetenido;
            decimal cantidadIVARetenido = 0;
            decimal cantidadISRRetenido = 0;
            decimal IvaRetenidoPorcentaje = 0, IsrRetenidoPorcentaje = 0;
            EntEmpresa empresaSeleccionada = Program.EmpresaSeleccionada;
            decimal iva = empresaSeleccionada.TasaOCuota * 100;
            decimal ieps = empresaSeleccionada.TasaIEPS * 100;

            decimal porcentaje;
            porcentaje = 100 - IvaRetenidoPorcentaje - IsrRetenidoPorcentaje;
            //porcentaje = porcentaje + iva;
            porcentaje = porcentaje + ieps;

            //if (empresaSeleccionada.TipoFactorId == 1 || empresaSeleccionada.TipoFactorId == 2)// Tasa ó Cuota (IVA: 16% ó 0%)
            //{
            //    total = ListaProductos.Sum(P => P.Precio);

            //    decimal totalConIVA = ListaProductos.Where(P => P.PrecioVentaSinIVA != P.PrecioVenta).Sum(P => P.Precio);
            //    cantidadIva = (totalConIVA * iva) / porcentaje;
            //    subtotal = total - cantidadIva;
            //}
            //else
            //{
            //    total = ListaProductos.Sum(P => P.Precio);
            //    subtotal = total;
            //    cantidadIva = 0;
            //}

            total = ListaProductos.Sum(P => P.Precio);
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

#region DECLARACION CAMPOS
        public EntPedido PedidoAgrega { get; set; }
        EntCliente ClienteSeleccionado = new EntCliente();
        EntProducto ProductoSeleccionado;
        List<EntProducto> ListaProductos = new List<EntProducto>();
        List<EntCliente> ListaClientes = new List<EntCliente>();

        /// <summary>
        /// ObtieneListaProductosFromGV(gvProductosPedido)
        /// </summary>
        List<EntProducto> ListaProductosPedido { get { return ObtieneListaProductosFromGV(gvProductosPedido); } }
        string detallePedido;
        public string DetallePedido
        {
            get
            {
                detallePedido = ("SUC: " + txtSucursal.Text.Trim() + " | ").Replace("SUC:  | ", "");
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

        bool IsPedidoPreVenta { get; set; }
        public bool PedidoFacturado { get; set; }
#endregion

#region Metodos
        void RevisaInventarioDisponible(List<EntProducto> ListaProductosPedidoPreVenta)
        {
            foreach (EntProducto pPre in ListaProductosPedidoPreVenta)
            {
                int existencia = 0;
                EntProducto productoConExistencia = this.ListaProductos.Find(P => P.Id == pPre.Id);
                if (productoConExistencia != null)
                {
                    existencia = this.ListaProductos.Find(P => P.Id == pPre.Id).Existencia;
                    pPre.Existencia = existencia;//PARA VALIDAR AL QUERER MODIFICAR CANTIDAD.
                    if (pPre.Cantidad > existencia)
                    {
                        MuestraMensaje("¡ADVERTENCIA! \n\n" + pPre.Codigo + " " + pPre.Descripcion.ToUpper() +
                            "\n\nLa Cantidad solicitada es mayor a la Existente. \n Existencia: "
                                                          + existencia + "" +
                                                          "\n\n SE REDUCIRÁ LA CANTIDAD A LA EXISTENCIA ACTUAL", "ERROR");
                        pPre.Cantidad = existencia;
                    }
                    else
                        pPre.Estatus = true;//CON EXISTENCIA DISPONIBLE.
                }
                else
                {
                    MuestraMensaje("¡ADVERTENCIA! \n\n" + pPre.Codigo + " " + pPre.Descripcion.ToUpper() +
                                     "\n\nProducto sin Existencia." +
                                     "\n\n SE REDUCIRÁ LA CANTIDAD A LA EXISTENCIA ACTUAL", "ERROR");
                    pPre.Cantidad = existencia;
                }
            }
        }
        void RevisaFaltaExistenciaEnGrid(List<EntProducto> ListaProductosPedidoPreVenta)
        {
            int index = 0;
            foreach (EntProducto pPre in ListaProductosPedidoPreVenta)
            {
                if (!pPre.Estatus)
                    base.CambiaBackColorGv(2, gvProductosPedido, index);

                index++;
            }
            gvProductosPedido.Refresh();
        }
        public void CargaPedidoPreVenta(EntPedido PedidoPreVenta)
        {
            this.IsPedidoPreVenta = true;
            
            InicializaPantalla();

            EntTrabajador trabajador = new BusTrabajadores().ObtieneTrabajador(PedidoPreVenta.TrabajadorId);
            //CargaEstablecimientos(Program.UsuarioSeleccionado.AlmacenMayoristaId);
            CargaEstablecimientos(trabajador.EstablecimientoId);
            pnlAlmacen.Enabled = false;

            this.ClienteSeleccionado = new BusClientes().ObtieneCliente(PedidoPreVenta.ClienteId);
            this.ClienteSeleccionado.Sucursal = ObtieneSucursalFromPedidoDetalle(PedidoPreVenta.Detalle);
            CargaDatosCliente(this.ClienteSeleccionado);
            pnlDatosCliente.Enabled = false;

            CargaTrabajadores(this.EstablecimientoId, PedidoPreVenta.TrabajadorId);

            pnlTipoProducto.Enabled = false;

            List<EntProducto> productosPedidoPreVenta= new BusProductos().ObtieneProductosPorPedidoPreVenta(PedidoPreVenta.Id);
            
            CargaProductos(this.AlmacenId, this.TipoProductoId);

            if (PedidoPreVenta.TipoPedidoId == (int)TipoPedido.PREVENTADEVOLUCION)
                chkDevolucionCortesia.Checked = true;
            else if (PedidoPreVenta.TipoPedidoId == (int)TipoPedido.PREVENTACORTESIA)
                chkCortesia.Checked = true;
            else                    
                RevisaInventarioDisponible(productosPedidoPreVenta);
            
            CargaProductosPedido(productosPedidoPreVenta);

            CalculaSumaTotal(productosPedidoPreVenta, txtSubtotal, txtIEPS, txtTotal);
            //NO SIRVE
            RevisaFaltaExistenciaEnGrid(productosPedidoPreVenta);
            
            chkDevolucionCortesia.Enabled = false;
            chkCortesia.Enabled = false;

            tcGeneral.TabPages.Remove(tpRegistroVentas);
        }

        void CargaEstablecimientos()
        {
            List<EntCatalogoGenerico> establecimientos = new BusEmpresas().ObtieneAlmacenes(Program.UsuarioSeleccionado.CompañiaId, Program.UsuarioSeleccionado.Id);//USUARIO PRINCIPAL
            cmbEstablecimientos.DataSource = establecimientos;
            cmbEstablecimientos.SelectedIndex = 0;
        }
        void CargaEstablecimientos(int SeleccionaEstablecimientoId)
        {
            List<EntCatalogoGenerico> establecimientos = new BusEmpresas().ObtieneAlmacenes(Program.UsuarioSeleccionado.CompañiaId, Program.UsuarioSeleccionado.Id);//USUARIO PRINCIPAL
            cmbEstablecimientos.DataSource = establecimientos;
            if (SeleccionaEstablecimientoId == 0)
                cmbEstablecimientos.SelectedIndex = 0;
            else
                cmbEstablecimientos.SelectedIndex = establecimientos.FindIndex(P => P.Id == SeleccionaEstablecimientoId);
        }

        public void CargaClientes()
        {
            this.ListaClientes = new BusClientes().ObtieneClientesPorEstablecimiento(this.EstablecimientoClientesId);
            gvClientes.DataSource = this.ListaClientes;
            this.ClienteSeleccionado = null;
        }
        public void CargaTrabajadores(int EstablecimientoId)
        {
            List<EntTrabajador> lst = new BusTrabajadores().ObtieneTrabajadores(EstablecimientoId);
            lst.Insert(0, new EntTrabajador() { Id = -1, Nombre = "-SELECCIONE-" });
            cmbTrabajadores.DataSource = lst;
            cmbTrabajadores.SelectedIndex = 0;
        }
        public void CargaTrabajadores(int EstablecimientoId,  int TrabajadorId)
        {
            List<EntTrabajador> lst = new BusTrabajadores().ObtieneTrabajadores(EstablecimientoId);
            lst.Insert(0, new EntTrabajador() { Id = -1, Nombre = "-SELECCIONE-" });
            cmbTrabajadores.DataSource = lst;
            cmbTrabajadores.SelectedIndex = lst.FindIndex(P => P.Id == TrabajadorId);
            if (cmbTrabajadores.SelectedIndex == -1)
                cmbTrabajadores.SelectedIndex = 0;
        }

        public void CargaProductos(int AlmacenId, int TipoProductoId)
        {
            this.ListaProductos = new BusProductos().ObtieneProductosExistenciaPorAlmacen(Program.UsuarioSeleccionado.CompañiaId, 0, AlmacenId,TipoProductoId)
                                                                                        .OrderBy(P => P.Descripcion).ToList();
            gvProductosBusqueda.DataSource = this.ListaProductos;
        }
        public void CargaProductos(List<EntProducto> ListaProductos)
            {
                this.ListaProductos = ListaProductos;
                gvProductosBusqueda.DataSource = ListaProductos;
            }
        /// <summary>
        /// CARGA LISTA EN GV
        /// </summary>
        /// <param name="ListaProductosPedido"></param>
        public void CargaProductosPedido(List<EntProducto> ListaProductosPedido)
        {
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
        bool ClientePublicoGeneral;
        EntCliente ObtieneCliente(int ClienteId)
        {
            List<EntCliente> clientes = this.ListaClientes.Where(P => P.Id == ClienteId).ToList();

            if (clientes.Count == 0)
                throw new Exception("Cliente NO encontrado");

            return clientes[0];
        }
        int IndexFormaPago = 0;

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
                pedidoImprime.NumOrden = "VE-M-" + PedidoAgrega.Id.ToString().PadLeft(6, '0');
                pedidoImprime.Fecha = PedidoAgrega.Fecha;
                pedidoImprime.SubTotal = PedidoAgrega.SubTotal;
                pedidoImprime.IVA = PedidoAgrega.IVA;
                pedidoImprime.IEPS = PedidoAgrega.IEPS;
                pedidoImprime.Total = PedidoAgrega.Total;
                pedidoImprime.Observaciones = Observaciones;
                pedidoImprime.Sucursal = PedidoAgrega.Sucursal;
                ImprimirNotaVenta(TituloImpresion, this.ClienteSeleccionado, pedidoImprime, this.ListaProductosPedido);
            }
        }

#endregion

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
            
            decimal cantidad = listaProductos.Sum(P => P.Cantidad);
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
                btnCancelaFactura.Visible = false;
            }
            //else if (Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.MASTER)
            //    btnEliminar.Visible = true;
        }


        private void Ventas_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPedidoPreVenta)
                {
                    InicializaPantalla();

                    this.ClienteSeleccionado = null;
                    gvProductosPedido.DataSource = null;

                    CargaEstablecimientos();
                    this.EstablecimientoClientesId = Program.UsuarioSeleccionado.EstablecimientoClientesId; //5;//PUNTO VENTA MOCHIS
                    CargaClientes();
                    CargaTrabajadores(this.EstablecimientoId);
                }
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
                gvProductosPedido.DataSource = null;
                this.AlmacenId = Program.UsuarioSeleccionado.AlmacenMayoristaId;
                CargaProductos(this.AlmacenId, this.TipoProductoId);
                CargaClientes();
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

        private void gvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ProductoSeleccionado = ObtieneProductoFromGV(gvProductosBusqueda);
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
                  || e.ColumnIndex == gvProductosPedidoPrecioVentaSinIVA.Index 
                  || e.ColumnIndex == gvProductosPedidoPrecioColumn.Index) && e.RowIndex > -1)//CANTIDAD | PRECIO SIN IVA
                {
                    EntProducto productoSeleccionado = ObtieneProductoFromGV(gvProductosPedido);
                    decimal iva = 0;
                    decimal ieps = 1.08m;
                    if (e.ColumnIndex == cantidadDataGridViewTextBoxColumn.Index)//CANTIDAD 
                    {
                        if (productoSeleccionado.Cantidad < 0)
                            productoSeleccionado.Cantidad = 0;
                        else if (!chkDevolucionCortesia.Checked && !chkCortesia.Checked)//SI NO ES DEV/CORTESIA VERIFICA CANTIDAD MINIMA.
                        {
                            if (this.AlmacenId == 6  //CULIACAN;
                             || this.AlmacenId == 16 //PUNTO DE VENTA MOVIL - CULIACAN
                             //|| this.AlmacenId == 21 //MAZATLAN;
                             || this.AlmacenId == 15)//15:PRUEBAS
                            {
                                if (productoSeleccionado.Cantidad < 6)//POR LOGICA EN AGREGADO SOLO HABRA PRODUCTOS CON EXISTENCIA MAYOR A 6.
                                    productoSeleccionado.Cantidad = 6;//  SIEMPRE 6
                            }
                        }

                        if (productoSeleccionado.Cantidad > productoSeleccionado.Existencia)
                        {
                            MuestraMensajeError("La Cantidad solicitada es mayor a la Existente. \n Existencia: "
                                                   + productoSeleccionado.Existencia, "ERROR");
                            productoSeleccionado.Cantidad = productoSeleccionado.Existencia;
                        }
                    }
                    else if (e.ColumnIndex == gvProductosPedidoPrecioVentaSinIVA.Index) //PRECIO SIN IEPS
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
                    else if (e.ColumnIndex == gvProductosPedidoPrecioColumn.Index)
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
                    gvProductosBusqueda.DataSource = this.ListaProductos.Where(P => P.Codigo.ToUpper()==((TextBox)sender).Text.ToUpper() ||
                                                                                    P.CodigoBarra.ToUpper() == ((TextBox)sender).Text.ToUpper()).ToList();
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

        private void btnRefrescarProductos_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                gvProductosPedido.DataSource = null;
                CargaProductos(this.AlmacenId, this.TipoProductoId);
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
                    //this.ListaProductos.Remove(vProducto.ProductoSeleccionado);
                    //CargaProductos(this.ListaProductos);
                    CargaProductos(this.AlmacenId, this.TipoProductoId);

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
                this.siguienteFacturaId = base.AsignaSiguienteFacturaEnPedido(this.siguienteFacturaId, PedidoAgrega, Program.EmpresaSeleccionada.Id, Program.EmpresaSeleccionada.SerieFactura);

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

        int siguienteFacturaId { get; set; }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                btnAgregar.Enabled = false;
                string mensaje, conexion = "";

                int tipoMovimientoId = (int)TipoMovimiento.VENTA;
                int tipoPedidoId = (int)TipoPedido.VENTAMAYOREO;//LOS TIPOS DE PEDIDOS SON CON ID MENOR(-1).
                string numFactura = "";
                string tipoMovimiento = "-Venta-";

                EntCatalogoGenerico establecimiento = ObtieneCatalogoGenericoFromCmb(cmbEstablecimientos);

                VerificaClienteSeleccionado();
                VerificaProductosSeleccionados(this.ListaProductosPedido);

                Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(this.ClienteSeleccionado.EmpresaId);

                if (chkFacturar.Checked)
                {
                    if (string.IsNullOrEmpty(txtCP.Text.Trim()))
                    {
                        this.DialogResult = DialogResult.Abort;
                        MandaExcepcion("Ingrese C.P. del Cliente");
                    }
                    if (cmbRegimenFiscal.SelectedIndex < 0)
                    {
                        this.DialogResult = DialogResult.Abort;
                        MandaExcepcion("Seleccione Régimen Fiscal");
                    }

                    numFactura = ObtieneUltimaFactura(Program.EmpresaSeleccionada.Id);
                    mensaje = "Se realizará la Factura " + conexion + " \n Núm: " + numFactura + "\n RFC: " + ClienteSeleccionado.RFC + "\n ¿Correcto?";
                    tipoMovimiento = "-Venta (FACTURADA)-";
                }
                else 
                    mensaje = "Se guardará la Venta sin Facturar \n ¿Correcto? ";

                if (MuestraMensajeYesNo(mensaje, "CONFIRMAR") == DialogResult.Yes)
                {
                    base.SetWaitCursor();

                    EntTrabajador trabajador = ObtieneTrabajadorFromCmb(cmbTrabajadores);
                    decimal cantidadIEPS = ConvierteTextoADecimal(txtIEPS);
                    decimal pago = 0;
                    if (chkPagada.Checked)
                        pago = ConvierteTextoADecimal(txtTotal);

                    if (chkDevolucionCortesia.Checked)
                        tipoPedidoId = (int)TipoPedido.DEVOLUCIONCORTESIA;// 4:DEVOLUCION.
                    else if (chkCortesia.Checked)
                        tipoPedidoId = (int)TipoPedido.CORTESIA;//11:CORTESIA.

                    //EntPedido pedidoAgrega
                    this.PedidoAgrega = AgregarPedido(establecimiento.Id, tipoPedidoId, this.ClienteSeleccionado.Id,
                                                this.DetallePedido, txtComentario.Text,
                                                ConvierteTextoADecimal(txtSubtotal), ConvierteTextoADecimal(txtIEPS),
                                                ConvierteTextoADecimal(txtTotal),
                                                pago,
                                                DateTime.Now, DateTime.Today, false,
                                                trabajador.Id);
                    this.PedidoAgrega.Cliente = this.ClienteSeleccionado.Nombre;
                    this.PedidoAgrega.Observaciones = txtComentario.Text;
                    this.PedidoAgrega.Direccion = txtDireccion.Text;
                    this.PedidoAgrega.Sucursal = txtSucursal.Text;
                    //this.PedidoAgrega.Id = pedidoAgrega.Id;

                    bool productosAgregados = false;
                    SalidasVentas vVen = new SalidasVentas();
                    try
                    {
                        AgregarProductoDetallePedido(this.PedidoAgrega.Id, this.ListaProductosPedido);
                        productosAgregados = true;
                    }
                    catch (Exception ex)
                    {
                        this.DialogResult = DialogResult.Abort;
                        vVen.EliminaPedido(this.PedidoAgrega.Id);
                        MandaExcepcion(ex.Message);
                    }

                    if (productosAgregados)
                        vVen.AgregaMovimientosVenta(tipoMovimientoId, this.AlmacenId, "VENTA:" + numFactura, this.PedidoAgrega.Id, this.ListaProductosPedido);

                    int pagoId = 0;
                    if (chkPagada.Checked)
                    {
                        try
                        {
                            pagoId = vVen.AgregarPagoBD(this.PedidoAgrega.Id, pago,
                                                        ConvierteTextoAInteger(cmbFormaPago.Text.Remove(2)), cmbFormaPago.Text.Remove(0, 4));
                            vVen.ActualizaEstatusPedido(this.PedidoAgrega.Id, (int)EstatusPedido.PAGADO);
                        }
                        catch (Exception ex)
                        {
                            MuestraMensajeError("EL PAGO NO PUDO SER AGREGADO. EL PEDIDO QUEDARÁ COMO DEUDA.");
                            vVen.EliminaPago(this.PedidoAgrega.Id, pagoId, pago);
                        }
                    }

                    //bool verificado=true;
                    bool facturado = false;
                    try
                    {
                        if (chkFacturar.Checked)
                        {
                            if (MuestraMensajeYesNo("¿Desea Facturar?") == DialogResult.Yes)
                            {
                                Facturar(this.PedidoAgrega, this.ListaProductosPedido, 0, 0, 0, cantidadIEPS, 0, "", "I",
                                         true, false);
                                facturado = true;
                                this.PedidoFacturado = true;
                            }
                            this.siguienteFacturaId = 0;
                        }
                    }
                    catch (Exception ex) {
                        //verificado = false;
                        //vVen.EliminaPedido(pedidoAgrega.Id);
                        MuestraMensaje(ex.Message + "\n\nEL PEDIDO FUE REGISTRADO PERO NO FACTURADO\n (INVENTARIO DESCARGADO)");
                    }

                    if (!facturado)
                    {
                        string tituloImpresion = " - NOTA VENTA - ";
                        if (this.PedidoAgrega.TipoPedidoId == (int)TipoPedido.DEVOLUCIONCORTESIA)//DEVOLUCION/CORTESIA
                            tituloImpresion = "DEV/COR";
                        MuestraMensaje("¡" + tipoMovimiento + " Registrada!");
                        MandarImprimirNotaVenta(tituloImpresion, tipoMovimiento, this.PedidoAgrega, txtComentario.Text);
                    }
                    if(!this.IsPedidoPreVenta)
                        btnCancelar.PerformClick();//AL PRESIONAR BOTON CANCELAR SE PONE DialogResult.CANCEL

                    //this.DialogResult = DialogResult.OK;//AL PRESIONAR BOTON CANCELAR 
                }
                else
                    this.DialogResult = DialogResult.Abort;             }
            catch (Exception ex)
            {
                this.DialogResult = DialogResult.Abort; MuestraExcepcion(ex); }
            finally { btnAgregar.Enabled = true; base.SetDefaultCursor(); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                LimpiaTextBox(pnlDatosCliente);
                LimpiaTextBox(gbDatosFacturacion);
                LimpiaTextBox(pnlAgrega);
                this.ClienteSeleccionado = null;

                gvProductosPedido.DataSource = null;
                lbContadorSeries.Text = "0";
                txtCambio.Clear();
                txtComentario.Clear();

                pnlProductos.Enabled = false;

                gbComentario.SendToBack();

                chkDevolucionCortesia.Checked = false;
                chkCortesia.Checked = false;

                cmbUsoCFDI.SelectedIndex = 0;

                chk5Descuento.Checked = false;

                CargaProductos(this.AlmacenId, this.TipoProductoId);
                CargaClientes();
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
                    //SE NECESITA AUTORIZAR HABILITAR SI EL CLIENTE NO TIENE CREDITO
                    //else if(this.IsPedidoPreVenta)
                    //    cmbMetodoPago.Enabled = true;

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
        public void CargaPedidos(int EstablecimientoId, DateTime FechaDesde, DateTime FechaHasta)
        {
            //this.ListaPedidos = new List<EntPedido>();
            this.ListaPedidos = new BusPedidos().ObtienePedidosPorEstablecimiento(EstablecimientoId, FechaDesde, FechaHasta);
            if (chkVerDevoluciones.Checked)
                this.ListaPedidos.AddRange(new BusPedidos().ObtienePedidosPorEstablecimiento(EstablecimientoId, FechaDesde, FechaHasta,
                                                                                      (int)TipoPedido.DEVOLUCIONCORTESIA));
            if (chkVerCortesias.Checked)
                this.ListaPedidos.AddRange(new BusPedidos().ObtienePedidosPorEstablecimiento(EstablecimientoId, FechaDesde, FechaHasta,
                                                                                      (int)TipoPedido.CORTESIA));
            //else
            //    this.ListaPedidos = new BusPedidos().ObtienePedidosPorEstablecimiento(EstablecimientoId, FechaDesde, FechaHasta);
            if (!chkVerFacturasCanceladas.Checked)
                this.ListaPedidos = this.ListaPedidos.Where(P => !P.EstatusDescripcion.Contains("CANCELA")).ToList();

            gvPedidos.DataSource = this.ListaPedidos.OrderByDescending(P => P.FacturaId).ToList();
            txtTotalPedidos.Text = FormatoMoney(this.ListaPedidos.Sum(P => P.Total));
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
                //if (rdoEntradasPorDia.Checked)
                //    CargaEntradas(dtpEntradasFechaDia.Value.Date, dtpEntradasFechaDia.Value.Date.AddDays(1), almacen.Id);
                //else 
                if (rdoPorMesVentas.Checked)
                {
                    if (cmbMesesEntradas.SelectedIndex >= 0)
                    {
                        //chkVerFacturasCanceladas.Checked = false;
                        CargaPedidos(establecimiento.Id,
                                    FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                                    FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas));
                    }
                }
                else if (rdoPorFechas.Checked)
                {
                    CargaPedidos(establecimiento.Id,
                                dtpFechaDesdeVentas.Value.Date,
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

        private void btnVerFactura_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (!pedidoSeleccionado.Facturado)
                    MandaExcepcion("Pedido Sin Facturar");

                List<EntFactura> facturasEncontradas = new BusFacturas().ObtieneFacturasPorPedido(pedidoSeleccionado.Id)
                                                                                        .Where(P => P.SerieFactura + P.NumeroFactura == pedidoSeleccionado.Factura).ToList();
                if (facturasEncontradas.Count > 0)
                {
                    VerificaExistenArchivosFactura(facturasEncontradas[0].Ruta,
                                                    //facturasEncontradas[0].SerieFactura + facturasEncontradas[0].NumeroFactura,
                                                    facturasEncontradas[0].SerieFactura
                                                                + facturasEncontradas[0].NumeroFactura,
                                                    facturasEncontradas[0].PDF,
                                                    facturasEncontradas[0].XML);

                    //VerificaExistenArchivosFactura(pedidoSeleccionado);                    
                    MuestraArchivo(pedidoSeleccionado.RutaFactura);
                }

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
        
        private void btnFacturar_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (pedidoSeleccionado.Facturado)
                    MandaExcepcion("PEDIDO YA FACTURADO");
                //if (MuestraMensajeYesNo("¿Desea Facturar el Pedido seleccionado?\n Num. Orden: " + pedidoSeleccionado.NumOrden) == DialogResult.Yes)
                //{
                //    Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(pedidoSeleccionado.EmpresaId);
                //    Facturar(pedidoSeleccionado, new BusProductos().ObtieneProductosPorPedido(pedidoSeleccionado.Id),
                //                0, 0, 0, pedidoSeleccionado.IEPS, 0, "", "I",
                //                Program.EmpresaSeleccionada.Facturacion, false);
                //}
                AgregaFactura vFacturar = new AgregaFactura(pedidoSeleccionado,
                                                            new BusClientes().ObtieneCliente(pedidoSeleccionado.ClienteId), this.EstablecimientoId);
                vFacturar.ShowDialog();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
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
                btnFacturar.Enabled = !pedido.Facturado;
                btnCancelaFactura.Enabled = pedido.Facturado;
                btnComplementoPago.Visible = pedido.Facturado;
                btnComplementoPago.Enabled = pedido.Facturado;
                btnVerComplemento.Visible = pedido.Facturado;
                btnVerComplemento.Enabled = pedido.Facturado;
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (MuestraMensajeYesNo(string.Format("¿Seguro desea ELIMINAR la venta seleccionada? \n "), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    base.SetWaitCursor();

                    SeleccionaAlmacen vAlmacen = new SeleccionaAlmacen();
                    if (vAlmacen.ShowDialog() == DialogResult.OK)
                    {
                        new RegistrosNeue().ReingresoDeProducto(vAlmacen.AlmacenSeleccionado.Id, pedidoSeleccionado.Id, pedidoSeleccionado.Factura);

                        new BusPedidos().CancelaPedidoTodo(pedidoSeleccionado.Id);

                        btnRefrescarVentas.PerformClick();

                        MuestraMensaje("¡Venta Eliminada!", "ELIMINACIÓN VENTA");
                    }
                }
            }
            catch (Exception ex) {
                MuestraExcepcion(ex);
            }
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

        void FiltrarPedidos(List<EntPedido> ListaPedidos, string NumeroOrden, string NombreCliente, string Descripcion, string NumeroFactura)
        {
            //List<EntCliente> clientes = (List<EntCliente>)gvClientes.DataSource;

            var pedidosFiltro = from c in ListaPedidos
                                where c.Cliente.ToUpper().Contains(NombreCliente.ToUpper()) ||
                                      c.ClienteNombreFiscal.ToUpper().Contains(NombreCliente.ToUpper())
                                select c;

            if (!string.IsNullOrWhiteSpace(NumeroOrden))
            {
                pedidosFiltro = from c in pedidosFiltro
                                where c.Id.ToString().Contains(NumeroOrden.ToUpper())
                                select c;
            }

            if (!string.IsNullOrWhiteSpace(Descripcion))
            {
                pedidosFiltro = from c in pedidosFiltro
                                where c.Detalle.ToUpper().Contains(Descripcion.ToUpper())
                                select c;
            }

            if (!string.IsNullOrWhiteSpace(NumeroFactura))
            {
                pedidosFiltro = from c in pedidosFiltro
                                where c.Factura.ToUpper().Contains(NumeroFactura.ToUpper())
                                select c;
            }


            gvPedidos.DataSource = null;
            gvPedidos.DataSource = pedidosFiltro.ToList();
        }
        private void btnFiltrarPedidos_Click(object sender, EventArgs e)
        {
            try
            {
                FiltrarPedidos(this.ListaPedidos, txtNumPedidoFiltro.Text, txtClienteFiltro.Text, txtDescripcionFiltro.Text, txtFacturaFiltro.Text);
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
                CargaProductos(this.AlmacenId, this.TipoProductoId);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

#endregion

#region IMPRESION TICKETS Y CORTE
        private void btnImprimeTicket_Click(object sender, EventArgs e)
        {
            try
            {
                this.PedidoAgrega = ObtienePedidoFromGV(gvPedidos);
                this.PedidoAgrega.NumOrden = "VE-" + this.PedidoAgrega.Id.ToString().PadLeft(5, '0');
                this.PedidoAgrega.Sucursal = ObtieneSucursalFromPedidoDetalle(this.PedidoAgrega.Detalle);
                this.IVA = this.PedidoAgrega.IVA;
                this.ClienteSeleccionado = new BusClientes().ObtieneCliente(this.PedidoAgrega.ClienteId);

                //Imprime = new UtiImpresiones();
                //Imprime.AsignaValoresParametrosImpresion(this.PedidoAgrega,
                //                                        new BusProductos().ObtieneProductosPorPedido(this.PedidoAgrega.Id));
                //ppdImprimeRecibo.ShowDialog();

                string tituloImpresion = " - NOTA VENTA - ";
                if (this.PedidoAgrega.TipoPedidoId == 4)//DEVOLUCION/CORTESIA
                    tituloImpresion = "DEV/COR";

                ImprimirNotaVenta(tituloImpresion, this.ClienteSeleccionado, this.PedidoAgrega,
                                    new BusProductos().ObtieneProductosPorPedido(this.PedidoAgrega.Id));

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnCorte_Click(object sender, EventArgs e)
        {
            try
            {
                if (MuestraMensajeYesNo("¿Desea realizar el Corte (MAYORISTAS) del día?") == DialogResult.Yes)
                {
                    List<EntPedido> corte = new BusPedidos().ObtieneCorteDetalleMayoreo(this.EstablecimientoId, Program.UsuarioSeleccionado.Id);
                    //Imprime = new UtiImpresiones();
                    //Imprime.AsignaValoresParametrosImpresion(corte);
                    //ppdImprimeCorte.Show();
                    ImpresionCorteDetalle vImprimeCorteDetalle = new ImpresionCorteDetalle(base.ConvierteListaPedidosEnProductos(corte));
                    vImprimeCorteDetalle.Show();

                    corte = new BusPedidos().ObtieneCorteMayoreo(this.EstablecimientoId, Program.UsuarioSeleccionado.Id);
                    //Imprime = new UtiImpresiones();
                    //Imprime.AsignaValoresParametrosImpresion(corte);
                    //ppdImprimeCorte.ShowDialog();
                    ImpresionCorte vImprimeCorte = new ImpresionCorte(base.ConvierteListaPedidosEnProductos(corte));
                    vImprimeCorte.Show();

                    new BusPedidos().AgregaCorteMayoreo(this.EstablecimientoId, Program.UsuarioSeleccionado.Id);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnPreCorte_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntPedido> corte = new BusPedidos().ObtieneCorteMayoreo(this.EstablecimientoId, Program.UsuarioSeleccionado.Id);
                ImpresionCorte vImprimeCorte = new ImpresionCorte(base.ConvierteListaPedidosEnProductos(corte));
                vImprimeCorte.Show();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnPreCorteDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntPedido> corte = new BusPedidos().ObtieneCorteDetalleMayoreo(this.EstablecimientoId, Program.UsuarioSeleccionado.Id);
                ImpresionCorteDetalle vImprimeCorte = new ImpresionCorteDetalle(base.ConvierteListaPedidosEnProductos(corte));
                vImprimeCorte.Show();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        #endregion

        private void SalidasVentasMayoreo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.Abort)
                e.Cancel = true;
        }
    }
}
