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
    public partial class AgregaComplementoPago : FormBase
    {
        public AgregaComplementoPago()
        {
            InitializeComponent();
        }
        public AgregaComplementoPago(EntFactura Factura)
        {
            InitializeComponent();

            this.Factura = Factura;
        }
        public EntFactura Factura { get; set; }
        public EntCliente Cliente { get; set; }
        public EntPedido PedidoFactura { get; set; }
        public decimal Cantidad { get { return ConvierteTextoADecimal(txtCantidadPago.Text); } }
        public bool CantidadEnabled { set { txtCantidadPago.Enabled = value; } }
        public int FormaPagoId { get { return cmbFormaPago.SelectedIndex + 1; } }

        //string PathFacturasComplementos = @"C:\TIIM\Facturacion\Facturas";

        /// <summary>
        /// Muestra Ventana emergente para Confirmar Envio de Correo, llama los métodos Imprime.AsignaValoresParametrosImpresionDatosCliente y Imprime.AsignaValoresParametrosImpresion.
        /// Envia correo electronico por medio de la clase UtiCorreo.
        /// </summary>
        /// <param name="Pedido"></param>
        /// <param name="Cliente"></param>
        /// <param name="NotaVenta"></param>
        /// <param name="Presupuesto"></param>
        void EnviarCorreo(EntPedido Pedido, EntCliente Cliente, string PathArchivosFactura)
        {
            Cursor.Current = Cursors.WaitCursor;
            EntEmpresa empresa = Program.EmpresaSeleccionada;
            List<string> archivosAdjuntos = new List<string>();

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PathArchivosFactura);
            foreach (System.IO.FileInfo file in dir.GetFiles())
            {
                archivosAdjuntos.Add(file.FullName);
            }

            //string asunto = "COMPLEMENTO PAGO - " + PedidoFactura.Factura + " - " ;
            string asunto = "COMPLEMENTO PAGO - FACTURA: " + PedidoFactura.Factura + " - " + empresa.NombreFiscal + "- " + DateTime.Today.ToString("dd MMM");
            string mensaje = "Apreciable " + Cliente.NombreFiscal + ", \n\n Le enviamos su debido comprobante fiscal solicitado, recordandole que estamos a sus ordenes para cualquier duda o aclaración. \n";
            mensaje += "\n Agradecemos su preferencia y esperamos seguirle atendiendo como se merece. \n";
            mensaje += "\n Atte. \n" + empresa.NombreFiscal;

            new UtiCorreo().EnviaCorreo(asunto, new List<string>() { Cliente.Email, Cliente.Email2, Cliente.Email3 }, mensaje, archivosAdjuntos);

            MessageBox.Show("El Correo se ha Enviado correctamente, a la(s) dirección(es): \n " + Cliente.Email + " \n " + Cliente.Email2 + " \n " + Cliente.Email3);
        }

        EntFactura EnviarComplementoPago(EntEmpresa EmpresaEmisor, EntCliente Cliente, EntPedido PedidoFactura,
                                        DateTime FechaPago, string FormaPago, decimal SaldoAnterior, decimal Total)
        {
            if (Cliente == null)
                Cliente = new EntCliente();
            ////Cliente.Nombre = txtNombre.Text;
            //Cliente.NombreFiscal = txtNombreFiscal.Text;
            //Cliente.RFC = txtRFC.Text;

            Cliente.Email = txtEmail.Text;


            string pathClienteDirectorio = PathFacturasComplementos + "\\" + Cliente.Nombre;
            if (!System.IO.Directory.Exists(pathClienteDirectorio))
                System.IO.Directory.CreateDirectory(pathClienteDirectorio);

            string pathClienteDirectorioFacturas = pathClienteDirectorio + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + "-CP";
            System.IO.Directory.CreateDirectory(pathClienteDirectorioFacturas);
            UtiFacturacion facturar = new UtiFacturacion();
            UtiFacturacionPruebas facturarPruebas = new UtiFacturacionPruebas();

            int ultimoComplemento = new BusPedidos().ObtieneUltimoComplementoPago().Id;
            ultimoComplemento++;

            //if (Program.ConexionIdActual == 1)//PRODUCCION
            facturar.FacturarComplementoPago(EmpresaEmisor, Cliente, ultimoComplemento.ToString(), DateTime.Now,
                                                        FechaPago, FormaPago, Total, PedidoFactura.UUID, PedidoFactura.Factura, SaldoAnterior, pathClienteDirectorioFacturas);
            //else if (Program.ConexionIdActual == 2)
            //{
            //    MessageBox.Show("FACTURACIÓN COMPLEMENTO DE PRUEBA");
            //    facturarPruebas.FacturarComplementoPago(Cliente, ultimoComplemento.ToString(), DateTime.Now,
            //                                                FechaPago, FormaPago, Total, PedidoFactura.UUID, PedidoFactura.Factura, SaldoAnterior, pathClienteDirectorioFacturas);
            //}
            return new EntFactura() { Id = ultimoComplemento, Fecha = DateTime.Today, Ruta = pathClienteDirectorioFacturas };// pathClienteDirectorioFacturas;
        }


        void AgregarComplementoPago(int FacturaId, int FormaPagoId, DateTime FechaComplemento, string Ruta)
        {
            EntFactura complemento = new EntFactura()
            {
                Id = FacturaId,
                FormaPagoId = FormaPagoId,
                Fecha = FechaComplemento,
                TipoComprobanteId = 5,//PAGO
                Ruta = Ruta
            };
            new BusPedidos().AgregaComplementePago(complemento);
        }

        void CargaDatosCliente(EntCliente Cliente)
        {
            txtEmail.Text = Cliente.Email;

            txtNombreFiscal.Text = Cliente.NombreFiscal;
            txtRFC.Text = Cliente.RFC;
        }
        void CargaDatosPedidoFactura(EntPedido PedidoFactura)
        {
            txtUUID.Text = PedidoFactura.UUID;
            txtFolio.Text = PedidoFactura.Factura;
            txtTotalFactura.Text = FormatoMoney(PedidoFactura.Total);
            txtSaldoAnterior.Text = FormatoMoney(PedidoFactura.Total - PedidoFactura.Pago);//FormatoMoney(PedidoFactura.Total - PedidoFactura.PagoTotal + PedidoFactura.Pago);
            //txtCantidadPago.Text = FormatoMoney(PedidoFactura.Pago);
            //txtSaldoPendiente.Text = FormatoMoney(PedidoFactura.Total - PedidoFactura.PagoTotal);
        }

        private void AgregaComplementoPago_Load(object sender, EventArgs e)
        {
            try
            {
                cmbFormaPago.SelectedIndex = 0;

                CargaDatosCliente(Cliente);
                CargaDatosPedidoFactura(PedidoFactura);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MuestraMensajeYesNo(string.Format("¿Desea enviar COMPLEMENTO DE PAGO? \n Cliente:{0}", txtNombreFiscal.Text), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    bool facturado = false;
                    EntFactura factura = new EntFactura();
                    //string pahtArchivosFactura = "";
                    try
                    {
                        txtFormaPago.Text = cmbFormaPago.Text.Remove(2, cmbFormaPago.Text.Length - 2);

                        factura = EnviarComplementoPago(Program.EmpresaSeleccionada, Cliente, PedidoFactura, dtpFechaPago.Value.Date, txtFormaPago.Text, ConvierteTextoADecimal(txtSaldoAnterior), ConvierteTextoADecimal(txtCantidadPago.Text));
                        facturado = true;
                    }
                    catch (Exception ex)
                    {
                        this.DialogResult = DialogResult.Abort;
                        MuestraExcepcion(ex, "Complemento NO Facturado");
                    }
                    //COMENTAR EN PRODUCCION
                    //facturado = true;
                    Cursor.Current = Cursors.Default;
                    if (facturado)
                    {
                        AgregarComplementoPago(factura.Id, cmbFormaPago.SelectedIndex + 1, factura.Fecha, factura.Ruta);
                        MuestraMensaje("¡El COMPLEMENTO fue FACTURADO satisfactoriamente!", "CONFIRMACIÓN COMPLEMENTO DE PAGO");
                        ////uuid = "A620B7D2-028B-4749-ACC9-52CB772CC4C2";

                        try
                        {
                            //throw new Exception("error");
                            //pahtArchivosFactura = PathClienteDirectorioFacturas;
                            //pahtArchivosFactura = @"C:\TIIM\Facturacion\Facturas\RAFAEL GIL ARMENTA\20170105122126";
                            EnviarCorreo(PedidoFactura, Cliente, factura.Ruta);
                        }
                        catch (Exception ex)
                        {
                            MuestraExcepcion(ex, "Correo NO enviado.");
                        }
                    }

                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex) { this.DialogResult = DialogResult.Abort; MuestraExcepcion(ex); }
        }

        private void txtSaldoAnterior_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtSaldoPendiente.Text = FormatoMoney(ConvierteTextoADecimal(txtSaldoAnterior) - ConvierteTextoADecimal(txtCantidadPago));
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }
    }
}
