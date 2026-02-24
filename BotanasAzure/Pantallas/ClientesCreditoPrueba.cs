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
    public partial class ClientesCreditoPrueba : FormBase
    {
        public ClientesCreditoPrueba()
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
            this.ListaPedidos = new BusPedidos().ObtienePedidosClientesDeuda(EstablecimientoId).Where(P => P.Debe > 0).ToList();
            gvPedidosClientesCreditoDetalle.DataSource = this.ListaPedidos;
            MuestraTotales();
        }
        public void CargaGvClientesDeuda(int EstablecimientoId)
        {
            this.ListaClientes = new BusClientes().ObtieneClientesDeuda(EstablecimientoId).Where(P => P.Debe > 0).ToList();
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
        }


        private void ClientesCredito_Load(object sender, EventArgs e)
        {
            try
            {
                InicializaPantalla();
                //CargaEmpresas();

                //if (Program.EmpresaSeleccionada == null)
                //    Program.EmpresaSeleccionada = SeleccionaEmpresa();

                //cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
                CargaAlmacenes();
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
                List<EntPedido> pedidosDeudaCliente = new BusPedidos().ObtienePedidosClientesDeuda(this.ClienteSeleccionado.RFC)
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
                        Program.EmpresaSeleccionada.Facturacion = false;

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
                                foreach (EntPedido pf in pedidosFactura)
                                {
                                    int pagoId = 0;
                                    pagoAgregado = true;

                                    decimal porcentajePagadoFac = pff.PagoTotal / pff.Total;
                                    decimal pagoPedido = Math.Round(pf.Debe * porcentajePagadoFac, 2);
                                    try
                                    {
                                        MuestraMensaje(FormatoMoney( pagoPedido));
                                        //pagoId = vVent.AgregarPagoPedido(this.AlmacenId,pf.Id, pf.Total, vComplePago.FormaPagoId, vComplePago.FormaPago, vComplePago.FechaPago);
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
                                        //vComplePago.AgregarComplementoPago(p.FacturaId, pagoId, DateTime.Today, pf.Total,
                                        //                                    vComplePago.FormaPagoId,
                                        //                                    vComplePago.ComplementoPago.NumeroFactura,
                                        //                                    vComplePago.CantidadPago,
                                        //                                    vComplePago.ComplementoPago.UUID,
                                        //                                    vComplePago.ComplementoPago.Ruta,
                                        //                                    vComplePago.ComplementoPago.PDF,
                                        //                                    vComplePago.ComplementoPago.XML);
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
                                //pagoId = vVent.AgregarPagoPedido(this.AlmacenId, p.Id, p.PagoTotal, vAgregaPago.FormaPagoId, vAgregaPago.FormaPago,
                                //                                                                        vAgregaPago.FechaPago);
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
                    //if (pagoAgregado)
                    //    MuestraMensaje("¡Pago Agregado!", "CONFIRMACIÓN PAGO");
                    //else
                    //    MuestraMensajeError("Error al agregar el Pago", "ERROR PAGO-COMPLEMENTO");
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

                SeleccionaComplemento vSelFactura = new SeleccionaComplemento(pedidoSeleccionado);
                vSelFactura.FacturaId = pedidoSeleccionado.FacturaId;
                vSelFactura.ListaFacturasComplemento = facturasComplementos;
                if (vSelFactura.ShowDialog() == DialogResult.OK)
                {
                    MuestraArchivo(vSelFactura.FacturaComplementoSeleccionada.Ruta);
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
                txtClienteBusqueda.Text = clienteSeleccionado.Nombre;

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
                FiltrarClientes(ListaPedidos, txtClienteBusqueda.Text, txtFacturaFiltro.Text);
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

        private void gvPagos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.ColumnIndex == 0)
                {
                    MuestraTotalSeleccionado(ObtieneListaPedidosFromGV(gvPagos), txtTotalPagosDepositos);

                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void gvPagos_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (e.ColumnIndex == 0)
            //    {
            //        MuestraTotalSeleccionado(ObtieneListaPedidosFromGV(gvPagos), txtTotalPagosDepositos);

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MuestraExcepcion(ex);
            //}
        }

        private void gvPagos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (e.ColumnIndex == 0)
            //    {
            //        MuestraTotalSeleccionado(ObtieneListaPedidosFromGV(gvPagos), txtTotalPagosDepositos);

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MuestraExcepcion(ex);
            //}
        }
        int AgregaDeposito(decimal Total, DateTime FechaDeposito)
        {
            //EntDeposito deposito = new EntDeposito()
            //{
            //    Total = Total,
            //    Fecha = FechaDeposito
            //};
            //return new BusDepositos().AgregaDeposito(deposito);
            return 0;
        }
        void AgregaPagoDeposito(int DepositoId, int PagoId)
        {
            //EntDeposito deposito = new EntDeposito()
            //{
            //    Id = DepositoId,
            //    PagoId = PagoId
            //};
            //new BusDepositos().AgregaPagoDeposito(deposito);
        }
        void AgregaPagosDeposito(List<EntPedido> PedidosPagos, int DepositoId)
        {
            //foreach (EntPedido p in PedidosPagos)
            //{
            //    AgregaPagoDeposito(DepositoId, p.Id);
            //    try
            //    {
            //        if (p.MetodoPagoId == 2 && p.Fecha > new DateTime(2017, 11, 11))//FECHA DE IMPLEMENTACION DE FACTURA 3.3.
            //        {
            //            AgregaComprobantePago(p);
            //        }
            //    }
            //    catch (Exception ex) { MuestraExcepcion(ex, "ERROR EN COMPLEMENTO"); }
            //}
        }
        void AgregaComprobantePago(EntPedido PedidoFactura)
        {
            //AgregaComplementoPago vComple = new AgregaComplementoPago();
            //vComple.Cliente = new BusClientes().ObtieneClientes(PedidoFactura.ClienteId);
            //vComple.PedidoFactura = PedidoFactura;

            //if (vComple.ShowDialog() == DialogResult.OK)
            //{

            //}
        }

        private void btnAgregaADeposito_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntPedido> pagosPedidoSeleccionados = ObtienePedidosConEstatus(gvPagos, true).OrderByDescending(P => P.FechaPago).ToList();
                if (pagosPedidoSeleccionados == null)
                    MandaExcepcion("Seleccione al menos un Pago para agregar a Depósito");

                //AgregaDeposito vAgregaDeposito = new AgregaDeposito();
                //vAgregaDeposito.CantidadEnabled = false;
                //vAgregaDeposito.CantidadPago = txtTotalPagosDepositos.Text;
                //vAgregaDeposito.FechaMin = pagosPedidoSeleccionados[0].Fecha;
                //vAgregaDeposito.FechaMax = DateTime.Today;

                //if (vAgregaDeposito.ShowDialog() == DialogResult.OK)
                //{
                //    ////decimal cantidadDeposito = ConvierteTextoADecimal(txtTotalPagosDepositos.Text);
                //    EntPedido pagoPedido = pagosPedidoSeleccionados[0];
                //    if (vAgregaDeposito.FechaPago < pagoPedido.FechaPago)
                //        MandaExcepcion("La Fecha de Depósito no puede ser inferior a la Fecha de Pago");

                //    int depositoId = AgregaDeposito(ConvierteTextoADecimal(vAgregaDeposito.CantidadPago), vAgregaDeposito.FechaPago);

                //    AgregaPagosDeposito(pagosPedidoSeleccionados, depositoId);

                //    CargaGvPagosClientes();

                //    MuestraMensaje("¡Depósito Agregado!", "CONFIRMACIÓN");
                //}
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
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

        private void button1_Click(object sender, EventArgs e)
        {

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

                                foreach (EntPedido p in pedidosfacturasSeleccionadas)
                                {
                                    List<EntPedido> pedidosFactura = new BusPedidos().ObtienePedidosPorFactura(p.FacturaId);
                                    foreach (EntPedido pf in pedidosFactura)
                                    {
                                        int pagoId = 0;
                                        pagoAgregado = true;
                                        try
                                        {
                                            pagoId = vVent.AgregarPagoPedidoBD(establecimientoId, pf.Id, pf.Total, vComplePago.FormaPagoId, vComplePago.FormaPago, vComplePago.FechaPago);
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
                                            vComplePago.AgregarComplementoPago(p.FacturaId, pagoId, DateTime.Today, pf.Total,
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
                    }
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
            finally { base.SetDefaultCursor(); }
        }
    }    
}
