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
    public partial class Proveedores : FormBase
    {
        public void VerificaEmpresa()
        {
            cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }

        public Proveedores()
        {
            InitializeComponent();
        }

        EntProveedor ProveedorSeleccionado { get { return ObtieneProveedorFromGV(gvProveedores); } }
        List<EntProveedor> ListaProveedores = new List<EntProveedor>();

        #region Metodos
            void CargaProveedoresEnPantallas()
            {
                Form vProd = BuscaFormaBase(new Productos().Titulo);
                if (vProd != null)
                    ((Productos)vProd).CargaProveedores(Program.EmpresaSeleccionada.Id);
            }
            void CargaProveedores(int EmpresaId)
            {
                ListaProveedores = new BusProveedores().ObtieneProveedores(EmpresaId);
                gvProveedores.DataSource = ListaProveedores;
                gvProveedoresSaldo.DataSource = ListaProveedores;

                cmbProveedores.DataSource = ListaProveedores;

                CargaProveedoresEnPantallas(); 
            }

            void AgregaProveedor(int EmpresaId, string Nombre, string NombreFiscal, string Direccion, string Telefono, string Telefono2, string Email, string Contacto, string TelefonoContacto, string Banco, string NumeroCuenta, string Sucursal, string CLABE, string NumeroReferencia)
            {
                EntProveedor proveedor = new EntProveedor()
                {
                    EmpresaId=EmpresaId,
                    Nombre = Nombre,
                    NombreFiscal = NombreFiscal,
                    Direccion = Direccion,
                    Telefono = Telefono,
                    Telefono2 = Telefono2,
                    Email = Email,
                    Contacto = Contacto,
                    TelefonoContacto = TelefonoContacto,
                    Banco = Banco,
                    NumeroCuenta = NumeroCuenta,
                    Sucursal = Sucursal,
                    CLABE = CLABE,
                    NumeroReferencia = NumeroReferencia,
                    Fecha = DateTime.Now
                };
                new BusProveedores().AgregaProveedor(proveedor);
            }
        public int AgregaProveedor(int EmpresaId, string Nombre, string NombreFiscal, string Direccion)
        {
            EntProveedor proveedor = new EntProveedor()
            {
                EmpresaId = EmpresaId,
                Nombre = Nombre,
                NombreFiscal = NombreFiscal,
                Direccion = Direccion,
                Telefono = "",
                Telefono2 = "",
                Email = "",
                Contacto = "",
                TelefonoContacto = "",
                Banco = "",
                NumeroCuenta = "",
                Sucursal = "",
                CLABE = "",
                NumeroReferencia = "",
                Fecha = DateTime.Now
            };
            return new BusProveedores().AgregaProveedor(proveedor);
        }
        //void AgregarNotaCredito(int GastoId, decimal Cantidad, DateTime FechaPago)
        //{
        //    EntPago pago = new EntPago()
        //    {
        //        GastoId = GastoId,
        //        Cantidad = Cantidad,
        //        Fecha= FechaPago,
        //        FechaPago=FechaPago
        //    };
        //    new BusEmpresas().AgregaNotaCredito(pago);
        //}
        void ActualizaProveedor(int ProveedorId, int EmpresaId, string Nombre, string NombreFiscal, string Direccion, string Telefono, string Telefono2, string Email, string Contacto, string TelefonoContacto, string Banco, string NumeroCuenta, string Sucursal, string CLABE, string NumeroReferencia)
            {
                EntProveedor proveedor = new EntProveedor()
                {
                    Id = ProveedorId,
                    EmpresaId=EmpresaId,
                    Nombre = Nombre,
                    NombreFiscal = NombreFiscal,
                    Direccion = Direccion,
                    Telefono = Telefono,
                    Telefono2 = Telefono2,
                    Email = Email,
                    Contacto = Contacto,
                    TelefonoContacto = TelefonoContacto,
                    Banco = Banco,
                    NumeroCuenta = NumeroCuenta,
                    Sucursal = Sucursal,
                    CLABE = CLABE,
                    NumeroReferencia = NumeroReferencia
                };
                new BusProveedores().ActualizaProveedor(proveedor);
            }

            void ActualizaEstatusProveedor(EntProveedor Proveedor, bool Estatus)
            {
                Proveedor.Estatus = Estatus;
                new BusProveedores().ActualizaEstatusProveedor(Proveedor);
            }
            void CargaDatosProveedor(EntProveedor Proveedor)
            {
                txtNombre.Text = Proveedor.Nombre;
                txtDireccion.Text = Proveedor.Direccion;
                txtNombreFiscal.Text = Proveedor.NombreFiscal;
                txtTelefono.Text = Proveedor.Telefono;
                txtTelefono2.Text = Proveedor.Telefono2;
                txtEmail.Text = Proveedor.Email;
                txtContacto.Text = Proveedor.Contacto;
                txtTelefonoContacto.Text = Proveedor.TelefonoContacto;
                txtBanco.Text = Proveedor.Banco;
                txtNumeroCuenta.Text = Proveedor.NumeroCuenta;
                txtSucursal.Text = Proveedor.Sucursal;
                txtCLABE.Text = Proveedor.CLABE;
                txtNumeroReferencia.Text = Proveedor.NumeroReferencia;

            }
            //void CargaDatosGastoEmpresa(EntEmpresa EmpresaGasto)
            //{
            //    txtNumeroFactura.Text = EmpresaGasto.NumeroFactura;
            //    dtpFechaFactura.Value = EmpresaGasto.FechaFactura;
            //    txtCantidadGasto.Text = FormatoMoney( EmpresaGasto.Deuda);
            //}

            /// <summary>
            /// Filtra Clientes por los distintos parametros, y los carga en gvClientes.
            /// </summary>
            /// <param name="Clientes"></param>
            /// <param name="Nombre"></param>
            void FiltrarProveedores(List<EntProveedor> Proveedor, string Nombre)
            {
                var proveedoresFiltro = from c in Proveedor
                                     where c.Nombre.ToUpper().Contains(Nombre.ToUpper())
                                     select c;

                gvProveedores.DataSource = null;
                gvProveedores.DataSource = proveedoresFiltro.ToList();
            }

            public void MuestraCompras()
            {
                tcProveedores.SelectedIndex = 1;//COMPRAS
            }
        #endregion
        List<EntEmpresa> ListaEmpresas;

        void InicializaPantalla()
        {
            LimpiaTextBox(pnlDatos);
            ActivaAgregar(true);
            //if(Program.EmpresaSeleccionada!=null)
            //    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }
        public void CargaEmpresas()
        {
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
                CargaProveedores(Program.EmpresaSeleccionada.Id);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                    MandaExcepcion("Ingrese Nombre de Proveedor");
                else
                    AgregaProveedor(Program.EmpresaSeleccionada.Id, txtNombre.Text, txtNombreFiscal.Text, txtDireccion.Text, txtTelefono.Text, txtTelefono2.Text, txtEmail.Text,txtContacto.Text,txtTelefonoContacto.Text, txtBanco.Text, txtNumeroCuenta.Text, txtSucursal.Text, txtCLABE.Text, txtNumeroReferencia.Text);
                //AgregaProveedor(Program.EmpresaSeleccionada.Id, txtNombre.Text, txtNombreFiscal.Text, txtDireccion.Text, txtTelefono.Text, txtTelefono2.Text, txtEmail.Text, txtContacto.Text, txtTelefonoContacto.Text, txtBanco.Text, txtNumeroCuenta.Text, txtSucursal.Text, txtCLABE.Text, txtNumeroReferencia.Text);

                CargaProveedores(Program.EmpresaSeleccionada.Id);
                LimpiaTextBox(pnlDatos);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        void ActivaAgregar(bool Visible) {
            btnAgregar.Visible = Visible;
            btnActualizar.Visible = !Visible;
            btnCambiaEmpresa.Visible = !Visible;
        }
        void ActivaAgregarGasto(bool Visible)
        {
            btnAgregaGasto.Visible = Visible;
            btnActualizaGasto.Visible = !Visible;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiaTextBox(pnlDatos);
                ActivaAgregar(true);
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
                    MandaExcepcion("Ingrese Nombre de Proveedor");
                else
                    ActualizaProveedor(ProveedorSeleccionado.Id, Program.EmpresaSeleccionada.Id, txtNombre.Text, txtNombreFiscal.Text, txtDireccion.Text, txtTelefono.Text, txtTelefono2.Text, txtEmail.Text,txtContacto.Text,txtTelefonoContacto.Text, txtBanco.Text, txtNumeroCuenta.Text, txtSucursal.Text, txtCLABE.Text, txtNumeroReferencia.Text);

                CargaProveedores(Program.EmpresaSeleccionada.Id);
                LimpiaTextBox(pnlDatos);
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
                if (MuestraMensajeYesNo("¿Desea Eliminar el Proveedor: " + ProveedorSeleccionado.Nombre+"?", "CONFIRMACIÓN ELIMINAR") == DialogResult.Yes)
                {
                    ActualizaEstatusProveedor(ProveedorSeleccionado, false);

                    CargaProveedores(Program.EmpresaSeleccionada.Id);
                    LimpiaTextBox(pnlDatos);

                    ActivaAgregar(true);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                CargaDatosProveedor(ProveedorSeleccionado);
                ActivaAgregar(false);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }


        private void gvProveedores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CargaDatosProveedor(ProveedorSeleccionado);
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

        private void gvEmpresas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
        private void btnAgregaPagoGasto_Click(object sender, EventArgs e)
            {
                try
                {
                    EntProveedor proveedorSeleccionado = ObtieneProveedorFromGV(gvProveedores);
                    //CargaGastosEmpresa(proveedorSeleccionado.Id);
                    SeleccionaFactura vSeleccionaFactura = new SeleccionaFactura(ObtieneListaProveedoresFromGV(gvProveedorGastos));
                    if (vSeleccionaFactura.ShowDialog() == DialogResult.OK)
                    {
                        int gastoId = vSeleccionaFactura.ProveedorGastoSeleccionado.GastoId;
                        AgregaPagoGasto vAgregaPago = new AgregaPagoGasto();
                        if (vAgregaPago.ShowDialog() == DialogResult.OK)
                        {
                            decimal cantidadPaga = ConvierteTextoADecimal(vAgregaPago.CantidadPago);
                            DateTime fechaPago = vAgregaPago.FechaPago;

                            if (cantidadPaga == 0)
                                throw new Exception("La cantidad a Pagar debe ser mayor a 0.");
                            if (cantidadPaga > proveedorSeleccionado.Saldo)
                                throw new Exception("La cantidad a Pagar debe ser menor o igual a la cantidad que Debe a Proveedor.");

                            AumentaPagoGasto(gastoId, cantidadPaga);
                            AgregarPago(gastoId, cantidadPaga, fechaPago);

                            int indexEmpresa = cmbProveedores.SelectedIndex;
                            CargaProveedores(Program.EmpresaSeleccionada.Id);
                            cmbProveedores.SelectedIndex = indexEmpresa;

                            MuestraMensaje("¡Pago Agregado!", "CONFIRMACIÓN PAGO");
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    MuestraExcepcion(ex);
                }
            }

            private void btnAgregaCompra_Click(object sender, EventArgs e)
            {
                try
                {
                    int indexEmpresa = cmbProveedores.SelectedIndex;
                    tcProveedores.SelectedIndex = 1;
                    cmbProveedores.SelectedIndex = indexEmpresa;
                    txtNumeroFactura.Focus();
                }
                catch (Exception ex)
                {
                    MuestraExcepcion(ex);
                }
            }
            
            private void btnAgregaNotaCredito_Click(object sender, EventArgs e)
            {
            //    try
            //    {
            //        EntEmpresa empresaSeleccionada = ObtieneEmpresaFromGV(gvEmpresas);
            //        CargaGastosEmpresa(empresaSeleccionada.Id);

            //        SeleccionaFactura vSeleccionaFactura = new SeleccionaFactura(ObtieneListaEmpresasFromGV(gvEmpresaGastos));
            //        if (vSeleccionaFactura.ShowDialog() == DialogResult.OK)
            //        {
            //            int gastoId = vSeleccionaFactura.EmpresaGastoSeleccionada.GastoId;
            //            //int gastoId = new BusGastos().ObtieneEmpresaGastosPorEmpresa(2, empresaSeleccionada.Id)[0].Id;

            //            AgregaPagoGasto vAgregaPago = new AgregaPagoGasto();
            //            if (vAgregaPago.ShowDialog() == DialogResult.OK)
            //            {
            //                decimal cantidadPaga = ConvierteTextoADecimal(vAgregaPago.CantidadPago);
            //                DateTime fechaPago = vAgregaPago.FechaPago;

            //                if (cantidadPaga == 0)
            //                    throw new Exception("La cantidad a Pagar debe ser mayor a 0.");
            //                if (cantidadPaga > empresaSeleccionada.Deuda)
            //                    throw new Exception("La cantidad a Pagar debe ser menor o igual a la cantidad que Debe a Proveedor.");

            //                //AumentaPagoGasto(gastoId, cantidadPaga);
            //                //SOLO NECESITA AGREGAR LA NOTA (SE RESTA EN CONSULTA A LA DEUDA).
            //                AgregarNotaCredito(gastoId, cantidadPaga, fechaPago);

            //                int indexEmpresa = cmbEmpresas.SelectedIndex;
            //                CargaEmpresas();
            //                cmbEmpresas.SelectedIndex = indexEmpresa;

            //                MuestraMensaje("¡Nota de Crédito Registrada!", "CONFIRMACIÓN PAGO");
            //            }
            //        }
            //}
            //catch (Exception ex)
            //    {
            //        MuestraExcepcion(ex);
            //    }
            }

        #region Metodos Gastos
            int AgregaGasto(int EmpresaId, int TipoGastoId, string NumeroFactura, decimal Cantidad, decimal Pago, DateTime FechaFactura, string Observaciones)
            {
                EntGasto gasto = new EntGasto()
                {
                    EmpresaId = EmpresaId,
                    TipoGastoId = TipoGastoId,
                    NumeroFactura = NumeroFactura,
                    Cantidad = Cantidad,
                    Pago=Pago,
                    FechaFactura = FechaFactura,
                    Observaciones = Observaciones,
                    FechaRegistro = DateTime.Now
                };
                gasto.Id = new BusGastos().AgregaGasto(gasto);
                return gasto.Id;
            }
            void ActualizaGasto(int GastoId,int EmpresaId, string NumeroFactura, decimal Cantidad, DateTime FechaFactura, string Observaciones, int EstatusId)
            {
                EntGasto gasto = new EntGasto()
                {
                    Id=GastoId,
                    EmpresaId = EmpresaId,
                    NumeroFactura = NumeroFactura,
                    Cantidad = Cantidad,
                    FechaFactura = FechaFactura,
                    Observaciones = Observaciones,
                    EstatusId=EstatusId
                };
                new BusGastos().ActualizaGasto(gasto);
            }
            /// <summary>
            /// Agrega nuevo registro del Pedido solicitado.
            /// </summary>
            /// <param name="pedido"></param>
            void AgregarPago(int GastoId, decimal Cantidad, DateTime FechaPago)
            {
                EntPago pago = new EntPago()
                {
                    GastoId = GastoId,
                    TipoPagoId = 2,
                    Cantidad = Cantidad,
                    FechaPago = FechaPago
                };
                new BusGastos().AgregaPagoGasto(pago);
            }
            void AumentaPagoGasto(int GastoId, decimal Cantidad)
            {
                EntGasto gasto = new EntGasto()
                {
                    Id = GastoId,
                    Cantidad = Cantidad,
                };
                new BusGastos().AumentaPagoGasto(gasto);
            } 
            void CargaGastosEmpresa(int EmpresaId)
            {
                List<EntEmpresa> lst = new BusEmpresas().ObtieneEmpresaGastosPorEmpresa(EmpresaId);
                gvProveedorGastos.DataSource = lst;
            }
        #endregion
            private void tcProveedores_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    switch (tcProveedores.SelectedIndex)
                    {
                        case 0://PROVEEDOR
                            int indexEmpresa = cmbProveedores.SelectedIndex;
                            CargaProveedores(Program.EmpresaSeleccionada.Id);
                            cmbProveedores.SelectedIndex = indexEmpresa;
                            break;
                        case 1://COMPRAS
                            //cmbEmpresas.SelectedIndex = 0;
                            break;

                    }
                }
                catch (Exception ex) { MuestraExcepcion(ex); }
            }
        #region Eventos Gastos
            int GastoId;
            private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
            {

        }

        private void txtPagoGasto_Leave(object sender, EventArgs e)
            {
                TextBoxDecimalMoney_Leave(sender, e);
            }

            private void txtCantidadGasto_KeyPress(object sender, KeyPressEventArgs e)
            {
                try
                {
                    if ((Keys)e.KeyChar == Keys.Enter)
                    {
                        //this.GetNextControl((Control)sender, true).Focus();
                        txtPagoGasto.Focus();
                    }
                }
                catch (Exception ex) { MuestraExcepcion(ex); }
            }

            private void dtpFechaPagoGasto_KeyPress(object sender, KeyPressEventArgs e)
            {
                try
                {
                    if ((Keys)e.KeyChar == Keys.Enter)
                    {
                        //this.GetNextControl((Control)sender, true).Focus();
                        btnAgregaGasto.Focus();
                    }
                }
                catch (Exception ex) { MuestraExcepcion(ex); }
            }

            private void btnAgregaGasto_Click(object sender, EventArgs e)
            {
                try
                {
                    //if (GastoSeleccionado == null)
                    //{
                        //if (VerificaFacturaRegistrada(txtNumeroFactura.Text))
                        //    throw new Exception("El Número de Factura ya fue registrada anteriormente.");
                    GastoId = AgregaGasto(ConvierteTextoAInteger(cmbProveedores.SelectedValue.ToString()), 1, txtNumeroFactura.Text, ConvierteTextoADecimal(txtCantidadGasto.Text), ConvierteTextoADecimal(txtPagoGasto.Text), dtpFechaFactura.Value.Date, "");
                    //}
                    //else
                    //{
                        //gastoId = GastoSeleccionado.Id;
                        //EliminaChequeGasto(GastoSeleccionado.Id);
                        //ActualizaGasto(GastoSeleccionado.Id, ConvierteTextoAInteger(cmbEmpresas.SelectedValue.ToString()), ConvierteTextoAInteger(cmbObras.SelectedValue.ToString()), ConvierteTextoAInteger(cmbTrabajadores.SelectedValue.ToString()), 1, txtNumeroFactura.Text, ConvierteTextoADecimal(txtCantidad.Text), dtpFechaPago.Value.Date, dtpFechaFactura.Value.Date, ConvierteTextoAInteger(cmbTipoPago.SelectedValue.ToString()), txtObservacionesGastos.Text, true);
                        //GastoSeleccionado = null;
                    //}

                    //if ((int)cmbTipoPago.SelectedValue == ((int)TipoPago.Cheque))
                    //AgregaChequeGasto(gastoId, txtNuneroChequeGastos.Text);

                    //if (rdoSemanaGastos.Checked)
                    //    CargaGastos(dtpVerGastosDesde.Value.Date, dtpVerGastosHasta.Value.Date);
                    //else
                    //    CargaGastos(new DateTime(ConvierteTextoAInteger(cmbAñoGastos.Text), cmbMesesGastos.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoGastos.Text), cmbMesesGastos.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoGastos.Text), cmbMesesGastos.SelectedIndex + 1)));

                    //btnAgregar.Text = "Agregar";
                    MuestraMensaje("¡Compra registrada!", "CONFIRMACIÓN COMPRA");

                    decimal cantidadPaga = ConvierteTextoADecimal(txtPagoGasto.Text);
                    if (cantidadPaga > 0)
                    {
                        if (cantidadPaga > ConvierteTextoADecimal(txtCantidadGasto.Text))
                            throw new Exception("La cantidad a Pagar debe ser menor o igual a la cantidad que Debe a Proveedor.");

                        //decimal cantidadAgregaPago;
                        //foreach (EntPedido p in pedidosDeudaCliente)
                        //{
                        //    if (cantidadPaga == 0)
                        //        return;

                        //    if (cantidadPaga > p.Debe)
                        //        cantidadAgregaPago = p.Debe;
                        //    else
                        //        cantidadAgregaPago = cantidadPaga;

                        AumentaPagoGasto(GastoId, cantidadPaga);
                        AgregarPago(GastoId, cantidadPaga,dtpFechaPagoGasto.Value);

                        //    cantidadPaga -= cantidadAgregaPago;
                        //}

                        //CargaGvClientesCredito();
                        //CargaGvPedidosClientesCredito();

                        MuestraMensaje("¡Pago Agregado!", "CONFIRMACIÓN PAGO");
                    }

                    LimpiaTextBox(pnlDatosGasto);
                    int indexEmpresa = cmbProveedores.SelectedIndex;
                    CargaGastosEmpresa(ObtieneEmpresaFromCmb(cmbProveedores).Id);
                    cmbProveedores.SelectedIndex = indexEmpresa;
                }
                catch (Exception ex) { MuestraExcepcion(ex); }
            }

            private void btnCancelaGasto_Click(object sender, EventArgs e)
            {
                try
                {
                    //cmbEmpresas.SelectedIndex = 0;
                    LimpiaTextBox(pnlDatosGasto);

                    ActivaAgregarGasto(true);
                }
                catch (Exception ex) { MuestraExcepcion(ex); }
            }

        #endregion

            private void btnActualizaGasto_Click(object sender, EventArgs e)
            {
                try
                {
                    EntEmpresa empresaGastoSeleccionado = ObtieneEmpresaFromGV(gvProveedorGastos);
                    GastoId = empresaGastoSeleccionado.GastoId; 
                    ActualizaGasto(GastoId,ConvierteTextoAInteger(cmbProveedores.SelectedValue.ToString()), txtNumeroFactura.Text, ConvierteTextoADecimal(txtCantidadGasto.Text), dtpFechaFactura.Value.Date, "",1);
     
                    MuestraMensaje("¡Compra actualizada!", "CONFIRMACIÓN ACTUALIZACIÓN");

                    decimal cantidadPaga = ConvierteTextoADecimal(txtPagoGasto.Text);
                    if (cantidadPaga > 0)
                    {
                        if (cantidadPaga > ConvierteTextoADecimal(txtCantidadGasto.Text))
                            throw new Exception("La cantidad a Pagar debe ser menor o igual a la cantidad que Debe a Proveedor.");

                        AumentaPagoGasto(GastoId, cantidadPaga);
                        AgregarPago(GastoId, cantidadPaga, dtpFechaPagoGasto.Value);


                        MuestraMensaje("¡Pago Agregado!", "CONFIRMACIÓN PAGO");
                    }

                    LimpiaTextBox(pnlDatosGasto);

                    int indexEmpresa = cmbProveedores.SelectedIndex;
                    CargaGastosEmpresa(empresaGastoSeleccionado.Id);
                    cmbProveedores.SelectedIndex = indexEmpresa;

                    ActivaAgregarGasto(true);
                }
                catch (Exception ex) { MuestraExcepcion(ex); }
            }

            private void btnEditarGasto_Click(object sender, EventArgs e)
            {
                try
                {
                    //EntEmpresa empresaGastoSeleccionado = ObtieneEmpresaFromGV(gvProveedorGastos);
                    //CargaDatosGastoEmpresa(empresaGastoSeleccionado);
                    //ActivaAgregarGasto(false);
                }
                catch (Exception ex) { MuestraExcepcion(ex); }
            }

            private void btnEliminarGasto_Click(object sender, EventArgs e)
            {
                try
                {
                    EntEmpresa empresaGastoSeleccionado = ObtieneEmpresaFromGV(gvProveedorGastos);

                    if (MuestraMensajeYesNo("¿Desea Eliminar la compra seleccionada: " + FormatoMoney(empresaGastoSeleccionado.Deuda) + "?", "CONFIRMACIÓN ELIMINAR") == DialogResult.Yes)
                    {
                        ActualizaGasto(empresaGastoSeleccionado.GastoId, empresaGastoSeleccionado.Id, empresaGastoSeleccionado.NumeroFactura, empresaGastoSeleccionado.Deuda, empresaGastoSeleccionado.FechaFactura,"", 0);//ESTATUSID=0:ELIMINADO

                        int indexEmpresa = cmbProveedores.SelectedIndex;
                        CargaProveedores(Program.EmpresaSeleccionada.Id);
                        cmbProveedores.SelectedIndex = indexEmpresa;

                        ActivaAgregar(true);
                    }
                }
                catch (Exception ex) { MuestraExcepcion(ex); }
            }

            private void btnRefrescar_Click(object sender, EventArgs e)
            {
                try
                {
                    CargaProveedores(Program.EmpresaSeleccionada.Id);
                }
                catch (Exception ex) { MuestraExcepcion(ex); }
            }

        private void btnCambiaEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                SeleccionaEmpresa vSeleccionaEmp = new Pantallas.SeleccionaEmpresa(ListaEmpresas);
                if (vSeleccionaEmp.ShowDialog() == DialogResult.OK)
                {
                    ProveedorSeleccionado.EmpresaId = vSeleccionaEmp.EmpresaSeleccionada.Id;
                    new BusProveedores().ActualizaProveedor(ProveedorSeleccionado);
                    //cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == vSeleccionaEmp.EmpresaSeleccionada.Id);
                    CargaProveedores(Program.EmpresaSeleccionada.Id);

                    InicializaPantalla();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbEmpresas_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (Program.CambiaEmpresa)
                {
                    Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                    CargaProveedores(Program.EmpresaSeleccionada.Id);

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
    }
}
