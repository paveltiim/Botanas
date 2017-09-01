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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private Form BuscaForma(string Titulo)
        {
            foreach (Form v in this.ParentForm.MdiChildren)
                if (v.Text == Titulo)
                    return v;
            return null;
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            try
            {
                Clientes l = (Clientes)BuscaForma(new Clientes().Titulo);
                if (l == null)
                {
                    l = new Clientes();
                    l.MdiParent = this.ParentForm;
                    l.Show();
                }
                else
                {
                    l.VerificaEmpresa();
                    l.BringToFront();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            try
            {
                Productos a = (Productos)BuscaForma(new Productos().Titulo);
                if (a == null)
                {
                    a = new Productos();
                    a.MdiParent = this.ParentForm;
                    a.Show();
                }
                else
                {
                    a.VerificaEmpresa();
                    a.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEmpresas_Click(object sender, EventArgs e)
        {
            try
            {
                Proveedores a = (Proveedores)BuscaForma(new Proveedores().Titulo);
                if (a == null)
                {
                    a = new Proveedores();
                    a.MdiParent = this.ParentForm;
                    a.Show();
                }
                else
                {
                    a.VerificaEmpresa();
                    a.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            try
            {
                Ventas a = (Ventas)BuscaForma(new Ventas().Titulo);
                if (a == null)
                {
                    a = new Ventas();
                    a.MdiParent = this.ParentForm;
                    a.Show();
                    if (Program.EmpresaSeleccionada == null)
                        a.Close();
                }
                else
                {
                    a.VerificaEmpresa();
                    a.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Reportes a = (Reportes)BuscaForma(new Reportes().Titulo);
                if (a == null)
                {
                    a = new Reportes();
                    a.MdiParent = this.ParentForm;
                    a.Show();
                    if (Program.EmpresaSeleccionada == null)
                        a.Close();
                }
                else
                {
                    a.VerificaEmpresa();
                    a.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Proveedores a = (Proveedores)BuscaForma(new Proveedores().Titulo);
                if (a == null)
                {
                    a = new Proveedores();
                    a.Show();
                    a.MdiParent = this.ParentForm;                    
                }
                else
                    a.BringToFront();

                a.MuestraCompras();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
