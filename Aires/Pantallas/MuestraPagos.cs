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
    public partial class MuestraPagos : FormBase
    {
        public int ClienteId;
        public MuestraPagos(int ClienteId)
        {
            InitializeComponent();

            this.ClienteId = ClienteId;
        }

        //public EntEmpresa ObtieneEmpresaFromGV(DataGridView GridViewEmpresas)
        //{
        //    if (GridViewEmpresas.CurrentRow == null)
        //        return null;
        //    return (EntEmpresa)((List<EntEmpresa>)GridViewEmpresas.DataSource)[GridViewEmpresas.CurrentRow.Index];
        //}

        //public AiresEntidades.EntEmpresa EmpresaGastoSeleccionada { get { return ObtieneEmpresaFromGV(gvGastosEmpresa); } }

        void CargaPagosPorCliente(int ClienteId)
        {
            gvPagos.DataSource = new BusPedidos().ObtienePagosPorCliente(ClienteId);
            gvPagos.Refresh();
        }
        void ActualizaEstatusPago(EntPago Pago, bool Estatus)
        {
            Pago.Estatus=Estatus;
            new BusPedidos().ActualizaEstatusPago(Pago);
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
        

        private void SeleccionaFactura_Load(object sender, EventArgs e)
        {
            try
            {
                CargaPagosPorCliente(ClienteId);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                EntPago pagoSeleccionado = ObtienePagoFromGV(gvPagos);
                if (MuestraMensajeYesNo(string.Format("¿Seguro desea ELIMINAR el pago seleccionado? \n Fecha Pago:{0} \n Cantidad Pago:{1}", pagoSeleccionado.FechaPago.ToShortDateString(), FormatoMoney(pagoSeleccionado.Cantidad)), "CONFIRMACIÓN") == DialogResult.Yes)
                { 
                    ActualizaEstatusPago(pagoSeleccionado, false);
                    AumentaPagoPedido(pagoSeleccionado.PedidoId, pagoSeleccionado.Cantidad*-1);
                    CargaPagosPorCliente(ClienteId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gvPagos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MuestraMensajeYesNo("¿Desea guardar los cambios realizdos?") == DialogResult.Yes)
                {
                    List<EntPago> pagosSeleccionados = ObtieneListaPagosFromGV(gvPagos);
                    foreach (EntPago p in pagosSeleccionados)
                    {
                        new BusPedidos().ActualizaPago(p);
                    }
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
    }
}
