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
    public partial class RegistrosConsignacion : FormBase
    {
        public RegistrosConsignacion()
        {
            InitializeComponent();
        }

        EntCliente ClienteSeleccionado = new EntCliente();
        List<EntCliente> ListaClientes = new List<EntCliente>();

        /// <summary>
        /// ObtieneCatalogoGenericoFromCmb(cmbAlmacenes).Id
        /// </summary>
        int AlmacenId { get { return ObtieneTrabajadorFromCmb(cmbTrabajadores).Id; } }
        int AlmacenMovilDefaultId = 18;
        #region Metodos

        List<EntPedido> ListaPedidos = new List<EntPedido>();
        public void CargaPedidosConsignacion(int TrabajadorId, DateTime FechaDesde, DateTime FechaHasta)
        {
            if (TrabajadorId <= 0)
                this.ListaPedidos = new BusPedidos().ObtienePedidosPorEstablecimiento(AlmacenMovilDefaultId, FechaDesde, FechaHasta);
            else
                this.ListaPedidos = new BusPedidos().ObtienePedidosPorEstablecimiento(AlmacenMovilDefaultId, FechaDesde, FechaHasta)
                                                                                        .Where(P => P.TrabajadorId == TrabajadorId).ToList();
            //if (!chkVerFacturasCanceladas.Checked)
            //    this.ListaPedidos = this.ListaPedidos.Where(P => !P.EstatusDescripcion.Contains("CANCELA")).ToList();

            gvPedidos.DataSource = this.ListaPedidos.OrderByDescending(P => P.Fecha).ToList();
            txtCantidadRegistros.Text = this.ListaPedidos.Count.ToString();
            txtTotalPedidos.Text = FormatoMoney(this.ListaPedidos.Sum(P => P.Total));
        }
        #endregion

        void InicializaPantalla()
        {
            cmbMesesEntradas.SelectedIndex = DateTime.Today.Month - 1;
            CargaAñosCmb(cmbAñoEntradas);
                      
            if (Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.ADMINISTRADORPUNTOVENTA
                || Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.PUNTOVENTA
                || Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.PUNTOVENTAMENUDEO)
            {
                btnEliminar.Visible = false;
                btnEliminarSinDevolución.Visible = false;
            }
            //if (Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.PUNTOVENTA)
            //{
            //    btnCancelaFactura.Visible = false;
            //    btnCancelarFacturaSinDevolucion.Visible = false;
            //    btnEliminar.Visible = false;
            //    btnEliminarSinDevolución.Visible = false;
            //}
            this.Size = new Size(base.PantallaSizeWidth, PantallaSizeHeight);
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

                //CargaAlmacenes();
                //CargaClientes();
                base.CargaTrabajadoresPorEmpresa(Program.UsuarioSeleccionado.CompañiaId, 2, cmbTrabajadores);

                //}
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void cmbTrabajadores_SelectedIndexChanged(object sender, EventArgs e)
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
                    //EntTrabajador trabajador = ObtieneTrabajadorFromCmb(cmbTrabajadores);
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
        
        private void btnRefrescarEntradas_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                EntTrabajador trabajador = ObtieneTrabajadorFromCmb(cmbTrabajadores);
                
                if (rdoPorMesVentas.Checked)
                {
                    if (cmbMesesEntradas.SelectedIndex >= 0)
                    {
                        chkVerFacturasCanceladas.Checked = false;
                        CargaPedidosConsignacion(trabajador.Id,
                                    FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                                    FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas));
                    }
                }
                else if (rdoPorFechas.Checked)
                {
                    CargaPedidosConsignacion(trabajador.Id,
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
    
        void ActualizaEstatusFacturaPedido(int FacturaId, int EstatusId)
        {
            EntFactura fac = new EntFactura()
            {
                Id = FacturaId,
                EstatusId = EstatusId
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

        }

        private void gvPedidos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedido = ObtienePedidoFromGV(gvPedidos);
                //btnFacturar.Enabled = !pedido.Facturado;
                //btnCancelaFactura.Enabled = pedido.Facturado;
                //btnComplementoPago.Visible = pedido.Facturado;
                //btnComplementoPago.Enabled = pedido.Facturado;
                btnVerComplemento.Visible = pedido.Facturado;
                btnVerComplemento.Enabled = pedido.Facturado;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnComplementoPago_Click(object sender, EventArgs e)
        {

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

        private void btnCancelarFacturaSinDevolucion_Click(object sender, EventArgs e)
        {

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

            gvPedidos.DataSource = null;
            gvPedidos.DataSource = pedidosFiltro.ToList();
            txtCantidadRegistros.Text = pedidosFiltro.Count().ToString();
            txtTotalPedidos.Text = FormatoMoney(pedidosFiltro.Sum(P => P.Total));
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

        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {

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

        private void btnCancelaSoloFactura_Click(object sender, EventArgs e)
        {

        }

        private void btnReasignaArchivos_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                EntFactura factura = new BusFacturas().ObtieneFacturasPorPedido(pedidoSeleccionado.Id).First();
                //base.ReAsignaArchivosFactura(factura);
           
                //    new BusFacturas().AgregaPDFXMLAFactura(factura.Id, factura.Ruta, factura.PDF, factura.XML, Program.UsuarioSeleccionado.Id);
                //MuestraMensaje("¡Archivos Actualizados!");
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
            finally { base.SetDefaultCursor(); }
        }

        private void btnFacturar_Click_1(object sender, EventArgs e)
        {
            try
            {
                List<EntPedido> pedidosSeleccionados = ObtieneListaPedidosFromGV(gvPedidos).Where(P => P.Estatus).ToList();
                if (pedidosSeleccionados.Count == 0)
                    MandaExcepcion("SELECCIONE AL MENOS UN PEDIDO");
                if (pedidosSeleccionados.Where(P => P.Facturado).Count() > 0)
                    MandaExcepcion("SELECCIONÓ UN PEDIDO YA FACTURADO");

                SeleccionaPedidosCliente vPedidos = new SeleccionaPedidosCliente(pedidosSeleccionados);
                if (vPedidos.ShowDialog() == DialogResult.OK)
                {
                    pedidosSeleccionados = vPedidos.PedidosSeleccionados;
                    AgregaFactura vFacturar = new AgregaFactura(vPedidos.PedidosSeleccionados,
                                                                vPedidos.ClienteSeleccionado,
                                                                //new BusClientes().ObtieneCliente(pedidosSeleccionados.First().ClienteId),
                                                                AlmacenMovilDefaultId, pedidosSeleccionados.First().TrabajadorId);
                    //AgregaFactura vFacturar = new AgregaFactura(pedidosSeleccionados,
                    //                               new BusClientes().ObtieneCliente(1497),
                    //                               AlmacenMovilDefaultId, pedidosSeleccionados.First().TrabajadorId);
                    if(vFacturar.ShowDialog()==DialogResult.OK)
                        btnRefrescarVentas.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnCancelaFactura_Click_1(object sender, EventArgs e)
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
                        base.TimbraCancelacion(pedidoSeleccionado);

                        ActualizaEstatusFacturaPedido(pedidoSeleccionado.FacturaId, 0);

                        ReingresoDeProducto(vAlmacen.AlmacenSeleccionado.Id, pedidoSeleccionado.Id, pedidoSeleccionado.Factura);

                        new BusPedidos().CancelaPedidoTodo(pedidoSeleccionado.Id);

                        btnRefrescarVentas.PerformClick();

                        MuestraMensaje("¡Factura Cancelada!", "CANCELACIÓN DE FACTURA");
                    }
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnCancelarFacturaSinDevolucion_Click_1(object sender, EventArgs e)
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

                    base.TimbraCancelacion(pedidoSeleccionado);

                    ActualizaEstatusFacturaPedido(pedidoSeleccionado.FacturaId, 0);

                    new BusPedidos().CancelaPedidoTodo(pedidoSeleccionado.Id);

                    btnRefrescarVentas.PerformClick();

                    MuestraMensaje("¡Factura Cancelada!", "CANCELACIÓN DE FACTURA");
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
        void ActualizaEstatusFacturaPedido(int FacturaId, int EstatusId, bool ActualizaPedidosFacturados = false)
        {
            EntFactura fac = new EntFactura()
            {
                Id = FacturaId,
                EstatusId = EstatusId,
                Estatus = ActualizaPedidosFacturados
            };
            new BusFacturas().ActualizaEstatusFacturaPedido(fac);
        }
        private void btnCancelaSoloFactura_Click_1(object sender, EventArgs e)
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

                    base.TimbraCancelacion(pedidoSeleccionado);

                    ActualizaEstatusFacturaPedido(pedidoSeleccionado.FacturaId, 0, true);

                    btnRefrescarVentas.PerformClick();

                    MuestraMensaje("¡Factura Cancelada!", "CANCELACIÓN DE FACTURA");
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
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

        private void btnComplementoPago_Click_1(object sender, EventArgs e)
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

        private void btnVerComplemento_Click_1(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                List<EntFactura> facturasComplementos = new BusFacturas().ObtieneComplementos(pedidoSeleccionado.FacturaId);

                SeleccionaComplemento vSelFactura = new SeleccionaComplemento(pedidoSeleccionado);
                vSelFactura.ListaFacturasComplemento = facturasComplementos;
                if (vSelFactura.ShowDialog() == DialogResult.OK)
                {
     
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
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
                    if (string.IsNullOrWhiteSpace(pedidoSeleccionado.RutaFactura))
                        pedidoSeleccionado.RutaFactura = this.CreaPathClienteDirectorioFacturas(this.PathFacturas, "APP", facturasEncontradas[0].SerieFactura
                                                                + facturasEncontradas[0].NumeroFactura);
                    MuestraArchivo(pedidoSeleccionado.RutaFactura);
                }

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
    }
}
