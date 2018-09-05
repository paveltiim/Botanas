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
        public void VerificaEmpresa()
        {
            cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }

        public Clientes()
        {
            InitializeComponent();
        }

        EntCliente ClienteSeleccionado;
        List<EntCliente> ListaClientes = new List<EntCliente>();

        #region Metodos
            public void CargaGvClientes(int EmpresaId)
            {
                ListaClientes = new BusClientes().ObtieneClientes(EmpresaId).OrderBy(P => P.Nombre).ToList();
                gvClientes.DataSource = ListaClientes;
                dataGridView1.DataSource = ListaClientes;

                CargaClientesEnPantallas();
            }

            public void AgregaCliente(int EmpresaId, string Nombre, string NombreFiscal, string Direccion, string Calle, string NoExterior, string NoInterior, string Colonia,
            string Localidad, string Municipio, string Estado, string CP, string Telefono, string Telefono2, string Celular, string RFC, 
            string Email, string Email2, string Email3, string Banco, string NumeroCuenta,
            string Sucursal, string CLABE, string NumeroReferencia, int FormaPagoId, bool IncluyeKit)
            {
                EntCliente Cliente = new EntCliente()
                {
                    EmpresaId=EmpresaId,
                    TipoPersonaId=0,
                    Nombre = Nombre,
                    NombreFiscal=NombreFiscal,
                    Direccion = Direccion,
                    Telefono = Telefono,
                    Telefono2=Telefono2,
                    Celular = Celular,
                    Email = Email,
                    Email2=Email2,
                    Email3=Email3,
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
                    IncluyeKit=IncluyeKit,
                    FormaPagoId = FormaPagoId,
                    Fecha =DateTime.Now
                };
                Cliente.Id = new BusClientes().AgregaCliente(Cliente);
            }
        public void AgregaCliente(int EmpresaId, string Nombre)
        {
            EntCliente Cliente = new EntCliente()
            {
                EmpresaId = EmpresaId,
                TipoPersonaId = 0,
                Nombre = Nombre,

                NombreFiscal = "",
                Direccion = "",
                Telefono = "",
                Telefono2 = "",
                Celular = "",
                Email = "",
                Email2 = "",
                Email3 = "",
                RFC = "",
                Calle = "",
                NoExterior = "",
                NoInterior = "",
                Colonia = "",
                Localidad = "",
                Municipio = "",
                Estado = "",
                CP = "",
                Banco = "",
                NumeroCuenta = "",
                Sucursal = "",
                CLABE = "",
                NumeroReferencia = "",

                FormaPagoId = 0,
                IncluyeKit = false,
                Fecha = DateTime.Now
            };
            Cliente.Id = new BusClientes().AgregaCliente(Cliente);
        }
        public void AgregaCliente(int ClienteId, int EmpresaId, string Nombre)
        {
            EntCliente Cliente = new EntCliente()
            {
                Id=ClienteId,
                EmpresaId = EmpresaId,
                TipoPersonaId = 0,
                Nombre = Nombre,

                NombreFiscal = "",
                Direccion = "",
                Telefono = "",
                Telefono2 = "",
                Celular = "",
                Email = "",
                Email2 = "",
                Email3 = "",
                RFC = "",
                Calle = "",
                NoExterior = "",
                NoInterior = "",
                Colonia = "",
                Localidad = "",
                Municipio = "",
                Estado = "",
                CP = "",
                Banco = "",
                NumeroCuenta = "",
                Sucursal = "",
                CLABE = "",
                NumeroReferencia = "",

                FormaPagoId = 0,
                IncluyeKit = false,
                Fecha = DateTime.Now
            };
            Cliente.Id = new BusClientes().AgregaCliente(ClienteId,Cliente);
        }

        public void ActualizaCliente(int ClienteId, int EmpresaId, string Nombre, string NombreFiscal, string Direccion, string Calle, string NoExterior, string NoInterior, string Colonia,
            string Localidad, string Municipio, string Estado, string CP, string Telefono, string Telefono2, string Celular, string RFC, string Email, string Email2, string Email3, string Banco, string NumeroCuenta,
            string Sucursal, string CLABE, string NumeroReferencia, int FormaPagoId, bool IncluyeKit)
            {
                EntCliente Cliente = new EntCliente()
                {
                    Id=ClienteId,
                    EmpresaId = EmpresaId,
                    TipoPersonaId = 0,
                    Nombre = Nombre,
                    NombreFiscal = NombreFiscal,
                    Direccion = Direccion,
                    Telefono = Telefono,
                    Telefono2 = Telefono2,
                    Celular = Celular,
                    Email = Email,
                    Email2 = Email2,
                    Email3 = Email3,
                    RFC = RFC,
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
                    IncluyeKit = IncluyeKit,
                    FormaPagoId = FormaPagoId,
                };
                new BusClientes().ActualizaCliente(Cliente);
            }
        public void ActualizaCliente(EntCliente Cliente, string Nombre)
        {
            //EntCliente Cliente = new EntCliente()
            //{
            //    Id = ClienteId,
            //    EmpresaId = EmpresaId,
            //    TipoPersonaId = 0,
            //    Nombre = Nombre,
            //    IncluyeKit = false,
            //    FormaPagoId = 0,
            //};
            Cliente.Nombre = Nombre;
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
                txtBanco.Text = Cliente.Banco;
                txtNumeroCuenta.Text = Cliente.NumeroCuenta;
                txtSucursal.Text = Cliente.Sucursal;
                txtCLABE.Text = Cliente.CLABE;
                txtNumeroReferencia.Text = Cliente.NumeroReferencia;
                chkIncluyeKit.Checked = Cliente.IncluyeKit;

                cmbFormaPago.SelectedIndex = Cliente.FormaPagoId-1;
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

            void CargaClientesEnPantallas()
            {
                Form forma = BuscaFormaBase(new Ventas().Titulo);
                if (forma != null)
                {
                    ((Ventas)forma).CargaClientes(Program.EmpresaSeleccionada.Id);
                }
            }
        #endregion
        List<EntEmpresa> ListaEmpresas;

        void InicializaPantalla()
        {
            cmbFormaPago.SelectedIndex = 0;
            LimpiaTextBox(pnlDatos);
            chkIncluyeKit.Checked = false;
            ClienteSeleccionado = null;
            ActivaAgregar(true);

            if (Program.UsuarioSeleccionado.Id > 1)
            {
                lbBanco.Visible = true;
                txtBanco.Visible = true;
            }
            //if(Program.EmpresaSeleccionada!=null)
            //    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }
        public void CargaEmpresas()
        {
            if (Program.UsuarioSeleccionado.Id > 1)
                ListaEmpresas = new BusEmpresas().ObtieneEmpresas().Where(P => P.UsuarioId == Program.UsuarioSeleccionado.Id).ToList();
            else
                ListaEmpresas = new BusEmpresas().ObtieneEmpresas();

            Program.CambiaEmpresa = false;
            cmbEmpresas.DataSource = ListaEmpresas;
            Program.CambiaEmpresa = true;

            //CargaClientesEnPantallas();
        }
        private void Clientes_Load(object sender, EventArgs e)
        {
            try
            {
                InicializaPantalla();
                CargaEmpresas();

                if (Program.EmpresaSeleccionada == null)
                    Program.EmpresaSeleccionada = SeleccionaEmpresa();

                cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
                CargaGvClientes(Program.EmpresaSeleccionada.Id);
            }catch(Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                    MandaExcepcion("Ingrese Nombre del Cliente");
                else
                    AgregaCliente(Program.EmpresaSeleccionada.Id, txtNombre.Text, txtNombreFiscal.Text, txtDireccion.Text, txtCalle.Text, txtNoExterior.Text, txtNoInterior.Text
                            , txtColonia.Text, txtLocalidad.Text, txtMunicipio.Text, txtEstado.Text, txtCP.Text, txtTelefono.Text, txtTelefono2.Text
                            , txtCelular.Text, txtRFC.Text, txtEmail.Text, txtEmail2.Text, txtEmail3.Text, txtBanco.Text, txtNumeroCuenta.Text
                            , txtSucursal.Text, txtCLABE.Text, txtNumeroReferencia.Text, cmbFormaPago.SelectedIndex+1, chkIncluyeKit.Checked);
                //AgregaCliente(Program.EmpresaSeleccionada.Id, txtNombre.Text, txtNombreFiscal.Text, txtDireccion.Text, txtCalle.Text, txtNoExterior.Text, txtNoInterior.Text, txtColonia.Text, txtLocalidad.Text, txtMunicipio.Text, txtEstado.Text, txtCP.Text, txtTelefono.Text, txtTelefono2.Text, txtCelular.Text, txtRFC.Text, txtEmail.Text, txtBanco.Text, txtNumeroCuenta.Text, txtSucursal.Text, txtCLABE.Text, txtNumeroReferencia.Text);

                CargaGvClientes(Program.EmpresaSeleccionada.Id);
                LimpiaTextBox(pnlDatos);
                chkIncluyeKit.Checked = false;
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        void ActivaAgregar(bool Visible) {
            btnAgregar.Visible = Visible;
            btnActualizar.Visible = !Visible;
            btnCambiaEmpresa.Visible= !Visible;
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
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                    MandaExcepcion("Ingrese Nombre del Cliente");
                else
                    ActualizaCliente(ClienteSeleccionado.Id, Program.EmpresaSeleccionada.Id, txtNombre.Text, txtNombreFiscal.Text, txtDireccion.Text, txtCalle.Text, txtNoExterior.Text, txtNoInterior.Text, txtColonia.Text, txtLocalidad.Text
                        , txtMunicipio.Text, txtEstado.Text, txtCP.Text, txtTelefono.Text, txtTelefono2.Text
                        , txtCelular.Text, txtRFC.Text, txtEmail.Text, txtEmail2.Text, txtEmail3.Text, txtBanco.Text
                        , txtNumeroCuenta.Text, txtSucursal.Text, txtCLABE.Text, txtNumeroReferencia.Text, cmbFormaPago.SelectedIndex+1, chkIncluyeKit.Checked);

                CargaGvClientes(Program.EmpresaSeleccionada.Id);
                LimpiaTextBox(pnlDatos);
                chkIncluyeKit.Checked = false;
                ActivaAgregar(true);
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

                    CargaGvClientes(Program.EmpresaSeleccionada.Id);
                    LimpiaTextBox(pnlDatos);
                    chkIncluyeKit.Checked = false;

                    ActivaAgregar(true);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteSeleccionado = ObtieneClienteFromGV(gvClientes);
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
        /// <summary>
        /// Agrega nuevo registro del Pedido solicitado.
        /// </summary>
        /// <param name="pedido"></param>
        EntPedido AgregarPedido(int ClienteId, string Detalle, string Observaciones, decimal Total, decimal Pago, DateTime Fecha, DateTime FechaEntrega, int EmpleadoId, bool Facturado, int EstatusId)
        {
            EntPedido pedido = new EntPedido()
            {
                ClienteId = ClienteId,
                Detalle = Detalle,
                Observaciones = Observaciones,
                Total = Total,
                Pago = Pago,
                Fecha = Fecha,
                FechaEntrega = FechaEntrega,
                Facturado = Facturado,
                EmpleadoId = EmpleadoId,
                EstatusId = EstatusId
            };
            pedido.Id = new BusPedidos().AgregaPedido(pedido);
            return pedido;
        }
            
        private void btnAgregaDeuda_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteSeleccionado = ObtieneClienteFromGV(gvClientes);

                //CargaDatosCliente(ClienteSeleccionado);
                //ActivaAgregar(false);
                AgregaDeuda vAgregaDeuda = new AgregaDeuda();
                if (vAgregaDeuda.ShowDialog() == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    string detallePedido = vAgregaDeuda.Descripcion; //"DEUDA";
                    EntPedido pedidoAgrega = new EntPedido();
                    pedidoAgrega = AgregarPedido(ClienteSeleccionado.Id, detallePedido, "", ConvierteTextoADecimal(vAgregaDeuda.Cantidad), 0, vAgregaDeuda.Fecha, vAgregaDeuda.Fecha.Date, 0, true, 1);

                    //AgregarFacturaPedido(pedidoAgrega.Id, uuid, PathClienteDirectorioFacturas);

                    CargaGvClientes(Program.EmpresaSeleccionada.Id);
                    MessageBox.Show("¡Deuda Registrada!");
                }
                
                //decimal pago = ConvierteTextoADecimal(txtPago.Text);
                //if (pago > 0)
                //{
                //    if (pago > ConvierteTextoADecimal(txtTotal.Text))
                //    {
                //        MuestraMensaje("El Pago debe ser menor al Total a pagar. \n El Pago NO será registrado", "PAGO NO REGISTRADO");
                //        AumentaPagoPedido(pedidoAgrega.Id, -pago);
                //    }
                //    else
                //    {
                //        AgregarPago(pedidoAgrega.Id, pago);
                //        AumentaPagoPedido(pedidoAgrega.Id, 0);//SOLO PARA CAMBIAR ESTATUS
                //    }
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
                    GastoId=p.Id,
                    Pago=p.Pago,
                    Deuda=p.Total,
                    UUIDFactura=p.UUID,
                    NumeroFactura = p.Factura,
                    FormaPagoId=p.FormaPagoId,
                    FechaFactura =p.Fecha
                };
                lst.Add(e);
            }
            return lst;
        }
        void AgregarNotaCredito(int EmpresaId,int PedidoId, string Numero, decimal Cantidad, DateTime FechaPago)
        {
            EntPago pago = new EntPago()
            {
                EmpresaId = EmpresaId,
                PedidoId = PedidoId,
                Cantidad = Cantidad,
                Descripcion = Numero,
                Fecha = FechaPago,
                FechaPago = FechaPago
            };
            new BusPedidos().AgregaNotaCredito(pago);
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
        private void btnAgregaNotaCredito_Click(object sender, EventArgs e)
        {
            try
            {
                EntCliente clienteSeleccionado = ObtieneClienteFromGV(gvClientes);
                List<EntPedido> pedidosPorCliente = new BusPedidos().ObtienePedidosClientesPorCliente(clienteSeleccionado.Id);

                //SeleccionaFactura vSeleccionaFactura = new SeleccionaFactura(ConvierteListaPedidosEnListaEmpresas(pedidosPorCliente));
                SeleccionaFactura vSeleccionaFactura = new SeleccionaFactura(ConvierteListaPedidosEnListaProveedores(pedidosPorCliente));
                if (vSeleccionaFactura.ShowDialog() == DialogResult.OK)
                {
                    EntProveedor proveedorGastoSeleccionado = vSeleccionaFactura.ProveedorGastoSeleccionado;
                    int pedidoId = proveedorGastoSeleccionado.GastoId;
                    //int gastoId = new BusGastos().ObtieneEmpresaGastosPorEmpresa(2, empresaSeleccionada.Id)[0].Id;

                    AgregaFactura vAgregaFactura = new AgregaFactura(clienteSeleccionado);
                    vAgregaFactura.ActivaNotaCredito();
                    vAgregaFactura.UUID = proveedorGastoSeleccionado.UUIDFactura;
                    vAgregaFactura.NumeroFactura = proveedorGastoSeleccionado.NumeroFactura;
                    vAgregaFactura.FormaPagoId = proveedorGastoSeleccionado.FormaPagoId;
                    if (vAgregaFactura.ShowDialog() == DialogResult.OK)
                    {
                        AgregarNotaCredito(Program.EmpresaSeleccionada.Id, pedidoId, vAgregaFactura.NumeroNotaCredito, vAgregaFactura.Cantidad, DateTime.Today.Date);
                        //if (proveedorGastoSeleccionado.Deuda == (proveedorGastoSeleccionado.Pago + vAgregaFactura.Cantidad))
                        //    ActualizaEstatusPedido(pedidoId, 2);//PAGADO

                        MuestraMensaje("¡Nota de Crédito Registrada!", "CONFIRMACIÓN PAGO");
                    }

                }
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnVerPagosCliente_Click(object sender, EventArgs e)
        {
            try
            {
                EntCliente clienteSeleccionado = ObtieneClienteFromGV(gvClientes);
                //MuestraPagos vPagos = new MuestraPagos(clienteSeleccionado.Id);
                //vPagos.ShowDialog();
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
                //EstadoDeCuenta vPagos = new EstadoDeCuenta(clienteSeleccionado);
                //vPagos.ShowDialog();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnCambiaEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                SeleccionaEmpresa vSeleccionaEmp = new Pantallas.SeleccionaEmpresa(ListaEmpresas);
                if (vSeleccionaEmp.ShowDialog() == DialogResult.OK)
                {
                    ClienteSeleccionado.EmpresaId = vSeleccionaEmp.EmpresaSeleccionada.Id;
                    new BusClientes().ActualizaCliente(ClienteSeleccionado);
                    //cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == vSeleccionaEmp.EmpresaSeleccionada.Id);
                    CargaGvClientes(Program.EmpresaSeleccionada.Id);

                    InicializaPantalla();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Program.CambiaEmpresa)
                {
                    Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                    CargaGvClientes(Program.EmpresaSeleccionada.Id);

                    InicializaPantalla();
                }
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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }    
}
