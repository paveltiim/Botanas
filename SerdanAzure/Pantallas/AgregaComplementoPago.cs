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
        //public AgregaComplementoPago(EntFactura Factura)
        //{
        //    InitializeComponent();

        //    this.Factura = Factura;
        //}
        public AgregaComplementoPago(List<EntFactura> ListaFacturas, EntCliente Cliente, decimal DeudaTotal, decimal Pago)
        {
            InitializeComponent();
            this.ListaFacturas = ListaFacturas;
            this.Cliente = Cliente;
            this.DeudaTotal = DeudaTotal;
            this.CantidadPago = Pago;
        }

        public EntFactura Factura { get; set; }
        public EntCliente Cliente { get; set; }
        public EntPedido PedidoFactura { get; set; }
        List<EntFactura> ListaFacturas { get; set; }
        public decimal DeudaTotal { get { return ConvierteTextoADecimal(txtSaldoAnterior.Text); } set { txtSaldoAnterior.Text=FormatoMoney(value); } }
        
        public decimal CantidadPago { get { return ConvierteTextoADecimal(txtCantidadPago.Text); } set { txtCantidadPago.Text = FormatoMoney(value); } }
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
        void EnviarCorreo(EntFactura FacturaCP, List<EntFactura> ListaFacturas, EntCliente Cliente, string PathArchivosFactura)
        {
            Cursor.Current = Cursors.WaitCursor;
            EntEmpresa empresa = Program.EmpresaSeleccionada;
            List<string> archivosAdjuntos = new List<string>();

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PathArchivosFactura);
            foreach (System.IO.FileInfo file in dir.GetFiles())
            {
                archivosAdjuntos.Add(file.FullName);
            }

            string facturas = "";
            foreach (EntFactura f in ListaFacturas)
            {
                facturas += f.NumeroFactura+ " | ";
            }
            string asunto = "COMPLEMENTO PAGO: CP-"+ FacturaCP.NumeroFactura+"; FACTURA(s): " + facturas + " - " + empresa.NombreFiscal + "- " + DateTime.Today.ToString("dd MMM");
            string mensaje = "Apreciable " + Cliente.NombreFiscal + ", \n\n Le enviamos su debido comprobante fiscal solicitado, recordandole que estamos a sus ordenes para cualquier duda o aclaración. \n";
            mensaje += "\n Agradecemos su preferencia y esperamos seguirle atendiendo como se merece. \n";
            mensaje += "\n Atte. \n" + empresa.NombreFiscal;

            new UtiCorreo().EnviaCorreo(asunto, new List<string>() { Cliente.Email, Cliente.Email2, Cliente.Email3 }, mensaje, archivosAdjuntos);

            MessageBox.Show("El Correo se ha Enviado correctamente, a la(s) dirección(es): \n " + Cliente.Email + " \n " + Cliente.Email2 + " \n " + Cliente.Email3);
        }

        //EntFactura EnviarComplementoPago(EntEmpresa EmpresaEmisor, EntCliente Cliente, EntPedido PedidoFactura,
        //                                DateTime FechaPago, string FormaPago, decimal SaldoAnterior,
        //                                decimal MontoPago, string NumParcialidad)
        //{
        //    if (Cliente == null)
        //        Cliente = new EntCliente();
        //    //Cliente.Nombre = txtNombre.Text;
        //    Cliente.NombreFiscal = txtNombreFiscal.Text;
        //    Cliente.RFC = txtRFC.Text;

        //    Cliente.Email = txtEmail.Text;


        //    string pathClienteDirectorio = PathFacturasComplementos + "\\" + Cliente.Nombre;
        //    if (!System.IO.Directory.Exists(pathClienteDirectorio))
        //        System.IO.Directory.CreateDirectory(pathClienteDirectorio);

        //    string pathClienteDirectorioFacturas = pathClienteDirectorio + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + "-CP";
        //    System.IO.Directory.CreateDirectory(pathClienteDirectorioFacturas);
        //    UtiFacturacion facturar = new UtiFacturacion();
        //    UtiFacturacionPruebas facturarPruebas = new UtiFacturacionPruebas();

        //    string uuid = "";
        //    int ultimoComplemento = new BusFacturas().ObtieneUltimoComplementoPago().Id;
        //    ultimoComplemento++;

        //    if (Program.ConexionIdActual == 1)//PRODUCCION
        //        uuid = facturar.FacturarComplementoPago(EmpresaEmisor, Cliente, ultimoComplemento.ToString(), DateTime.Now,
        //                                                    FechaPago, FormaPago, PedidoFactura.UUID, EmpresaEmisor.SerieFactura, PedidoFactura.Factura,
        //                                                    PedidoFactura.Total, SaldoAnterior, MontoPago, NumParcialidad, pathClienteDirectorioFacturas);
        //    else if (Program.ConexionIdActual == 2)
        //    {
        //        uuid = facturarPruebas.FacturarComplementoPago(EmpresaEmisor, Cliente, ultimoComplemento.ToString(), DateTime.Now,
        //                                                    FechaPago, FormaPago, PedidoFactura.UUID, EmpresaEmisor.SerieFactura, PedidoFactura.Factura,
        //                                                    PedidoFactura.Total, SaldoAnterior, MontoPago,
        //                                                    NumParcialidad, pathClienteDirectorioFacturas);
        //    }
        //    return new EntFactura() { Id = ultimoComplemento, Fecha = DateTime.Today, UUID = uuid, Ruta = pathClienteDirectorioFacturas };// pathClienteDirectorioFacturas;
        //}

        EntFactura EnviarComplementoPago(EntEmpresa EmpresaSeleccionada, EntCliente Cliente, List<EntFactura> ListaFacturas, string FacturasRelacionadas,
                                        DateTime FechaPago, string FormaPago, decimal CantidadPago, int TipoMonedaId, string Moneda, decimal TipoCambio)
        {
            if (Cliente == null)
                Cliente = new EntCliente();
            ////Cliente.Nombre = txtNombre.Text;
            //Cliente.NombreFiscal = txtNombreFiscal.Text;

            Cliente.Email = txtEmail.Text;
            //Cliente.Email2 = txtEmail2.Text;
            //Cliente.Email3 = txtEmail3.Text;
            Cliente.Nombre = txtNombreFiscal.Text;
            Cliente.RFC = txtRFC.Text;

            string pathClienteDirectorio = PathFacturasComplementos + "\\" + Cliente.Nombre;
            if (!System.IO.Directory.Exists(pathClienteDirectorio))
                System.IO.Directory.CreateDirectory(pathClienteDirectorio);

            string pathClienteDirectorioFacturas = pathClienteDirectorio + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + "-CP";
            System.IO.Directory.CreateDirectory(pathClienteDirectorioFacturas);
            UtiFacturacion facturar = new UtiFacturacion();
            UtiFacturacionPruebas facturarPruebas = new UtiFacturacionPruebas();

            string uuid = "";
            int ultimoComplemento = new BusFacturas().ObtieneUltimoComplementoPago().Id;
            ultimoComplemento++;

            if (Program.ConexionIdActual == 1)//PRODUCCION
            {
                uuid = facturar.FacturarComplementoPago(EmpresaSeleccionada, Cliente, ultimoComplemento.ToString(), DateTime.Now, FechaPago,
                                                            FormaPago, CantidadPago, Moneda, TipoCambio,
                                                            ListaFacturas, FacturasRelacionadas, pathClienteDirectorioFacturas);
            }
            else if (Program.ConexionIdActual == 2)
            {
                MessageBox.Show("FACTURACIÓN COMPLEMENTO DE PRUEBA");
                uuid = facturarPruebas.FacturarComplementoPago(EmpresaSeleccionada, Cliente, ultimoComplemento.ToString(), DateTime.Now, FechaPago,
                                                            FormaPago, CantidadPago, Moneda, TipoCambio,
                                                            ListaFacturas, FacturasRelacionadas, pathClienteDirectorioFacturas);
            }

            EntFactura comp = new EntFactura();
            comp = new EntFactura()
            {
                Id = ultimoComplemento,
                NumeroFactura = ultimoComplemento.ToString(),
                Fecha = DateTime.Today,
                Pago = CantidadPago,
                UUID = uuid,
                Ruta = pathClienteDirectorioFacturas,
                //MonedaId = TipoMonedaId,
                //TipoCambio = TipoCambio,
                //UsuarioId = this.UsuarioLogin.Id
            };
            //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(pathClienteDirectorioFacturas);
            //foreach (System.IO.FileInfo file in dir.GetFiles())
            //{
            //    if (file.Extension == ".pdf")
            //        comp.PDF = System.IO.File.ReadAllBytes(file.FullName);
            //    else
            //        comp.XML = System.IO.File.ReadAllBytes(file.FullName);
            //}
            return comp;
        }


        public void AgregarComplementoPago(int FacturaId, DateTime FechaComplemento, decimal PagoFactura, int FormaPagoId, 
                                            string NumeroComplemento, decimal Pago, string UUID, string Ruta)
        {
            EntFactura complemento = new EntFactura()
            {
                Id = FacturaId,
                FormaPagoId = FormaPagoId,
                Fecha = FechaComplemento,
                Pago = Pago,
                TipoComprobanteId = 5,//PAGO
                NumeroFactura= NumeroComplemento,
                Saldo= Pago,
                UUID =UUID,
                Ruta = Ruta
            };
            new BusFacturas().AgregaComplementePago(complemento);
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
            txtSaldoAnterior.Text = FormatoMoney(PedidoFactura.Total - PedidoFactura.PagoTotal);
            txtCantidadPago.Text = FormatoMoney(PedidoFactura.Pago);
            txtSaldoPendiente.Text = FormatoMoney(PedidoFactura.Total - PedidoFactura.PagoTotal - PedidoFactura.Pago);
            txtNumParcialidad.Text = (new BusFacturas().ObtieneNumeroParcialidades(PedidoFactura.FacturaId)+1).ToString();
        }
        void CargaDatosFacturasPedido(List<EntPedido> ListaFacturasPedido)
        {
            //txtUUID.Text = PedidoFactura.UUID;
            foreach (EntFactura f in this.ListaFacturas)
            {
                txtFolio.Text += f.NumeroFactura + " | ";
            }

            //txtTotalFactura.Text = FormatoMoney(PedidoFactura.Total);
            //txtSaldoAnterior.Text = FormatoMoney(PedidoFactura.Total - PedidoFactura.PagoTotal);
            txtSaldoPendiente.Text = FormatoMoney(this.DeudaTotal - this.CantidadPago);
            //txtCantidadPago.Text = FormatoMoney(PedidoFactura.Pago);
            //txtSaldoPendiente.Text = FormatoMoney(PedidoFactura.Total - PedidoFactura.PagoTotal - PedidoFactura.Pago);
            //txtNumParcialidad.Text = (new BusFacturas().ObtieneNumeroParcialidades(PedidoFactura.FacturaId) + 1).ToString();
        }



        private void AgregaComplementoPago_Load(object sender, EventArgs e)
        {
            try
            {
                cmbFormaPago.SelectedIndex = 0;

                CargaDatosCliente(Cliente);
                List<EntPedido> lst = new List<EntPedido>();
                CargaDatosFacturasPedido(lst);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public EntFactura ComplementoPago = new EntFactura();
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.ConexionIdActual == 2)
                    MessageBox.Show("FACTURACIÓN COMPLEMENTO DE PRUEBA");

                if (MuestraMensajeYesNo(string.Format("¿Desea enviar COMPLEMENTO DE PAGO? \n Cliente:{0}", txtNombreFiscal.Text), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    bool facturado = false;
                    
                    //string pahtArchivosFactura = "";
                    try
                    {
                        txtFormaPago.Text = cmbFormaPago.Text.Remove(2, cmbFormaPago.Text.Length - 2);

                        ComplementoPago = EnviarComplementoPago(Program.EmpresaSeleccionada, Cliente, this.ListaFacturas, txtFolio.Text,
                                                                dtpFechaPago.Value.Date, txtFormaPago.Text, ConvierteTextoADecimal(txtCantidadPago.Text),
                                                                1,"MXN", 0);
                        facturado = true;
                    }
                    catch (Exception ex)
                    {
                        this.DialogResult = DialogResult.Abort;
                        MuestraExcepcion(ex, "Complemento NO Facturado");
                    }
                    //COMENTAR EN PRODUCCION
                    //facturado = true;
                    if (facturado)
                    {
                        //AgregarComplementoPago(this.Factura.Id, factura.Fecha, ConvierteTextoADecimal(txtCantidadPago.Text), cmbFormaPago.SelectedIndex + 1, factura.UUID, factura.Ruta);
                        //MuestraMensaje("¡El COMPLEMENTO fue FACTURADO satisfactoriamente!", "CONFIRMACIÓN COMPLEMENTO DE PAGO");
                        //////uuid = "A620B7D2-028B-4749-ACC9-52CB772CC4C2";
                        MuestraMensaje("¡El COMPLEMENTO fue FACTURADO satisfactoriamente!", "CONFIRMACIÓN COMPLEMENTO DE PAGO");
                        try
                        {
                            //throw new Exception("error");
                            //pahtArchivosFactura = PathClienteDirectorioFacturas;
                            //pahtArchivosFactura = @"C:\TIIM\Facturacion\Facturas\RAFAEL GIL ARMENTA\20170105122126";
                            EnviarCorreo(ComplementoPago, this.ListaFacturas, Cliente, ComplementoPago.Ruta);
                        }
                        catch (Exception ex)
                        {
                            MuestraExcepcion(ex, "Correo NO enviado.");
                        }
                    }

                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
                Cursor.Current = Cursors.Default;

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
