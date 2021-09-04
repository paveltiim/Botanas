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
    public partial class Inicio : Form
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
            ADMINISTRADORBASICO = 11,
            ADMINISTRADORCASAR = 12
        }
        public Inicio()
        {
            InitializeComponent();
        }
        #region Metodos

        private Form BuscaForma(string titulo)
        {
            foreach (Form v in this.MdiChildren)
                if (v.Text == titulo)
                    return v;
            return null;
        }
        bool Vencimiento;
        void VerificaVencimiento()
        {
            new BusVencimiento().VerificaVencimiento();
            List<EntVencimiento> lst = new BusVencimiento().ObtieneVencimientoActual();
            if (lst.Count <= 0)
            {
                Vencimiento = true;
                MessageBox.Show("Su Sistema ha sido bloqueado, debido a que ha llegado a su Fecha de Vencimiento. \n\nFavor de comunicarse con TIIM para seguir utilizando su Sistema.                                   Gerente Admin. Anabel Araujo: 6681013253");
                this.Close();
            }
        }
        string EmailPrincipal ="pavel_tiim@hotmail.com";// "pavel_tiim@hotmail.com";
        void VerificaExportacion(int EmpresaId)
        {
            //EntCatalogoGenerico reg = new BusVencimiento().ObtieneUltimoRegistroSincronizacion();//new BusVencimiento().ObtieneRegistroSincronizacion(Fecha);

            //DateTime fechaInicial = new DateTime(2017, 10, 4);
            //DateTime fechaFinal = new DateTime(2017, 11, 22);

            DateTime fechaInicial = new DateTime(2017, 10, 2);
            DateTime fechaFinal = new DateTime(2017, 10, 2);
            //for (DateTime x = reg.Fecha.AddDays(1); x <= DateTime.Today.AddDays(-1); x=x.AddDays(1))
            for (DateTime x = fechaInicial; x <= fechaFinal; x = x.AddDays(1))
            {
                    DateTime fecha = x;
                new Registros().ExportarVentas(EmpresaId, fecha, EmailPrincipal);
                //new BusVencimiento().AgregarRegistroSincronizacion(fecha);
            }
            
            //if (reg.Id <= 0)
            //{
            //    //Vencimiento = true;
            //    //MessageBox.Show("Su Sistema ha sido bloqueado, debido a que ha llegado a su Fecha de Vencimiento. \n\nFavor de comunicarse con TIIM para seguir utilizando su Sistema.                                   Gerente Admin. Anabel Araujo: 6681013253");
            //    //this.Close();
            //    new Registros().ExportarVentas(EmpresaId,Fecha, EmailPrincipal);
            //    //ingresa registro sincronizacion
            //    new BusVencimiento().AgregarRegistroSincronizacion(Fecha);
            //}
        }
        #endregion

        int TipoUsuarioLoginId { get; set; }
        void MuestraBotonesMenu(TiposUsuario TipoUsuario)
        {
            switch (TipoUsuario)
            {
                case TiposUsuario.ADMINISTRADORGENERAL:
                    break;
                case TiposUsuario.ADMINISTRADORBASICO:
                    break;
                case TiposUsuario.MASTER:
                    tsbEntradas.Visible = true;
                    tsbProductos.Visible = true;
                    break;
            }

        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            try
            {
                Program.ConexionIdActual = 1;// 1:PRODUCCION; 2:PRUEBAS
                Program.EmpresaSeleccionada = new EntEmpresa() { Id = 1, Nombre = "-BOTANAS JAVI-" };


                Pantallas.Contraseña vInicioSesion = new Pantallas.Contraseña();
                vInicioSesion.CargaNombreUsuario();
                if (vInicioSesion.ShowDialog() == DialogResult.OK)
                {
                    Program.UsuarioSeleccionado = vInicioSesion.UsuarioLogin;
                    //if (!vInicioSesion.UsuarioLogin.Administrador)
                    //    Program.EmpresaSeleccionada = new AiresNegocio.BusEmpresas().ObtieneEmpresa(vInicioSesion.UsuarioLogin.EmpresaId);
                }
                else
                    this.Close();

                if (Program.UsuarioSeleccionado != null)
                { 
                    this.Text = " Usuario: " + vInicioSesion.UsuarioLogin.Usuario + " | " + "Conexión:" + Program.ConexionIdActual;
                    this.TipoUsuarioLoginId = vInicioSesion.UsuarioLogin.TipoUsuarioId;

                    MuestraBotonesMenu((TiposUsuario)TipoUsuarioLoginId);

                    Menu a = (Menu)BuscaForma("Menu");
                    if (a == null)
                    {
                        a = new Menu(this.TipoUsuarioLoginId);
                        a.MdiParent = this;
                        a.Show();
                    }
                    else
                        a.BringToFront();
                }

                //switch (version)
                //{
                //    case 1:
                //        Program.UsuarioSeleccionado = usuarios[0];// 1 - admin //new EntUsuario() { Id = 1 };
                //        break;
                //    case 2:
                //        Program.UsuarioSeleccionado = usuarios[3];// 5 - SerdanCasar //new EntUsuario()
                //        break;
                //    case 3:
                //        Contraseña vInicioSesion = new Contraseña(usuarios);

                //        if (vInicioSesion.ShowDialog() != DialogResult.OK)
                //            this.Close();
                //        else
                //            Program.UsuarioSeleccionado = vInicioSesion.Usuario;// new EntUsuario() { Id = 9 };/
                //        break;
                //    case 4:
                //        Program.UsuarioSeleccionado = usuarios[4];// 2 - Martin //new EntUsuario() { Id = 2 };/
                //        break;
                //}

                //if (Program.UsuarioSeleccionado.Id > 1)
                //{
                //    tsbProveedores.Visible = false;
                //    ////tsbInventario.Visible = false;

                //    //VERSION AZURE - YA NO ES NECESASRIO EXPORTACION DE VENTAS
                //    //VerificaExportacion(Program.UsuarioSeleccionado.Id);

                //    ////VerificaExportacion(DateTime.Today.AddDays(-2), Program.UsuarioSeleccionado.Id);
                //    ////VerificaExportacion(DateTime.Today.AddDays(-1), Program.UsuarioSeleccionado.Id);
                //}

                //Menu a = (Menu)BuscaForma("Menu");
                //if (a == null)
                //{
                //    a = new Menu();
                //    a.MdiParent = this;
                //    a.Show();
                //}
                //else
                //    a.BringToFront();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Menu a = (Menu)BuscaForma("Menu");
                if (a == null)
                {
                    a = new Menu();
                    a.MdiParent = this;
                    a.Show();
                }
                else
                    a.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Productos a = (Productos)BuscaForma("Productos");
                if (a == null)
                {
                    a = new Productos();
                    a.MdiParent = this;
                    a.Show();
                    //if (Program.EmpresaSeleccionada == null)
                    //    a.Close();
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

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                Clientes a = (Clientes)BuscaForma("Clientes");
                if (a == null)
                {
                    a = new Clientes();
                    a.MdiParent = this;
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

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            try
            {
                Proveedores a = (Proveedores)BuscaForma(new Proveedores().Titulo);
                if (a == null)
                {
                    a = new Proveedores();
                    a.MdiParent = this;
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

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try
            {
                Ventas a = (Ventas)BuscaForma("Ventas");
                if (a == null)
                {
                    a = new Ventas();
                    a.MdiParent = this;
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
        

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            try
            {
                ClientesCredito a = (ClientesCredito)BuscaForma(new ClientesCredito().Titulo);
                if (a == null)
                {
                    a = new ClientesCredito();
                    a.MdiParent = this;
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
        
        
        private void toolStripButton6_Click_1(object sender, EventArgs e)
        {
            try
            {
                Registros a = (Registros)BuscaForma(new Registros().Titulo);
                if (a == null)
                {
                    a = new Registros();
                    a.MdiParent = this;
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

        private void toolStripButton8_Click_1(object sender, EventArgs e)
        {
            try
            {
                Inventarios a = (Inventarios)BuscaForma(new Inventarios().Titulo);
                if (a == null)
                {
                    a = new Inventarios();
                    a.MdiParent = this;
                    a.Show();
                    //if (Program.EmpresaSeleccionada == null)
                    //    a.Close();
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

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            try
            {
                ReportesGenerales a = (ReportesGenerales)BuscaForma(new ReportesGenerales().Titulo);
                if (a == null)
                {
                    a = new ReportesGenerales();
                    a.MdiParent = this;
                    a.Show();
                    if (Program.EmpresaSeleccionada == null)
                        a.Close();
                }
                else
                    a.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            try
            {
                Empresas a = (Empresas)BuscaForma(new Empresas().Titulo);
                if (a == null)
                {
                    a = new Empresas();
                    a.MdiParent = this;
                    a.Show();
                }
                else
                    a.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            try
            {
                Entradas a = (Entradas)BuscaForma(new Entradas().Titulo);
                if (a == null)
                {
                    a = new Entradas();
                    a.MdiParent = this;
                    a.Show();
                    //if (Program.EmpresaSeleccionada == null)
                    //    a.Close();
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

        private void tsbSalidas_Click(object sender, EventArgs e)
        {
            try
            {
                Salidas a = (Salidas)BuscaForma(new Salidas().Titulo);
                if (a == null)
                {
                    a = new Salidas();
                    a.MdiParent = this;
                    a.Show();
                    //if (Program.EmpresaSeleccionada == null)
                    //    a.Close();
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
