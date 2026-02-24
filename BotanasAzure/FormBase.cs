using Aires.Pantallas;
using AiresEntidades;
using AiresNegocio;
using AiresUtilerias;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using CheckBox = System.Windows.Forms.CheckBox;
using TextBox = System.Windows.Forms.TextBox;

namespace Aires
{
    public class FormBase : Form
    {
        string PantallaSize = "1700, 802";
        protected int PantallaSizeWidth = 1000;
        protected int PantallaSizeHeight = 400;

        public string RfcPublicoGeneral { get { return "XAXX010101000"; } }

        /// <summary>
        /// @"C:\TIIM\Facturacion\Facturas"
        /// </summary>
        public string PathFacturas = @"C:\TIIM\Facturacion\Facturas";
        public string PathPreFacturas = @"C:\TIIM\Facturacion\PreFacturas";
        public string PathFacturasComplementos = @"C:\TIIM\Facturacion\Facturas";
        public string RutaImpresion= @"C:\TIIM\OrdenesSalidas";

        public string Titulo { get { return this.Text; } }

        public decimal IVA = 0.16m;
        public decimal RetencionIVA = 0.06m;
        public decimal IEPS = 0.08m;

        public int AlturaMaximaGrid = 200;

        public int AñoInicio = 2021;

        public int CantidadLimiteMayoreo = 6;

        public enum EstatusPedido
        {
            ACTIVO = 1,
            PAGADO = 2
        }
        public enum EstatusPedidoPreVenta
        {
            PREVENTA = 1,
            NOTAVENTA = 2,
            FACTURA = 3,
            PREVENTA_DIVIDIDA = 4,
            CANCELADA = 0
        }
        public enum TipoOrientacion
        {
            ENTRADA = 1,
            SALIDA = 2
        }
        public enum TipoUsuario
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
            CUENTASPORCOBRARVENTAS = 10,
            GERENTEVENTAS = 11,
            SUPERVISOR = 12, //OSBALDO
            GERENTEALMACEN = 13,
            GERENTEPRODUCCION = 14,
            CYCVISUALIZA = 15
        }
        public enum TipoMovimiento
        {
            COMPRA=1,
            VENTA=2,
            AJUSTE=3,
            TRASPASO=4,
            PRODUCCION=5,
            CANCELACION=6,
            TRASPASOCONSIGNA=8,
            COTIZACION=9
                //NO EXISTE PREVENTA, POR NO SER MOVIMIENTO DE INVENTARIO
        }
        public enum TipoPedido
        {
            VENTA = 1,
            AJUSTE = 2,
            TRASPASO = 3,
            DEVOLUCIONCORTESIA = 4,
            VENTAMAYOREO = 5,
            COTIZACION =6,
            CORTESIA = 11,
            PREVENTA = 12,
            PREVENTACOTIZACION = 13,
            PREVENTADEVOLUCION = 14,
            PREVENTACORTESIA = 15,
            PREVENTADIVIDIDA = 16
        }
        public enum TipoCliente
        {
            CONVENIENCIA = 9
        }
        public enum TipoEstadosMovimientoTraspaso
        {
            PROCESO= 1,
            RECIBIDO = 2,
            RECHAZADO = 3
        }
        public enum Almacenes
        {
            PUNTOVENTA= 5
        }

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
        public void SetWaitCursor()
        {
            this.Cursor = Cursors.WaitCursor;
        }
        public void SetDefaultCursor()
        {
            this.Cursor = Cursors.Default;
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

        /// <summary>
        /// OPCION: 1 = SELECCIONADO, 2 = ROJO, 0 = NORMAL
        /// </summary>
        /// <param name="Opcion"></param>
        /// <param name="Txts"></param>
        public void CambiaColorTxt(int Opcion, params TextBox[] Txts)
        {
            foreach (TextBox t in Txts)
            {
                switch(Opcion)
                {
                    case 1:
                        t.BackColor = Color.DodgerBlue;
                        t.ForeColor = Color.White;
                        //txtReporteBusquedaHistorialFalla.BackColor = Color.DodgerBlue;
                        //txtReporteBusquedaHistorialFalla.ForeColor = Color.White;
                        break;
                    case 2:
                        //txtSerieBusquedaHistoroialFallas.BackColor = Color.White;
                        //txtSerieBusquedaHistoroialFallas.ForeColor = Color.Black;
                        t.BackColor = Color.DarkRed;
                        t.ForeColor = Color.White;
                        break;
                    case 0:
                        t.BackColor = Color.DodgerBlue;
                        t.ForeColor = Color.White;
                        //txtReporteBusquedaHistorialFalla.BackColor = Color.DodgerBlue;
                        //txtReporteBusquedaHistorialFalla.ForeColor = Color.White;
                        break;
                }
            }
        }
        /// <summary>
        /// OPCION: 1 = SELECCIONADO, 2 = ROJO, 0 = NORMAL
        /// </summary>
        /// <param name="Opcion"></param>
        /// <param name="Txts"></param>
        public void CambiaBackColorGv(int Opcion, DataGridView GvCambiaBack, int IndexCambia)
        {
            switch (Opcion)
            {
                case 1:
                    GvCambiaBack.Rows[IndexCambia].DefaultCellStyle.BackColor = Color.DodgerBlue;
                    GvCambiaBack.Rows[IndexCambia].DefaultCellStyle.ForeColor = Color.White;
                    break;
                case 2:
                    GvCambiaBack.Rows[IndexCambia].DefaultCellStyle.BackColor = Color.DarkRed;
                    GvCambiaBack.Rows[IndexCambia].DefaultCellStyle.ForeColor = Color.Wheat;
                    break;
                case 0:
                    GvCambiaBack.Rows[IndexCambia].DefaultCellStyle.BackColor = Color.DodgerBlue;
                    GvCambiaBack.Rows[IndexCambia].DefaultCellStyle.ForeColor = Color.White;
                    break;
            }
        }
        public string ObtieneSucursalFromPedidoDetalle(string PedidoDetalle)
        {
            if (PedidoDetalle.Contains("SUC:"))
                return PedidoDetalle.Split('|')[0].Replace("SUC: ", "");
            return "";
        }
        public void CargaAñosCmb(ComboBox cmbAños)
        {
            List<EntCatalogoGenerico> años = new List<EntCatalogoGenerico>();
            for (int x = DateTime.Today.Year; x >= AñoInicio; x--)
            {
                EntCatalogoGenerico año = new EntCatalogoGenerico();
                año.Descripcion = x.ToString();
                años.Add(año);
            }
            cmbAños.DataSource = años;
        }
        public void CargaPeriodosCmb(ComboBox cmbPeriodos, int TipoPeriodo)
        {
            List<EntCatalogoGenerico> periodos = new List<EntCatalogoGenerico>();
            //p = 1;
            Program.AñoInicio = DateTime.Today.Year - 2;
            //if (!this.Demo)
            //{
                for (int x = DateTime.Today.Year; x >= Program.AñoInicio; x--)
                {
                    switch (TipoPeriodo)
                    {
                        case 1: //MENSUAL
                            for (int p = 12; p >= 1; p--)
                            {
                                EntCatalogoGenerico periodo = new EntCatalogoGenerico();
                                periodo.Descripcion = x.ToString() + "-" + p.ToString().PadLeft(2, '0')
                                    + " " + new DateTime(Program.AñoInicio, p, 1).ToString("MMMM").ToUpper();
                                periodo.Id = x;
                                periodo.Clave = p.ToString().PadLeft(2, '0');
                                periodos.Add(periodo);
                            }
                            break;
                        case 2: //BIMESTRAL
                            for (int p = 6; p >= 1; p--)
                            {
                                int mesFinal = p * 2; //PARA TRIMESTRE APLICA x 3.
                                EntCatalogoGenerico periodo = new EntCatalogoGenerico();
                                periodo.Descripcion = x.ToString() + "-" + p.ToString().PadLeft(2, '0')
                                    + " " + new DateTime(Program.AñoInicio, mesFinal - 1, 1).ToString("MMM").ToUpper()
                                    + "-" + new DateTime(Program.AñoInicio, mesFinal, 1).ToString("MMM").ToUpper();
                                periodo.Id = x;
                                periodo.Clave = p.ToString().PadLeft(2, '0');
                                periodos.Add(periodo);
                            }
                            break;
                        case 3: //TRIMESTRAL
                            for (int p = 4; p >= 1; p--)
                            {
                                EntCatalogoGenerico periodo = new EntCatalogoGenerico();
                                periodo.Descripcion = x.ToString() + p.ToString().PadLeft(2, '0');
                                periodo.Id = x;
                                periodo.Clave = p.ToString().PadLeft(2, '0');
                                periodos.Add(periodo);
                            }
                            break;
                    }
                }
            //}
            //else //DEMO
            //{
            //    for (int x = Program.AñoInicio; x <= Program.AñoInicio + 1; x++)
            //    {
            //        switch (TipoPeriodo)
            //        {
            //            case 1:
            //                //DEMO
            //                if (x == Program.AñoInicio)
            //                {
            //                    for (int p = 11; p <= 12; p++)
            //                    {
            //                        EntCatalogoGenerico periodo = new EntCatalogoGenerico();
            //                        periodo.Descripcion = x.ToString() + p.ToString().PadLeft(2, '0');
            //                        periodo.Id = x;
            //                        periodo.Clave = p.ToString().PadLeft(2, '0');
            //                        periodos.Add(periodo);
            //                    }
            //                }
            //                else
            //                {
            //                    for (int p = 1; p <= 8; p++)
            //                    {
            //                        EntCatalogoGenerico periodo = new EntCatalogoGenerico();
            //                        periodo.Descripcion = x.ToString() + p.ToString().PadLeft(2, '0');
            //                        periodo.Id = x;
            //                        periodo.Clave = p.ToString().PadLeft(2, '0');
            //                        periodos.Add(periodo);
            //                    }
            //                }
            //                break;
            //            case 2:
            //                //DEMO
            //                if (x == Program.AñoInicio)
            //                {
            //                    for (int p = 6; p <= 6; p++)
            //                    {
            //                        EntCatalogoGenerico periodo = new EntCatalogoGenerico();
            //                        periodo.Descripcion = x.ToString() + p.ToString().PadLeft(2, '0');
            //                        periodo.Id = x;
            //                        periodo.Clave = p.ToString().PadLeft(2, '0');
            //                        periodos.Add(periodo);
            //                    }
            //                }
            //                else
            //                {
            //                    for (int p = 1; p <= 4; p++)
            //                    {
            //                        EntCatalogoGenerico periodo = new EntCatalogoGenerico();
            //                        periodo.Descripcion = x.ToString() + p.ToString().PadLeft(2, '0');
            //                        periodo.Id = x;
            //                        periodo.Clave = p.ToString().PadLeft(2, '0');
            //                        periodos.Add(periodo);
            //                    }
            //                }
            //                break;
            //            case 3:
            //                //DEMO
            //                if (x == Program.AñoInicio)
            //                {
            //                    for (int p = 4; p <= 4; p++)
            //                    {
            //                        EntCatalogoGenerico periodo = new EntCatalogoGenerico();
            //                        periodo.Descripcion = x.ToString() + p.ToString().PadLeft(2, '0');
            //                        periodo.Id = x;
            //                        periodo.Clave = p.ToString().PadLeft(2, '0');
            //                        periodos.Add(periodo);
            //                    }
            //                }
            //                else
            //                {
            //                    for (int p = 1; p <= 3; p++)
            //                    {
            //                        EntCatalogoGenerico periodo = new EntCatalogoGenerico();
            //                        periodo.Descripcion = x.ToString() + p.ToString().PadLeft(2, '0');
            //                        periodo.Id = x;
            //                        periodo.Clave = p.ToString().PadLeft(2, '0');
            //                        periodos.Add(periodo);
            //                    }
            //                }
            //                break;
            //        }
            //    }
            //}
            cmbPeriodos.DataSource = periodos;
        }
        public void CargaCatalogoRegimen(ComboBox CmbRegimenFiscal)
        {
            CmbRegimenFiscal.DataSource = new BusEmpresas().ObtieneCatalogoRegimen();
            CmbRegimenFiscal.SelectedIndex = 0;
        }
        public void CargaCatalogoUsoCFDI(ComboBox CmbUsoCFDI)
        {
            CmbUsoCFDI.DataSource = new BusEmpresas().ObtieneCatalogoUsoCFDI();
            CmbUsoCFDI.SelectedIndex = 0;
        }
        public void CargaTrabajadoresPorEmpresa(int EmpresaId, ComboBox CmbTrabajadores)
        {
            List<EntTrabajador> lst = new BusTrabajadores().ObtieneTrabajadoresPorEmpresa(EmpresaId);
            lst.Insert(0, new EntTrabajador() { Id = -1, Nombre = "-SELECCIONE-" });
            CmbTrabajadores.DataSource = lst;
            CmbTrabajadores.SelectedIndex = 0;
        }
        public void CargaTrabajadoresPorEmpresa(int EmpresaId, int TipoTrabajadorId, ComboBox CmbTrabajadores)
        {
            List<EntTrabajador> lst = new BusTrabajadores().ObtieneTrabajadoresPorEmpresa(EmpresaId,TipoTrabajadorId);
            lst.Insert(0, new EntTrabajador() { Id = -1, Nombre = "-SELECCIONE VENDEDOR-" });
            CmbTrabajadores.DataSource = lst;
            CmbTrabajadores.SelectedIndex = 0;
        }
        public void CargaTrabajadoresPorEmpresa(int EmpresaId, int TipoTrabajadorId, ComboBox CmbTrabajadores, int TrabajadorId)
        {
            List<EntTrabajador> lst = new BusTrabajadores().ObtieneTrabajadoresPorEmpresa(EmpresaId, TipoTrabajadorId);
            lst.Insert(0, new EntTrabajador() { Id = -1, Nombre = "-SELECCIONE VENDEDOR-" });
            CmbTrabajadores.DataSource = lst;
            CmbTrabajadores.SelectedIndex = 0;
            CmbTrabajadores.SelectedIndex = lst.FindIndex(P => P.Id == TrabajadorId);
            if (CmbTrabajadores.SelectedIndex == -1)
                CmbTrabajadores.SelectedIndex = 0;
        }
        public void CargaTrabajadores(int EstablecimientoId, ComboBox CmbTrabajadores)
        {
            List<EntTrabajador> lst = new BusTrabajadores().ObtieneTrabajadores(EstablecimientoId);
            lst.Insert(0, new EntTrabajador() { Id = -1, Nombre = "-SELECCIONE-" });
            CmbTrabajadores.DataSource = lst;
            CmbTrabajadores.SelectedIndex = 0;
        }
        public void CargaTrabajadores(int EstablecimientoId, ComboBox CmbTrabajadores, int TrabajadorId)
        {
            List<EntTrabajador> lst = new BusTrabajadores().ObtieneTrabajadores(EstablecimientoId);
            lst.Insert(0, new EntTrabajador() { Id = -1, Nombre = "-SELECCIONE-" });
            CmbTrabajadores.DataSource = lst;
            CmbTrabajadores.SelectedIndex = 0;
            CmbTrabajadores.SelectedIndex = lst.FindIndex(P => P.Id == TrabajadorId);
            if (CmbTrabajadores.SelectedIndex == -1)
                CmbTrabajadores.SelectedIndex = 0;
        }
        public void SeleccionarIndexComboBox(ComboBox comboBox, int idABuscar)
        {
            var dataSource = comboBox.DataSource;
            if (dataSource is System.Collections.IEnumerable enumerable)
            {
                int index = -1;
                int current = 0;

                foreach (var item in enumerable)
                {
                    var tipo = item.GetType();
                    var propiedadId = tipo.GetProperty("Id");
                    if (propiedadId != null)
                    {
                        var valor = propiedadId.GetValue(item);
                        if (valor is int id && id == idABuscar)
                        {
                            index = current;
                            break;
                        }
                    }
                    current++;
                }

                if (index >= 0)
                {
                    comboBox.SelectedIndex = index;
                }
            }
        }
        public DateTime ObtieneFechaInicial()
        {
            return new DateTime(AñoInicio, 1, 1);
        }
        public DateTime ObtieneLunesEstaSemana(DateTime Dia)
        {
            if (Dia.DayOfWeek == DayOfWeek.Monday)
                return Dia;
            else
                return ObtieneLunesEstaSemana(Dia.AddDays(-1));
        }
        public DateTime ObtieneFechaUltimoDiaMes(int Mes, int Año)
        {
            return new DateTime(Año, Mes, DateTime.DaysInMonth(Año, Mes));
        }
        public DateTime ObtieneFechaPrimerDiaMes(int Mes, int Año)
        {
            return new DateTime(Año, Mes, 1);
        }
        public DateTime FechaDesdeFromComboBoxs(ComboBox ComboBoxMes, ComboBox ComboBoxAño)
        {
            return new DateTime(ConvierteTextoAInteger(ComboBoxAño.Text), ComboBoxMes.SelectedIndex + 1, 1);
        }
        public DateTime FechaHastaFromComboBoxs(ComboBox ComboBoxMes, ComboBox ComboBoxAño)
        {
            return new DateTime(ConvierteTextoAInteger(ComboBoxAño.Text), ComboBoxMes.SelectedIndex + 1,
                    DateTime.DaysInMonth(ConvierteTextoAInteger(ComboBoxAño.Text), ComboBoxMes.SelectedIndex + 1));
        }
        public void AsignaFechaDesdeFechaHastaFromCmbPeriodos(ComboBox cmbPeriodos, int TipoPeriodo, ref DateTime FechaDesde, ref DateTime FechaHasta)
        {
            EntCatalogoGenerico periodo = ObtieneCatalogoGenericoFromCmb(cmbPeriodos);
            switch (TipoPeriodo)
            {
                case 1://MES
                    FechaDesde = ObtieneFechaPrimerDiaMes(ConvierteTextoAInteger(periodo.Clave), periodo.Id);
                    FechaHasta = ObtieneFechaUltimoDiaMes(ConvierteTextoAInteger(periodo.Clave), periodo.Id);
                    break;
                case 2://BIMESTRE
                    int mesFinal = ConvierteTextoAInteger(periodo.Clave) * 2; //PARA TRIMESTRE APLICA x 3.
                    FechaDesde = ObtieneFechaPrimerDiaMes(mesFinal - 1, periodo.Id);
                    FechaHasta = ObtieneFechaUltimoDiaMes(mesFinal, periodo.Id);
                    break;
                case 3://TRIMESTRE
                    int variable = ConvierteTextoAInteger(periodo.Clave);
                    int mesInicial = ConvierteTextoAInteger(periodo.Clave) * 2 + (variable - 2);
                    FechaDesde = ObtieneFechaPrimerDiaMes(mesInicial, periodo.Id);
                    FechaHasta = ObtieneFechaUltimoDiaMes(mesInicial + 2, periodo.Id);
                    break;
            }
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
        public int ConvierteTextoAInteger(TextBox TxtValor)
        {
            int resul = 0;
            int.TryParse(TxtValor.Text, out resul);
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
        public void LimpiaTextBox(Control Contenedor, bool InicializaComboBox, bool InicializaCheckBox)
        {
            foreach (Control c in Contenedor.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                    ((TextBox)c).Text = "";
                else if (c.GetType() == typeof(ComboBox) && InicializaComboBox)
                    ((ComboBox)c).SelectedIndex = 0;
                else if (c.GetType() == typeof(CheckBox) && InicializaCheckBox)
                    ((CheckBox)c).Checked = false;
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
        public void FiltrarFechasDeInteres(CheckBox chkUltimoMes, CheckBox chkUltimoTrimestre, CheckBox chkUltimoAño,
                                    ref System.Windows.Forms.DateTimePicker dtpFechaDesdeVentas, ref System.Windows.Forms.DateTimePicker dtpFechaHastaVentas,
                                    bool aPartirDehoy,
                                    ref System.Windows.Forms.Button btnRefrescarVentas)
        {
            int mesesFiltro = 1;
            DateTime fechaHasta = DateTime.Today;
            bool filtrar = true;
            if (chkUltimoMes.Checked)
            {
                mesesFiltro = 1;
                chkUltimoTrimestre.Checked = false;
                chkUltimoAño.Checked = false;
            }
            else
                if (chkUltimoTrimestre.Checked)
            {
                mesesFiltro = 3;
                chkUltimoMes.Checked = false;
                chkUltimoAño.Checked = false;
            }
            else
                if (chkUltimoAño.Checked)
            {
                mesesFiltro = 12;
                chkUltimoMes.Checked = false;
                chkUltimoTrimestre.Checked = false;
            }
            else
                filtrar = false;

            if (filtrar)
            {
                if (!aPartirDehoy)
                    fechaHasta = ObtieneFechaUltimoDiaMes(DateTime.Today.Month, DateTime.Today.Year);

                dtpFechaDesdeVentas.Value = fechaHasta.AddMonths(-mesesFiltro).AddDays(1);
                dtpFechaHastaVentas.Value = fechaHasta;
                btnRefrescarVentas.PerformClick();
            }
        }
        public EntFactura LeeXMLFactura(System.IO.FileInfo FileXML, bool Emitidos)
        {
            EntFactura fac = new EntFactura();

            string rutaArchivo = FileXML.FullName;
            if (FileXML.Extension.ToLower() != ".xml")
                MandaExcepcion("El archivo no es el correcto. \n Debe ser archivo XML (.xml)");

            if (FileXML.Extension.ToLower() == ".xml")
            {
                XmlTextReader reader = new XmlTextReader(rutaArchivo);
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element: // The node is an element.
                            if (reader.Name.ToUpper() == "cfdi:Comprobante".ToUpper())
                            {
                                while (reader.MoveToNextAttribute())
                                { // Read the attributes.
                                    switch (reader.Name.ToUpper())
                                    {
                                        case "VERSION":
                                            //fac.Version = reader.Value;
                                            break;
                                        case "FECHA":
                                            fac.Fecha = Convert.ToDateTime(reader.Value);
                                            break;
                                        case "SUBTOTAL":
                                            fac.SubTotal = Convert.ToDecimal(reader.Value);
                                            break;
                                        case "IEPS":
                                            fac.IEPS = Convert.ToDecimal(reader.Value);
                                            break;
                                        case "IVA":
                                            fac.IVA = Convert.ToDecimal(reader.Value);
                                            break;
                                        case "TOTAL":
                                            fac.Total = Convert.ToDecimal(reader.Value);
                                            break;
                                        case "DESCUENTO":
                                            fac.Descuento = Convert.ToDecimal(reader.Value);
                                            break;
                                        case "SERIE":
                                            fac.SerieFactura = reader.Value;
                                            break;
                                        case "FOLIO":
                                            fac.NumeroFactura = reader.Value;
                                            break;
                                        case "MONEDA":
                                            if (reader.Value.ToUpper() == "USD")
                                                fac.MonedaId = 2;
                                            else
                                                fac.MonedaId = 1;
                                            break;
                                        case "TIPOCAMBIO":
                                            //fac.TipoCambio = ConvierteTextoADecimal(reader.Value);
                                            break;
                                    }
                                }
                            }
                            if (Emitidos)
                            {
                                if (reader.Name.ToUpper() == "cfdi:Receptor".ToUpper())
                                {
                                    while (reader.MoveToNextAttribute())
                                    { // Read the attributes.
                                        Console.Write(" " + reader.Name + "='" + reader.Value + "'");
                                        switch (reader.Name.ToUpper())
                                        {
                                            case "NOMBRE":
                                                fac.Nombre = reader.Value;
                                                break;
                                            case "RFC":
                                                fac.RFC = reader.Value;
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (reader.Name.ToUpper() == "cfdi:Emisor".ToUpper())
                                {
                                    while (reader.MoveToNextAttribute())
                                    { // Read the attributes.
                                        Console.Write(" " + reader.Name + "='" + reader.Value + "'");
                                        switch (reader.Name.ToUpper())
                                        {
                                            case "NOMBRE":
                                                fac.Nombre = reader.Value.Replace("\n", "");
                                                break;
                                            case "RFC":
                                                fac.RFC = reader.Value;
                                                break;
                                        }
                                    }
                                }
                            }
                            if (reader.Name.ToUpper() == "cfdi:Conceptos".ToUpper())
                            {
                                fac.Productos = new List<EntProducto>();
                            }
                            if (reader.Name.ToUpper() == "cfdi:Concepto".ToUpper())
                            {
                                EntProducto p = new EntProducto();
                                //< cfdi:Concepto Descuento = "0.00" Importe = "500.0000" ValorUnitario = "500.0000" Descripcion = "MENSUALIDAD SERVICIO DE USO EN LA NUBE" Unidad = "Unidad de servicio" ClaveUnidad = "E48" Cantidad = "1" ClaveProdServ = "81111510" >
                                while (reader.MoveToNextAttribute())
                                { // Read the attributes.
                                    switch (reader.Name.ToUpper())
                                    {
                                        case "CLAVEPRODSERV":
                                            p.ClaveProductoServicio = reader.Value;
                                            break;
                                        case "CLAVEUNIDAD":
                                            p.ClaveUnidad = reader.Value;
                                            break;
                                        case "DESCRIPCION":
                                            p.ProductoServicio = reader.Value;
                                            break;
                                        case "CANTIDAD":
                                            p.Cantidad = Convert.ToDecimal(reader.Value);
                                            break;
                                        case "VALORUNITARIO":
                                            p.PrecioCosto = Convert.ToDecimal(reader.Value);
                                            break;
                                        case "DESCUENTO":
                                            p.PrecioEspecial = Convert.ToDecimal(reader.Value);
                                            break;
                                    }
                                }
                                fac.Productos.Add(p);
                            }
                            if (reader.Name.ToUpper() == "cfdi:Impuestos".ToUpper())
                            {
                                while (reader.MoveToNextAttribute())
                                { // Read the attributes.
                                    //Console.Write(" " + reader.Name + "='" + reader.Value + "'");
                                    switch (reader.Name.ToUpper())
                                    {
                                        case "TOTALIMPUESTOSTRASLADADOS":
                                            fac.IVA = Convert.ToDecimal(reader.Value);
                                            break;
                                        case "TOTALIMPUESTOSRETENIDOS":
                                            fac.Retenciones = Convert.ToDecimal(reader.Value);
                                            break;
                                    }
                                }
                            }
                            if (reader.Name.ToUpper() == "tfd:TimbreFiscalDigital".ToUpper())
                            {
                                while (reader.MoveToNextAttribute())
                                { // Read the attributes.
                                    //Console.Write(" " + reader.Name + "='" + reader.Value + "'");
                                    switch (reader.Name.ToUpper())
                                    {
                                        case "UUID":
                                            fac.UUID = reader.Value;
                                            break;
                                        case "FechaTimbrado":
                                            //fac.UUID = reader.Value;
                                            break;
                                        case "NoCertificadoSAT":
                                            //fac.UUID = reader.Value;
                                            break;
                                        case "SelloCFD":
                                            //fac.UUID = reader.Value;
                                            break;
                                    }
                                }
                            }
                            break;
                        case XmlNodeType.Text: //Display the text in each element.
                                               //Console.WriteLine(reader.Value);
                            break;
                        case XmlNodeType.EndElement: //Display the end of the element.
                                                     //Console.Write("</" + reader.Name);
                                                     //Console.WriteLine(">");
                            break;
                    }
                }
            }
            return fac;
        }

        /// <summary>
        /// VERIFICA SI EXISTEN LOS ARCHIVOS REQUERIDOS; SI NO EXISTEN LOS CREA.
        /// </summary>
        /// <param name="RutaArchivos"></param>
        /// <param name="NombreArchivos"></param>
        /// <param name="PDF"></param>
        /// <param name="XML"></param>
        public void VerificaExistenArchivosFactura(string RutaArchivos, string NombreArchivos, byte[] PDF, byte[] XML)
        {

            if (!System.IO.Directory.Exists(RutaArchivos))
            {
                //string pathFacturasBase = @"C:\TIIM\Facturacion\Facturas";
                if(string.IsNullOrWhiteSpace(RutaArchivos))
                    RutaArchivos= this.CreaPathClienteDirectorioFacturas(this.PathFacturas, "APP", NombreArchivos) ;
                System.IO.Directory.CreateDirectory(RutaArchivos);

                System.IO.File.WriteAllBytes(RutaArchivos + "\\" + NombreArchivos + ".pdf", PDF);
                System.IO.File.WriteAllBytes(RutaArchivos + "\\" + NombreArchivos + ".xml", XML);
            }
            else
            {
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(RutaArchivos);
                if (dir.GetFiles().Length == 0)
                {
                    System.IO.File.WriteAllBytes(RutaArchivos + "\\" + NombreArchivos + ".pdf", PDF);
                    System.IO.File.WriteAllBytes(RutaArchivos + "\\" + NombreArchivos + ".xml", XML);
                }
            }
        }
        public void VerificaExistenArchivosFactura(EntPedido PedidoFactura)
        {

            if (!System.IO.Directory.Exists(PedidoFactura.RutaFactura))
            {
                System.IO.Directory.CreateDirectory(PedidoFactura.RutaFactura);

                System.IO.File.WriteAllBytes(PedidoFactura.RutaFactura + "\\" + PedidoFactura.Factura + ".pdf", PedidoFactura.PDF);
                System.IO.File.WriteAllBytes(PedidoFactura.RutaFactura + "\\" + PedidoFactura.Factura + ".xml", PedidoFactura.XML);
            }
            else
            {
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PedidoFactura.RutaFactura);
                if (dir.GetFiles().Length == 0)
                {
                    System.IO.File.WriteAllBytes(PedidoFactura.RutaFactura + "\\" + PedidoFactura.UUID + ".pdf", PedidoFactura.PDF);
                    System.IO.File.WriteAllBytes(PedidoFactura.RutaFactura + "\\" + PedidoFactura.UUID + ".xml", PedidoFactura.XML);
                }
            }
        }
        /// <summary>
        /// Verifica si existe directorio para la Ruta + DateTime.Today y para Ruta + DateTime.Today + Cliente.
        /// Si no existen los crea.
        /// Regresa la Ruta completa (Ruta + DateTime.Today + Cliente).
        /// </summary>
        /// <param name="Ruta"></param>
        /// <param name="ClienteId"></param>
        public string VerificaRutas(string Ruta, string ClienteOrden)
        {
            string rutaCompleta = Ruta; //+ DateTime.Today.ToString("yyyyMMdd");
            if (!System.IO.Directory.Exists(rutaCompleta))
                System.IO.Directory.CreateDirectory(rutaCompleta);
            if (!System.IO.Directory.Exists(rutaCompleta + "\\" + ClienteOrden))
                System.IO.Directory.CreateDirectory(rutaCompleta + "\\" + ClienteOrden);

            return rutaCompleta + "\\" + ClienteOrden;
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
        /// 
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="FileName"></param>
        public string SeleccionaArchivoConFiltro(string ExtensionFiltro)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos " + ExtensionFiltro.Remove(0, 1).ToUpper() + "|*" + ExtensionFiltro;
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
        /// Busca el tipo de archivo con la Extension solicitada y regresa el nombre del archivo (el primero que encuentre).
        /// </summary>
        /// <param name="Ruta">Ruta del directorio que contiene el archivo</param>
        /// <param name="Extension"></param>
        /// <returns></returns>
        public string EncuentraArchivoOriginal(string Ruta, string Extension)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Ruta);

            System.IO.FileInfo[] fi = di.GetFiles();
            foreach (System.IO.FileInfo f in fi)
            {
                if (f.Extension == Extension && !f.Name.Contains("OBSERVACION"))
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

            if (di.Exists)
            {
                System.IO.FileInfo[] fi = di.GetFiles();
                //foreach (System.IO.FileInfo f in fi)
                //{
                //    if (f.Extension == Extension)
                //        return f.Name;
                //}
                return fi.Length;
            }
            return 0;
        }
        public string CreaPathClienteDirectorioFacturas(string PathFacturaBase, string NombreCliente, string SerieFactura, string NumeroFactura)
        {
            string pathClienteDirectorio = PathFacturaBase + "\\" + NombreCliente;
            if (!System.IO.Directory.Exists(pathClienteDirectorio))
                System.IO.Directory.CreateDirectory(pathClienteDirectorio);

            string pathClienteDirectorioFacturas = pathClienteDirectorio + "\\"
                                                    + SerieFactura + " " + NumeroFactura;
            System.IO.Directory.CreateDirectory(pathClienteDirectorioFacturas);
            return pathClienteDirectorioFacturas;
        }
        public void EliminaArchivosDireccion(string PathDirectorioArchivos)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PathDirectorioArchivos);
            foreach (System.IO.FileInfo file in dir.GetFiles())
            {
                file.Delete();
            }
        }

        void ReAsignaArchivosComplementoPago(EntFactura Factura)
        {
            if (Factura.XML.Length == new byte[1].Length)
            {
                string rutaArchivoXml = SeleccionaArchivoConFiltro(".xml");
                if (!string.IsNullOrWhiteSpace(rutaArchivoXml))
                {
                    Factura.XML = System.IO.File.ReadAllBytes(rutaArchivoXml);
                    if (!string.IsNullOrWhiteSpace(EncuentraArchivo(Factura.Ruta, ".xml"))) //ELIMINA ARCHIVO "VACIO" o con ERROR.
                        File.Delete(Factura.Ruta + "\\" + EncuentraArchivo(Factura.Ruta, ".xml"));
                }
                else
                    return;
            }

            if (Factura.PDF.Length == new byte[1].Length)
            {
                string rutaArchivoPDF = SeleccionaArchivoConFiltro(".pdf");
                if (!string.IsNullOrWhiteSpace(rutaArchivoPDF))
                {
                    Factura.PDF = System.IO.File.ReadAllBytes(rutaArchivoPDF);
                    if (!string.IsNullOrWhiteSpace(EncuentraArchivo(Factura.Ruta, ".pdf")))
                        File.Delete(Factura.Ruta + "\\" + EncuentraArchivo(Factura.Ruta, ".pdf"));
                }
                else
                    return;
            }

            Factura.Ruta = this.PathFacturas + "\\" + Factura.Nombre + "\\CP" + Factura.NumeroComplemento + " " + Factura.NumeroFactura;
            new BusFacturas().ActualizaPDFXMLEnComplementoPago(Factura);

            VerificaExistenArchivosFactura(Factura.Ruta, "CP" + Factura.NumeroComplemento + " " + Factura.NumeroFactura, Factura.PDF, Factura.XML);
            //MuestraArchivo(facturaEncontrada.Ruta);
        }

        public bool VerificaReAsignarFactura(EntFactura Factura)
        {
            if (string.IsNullOrWhiteSpace(Factura.Ruta) || Factura.PDF.Length == new byte[1].Length || Factura.XML.Length == new byte[1].Length)
            // || pedidoSeleccionado.RutaFactura == null)
            {
                if (MuestraMensajeYesNo("NO SE ENCONTRARON ARCHIVOS PARA MOSTRAR. \n ¿Desea asignar archivos PDF y XML?") == DialogResult.Yes)
                    ReAsignaArchivosComplementoPago(Factura);
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Muestra Ventana emergente para Confirmar Envio de Correo, llama los métodos Imprime.AsignaValoresParametrosImpresionDatosCliente y Imprime.AsignaValoresParametrosImpresion.
        /// Envia correo electronico por medio de la clase UtiCorreo.
        /// </summary>
        /// <param name="Pedido"></param>
        /// <param name="Cliente"></param>
        /// <param name="NotaVenta"></param>
        /// <param name="Presupuesto"></param>
        public void EnviaCorreo(string TipoDocumento, EntEmpresa EmpresaSeleccionada, EntPedido Pedido, EntCliente Cliente,
                                string PathArchivosFactura,
                                string Email2, string Email3,
                                string UUID)
        {
            this.SetWaitCursor();

            try
            {
                MandaExcepcion("MANDA DIRECTO A ENVIO CORREO PADE");
                List<string> archivosAdjuntos = new List<string>();

                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PathArchivosFactura);
                foreach (System.IO.FileInfo file in dir.GetFiles())
                {
                    archivosAdjuntos.Add(file.FullName);
                }

                string asunto = TipoDocumento + " " + Pedido.Factura + " - " + EmpresaSeleccionada.Nombre + " | ";
                string mensaje = "Apreciable " + Cliente.NombreFiscal + ", \n" +
                    "\n Le enviamos su debido comprobante fiscal, recordandole que estamos a sus ordenes para cualquier duda o aclaración. \n" +
                    "\n Agradecemos su preferencia y esperamos seguirle atendiendo como se merece. \n";
                mensaje += "\n Atte. \n " + EmpresaSeleccionada.Nombre;

                new AiresUtilerias.UtiCorreo().EnviaCorreo(asunto, new List<string>() { Cliente.Email, Email2, Email3 },
                                                            mensaje, archivosAdjuntos);

                MessageBox.Show("El Correo se ha Enviado correctamente a la(s) dirección(es): " +
                                "\n " + Cliente.Email + " \n " + Email2 + " \n " + Email3);
            }
            catch (Exception ex)
            {
                new UtiCorreo().EnviaCorreoPADE(new List<string>() { Cliente.Email, Email2, Email3 },
                                                        Program.EmpresaSeleccionada.NumeroReferencia, UUID);
                MuestraMensaje("El Correo se ha Enviado correctamente, a la(s) dirección(es): \n\n " + Cliente.Email
                    + " \n " + Email2
                    + " \n " + Email3);
            }
        }
        public void EnviaCorreo(string TipoDocumento, string Mensaje, EntEmpresa EmpresaSeleccionada, EntPedido Pedido, EntCliente Cliente,
                                string PathArchivosFactura,
                                string Email2, string Email3)
        {
            this.SetWaitCursor();

            List<string> archivosAdjuntos = new List<string>();

            if (!string.IsNullOrWhiteSpace(PathArchivosFactura))
            {
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PathArchivosFactura);
                foreach (System.IO.FileInfo file in dir.GetFiles())
                {
                    archivosAdjuntos.Add(file.FullName);
                }
            }

            string asunto = TipoDocumento + " " + Pedido.Factura + " - "+ EmpresaSeleccionada.Nombre;
            string mensaje = Mensaje;

            new AiresUtilerias.UtiCorreo().EnviaCorreo(asunto, new List<string>() { Cliente.Email, Email2, Email3 }, mensaje, archivosAdjuntos);

            MessageBox.Show("El Correo se ha Enviado correctamente, a la dirección -" + Cliente.Email + "-");
            //}
        }
        /// <summary>
        /// Muestra Ventana emergente para Confirmar Envio de Correo, llama los métodos Imprime.AsignaValoresParametrosImpresionDatosCliente y Imprime.AsignaValoresParametrosImpresion.
        /// Envia correo electronico por medio de la clase UtiCorreo.
        /// </summary>
        /// <param name="Pedido"></param>
        /// <param name="Cliente"></param>
        /// <param name="NotaVenta"></param>
        /// <param name="Presupuesto"></param>
        public void EnviaCorreo(string TipoDocumento, EntEmpresa EmpresaSeleccionada, string NumeroDocumento, EntCliente Cliente,
                                string PathArchivosFactura,
                                string Email2, string Email3,
                                string UUID)
        {
            this.SetWaitCursor();
            try
            {
                MandaExcepcion("MANDA DIRECTO A ENVIO CORREO PADE");
                List<string> archivosAdjuntos = new List<string>();

                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PathArchivosFactura);
                foreach (System.IO.FileInfo file in dir.GetFiles())
                {
                    archivosAdjuntos.Add(file.FullName);
                }

                string asunto = TipoDocumento + " " + NumeroDocumento + " - " + EmpresaSeleccionada.Nombre + " | ";
                string mensaje = "Apreciable " + Cliente.NombreFiscal + ", \n" +
                    "\n Le enviamos su debido comprobante fiscal, recordandole que estamos a sus ordenes para cualquier duda o aclaración. \n" +
                    "\n Agradecemos su preferencia y esperamos seguirle atendiendo como se merece. \n";
                mensaje += "\n Atte. \n " + EmpresaSeleccionada.Nombre;

                new AiresUtilerias.UtiCorreo().EnviaCorreo(asunto, new List<string>() { Cliente.Email, Email2, Email3 },
                                                            mensaje, archivosAdjuntos);

                MessageBox.Show("El Correo se ha Enviado correctamente a la(s) dirección(es): " +
                                "\n " + Cliente.Email + " \n " + Email2 + " \n " + Email3);
            }
            catch (Exception ex)
            {
                new UtiCorreo().EnviaCorreoPADE(new List<string>() { Cliente.Email, Email2, Email3 },
                                                        Program.EmpresaSeleccionada.NumeroReferencia, UUID);
                MuestraMensaje("El Correo se ha Enviado correctamente, a la(s) dirección(es): \n\n " + Cliente.Email
                    + " \n " + Email2
                    + " \n " + Email3);
            }
        }

        /// <summary>
        /// "CONFIRMACIÓN"
        /// </summary>
        /// <param name="Mensaje"></param>
        public void MuestraMensaje(string Mensaje)
        {
            MessageBox.Show(Mensaje, "CONFIRMACIÓN", MessageBoxButtons.OK);
        }
        public void MuestraMensaje(string Mensaje, string Titulo)
        {
            MessageBox.Show(Mensaje, Titulo, MessageBoxButtons.OK);
        }
        public void MuestraMensajeError(string Mensaje, string Titulo)
        {
            MessageBox.Show(Mensaje, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void MuestraMensajeError(string Mensaje)
        {
            MessageBox.Show(Mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            {
                MuestraExcepcion(ex, "Pedido NO Facturado");
                if (MuestraMensajeYesNo("¿Desea enviar correo de su error al administrador del Sistema?", "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    this.SetWaitCursor();
                    try
                    {
                        this.EnviaCorreo("ERROR", ex.Message, Program.EmpresaSeleccionada, new EntPedido(), new EntCliente() { Email = "pavel_tiim@hotmail.com" }, "", "", "");
                    }
                    catch (Exception eex)
                    {
                        MuestraExcepcion(eex, "Correo NO enviado.");
                    }

                    this.SetDefaultCursor();
                }
            }
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

        public List<EntPedido> ConvierteListaClientesEnPedidos(List<EntCliente> ListaClientes)
        {
            List<EntPedido> lst = new List<EntPedido>();
            foreach (EntCliente p in ListaClientes)
            {
                EntPedido e = new EntPedido()
                {
                    ClienteId = p.Id,
                    ClienteCodigo = p.NumCliente,
                    Cliente = p.Nombre,
                    ClienteNombreFiscal = p.NombreFiscal,
                    ClienteRFC = p.RFC,
                    Direccion=p.Direccion,
                    Detalle = p.Celular + " | " + p.Telefono + " | " + p.Telefono2+ " ",
                    Descripcion= p.Email + " | "+p.Email2 + " | " + p.Email3+ " "
                };
                e.Detalle = e.Detalle.Replace(" |  ", "");
                e.Descripcion = e.Descripcion.Replace(" |  ", "");
                lst.Add(e);
            }
            return lst;
        }
        public List<EntPedido> ConvierteListaPagosEnPedidos(List<EntPago> ListaClientes)
        {
            List<EntPedido> lst = new List<EntPedido>();
            EntCliente cliente = new BusClientes().ObtieneCliente(ListaClientes.First().EmpresaId);
            foreach (EntPago p in ListaClientes)
            {
                EntPedido e = new EntPedido()
                {
                    ClienteId = cliente.Id,
                    ClienteCodigo = cliente.NumCliente,
                    Cliente = cliente.Nombre,
                    ClienteNombreFiscal = cliente.NombreFiscal,
                    ClienteRFC = cliente.RFC,
                    Direccion = cliente.Direccion,
                    
                    Id=p.PedidoId,
                    FechaPago=p.FechaPago,
                    Pago = p.Cantidad,
                    Detalle = p.FormaPago,
                    Fecha=p.Fecha,//FECHAPEDIDO
                    Factura = p.Factura,
                    Descripcion = p.Descripcion,



                    //Detalle = cliente.Celular + " | " + cliente.Telefono + " | " + cliente.Telefono2 + " ",
                    //Descripcion = cliente.Email + " | " + cliente.Email2 + " | " + cliente.Email3 + " "
                };
                //e.Detalle = e.Detalle.Replace(" |  ", "");
                //e.Descripcion = e.Descripcion.Replace(" |  ", "");
                lst.Add(e);
            }
            return lst;
        }
        public List<EntFactura> ConvierteListaPedidosEnFacturas(List<EntPedido> ListaPedidos)
        {
            List<EntFactura> lst = new List<EntFactura>();
            foreach (EntPedido p in ListaPedidos)
            {
                EntFactura e = new EntFactura()
                {
                    Id = p.FacturaId,
                    PedidoId = p.Id,
                    Saldo = p.Debe,
                    Pago = p.PagoTotal,
                    //IVA = p.IVA,
                    IEPS = p.IEPS,
                    Total = p.Total,
                    //SubTotal = p.Total - p.IVA,
                    Fecha = p.Fecha,
                    //FechaCorta = p.FechaCorta,
                    //Serie = p.Detalle,
                    NumeroFactura = p.Factura,
                    UUID = p.UUID,
                    //MonedaId = p.TipoMonedaId,
                    //TipoCambio = p.TipoCambio,
                    VersionCFDI = p.VersionCFDI,
                    Parcialidad = new AiresNegocio.BusFacturas().ObtieneNumeroParcialidades(p.FacturaId) + 1
                };
                lst.Add(e);
            }
            return lst;
        }

        public List<EntProducto> ObtieneListaProductosFromGV(DataGridView GridViewProductos)
        {
            if (GridViewProductos.DataSource == null)
                return new List<EntProducto>();

            if (GridViewProductos.Rows.Count == 0)
                return new List<EntProducto>();

            return (List<EntProducto>)GridViewProductos.DataSource;
        }
        public List<EntCatalogoGenerico> ObtieneListaGenericaFromGV(DataGridView GridViewGenerico)
        {
            if (GridViewGenerico.DataSource == null)
                return new List<EntCatalogoGenerico>();
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
            if (GridViewPedidos.DataSource == null)
                return new List<EntPedido>();
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
        public List<EntProveedor> ObtieneListaProveedoresFromCmb(ComboBox ComboBoxProveedores)
        {
            return (List<EntProveedor>)ComboBoxProveedores.DataSource;
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
            if (ComboBoxCliente.SelectedIndex >= 0)
                return (EntCliente)((List<EntCliente>)ComboBoxCliente.DataSource)[ComboBoxCliente.SelectedIndex];
           
            return new EntCliente();
        }
        public EntCliente ObtieneClienteAnteriorFromGV(DataGridView GridViewClientes)
        {
            if (GridViewClientes.CurrentRow == null)
                return null;

            return (EntCliente)((List<EntCliente>)GridViewClientes.DataSource)[GridViewClientes.CurrentRow.Index - 1];
        }
        public EntTrabajador ObtieneTrabajadorFromGV(DataGridView GridViewTrabajadores)
        {
            if (GridViewTrabajadores.CurrentRow == null)
                return null;
            return (EntTrabajador)((List<EntTrabajador>)GridViewTrabajadores.DataSource)[GridViewTrabajadores.CurrentRow.Index];
        }
        public EntTrabajador ObtieneTrabajadorFromCmb(ComboBox ComboBoxTrabajadores)
        {
            return (EntTrabajador)((List<EntTrabajador>)ComboBoxTrabajadores.DataSource)[ComboBoxTrabajadores.SelectedIndex];
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
        public EntProveedor ObtieneProveedorFromCmb(ComboBox ComboBoxProveedor)
        {
            return (EntProveedor)((List<EntProveedor>)ComboBoxProveedor.DataSource)[ComboBoxProveedor.SelectedIndex];
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
        public EntFactura ObtieneFacturaFromGV(DataGridView GridViewFacturas)
        {
            if (GridViewFacturas.CurrentRow == null)
                return null;

            return (EntFactura)((List<EntFactura>)GridViewFacturas.DataSource)[GridViewFacturas.CurrentRow.Index];
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

        /// <summary>
        /// USA ImprimirNotaVenta DE UtiImpresiones
        /// </summary>
        /// <param name="RutaCompleta"></param>
        /// <param name="Fondo"></param>
        /// <param name="Logo"></param>
        /// <param name="Leyenda"></param>
        /// <param name="Firma"></param>
        /// <param name="TituloImpresion"></param>
        /// <param name="NumeroOrden"></param>
        /// <param name="EmpresaSeleccionada"></param>
        /// <param name="ClienteSeleccionado"></param>
        /// <param name="PedidoAgrega"></param>
        /// <param name="ProductosSeleccionados"></param>
        public void CreaImagenBMPnotaVenta(string RutaCompleta,
                                System.Drawing.Image Fondo, System.Drawing.Image Logo, System.Drawing.Image Leyenda, System.Drawing.Image Firma,
                                string TituloImpresion, string NumeroOrden, EntEmpresa EmpresaSeleccionada, EntCliente ClienteSeleccionado, EntPedido PedidoAgrega,
                                List<EntProducto> ProductosSeleccionados)
        {
            string rutaArchivoImagen = RutaCompleta + "\\" + NumeroOrden + ".bmp";
            using (Bitmap myBitmap = new Bitmap(820, 1070))
            {
                Graphics newGraphics = Graphics.FromImage(myBitmap);
                newGraphics.DrawImage(Fondo, 0, 0);
                UtiImpresiones imprimir = new UtiImpresiones();
                imprimir.ImprimirNotaVenta(TituloImpresion,EmpresaSeleccionada, ClienteSeleccionado, 
                                        PedidoAgrega, ProductosSeleccionados, this.IVA,
                                        Logo, Leyenda, Firma, newGraphics);

                //myBitmap.Save(RutaCompleta+ "\\"+NumeroOrden+".bmp");

                using (MemoryStream stream = new MemoryStream())
                {
                    StreamWriter sw = new StreamWriter(rutaArchivoImagen);
                    myBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    stream.WriteTo(sw.BaseStream);
                    Clipboard.SetImage(System.Drawing.Image.FromStream(stream));//FromFile(RutaCompleta + "\\" + NumeroOrden + ".bmp"));
                    sw.Close();
                    stream.Close();
                }
                myBitmap.Dispose();
                newGraphics.Dispose();
            }
        }
        public List<string> CreaImagenesBMPnotaVenta(string RutaCompleta, Image Fondo, Image Logo, Image Leyenda, Image Firma,
                            string TituloImpresion, string NumeroOrden,
                            EntEmpresa EmpresaSeleccionada, EntCliente ClienteSeleccionado, EntPedido PedidoAgrega, 
                            List<EntProducto> ProductosSeleccionados)
        {
            int index = 0;
            int cantLimite = 25;
            List<string> listaRutas = new List<string>();
            while (index < ProductosSeleccionados.Count)
            {
                string rutaArchivoImagen = RutaCompleta + "\\" + NumeroOrden + "-" + index.ToString() + ".bmp";
                listaRutas.Add(rutaArchivoImagen);
                using (Bitmap myBitmap = new Bitmap(820, 1100))
                {
                    Graphics newGraphics = Graphics.FromImage(myBitmap);
                    newGraphics.DrawImage(Fondo, 0, 0);
                    UtiImpresiones imprimir = new UtiImpresiones();

                    if (index + cantLimite < ProductosSeleccionados.Count)
                    {
                        imprimir.ImprimirNotaVentaSoloProductos(TituloImpresion,EmpresaSeleccionada, ClienteSeleccionado, PedidoAgrega,
                            ProductosSeleccionados.GetRange(index, cantLimite), IVA, Logo, Leyenda, Firma, newGraphics);
                    }
                    else
                        imprimir.ImprimirNotaVenta(TituloImpresion,EmpresaSeleccionada, ClienteSeleccionado, 
                                            PedidoAgrega,ProductosSeleccionados.GetRange(index, ProductosSeleccionados.Count - index), IVA, 
                                            Logo, Leyenda, Firma, newGraphics);
                    //myBitmap.Save(RutaCompleta+ "\\"+NumeroOrden+".bmp");

                    using (MemoryStream stream = new MemoryStream())
                    {
                        StreamWriter sw = new StreamWriter(rutaArchivoImagen);
                        myBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                        stream.WriteTo(sw.BaseStream);
                        Clipboard.SetImage(Image.FromStream(stream));//FromFile(RutaCompleta + "\\" + NumeroOrden + ".bmp"));
                                                                     //new UtiPDF().ModificaPDF(Image.FromStream(stream),
                                                                     //                    @"C:\TIIM\OrdenesCompra\OrdenCompra.pdf", base.RutaImpresionCompra + PedidoAgrega.Cliente + "\\" + PedidoAgrega.NumOrden + "\\" + PedidoAgrega.NumOrden + ".pdf");
                        sw.Close();
                        stream.Close();
                    }
                    myBitmap.Dispose();
                    newGraphics.Dispose();
                }
                index += cantLimite;
            }
            return listaRutas;
        }
        public void CreaImagenBMPsalida(string RutaCompleta,
                                System.Drawing.Image Fondo, System.Drawing.Image Logo, System.Drawing.Image Leyenda, System.Drawing.Image Firma,
                                string NumeroOrden, EntEmpresa EmpresaSeleccionada, EntCliente ClienteSeleccionado, EntPedido PedidoAgrega,
                                List<EntProducto> ProductosSeleccionados, string TipoImpresion)
        {
            string rutaArchivoImagen = RutaCompleta + "\\" + NumeroOrden + ".bmp";
            using (Bitmap myBitmap = new Bitmap(820, 1070))
            {
                Graphics newGraphics = Graphics.FromImage(myBitmap);
                newGraphics.DrawImage(Fondo, 0, 0);
                UtiImpresiones imprimir = new UtiImpresiones();
                imprimir.ImprimirSalida(EmpresaSeleccionada, ClienteSeleccionado, PedidoAgrega, ProductosSeleccionados, this.IVA,
                                        Logo, Leyenda, Firma, newGraphics,
                                        TipoImpresion);

                //myBitmap.Save(RutaCompleta+ "\\"+NumeroOrden+".bmp");

                using (MemoryStream stream = new MemoryStream())
                {
                    StreamWriter sw = new StreamWriter(rutaArchivoImagen);
                    myBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    stream.WriteTo(sw.BaseStream);
                    Clipboard.SetImage(System.Drawing.Image.FromStream(stream));//FromFile(RutaCompleta + "\\" + NumeroOrden + ".bmp"));
                    sw.Close();
                    stream.Close();
                }
                myBitmap.Dispose();
                newGraphics.Dispose();
            }
        }
        public void CreaImagenBMPentrada(string RutaCompleta,
                                System.Drawing.Image Fondo, System.Drawing.Image Logo, System.Drawing.Image Leyenda, System.Drawing.Image Firma,
                                string NumeroOrden, EntEmpresa EmpresaSeleccionada, EntCliente ClienteSeleccionado, EntPedido PedidoAgrega,
                                List<EntProducto> ProductosSeleccionados)
        {
            string rutaArchivoImagen = RutaCompleta + "\\" + NumeroOrden + ".bmp";
            using (Bitmap myBitmap = new Bitmap(820, 1070))
            {
                Graphics newGraphics = Graphics.FromImage(myBitmap);
                newGraphics.DrawImage(Fondo, 0, 0);
                UtiImpresiones imprimir = new UtiImpresiones();
                imprimir.ImprimirEntrada(EmpresaSeleccionada, ClienteSeleccionado, PedidoAgrega, ProductosSeleccionados, this.IVA,
                                        Logo, Leyenda, Firma, newGraphics);

                //myBitmap.Save(RutaCompleta+ "\\"+NumeroOrden+".bmp");

                using (MemoryStream stream = new MemoryStream())
                {
                    StreamWriter sw = new StreamWriter(rutaArchivoImagen);
                    myBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    stream.WriteTo(sw.BaseStream);
                    Clipboard.SetImage(System.Drawing.Image.FromStream(stream));//FromFile(RutaCompleta + "\\" + NumeroOrden + ".bmp"));
                    sw.Close();
                    stream.Close();
                }
                myBitmap.Dispose();
                newGraphics.Dispose();
            }
        }

        public List<string> CreaImagenesBMPcomplementoPagoResumido(string RutaCompleta, 
                            Image Fondo, Image Logo, Image Leyenda, Image Firma,
                            string TituloImpresion, string NumeroOrden,
                            EntEmpresa EmpresaSeleccionada, EntCliente ClienteSeleccionado, List<EntFactura> FacturasPago)
        {
            int index = 0;
            int cantLimite = 25;
            List<string> listaRutas = new List<string>();
            while (index < FacturasPago.Count)
            {
                string rutaArchivoImagen = RutaCompleta + "\\" + NumeroOrden + "-" + index.ToString() + ".bmp";
                listaRutas.Add(rutaArchivoImagen);
                using (Bitmap myBitmap = new Bitmap(820, 1100))
                {
                    Graphics newGraphics = Graphics.FromImage(myBitmap);
                    newGraphics.DrawImage(Fondo, 0, 0);
                    UtiImpresiones imprimir = new UtiImpresiones();

                    //if (index + cantLimite < FacturasPago.Count)
                    //{
                    //    imprimir.ImprimirComplementoPagoSoloPagos(TituloImpresion, EmpresaSeleccionada, ClienteSeleccionado, PedidoAgrega,
                    //        FacturasPago.GetRange(index, cantLimite), IVA, Logo, Leyenda, Firma, newGraphics);
                    //}
                    //else
                    //    imprimir.ImprimirNotaVenta(TituloImpresion, EmpresaSeleccionada, ClienteSeleccionado,
                    //                        PedidoAgrega, ProductosSeleccionados.GetRange(index, ProductosSeleccionados.Count - index), IVA,
                    //                        Logo, Leyenda, Firma, newGraphics);
                    ////myBitmap.Save(RutaCompleta+ "\\"+NumeroOrden+".bmp");

                    using (MemoryStream stream = new MemoryStream())
                    {
                        StreamWriter sw = new StreamWriter(rutaArchivoImagen);
                        myBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                        stream.WriteTo(sw.BaseStream);
                        Clipboard.SetImage(Image.FromStream(stream));//FromFile(RutaCompleta + "\\" + NumeroOrden + ".bmp"));
                                                                     //new UtiPDF().ModificaPDF(Image.FromStream(stream),
                                                                     //                    @"C:\TIIM\OrdenesCompra\OrdenCompra.pdf", base.RutaImpresionCompra + PedidoAgrega.Cliente + "\\" + PedidoAgrega.NumOrden + "\\" + PedidoAgrega.NumOrden + ".pdf");
                        sw.Close();
                        stream.Close();
                    }
                    myBitmap.Dispose();
                    newGraphics.Dispose();
                }
                index += cantLimite;
            }
            return listaRutas;
        }

        public void ImprimirNotaVenta(string TituloImpresion, EntCliente Cliente, EntPedido Pedido, List<EntProducto> ProductosSeleccionados,
                                System.Drawing.Image Fondo, System.Drawing.Image Logo, System.Drawing.Image Leyenda)
        {
            string rutaGuardaArchivosOrden = this.RutaImpresion + "\\" + Cliente.Nombre + "\\" + Pedido.NumOrden;
            this.VerificaRutas(this.RutaImpresion, Cliente.Nombre + "\\" + Pedido.NumOrden);

            List<string> lstRutas = new List<string>();
            if (ProductosSeleccionados.Count > 20)
                lstRutas = this.CreaImagenesBMPnotaVenta(rutaGuardaArchivosOrden,
                                    Fondo, Logo, Leyenda, Fondo,
                                    TituloImpresion, Pedido.NumOrden,
                                    Program.EmpresaSeleccionada, Cliente, Pedido, ProductosSeleccionados);
            else
            {
                this.CreaImagenBMPnotaVenta(rutaGuardaArchivosOrden,
                                    Fondo, Logo, Leyenda, Fondo,
                                    TituloImpresion, Pedido.NumOrden,
                                    Program.EmpresaSeleccionada, Cliente, Pedido, ProductosSeleccionados);
                lstRutas.Add(rutaGuardaArchivosOrden + "\\" + Pedido.NumOrden + ".bmp");
            }

            try
            {
                new UtiPDF().CreaPDF(rutaGuardaArchivosOrden + "\\" + Pedido.NumOrden + ".bmp",
                                        rutaGuardaArchivosOrden + "\\NOTAVENTA-" + Pedido.NumOrden + ".pdf");
            }
            catch (Exception ex) { }
            finally
            {
                string nombreArchivo = EncuentraArchivo(rutaGuardaArchivosOrden, ".pdf");
                MuestraArchivo(rutaGuardaArchivosOrden, nombreArchivo);
            }
        }

        public void ImprimirSalida(EntCliente Cliente, EntPedido PedidoCotizacion, List<EntProducto> ProductosSeleccionados,
                                System.Drawing.Image Fondo, System.Drawing.Image Logo, System.Drawing.Image Leyenda, 
                                string TipoImpresion="SALIDA")
        {
            string numeroOrden = PedidoCotizacion.NumOrden.Replace("\n", "");
            string rutaGuardaArchivosOrden = this.RutaImpresion + "\\" + Cliente.Nombre + "\\" + numeroOrden;
            VerificaRutas(this.RutaImpresion, Cliente.Nombre + "\\" + numeroOrden);
            CreaImagenBMPsalida(rutaGuardaArchivosOrden, Fondo, Logo, Leyenda, Fondo, numeroOrden,
                            Program.EmpresaSeleccionada, Cliente, PedidoCotizacion, ProductosSeleccionados,
                            TipoImpresion);
            try
            {
                new UtiPDF().CreaPDF(rutaGuardaArchivosOrden + "\\" + numeroOrden + ".bmp",
                                        rutaGuardaArchivosOrden + "\\"+TipoImpresion+"-" + numeroOrden + ".pdf");
            }
            catch (Exception ex) { }
            finally
            {
                string nombreArchivo = EncuentraArchivo(rutaGuardaArchivosOrden, ".pdf");
                MuestraArchivo(rutaGuardaArchivosOrden, nombreArchivo);
            }
        }
        public void ImprimirEntrada(EntCliente Proveedor, EntPedido PedidoCotizacion, List<EntProducto> ProductosSeleccionados,
                                System.Drawing.Image Fondo, System.Drawing.Image Logo, System.Drawing.Image Leyenda)
        {
            string numeroOrden = PedidoCotizacion.NumOrden.Replace("\n", "");
            string rutaGuardaArchivosOrden = this.RutaImpresion + "\\" + Proveedor.Nombre + "\\" + numeroOrden;
            VerificaRutas(this.RutaImpresion, Proveedor.Nombre + "\\" + numeroOrden);
            CreaImagenBMPentrada(rutaGuardaArchivosOrden, Fondo, Logo, Leyenda, Fondo,numeroOrden,
                            Program.EmpresaSeleccionada, Proveedor, PedidoCotizacion, ProductosSeleccionados);
            try
            {
                new UtiPDF().CreaPDF(rutaGuardaArchivosOrden + "\\" + numeroOrden + ".bmp",
                                        rutaGuardaArchivosOrden + "\\ENTRADA-" + numeroOrden + ".pdf");
            }
            catch (Exception ex) { }
            finally
            {
                string nombreArchivo = EncuentraArchivo(rutaGuardaArchivosOrden, ".pdf");
                MuestraArchivo(rutaGuardaArchivosOrden, nombreArchivo);
            }
        }

        public void ImprimirComplementoPagoResumido(string TituloImpresion, string PathGuardaPDF,
                                EntCliente Cliente, List<EntFactura> FacturasPagadas,
                                System.Drawing.Image Fondo, System.Drawing.Image Logo, System.Drawing.Image Leyenda)
        {
            string rutaGuardaArchivosOrden = PathGuardaPDF; // this.RutaImpresion + "\\" + Cliente.Nombre + "\\" + Pedido.NumOrden;
            //this.VerificaRutas(this.RutaImpresion, Cliente.Nombre + "\\" + Pedido.NumOrden);

            //List<string> lstRutas = new List<string>();
            //if (FacturasPagadas.Count > 20)
            //    lstRutas = this.CreaImagenesBMPnotaVenta(rutaGuardaArchivosOrden,
            //                        Fondo, Logo, Leyenda, Fondo,
            //                        TituloImpresion, Pedido.NumOrden,
            //                        Program.EmpresaSeleccionada, Cliente, Pedido, ProductosSeleccionados);
            //else
            //{
            //    this.CreaImagenBMPnotaVenta(rutaGuardaArchivosOrden,
            //                        Fondo, Logo, Leyenda, Fondo,
            //                        TituloImpresion, Pedido.NumOrden,
            //                        Program.EmpresaSeleccionada, Cliente, Pedido, ProductosSeleccionados);
            //    lstRutas.Add(rutaGuardaArchivosOrden + "\\" + Pedido.NumOrden + ".bmp");
            //}

            //try
            //{
            //    new UtiPDF().CreaPDF(rutaGuardaArchivosOrden + "\\" + Pedido.NumOrden + ".bmp",
            //                            rutaGuardaArchivosOrden + "\\NOTAVENTA-" + Pedido.NumOrden + ".pdf");
            //}
            //catch (Exception ex) { }
            //finally
            //{
            //    string nombreArchivo = EncuentraArchivo(rutaGuardaArchivosOrden, ".pdf");
            //    MuestraArchivo(rutaGuardaArchivosOrden, nombreArchivo);
            //}
        }

        


        public List<EntProducto> ConvierteListaPedidosEnProductos(List<EntPedido> ListaPedidos)
        {
            List<EntProducto> lst = new List<EntProducto>();
            foreach (EntPedido p in ListaPedidos)
            {
                EntProducto e = new EntProducto()
                {
                    Id = p.Id,
                    PrecioVenta=p.Total,
                    Codigo = p.Descripcion,
                    Descripcion =p.Detalle
                };
                lst.Add(e);
            }
            return lst;
        }

        public string CreaPathClienteDirectorioFacturas(string PathFacturaBase, string NombreCliente, string NumeroFactura)
        {
            string pathClienteDirectorio = PathFacturaBase + "\\" + NombreCliente;
            if (!System.IO.Directory.Exists(pathClienteDirectorio))
                System.IO.Directory.CreateDirectory(pathClienteDirectorio);

            string pathClienteDirectorioFacturas = pathClienteDirectorio + "\\"
                                                    + Program.EmpresaSeleccionada.SerieFactura + " " + NumeroFactura;
            System.IO.Directory.CreateDirectory(pathClienteDirectorioFacturas);
            return pathClienteDirectorioFacturas;
        }

        /// <summary>
        /// ASIGNA Pedido.Factura = siguienteF.NumeroFactura; Pedido.SiguienteFacturaId = siguienteF.Id; 
        /// DESDE new BusPedidos().ObtieneSiguienteFactura(EmpresaId, Pedido.Id, Serie);
        /// </summary>
        /// <param name="Pedido"></param>
        /// <param name="EmpresaId"></param>
        /// <param name="Serie"></param>
        public int AsignaSiguienteFacturaEnPedido(int SiguienteFacturaId, EntPedido Pedido,int EmpresaId, string Serie)
        {

            if (SiguienteFacturaId == 0)
            {
                EntFactura siguienteF = new BusPedidos().ObtieneSiguienteFactura(EmpresaId, Pedido.Id, Serie);
                //int siguienteFactura = ConvierteTextoAInteger(siguienteF.NumeroFactura);
                //return siguienteFactura.ToString();

                //if (Pedido.SiguienteFacturaId == 0)
                //{
                Pedido.Factura = siguienteF.NumeroFactura;
                Pedido.SiguienteFacturaId = siguienteF.Id;
                //}
                //return siguienteF;
            }
            return SiguienteFacturaId;
        }
        public void TimbraCancelacion(EntPedido PedidoSeleccionado)
        {
            AgregaMotivoCancelacion vMotCan = new AgregaMotivoCancelacion(new EntFactura() { NumeroFactura = PedidoSeleccionado.Factura, Total=PedidoSeleccionado.Total }, true);
            if (vMotCan.ShowDialog() == DialogResult.OK)
            {
                string motivoCancelacion = vMotCan.MotivoCancelacionId;
                string folioSustituye = vMotCan.FolioSustituye;

                Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(new BusClientes().ObtieneCliente(PedidoSeleccionado.ClienteId).EmpresaId);
                if (Program.ConexionIdActual == 1 && Program.EmpresaSeleccionada.Facturacion)//PRODUCCION
                {
                    UtiFacturacion factura = new UtiFacturacion();
                    //factura.Cancelar(Program.EmpresaSeleccionada, pedidoSeleccionado.UUID, "02", "");
                    factura.CancelarPADE(Program.EmpresaSeleccionada, PedidoSeleccionado.UUID,
                                        motivoCancelacion, folioSustituye, PedidoSeleccionado.ClienteRFC, PedidoSeleccionado.Total);
                }
                else if (Program.ConexionIdActual == 2 || !Program.EmpresaSeleccionada.Facturacion)
                {
                    MuestraMensaje("CANCELACIÓN PRUEBA");
                    UtiFacturacionPruebas facturaPruebas = new UtiFacturacionPruebas();
                    facturaPruebas.Cancelar(new EntEmpresa() { RFC = "MSE061107IA8" }, PedidoSeleccionado.UUID, "02", "");
                }
            }
            else
                MandaExcepcion("NO SE ELIGIÓ MOTIVO DE CANCELACIÓN");
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
