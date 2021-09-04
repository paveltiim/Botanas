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
        public void VerificaEmpresa()
        {
            cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }

        public ClientesCredito()
        {
            InitializeComponent();
        }

        List<EntPedido> ListaPedidos;
        List<EntCliente> ListaClientes;

        EntCliente ClienteSeleccionado { get { return ObtieneClienteFromGV(gvClientesCredito); } }

        EntPedido PedidoDetalleSeleccionado { get { return ObtienePedidoFromGV(gvPedidosClientesCreditoDetalle); } }

        public void CargaGvPedidosClientesDeuda()
        {
            ListaPedidos = new BusPedidos().ObtienePedidosClientesDeuda(Program.EmpresaSeleccionada.Id);
            gvPedidosClientesCreditoDetalle.DataSource = ListaPedidos;
            txtTotalDebe.Text = FormatoMoney(ListaPedidos.Sum(P => P.Debe));
        }
        public void CargaGvClientesDeuda()
        {
            ListaClientes = new BusClientes().ObtieneClientesDeuda(Program.EmpresaSeleccionada.Id);
            gvClientesCredito.DataSource = ListaClientes;
        }
        //public void CargaGvPagosClientes()
        //{
        //    gvPagos.DataSource = new BusPedidos().ObtienePagosClientesSinDeposito();
        //}

        List<EntEmpresa> ListaEmpresas;
        public void CargaEmpresas()
        {
            if (Program.UsuarioSeleccionado.Id > 1)
                this.ListaEmpresas = new BusEmpresas().ObtieneEmpresas().Where(P => P.UsuarioId == Program.UsuarioSeleccionado.Id).ToList();
            else
                this.ListaEmpresas = new BusEmpresas().ObtieneEmpresas();

            Program.CambiaEmpresa = false;
            cmbEmpresas.DataSource = this.ListaEmpresas;
            Program.CambiaEmpresa = true;

            //CargaClientesEnPantallas();
        }
        void InicializaPantalla()
        {

        }

        private void ClientesCredito_Load(object sender, EventArgs e)
        {
            try
            {
                InicializaPantalla();
                CargaEmpresas();

                if (Program.EmpresaSeleccionada == null)
                    Program.EmpresaSeleccionada = SeleccionaEmpresa();

                cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);

                CargaGvClientesDeuda();
                CargaGvPedidosClientesDeuda();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }
        /// <summary>
        /// Agrega nuevo registro del Pedido solicitado.
        /// </summary>
        /// <param name="pedido"></param>
        void AgregarPago(int PedidoId, decimal Cantidad, DateTime FechaPago)
        {
            EntPago pago = new EntPago()
            {
                PedidoId = PedidoId,
                TipoPagoId = 1,
                Cantidad = Cantidad,
                FechaPago = FechaPago
            };
            new BusPedidos().AgregaPagoPedido(pago);
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntPedido> pedidosDeudaCliente = ListaPedidos.Where(P => P.ClienteId == ClienteSeleccionado.Id)
                                                                    .OrderBy(P => P.Fecha).ToList();

                //    AgregaPago vAgregaPago = new AgregaPago(pedidosDeudaCliente);
                //    if (vAgregaPago.ShowDialog() == DialogResult.OK)
                //    {
                //        EntCliente ClienteSeleccionado = this.ClienteSeleccionado;
                //        decimal cantidadPaga = ConvierteTextoADecimal(vAgregaPago.CantidadPago);

                //        EntPedido pedidoPrimero = pedidosDeudaCliente[0];

                //        if (cantidadPaga == 0)
                //            throw new Exception("La cantidad a Pagar debe ser mayor a 0.");
                //        if (cantidadPaga > ClienteSeleccionado.Debe)
                //            throw new Exception("La cantidad a Pagar debe ser menor o igual a la cantidad que Debe el cliente.");
                //        if (vAgregaPago.FechaPago < pedidoPrimero.Fecha)
                //            throw new Exception("La Fecha de Pago no puede ser inferior a la Fecha de Factura");


                //        decimal cantidadAgregaPago;
                //        foreach (EntPedido p in pedidosDeudaCliente)
                //        {
                //            if (cantidadPaga == 0)
                //                return;

                //            if (cantidadPaga > p.Debe)
                //                cantidadAgregaPago = p.Debe;
                //            else
                //                cantidadAgregaPago = cantidadPaga;

                //            //AumentaPagoPedido(p.Id, cantidadAgregaPago);
                //            AgregarPago(p.Id, cantidadAgregaPago, vAgregaPago.FechaPago);

                //            cantidadPaga -= cantidadAgregaPago;
                SeleccionaFacturas vSelFac = new SeleccionaFacturas(pedidosDeudaCliente);
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
                                                                                    ClienteSeleccionado,
                                                                                    pedidosfacturasSeleccionadas.Sum(P => P.Debe),
                                                                                    pedidosfacturasSeleccionadas.Sum(P => P.PagoTotal));
                    if (vComplePago.ShowDialog() == DialogResult.OK)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        bool pagoAgregado = false;
                        Registros vReg = new Registros();

                        vReg.DescuentaTimbre(Program.EmpresaSeleccionada);

                        foreach (EntPedido p in pedidosfacturasSeleccionadas)
                        {
                            int pagoId = 0;
                            pagoAgregado = true;
                            try
                            {
                                pagoId = vReg.AgregarPago(p.Id, p.PagoTotal, p.FechaPago);
                                vReg.AumentaPagoPedido(p.Id, p.PagoTotal);
                            }
                            catch (Exception ex)
                            {
                                MuestraMensajeError("NO SE LOGRO GUARDAR TODOS LOS PAGOS ASIGNADOS A LAS FACTURAS \n\n" + ex.Message, "ERROR AL AGREGAR PAGO");
                            }
                            try
                            {
                                vComplePago.AgregarComplementoPago(p.FacturaId, DateTime.Today, p.PagoTotal,
                                                                    vComplePago.FormaPagoId,
                                                                    vComplePago.ComplementoPago.NumeroFactura, vComplePago.CantidadPago,
                                                                    vComplePago.ComplementoPago.UUID, vComplePago.ComplementoPago.Ruta);
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
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
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
                        Registros vReg = new Registros();
                        AgregarPago(pedidoSeleccionado.Id, cantidadAgregaPago, vAgregaPago.FechaPago);
                        int pagoId = vReg.AgregarPago(pedidoSeleccionado.Id, cantidadAgregaPago, vAgregaPago.FechaPago);
                        vReg.AumentaPagoPedido(pedidoSeleccionado.Id, cantidadAgregaPago);

                        CargaGvClientesDeuda();
                        CargaGvPedidosClientesDeuda();

                        MuestraMensaje("¡Pago Agregado!", "CONFIRMACIÓN PAGO");
                    }
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
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
                this.Cursor = Cursors.WaitCursor;
                CargaGvClientesDeuda();
                CargaGvPedidosClientesDeuda();
                //CargaGvPagosClientes();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnRefrescarPedidosClientesDeuda_Click(object sender, EventArgs e)
        {
            try
            {
                CargaGvPedidosClientesDeuda();
                btnBuscarCliente.PerformClick();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
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
            txtTotalDebe.Text = FormatoMoney(pedidosFiltro.Sum(P => P.Debe));
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
                EstadoDeCuenta vPagos = new EstadoDeCuenta(clienteSeleccionado);
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
                if (Program.CambiaEmpresa)
                {
                    Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);

                    CargaGvClientesDeuda();
                    CargaGvPedidosClientesDeuda();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
    }    
}
