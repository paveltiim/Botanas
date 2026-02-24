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
    public partial class AgregaProveedor : FormBase
    {
        int EmpresaId { get; set; }
        public int ProveedorId { get; set; }
        public EntProveedor ProveedorAgregado { get; set; }

        public AgregaProveedor(int EmpresaId)
        {
            InitializeComponent();
            this.EmpresaId = EmpresaId;
        }

        void AgregarProveedor(int EmpresaId, string Nombre, string NombreFiscal, string RFC, string Direccion, string Telefono, string Telefono2, string Email, string Contacto, string TelefonoContacto, string Banco, string NumeroCuenta, string Sucursal, string CLABE, string NumeroReferencia, int TipoMonedaId)
        {
            this.ProveedorAgregado = new EntProveedor()
            {
                EmpresaId = EmpresaId,
                Nombre = Nombre,
                NombreFiscal = NombreFiscal,
                RFC = RFC,
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
                //TipoMonedaId = TipoMonedaId,
                Fecha = DateTime.Now
            };
            this.ProveedorId = new BusProveedores().AgregaProveedor(ProveedorAgregado);
            this.ProveedorAgregado.Id = this.ProveedorId;
        }


        private void AgregaProveedor_Load(object sender, EventArgs e)
        {
            try
            {
                cmbTipoMoneda.SelectedIndex = 0;
                txtNombre.Focus();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                    MandaExcepcion("Ingrese Nombre de Proveedor");
                else
                    AgregarProveedor(Program.UsuarioSeleccionado.CompañiaId, txtNombre.Text.Trim(), txtNombreFiscal.Text.Trim(), txtRFC.Text,
                        txtDireccion.Text, txtTelefono.Text, txtTelefono2.Text, txtEmail.Text, txtContacto.Text, txtTelefonoContacto.Text,
                        txtBanco.Text, txtNumeroCuenta.Text, txtSucursal.Text, txtCLABE.Text, txtNumeroReferencia.Text,
                        cmbTipoMoneda.SelectedIndex + 1);
            }
            catch (Exception ex) { this.DialogResult = DialogResult.Abort; MuestraExcepcion(ex); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void AgregaProveedor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.Abort)
                e.Cancel = true;
        }
    }
}
