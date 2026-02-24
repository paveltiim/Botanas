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
    public partial class SalidasVentasVarios : FormBase
    {
        public void VerificaEmpresa()
        {
            cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }

        public SalidasVentasVarios()
        {
            InitializeComponent();
        }

        public SalidasVentasVarios(EntEmpresa Empresa, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente
                     , int FormaPagoId, int MedioPagoId, string CondicionPago, string NumeroCuenta)
        {
            InitializeComponent();
            CargaProductosPedido(ListaProductos);
            CalculaSumaTotal(ListaProductos, txtSubtotal, txtIEPS, txtTotal);
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
                detallePedido = "";
                foreach (EntProducto p in this.ListaProductosPedido)
                    detallePedido += p.Cantidad + " " + p.Descripcion + " | ";
                return detallePedido.Remove(detallePedido.Length - 2);
            }
            //set { detallePedido = value; }
        }

        /// <summary>
        /// ObtieneCatalogoGenericoFromCmb(cmbAlmacenes).Id
        /// </summary>
        int AlmacenId { get { return ObtieneCatalogoGenericoFromCmb(cmbAlmacenes).Id; } }
        /// <summary>
        /// cmbTipoProductoFiltro.SelectedIndex+1
        /// </summary>
        int TipoProductoId { get { return cmbTipoProductoFiltro.SelectedIndex+1; } }

        bool ClientePublicoGeneral;

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
        public void CargaCatalogoUsoCFDI()
        {
            //ListaEmpresas = new BusEmpresas().ObtieneCatalogoRegimen();
            cmbUsoCFDI.DataSource = new BusEmpresas().ObtieneCatalogoUsoCFDI();
        }
        void CargaAlmacenes()
        {
            List<EntCatalogoGenerico> almacenes = new BusEmpresas().ObtieneAlmacenes(Program.UsuarioSeleccionado.CompañiaId, Program.UsuarioSeleccionado.Id);
            cmbAlmacenes.DataSource = almacenes;
            cmbAlmacenes.SelectedIndex = 0;
        }
        //void CargaTipoSalidas()
        //{
        //    List<EntCatalogoGenerico> salidas = new List<EntCatalogoGenerico>();
        //    salidas.Add(new EntCatalogoGenerico() { Id=2, Descripcion ="VENTA"});
        //    salidas.Add(new EntCatalogoGenerico() { Id = 3, Descripcion = "AJUSTE" });
        //    salidas.Add(new EntCatalogoGenerico() { Id = 4, Descripcion = "TRASPASO" });
        //    cmbTipoSalidas.DataSource = salidas;
        //    cmbTipoSalidas.SelectedIndex = 0;
        //}
        public void CargaClientes()
        {
            this.ListaClientes = new BusClientes().ObtieneClientesPorEstablecimiento(this.AlmacenId);
            gvClientes.DataSource = this.ListaClientes;
            this.ClienteSeleccionado = null;
        }

        public void CargaProductos(int AlmacenId, int TipoProductoId)
        {
            this.ListaProductos = new BusProductos().ObtieneProductosPorAlmacen(Program.UsuarioSeleccionado.CompañiaId, 0, AlmacenId, TipoProductoId)
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
            btnCancelar.PerformClick();
            this.ClienteSeleccionado = Cliente;//HAY QUE REASIGNAR POR NULLIFICAR EN btnCancelar
            Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(this.ClienteSeleccionado.EmpresaId);
            txtNombre.Text = Cliente.Nombre;
            txtNombreFiscal.Text = Cliente.NombreFiscal;
            txtRFC.Text = Cliente.RFC;
            txtEmail.Text = Cliente.Email;
            txtCP.Text = Cliente.CP;

            if (Cliente.Credito)
                cmbMetodoPago.SelectedIndex = 0;
            cmbMetodoPago.Enabled = Cliente.Credito;
            cmbFormaPago.SelectedIndex = Cliente.FormaPagoId - 1;
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
                Empleado = Program.UsuarioSeleccionado.Usuario,
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
            Productos vProd = new Productos();
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
                vProd.AumentaProducto(p.Id, -p.Cantidad);
            }
        }
        
        void AgregaProductoEnPedido(EntProducto ProductoSeleccionado, decimal CantidadAgrega)
        {
            List<EntProducto> productosPedido = ObtieneListaProductosFromGV(gvProductosPedido);

            if (CantidadAgrega > 0)
            {
                if (productosPedido == null)
                    productosPedido = new List<EntProducto>();

                ProductoSeleccionado.Cantidad = CantidadAgrega;
                if (ProductoSeleccionado.TipoProductoId != 3)
                {
                    EntProducto productoPrecio = new BusProductos().ObtieneProductosExistenciaPorAlmacenConPreciosVenta(
                                                                ProductoSeleccionado.Id, this.AlmacenId,
                                                                ProductoSeleccionado.TipoProductoId, this.ClienteSeleccionado.Id).First();

                    ProductoSeleccionado.PrecioVentaSinIVA = productoPrecio.PrecioVentaSinIVA;
                    ProductoSeleccionado.IEPS = productoPrecio.IEPS;
                    ProductoSeleccionado.IncluyeIeps = productoPrecio.IncluyeIeps;
                    //CALCULO PRECIO MAYOREO MENUDEO
                    if (this.ClienteSeleccionado.TipoPersonaId == 2) //NO HAY MAYOREO INICIAL
                    {
                        decimal precioPorCantidad = new BusProductos().ObtienePrecioProductoCantidad(0, ProductoSeleccionado.Id, 1).PrecioVenta;
                        if (precioPorCantidad > 0)
                        {
                            ProductoSeleccionado.PrecioVentaSinIVA = precioPorCantidad;
                            if (ProductoSeleccionado.IncluyeIeps)
                                ProductoSeleccionado.IEPS = ProductoSeleccionado.PrecioVentaSinIVA * (base.IEPS);
                        }
                    }
                }

                ProductoSeleccionado.PrecioVenta = ProductoSeleccionado.PrecioVentaSinIVA + ProductoSeleccionado.IEPS;
  
                EntProducto productoClaves = new BusProductos().ObtieneProducto(ProductoSeleccionado.Id);
                ProductoSeleccionado.ProductoServicioId = productoClaves.ProductoServicioId;
                ProductoSeleccionado.ProductoServicio = productoClaves.ProductoServicio;
                ProductoSeleccionado.ClaveProductoServicio = productoClaves.ClaveProductoServicio;
                ProductoSeleccionado.ClaveUnidad= productoClaves.ClaveUnidad;
                ProductoSeleccionado.UnidadId = productoClaves.UnidadId;
                ProductoSeleccionado.Unidad = productoClaves.Unidad;
                
                if (chkDevolucionCortesia.Checked)
                {
                    ProductoSeleccionado.PrecioVentaSinIVA = 0;
                    ProductoSeleccionado.PrecioVenta = 0;
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

            //productosPedido[productosPedido.Count - 1].Descripcion=productosPedido[productosPedido.Count - 1].Descripcion.Replace("Solicitud:".PadLeft(5, '-') + txtSolicitud.Text,"");
            //productosPedido[productosPedido.Count - 1].Descripcion += "Solicitud:".PadLeft(5, '-') + txtSolicitud.Text;

        }

        /// <summary>
        /// Solo Agrega registro de Pago. TOMA LA FECHA DEL DIA.
        /// </summary>
        /// <param name="PedidoId"></param>
        /// <param name="Cantidad"></param>
        /// <param name="FormaPagoId"></param>
        /// <param name="FormaPago"></param>
        /// <returns></returns>
        public int AgregarPago(int PedidoId, decimal Cantidad, int FormaPagoId, string FormaPago)
        {
            EntPago pago = new EntPago()
            {
                PedidoId = PedidoId,
                TipoPagoId = 1,
                Cantidad = Cantidad,
                FechaPago = DateTime.Today,
                FormaPagoId = FormaPagoId,
                FormaPago = FormaPago,
                UsuarioId = Program.UsuarioSeleccionado.Id
            };
            return new BusPedidos().AgregaPago(pago);
        }

        /// <summary>
        /// Agrega Registro de Pago y Actualiza EstatusId de Pedido si es necesario.
        /// </summary>
        /// <param name="PedidoId"></param>
        /// <param name="Cantidad"></param>
        /// <param name="FormaPagoId"></param>
        /// <param name="FormaPago"></param>
        /// <param name="FechaPago"></param>
        /// <returns></returns>
        public int AgregarPagoPedido(int EstablecimientoId, int PedidoId, decimal Cantidad, int FormaPagoId, string FormaPago, DateTime FechaPago)
        {
            EntPago pago = new EntPago()
            {
                PedidoId = PedidoId,
                TipoPagoId = 1,
                Cantidad = Cantidad,
                FechaPago = FechaPago,
                FormaPagoId = FormaPagoId,
                FormaPago = FormaPago,
                UsuarioId = Program.UsuarioSeleccionado.Id
            };
            return new BusPedidos().AgregaPagoPedido(EstablecimientoId, pago);
        }

        public void EliminaPago(int PedidoId, int PagoId, decimal Pago)
        {
            new BusPedidos().ActualizaEstatusPago(PagoId, false);
            new BusPedidos().AumentaPagoPedido(PedidoId, Pago);
        }
        public void EliminaPedido(int PedidoId)
        {
            new BusPedidos().EliminaPedidoTodo(PedidoId);
        }

        public void ActualizaEstatusPedido(int PedidoId, int EstatusId)
        {
            EntPedido pedido = new EntPedido()
            {
                Id = PedidoId,
                EstatusId = EstatusId
            };
            new BusPedidos().ActualizaEstatusPedido(pedido);
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
        
        public void CreaImagenBMPsalida(string RutaCompleta,
                                System.Drawing.Image Fondo, System.Drawing.Image Logo, System.Drawing.Image Leyenda, System.Drawing.Image Firma,
                                string NumeroOrden, EntEmpresa EmpresaSeleccionada, EntCliente ClienteSeleccionado, EntPedido PedidoAgrega,
                                List<EntProducto> ProductosSeleccionados)
        {
            string rutaArchivoImagen = RutaCompleta + "\\" + NumeroOrden + ".bmp";
            using (Bitmap myBitmap = new Bitmap(820, 1070))
            {
                Graphics newGraphics = Graphics.FromImage(myBitmap);
                newGraphics.DrawImage(Fondo, 0, 0);
                UtiImpresiones imprimir = new UtiImpresiones();
                imprimir.ImprimirSalida(EmpresaSeleccionada, ClienteSeleccionado, PedidoAgrega, ProductosSeleccionados, IVA, Logo, Leyenda, Firma, newGraphics);

                //myBitmap.Save(RutaCompleta+ "\\"+NumeroOrden+".bmp");

                using (MemoryStream stream = new MemoryStream())
                {
                    StreamWriter sw = new StreamWriter(rutaArchivoImagen);
                    myBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    stream.WriteTo(sw.BaseStream);
                    Clipboard.SetImage(System.Drawing.Image.FromStream(stream));//FromFile(RutaCompleta + "\\" + NumeroOrden + ".bmp"));
                    sw.Close();
                    stream.Close();
                }
                myBitmap.Dispose();
                newGraphics.Dispose();
            }
        }
        void ImprimirNotaVenta(string TituloImpresion, EntCliente Cliente, EntPedido Pedido, List<EntProducto> ProductosSeleccionados)
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
                                        rutaGuardaArchivosOrden + "\\NOTAVENTA-" + Pedido.NumOrden + ".pdf");
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
                pedidoImprime.NumOrden = "VE-" + PedidoAgrega.Id.ToString().PadLeft(6, '0');
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
    
        void InicializaPantalla()
        {
            if (Program.EmpresaSeleccionada != null && cmbEmpresas.Items.Count > 0)
                cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);

            cmbMesesEntradas.SelectedIndex = DateTime.Today.Month - 1;
            CargaAñosCmb(cmbAñoEntradas);
            base.CargaCatalogoRegimen(cmbRegimenFiscal);
            base.CargaCatalogoUsoCFDI(cmbUsoCFDI);
            cmbFormaPago.SelectedIndex = 0;
            cmbMetodoPago.SelectedIndex = 0;

            gbComentario.SendToBack();

            cmbTipoProductoFiltro.SelectedIndex = cmbTipoProductoFiltro.Items.Count-1;
            if (Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.PUNTOVENTA
                || Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.PUNTOVENTAMENUDEO)
            {
                btnEliminar.Visible = false;
                btnCancelaFactura.Visible = false;
            }
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
                //CargaTipoSalidas();
                CargaClientes();
                //}
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
                CargaProductos(this.AlmacenId, this.TipoProductoId);
                CargaClientes();
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
                        CargaDatosCliente(this.ClienteSeleccionado);

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
                FiltroClientes vClientes = new FiltroClientes(new BusClientes().ObtieneClientesPorEstablecimiento(this.AlmacenId));
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
                if ((e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5) && e.RowIndex > -1)//CANTIDAD | PRECIO SIN IVA
                {
                    EntProducto productoSeleccionado = ObtieneProductoFromGV(gvProductosPedido);
                    decimal iva = 0;
                    decimal ieps = 1.08m;
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
                        //CALCULO PRECIO MAYOREO MENUDEO
                        if (!chkDevolucionCortesia.Checked)//SI ES DEVOLUCION POR CORTESIA EL PRECIO DEBE SER SIEMPRE $0.
                        {
                            if (this.ClienteSeleccionado.TipoPersonaId == 1 || this.ClienteSeleccionado.TipoPersonaId == 2) //MENUDEO (PUEDE PASAR A MAYOREO)
                            {
                                if (productoSeleccionado.Cantidad >= base.CantidadLimiteMayoreo)
                                {
                                    decimal precioPorCantidad = productoSeleccionado.PrecioVentaSinIVA;
                                    precioPorCantidad = new BusProductos().ObtienePrecioProductoCantidad(0, productoSeleccionado.Id, base.CantidadLimiteMayoreo).PrecioVenta;
                                    if (precioPorCantidad > 0)
                                    {
                                        productoSeleccionado.PrecioVentaSinIVA = precioPorCantidad;
                                        if (productoSeleccionado.IncluyeIeps)
                                            productoSeleccionado.IEPS = productoSeleccionado.PrecioVentaSinIVA * (ieps - 1);
                                        ProductoSeleccionado.PrecioVenta = ProductoSeleccionado.PrecioVentaSinIVA + ProductoSeleccionado.IEPS;
                                    }
                                }
                                //}
                                //else if (this.ClienteSeleccionado.TipoPersonaId == 2) //MAYOREO (PUEDE PASAR A MENUDEO)
                                //{
                                if (productoSeleccionado.Cantidad < base.CantidadLimiteMayoreo)
                                {
                                    decimal precioPorCantidad = productoSeleccionado.PrecioVentaSinIVA;
                                    precioPorCantidad = new BusProductos().ObtienePrecioProductoCantidad(0, productoSeleccionado.Id, 1).PrecioVenta;
                                    if (precioPorCantidad > 0)
                                    {
                                        productoSeleccionado.PrecioVentaSinIVA = precioPorCantidad;
                                        if (productoSeleccionado.IncluyeIeps)
                                            productoSeleccionado.IEPS = productoSeleccionado.PrecioVentaSinIVA * (ieps - 1);
                                        ProductoSeleccionado.PrecioVenta = ProductoSeleccionado.PrecioVentaSinIVA + ProductoSeleccionado.IEPS;
                                    }
                                }
                            }
                        }
                    }
                    else if (e.ColumnIndex == 4) //PRECIO SIN IEPS
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
                    else if (e.ColumnIndex == 5)
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

        private void txtDescuento_Leave(object sender, EventArgs e)
        {
            TextBoxDecimalMoney_Leave(sender, e);
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        public string ObtieneUltimaFactura(int EmpresaId)
        {
            int ultimaFactura = ConvierteTextoAInteger(new BusPedidos().ObtieneUltimaFactura(EmpresaId).NumeroFactura);
            ultimaFactura++;

            return ultimaFactura.ToString();
        }

        public void AgregaMovimientosVenta(int tipoMovimientoId, int AlmacenId, string Descripcion, int PedidoId, 
                                List<EntProducto> ProductosSeleccionados)
        {
            int orientacion = 2;
            int movimientoId = new BusPedidos().AgregaMovimientoMaster(Descripcion, tipoMovimientoId,
                                                        orientacion, AlmacenId, PedidoId, Program.UsuarioSeleccionado.Id);

            foreach (EntProducto p in ProductosSeleccionados)
            {
                new BusPedidos().AgregaMovimientoDetalle(movimientoId, p.Id, p.Cantidad, p.Precio);
            }

            new BusPedidos().AgregaMovimientoLote(movimientoId, orientacion);
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
            if (Program.ConexionIdActual == 1 && Timbrar && Program.EmpresaSeleccionada.Facturacion)//PRODUCCION
            {
                string pathFacturasBase = @"C:\TIIM\Facturacion\Facturas";
                this.PathClienteDirectorioFacturas = base.CreaPathClienteDirectorioFacturas(pathFacturasBase, Cliente.Nombre, Pedido.Factura);

                Pedido.Factura = ObtieneUltimaFactura(Program.EmpresaSeleccionada.Id);
                //uuid = factura.FacturarNeue(Program.EmpresaSeleccionada, Pedido, ListaProductos, Cliente,
                //                            Program.EmpresaSeleccionada.SerieFactura, Pedido.Factura, FechaFactura,
                //                            FormaPago, MedioPago, CondicionPago, 
                //                            CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, ImpuestoRetenido, "",
                //                            UsoCFDI, this.PathClienteDirectorioFacturas);
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
                Fecha = DateTime.Today
            };
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(this.PathClienteDirectorioFacturas);
            foreach (System.IO.FileInfo file in dir.GetFiles())
            {
                if (file.Extension == ".pdf")
                {
                    UtiPDF modiPDF = new UtiPDF();
                    string nota1 = "";
                    string nota2 = "";
                    string nota3 = "";// "Dirección: " + txtDireccion.Text;
                    string nota4 = "NOTA: FAVOR DE SOLICITAR LAS DEVOLUCIONES AL MOMENTO DE HACER UN PEDIDO Y NO AL MOMENTO DE QUE ESTEN ENTREGANDO PRODUCTO. "
                                    + "NO HABRÁ DEVOLUCIONES DESPUÉS DE 8 DÍAS DE PEDIDO.";// "Dirección: " + txtDireccion.Text;
                    string rutaArchivoPDF = file.FullName;
                    if (ListaProductos.Count > 40)
                        nota4 = "";
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
                //foreach (EntProducto p in ProductosSeleccionados)
                //{
                //    Productos vProd = new Productos();
                //    vProd.ActualizaEstatusProductoDetalle(p, 1);//ESTATUS:1=ACTIVO
                //    vProd.AumentaProducto(p.ProductoId, Convert.ToInt32(p.CantidadDecimal));
                //}
                //ActualizaEstatusProductoDetallePedido(PedidoAgrega, false);//ESTATUS:0=CANCELADO
                //ActualizaEstatusPedido(PedidoAgrega, 0);//ESTATUS:0=CANCELADO

                //MuestraExcepcionFacturacion(ex);
                MandaExcepcion("ERROR EN TIMBRADO\n"+ex.Message);
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

        EntPedido PedidoAgrega { get; set; }
        int siguienteFacturaId { get; set; }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                btnAgregar.Enabled = false;
                string mensaje, conexion = "";

                int tipoMovimientoId = (int)TipoMovimiento.VENTA;
                int tipoPedidoId = tipoMovimientoId - 1; //LOS TIPOS DE PEDIDOS SON CON ID MENOR(-1).
                string numFactura = "";
                string tipoMovimiento = "-Venta-";

                VerificaClienteSeleccionado();
                VerificaProductosSeleccionados(this.ListaProductosPedido);

                Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(this.ClienteSeleccionado.EmpresaId);

                if (chkFacturar.Checked)
                {
                    if (string.IsNullOrEmpty(txtCP.Text.Trim()))
                        MandaExcepcion("Ingrese C.P. del Cliente");
                    if (cmbRegimenFiscal.SelectedIndex < 0)
                        MandaExcepcion("Seleccione Régimen Fiscal");

                    numFactura = ObtieneUltimaFactura(Program.EmpresaSeleccionada.Id);
                    mensaje = "Se realizará la Factura " + conexion + " \n Núm: " + numFactura + "\n RFC: " + ClienteSeleccionado.RFC + "\n ¿Correcto?";
                    tipoMovimiento = "-Venta (FACTURADA)-";
                }
                else 
                    mensaje = "Se guardará la Venta sin Facturar \n ¿Correcto? ";
                
                if (MuestraMensajeYesNo(mensaje, "CONFIRMAR") == DialogResult.Yes)
                {
                    base.SetWaitCursor();
                                        
                    decimal cantidadIEPS = ConvierteTextoADecimal(txtIEPS);
                    decimal pago = 0;
                    if (chkPagada.Checked)
                        pago = ConvierteTextoADecimal(txtTotal);

                    if (chkDevolucionCortesia.Checked)
                        tipoPedidoId = 4;//DEVOLUCION/CORTESIA.
                    this.PedidoAgrega = AgregarPedido(this.AlmacenId, tipoPedidoId, this.ClienteSeleccionado.Id, 
                                                this.DetallePedido, txtComentario.Text,
                                                ConvierteTextoADecimal(txtSubtotal), ConvierteTextoADecimal(txtIEPS), 
                                                ConvierteTextoADecimal(txtTotal), 
                                                pago, 
                                                DateTime.Now, DateTime.Today, false,//chkFacturar.Checked,
                                                0);
                    this.PedidoAgrega.Cliente = this.ClienteSeleccionado.Nombre;
                    this.PedidoAgrega.Observaciones = txtComentario.Text;
                    this.PedidoAgrega.Sucursal = "";

                    bool productosAgregados = false;
                    try
                    {
                        AgregarProductoDetallePedido(this.PedidoAgrega.Id, this.ListaProductosPedido);
                        productosAgregados = true;
                    }
                    catch (Exception ex)
                    {
                        EliminaPedido(this.PedidoAgrega.Id);
                        MandaExcepcion(ex.Message);
                    }

                    if(productosAgregados)
                        AgregaMovimientosVenta(tipoMovimientoId, this.AlmacenId, "VENTA: " + numFactura, this.PedidoAgrega.Id, this.ListaProductosPedido);

                    int pagoId = 0;
                    if (chkPagada.Checked)
                    {
                        try
                        {
                            pagoId = AgregarPago(this.PedidoAgrega.Id, pago, ConvierteTextoAInteger(cmbFormaPago.Text.Remove(2)), cmbFormaPago.Text.Remove(0, 4));
                            ActualizaEstatusPedido(this.PedidoAgrega.Id, (int)EstatusPedido.PAGADO);
                        }
                        catch (Exception ex) {
                            MuestraMensajeError("EL PAGO NO PUDO SER AGREGADO. EL PEDIDO QUEDARÁ COMO DEUDA.");
                            EliminaPago(this.PedidoAgrega.Id, pagoId, pago);
                        }
                    }

                    bool facturado = false;
                    try
                    {
                        if (chkFacturar.Checked)
                        {
                            if (MuestraMensajeYesNo("¿Desea Facturar?") == DialogResult.Yes)
                            {
                                Facturar(this.PedidoAgrega, this.ListaProductosPedido, 0, 0, 0, cantidadIEPS, 0, "", "I",
                                         Program.EmpresaSeleccionada.Facturacion, false);
                                facturado = true;
                            }
                            this.siguienteFacturaId = 0;
                        }
                    }
                    catch (Exception ex){
                        //EliminaPedido(this.PedidoAgrega.Id);
                        MuestraMensaje(ex.Message + "\n\nEL PEDIDO FUE REGISTRADO PERO NO FACTURADO\n (INVENTARIO DESCARGADO)");
                    }

                    if (!facturado)
                    {
                        string tituloImpresion = " - NOTA VENTA - ";
                        if (this.PedidoAgrega.TipoPedidoId == 4)//DEVOLUCION/CORTESIA
                            tituloImpresion = "DEV/COR";
                        //MuestraMensaje("¡" + tipoMovimiento + " Registrada!");
                        ////MandarImprimirNotaVenta(tituloImpresion, tipoMovimiento, this.PedidoAgrega, txtComentario.Text);
                        Imprime = new UtiImpresiones();
                        Imprime.AsignaValoresParametrosImpresion(this.PedidoAgrega,
                                                                new BusProductos().ObtieneProductosPorPedido(this.PedidoAgrega.Id));
                        pdImprimeRecibo.Print();
                        ppdImprimeRecibo.ShowDialog();                        
                    }
                    btnCancelar.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
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

                cmbUsoCFDI.SelectedIndex = 0;

                CargaProductos(this.AlmacenId, this.TipoProductoId);
                CargaClientes();
                chkPagada.Checked = true;
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

        #region REGISTROS

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
                        btnRefrescarVentas.PerformClick();
                    //}
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }
        
        List<EntPedido> ListaPedidos = new List<EntPedido>();
        public void CargaPedidos(int EstablecimientoId, DateTime FechaDesde, DateTime FechaHasta)
        {
            this.ListaPedidos = new BusPedidos().ObtienePedidosPorEstablecimiento(EstablecimientoId, FechaDesde, FechaHasta);
            if (!chkVerFacturasCanceladas.Checked)
                this.ListaPedidos = this.ListaPedidos.Where(P => !P.EstatusDescripcion.Contains("CANCELA")).ToList();

            gvPedidos.DataSource = this.ListaPedidos.OrderByDescending(P => P.FacturaId).ToList();
            txtTotalPedidos.Text = FormatoMoney(this.ListaPedidos.Sum(P => P.Total));
        }
        private void btnRefrescarEntradas_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                EntCatalogoGenerico establecimiento = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);
                //if (rdoEntradasPorDia.Checked)
                //    CargaEntradas(dtpEntradasFechaDia.Value.Date, dtpEntradasFechaDia.Value.Date.AddDays(1), almacen.Id);
                //else 
                if (rdoPorMesVentas.Checked)
                {
                    if (cmbMesesEntradas.SelectedIndex >= 0)
                    {
                        chkVerFacturasCanceladas.Checked = false;
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

        #endregion

        private void chkFacturar_CheckedChanged(object sender, EventArgs e)
        {
            try {
                gbDatosFacturacion.Enabled = chkFacturar.Checked;
                //chkPagada.Enabled = !chkFacturar.Checked;

                if (chkFacturar.Checked)
                    chkDevolucionCortesia.Checked = false;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        int IndexFormaPago = 0;
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

        private void btnVerFactura_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (!pedidoSeleccionado.Facturado)
                    MandaExcepcion("Pedido Sin Facturar");

                List<EntFactura> facturasEncontradas = new BusFacturas().ObtieneFacturasPorPedido(pedidoSeleccionado.Id);
                if (facturasEncontradas.Count > 0)
                {
                    VerificaExistenArchivosFactura(facturasEncontradas[0].Ruta,
                                                    //facturasEncontradas[0].SerieFactura + facturasEncontradas[0].NumeroFactura,
                                                    facturasEncontradas[0].SerieFactura 
                                                                + facturasEncontradas[0].NumeroFactura,
                                                    facturasEncontradas[0].PDF,
                                                    facturasEncontradas[0].XML);

                    MuestraArchivo(pedidoSeleccionado.RutaFactura);
                }

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
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
        EntCliente ObtieneCliente(int ClienteId)
        {
            List<EntCliente> clientes = this.ListaClientes.Where(P => P.Id == ClienteId).ToList();

            if (clientes.Count == 0)
                throw new Exception("Cliente NO encontrado");

            return clientes[0];
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
        void ActualizaEstatusFacturaPedido(int FacturaId, int EstatusId)
        {
            EntFactura fac = new EntFactura()
            {
                Id = FacturaId,
                EstatusId = EstatusId
            };
            new BusFacturas().ActualizaEstatusFacturaPedido(fac);
        }

        private void btnCancelaFactura_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (MuestraMensajeYesNo(string.Format("¿Seguro desea CANCELAR la Factura seleccionada? \n " +
                                         "UUID: {0}", pedidoSeleccionado.UUID), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    base.SetWaitCursor();

                    base.TimbraCancelacion(pedidoSeleccionado);

                    ActualizaEstatusFacturaPedido(pedidoSeleccionado.FacturaId, 0);

                    btnRefrescarVentas.PerformClick();

                    MuestraMensaje("¡Factura Cancelada!", "CANCELACIÓN DE FACTURA");
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
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

                //ActualizaDetallePedido(pedido.Id);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
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
                            int pagoId = 0;
                            pagoAgregado = true;
                            try
                            {
                                pagoId = AgregarPagoPedido(this.AlmacenId, p.Id, p.PagoTotal, vComplePago.FormaPagoId, vComplePago.FormaPago, vComplePago.FechaPago);
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

        UtiImpresiones Imprime;
        private void pdImprimeRecibo_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                this.Imprime.ImprimeRecibo(e.Graphics, pbLogoTicket.Image);
                //string titulo = " - NOTA VENTA - ";
                //if (this.PedidoAgrega.TipoPedidoId == 4)//DEVOLUCION/CORTESIA
                //    titulo = "DEV/COR";
                //this.Imprime.ImprimirNotaVenta(titulo, Program.EmpresaSeleccionada, this.ClienteSeleccionado, this.PedidoAgrega,
                //    new BusProductos().ObtieneProductosPorPedido(this.PedidoAgrega.Id), this.IVA,
                //                        pbLogo.Image, pbLeyendaCotizacion.Image, pbLeyendaCotizacion.Image, e.Graphics);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnImprimeTicket_Click(object sender, EventArgs e)
        {
            try
            {
                this.PedidoAgrega = ObtienePedidoFromGV(gvPedidos);
                if (this.PedidoAgrega == null)
                    MandaExcepcion("SELECCIONE UN PEDIDO");
                this.PedidoAgrega.NumOrden = "VE-"+this.PedidoAgrega.Id.ToString().PadLeft(5, '0');
                this.IVA = this.PedidoAgrega.IVA;
                this.ClienteSeleccionado = new BusClientes().ObtieneCliente(this.PedidoAgrega.ClienteId);

                Imprime = new UtiImpresiones();
                Imprime.AsignaValoresParametrosImpresion(this.PedidoAgrega,
                                                        new BusProductos().ObtieneProductosPorPedido(this.PedidoAgrega.Id));
                pdImprimeRecibo.Print();
                ppdImprimeRecibo.ShowDialog();

                //string tituloImpresion = " - NOTA VENTA - ";
                //if (this.PedidoAgrega.TipoPedidoId == 4)//DEVOLUCION/CORTESIA
                //    tituloImpresion = "DEV/COR";

                //ImprimirNotaVenta(tituloImpresion, this.ClienteSeleccionado, this.PedidoAgrega, 
                //                    new BusProductos().ObtieneProductosPorPedido(this.PedidoAgrega.Id));

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (pedidoSeleccionado.Facturado)
                    MandaExcepcion("PEDIDO YA FACTURADO");
                //if (MuestraMensajeYesNo("¿Desea Facturar el Pedido seleccionado?\n Num. Orden: "+pedidoSeleccionado.Id) == DialogResult.Yes)
                //{
                //    EntCliente cliente = new BusClientes().ObtieneCliente(pedidoSeleccionado.ClienteId);
                //    Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(cliente.EmpresaId);
                //    bool facturado = Facturar(pedidoSeleccionado, new BusProductos().ObtieneProductosPorPedido(pedidoSeleccionado.Id),
                //                0, 0, 0, pedidoSeleccionado.IEPS, 0, "", "I",
                //    Program.EmpresaSeleccionada.Facturacion, false);
                //    //false, true);

                //    new BusPedidos().ActualizaPedido(pedidoSeleccionado.Id, facturado);
                //}
                AgregaFactura vFacturar = new AgregaFactura(pedidoSeleccionado,
                                                           new BusClientes().ObtieneCliente(pedidoSeleccionado.ClienteId), this.AlmacenId);
                vFacturar.ShowDialog();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnPreCorte_Click(object sender, EventArgs e)
        {
            try
            {

                List<EntPedido> corte = new BusPedidos().ObtieneCorte(this.AlmacenId, Program.UsuarioSeleccionado.Id);
                //Imprime = new UtiImpresiones();
                //Imprime.AsignaValoresParametrosImpresion(corte);
                //ppdImprimeCorte.ShowDialog();
                ImpresionCorte vImprimeCorte = new ImpresionCorte(base.ConvierteListaPedidosEnProductos(corte));
                vImprimeCorte.Show();

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void pdImprimeCorte_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                Imprime.ImprimeCorte(e.Graphics);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnCorte_Click(object sender, EventArgs e)
        {
            try
            {
                if (MuestraMensajeYesNo("¿Desea realizar el Corte del día?") == DialogResult.Yes)
                {
                    List<EntPedido> corte = new BusPedidos().ObtieneCorteDetalle(this.AlmacenId, Program.UsuarioSeleccionado.Id);
                    //Imprime = new UtiImpresiones();
                    //Imprime.AsignaValoresParametrosImpresion(corte);
                    //ppdImprimeCorte.Show();
                    ImpresionCorteDetalle vImprimeCorteDetalle = new ImpresionCorteDetalle(base.ConvierteListaPedidosEnProductos(corte));
                    vImprimeCorteDetalle.Show();

                    corte = new BusPedidos().ObtieneCorte(this.AlmacenId, Program.UsuarioSeleccionado.Id);
                    //Imprime = new UtiImpresiones();
                    //Imprime.AsignaValoresParametrosImpresion(corte);
                    //ppdImprimeCorte.ShowDialog();
                    ImpresionCorte vImprimeCorte = new ImpresionCorte(base.ConvierteListaPedidosEnProductos(corte));
                    vImprimeCorte.Show();

                    new BusPedidos().AgregaCorte(this.AlmacenId, Program.UsuarioSeleccionado.Id);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void chkDevolucionCortesia_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkDevolucionCortesia.Checked)
                {
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
                else
                {
                    gbComentario.SendToBack();
                    txtComentario.Clear();
                    btnCancelar.PerformClick();
                }
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

        private void btnPreCorteDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntPedido> corte = new BusPedidos().ObtieneCorteDetalle(this.AlmacenId, Program.UsuarioSeleccionado.Id);
                //Imprime = new UtiImpresiones();
                //Imprime.AsignaValoresParametrosImpresion(corte);
                //ppdImprimeCorte.ShowDialog();
                ImpresionCorteDetalle vImprimeCorte = new ImpresionCorteDetalle(base.ConvierteListaPedidosEnProductos(corte));
                vImprimeCorte.Show();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
            if (MuestraMensajeYesNo(string.Format("¿Seguro desea ELIMINAR la venta seleccionada? \n " ), "CONFIRMACIÓN") == DialogResult.Yes)
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
                                where c.Id.ToString().ToUpper().Contains(NumeroOrden.ToUpper())
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
                if(cmbAlmacenes.Items.Count>0)
                    CargaProductos(this.AlmacenId, this.TipoProductoId);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
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

        private void btnImprimeNV_Click(object sender, EventArgs e)
        {
            try
            {
                this.PedidoAgrega = ObtienePedidoFromGV(gvPedidos);
                if (this.PedidoAgrega == null)
                    MandaExcepcion("SELECCIONE UN PEDIDO");
                this.PedidoAgrega.NumOrden = "VE-" + this.PedidoAgrega.Id.ToString().PadLeft(5, '0');
                this.IVA = this.PedidoAgrega.IVA;
                this.ClienteSeleccionado = new BusClientes().ObtieneCliente(this.PedidoAgrega.ClienteId);

                string tituloImpresion = " - NOTA VENTA - ";
                if (this.PedidoAgrega.TipoPedidoId == 4)//DEVOLUCION/CORTESIA
                    tituloImpresion = "DEV/COR";

                ImprimirNotaVenta(tituloImpresion, this.ClienteSeleccionado, this.PedidoAgrega,
                                    new BusProductos().ObtieneProductosPorPedido(this.PedidoAgrega.Id));

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
    }
}
