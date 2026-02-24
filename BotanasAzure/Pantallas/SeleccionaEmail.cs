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
    public partial class SeleccionaEmail : Form
    {
        public SeleccionaEmail(EntCliente Cliente)
        {
            this.Cliente = Cliente;
            InitializeComponent();
        }
        public EntCliente Cliente { get; set; }

        private void SeleccionaEmpresa_Load(object sender, EventArgs e)
        {
            try
            {
                txtEmail.Text = this.Cliente.Email;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        
        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cliente.Email = txtEmail.Text;
                this.Cliente.Email2 = "";
                this.Cliente.Email3 = "";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
