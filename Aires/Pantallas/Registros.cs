using AiresEntidades;
using AiresNegocio;
using AiresUtilerias;
using Microsoft.Office.Interop.Excel;
using Microsoft.Reporting.WinForms;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.parser;
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

            void AgregarFacturaPedido(int PedidoId, string UUID, string Ruta)
            {
                EntFactura factura = new EntFactura()
                {
                    PedidoId = PedidoId,
                    UUID = UUID,
                    Fecha = DateTime.Today,
                    Ruta=Ruta
                };
                new BusPedidos().AgregaFactura(factura);
            }
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
        public void CargaRvVentas(int EmpresaId, DateTime FechaDesde, DateTime FechaHasta)
        {
            List<EntPedido> ListaPedidos = new BusPedidos().ObtienePedidosClientesPorFechas(EmpresaId, FechaDesde, FechaHasta);
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


        private void Reportes_Load(object sender, EventArgs e)
        {
            try
            {
                InicializaPantalla();
                CargaEmpresas();

                if (Program.EmpresaSeleccionada == null)
                    Program.EmpresaSeleccionada = SeleccionaEmpresa();

                if (Program.EmpresaSeleccionada !=null)
                {
                    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);

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
        
        private void btnFacturar_Click(object sender, EventArgs e)
        {
            try
            {
                EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos); 
                
                if (pedidoSeleccionado.Facturado)
                    throw new Exception("El Pedido ya fue facturado.");

                if (MuestraMensajeYesNo(string.Format("¿Seguro desea FACTURAR el pedido seleccionado? \n Cliente:{0}",pedidoSeleccionado.Cliente), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    //List<EntProducto> productosPorPedido = new BusProductos().ObtieneProductosDetallePorPedido(pedidoSeleccionado.Id);
                    List<EntProducto> productosPorPedido = new BusProductos().ObtieneProductosPorPedido(pedidoSeleccionado.Id);

                    foreach (EntProducto p in productosPorPedido)
                    {
                        p.Descripcion=p.Descripcion.PadRight(100, '.');
                        p.Descripcion += " ";

                        foreach (EntProducto pd in new BusProductos().ObtieneProductosDetallePorPedido(pedidoSeleccionado.Id).Where(P => P.ProductoId == p.Id))
                        {
                            if (!string.IsNullOrWhiteSpace(pd.Serie))
                                p.Descripcion += pd.Serie + " | ";
                        }
                    }
                    EntCliente cliente = ObtieneCliente(pedidoSeleccionado.ClienteId);
                    if (chkFacturaPublicoGeneral.Checked)
                    {
                        EntCliente clientePublicoGeneral = ObtieneCliente(10);
                        clientePublicoGeneral.Nombre = cliente.Nombre;

                        cliente = clientePublicoGeneral;
                    }

                    bool facturado = false;
                    string uuid = "";
                    //string pahtArchivosFactura = "";
                    try
                    {
                        //throw new Exception("error");
                        int ultimaFactura = new BusPedidos().ObtieneUltimaFactura().Id;
                        ultimaFactura++;
                        pedidoSeleccionado.Factura = ultimaFactura.ToString();
                        //uuid = EnviarFactura(pedidoSeleccionado, productosPorPedido, cliente, DateTime.Now, txtFormaPago.Text, txtMetodoPago.Text, txtCondicionesPago.Text, txtNumeroCuenta.Text);
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
                        AgregarFacturaPedido(pedidoSeleccionado.Id, uuid, PathClienteDirectorioFacturas);
                        
                        try
                        {
                            //throw new Exception("error");
                            //pahtArchivosFactura = PathClienteDirectorioFacturas;
                            //pahtArchivosFactura = @"C:\TIIM\Facturacion\Facturas\RAFAEL GIL ARMENTA\20170105122126";
                            EnviaCorreo(cliente, PathClienteDirectorioFacturas);
                        }
                        catch (Exception ex)
                        {
                            MuestraExcepcion(ex, "Correo NO enviado.");
                        }
                    }

                    CargaGvPedidos(Program.EmpresaSeleccionada.Id);
                    Cursor.Current = Cursors.Default;
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

        private void btnCancelar_Click(object sender, EventArgs e)
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
                        facturaPruebas.Cancelar(new EntEmpresa() { RFC="XAXX010101000" },pedidoSeleccionado.UUID);
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
                //LimpiaTextBox(pnlFacturacion,true);
                LimpiaTextBox(groupBox2, true);
                CargaDatosCliente(ObtieneCliente(pedido.ClienteId));
                txtFormaPago.Text = cmbFormaPago.Text;
                txtMetodoPago.Text = cmbMetodoPago.Text.Remove(2, cmbMetodoPago.Text.Length - 2);
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
                EnableTextBox(pnlFacturacion, !chkFacturaPublicoGeneral.Checked);
                LimpiaTextBox(pnlFacturacion);

                txtFormaPago.Text = cmbFormaPago.Text;
                txtMetodoPago.Text = cmbMetodoPago.Text.Remove(2, cmbMetodoPago.Text.Length - 2);
                if (chkFacturaPublicoGeneral.Checked)
                {
                    EntCliente clientePublicoGeneral = ObtieneCliente(10);
                    //EntCliente clientePublicoGeneral = ListaClientes.Where(P => P.Id == 10).ToList()[0];

                    CargaDatosClientePubicoGeneral(clientePublicoGeneral);

                    txtCondicionesPago.Enabled = true;
                    txtNumeroCuenta.Enabled = true;
                }
                else
                {
                    EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);
                    CargaDatosCliente(ObtieneCliente(pedidoSeleccionado.ClienteId));

                    txtNombreFiscal.Enabled = false;
                    txtRFC.Enabled = false;
                }
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

                            CargaRvVentas(Program.EmpresaSeleccionada.Id, new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1)));

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
                        CargaRvVentas(Program.EmpresaSeleccionada.Id, new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1)));
                }
                else if (rdoPorDiaVentas.Checked)
                    CargaRvVentas(Program.EmpresaSeleccionada.Id, dtpFechaDiaVentas.Value.Date, dtpFechaDiaVentas.Value.Date);
                else if (rdoPorFechasVentas.Checked)
                    CargaRvVentas(Program.EmpresaSeleccionada.Id, dtpFechaDesdeVentas.Value.Date, dtpFechaHastaVentas.Value.Date);
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

                        CargaRvVentas(Program.EmpresaSeleccionada.Id, new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1, 1), new DateTime(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1, DateTime.DaysInMonth(ConvierteTextoAInteger(cmbAñoVentas.Text), cmbMesesVentas.SelectedIndex + 1)));
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
                        CargaRvVentas(Program.EmpresaSeleccionada.Id, dtpFechaDiaVentas.Value.Date, dtpFechaDiaVentas.Value.Date);
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
                        CargaRvVentas(Program.EmpresaSeleccionada.Id, dtpFechaDesdeVentas.Value.Date, dtpFechaHastaVentas.Value.Date);
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
                        CargaRvVentas(Program.EmpresaSeleccionada.Id, dtpFechaDesdeVentas.Value.Date, dtpFechaHastaVentas.Value.Date);
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
                        CargaRvVentas(Program.EmpresaSeleccionada.Id, dtpFechaDesdeVentas.Value.Date, dtpFechaHastaVentas.Value.Date);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        #endregion

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

    }
}
