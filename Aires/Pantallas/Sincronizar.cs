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
        
        int DiasPorSemana = 6;
        void InicializaPantalla()
        {
            //if(Program.EmpresaSeleccionada!=null)
            //    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
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

                    //CargaGvPedidos(Program.EmpresaSeleccionada.Id);
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
                    ////CargaGvPedidos(Program.EmpresaSeleccionada.Id);
                    //btnRefrescar.PerformClick();
                    //btnRefrescarReporteVentas.PerformClick();
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
     
        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                gvProductosDetalle.DataSource = null;
                gvProductosDetalle.Refresh();
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
                string rutaArchivo = SeleccionaArchivo();
                System.IO.FileInfo fi = new FileInfo(rutaArchivo);
                if (fi != null)
                {
                    if (fi.Extension != ".xls")
                        MandaExcepcion("El archivo no es el correcto. \n Debe ser archivo de Excel (.xls)");

                    txtDescripcionFiltro.Text = rutaArchivo; //fi.FullName;
                    List<EntProducto> lst = new UtiLinqToExcel().ListaProductos(rutaArchivo);

                    gvProductosDetalle.DataSource = lst;
                    txtCantidadVentas.Text = lst.Count.ToString();
                }
                else
                    MandaExcepcion("ARCHIVO NO ENCONTRADO");
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }
        
        #region Eventos Pestaña Impresion
      
            private void tcPedidosGrids_SelectedIndexChanged(object sender, EventArgs e)
            {
                
            }
           
        #endregion

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void gvProductosDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        bool EncuentraProductoDetalleEnListaProductos(List<EntProducto> ListaProductos, int ProductoId)
        {
            if (ListaProductos.Where(P => P.Id == ProductoId).Count() > 0)
                return true;
            else
                return false;
        }
        int CuentaProductosEnListaProductos(List<EntProducto> ListaProductos, int ProductoId)
        {
            return ListaProductos.Where(P => P.ProductoId == ProductoId).Count();             
        }
        List<EntProducto> ObtieneProductosEnListaProductos(List<EntProducto> ListaProductos, int ProductoId)
        {
            return ListaProductos.Where(P => P.ProductoId == ProductoId).ToList();
        }
        void AgregaProductoDetalle(int Id, int ProductoId, int IngresoId, int EmpresaId, string Serie, decimal PrecioCosto, decimal PrecioVenta, decimal PrecioVenta2, decimal PrecioEspecial)
        {
            EntProducto producto = new EntProducto()
            {
                Id = Id,
                ProductoId=ProductoId,
                IngresoId = IngresoId,
                EmpresaId = EmpresaId,
                Serie = Serie,
                PrecioCosto = PrecioCosto,
                PrecioVenta = PrecioVenta,
                PrecioVenta2 = PrecioVenta2,
                PrecioEspecial = PrecioEspecial,
                Fecha = DateTime.Now
            };
            producto.Id = new BusProductos().AgregaProductoDetalle(Id,producto);
        }
        private void btnImportar_Click(object sender, EventArgs e)
        {
            try {
                //REVISAR FECHA DE IMPORTACION PARA EVITAR DUPLICAR
                //PENDIENTE TABLA DE REGISTRO DE IMPORTACIONES
                EntEmpresa empresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                List<EntProducto> listaProductosTodos = new BusProductos().ObtieneProductosDetalleTodos();
                List<EntProducto> listaProductosIngresar = ObtieneListaProductosFromGV(gvProductosDetalle);

                int productoId = 0;
                bool productoRepetido = false;
                
                foreach (EntProducto p in listaProductosIngresar) {
                    if (!EncuentraProductoDetalleEnListaProductos(listaProductosTodos, p.Id))
                    {
                        if (new BusProductos().ObtieneIngreso(p.IngresoId).Id != p.IngresoId)
                            new BusProductos().AgregaIngresoProducto(new EntCatalogoGenerico() { Descripcion = p.Ingreso, Fecha = p.Fecha });

                        if (productoId != p.ProductoId)
                        {
                            productoId = p.ProductoId;
                            int cantidadProductos = CuentaProductosEnListaProductos(listaProductosIngresar, p.ProductoId);
                            if (cantidadProductos == 0)
                                new Productos().AgregaProducto(p.TipoProductoId, p.Codigo, p.Descripcion);
                            else
                                new Productos().ActualizaProducto(p.ProductoId, p.TipoProductoId, p.Codigo, p.Descripcion);

                            if (p.TipoProductoId == 1)//PRODUCTO
                                new Productos().AumentaProducto(p.ProductoId, cantidadProductos);
                            else//SERVICIO
                                new Productos().AumentaProducto(p.ProductoId, 1);
                        }
                        AgregaProductoDetalle(p.Id, p.ProductoId, p.IngresoId, empresaSeleccionada.Id, p.Serie, p.PrecioCosto, p.PrecioVenta, p.PrecioVenta2, p.PrecioEspecial);
                    }
                    else
                        productoRepetido = true;
                }
                if(productoRepetido)
                    MuestraMensaje("Se encontraron Productos previamente importados. \n¡IMPORTACIÓN COMPLETA!","ALERTA");
                else
                    MuestraMensaje("¡IMPORTACIÓN COMPLETA!", "CONFIRMACIÓN");
                txtDescripcionFiltro.Clear();
                gvProductosDetalle.DataSource = null;
                gvProductosDetalle.Refresh();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void gvPedidos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvPedidos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void gvPedidos_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void btnImportaVentas_Click(object sender, EventArgs e)
        {
            Ventas vVentas = new Pantallas.Ventas();
            //pedidoAgrega = vVentas.AgregarPedido(ClienteSeleccionado.Id, detallePedido.Remove(detallePedido.Length - 2), "", ConvierteTextoADecimal(txtTotal.Text), ConvierteTextoADecimal(txtPago.Text), DateTime.Now, DateTime.Today, 0, chkFacturar.Checked, 1);

            //foreach (EntProducto p in productosSeleccionados)
            //{
            //    AgregarProductoDetallePedido(pedidoAgrega.Id, p.Id, p.Cantidad, p.PrecioCosto, p.PrecioVenta);
            //    Productos vProd = new Productos();
            //    if (p.TipoProductoId == 1)
            //    {
            //        vProd.ActualizaEstatusProductoDetalle(p, 2);//ESTATUS:2=ENTREGADO
            //        vProd.AumentaProducto(p.ProductoId, -p.Cantidad);
            //    }
            //}

            //decimal pago = ConvierteTextoADecimal(txtPago.Text);
            //if (pago > 0)
            //{
            //    if (pago > ConvierteTextoADecimal(txtTotal.Text))
            //    {
            //        MuestraMensaje("El Pago debe ser menor al Total a pagar. \n El Pago NO será registrado", "PAGO NO REGISTRADO");
            //        AumentaPagoPedido(pedidoAgrega.Id, -pago);
            //    }
            //    else
            //    {
            //        AgregarPago(pedidoAgrega.Id, pago);
            //        AumentaPagoPedido(pedidoAgrega.Id, 0);//SOLO PARA CAMBIAR ESTATUS. VERIFICA SI EL TOTAL DEL PEDIDO ESTA PAGADO, CAMBIA ESTATUS DE SER ASI.
            //        if (pago < ConvierteTextoADecimal(txtTotal.Text))
            //            CargaClientesDeudaEnPantallas();
            //    }
            //}

            //if (chkFacturar.Checked)
            //{
            //    bool facturado = false;
            //    EntFactura factura = new EntFactura();

            //    try
            //    {
            //        //int ultimaFactura = new BusPedidos().ObtieneUltimaFactura().Id;
            //        //ultimaFactura++;
            //        //pedidoAgrega.Factura = ultimaFactura.ToString();
            //        pedidoAgrega.Factura = ObtieneUltimaFactura(empresaSeleccionada.Id);
            //        pedidoAgrega.SubTotal = ConvierteTextoADecimal(txtSubtotal.Text);
            //        pedidoAgrega.Total = ConvierteTextoADecimal(txtTotal.Text);

            //        txtFormaPago.Text = cmbFormaPago.Text.Remove(2, cmbFormaPago.Text.Length - 2);
            //        txtMetodoPago.Text = cmbMetodoPago.Text;

            //        TomaDatosCliente(ClienteSeleccionado);

            //        if (empresaSeleccionada.Facturacion)
            //            factura = EnviarFactura(empresaSeleccionada, pedidoAgrega, productosSeleccionados, ClienteSeleccionado, DateTime.Now, txtFormaPago.Text, txtMetodoPago.Text, txtCondicionesPago.Text,
            //                                txtNumeroCuenta.Text,
            //                                cantidadIVA, cantidadIVARetenido, cantidadISRRetenido, cantidadIEPS);
            //        else
            //            factura = EnviarFacturaPrueba(empresaSeleccionada, pedidoAgrega, productosSeleccionados, ClienteSeleccionado, DateTime.Now, txtFormaPago.Text, txtMetodoPago.Text, txtCondicionesPago.Text,
            //                                txtNumeroCuenta.Text,
            //                                cantidadIVA, cantidadIVARetenido, cantidadISRRetenido, cantidadIEPS);

            //        factura.EmpresaId = empresaSeleccionada.Id;
            //        facturado = true;
            //    }
            //    catch (Exception ex)
            //    {
            //        foreach (EntProducto p in productosSeleccionados)
            //        {
            //            Productos vProd = new Productos();
            //            vProd.ActualizaEstatusProductoDetalle(p, 1);//ESTATUS:1=ACTIVO
            //            vProd.AumentaProducto(p.ProductoId, p.Cantidad);
            //        }
            //        ActualizaEstatusProductoDetallePedido(pedidoAgrega, false);//ESTATUS:0=CANCELADO
            //        ActualizaEstatusPedido(pedidoAgrega, 0);//ESTATUS:0=CANCELADO

            //        MuestraExcepcionFacturacion(ex);
            //    }

            //    //COMENTAR EN PRODUCCION
            //    //facturado = true;
            //    if (facturado)
            //    {
            //        //MuestraMensaje("¡El pedido fue FACTURADO satisfactoriamente!", "CONFIRMACIÓN PEDIDO FACTURADO");
            //        Cursor.Current = Cursors.WaitCursor;

            //        AgregarFacturaPedido(factura);

            //        try
            //        {
            //            Cursor.Current = Cursors.WaitCursor;

            //            EnviaCorreo(empresaSeleccionada, pedidoAgrega, ClienteSeleccionado, factura.Ruta);
            //        }
            //        catch (Exception ex)
            //        {
            //            MuestraExcepcion(ex, "Correo NO enviado.");
            //        }

            //        try
            //        {
            //            if (MuestraMensajeYesNo("¿Desea mostrar Factura?") == DialogResult.Yes)
            //            {
            //                string nombreArchivo = EncuentraArchivo(factura.Ruta, ".pdf");
            //                MuestraArchivo(factura.Ruta, nombreArchivo);
            //            }
            //        }
            //        catch (Exception ex) { MuestraExcepcion(ex, "ERROR AL MOSTRAR FACTURA"); }


            //        LimpiaTextBox(pnlCliente);
            //        LimpiaTextBox(pnlAgrega);
            //        LimpiaTextBox(pnlFacturacion, true);

            //        ClienteSeleccionado = null;
            //        chkFacturar.Checked = false;
            //        chkFacturaPublicoGeneral.Checked = false;
            //        gvProductosPedido.DataSource = null;
            //        lbContadorSeries.Text = "0";

            //        CargaVentasEnPantallas();
            //        CargaClientes(empresaSeleccionada.Id);//SERA POR VOLVER A PONER LOS PARAMETROS???
            //        CargaProductosDetalle(empresaSeleccionada.Id);

            //        Cursor.Current = Cursors.Default;
            //        MessageBox.Show("¡Pedido Registrado y Facturado!");
            //    }
            }
    }
}
