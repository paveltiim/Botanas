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
    public partial class Trabajadores : FormBase
    {
        public Trabajadores()
        {
            InitializeComponent();
        }

        EntTrabajador TrabajadorSeleccionado;
        List<EntTrabajador> ListaClientes = new List<EntTrabajador>();
        /// <summary>
        /// return ObtieneCatalogoGenericoFromCmb(cmbAlmacenes).Id; 
        /// </summary>
        int EstablecimientoId { get { return ObtieneCatalogoGenericoFromCmb(cmbAlmacenes).Id; } }

        #region Metodos
        public void CargaGvTrabajadores(int EstablecimientoId)
        {
            ListaClientes = new BusTrabajadores().ObtieneTrabajadores(EstablecimientoId);
            gvTrabajadores.DataSource = ListaClientes;

            CargaTrabajadoresEnPantallas();
        }
        public void CargaGvTrabajadores(List<EntTrabajador> ListaTrabajadores)
        {
            gvTrabajadores.DataSource = null;
            gvTrabajadores.DataSource = ListaTrabajadores;
            gvTrabajadores.Refresh();
        }

        public void AgregaTrabajador(int EstablecimientoId, int TipoTrabajadorId, string Nombre, string Direccion, string Calle, string NoExterior, string NoInterior, string Colonia,
        string Localidad, string Municipio, string Estado, string CP,
        string Telefono, string Telefono2, string Celular, string RFC, string Curp, string Credencial,
        string Email, string Email2, string Email3, string Banco, string NumeroCuenta,
        string Sucursal, string CLABE, string NumeroReferencia, int FormaPagoId, int RutaId)
        {
            EntTrabajador Trabajador = new EntTrabajador()
            {
                TipoTrabajadorId = TipoTrabajadorId,
                EstablecimientoId = EstablecimientoId,
                Nombre = Nombre,
                Direccion = Direccion,
                Telefono = Telefono,
                Telefono2 = Telefono2,
                Celular = Celular,
                Email = Email,
                Email2 = Email2,
                Email3 = Email3,
                RFC = RFC,
                Curp = Curp,
                NoCredencial = Credencial,
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
                RutaId = RutaId,
                FormaPagoId = FormaPagoId,
            };
            Trabajador.Id = new BusTrabajadores().AgregaTrabajador(Program.UsuarioSeleccionado.CompañiaId,EstablecimientoId,Trabajador);
        }

        public void ActualizaTrabajador(int TrabajadorId, int TipoTrabajadorId, string Nombre,
            string Direccion, string Calle, string NoExterior, string NoInterior, string Colonia,
            string Localidad, string Municipio, string Estado, string CP,
            string Telefono, string Telefono2, string Celular, string RFC, string Curp, string Credencial,
            string Email, string Email2, string Email3, string Banco, string NumeroCuenta,
            string Sucursal, string CLABE, string NumeroReferencia, int FormaPagoId, int RutaId)
        {
            EntTrabajador Cliente = new EntTrabajador()
            {
                Id = TrabajadorId,
                TipoTrabajadorId = TipoTrabajadorId,
                Nombre = Nombre,
                Direccion = Direccion,
                Telefono = Telefono,
                Telefono2 = Telefono2,
                Celular = Celular,
                Email = Email,
                Email2 = Email2,
                Email3 = Email3,
                RFC = RFC,
                Curp = Curp,
                NoCredencial = Credencial,
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
                RutaId = RutaId
            };
            new BusTrabajadores().ActualizaTrabajador(Cliente);
        }

        void ActualizaEstatusTrabajador(EntTrabajador Trabajador, bool Estatus)
        {
            Trabajador.Estatus = Estatus;
            new BusTrabajadores().ActualizaEstatusTrabajador(Trabajador);
        }

        void CargaDatosTrabajador(EntTrabajador Trabajador)
        {
            txtNombre.Text = Trabajador.Nombre;
            txtDireccion.Text = Trabajador.Direccion;
            txtTelefono.Text = Trabajador.Telefono;
            txtTelefono2.Text = Trabajador.Telefono2;
            txtCelular.Text = Trabajador.Celular;
            //txtEmail.Text = Trabajador.Email;
            //txtEmail2.Text = Trabajador.Email2;
            //txtEmail3.Text = Trabajador.Email3;
            //txtNombreFiscal.Text = Trabajador.NombreFiscal;
            //txtRFC.Text = Trabajador.RFC;
            txtCurp.Text = Trabajador.Curp;
            txtNoCredencial.Text = Trabajador.NoCredencial;
            //txtNoExterior.Text = Trabajador.NoExterior;
            //txtNoInterior.Text = Trabajador.NoInterior;
            //txtCalle.Text = Trabajador.Calle;
            //txtColonia.Text = Trabajador.Colonia;
            //txtLocalidad.Text = Trabajador.Localidad;
            //txtMunicipio.Text = Trabajador.Municipio;
            //txtEstado.Text = Trabajador.Estado;
            //txtCP.Text = Trabajador.CP;
            cmbTipoTrabajador.SelectedIndex = Trabajador.TipoTrabajadorId - 1;
            cmbRutas.SelectedIndex = Trabajador.RutaId - 1;//((List<EntCatalogoGenerico>)cmbRutas.DataSource).FindIndex(P => P.Id == Trabajador.RutaId);
        }

        /// <summary>
        /// Filtra Clientes por los distintos parametros, y los carga en gvClientes.
        /// </summary>
        /// <param name="Trabajadores"></param>
        /// <param name="Nombre"></param>
        void FiltrarTrabajadores(List<EntTrabajador> Trabajadores, string Nombre)
        {
            //List<EntCliente> clientes = (List<EntCliente>)gvClientes.DataSource;

            var clientesFiltro = from c in Trabajadores
                                 where c.Nombre.ToUpper().Contains(Nombre.ToUpper())
                                 select c;

            gvTrabajadores.DataSource = null;
            gvTrabajadores.DataSource = clientesFiltro.ToList();
        }

        void CargaTrabajadoresEnPantallas()
        {
            Form forma = BuscaFormaBase(new Clientes().Titulo);
            if (forma != null)
            {
                //((Clientes)forma).CargaClientes();
            }
            //forma = BuscaFormaBase(new RegistrosRentas(this.EmpresaSeleccionadaId).Titulo);
            //if (forma != null)
            //{
            //    ((RegistrosRentas)forma).CargaCmbCobradores();
            //}
        }
        #endregion
        void CargaAlmacenes()
        {
            List<EntCatalogoGenerico> almacenes = new BusEmpresas().ObtieneAlmacenes(Program.UsuarioSeleccionado.CompañiaId, Program.UsuarioSeleccionado.Id);
            cmbAlmacenes.DataSource = almacenes;
            if (almacenes.Count == 0)
                MuestraMensajeError("SIN ALMACENES DISPONIBLES PARA EL USUARIO");
            else
                cmbAlmacenes.SelectedIndex = 0;
        }

        void InicializaPantalla()
        {
            LimpiaTextBox(pnlDatos, true);
            TrabajadorSeleccionado = null;
            ActivaAgregar(true);
        }
        private void Clientes_Load(object sender, EventArgs e)
        {
            try
            {
                InicializaPantalla();
                //CargaEmpresas();

                //if (Program.EmpresaSeleccionada == null)
                //    Program.EmpresaSeleccionada = SeleccionaEmpresa();

                //cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);

                CargaAlmacenes();
                CargaGvTrabajadores(this.EstablecimientoId);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                //int establecimientoId = this.EstablecimientoId;
                if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                    MandaExcepcion("Ingrese Nombre del Cliente");
                else
                    AgregaTrabajador(this.EstablecimientoId, cmbTipoTrabajador.SelectedIndex + 1, txtNombre.Text
                            , txtDireccion.Text, "", "", ""
                            , "", "", "", "", ""
                            , txtTelefono.Text, txtTelefono2.Text, txtCelular.Text, "", txtCurp.Text, txtNoCredencial.Text
                            , "", "", "", "", ""
                            , "", "", "", 0, cmbRutas.SelectedIndex + 1);
                //AgregaCliente(Program.EmpresaSeleccionada.Id, txtNombre.Text, txtNombreFiscal.Text, txtDireccion.Text, txtCalle.Text, txtNoExterior.Text, txtNoInterior.Text, txtColonia.Text, txtLocalidad.Text, txtMunicipio.Text, txtEstado.Text, txtCP.Text, txtTelefono.Text, txtTelefono2.Text, txtCelular.Text, txtRFC.Text, txtEmail.Text, txtBanco.Text, txtNumeroCuenta.Text, txtSucursal.Text, txtCLABE.Text, txtNumeroReferencia.Text);

                btnCancelar.PerformClick();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        void ActivaAgregar(bool Visible)
        {
            btnAgregar.Visible = Visible;
            btnActualizar.Visible = !Visible;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CargaGvTrabajadores(this.EstablecimientoId);
                LimpiaTextBox(pnlDatos, true);
                TrabajadorSeleccionado = null;
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
                    MandaExcepcion("Ingrese Nombre del Cliente");
                else
                    ActualizaTrabajador(TrabajadorSeleccionado.Id, cmbTipoTrabajador.SelectedIndex + 1, txtNombre.Text
                        , txtDireccion.Text, "", "", "", "", ""
                        , "", "", ""
                        , txtTelefono.Text, txtTelefono2.Text, txtCelular.Text, "", txtCurp.Text, txtNoCredencial.Text
                        , "", "", "", "", ""
                            , "", "", "", 0, cmbRutas.SelectedIndex + 1);

                CargaGvTrabajadores(this.EstablecimientoId);
                btnCancelar.PerformClick();
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
                TrabajadorSeleccionado = ObtieneTrabajadorFromGV(gvTrabajadores);

                if (MuestraMensajeYesNo("¿Desea Eliminar al Trabajador: " + TrabajadorSeleccionado.Nombre + "?", "CONFIRMACIÓN ELIMINAR") == DialogResult.Yes)
                {
                    ActualizaEstatusTrabajador(TrabajadorSeleccionado, false);
                    TrabajadorSeleccionado = null;

                    CargaGvTrabajadores(this.EstablecimientoId);
                    btnCancelar.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                TrabajadorSeleccionado = ObtieneTrabajadorFromGV(gvTrabajadores);
                CargaDatosTrabajador(TrabajadorSeleccionado);
                ActivaAgregar(false);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }


        private void gvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TrabajadorSeleccionado = ObtieneTrabajadorFromGV(gvTrabajadores);
                CargaDatosTrabajador(TrabajadorSeleccionado);
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

        private void txtClienteBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                btnBuscarCliente.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                FiltrarTrabajadores(ListaClientes, txtClienteBusqueda.Text);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRefresca_Click(object sender, EventArgs e)
        {
            try
            {
                CargaGvTrabajadores(this.EstablecimientoId);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        void OrdenarPorNombre(DataGridViewColumn GridViewColumn, List<EntTrabajador> Trabajadores, SortOrder Orden)
        {
            var pedidosOrdenados = from p in Trabajadores
                                   orderby p.Nombre ascending
                                   select p;
            if (Orden == SortOrder.Descending)
            {
                pedidosOrdenados = from p in Trabajadores
                                   orderby p.Nombre descending
                                   select p;
            }
            CargaGvTrabajadores(pedidosOrdenados.ToList());

            GridViewColumn.HeaderCell.SortGlyphDirection = Orden;
        }
        void OrdenarPorRuta(DataGridViewColumn GridViewColumn, List<EntTrabajador> Trabajadores, SortOrder Orden)
        {
            var pedidosOrdenados = from p in Trabajadores
                                   orderby p.RutaId ascending
                                   select p;
            if (Orden == SortOrder.Descending)
            {
                pedidosOrdenados = from p in Trabajadores
                                   orderby p.RutaId descending
                                   select p;
            }
            CargaGvTrabajadores(pedidosOrdenados.ToList());

            GridViewColumn.HeaderCell.SortGlyphDirection = Orden;
        }
        private void gvClientes_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0) //NOMBRE
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                        OrdenarPorNombre(((DataGridView)sender).Columns[e.ColumnIndex], (List<EntTrabajador>)((DataGridView)sender).DataSource,
                                                SortOrder.Ascending);
                    else
                        OrdenarPorNombre(((DataGridView)sender).Columns[e.ColumnIndex], (List<EntTrabajador>)((DataGridView)sender).DataSource,
                                                SortOrder.Descending);
                }
                else if (e.ColumnIndex == 5)//RUTA
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                        OrdenarPorRuta(((DataGridView)sender).Columns[e.ColumnIndex], (List<EntTrabajador>)((DataGridView)sender).DataSource,
                                                SortOrder.Ascending);
                    else
                        OrdenarPorRuta(((DataGridView)sender).Columns[e.ColumnIndex], (List<EntTrabajador>)((DataGridView)sender).DataSource,
                                                SortOrder.Descending);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
    }
}
