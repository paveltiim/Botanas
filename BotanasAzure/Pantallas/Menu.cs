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
            ADMINISTRADORINSUMOS = 2,
            ADMINISTRADORPRODUCCION = 3,
            ADMINISTRADORALMACEN = 4,
            PUNTOVENTA = 5,
            ADMINISTRADORPUNTOVENTA = 7,
            ADMINISTRADORINVENTARIOS = 9,
            CUENTASPORCOBRARVENTAS = 10,
            GERENTEVENTAS = 11,
            SUPERVISOR = 12, //OSBALDO
            GERENTEALMACEN = 13,
            GERENTEPRODUCCION = 14,
            CYCVISUALIZA = 15//CARLOS ALONSO
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
            btnEntradas.Enabled = false;
            btnSalidas.Enabled = false;
            switch (this.TipoUsuarioLogin)
            {
                case TiposUsuario.ADMINISTRADORINSUMOS:
                    btnProductos.Enabled = true;
                    btnEntradasInsumos.Visible = true;
                    btnEntradas.Enabled = true;
                    btnSalidasInsumos.Visible = true;
                    btnSalidas.Enabled = true;
                    break;
                case TiposUsuario.ADMINISTRADORINVENTARIOS:
                    btnEntradas.Enabled= true;
                    btnEntradasInsumos.Enabled= true;
                    btnSalidasInsumos.Enabled= true;
                    btnSalidas.Enabled = true;
                    btnCotizaciones.Visible = false;
                    break;
                case TiposUsuario.ADMINISTRADORALMACEN:
                    btnProductos.Enabled = true;
                    btnSalidas.Enabled = true;
                    break;
                case TiposUsuario.PUNTOVENTA:
                    btnCotizaciones.Enabled = true;
                    break;
                case TiposUsuario.ADMINISTRADORPUNTOVENTA:
                    btnProductos.Enabled = true;
                    btnSalidas.Enabled = true;
                    btnCotizaciones.Enabled = true;
                    break;
                case TiposUsuario.GERENTEVENTAS:
                    
                    break;
                case TiposUsuario.GERENTEALMACEN://GERENTE (CULIACAN) 
                    btnProductos.Visible = false;
                    btnCotizaciones.Visible = false;
                    btnSalidas.Enabled = true;
                    btnEntradas.Enabled = true;
                    break;
                case TiposUsuario.CYCVISUALIZA://GERENTE (CULIACAN) 
                    btnProductos.Visible = false;
                    btnCotizaciones.Visible = false;
                    btnSalidas.Enabled = true;
                    btnEntradas.Enabled = true;
                    btnInventario.Visible = false;
                    break;
                case TiposUsuario.MASTER:
                    btnProductos.Enabled = true;
                    btnEntradasInsumos.Visible = true;
                    btnSalidasInsumos.Visible = true;
                    btnSalidas.Enabled = true;
                    btnEntradas.Enabled = true;
                    btnCotizaciones.Enabled= true;
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

        private void btnEntradasCompras_Click(object sender, EventArgs e)
        {
            try
            {
                EntradasCompras a = (EntradasCompras)BuscaForma(new EntradasCompras().Titulo);
                if (a == null)
                {
                    a = new EntradasCompras();
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                RegistrosNeue a = (RegistrosNeue)BuscaForma(new RegistrosNeue().Titulo);
                if (a == null)
                {
                    a = new RegistrosNeue();
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

        private void btnSalidasInsumo_Click(object sender, EventArgs e)
        {
            try
            {
                SalidasInsumo a = (SalidasInsumo)BuscaForma(new SalidasInsumo().Titulo);
                if (a == null)
                {
                    a = new SalidasInsumo();
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

        private void btnInventario_Click(object sender, EventArgs e)
        {
            try
            {
                Inventarios a = (Inventarios)BuscaForma(new Inventarios().Titulo);
                if (a == null)
                {
                    a = new Inventarios();
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

        private void btnEntradas_Click(object sender, EventArgs e)
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

        private void btnEntradasInsumos_Click(object sender, EventArgs e)
        {
            try
            {
                EntradasCompras a = (EntradasCompras)BuscaForma(new EntradasCompras().Titulo);
                if (a == null)
                {
                    a = new EntradasCompras();
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

        private void btnEntradas_Click_1(object sender, EventArgs e)
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

        private void btnCotizaciones_Click(object sender, EventArgs e)
        {
            try
            {
                Cotizaciones a = (Cotizaciones)BuscaForma(new Cotizaciones().Titulo);
                if (a == null)
                {
                    a = new Cotizaciones();
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
