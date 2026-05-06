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
            ADMINISTRADORINSUMOS = 2,
            ADMINISTRADORPRODUCCION = 3,
            ADMINISTRADORALMACEN = 4,
            PUNTOVENTA = 5,
            CUENTASPORCOBRAR = 6,
            ADMINISTRADORPUNTOVENTA = 7,
            PUNTOVENTAMENUDEO = 8,
            ADMINISTRADORINVENTARIOS = 9,
            CUENTASPORCOBRARVENTAS=10,
            GERENTEVENTAS = 11,
            SUPERVISOR = 12, //OSBALDO
            GERENTEALMACEN = 13,
            GERENTEPRODUCCION = 14,
            CYCVISUALIZA = 15,
            PREVENTA = 16
        }
        public Inicio()
        {
            InitializeComponent();
            menuStrip1.Renderer = new TopNavRenderer();
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
        string EmailPrincipal ="tiimfacturacion@hotmail.com";// "pavel_tiim@hotmail.com";
        
        #endregion

        int TipoUsuarioLoginId { get; set; }
        void MuestraBotonesMenu(TiposUsuario TipoUsuario)
        {
            // Hide all sub-items initially
            mnuClientesClientes.Visible = false;
            mnuAlmacenEntradasProductos.Enabled = false;
            mnuAlmacenEntradasProductos.Visible = false;
            mnuAlmacenSalidasProducto.Visible = false;
            mnuAlmacenSalidasProducto.Enabled = false;
            mnuAlmacenEntradasInsumos.Visible = false;
            mnuAlmacenEntradasInsumos.Enabled = false;
            mnuAlmacenEntradasTraspasos.Visible = false;
            mnuVentasMayoristas.Visible = false;
            mnuVentasRegistros.Visible = false;
            mnuClientesCredito.Visible = false;
            mnuClientesTrabajadores.Visible = false;
            mnuReportesGenerales.Visible = false;
            mnuReportesGlobales.Visible = false;
            mnuMovilTraspasos.Visible = false;
            mnuMovilRegistros.Visible = false;
            mnuMovilInventario.Visible = false;
            mnuAlmacenInventario.Visible = false;
            mnuAlmacenProductos.Visible = false;
            mnuAlmacenProductos.Enabled = false;
            mnuAlmacenSalidasInsumos.Visible = false;
            mnuVentasPuntoVenta.Visible = false;
            mnuVentasPVDetalle.Visible = false;
            mnuVentasPVVarios.Visible = false;
            mnuVentasPreVenta.Visible = false;
            mnuClientesProveedores.Visible = false;
            mnuClientesEmpresas.Visible = false;

            switch (TipoUsuario)
            {
                case TiposUsuario.ADMINISTRADORINSUMOS:
                    mnuAlmacenInventario.Visible = true;
                    mnuAlmacenProductos.Enabled = true;
                    mnuAlmacenEntradasInsumos.Visible = true;
                    mnuAlmacenEntradasProductos.Visible = true;
                    mnuAlmacenEntradasProductos.Enabled = true;
                    mnuAlmacenEntradasTraspasos.Visible = true;
                    mnuAlmacenSalidasInsumos.Visible = true;
                    mnuAlmacenSalidasProducto.Enabled = true;
                    mnuClientesClientes.Visible = true;
                    mnuVentasRegistros.Visible = true;
                    break;
                case TiposUsuario.ADMINISTRADORPRODUCCION:
                    mnuAlmacenInventario.Visible = true;
                    mnuAlmacenEntradasProductos.Visible = true;
                    mnuAlmacenEntradasProductos.Enabled = true;
                    break;

                case TiposUsuario.ADMINISTRADORALMACEN:
                    mnuAlmacenInventario.Visible = true;
                    mnuClientesClientes.Visible = true;
                    mnuAlmacenSalidasProducto.Enabled = true;
                    mnuVentasMayoristas.Visible = true;
                    mnuVentasRegistros.Visible = true;
                    mnuClientesCredito.Visible = true;
                    mnuClientesTrabajadores.Visible = true;
                    break;                
                case TiposUsuario.PUNTOVENTA://DINA;
                    mnuAlmacenInventario.Visible = true;
                    mnuClientesClientes.Visible = true;
                    mnuVentasPuntoVenta.Visible = true;
                    mnuVentasMayoristas.Visible = true;
                    mnuVentasPVDetalle.Visible = true;
                    mnuVentasPVVarios.Visible = true;
                    mnuAlmacenEntradasTraspasos.Visible = true;
                    mnuVentasRegistros.Visible = true;
                    mnuClientesCredito.Visible = true;
                    mnuClientesTrabajadores.Visible = true;
                    mnuReportesGenerales.Visible = true;
                    mnuReportesGlobales.Visible = true;
                    mnuMovilRegistros.Visible = true;
                    break;
                case TiposUsuario.CUENTASPORCOBRAR://MIRIAM
                    mnuClientesCredito.Visible = true;
                    mnuReportesGenerales.Visible = true;
                    mnuClientesClientes.Visible = true;
                    mnuVentasRegistros.Visible = true;
                    mnuVentasPVVarios.Visible = true;
                    break;
                case TiposUsuario.ADMINISTRADORPUNTOVENTA:
                    mnuAlmacenInventario.Visible = true;
                    mnuAlmacenEntradasTraspasos.Visible = true;
                    mnuClientesClientes.Visible = true;
                    mnuVentasRegistros.Visible = true;
                    mnuVentasMayoristas.Visible = true;
                    mnuClientesTrabajadores.Visible = true;
                    break;
                case TiposUsuario.PUNTOVENTAMENUDEO://CAROLINA;
                    mnuClientesClientes.Visible = true;
                    mnuVentasPuntoVenta.Visible = true;
                    mnuAlmacenEntradasTraspasos.Visible = true;
                    mnuVentasRegistros.Visible = true;
                    mnuClientesCredito.Visible = true;
                    mnuClientesTrabajadores.Visible = true;
                    mnuReportesGenerales.Visible = true;
                    break;

                case TiposUsuario.ADMINISTRADORINVENTARIOS:
                    mnuAlmacenInventario.Visible = true;
                    mnuAlmacenEntradasProductos.Visible = true;
                    mnuAlmacenEntradasProductos.Enabled = true;
                    mnuAlmacenEntradasInsumos.Visible = true;
                    mnuAlmacenEntradasInsumos.Enabled = true;
                    mnuAlmacenSalidasInsumos.Visible = true;
                    mnuAlmacenSalidasProducto.Enabled = true;
                    mnuAlmacenEntradasTraspasos.Visible = true;
                    break;
                case TiposUsuario.CUENTASPORCOBRARVENTAS://KENIA
                    mnuVentasPuntoVenta.Visible = true;
                    mnuVentasMayoristas.Visible = true;
                    mnuAlmacenEntradasTraspasos.Visible = true;
                    mnuVentasRegistros.Visible = true;
                    mnuReportesGenerales.Visible = true;
                    mnuReportesGlobales.Visible = true;
                    mnuClientesClientes.Visible = true;
                    mnuClientesCredito.Visible = true;
                    mnuClientesTrabajadores.Visible = true;
                    mnuMovilRegistros.Visible = true;
                    break;
                case TiposUsuario.GERENTEVENTAS://(PAUL ALVAREZ - )
                    mnuAlmacenProductos.Enabled = true;
                    mnuAlmacenInventario.Visible = true;
                    mnuVentasPreVenta.Visible = true;
                    mnuVentasRegistros.Visible = true;
                    mnuMovilRegistros.Visible = true;
                    mnuReportesGenerales.Visible = true;
                    mnuReportesGlobales.Visible = true;
                    mnuClientesClientes.Visible = true;
                    mnuClientesClientes.Enabled = true;
                    mnuClientesCredito.Visible = true;
                    break;
                case TiposUsuario.SUPERVISOR://OSBALDO (MOCHIS) 
                    mnuAlmacenInventario.Visible = true;
                    mnuAlmacenProductos.Enabled = true;
                    mnuClientesClientes.Visible = true;
                    mnuClientesClientes.Enabled = true;
                    mnuReportesGenerales.Visible = true;
                    mnuReportesGlobales.Visible = true;
                    mnuMovilTraspasos.Visible = true;
                    mnuMovilRegistros.Visible = true;
                    mnuMovilInventario.Visible = true;
                    break;
                case TiposUsuario.GERENTEALMACEN://GERENTE (CULIACAN) 
                    mnuAlmacenInventario.Visible = true;
                    mnuReportesGenerales.Visible = true;
                    break;
                case TiposUsuario.GERENTEPRODUCCION:
                    mnuAlmacenInventario.Visible = true;
                    mnuAlmacenEntradasProductos.Visible = true;
                    mnuAlmacenEntradasProductos.Enabled = true;
                    mnuReportesGenerales.Visible = true;
                    break;
                case TiposUsuario.CYCVISUALIZA://(CARLOS ALONSO-CUL)
                    mnuReportesGenerales.Visible = true;
                    mnuReportesGlobales.Visible = true;
                    mnuClientesClientes.Visible = true;
                    mnuClientesClientes.Enabled = true;
                    mnuClientesCredito.Visible = true;
                    break;
                case TiposUsuario.PREVENTA://(PREVENTA-HILLO)
                    mnuAlmacenInventario.Visible = true;
                    mnuVentasRegistros.Visible = true;
                    mnuVentasPreVenta.Visible = true;
                    mnuReportesGenerales.Visible = true;
                    mnuClientesClientes.Visible = true;
                    mnuClientesClientes.Enabled = true;
                    mnuAlmacenEntradasTraspasos.Visible = true;
                    break;
                case TiposUsuario.MASTER:
                    mnuAlmacenInventario.Visible = true;
                    mnuClientesClientes.Visible = true;
                    mnuClientesCredito.Visible = true;
                    mnuClientesTrabajadores.Visible = true;
                    mnuVentasPuntoVenta.Visible = true;
                    mnuVentasMayoristas.Visible = true;
                    mnuVentasPVDetalle.Visible = true;
                    mnuVentasPVVarios.Visible = true;
                    mnuAlmacenEntradasTraspasos.Visible = true;
                    mnuAlmacenEntradasProductos.Visible = true;
                    mnuAlmacenEntradasProductos.Enabled = true;
                    mnuAlmacenEntradasInsumos.Visible = true;
                    mnuAlmacenEntradasInsumos.Enabled = true;
                    mnuAlmacenSalidasProducto.Enabled = true;
                    mnuAlmacenSalidasInsumos.Visible = true;
                    mnuAlmacenProductos.Enabled = true;
                    mnuVentasRegistros.Visible = true;
                    mnuReportesGenerales.Visible = true;
                    mnuReportesGlobales.Visible = true;
                    mnuMovilTraspasos.Visible = true;
                    mnuMovilRegistros.Visible = true;
                    mnuMovilInventario.Visible = true;
                    mnuClientesProveedores.Visible = true;
                    mnuClientesEmpresas.Visible = true;
                    break;
            }
        }


        private void Inicio_Load(object sender, EventArgs e)
        {
            try
            {
                Program.ConexionIdActual = 1;// 1:PRODUCCION; 2:PRUEBAS
                
                Pantallas.Contraseña vInicioSesion = new Pantallas.Contraseña();
                vInicioSesion.CargaNombreUsuario();
                if (vInicioSesion.ShowDialog() == DialogResult.OK)
                {
                    Program.UsuarioSeleccionado = vInicioSesion.UsuarioLogin;
                    //if (!vInicioSesion.UsuarioLogin.Administrador)
                    //    Program.EmpresaSeleccionada = new AiresNegocio.BusEmpresas().ObtieneEmpresa(vInicioSesion.UsuarioLogin.EmpresaId);
                    Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresas(Program.UsuarioSeleccionado.CompañiaId).First();//new EntEmpresa() { Id = 1, Nombre = "-BOTANAS JAVY-" };
                }
                else
                    this.Close();

                if (Program.UsuarioSeleccionado != null)
                { 
                    this.Text = " Usuario: " + vInicioSesion.UsuarioLogin.Usuario + " | " 
                              + "Tipo Usuario:" + vInicioSesion.UsuarioLogin.TipoUsuarioId.ToString() + " | " + "Conexión:" + Program.ConexionIdActual;
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
                    a = new Menu(this.TipoUsuarioLoginId);
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
                Productos a = (Productos)BuscaForma(new Productos().Titulo);
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

        private void tsbClientes_Click(object sender, EventArgs e)
        {
            try
            {
                Clientes a = (Clientes)BuscaForma(new Clientes().Titulo);
                if (a == null)
                {
                    a = new Clientes();
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
                //Ventas a = (Ventas)BuscaForma("Ventas");
                //if (a == null)
                //{
                //    a = new Ventas();
                //    a.MdiParent = this;
                //    a.Show();
                //    if (Program.EmpresaSeleccionada == null)
                //        a.Close();
                //}
                //else
                //{
                //    a.VerificaEmpresa();
                //    a.BringToFront();
                //}
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

        private void tsbRegistrosVentas_Click_1(object sender, EventArgs e)
        {
            try
            {
                RegistrosNeue a = (RegistrosNeue)BuscaForma(new RegistrosNeue().Titulo);
                if (a == null)
                {
                    a = new RegistrosNeue();
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

        private void toolStripButton2_Click_2(object sender, EventArgs e)
        {
            try
            {
                EntradasCompras a = (EntradasCompras)BuscaForma(new EntradasCompras().Titulo);
                if (a == null)
                {
                    a = new EntradasCompras();
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

        private void tsbSalidasInsumo_Click(object sender, EventArgs e)
        {
            try
            {
                SalidasInsumo a = (SalidasInsumo)BuscaForma(new SalidasInsumo().Titulo);
                if (a == null)
                {
                    a = new SalidasInsumo();
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

        private void entradasTraspasosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EntradasTraspasos a = (EntradasTraspasos)BuscaForma(new EntradasTraspasos().Titulo);
                if (a == null)
                {
                    a = new EntradasTraspasos();
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

        private void entradasInsumosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EntradasCompras a = (EntradasCompras)BuscaForma(new EntradasCompras().Titulo);
                if (a == null)
                {
                    a = new EntradasCompras();
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

        private void entradasProductosToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void tsbVentasNeue_Click(object sender, EventArgs e)
        {
            try
            {
                SalidasVentas a = (SalidasVentas)BuscaForma(new SalidasVentas().Titulo);
                if (a == null)
                {
                    a = new SalidasVentas();
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

        private void tsbTrabajadores_Click(object sender, EventArgs e)
        {
            try
            {
                Trabajadores a = (Trabajadores)BuscaForma(new Trabajadores().Titulo);
                if (a == null)
                {
                    a = new Trabajadores();
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

        private void tsbClientesCreditoPruebas_Click(object sender, EventArgs e)
        {
            try
            {
                ClientesCreditoPrueba a = (ClientesCreditoPrueba)BuscaForma(new ClientesCreditoPrueba().Titulo);
                if (a == null)
                {
                    a = new ClientesCreditoPrueba();
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

        private void tsbVentasNeueProductosVarios_Click(object sender, EventArgs e)
        {
            try
            {
                SalidasVentasVarios a = (SalidasVentasVarios)BuscaForma(new SalidasVentasVarios().Titulo);
                if (a == null)
                {
                    a = new SalidasVentasVarios();
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

        private void tsbPuntoVentaDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                SalidasVentasDetalle a = (SalidasVentasDetalle)BuscaForma(new SalidasVentasDetalle().Titulo);
                if (a == null)
                {
                    a = new SalidasVentasDetalle();
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

        private void tsbRegtistroVentas_Click(object sender, EventArgs e)
        {

        }

        private void tsbRegtistroVentas_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void tsbReportesGlobales_Click(object sender, EventArgs e)
        {
            try
            {
                ReportesGlobales a = (ReportesGlobales)BuscaForma(new ReportesGlobales().Titulo);
                if (a == null)
                {
                    a = new ReportesGlobales();
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

        private void tsbVentaMayorista_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                SalidasVentasMayoreo a = (SalidasVentasMayoreo)BuscaForma(new SalidasVentasMayoreo().Titulo);
                if (a == null)
                {
                    a = new SalidasVentasMayoreo();
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

        private void tsbVentaMayorista_Click(object sender, EventArgs e)
        {
            try
            {
                SalidasVentasMayoreo a = (SalidasVentasMayoreo)BuscaForma(new SalidasVentasMayoreo().Titulo);
                if (a == null)
                {
                    a = new SalidasVentasMayoreo();
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

        private void tsmPreVenta_Click(object sender, EventArgs e)
        {
            try
            {
                PreVentasMayoreo a = (PreVentasMayoreo)BuscaForma(new PreVentasMayoreo().Titulo);
                if (a == null)
                {
                    a = new PreVentasMayoreo();
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

        private void tsbVentasNeueDetalleNeue_Click(object sender, EventArgs e)
        {
            try
            {
                SalidasVentasDetalle a = (SalidasVentasDetalle)BuscaForma(new SalidasVentasDetalle().Titulo);
                if (a == null)
                {
                    a = new SalidasVentasDetalle();
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

        private void tsbVentasNeueProductosVariosNeue_Click(object sender, EventArgs e)
        {
            try
            {
                SalidasVentasVarios a = (SalidasVentasVarios)BuscaForma(new SalidasVentasVarios().Titulo);
                if (a == null)
                {
                    a = new SalidasVentasVarios();
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

        private void tsbVentasNeue_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                SalidasVentas a = (SalidasVentas)BuscaForma(new SalidasVentas().Titulo);
                if (a == null)
                {
                    a = new SalidasVentas();
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

        private void registrosMóvilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RegistrosConsignacion a = (RegistrosConsignacion)BuscaForma(new RegistrosConsignacion().Titulo);
                if (a == null)
                {
                    a = new RegistrosConsignacion();
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

        private void tsbInventarioMovil_Click(object sender, EventArgs e)
        {
            try
            {
                InventariosMovil a = (InventariosMovil)BuscaForma(new InventariosMovil().Titulo);
                if (a == null)
                {
                    a = new InventariosMovil();
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

        private void tsmTraspasosMovil_Click(object sender, EventArgs e)
        {
            try
            {
                SalidasTraspasoMovil a = (SalidasTraspasoMovil)BuscaForma(new SalidasTraspasoMovil().Titulo);
                if (a == null)
                {
                    a = new SalidasTraspasoMovil();
                    a.MdiParent = this;
                    a.Show();
                }
                else
                {
                    a.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// Custom renderer for the top navigation bar with a modern dark professional look.
    /// </summary>
    internal class TopNavRenderer : ToolStripProfessionalRenderer
    {
        private static readonly Color NavBackground   = Color.FromArgb(33, 37, 41);
        private static readonly Color NavHover        = Color.FromArgb(13, 110, 253);
        private static readonly Color NavPressed      = Color.FromArgb(10,  88, 202);
        private static readonly Color DropdownBack    = Color.FromArgb(248, 249, 250);
        private static readonly Color DropdownHover   = Color.FromArgb(209, 227, 253);
        private static readonly Color DropdownBorder  = Color.FromArgb(200, 200, 200);
        private static readonly Color TextLight       = Color.White;
        private static readonly Color TextDark        = Color.FromArgb(33, 37, 41);
        private static readonly Color SeparatorColor  = Color.FromArgb(200, 200, 200);

        public TopNavRenderer() : base(new TopNavColorTable()) { }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            using (var brush = new SolidBrush(e.ToolStrip.IsDropDown ? DropdownBack : NavBackground))
                e.Graphics.FillRectangle(brush, e.AffectedBounds);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            var item = e.Item;
            var bounds = new Rectangle(0, 0, item.Width, item.Height);

            if (item.IsOnDropDown)
            {
                Color bg = (item.Selected || item.Pressed) ? DropdownHover : DropdownBack;
                using (var brush = new SolidBrush(bg))
                    e.Graphics.FillRectangle(brush, bounds);
            }
            else
            {
                Color bg = item.Pressed ? NavPressed
                         : item.Selected ? NavHover
                         : NavBackground;
                using (var brush = new SolidBrush(bg))
                    e.Graphics.FillRectangle(brush, bounds);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = e.Item.IsOnDropDown ? TextDark : TextLight;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = e.Item.IsOnDropDown ? TextDark : TextLight;
            base.OnRenderArrow(e);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            int mid = e.Item.Height / 2;
            using (var pen = new Pen(SeparatorColor))
                e.Graphics.DrawLine(pen, 28, mid, e.Item.Width - 4, mid);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip.IsDropDown)
            {
                using (var pen = new Pen(DropdownBorder))
                    e.Graphics.DrawRectangle(pen, 0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
            }
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip.IsDropDown)
            {
                using (var brush = new SolidBrush(Color.FromArgb(235, 237, 240)))
                    e.Graphics.FillRectangle(brush, e.AffectedBounds);
            }
        }
    }

    internal class TopNavColorTable : ProfessionalColorTable
    {
        public override Color MenuItemSelected            => Color.FromArgb(13, 110, 253);
        public override Color MenuItemSelectedGradientBegin => Color.FromArgb(13, 110, 253);
        public override Color MenuItemSelectedGradientEnd   => Color.FromArgb(13, 110, 253);
        public override Color MenuItemPressedGradientBegin  => Color.FromArgb(10,  88, 202);
        public override Color MenuItemPressedGradientEnd    => Color.FromArgb(10,  88, 202);
        public override Color MenuItemBorder              => Color.FromArgb(13, 110, 253);
        public override Color MenuBorder                  => Color.FromArgb(200, 200, 200);
        public override Color ToolStripDropDownBackground => Color.FromArgb(248, 249, 250);
    }
}
