using AiresEntidades;
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
    public partial class Contraseña : Form
    {
        public Contraseña(List<AiresEntidades.EntUsuario> Usuarios)
        {
            InitializeComponent();
            this.Usuarios = Usuarios;
        }

        List<EntUsuario> Usuarios;

        public EntUsuario Usuario { get; set; } 
        //AiresEntidades.EntCatalogoGenerico UsuarioSeleccionado
        //public string CantidadPago { get { return txtCantidadPaga.Text; } }
        //public DateTime FechaPago { get { return dtpFechaPago.Value; } }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            bool usuarioEncontrado=false;
            foreach (EntUsuario u in Usuarios)
            {
                if (textBox1.Text.ToUpper() == u.Usuario.ToUpper() && txtContraseña.Text == u.Contraseña)
                {
                    usuarioEncontrado = true;
                    Usuario = u;
                    break;
                }
            }
            if(!usuarioEncontrado)
                this.DialogResult = DialogResult.Abort;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void txtCantidadPaga_Leave(object sender, EventArgs e)
        {

        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                btnAgregar.PerformClick();
            }
        }

        private void AgregaPago_Load(object sender, EventArgs e)
        {

        }

        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void Contraseña_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.DialogResult == DialogResult.Abort)
                {
                    e.Cancel = true;
                    textBox1.Focus();
                    MessageBox.Show("Usuario y/o Contraseña Incorrecto(s)");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
