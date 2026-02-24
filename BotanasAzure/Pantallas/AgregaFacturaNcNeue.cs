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
    public partial class AgregaFacturaNcNeue : FormBase
    {
        List<EntFactura> ListaFacturas { get; set; }
        public EntCliente Cliente { get; set; }
        public EntEmpresa EmpresaSeleccionada { get; set; }

        public EntFactura NotaCredito = new EntFactura();

        public AgregaFacturaNcNeue(List<EntFactura> ListaFacturas, EntCliente Cliente, decimal Total)
        {
            InitializeComponent();

            this.ListaFacturas = ListaFacturas;
            this.Cliente = Cliente;
            this.Total = Total;
            this.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(ListaFacturas.First().EmpresaId);
        }

        public decimal Total { get { return ConvierteTextoADecimal(txtTotalNC.Text); } set { txtTotalNC.Text = FormatoMoney(value); } }

        public string UUID { get; set; }
        public string NumeroFactura { get; set; }
        public int FormaPagoId { get; set; }
        public string NumeroNotaCredito { get; set; }

        #region Metodos
        public void ActivaNotaCredito()
        {
            cmbMetodoPago.SelectedIndex = cmbMetodoPago.Items.Count - 1;
            cmbMetodoPago.Enabled = false;
        }

        void CargaDatosCliente(EntCliente Cliente)
        {
            txtNombreFiscal.Text = Cliente.NombreFiscal;
            txtRFC.Text = Cliente.RFC;
            txtCP.Text = Cliente.CP;
            txtEmail.Text = Cliente.Email;
            txtEmail2.Text = Cliente.Email2;
            txtEmail3.Text = Cliente.Email3;

            txtNombreFiscal.Text = Cliente.NombreFiscal;
            txtRFC.Text = Cliente.RFC;
            txtCP.Text = Cliente.CP;
            cmbRegimenFiscal.SelectedIndex = ((List<EntCatalogoGenerico>)cmbRegimenFiscal.DataSource).FindIndex(P => P.Id == Cliente.RegimenFiscalId);

            if (txtRFC.Text == this.RfcPublicoGeneral)
            {
                cmbUsoCFDI.SelectedIndex = cmbUsoCFDI.Items.Count - 1;

                txtCP.Text = this.EmpresaSeleccionada.CP;//Program.EmpresaSeleccionada.CP;
                txtCP.ReadOnly = true;
                cmbRegimenFiscal.SelectedIndex = ((List<EntCatalogoGenerico>)cmbRegimenFiscal.DataSource).FindIndex(P => P.Id == 616);//SIN OBLIGACIONES FISCALES
                cmbRegimenFiscal.Enabled = false;
                cmbUsoCFDI.Enabled = false;
            }
        }

        public string ObtieneUltimaNotaCredito(int EmpresaId)
        {
            int ultimaFactura = ConvierteTextoAInteger(new BusPedidos().ObtieneUltimaNotaCredito(EmpresaId).NumeroFactura);
            ultimaFactura++;

            return ultimaFactura.ToString();
        }
        string PathClienteDirectorioFacturas { get; set; }
        public EntFactura TimbrarFacturaNC(EntEmpresa EmpresaSeleccionada, decimal Total, EntCliente Cliente, string UUIDFacturaRelacionada,
                                DateTime FechaFactura,
                                string FormaPago, string MedioPago, string CondicionPago, string NumeroCuenta,
                                decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS,
                                decimal ImpuestoRetenido,
                                string Descripcion, string Observaciones,
                                string UsoCFDI, string TipoNC, bool Timbrar, bool CargaDatosCliente)
        {
            if (CargaDatosCliente)
            {
                if (Cliente == null)
                {
                    Cliente = new EntCliente();
                    Cliente.EmpresaId = EmpresaSeleccionada.Id;
                }
                //Cliente.Nombre = txtNombre.Text;
                Cliente.NombreFiscal = txtNombreFiscal.Text;
                Cliente.RFC = txtRFC.Text;
                Cliente.RFC = txtRFC.Text;
                Cliente.CP = txtCP.Text;
                Cliente.Email = txtEmail.Text;
                Cliente.Email2 = txtEmail2.Text;
                Cliente.Email3 = txtEmail3.Text;
                Cliente.RegimenFiscalId = ObtieneCatalogoGenericoFromCmb(cmbRegimenFiscal).Id;
            }

            UtiFacturacion factura = new UtiFacturacion();
            UtiFacturacionPruebas facturaPruebas = new UtiFacturacionPruebas();

            string pathPreFacturasBase = @"C:\TIIM\Facturacion\PreFacturas";
            string pathFacturasBase = @"C:\TIIM\Facturacion\Facturas";

            string uuid = "";

            string ultimaNota = ObtieneUltimaNotaCredito(EmpresaSeleccionada.Id);
            string serieNC = "NC";
            if (Program.ConexionIdActual == 1 && Timbrar && EmpresaSeleccionada.Facturacion)//PRODUCCION
            {
                //this.PathClienteDirectorioFacturas = CreaPathClienteDirectorioFacturas(pathFacturasBase, Cliente.Nombre, Program.EmpresaSeleccionada.SerieFactura, Pedido.Factura);
                this.PathClienteDirectorioFacturas = base.CreaPathClienteDirectorioFacturas(pathFacturasBase, Cliente.NombreFiscal, serieNC, ultimaNota);
                base.EliminaArchivosDireccion(this.PathClienteDirectorioFacturas);

                if (MuestraMensajeYesNo("¿DESEA TIMBRAR ANTE SAT? ") == DialogResult.No)
                    MandaExcepcion("TIMBRADO NO AUTORIZADO");
                uuid = factura.FacturarNotaDeCredito40PADE(EmpresaSeleccionada, Total, Descripcion,
                                                Cliente, UUIDFacturaRelacionada,
                                                serieNC, ultimaNota, FechaFactura,
                                                FormaPago, MedioPago, CondicionPago, NumeroCuenta,
                                                //CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, ImpuestoRetenido,
                                                this.PathClienteDirectorioFacturas, CantidadIVA, CantidadIEPS,
                                                UsoCFDI, TipoNC,
                                                Observaciones);
            }
            else
            {
                this.PathClienteDirectorioFacturas = base.CreaPathClienteDirectorioFacturas(pathPreFacturasBase, Cliente.NombreFiscal, serieNC, ultimaNota);
                base.   EliminaArchivosDireccion(this.PathClienteDirectorioFacturas);

                if (MuestraMensajeYesNo("¿DESEA TIMBRAR? - FACTURACIÓN DE PRUEBA -") == DialogResult.No)
                    MandaExcepcion("TIMBRADO NO AUTORIZADO");
                uuid = facturaPruebas.FacturarNotaDeCredito40PADE(EmpresaSeleccionada, Total, Descripcion,
                                                Cliente, UUIDFacturaRelacionada,
                                                serieNC, ultimaNota, FechaFactura,
                                                FormaPago, MedioPago, CondicionPago, NumeroCuenta,
                                                //CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, ImpuestoRetenido,
                                                this.PathClienteDirectorioFacturas, CantidadIVA, CantidadIEPS,
                                                UsoCFDI, TipoNC,
                                                Observaciones);

            }
            EntFactura fact = new EntFactura()
            {
                //PedidoId = Pedido.Id,
                Total = Total,
                Pago = Total,
                SerieFactura = serieNC,
                NumeroFactura = ultimaNota,
                UUID = uuid,
                Ruta = this.PathClienteDirectorioFacturas,
                Fecha = DateTime.Today
            };
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(this.PathClienteDirectorioFacturas);
            foreach (System.IO.FileInfo file in dir.GetFiles())
            {
                if (file.Extension == ".pdf")
                {
                    //UtiPDF modiPDF = new UtiPDF();
                    //string nota1 = "VENDEDOR: " + cmbTrabajadores.Text.Replace("-SELECCIONE-", "");
                    //string nota2 = "SUCURSAL: " + txtSucursal.Text;
                    //string nota3 = "";//"Dirección: " + txtDireccion.Text;                    
                    //string nota4 = "NOTA: FAVOR DE SOLICITAR LAS DEVOLUCIONES AL MOMENTO DE HACER UN PEDIDO Y NO AL MOMENTO DE QUE ESTEN ENTREGANDO PRODUCTO. "
                    //                + "NO HABRÁ DEVOLUCIONES DESPUÉS DE 8 DÍAS DE PEDIDO.";// "Dirección: " + txtDireccion.Text;

                    //if (ListaProductos.Count > 40)
                    //    nota4 = "";
                    //if (chkSinSucursal.Checked)
                    //    nota2 = "";

                    //string rutaArchivoPDF = file.FullName;
                    //modiPDF.ModificaPDF(nota1, nota2, nota3, nota4, rutaArchivoPDF, "1", 1);
                    fact.PDF = System.IO.File.ReadAllBytes(file.FullName);
                }
                else
                    fact.XML = System.IO.File.ReadAllBytes(file.FullName);
            }
            return fact;// pathClienteDirectorioFacturas;
        }

        #endregion


        private void AgregaDeuda_Load(object sender, EventArgs e)
        {
            try
            {
                base.CargaCatalogoRegimen(cmbRegimenFiscal);
                base.CargaCatalogoUsoCFDI(cmbUsoCFDI);
                cmbUsoCFDI.SelectedIndex = ((List<EntCatalogoGenerico>)cmbUsoCFDI.DataSource).FindIndex(P => P.Clave == "G02");
                cmbFormaPago.SelectedIndex = cmbFormaPago.FindString("99");
                cmbMetodoPago.SelectedIndex = 0;
                CargaDatosCliente(Cliente);
                foreach (EntFactura f in this.ListaFacturas)
                {
                    txtObservaciones.Text += f.NumeroFactura + " | ";
                }
                txtDescripcionNC.Text+="("+ txtObservaciones.Text.Remove(txtObservaciones.Text.Length-3) +")";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public bool FacturarNC(EntEmpresa EmpresaSeleccionada,EntCliente ClienteSeleccionado, int PedidoId, int FacturaId, string UUIDsRelacionados, decimal Total,
                       decimal CantidadIVA, decimal CantidadIVARetenido, decimal CantidadISRRetenido, decimal CantidadIEPS,
                       decimal ImpuestoRetenido,
                       string Descripcion, string Observaciones, bool Timbrar, bool PreFactura)
        {
            base.SetWaitCursor();

            bool facturado = false;
            EntFactura notaCredito = new EntFactura();

            try
            {
                txtFormaPago.Text = cmbFormaPago.Text.Remove(2, cmbFormaPago.Text.Length - 2);
                txtMetodoPago.Text = cmbMetodoPago.Text.Remove(3);
                txtUsoCFDI.Text = cmbUsoCFDI.Text.Remove(3);

                notaCredito = this.TimbrarFacturaNC(EmpresaSeleccionada, Total, ClienteSeleccionado, UUIDsRelacionados, DateTime.Now,
                                    txtFormaPago.Text, txtMetodoPago.Text, "", "",
                                    CantidadIVA, CantidadIVARetenido, CantidadISRRetenido, CantidadIEPS, ImpuestoRetenido,
                                    Descripcion, Observaciones,
                                    txtUsoCFDI.Text, "01", Timbrar, true);
                notaCredito.EmpresaId = EmpresaSeleccionada.Id;
                notaCredito.PedidoId = PedidoId;
                notaCredito.Id = FacturaId;
                notaCredito.TipoComprobanteId = 2;//1:I-INGRESO; 2:E-EGRESO.
                notaCredito.Total = Total;
                notaCredito.Pago = Total;
                notaCredito.Descripcion = Descripcion;// + " | " + Observaciones;
                notaCredito.FormaPagoId = ConvierteTextoAInteger(txtFormaPago.Text);
                notaCredito.MetodoPagoId = cmbMetodoPago.SelectedIndex + 1;
                notaCredito.MonedaId = 1;
                notaCredito.UsoCFDIId = ObtieneCatalogoGenericoFromCmb(cmbUsoCFDI).Id;
                notaCredito.UsuarioId = Program.UsuarioSeleccionado.Id;

                facturado = true;
            }
            catch (Exception ex)
            {
                MandaExcepcion(ex.Message + " \n\n- Nota de Crédito NO Facturado");
            }

            if (facturado)
            {
                if (!PreFactura)
                {
                    try
                    {
                        //notaCredito.Id = new BusFacturas().AgregaFactura(notaCredito);
                        new BusFacturas().AgregaNotaCredito(notaCredito);
                        foreach (EntFactura f in this.ListaFacturas)
                        {
                            new BusPedidos().AumentaNotaCreditoPedido(f.PedidoId, ConvierteTextoADecimal(txtTotalNC), Program.UsuarioSeleccionado.Id);
                        }
                        MuestraMensaje("¡La NOTA DE CRÉDITO fue realizada satisfactoriamente!", "CONFIRMACIÓN");

                        //foreach (EntPedido pe in this.ListaPedidos)
                        //{
                        //    //new BusPedidos().ActualizaPedido(this.Pedido.Id, facturado);
                        //    new BusPedidos().ActualizaPedido(pe.Id, facturado);
                        //    if (notaCredito.PedidoId != pe.Id) //EL PRIMER PEDIDO SE AGREGA DESDE AgregaFactura.
                        //        new BusFacturas().AgregaFacturaPedidos(notaCredito, pe.Id);
                        //}
                    }
                    catch (Exception ex)
                    {
                        MuestraExcepcion(ex, "NOTA DE CRÉDITO TIMBRADA ANTE SAT PERO NO SE LOGRO GUARDAR EN EL SISTEMA\n" +
                            "\n-COMUNIQUESE CON EL ADMINISTRADOR DE SISTEMAS-");
                    }
                    finally { base.SetDefaultCursor(); }

                    try
                    {
                        if (MuestraMensajeYesNo("¿Desea mostrar Nota de Crédito?") == DialogResult.Yes)
                        {
                            string nombreArchivo = EncuentraArchivo(notaCredito.Ruta, ".pdf");
                            MuestraArchivo(notaCredito.Ruta, nombreArchivo);
                        }
                    }
                    catch (Exception ex) { MuestraExcepcion(ex, "ERROR AL MOSTRAR FACTURA"); }

                    try
                    {
                        base.SetWaitCursor();
                        base.EnviaCorreo("NOTA DE CRÉDITO", EmpresaSeleccionada, notaCredito.NumeroFactura, ClienteSeleccionado,
                                            notaCredito.Ruta, txtEmail2.Text, txtEmail3.Text, notaCredito.UUID);
                    }
                    catch (Exception ex)
                    {
                        MuestraExcepcion(ex, "Correo NO enviado.");
                    }
                    finally { base.SetDefaultCursor(); }
                }
                else
                {
                    string nombreArchivo = EncuentraArchivo(notaCredito.Ruta, ".pdf");
                    MuestraArchivo(notaCredito.Ruta, nombreArchivo);
                }
            }

            return facturado;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MuestraMensajeYesNo(string.Format("¿Seguro desea FACTURAR NOTA DE CRÉDITO? \n Cliente:{0}", txtNombreFiscal.Text), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    base.SetWaitCursor();

                    this.NotaCredito = new EntFactura();
                    this.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(this.Cliente.EmpresaId);
                    
                    decimal subtotal = Math.Round(Math.Round(Total, 2) / (1 + 0.08m), 2); //Math.Round(Total, 2) / (1 + IVA); //Math.Round(total / (1 + IVA), 2);
                    decimal cantidadIeps = Math.Round(Total, 2) - subtotal;
                    if (chkSinIEPS.Checked)
                        cantidadIeps = 0;
                    FacturarNC(this.EmpresaSeleccionada, this.Cliente, this.ListaFacturas.First().PedidoId, this.ListaFacturas.First().Id,
                                    this.ListaFacturas.First().UUID, ConvierteTextoADecimal(txtTotalNC),
                                    0, 0, 0, cantidadIeps, 0,
                                    txtDescripcionNC.Text, txtObservaciones.Text,
                                    this.EmpresaSeleccionada.Facturacion, false);
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
                this.DialogResult = DialogResult.Abort;
            }
            finally { base.SetDefaultCursor(); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

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

        private void chkMSC_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkMSC.Checked)
                    txtObservaciones.Text += " - MSC";
                else
                    txtObservaciones.Text = txtObservaciones.Text.Replace(" - MSC", "");

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
    }
}
