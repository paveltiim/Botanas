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
        public SeleccionaEmail()
        {
            InitializeComponent();
        }
        
        public string EmailSeleccionado { get { return txtEmail.Text; } }

        private void SeleccionaEmpresa_Load(object sender, EventArgs e)
        {

        }
        
        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}
