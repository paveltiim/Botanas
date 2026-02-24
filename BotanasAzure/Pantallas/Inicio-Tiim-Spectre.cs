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
            PUNTOVENTAMENUDEO = 8
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
        string EmailPrincipal ="tiimfacturacion@hotmail.com";// "pavel_tiim@hotmail.com";
        
        #endregion

        int TipoUsuarioLoginId { get; set; }
        void MuestraBotonesMenu(TiposUsuario TipoUsuario)
        {
            tsbClientes.Visible = false;
            tsbEntradasProductos.Enabled = false;
            tsbEntradasProductos.Visible = false;
            tsbSalidas.Enabled = false;
            entradasProductosToolStripMenuItem.Visible = false;
            entradasInsumosToolStripMenuItem.Visible = false;
            entradasTraspasosToolStripMenuItem.Visible = false;
            tsbVentaMayorista.Visible = false;
            tsbRegtistroVentas.Visible = false;
            tsbClientesCredito.Visible = false;
            tsbTrabajadores.Visible = false;
            tsbReportes.Visible = false;
            tsbClientesCreditoPruebas.Visible = false;
            tsbRegtistroVentas.Visible=false;
            switch (TipoUsuario)
            {
                case TiposUsuario.ADMINISTRADORINSUMOS:
                    tsbProductos.Enabled = true;
                    //tsbEntradasInsumos.Visible = true;
                    tsbEntradas.Visible = true;
                    tsbEntradas.Enabled = true;
                    entradasInsumosToolStripMenuItem.Visible = true;
                    entradasProductosToolStripMenuItem.Visible = true;
                    entradasTraspasosToolStripMenuItem.Visible = true;
                    tsbSalidasInsumo.Visible = true;
                    tsbSalidas.Enabled = true;
                    tsbClientes.Visible = true;
                    tsbRegtistroVentas.Visible = true;
                    break;
                case TiposUsuario.ADMINISTRADORPRODUCCION:
                    tsbEntradasProductos.Visible = true;
                    tsbEntradasProductos.Enabled = true;
                    entradasProductosToolStripMenuItem.Visible = true;
                    break;
                case TiposUsuario.ADMINISTRADORALMACEN:
                    tsbClientes.Visible = true;
                    tsbSalidas.Enabled = true;
                    tsbVentaMayorista.Visible = true;
                    tsbRegtistroVentas.Visible = true;
                    tsbRegtistroVentasMovil.Visible = true;
                    tsbClientesCredito.Visible = true;
                    tsbTrabajadores.Visible = true;
                    break;
                case TiposUsuario.PUNTOVENTA:
                    tsbClientes.Visible = true;
                    //tsbVentasNeueDetalle.Visible = true;
                    tsbVentasNeue.Visible = true;
                    tsbVentaMayorista.Visible = true;
                    //tsbVentasNeueProductosVarios.Visible = true;
                    tsbEntradas.Visible = true;
                    entradasTraspasosToolStripMenuItem.Visible = true;
                    tsbRegtistroVentas.Visible = true;
                    tsbClientesCredito.Visible = true;
                    tsbEntradasInsumos.Visible = false;
                    tsbEntradasProductos.Visible = false;
                    tsbProductos.Visible = false;
                    tsbSalidas.Visible = false;
                    tsbTrabajadores.Visible = true;
                    tsbReportes.Visible = true;
                    tsbReportesGlobales.Visible = true;
                    break;
                case TiposUsuario.CUENTASPORCOBRAR:
                    tsbClientesCredito.Visible = true;
                    tsbReportes.Visible = true;
                    tsbClientes.Visible = true;
                    tsbRegtistroVentas.Visible = true;
                    tsbRegtistroVentasMovil.Visible = true;
                    tsbVentasNeueProductosVarios.Visible = true;
                    tsbEntradasInsumos.Visible = false;
                    tsbEntradasProductos.Visible = false;
                    tsbProductos.Visible = false;
                    tsbSalidas.Visible = false;

                    break;
                case TiposUsuario.ADMINISTRADORPUNTOVENTA:
                    tsbEntradas.Visible = true;
                    tsbEntradas.Enabled = true;
                    entradasTraspasosToolStripMenuItem.Visible = true;
                    tsbClientes.Visible = true;
                    tsbRegtistroVentas.Visible = true;
                    tsbRegtistroVentasMovil.Visible = true;
                    tsbVentaMayorista.Visible = true;
                    tsbEntradasInsumos.Visible = false;
                    tsbEntradasProductos.Visible = false;
                    tsbProductos.Visible = false;
                    tsbSalidas.Visible = false;
                    tsbTrabajadores.Visible = true;
                    break;
                case TiposUsuario.PUNTOVENTAMENUDEO:
                    tsbClientes.Visible = true;
                    tsbVentasNeue.Visible = true;
                    //tsbVentaMayorista.Visible = true;
                    tsbEntradas.Visible = true;
                    entradasTraspasosToolStripMenuItem.Visible = true;
                    tsbRegtistroVentas.Visible = true;
                    tsbClientesCredito.Visible = true;
                    tsbEntradasInsumos.Visible = false;
                    tsbEntradasProductos.Visible = false;
                    tsbProductos.Visible = false;
                    tsbSalidas.Visible = false;
                    tsbTrabajadores.Visible = true;
                    tsbReportes.Visible = true;
                    break;
                case TiposUsuario.MASTER:
                    tsbClientesCreditoPruebas.Visible = true;
                    tsbReportes.Visible = true;
                    tsbEntradas.Visible = true;
                    //tsbEntradasProductos.Visible = true;
                    //tsbEntradasProductos.Enabled = true;
                    entradasProductosToolStripMenuItem.Visible = true;
                    tsbProductos.Enabled = true;
                    //tsbEntradasInsumos.Visible = true;
                    entradasInsumosToolStripMenuItem.Visible = true;
                    tsbSalidasInsumo.Visible = true;
                    tsbClientes.Visible = true;
                    tsbSalidas.Enabled = true;
                    entradasTraspasosToolStripMenuItem.Visible = true;
                    tsbVentasNeueDetalle.Visible = true;
                    tsbVentasNeue.Visible = true;
                    tsbVentaMayorista.Visible = true;
                    tsbVentasNeueProductosVarios.Visible = true;
                    tsbRegtistroVentas.Visible = true;
                    tsbRegtistroVentasMovil.Visible = true;
                    tsbClientesCredito.Visible = true;
                    tsbTrabajadores.Visible = true;
                    tsbReportesGlobales.Visible = true;
                    break;
            }
        }


        private void Inicio_Load(object sender, EventArgs e)
        {
            try
            {
                Program.ConexionIdActual = 1;// 1:PRODUCCION; 2:PRUEBAS
                Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresas().First();//new EntEmpresa() { Id = 1, Nombre = "-BOTANAS JAVY-" };

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
            //try
            //{
            //    RegistrosNeue a = (RegistrosNeue)BuscaForma(new RegistrosNeue().Titulo);
            //    if (a == null)
            //    {
            //        a = new RegistrosNeue();
            //        a.MdiParent = this;
            //        a.Show();
            //        //if (Program.EmpresaSeleccionada == null)
            //        //    a.Close();
            //    }
            //    else
            //    {
            //        //a.VerificaEmpresa();
            //        a.BringToFront();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
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

        private void tsbRegtistroVentas_DoubleClick(object sender, EventArgs e)
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
    }
}
