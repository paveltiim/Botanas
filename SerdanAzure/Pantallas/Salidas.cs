using AiresEntidades;
using AiresNegocio;
using AiresUtilerias;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
//using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static iTextSharp.text.pdf.parser.LocationTextExtractionStrategy;

namespace Aires.Pantallas
{
    public partial class Salidas : FormBase
    {
        public void VerificaEmpresa()
        {
            cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }

        public Salidas()
        {
            InitializeComponent();
        }

        public Salidas(EntEmpresa Empresa, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente
                     ,int FormaPagoId, int MedioPagoId, string CondicionPago, string NumeroCuenta)
        {
            InitializeComponent();
            CargaProductosPedido(ListaProductos);
            CalculaSumaTotal(ListaProductos, txtTotal);
        }

        void InicializaPantalla()
        {
            if (Program.EmpresaSeleccionada != null && cmbEmpresas.Items.Count > 0)
                cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }

        EntCliente ClienteSeleccionado = new EntCliente();
        EntProducto ProductoSeleccionado;
        List<EntProducto> ListaProductos = new List<EntProducto>();
        List<EntCliente> ListaClientes = new List<EntCliente>();

        List<EntProducto> ListaProductosPedido { get { return ObtieneListaProductosFromGV(gvProductosPedido); } }
        EntProducto ProductoPedidoSeleccionado { get { return ObtieneProductoFromGV(gvProductosPedido); } }

        int almacenId { get { return cmbAlmacenes.SelectedIndex + 1; } }

        #region Metodos

        public void CargaEmpresas()
        {
            Program.CambiaEmpresa = false;
        if (Program.UsuarioSeleccionado.Id > 1)
            cmbEmpresas.DataSource = new BusEmpresas().ObtieneEmpresas().Where(P => P.UsuarioId == Program.UsuarioSeleccionado.Id).ToList();
        else
            cmbEmpresas.DataSource = new BusEmpresas().ObtieneEmpresas();
        //cmbEmpresas.DataSource = new BusEmpresas().ObtieneEmpresas();
            Program.CambiaEmpresa = true;
        }

        public void CargaProductos(int AlmacenId)
        {
            this.ListaProductos = new BusProductos().ObtieneProductosExistenciaPorAlmacen(0, AlmacenId);//.OrderBy(P => P.Descripcion).ToList();
            gvProductosBusqueda.DataSource = ListaProductos;
        }
        public void CargaProductos(List<EntProducto> ListaProductos)
            {
                this.ListaProductos = ListaProductos;
                gvProductosBusqueda.DataSource = ListaProductos;
            }
        public void CargaProductosPedido(List<EntProducto> ListaProductosPedido)
        {
            gvProductosPedido.DataSource = ListaProductosPedido;
        }

        /// <summary>
        /// Agrega nuevo registro del Pedido solicitado.
        /// </summary>
        /// <param name="pedido"></param>
        public EntPedido AgregarPedido(int ClienteId, string Detalle, string Solicitud, string Observaciones, decimal Total, decimal Pago, DateTime Fecha, DateTime FechaEntrega, int EmpleadoId, bool Facturado, int EstatusId)
            {
                EntPedido pedido = new EntPedido()
                {
                    ClienteId = ClienteId,
                    Detalle = Detalle,
                    Solicitud = Solicitud,
                    Observaciones = Observaciones,
                    Total = Total,
                    Pago = Pago,
                    Fecha = Fecha,
                    FechaEntrega = FechaEntrega,
                    Facturado = Facturado,
                    EmpleadoId = EmpleadoId,
                    EstatusId = EstatusId
                };
                pedido.Id = new BusPedidos().AgregaPedido(pedido);
                return pedido;
            }
            /// <summary>
            /// Agrega nueva relación de Producto con el Pedido.
            /// </summary>
            /// <param name="pedido"></param>
            public void AgregarProductoDetallePedido(int PedidoId, int ProductoId, decimal Cantidad, decimal PrecioCosto, decimal PrecioVenta)
            {
                EntPedido pedido = new EntPedido()
                {
                    Id = PedidoId,
                    Fecha = DateTime.Now
                };

                EntProducto producto = new EntProducto()
                {
                    Id = ProductoId,
                    Cantidad = Cantidad,
                    PrecioCosto = PrecioCosto,
                    PrecioVenta = PrecioVenta
                };
                new BusPedidos().AgregaProductoDetallePedido(pedido, producto);
            }
            /// <summary>
            /// Agrega nuevo registro del Pedido solicitado.
            /// </summary>
            /// <param name="pedido"></param>
            public void AgregarPago(int PedidoId, decimal Cantidad)
            {
                EntPago pago = new EntPago()
                {
                    PedidoId=PedidoId,
                    TipoPagoId=1,
                    Cantidad = Cantidad,
                    FechaPago = DateTime.Today
                };
                new BusPedidos().AgregaPagoPedido(pago);
            }
        
        void AgregaProductoEnPedido(EntProducto ProductoSeleccionado, decimal CantidadAgrega)
        {
            List<EntProducto> productosPedido = ObtieneListaProductosFromGV(gvProductosPedido);

            if (CantidadAgrega > 0)
            {
                if (productosPedido == null)
                    productosPedido = new List<EntProducto>();

                //ProductoSeleccionado = ObtieneProductosFromGV(gvProductos);
                ProductoSeleccionado.Descripcion = ProductoSeleccionado.Descripcion.Replace(" MARCA: " + ProductoSeleccionado.Marca + " MODELO: " + ProductoSeleccionado.Modelo, "");
                if(!string.IsNullOrWhiteSpace(ProductoSeleccionado.Marca))
                    ProductoSeleccionado.Descripcion += " | MARCA: " + ProductoSeleccionado.Marca;
                if (!string.IsNullOrWhiteSpace(ProductoSeleccionado.Modelo))
                    ProductoSeleccionado.Descripcion += " | MODELO: " + ProductoSeleccionado.Modelo;
                ProductoSeleccionado.Cantidad = CantidadAgrega;
                productosPedido.Add(ProductoSeleccionado);
            }
            else if (CantidadAgrega < 0)
            {
                productosPedido.Remove(ProductoSeleccionado);
                ListaProductos.Add(ProductoSeleccionado);
            }

            gvProductosPedido.DataSource = null;
            gvProductosPedido.DataSource = productosPedido;

            CalculaSumaTotal(productosPedido, txtTotal);
            lbContadorSeries.Text = productosPedido.Count.ToString();

            //productosPedido[productosPedido.Count - 1].Descripcion=productosPedido[productosPedido.Count - 1].Descripcion.Replace("Solicitud:".PadLeft(5, '-') + txtSolicitud.Text,"");
            //productosPedido[productosPedido.Count - 1].Descripcion += "Solicitud:".PadLeft(5, '-') + txtSolicitud.Text;

        }
        void AgregaRemueveProductoEnPedido(EntProducto ProductoSeleccionado, decimal CantidadAgrega)
        {
            List<EntProducto> productosPedido = ObtieneListaProductosFromGV(gvProductosPedido);

            if (CantidadAgrega > 0)
            {
                if (productosPedido == null)
                    productosPedido = new List<EntProducto>();

                //ProductoSeleccionado = ObtieneProductosFromGV(gvProductos);
                ProductoSeleccionado.Descripcion += " " + ProductoSeleccionado.Marca + " " + ProductoSeleccionado.Modelo;
                ProductoSeleccionado.Cantidad = CantidadAgrega;
                productosPedido.Add(ProductoSeleccionado);
                ListaProductos.Remove(ProductoSeleccionado);
            }
            else if (CantidadAgrega < 0)
            {
                productosPedido.Remove(ProductoSeleccionado);
                ListaProductos.Add(ProductoSeleccionado);
            }

            gvProductosPedido.DataSource = null;
            gvProductosPedido.DataSource = productosPedido;

            CalculaSumaTotal(productosPedido, txtTotal);
            lbContadorSeries.Text = productosPedido.Count.ToString();

        }

        public void AgregarFacturaPedido(EntFactura Factura)
        {
            new BusFacturas().AgregaFactura(Factura);
        }

        public void AumentaPagoPedido(int PedidoId, decimal Pago)
            {
                EntPedido pedido= new EntPedido()
                {
                    Id= PedidoId,
                    Pago=Pago,
                    Fecha = DateTime.Today
                };
                new BusPedidos().AumentaPagoPedido(pedido);
            }

            
            /// <summary>
            /// Convierte Texto a Decimal de txtTotal.Text y le suma CantidadSumar.
            /// Muestra el resultado en txtTotal.Text.
            /// </summary>
            /// <param name="CantidadSumar"></param>
            void SumaTotal(decimal CantidadSumar)
            {
                txtTotal.Text = FormatoMoney(ConvierteTextoADecimal(txtTotal.Text) + CantidadSumar);
            }

        /// <summary>
        /// Muestra el resultado de ListaProductos.Sum(P=>P.Precio) en TxtMuestraTotal.Text.
        /// </summary>
        /// <param name="CantidadSumar"></param>
        void CalculaSumaTotal(List<EntProducto> ListaProductos, TextBox TxtMuestraTotal)
        {
            decimal total, subtotal, cantidadIva, descuento;
            decimal cantidadIVARetenido = 0;

            if (this.ListaProductos == null)
            {
                total = 0;
                subtotal = 0;
                cantidadIva = 0;
            }
            else
            {
                total = ListaProductos.Sum(P => P.PrecioC * P.Cantidad);

                //subtotal = Math.Round(total, 2) / (1 + IVA); //Math.Round(total / (1 + IVA), 2);
                //cantidadIva = subtotal * this.IVA;
                //cantidadIva = Math.Round(total, 2) - subtotal;
            }
         
            descuento = ConvierteTextoADecimal(txtDescuento);
            TxtMuestraTotal.Text = FormatoMoney(total-descuento);
        }

        #endregion


        void ActualizaEstatusPedido(EntPedido Pedido, int EstatusId)
        {
            Pedido.EstatusId = EstatusId;
            new BusPedidos().ActualizaEstatusPedido(Pedido);
        }
        public void ActualizaEstatusProductoDetallePedido(EntPedido Pedido, bool Estatus)
        {
            Pedido.Estatus = Estatus;
            new BusPedidos().ActualizaEstatusProductoDetallePedido(Pedido);
        }
        
        string ObtieneUltimaFactura(int EmpresaId)
        {
            int ultimaFactura = ConvierteTextoAInteger(new BusFacturas().ObtieneUltimaFactura(EmpresaId).NumeroFactura);
            ultimaFactura++;

            return ultimaFactura.ToString();
        }

        /// <summary>
        /// Muestra Ventana emergente para Confirmar Envio de Correo, llama los métodos Imprime.AsignaValoresParametrosImpresionDatosCliente y Imprime.AsignaValoresParametrosImpresion.
        /// Envia correo electronico por medio de la clase UtiCorreo.
        /// </summary>
        /// <param name="Pedido"></param>
        /// <param name="Cliente"></param>
        /// <param name="NotaVenta"></param>
        /// <param name="Presupuesto"></param>
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
                    new UtiCorreo().EnviaCorreo(asunto, new List<string>() { Cliente.Email, Cliente.Email2, Cliente.Email3, email4 , email5, email6}, mensaje, archivosAdjuntos);
                }
                //else
                    new UtiCorreo().EnviaCorreo(asunto, new List<string>() { Cliente.Email, Cliente.Email2, Cliente.Email3, email4 }, mensaje, archivosAdjuntos);
            //}
            //else
            //    new UtiCorreo().EnviaCorreo(asunto, new List<string>() { Cliente.Email, Cliente.Email2, Cliente.Email3}, mensaje, archivosAdjuntos);

            MessageBox.Show("El Correo se ha Enviado correctamente, a la(s) dirección(es): \n " + Cliente.Email + " \n " + Cliente.Email2 + " \n " + Cliente.Email3);
            //}
        }
        
        EntFactura EnviarFactura(EntEmpresa Empresa, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente, DateTime FechaFactura,
                                    string FormaPago, string MedioPago, string CondicionPago, string NumeroCuenta,
                                    decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS,
                                    string Observaciones,
                                    string TipoComprobante, string UsoCFDI,
                                    bool RelacionaFactura, string UUIDRelacionado)
        {
            string pathClienteDirectorio = PathFacturas + "\\" + Cliente.Nombre;
            if (!System.IO.Directory.Exists(pathClienteDirectorio))
                System.IO.Directory.CreateDirectory(pathClienteDirectorio);

            string carpetaFecha = DateTime.Now.ToString("yyyyMMddhhmmss");
            string pathClienteDirectorioFacturas = pathClienteDirectorio + "\\" + carpetaFecha;
            System.IO.Directory.CreateDirectory(pathClienteDirectorioFacturas);

            List<EntProducto> productosDetalle = ListaProductos;
            //ListaProductos = new BusProductos().ObtieneProductosPorPedido(Pedido.Id);

            //foreach (EntProducto p in ListaProductos)
            //{
            //    p.Descripcion = p.Descripcion.PadRight(100, '.');
            //    p.Descripcion += " ";

            //    foreach (EntProducto pd in productosDetalle.Where(P => P.ProductoId == p.Id))
            //    {
            //        if (!string.IsNullOrWhiteSpace(pd.Serie))
            //            p.Descripcion += pd.Serie +" | ";
            //    }
            //}
            List<EntProducto> ListaProductosFactura = new List<EntProducto>();
            string codigo = "";
            int cantidad = 1;
            string finalDescripcion = "";
            foreach (EntProducto p in productosDetalle.OrderByDescending(P => P.TipoProductoId).ThenBy(P => P.Codigo).ToList())
            {
                if (p.Codigo != codigo)
                {
                    EntProducto pneue = new EntProducto()
                    {
                        Id = p.Id,
                        Codigo = p.Codigo,
                        Serie = p.Serie,
                        Descripcion = p.Descripcion,

                        ProductoServicioId = p.ProductoServicioId,
                        ProductoServicio = p.ClaveProductoServicio + '-' + p.ProductoServicio,
                        ClaveProductoServicio = p.ClaveProductoServicio,

                        UnidadId = p.UnidadId,
                        Unidad = p.Unidad,
                        ClaveUnidad = p.ClaveUnidad,

                        Cantidad = p.Cantidad,
                        PrecioVenta = p.PrecioVenta,
                        PrecioVentaSinIVA = p.PrecioVentaSinIVA,
                        ProductoId = p.ProductoId
                    };

                    if (p.TipoProductoId == 1)
                    {
                        if (!string.IsNullOrWhiteSpace(finalDescripcion))
                            ListaProductosFactura[ListaProductosFactura.Count - 1].Descripcion += finalDescripcion;

                        if (pneue.Descripcion.Contains("CONTRATO"))
                        {
                            int index = pneue.Descripcion.IndexOf("CONTRATO");
                            finalDescripcion = pneue.Descripcion.Substring(index);
                            pneue.Descripcion = pneue.Descripcion.Remove(index);
                        }
                        if (!string.IsNullOrWhiteSpace(p.Serie))
                            pneue.Descripcion += "SERIE:";
                        pneue.Descripcion += " ";
                    }
                    ListaProductosFactura.Add(pneue);
                    codigo = pneue.Codigo;
                    cantidad = 1;             
                }
                else
                {
                    if (p.Descripcion.Contains("CONTRATO"))
                    {
                        int index = p.Descripcion.IndexOf("CONTRATO");
                        finalDescripcion = p.Descripcion.Substring(index);
                        p.Descripcion = p.Descripcion.Remove(index);
                    }

                    cantidad++;
                    ListaProductosFactura[ListaProductosFactura.Count - 1].Cantidad++;
                }
                if (!string.IsNullOrWhiteSpace(p.Serie))
                    ListaProductosFactura[ListaProductosFactura.Count - 1].Descripcion += p.Serie + " | ";
            }
            ListaProductosFactura[ListaProductosFactura.Count - 1].Descripcion += finalDescripcion;

            //FacturacionPrueba factura = new FacturacionPrueba();
            //MessageBox.Show("FACTURACIÓN DE PRUEBA");
            UtiFacturacion factura = new UtiFacturacion();

            int tipoTasaIVAid = Empresa.TipoTasaIVAId;
            decimal tasaOCuota = Empresa.TasaOCuota;

            string serie = Empresa.SerieFactura;//"AA";
            string uuid;
            if(RelacionaFactura)
                uuid = factura.Facturar33conRelacion(Empresa, Pedido, ListaProductosFactura, Cliente, Pedido.Factura, serie, FechaFactura, 
                                           TipoComprobante, UsoCFDI, FormaPago, MedioPago, CondicionPago,
                                           NumeroCuenta, pathClienteDirectorioFacturas,
                                           CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, Observaciones,"04",UUIDRelacionado);
            else
                uuid = factura.Facturar33(Empresa, Pedido, ListaProductosFactura, Cliente, Pedido.Factura, serie, FechaFactura,
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

            List<EntProducto> productosDetalle = ListaProductos;
            ListaProductos = new BusProductos().ObtieneProductosPorPedido(Pedido.Id);

            foreach (EntProducto p in ListaProductos)
            {
                p.Descripcion = p.Descripcion.PadRight(100, '.');
                p.Descripcion += " ";

                foreach (EntProducto pd in productosDetalle.Where(P => P.ProductoId == p.Id))
                {
                    if (!string.IsNullOrWhiteSpace(pd.Serie))
                        p.Descripcion += pd.Serie + " - ";
                }
            }
            //if (Program.UsuarioSeleccionado.Id > 1)
            //    ListaProductosFactura[ListaProductosFactura.Count - 1].Descripcion += "Solicitud:".PadLeft(100, '-') + txtBanco.Text;

            UtiFacturacionPruebas factura = new UtiFacturacionPruebas();
            MessageBox.Show("FACTURACIÓN DE PRUEBA");
            //UtiFacturacion factura = new UtiFacturacion();

            int tipoTasaIVAid = Empresa.TipoTasaIVAId;
            decimal tasaOCuota = Empresa.TasaOCuota;

            //if ((txtRFC.Text == "XAXX010101000" || chkFacturaPublicoGeneral.Checked) && Program.EmpresaSeleccionada.TipoPersonaId == 1)//Persona Fisica
            //{
            //    Empresa.TipoTasaIVAId = 2;//TASA 0%
            //    Empresa.TasaOCuota = 0.0m;//TASA 0%
            //}
            //else
            //{
            //    Empresa.TipoTasaIVAId = 1;//TASA 16%
            //    Empresa.TasaOCuota = 0.16m;//TASA 0%
            //}

            ////string uuid = factura.Facturar(Empresa, Pedido, ListaProductos, Cliente, Pedido.Factura, FechaFactura, FormaPago, MedioPago, CondicionPago,
            ////                               NumeroCuenta, pathClienteDirectorioFacturas,
            ////                               CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, Observaciones);
            //UtiFacturacion  FAC= new UtiFacturacion();
            string uuid = factura.Facturar33(Empresa, Pedido, ListaProductos, Cliente, Pedido.Factura, FechaFactura, 
                                           TipoComprobante, UsoCFDI, FormaPago, MedioPago, CondicionPago,
                                           NumeroCuenta, pathClienteDirectorioFacturas,
                                           CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, Observaciones);

            Empresa.TipoTasaIVAId = tipoTasaIVAid;
            Empresa.TasaOCuota = tasaOCuota;

             EntFactura fact = new EntFactura() { PedidoId = Pedido.Id, NumeroFactura = Pedido.Factura, UUID = uuid, Ruta = pathClienteDirectorioFacturas, Fecha = DateTime.Today };

            return fact;// pathClienteDirectorioFacturas;
        }


        /// <summary>
        /// Verifica que se halla seleccionado cliente.
        /// Revisa el SelectedIndex de cmbClientes. Si su valor es 0 o -1, Manda excepción y Focus() en cmbClientes.
        /// </summary>
        void VerificaClienteSeleccionado()
        {
            if (ClienteSeleccionado == null)
                throw new Exception("Seleccione un Cliente");
        }
        void VerificaProductosSeleccionados(List<EntProducto> ProductosSeleccionados)
        {
            if (ProductosSeleccionados == null || ProductosSeleccionados.Count == 0)
                throw new Exception("Agregue al menos un producto.");
        }
        

        private void Ventas_Load(object sender, EventArgs e)
        {
            try
            {
                InicializaPantalla();
                //CargaEmpresas();

                //if (Program.EmpresaSeleccionada == null)
                //    Program.EmpresaSeleccionada = SeleccionaEmpresa();
                //if (Program.EmpresaSeleccionada != null)
                //{
                    //cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
                //CargaProductos(Program.EmpresaSeleccionada.Id);

                ClienteSeleccionado = null;
                gvProductosPedido.DataSource = null;
                //}
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }
        
        private void txtProductoBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    ProductoSeleccionado = ObtieneProductoFromGV(gvProductosBusqueda);

                    if (ProductoSeleccionado != null)
                    {
                        AgregaProductoEnPedido(ProductoSeleccionado, 1);

                        this.ListaProductos.Remove(ProductoSeleccionado);
                        //CargaProductos(ListaProductos);
                        CargaProductos(ListaProductos);
                    }

                    gvProductosBusqueda.Visible = false;
                    LimpiaTextBox(pnlProductos);
                }
                else if (e.KeyChar == (char)Keys.Escape)
                {
                    gvProductosBusqueda.Visible = false;
                    LimpiaTextBox(pnlProductos);
                }

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ProductoSeleccionado=ObtieneProductoFromGV(gvProductosBusqueda);
                AgregaProductoEnPedido(ProductoSeleccionado, 1);

                ListaProductos.Remove(ProductoSeleccionado);
                //CargaProductos(ListaProductos);
                CargaProductos(ListaProductos);

                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProducto);
                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProductoCodigo);
                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProductoSerie);
                //gvProductos.Visible = false;
                //txtProductoBusqueda.Text = "";
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void txtProductoBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBuscaProducto.Text))
                {
                    gvProductosBusqueda.Visible = false;
                    //gvProductos.DataSource = ListaProductos;
                }
                else
                {
                    gvProductosBusqueda.DataSource = ListaProductos.Where(P => P.Descripcion.ToUpper().Contains(txtBuscaProducto.Text.ToUpper())).ToList();
                    gvProductosBusqueda.Visible = true;
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                FiltroProductos vProveedores = new FiltroProductos();
                vProveedores.CargaProductosDetalle(ListaProductos);
                if (vProveedores.ShowDialog() == DialogResult.OK)
                {
                    if (vProveedores.ProductoSeleccionado == null)
                        throw new Exception("Producto NO encontrado");

                    ProductoSeleccionado = vProveedores.ProductoSeleccionado;

                    AgregaProductoEnPedido(ProductoSeleccionado, 1);
                    ListaProductos.Remove(vProveedores.ProductoSeleccionado);

                    CargaProductos(ListaProductos);

                    OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProducto);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductosPedido_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CalculaSumaTotal(ObtieneListaProductosFromGV(gvProductosPedido), txtTotal);
            }catch(Exception ex) { MuestraMensaje(ex.Message, "ERROR"); }
        }
        bool ClientePublicoGeneral;

        private void gvProductosPedido_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    EntProducto productoRemueve = ObtieneProductoFromGV(gvProductosPedido);

                    AgregaProductoEnPedido(productoRemueve, -productoRemueve.Cantidad);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductosPedido_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.ColumnIndex == 3 || e.ColumnIndex == 4 ) && e.RowIndex > -1)//CANTIDAD | PRECIO SIN IVA
                {
                    EntProducto productoSeleccionado = ObtieneProductoFromGV(gvProductosPedido);
                    decimal iva = 1.16m;
                    if (e.ColumnIndex == 4)//CANTIDAD 
                    {
                        if (productoSeleccionado.Cantidad < 0)
                            productoSeleccionado.Cantidad = 0;

                        if (productoSeleccionado.Cantidad > productoSeleccionado.Existencia)
                        {
                            MuestraMensajeError("La Cantidad solicitada es mayor a la Existente. \n Existencia: "
                                                   + productoSeleccionado.Existencia, "ERROR");
                            productoSeleccionado.Cantidad = productoSeleccionado.Existencia;
                        }
                            //if (productoSeleccionado.TipoProductoId == 1)//1:PRODUCTO | 2:SERVICIO
                            //{
                            //if (!string.IsNullOrWhiteSpace(productoSeleccionado.Serie))
                            //{
                            //    productoSeleccionado.Cantidad = 1;
                            //    MandaExcepcion("PRODUCTOS CON SERIE NO SE PUEDEN VENDER EN CANTIDADES MAYORES A 1.");
                            //}

                            //List<EntProducto> listaProductosSeleccionados = this.ListaProductos.Where(P => P.ProductoId == productoSeleccionado.ProductoId).ToList();
                            //int existenciaProducto = listaProductosSeleccionados.Count() + 1;//COMPENSACION POR PRODUCTOSELECCIONADO
                            //int cantidadProductos = Convert.ToInt32(productoSeleccionado.Cantidad);
                            //if (productoSeleccionado.Cantidad > existenciaProducto)
                            //{
                            //    MuestraMensajeError("La Cantidad solicitada es mayor a la Existente. \n Existencia: " + existenciaProducto, "ERROR");
                            //    cantidadProductos = existenciaProducto;
                            //}
                            //for (int x = 0; x < cantidadProductos - 1; x++)
                            //{
                            //    AgregaRemueveProductoEnPedido(listaProductosSeleccionados[x], 1);
                            //}
                            //productoSeleccionado.Cantidad = 1;
                            //}
                        }
                    else if (e.ColumnIndex == 5)//PRECIO COSTO
                    {
                        //decimal precioSinIVA = productoSeleccionado.PrecioVentaSinIVA; // ConvierteTextoADecimal(gvProductosPedido.CurrentRow.Cells[6].Value.ToString());
                        //decimal precioIVA = Math.Round(precioSinIVA * iva, 2);

                        //productoSeleccionado.PrecioVenta = precioIVA;
                        ////gvProductosPedido.CurrentRow.Cells[7].Value = precioIVA;                    }
                    }
                    //gvProductosPedido.CurrentCell = gvProductosPedido[e.ColumnIndex + 1, gvProductosPedido.Rows.Count - 1];
                    //gvProductosPedido.BeginEdit(true);
                    gvProductosPedido.Refresh();
                    CalculaSumaTotal(this.ListaProductosPedido, txtTotal);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductosPedido_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string mensaje;

                EntEmpresa empresaSeleccionada = Program.EmpresaSeleccionada;
                
                //Pendiente
                //VerificaClienteSeleccionado();
                VerificaProductosSeleccionados(ListaProductosPedido);

                mensaje = "¿Desea dar Salida a los Productos seleccionados? ";

                if (MuestraMensajeYesNo(mensaje, "CONFIRMAR") == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    string detallePedido = "";
                    EntPedido pedidoAgrega = new EntPedido();

                    List<EntProducto> productosSeleccionados = ListaProductosPedido;

                    foreach (EntProducto p in productosSeleccionados)
                        detallePedido += p.Cantidad + " " + p.Descripcion + " | ";

                    int orientacion = 2;
                    int movimientoId = new BusPedidos().AgregaMovimientoMaster("", 2, orientacion, cmbAlmacenes.SelectedIndex + 1, 0, Program.UsuarioSeleccionado.Id);

                    foreach (EntProducto p in productosSeleccionados) {
                        new BusPedidos().AgregaMovimientoDetalle(movimientoId, p.Id, p.Cantidad, p.PrecioCosto);
                    }

                    new BusPedidos().AgregaMovimientoLote(movimientoId, orientacion);

                    btnCancelar.PerformClick();

                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("¡Entrada Registrada!");
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteSeleccionado = null;

                gvProductosPedido.DataSource = null;
                lbContadorSeries.Text = "0";

                CargaProductos(almacenId);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtCodigoProductoBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
                    gvProductosBusqueda.Visible = false;
                else
                {
                    gvProductosBusqueda.DataSource = ListaProductos.Where(P => P.Codigo.ToUpper().Contains(((TextBox)sender).Text.ToUpper())).ToList();
                    gvProductosBusqueda.Visible = true;
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtBuscaProductoSerie_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
                    gvProductosBusqueda.Visible = false;
                else
                {
                    gvProductosBusqueda.DataSource = ListaProductos.Where(P => P.Serie.ToUpper().Contains(((TextBox)sender).Text.ToUpper())).ToList();
                    gvProductosBusqueda.Visible = true;
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductosBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                if (gvProductosBusqueda.CurrentRow.Index != gvProductosBusqueda.Rows.Count-1)
                                    ProductoSeleccionado = ObtieneProductoAnteriorFromGV(gvProductosBusqueda);
                                    else 
                    ProductoSeleccionado = ObtieneProductoFromGV(gvProductosBusqueda);
                
                AgregaProductoEnPedido(ProductoSeleccionado, 1);

                ListaProductos.Remove(ProductoSeleccionado);
                //CargaProductos(ListaProductos);
                CargaProductos(ListaProductos);

                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProducto);
                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProductoCodigo);
                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProductoSerie);
            }
        }

        private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Program.CambiaEmpresa)
                {
                    Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                    //btnCancelar.PerformClick();

                    CargaProductos(almacenId);
                    btnCancelar.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnRefrescarProductos_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                CargaProductos(almacenId);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }


        private void btnRefrescaEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnBuscaEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                SeleccionaEmpresa vSeleccionaEmp = new Pantallas.SeleccionaEmpresa();
                if (vSeleccionaEmp.ShowDialog() == DialogResult.OK)
                {
                    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == vSeleccionaEmp.EmpresaSeleccionada.Id);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtDescuento_Leave(object sender, EventArgs e)
        {
            TextBoxDecimalMoney_Leave(sender, e);
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            try {
                CalculaSumaTotal(ListaProductosPedido, txtTotal);
            } catch(Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbAlmacenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                CargaProductos(almacenId);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }
    }
}
