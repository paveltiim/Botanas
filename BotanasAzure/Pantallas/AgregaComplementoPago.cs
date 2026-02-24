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

        public EntCliente Cliente { get; set; }
        List<EntFactura> ListaFacturas { get; set; }
        public decimal DeudaTotal { get { return ConvierteTextoADecimal(txtSaldoAnterior.Text); } set { txtSaldoAnterior.Text=FormatoMoney(value); } }
        
        public decimal CantidadPago { get { return ConvierteTextoADecimal(txtCantidadPago.Text); } set { txtCantidadPago.Text = FormatoMoney(value); } }
        public int FormaPagoId { get { return cmbFormaPago.SelectedIndex + 1; } }
        public string FormaPago { get { if(cmbFormaPago.Text.Length>4) 
                                            return cmbFormaPago.Text.Remove(0, 4);
                                        else 
                                            return "";
            } }
        public DateTime FechaPago { get { return dtpFechaPago.Value; } }
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
            base.SetWaitCursor();

            new UtiCorreo().EnviaCorreoPADE(new List<string>() { Cliente.Email, Cliente.Email2, Cliente.Email3 },
                                                       Program.EmpresaSeleccionada.NumeroReferencia, FacturaCP.UUID);
            MuestraMensaje("El Correo se ha Enviado correctamente, a la(s) dirección(es): \n\n " + Cliente.Email
                + " \n " + Cliente.Email2
                + " \n " + Cliente.Email3);
            base.SetDefaultCursor();

        }

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
            Cliente.CP = txtCP.Text;
            if (pnlDatosFacturacion40.Visible)
                Cliente.RegimenFiscalId = ObtieneCatalogoGenericoFromCmb(cmbRegimenFiscal).Id;

            string pathClienteDirectorio = PathFacturasComplementos + "\\" + Cliente.Nombre;
            if (!System.IO.Directory.Exists(pathClienteDirectorio))
                System.IO.Directory.CreateDirectory(pathClienteDirectorio);

            string uuid = "";
            int ultimoComplemento = new BusFacturas().ObtieneUltimoComplementoPago().Id;
            ultimoComplemento++;

            string pathClienteDirectorioFacturas = pathClienteDirectorio + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + "-CP "+ ultimoComplemento.ToString();
            //pathClienteDirectorioFacturas = @"C:\TIIM\Facturacion\Facturas\COMERCIALIZADORA SAN VALENTIN S DE RL DE CV\20220622013136-CP 4022";
            System.IO.Directory.CreateDirectory(pathClienteDirectorioFacturas);
            UtiFacturacion facturar = new UtiFacturacion();
            UtiFacturacionPruebas facturarPruebas = new UtiFacturacionPruebas();

            if (Program.ConexionIdActual == 1 && Program.EmpresaSeleccionada.Facturacion)//PRODUCCION
            {
                if (pnlDatosFacturacion40.Visible)
                {
                    if (chkRecalcularFactura.Checked)
                        uuid = facturar.FacturarComplementoPago40PADERecalculo(EmpresaSeleccionada, Cliente, ultimoComplemento.ToString(), DateTime.Now, FechaPago,
                                                            FormaPago, CantidadPago, Moneda, TipoCambio,
                                                            ListaFacturas, FacturasRelacionadas, pathClienteDirectorioFacturas);
                    else
                        uuid = facturar.FacturarComplementoPago40PADE(EmpresaSeleccionada, Cliente, ultimoComplemento.ToString(), DateTime.Now, FechaPago,
                                                            FormaPago, CantidadPago, Moneda, TipoCambio,
                                                            ListaFacturas, FacturasRelacionadas, pathClienteDirectorioFacturas);
                }
                //else
                //    uuid = facturar.FacturarComplementoPago(EmpresaSeleccionada, Cliente, ultimoComplemento.ToString(), DateTime.Now, FechaPago,
                //                                                FormaPago, CantidadPago, Moneda, TipoCambio,
                //                                                ListaFacturas, FacturasRelacionadas, pathClienteDirectorioFacturas);
            }
            else 
            {
                MuestraMensaje("FACTURACIÓN COMPLEMENTO DE PRUEBA");
                //uuid = facturarPruebas.FacturarComplementoPago(EmpresaSeleccionada, Cliente, ultimoComplemento.ToString(), DateTime.Now, FechaPago,
                //                                            FormaPago, CantidadPago, Moneda, TipoCambio,
                //                                            ListaFacturas, FacturasRelacionadas, pathClienteDirectorioFacturas);

                if (chkRecalcularFactura.Checked)
                    uuid = facturarPruebas.FacturarComplementoPago40PADERecalculo(EmpresaSeleccionada, Cliente, ultimoComplemento.ToString(), DateTime.Now, FechaPago,
                                                            FormaPago, CantidadPago, Moneda, TipoCambio,
                                                            ListaFacturas, FacturasRelacionadas, pathClienteDirectorioFacturas);
                else
                    uuid = facturarPruebas.FacturarComplementoPago40PADE(EmpresaSeleccionada, Cliente, ultimoComplemento.ToString(), DateTime.Now, FechaPago,
                                                            FormaPago, CantidadPago, Moneda, TipoCambio,
                                                            ListaFacturas, FacturasRelacionadas, pathClienteDirectorioFacturas);
                //uuid = facturarPruebas.FacturarComplementoPago33PADE(EmpresaSeleccionada, Cliente, ultimoComplemento.ToString(), DateTime.Now, FechaPago,
                //                                            FormaPago, CantidadPago, Moneda, TipoCambio,
                //                                            ListaFacturas, FacturasRelacionadas, pathClienteDirectorioFacturas);
            }
            //uuid = "cb3ea443-f195-49be-ac45-9f92e581a416";
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
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(pathClienteDirectorioFacturas);
            foreach (System.IO.FileInfo file in dir.GetFiles())
            {
                if (file.Extension == ".pdf")
                    comp.PDF = System.IO.File.ReadAllBytes(file.FullName);
                else
                    comp.XML = System.IO.File.ReadAllBytes(file.FullName);
            }
            return comp;
        }


        public void AgregarComplementoPago(int FacturaId, int PagoId, DateTime FechaComplemento, decimal PagoFactura, int FormaPagoId,
                                            string NumeroComplemento, decimal Pago, string UUID, string Ruta, byte[] PDF, byte[] XML)
        {
            EntFactura complemento = new EntFactura()
            {
                Id = FacturaId,
                FormaPagoId = FormaPagoId,
                Fecha = FechaComplemento,
                Pago = PagoFactura,
                TipoComprobanteId = 5,//PAGO
                NumeroFactura = NumeroComplemento,
                Saldo = Pago,
                UUID = UUID,
                Ruta = Ruta,
                PDF = PDF,
                XML = XML
            };
            new BusFacturas().AgregaComplementePago(Program.EmpresaSeleccionada.Id, PagoId, complemento);
        }

        void CargaDatosCliente(EntCliente Cliente)
        {
            txtEmail.Text = Cliente.Email;

            txtNombreFiscal.Text = Cliente.NombreFiscal;
            txtRFC.Text = Cliente.RFC;
            txtCP.Text = Cliente.CP;
            cmbRegimenFiscal.SelectedIndex = ((List<EntCatalogoGenerico>)cmbRegimenFiscal.DataSource).FindIndex(P => P.Id == Cliente.RegimenFiscalId);
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
                base.CargaCatalogoRegimen(cmbRegimenFiscal);
                cmbFormaPago.SelectedIndex = 0;

                CargaDatosCliente(this.Cliente);
                List<EntPedido> lst = new List<EntPedido>();
                CargaDatosFacturasPedido(lst);
                if (this.ListaFacturas.First().VersionCFDI == "3.3")
                    pnlDatosFacturacion40.Visible = false;
                if (txtRFC.Text == this.RfcPublicoGeneral)
                {
                    txtCP.Text = Program.EmpresaSeleccionada.CP;
                    txtCP.ReadOnly = true;
                    cmbRegimenFiscal.SelectedIndex = ((List<EntCatalogoGenerico>)cmbRegimenFiscal.DataSource).FindIndex(P => P.Id == 616);//SIN OBLIGACIONES FISCALES
                    cmbRegimenFiscal.Enabled = false;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public EntFactura ComplementoPago = new EntFactura();
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.ConexionIdActual == 2)
                    MuestraMensaje("FACTURACIÓN COMPLEMENTO DE PRUEBA");

                if (pnlDatosFacturacion40.Visible)
                {
                    if (string.IsNullOrEmpty(txtCP.Text.Trim()))
                    {
                        this.DialogResult = DialogResult.Abort;
                        MandaExcepcion("Ingrese C.P. del Cliente");
                    }
                    if (cmbRegimenFiscal.SelectedIndex < 0)
                    {
                        this.DialogResult = DialogResult.Abort;
                        MandaExcepcion("Seleccione Régimen Fiscal");
                    }
                }

                if (MuestraMensajeYesNo(string.Format("¿Desea enviar COMPLEMENTO DE PAGO? \n Cliente:{0}", txtNombreFiscal.Text), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    base.SetWaitCursor();

                    bool facturado = false;
                    
                    //string pahtArchivosFactura = "";
                    try
                    {
                        txtFormaPago.Text = cmbFormaPago.Text.Remove(2, cmbFormaPago.Text.Length - 2);

                        this.ComplementoPago = EnviarComplementoPago(Program.EmpresaSeleccionada, this.Cliente, this.ListaFacturas, txtFolio.Text,
                                                                dtpFechaPago.Value.Date, txtFormaPago.Text, ConvierteTextoADecimal(txtCantidadPago.Text),
                                                                1,"MXN", 0);
                        facturado = true;
                    }
                    catch (Exception ex)
                    {
                        this.DialogResult = DialogResult.Abort;
                        MuestraExcepcionFacturacion(ex);
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
                            try
                            {
                                string nombreArchivo = EncuentraArchivo(ComplementoPago.Ruta, ".pdf");
                                MuestraArchivo(this.ComplementoPago.Ruta, nombreArchivo);
                            }catch(Exception ex) { MuestraMensaje("ERROR AL MOSTRAR COMPLEMENTO DE PAGO \n" + ex.Message); }
                            EnviarCorreo(this.ComplementoPago, this.ListaFacturas, this.Cliente, ComplementoPago.Ruta);
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
            }
            catch (Exception ex) { this.DialogResult = DialogResult.Abort; MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
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

        private void AgregaComplementoPago_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.Abort)
                e.Cancel = true;
        }
    }
}
