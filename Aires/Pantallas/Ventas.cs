using AiresEntidades;
using AiresNegocio;
using AiresUtilerias;
//using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aires.Pantallas
{
    public partial class Ventas : FormBase
    {
        public void VerificaEmpresa()
        {
            cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }

        public Ventas()
        {
            InitializeComponent();
        }

        void InicializaPantalla()
        {
            cmbFormaPago.SelectedIndex = 0;
            cmbMetodoPago.SelectedIndex = 0;
            gvProductosPedido.DataSource = null;
            //MuestraUltimaFactura(txtNumeroFactura);

            if (Program.EmpresaSeleccionada != null && cmbEmpresas.Items.Count > 0)
                cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }

        EntCliente ClienteSeleccionado = new EntCliente();
        EntProducto ProductoSeleccionado;
        List<EntProducto> ListaProductos = new List<EntProducto>();
        List<EntCliente> ListaClientes = new List<EntCliente>();

        List<EntProducto> ListaProductosPedido { get { return ObtieneListaProductosFromGV(gvProductosPedido); } }
        EntProducto ProductoPedidoSeleccionado { get { return ObtieneProductoFromGV(gvProductosPedido); } }

        #region Metodos

            public void CargaEmpresas()
            {
                Program.CambiaEmpresa = false;
                cmbEmpresas.DataSource = new BusEmpresas().ObtieneEmpresas();
                Program.CambiaEmpresa = true;
            }
            //public void CargaClientes(int EmpresaId)
            //{
            //    ListaClientes = new BusClientes().ObtieneClientes(EmpresaId);
            //    gvClientes.DataSource = ListaClientes;
            //}
            //public void CargaProductos(int EmpresaId)
            //{
            //    ListaProductos = new BusProductos().ObtieneProductos(EmpresaId);
            //    gvProductosBusqueda.DataSource = ListaProductos;
            //}

            public void CargaClientes(int EmpresaId)
            {
                ListaClientes = new BusClientes().ObtieneClientes(EmpresaId).OrderBy(P => P.Nombre).ToList();
                gvClientes.DataSource = ListaClientes;
            }
            //public void CargaProductos()
            //{
            //    ListaProductos = new BusProductos().ObtieneProductos().OrderBy(P => P.Descripcion).ToList();
            //    gvProductosBusqueda.DataSource = ListaProductos;
            //}
            //public void CargaProductos(List<EntProducto> ListaProductos)
            //{
            //    this.ListaProductos = ListaProductos;
            //    gvProductosBusqueda.DataSource = ListaProductos;
            //}
            public void CargaProductosDetalle(int EmpresaId)
           {
                ListaProductos = new BusProductos().ObtieneProductosDetalle(EmpresaId);//.OrderBy(P => P.Descripcion).ToList();
                gvProductosBusqueda.DataSource = ListaProductos;
            }
            public void CargaProductosDetalle(List<EntProducto> ListaProductos)
            {
                this.ListaProductos = ListaProductos;
                gvProductosBusqueda.DataSource = ListaProductos;
            }
            void CargaVentasEnPantallas()
            {
                Form vRegVent = BuscaFormaBase(new Registros().Titulo);
                if (vRegVent != null)
                    ((Registros)vRegVent).CargaGvPedidos(Program.EmpresaSeleccionada.Id);
            }

            /// <summary>
            /// Agrega nuevo registro del Pedido solicitado.
            /// </summary>
            /// <param name="pedido"></param>
            public EntPedido AgregarPedido(int ClienteId, string Detalle, string Observaciones, decimal Total, decimal Pago, DateTime Fecha, DateTime FechaEntrega, int EmpleadoId, bool Facturado, int EstatusId)
            {
                EntPedido pedido = new EntPedido()
                {
                    ClienteId = ClienteId,
                    Detalle = Detalle,
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
            void AgregarProductoDetallePedido(int PedidoId, int ProductoId, int Cantidad, decimal PrecioCosto, decimal PrecioVenta)
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
            void AgregarPago(int PedidoId, decimal Cantidad)
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
            /// <summary>
            /// 
            /// </summary>
            /// <param name="pedido"></param>
            void AgregarFacturaPedido(int PedidoId, string UUID, string Ruta)
            {
                EntFactura factura = new EntFactura()
                {
                    PedidoId = PedidoId,
                    UUID=UUID,
                    Fecha = DateTime.Today,
                    Ruta=Ruta
                };
                new BusPedidos().AgregaFactura(factura);
            }

        void AgregaProductoEnPedido(EntProducto ProductoSeleccionado, int CantidadAgrega)
        {
            List<EntProducto> productosPedido = ObtieneListaProductosFromGV(gvProductosPedido);

            if (CantidadAgrega > 0)
            {
                if (productosPedido == null)
                    productosPedido = new List<EntProducto>();

                //ProductoSeleccionado = ObtieneProductosFromGV(gvProductos);
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
        }

        void AgregarFacturaPedido(EntFactura Factura)
        {
            new BusFacturas().AgregaFactura(Factura);
        }
        void TomaDatosCliente(EntCliente Cliente)
        {
            if (Cliente == null)
                Cliente = new EntCliente();
            Cliente.Nombre = txtNombre.Text;
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
        EntCliente ObtieneCliente(int ClienteId)
        {
            List<EntCliente> clientes = ListaClientes.Where(P => P.Id == ClienteId).ToList();

            if (clientes.Count == 0)
                throw new Exception("Cliente NO encontrado");

            return clientes[0];
        }
        void ActualizaEstatusFacturaPedido(int FacturaId, int EstatusId)
        {
            EntFactura fac = new EntFactura()
            {
                Id = FacturaId,
                EstatusId = EstatusId
            };
            new BusFacturas().ActualizaEstatusFacturaPedido(fac);
        }

        void MuestraUltimaFactura(TextBox TxtMuestraFactura)
        {
            int ultimaFactura = ConvierteTextoAInteger(new BusFacturas().ObtieneUltimaFactura().NumeroFactura);
            ultimaFactura++;
            //pedidoAgrega.Factura = ultimaFactura.ToString();
            TxtMuestraFactura.Text = ultimaFactura.ToString();
        }

        void AumentaPagoPedido(int PedidoId, decimal Pago)
            {
                EntPedido pedido= new EntPedido()
                {
                    Id= PedidoId,
                    Pago=Pago,
                    Fecha = DateTime.Today
                };
                new BusPedidos().AumentaPagoPedido(pedido);
            }

            void CargaDatosCliente(EntCliente Cliente)
            {
                txtNombre.Text = Cliente.Nombre;
                txtNombreFiscal.Text = Cliente.NombreFiscal;
                txtRFC.Text = Cliente.RFC;

                //txtDireccion.Text = Cliente.Direccion;
                //txtTelefono.Text = Cliente.Telefono;
                //txtTelefono2.Text = Cliente.Telefono2;
                //txtCelular.Text = Cliente.Celular;
                txtEmail.Text = Cliente.Email;

                txtNoExterior.Text = Cliente.NoExterior;
                txtNoInterior.Text = Cliente.NoInterior;
                txtCalle.Text = Cliente.Calle;
                txtColonia.Text = Cliente.Colonia;
                txtLocalidad.Text = Cliente.Localidad;
                txtMunicipio.Text = Cliente.Municipio;
                txtEstado.Text = Cliente.Estado;
                txtCP.Text = Cliente.CP;
                //txtBanco.Text = Cliente.Banco;
                //txtNumeroCuenta.Text = Cliente.NumeroCuenta;
                //txtSucursal.Text = Cliente.Sucursal;
                //txtCLABE.Text = Cliente.CLABE;
                //txtNumeroReferencia.Text = Cliente.NumeroReferencia;
            }
            void CargaDatosClientePubicoGeneral(EntCliente Cliente)
            {
                //txtNombre.Text = Cliente.Nombre;
                txtNombreFiscal.Text = Cliente.NombreFiscal;
                txtRFC.Text = Cliente.RFC;

                //txtDireccion.Text = Cliente.Direccion;
                //txtTelefono.Text = Cliente.Telefono;
                //txtTelefono2.Text = Cliente.Telefono2;
                //txtCelular.Text = Cliente.Celular;
                txtEmail.Text = Cliente.Email;

                txtNoExterior.Text = Cliente.NoExterior;
                txtNoInterior.Text = Cliente.NoInterior;
                txtCalle.Text = Cliente.Calle;
                txtColonia.Text = Cliente.Colonia;
                txtLocalidad.Text = Cliente.Localidad;
                txtMunicipio.Text = Cliente.Municipio;
                txtEstado.Text = Cliente.Estado;
                txtCP.Text = Cliente.CP;
                //txtBanco.Text = Cliente.Banco;
                //txtNumeroCuenta.Text = Cliente.NumeroCuenta;
                //txtSucursal.Text = Cliente.Sucursal;
                //txtCLABE.Text = Cliente.CLABE;
                //txtNumeroReferencia.Text = Cliente.NumeroReferencia;
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
                decimal total, subtotal, cantidadIva;

                total = ListaProductos.Sum(P => P.Precio);
                subtotal = total / (1 + IVA);
                cantidadIva = subtotal * IVA;
                cantidadIva = total-subtotal ;

                TxtMuestraTotal.Text = FormatoMoney(total);
                txtSubtotal.Text = FormatoMoney(subtotal);
                txtIVA.Text = FormatoMoney(cantidadIva);
                txtRestante.Text = FormatoMoney(total-ConvierteTextoADecimal(txtPago.Text));
            }
        
        #endregion

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
            if (ProductosSeleccionados==null ||ProductosSeleccionados.Count == 0)
                throw new Exception("Agregue al menos un producto.");
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
            new UtiCorreo().EnviaCorreo("" + asunto, new List<string>() { Cliente.Email, Cliente.Email2, Cliente.Email3 }, mensaje, archivosAdjuntos);

            MessageBox.Show("El Correo se ha Enviado correctamente, a la(s) dirección(es): \n " + Cliente.Email + " \n " + Cliente.Email2 + " \n " + Cliente.Email3);
            //}
        }
        void CargaClientesDeudaEnPantallas()
        {
            Form vCli = BuscaFormaBase(new ClientesCredito().Titulo);
            if (vCli != null)
            {
                ((ClientesCredito)vCli).CargaGvClientesCredito();
                ((ClientesCredito)vCli).CargaGvPedidosClientesCredito();
            }
        }
        //string ObtieneUltimaFactura()
        //{
        //    int ultimaFactura = ConvierteTextoAInteger(new BusFacturas().ObtieneUltimaFactura().NumeroFactura);
        //    ultimaFactura++;

        //    return ultimaFactura.ToString();
        //}
        string ObtieneUltimaFactura(int EmpresaId)
        {
            int ultimaFactura = ConvierteTextoAInteger(new BusFacturas().ObtieneUltimaFactura(EmpresaId).NumeroFactura);
            ultimaFactura++;

            return ultimaFactura.ToString();
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

        string EncuentraArchivo(string Ruta,string Extension)
        {
            System.IO.DirectoryInfo di= new System.IO.DirectoryInfo(Ruta);
            
            System.IO.FileInfo [] fi = di.GetFiles();
            foreach(System.IO.FileInfo f in fi)
            {
                if (f.Extension == Extension)
                    return f.Name;
            }
            return "";
        }
        
        EntFactura EnviarPreFactura(EntEmpresa Empresa, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente, DateTime FechaFactura,
                                    string FormaPago, string MedioPago, string CondicionPago, string NumeroCuenta,
                                    decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS)
        {
            string pathClienteDirectorio = PathFacturas + "\\" + Cliente.Nombre;
            if (!System.IO.Directory.Exists(pathClienteDirectorio))
                System.IO.Directory.CreateDirectory(pathClienteDirectorio);

            string pathClienteDirectorioFacturas = pathClienteDirectorio + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss");
            System.IO.Directory.CreateDirectory(pathClienteDirectorioFacturas);

            UtiFacturacionPruebas factura = new UtiFacturacionPruebas();
            MessageBox.Show("ENVÍO PRE-FACTURA");



            List<EntProducto> productosDetalle =ListaProductos;

            //ListaProductos = new BusProductos().ObtieneProductosPorPedido(Pedido.Id);

            List<EntProducto> ListaProductosFactura = new List<EntProducto>();
            string codigo="";
            int cantidad = 1;
            foreach(EntProducto p in productosDetalle.OrderBy(P=>P.Codigo).ToList())
            {
                if (p.Codigo != codigo)
                {
                    EntProducto pneue = new EntProducto() { Id = p.Id, Codigo=p.Codigo, Serie=p.Serie, Descripcion=p.Descripcion, TipoUnidad=p.TipoUnidad, Cantidad=p.Cantidad, PrecioVenta = p.PrecioVenta, ProductoId = p.ProductoId };
                    //pneue.Descripcion = p.Descripcion.PadRight(100, '°');
                    pneue.Descripcion += " ";

                    ListaProductosFactura.Add(pneue);
                    codigo = pneue.Codigo;
                    cantidad = 1;
                }
                else
                {
                    cantidad++;
                    ListaProductosFactura[ListaProductosFactura.Count - 1].Cantidad++;
                }
                if (!string.IsNullOrWhiteSpace(p.Serie))
                    ListaProductosFactura[ListaProductosFactura.Count - 1].Descripcion += p.Serie + " | ";
            }

            //foreach (EntProducto p in ListaProductosFactura)
            //{
            //    p.Descripcion = p.Descripcion.PadRight(100, '.');
            //    p.Descripcion += " ";

            //    foreach (EntProducto pd in productosDetalle.Where(P => P.ProductoId == p.Id))
            //    {
            //        if (!string.IsNullOrWhiteSpace(pd.Serie))
            //            p.Descripcion += pd.Serie + " | ";
            //    }
            //}
            //Pedido.Factura = txtNumeroFactura.Text;
            string uuid = factura.Facturar(Empresa, Pedido, ListaProductosFactura, Cliente, Pedido.Factura, FechaFactura, FormaPago, MedioPago, CondicionPago,
                                            NumeroCuenta, pathClienteDirectorioFacturas,
                                            CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS);
            EntFactura fact = new EntFactura() { PedidoId = Pedido.Id, NumeroFactura = Pedido.Factura, UUID = uuid, Ruta = pathClienteDirectorioFacturas, Fecha = DateTime.Today };

            return fact;
        }

        EntFactura EnviarFactura(EntEmpresa Empresa, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente, DateTime FechaFactura,
                                    string FormaPago, string MedioPago, string CondicionPago, string NumeroCuenta,
                                    decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS)
        {
            string pathClienteDirectorio = PathFacturas + "\\" + Cliente.Nombre;
            if (!System.IO.Directory.Exists(pathClienteDirectorio))
                System.IO.Directory.CreateDirectory(pathClienteDirectorio);

            string pathClienteDirectorioFacturas = pathClienteDirectorio + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss");
            System.IO.Directory.CreateDirectory(pathClienteDirectorioFacturas);

            //FacturacionPrueba factura = new FacturacionPrueba();
            //MessageBox.Show("FACTURACIÓN DE PRUEBA");
            UtiFacturacion factura = new UtiFacturacion();
            
            List<EntProducto> productosDetalle = ListaProductos;
            ListaProductos = new BusProductos().ObtieneProductosPorPedido(Pedido.Id);
          
            foreach (EntProducto p in ListaProductos)
            {
                p.Descripcion = p.Descripcion.PadRight(100, '.');
                p.Descripcion += " ";

                foreach (EntProducto pd in productosDetalle.Where(P => P.ProductoId == p.Id))
                {
                    if (!string.IsNullOrWhiteSpace(pd.Serie))
                        p.Descripcion += pd.Serie +" | ";
                }
            }

            string uuid = factura.Facturar(Empresa, Pedido, ListaProductos, Cliente, Pedido.Factura, FechaFactura, FormaPago, MedioPago, CondicionPago,
                                           NumeroCuenta, pathClienteDirectorioFacturas,
                                           CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS);
            
            EntFactura fact = new EntFactura() { PedidoId = Pedido.Id, NumeroFactura = Pedido.Factura, UUID = uuid, Ruta = pathClienteDirectorioFacturas, Fecha = DateTime.Today };

            return fact;// pathClienteDirectorioFacturas;
        }
        EntFactura EnviarFacturaPrueba(EntEmpresa Empresa, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente, DateTime FechaFactura,
                                   string FormaPago, string MedioPago, string CondicionPago, string NumeroCuenta,
                                   decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS)
        {
            string pathClienteDirectorio = PathFacturas + "\\" + Cliente.Nombre;
            if (!System.IO.Directory.Exists(pathClienteDirectorio))
                System.IO.Directory.CreateDirectory(pathClienteDirectorio);

            string pathClienteDirectorioFacturas = pathClienteDirectorio + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss");
            System.IO.Directory.CreateDirectory(pathClienteDirectorioFacturas);

            UtiFacturacionPruebas factura = new UtiFacturacionPruebas();
            MessageBox.Show("FACTURACIÓN DE PRUEBA");
            //UtiFacturacion factura = new UtiFacturacion();

            List<EntProducto> productosDetalle = ListaProductos;
            ListaProductos = new BusProductos().ObtieneProductosPorPedido(Pedido.Id);

            foreach (EntProducto p in ListaProductos)
            {
                p.Descripcion = p.Descripcion.PadRight(100, '.');
                p.Descripcion += " ";

                foreach (EntProducto pd in productosDetalle.Where(P => P.ProductoId == p.Id))
                {
                    if (!string.IsNullOrWhiteSpace(pd.Serie))
                        p.Descripcion += pd.Serie + " | ";
                }
            }

            string uuid = factura.Facturar(Empresa, Pedido, ListaProductos, Cliente, Pedido.Factura, FechaFactura, FormaPago, MedioPago, CondicionPago,
                                           NumeroCuenta, pathClienteDirectorioFacturas,
                                           CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS);

            EntFactura fact = new EntFactura() { PedidoId = Pedido.Id, NumeroFactura = Pedido.Factura, UUID = uuid, Ruta = pathClienteDirectorioFacturas, Fecha = DateTime.Today };

            return fact;// pathClienteDirectorioFacturas;
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            try
            {
                InicializaPantalla();
                CargaEmpresas();

                if (Program.EmpresaSeleccionada == null)
                    Program.EmpresaSeleccionada = SeleccionaEmpresa();
                if (Program.EmpresaSeleccionada != null)
                {
                    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
                    CargaClientes(Program.EmpresaSeleccionada.Id);
                    CargaProductosDetalle(Program.EmpresaSeleccionada.Id);
                    ClienteSeleccionado = null;
                }
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

                        ListaProductos.Remove(ProductoSeleccionado);
                        //CargaProductos(ListaProductos);
                        CargaProductosDetalle(ListaProductos);
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
                CargaProductosDetalle(ListaProductos);

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
                    //ListaProductos.IndexOf(ProductoSeleccionado);
                    ListaProductos.Remove(vProveedores.ProductoSeleccionado);

                    //CargaProductos(ListaProductos);
                    CargaProductosDetalle(ListaProductos);

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
        
        private void txtClienteBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtClienteBusqueda.Text))
                    gvClientes.Visible = false;
                else
                {
                    List<EntCliente> clientesEncontrados = ListaClientes.Where(P => P.Nombre.ToUpper().Contains(txtClienteBusqueda.Text.ToUpper())).ToList();
                    gvClientes.DataSource = clientesEncontrados;
                    gvClientes.Visible = true;

                    AjustaAlturaGrid(gvClientes,clientesEncontrados.Count, AlturaMaximaGrid);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtClienteBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    //EntCliente clienteSeleccionado = ObtieneClienteFromGV(gvClientes);
                    ClienteSeleccionado= ObtieneClienteFromGV(gvClientes);

                    if (ClienteSeleccionado != null)
                    {
                        CargaDatosCliente(ClienteSeleccionado);
                        chkFacturaPublicoGeneral.Checked = false;
                    }

                    OcultaBuscadorGrid(gvClientes, txtClienteBusqueda);
                }
                else if (e.KeyChar == (char)Keys.Escape)
                {
                    OcultaBuscadorGrid(gvClientes, txtClienteBusqueda);
                }

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ClienteSeleccionado = ObtieneClienteFromGV(gvClientes);

                if (ClienteSeleccionado != null)
                {
                    CargaDatosCliente(ClienteSeleccionado);
                    chkFacturaPublicoGeneral.Checked = false;
                }

                OcultaBuscadorGrid(gvClientes, txtClienteBusqueda);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                FiltroClientes vClientes = new FiltroClientes();
                if (vClientes.ShowDialog() == DialogResult.OK)
                {
                    ClienteSeleccionado = vClientes.ClienteSeleccionado;
                    CargaDatosCliente(ClienteSeleccionado);
                    OcultaBuscadorGrid(gvClientes, txtClienteBusqueda);

                    chkFacturaPublicoGeneral.Checked = false;
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

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
                if ((e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 7) && e.RowIndex > -1)
                {
                  //  EntProducto productoSeleccionado = ObtieneProductoFromGV(gvProductosPedido);
                    decimal iva = 1.16m;
                    if (e.ColumnIndex == 6)
                    {
                        decimal precioSinIVA = ConvierteTextoADecimal(gvProductosPedido.CurrentRow.Cells[6].Value.ToString());
                        decimal precioIVA = Math.Round( precioSinIVA * iva,2);
                        gvProductosPedido.CurrentRow.Cells[7].Value = precioIVA;
                    }else if (e.ColumnIndex == 7)
                    {
                        decimal precioIVA = (decimal)gvProductosPedido.CurrentRow.Cells[7].Value;
                        decimal precioSinIVA = Math.Round( precioIVA / iva);
                        gvProductosPedido.CurrentRow.Cells[6].Value = precioSinIVA;
                    }

                    gvProductosPedido.CurrentRow.Cells[gvProductosPedido.ColumnCount - 1].Selected = true;

                    CalculaSumaTotal(ListaProductosPedido, txtTotal);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        
        private void txtPago_TextChanged(object sender, EventArgs e)
        {
            try {
                txtRestante.Text = FormatoMoney(ConvierteTextoADecimal(txtTotal.Text) - ConvierteTextoADecimal(txtPago.Text));
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void chkFacturar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkFacturar.Checked)
                    VerificaProductosSeleccionados(ListaProductosPedido);

                pnlFacturacion.Visible = chkFacturar.Checked;
            }
            catch (Exception ex) {
                chkFacturar.Checked = false;
                MuestraExcepcion(ex);
            }
        }

        private void cmbMetodoPago_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                //txtMetodoPago.Text=cmbFormaPago.Text.Remove(2,cmbFormaPago.Text.Length-2);
                txtFormaPago.Text = cmbFormaPago.Text.Remove(2, cmbFormaPago.Text.Length - 2);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbFormaPago_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                //txtFormaPago.Text = cmbMetodoPago.Text;
                txtMetodoPago.Text = cmbMetodoPago.Text;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                EntEmpresa empresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                string mensaje;

                //Pendiente
                VerificaClienteSeleccionado();
                VerificaProductosSeleccionados(ListaProductosPedido);

                if (chkFacturar.Checked)
                    mensaje = "Se realizará la Factura \n Núm: " + ObtieneUltimaFactura(empresaSeleccionada.Id) + "\n RFC: " + ClienteSeleccionado.RFC + "\n ¿Correcto ? ";
                else
                    mensaje = "Se guardará el Pedido sin Facturar \n ¿Correcto ? ";

                if (MuestraMensajeYesNo(mensaje, "CONFIRMAR") == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    string detallePedido = "";
                    EntPedido pedidoAgrega = new EntPedido();

                    List<EntProducto> productosSeleccionados = ListaProductosPedido;

                    //VerificaProductosSeleccionados(productosSeleccionados);

                    foreach (EntProducto p in productosSeleccionados)
                        detallePedido += p.Cantidad + " " + p.Descripcion + " | ";

                    decimal cantidadIVA = ConvierteTextoADecimal(txtIVA.Text);
                    decimal cantidadIVARetenido = 0;//ConvierteTextoADecimal(txtIVARetenido);
                    decimal cantidadISRRetenido = 0;//ConvierteTextoADecimal(txtISRRetenido);
                    decimal cantidadIEPS = 0;// ConvierteTextoADecimal(textBox1.Text);

                    pedidoAgrega = AgregarPedido(ClienteSeleccionado.Id, detallePedido.Remove(detallePedido.Length - 2), "", ConvierteTextoADecimal(txtTotal.Text), ConvierteTextoADecimal(txtPago.Text), DateTime.Now, DateTime.Today, 0, chkFacturar.Checked, 1);

                    foreach (EntProducto p in productosSeleccionados)
                    {
                        AgregarProductoDetallePedido(pedidoAgrega.Id, p.Id, p.Cantidad, p.PrecioCosto, p.PrecioVenta);
                        Productos vProd = new Productos();
                        if (p.TipoProductoId == 1)
                        {
                            vProd.ActualizaEstatusProductoDetalle(p, 2);//ESTATUS:2=ENTREGADO
                            vProd.AumentaProducto(p.ProductoId, -p.Cantidad);
                        }
                    }

                    decimal pago = ConvierteTextoADecimal(txtPago.Text);
                    if (pago > 0)
                    {
                        if (pago > ConvierteTextoADecimal(txtTotal.Text))
                        {
                            MuestraMensaje("El Pago debe ser menor al Total a pagar. \n El Pago NO será registrado", "PAGO NO REGISTRADO");
                            AumentaPagoPedido(pedidoAgrega.Id, -pago);
                        }
                        else
                        {
                            AgregarPago(pedidoAgrega.Id, pago);
                            AumentaPagoPedido(pedidoAgrega.Id, 0);//SOLO PARA CAMBIAR ESTATUS. VERIFICA SI EL TOTAL DEL PEDIDO ESTA PAGADO, CAMBIA ESTATUS DE SER ASI.
                            if (pago < ConvierteTextoADecimal(txtTotal.Text))
                                CargaClientesDeudaEnPantallas();
                        }
                    }

                    if (chkFacturar.Checked)
                    {
                        bool facturado = false;
                        EntFactura factura = new EntFactura();

                        try
                        {
                            //int ultimaFactura = new BusPedidos().ObtieneUltimaFactura().Id;
                            //ultimaFactura++;
                            //pedidoAgrega.Factura = ultimaFactura.ToString();
                            pedidoAgrega.Factura = ObtieneUltimaFactura(empresaSeleccionada.Id);
                            pedidoAgrega.SubTotal = ConvierteTextoADecimal(txtSubtotal.Text);
                            pedidoAgrega.Total = ConvierteTextoADecimal(txtTotal.Text);

                            txtFormaPago.Text = cmbFormaPago.Text.Remove(2, cmbFormaPago.Text.Length - 2);
                            txtMetodoPago.Text = cmbMetodoPago.Text;

                            TomaDatosCliente(ClienteSeleccionado);

                            if(empresaSeleccionada.Facturacion)
                                factura = EnviarFactura(empresaSeleccionada, pedidoAgrega, productosSeleccionados, ClienteSeleccionado, DateTime.Now, txtFormaPago.Text, txtMetodoPago.Text, txtCondicionesPago.Text,
                                                    txtNumeroCuenta.Text,
                                                    cantidadIVA, cantidadIVARetenido, cantidadISRRetenido, cantidadIEPS);
                            else
                                factura = EnviarFacturaPrueba(empresaSeleccionada, pedidoAgrega, productosSeleccionados, ClienteSeleccionado, DateTime.Now, txtFormaPago.Text, txtMetodoPago.Text, txtCondicionesPago.Text,
                                                    txtNumeroCuenta.Text,
                                                    cantidadIVA, cantidadIVARetenido, cantidadISRRetenido, cantidadIEPS);

                            factura.EmpresaId = empresaSeleccionada.Id;
                            facturado = true;
                        }
                        catch (Exception ex)
                        {
                            foreach (EntProducto p in productosSeleccionados)
                            {
                                Productos vProd = new Productos();
                                vProd.ActualizaEstatusProductoDetalle(p, 1);//ESTATUS:1=ACTIVO
                                vProd.AumentaProducto(p.ProductoId, p.Cantidad);
                            }
                            ActualizaEstatusProductoDetallePedido(pedidoAgrega, false);//ESTATUS:0=CANCELADO
                            ActualizaEstatusPedido(pedidoAgrega, 0);//ESTATUS:0=CANCELADO

                            MuestraExcepcionFacturacion(ex);
                        }

                        //COMENTAR EN PRODUCCION
                        //facturado = true;
                        if (facturado)
                        {
                            //MuestraMensaje("¡El pedido fue FACTURADO satisfactoriamente!", "CONFIRMACIÓN PEDIDO FACTURADO");
                            Cursor.Current = Cursors.WaitCursor;

                            AgregarFacturaPedido(factura);

                            try
                            {
                                Cursor.Current = Cursors.WaitCursor;

                                EnviaCorreo(empresaSeleccionada, pedidoAgrega, ClienteSeleccionado, factura.Ruta);
                            }
                            catch (Exception ex)
                            {
                                MuestraExcepcion(ex, "Correo NO enviado.");
                            }

                            try
                            {
                                if (MuestraMensajeYesNo("¿Desea mostrar Factura?") == DialogResult.Yes)
                                {
                                    string nombreArchivo = EncuentraArchivo(factura.Ruta, ".pdf");
                                    MuestraArchivo(factura.Ruta, nombreArchivo);
                                }
                            }
                            catch (Exception ex) { MuestraExcepcion(ex, "ERROR AL MOSTRAR FACTURA"); }


                            LimpiaTextBox(pnlCliente);
                            LimpiaTextBox(pnlAgrega);
                            LimpiaTextBox(pnlFacturacion, true);
                            
                            ClienteSeleccionado = null;
                            chkFacturar.Checked = false;
                            chkFacturaPublicoGeneral.Checked = false;
                            gvProductosPedido.DataSource = null;
                            lbContadorSeries.Text = "0";

                            CargaVentasEnPantallas();
                            CargaClientes(empresaSeleccionada.Id);//SERA POR VOLVER A PONER LOS PARAMETROS???
                            CargaProductosDetalle(empresaSeleccionada.Id);

                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("¡Pedido Registrado y Facturado!");
                        }
                    }
                    else
                    {
                        LimpiaTextBox(pnlCliente);
                        LimpiaTextBox(pnlAgrega);
                        LimpiaTextBox(pnlFacturacion, true);
                                        
                        ClienteSeleccionado = null;
                        chkFacturar.Checked = false;
                        chkFacturaPublicoGeneral.Checked = false;
                        gvProductosPedido.DataSource = null;
                        lbContadorSeries.Text = "0";

                        CargaVentasEnPantallas();
                        CargaClientes(empresaSeleccionada.Id);
                        CargaProductosDetalle(empresaSeleccionada.Id);

                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("¡Pedido Registrado!");
                    }

                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiaTextBox(pnlCliente);
                LimpiaTextBox(pnlAgrega);
                LimpiaTextBox(pnlFacturacion, true);

                ClienteSeleccionado = null;
                chkFacturar.Checked = false;
                chkFacturaPublicoGeneral.Checked = false;
                gvProductosPedido.DataSource = null;
                lbContadorSeries.Text = "0";

                //CargaProductos();
                CargaProductosDetalle(Program.EmpresaSeleccionada.Id);
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
                CargaProductosDetalle(ListaProductos);

                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProducto);
                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProductoCodigo);
                OcultaBuscadorGrid(gvProductosBusqueda, txtBuscaProductoSerie);
            }
        }

        private void cmbFormaPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((Keys)e.KeyChar == Keys.Enter)
                {
                    this.GetNextControl((Control)sender, true).Focus();
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

                txtFormaPago.Text = cmbFormaPago.Text.Remove(2, cmbFormaPago.Text.Length - 2);
                txtMetodoPago.Text = cmbMetodoPago.Text;

                txtCondicionesPago.Enabled = true;
                txtNumeroCuenta.Enabled = true;
                if (chkFacturaPublicoGeneral.Checked)
                {
                    //EntCliente clientePublicoGeneral = ListaClientes.Where(P => P.Id == 10).ToList()[0];
                    //CargaDatosClientePubicoGeneral(clientePublicoGeneral);

                    EntCliente clientePublicoGeneral = new EntCliente();
                    //clientePublicoGeneral.Id = 0;
                    //clientePublicoGeneral.Nombre = "PUBLICO GENERAL";
                    if (ClienteSeleccionado != null)
                        clientePublicoGeneral.Nombre = ClienteSeleccionado.Nombre;
                    else
                        clientePublicoGeneral.Nombre = "PUBLICO GENERAL";
                    clientePublicoGeneral.NombreFiscal = "PUBLICO GENERAL";
                    clientePublicoGeneral.RFC = "XAXX010101000";
                    clientePublicoGeneral.Email = "pavel_tiim@hotmail.com";

                    CargaDatosCliente(clientePublicoGeneral);

                    //ClienteSeleccionado = clientePublicoGeneral;

                    CalculaSumaTotal(ListaProductosPedido, txtTotal);
                }
                else if (ClienteSeleccionado != null)
                    CargaDatosCliente(ClienteSeleccionado);
                else
                    CargaDatosCliente(new EntCliente());
                //ClienteSeleccionado = null;


                    //pnlProductos.Enabled = chkFacturaPublicoGeneral.Checked;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtIVA_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Program.CambiaEmpresa)
                {
                    Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                    CargaClientes(Program.EmpresaSeleccionada.Id);
                    //btnCancelar.PerformClick();
                    CargaProductosDetalle(Program.EmpresaSeleccionada.Id);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnPreFactura_Click(object sender, EventArgs e)
        {
            try
            {
                if (MuestraMensajeYesNo("Se realizará la Pre-Factura \n RFC: " + ClienteSeleccionado.RFC + "\n ¿Correcto?", "CONFIRMAR") == DialogResult.Yes)
                {

                    Cursor.Current = Cursors.WaitCursor;

                    string detallePedido = "";
                    EntPedido pedidoAgrega = new EntPedido();
                    EntEmpresa empresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);

                    List<EntProducto> productosSeleccionados = ListaProductosPedido;

                    VerificaProductosSeleccionados(productosSeleccionados);

                    txtFormaPago.Text = cmbFormaPago.Text.Remove(2, cmbFormaPago.Text.Length - 2);
                    txtMetodoPago.Text = cmbMetodoPago.Text;

                    foreach (EntProducto p in productosSeleccionados)
                        detallePedido += p.Cantidad + " " + p.Descripcion + " | ";

                    decimal cantidadIVA = ConvierteTextoADecimal(txtIVA.Text);
                    decimal cantidadIVARetenido = 0;//ConvierteTextoADecimal(txtIVARetenido);
                    decimal cantidadISRRetenido = 0;//ConvierteTextoADecimal(txtISRRetenido);
                    decimal cantidadIEPS = ConvierteTextoADecimal(txtEmail2.Text);
                    
                    bool facturado = false;
                    EntFactura factura = new EntFactura();

                    try
                    {
                        TomaDatosCliente(ClienteSeleccionado);
                        pedidoAgrega.SubTotal = ConvierteTextoADecimal(txtSubtotal.Text);
                        pedidoAgrega.Total = ConvierteTextoADecimal(txtTotal.Text);
                        pedidoAgrega.Factura = ObtieneUltimaFactura(empresaSeleccionada.Id);
                        factura = EnviarPreFactura(empresaSeleccionada, pedidoAgrega, productosSeleccionados, ClienteSeleccionado, DateTime.Now, txtFormaPago.Text, txtMetodoPago.Text, txtCondicionesPago.Text,
                                                txtNumeroCuenta.Text,
                                                cantidadIVA, cantidadIVARetenido, cantidadISRRetenido, cantidadIEPS);
                        facturado = true;
                    }
                    catch (Exception ex)
                    {
                        MuestraExcepcionPreFacturacion(ex);
                        //MuestraExcepcion(ex, "Error en Pre-Factura");
                    }

                    if (facturado)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        
                        try
                        {
                            string nombreArchivo = EncuentraArchivo(factura.Ruta, ".pdf");
                            MuestraArchivo(factura.Ruta, nombreArchivo);
                        }
                        catch (Exception ex) { MuestraExcepcion(ex, "ERROR AL MOSTRAR FACTURA"); }
                    }
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
    }
}
