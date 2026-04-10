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
    public partial class RegistrosNeue: FormBase
    {
        public RegistrosNeue()
        {
            InitializeComponent();
        }

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

                    btnRefrescarVentas.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        EntCliente ClienteSeleccionado = new EntCliente();
        //List<EntCliente> ListaClientes = new List<EntCliente>();

        /// <summary>
        /// ObtieneCatalogoGenericoFromCmb(cmbAlmacenes).Id
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
            //List<EntCatalogoGenerico> almacenes = new BusEmpresas().ObtieneAlmacenes(1, Program.UsuarioSeleccionado.Id);
            List<EntCatalogoGenerico> almacenes = new BusEmpresas().ObtieneAlmacenes(Program.UsuarioSeleccionado.CompañiaId, Program.UsuarioSeleccionado.Id);
            cmbAlmacenes.DataSource = almacenes;
            cmbAlmacenes.SelectedIndex = 0;
        }
        
        public void CargaClientes()
        {
            //this.ListaClientes = new BusClientes().ObtieneClientes(Program.EmpresaSeleccionada.Id);
            //gvClientes.DataSource = this.ListaClientes;
            this.ClienteSeleccionado = null;
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
        void ImprimirSalida(EntCliente Cliente, EntPedido PedidoCotizacion, List<EntProducto> ProductosSeleccionados)
        {
            string rutaGuardaArchivosOrden = base.RutaImpresion + "\\" + Cliente.Nombre + "\\" + PedidoCotizacion.NumOrden;
            VerificaRutas(base.RutaImpresion, Cliente.Nombre + "\\" + PedidoCotizacion.NumOrden);
            CreaImagenBMPsalida(rutaGuardaArchivosOrden, pbImpresionFondoBlanco.Image, pbLogo.Image, pbLeyendaCotizacion.Image, pbImpresionFondoBlanco.Image, PedidoCotizacion.NumOrden,
                            Program.EmpresaSeleccionada, Cliente, PedidoCotizacion, ProductosSeleccionados);

            try
            {
                new UtiPDF().CreaPDF(rutaGuardaArchivosOrden + "\\" + PedidoCotizacion.NumOrden + ".bmp",
                                        rutaGuardaArchivosOrden + "\\SALIDA-" + PedidoCotizacion.NumOrden + ".pdf");
            }
            catch (Exception ex) { }
            finally
            {
                string nombreArchivo = EncuentraArchivo(rutaGuardaArchivosOrden, ".pdf");
                MuestraArchivo(rutaGuardaArchivosOrden, nombreArchivo);
            }
        }
        #endregion

        void InicializaPantalla()
        {
            if (Program.EmpresaSeleccionada != null && cmbEmpresas.Items.Count > 0)
                cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);

            cmbMesesEntradas.SelectedIndex = DateTime.Today.Month - 1;
            CargaAñosCmb(cmbAñoEntradas);
                      
            if (Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.ADMINISTRADORPUNTOVENTA
             || Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.PUNTOVENTAMENUDEO)
            {
                btnCancelaFactura.Visible = false;
                btnCancelarFacturaSinDevolucion.Visible = false;
                btnCancelaSoloFactura.Visible = false;
                btnCancelarPorUUID.Visible = false;
            }
            else if (Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.PUNTOVENTA)//DINA
            {
                btnEliminar.Visible = false;
                btnEliminarSinDevolución.Visible = false;
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

                this.ClienteSeleccionado = null;

                CargaAlmacenes();
                //CargaClientes();
                //}
                if ((TipoUsuario)Program.UsuarioSeleccionado.TipoUsuarioId ==
                                               TipoUsuario.PREVENTA)//PREVENTA-HILLO
                {
                    btnFacturar.Visible = false;
                    btnEliminar.Visible = false;
                    btnEliminarSinDevolución.Visible = false;
                    btnReasignaArchivos.Visible = false;
                    btnVerComplemento.Visible = false;
                    btnComplementoPago.Visible = false;
                    btnCancelaFactura.Visible = false;
                    btnCancelarFacturaSinDevolucion.Visible = false;
                    btnCancelarPorUUID.Visible = false;
                    btnCancelaSoloFactura.Visible = false;
                    SeleccionarIndexComboBox(cmbAlmacenes, Program.UsuarioSeleccionado.AlmacenMayoristaId);
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
                btnRefrescarVentas.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
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
            if (chkVerDevoluciones.Checked)
                this.ListaPedidos.AddRange(new BusPedidos().ObtienePedidosPorEstablecimiento(EstablecimientoId, FechaDesde, FechaHasta,
                                                                                      (int)TipoPedido.DEVOLUCIONCORTESIA));
            if (chkVerCortesias.Checked)
                this.ListaPedidos.AddRange(new BusPedidos().ObtienePedidosPorEstablecimiento(EstablecimientoId, FechaDesde, FechaHasta,
                                                                                      (int)TipoPedido.CORTESIA));

            if (!chkVerFacturasCanceladas.Checked)
                this.ListaPedidos = this.ListaPedidos.Where(P => !P.EstatusDescripcion.Contains("CANCELA")).ToList();

            gvPedidos.DataSource = this.ListaPedidos.OrderByDescending(P => P.FacturaId).ToList();
        }
        private void btnRefrescarEntradas_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                EntCatalogoGenerico establecimiento = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);
                
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

        #endregion

        int IndexFormaPago = 0;
        
        private void btnVerFactura_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (!pedidoSeleccionado.Facturado)
                    MandaExcepcion("Pedido Sin Facturar");

                List<EntFactura> facturasEncontradas = new BusFacturas().ObtieneFacturasPorPedido(pedidoSeleccionado.Id)
                                                                    .Where(P => P.SerieFactura + P.NumeroFactura == pedidoSeleccionado.Factura).ToList(); ;
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
            //List<EntCliente> clientes = this.ListaClientes.Where(P => P.Id == ClienteId).ToList();
            EntCliente cliente = new BusClientes().ObtieneCliente(ClienteId);

            if (cliente.Id == 0)
                throw new Exception("Cliente NO encontrado");

            return cliente;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FacturaId"></param>
        /// <param name="EstatusId"></param>
        /// <param name="ActualizaPedidosFacturados">True: Cuando se quiere dejar los Pedidos listos para volver a Facturar.</param>
        void ActualizaEstatusFacturaPedido(int FacturaId, int EstatusId, bool ActualizaPedidosFacturados=false)
        {
            EntFactura fac = new EntFactura()
            {
                Id = FacturaId,
                EstatusId = EstatusId,
                Estatus=ActualizaPedidosFacturados
            };
            new BusFacturas().ActualizaEstatusFacturaPedido(fac);
        }

        public void ReingresoDeProducto(int AlmacenId, int PedidoId,string NumeroFactura)
        {
            int orientacion = 1;
            int movimientoId = new BusPedidos().AgregaMovimientoMaster("REINGRESO POR CANCELACIÓN DE FACTURA. FACTURA: "+NumeroFactura,
                (int)TipoMovimiento.CANCELACION, orientacion, AlmacenId, 0, Program.UsuarioSeleccionado.Id);

            List<EntProducto> productosSeleccionados = new BusProductos().ObtieneProductosPorPedido(PedidoId);
            Productos vProd = new Productos();
            foreach (EntProducto p in productosSeleccionados)
            {
                new BusPedidos().AgregaMovimientoDetalle(movimientoId, p.Id, p.Cantidad, p.PrecioC);
                vProd.AumentaProducto(p.Id, p.Cantidad);
            }

            new BusPedidos().AgregaMovimientoLote(movimientoId, orientacion);
        }
                
        private void btnCancelaFactura_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (MuestraMensajeYesNo(string.Format("¿Seguro desea CANCELAR la Factura seleccionada? \n " +
                                         "UUID: {0} \n\n " +
                    "Deberá Seleccionar Almacen a donde se Reingresará el Producto incluido en la Venta.", pedidoSeleccionado.UUID), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    base.SetWaitCursor();
                    SeleccionaAlmacen vAlmacen = new SeleccionaAlmacen();
                    if (vAlmacen.ShowDialog() == DialogResult.OK)
                    {
                        //AgregaMotivoCancelacion vMotCan = new AgregaMotivoCancelacion(new EntFactura() { NumeroFactura = "" }, true);
                        //if (vMotCan.ShowDialog() == DialogResult.OK)
                        //{
                        //    string motivoCancelacion = vMotCan.MotivoCancelacionId;
                        //    string folioSustituye = vMotCan.FolioSustituye;
                        
                            base.TimbraCancelacion(pedidoSeleccionado);

                            ActualizaEstatusFacturaPedido(pedidoSeleccionado.FacturaId, 0);

                            ReingresoDeProducto(vAlmacen.AlmacenSeleccionado.Id, pedidoSeleccionado.Id, pedidoSeleccionado.Factura);

                            new BusPedidos().CancelaPedidoTodo(pedidoSeleccionado.Id);

                            btnRefrescarVentas.PerformClick();

                            MuestraMensaje("¡Factura Cancelada!", "CANCELACIÓN DE FACTURA");
                        //}
                    }
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnCancelarFacturaSinDevolucion_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (MuestraMensajeYesNo(string.Format("¿Seguro desea CANCELAR la Factura seleccionada? \n " +
                                         "*SE CANCELA FACTURA Y SE ELIMINAN LAS NOTAS DE VENTA*\n " +
                                         "*NO SE DEVUELVE EL PRODUCTO A ALMACEN*\n\n " +
                                         "UUID: {0} \n\n ", pedidoSeleccionado.UUID), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    base.SetWaitCursor();
                    //SeleccionaAlmacen vAlmacen = new SeleccionaAlmacen();
                    //if (vAlmacen.ShowDialog() == DialogResult.OK)
                    //{

                    base.TimbraCancelacion(pedidoSeleccionado);

                    ActualizaEstatusFacturaPedido(pedidoSeleccionado.FacturaId, 0);

                    //ReingresoDeProducto(vAlmacen.AlmacenSeleccionado.Id, pedidoSeleccionado.Id, pedidoSeleccionado.Factura);

                    new BusPedidos().CancelaPedidoTodo(pedidoSeleccionado.Id);

                    btnRefrescarVentas.PerformClick();

                    MuestraMensaje("¡Factura Cancelada!", "CANCELACIÓN DE FACTURA");
                    //}
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnCancelaSoloFactura_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (MuestraMensajeYesNo(string.Format("¿Seguro desea CANCELAR la Factura seleccionada? \n\n " +
                                         "*SE CANCELA FACTURA*\n" +
                                         "*LAS NOTAS DE VENTA QUEDAN VIGENTES*\n\n " +
                                         "UUID: {0} \n\n ", pedidoSeleccionado.UUID), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    base.SetWaitCursor();
                    //SeleccionaAlmacen vAlmacen = new SeleccionaAlmacen();
                    //if (vAlmacen.ShowDialog() == DialogResult.OK)
                    //{

                    base.TimbraCancelacion(pedidoSeleccionado);

                    ActualizaEstatusFacturaPedido(pedidoSeleccionado.FacturaId, 0, true);

                    //ReingresoDeProducto(vAlmacen.AlmacenSeleccionado.Id, pedidoSeleccionado.Id, pedidoSeleccionado.Factura);
                    //new BusPedidos().CancelaPedidoTodo(pedidoSeleccionado.Id);

                    btnRefrescarVentas.PerformClick();

                    MuestraMensaje("¡Factura Cancelada!", "CANCELACIÓN DE FACTURA");
                    //}
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
                    SalidasVentas vVent = new SalidasVentas();
                    bool pagoAgregado = false;

                    List<EntPedido> pedidosfacturasSeleccionadas = vSelFac.FacturasPedidoSeleccionados;
                    foreach (EntPedido pf in pedidosfacturasSeleccionadas)
                    {
                        //SOLO AGREGA EL PAGO QUE DEBE TENER CADA FACTURA
                        AgregaPago vAgregaPago = new AgregaPago(pf, false);//pedidosDeudaCliente);
                        if (vAgregaPago.ShowDialog() == DialogResult.OK)
                        {
                            decimal cantidadPaga = ConvierteTextoADecimal(vAgregaPago.CantidadPago);
                            //SE USA INSTEAD OF PAGO PARA NO ALTERAR DEBE
                            pf.PagoTotal = cantidadPaga;
                            //pf.FechaPago = vAgregaPago.FechaPago;
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

                        //DescuentaTimbre(Program.EmpresaSeleccionada);


                        foreach (EntPedido pff in pedidosfacturasSeleccionadas)
                        {
                            List<EntPedido> pedidosFactura = new BusPedidos().ObtienePedidosPorFactura(pff.FacturaId);
                            foreach (EntPedido p in pedidosFactura)
                            {
                                int pagoId = 0;
                                pagoAgregado = true;

                                decimal porcentajePagadoFac = pff.PagoTotal / pff.Debe;
                                decimal pagoPedido = Math.Round(p.Debe * porcentajePagadoFac, 2);
                                try
                                {
                                    pagoId = vVent.AgregarPagoPedidoBD(this.AlmacenId, p.Id, pagoPedido, vComplePago.FormaPagoId, vComplePago.FormaPago, vComplePago.FechaPago);
                                }
                                catch (Exception ex)
                                {
                                    MuestraMensajeError("NO SE LOGRO GUARDAR TODOS LOS PAGOS ASIGNADOS A LAS FACTURAS \n\n " +
                                                        "COMUNIQUESE CON PERSONAL DE SISTEMAS ANTES DE CERRAR ESTE MENSAJE.\n\n\n"
                                                        + ex.Message,
                                                        "ERROR AL AGREGAR PAGO");
                                }
                                try
                                {
                                    vComplePago.AgregarComplementoPago(p.FacturaId, pagoId, DateTime.Today, pagoPedido,
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
                                    MuestraMensajeError("NO SE LOGRO RELACIONAR TODAS LAS FACTURAS AL COMPLEMNTO \n\n" +
                                                        "COMUNIQUESE CON PERSONAL DE SISTEMAS ANTES DE CERRAR ESTE MENSAJE.\n\n\n"
                                                        + ex.Message,
                                                        "ERROR - COMPLEMENTO SI FUE TIMBRADO");
                                }
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (MuestraMensajeYesNo(string.Format("¿Seguro desea ELIMINAR la venta seleccionada? \n \n" +
                    "Deberá Seleccionar Almacen a donde se Reingresará el Producto incluido en la Venta."), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    base.SetWaitCursor();

                    SeleccionaAlmacen vAlmacen = new SeleccionaAlmacen();
                    if (vAlmacen.ShowDialog() == DialogResult.OK)
                    {
                        ReingresoDeProducto(vAlmacen.AlmacenSeleccionado.Id, pedidoSeleccionado.Id, pedidoSeleccionado.Factura);

                        new BusPedidos().CancelaPedidoTodo(pedidoSeleccionado.Id);

                        btnRefrescarVentas.PerformClick();

                        MuestraMensaje("¡Venta Eliminada!", "ELIMINACIÓN VENTA");
                    }
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnEliminarSinDevolución_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (MuestraMensajeYesNo(string.Format("¿Seguro desea ELIMINAR la venta seleccionada? \n \n "), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    base.SetWaitCursor();

                    //SeleccionaAlmacen vAlmacen = new SeleccionaAlmacen();
                    //if (vAlmacen.ShowDialog() == DialogResult.OK)
                    //{
                        //ReingresoDeProducto(vAlmacen.AlmacenSeleccionado.Id, pedidoSeleccionado.Id, pedidoSeleccionado.Factura);

                        new BusPedidos().CancelaPedidoTodo(pedidoSeleccionado.Id);

                        btnRefrescarVentas.PerformClick();

                        MuestraMensaje("¡Venta Eliminada!", "ELIMINACIÓN VENTA");
                    //}
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
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
                                    //where c.NumOrden.ToUpper().Contains(NumeroOrden.ToUpper())
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

            var pedidosVisibles = pedidosFiltro.ToList();
            gvPedidos.DataSource = null;
            gvPedidos.DataSource = pedidosVisibles;
            ActualizaTotales(pedidosVisibles);
        }

        private void ActualizaTotales(List<EntPedido> pedidosVisibles)
        {
            txtNumRegistros.Text = pedidosVisibles.Count.ToString();
            txtTotalPedidos.Text = FormatoMoney(pedidosVisibles.Sum(P => P.Total));
            txtTotalFacturado.Text = FormatoMoney(pedidosVisibles
                .Where(P => P.Facturado && !P.EstatusDescripcion.Contains("CANCELA"))
                .Sum(P => P.Total));
            txtTotalSinFacturar.Text = FormatoMoney(pedidosVisibles
                .Where(P => !P.Facturado && !P.EstatusDescripcion.Contains("CANCELA"))
                .Sum(P => P.Total));
            txtTotalCancelado.Text = FormatoMoney(pedidosVisibles
                .Where(P => P.EstatusDescripcion.Contains("CANCELA"))
                .Sum(P => P.Total));
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

        private void btnVerComplemento_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                //List<EntFactura> facturasComplementos = new BusFacturas().ObtieneComplementos(pedidoSeleccionado.FacturaId, pedidoSeleccionado.Factura, pedidoSeleccionado.Total);
                List<EntFactura> facturasComplementos = new BusFacturas().ObtieneComplementos(pedidoSeleccionado.FacturaId);

                SeleccionaComplemento vSelFactura = new SeleccionaComplemento(pedidoSeleccionado);
                vSelFactura.ListaFacturasComplemento = facturasComplementos;
                if (vSelFactura.ShowDialog() == DialogResult.OK)
                {
                    //vSelFactura.FacturaComplementoSeleccionada.Nombre = pedidoSeleccionado.Cliente;
                    //if (base.VerificaReAsignarFactura(vSelFactura.FacturaComplementoSeleccionada))
                    //{
                    //    base.VerificaExistenArchivosFactura(vSelFactura.FacturaComplementoSeleccionada.Ruta,
                    //                                   "CP" + vSelFactura.FacturaComplementoSeleccionada.NumeroComplemento+" FAC-"+ vSelFactura.FacturaComplementoSeleccionada.NumeroFactura,
                    //                                   vSelFactura.FacturaComplementoSeleccionada.PDF,
                    //                                   vSelFactura.FacturaComplementoSeleccionada.XML);
                    //    base.MuestraArchivo(vSelFactura.FacturaComplementoSeleccionada.Ruta, EncuentraArchivo(vSelFactura.FacturaComplementoSeleccionada.Ruta, ".pdf"));
                    //}

                    ////VerificaExistenArchivosFactura(vSelFactura.FacturaComplementoSeleccionada.Ruta,
                    ////                                   //facturasEncontradas[0].SerieFactura + facturasEncontradas[0].NumeroFactura,
                    ////                                   vSelFactura.FacturaComplementoSeleccionada.SerieFactura
                    ////                                               + vSelFactura.FacturaComplementoSeleccionada.NumeroFactura,
                    ////                                   vSelFactura.FacturaComplementoSeleccionada.PDF,
                    ////                                   vSelFactura.FacturaComplementoSeleccionada.XML);

                    ////MuestraArchivo(pedidoSeleccionado.RutaFactura);
                    ////MuestraArchivo(vSelFactura.FacturaComplementoSeleccionada.Ruta);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            try
            {
                //EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                List<EntPedido> pedidosSeleccionados = ObtieneListaPedidosFromGV(gvPedidos).Where(P=>P.Estatus).ToList();
                if(pedidosSeleccionados.Count==0)
                    MandaExcepcion("SELECCIONE AL MENOS UN PEDIDO");
                //if (pedidoSeleccionado.Facturado)
                //    MandaExcepcion("PEDIDO YA FACTURADO");
                if (pedidosSeleccionados.Where(P => P.Facturado).Count() > 0)
                    MandaExcepcion("SELECCIONÓ UN PEDIDO YA FACTURADO");

                //AgregaFactura vFacturar = new AgregaFactura(pedidoSeleccionado,
                //                                           new BusClientes().ObtieneCliente(pedidoSeleccionado.ClienteId), this.AlmacenId);
                AgregaFactura vFacturar = new AgregaFactura(pedidosSeleccionados,
                                                           new BusClientes().ObtieneCliente(pedidosSeleccionados.First().ClienteId), this.AlmacenId);
                vFacturar.ShowDialog();

                btnRefrescarVentas.PerformClick();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnCancelarPorUUID_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                string uuid = "";
                AgregaUUID vAgregaUUID = new AgregaUUID();
                if (vAgregaUUID.ShowDialog() == DialogResult.OK)
                {
                    uuid = vAgregaUUID.Observacion;
                    if (MuestraMensajeYesNo(string.Format("¿Seguro desea CANCELAR la Factura seleccionada? \n " +
                                             "UUID: {0} \n\n ", uuid), "CONFIRMACIÓN") == DialogResult.Yes)
                    {
                        base.SetWaitCursor();

                        base.TimbraCancelacion(new EntPedido() { UUID=uuid, ClienteId=pedidoSeleccionado.ClienteId, ClienteRFC=pedidoSeleccionado.ClienteRFC, Total=pedidoSeleccionado.Total});

                        btnRefrescarVentas.PerformClick();

                        MuestraMensaje("¡Factura Cancelada!", "CANCELACIÓN DE FACTURA");
                    }
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnReasignaArchivos_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                EntFactura factura = new BusFacturas().ObtieneFacturasPorPedido(pedidoSeleccionado.Id).First();
                base.ReAsignaArchivosFactura(factura);

                new BusFacturas().AgregaPDFXMLAFactura(factura.Id, factura.Ruta, factura.PDF, factura.XML, Program.UsuarioSeleccionado.Id);
                MuestraMensaje("¡Archivos Actualizados!");
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
            finally { base.SetDefaultCursor(); }
        }

        private void chkVerDevoluciones_CheckedChanged(object sender, EventArgs e)
        {
            btnRefrescarVentas.PerformClick();
        }
        void FiltrarFechasDeInteres()
        {
            int mesesFiltro = 1;
            DateTime fechaHasta = DateTime.Today;
            bool noFiltrar=false;
            if (chkUltimoMes.Checked)
            {
                mesesFiltro = 1;
                chkUltimoTrimestre.Checked = false;
                chkUltimoAño.Checked = false;
            }
            else
                if (chkUltimoTrimestre.Checked)
            {
                mesesFiltro = 3;
                chkUltimoMes.Checked = false;
                chkUltimoAño.Checked = false;
            }
            else
                if (chkUltimoAño.Checked)
            {
                mesesFiltro = 12;
                chkUltimoMes.Checked = false;
                chkUltimoTrimestre.Checked = false;
            }
            else
                noFiltrar = true;

            if (noFiltrar)
            {
                if (!chkApartirDeHoy.Checked)
                    fechaHasta = ObtieneFechaUltimoDiaMes(DateTime.Today.Month, DateTime.Today.Year);

                dtpFechaDesdeVentas.Value = fechaHasta.AddMonths(-mesesFiltro);
                dtpFechaHastaVentas.Value = fechaHasta;
                btnRefrescarVentas.PerformClick();
            }
        }
        private void chkUltimoMes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((CheckBox)sender).Checked)
                {
                    chkUltimoTrimestre.Checked = false;
                    chkUltimoAño.Checked = false;
                    base.FiltrarFechasDeInteres(chkUltimoMes, chkUltimoTrimestre, chkUltimoAño,
                                                ref dtpFechaDesdeVentas, ref dtpFechaHastaVentas, chkApartirDeHoy.Checked,
                                                ref btnRefrescarVentas);
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
            finally { base.SetDefaultCursor(); }
        }

        private void chkUltimoTrimestre_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((CheckBox)sender).Checked)
                {
                    chkUltimoMes.Checked = false;
                    chkUltimoAño.Checked = false;
                    base.FiltrarFechasDeInteres(chkUltimoMes, chkUltimoTrimestre, chkUltimoAño,
                                                ref dtpFechaDesdeVentas, ref dtpFechaHastaVentas, chkApartirDeHoy.Checked,
                                                ref btnRefrescarVentas);
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
            finally { base.SetDefaultCursor(); }
        }

        private void chkUltimoAño_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((CheckBox)sender).Checked)
                {
                    chkUltimoMes.Checked = false;
                    chkUltimoTrimestre.Checked = false;
                    base.FiltrarFechasDeInteres(chkUltimoMes, chkUltimoTrimestre, chkUltimoAño,
                                                ref dtpFechaDesdeVentas, ref dtpFechaHastaVentas, chkApartirDeHoy.Checked,
                                                ref btnRefrescarVentas);
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
            finally { base.SetDefaultCursor(); }
        }

        private void chkApartirDeHoy_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                base.FiltrarFechasDeInteres(chkUltimoMes, chkUltimoTrimestre, chkUltimoAño,
                                            ref dtpFechaDesdeVentas, ref dtpFechaHastaVentas, chkApartirDeHoy.Checked,
                                            ref btnRefrescarVentas);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
            finally { base.SetDefaultCursor(); }
        }
    }
}
