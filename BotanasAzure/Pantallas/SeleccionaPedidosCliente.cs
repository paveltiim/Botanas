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
    public partial class SeleccionaPedidosCliente : FormBase
    {
        List<EntPedido> PedidosFacturas;
        List<EntPedido> ListaPedidosInciales;
        public SeleccionaPedidosCliente(List<AiresEntidades.EntPedido> PedidosInciales)
        {
            InitializeComponent();
            this.ListaPedidosInciales = PedidosInciales;
            gvFacturasPedido.DataSource = PedidosInciales;
        }

        public List<EntPedido> ObtieneListaPedidosFromGV(DataGridView GridViewPedidos, bool Estatus)
        {
            if (GridViewPedidos.DataSource == null)
                return new List<EntPedido>();
            return ((List<EntPedido>)GridViewPedidos.DataSource).Where(P => P.Estatus = Estatus).ToList();
        }

        /// <summary>
        /// ObtieneListaPedidosFromGV(gvFacturasPedido).Where(P => P.Estatus).ToList(); 
        /// </summary>
        public List<EntPedido> FacturasPedidoSeleccionados
        {
            get
            {
                //txtFacturaFiltro.Clear();
                return ObtieneListaPedidosFromGV(gvFacturasPedido).Where(P => P.Estatus).ToList();
            }
        }
        public List<EntPedido> PedidosSeleccionados { get { return ObtieneListaPedidosFromGV(gvFacturasPedido).Where(P => P.Estatus).ToList(); } }

        string versionCFDI = "";

        public EntCliente ClienteSeleccionado { get; set; }
        List<EntCliente> ListaClientes = new List<EntCliente>();
        public void CargaClientes(int TipoClienteId)
        {
            this.ListaClientes = new BusClientes().ObtieneClientesPorEstablecimiento(Program.UsuarioSeleccionado.EstablecimientoClientesId)
                                                                            .Where(P => P.TipoPersonaId == TipoClienteId).ToList();
            gvClientes.DataSource = this.ListaClientes;
            this.ClienteSeleccionado = null;
        }
        int EstableccimientoId { get { return ObtieneCatalogoGenericoFromCmb(cmbEstablecimientos).Id; } }
        void CargaEstablecimientos()
        {
            List<EntCatalogoGenerico> establecimientos = new BusEmpresas().ObtieneAlmacenes(Program.UsuarioSeleccionado.CompañiaId, Program.UsuarioSeleccionado.Id);//USUARIO PRINCIPAL
            cmbEstablecimientos.DataSource = establecimientos;
            cmbEstablecimientos.SelectedIndex = 0;
        }
        void CargaTipoCliente(int TipoClienteId)
        {
            List<EntCatalogoGenerico> listaProductos = new List<EntCatalogoGenerico>();
            listaProductos.Add(new EntCatalogoGenerico() { Id = 1, Descripcion = "Menudeo" });
            listaProductos.Add(new EntCatalogoGenerico() { Id = 2, Descripcion = "MayoreoLocal" });
            listaProductos.Add(new EntCatalogoGenerico() { Id = 4, Descripcion = "Culiacan" });
            listaProductos.Add(new EntCatalogoGenerico() { Id = 9, Descripcion = "CONVENIENCIA" });
            listaProductos.Add(new EntCatalogoGenerico() { Id = 10, Descripcion = "EVENTUAL" });
            listaProductos.Add(new EntCatalogoGenerico() { Id = 11, Descripcion = "DETALLE" });
            cmbTipoCliente.DataSource = null;
            cmbTipoCliente.DataSource = listaProductos;
            //cmbTipoCliente.SelectedIndex = 0;
            //cmbTipoCliente.SelectedIndex = ((List<EntCatalogoGenerico>)cmbTipoCliente.DataSource).FindIndex(P => P.Id == TipoClienteId);//Cliente.TipoPersonaId - 1;
            base.SeleccionarIndexComboBox(cmbTipoCliente, TipoClienteId);
        }

        void CargaDatosCliente(EntCliente Cliente)
        {
            this.ClienteSeleccionado = Cliente;

            lbCliente.Text = Cliente.Nombre;
            lbNombreFiscal.Text = Cliente.NombreFiscal;
            lbDireccion.Text = Cliente.Direccion;
            lbRFC.Text = Cliente.RFC;

            pnlFechasVentas.Visible = true;
            pnlPedidos.Visible = true;
            btnRefrescarVentas.PerformClick();
        }
        List<EntPedido> ListaPedidos = new List<EntPedido>();
        public void CargaPedidos(int EstablecimientoId, DateTime FechaDesde, DateTime FechaHasta,
                                    int ClienteId)
        {
            this.ListaPedidos = new BusPedidos().ObtienePedidosPorEstablecimiento(EstablecimientoId, FechaDesde, FechaHasta)
                                                                                   .Where(P => P.ClienteId == ClienteId).ToList();
            //if (chkVerDevoluciones.Checked)
            //    this.ListaPedidos.AddRange(new BusPedidos().ObtienePedidosPorEstablecimiento(EstablecimientoId, FechaDesde, FechaHasta,
            //                                                                          (int)TipoPedido.DEVOLUCIONCORTESIA));
            //if (chkVerCortesias.Checked)
            //    this.ListaPedidos.AddRange(new BusPedidos().ObtienePedidosPorEstablecimiento(EstablecimientoId, FechaDesde, FechaHasta,
            //                                                                          (int)TipoPedido.CORTESIA));

            //if (!chkVerFacturasCanceladas.Checked)
            //    this.ListaPedidos = this.ListaPedidos.Where(P => !P.EstatusDescripcion.Contains("CANCELA")).ToList();
            //foreach(EntProducto p in this.ListaPedidosInciales)
            this.ListaPedidos.AddRange(this.ListaPedidosInciales.Select(p => p.Copiar()).ToList());
            gvFacturasPedido.DataSource = this.ListaPedidos.OrderByDescending(P => P.Estatus).ThenByDescending(P => P.Id).ToList();
            CalculaTotales(this.ListaPedidos.Where(P => P.Estatus).ToList());
        }
        void CalculaTotales(List<EntPedido> ListaPedidos)
        {
            txtCantidadRegistros.Text = (ListaPedidos.Count).ToString();
            txtIEPS.Text = FormatoMoney(ListaPedidos.Sum(P => P.IEPS));
            txtTotalPedidos.Text = FormatoMoney(ListaPedidos.Sum(P => P.Total));
        }

        private void SeleccionaFactura_Load(object sender, EventArgs e)
        {
            try
            {
                dtpFechaDesdeVentas.Value = DateTime.Today.AddDays(-7);
                CargaTipoCliente((int)TipoCliente.CONVENIENCIA);
                CargaClientes((int)TipoCliente.CONVENIENCIA);
                CargaEstablecimientos();
                pnlFechasVentas.Enabled = true;
                pnlFechasVentas.Visible = false;
                pnlPedidos.Visible = false;
                gvClientes.BringToFront();
                gvFacturasPedido.ClearSelection();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void gvFacturasPedido_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
          
                ObtienePedidoFromGV(gvFacturasPedido).Estatus = !ObtienePedidoFromGV(gvFacturasPedido).Estatus;

                if (gvFacturasPedido.Rows.Count > 0)
                    CalculaTotales(this.ListaPedidos.Where(P => P.Estatus).ToList());

                gvFacturasPedido.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gvFacturasPedido_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //NO FUNCIONA AQUI
                //if (e.RowIndex >= 0)
                //{
                //    if (this.FacturasPedidoSeleccionados.Count == 0)
                //        this.versionCFDI = "";
                //}
                if(gvFacturasPedido.Rows.Count>0)
                    CalculaTotales(this.ListaPedidos.Where(P => P.Estatus).ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtClienteFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNumClienteFiltro.Text) && string.IsNullOrWhiteSpace(txtClienteFiltro.Text))
                    gvClientes.Visible = false;
                else
                {
                    List<EntCliente> clientesEncontrados = this.ListaClientes.Where(P => P.Nombre.ToUpper()
                                                                                            .Contains(txtClienteFiltro.Text.ToUpper())).ToList();
                    if (!string.IsNullOrWhiteSpace(txtNumClienteFiltro.Text))
                        clientesEncontrados = clientesEncontrados.Where(P => P.NumCliente.ToUpper()
                                                                                            .Contains(txtNumClienteFiltro.Text.ToUpper())).ToList();
                    gvClientes.DataSource = clientesEncontrados;
                    gvClientes.Visible = true;
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbTipoCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbTipoCliente.Focused)
                    CargaClientes(base.ObtieneCatalogoGenericoFromCmb(cmbTipoCliente).Id);
                gvFacturasPedido.ClearSelection();

                gvFacturasPedido.DataSource = null;
                gvFacturasPedido.DataSource = this.ListaPedidosInciales;
                gvFacturasPedido.Refresh();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        
        private void txtClienteFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    this.ClienteSeleccionado = ObtieneClienteFromGV(gvClientes);

                    if (this.ClienteSeleccionado != null)
                        CargaDatosCliente(this.ClienteSeleccionado);

                    base.OcultaBuscadorGrid(gvClientes, txtClienteFiltro);
                    txtNumClienteFiltro.Clear();
                }
                else if (e.KeyChar == (char)Keys.Escape)
                {
                    base.OcultaBuscadorGrid(gvClientes, txtClienteFiltro);
                    txtNumClienteFiltro.Clear();
                }

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                FiltroClientes vClientes = new FiltroClientes(this.ListaClientes);
                if (vClientes.ShowDialog() == DialogResult.OK)
                {
                    this.ClienteSeleccionado = vClientes.ClienteSeleccionado;
                    if (this.ClienteSeleccionado != null)
                        CargaDatosCliente(this.ClienteSeleccionado);
                    base.OcultaBuscadorGrid(gvClientes, txtClienteFiltro);
                    txtNumClienteFiltro.Clear();
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

                base.OcultaBuscadorGrid(gvClientes, txtClienteFiltro);
                txtNumClienteFiltro.Clear();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnRefrescarVentas_Click(object sender, EventArgs e)
        {
            try
            {
                CargaPedidos(this.EstableccimientoId,
                             dtpFechaDesdeVentas.Value, dtpFechaHastaVentas.Value,
                             this.ClienteSeleccionado.Id);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void SeleccionaPedidosCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.DialogResult == DialogResult.OK)
                {
                    if (this.ClienteSeleccionado == null || this.ClienteSeleccionado.Id <= 0)
                    {
                        MuestraMensajeError("SELECCIONE CLIENTE");
                        txtClienteFiltro.Focus();
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

    }
}
