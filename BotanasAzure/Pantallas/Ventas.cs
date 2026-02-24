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

        public Ventas(EntEmpresa Empresa, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente
                     , int FormaPagoId, int MedioPagoId, string CondicionPago, string NumeroCuenta)
        {
            InitializeComponent();
            CargaDatosCliente(Cliente);
            CargaProductosPedido(ListaProductos);
            CargaDatosFactura(FormaPagoId, MedioPagoId, CondicionPago, NumeroCuenta);
            CalculaSumaTotal(ListaProductos, txtTotal);
        }

        void InicializaPantalla()
        {
            cmbFormaPago.SelectedIndex = 0;
            cmbMetodoPago.SelectedIndex = 0;
            gvProductosPedido.DataSource = null;
            //MuestraUltimaFactura(txtNumeroFactura);
            //if (Program.UsuarioSeleccionado.Id == 8 || Program.UsuarioSeleccionado.Id == 9)
            pnlSolicitud.Visible = true;

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
            if (Program.UsuarioSeleccionado.Id > 1)
                cmbEmpresas.DataSource = new BusEmpresas().ObtieneEmpresas().Where(P => P.UsuarioId == Program.UsuarioSeleccionado.Id).ToList();
            else
                cmbEmpresas.DataSource = new BusEmpresas().ObtieneEmpresas();
            //cmbEmpresas.DataSource = new BusEmpresas().ObtieneEmpresas();
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
        public void CargaProductosPedido(List<EntProducto> ListaProductosPedido)
        {
            gvProductosPedido.DataSource = ListaProductosPedido;
        }
        public void CargaDatosFactura(int FormaPagoId, int MedioPagoId, string CondicionPago, string NumeroCuenta)
        {
            cmbFormaPago.SelectedIndex = FormaPagoId - 1;
            cmbMetodoPago.SelectedIndex = MedioPagoId - 1;
            txtCondicionesPago.Text = CondicionPago;
            txtNumeroCuenta.Text = NumeroCuenta;

        }
        void CargaVentasEnPantallas()
        {
            //Form vRegVent = BuscaFormaBase(new Registros().Titulo);
            //if (vRegVent != null)
            //    ((Registros)vRegVent).CargaGvPedidos(Program.EmpresaSeleccionada.Id);
        }

        void PoneSolicitud()
        {
            if (ObtieneListaProductosFromGV(gvProductosPedido)
                .Where(P => P.TipoProductoId == 1).Count() > 0)
            {
                EntProducto productoPedido = ObtieneListaProductosFromGV(gvProductosPedido)
                    .Where(P => P.TipoProductoId == 1).Last();

                //productosPedido[productosPedido.Count - 1].Descripcion = productosPedido[productosPedido.Count - 1].Descripcion.Replace("SOLICITUD:".PadLeft(5, '-') + txtSolicitud.Text, "")
                //    .Replace("CONTRATO: NO-LM-004-20", "");
                //productosPedido[productosPedido.Count - 1].Descripcion += " SOLICITU:".PadLeft(5, '-') + txtSolicitud.Text 
                //    + " CONTRATO: NO-LM-004-20";
                productoPedido.Descripcion = productoPedido.Descripcion
                    .Replace("GARANTIA () AÑO(S) EN TODAS SUS PARTES", "")
                    .Replace("SOLICITUD:".PadLeft(5, '-') + txtSolicitud.Text, "")
                    .Replace("CONTRATO: NO-LM-004-20", "");
                productoPedido.Descripcion += ". CONTRATO: NO-LM-004-20 |"
                    + " SOLICITUD:".PadLeft(5, '-') + txtSolicitud.Text + " |"
                    + " GARANTIA () AÑO(S) EN TODAS SUS PARTES";
            }
            gvProductosPedido.Refresh();
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
                PedidoId = PedidoId,
                TipoPagoId = 1,
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
                if (!string.IsNullOrWhiteSpace(ProductoSeleccionado.Marca))
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

        public void AumentaPagoPedido(int PedidoId, decimal Pago)
        {
            EntPedido pedido = new EntPedido()
            {
                Id = PedidoId,
                Pago = Pago,
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
            txtEmail2.Text = Cliente.Email2;
            txtEmail3.Text = Cliente.Email3;

            txtNoExterior.Text = Cliente.NoExterior;
            txtNoInterior.Text = Cliente.NoInterior;
            txtCalle.Text = Cliente.Calle;
            txtColonia.Text = Cliente.Colonia;
            txtLocalidad.Text = Cliente.Localidad;
            txtMunicipio.Text = Cliente.Municipio;
            txtEstado.Text = Cliente.Estado;
            txtCP.Text = Cliente.CP;
            txtSolicitud.Text = Cliente.Banco;
            txtNumeroCuenta.Text = Cliente.NumeroCuenta;
            //txtSucursal.Text = Cliente.Sucursal;
            //txtCLABE.Text = Cliente.CLABE;
            //txtNumeroReferencia.Text = Cliente.NumeroReferencia;
            if (Cliente.FormaPagoId > 0)
                cmbFormaPago.SelectedIndex = Cliente.FormaPagoId - 1;

            if (Cliente.RFC == "XAXX010101000")
                ClientePublicoGeneral = true;
            else
                ClientePublicoGeneral = false;
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
            decimal total, subtotal, cantidadIva, descuento;
            decimal cantidadIVARetenido = 0;

            if (ListaProductos == null)
            {
                total = 0;
                subtotal = 0;
                cantidadIva = 0;
            }
            else
            {
                total = ListaProductos.Sum(P => P.Precio);

                ////if (txtRFC.Text == "XAXX010101000" || chkFacturaPublicoGeneral.Checked)
                //if ((ClientePublicoGeneral || chkFacturaPublicoGeneral.Checked) && Program.EmpresaSeleccionada.TipoPersonaId==1)
                //{
                //    subtotal = total;
                //    cantidadIva = 0;
                //}
                //else
                //{
                subtotal = Math.Round(total, 2) / (1 + IVA); //Math.Round(total / (1 + IVA), 2);
                cantidadIva = subtotal * this.IVA;
                cantidadIva = Math.Round(total, 2) - subtotal;
                //}
            }
            if (chkRetencionIVA.Checked)
            {
                cantidadIVARetenido = subtotal * this.RetencionIVA;
                total -= cantidadIVARetenido;
            }

            descuento = ConvierteTextoADecimal(txtDescuento);
            TxtMuestraTotal.Text = FormatoMoney(total - descuento);
            txtSubtotal.Text = FormatoMoney(subtotal);
            txtIVA.Text = FormatoMoney(cantidadIva);
            txtIVARetenido.Text = FormatoMoney(cantidadIVARetenido);
            txtRestante.Text = FormatoMoney(total - ConvierteTextoADecimal(txtPago.Text));
        }

        #endregion

        public void CargaCatalogoUsoCFDI()
        {
            //ListaEmpresas = new BusEmpresas().ObtieneCatalogoRegimen();
            cmbUsoCFDI.DataSource = new BusEmpresas().ObtieneCatalogoUsoCFDI();
        }

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
                new UtiCorreo().EnviaCorreo(asunto, new List<string>() { Cliente.Email, Cliente.Email2, Cliente.Email3, email4, email5, email6 }, mensaje, archivosAdjuntos);
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
            if (RelacionaFactura)
                uuid = factura.Facturar33conRelacion(Empresa, Pedido, ListaProductosFactura, Cliente, Pedido.Factura, serie, FechaFactura,
                                           TipoComprobante, UsoCFDI, FormaPago, MedioPago, CondicionPago,
                                           NumeroCuenta, pathClienteDirectorioFacturas,
                                           CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, Observaciones, "04", UUIDRelacionado);
            else
                uuid = factura.FacturarNeue(Empresa, Pedido, ListaProductosFactura, Cliente, serie, Pedido.Factura, FechaFactura,
                                           FormaPago, MedioPago, CondicionPago,
                                           CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, 0, Observaciones,UsoCFDI, 
                                           pathClienteDirectorioFacturas);

            Empresa.TipoTasaIVAId = tipoTasaIVAid;
            Empresa.TasaOCuota = tasaOCuota;

            EntFactura fact = new EntFactura() { PedidoId = Pedido.Id, NumeroFactura = Pedido.Factura, UUID = uuid, Ruta = pathClienteDirectorioFacturas, Fecha = DateTime.Today };

            return fact;// pathClienteDirectorioFacturas;
        }
        EntFactura EnviarFacturaPrueba(EntEmpresa Empresa, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente, 
                                   DateTime FechaFactura,
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
            string uuid = factura.FacturarNeue(Program.EmpresaSeleccionada, Pedido, ListaProductos, Cliente,
                                            Program.EmpresaSeleccionada.SerieFactura, FechaFactura,
                                            FormaPago, MedioPago, CondicionPago, NumeroCuenta,
                                            CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, 0,
                                            pathClienteDirectorioFacturas,
                                            UsoCFDI);

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
        
        void CargaClientesDeudaEnPantallas()
        {
            //Form vCli = BuscaFormaBase(new ClientesCredito().Titulo);
            //if (vCli != null)
            //{
            //    ((ClientesCredito)vCli).CargaGvClientesCredito();
            //    ((ClientesCredito)vCli).CargaGvPedidosClientesCredito();
            //}
        }


        private void Ventas_Load(object sender, EventArgs e)
        {
            try
            {
                CargaCatalogoUsoCFDI();
                //CAMBIAR A CONSTRUCTOR
                InicializaPantalla();
                CargaEmpresas();

                if (Program.EmpresaSeleccionada == null)
                    Program.EmpresaSeleccionada = SeleccionaEmpresa();
                if (Program.EmpresaSeleccionada != null)
                {
                    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
                    CargaClientes(Program.EmpresaSeleccionada.Id);
                    CargaProductosDetalle(Program.EmpresaSeleccionada.Id);

                    cmbUsoCFDI.SelectedIndex = ((List<EntCatalogoGenerico>)cmbUsoCFDI.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.UsoCFDIId);

                    ClienteSeleccionado = null;
                }
                int timbresrestantes = Program.EmpresaSeleccionada.TimbresRestantes;
                lbFacturasRestantes.Text = timbresrestantes.ToString();
                lbFacturasContratadas.Text = Program.EmpresaSeleccionada.TimbresUsados.ToString() + '/' + Program.EmpresaSeleccionada.Timbres.ToString();
                if (timbresrestantes <= 5)
                    MuestraMensaje(string.Format("¡Solo quedan {0} timbres para la empresa seleccionada!", timbresrestantes), "LÍMITE DE TIMBRES");
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
        bool ClientePublicoGeneral;

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
                        //chkFacturaPublicoGeneral.Checked = false;
                        chkFacturaPublicoGeneral.Checked = ClientePublicoGeneral;
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
                    //chkFacturaPublicoGeneral.Checked = false;
                    chkFacturaPublicoGeneral.Checked = ClientePublicoGeneral;
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

                    //chkFacturaPublicoGeneral.Checked = false;
                    chkFacturaPublicoGeneral.Checked = ClientePublicoGeneral;
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

                    chkFacturar.Checked = false;
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductosPedido_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.ColumnIndex == 8 || e.ColumnIndex == 9 || e.ColumnIndex == 10) && e.RowIndex > -1)//CANTIDAD | PRECIO SIN IVA
                {
                    EntProducto productoSeleccionado = ObtieneProductoFromGV(gvProductosPedido);
                    decimal iva = 1.16m;
                    if (e.ColumnIndex == 8)//CANTIDAD 
                    {
                        //EntProducto productoSeleccionado = ObtieneProductoFromGV(gvProductosPedido);
                        if (productoSeleccionado.TipoProductoId == 1)//1:PRODUCTO | 2:SERVICIO
                        {
                            if (!string.IsNullOrWhiteSpace(productoSeleccionado.Serie))
                            {
                                productoSeleccionado.Cantidad = 1;
                                MandaExcepcion("PRODUCTOS CON SERIE NO SE PUEDEN VENDER EN CANTIDADES MAYORES A 1.");
                            }

                            List<EntProducto> listaProductosSeleccionados = this.ListaProductos.Where(P => P.ProductoId == productoSeleccionado.ProductoId).ToList();
                            int existenciaProducto = listaProductosSeleccionados.Count() + 1;//COMPENSACION POR PRODUCTOSELECCIONADO
                            int cantidadProductos = Convert.ToInt32(productoSeleccionado.Cantidad);
                            if (productoSeleccionado.Cantidad > existenciaProducto)
                            {
                                MuestraMensajeError("La Cantidad solicitada es mayor a la Existente. \n Existencia: " + existenciaProducto, "ERROR");
                                // productoSeleccionado.Existencia;
                                //gvProductosPedido.CurrentRow.Cells[4].Value = productoSeleccionado.Existencia;
                                cantidadProductos = existenciaProducto;
                                //if (cantidadProductos == 1)
                                //{
                                //    AgregaRemueveProductoEnPedido(productoSeleccionado, -1);
                                //    AgregaRemueveProductoEnPedido(productoSeleccionado, 1);
                                //}
                            }
                            //else
                            //{
                            //    //    //gvProductosPedido.CurrentRow.Cells[gvProductosPedido.ColumnCount - 1].Selected = true;
                            //    //    //gvProductosPedido.CurrentRow.Cells[4].Selected = true;
                            //}
                            for (int x = 0; x < cantidadProductos - 1; x++)
                            {
                                AgregaRemueveProductoEnPedido(listaProductosSeleccionados[x], 1);
                            }
                            productoSeleccionado.Cantidad = 1;
                            //gvProductosPedido.Rows[gvProductosPedido.CurrentRow.Index].Cells[8].Value =1;
                            //gvProductosPedido.Rows[gvProductosPedido.CurrentRow.Index].Cells[8].Value = ConvierteTextoADecimal("1");
                            //gvProductosPedido.Refresh();
                        }
                    }
                    else if (e.ColumnIndex == 9)//PRECIO SIN IVA
                    {
                        decimal precioSinIVA = productoSeleccionado.PrecioVentaSinIVA; // ConvierteTextoADecimal(gvProductosPedido.CurrentRow.Cells[6].Value.ToString());
                        decimal precioIVA = Math.Round(precioSinIVA * iva, 2);

                        productoSeleccionado.PrecioVenta = precioIVA;
                        //gvProductosPedido.CurrentRow.Cells[7].Value = precioIVA;                    }
                    }
                    else if (e.ColumnIndex == 10)
                    {
                        decimal precioIVA = productoSeleccionado.PrecioVenta; // (decimal)gvProductosPedido.CurrentRow.Cells[7].Value;
                        decimal precioSinIVA = Math.Round(precioIVA / iva,2);
                        productoSeleccionado.PrecioVentaSinIVA = precioSinIVA;
                        //gvProductosPedido.CurrentRow.Cells[6].Value = precioSinIVA;
                    }

                    //if(gvProductosPedido.Rows.Count>1)
                    //    gvProductosPedido.Rows[gvProductosPedido.CurrentRow.Index].Cells[gvProductosPedido.ColumnCount - 1].Selected = true;
                    //else
                    //    gvProductosPedido.CurrentRow.Cells[gvProductosPedido.ColumnCount - 1].Selected = true;
                    gvProductosPedido.Refresh();
                    //gvProductosPedido.CurrentRow.Cells[gvProductosPedido.ColumnCount - 1].Selected = true;
                    CalculaSumaTotal(ListaProductosPedido, txtTotal);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductosPedido_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if ((e.ColumnIndex == 6 || e.ColumnIndex == 7) && e.RowIndex > -1)//PRECIO SIN IVA | PRECIO IVA
                //{
                //    ContraseñaGeneral vContra = new Pantallas.ContraseñaGeneral(true);
                //    if (vContra.ShowDialog() == DialogResult.OK)
                //    {
                //        gvProductosPedido.Columns[6].ReadOnly = false;
                //        gvProductosPedido.Columns[7].ReadOnly = false;
                //    }
                //}
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
                {
                    VerificaProductosSeleccionados(ListaProductosPedido);
                    PoneSolicitud();
                }

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
                string mensaje;

                lbFacturasRestantes.Text = Program.EmpresaSeleccionada.TimbresRestantes.ToString();
                if (ConvierteTextoAInteger(lbFacturasRestantes.Text) <= 0)
                    MandaExcepcion("Timbres insuficientes");

                EntEmpresa empresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                empresaSeleccionada.RFC = Program.EmpresaSeleccionada.RFC;
                
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
                    decimal cantidadIVARetenido = ConvierteTextoADecimal(txtIVARetenido);
                    decimal cantidadISRRetenido = 0;//ConvierteTextoADecimal(txtISRRetenido);
                    decimal cantidadIEPS = 0;// ConvierteTextoADecimal(textBox1.Text);

                    pedidoAgrega = AgregarPedido(ClienteSeleccionado.Id, detallePedido.Remove(detallePedido.Length - 2), txtSolicitud.Text, "", ConvierteTextoADecimal(txtTotal.Text), ConvierteTextoADecimal(txtPago.Text), DateTime.Now, DateTime.Today, 0, chkFacturar.Checked, 1);

                    int ingresoId = 0;
                    foreach (EntProducto p in productosSeleccionados)
                    {
                        AgregarProductoDetallePedido(pedidoAgrega.Id, p.Id, p.Cantidad, p.PrecioCosto, p.PrecioVenta);
                        Productos vProd = new Productos();
                        if (p.TipoProductoId == 1)
                        {
                            vProd.ActualizaEstatusProductoDetalle(p, 2);//ESTATUS:2=ENTREGADO
                            vProd.AumentaProducto(p.ProductoId, -p.Cantidad);

                            ////INGRESA SI EL CLIENTE ES EMPRESA PROPIA.
                            //if (ClienteSeleccionado.EmpresaId > 0)
                            //{
                            //    if (ingresoId == 0) {
                            //        int proveedorId = 0;
                            //        string descripcion, factura = "";

                            //        if (chkFacturar.Checked)
                            //            factura = "FAC: " + empresaSeleccionada.SerieFactura + ObtieneUltimaFactura(empresaSeleccionada.Id);
                            //        else
                            //            factura = "SIN FACTURAR";
                            //        descripcion = "COMPRA A EMPRESA -" + empresaSeleccionada.Nombre + "- " + factura + " - " + DateTime.Today.ToShortDateString();
                            //        //ProductoIngresa.Fecha.ToShortDateString();

                            //        //VERIFICA SI YA SE AGREGO LA EMPRESANUEVA COMO PROVEEDOR. LO HACE POR NOMBRE, NO SE PUEDE BUSCAR POR ID DE PROVEEDOR(NO SE SABE).
                            //        List<EntProveedor> provedores = new BusProveedores().ObtieneProveedores(ClienteSeleccionado.EmpresaId).Where(P => P.Nombre == empresaSeleccionada.Nombre).ToList();
                            //        if (provedores.Count > 0)
                            //            proveedorId = provedores[0].Id;
                            //        else
                            //            proveedorId = new Pantallas.Proveedores().AgregaProveedor(ClienteSeleccionado.EmpresaId, empresaSeleccionada.Nombre, empresaSeleccionada.Nombre, "");

                            //        ingresoId = new BusProductos().AgregaIngreso(new EntCatalogoGenerico() { EmpresaId = proveedorId, Descripcion = descripcion, Fecha = DateTime.Today.Date });
                            //    }

                            //    //List<EntProducto> productosEmpresa=new BusProductos().ObtieneProductos(ClienteSeleccionado.EmpresaId);
                            //    int productoId = 0;
                            //    if (productoId != p.ProductoId)
                            //    {
                            //        productoId = p.ProductoId;

                            //        new BusProductos().ActualizaEstatusProducto(p.ProductoId, true);
                            //    }

                            //    //PARA LA EMPRESA QUE COMPRA EL PRODUCTO EL PRECIOCOSTO SERIA A LO QUE SE LE ESTA VENDIENDO.
                            //    vProd.AgregaProductoDetalle(p.ProductoId, ingresoId, ClienteSeleccionado.EmpresaId, 
                            //        p.Marca, p.Modelo, p.Serie, p.PrecioVenta, p.PrecioVenta, p.PrecioVenta2, p.PrecioCosto);
                            //}
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
                            //SOLO PARA CAMBIAR ESTATUS. VERIFICA SI EL TOTAL DEL PEDIDO ESTA PAGADO, CAMBIA ESTATUS DE SER ASI.
                            AumentaPagoPedido(pedidoAgrega.Id, 0);

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
                            txtMetodoPago.Text = cmbMetodoPago.Text.Remove(3);
                            txtUsoCFDI.Text = cmbUsoCFDI.Text.Remove(3);
                            
                            TomaDatosCliente(ClienteSeleccionado);

                            if (empresaSeleccionada.Facturacion)
                            {
                                factura = EnviarFactura(empresaSeleccionada, pedidoAgrega, productosSeleccionados, ClienteSeleccionado, DateTime.Now,
                                                    txtFormaPago.Text, txtMetodoPago.Text, txtCondicionesPago.Text,
                                                    txtNumeroCuenta.Text,
                                                    cantidadIVA, cantidadIVARetenido, cantidadISRRetenido, cantidadIEPS, txtObservaciones.Text,
                                                    "I", txtUsoCFDI.Text,
                                                    chkRelacionaFactura.Checked, txtUUIDRelacion.Text);

                                try
                                {
                                    //DESCUENTA TIMBRE
                                    new BusEmpresas().AumentaTimbreEmpresa(empresaSeleccionada.Id);
                                    //Program.EmpresaSeleccionada.TimbresRestantes--;
                                    //Program.EmpresaSeleccionada.TimbresUsados++;
                                    empresaSeleccionada.TimbresRestantes--;
                                    empresaSeleccionada.TimbresUsados++;
                                }
                                catch(Exception ex) { }
                            }
                            else
                                factura = EnviarFacturaPrueba(empresaSeleccionada, pedidoAgrega, productosSeleccionados, ClienteSeleccionado, DateTime.Now,
                                                    txtFormaPago.Text, txtMetodoPago.Text, txtCondicionesPago.Text,
                                                    txtNumeroCuenta.Text,
                                                    cantidadIVA, cantidadIVARetenido, cantidadISRRetenido, cantidadIEPS, txtObservaciones.Text,
                                                    "I", txtUsoCFDI.Text);


                            factura.EmpresaId = empresaSeleccionada.Id;
                            factura.TipoComprobanteId = 1;//I-INGRESO.
                            factura.FormaPagoId = ConvierteTextoAInteger(txtFormaPago.Text);
                            factura.MetodoPagoId = cmbMetodoPago.SelectedIndex + 1;
                            factura.UsoCFDIId = ObtieneCatalogoGenericoFromCmb(cmbUsoCFDI).Id;

                            factura.SerieFactura = empresaSeleccionada.SerieFactura;

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
                        
                        if (facturado)
                        {
                            //MuestraMensaje("¡El pedido fue FACTURADO satisfactoriamente!", "CONFIRMACIÓN PEDIDO FACTURADO");
                            Cursor.Current = Cursors.WaitCursor;

                            AgregarFacturaPedido(factura);

                            try
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                UtiPDF modiPDF = new UtiPDF();
                                string estonoes = "";
                                string rutaArchivoPDF = factura.Ruta + "\\" + EncuentraArchivo(factura.Ruta, ".pdf");
                                modiPDF.ModificaPDF(estonoes,estonoes, rutaArchivoPDF, "1", productosSeleccionados.Count);

                                EnviaCorreo(empresaSeleccionada, pedidoAgrega, ClienteSeleccionado, factura.Ruta);
                            }
                            catch (Exception ex)
                            {
                                MuestraExcepcion(ex, "Correo NO enviado.");
                            }
                            ////COPIAR EN RED (SOLO NAVOJOA)
                            //if (Program.UsuarioSeleccionado.Id == 8)
                            //{
                            //    try
                            //    {
                            //        string pathClienteDirectorioCopia = PathFacturasCopia + "\\" + ClienteSeleccionado.Nombre;
                            //        //pathClienteDirectorioCopia = "LAPTOP-LJCQA84V\\Users\\pavel\\Documents\\FacturacionModerna\\PUBLICO EN GENERAL";
                            //        if (!System.IO.Directory.Exists(pathClienteDirectorioCopia))
                            //            System.IO.Directory.CreateDirectory(pathClienteDirectorioCopia);

                            //        string pathCopy = pathClienteDirectorioCopia + "\\" + factura.Ruta.Remove(0, factura.Ruta.Length - 14);
                            //        System.IO.Directory.CreateDirectory(pathCopy);

                            //        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(factura.Ruta);
                            //        foreach (System.IO.FileInfo file in dir.GetFiles())
                            //        {
                            //            string nombreArchivo = file.Name;//EncuentraArchivo(factura.Ruta, ".pdf");
                            //            System.IO.File.Copy(file.FullName, pathCopy + "\\" + nombreArchivo);
                            //        }

                            //        //System.IO.File.Copy(factura.Ruta+"\\"+nombreArchivo, pathCopy + "\\" + nombreArchivo);
                            //        //nombreArchivo = EncuentraArchivo(factura.Ruta, ".xml");
                            //        //System.IO.File.Copy(factura.Ruta + "\\" + nombreArchivo, pathCopy + "\\" + nombreArchivo);
                            //    }
                            //    catch (Exception)
                            //    {
                            //    }
                            //}

                            try
                            {
                                if (MuestraMensajeYesNo("¿Desea mostrar Factura?") == DialogResult.Yes)
                                {
                                    string nombreArchivo = EncuentraArchivo(factura.Ruta, ".pdf");
                                    MuestraArchivo(factura.Ruta, nombreArchivo);
                                }
                            }
                            catch (Exception ex) { MuestraExcepcion(ex, "ERROR AL MOSTRAR FACTURA"); }


                            //LimpiaTextBox(pnlCliente);
                            //LimpiaTextBox(pnlAgrega);
                            //LimpiaTextBox(pnlFacturacion, true);

                            //ClienteSeleccionado = null;
                            //chkFacturar.Checked = false;
                            //chkFacturaPublicoGeneral.Checked = false;
                            //gvProductosPedido.DataSource = null;
                            //lbContadorSeries.Text = "0";

                            //CargaProductosDetalle(empresaSeleccionada.Id);
                            btnCancelar.PerformClick();

                            CargaVentasEnPantallas();
                            CargaClientes(empresaSeleccionada.Id);//SERA POR VOLVER A PONER LOS PARAMETROS???
                            
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("¡Pedido Registrado y Facturado!");
                        }
                    }
                    else
                    {
                        //LimpiaTextBox(pnlCliente);
                        //LimpiaTextBox(pnlAgrega);
                        //LimpiaTextBox(pnlFacturacion, true);

                        //ClienteSeleccionado = null;
                        //chkFacturar.Checked = false;
                        //chkFacturaPublicoGeneral.Checked = false;
                        //gvProductosPedido.DataSource = null;
                        //lbContadorSeries.Text = "0";

                        //CargaProductosDetalle(empresaSeleccionada.Id);
                        btnCancelar.PerformClick();

                        CargaVentasEnPantallas();
                        CargaClientes(empresaSeleccionada.Id);
                        
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("¡Pedido Registrado!");
                    }

                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        public void MuestraCantidadTimbres() {
            lbFacturasRestantes.Text = Program.EmpresaSeleccionada.TimbresRestantes.ToString();
            lbFacturasContratadas.Text = Program.EmpresaSeleccionada.TimbresUsados.ToString() + '/' + Program.EmpresaSeleccionada.Timbres.ToString();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                chkRetencionIVA.Checked = false;

                LimpiaTextBox(pnlCliente);
                LimpiaTextBox(pnlAgrega);
                LimpiaTextBox(pnlFacturacion, true);

                ClienteSeleccionado = null;

                gvProductosPedido.DataSource = null;
                lbContadorSeries.Text = "0";

                chkFacturar.Checked = false;
                chkFacturaPublicoGeneral.Checked = false;

                //CargaProductos();
                CargaProductosDetalle(Program.EmpresaSeleccionada.Id);
                lbFacturasRestantes.Text = Program.EmpresaSeleccionada.TimbresRestantes.ToString();
                lbFacturasContratadas.Text = Program.EmpresaSeleccionada.TimbresUsados.ToString() + '/' + Program.EmpresaSeleccionada.Timbres.ToString() ;
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
                //SE LIMPIA TODO LO QUE NO SE NECESITA POR SE PUBLICO GENERAL
                //EnableTextBox(pnlFacturacion, !chkFacturaPublicoGeneral.Checked);
                EnableTextBox(gbDireccionFiscal, !chkFacturaPublicoGeneral.Checked);
                LimpiaTextBox(pnlFacturacion);
                
                //SE VUELVE A COLOCAR DESPUES DE HABER LIMPIADO
                txtFormaPago.Text = cmbFormaPago.Text.Remove(2, cmbFormaPago.Text.Length - 2);
                txtMetodoPago.Text = cmbMetodoPago.Text.Remove(3);
                txtUsoCFDI.Text = cmbUsoCFDI.Text.Remove(3);

                //txtCondicionesPago.Enabled = true;
                //txtNumeroCuenta.Enabled = true;
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
                    clientePublicoGeneral.Banco = ClienteSeleccionado.Banco;
                    clientePublicoGeneral.Email = "tiimfacturacion@hotmail.com";
                    clientePublicoGeneral.FormaPagoId = 1;

                    CargaDatosCliente(clientePublicoGeneral);

                    //ClienteSeleccionado = clientePublicoGeneral;

                    //CalculaSumaTotal(ListaProductosPedido, txtTotal);
                }
                else if (ClienteSeleccionado != null)
                    CargaDatosCliente(ClienteSeleccionado);
                else
                    CargaDatosCliente(new EntCliente());
                //ClienteSeleccionado = null;

                CalculaSumaTotal(ListaProductosPedido, txtTotal);

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
                    //btnCancelar.PerformClick();

                    cmbUsoCFDI.SelectedIndex = ((List<EntCatalogoGenerico>)cmbUsoCFDI.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.UsoCFDIId);

                    CargaClientes(Program.EmpresaSeleccionada.Id);
                    CargaProductosDetalle(Program.EmpresaSeleccionada.Id);
                    btnCancelar.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnPreFactura_Click(object sender, EventArgs e)
        {
            try
            {
                VerificaClienteSeleccionado();
                if (MuestraMensajeYesNo("Se realizará la Pre-Factura \n RFC: " + ClienteSeleccionado.RFC + "\n ¿Correcto?", "CONFIRMAR") == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    string detallePedido = "";
                    EntPedido pedidoAgrega = new EntPedido();
                    EntEmpresa empresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);

                    List<EntProducto> productosSeleccionados = ListaProductosPedido;

                    VerificaProductosSeleccionados(productosSeleccionados);

                    txtFormaPago.Text = cmbFormaPago.Text.Remove(2, cmbFormaPago.Text.Length - 2);
                    txtMetodoPago.Text = cmbMetodoPago.Text.Remove(3);
                    txtUsoCFDI.Text = cmbUsoCFDI.Text.Remove(3);

                    foreach (EntProducto p in productosSeleccionados)
                        detallePedido += p.Cantidad + " " + p.Descripcion + " - ";

                    decimal cantidadIVA = ConvierteTextoADecimal(txtIVA.Text);
                    decimal cantidadIEPS = ConvierteTextoADecimal(txtEmail2.Text);
                    decimal cantidadIVARetenido = ConvierteTextoADecimal(txtIVARetenido);

                    //EntFactura factura = new EntFactura();

                    //try
                    //{
                    TomaDatosCliente(ClienteSeleccionado);
                    pedidoAgrega.SubTotal = ConvierteTextoADecimal(txtSubtotal.Text);
                    pedidoAgrega.Total = ConvierteTextoADecimal(txtTotal.Text);
                    pedidoAgrega.Factura = ObtieneUltimaFactura(empresaSeleccionada.Id);
                    //factura = EnviarPreFactura(empresaSeleccionada, pedidoAgrega, productosSeleccionados, ClienteSeleccionado, DateTime.Now, txtFormaPago.Text, txtMetodoPago.Text, txtCondicionesPago.Text,
                    //                        txtNumeroCuenta.Text,
                    //                        cantidadIVA, cantidadIVARetenido, cantidadISRRetenido, cantidadIEPS);

                    int tipoTasaIVAid = empresaSeleccionada.TipoTasaIVAId;
                    decimal tasaIVA = empresaSeleccionada.TasaOCuota;
                    
                    PreFactura vPreFac = new PreFactura(empresaSeleccionada, pedidoAgrega, productosSeleccionados, ClienteSeleccionado,
                                           "I", txtUsoCFDI.Text, txtFormaPago.Text, txtMetodoPago.Text, txtCondicionesPago.Text,
                                           txtNumeroCuenta.Text,
                                           cantidadIVA, cantidadIVARetenido, 0, cantidadIEPS, txtObservaciones.Text, 
                                           chkRelacionaFactura.Checked,"04",txtUUIDRelacion.Text);
                    vPreFac.Show();
                    vPreFac.Close();

                    empresaSeleccionada.TipoTasaIVAId = tipoTasaIVAid;
                    empresaSeleccionada.TasaOCuota = tasaIVA;
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtEmail3_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkLeyendaGarantia_CheckedChanged(object sender, EventArgs e)
        {
            try {
                if (chkLeyendaGarantia.Checked)
                    txtObservaciones.Text = "GARANTIA DE UN AÑO TOTAL EXCEPTO CONTRA FALLAS ELECTRICAS O QUEMADURAS";
                else
                    txtObservaciones.Text = "";
            } catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnRefrescarProductos_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CargaProductosDetalle(Program.EmpresaSeleccionada.Id);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }


        private void btnRefrescaEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnClienteNuevo_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                lbFacturasRestantes.Text = Program.EmpresaSeleccionada.TimbresRestantes.ToString();
                if (ConvierteTextoAInteger(lbFacturasRestantes.Text) <= 0)
                    MandaExcepcion("Timbres insuficientes");

                EntEmpresa empresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                empresaSeleccionada.RFC = Program.EmpresaSeleccionada.RFC;

                //DESCUENTA TIMBRE
                new BusEmpresas().AumentaTimbreEmpresa(empresaSeleccionada.Id);
                Program.EmpresaSeleccionada.TimbresRestantes--;
                Program.EmpresaSeleccionada.TimbresUsados++;
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

        private void cmbFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (cmbFormaPago.SelectedIndex == cmbFormaPago.Items.Count - 1)
                //    cmbMetodoPago.SelectedIndex = 1;//PARCIALIDADES
                //else
                //    cmbMetodoPago.SelectedIndex = 0;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (cmbMetodoPago.SelectedIndex == 1)//PARCIALIDADES
                //    cmbFormaPago.SelectedIndex = cmbFormaPago.Items.Count - 1;
                //else
                //    cmbFormaPago.SelectedIndex = 0;
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

        private void chkRetencionIVA_CheckedChanged(object sender, EventArgs e)
        {
            try {
                CalculaSumaTotal(ObtieneListaProductosFromGV(gvProductosPedido), txtTotal);
            } catch(Exception ex) { MuestraExcepcion(ex); }
        }

        private void chkRelacionaFactura_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pnlRelacionFactura.Visible = chkRelacionaFactura.Checked;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnBuscaFacturaRelacionar_Click(object sender, EventArgs e)
        {
            try
            {
                VerificaClienteSeleccionado();
                List<EntPedido> facturasCliente = new BusPedidos().ObtienePedidosClientesPorCliente(this.ClienteSeleccionado.Id);
                EntCliente cliente = new BusClientes().ObtieneCliente(this.ClienteSeleccionado.Id);
                SeleccionaFactura vSelFac = new SeleccionaFactura(facturasCliente);
                if (vSelFac.ShowDialog() == DialogResult.OK)
                {
                    EntPedido pedidofacturaSeleccionada = vSelFac.FacturaPedidoSeleccionado;
                    txtFacturaRelacion.Text = pedidofacturaSeleccionada.Factura;
                    txtUUIDRelacion.Text = pedidofacturaSeleccionada.UUID;
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
    }
}
