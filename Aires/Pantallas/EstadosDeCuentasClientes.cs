using AiresEntidades;
using AiresNegocio;
using AiresUtilerias;
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
    public partial class EstadosDeCuentasClientes : FormBase
    {
        public EstadosDeCuentasClientes()
        {
            InitializeComponent();
        }

        List<EntPedido> ListaPedidos;

        public void CargaGvPedidosClientesCredito(int ClienteId)
        {
            ListaPedidos = new BusPedidos().ObtienePedidosClienteCredito(ClienteId);
            EntPedidoBindingSource.DataSource = ListaPedidos;
            rvPedidosDeudaPorCliente.RefreshReport();
        }
        public void CargaClientes()
        {
            ListaClientes = new BusClientes().ObtieneClientes().OrderBy(P => P.Nombre).ToList();
            gvClientes.DataSource = ListaClientes;
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        
        private void SeleccionaFactura_Load(object sender, EventArgs e)
        {
            try
            {
                CargaClientes();
                CargaGvPedidosClientesCredito(ClienteSeleccionado.Id);
                txtEmail.Text = ClienteSeleccionado.Email;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rvPedidosDeudaPorCliente_Load(object sender, EventArgs e)
        {

        }
        void EnviaCorreo(EntCliente Cliente, string PathArchivo)
        {
            Cursor.Current = Cursors.WaitCursor;

            List<string> archivosAdjuntos = new List<string>();

            System.IO.FileInfo arc = new System.IO.FileInfo(PathArchivo);
            if (!arc.Exists)
                throw new Exception("La ruta del archivo seleccionado a cambiado");
            
            archivosAdjuntos.Add(PathArchivo);
            
            string asunto = "ESTADO DE CUENTA - DISTRIBUIDORA LM";

            new UtiCorreo().EnviaCorreo("" + asunto, new List<string>() { Cliente.Email }, "Envio Estado de Cuenta", archivosAdjuntos);

            MessageBox.Show("El Correo se ha Enviado correctamente, a la dirección -" + Cliente.Email + "-");
            //}
        }

        private void btnEnviaCorreo_Click(object sender, EventArgs e)
        {
            try
            {   if (MuestraMensajeYesNo(string.Format("¿Seguro desea enviar el Estado de Cuenta al correo seleccionado? \n Cliente:{0} \n Email:{1}", ClienteSeleccionado.Nombre, txtEmail.Text), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    ClienteSeleccionado.Email = txtEmail.Text;
                    try
                    {
                        //throw new Exception("error");
                        //pahtArchivosFactura = PathClienteDirectorioFacturas;
                        //pahtArchivosFactura = @"C:\TIIM\Facturacion\Facturas\RAFAEL GIL ARMENTA\20170105122126";
                        EnviaCorreo(ClienteSeleccionado, txtAdjuntaArchivo.Text);
                    }
                    catch (Exception ex)
                    {
                        MuestraExcepcion(ex, "Correo NO enviado.");
                    }

                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdjuntaArchivo_Click(object sender, EventArgs e)
        {
            if (ofdBuscaArchivo.ShowDialog() == DialogResult.OK)
            {
                txtAdjuntaArchivo.Text = ofdBuscaArchivo.FileName;

                MuestraArchivo(ofdBuscaArchivo.FileName, true);
            }
        }

        EntCliente ClienteSeleccionado = new EntCliente();
        List<EntCliente> ListaClientes = new List<EntCliente>();
        void CargaDatosCliente(EntCliente Cliente)
        {
            txtNombre.Text = Cliente.Nombre;
            txtEmail.Text = Cliente.Email;
            
            CargaGvPedidosClientesCredito(Cliente.Id);
        }
        void OcultaBuscadorGrid(DataGridView Grid, TextBox TxtBuscador)
        {
            Grid.Visible = false;
            TxtBuscador.Text = "";
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                FiltroClientes vClientes = new FiltroClientes();
                if (vClientes.ShowDialog() == DialogResult.OK)
                {
                    ClienteSeleccionado = vClientes.ClienteSeleccionado;
                    CargaDatosCliente(ClienteSeleccionado);
                    OcultaBuscadorGrid(gvClientes, txtClienteBusqueda);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        void AjustaAlturaGrid(DataGridView Grid, List<EntCliente> ListaClientes, int AlturaMaxima)
        {
            int altura = 25 + (28 * ListaClientes.Count);
            if (altura > AlturaMaxima)
                altura = AlturaMaxima;

            Grid.Height = altura;
        }

        private void txtClienteBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtClienteBusqueda.Text))
                    gvClientes.Visible = false;
                else
                {
                    List<EntCliente> clientesEncontrados = ListaClientes.Where(P => P.Nombre.ToUpper().Contains(txtClienteBusqueda.Text.ToUpper())).ToList();
                    gvClientes.DataSource = clientesEncontrados;
                    gvClientes.Visible = true;

                    AjustaAlturaGrid(gvClientes, clientesEncontrados, AlturaMaximaGrid);
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
                    //EntCliente clienteSeleccionado = ObtieneClienteFromGV(gvClientes);
                    ClienteSeleccionado = ObtieneClienteFromGV(gvClientes);

                    if (ClienteSeleccionado != null)
                    {
                        CargaDatosCliente(ClienteSeleccionado);
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

        private void gvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ClienteSeleccionado = ObtieneClienteFromGV(gvClientes);

                if (ClienteSeleccionado != null)
                {
                    CargaDatosCliente(ClienteSeleccionado);
                }

                OcultaBuscadorGrid(gvClientes, txtClienteBusqueda);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }
    }
}
