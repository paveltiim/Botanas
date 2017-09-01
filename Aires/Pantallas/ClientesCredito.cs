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

        EntCliente ClienteSeleccionado { get { return ObtieneClienteFromGV(gvClientesCredito); } }

        EntPedido PedidoDetalleSeleccionado { get { return ObtienePedidoFromGV(gvPedidosClientesCreditoDetalle); } }

        public void CargaGvPedidosClientesCredito()
        {
            ListaPedidos = new BusPedidos().ObtienePedidosClientesCredito();
            gvPedidosClientesCreditoDetalle.DataSource = ListaPedidos;
        }
        public void CargaGvClientesCredito()
        {
            ListaClientes = new BusClientes().ObtieneClientesCredito();
            gvClientesCredito.DataSource = ListaClientes;
        }
        public void CargaGvPagosClientes()
        {
            gvPagos.DataSource = new BusPedidos().ObtienePagosClientesSinDeposito();
        }


        private void ClientesCredito_Load(object sender, EventArgs e)
        {
            try
            {
                CargaGvClientesCredito();
                CargaGvPedidosClientesCredito();
                CargaGvPagosClientes();
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
        void AumentaPagoPedido(int PedidoId, decimal Pago)
        {
            EntPedido pedido = new EntPedido()
            {
                Id = PedidoId,
                Pago = Pago,
                Fecha = DateTime.Today
            };
            new BusPedidos().AumentaPagoPedido(pedido);
        }
        private void btnPagar_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntPedido> pedidosDeudaCliente = ListaPedidos.Where(P => P.ClienteId == ClienteSeleccionado.Id).OrderBy(P => P.Fecha).ToList();

                AgregaPago vAgregaPago = new AgregaPago(pedidosDeudaCliente);
                
                if (vAgregaPago.ShowDialog() == DialogResult.OK)
                {
                    EntCliente ClienteSeleccionado = this.ClienteSeleccionado;
                    decimal cantidadPaga = ConvierteTextoADecimal(vAgregaPago.CantidadPago);

                    if (cantidadPaga == 0)
                        throw new Exception("La cantidad a Pagar debe ser mayor a 0.");
                    if(cantidadPaga>ClienteSeleccionado.Debe)
                        throw new Exception("La cantidad a Pagar debe ser menor o igual a la cantidad que Debe el cliente.");
  
                    
                    decimal cantidadAgregaPago;
                    foreach (EntPedido p in pedidosDeudaCliente)
                    {
                        if (cantidadPaga == 0)
                            return;

                        if (cantidadPaga > p.Debe)
                            cantidadAgregaPago = p.Debe;
                        else
                            cantidadAgregaPago = cantidadPaga;

                        AumentaPagoPedido(p.Id, cantidadAgregaPago);
                        AgregarPago(p.Id, cantidadAgregaPago, vAgregaPago.FechaPago); 
                        
                        cantidadPaga -= cantidadAgregaPago;
                    }

                    CargaGvClientesCredito();
                    CargaGvPedidosClientesCredito();
                    CargaGvPagosClientes();

                    MuestraMensaje("¡Pago Agregado!", "CONFIRMACIÓN PAGO");
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
                AgregaPago vAgregaPago = new AgregaPago(pedidoSeleccionado);
                if (vAgregaPago.ShowDialog() == DialogResult.OK)
                {
                    decimal cantidadPaga = ConvierteTextoADecimal(vAgregaPago.CantidadPago);

                    if (cantidadPaga == 0)
                        throw new Exception("La cantidad a Pagar debe ser mayor a 0.");
                    if (cantidadPaga > pedidoSeleccionado.Debe)
                        throw new Exception("La cantidad a Pagar debe ser menor o igual a la cantidad que Debe el cliente.");

                    //List<EntPedido> pedidosDeudaCliente = ListaPedidos.Where(P => P.ClienteId == pedidoSeleccionado.Id).OrderBy(P => P.Fecha).ToList();

                    decimal cantidadAgregaPago=cantidadPaga;
                    //foreach (EntPedido p in pedidosDeudaCliente)
                    //{
                    //    if (cantidadPaga == 0)
                    //        return;

                    //    if (cantidadPaga > p.Debe)
                    //        cantidadAgregaPago = p.Debe;
                    //    else
                    //        cantidadAgregaPago = cantidadPaga;

                    //    AumentaPagoPedido(p.Id, cantidadAgregaPago);
                    //    AgregarPago(p.Id, cantidadAgregaPago);

                    //    cantidadPaga -= cantidadAgregaPago;
                    //}
                    
                    AumentaPagoPedido(pedidoSeleccionado.Id, cantidadAgregaPago);
                    AgregarPago(pedidoSeleccionado.Id, cantidadAgregaPago, vAgregaPago.FechaPago);

                    CargaGvClientesCredito();
                    CargaGvPedidosClientesCredito();

                    MuestraMensaje("¡Pago Agregado!", "CONFIRMACIÓN PAGO");
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnVerPagosCliente_Click(object sender, EventArgs e)
        {
            try {
                EntCliente clienteSeleccionado= ObtieneClienteFromGV(gvClientesCredito);
                MuestraPagos vPagos = new MuestraPagos(clienteSeleccionado.Id);
                vPagos.ShowDialog();

                CargaGvClientesCredito();
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
                CargaGvClientesCredito();
                CargaGvPedidosClientesCredito();
                CargaGvPagosClientes();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnRefrescarPedidosClientesDeuda_Click(object sender, EventArgs e)
        {
            try
            {
                CargaGvPedidosClientesCredito();
                btnBuscarCliente.PerformClick();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }
        void FiltrarClientes(List<EntPedido> ListaPedidos, string NombreCliente)
        {
            //List<EntCliente> clientes = (List<EntCliente>)gvClientes.DataSource;

            var pedidosFiltro = from c in ListaPedidos
                                 where c.Cliente.ToUpper().Contains(NombreCliente.ToUpper())
                                 select c;

            gvPedidosClientesCreditoDetalle.DataSource = null;
            gvPedidosClientesCreditoDetalle.DataSource = pedidosFiltro.ToList();
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
                FiltrarClientes(ListaPedidos, txtClienteBusqueda.Text);                    
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
            decimal total=0;
            foreach(EntPedido c in ListaPedidosPagos)
            {
                if (c.Estatus)
                    total += c.Pago;
            }
            TextBoxMuestraTotal.Text = FormatoMoney(total);
        }
        List<EntPedido> ObtienePedidosConEstatus(DataGridView GvPedidos, bool Estatus)
        {
            return ObtieneListaPedidosFromGV(GvPedidos).Where(P=>P.Estatus==Estatus).ToList();
        }

        private void gvPagos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    //MuestraTotalSeleccionado(ObtieneListaPedidosFromGV(gvPagos), txtTotalPagosDepositos);
                    gvPagos.CurrentRow.Selected = true;
                    gvPagos.EndEdit();
                }
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
            EntDeposito deposito = new EntDeposito() {
                Total=Total,
                Fecha=FechaDeposito
            };
            return new BusDepositos().AgregaDeposito(deposito);
        }
        void AgregaPagoDeposito(int DepositoId, int PagoId)
        {
            EntDeposito deposito = new EntDeposito() {
               Id=DepositoId,
               PagoId=PagoId
            };
            new BusDepositos().AgregaPagoDeposito(deposito);
        }
        void AgregaPagosDeposito(List<EntPedido> PedidosPagos, int DepositoId)
        {
            foreach (EntPedido p in PedidosPagos)
            {
                AgregaPagoDeposito(DepositoId, p.Id);
            }            
        }

        private void btnAgregaADeposito_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntPedido> pagosPedidoSeleccionados = ObtienePedidosConEstatus(gvPagos, true);
                if (pagosPedidoSeleccionados == null)
                    MandaExcepcion("Seleccione al menos un Pago para agregar a Depósito");

                AgregaDeposito vAgregaDeposito = new AgregaDeposito();
                vAgregaDeposito.CantidadEnabled = false;
                vAgregaDeposito.CantidadPago = txtTotalPagosDepositos.Text;
                if (vAgregaDeposito.ShowDialog() == DialogResult.OK)
                {
                    //decimal cantidadDeposito = ConvierteTextoADecimal(txtTotalPagosDepositos.Text);
                    int depositoId = AgregaDeposito(ConvierteTextoADecimal(vAgregaDeposito.CantidadPago), vAgregaDeposito.FechaPago);

                    AgregaPagosDeposito(pagosPedidoSeleccionados, depositoId);

                    CargaGvPagosClientes();

                    MuestraMensaje("¡Depósito Agregado!", "CONFIRMACIÓN");
                }
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
    }
}
