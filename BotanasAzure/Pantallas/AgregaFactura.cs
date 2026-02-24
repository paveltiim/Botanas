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
        List<EntPedido> ListaPedidos { get; set; }
        public EntPedido Pedido { get; set; }
        public EntCliente Cliente { get; set; }
        public int TrabajadorId { get; set; }
        public int FacturaId { get; set; }
        int EstablecimientoId {get; set; }
        public EntFactura Factura { get; set; }

        public AgregaFactura()
        {
            InitializeComponent();
        }

        public AgregaFactura(EntPedido Pedido, EntCliente Cliente, int EstablecimientoId)
        {
            InitializeComponent();

            this.Cliente = Cliente;
            this.Cliente.Sucursal = ObtieneSucursalFromPedidoDetalle(Pedido.Detalle);
            this.Pedido = Pedido;
            this.EstablecimientoId = EstablecimientoId;
        }
        public AgregaFactura(List<EntPedido> ListaPedidos, EntCliente Cliente, int EstablecimientoId)
        {
            InitializeComponent();

            this.ListaPedidos = ListaPedidos;
            this.Cliente = Cliente;
            this.EstablecimientoId = EstablecimientoId;
            
            this.Pedido =this.ListaPedidos.First();
            //this.Pedido.SubTotal = 0;
            //this.Pedido.Total = 0;
            //this.Pedido.IEPS = 0;
            //for (int x = 1; x < this.ListaPedidos.Count; x++)
            //{
            //    this.Pedido.SubTotal += this.ListaPedidos[x].SubTotal;
            //    this.Pedido.Total += this.ListaPedidos[x].Total;
            //    this.Pedido.IEPS += this.ListaPedidos[x].IEPS;
            //}
            this.Pedido.SubTotal = this.ListaPedidos.Sum(P=>P.SubTotal);
            this.Pedido.IEPS = this.ListaPedidos.Sum(P => P.IEPS);
            this.Pedido.Total = this.ListaPedidos.Sum(P => P.Total);    
        }
        public AgregaFactura(List<EntPedido> ListaPedidos, EntCliente Cliente, 
                            int EstablecimientoId, int TrabajadorId)
        {
            InitializeComponent();

            this.ListaPedidos = ListaPedidos;
            this.Cliente = Cliente;
            this.EstablecimientoId = EstablecimientoId;
            this.TrabajadorId = TrabajadorId;

            this.Pedido = this.ListaPedidos.First();
            //this.Pedido.SubTotal = 0;
            //this.Pedido.Total = 0;
            //this.Pedido.IEPS = 0;
            //for (int x = 1; x < this.ListaPedidos.Count; x++)
            //{
            //    this.Pedido.SubTotal += this.ListaPedidos[x].SubTotal;
            //    this.Pedido.Total += this.ListaPedidos[x].Total;
            //    this.Pedido.IEPS += this.ListaPedidos[x].IEPS;
            //}
            this.Pedido.SubTotal = this.ListaPedidos.Sum(P => P.SubTotal);
            this.Pedido.IEPS = this.ListaPedidos.Sum(P => P.IEPS);
            this.Pedido.Total = this.ListaPedidos.Sum(P => P.Total);
        }

        #region Metodos

        void CargaDatosCliente(EntCliente Cliente)
        {
            txtEmail.Text = Cliente.Email;

            txtNombreFiscal.Text = Cliente.NombreFiscal;
            txtRFC.Text = Cliente.RFC;
            txtDireccion.Text = Cliente.Direccion;
            txtSucursal.Text = Cliente.Sucursal;

            txtNoExterior.Text = Cliente.NoExterior;
            txtNoInterior.Text = Cliente.NoInterior;
            txtCalle.Text = Cliente.Calle;
            txtColonia.Text = Cliente.Colonia;
            txtLocalidad.Text = Cliente.Localidad;
            txtMunicipio.Text = Cliente.Municipio;
            txtEstado.Text = Cliente.Estado;
            txtCP.Text = Cliente.CP;
            cmbRegimenFiscal.SelectedIndex = ((List<EntCatalogoGenerico>)cmbRegimenFiscal.DataSource).FindIndex(P => P.Id == Cliente.RegimenFiscalId);

            if (txtRFC.Text == this.RfcPublicoGeneral)
            {
                cmbUsoCFDI.SelectedIndex = cmbUsoCFDI.Items.Count - 1;

                txtCP.Text = Program.EmpresaSeleccionada.CP;
                txtCP.ReadOnly = true;
                cmbRegimenFiscal.SelectedIndex = ((List<EntCatalogoGenerico>)cmbRegimenFiscal.DataSource).FindIndex(P => P.Id == 616);//SIN OBLIGACIONES FISCALES
                cmbRegimenFiscal.Enabled = false;
            }
        }

        #endregion

        string PathClienteDirectorioFacturas { get; set; }

        EntFactura TimbrarFactura(EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente, DateTime FechaFactura,
                                string FormaPago, string MedioPago, string CondicionPago, string NumeroCuenta,
                                decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS,
                                decimal ImpuestoRetenido,
                                string Observaciones,
                                string TipoComprobante,
                                string UsoCFDI, bool Timbrar)
        {
            if (Cliente == null)
                Cliente = new EntCliente();
            //Cliente.Nombre = txtNombre.Text;
            Cliente.NombreFiscal = txtNombreFiscal.Text;
            Cliente.RFC = txtRFC.Text;
            Cliente.Email = txtEmail.Text;
            //Cliente.Email2 = txtEmail2.Text;
            //Cliente.Email3 = txtEmail3.Text;
            Cliente.Nombre = txtNombreFiscal.Text.Trim();
            Cliente.RFC = txtRFC.Text;
            Cliente.CP = txtCP.Text;
            if (pnlDatosFacturacion40.Visible)
                Cliente.RegimenFiscalId = ObtieneCatalogoGenericoFromCmb(cmbRegimenFiscal).Id;

            UtiFacturacion factura = new UtiFacturacion();
            UtiFacturacionPruebas facturaPruebas = new UtiFacturacionPruebas();
            
            string pathPreFacturasBase = @"C:\TIIM\Facturacion\PreFacturas";
            string pathFacturasBase = @"C:\TIIM\Facturacion\Facturas";            
                
            string uuid = "";
            
            ////EN PRUEBA PARA CAMBIAR ULTIMA FACTURA
            //EntFactura siguienteF = new EntFactura();
            //if (Pedido.SiguienteFacturaId == 0)
            //{
            //    siguienteF = ObtieneSiguienteFactura(Program.EmpresaSeleccionada.Id, Pedido.Id, Program.EmpresaSeleccionada.SerieFactura);
            //    //siguienteF.NumeroFactura = "1";
            //    Pedido.Factura = siguienteF.NumeroFactura;
            //    Pedido.SiguienteFacturaId = siguienteF.Id;
            //}

            if (Program.ConexionIdActual == 1 && Timbrar && Program.EmpresaSeleccionada.Facturacion)//PRODUCCION
            {
                this.PathClienteDirectorioFacturas = base.CreaPathClienteDirectorioFacturas(pathFacturasBase, Cliente.Nombre, Pedido.Factura);
                base.EliminaArchivosDireccion(this.PathClienteDirectorioFacturas);

                //EN PRUEBA PARA CAMBIAR ULTIMA FACTURA
                //Pedido.Factura = ObtieneUltimaFactura(Program.EmpresaSeleccionada.Id);
                
                if (chkRecalcularFactura.Checked)
                {
                    uuid = factura.Facturar40Recalculo(Program.EmpresaSeleccionada, Pedido, ListaProductos, Cliente,
                                                Program.EmpresaSeleccionada.SerieFactura, Pedido.Factura, FechaFactura,
                                                FormaPago, MedioPago, CondicionPago,
                                                CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, ImpuestoRetenido, "",
                                                UsoCFDI, this.PathClienteDirectorioFacturas);
                    //uuid = factura.FacturarNeueRecalculo(Program.EmpresaSeleccionada, Pedido, ListaProductos, Cliente,
                    //                            Program.EmpresaSeleccionada.SerieFactura, Pedido.Factura, FechaFactura,
                    //                            FormaPago, MedioPago, CondicionPago,
                    //                            CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, ImpuestoRetenido, "",
                    //                            UsoCFDI, this.PathClienteDirectorioFacturas);
                }
                else
                {
                    uuid = factura.Facturar40(Program.EmpresaSeleccionada, Pedido, ListaProductos, Cliente,
                                                Program.EmpresaSeleccionada.SerieFactura, Pedido.Factura, FechaFactura,
                                                FormaPago, MedioPago, CondicionPago,
                                                CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, ImpuestoRetenido, "",
                                                UsoCFDI, this.PathClienteDirectorioFacturas);
                    //uuid = factura.FacturarNeue(Program.EmpresaSeleccionada, Pedido, ListaProductos, Cliente,
                    //                            Program.EmpresaSeleccionada.SerieFactura, Pedido.Factura, FechaFactura,
                    //                            FormaPago, MedioPago, CondicionPago,
                    //                            CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, ImpuestoRetenido, "",
                    //                            UsoCFDI, this.PathClienteDirectorioFacturas);
                }
            }
            else
            {
                this.PathClienteDirectorioFacturas = base.CreaPathClienteDirectorioFacturas(pathPreFacturasBase, Cliente.Nombre, Pedido.Factura);
                base.EliminaArchivosDireccion(this.PathClienteDirectorioFacturas);

                MuestraMensaje("FACTURACIÓN DE PRUEBA");
                //Program.EmpresaSeleccionada.NombreFiscal = "PAVEL URIEL PINEDA RUIZ";
                //Program.EmpresaSeleccionada.CP = "81277";
                if (chkRecalcularFactura.Checked)
                {
                    uuid = facturaPruebas.Facturar40Recalculo(Program.EmpresaSeleccionada, Pedido, ListaProductos, Cliente,
                                                   Program.EmpresaSeleccionada.SerieFactura, FechaFactura,
                                                   FormaPago, MedioPago, CondicionPago, NumeroCuenta,
                                                   CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, ImpuestoRetenido,
                                                   this.PathClienteDirectorioFacturas,
                                                   UsoCFDI);
                    //uuid = facturaPruebas.FacturarNeueRecalculo(Program.EmpresaSeleccionada, Pedido, ListaProductos, Cliente,
                    //                            Program.EmpresaSeleccionada.SerieFactura, FechaFactura,
                    //                            FormaPago, MedioPago, CondicionPago, NumeroCuenta,
                    //                            CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, ImpuestoRetenido,
                    //                            this.PathClienteDirectorioFacturas,
                    //                            UsoCFDI);
                }
                else
                {
                    uuid = facturaPruebas.Facturar40(Program.EmpresaSeleccionada, Pedido, ListaProductos, Cliente,
                                                Program.EmpresaSeleccionada.SerieFactura, FechaFactura,
                                                FormaPago, MedioPago, CondicionPago, NumeroCuenta,
                                                CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, ImpuestoRetenido,
                                                this.PathClienteDirectorioFacturas,
                                                UsoCFDI);
                    //uuid = facturaPruebas.FacturarNeue(Program.EmpresaSeleccionada, Pedido, ListaProductos, Cliente,
                    //                            Program.EmpresaSeleccionada.SerieFactura, FechaFactura,
                    //                            FormaPago, MedioPago, CondicionPago, NumeroCuenta,
                    //                            CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, ImpuestoRetenido,
                    //                            this.PathClienteDirectorioFacturas,
                    //                            UsoCFDI);
                }
            }
            EntFactura fact = new EntFactura()
            {
                PedidoId = Pedido.Id,
                SerieFactura = Program.EmpresaSeleccionada.SerieFactura,
                NumeroFactura = Pedido.Factura,
                UUID = uuid,
                Ruta = this.PathClienteDirectorioFacturas,
                Fecha = DateTime.Today,
                SiguienteFacturaId = Pedido.SiguienteFacturaId
            };
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(this.PathClienteDirectorioFacturas);
            foreach (System.IO.FileInfo file in dir.GetFiles())
            {
                if (file.Extension == ".pdf")
                {
                    UtiPDF modiPDF = new UtiPDF();
                    string nota1 = "VENDEDOR: " + cmbTrabajadores.Text.Replace("-SELECCIONE-", "");
                    string nota2 = "SUCURSAL: " + txtSucursal.Text;
                    string nota3 = "";//"Dirección: " + txtDireccion.Text;                    
                    string nota4 = "NOTA: FAVOR DE SOLICITAR LAS DEVOLUCIONES AL MOMENTO DE HACER UN PEDIDO Y NO AL MOMENTO DE QUE ESTEN ENTREGANDO PRODUCTO. "
                                    + "NO HABRÁ DEVOLUCIONES DESPUÉS DE 8 DÍAS DE PEDIDO.";// "Dirección: " + txtDireccion.Text;

                    if (ListaProductos.Count > 40)
                        nota4 = "";
                    if (chkSinSucursal.Checked)
                        nota2 = "";

                    string rutaArchivoPDF = file.FullName;
                    modiPDF.ModificaPDF(nota1, nota2, nota3, nota4, rutaArchivoPDF, "1", 1);
                    fact.PDF = System.IO.File.ReadAllBytes(file.FullName);
                }
                else
                    fact.XML = System.IO.File.ReadAllBytes(file.FullName);
            }
            
            return fact;// pathClienteDirectorioFacturas;
        }
        
        bool Facturar(EntPedido PedidoAgrega, List<EntProducto> ProductosSeleccionados,
                        decimal CantidadIVA, decimal CantidadIVARetenido, decimal CantidadISRRetenido, decimal CantidadIEPS,
                        decimal ImpuestoRetenido,
                        string Observaciones, string TipoComprobante, bool Timbrar, bool PreFactura)
        {
            bool facturado = false;
            EntFactura factura = new EntFactura();
            base.SetWaitCursor();

            try
            {
                //EN PRUEBA PARA CAMBIAR ULTIMA FACTURA
                //PedidoAgrega.Factura = ObtieneUltimaFactura(Program.EmpresaSeleccionada.Id);

                base.AsignaSiguienteFacturaEnPedido(PedidoAgrega.SiguienteFacturaId, PedidoAgrega, Program.EmpresaSeleccionada.Id, Program.EmpresaSeleccionada.SerieFactura);

                txtFormaPago.Text = cmbFormaPago.Text.Remove(2, cmbFormaPago.Text.Length - 2);
                txtMetodoPago.Text = cmbMetodoPago.Text.Remove(3);
                txtUsoCFDI.Text = cmbUsoCFDI.Text.Remove(3);

                factura = TimbrarFactura(PedidoAgrega, ProductosSeleccionados, this.Cliente, DateTime.Now,
                                    txtFormaPago.Text, txtMetodoPago.Text, txtCondicionesPago.Text, txtNumeroCuenta.Text,
                                    CantidadIVA, CantidadIVARetenido, CantidadISRRetenido, CantidadIEPS, ImpuestoRetenido,
                                    Observaciones, TipoComprobante,
                                    txtUsoCFDI.Text, Timbrar);
                factura.EmpresaId = Program.EmpresaSeleccionada.Id;
                factura.TipoComprobanteId = 1;//I-INGRESO.
                factura.FormaPagoId = ConvierteTextoAInteger(txtFormaPago.Text);
                factura.MetodoPagoId = cmbMetodoPago.SelectedIndex + 1;
                factura.UsoCFDIId = ObtieneCatalogoGenericoFromCmb(cmbUsoCFDI).Id;
                factura.VersionCFDI = "4.0";
                facturado = true;
            }
            catch (Exception ex)
            {
                MandaExcepcion(ex.Message);
            }

            if (facturado)
            {
                if (!PreFactura)
                {
                    MuestraMensaje("¡El Pedido fue FACTURADO satisfactoriamente!", "CONFIRMACIÓN PEDIDO FACTURADO");

                    try
                    {
                        factura.Id = new BusFacturas().AgregaFactura(factura);
                        //EN PRUEBA PARA CAMBIAR ULTIMA FACTURA -- SE PUSO EN NEGOCIO EN METODO AgregaFactura(factura).
                        //new BusFacturas().ActualizaNumeroFactura(factura.SiguienteFacturaId, factura.Id, 
                        //                                            System.Convert.ToBase64String(factura.XML),
                        //                                            System.Convert.ToBase64String(factura.PDF));

                        foreach (EntPedido pe in this.ListaPedidos)
                        {
                            //new BusPedidos().ActualizaPedido(this.Pedido.Id, facturado);
                            new BusPedidos().ActualizaPedido(pe.Id, facturado);
                            if (factura.PedidoId != pe.Id) //EL PRIMER PEDIDO SE AGREGA DESDE AgregaFactura.
                                new BusFacturas().AgregaFacturaPedidos(factura, pe.Id);
                        }
                    }
                    catch (Exception ex)
                    {
                        MuestraExcepcion(ex, "ERROR AL GUARDAR FACTURA EN SISTEMA. LA FACTURA SI FUE TIMBRADA ANTE SAT. " +
                                             " \nFAVOR DE COMUNICARSE CON EL ADMINISTRADOR DE SISTEMA");
                    }
                    finally { base.SetDefaultCursor(); }
                    try
                    {
                        if (MuestraMensajeYesNo("¿Desea mostrar Factura?") == DialogResult.Yes)
                        {
                            string nombreArchivo = EncuentraArchivo(PathClienteDirectorioFacturas, ".pdf");
                            MuestraArchivo(PathClienteDirectorioFacturas, nombreArchivo);
                        }
                    }
                    catch (Exception ex) { MuestraExcepcion(ex, "ERROR AL MOSTRAR FACTURA"); }

                    try
                    {
                        base.SetWaitCursor();
                        base.EnviaCorreo("FACTURA", Program.EmpresaSeleccionada, PedidoAgrega, this.Cliente,
                                            this.PathClienteDirectorioFacturas, "", "",
                                            factura.UUID);
                    }
                    catch (Exception ex)
                    {
                        MuestraExcepcion(ex, "Correo NO enviado.");
                    }
                    finally { base.SetDefaultCursor(); }
                }
                else
                {
                    string nombreArchivo = EncuentraArchivo(PathClienteDirectorioFacturas, ".pdf");
                    MuestraArchivo(PathClienteDirectorioFacturas, nombreArchivo);
                }
            }

            return facturado;
        }


        private void AgregaFactura_Load(object sender, EventArgs e)
        {
            try
            {
                base.CargaCatalogoRegimen(cmbRegimenFiscal);
                base.CargaCatalogoUsoCFDI(cmbUsoCFDI);
                cmbFormaPago.SelectedIndex = 0;
                cmbMetodoPago.SelectedIndex = 0;
                //txtUUID.Text = this.UUID;
                //txtFolio.Text = this.NumeroFactura;
                CargaDatosCliente(this.Cliente);
                if (this.TrabajadorId > 0)
                {
                    base.CargaTrabajadoresPorEmpresa(Program.UsuarioSeleccionado.CompañiaId, 2, cmbTrabajadores, this.TrabajadorId);
                    cmbTrabajadores.Enabled = false;
                }
                else
                    base.CargaTrabajadores(this.EstablecimientoId, cmbTrabajadores);

                txtSubtotal.Text = FormatoMoney(this.Pedido.SubTotal);
                txtIeps.Text = FormatoMoney(this.Pedido.IEPS);
                txtTotal.Text = FormatoMoney(this.Pedido.Total);

                txtNombreFiscal.Enabled = true;
                txtNombreFiscal.ReadOnly = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
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

                if (MuestraMensajeYesNo(string.Format("¿Seguro desea FACTURAR NOTA DE VENTA? \n Cliente:{0}", txtNombreFiscal.Text), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    base.SetWaitCursor();
                    //REVISAR °INDICES°
                    Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(this.Cliente.EmpresaId);

                    List<EntProducto> productosSeleccionados = new List<EntProducto>();

                    if (this.ListaPedidos == null)
                        this.ListaPedidos = new List<EntPedido>{this.Pedido}; 

                    foreach (EntPedido pe in this.ListaPedidos)
                    {
                        //List<EntProducto> productosPedido = new BusProductos().ObtieneProductosPorPedido(Pedido.Id);
                        List<EntProducto> productosPedido = new BusProductos().ObtieneProductosPorPedido(pe.Id);
                        //foreach (EntProducto p in productosSeleccionados)
                        foreach (EntProducto p in productosPedido)
                        {
                            if (p.IncluyeIeps)
                            {
                                //p.PrecioVenta = p.PrecioVentaSinIVA + p.IEPS;
                                decimal porcentaje = 108;
                                decimal ieps = 8;
                                p.IEPS = Math.Round((p.PrecioVenta / porcentaje) * ieps, 2);
                                p.PrecioVentaSinIVA = p.PrecioVenta - p.IEPS;
                            }
                        }
                        productosSeleccionados.AddRange(productosPedido);
                    }

                    bool facturado = Facturar(this.Pedido, productosSeleccionados,
                                0, 0, 0, this.Pedido.IEPS, 0, "", "I",
                                Program.EmpresaSeleccionada.Facturacion, false);

                }
            }
            catch (Exception ex) {
                MuestraExcepcion(ex);
                this.DialogResult = DialogResult.Abort;
            }
            finally { base.SetDefaultCursor(); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnPreFactura_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Abort;
                if (string.IsNullOrEmpty(txtCP.Text.Trim()))
                    MandaExcepcion("Ingrese C.P. del Cliente");
                if (cmbRegimenFiscal.SelectedIndex < 0)
                    MandaExcepcion("Seleccione Régimen Fiscal");

                if (MuestraMensajeYesNo("¿Desea mostrar PRE-FACTURAR?", "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    base.SetWaitCursor();
                    Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(this.Cliente.EmpresaId);
                   
                    List<EntProducto> productosSeleccionados = new List<EntProducto>();

                    if(this.ListaPedidos==null) {
                        this.ListaPedidos = new List<EntPedido>();
                        this.ListaPedidos.Add(this.Pedido);
                    }

                    foreach (EntPedido pe in this.ListaPedidos)
                    {
                        List<EntProducto> productosPedido = new BusProductos().ObtieneProductosPorPedido(pe.Id);
                        foreach (EntProducto p in productosPedido)
                        {
                            if (p.IncluyeIeps)
                            {
                                decimal porcentaje = 108;
                                decimal ieps = 8;
                                p.IEPS = Math.Round((p.PrecioVenta / porcentaje) * ieps, 2);
                                p.PrecioVentaSinIVA = p.PrecioVenta - p.IEPS;
                            }
                        }
                        productosSeleccionados.AddRange(productosPedido);
                    }

                    bool facturado = Facturar(this.Pedido, productosSeleccionados,
                                0, 0, 0, this.Pedido.IEPS, 0, "", "I",
                                false, true);

                }
            }
            catch (Exception ex)
            {
                base.MuestraExcepcionFacturacion(ex);
                this.DialogResult = DialogResult.Abort;
            }
            finally { base.SetDefaultCursor(); }
        }

        private void chkRecalcularFactura_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                List<EntProducto> productosSeleccionados = new List<EntProducto>();

                decimal importeSubtotal = 0;
                decimal importeTotal = 0;
                decimal iepsTotal = 0;
                foreach (EntPedido pe in this.ListaPedidos)
                {
                    List<EntProducto> productosPedido = new BusProductos().ObtieneProductosPorPedido(pe.Id);
                    foreach (EntProducto p in productosPedido)
                    {
                        decimal porcentaje = 100;
                        decimal cantidadIeps = 0;
                        decimal importe = 0;
                        if (p.IncluyeIeps)
                        {
                            porcentaje += (IEPS) * 100;

                            cantidadIeps = (Math.Round(p.Precio, 2) * (IEPS * 100)) / porcentaje;
                            importe = Math.Round(p.Precio, 2) - cantidadIeps;
                        }
                        else
                        {
                            importe = Math.Round(Math.Round(p.Precio, 2), 4);
                        }
                        importeSubtotal += importe;
                        iepsTotal += cantidadIeps;

                        //p.PrecioVentaSinIVA = importe;
                        //p.IEPS = cantidadIeps;
                        //p.PrecioVenta = importe + cantidadIeps;
                        //productosSeleccionados.AddRange(productosPedido);
                    }
                }
                importeTotal = importeSubtotal + iepsTotal;

                //txtSubtotal.Text = FormatoMoney(productosSeleccionados.Sum(P=> P.PrecioVentaSinIVA));
                //txtIeps.Text = FormatoMoney(productosSeleccionados.Sum(P => P.IEPS));
                //txtTotal.Text = FormatoMoney(productosSeleccionados.Sum(P => P.PrecioVenta));
                txtSubtotal.Text = FormatoMoney(importeSubtotal);
                txtIeps.Text = FormatoMoney(iepsTotal);
                txtTotal.Text = FormatoMoney(importeTotal);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
                this.DialogResult = DialogResult.Abort;
            }
            finally { base.SetDefaultCursor(); }
        }

        private void AgregaFactura_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.DialogResult == DialogResult.Abort)
                {
                    e.Cancel = true;
                }
                else
                {
                    //EN PRUEBA PARA CAMBIAR ULTIMA FACTURA
                    this.Pedido.Id = 0;
                    this.Pedido.Factura = "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

    }
}
