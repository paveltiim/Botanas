using AiresEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aires
{
    public class FormBase : Form
    {
        string PantallaSize = "1700, 802";

        public string PathFacturas = @"C:\TIIM\Facturacion\Facturas";
        public string PathPreFacturas = @"C:\TIIM\Facturacion\PreFacturas";
        public string PathPreFacturasCopia = @"\Servidor\Facturacion\PreFacturas";
        public string PathFacturasCopia = @"\Oficina - pc\tiim\Facturacion\Facturas";//@"\LAPTOP-LJCQA84V\Users\pavel\Documents\FacturacionModerna";//
        public string PathFacturasComplementos = @"C:\TIIM\Facturacion\Facturas";

        public string Titulo { get { return this.Text; } }

        public decimal IVA = 0.16m;
        public int AlturaMaximaGrid = 200;

        public int AñoInicio = 2016;

        /// <summary>
        /// Busca forma en ParentForm.
        /// </summary>
        /// <param name="Titulo"></param>
        /// <returns></returns>
        public Form BuscaFormaBase(string Titulo)
        {
            foreach (Form v in this.ParentForm.MdiChildren)
                if (v.Text == Titulo)
                    return v;
            return null;
        }

        public EntEmpresa SeleccionaEmpresa()
        {
            Pantallas.SeleccionaEmpresa vSeleccionaEmp = new Pantallas.SeleccionaEmpresa();
            if (vSeleccionaEmp.ShowDialog() == DialogResult.OK)
            {
                return vSeleccionaEmp.EmpresaSeleccionada;
            }
            //else
            //    return new EntEmpresa() { Id = -1 };
            //    this.Close();

            return null;
        }
        public void AjustaAlturaGrid(DataGridView Grid, int CantidadRegistros, int AlturaMaxima)
        {
            int altura = 25 + (28 * CantidadRegistros);
            if (altura > AlturaMaxima)
                altura = AlturaMaxima;

            Grid.Height = altura;
        }
        public void OcultaBuscadorGrid(DataGridView Grid, TextBox TxtBuscador)
        {
            Grid.Visible = false;
            TxtBuscador.Text = "";
        }
        public DateTime ObtieneLunesEstaSemana(DateTime Dia)
        {
            if (Dia.DayOfWeek == DayOfWeek.Monday)
                return Dia;
            else
                return ObtieneLunesEstaSemana(Dia.AddDays(-1));
        }

        /// <summary>
        /// Regresa el valor con formato {0:c}.
        /// </summary>
        /// <param name="Valor"></param>
        /// <returns></returns>
        public string FormatoMoney(decimal Valor)
        {
            return string.Format("{0:c}", Valor);
        }
        /// <summary>
        /// Regresa el valor con formato {0:c}.
        /// </summary>
        /// <param name="Valor"></param>
        /// <returns></returns>
        public string FormatoMoneyDecimales(decimal Valor)
        {
            return string.Format("{0:c6}", Valor);
        }
        /// <summary>
        /// Intenta convertir el Text de TxtValor solicitado en Decimal y devuelve la conversión. 
        /// Si la conversión es invalida devuelve 0.
        /// Reemplaza "$" a valor si es que lo tiene.
        /// </summary>
        /// <param name="Valor"></param>
        /// <returns></returns>
        public decimal ConvierteTextoADecimal(TextBox TxtValor)
        {
            decimal resul = 0;
            Decimal.TryParse(TxtValor.Text.Replace("$", ""), out resul);
            return resul;
        }
        /// <summary>
        /// Intenta convertir el Valor solicitado en Decimal y devuelve la conversión. 
        /// Si la conversión es invalida devuelve 0.
        /// Reemplaza "$" a valor si es que lo tiene.
        /// </summary>
        /// <param name="Valor"></param>
        /// <returns></returns>
        public decimal ConvierteTextoADecimal(string Valor)
        {
            decimal resul = 0;
            Decimal.TryParse(Valor.Replace("$", ""), out resul);
            return resul;
        }
        /// <summary>
        /// Intenta convertir el Valor solicitado en Int y devuelve la conversión. 
        /// Si la conversión es invalida devuelve 0.
        /// </summary>
        /// <param name="Valor"></param>
        /// <returns></returns>
        public int ConvierteTextoAInteger(string Valor)
        {
            int resul = 0;
            int.TryParse(Valor, out resul);
            return resul;
        }
        public void TextBoxDecimal_TextChanged(object sender, EventArgs e)
        {
            decimal resul = 0;
            if (((TextBox)sender).Text.Trim() != "")
            {
                Decimal.TryParse(((TextBox)sender).Text.Replace("$", ""), out resul);
                ((TextBox)sender).Text = resul.ToString();
            }
        }
        public void TextBoxDecimal_Leave(object sender, EventArgs e)
        {
            decimal resul = 0;
            Decimal.TryParse(((TextBox)sender).Text.Replace("$", ""), out resul);
            ((TextBox)sender).Text = resul.ToString();
        }
        public void TextBoxDecimalMoney_Leave(object sender, EventArgs e)
        {
            decimal resul = 0;
            Decimal.TryParse(((TextBox)sender).Text.Replace("$", ""), out resul);
            ((TextBox)sender).Text = FormatoMoney(resul);
        }
        public void TextBoxDecimalMoneyDecimales_Leave(object sender, EventArgs e)
        {
            decimal resul = 0;
            Decimal.TryParse(((TextBox)sender).Text.Replace("$", ""), out resul);
            ((TextBox)sender).Text = FormatoMoneyDecimales(resul);
        }
        public void LimpiaTextBox(Control Contenedor)
        {
            foreach (Control c in Contenedor.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                    ((TextBox)c).Text = "";
                if (c.HasChildren)
                    LimpiaTextBox(c);
            }
        }
        public void LimpiaTextBox(Control Contenedor, bool InicializaComboBox)
        {
            foreach (Control c in Contenedor.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                    ((TextBox)c).Text = "";
                else if (c.GetType() == typeof(ComboBox) && InicializaComboBox)
                    ((ComboBox)c).SelectedIndex = 0;
                if (c.HasChildren)
                    LimpiaTextBox(c, InicializaComboBox);
            }
        }
        public void ChecaChecBox(Control Contenedor, bool Checked)
        {
            foreach (Control c in Contenedor.Controls)
            {
                if (c.GetType() == typeof(CheckBox))
                    ((CheckBox)c).Checked = Checked;
                if (c.HasChildren)
                    ChecaChecBox(c, Checked);
            }
        }
        public void EnableTextBox(Control Contenedor, bool Checked)
        {
            foreach (Control c in Contenedor.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                    ((TextBox)c).Enabled = Checked;
                if (c.HasChildren)
                    EnableTextBox(c, Checked);
            }
        }

        /// <summary>
        /// Crea OpenFileDialog y lo abre en la ruta especificada Path. Abre el archivo seleccionado con Proccess.
        /// Si no encuentra la ruta Path envia mensaje de error.
        /// </summary>
        /// <param name="Path"></param>
        public void MuestraArchivo(string Path)
        {
            if (System.IO.Directory.Exists(Path))
            {
                OpenFileDialog ofd = new OpenFileDialog();
                //ofd.InitialDirectory = @"C:\TIIM\Anticipo\20150806\A quien corresponda";
                ofd.InitialDirectory = Path;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    //System.IO.File.Open(ofd.FileName, System.IO.FileMode.Open);
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = ofd.FileName;
                    proc.Start();
                    proc.Close();
                }
            }
            else
                MuestraMensaje("No se encontraron archivos en la ruta seleccionada", "");
        }
        /// <summary>
        /// Abre el archivo seleccionado con Proccess.
        /// Si no encuentra la ruta Path envia mensaje de error.
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="FileName"></param>
        public void MuestraArchivo(string Path, string FileName)
        {
            if (System.IO.File.Exists(Path + "\\" + FileName))
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = Path + "\\" + FileName;
                proc.Start();
                proc.Close();
            }
            else
                MuestraMensaje("No se encontraron archivos en la ruta seleccionada", "");
        }

        /// <summary>
        /// Abre el archivo seleccionado con Proccess.
        /// Si no encuentra la ruta Path envia mensaje de error.
        /// </summary>
        /// <param name="FileName"></param>
        public void MuestraArchivo(string FileName, bool IsFile)
        {
            if (System.IO.File.Exists(FileName))
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = FileName;
                proc.Start();
                proc.Close();
            }
            else
                MuestraMensaje("No se encontró archivo", "");
        }
        /// <summary>
        /// Abre el archivo seleccionado con Proccess.
        /// Si no encuentra la ruta Path envia mensaje de error.
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="FileName"></param>
        public System.IO.FileInfo SeleccionaArchivo(string Path)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //ofd.InitialDirectory = @"C:\TIIM\Anticipo\20150806\A quien corresponda";
            ofd.InitialDirectory = Path;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return new System.IO.FileInfo(ofd.FileName);
                ////System.IO.File.Open(ofd.FileName, System.IO.FileMode.Open);
                //System.Diagnostics.Process proc = new System.Diagnostics.Process();
                //proc.StartInfo.FileName = ofd.FileName;
                //proc.Start();
                //proc.Close();
            }
            return null;
        }
        /// <summary>
        /// Abre el archivo seleccionado con Proccess.
        /// Si no encuentra la ruta Path envia mensaje de error.
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="FileName"></param>
        public string SeleccionaArchivo()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;//new System.IO.FileInfo(ofd.FileName);
            }
            return null;
        }

        /// <summary>
        /// Busca el tipo de archivo con la Extension solicitada y regresa el nombre del archivo (el primero que encuentre).
        /// </summary>
        /// <param name="Ruta">Ruta del directorio que contiene el archivo</param>
        /// <param name="Extension"></param>
        /// <returns></returns>
        public string EncuentraArchivo(string Ruta, string Extension)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Ruta);

            System.IO.FileInfo[] fi = di.GetFiles();
            foreach (System.IO.FileInfo f in fi)
            {
                if (f.Extension == Extension)
                    return f.Name;
            }
            return "";
        }
        /// <summary>
        /// Regresa el numero de archivos encontrados con la Extension solicitada en la Ruta solicitada.
        /// </summary>
        /// <param name="Ruta"></param>
        /// <param name="Extension"></param>
        /// <returns></returns>
        public int CuentaArchivos(string Ruta, string Extension)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Ruta);

            System.IO.FileInfo[] fi = di.GetFiles();
            //foreach (System.IO.FileInfo f in fi)
            //{
            //    if (f.Extension == Extension)
            //        return f.Name;
            //}
            return fi.Length;
        }

        public void MuestraMensaje(string Mensaje, string Titulo)
        {
            MessageBox.Show(Mensaje, Titulo, MessageBoxButtons.OK);
        }
        public void MuestraMensajeError(string Mensaje, string Titulo)
        {
            MessageBox.Show(Mensaje, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public DialogResult MuestraMensajeYesNo(string Mensaje)
        {
            return MessageBox.Show(Mensaje, "CONFIRMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        public DialogResult MuestraMensajeYesNo(string Mensaje, string Titulo)
        {
            return MessageBox.Show(Mensaje, Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        public void MandaExcepcion(string MensajeExcepcion)
        {
            throw new Exception(MensajeExcepcion);
        }
        /// <summary>
        /// Muestra MessageBox.Show con ex.Message.
        /// </summary>
        /// <param name="ex"></param>
        public void MuestraExcepcion(Exception ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void MuestraExcepcionFacturacion(Exception ex) {
            if (ex.Message.Contains("Emisor") && (ex.Message.Contains("has a length of '0'") || ex.Message.Contains("is required but missing")))
                MuestraMensajeError("Datos de Empresa incompletos \n \n" + ex.Message, "ERROR-Pedido NO Facturado");
            else if (ex.Message.Contains("Receptor") && (ex.Message.Contains("has a length of") || ex.Message.Contains("is required but missing")))
                MuestraMensajeError("Datos de Cliente incorrectos \n \n" + ex.Message, "ERROR-Pedido NO Facturado");
            else
                MuestraExcepcion(ex, "Pedido NO Facturado");
        }
        public void MuestraExcepcionPreFacturacion(Exception ex)
        {
            if (ex.Message.Contains("Emisor") && (ex.Message.Contains("has a length of '0'") || ex.Message.Contains("is required but missing")))
                MuestraMensajeError("Datos de Empresa incompletos \n \n" + ex.Message, "ERROR en Pre-Factura");
            else if (ex.Message.Contains("Receptor") && (ex.Message.Contains("has a length of") || ex.Message.Contains("is required but missing")))
                MuestraMensajeError("Datos de Cliente incorrectos \n \n" + ex.Message, "ERROR en Pre-Factura");
            else
                MuestraExcepcion(ex, "Error en Pre-Factura");
        }
        public void MuestraExcepcion(Exception ex, string MensajeAgregado)
        {
            MessageBox.Show(ex.Message + "\n (" + MensajeAgregado + ")", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        public List<EntProducto> ObtieneListaProductosFromGV(DataGridView GridViewProductos)
        {
            return (List<EntProducto>)GridViewProductos.DataSource;
        }
        public List<EntCatalogoGenerico> ObtieneListaGenericaFromGV(DataGridView GridViewGenerico)
        {
            return (List<EntCatalogoGenerico>)GridViewGenerico.DataSource;
        }
        public List<EntEmpresa> ObtieneListaEmpresasFromGV(DataGridView GridViewEmpresas)
        {
            return (List<EntEmpresa>)GridViewEmpresas.DataSource;
        }
        public List<EntCliente> ObtieneListaClientesFromGV(DataGridView GridViewClientes)
        {
            return (List<EntCliente>)GridViewClientes.DataSource;
        }
        public List<EntPedido> ObtieneListaPedidosFromGV(DataGridView GridViewPedidos)
        {
            return (List<EntPedido>)GridViewPedidos.DataSource;
        }
        public List<EntPago> ObtieneListaPagosFromGV(DataGridView GridViewPagos)
        {
            return (List<EntPago>)GridViewPagos.DataSource;
        }
        public List<EntProveedor> ObtieneListaProveedoresFromGV(DataGridView GridViewProveedor)
        {
            return (List<EntProveedor>)GridViewProveedor.DataSource;
        }

        public EntCatalogoGenerico ObtieneCatalogoGenericoFromGV(DataGridView GridViewGenerico)
        {
            if (GridViewGenerico.CurrentRow == null)
                return null;
            return (EntCatalogoGenerico)((List<EntCatalogoGenerico>)GridViewGenerico.DataSource)[GridViewGenerico.CurrentRow.Index];
        }
        public EntCliente ObtieneClienteFromGV(DataGridView GridViewClientes)
        {
            if (GridViewClientes.CurrentRow == null)
                return null;
            return (EntCliente)((List<EntCliente>)GridViewClientes.DataSource)[GridViewClientes.CurrentRow.Index];
        }
        public EntCliente ObtieneClienteFromCmb(ComboBox ComboBoxCliente)
        {
            return (EntCliente)((List<EntCliente>)ComboBoxCliente.DataSource)[ComboBoxCliente.SelectedIndex];
        }
        public EntEmpresa ObtieneEmpresaFromGV(DataGridView GridViewEmpresas)
        {
            if (GridViewEmpresas.CurrentRow == null)
                return null;
            return (EntEmpresa)((List<EntEmpresa>)GridViewEmpresas.DataSource)[GridViewEmpresas.CurrentRow.Index];
        }
        public EntEmpresa ObtieneEmpresaFromCmb(ComboBox ComboBoxEmpresas)
        {
            return (EntEmpresa)((List<EntEmpresa>)ComboBoxEmpresas.DataSource)[ComboBoxEmpresas.SelectedIndex];
        }
        public EntProveedor ObtieneProveedorFromGV(DataGridView GridViewProveedores)
        {
            if (GridViewProveedores.CurrentRow == null)
                return null;
            return (EntProveedor)((List<EntProveedor>)GridViewProveedores.DataSource)[GridViewProveedores.CurrentRow.Index];
        }
        public EntProducto ObtieneProductoFromGV(DataGridView GridViewProductos)
        {
            if (GridViewProductos.CurrentRow == null)
                return null;

            return (EntProducto)((List<EntProducto>)GridViewProductos.DataSource)[GridViewProductos.CurrentRow.Index];
        }
        public EntProducto ObtieneProductoFromCmb(ComboBox ComboBoxProducto)
        {
            return (EntProducto)((List<EntProducto>)ComboBoxProducto.DataSource)[ComboBoxProducto.SelectedIndex];
        }
        public EntProducto ObtieneProductoAnteriorFromGV(DataGridView GridViewProductos)
        {
            if (GridViewProductos.CurrentRow == null)
                return null;

            return (EntProducto)((List<EntProducto>)GridViewProductos.DataSource)[GridViewProductos.CurrentRow.Index - 1];
        }
        public EntPedido ObtienePedidoFromGV(DataGridView GridViewPedidos)
        {
            if (GridViewPedidos.CurrentRow == null)
                return null;

            return (EntPedido)((List<EntPedido>)GridViewPedidos.DataSource)[GridViewPedidos.CurrentRow.Index];
        }
        public EntPago ObtienePagoFromGV(DataGridView GridViewPagos)
        {
            if (GridViewPagos.CurrentRow == null)
                return null;

            return (EntPago)((List<EntPago>)GridViewPagos.DataSource)[GridViewPagos.CurrentRow.Index];
        }

        public EntCatalogoGenerico ObtieneCatalogoGenericoFromCmb(ComboBox ComboBox)
        {
            return (EntCatalogoGenerico)((List<EntCatalogoGenerico>)ComboBox.DataSource)[ComboBox.SelectedIndex];
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormBase
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ClientSize = new System.Drawing.Size(1754, 813);
            this.Name = "FormBase";
            this.Load += new System.EventHandler(this.FormBase_Load);
            this.ResumeLayout(false);

        }

        private void FormBase_Load(object sender, EventArgs e)
        {

        }
    }
}
