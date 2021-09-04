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
        enum TiposUsuario
        {
            MASTER = 1,
            COMPRAS = 2,
            COBRANZA = 3,
            CONTADOR = 4,
            GERENTECOMERCIAL = 5,
            ALMACEN = 6,
            VENTAS = 7,
            FACTURACION = 8,
            ADMINISTRADORGENERAL = 10,
            ADMINISTRADORBASICO = 11
        }

        TiposUsuario TipoUsuarioLogin { get; set; }

        public Menu(int TipoUsuario)
        {
            InitializeComponent();
            this.TipoUsuarioLogin = (TiposUsuario)TipoUsuario;
        }

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



        private void Menu_Load(object sender, EventArgs e)
        {
            //if (Program.UsuarioSeleccionado.Supervisor)
            //    btnUsuarios.Visible = true;
            switch (this.TipoUsuarioLogin)
            {
                case TiposUsuario.COBRANZA:
                    btnProductos.Visible = false;
                    btnVentas.Visible = false;
                    btnInventario.Visible = false;
                    btnVentas.Visible = true;

                    btnEmpresas.Enabled = false;
                    break;
                case TiposUsuario.COMPRAS:
                    btnVentas.Visible = false;
                    btnClientes.Visible = false;
                    btnEmpresas.Enabled = false;
                    break;
                case TiposUsuario.CONTADOR:
                    btnProductos.Visible = false;
                    btnVentas.Visible = false;
                    btnClientes.Visible = false;
                    btnEmpresas.Enabled = false;
                    break;
                case TiposUsuario.GERENTECOMERCIAL:
                    btnProductos.Visible = false;
                    btnVentas.Visible = false;
                    btnEmpresas.Enabled = false;
                    break;
                case TiposUsuario.ALMACEN:
                    btnVentas.Visible = false;
                    btnClientes.Visible = false;
                    btnInventario.Visible = false;
                    btnEmpresas.Enabled = false;
                    break;
                case TiposUsuario.VENTAS:
                    btnClientes.Visible = false;
                    btnEmpresas.Enabled = false;
                    break;
                case TiposUsuario.FACTURACION:
                    btnProductos.Visible = false;
                    btnInventario.Visible = false;
                    btnEmpresas.Enabled = false;
                    break;
                case TiposUsuario.ADMINISTRADORGENERAL:
                    break;
                case TiposUsuario.ADMINISTRADORBASICO:
                    btnEmpresas.Enabled = false;
                    break;
                case TiposUsuario.MASTER:
                    break;
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Entradas a = (Entradas)BuscaForma(new Entradas().Titulo);
                if (a == null)
                {
                    a = new Entradas();
                    a.MdiParent = this.ParentForm;
                    a.Show();
                }
                else
                {
                    //a.VerificaEmpresa();
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
                Registros a = (Registros)BuscaForma(new Registros().Titulo);
                if (a == null)
                {
                    a = new Registros();
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

        private void btnReportes_Click(object sender, EventArgs e)
        {
            try
            {
                ReportesGenerales a = (ReportesGenerales)BuscaForma(new ReportesGenerales().Titulo);
                if (a == null)
                {
                    a = new ReportesGenerales();
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
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSalidas_Click(object sender, EventArgs e)
        {
            try
            {
                Salidas a = (Salidas)BuscaForma(new Salidas().Titulo);
                if (a == null)
                {
                    a = new Salidas();
                    a.MdiParent = this.ParentForm;
                    a.Show();
                }
                else
                {
                    //a.VerificaEmpresa();
                    a.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
