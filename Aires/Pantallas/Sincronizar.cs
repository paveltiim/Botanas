using AiresEntidades;
using AiresNegocio;
using AiresUtilerias;
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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aires.Pantallas
{
    public partial class Sincronizacion : FormBase
    {
        public void VerificaEmpresa()
        {
            cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }
        public Sincronizacion()
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
                //gvPedidos.DataSource = ListaPedidos;
                txtCantidadVentas.Text = ListaPedidos.Count.ToString();
            }
            public void CargaGvPedidos(int EmpresaId, bool Estatus)
            {
                ListaPedidos = new BusPedidos().ObtienePedidos(EmpresaId, 1, 1, 1, Convert.ToInt16(Estatus), 1);
                //gvPedidos.DataSource = ListaPedidos;
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
        
            /// <summary>
            /// 
            /// </summary>
            /// <param name="pedido"></param>

            string PathClienteDirectorioFacturas;
        EntFactura EnviarFactura(EntEmpresa Empresa, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente, DateTime FechaFactura,
                                string FormaPago, string MedioPago, string CondicionPago, string NumeroCuenta,
                                decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS)
        {
            if (Cliente == null)
                Cliente = new EntCliente();
            
            string pathClienteDirectorio = PathFacturas + "\\" + Cliente.Nombre;
            if (!System.IO.Directory.Exists(pathClienteDirectorio))
                System.IO.Directory.CreateDirectory(pathClienteDirectorio);

            string pathClienteDirectorioFacturas = pathClienteDirectorio + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss");
            System.IO.Directory.CreateDirectory(pathClienteDirectorioFacturas);

            //UtiFacturacionPruebas factura = new UtiFacturacionPruebas();
            //MessageBox.Show("FACTURACIÓN DE PRUEBA");
            UtiFacturacion factura = new UtiFacturacion();
            

            //Pedido.Factura = txtNumeroFactura.Text;
            string uuid = factura.Facturar(Empresa, Pedido, ListaProductos, Cliente, Pedido.Factura, FechaFactura, FormaPago, MedioPago, CondicionPago,
                                            NumeroCuenta, pathClienteDirectorioFacturas,
                                            CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS);
            EntFactura fact = new EntFactura() { PedidoId = Pedido.Id, NumeroFactura = Pedido.Factura, UUID = uuid, Ruta = pathClienteDirectorioFacturas, Fecha = DateTime.Today };

            return fact;// pathClienteDirectorioFacturas;
        }


        /// <summary>
        /// Muestra Ventana emergente para Confirmar Envio de Correo, llama los métodos Imprime.AsignaValoresParametrosImpresionDatosCliente y Imprime.AsignaValoresParametrosImpresion.
        /// Envia correo electronico por medio de la clase UtiCorreo.
        /// </summary>
        /// <param name="Pedido"></param>
        /// <param name="Cliente"></param>
        /// <param name="NotaVenta"></param>
        /// <param name="Presupuesto"></param>
        void EnviaCorreo(EntPedido Pedido, EntCliente Cliente, string PathArchivosFactura)
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

                string asunto = "FACTURA DISTRIBUIDORA LM";

                new UtiCorreo().EnviaCorreo("Distribuidora LM mirage - " + asunto, new List<string>() { Cliente.Email }, "Envío factura.", archivosAdjuntos);

                MessageBox.Show("El Correo se ha Enviado correctamente, a la dirección -" + Cliente.Email + "-");
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
                List<EntPedido> ListaPedidos = new BusPedidos().ObtienePedidosClientesPorFechas(EmpresaId,FechaDesde,FechaHasta);
                entPedidoBindingSource.DataSource = ListaPedidos;
                rvVentas.RefreshReport();
            }
        #endregion

        int DiasPorSemana = 6;
        void InicializaPantalla()
        {
            //if(Program.EmpresaSeleccionada!=null)
            //    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
            dtpFechaDesdeVentas.Value = ObtieneLunesEstaSemana(DateTime.Today);
            dtpFechaHastaVentas.Value = dtpFechaDesdeVentas.Value.AddDays(DiasPorSemana);
        }
        public void CargaEmpresas()
        {
            ListaEmpresas = new BusEmpresas().ObtieneEmpresas();

            Program.CambiaEmpresa = false;
            cmbEmpresas.DataSource = ListaEmpresas;
            Program.CambiaEmpresa = true;

            //CargaClientesEnPantallas();
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
               
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnEnviaCorreo_Click(object sender, EventArgs e)
        {
            try
            {
                //EntPedido pedidoSeleccionado = ObtienePedidoFromGV(gvPedidos);

                //if (!pedidoSeleccionado.Facturado)
                //    throw new Exception("El Pedido NO ha sido facturado.");

                //if (MuestraMensajeYesNo(string.Format("¿Seguro desea enviar la FACTURA al correo seleccionado? \n Cliente:{0}", pedidoSeleccionado.Cliente), "CONFIRMACIÓN") == DialogResult.Yes)
                //{
                //    Cursor.Current = Cursors.WaitCursor;
                //    EntCliente cliente = ObtieneCliente(pedidoSeleccionado.ClienteId);
                //    cliente.Email = "";
                //    try
                //    {
                //        //throw new Exception("error");
                //        //pahtArchivosFactura = PathClienteDirectorioFacturas;
                //        //pahtArchivosFactura = @"C:\TIIM\Facturacion\Facturas\RAFAEL GIL ARMENTA\20170105122126";
                //        EnviaCorreo(pedidoSeleccionado, cliente, pedidoSeleccionado.RutaFactura);
                //    }
                //    catch (Exception ex)
                //    {
                //        MuestraExcepcion(ex, "Correo NO enviado.");
                //    }

                //    Cursor.Current = Cursors.Default;
                //}
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                CargaGvPedidos(Program.EmpresaSeleccionada.Id);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnVerFactura_Click(object sender, EventArgs e)
        {
            try
            {
                //EntPedido pedidoSeleccionado= ObtienePedidoFromGV(gvPedidos);
                //if (!pedidoSeleccionado.Facturado)
                //    throw new Exception("Pedido Sin Facturar");

                //MuestraArchivo(pedidoSeleccionado.RutaFactura);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnFiltrarCliente_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
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
