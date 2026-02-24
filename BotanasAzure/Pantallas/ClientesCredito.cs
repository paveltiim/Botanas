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
    public partial class ClientesCredito : FormBase
    {
        public ClientesCredito()
        {
            InitializeComponent();
        }

        List<EntPedido> ListaPedidos;
        List<EntCliente> ListaClientes;
        /// <summary>
        /// return ObtieneClienteFromGV(gvClientesCredito).
        /// </summary>

        EntCliente ClienteSeleccionado { get { return ObtieneClienteFromGV(gvClientesCredito); } }
        EntPedido PedidoDetalleSeleccionado { get { return ObtienePedidoFromGV(gvPedidosClientesCreditoDetalle); } }
        /// <summary>
        /// ObtieneCatalogoGenericoFromCmb(cmbAlmacenes).Id;
        /// </summary>
        int AlmacenId { get { return ObtieneCatalogoGenericoFromCmb(cmbAlmacenes).Id; } }

        public void CargaGvPedidosClientesDeuda(int EstablecimientoId)
        {
            this.ListaPedidos = new BusPedidos().ObtienePedidosClientesDeuda(EstablecimientoId).Where(P => (P.Debe + P.DebeFacturas) > 0).ToList();
            gvPedidosClientesCreditoDetalle.DataSource = this.ListaPedidos;
            MuestraTotales();
        }
        public void CargaGvClientesDeuda(int EstablecimientoId)
        {
            //this.ListaClientes = new BusClientes().ObtieneClientesDeuda(EstablecimientoId)
            //                                                        .Where(P => (P.Debe + P.DebeFacturas) > 0).ToList();
            this.ListaClientes = new BusClientes().ObtieneClientesDeudaUltimoPago(EstablecimientoId)
                                                                    .Where(P => (P.Debe + P.DebeFacturas) > 0).ToList();
            gvClientesCredito.DataSource = this.ListaClientes;
            MuestraTotales();
        }

        void MuestraTotales()
        {
            List<EntCliente> listaClientesFiltrados = ObtieneListaClientesFromGV(gvClientesCredito);
            if (listaClientesFiltrados != null)
            {
                txtTotalTotal.Text = FormatoMoney(listaClientesFiltrados.Sum(P => P.Total));
                txtPagoTotal.Text = FormatoMoney(listaClientesFiltrados.Sum(P => P.Pago));
                txtNCtotal.Text = FormatoMoney(listaClientesFiltrados.Sum(P => P.NotasCredito));
                txtDebeTotal.Text = FormatoMoney(listaClientesFiltrados.Sum(P => P.Debe));
            }

            List<EntPedido> listaPedidosFiltrados= ObtieneListaPedidosFromGV(gvPedidosClientesCreditoDetalle);
            if (listaPedidosFiltrados != null)
            {
                txtDebeTotalFacturas.Text = FormatoMoney(listaPedidosFiltrados.Sum(P => P.Debe));
                txtDebeTotalFacturasVentasMovil.Text = FormatoMoney(listaPedidosFiltrados.Sum(P => P.DebeFacturas));
                txtDebeTotalFacturasTodas.Text = FormatoMoney(ConvierteTextoADecimal(txtDebeTotalFacturas)+ ConvierteTextoADecimal(txtDebeTotalFacturasVentasMovil));
            }
        }
        
        void CargaAlmacenes()
        {
            List<EntCatalogoGenerico> almacenes = new BusEmpresas().ObtieneAlmacenes(Program.UsuarioSeleccionado.CompañiaId, Program.UsuarioSeleccionado.Id);
            cmbAlmacenes.DataSource = almacenes;
            cmbAlmacenes.SelectedIndex = 0;
        }
        void InicializaPantalla()
        {
            gvPedidosClientesCreditoDetalle.DataSource = null;
            gvClientesCredito.DataSource = null;

            btnVerCP.Enabled = true;

            dtpFechaDesdeCP.Value = ObtieneFechaPrimerDiaMes(DateTime.Today.Month, DateTime.Today.Year);

            if (Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.GERENTEVENTAS
             || Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.CYCVISUALIZA)
            {
                flpBotonesRight.Visible = false;
                btnPagarDetalle.Visible = false;
                tcPedidosGrids.TabPages.Remove(tpRegistrosComplementosPago);
            }
        }


        private void ClientesCredito_Load(object sender, EventArgs e)
        {
            try
            {
                InicializaPantalla();
                CargaAlmacenes();

                //CargaEmpresas();

                //if (Program.EmpresaSeleccionada == null)
                //    Program.EmpresaSeleccionada = SeleccionaEmpresa();

                //cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        void RevisaFacturasMismoEmisor(List<EntPedido> FacturasPedidoRevisa)
        {
            int empresaEmisorId = FacturasPedidoRevisa.First().EmpresaId;
            foreach(EntPedido p in FacturasPedidoRevisa)
            {
                if (empresaEmisorId != p.EmpresaId)
                    MandaExcepcion("NO SE PUEDE HACER UN COMPLEMENTO PARA FACTURAS REALIZADAS POR DISTINTOS EMISORES");
            }
        }
        private void btnPagar_Click(object sender, EventArgs e)
        {
            try
            {
                //List<EntPedido> pedidosDeudaCliente = new BusPedidos().ObtienePedidosClientesDeuda().Where(P => P.ClienteRFC == this.ClienteSeleccionado.RFC)
                //                                                        .OrderBy(P => P.Fecha).ToList();
                //this.ListaPedidos.Where(P => P.ClienteRFC == this.ClienteSeleccionado.RFC)
                //                                                    .OrderBy(P => P.Fecha).ToList();
                List<EntPedido> pedidosDeudaCliente = new BusPedidos().ObtienePedidosClientesDeuda(this.ClienteSeleccionado.RFC)
                                                                        .OrderBy(P => P.Fecha).ToList();

                SeleccionaFacturas vSelFac = new SeleccionaFacturas(pedidosDeudaCliente);
                if (vSelFac.ShowDialog() == DialogResult.OK)
                {
                    SalidasVentas vVent = new SalidasVentas();
                    bool pagoAgregado = false;
                    if (this.ClienteSeleccionado.RFC != "XAXX010101000")
                    {
                        RevisaFacturasMismoEmisor(vSelFac.FacturasPedidoSeleccionados);
                        Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(vSelFac.FacturasPedidoSeleccionados.First().EmpresaId);

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

                        AgregaComplementoPago vComplePago = new AgregaComplementoPago(
                                                        ConvierteListaPedidosEnFacturas(pedidosfacturasSeleccionadas),
                                                        this.ClienteSeleccionado,
                                                        pedidosfacturasSeleccionadas.Sum(P => P.Debe),
                                                        pedidosfacturasSeleccionadas.Sum(P => P.PagoTotal));

                        if (vComplePago.ShowDialog() == DialogResult.OK)
                        {
                            base.SetWaitCursor();

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
                                        pagoId = vVent.AgregarPagoPedidoBD(this.AlmacenId, p.Id, pagoPedido, 
                                                                        vComplePago.FormaPagoId, vComplePago.FormaPago, vComplePago.FechaPago);
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
                        }
                    }
                    else
                    {
                        MuestraMensaje("NO SE GENERARÁ COMPLEMENTO DE PAGO POR SER RFC GENÉRICO: XAXX010101000");
                        List<EntPedido> pedidosfacturasSeleccionadas = vSelFac.FacturasPedidoSeleccionados;
                        AgregaPago vAgregaPago=new AgregaPago(pedidosfacturasSeleccionadas.First());
                        foreach (EntPedido pf in pedidosfacturasSeleccionadas)
                        {
                            //SOLO AGREGA EL PAGO QUE DEBE TENER CADA FACTURA
                            vAgregaPago = new AgregaPago(pf, true);//pedidosDeudaCliente);
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

                        foreach (EntPedido p in pedidosfacturasSeleccionadas)
                        {
                            int pagoId = 0;
                            pagoAgregado = true;
                            try
                            {
                                pagoId = vVent.AgregarPagoPedidoBD(this.AlmacenId, p.Id, p.PagoTotal, vAgregaPago.FormaPagoId, vAgregaPago.FormaPago, 
                                                                                                        vAgregaPago.FechaPago);
                            }
                            catch (Exception ex)
                            {
                                MuestraMensajeError("NO SE LOGRO GUARDAR TODOS LOS PAGOS ASIGNADOS A LAS FACTURAS \n\n " +
                                                    "COMUNIQUESE CON PERSONAL DE SISTEMAS ANTES DE CERRAR ESTE MENSAJE.\n\n\n"
                                                    + ex.Message,
                                                    "ERROR AL AGREGAR PAGO");
                            }
                        }
                    }
                    if (pagoAgregado)
                        MuestraMensaje("¡Pago Agregado!", "CONFIRMACIÓN PAGO");
                    else
                        MuestraMensajeError("Error al agregar el Pago", "ERROR PAGO-COMPLEMENTO");

                    btnRefrescarClientesCredito.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }finally { base.SetDefaultCursor(); }
        }

        private void btnPagarDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = this.PedidoDetalleSeleccionado;

                if(MuestraMensajeYesNo("¿Desea agregar Pago sin COMPLEMENTO DE PAGO?") == DialogResult.Yes) { 
                    AgregaPago vAgregaPago = new AgregaPago(pedidoSeleccionado);
                    if (vAgregaPago.ShowDialog() == DialogResult.OK)
                    {
                        decimal cantidadPaga = ConvierteTextoADecimal(vAgregaPago.CantidadPago);

                        if (cantidadPaga == 0)
                            throw new Exception("La cantidad a Pagar debe ser mayor a 0.");
                        if (cantidadPaga > pedidoSeleccionado.Debe)
                            throw new Exception("La cantidad a Pagar debe ser menor o igual a la cantidad que Debe el cliente.");
                        if (vAgregaPago.FechaPago < pedidoSeleccionado.Fecha)
                            MandaExcepcion("La Fecha de Pago no puede ser inferior a la Fecha de Factura");

                        //List<EntPedido> pedidosDeudaCliente = ListaPedidos.Where(P => P.ClienteId == pedidoSeleccionado.Id).OrderBy(P => P.Fecha).ToList();

                        decimal cantidadAgregaPago = cantidadPaga;

                        //AumentaPagoPedido(pedidoSeleccionado.Id, cantidadAgregaPago);
                        SalidasVentas vVent = new SalidasVentas();
                        
                        int pagoId = vVent.AgregarPagoPedidoBD(this.AlmacenId, pedidoSeleccionado.Id, cantidadAgregaPago, vAgregaPago.FormaPagoId,         
                                                            vAgregaPago.FormaPago, vAgregaPago.FechaPago);

                        btnRefrescarClientesCredito.PerformClick();

                        MuestraMensaje("¡Pago Agregado!", "CONFIRMACIÓN PAGO");
                    }

                    //tcPedidosGrids.SelectedIndex = 0;
                    //btnRefrescarClientesCredito.PerformClick();
                    btnRefrescarPedidosClientesDeuda.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
            finally { base.SetDefaultCursor(); }
        }

        private void btnVerPagosCliente_Click(object sender, EventArgs e)
        {
            try
            {
                //EntCliente clienteSeleccionado = ObtieneClienteFromGV(gvClientesCredito);
                //MuestraPagos vPagos = new MuestraPagos(clienteSeleccionado.Id);
                //vPagos.ShowDialog();

                ////CargaGvClientesCredito();
                //

                //List<EntPedido> pedidosDeudaCliente = ListaPedidos.Where(P => P.ClienteId == ClienteSeleccionado.Id)
                //                                                    .OrderBy(P => P.Fecha).ToList();

                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidosClientesCreditoDetalle);
                List<EntFactura> facturasComplementos = new BusFacturas().ObtieneComplementos(pedidoSeleccionado.FacturaId, pedidoSeleccionado.Factura, pedidoSeleccionado.Total);
                //List<EntFactura> facturasComplementos = new BusFacturas().ObtieneFacturaConPagos(pedidoSeleccionado.FacturaId);

                SeleccionaComplemento vSelFactura = new SeleccionaComplemento(pedidoSeleccionado);
                vSelFactura.FacturaId = pedidoSeleccionado.FacturaId;
                vSelFactura.ListaFacturasComplemento = facturasComplementos;
                if (vSelFactura.ShowDialog() == DialogResult.OK)
                {
                    vSelFactura.FacturaComplementoSeleccionada.Nombre = pedidoSeleccionado.Cliente;
                    if (base.VerificaReAsignarFactura(vSelFactura.FacturaComplementoSeleccionada))
                    {
                        base.VerificaExistenArchivosFactura(vSelFactura.FacturaComplementoSeleccionada.Ruta,
                                                       "CP" + vSelFactura.FacturaComplementoSeleccionada.NumeroComplemento + " FAC-" + vSelFactura.FacturaComplementoSeleccionada.NumeroFactura,
                                                       vSelFactura.FacturaComplementoSeleccionada.PDF,
                                                       vSelFactura.FacturaComplementoSeleccionada.XML);
                        base.MuestraArchivo(vSelFactura.FacturaComplementoSeleccionada.Ruta, EncuentraArchivo(vSelFactura.FacturaComplementoSeleccionada.Ruta, ".pdf"));
                    }
                    //MuestraArchivo(vSelFactura.FacturaComplementoSeleccionada.Ruta);
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnRefrescarClientesCredito_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                EntCatalogoGenerico establecimiento = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);

                CargaGvClientesDeuda(establecimiento.Id);
                CargaGvPedidosClientesDeuda(establecimiento.Id);
                //CargaGvPagosClientes();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
            finally
            {
                base.SetDefaultCursor();
            }
        }

        private void btnRefrescarPedidosClientesDeuda_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                EntCatalogoGenerico establecimiento = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);

                CargaGvClientesDeuda(establecimiento.Id);
                CargaGvPedidosClientesDeuda(establecimiento.Id);
                btnBuscarCliente.PerformClick();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
            finally { base.SetDefaultCursor(); }
        }
        void FiltrarClientes(List<EntPedido> ListaPedidos, string NombreCliente, string NumeroFactura)
        {
            //List<EntCliente> clientes = (List<EntCliente>)gvClientes.DataSource;

            var pedidosFiltro = from c in ListaPedidos
                                where c.Cliente.ToUpper().Contains(NombreCliente.ToUpper())
                                select c;

            if (!string.IsNullOrWhiteSpace(NumeroFactura))
            {
                pedidosFiltro = from c in pedidosFiltro
                                where c.Factura.ToUpper().Contains(NumeroFactura.ToUpper())
                                select c;
            }

            gvPedidosClientesCreditoDetalle.DataSource = null;
            gvPedidosClientesCreditoDetalle.DataSource = pedidosFiltro.ToList();
            txtDebeTotalFacturas.Text = FormatoMoney(pedidosFiltro.Sum(P => P.Debe));
        }
        void FiltrarClientes(List<EntCliente> ListaClientes, string NombreCliente)
        {
            //List<EntCliente> clientes = (List<EntCliente>)gvClientes.DataSource;

            var pedidosFiltro = from c in ListaClientes
                                where c.Nombre.ToUpper().Contains(NombreCliente.ToUpper())
                                select c;

            gvClientesCredito.DataSource = null;
            gvClientesCredito.DataSource = pedidosFiltro.ToList();
        }
        private void gvClientesCredito_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                EntCliente clienteSeleccionado = ObtieneClienteFromGV(gvClientesCredito);
                txtFiltroClienteFacturasD.Text = clienteSeleccionado.Nombre;

                tcPedidosGrids.SelectedIndex = 1;
                btnBuscarCliente.PerformClick();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                FiltrarClientes(ListaPedidos, txtFiltroClienteFacturasD.Text, txtFiltroFacturaDeudas.Text);
                MuestraTotales();
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
                btnBuscarCliente.PerformClick();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void tcPedidosGrids_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    btnRefrescarPedidosClientesDeuda.PerformClick();
            //}
            //catch (Exception ex)
            //{
            //    MuestraExcepcion(ex);
            //}
        }
        void MuestraTotalSeleccionado(List<EntPedido> ListaPedidosPagos, TextBox TextBoxMuestraTotal)
        {
            decimal total = 0;
            foreach (EntPedido c in ListaPedidosPagos)
            {
                if (c.Estatus)
                    total += c.Pago;
            }
            TextBoxMuestraTotal.Text = FormatoMoney(total);
        }
        List<EntPedido> ObtienePedidosConEstatus(DataGridView GvPedidos, bool Estatus)
        {
            return ObtieneListaPedidosFromGV(GvPedidos).Where(P => P.Estatus == Estatus).ToList();
        }

        private void gvPagos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (e.ColumnIndex == 0)
                //{
                //    //MuestraTotalSeleccionado(ObtieneListaPedidosFromGV(gvPagos), txtTotalPagosDepositos);
                //    gvPagos.CurrentRow.Selected = true;
                //    gvPagos.EndEdit();
                //}
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnAgregaADeposito_Click(object sender, EventArgs e)
        {
        }

        private void btnEstadoDeCuenta_Click(object sender, EventArgs e)
        {
            try
            {
                EntCliente clienteSeleccionado = ObtieneClienteFromGV(gvClientesCredito);
                EstadoDeCuenta vPagos = new EstadoDeCuenta(clienteSeleccionado, ObtieneCatalogoGenericoFromCmb(cmbAlmacenes).Id);
                vPagos.ShowDialog();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnFiltraClientesDeuda_Click(object sender, EventArgs e)
        {
            try
            {
                FiltrarClientes(ListaClientes, txtFiltroClientesDeuda.Text);
                MuestraTotales();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (Program.CambiaEmpresa)
                //{
                //    Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);

                //    btnRefrescarClientesCredito.PerformClick();
                //    btnRefrescarPedidosClientesDeuda.PerformClick();
                //}
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbAlmacenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnRefrescarClientesCredito.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnPagarOtroEstablecimiento_Click(object sender, EventArgs e)
        {
            try
            {
                SeleccionaEstablecimiento vEstablecimiento = new SeleccionaEstablecimiento(Program.UsuarioSeleccionado.EstablecimientoCajaId, "Depositar Pago en");
                if (vEstablecimiento.ShowDialog() == DialogResult.OK)
                {
                    int establecimientoId = vEstablecimiento.EstablecimientoSeleccionado.Id;

                    List<EntPedido> pedidosDeudaCliente = new BusPedidos().ObtienePedidosClientesDeuda().Where(P => P.ClienteRFC == this.ClienteSeleccionado.RFC)
                                                                            .OrderBy(P => P.Fecha).ToList();
                    //this.ListaPedidos.Where(P => P.ClienteRFC == this.ClienteSeleccionado.RFC)
                    //                                                    .OrderBy(P => P.Fecha).ToList();

                    SeleccionaFacturas vSelFac = new SeleccionaFacturas(pedidosDeudaCliente);
                    if (vSelFac.ShowDialog() == DialogResult.OK)
                    {
                        SalidasVentas vVent = new SalidasVentas();
                        bool pagoAgregado = false;
                        if (this.ClienteSeleccionado.RFC != "XAXX010101000")
                        {
                            RevisaFacturasMismoEmisor(vSelFac.FacturasPedidoSeleccionados);
                            Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(vSelFac.FacturasPedidoSeleccionados.First().EmpresaId);

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

                            AgregaComplementoPago vComplePago = new AgregaComplementoPago(
                                                            ConvierteListaPedidosEnFacturas(pedidosfacturasSeleccionadas),
                                                            this.ClienteSeleccionado,
                                                            pedidosfacturasSeleccionadas.Sum(P => P.Debe),
                                                            pedidosfacturasSeleccionadas.Sum(P => P.PagoTotal));

                            if (vComplePago.ShowDialog() == DialogResult.OK)
                            {
                                base.SetWaitCursor();

                                foreach (EntPedido pff in pedidosfacturasSeleccionadas)
                                {
                                    List<EntPedido> pedidosFactura = new BusPedidos().ObtienePedidosPorFactura(pff.FacturaId);
                                    foreach (EntPedido p in pedidosFactura)
                                    {
                                        int pagoId = 0;
                                        pagoAgregado = true;

                                        decimal porcentajePagadoFac = pff.PagoTotal / pff.Total;
                                        decimal pagoPedido = Math.Round(p.Debe * porcentajePagadoFac,2);
                                        try
                                        {
                                            pagoId = vVent.AgregarPagoPedidoBD(establecimientoId, p.Id, pagoPedido, vComplePago.FormaPagoId, vComplePago.FormaPago, vComplePago.FechaPago);
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
                                            vComplePago.AgregarComplementoPago(pff.FacturaId, pagoId, DateTime.Today, pagoPedido,
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
                            }
                        }
                        else
                        {
                            MuestraMensaje("NO SE GENERARÁ COMPLEMENTO DE PAGO POR SER RFC GENÉRICO: XAXX010101000");
                            List<EntPedido> pedidosfacturasSeleccionadas = vSelFac.FacturasPedidoSeleccionados;
                            AgregaPago vAgregaPago = new AgregaPago(pedidosfacturasSeleccionadas.First());
                            foreach (EntPedido pf in pedidosfacturasSeleccionadas)
                            {
                                //SOLO AGREGA EL PAGO QUE DEBE TENER CADA FACTURA
                                vAgregaPago = new AgregaPago(pf, true);//pedidosDeudaCliente);
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

                            foreach (EntPedido p in pedidosfacturasSeleccionadas)
                            {
                                int pagoId = 0;
                                pagoAgregado = true;
                                try
                                {
                                    pagoId = vVent.AgregarPagoPedidoBD(this.AlmacenId, p.Id, p.PagoTotal, vAgregaPago.FormaPagoId, vAgregaPago.FormaPago,
                                                                                                            vAgregaPago.FechaPago);
                                }
                                catch (Exception ex)
                                {
                                    MuestraMensajeError("NO SE LOGRO GUARDAR TODOS LOS PAGOS ASIGNADOS A LAS FACTURAS \n\n " +
                                                        "COMUNIQUESE CON PERSONAL DE SISTEMAS ANTES DE CERRAR ESTE MENSAJE.\n\n\n"
                                                        + ex.Message,
                                                        "ERROR AL AGREGAR PAGO");
                                }
                            }
                        }
                        if (pagoAgregado)
                            MuestraMensaje("¡Pago Agregado!", "CONFIRMACIÓN PAGO");
                        else
                            MuestraMensajeError("Error al agregar el Pago", "ERROR PAGO-COMPLEMENTO");

                        btnRefrescarClientesCredito.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
            finally { base.SetDefaultCursor(); }
        }

        private void btnNotaDeCredito_Click(object sender, EventArgs e)
        {
            try
            {
                //AgregaTotalPago vTotPago = new AgregaTotalPago();
                //if (vTotPago.ShowDialog() == DialogResult.OK)
                //{
                    //decimal totalPagoRestante = ConvierteTextoADecimal(vTotPago.CantidadPago);

                    List<EntPedido> pedidosDeudaCliente = new BusPedidos().ObtienePedidosClientesDeuda(this.ClienteSeleccionado.RFC)
                                                                            .OrderBy(P => P.Fecha).ToList();

                SeleccionaFactura vSelFac = new SeleccionaFactura(pedidosDeudaCliente);
                if (vSelFac.ShowDialog() == DialogResult.OK)
                {
                    SalidasVentas vVent = new SalidasVentas();
                    bool pagoAgregado = false;
                    //if (this.ClienteSeleccionado.RFC != "XAXX010101000")
                    //{
                    RevisaFacturasMismoEmisor(vSelFac.FacturasPedidoSeleccionados);
                    Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(vSelFac.FacturasPedidoSeleccionados.First().EmpresaId);

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
                            //totalPagoRestante = vAgregaPago.CantidadPagoRestante;
                        }
                        else
                            return;
                    }
                    //if (totalPagoRestante > 0)
                    //    MandaExcepcion("NO SE HA ASIGNADO EL TOTAL A PAGAR\n\n TOTAL A PAGAR RESTANTE: " + FormatoMoney(totalPagoRestante));

                    AgregaFacturaNcNeue vNcPago = new AgregaFacturaNcNeue(
                                                    ConvierteListaPedidosEnFacturas(pedidosfacturasSeleccionadas),
                                                    this.ClienteSeleccionado,
                                                    pedidosfacturasSeleccionadas.Sum(P => P.PagoTotal));

                    if (vNcPago.ShowDialog() == DialogResult.OK)
                    {
                        pagoAgregado = true;
                    }
                    //}

                    if (pagoAgregado)
                        MuestraMensaje("¡Nota de Crédito Agregada!", "CONFIRMACIÓN NOTA DE CRÉDITO");
                    else
                        MuestraMensajeError("Error al agregar el Descuento(Pago)", "ERROR PAGO-NOTA DE CREDITO");


                    btnRefrescarClientesCredito.PerformClick();
                }
                //}
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
            finally { base.SetDefaultCursor(); }
        }

        private void btnExporta_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();

                //List<EntCliente> listaClientes = ObtieneListaClientesFromGV(gvClientesCredito);
                //foreach(EntCliente c in listaClientes)
                //{
                //    new BusClientes().AsignaUltimaFechaPagoFacturaEnClientesDeuda(this.AlmacenId, c);
                //}

                List<EntCliente> listaClientes = new BusClientes().ObtieneClientesDeudaUltimoPago(this.AlmacenId);

                ImpresionClientesCredito vImprimeClientesCredito = new ImpresionClientesCredito(listaClientes);
                vImprimeClientesCredito.Show();

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        List<EntPedido> ListaPedidosCP = new List<EntPedido>();
        public void CargaComplementosPago(int EmpresaId, bool PorFechaCP,
                                           DateTime FechaDesdeCP, DateTime FechaHastaCP,
                                           DateTime FechaPagoDesde, DateTime FechaPagoHasta)
        {
            base.SetWaitCursor();
            if(PorFechaCP)            
                this.ListaPedidosCP = new BusPedidos().ObtieneComplementosPago(EmpresaId, this.AlmacenId)
                                                                            .Where(P=>P.Fecha>=FechaDesdeCP && P.Fecha <= FechaHastaCP).ToList();
            else
                this.ListaPedidosCP = new BusPedidos().ObtieneComplementosPago(EmpresaId, this.AlmacenId)
                                                                                .Where(P => P.FechaPago >= FechaPagoDesde && P.FechaPago <= FechaPagoHasta).ToList();
            if (!chkIncluirCanceladasCP.Checked)
                this.ListaPedidosCP = this.ListaPedidosCP.Where(P => !P.EstatusDescripcion.Contains("CANCELADO")).ToList();

            gvComplementosPago.DataSource = this.ListaPedidosCP;

            //txtCantidad.Text = this.ListaPedidosCP.Count.ToString();
            //txtTotalPedidos.Text = FormatoMoney(this.ListaPedidosCP.Sum(P => P.Total));
        }
        public void CargaComplementosPago(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta)
        {
            base.SetWaitCursor();
            this.ListaPedidosCP = new BusPedidos().ObtieneComplementosPago(EmpresaId, this.AlmacenId,
                                                                           FechaDesde, FechaHasta);
            if (!chkIncluirCanceladasCP.Checked)
                this.ListaPedidosCP = this.ListaPedidosCP.Where(P => !P.EstatusDescripcion.Contains("CANCELADO")).ToList();

            gvComplementosPago.DataSource = this.ListaPedidosCP;

            //txtCantidad.Text = this.ListaPedidosCP.Count.ToString();
            //txtTotalPedidos.Text = FormatoMoney(this.ListaPedidosCP.Sum(P => P.Total));
        }
        public void CargaComplementosPagoPorFechaPago(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta)
        {
            base.SetWaitCursor();
            this.ListaPedidosCP = new BusPedidos().ObtieneComplementosPagoPorFechaPago(EmpresaId, this.AlmacenId,
                                                                           FechaDesde, FechaHasta);
            if (!chkIncluirCanceladasCP.Checked)
                this.ListaPedidosCP = this.ListaPedidosCP.Where(P => !P.EstatusDescripcion.Contains("CANCELADO")).ToList();

            gvComplementosPago.DataSource = this.ListaPedidosCP;

            //txtCantidad.Text = this.ListaPedidosCP.Count.ToString();
            //txtTotalPedidos.Text = FormatoMoney(this.ListaPedidosCP.Sum(P => P.Total));
        }
        private void btnRefrescarRegistrosCP_Click(object sender, EventArgs e)
        {
            try
            {
                //CargaComplementosPago(Program.EmpresaSeleccionada.Id,
                //                        rdoPorFechas.Checked,
                //                        dtpFechaDesdeCP.Value.Date, dtpFechaHastaCP.Value.Date,
                //                        dtpFechaDesdePagoCP.Value.Date, dtpFechaHastaPagoCP.Value.Date);
                if (rdoPorFechas.Checked)
                    CargaComplementosPago(Program.EmpresaSeleccionada.Id, dtpFechaDesdeCP.Value.Date, dtpFechaHastaCP.Value.Date);
                else
                    CargaComplementosPago(Program.EmpresaSeleccionada.Id, dtpFechaDesdePagoCP.Value.Date, dtpFechaHastaPagoCP.Value.Date);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnPagarComplemento_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntPedido> pedidosDeudaCliente = new BusPedidos().ObtienePedidosClientesDeuda(this.ClienteSeleccionado.RFC)
                                                                        .OrderBy(P => P.Fecha).ToList();

                SeleccionaFacturas vSelFac = new SeleccionaFacturas(pedidosDeudaCliente);
                if (vSelFac.ShowDialog() == DialogResult.OK)
                {
                    SalidasVentas vVent = new SalidasVentas();
                    bool pagoAgregado = false;
                    if (this.ClienteSeleccionado.RFC != "XAXX010101000")
                    {
                        RevisaFacturasMismoEmisor(vSelFac.FacturasPedidoSeleccionados);
                        Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(vSelFac.FacturasPedidoSeleccionados.First().EmpresaId);

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

                        //AgregaComplementoPago vComplePago = new AgregaComplementoPago(
                        //                                ConvierteListaPedidosEnFacturas(pedidosfacturasSeleccionadas),
                        //                                this.ClienteSeleccionado,
                        //                                pedidosfacturasSeleccionadas.Sum(P => P.Debe),
                        //                                pedidosfacturasSeleccionadas.Sum(P => P.PagoTotal));
                        SeleccionaComplemento vComplePago = new SeleccionaComplemento(
                                        ConvierteListaPedidosEnFacturas(new BusPedidos().ObtieneComplementosPago(Program.EmpresaSeleccionada.Id, this.AlmacenId)));
                        if (vComplePago.ShowDialog() == DialogResult.OK)
                        {
                            base.SetWaitCursor();

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
                                        pagoId = vVent.AgregarPagoPedidoBD(this.AlmacenId, p.Id, pagoPedido, 
                                            vComplePago.FacturaComplementoSeleccionada.FormaPagoId, 
                                            vComplePago.FacturaComplementoSeleccionada.FormaPago, 
                                            vComplePago.FacturaComplementoSeleccionada.Fecha);
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
                                        AgregaComplementoPago vCP = new AgregaComplementoPago();
                                        vCP.AgregarComplementoPago(p.FacturaId, pagoId, DateTime.Today, pagoPedido,
                                                                            vComplePago.FacturaComplementoSeleccionada.FormaPagoId,
                                                                            vComplePago.FacturaComplementoSeleccionada.NumeroFactura,
                                                                            vComplePago.FacturaComplementoSeleccionada.Pago,
                                                                            vComplePago.FacturaComplementoSeleccionada.UUID,
                                                                            vComplePago.FacturaComplementoSeleccionada.Ruta,
                                                                            vComplePago.FacturaComplementoSeleccionada.PDF,
                                                                            vComplePago.FacturaComplementoSeleccionada.XML);
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
                        }
                    }
                    else
                    {
                        MuestraMensaje("NO SE GENERARÁ COMPLEMENTO DE PAGO POR SER RFC GENÉRICO: XAXX010101000");
                        List<EntPedido> pedidosfacturasSeleccionadas = vSelFac.FacturasPedidoSeleccionados;
                        AgregaPago vAgregaPago = new AgregaPago(pedidosfacturasSeleccionadas.First());
                        foreach (EntPedido pf in pedidosfacturasSeleccionadas)
                        {
                            //SOLO AGREGA EL PAGO QUE DEBE TENER CADA FACTURA
                            vAgregaPago = new AgregaPago(pf, true);//pedidosDeudaCliente);
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

                        foreach (EntPedido p in pedidosfacturasSeleccionadas)
                        {
                            int pagoId = 0;
                            pagoAgregado = true;
                            try
                            {
                                pagoId = vVent.AgregarPagoPedidoBD(this.AlmacenId, p.Id, p.PagoTotal, vAgregaPago.FormaPagoId, vAgregaPago.FormaPago,
                                                                                                        vAgregaPago.FechaPago);
                            }
                            catch (Exception ex)
                            {
                                MuestraMensajeError("NO SE LOGRO GUARDAR TODOS LOS PAGOS ASIGNADOS A LAS FACTURAS \n\n " +
                                                    "COMUNIQUESE CON PERSONAL DE SISTEMAS ANTES DE CERRAR ESTE MENSAJE.\n\n\n"
                                                    + ex.Message,
                                                    "ERROR AL AGREGAR PAGO");
                            }
                        }
                    }
                    if (pagoAgregado)
                        MuestraMensaje("¡Pago Agregado!", "CONFIRMACIÓN PAGO");
                    else
                        MuestraMensajeError("Error al agregar el Pago", "ERROR PAGO-COMPLEMENTO");

                    btnRefrescarClientesCredito.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
            finally { base.SetDefaultCursor(); }
        }

        private void btnVerCP_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvComplementosPago);
                List<EntFactura> facturasComplementos = new BusFacturas().ObtieneComplementos(pedidoSeleccionado.FacturaId, pedidoSeleccionado.Factura, pedidoSeleccionado.Total);

                SeleccionaComplemento vSelFactura = new SeleccionaComplemento(pedidoSeleccionado);
                vSelFactura.ListaFacturasComplemento = facturasComplementos;
                if (vSelFactura.ShowDialog() == DialogResult.OK)
                {
                    vSelFactura.FacturaComplementoSeleccionada.Nombre = pedidoSeleccionado.Cliente;
                    if (base.VerificaReAsignarFactura(vSelFactura.FacturaComplementoSeleccionada))
                    {
                        base.VerificaExistenArchivosFactura(vSelFactura.FacturaComplementoSeleccionada.Ruta,
                                                       "CP" + vSelFactura.FacturaComplementoSeleccionada.NumeroComplemento + " FAC-" + vSelFactura.FacturaComplementoSeleccionada.NumeroFactura,
                                                       vSelFactura.FacturaComplementoSeleccionada.PDF,
                                                       vSelFactura.FacturaComplementoSeleccionada.XML);
                        base.MuestraArchivo(vSelFactura.FacturaComplementoSeleccionada.Ruta, EncuentraArchivo(vSelFactura.FacturaComplementoSeleccionada.Ruta, ".pdf"));
                    }
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        void FiltrarPedidos(List<EntPedido> ListaPedidos, string NombreCliente, string RFC, string NumComplemento, string NumeroFactura,
                            DataGridView GvMuestraPedidos)
        {
            //List<EntCliente> clientes = (List<EntCliente>)gvClientes.DataSource;

            var pedidosFiltro = from c in ListaPedidos
                                where c.Cliente.ToUpper().Contains(NombreCliente.ToUpper()) ||
                                      c.ClienteNombreFiscal.ToUpper().Contains(NombreCliente.ToUpper())
                                select c;

            if (!string.IsNullOrWhiteSpace(RFC))
            {
                pedidosFiltro = from c in pedidosFiltro
                                    //where c.NumOrden.ToUpper().Contains(NumeroOrden.ToUpper())
                                where c.ClienteRFC.ToString().Contains(RFC.ToUpper())
                                select c;
            }

            if (!string.IsNullOrWhiteSpace(NumComplemento))
            {
                pedidosFiltro = from c in pedidosFiltro
                                where c.Descripcion.ToUpper().Contains(NumComplemento.ToUpper())
                                select c;
            }

            if (!string.IsNullOrWhiteSpace(NumeroFactura))
            {
                pedidosFiltro = from c in pedidosFiltro
                                where c.Factura.ToUpper().Contains(NumeroFactura.ToUpper())
                                select c;
            }


            GvMuestraPedidos.DataSource = null;
            GvMuestraPedidos.DataSource = pedidosFiltro.ToList();
        }
        private void btnFiltrarCP_Click(object sender, EventArgs e)
        {
            try
            {
                FiltrarPedidos(this.ListaPedidosCP, txtFiltroClienteCP.Text, txtFiltroRfcCP.Text, txtFiltroNumCP.Text, txtFiltroFacturaCP.Text,
                               gvComplementosPago);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void rdoPorFechas_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pnlFechasCP.Enabled = rdoPorFechas.Checked;
                pnlFechasPagoCP.Enabled = !rdoPorFechas.Checked;
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnExportaComplementosPago_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();

                List<EntPedido> listaCP = ObtieneListaPedidosFromGV(gvComplementosPago);

                ImpresionPedidosCP vImprimeClientesCredito = new ImpresionPedidosCP(listaCP);
                vImprimeClientesCredito.Show();

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnEstadoDeCuentaFacturasVencidas_Click(object sender, EventArgs e)
        {
            try
            {
                EstadoDeCuentaFacturasVencidas vPagos = new EstadoDeCuentaFacturasVencidas(ObtieneCatalogoGenericoFromCmb(cmbAlmacenes).Id);
                vPagos.ShowDialog();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void gvClientesCredito_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                var grid = (DataGridView)sender;
                var columna = grid.Columns[e.ColumnIndex];

                // Si la columna no está ligada a una propiedad, no hacemos nada
                if (string.IsNullOrWhiteSpace(columna.DataPropertyName))
                    return;

                // Obtener la lista original (ya la tienes cargada)
                var lista = (List<EntCliente>)grid.DataSource;

                // Determinar si toca ASC o DESC usando Tag de la columna
                bool asc = columna.Tag == null || !(bool)columna.Tag;

                // Ordenar usando reflection
                if (asc)
                {
                    grid.DataSource = lista
                        .OrderBy(x => x.GetType().GetProperty(columna.DataPropertyName).GetValue(x))
                        .ToList();
                }
                else
                {
                    grid.DataSource = lista
                        .OrderByDescending(x => x.GetType().GetProperty(columna.DataPropertyName).GetValue(x))
                        .ToList();
                }

                // Guardar el nuevo estado
                columna.Tag = asc;
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }
    }    
}
