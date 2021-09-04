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
    public partial class Contraseña : Form
    {
        List<EntUsuario> Usuarios;
        public EntUsuario UsuarioLogin { get; set; }
        public EntUsuario Usuario { get; set; }

        public Contraseña()
        {
            InitializeComponent();
            this.Usuarios = new BusUsuarios().ObtieneUsuarios();
        }

        public Contraseña(List<AiresEntidades.EntUsuario> Usuarios)
        {
            InitializeComponent();
            this.Usuarios = Usuarios;
        }
        public void CargaNombreUsuario()
        {
            txtUsuario.Text = this.Usuarios[0].Usuario;
        }


        private void Contraseña_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            bool usuarioEncontrado=false;
            foreach (EntUsuario u in Usuarios)
            {
                if (txtUsuario.Text.ToUpper() == u.Usuario.ToUpper() && txtContraseña.Text == u.Contraseña)
                {
                    usuarioEncontrado = true;
                    UsuarioLogin = u;
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
                    txtUsuario.Focus();
                    MessageBox.Show("Usuario y/o Contraseña Incorrecto(s)");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
