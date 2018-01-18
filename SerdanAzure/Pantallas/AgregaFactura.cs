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
        
        public string NumeroFactura { get; set; }

        public string NumeroNotaCredito { get; set; }
        public EntFactura NotaCredito { get; set; }


        public void ActivaNotaCredito()
        {
            cmbMetodoPago.SelectedIndex = cmbMetodoPago.Items.Count - 1;
            cmbMetodoPago.Enabled = false;
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

        string ObtieneUltimaNotaCredito(int EmpresaId)
        {
            int ultimaFactura = ConvierteTextoAInteger(new BusPedidos().ObtieneUltimaNotaCredito(EmpresaId).NumeroFactura);
            ultimaFactura++;

            return ultimaFactura.ToString();
        }

        /// <summary>
        /// Muestra Ventana emergente para Confirmar Envio de Correo, llama los métodos Imprime.AsignaValoresParametrosImpresionDatosCliente y Imprime.AsignaValoresParametrosImpresion.
        /// Envia correo electronico por medio de la clase UtiCorreo.
        /// </summary>
        /// <param name="Pedido"></param>
        /// <param name="Cliente"></param>
        /// <param name="NotaVenta"></param>
        /// <param name="Presupuesto"></param>
        void EnviaCorreo(EntEmpresa Empresa, EntCliente Cliente, string PathArchivosFactura)
        {
            Cursor.Current = Cursors.WaitCursor;

            List<string> archivosAdjuntos = new List<string>();

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PathArchivosFactura);
            foreach (System.IO.FileInfo file in dir.GetFiles())
            {
                archivosAdjuntos.Add(file.FullName);
            }

            string asunto = "NOTA CRÉDITO -" + Empresa.NombreFiscal + "- " + DateTime.Today.ToString("dd MMM");
            string mensaje = "Apreciable " + Cliente.NombreFiscal + ", \n\n Le enviamos su debido comprobante fiscal solicitado, recordandole que estamos a sus ordenes para cualquier duda o aclaración. \n";
            mensaje += "\n Agradecemos su preferencia y esperamos seguirle atendiendo como se merece. \n";
            mensaje += "\n Atte. \n" + Empresa.NombreFiscal;

            if (Program.UsuarioSeleccionado.Id > 1)
            {
                string email4 = "refrigeracion_serdan@hotmail.com";
                if (Program.UsuarioSeleccionado.Id == 5)
                {//CASAR
                    string email5 = "martin_serdan@hotmail.com";
                    string email6 = "comercializadoracasar@hotmail.com";
                    new UtiCorreo().EnviaCorreo(asunto, new List<string>() { Cliente.Email, Cliente.Email2, Cliente.Email3, email4, email5, email6 }, mensaje, archivosAdjuntos);
                }
                else
                    new UtiCorreo().EnviaCorreo(asunto, new List<string>() { Cliente.Email, Cliente.Email2, Cliente.Email3, email4 }, mensaje, archivosAdjuntos);
            }
            else
                new UtiCorreo().EnviaCorreo(asunto, new List<string>() { Cliente.Email, Cliente.Email2, Cliente.Email3 }, mensaje, archivosAdjuntos);

            MessageBox.Show("El Correo se ha Enviado correctamente, a la(s) dirección(es): \n " + Cliente.Email + " \n " + Cliente.Email2 + " \n " + Cliente.Email3);
            //}
        }
        EntFactura EnviarNotaCredito(EntCliente Cliente,
                        DateTime FechaFactura, string NumeroNota,
                        string FormaPago, string MedioPago, string CondicionPago, string NumeroCuenta,
                        decimal Total, string Descripcion)
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
            UtiFacturacionPruebas facturarPruebas = new UtiFacturacionPruebas();

            // //ConviertePdfToImage(pathClienteDirectorioFacturas + "");
            ////ConviertePdfToImage(@"C:\TIIM\Facturacion\Facturas\RAFAEL GIL ARMENTA\20170105121745\64DD0F9F-A637-4BEA-A03B-D3DEEF45D8FE.pdf");

            //string folioFactura = "";
            factura.UUID = facturarPruebas.FacturarNotaCredito(Program.EmpresaSeleccionada, Total, Descripcion, NumeroNota, Cliente, FechaFactura, FormaPago, MedioPago, CondicionPago, NumeroCuenta, pathClienteDirectorioFacturas);
            factura.Ruta = pathClienteDirectorioFacturas;
            factura.NumeroFactura = NumeroNota;
            this.NumeroNotaCredito = NumeroNota;

            return factura;// pathClienteDirectorioFacturas;
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MuestraMensajeYesNo(string.Format("¿Seguro desea FACTURAR NOTA DE CRÉDITO? \n Cliente:{0}", txtNombreFiscal.Text), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    bool facturado = false;
                    //EntFactura factura = new EntFactura();
                    NotaCredito = new EntFactura();
                    try
                    {
                        //int ultimaFactura = ConvierteTextoAInteger(new BusPedidos().ObtieneUltimaNotaCredito(Program.EmpresaSeleccionada.Id).NumeroFactura);//new BusPedidos().ObtieneUltimaFactura().Id;
                        //int nuevaFactura = ultimaFactura++;
                        string ultimaNota = ObtieneUltimaNotaCredito(Program.EmpresaSeleccionada.Id);
                        NotaCredito = EnviarNotaCredito(Cliente, DateTime.Now, ultimaNota, txtFormaPago.Text, txtMetodoPago.Text, txtCondicionesPago.Text, txtNumeroCuenta.Text, ConvierteTextoADecimal(txtCantidad.Text), txtDescripcion.Text);

                        facturado = true;
                    }
                    catch (Exception ex)
                    {
                        MuestraExcepcion(ex, "Nota de Crédito NO Facturado");
                        this.DialogResult = DialogResult.Abort;
                    }
                    //COMENTAR EN PRODUCCION
                    //facturado = true;
                    Cursor.Current = Cursors.Default;
                    if (facturado)
                    {
                        MuestraMensaje("¡La NOTA DE CRÉDITO fue realizada satisfactoriamente!", "CONFIRMACIÓN");
                        ////uuid = "A620B7D2-028B-4749-ACC9-52CB772CC4C2";
                        //AgregarFacturaPedido(pedidoSeleccionado.Id, uuid, PathClienteDirectorioFacturas);

                        try
                        {
                            ////throw new Exception("error");
                            ////pahtArchivosFactura = PathClienteDirectorioFacturas;
                            ////pahtArchivosFactura = @"C:\TIIM\Facturacion\Facturas\RAFAEL GIL ARMENTA\20170105122126";
                            EnviaCorreo(Program.EmpresaSeleccionada, Cliente, NotaCredito.Ruta);
                        }
                        catch (Exception ex)
                        {
                            MuestraExcepcion(ex, "Correo NO enviado.");
                        }

                        try
                        {
                            //DESCUENTA TIMBRE
                            new BusEmpresas().AumentaTimbreEmpresa(Program.EmpresaSeleccionada.Id);
                            Program.EmpresaSeleccionada.TimbresEmpresa--;
                            Program.EmpresaSeleccionada.Timbres--;
                        }
                        catch (Exception ex) {

                        }
                         
                        string nombreArchivo = EncuentraArchivo(NotaCredito.Ruta, ".pdf");
                        MuestraArchivo(NotaCredito.Ruta, nombreArchivo);
                    }

                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex) {
                MuestraExcepcion(ex);
                this.DialogResult = DialogResult.Abort;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void AgregaDeuda_Load(object sender, EventArgs e)
        {
            try {
                cmbFormaPago.SelectedIndex = 0;
                cmbMetodoPago.SelectedIndex = 0;

                CargaDatosCliente(Cliente);
            } catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void AgregaFactura_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.DialogResult == DialogResult.Abort)
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }
    }
}
