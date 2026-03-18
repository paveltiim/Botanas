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
    public partial class Clientes : FormBase
    {
        // Sorting state for gvClientes
        string _lastSortColumn = "";
        bool _lastSortAscending = true;
        public void VerificaEmpresa()
        {
            cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }

        private void gvClientes_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                var col = gvClientes.Columns[e.ColumnIndex];
                string prop = col.DataPropertyName;
                if (string.IsNullOrEmpty(prop))
                    return;

                // Toggle sort direction if same column
                if (prop == _lastSortColumn)
                    _lastSortAscending = !_lastSortAscending;
                else
                {
                    _lastSortColumn = prop;
                    _lastSortAscending = true;
                }

                // Use string representation for comparison (works for most properties)
                if (_lastSortAscending)
                    ListaClientes = ListaClientes.OrderBy(x => (x.GetType().GetProperty(prop).GetValue(x, null) ?? "").ToString(), StringComparer.CurrentCultureIgnoreCase).ToList();
                else
                    ListaClientes = ListaClientes.OrderByDescending(x => (x.GetType().GetProperty(prop).GetValue(x, null) ?? "").ToString(), StringComparer.CurrentCultureIgnoreCase).ToList();

                // Rebind
                gvClientes.DataSource = null;
                gvClientes.DataSource = ListaClientes;
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = ListaClientes;

                // Update sort glyphs
                foreach (DataGridViewColumn c in gvClientes.Columns)
                {
                    c.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
                col.HeaderCell.SortGlyphDirection = _lastSortAscending ? SortOrder.Ascending : SortOrder.Descending;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        public Clientes()
        {
            InitializeComponent();
        }

        EntCliente ClienteSeleccionado;
        List<EntCliente> ListaClientes = new List<EntCliente>();
        int EstablecimientoId { get { return ObtieneCatalogoGenericoFromCmb(cmbAlmacenes).Id; } }
        #region Metodos

        public void CargaGvClientes(int EstablecimientoId)
        {
            ListaClientes = new BusClientes().ObtieneClientesPorEstablecimiento(EstablecimientoId);
            gvClientes.DataSource = ListaClientes;
            dataGridView1.DataSource = ListaClientes;

        }

        public void AgregaCliente(int EmpresaId, int EstablecimientoId, int TipoClienteId, string Nombre, string NombreFiscal,
            int RegimenFiscalId,
            string Direccion, string Calle, string NoExterior, string NoInterior, string Colonia,
            string Localidad, string Municipio, string Estado, string CP, string Telefono, string Telefono2, string Celular,
            string Contacto, string RFC, string Email, string Email2, string Email3, string Banco, string NumeroCuenta,
            string Sucursal, string CLABE, string NumeroReferencia, int FormaPagoId, bool Credito, int DiasCredito)
        {
            EntCliente Cliente = new EntCliente()
            {
                EmpresaId = EmpresaId,
                EstablecimientoId = EstablecimientoId,
                TipoPersonaId = TipoClienteId,
                Nombre = Nombre,
                NombreFiscal=NombreFiscal,
                Direccion = Direccion,
                Telefono = Telefono,
                Telefono2=Telefono2,
                Celular = Celular,
                Email = Email,
                Email2=Email2,
                Email3=Email3,
                Contacto=Contacto,
                RFC = RFC,
                Calle=Calle,
                NoExterior=NoExterior,
                NoInterior=NoInterior,                    
                Colonia = Colonia,
                Localidad=Localidad,
                Municipio=Municipio,
                Estado=Estado,
                CP=CP,
                Banco = Banco,
                NumeroCuenta = NumeroCuenta,
                Sucursal = Sucursal,
                CLABE = CLABE,
                NumeroReferencia = NumeroReferencia,
                FormaPagoId = FormaPagoId,
                Credito = Credito,
                DiasCredito = DiasCredito,
                RegimenFiscalId=RegimenFiscalId
            };
            Cliente.Id = new BusClientes().AgregaCliente(Cliente);
        }
        
        public void ActualizaCliente(int ClienteId, int EmpresaId, int TipoClienteId, string Nombre, string NombreFiscal,
            int RegimenFiscalId,
            string Direccion, string Calle, string NoExterior, string NoInterior, string Colonia,
            string Localidad, string Municipio, string Estado, string CP, string Telefono, string Telefono2, string Celular,
            string Contacto, string RFC, string Email, string Email2, string Email3, string Banco, string NumeroCuenta,
            string Sucursal, string CLABE, string NumeroReferencia, int FormaPagoId, bool Credito, int DiasCredito)
            {
                EntCliente Cliente = new EntCliente()
                {
                    Id=ClienteId,
                    EmpresaId = EmpresaId,
                    TipoPersonaId = TipoClienteId,
                    Nombre = Nombre,
                    NombreFiscal = NombreFiscal,
                    Direccion = Direccion,
                    Telefono = Telefono,
                    Telefono2 = Telefono2,
                    Celular = Celular,
                    Contacto = Contacto,
                    RFC = RFC,
                    Email = Email,
                    Email2 = Email2,
                    Email3 = Email3,
                    Calle = Calle,
                    NoExterior = NoExterior,
                    NoInterior = NoInterior,
                    Colonia = Colonia,
                    Localidad = Localidad,
                    Municipio = Municipio,
                    Estado = Estado,
                    CP = CP,
                    Banco = Banco,
                    NumeroCuenta = NumeroCuenta,
                    Sucursal = Sucursal,
                    CLABE = CLABE,
                    NumeroReferencia = NumeroReferencia,
                    FormaPagoId = FormaPagoId,
                    Credito=Credito,
                    DiasCredito=DiasCredito,
                    RegimenFiscalId = RegimenFiscalId
                };
                new BusClientes().ActualizaCliente(Cliente);
            }

        void ActualizaEstatusCliente(EntCliente Cliente, bool Estatus)
            {
                Cliente.Estatus = Estatus;
                new BusClientes().ActualizaEstatusCliente(Cliente);
            }

        void CargaDatosCliente(EntCliente Cliente)
        {
            txtNombre.Text = Cliente.Nombre;
            txtDireccion.Text = Cliente.Direccion;
            txtTelefono.Text = Cliente.Telefono;
            txtTelefono2.Text = Cliente.Telefono2;
            txtCelular.Text = Cliente.Celular;
            txtEmail.Text = Cliente.Email;
            txtEmail2.Text = Cliente.Email2;
            txtEmail3.Text = Cliente.Email3;
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
            txtDiasCredito.Text = Cliente.Banco;
            txtNumeroCuenta.Text = Cliente.NumeroCuenta;
            txtSucursal.Text = Cliente.Sucursal;
            txtCLABE.Text = Cliente.CLABE;
            txtNumeroReferencia.Text = Cliente.NumeroReferencia;
            txtContacto.Text = Cliente.Contacto;
            cmbTipoCliente.SelectedIndex = ((List<EntCatalogoGenerico>)cmbTipoCliente.DataSource).FindIndex(P => P.Id == Cliente.TipoPersonaId);//Cliente.TipoPersonaId - 1;
            chkClienteCredito.Checked = Cliente.Credito;
            txtDiasCredito.Text = Cliente.DiasCredito.ToString();
            cmbFormaPago.SelectedIndex = Cliente.FormaPagoId - 1;
            cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource)
                                            .FindIndex(P => P.Id == Cliente.EmpresaId);
            cmbRegimenFiscal.SelectedIndex = ((List<EntCatalogoGenerico>)cmbRegimenFiscal.DataSource).FindIndex(P => P.Id == Cliente.RegimenFiscalId);
            if (Cliente.RFC == base.RfcPublicoGeneral)
            {
                txtCP.Text = Program.EmpresaSeleccionada.CP; //OBLIGATORIO.
                txtCP.ReadOnly = true;
                if (Cliente.RegimenFiscalId != 616)//SIN OBLIGACIONES FISCALES.
                {
                    Cliente.RegimenFiscalId = 616; //Sin Obligaciones Fiscales      
                    //MuestraMensaje("EL RFC -" + txtRFC.Text + "- DEBE USAR OBLIGATORIAMENTE EL REGIMEN FISCAL: 616 - SIN OBLIGACIONES FISCALES." +
                    //    "\nSE DEBE ACTUALIZAR LOS DATOS DEL CLIENTE PARA CAMBIAR SU REGIMEN FISCAL.");
                    cmbRegimenFiscal.SelectedIndex = ((List<EntCatalogoGenerico>)cmbRegimenFiscal.DataSource).FindIndex(P => P.Id == 616);//SIN OBLIGACIONES FISCALES
                }
                cmbRegimenFiscal.Enabled = false;
            }
            else
            {
                cmbRegimenFiscal.Enabled = true;
                txtCP.ReadOnly = false;
            }
        }

        /// <summary>
        /// Filtra Clientes por los distintos parametros, y los carga en gvClientes.
        /// </summary>
        /// <param name="Clientes"></param>
        /// <param name="Nombre"></param>
        void FiltrarClientes(List<EntCliente> Clientes, string Nombre)
            {
                //List<EntCliente> clientes = (List<EntCliente>)gvClientes.DataSource;

                var clientesFiltro = from c in Clientes
                                     where c.Nombre.ToUpper().Contains(Nombre.ToUpper())
                                     select c;

                gvClientes.DataSource = null;
                gvClientes.DataSource = clientesFiltro.ToList();
            }

        #endregion
        List<EntEmpresa> ListaEmpresas;

        public void CargaEmpresas()
        {
            Program.CambiaEmpresa = false;
            //if (Program.UsuarioSeleccionado.Id > 1)
            //    cmbEmpresas.DataSource = new BusEmpresas().ObtieneEmpresas().Where(P => P.UsuarioId == Program.UsuarioSeleccionado.Id).ToList();
            //else
            cmbEmpresas.DataSource = new BusEmpresas().ObtieneEmpresas(Program.UsuarioSeleccionado.CompañiaId);
            Program.CambiaEmpresa = true;
        }
        void CargaAlmacenes()
        {
            List<EntCatalogoGenerico> almacenes = new BusEmpresas().ObtieneAlmacenes(Program.UsuarioSeleccionado.CompañiaId, Program.UsuarioSeleccionado.Id);
            cmbAlmacenes.DataSource = almacenes;
            if (almacenes.Count == 0)
                MuestraMensajeError("SIN ALMACENES DISPONIBLES PARA EL USUARIO");
            else
                cmbAlmacenes.SelectedIndex = 0;
        }
        public void CargaCatalogoRegimen()
        {
            //ListaEmpresas = new BusEmpresas().ObtieneCatalogoRegimen();
            cmbRegimenFiscal.DataSource = new BusEmpresas().ObtieneCatalogoRegimen();
            cmbRegimenFiscal.SelectedIndex = 0;
        }

        void InicializaPantalla()
        {
            cmbFormaPago.SelectedIndex = 0;
            LimpiaTextBox(pnlDatos);
            ClienteSeleccionado = null;
            ActivaAgregar(true);

            //if (Program.UsuarioSeleccionado.Id > 1)
            //{
                lbDiasCredito.Visible = true;
                txtDiasCredito.Visible = true;
            //}
            ////if(Program.EmpresaSeleccionada!=null)
            ////    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
            if (Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.CUENTASPORCOBRAR
                || Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.ADMINISTRADORPUNTOVENTA
                || Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.CUENTASPORCOBRARVENTAS
                || Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.MASTER)
                btnAgregaNotaCredito.Visible = true;

            List<EntCatalogoGenerico> lstPrecios = new List<EntCatalogoGenerico>();
            lstPrecios.Add(new EntCatalogoGenerico() { Id = 1, Descripcion = "Menudeo" });
            lstPrecios.Add(new EntCatalogoGenerico() { Id = 2, Descripcion = "MayoreoLocal" });
            lstPrecios.Add(new EntCatalogoGenerico() { Id = 3, Descripcion = "Sonora" });
            lstPrecios.Add(new EntCatalogoGenerico() { Id = 4, Descripcion = "Culiacan" });
            lstPrecios.Add(new EntCatalogoGenerico() { Id = 5, Descripcion = "Especial" });
            //listaProductos.Add(new EntCatalogoGenerico() { Id = 7, Descripcion = "A Detalle" });
            lstPrecios.Add(new EntCatalogoGenerico() { Id = 7, Descripcion = "Cabos" });
            lstPrecios.Add(new EntCatalogoGenerico() { Id = 14, Descripcion = "HERMOSILLO" });
            lstPrecios.Add(new EntCatalogoGenerico() { Id = 9, Descripcion = "CONVENIENCIA" });
            lstPrecios.Add(new EntCatalogoGenerico() { Id = 10, Descripcion = "EVENTUAL" });
            lstPrecios.Add(new EntCatalogoGenerico() { Id = 11, Descripcion = "DETALLE" });
            lstPrecios.Add(new EntCatalogoGenerico() { Id = 12, Descripcion = "DETALLE MAYOREO" });//TIPO = 2: Venta Movil
            lstPrecios.Add(new EntCatalogoGenerico() { Id = 13, Descripcion = "CADENA COMERCIAL" });//TIPO = 2: Venta Movil

            cmbTipoCliente.DataSource = null;
            cmbTipoCliente.DataSource = lstPrecios;
            cmbTipoCliente.SelectedIndex = 0;
        }


        private void Clientes_Load(object sender, EventArgs e)
        {
            try
            {
                InicializaPantalla();
                CargaEmpresas();

                //if (Program.EmpresaSeleccionada == null)
                //    Program.EmpresaSeleccionada = SeleccionaEmpresa();

                //cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);

                CargaAlmacenes();
                CargaCatalogoRegimen();
                CargaGvClientes(this.EstablecimientoId);
            }catch(Exception ex) { MuestraExcepcion(ex); }
        }
        void VerificaDatosCliente()
        {
            if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                MandaExcepcion("Ingrese Nombre del Cliente");
            if (string.IsNullOrEmpty(txtRFC.Text.Trim()))
                MandaExcepcion("Ingrese RFC del Cliente");
            if (string.IsNullOrEmpty(txtNombreFiscal.Text.Trim()))
                MandaExcepcion("Ingrese Nombre Fiscal del Cliente");
            if (string.IsNullOrEmpty(txtCP.Text.Trim()))
                MandaExcepcion("Ingrese C.P. del Cliente");
            if (cmbRegimenFiscal.SelectedIndex < 0)
                MandaExcepcion("Seleccione Régimen Fiscal");
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                
                VerificaDatosCliente();

                //if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                //    MandaExcepcion("Ingrese Nombre del Cliente");
                //else
                EntCatalogoGenerico regimen = ObtieneCatalogoGenericoFromCmb(cmbRegimenFiscal);
                EntCatalogoGenerico tipoCliente = ObtieneCatalogoGenericoFromCmb(cmbTipoCliente);
                AgregaCliente(ObtieneEmpresaFromCmb(cmbEmpresas).Id, this.EstablecimientoId
                            , tipoCliente.Id //cmbTipoCliente.SelectedIndex+1
                            , txtNombre.Text.Trim(), txtNombreFiscal.Text.Trim()
                            , regimen.Id
                            , txtDireccion.Text, txtCalle.Text, txtNoExterior.Text, txtNoInterior.Text
                            , txtColonia.Text, txtLocalidad.Text, txtMunicipio.Text, txtEstado.Text, txtCP.Text, txtTelefono.Text, txtTelefono2.Text
                            , txtCelular.Text
                            , txtContacto.Text, txtRFC.Text, txtEmail.Text, txtEmail2.Text, txtEmail3.Text, txtDiasCredito.Text, txtNumeroCuenta.Text
                            , txtSucursal.Text, txtCLABE.Text, txtNumeroReferencia.Text, cmbFormaPago.SelectedIndex+1
                            , chkClienteCredito.Checked, ConvierteTextoAInteger(txtDiasCredito));
                //AgregaCliente(Program.EmpresaSeleccionada.Id, txtNombre.Text, txtNombreFiscal.Text, txtDireccion.Text, txtCalle.Text, txtNoExterior.Text, txtNoInterior.Text, txtColonia.Text, txtLocalidad.Text, txtMunicipio.Text, txtEstado.Text, txtCP.Text, txtTelefono.Text, txtTelefono2.Text, txtCelular.Text, txtRFC.Text, txtEmail.Text, txtBanco.Text, txtNumeroCuenta.Text, txtSucursal.Text, txtCLABE.Text, txtNumeroReferencia.Text);

                string nombre = txtNombre.Text;
                CargaGvClientes(this.EstablecimientoId);
                LimpiaTextBox(pnlDatos,true);
                chkClienteCredito.Checked = false;

                tcDatos.SelectedIndex = 0;

                MuestraMensaje("¡Cliente - " + nombre + " - Agregado!");
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
            finally { base.SetDefaultCursor(); }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                VerificaDatosCliente();

                //if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                //    MandaExcepcion("Ingrese Nombre del Cliente");
                //else
                EntCatalogoGenerico regimen = ObtieneCatalogoGenericoFromCmb(cmbRegimenFiscal);
                EntCatalogoGenerico tipoCliente = ObtieneCatalogoGenericoFromCmb(cmbTipoCliente);
                ActualizaCliente(this.ClienteSeleccionado.Id, ObtieneEmpresaFromCmb(cmbEmpresas).Id
                        , tipoCliente.Id //cmbTipoCliente.SelectedIndex + 1
                        , txtNombre.Text.Trim(), txtNombreFiscal.Text.Trim()
                        , regimen.Id
                        , txtDireccion.Text, txtCalle.Text, txtNoExterior.Text, txtNoInterior.Text
                        , txtColonia.Text, txtLocalidad.Text
                        , txtMunicipio.Text, txtEstado.Text, txtCP.Text, txtTelefono.Text, txtTelefono2.Text
                        , txtCelular.Text
                        , txtContacto.Text, txtRFC.Text, txtEmail.Text, txtEmail2.Text, txtEmail3.Text, txtDiasCredito.Text
                        , txtNumeroCuenta.Text, txtSucursal.Text, txtCLABE.Text, txtNumeroReferencia.Text
                        , cmbFormaPago.SelectedIndex + 1
                        , chkClienteCredito.Checked, ConvierteTextoAInteger(txtDiasCredito));

                CargaGvClientes(this.EstablecimientoId);
                LimpiaTextBox(pnlDatos, true);
                chkClienteCredito.Checked = false;
                ActivaAgregar(true);
                tcDatos.SelectedIndex = 0;

                MuestraMensaje("¡Cliente - " + this.ClienteSeleccionado.Nombre + " - Actualizados!");
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
            finally { base.SetDefaultCursor(); }
        }

        void ActivaAgregar(bool Visible) {
            btnAgregar.Visible = Visible;
            btnActualizar.Visible = !Visible;
            chkFacturaPublicoGeneral.Checked = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                //LimpiaTextBox(pnlDatos);
                //chkIncluyeKit.Checked = false;
                //ClienteSeleccionado = null;
                //ActivaAgregar(true);
                InicializaPantalla();
                CargaGvClientes(this.EstablecimientoId);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteSeleccionado = ObtieneClienteFromGV(gvClientes);

                if (MuestraMensajeYesNo("¿Desea Eliminar al Cliente: " + ClienteSeleccionado.Nombre+"?", "CONFIRMACIÓN ELIMINAR") == DialogResult.Yes)
                {
                    ActualizaEstatusCliente(ClienteSeleccionado, false);
                    ClienteSeleccionado = null;

                    CargaGvClientes(this.EstablecimientoId);
                    LimpiaTextBox(pnlDatos,true);
                    chkClienteCredito.Checked = false;

                    ActivaAgregar(true);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClienteSeleccionado = ObtieneClienteFromGV(gvClientes);
                CargaDatosCliente(ClienteSeleccionado);
                ActivaAgregar(false);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }


        private void gvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ClienteSeleccionado = ObtieneClienteFromGV(gvClientes);
                CargaDatosCliente(ClienteSeleccionado);
                ActivaAgregar(false);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((Keys)e.KeyChar == Keys.Enter)
                {
                    this.GetNextControl((Control)sender, true).Focus();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((Keys)e.KeyChar == Keys.Enter)
                {
                    txtNombreFiscal.Focus();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
                
        private void btnAgregaDeuda_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteSeleccionado = ObtieneClienteFromGV(gvClientes);

                ////CargaDatosCliente(ClienteSeleccionado);
                ////ActivaAgregar(false);
                //AgregaDeuda vAgregaDeuda = new AgregaDeuda();
                //if (vAgregaDeuda.ShowDialog() == DialogResult.OK)
                //{
                //    Cursor.Current = Cursors.WaitCursor;

                //    string detallePedido = vAgregaDeuda.Descripcion; //"DEUDA";
                //    EntPedido pedidoAgrega = new EntPedido();
                //    pedidoAgrega = AgregarPedido(ClienteSeleccionado.Id, detallePedido, "", ConvierteTextoADecimal(vAgregaDeuda.Cantidad), 0, vAgregaDeuda.Fecha, vAgregaDeuda.Fecha.Date, 0, true, 1);

                //    //AgregarFacturaPedido(pedidoAgrega.Id, uuid, PathClienteDirectorioFacturas);

                //    CargaGvClientes(Program.EmpresaSeleccionada.Id);
                //    MessageBox.Show("¡Deuda Registrada!");
                //}
                

                ClienteSeleccionado = null;

                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtClienteBusqueda_TextChanged(object sender, EventArgs e)
        {
            try {
                btnBuscarCliente.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                FiltrarClientes(ListaClientes, txtClienteBusqueda.Text);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        List<EntProveedor> ConvierteListaPedidosEnListaProveedores(List<EntPedido> ListaPedidos) {
            List<EntProveedor> lst = new List<EntProveedor>();
            foreach (EntPedido p in ListaPedidos)
            {
                EntProveedor e = new EntProveedor() {
                    EmpresaId=p.EmpresaId,
                    GastoId=p.Id,
                    NotasCredito=p.NotasCredito,
                    Pago=p.Pago,
                    Deuda=p.Total,
                    UUIDFactura=p.UUID,
                    Id = p.FacturaId,
                    NumeroFactura = p.Factura,
                    FormaPagoId=p.FormaPagoId,
                    FechaFactura =p.Fecha
                };
                lst.Add(e);
            }
            return lst;
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
        void ActualizaEstatusPedido(int PedidoId, int EstatusId)
        {
            EntPedido pedido = new EntPedido()
            {
                Id = PedidoId,
                EstatusId = EstatusId
            };
            new BusPedidos().ActualizaEstatusPedido(pedido);
        }

        List<EntEmpresa> ConvierteListaPedidosEnListaEmpresas(List<EntPedido> ListaPedidos)
        {
            List<EntEmpresa> lst = new List<EntEmpresa>();
            foreach (EntPedido p in ListaPedidos)
            {
                EntEmpresa e = new EntEmpresa()
                {
                    GastoId = p.Id,
                    Pago = p.Pago,
                    Deuda = p.Total,
                    NumeroFactura = p.Factura,
                    FechaFactura = p.Fecha
                };
                lst.Add(e);
            }
            return lst;
        }
        public void DescuentaTimbre(EntEmpresa EmpresaSeleccionada)
        {
            try
            {
                //DESCUENTA TIMBRE
                new BusEmpresas().AumentaTimbreEmpresa(EmpresaSeleccionada.Id);
                //Program.EmpresaSeleccionada.TimbresRestantes--;
                //Program.EmpresaSeleccionada.TimbresUsados++;
                EmpresaSeleccionada.TimbresRestantes--;
                EmpresaSeleccionada.TimbresUsados++;

                //MuestraTimbresEnPantallas();
            }
            catch (Exception ex) { }
        }
        private void btnAgregaNotaCredito_Click(object sender, EventArgs e)
        {
            try
            {
                EntCliente clienteSeleccionado = ObtieneClienteFromGV(gvClientes);
                List<EntPedido> pedidosDeudaCliente = new BusPedidos().ObtienePedidosClientesDeuda(clienteSeleccionado.RFC)
                                                                            .OrderBy(P => P.Fecha).ToList();

                SeleccionaFactura vSelFac = new SeleccionaFactura(pedidosDeudaCliente);
                if (vSelFac.ShowDialog() == DialogResult.OK)
                {
                    SalidasVentas vVent = new SalidasVentas();
                    bool pagoAgregado = false;
                    //RevisaFacturasMismoEmisor(vSelFac.FacturasPedidoSeleccionados);
                    Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(vSelFac.FacturasPedidoSeleccionados.First().EmpresaId);

                    List<EntPedido> pedidosfacturasSeleccionadas = vSelFac.FacturasPedidoSeleccionados;
                    foreach (EntPedido pf in pedidosfacturasSeleccionadas)
                    {
                        //SOLO AGREGA EL PAGO QUE DEBE TENER CADA FACTURA
                        AgregaPago vAgregaPago = new AgregaPago(pf, false);//pedidosDeudaCliente);
                        if (vAgregaPago.ShowDialog() == DialogResult.OK)
                        {
                            decimal cantidadPaga = ConvierteTextoADecimal(vAgregaPago.CantidadPago);
                            //SE USA INSTEAD OF PAGO PARA NO ALTERAR DEBE
                            pf.PagoTotal = cantidadPaga;
                            //totalPagoRestante = vAgregaPago.CantidadPagoRestante;
                        }
                        else
                            return;
                    }
                    //if (totalPagoRestante > 0)
                    //    MandaExcepcion("NO SE HA ASIGNADO EL TOTAL A PAGAR\n\n TOTAL A PAGAR RESTANTE: " + FormatoMoney(totalPagoRestante));

                    AgregaFacturaNcNeue vNcPago = new AgregaFacturaNcNeue(
                                                    ConvierteListaPedidosEnFacturas(pedidosfacturasSeleccionadas),
                                                    clienteSeleccionado,
                                                    pedidosfacturasSeleccionadas.Sum(P => P.PagoTotal));

                    if (vNcPago.ShowDialog() == DialogResult.OK)
                    {
                        pagoAgregado = true;
                    }
                    //}

                    if (pagoAgregado)
                        MuestraMensaje("¡Nota de Crédito Agregada!", "CONFIRMACIÓN NOTA DE CRÉDITO");
                    else
                        MuestraMensajeError("Error al agregar el Descuento(Pago)", "ERROR PAGO-NOTA DE CREDITO");
                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }finally { base.SetDefaultCursor(); }
        }

        private void btnVerPagosCliente_Click(object sender, EventArgs e)
        {
            try
            {
                EntCliente clienteSeleccionado = ObtieneClienteFromGV(gvClientes);
                MuestraPagos vPagos = new MuestraPagos(clienteSeleccionado.Id);
                vPagos.ShowDialog();
                //EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidosClientesCreditoDetalle);
                //List<EntFactura> facturasComplementos = new BusFacturas().ObtieneComplementos(pedidoSeleccionado.FacturaId, pedidoSeleccionado.Factura, pedidoSeleccionado.Total);

                //SeleccionaComplemento vSelFactura = new SeleccionaComplemento(pedidoSeleccionado);
                //vSelFactura.FacturaId = pedidoSeleccionado.FacturaId;
                //vSelFactura.ListaFacturasComplemento = facturasComplementos;
                //if (vSelFactura.ShowDialog() == DialogResult.OK)
                //{
                //    MuestraArchivo(vSelFactura.FacturaComplementoSeleccionada.Ruta);
                //}
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnEstadoDeCuenta_Click(object sender, EventArgs e)
        {
            try
            {
                EntCliente clienteSeleccionado = ObtieneClienteFromGV(gvClientes);
                EstadoDeCuenta vPagos = new EstadoDeCuenta(clienteSeleccionado, this.EstablecimientoId);
                vPagos.ShowDialog();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (Program.CambiaEmpresa)
                //{
                //    Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                //    CargaGvClientes(Program.EmpresaSeleccionada.Id);

                //    InicializaPantalla();
                //}
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnBuscaEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                SeleccionaEmpresa vSeleccionaEmp = new Pantallas.SeleccionaEmpresa(ListaEmpresas);
                if (vSeleccionaEmp.ShowDialog() == DialogResult.OK)
                {
                    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == vSeleccionaEmp.EmpresaSeleccionada.Id);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void chkClienteCredito_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtDiasCredito.Text = Convert.ToInt32(chkClienteCredito.Checked).ToString();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbAlmacenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { 
                CargaGvClientes(this.EstablecimientoId);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnVerNotasCredito_Click(object sender, EventArgs e)
        {
            try
            {
                EntCliente clienteSeleccionado = ObtieneClienteFromGV(gvClientes);

                Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(clienteSeleccionado.EmpresaId);

                MuestraPagosNC vPagos = new MuestraPagosNC(clienteSeleccionado.Id);
                vPagos.ShowDialog();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void chkFacturaPublicoGeneral_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkFacturaPublicoGeneral.Checked)
                {
                    txtRFC.Text = this.RfcPublicoGeneral;
                    txtCP.Text = Program.EmpresaSeleccionada.CP;
                    txtCP.ReadOnly = true;
                    cmbRegimenFiscal.SelectedIndex = ((List<EntCatalogoGenerico>)cmbRegimenFiscal.DataSource).FindIndex(P => P.Id == 616);//SIN OBLIGACIONES FISCALES
                    cmbRegimenFiscal.Enabled = false;
                }
                else
                {
                    txtRFC.Clear();
                    cmbRegimenFiscal.Enabled = true;
                    txtCP.ReadOnly = false;
                }

                txtNombreFiscal.Focus();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtRFC_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtRFC.Text.ToUpper().Trim() == base.RfcPublicoGeneral)
                {
                    txtCP.Text = Program.EmpresaSeleccionada.CP;
                    txtCP.ReadOnly = true;
                    cmbRegimenFiscal.SelectedIndex = ((List<EntCatalogoGenerico>)cmbRegimenFiscal.DataSource).FindIndex(P => P.Id == 616);//SIN OBLIGACIONES FISCALES
                    cmbRegimenFiscal.Enabled = false;
                }
                else
                {
                    cmbRegimenFiscal.Enabled = true;
                    txtCP.ReadOnly = false;
                }
                txtRFC.Text = txtRFC.Text.ToUpper();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnExporta_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                
                List<EntCliente> listaProductos = ObtieneListaClientesFromGV(gvClientes);
                
                ImpresionClientes vImprimeEntradas = new ImpresionClientes(listaProductos);
                vImprimeEntradas.Show();

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }
    }    
}
