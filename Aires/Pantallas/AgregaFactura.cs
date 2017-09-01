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
    public partial class AgregaFactura : FormBase
    {
        public AgregaFactura()
        {
            InitializeComponent();
        }
        public AgregaFactura(EntCliente Cliente)
        {
            InitializeComponent();

            this.Cliente = Cliente;
        }
        public EntCliente Cliente { get; set; }
        public decimal Cantidad { get { return ConvierteTextoADecimal(txtCantidad.Text); } }
        
        EntFactura EnviarNotaCredito(EntCliente Cliente, DateTime FechaFactura, string FormaPago, string MedioPago, string CondicionPago, string NumeroCuenta, decimal Total)
        {
            EntFactura factura = new EntFactura();
            if (Cliente == null)
                Cliente = new EntCliente();
            //Cliente.Nombre = txtNombre.Text;
            Cliente.NombreFiscal = txtNombreFiscal.Text;
            Cliente.RFC = txtRFC.Text;

            Cliente.NoExterior = txtNoExterior.Text;
            Cliente.NoInterior = txtNoInterior.Text;
            Cliente.Calle = txtCalle.Text;
            Cliente.Colonia = txtColonia.Text;
            Cliente.Localidad = txtLocalidad.Text;
            Cliente.Municipio = txtMunicipio.Text;
            Cliente.Estado = txtEstado.Text;
            Cliente.CP = txtCP.Text;

            Cliente.Email = txtEmail.Text;

            
            string pathClienteDirectorio = PathFacturas + "\\" + Cliente.Nombre;
            if (!System.IO.Directory.Exists(pathClienteDirectorio))
                System.IO.Directory.CreateDirectory(pathClienteDirectorio);

            string pathClienteDirectorioFacturas = pathClienteDirectorio + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss");
            System.IO.Directory.CreateDirectory(pathClienteDirectorioFacturas);
            UtiFacturacion facturar = new UtiFacturacion();

            // //ConviertePdfToImage(pathClienteDirectorioFacturas + "");
            ////ConviertePdfToImage(@"C:\TIIM\Facturacion\Facturas\RAFAEL GIL ARMENTA\20170105121745\64DD0F9F-A637-4BEA-A03B-D3DEEF45D8FE.pdf");
            
            string folioFactura = "";
            factura.UUID = facturar.FacturarNotaCredito( Total, folioFactura, Cliente, FechaFactura, FormaPago, MedioPago, CondicionPago, NumeroCuenta, pathClienteDirectorioFacturas);

            return factura;// pathClienteDirectorioFacturas;
        }
        /// <summary>
        /// Muestra Ventana emergente para Confirmar Envio de Correo, llama los métodos Imprime.AsignaValoresParametrosImpresionDatosCliente y Imprime.AsignaValoresParametrosImpresion.
        /// Envia correo electronico por medio de la clase UtiCorreo.
        /// </summary>
        /// <param name="Pedido"></param>
        /// <param name="Cliente"></param>
        /// <param name="NotaVenta"></param>
        /// <param name="Presupuesto"></param>
        void EnviaCorreo(EntPedido Pedido, EntCliente Cliente, string PathArchivosFactura)
        {
            //ConfirmaEnviaCorreo vConfirmaEnviaCorreo = new ConfirmaEnviaCorreo();
            //vConfirmaEnviaCorreo.CargaValores(Cliente);
            //if (vConfirmaEnviaCorreo.ShowDialog() == DialogResult.OK)
            //{
            Cursor.Current = Cursors.WaitCursor;

            List<string> archivosAdjuntos = new List<string>();

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PathArchivosFactura);
            foreach (System.IO.FileInfo file in dir.GetFiles())
            {
                archivosAdjuntos.Add(file.FullName);
            }

            string asunto = "FACTURA DISTRIBUIDORA LM";

            new UtiCorreo().EnviaCorreo("Distribuidora LM mirage - " + asunto, new List<string>() { Cliente.Email }, "Envío factura.", archivosAdjuntos);

            MessageBox.Show("El Correo se ha Enviado correctamente, a la dirección -" + Cliente.Email + "-");
            //}
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MuestraMensajeYesNo(string.Format("¿Seguro desea FACTURAR? \n Cliente:{0}", txtNombreFiscal.Text), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    bool facturado = false;
                    EntFactura factura = new EntFactura();
                    //string pahtArchivosFactura = "";
                    try
                    {
                        //throw new Exception("error");
                        int ultimaFactura = new BusPedidos().ObtieneUltimaFactura().Id;
                        int nuevaFactura = ultimaFactura++;
                        factura = EnviarNotaCredito(Cliente, DateTime.Now, txtFormaPago.Text, txtMetodoPago.Text, txtCondicionesPago.Text, txtNumeroCuenta.Text, ConvierteTextoADecimal(txtCantidad.Text));
                        factura.Id = nuevaFactura;
                        facturado = true;
                    }
                    catch (Exception ex)
                    {
                        MuestraExcepcion(ex, "Pedido NO Facturado");
                    }
                    //COMENTAR EN PRODUCCION
                    //facturado = true;
                    Cursor.Current = Cursors.Default;
                    if (facturado)
                    {
                        MuestraMensaje("¡El pedido fue FACTURADO satisfactoriamente!", "CONFIRMACIÓN PEDIDO FACTURADO");
                        ////uuid = "A620B7D2-028B-4749-ACC9-52CB772CC4C2";
                        //AgregarFacturaPedido(pedidoSeleccionado.Id, uuid, PathClienteDirectorioFacturas);

                        try
                        {
                            ////throw new Exception("error");
                            ////pahtArchivosFactura = PathClienteDirectorioFacturas;
                            ////pahtArchivosFactura = @"C:\TIIM\Facturacion\Facturas\RAFAEL GIL ARMENTA\20170105122126";
                            //EnviaCorreo(pedidoSeleccionado, cliente, PathClienteDirectorioFacturas);
                        }
                        catch (Exception ex)
                        {
                            MuestraExcepcion(ex, "Correo NO enviado.");
                        }
                    }

                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        void CargaDatosCliente(EntCliente Cliente)
        {
            txtEmail.Text = Cliente.Email;

            txtNombreFiscal.Text = Cliente.NombreFiscal;
            txtRFC.Text = Cliente.RFC;

            txtNoExterior.Text = Cliente.NoExterior;
            txtNoInterior.Text = Cliente.NoInterior;
            txtCalle.Text = Cliente.Calle;
            txtColonia.Text = Cliente.Colonia;
            txtLocalidad.Text = Cliente.Localidad;
            txtMunicipio.Text = Cliente.Municipio;
            txtEstado.Text = Cliente.Estado;
            txtCP.Text = Cliente.CP;
        }

        public void ActivaNotaCredito() {
            cmbMetodoPago.SelectedIndex = cmbMetodoPago.Items.Count - 1;
            cmbMetodoPago.Enabled = false;
        }

        private void AgregaDeuda_Load(object sender, EventArgs e)
        {
            try {
                cmbFormaPago.SelectedIndex = 0;
                cmbMetodoPago.SelectedIndex = 0;

                CargaDatosCliente(Cliente);
            } catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
