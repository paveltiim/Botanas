using AiresEntidades;
using AiresNegocio;
using AiresUtilerias;
using Microsoft.Office.Interop.Excel;
using Microsoft.Reporting.WinForms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aires.Pantallas
{
    public partial class Registros : FormBase
    {
        public void VerificaEmpresa()
        {
            cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }
        public Registros()
        {
            InitializeComponent();
        }

        List<EntPedido> ListaPedidos;
        List<EntEmpresa> ListaEmpresas;
        
        #region Metodos
        /// <summary>
        /// 
        /// 
        /// </summary>
        public void CargaGvPedidos(int EmpresaId)
            {
                ListaPedidos = new BusPedidos().ObtienePedidos(EmpresaId,1, 1, 1, 1, 1);
                gvPedidos.DataSource = ListaPedidos;
                txtCantidadVentas.Text = ListaPedidos.Count.ToString();
            }
            public void CargaGvPedidos(int EmpresaId, bool Estatus)
            {
                ListaPedidos = new BusPedidos().ObtienePedidos(EmpresaId, 1, 1, 1, Convert.ToInt16(Estatus), 1);
                gvPedidos.DataSource = ListaPedidos;
                txtCantidadVentas.Text = ListaPedidos.Count.ToString();
            }

            void ActualizaEstatusPedido(EntPedido Pedido, int EstatusId)
            {
                Pedido.EstatusId = EstatusId;
                new BusPedidos().ActualizaEstatusPedido(Pedido);
            }
            void ActualizaEstatusProductoDetallePedido(EntPedido Pedido, bool Estatus)
            {
                Pedido.Estatus = Estatus;
                new BusPedidos().ActualizaEstatusProductoDetallePedido(Pedido);
            }

            //void AgregarFacturaPedido(int PedidoId, string UUID, string Ruta)
            //{
            //    EntFactura factura = new EntFactura()
            //    {
            //        PedidoId = PedidoId,
            //        UUID = UUID,
            //        Fecha = DateTime.Today,
            //        Ruta=Ruta
            //    };
            //    new BusPedidos().AgregaFactura(factura);
            //}
            void ActualizaEstatusFacturaPedido(EntFactura Factura, int EstatusId)
            {
                Factura.EstatusId = EstatusId;
                new BusPedidos().ActualizaEstatusFacturaPedido(Factura);
            }

            //Obtiene el Cliente con el ClienteId seleccionado.
            EntCliente ObtieneCliente(int ClienteId)
            {
                return new BusClientes().ObtieneCliente(ClienteId);
            }

            void CargaDatosCliente(EntCliente Cliente)
            {
                txtEmail.Text = Cliente.Email;

                txtNombreFiscal.Text = Cliente.NombreFiscal;
                txtRFC.Text = Cliente.RFC;

                txtNoExterior.Text = Cliente.NoExterior;
                txtNoInterior.Text = Cliente.NoInterior;
                txtCalle.Text = Cliente.Calle;
                txtColonia.Text = Cliente.Colonia;
                txtLocalidad.Text = Cliente.Localidad;
                txtMunicipio.Text = Cliente.Municipio;
                txtEstado.Text = Cliente.Estado;
                txtCP.Text = Cliente.CP;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="pedido"></param>

            string PathClienteDirectorioFacturas;
        //EntFactura EnviarFactura(EntEmpresa Empresa, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente, DateTime FechaFactura,
        //                        string FormaPago, string MedioPago, string CondicionPago, string NumeroCuenta,
        //                        decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS)
        //{
        //    if (Cliente == null)
        //        Cliente = new EntCliente();

        //    Cliente.NoExterior = txtNoExterior.Text;
        //    Cliente.NoInterior = txtNoInterior.Text;
        //    Cliente.Calle = txtCalle.Text;
        //    Cliente.Colonia = txtColonia.Text;
        //    Cliente.Localidad = txtLocalidad.Text;
        //    Cliente.Municipio = txtMunicipio.Text;
        //    Cliente.Estado = txtEstado.Text;
        //    Cliente.CP = txtCP.Text;

        //    Cliente.Email = txtEmail.Text;

        //    string pathClienteDirectorio = PathFacturas + "\\" + Cliente.Nombre;
        //    if (!System.IO.Directory.Exists(pathClienteDirectorio))
        //        System.IO.Directory.CreateDirectory(pathClienteDirectorio);

        //    string pathClienteDirectorioFacturas = pathClienteDirectorio + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss");
        //    System.IO.Directory.CreateDirectory(pathClienteDirectorioFacturas);

        //    //UtiFacturacionPruebas factura = new UtiFacturacionPruebas();
        //    //MessageBox.Show("FACTURACIÓN DE PRUEBA");
        //    UtiFacturacion factura = new UtiFacturacion();
            

        //    //Pedido.Factura = txtNumeroFactura.Text;
        //    string uuid = factura.Facturar(Empresa, Pedido, ListaProductos, Cliente, Pedido.Factura, FechaFactura, FormaPago, MedioPago, CondicionPago,
        //                                    NumeroCuenta, pathClienteDirectorioFacturas,
        //                                    CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS);
        //    EntFactura fact = new EntFactura() { PedidoId = Pedido.Id, NumeroFactura = Pedido.Factura, UUID = uuid, Ruta = pathClienteDirectorioFacturas, Fecha = DateTime.Today };

        //    return fact;// pathClienteDirectorioFacturas;
        //}


        /// <summary>
        /// Muestra Ventana emergente para Confirmar Envio de Correo, llama los métodos Imprime.AsignaValoresParametrosImpresionDatosCliente y Imprime.AsignaValoresParametrosImpresion.
        /// Envia correo electronico por medio de la clase UtiCorreo.
        /// </summary>
        /// <param name="Pedido"></param>
        /// <param name="Cliente"></param>
        /// <param name="NotaVenta"></param>
        /// <param name="Presupuesto"></param>
        void EnviaCorreo(EntCliente Cliente, string PathArchivosFactura)
            {
                //ConfirmaEnviaCorreo vConfirmaEnviaCorreo = new ConfirmaEnviaCorreo();
                //vConfirmaEnviaCorreo.CargaValores(Cliente);
                //if (vConfirmaEnviaCorreo.ShowDialog() == DialogResult.OK)
                //{
                Cursor.Current = Cursors.WaitCursor;

                List<string> archivosAdjuntos = new List<string>();

                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PathArchivosFactura);
                foreach (System.IO.FileInfo file in dir.GetFiles())
                {
                    archivosAdjuntos.Add(file.FullName);
                }

                EntEmpresa empresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                string asunto = "FACTURA -" + empresaSeleccionada.NombreFiscal + "- " + DateTime.Today.ToString("dd MMM");
                string mensaje = "Apreciable " + Cliente.NombreFiscal + ", \n\n Le enviamos su debido comprobante fiscal solicitado, recordandole que estamos a sus ordenes para cualquier duda o aclaración. \n";
                mensaje += "\n Agradecemos su preferencia y esperamos seguirle atendiendo como se merece. \n";
                mensaje += "\n Atte. \n" + empresaSeleccionada.NombreFiscal;
                new UtiCorreo().EnviaCorreo(asunto, new List<string>() { Cliente.Email, Cliente.Email2, Cliente.Email3 }, mensaje, archivosAdjuntos);

            MessageBox.Show("El Correo se ha Enviado correctamente, a la(s) dirección(es): \n " + Cliente.Email + " \n " + Cliente.Email2 + " \n " + Cliente.Email3);
            //}
        }

        public void ConviertePdfToImage(string RutaPDF)
            {

                //System.Drawing.Image pageImage = (Image)new PDFtoImage().ConvertPdfToImage(RutaPDF);

                //// Save converted image to jpeg format
                //pageImage.Save("Prueba1PDFtoImage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            }

            void CargaProductosEnPantallas()
            {
                Form forma = BuscaFormaBase(new Ventas().Titulo);
                if (forma != null)
                {
                    //((Ventas)forma).CargaProductos();
                    ((Ventas)forma).CargaProductosDetalle(Program.EmpresaSeleccionada.Id);
                }
            }

        #endregion

        #region Metodos Impresion
        public void CargaRvVentas(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta, int ClienteId)
        {
            List<EntPedido> ListaPedidos = new BusPedidos().ObtienePedidosClientesPorFechas(EmpresaId, FechaDesde, FechaHasta);
            if(ClienteId>0)
                entPedidoBindingSource.DataSource = ListaPedidos.Where(P=>P.ClienteId==ClienteId);
            else
                entPedidoBindingSource.DataSource = ListaPedidos;
            ReportParameter parmEmpresa;
            parmEmpresa = new ReportParameter("Empresa", Program.EmpresaSeleccionada.Nombre);

            rvVentas.LocalReport.SetParameters(parmEmpresa);
            rvVentas.RefreshReport();
        }
        public List<EntPedido> ObtieneVentas(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta)
        {
            List<EntPedido> ListaPedidos = new BusPedidos().ObtienePedidosClientesPorFechas(EmpresaId, FechaDesde, FechaHasta);
            return ListaPedidos;
        }
        #endregion

        int DiasPorSemana = 6;
        public void CargaEmpresas()
        {
            if (Program.UsuarioSeleccionado.Id > 1)
                ListaEmpresas = new BusEmpresas().ObtieneEmpresas().Where(P => P.UsuarioId == Program.UsuarioSeleccionado.Id).ToList();
            else
                ListaEmpresas = new BusEmpresas().ObtieneEmpresas();

            Program.CambiaEmpresa = false;
            cmbEmpresas.DataSource = ListaEmpresas;
            Program.CambiaEmpresa = true;

            //CargaClientesEnPantallas();
        }

        void CargaDatosClientePubicoGeneral(EntCliente Cliente)
        {
            //txtNombre.Text = Cliente.Nombre;
            txtNombreFiscal.Text = Cliente.NombreFiscal;
            txtRFC.Text = Cliente.RFC;

            txtEmail.Text = Cliente.Email;

            txtNoExterior.Text = Cliente.NoExterior;
            txtNoInterior.Text = Cliente.NoInterior;
            txtCalle.Text = Cliente.Calle;
            txtColonia.Text = Cliente.Colonia;
            txtLocalidad.Text = Cliente.Localidad;
            txtMunicipio.Text = Cliente.Municipio;
            txtEstado.Text = Cliente.Estado;
            txtCP.Text = Cliente.CP;
        }
        void FiltrarClientes(List<EntPedido> ListaPedidos, string NombreCliente)
        {
            //List<EntCliente> clientes = (List<EntCliente>)gvClientes.DataSource;

            var pedidosFiltro = from c in ListaPedidos
                                where c.Cliente.ToUpper().Contains(NombreCliente.ToUpper())
                                select c;

            gvPedidos.DataSource = null;
            gvPedidos.DataSource = pedidosFiltro.ToList();
        }
        void FiltrarClientes(List<EntPedido> ListaPedidos, string NumeroCliente, string NombreCliente)
        {
            //List<EntCliente> clientes = (List<EntCliente>)gvClientes.DataSource;

            var pedidosFiltro = from c in ListaPedidos
                                where c.Cliente.ToUpper().Contains(NombreCliente.ToUpper())
                                select c;

            if (!string.IsNullOrWhiteSpace(txtNumClienteFiltro.Text))
            {
                pedidosFiltro = from c in pedidosFiltro
                                where c.NumCliente.ToUpper().Contains(NumeroCliente.ToUpper())
                                select c;
            }

            gvPedidos.DataSource = null;
            gvPedidos.DataSource = pedidosFiltro.ToList();
        }
        void FiltrarClientes(List<EntPedido> ListaPedidos, string NumeroCliente, string NombreCliente, string Descripcion, string NumeroFactura)
        {
            //List<EntCliente> clientes = (List<EntCliente>)gvClientes.DataSource;

            var pedidosFiltro = from c in ListaPedidos
                                where c.Cliente.ToUpper().Contains(NombreCliente.ToUpper())
                                select c;

            if (!string.IsNullOrWhiteSpace(NumeroCliente))
            {
                pedidosFiltro = from c in pedidosFiltro
                                where c.NumCliente.ToUpper().Contains(NumeroCliente.ToUpper())
                                select c;
            }

            if (!string.IsNullOrWhiteSpace(Descripcion))
            {
                pedidosFiltro = from c in pedidosFiltro
                                where c.Detalle.ToUpper().Contains(Descripcion.ToUpper())
                                select c;
            }

            if (!string.IsNullOrWhiteSpace(NumeroFactura))
            {
                pedidosFiltro = from c in pedidosFiltro
                                where c.Factura.ToUpper().Contains(NumeroFactura.ToUpper())
                                select c;
            }

            gvPedidos.DataSource = null;
            gvPedidos.DataSource = pedidosFiltro.ToList();
        }
        void InicializaPantalla()
        {
            if(Program.UsuarioSeleccionado.Id>1)
            {
                tpReportes.Text="";
                pnlReportesVentas.Visible = false;
            }
            //if(Program.EmpresaSeleccionada!=null)
            //    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
            dtpFechaDesdeVentas.Value = ObtieneLunesEstaSemana(DateTime.Today);
            dtpFechaHastaVentas.Value = dtpFechaDesdeVentas.Value.AddDays(DiasPorSemana);
            cmbUsoCFDI.SelectedIndex = 0;
        }

        void EnviaCorreoArchivo(string Email, DateTime Fecha, string PathArchivo, string Empresa)
        {
            Cursor.Current = Cursors.WaitCursor;

            List<string> archivosAdjuntos = new List<string>();

            System.IO.FileInfo file = new System.IO.FileInfo(PathArchivo);
            archivosAdjuntos.Add(file.FullName);

            EntEmpresa empresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
            string asunto = "VENTAS DE PRODUCTOS -" + Fecha.ToString("dd MMM yyyy")+"-"+Empresa;
            string mensaje = "IMPORTAR ARCHIVO AL SISTEMA 'SERDAN SOFTWARE'. \n\n Abrir sistema 'SERDAN SOFTWARE'-->Ir a 'Sincronización' en menu-->En Pestaña 'Importar Ventas'-->Seleccionar archivo descargado desde correo (archivo Excel adjunto). Se mostrarán las Ventas a Importar-->Presionar botón Importar-->";
            new AiresUtilerias.UtiCorreo().EnviaCorreo("" + asunto, new List<string>() { Email }, mensaje, archivosAdjuntos);

            //MessageBox.Show("El Correo se ha Enviado correctamente, a la dirección -" + Email + "-");
            //}
        }
        public void ExportarVentas(int EmpresaId, DateTime Fecha, string Email)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                if (xlApp == null)
                    MandaExcepcion("Excel NO esta instalado apropiadamente!!");

                this.Cursor = Cursors.WaitCursor;

                CargaEmpresas();
                EntEmpresa empresaSeleccionada = ListaEmpresas.Where(P => P.Id == EmpresaId).ToList()[0];

                List<EntPedido> pedidosVentas = ObtieneVentas(EmpresaId, Fecha, Fecha);
                //SeleccionaEmail vEmail = new Pantallas.SeleccionaEmail();
                //if (vEmail.ShowDialog() == DialogResult.OK)
                //{

                if (pedidosVentas.Count > 0)
                {
                    Workbook xlWorkBook;
                    Worksheet xlWorkSheet;

                    object misValue = System.Reflection.Missing.Value;
                    xlWorkBook = xlApp.Workbooks.Add(misValue);
                    xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);


                    try
                    {
                        //agregaPedido
                        //int ClienteId, string Detalle, string Observaciones, decimal Total, decimal Pago, 
                        //DateTime Fecha, DateTime FechaEntrega, int EmpleadoId, bool Facturado, int EstatusId

                        //AgregarProductoDetallePedido
                        //(int PedidoId, int ProductoId, int Cantidad, decimal PrecioCosto, decimal PrecioVenta)

                        //if (p.TipoProductoId == 1)
                        //{
                        //    vProd.ActualizaEstatusProductoDetalle(p, 2);//ESTATUS:2=ENTREGADO
                        //    vProd.AumentaProducto(p.ProductoId, -p.Cantidad);
                        //}

                        // AumentaPagoPedido
                        //(pedidoAgrega.Id, -pago);

                        //agregaFactura
                        //(Factura.EmpresaId, Factura.PedidoId, Factura.NumeroFactura, Factura.UUID, Factura.Fecha, Factura.Ruta);

                        //--------------REN|COL----------//
                        xlWorkSheet.Cells[1, 1] = "CLIENTEID";
                        xlWorkSheet.Cells[1, 2] = "CLIENTE";
                        xlWorkSheet.Cells[1, 3] = "PEDIDOID";
                        xlWorkSheet.Cells[1, 4] = "PEDIDODETALLE";
                        xlWorkSheet.Cells[1, 5] = "TOTAL";
                        xlWorkSheet.Cells[1, 6] = "PAGO";
                        xlWorkSheet.Cells[1, 7] = "FECHA";
                        xlWorkSheet.Cells[1, 8] = "FACTURADO";
                        xlWorkSheet.Cells[1, 9] = "PEDIDOESTATUSID";

                        xlWorkSheet.Cells[1, 10] = "PRODUCTODETALLEID";
                        xlWorkSheet.Cells[1, 11] = "CANTIDAD";
                        xlWorkSheet.Cells[1, 12] = "PRECIOCOSTO";
                        xlWorkSheet.Cells[1, 13] = "PRECIOVENTA";
                    
                        xlWorkSheet.Cells[1, 14] = "TIPOPRODUCTOID";
                        xlWorkSheet.Cells[1, 15] = "PRODUCTOID";

                        xlWorkSheet.Cells[1, 16] = "NUMEROFACTURA";
                        xlWorkSheet.Cells[1, 17] = "UUID";
                        xlWorkSheet.Cells[1, 18] = "FECHAFACTURA";
                        xlWorkSheet.Cells[1, 19] = "RUTAFACTURA";
                        //hacer rutina para obtener los archivos de facturas

                        int ren = 2;
                        foreach (EntPedido pe in pedidosVentas)
                        {
                            List<EntProducto> listaProductos = new BusProductos().ObtieneProductosDetallePorPedido(pe.Id);

                            //p.Id = Convert.ToInt32(r["PED_ID"]);
                            //p.Detalle = r["PED_DETALLE"].ToString();
                            //p.ClienteId = Convert.ToInt32(r["PED_CLIENTEID"]);
                            //p.Cliente = r["CLI_NOMBRE"].ToString();
                            //p.Pago = Convert.ToDecimal(r["PAG_PAGO"]);
                            //p.Total = Convert.ToDecimal(r["PED_TOTAL"]);
                            //p.FechaCorta = Convert.ToDateTime(r["PED_FECHA"]).ToShortDateString();
                            //p.Fecha = Convert.ToDateTime(r["PED_FECHA"]);
                            //p.EstatusId = Convert.ToInt32(r["PED_ESTATUSID"]);
                            //p.EstatusDescripcion = r["ESTPED_DESCRIPCION"].ToString();
                            //p.Facturado = Convert.ToBoolean(r["FAC_ID"]);
                            //p.Factura = "AA" + r["FAC_NUMEROFACTURA"].ToString();
                            //p.Factura = "S/F";
                            //p.UUID = r["FAC_UUID"].ToString();
                            //p.RutaFactura = r["FAC_RUTA"].ToString();
                            //p.FechaEntrega = Convert.ToDateTime(r["FAC_FECHA"])

                            //p.Id = Convert.ToInt32(r["PRODET_ID"]);
                            //p.ProductoId = Convert.ToInt32(r["PRO_ID"]);
                            //p.TipoProductoId = Convert.ToInt32(r["PRO_TIPOPRODUCTOID"]);
                            //p.Codigo = r["PRO_CODIGO"].ToString();
                            //p.Descripcion = r["PRO_DESCRIPCION"].ToString();
                            //p.Cantidad = Convert.ToInt32(r["PROPED_CANTIDAD"]);
                            //p.PrecioCosto = Convert.ToDecimal(r["PROPED_PRECIOCOSTO"]);
                            //p.PrecioVenta = Convert.ToDecimal(r["PROPED_PRECIOVENTA"]);
                            //p.Serie = r["PRODET_SERIE"].ToString();

                            foreach (EntProducto p in listaProductos)
                            {
                                xlWorkSheet.Cells[ren, 1] = pe.ClienteId;           // "CLIENTEID";
                                xlWorkSheet.Cells[ren, 2] = pe.Cliente;             // "CLIENTE";
                                xlWorkSheet.Cells[ren, 3] = pe.Id;             // "PEDIDOID";
                                xlWorkSheet.Cells[ren, 4] = pe.Detalle;             // "PEDIDODETALLE";
                                xlWorkSheet.Cells[ren, 5] = pe.Total;               // "TOTAL";
                                xlWorkSheet.Cells[ren, 6] = pe.Pago;                //"PAGO";
                                xlWorkSheet.Cells[ren, 7] = pe.Fecha;               //"FECHA";
                                xlWorkSheet.Cells[ren, 8] = pe.Facturado;           // "FACTURADO";
                                xlWorkSheet.Cells[ren, 9] = pe.EstatusId;           // "PEDIDOESTATUSID";

                                xlWorkSheet.Cells[ren, 10] = p.Id;                   // "PRODUCTODETALLEID";
                                xlWorkSheet.Cells[ren, 11] = p.Cantidad;            // "CANTIDAD";
                                xlWorkSheet.Cells[ren, 12] = p.PrecioCosto;         // "PRECIOCOSTO";
                                xlWorkSheet.Cells[ren, 13] = p.PrecioVenta;         // "PRECIOVENTA";
                            
                                xlWorkSheet.Cells[ren, 14] = p.TipoProductoId;      // "TIPOPRODUCTOID";
                                xlWorkSheet.Cells[ren, 15] = p.ProductoId;          // "PRODUCTOID";

                                xlWorkSheet.Cells[ren, 16] = pe.Factura;            // "NUMEROFACTURA";
                                xlWorkSheet.Cells[ren, 17] = pe.UUID;               // "UUID";
                                xlWorkSheet.Cells[ren, 18] = pe.FechaEntrega;       // "FECHAFACTURA";
                                xlWorkSheet.Cells[ren, 19] = pe.RutaFactura;        // "RUTAFACTURA";
                                ren++;
                            }
                        }

                        //EntCatalogoGenerico ingreso = ObtieneCatalogoGenericoFromGV(gvIngresos);
                        string rutaExportacion = string.Format(@"c:\TIIM\EXPORTACIONES\Ventas {0:yyyy-MM-dd} {1}.xls", Fecha, empresaSeleccionada.Nombre);

                        xlWorkBook.SaveAs(rutaExportacion, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        xlWorkBook.Close(true, misValue, misValue);
                        xlApp.Quit();

                        Marshal.ReleaseComObject(xlWorkSheet);
                        Marshal.ReleaseComObject(xlWorkBook);
                        Marshal.ReleaseComObject(xlApp);

                        EnviaCorreoArchivo(Email, Fecha, rutaExportacion, empresaSeleccionada.Nombre);
                    }
                    catch (Exception ex)
                    {
                        xlWorkBook.Close(true, misValue, misValue);
                        xlApp.Quit();

                        Marshal.ReleaseComObject(xlWorkSheet);
                        Marshal.ReleaseComObject(xlWorkBook);
                        Marshal.ReleaseComObject(xlApp);
                        MandaExcepcion(ex.Message);
                    }
                }
                //MessageBox.Show("Excel file created , you can find the file d:\\csharp-Excel.xls");
                //}
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex) { MuestraExcepcion(ex, "ERROR EN LA EXPORTACIÓN"); }
        }

        public void CargaCatalogoUsoCFDI()
        {
            //ListaEmpresas = new BusEmpresas().ObtieneCatalogoRegimen();
            cmbUsoCFDI.DataSource = new BusEmpresas().ObtieneCatalogoUsoCFDI();
        }

        public void CargaClientes(int EmpresaId)
        {
            List<EntCliente > lst = new BusClientes().ObtieneClientes(EmpresaId).OrderBy(P => P.Nombre).ToList();
            //List<EntCliente> lst = new BusClientes().ObtieneClientes(EmpresaId);
            lst.Insert(0, new EntCliente() { Id = -1, Nombre = "-TODOS-" });
            cmbClientes.DataSource = lst;

        }
        private void Reportes_Load(object sender, EventArgs e)
        {
            try
            {
                CargaCatalogoUsoCFDI();
                InicializaPantalla();
                CargaEmpresas();

                if (Program.EmpresaSeleccionada == null)
                    Program.EmpresaSeleccionada = SeleccionaEmpresa();

                if (Program.EmpresaSeleccionada !=null)
                {
                    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);

                    CargaClientes(Program.EmpresaSeleccionada.Id);
                    CargaGvPedidos(Program.EmpresaSeleccionada.Id);

                    gvPedidos.ClearSelection();

                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        
        private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Program.CambiaEmpresa)
                {
                    Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                    //CargaGvPedidos(Program.EmpresaSeleccionada.Id);
                    btnRefrescar.PerformClick();
                    btnRefrescarReporteVentas.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnBuscaEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                SeleccionaEmpresa vSeleccionaEmp = new Pantallas.SeleccionaEmpresa(ListaEmpresas);
                if (vSeleccionaEmp.ShowDialog() == DialogResult.OK)
                {
                    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == vSeleccionaEmp.EmpresaSeleccionada.Id);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        string ObtieneUltimaFactura(int EmpresaId)
        {
            int ultimaFactura = ConvierteTextoAInteger(new BusFacturas().ObtieneUltimaFactura(EmpresaId).NumeroFactura);
            ultimaFactura++;

            return ultimaFactura.ToString();
        }
        void TomaDatosCliente(EntCliente Cliente)
        {
            if (Cliente == null)
                Cliente = new EntCliente();
            //Cliente.Nombre = txtNombre.Text;
            Cliente.NombreFiscal = txtNombreFiscal.Text;
            Cliente.RFC = txtRFC.Text;

            Cliente.NoExterior = txtNoExterior.Text;
            Cliente.NoInterior = txtNoInterior.Text;
            Cliente.Calle = txtCalle.Text;
            Cliente.Colonia = txtColonia.Text;
            Cliente.Localidad = txtLocalidad.Text;
            Cliente.Municipio = txtMunicipio.Text;
            Cliente.Estado = txtEstado.Text;
            Cliente.CP = txtCP.Text;

            Cliente.Email = txtEmail.Text;
            Cliente.Email2 = txtEmail2.Text;
            Cliente.Email3 = txtEmail3.Text;
        }
        EntFactura EnviarFactura(EntEmpresa Empresa, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente, DateTime FechaFactura,
                                    string FormaPago, string MedioPago, string CondicionPago, string NumeroCuenta,
                                    decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS,
                                    string Observaciones,
                                   string TipoComprobante, string UsoCFDI)
        {
            string pathClienteDirectorio = PathFacturas + "\\" + Cliente.Nombre;
            if (!System.IO.Directory.Exists(pathClienteDirectorio))
                System.IO.Directory.CreateDirectory(pathClienteDirectorio);

            string carpetaFecha = DateTime.Now.ToString("yyyyMMddhhmmss");
            string pathClienteDirectorioFacturas = pathClienteDirectorio + "\\" + carpetaFecha;
            System.IO.Directory.CreateDirectory(pathClienteDirectorioFacturas);

            //List<EntProducto> productosDetalle = ListaProductos;
            ////ListaProductos = new BusProductos().ObtieneProductosPorPedido(Pedido.Id);

            ////foreach (EntProducto p in ListaProductos)
            ////{
            ////    p.Descripcion = p.Descripcion.PadRight(100, '.');
            ////    p.Descripcion += " ";

            ////    foreach (EntProducto pd in productosDetalle.Where(P => P.ProductoId == p.Id))
            ////    {
            ////        if (!string.IsNullOrWhiteSpace(pd.Serie))
            ////            p.Descripcion += pd.Serie +" | ";
            ////    }
            ////}
            //List<EntProducto> ListaProductosFactura = new List<EntProducto>();
            //string codigo = "";
            //int cantidad = 1;
            //foreach (EntProducto p in productosDetalle.OrderBy(P => P.Codigo).ToList())
            //{
            //    if (p.Codigo != codigo)
            //    {
            //        EntProducto pneue = new EntProducto()
            //        {
            //            Id = p.Id,
            //            Codigo = p.Codigo,
            //            Serie = p.Serie,
            //            Descripcion = p.Descripcion
            //                                                ,
            //            TipoUnidad = p.TipoUnidad,
            //            Cantidad = p.Cantidad,
            //            PrecioVenta = p.PrecioVenta
            //                                                ,
            //            PrecioVentaSinIVA = p.PrecioVentaSinIVA,
            //            ProductoId = p.ProductoId
            //        };
            //        //pneue.Descripcion = p.Descripcion.PadRight(100, '°');
            //        pneue.Descripcion += " ";

            //        ListaProductosFactura.Add(pneue);
            //        codigo = pneue.Codigo;
            //        cantidad = 1;
            //    }
            //    else
            //    {
            //        cantidad++;
            //        ListaProductosFactura[ListaProductosFactura.Count - 1].Cantidad++;
            //    }
            //    if (!string.IsNullOrWhiteSpace(p.Serie))
            //        ListaProductosFactura[ListaProductosFactura.Count - 1].Descripcion += p.Serie + " | ";
            //}
            //if (Program.UsuarioSeleccionado.Id == 8 || Program.UsuarioSeleccionado.Id == 9)
            //    ListaProductosFactura[ListaProductosFactura.Count - 1].Descripcion += "Solicitud:".PadLeft(60, '-') + txtBanco.Text;

            //FacturacionPrueba factura = new FacturacionPrueba();
            //MessageBox.Show("FACTURACIÓN DE PRUEBA");
            UtiFacturacion factura = new UtiFacturacion();

            int tipoTasaIVAid = Empresa.TipoTasaIVAId;
            decimal tasaOCuota = Empresa.TasaOCuota;

            if ((txtRFC.Text == "XAXX010101000" || chkFacturaPublicoGeneral.Checked) && Program.EmpresaSeleccionada.TipoPersonaId == 1)//Persona Fisica
            {
                Empresa.TipoTasaIVAId = 2;//TASA 0%
                Empresa.TasaOCuota = 0.0m;//TASA 0%
            }
            else
            {
                Empresa.TipoTasaIVAId = 1;//TASA 16%
                Empresa.TasaOCuota = 0.16m;//TASA 0%
            }

            string uuid = factura.Facturar33(Empresa, Pedido, ListaProductos, Cliente, Pedido.Factura,"AA", FechaFactura,
                                           TipoComprobante, UsoCFDI, FormaPago, MedioPago, CondicionPago,
                                           NumeroCuenta, pathClienteDirectorioFacturas,
                                           CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, Observaciones);

            Empresa.TipoTasaIVAId = tipoTasaIVAid;
            Empresa.TasaOCuota = tasaOCuota;

            EntFactura fact = new EntFactura() { PedidoId = Pedido.Id, NumeroFactura = Pedido.Factura, UUID = uuid, Ruta = pathClienteDirectorioFacturas, Fecha = DateTime.Today };

            return fact;// pathClienteDirectorioFacturas;
        }
        EntFactura EnviarFacturaPrueba(EntEmpresa Empresa, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente, DateTime FechaFactura,
                                   string FormaPago, string MedioPago, string CondicionPago, string NumeroCuenta,
                                   decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS,
                                   string Observaciones,
                                   string TipoComprobante, string UsoCFDI)
        {
            string pathClienteDirectorio = PathFacturas + "\\" + Cliente.Nombre;
            if (!System.IO.Directory.Exists(pathClienteDirectorio))
                System.IO.Directory.CreateDirectory(pathClienteDirectorio);

            string pathClienteDirectorioFacturas = pathClienteDirectorio + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss");
            System.IO.Directory.CreateDirectory(pathClienteDirectorioFacturas);

            //List<EntProducto> productosDetalle = ListaProductos;
            //ListaProductos = new BusProductos().ObtieneProductosPorPedido(Pedido.Id);

            //foreach (EntProducto p in ListaProductos)
            //{
            //    p.Descripcion = p.Descripcion.PadRight(100, '.');
            //    p.Descripcion += " ";

            //    foreach (EntProducto pd in productosDetalle.Where(P => P.ProductoId == p.Id))
            //    {
            //        if (!string.IsNullOrWhiteSpace(pd.Serie))
            //            p.Descripcion += pd.Serie + " | ";
            //    }
            //}
            ////if (Program.UsuarioSeleccionado.Id > 1)
            ////    ListaProductosFactura[ListaProductosFactura.Count - 1].Descripcion += "Solicitud:".PadLeft(100, '-') + txtBanco.Text;

            UtiFacturacionPruebas factura = new UtiFacturacionPruebas();
            MessageBox.Show("FACTURACIÓN DE PRUEBA");
            //UtiFacturacion factura = new UtiFacturacion();

            int tipoTasaIVAid = Empresa.TipoTasaIVAId;
            decimal tasaOCuota = Empresa.TasaOCuota;

            if ((txtRFC.Text == "XAXX010101000" || chkFacturaPublicoGeneral.Checked) && Program.EmpresaSeleccionada.TipoPersonaId == 1)//Persona Fisica
            {
                Empresa.TipoTasaIVAId = 2;//TASA 0%
                Empresa.TasaOCuota = 0.0m;//TASA 0%
            }
            else
            {
                Empresa.TipoTasaIVAId = 1;//TASA 16%
                Empresa.TasaOCuota = 0.16m;//TASA 0%
            }

            string uuid = factura.Facturar33(Empresa, Pedido, ListaProductos, Cliente, Pedido.Factura, FechaFactura,
                                           TipoComprobante, UsoCFDI, FormaPago, MedioPago, CondicionPago,
                                           NumeroCuenta, pathClienteDirectorioFacturas,
                                           CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, Observaciones);
            Empresa.TipoTasaIVAId = tipoTasaIVAid;
            Empresa.TasaOCuota = tasaOCuota;

            EntFactura fact = new EntFactura() { PedidoId = Pedido.Id, NumeroFactura = Pedido.Factura, UUID = uuid, Ruta = pathClienteDirectorioFacturas, Fecha = DateTime.Today };

            return fact;// pathClienteDirectorioFacturas;
        }
        void AgregarFacturaPedido(EntFactura Factura)
        {
            new BusFacturas().AgregaFactura(Factura);
        }
        void EnviaCorreo(EntEmpresa Empresa, EntPedido Pedido, EntCliente Cliente, string PathArchivosFactura)
        {
            Cursor.Current = Cursors.WaitCursor;

            List<string> archivosAdjuntos = new List<string>();

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PathArchivosFactura);
            foreach (System.IO.FileInfo file in dir.GetFiles())
            {
                archivosAdjuntos.Add(file.FullName);
            }

            string asunto = "FACTURA -" + Empresa.NombreFiscal + "- " + DateTime.Today.ToString("dd MMM");
            string mensaje = "Apreciable " + Cliente.NombreFiscal + ", \n\n Le enviamos su debido comprobante fiscal solicitado, recordandole que estamos a sus ordenes para cualquier duda o aclaración. \n";
            mensaje += "\n Agradecemos su preferencia y esperamos seguirle atendiendo como se merece. \n";
            mensaje += "\n Atte. \n" + Empresa.NombreFiscal;

            //if (Program.UsuarioSeleccionado.Id > 1)
            //{
                string email4 = "refrigeracion_serdan@hotmail.com";
                if (Program.UsuarioSeleccionado.Id == 5)
                {//CASAR
                    string email5 = "martin_serdan@hotmail.com";
                    string email6 = "comercializadoracasar@hotmail.com";
                    new UtiCorreo().EnviaCorreo(asunto, new List<string>() { Cliente.Email, Cliente.Email2, Cliente.Email3, email4, email5, email6 }, mensaje, archivosAdjuntos);
                }
                //else
                    new UtiCorreo().EnviaCorreo(asunto, new List<string>() { Cliente.Email, Cliente.Email2, Cliente.Email3, email4 }, mensaje, archivosAdjuntos);
            //}
            //else
            //    new UtiCorreo().EnviaCorreo(asunto, new List<string>() { Cliente.Email, Cliente.Email2, Cliente.Email3 }, mensaje, archivosAdjuntos);

            MessageBox.Show("El Correo se ha Enviado correctamente, a la(s) dirección(es): \n " + Cliente.Email + " \n " + Cliente.Email2 + " \n " + Cliente.Email3);
            //}
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            try
            {
                EntEmpresa empresaSeleccionada = Program.EmpresaSeleccionada;

                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos); 
                
                if (pedidoSeleccionado.Facturado)
                    throw new Exception("El Pedido ya fue facturado.");

                if (MuestraMensajeYesNo(string.Format("¿Desea FACTURAR el pedido seleccionado? \n Cliente:{0}",pedidoSeleccionado.Cliente), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    //List<EntProducto> productosPorPedido = new BusProductos().ObtieneProductosDetallePorPedido(pedidoSeleccionado.Id);
                    List<EntProducto> productosPorPedido = new BusProductos().ObtieneProductosPorPedido(pedidoSeleccionado.Id);

                    //YA TIENE EL DETALLE
                    //foreach (EntProducto p in productosSeleccionados)
                    //    detallePedido += p.Cantidad + " " + p.Descripcion + " | ";

                    foreach (EntProducto p in productosPorPedido)
                    {
                        //p.Descripcion=p.Descripcion.PadRight(100, '.');
                        p.Descripcion += " ";

                        foreach (EntProducto pd in new BusProductos().ObtieneProductosDetallePorPedido(pedidoSeleccionado.Id).Where(P => P.ProductoId == p.Id))
                        {
                            if (!string.IsNullOrWhiteSpace(pd.Serie))
                                p.Descripcion += pd.Serie + " - ";
                        }
                    }
                    if (Program.UsuarioSeleccionado.Id == 8 || Program.UsuarioSeleccionado.Id == 9)
                        productosPorPedido[productosPorPedido.Count - 1].Descripcion += "Solicitud:".PadLeft(60, '-') + txtBanco.Text;

                    EntCliente cliente = ObtieneCliente(pedidoSeleccionado.ClienteId);
                    TomaDatosCliente(cliente);

                    bool facturado = false;
                    //string uuid = "";
                    EntFactura factura = new EntFactura();

                    try
                    {
                        //int ultimaFactura = new BusPedidos().ObtieneUltimaFactura().Id;
                        //ultimaFactura++;
                        //pedidoSeleccionado.Factura = ultimaFactura.ToString();

                        pedidoSeleccionado.Factura = ObtieneUltimaFactura(Program.EmpresaSeleccionada.Id);

                        decimal cantidadIva;
                        if ((cliente.RFC == "XAXX010101000" || chkFacturaPublicoGeneral.Checked) && Program.EmpresaSeleccionada.TipoPersonaId == 1)//Persona Fisica
                        {
                            pedidoSeleccionado.SubTotal = pedidoSeleccionado.Total;
                            cantidadIva = 0;
                        }
                        else
                        {
                            pedidoSeleccionado.SubTotal = Math.Round(pedidoSeleccionado.Total, 2) / (1 + IVA); //Math.Round(total / (1 + IVA), 2);
                            //SOLO VERIFICACION QUE EL CALCULO ES EL MISMO
                            //cantidadIva = pedidoSeleccionado.SubTotal * IVA;
                            cantidadIva = Math.Round(pedidoSeleccionado.Total, 2) - pedidoSeleccionado.SubTotal;
                        }
                        
                        txtFormaPago.Text = cmbFormaPago.Text.Remove(2, cmbFormaPago.Text.Length - 2);
                        txtMetodoPago.Text = cmbMetodoPago.Text.Remove(3);
                        txtUsoCFDI.Text = cmbUsoCFDI.Text.Remove(3);

                        //TomaDatosCliente(cliente);

                        //pruebas
                        //empresaSeleccionada.Facturacion = false;
                        if (empresaSeleccionada.Facturacion)
                        {
                            factura = EnviarFactura(empresaSeleccionada, pedidoSeleccionado, productosPorPedido, cliente, DateTime.Now, txtFormaPago.Text, txtMetodoPago.Text, txtCondicionesPago.Text,
                                                txtNumeroCuenta.Text,
                                                cantidadIva, 0, 0, 0, txtObservaciones.Text,
                                                "I", txtUsoCFDI.Text);

                            try
                            {
                                //DESCUENTA TIMBRE
                                new BusEmpresas().AumentaTimbreEmpresa(empresaSeleccionada.Id);
                                Program.EmpresaSeleccionada.TimbresRestantes--;
                                //Program.EmpresaSeleccionada.Timbres--;
                                empresaSeleccionada.TimbresRestantes--;
                                //empresaSeleccionada.Timbres--;
                            }
                             catch (Exception ex) { }
                        }
                        else
                            factura = EnviarFacturaPrueba(empresaSeleccionada, pedidoSeleccionado, productosPorPedido, cliente, DateTime.Now, 
                                                txtFormaPago.Text, txtMetodoPago.Text, txtCondicionesPago.Text,
                                                txtNumeroCuenta.Text,
                                                cantidadIva, 0, 0, 0, txtObservaciones.Text, 
                                                "I", txtUsoCFDI.Text);

                        //uuid = EnviarFactura(pedidoSeleccionado, productosPorPedido, cliente, DateTime.Now, txtFormaPago.Text, txtMetodoPago.Text, txtCondicionesPago.Text, txtNumeroCuenta.Text);
                        factura.EmpresaId = empresaSeleccionada.Id;
                        factura.TipoComprobanteId = 1;//I-INGRESO.
                        factura.FormaPagoId = ConvierteTextoAInteger(txtFormaPago.Text);
                        factura.MetodoPagoId = cmbMetodoPago.SelectedIndex + 1;
                        factura.UsoCFDIId = ObtieneCatalogoGenericoFromCmb(cmbUsoCFDI).Id;

                        //if(empresaSeleccionada.Id==5)//CASAR | TODAVIA NO APLICA
                        factura.SerieFactura = empresaSeleccionada.SerieFactura;

                        facturado = true;
                    }
                    catch (Exception ex)
                    {
                        MuestraExcepcion(ex, "Pedido NO Facturado");
                    }
                    //QUITAR EN PRODUCCION
                    //facturado = true;
                    Cursor.Current = Cursors.Default;
                    if (facturado)
                    {
                        MuestraMensaje("¡El pedido fue FACTURADO satisfactoriamente!", "CONFIRMACIÓN PEDIDO FACTURADO");
                        //uuid = "A620B7D2-028B-4749-ACC9-52CB772CC4C2";
                        //AgregarFacturaPedido(pedidoSeleccionado.Id, uuid, PathClienteDirectorioFacturas);
                        AgregarFacturaPedido(factura);
                        try
                        {
                            Cursor.Current = Cursors.WaitCursor;

                            ////throw new Exception("error");
                            ////pahtArchivosFactura = PathClienteDirectorioFacturas;
                            ////pahtArchivosFactura = @"C:\TIIM\Facturacion\Facturas\RAFAEL GIL ARMENTA\20170105122126";
                            //EnviaCorreo(cliente, PathClienteDirectorioFacturas);
                            EnviaCorreo(empresaSeleccionada, pedidoSeleccionado, cliente, factura.Ruta);
                        }
                        catch (Exception ex)
                        {
                            MuestraExcepcion(ex, "Correo NO enviado.");
                        }

                        btnFacturar.Visible = false;
                        pnlFacturacion.Visible = false;
                        CargaGvPedidos(Program.EmpresaSeleccionada.Id);

                        Cursor.Current = Cursors.Default;

                        try
                        {
                            if (MuestraMensajeYesNo("¿Desea mostrar Factura?") == DialogResult.Yes)
                            {
                                string nombreArchivo = EncuentraArchivo(factura.Ruta, ".pdf");
                                MuestraArchivo(factura.Ruta, nombreArchivo);
                            }
                        }
                        catch (Exception ex) { MuestraExcepcion(ex, "ERROR AL MOSTRAR FACTURA"); }

                    }

                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnEnviaCorreo_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);

                if (!pedidoSeleccionado.Facturado)
                    throw new Exception("El Pedido NO ha sido facturado.");

                EntCliente cliente = ObtieneCliente(pedidoSeleccionado.ClienteId);
                if (MuestraMensajeYesNo(string.Format("¿Seguro desea enviar la FACTURA al correo seleccionado? \n \n Cliente:{0} \n \n Email:  {1} \n Email2: {2} \n Email3: {3}", pedidoSeleccionado.Cliente, cliente.Email, cliente.Email2, cliente.Email3), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    //cliente.Email = txtEmail.Text;
                    try
                    {
                        //throw new Exception("error");
                        //pahtArchivosFactura = PathClienteDirectorioFacturas;
                        //pahtArchivosFactura = @"C:\TIIM\Facturacion\Facturas\RAFAEL GIL ARMENTA\20170105122126";
                        EnviaCorreo(cliente, pedidoSeleccionado.RutaFactura);
                    }
                    catch (Exception ex)
                    {
                        MuestraExcepcion(ex, "Correo NO enviado.");
                    }

                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnCancelaFactura_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (MuestraMensajeYesNo(string.Format("¿Seguro desea CANCELAR la factura seleccionada? \n UUID: {0}", pedidoSeleccionado.UUID), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    //FacturacionPrueba factura = new FacturacionPrueba();
                    //MessageBox.Show("CANCELACIÓN PRUEBA");
                    UtiFacturacion factura = new UtiFacturacion();
                    UtiFacturacionPruebas facturaPruebas = new UtiFacturacionPruebas();


                    if (Program.EmpresaSeleccionada.Facturacion)
                        factura.Cancelar(Program.EmpresaSeleccionada, pedidoSeleccionado.UUID);
                    else
                        facturaPruebas.Cancelar(new EntEmpresa() { RFC = "XAXX010101000" }, pedidoSeleccionado.UUID);
                    //factura.Cancelar(1, pedidoSeleccionado.UUID);

                    List<EntFactura> facturasPedido=new BusPedidos().ObtieneFacturasPorPedido(pedidoSeleccionado.Id);

                    if (facturasPedido.Count > 0)
                        ActualizaEstatusFacturaPedido(facturasPedido[0], 0);

                    CargaGvPedidos(Program.EmpresaSeleccionada.Id);
                    Cursor.Current = Cursors.Default;
                    MuestraMensaje("¡Factura Cancelada!", "CANCELACIÓN DE FACTURA");
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvPedidos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedido = ObtienePedidoFromGV(gvPedidos);
                btnCancelar.Enabled = pedido.Facturado;
                btnFacturar.Visible= string.IsNullOrEmpty(pedido.UUID);
                pnlFacturacion.Visible = string.IsNullOrEmpty(pedido.UUID);

                btnFacturaExterna.Visible = string.IsNullOrEmpty(pedido.UUID);

                btnComplementoPago.Enabled = pedido.Facturado;

                //LimpiaTextBox(pnlFacturacion,true);

                LimpiaTextBox(groupBox2, true);
                CargaDatosCliente(ObtieneCliente(pedido.ClienteId));
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbFormaPago_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtFormaPago.Text = cmbFormaPago.Text;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbMetodoPago_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtMetodoPago.Text = cmbMetodoPago.Text.Remove(2, cmbMetodoPago.Text.Length - 2);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (pedidoSeleccionado.Facturado)
                    throw new Exception("No se permite ELIMINAR pedidos facturados. \n Primero es necesario CANCELAR la factura.");

                List<EntProducto> productosPedido = new BusProductos().ObtieneProductosDetallePorPedido(pedidoSeleccionado.Id);
                if (MuestraMensajeYesNo(string.Format("¿Seguro desea ELIMINAR el pedido seleccionado? \n Se devolverá a inventario los productos relacionados con el pedido", pedidoSeleccionado.UUID), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    foreach (EntProducto p in productosPedido)
                    {
                        Productos vProd = new Productos();
                        new BusProductos().ActualizaEstatusProducto(p.ProductoId, true);
                        vProd.ActualizaEstatusProductoDetalle(p, 1);//ESTATUS:1=ACTIVO
                        vProd.AumentaProducto(p.ProductoId, Convert.ToInt32(p.Cantidad));
                    }
                    ActualizaEstatusProductoDetallePedido(pedidoSeleccionado, false);//ESTATUS:0=CANCELADO
                    ActualizaEstatusPedido(pedidoSeleccionado,0);//ESTATUS:0=CANCELADO

                    CargaGvPedidos(Program.EmpresaSeleccionada.Id);
                    CargaProductosEnPantallas();
                    MuestraMensaje("¡Pedido ELIMINADO!", "CANCELACIÓN DE PEDIDO");
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                chkVerFacturasEliminadas.Checked = false;
                CargaGvPedidos(Program.EmpresaSeleccionada.Id);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnVerFactura_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado= ObtienePedidoFromGV(gvPedidos);
                if (!pedidoSeleccionado.Facturado)
                    throw new Exception("Pedido Sin Facturar");

                MuestraArchivo(pedidoSeleccionado.RutaFactura);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvPedidos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)//Descripcion 
                {
                    EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                    MuestraProductosDetalle vMuestraDetalle = new MuestraProductosDetalle(new BusProductos().ObtieneProductosDetallePorPedido(pedidoSeleccionado.Id));
                    vMuestraDetalle.Show();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        private void chkFacturaPublicoGeneral_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                EnableTextBox(gbDatosFiscales, !chkFacturaPublicoGeneral.Checked);
                LimpiaTextBox(pnlFacturacion);

                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                EntCliente clienteSeleccionado = ObtieneCliente(pedidoSeleccionado.ClienteId);
                if (chkFacturaPublicoGeneral.Checked)
                {
                    //EntCliente clientePublicoGeneral = ObtieneCliente(10);
                    //CargaDatosClientePubicoGeneral(clientePublicoGeneral);
                    EntCliente clientePublicoGeneral = new EntCliente();
                    if (clienteSeleccionado != null)
                        clientePublicoGeneral.Nombre = clienteSeleccionado.Nombre;
                    else
                        clientePublicoGeneral.Nombre = "PUBLICO GENERAL";
                    clientePublicoGeneral.NombreFiscal = "PUBLICO GENERAL";
                    clientePublicoGeneral.RFC = "XAXX010101000";
                    clientePublicoGeneral.Email = "tiimfacturacion@hotmail.com";
                    clientePublicoGeneral.FormaPagoId = 1;

                    CargaDatosCliente(clientePublicoGeneral);
                }
                else if (clienteSeleccionado != null)
                    CargaDatosCliente(clienteSeleccionado);
                else
                    CargaDatosCliente(new EntCliente());

                txtNombreFiscal.Enabled = false;
                txtRFC.Enabled = false;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        private void btnFiltrarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                FiltrarClientes(ListaPedidos, txtNumClienteFiltro.Text, txtClienteFiltro.Text, txtDescripcionFiltro.Text, txtFacturaFiltro.Text);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void chkVerFacturasEliminadas_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CargaGvPedidos(Program.EmpresaSeleccionada.Id,!chkVerFacturasEliminadas.Checked);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void gvPedidos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == System.Windows.Forms.SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == System.Windows.Forms.SortOrder.Descending)
                    {
                        gvPedidos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderBy(P => P.NumCliente).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Ascending;
                    }
                    else
                    {
                        gvPedidos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderByDescending(P => P.NumCliente).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Descending;
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == System.Windows.Forms.SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == System.Windows.Forms.SortOrder.Descending)
                    {
                        gvPedidos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderBy(P => P.Cliente).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Ascending;
                    }
                    else
                    {
                        gvPedidos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderByDescending(P => P.Cliente).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Descending;
                    }
                }
                else if (e.ColumnIndex == 2)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == System.Windows.Forms.SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == System.Windows.Forms.SortOrder.Descending)
                    {
                        gvPedidos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderBy(P => P.Detalle).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Ascending;
                    }
                    else
                    {
                        gvPedidos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderByDescending(P => P.Detalle).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Descending;
                    }
                }

                else if (e.ColumnIndex == 3)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == System.Windows.Forms.SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == System.Windows.Forms.SortOrder.Descending)
                    {
                        gvPedidos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderBy(P => P.Factura).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Ascending;
                    }
                    else
                    {
                        gvPedidos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderByDescending(P => P.Factura).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Descending;
                    }
                }
                else if (e.ColumnIndex == 5)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == System.Windows.Forms.SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == System.Windows.Forms.SortOrder.Descending)
                    {
                        gvPedidos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderBy(P => P.Fecha).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Ascending;
                    }
                    else
                    {
                        gvPedidos.DataSource = ((List<EntPedido>)((DataGridView)sender).DataSource).OrderByDescending(P => P.Fecha).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Descending;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        
        #region Eventos Pestaña Impresion
        bool CargarAños;
            /// <summary>
            /// Carga cmbAñoGastos,cmbAñosManoDeObra,cmbAñosMateriales.
            /// </summary>
            void CargaAños()
            {
                List<EntCatalogoGenerico> años = new List<EntCatalogoGenerico>();
                for (int x = DateTime.Today.Year; x >= AñoInicio; x--)
                {
                    EntCatalogoGenerico año = new EntCatalogoGenerico();
                    año.Descripcion = x.ToString();
                    años.Add(año);
                }
                cmbAñoVentas.DataSource = años;
                ////cmbAñoDepositos.DataSource = años;
                //EntCatalogoGenerico[] añosCopy = new EntCatalogoGenerico[años.Count];
                //años.CopyTo(añosCopy);
                //cmbAñoDepositos.DataSource = añosCopy;
                //cmbAñosManoDeObra.DataSource = añosCopy;
                //cmbAñosMateriales.DataSource = añosCopy;
            }
            private void tcPedidosGrids_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {
                    if (tcPedidosGrids.SelectedIndex == 0) {
                        btnRefrescar.PerformClick();
                    }
                    else if (tcPedidosGrids.SelectedIndex == 1)
                    {
                        if (!CargarAños)
                        {
                            CargaAños();
                            cmbMesesVentas.SelectedIndex = DateTime.Today.Month - 1;
                            //dtpEntradasFechaDesde.Value = DateTime.Today;

                            CargaRvVentas(Program.EmpresaSeleccionada.Id, 
                                new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1, 1), 
                                new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1)),
                                ObtieneClienteFromCmb(cmbClientes).Id);

                            CargarAños = false;
                        }
                    }
                }
                catch (Exception ex) { MuestraExcepcion(ex); }
            }
            private void btnRefrescarReporteVentas_Click(object sender, EventArgs e)
        {
            try {
                if (rdoPorMesVentas.Checked)
                {
                    if (cmbMesesVentas.SelectedIndex >= 0)
                        CargaRvVentas(Program.EmpresaSeleccionada.Id, new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1)),
                            ObtieneClienteFromCmb(cmbClientes).Id);
                }
                else if (rdoPorDiaVentas.Checked)
                    CargaRvVentas(Program.EmpresaSeleccionada.Id, dtpFechaDiaVentas.Value.Date, dtpFechaDiaVentas.Value.Date,
                        ObtieneClienteFromCmb(cmbClientes).Id);
                else if (rdoPorFechasVentas.Checked)
                    CargaRvVentas(Program.EmpresaSeleccionada.Id, dtpFechaDesdeVentas.Value.Date, dtpFechaHastaVentas.Value.Date,
                        ObtieneClienteFromCmb(cmbClientes).Id);
            }catch(Exception ex) { MuestraExcepcion(ex); }
            }
            private void rdoVentasPorMes_CheckedChanged(object sender, EventArgs e)
            {
                try
                {
                    if (((RadioButton)sender).Checked)
                    {
                        rdoPorDiaVentas.Checked = false;
                        rdoPorFechasVentas.Checked = false;
                        pnlVentasPorMes.Enabled = true;
                        pnlVentasPorDia.Enabled = false;
                        pnlVentasPorFechas.Enabled = false;

                        CargaRvVentas(Program.EmpresaSeleccionada.Id, new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1)),
                            ObtieneClienteFromCmb(cmbClientes).Id);
                    }
                }
                catch (Exception ex) { MuestraExcepcion(ex); }
            }

            private void rdoVentasPorSemana_CheckedChanged(object sender, EventArgs e)
            {
                try
                {
                    if (((RadioButton)sender).Checked)
                    {
                        rdoPorMesVentas.Checked = false;
                        rdoPorFechasVentas.Checked = false;

                        pnlVentasPorMes.Enabled = false;
                        pnlVentasPorDia.Enabled = true;
                        pnlVentasPorFechas.Enabled = false;
                        CargaRvVentas(Program.EmpresaSeleccionada.Id, dtpFechaDiaVentas.Value.Date, dtpFechaDiaVentas.Value.Date,
                            ObtieneClienteFromCmb(cmbClientes).Id);
                    }
                }
                catch (Exception ex) { MuestraExcepcion(ex); } 
            }

            private void rdoPorFechasVentas_CheckedChanged(object sender, EventArgs e)
            {
                try
                {
                    if (((RadioButton)sender).Checked)
                    {
                        rdoPorMesVentas.Checked = false;
                        rdoPorDiaVentas.Checked = false;

                        pnlVentasPorMes.Enabled = false;
                        pnlVentasPorDia.Enabled = false;
                        pnlVentasPorFechas.Enabled = true;
                        CargaRvVentas(Program.EmpresaSeleccionada.Id, dtpFechaDesdeVentas.Value.Date, dtpFechaHastaVentas.Value.Date,
                            ObtieneClienteFromCmb(cmbClientes).Id);
                    }
                }
                catch (Exception ex) { MuestraExcepcion(ex); }
        }


        private void dtpFechaDesdeVentas_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoPorFechasVentas.Checked)
                {
                    if (dtpFechaDesdeVentas.Value.Date > dtpFechaHastaVentas.Value.Date)
                        dtpFechaHastaVentas.Value = dtpFechaDesdeVentas.Value;
                    else
                        CargaRvVentas(Program.EmpresaSeleccionada.Id, dtpFechaDesdeVentas.Value.Date, dtpFechaHastaVentas.Value.Date,
                            ObtieneClienteFromCmb(cmbClientes).Id);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void dtpFechaHastaVentas_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoPorFechasVentas.Checked)
                {
                    if (dtpFechaHastaVentas.Value.Date < dtpFechaDesdeVentas.Value.Date)
                        dtpFechaDesdeVentas.Value = dtpFechaHastaVentas.Value;
                    else
                        CargaRvVentas(Program.EmpresaSeleccionada.Id, dtpFechaDesdeVentas.Value.Date, dtpFechaHastaVentas.Value.Date,
                            ObtieneClienteFromCmb(cmbClientes).Id);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        #endregion

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void pnlSolicitud_Paint(object sender, PaintEventArgs e)
        {

        }
        /// <summary>
        /// Agrega nuevo registro del Pedido solicitado.
        /// </summary>
        /// <param name="pedido"></param>
        void AgregarPago(int PedidoId, decimal Cantidad)
        {
            EntPago pago = new EntPago()
            {
                PedidoId = PedidoId,
                TipoPagoId = 1,
                Cantidad = Cantidad,
                FechaPago = DateTime.Today
            };
            new BusPedidos().AgregaPagoPedido(pago);
        }
        void AumentaPagoPedido(int PedidoId, decimal Pago)
        {
            EntPedido pedido = new EntPedido()
            {
                Id = PedidoId,
                Pago = Pago,
                Fecha = DateTime.Today
            };
            new BusPedidos().AumentaPagoPedido(pedido);
        }

        void MuestraAgregarComprobantePago(EntPedido PedidoFactura)
        {
            AgregaComplementoPago vComple = new AgregaComplementoPago();
            vComple.Cliente = new BusClientes().ObtieneCliente(PedidoFactura.ClienteId);
            vComple.PedidoFactura = PedidoFactura;

            if (vComple.ShowDialog() == DialogResult.OK)
            {
                AgregarPago(PedidoFactura.Id, vComple.Cantidad);
                AumentaPagoPedido(PedidoFactura.Id, vComple.Cantidad);

                btnRefrescar.PerformClick();
                MuestraMensaje("COMPLEMENTO ENVIADO", "CONFIRMACIÓN");
            }
        }
        private void btnComplementoPago_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedido = ObtienePedidoFromGV(gvPedidos);
                MuestraAgregarComprobantePago(pedido);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnFacturaExterna_Click(object sender, EventArgs e)
        {
            try
            {
                EntEmpresa empresaSeleccionada = Program.EmpresaSeleccionada;

                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);

                AgregaNumeroFactura vNumFac = new Pantallas.AgregaNumeroFactura();
                if (vNumFac.ShowDialog() == DialogResult.OK) { 
                    EntFactura factura = new EntFactura();
                    factura.EmpresaId = empresaSeleccionada.Id;
                    factura.PedidoId = pedidoSeleccionado.Id;
                    
                    factura.TipoComprobanteId = 1;//I-INGRESO.
                    //factura.FormaPagoId = ConvierteTextoAInteger(txtFormaPago.Text);
                    //factura.MetodoPagoId = cmbMetodoPago.SelectedIndex + 1;
                    //factura.UsoCFDIId = ObtieneCatalogoGenericoFromCmb(cmbUsoCFDI).Id;
                    factura.SerieFactura = "";// empresaSeleccionada.SerieFactura;

                    factura.NumeroFactura = vNumFac.NumeroFactura;
                    factura.Fecha = vNumFac.FechaFactura;
                    factura.UUID = "";
                    factura.Ruta = "";

                    AgregarFacturaPedido(factura);

                    btnFacturar.Visible = false;
                    pnlFacturacion.Visible = false;

                    btnRefrescar.Refresh();
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        ///CREAR PDF'S
        void ModificaPDF(string TextoEscribe, string RutaArchivoModifica, string VersionModificada, List<EntProducto> ListaProductos)
        {
            string pathSourcePDF = RutaArchivoModifica;
            //pathSourcePDF = @"C:\TIIM\Facturacion\FacturasPruebas\SERGIO PATRICIO GREE\20171004024601\439DFA02-9679-11E8-9275-D737D49CA409.pdf";
            FileInfo file = new FileInfo(RutaArchivoModifica);
            string pathOutputPDF = RutaArchivoModifica.Remove(RutaArchivoModifica.Length-4) + "-OBSERVACION"+VersionModificada+".pdf";
             //   pathOutputPDF = @"C:\TIIM\Facturacion\FacturasPruebas\SERGIO PATRICIO GREE\20171004024601\neuefile.pdf";

            ////Create an instance of our strategy
            //var t = new MyLocationTextExtractionStrategy("TOTAL");

            ////Parse page 1 of the document above
            //using (var r = new PdfReader(pathSourcePDF))
            //{
            //    var ex = PdfTextExtractor.GetTextFromPage(r, 1, t);
            //}

            //create PdfReader object to read from the existing document
            using (PdfReader reader3 = new PdfReader(pathSourcePDF))
            //create PdfStamper object to write to get the pages from reader 
            using (PdfStamper stamper = new PdfStamper(reader3, new FileStream(pathOutputPDF, FileMode.Create)))
            {
                //select two pages from the original document
                reader3.SelectPages("1");

                //gettins the page size in order to substract from the iTextSharp coordinates
                var pageSize = reader3.GetPageSize(1);

                // PdfContentByte from stamper to add content to the pages over the original content
                PdfContentByte pbover = stamper.GetOverContent(1);

                //add content to the page using ColumnText
                iTextSharp.text.Font font = new iTextSharp.text.Font();
                font.Size = 8;

                //USING SPIRE
                ////PdfTextFind[] results = null;
                ////foreach (PdfPageBase page in doc.Pages)
                ////{
                ////    results = page.FindText("Spire.PDF").Finds;
                ////    foreach (PdfTextFind text in results)
                ////    {
                ////        PointF p = text.Position;
                ////        Console.WriteLine(p);
                ////    }
                ////}
                //USING SPIRE

                //var t = new MyLocationTextExtractionStrategy("TOTAL");
                //var ex = PdfTextExtractor.GetTextFromPage(reader3, 1, t);
                //int x = Convert.ToInt32(t.myPoints[0].Rect.Left);
                //int y = Convert.ToInt32(t.myPoints[0].Rect.Bottom);

                //***BUSCACOORDENADAS***
                string textSearch = "TOTAL";
                var parser = new PdfReaderContentParser(reader3);

                    var strategy = parser.ProcessContent(1, new LocationTextExtractionStrategyWithPosition());

                    var res = strategy.GetLocations();
                    
                var searchResult = res.Where(p => p.Text.Contains(textSearch)).OrderBy(p => p.Y).Reverse().ToList();
                //****//
                int cant = ListaProductos.Count;
                
                int x = 120;
                int y = (Convert.ToInt32(searchResult[1].Y)+118)/cant;

                y = (int)(pageSize.Height - y);
                //Creates an image that is the size i need to hide the text i'm interested in removing
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(new Bitmap(600, 40), BaseColor.WHITE);
                //Sets the position that the image needs to be placed (ie the location of the text to be removed)
                image.SetAbsolutePosition(0, y-20);
                //Adds the image to the output pdf
                stamper.GetOverContent(1).AddImage(image, true);
                //setting up the X and Y coordinates of the document
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_CENTER, new Phrase(TextoEscribe, font), x, y, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_CENTER, new Phrase("Facturación 3.3 - Tiim Tecnología - www.tiimtecnologia.com -", font), 80, y-20, 0);
            }

        }

        public class LocationTextExtractionStrategyWithPosition : LocationTextExtractionStrategy
        {
            private readonly List<TextChunk> locationalResult = new List<TextChunk>();

            private readonly ITextChunkLocationStrategy tclStrat;

            public LocationTextExtractionStrategyWithPosition() : this(new TextChunkLocationStrategyDefaultImp())
            {
            }

            /**
             * Creates a new text extraction renderer, with a custom strategy for
             * creating new TextChunkLocation objects based on the input of the
             * TextRenderInfo.
             * @param strat the custom strategy
             */
            public LocationTextExtractionStrategyWithPosition(ITextChunkLocationStrategy strat)
            {
                tclStrat = strat;
            }


            private bool StartsWithSpace(string str)
            {
                if (str.Length == 0) return false;
                return str[0] == ' ';
            }


            private bool EndsWithSpace(string str)
            {
                if (str.Length == 0) return false;
                return str[str.Length - 1] == ' ';
            }

            /**
             * Filters the provided list with the provided filter
             * @param textChunks a list of all TextChunks that this strategy found during processing
             * @param filter the filter to apply.  If null, filtering will be skipped.
             * @return the filtered list
             * @since 5.3.3
             */

            private List<TextChunk> filterTextChunks(List<TextChunk> textChunks, ITextChunkFilter filter)
            {
                if (filter == null)
                {
                    return textChunks;
                }

                var filtered = new List<TextChunk>();

                foreach (var textChunk in textChunks)
                {
                    if (filter.Accept(textChunk))
                    {
                        filtered.Add(textChunk);
                    }
                }

                return filtered;
            }

            public override void RenderText(TextRenderInfo renderInfo)
            {
                LineSegment segment = renderInfo.GetBaseline();
                if (renderInfo.GetRise() != 0)
                { // remove the rise from the baseline - we do this because the text from a super/subscript render operations should probably be considered as part of the baseline of the text the super/sub is relative to 
                    Matrix riseOffsetTransform = new Matrix(0, -renderInfo.GetRise());
                    segment = segment.TransformBy(riseOffsetTransform);
                }
                TextChunk tc = new TextChunk(renderInfo.GetText(), tclStrat.CreateLocation(renderInfo, segment));
                locationalResult.Add(tc);
            }


            public IList<TextLocation> GetLocations()
            {

                var filteredTextChunks = filterTextChunks(locationalResult, null);
                filteredTextChunks.Sort();

                TextChunk lastChunk = null;

                var textLocations = new List<TextLocation>();

                foreach (var chunk in filteredTextChunks)
                {

                    if (lastChunk == null)
                    {
                        //initial
                        textLocations.Add(new TextLocation
                        {
                            Text = chunk.Text,
                            X = iTextSharp.text.Utilities.PointsToMillimeters(chunk.Location.StartLocation[0]),
                            Y = iTextSharp.text.Utilities.PointsToMillimeters(chunk.Location.StartLocation[1])
                        });

                    }
                    else
                    {
                        if (chunk.SameLine(lastChunk))
                        {
                            var text = "";
                            // we only insert a blank space if the trailing character of the previous string wasn't a space, and the leading character of the current string isn't a space
                            if (IsChunkAtWordBoundary(chunk, lastChunk) && !StartsWithSpace(chunk.Text) && !EndsWithSpace(lastChunk.Text))
                                text += ' ';

                            text += chunk.Text;

                            textLocations[textLocations.Count - 1].Text += text;

                        }
                        else
                        {

                            textLocations.Add(new TextLocation
                            {
                                Text = chunk.Text,
                                X = iTextSharp.text.Utilities.PointsToMillimeters(chunk.Location.StartLocation[0]),
                                Y = iTextSharp.text.Utilities.PointsToMillimeters(chunk.Location.StartLocation[1])
                            });
                        }
                    }
                    lastChunk = chunk;
                }

                //now find the location(s) with the given texts
                return textLocations;

            }

        }

        public class TextLocation
        {
            public float X { get; set; }
            public float Y { get; set; }

            public string Text { get; set; }
        }
    
    ////Helper class that stores our rectangle and text
    //public class RectAndText
    //{
    //    public iTextSharp.text.Rectangle Rect;
    //    public String Text;
    //    public RectAndText(iTextSharp.text.Rectangle rect, String text)
    //    {
    //        this.Rect = rect;
    //        this.Text = text;
    //    }
    //}
    //public class MyLocationTextExtractionStrategy : LocationTextExtractionStrategy
    //{
    //    //Hold each coordinate
    //    public List<RectAndText> myPoints = new List<RectAndText>();

    //    //The string that we're searching for
    //    public String TextToSearchFor { get; set; }

    //    //How to compare strings
    //    public System.Globalization.CompareOptions CompareOptions { get; set; }

    //    public MyLocationTextExtractionStrategy(String textToSearchFor, System.Globalization.CompareOptions compareOptions = System.Globalization.CompareOptions.None)
    //    {
    //        this.TextToSearchFor = textToSearchFor;
    //        this.CompareOptions = compareOptions;
    //    }

    //    //Automatically called for each chunk of text in the PDF
    //    public override void RenderText(TextRenderInfo renderInfo)
    //    {
    //        base.RenderText(renderInfo);

    //        //See if the current chunk contains the text
    //        var startPosition = System.Globalization.CultureInfo.CurrentCulture.CompareInfo.IndexOf(renderInfo.GetText(), this.TextToSearchFor, this.CompareOptions);

    //        //If not found bail
    //        if (startPosition < 0)
    //        {
    //            return;
    //        }

    //        //Grab the individual characters
    //        var chars = renderInfo.GetCharacterRenderInfos().Skip(startPosition).Take(this.TextToSearchFor.Length).ToList();

    //        //Grab the first and last character
    //        var firstChar = chars.First();
    //        var lastChar = chars.Last();


    //        //Get the bounding box for the chunk of text
    //        var bottomLeft = firstChar.GetDescentLine().GetStartPoint();
    //        var topRight = lastChar.GetAscentLine().GetEndPoint();

    //        //Create a rectangle from it
    //        var rect = new iTextSharp.text.Rectangle(
    //                                                bottomLeft[Vector.I1],
    //                                                bottomLeft[Vector.I2],
    //                                                topRight[Vector.I1],
    //                                                topRight[Vector.I2]
    //                                                );

    //        //Add this to our main collection
    //        this.myPoints.Add(new RectAndText(rect, this.TextToSearchFor));
    //    }

    //}

    //public static class PdfTextExtract
    //{
    //    public static string pdfText(string path)
    //    {
    //        PdfReader reader = new PdfReader(path);
    //        string text = string.Empty;
    //        for (int page = 1; page <= reader.NumberOfPages; page++)
    //        {
    //            text += PdfTextExtractor.GetTextFromPage(reader, page);
    //        }
    //        reader.Close();
    //        return text;
    //    }
    //}

    ///// <summary>
    ///// Parses a PDF file and extracts the text from it.
    ///// </summary>
    //public class PDFParser
    //{
    //    /// BT = Beginning of a text object operator 
    //    /// ET = End of a text object operator
    //    /// Td move to the start of next line
    //    ///  5 Ts = superscript
    //    /// -5 Ts = subscript

    //    #region Fields

    //    #region _numberOfCharsToKeep
    //    /// <summary>
    //    /// The number of characters to keep, when extracting text.
    //    /// </summary>
    //    private static int _numberOfCharsToKeep = 15;
    //    #endregion

    //    #endregion

    //    #region ExtractText
    //    /// <summary>
    //    /// Extracts a text from a PDF file.
    //    /// </summary>
    //    /// <param name="inFileName">the full path to the pdf file.</param>
    //    /// <param name="outFileName">the output file name.</param>
    //    /// <returns>the extracted text</returns>
    //    public bool ExtractText(string inFileName, string outFileName)
    //    {
    //        StreamWriter outFile = null;
    //        try
    //        {
    //            // Create a reader for the given PDF file
    //            PdfReader reader = new PdfReader(inFileName);
    //            //outFile = File.CreateText(outFileName);
    //            outFile = new StreamWriter(outFileName, false, System.Text.Encoding.UTF8);

    //            Console.Write("Processing: ");

    //            int totalLen = 68;
    //            float charUnit = ((float)totalLen) / (float)reader.NumberOfPages;
    //            int totalWritten = 0;
    //            float curUnit = 0;

    //            for (int page = 1; page <= reader.NumberOfPages; page++)
    //            {
    //                outFile.Write(ExtractTextFromPDFBytes(reader.GetPageContent(page)) + " ");

    //                // Write the progress.
    //                if (charUnit >= 1.0f)
    //                {
    //                    for (int i = 0; i < (int)charUnit; i++)
    //                    {
    //                        Console.Write("#");
    //                        totalWritten++;
    //                    }
    //                }
    //                else
    //                {
    //                    curUnit += charUnit;
    //                    if (curUnit >= 1.0f)
    //                    {
    //                        for (int i = 0; i < (int)curUnit; i++)
    //                        {
    //                            Console.Write("#");
    //                            totalWritten++;
    //                        }
    //                        curUnit = 0;
    //                    }

    //                }
    //            }

    //            if (totalWritten < totalLen)
    //            {
    //                for (int i = 0; i < (totalLen - totalWritten); i++)
    //                {
    //                    Console.Write("#");
    //                }
    //            }
    //            return true;
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //        finally
    //        {
    //            if (outFile != null) outFile.Close();
    //        }
    //    }
    //    #endregion

    //    #region ExtractTextFromPDFBytes
    //    /// <summary>
    //    /// This method processes an uncompressed Adobe (text) object 
    //    /// and extracts text.
    //    /// </summary>
    //    /// <param name="input">uncompressed</param>
    //    /// <returns></returns>
    //    public string ExtractTextFromPDFBytes(byte[] input)
    //    {
    //        if (input == null || input.Length == 0) return "";

    //        try
    //        {
    //            string resultString = "";

    //            // Flag showing if we are we currently inside a text object
    //            bool inTextObject = false;

    //            // Flag showing if the next character is literal 
    //            // e.g. '\\' to get a '\' character or '\(' to get '('
    //            bool nextLiteral = false;

    //            // () Bracket nesting level. Text appears inside ()
    //            int bracketDepth = 0;

    //            // Keep previous chars to get extract numbers etc.:
    //            char[] previousCharacters = new char[_numberOfCharsToKeep];
    //            for (int j = 0; j < _numberOfCharsToKeep; j++) previousCharacters[j] = ' ';


    //            for (int i = 0; i < input.Length; i++)
    //            {
    //                char c = (char)input[i];
    //                if (input[i] == 213)
    //                    c = "'".ToCharArray()[0];

    //                if (inTextObject)
    //                {
    //                    // Position the text
    //                    if (bracketDepth == 0)
    //                    {
    //                        if (CheckToken(new string[] { "TD", "Td" }, previousCharacters))
    //                        {
    //                            resultString += "\n\r";
    //                        }
    //                        else
    //                        {
    //                            if (CheckToken(new string[] { "'", "T*", "\"" }, previousCharacters))
    //                            {
    //                                resultString += "\n";
    //                            }
    //                            else
    //                            {
    //                                if (CheckToken(new string[] { "Tj" }, previousCharacters))
    //                                {
    //                                    resultString += " ";
    //                                }
    //                            }
    //                        }
    //                    }

    //                    // End of a text object, also go to a new line.
    //                    if (bracketDepth == 0 &&
    //                        CheckToken(new string[] { "ET" }, previousCharacters))
    //                    {

    //                        inTextObject = false;
    //                        resultString += " ";
    //                    }
    //                    else
    //                    {
    //                        // Start outputting text
    //                        if ((c == '(') && (bracketDepth == 0) && (!nextLiteral))
    //                        {
    //                            bracketDepth = 1;
    //                        }
    //                        else
    //                        {
    //                            // Stop outputting text
    //                            if ((c == ')') && (bracketDepth == 1) && (!nextLiteral))
    //                            {
    //                                bracketDepth = 0;
    //                            }
    //                            else
    //                            {
    //                                // Just a normal text character:
    //                                if (bracketDepth == 1)
    //                                {
    //                                    // Only print out next character no matter what. 
    //                                    // Do not interpret.
    //                                    if (c == '\\' && !nextLiteral)
    //                                    {
    //                                        resultString += c.ToString();
    //                                        nextLiteral = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        if (((c >= ' ') && (c <= '~')) ||
    //                                            ((c >= 128) && (c < 255)))
    //                                        {
    //                                            resultString += c.ToString();
    //                                        }

    //                                        nextLiteral = false;
    //                                    }
    //                                }
    //                            }
    //                        }
    //                    }
    //                }

    //                // Store the recent characters for 
    //                // when we have to go back for a checking
    //                for (int j = 0; j < _numberOfCharsToKeep - 1; j++)
    //                {
    //                    previousCharacters[j] = previousCharacters[j + 1];
    //                }
    //                previousCharacters[_numberOfCharsToKeep - 1] = c;

    //                // Start of a text object
    //                if (!inTextObject && CheckToken(new string[] { "BT" }, previousCharacters))
    //                {
    //                    inTextObject = true;
    //                }
    //            }

    //            return CleanupContent(resultString);
    //        }
    //        catch
    //        {
    //            return "";
    //        }
    //    }

    //    private string CleanupContent(string text)
    //    {
    //        string[] patterns = { @"\\\(", @"\\\)", @"\\226", @"\\222", @"\\223", @"\\224", @"\\340", @"\\342", @"\\344", @"\\300", @"\\302", @"\\304", @"\\351", @"\\350", @"\\352", @"\\353", @"\\311", @"\\310", @"\\312", @"\\313", @"\\362", @"\\364", @"\\366", @"\\322", @"\\324", @"\\326", @"\\354", @"\\356", @"\\357", @"\\314", @"\\316", @"\\317", @"\\347", @"\\307", @"\\371", @"\\373", @"\\374", @"\\331", @"\\333", @"\\334", @"\\256", @"\\231", @"\\253", @"\\273", @"\\251", @"\\221" };
    //        string[] replace = { "(", ")", "-", "'", "\"", "\"", "à", "â", "ä", "À", "Â", "Ä", "é", "è", "ê", "ë", "É", "È", "Ê", "Ë", "ò", "ô", "ö", "Ò", "Ô", "Ö", "ì", "î", "ï", "Ì", "Î", "Ï", "ç", "Ç", "ù", "û", "ü", "Ù", "Û", "Ü", "®", "™", "«", "»", "©", "'" };

    //        for (int i = 0; i < patterns.Length; i++)
    //        {
    //            string regExPattern = patterns[i];
    //            Regex regex = new Regex(regExPattern, RegexOptions.IgnoreCase);
    //            text = regex.Replace(text, replace[i]);
    //        }

    //        return text;
    //    }

    //    #endregion

    //    #region CheckToken
    //    /// <summary>
    //    /// Check if a certain 2 character token just came along (e.g. BT)
    //    /// </summary>
    //    /// <param name="tokens">the searched token</param>
    //    /// <param name="recent">the recent character array</param>
    //    /// <returns></returns>
    //    private bool CheckToken(string[] tokens, char[] recent)
    //    {
    //        foreach (string token in tokens)
    //        {
    //            if ((recent[_numberOfCharsToKeep - 3] == token[0]) &&
    //                (recent[_numberOfCharsToKeep - 2] == token[1]) &&
    //                ((recent[_numberOfCharsToKeep - 1] == ' ') ||
    //                (recent[_numberOfCharsToKeep - 1] == 0x0d) ||
    //                (recent[_numberOfCharsToKeep - 1] == 0x0a)) &&
    //                ((recent[_numberOfCharsToKeep - 4] == ' ') ||
    //                (recent[_numberOfCharsToKeep - 4] == 0x0d) ||
    //                (recent[_numberOfCharsToKeep - 4] == 0x0a))
    //                )
    //            {
    //                return true;
    //            }
    //        }
    //        return false;
    //    }
    //    #endregion
    //}

    private void btnReimprimeObservaciones_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                if (!pedidoSeleccionado.Facturado)
                    throw new Exception("Pedido Sin Facturar");

                AgregaObservacionFactura vObserv = new AgregaObservacionFactura();
                if (vObserv.ShowDialog() == DialogResult.OK)
                {
                    string rutaArchivo = pedidoSeleccionado.RutaFactura + "\\" + EncuentraArchivo(pedidoSeleccionado.RutaFactura, ".pdf");
                    //string rutaArchivo = @"C:\TIIM\Facturacion\FacturasPruebas\SERGIO PATRICIO GREE\20171004024601\439DFA02-9679-11E8-9275-D737D49CA409.pdf";
                    int versionArchivo = CuentaArchivos(pedidoSeleccionado.RutaFactura, ".pdf");
                    ModificaPDF(vObserv.Observacion, rutaArchivo, versionArchivo.ToString(), new BusProductos().ObtieneProductosPorPedido(pedidoSeleccionado.Id));
                    MuestraArchivo(rutaArchivo.Remove(rutaArchivo.Length - 4) + "-OBSERVACION" + versionArchivo.ToString() + ".pdf", true);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
    }
}
