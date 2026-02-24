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
    public partial class Empresas: FormBase
    {
        void InicializaPantalla()
        {
            if (Program.UsuarioSeleccionado.Id > 1)
            {
                btnEliminar.Enabled = false;
                btnEditar.Enabled = false;
                pnlDatos.Visible = false;
             }
            cmbRegimenFiscal.SelectedIndex = 0;
            cmbUsoCFDI.SelectedIndex = 0;
            cmbTipoPersona.SelectedIndex = 0;
            cmbTipoTasaIVA.SelectedIndex = 0;
        }

        public Empresas()
        {
            InitializeComponent();
        }

        List<EntEmpresa> ListaEmpresas;
        EntEmpresa EmpresaSeleccionada;

        #region Metodos
        public void CargaEmpresas()
        {
            ListaEmpresas = new BusEmpresas().ObtieneEmpresas();

            gvEmpresas.DataSource = ListaEmpresas;
            dataGridView1.DataSource = ListaEmpresas;

            CargaEmpresasEnPantallas();
        }
        void AgregaEmpresa(int TipoPersonaId, string Nombre, string NombreFiscal, int RegimenFiscalId, string RegimenFiscal, string Direccion, string Calle, string NoExterior, string NoInterior, string Colonia,
            string Localidad, string Municipio, string Estado, string CP, string Telefono, string Telefono2, string RFC, string Email, string Banco, string NumeroCuenta,
            string Sucursal, string CLABE, string NumeroReferencia, string Certificado, string Key, string Clave,
                  int TipoTasaIVAId, string NoCertificado, int TipoFactorId, decimal TasaOCuota, int UsoCFDIId)
        {
            EntEmpresa empresa = new EntEmpresa()
            {
                TipoPersonaId = TipoPersonaId,
                Nombre = Nombre,
                NombreFiscal = NombreFiscal,

                RegimenFiscalId = RegimenFiscalId,

                RegimenFiscal = RegimenFiscal,
                Direccion = Direccion,
                Telefono = Telefono,
                Telefono2 = Telefono2,

                Email = Email,
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

                Certificado = Certificado,
                Key = Key,
                Clave = Clave,

                TipoTasaIVAId = TipoTasaIVAId,
                NoCertificado = NoCertificado,

                TipoFactorId = TipoFactorId,
                TasaOCuota = TasaOCuota,
                UsoCFDIId = UsoCFDIId
            };
            new BusEmpresas().AgregaEmpresa(empresa);
        }
        void ActualizaEmpresa(int EmpresaId, int TipoPersonaId, string Nombre, string NombreFiscal, int RegimenFiscalId, string RegimenFiscal, string Direccion, string Calle, string NoExterior, string NoInterior, string Colonia,
            string Localidad, string Municipio, string Estado, string CP, string Telefono, string Telefono2, string RFC, string Email, string Banco, string NumeroCuenta,
            string Sucursal, string CLABE, string NumeroReferencia, string Certificado, string Key, string Clave,
                    int TipoTasaIVAId, string NoCertificado, int TipoFactorId, decimal TasaOCuota, int UsoCFDIId)
        {
            EntEmpresa empresa = new EntEmpresa()
            {
                Id = EmpresaId,
                TipoPersonaId = TipoPersonaId,
                Nombre = Nombre,
                NombreFiscal = NombreFiscal,

                RegimenFiscalId = RegimenFiscalId,

                RegimenFiscal = RegimenFiscal,
                Direccion = Direccion,
                Telefono = Telefono,
                Telefono2 = Telefono2,

                Email = Email,
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
                Certificado = Certificado,
                Key = Key,
                Clave = Clave,

                TipoTasaIVAId = TipoTasaIVAId,
                NoCertificado = NoCertificado,

                TipoFactorId = TipoFactorId,
                TasaOCuota = TasaOCuota,
                UsoCFDIId = UsoCFDIId
            };
            new BusEmpresas().ActualizaEmpresa(empresa);
        }

        void ActualizaEstatusEmpresa(EntEmpresa Empresa, bool Estatus)
        {
            Empresa.Estatus = Estatus;
            new BusEmpresas().ActualizaEstatusEmpresa(Empresa);
        }
        void CargaDatosEmpresa(EntEmpresa Empresa)
        {
            cmbTipoPersona.SelectedIndex = Empresa.TipoPersonaId - 1;
            txtNombre.Text = Empresa.Nombre;
            txtDireccion.Text = Empresa.Direccion;
            txtTelefono.Text = Empresa.Telefono;
            txtTelefono2.Text = Empresa.Telefono2;
            //txtCelular.Text = Empresa.Celular;
            txtEmail.Text = Empresa.Email;

            txtNombreFiscal.Text = Empresa.NombreFiscal;
            txtRegimenFiscal.Text = Empresa.RegimenFiscal;
            txtRFC.Text = Empresa.RFC;
            txtNoExterior.Text = Empresa.NoExterior;
            txtNoInterior.Text = Empresa.NoInterior;
            txtCalle.Text = Empresa.Calle;
            txtColonia.Text = Empresa.Colonia;
            txtLocalidad.Text = Empresa.Localidad;
            txtMunicipio.Text = Empresa.Municipio;
            txtEstado.Text = Empresa.Estado;
            txtCP.Text = Empresa.CP;

            cmbRegimenFiscal.SelectedIndex = ((List<EntCatalogoGenerico>)cmbRegimenFiscal.DataSource).FindIndex(P => P.Id == Empresa.RegimenFiscalId);
            
            //txtCertificado.Text = Empresa.Certificado;
            //txtKey.Text = Empresa.Key;
            //txtClave.Text = Empresa.Clave;
            txtNoCertificado.Text = Empresa.NoCertificado;

            cmbTipoTasaIVA.SelectedIndex = Empresa.TipoTasaIVAId - 1;

            txtBanco.Text = Empresa.Banco;
            txtSucursal.Text = Empresa.Sucursal;
            txtCLABE.Text = Empresa.CLABE;

            cmbTipoFactor.SelectedIndex = Empresa.TipoFactorId - 1;
            txtTasaOCuota.Text = Empresa.TasaOCuota.ToString("0.000000");
            cmbUsoCFDI.SelectedIndex = ((List<EntCatalogoGenerico>)cmbUsoCFDI.DataSource).FindIndex(P => P.Id == Empresa.UsoCFDIId);

        }


        void CargaEmpresasEnPantallas()
        {
            //Form forma = BuscaFormaBase(new Ventas().Titulo);
            //if (forma != null)
            //{
            //    ((Ventas)forma).CargaEmpresas();
            //}

            //forma = BuscaFormaBase(new Clientes().Titulo);
            //if (forma != null)
            //{
            //    ((Clientes)forma).CargaEmpresas();
            //}

            //forma = BuscaFormaBase(new Reportes().Titulo);
            //if (forma != null)
            //{
            //    ((Reportes)forma).CargaEmpresas();
            //}
        }

        bool VerificaTextosIguales(string Texto1, string Texto2)
        {
            if (Texto1 == Texto2)
                return true;
            else
                return false;
        }

        void FiltrarEmpresasPorDescripcion(List<EntEmpresa> ListaEmpresas, string DescripcionEmpresa, DataGridView GridViewEmpresas)
        {
            //List<EntCliente> clientes = (List<EntCliente>)gvClientes.DataSource;

            var empresasFiltro = from c in ListaEmpresas
                                 where c.Descripcion.ToUpper().Contains(DescripcionEmpresa.ToUpper())
                                 select c;

            GridViewEmpresas.DataSource = null;
            GridViewEmpresas.DataSource = empresasFiltro.ToList();
        }

        //public EntEmpresa ObtieneEmpresaFromGV(DataGridView GridViewEmpresa)
        //{
        //    if (GridViewEmpresa.CurrentRow == null)
        //        return null;
        //    return (EntEmpresa)((List<EntEmpresa>)GridViewEmpresa.DataSource)[GridViewEmpresa.CurrentRow.Index];
        //}

        void ActivaAgregar(bool Visible)
        {
            btnAgregar.Visible = Visible;
            btnActualizar.Visible = !Visible;
        }
        #endregion
        public void CargaCatalogoRegimen()
        {
            //ListaEmpresas = new BusEmpresas().ObtieneCatalogoRegimen();
            cmbRegimenFiscal.DataSource = new BusEmpresas().ObtieneCatalogoRegimen();
        }
        public void CargaCatalogoUsoCFDI()
        {
            //ListaEmpresas = new BusEmpresas().ObtieneCatalogoRegimen();
            cmbUsoCFDI.DataSource = new BusEmpresas().ObtieneCatalogoUsoCFDI();
        }

        private void Empresas_Load(object sender, EventArgs e)
        {
            try
            {
                CargaCatalogoRegimen();
                CargaCatalogoUsoCFDI();
                InicializaPantalla();
                CargaEmpresas();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                    MandaExcepcion("Ingrese Nombre de Empresa");
                if (!VerificaTextosIguales(txtClave.Text, txtConfirmaClave.Text))
                {
                    tcNuevaEmpresa.SelectedIndex = 1;
                    txtClave.Focus();
                    MandaExcepcion("Las Claves no cohinciden");
                }
                else
                {
                    EntCatalogoGenerico regimen = ObtieneCatalogoGenericoFromCmb(cmbRegimenFiscal);
                    EntCatalogoGenerico usoCFDI = ObtieneCatalogoGenericoFromCmb(cmbUsoCFDI);
                    AgregaEmpresa(cmbTipoPersona.SelectedIndex + 1, txtNombre.Text, txtNombreFiscal.Text,
                        regimen.Id, txtRegimenFiscal.Text, txtDireccion.Text, txtCalle.Text, txtNoExterior.Text, txtNoInterior.Text,
                        txtColonia.Text, txtLocalidad.Text, txtMunicipio.Text, txtEstado.Text, txtCP.Text, txtTelefono.Text, txtTelefono2.Text,
                        txtRFC.Text, txtEmail.Text, txtBanco.Text, txtNoCuenta.Text, txtSucursal.Text, txtCLABE.Text, txtNoReferencia.Text,
                        txtCertificado.Text, txtKey.Text, txtClave.Text,
                        cmbTipoTasaIVA.SelectedIndex + 1, txtNoCertificado.Text, 1, 0.16m, usoCFDI.Id);
                }

                CargaEmpresas();
                LimpiaTextBox(pnlDatos, true);
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
                    MandaExcepcion("Ingrese Nombre de Empresa");
                if (!VerificaTextosIguales(txtClave.Text, txtConfirmaClave.Text))
                {
                    tcNuevaEmpresa.SelectedIndex = 1;
                    txtClave.Focus();
                    MandaExcepcion("Las Claves no cohinciden");
                }
                else
                {
                    EntCatalogoGenerico regimen = ObtieneCatalogoGenericoFromCmb(cmbRegimenFiscal);
                    EntCatalogoGenerico usoCFDI = ObtieneCatalogoGenericoFromCmb(cmbUsoCFDI);
                    ActualizaEmpresa(EmpresaSeleccionada.Id, cmbTipoPersona.SelectedIndex + 1, txtNombre.Text, txtNombreFiscal.Text,
                        regimen.Id, txtRegimenFiscal.Text, txtDireccion.Text, txtCalle.Text, txtNoExterior.Text, txtNoInterior.Text, 
                        txtColonia.Text, txtLocalidad.Text, txtMunicipio.Text, txtEstado.Text, txtCP.Text, txtTelefono.Text, txtTelefono2.Text, 
                        txtRFC.Text, txtEmail.Text, txtBanco.Text, txtNoCuenta.Text, txtSucursal.Text, txtCLABE.Text, txtNoReferencia.Text, 
                        txtCertificado.Text, txtKey.Text, txtClave.Text,
                        cmbTipoTasaIVA.SelectedIndex + 1, txtNoCertificado.Text, 1, 0.16m, usoCFDI.Id);

                }
                CargaEmpresas();
                LimpiaTextBox(pnlDatos, true);

                ActivaAgregar(true);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiaTextBox(pnlDatos);
                //EmpresaSeleccionada = null;
                ActivaAgregar(true);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }
        
        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                EmpresaSeleccionada = ObtieneEmpresaFromGV(gvEmpresas);
                CargaDatosEmpresa(EmpresaSeleccionada);
                ActivaAgregar(false);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvEmpresas_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnEditar.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvEmpresas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnEditar.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnEmpresaBusqueda_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEmpresaBusqueda.Text))
                    FiltrarEmpresasPorDescripcion(ListaEmpresas, txtEmpresaBusqueda.Text, gvEmpresas);
                else
                    FiltrarEmpresasPorDescripcion(ListaEmpresas, "", gvEmpresas);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtEmpresaBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEmpresaBusqueda.Text))
                    FiltrarEmpresasPorDescripcion(ListaEmpresas, txtEmpresaBusqueda.Text, gvEmpresas);
                else
                    FiltrarEmpresasPorDescripcion(ListaEmpresas, "", gvEmpresas);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                EntEmpresa empresaSeleccionada = ObtieneEmpresaFromGV(gvEmpresas);
                if (MuestraMensajeYesNo(string.Format("¿Desea Eliminar la empresa seleccionada? Empresa: {0}", empresaSeleccionada.Nombre)) == DialogResult.Yes)
                {
                    empresaSeleccionada.Estatus = false;
                    new BusEmpresas().ActualizaEstatusEmpresa(empresaSeleccionada);

                    CargaEmpresas();
                    MuestraMensaje("Empresa Eliminada", "CONFIRMACIÓN");
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                CargaEmpresas();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

    }
}
