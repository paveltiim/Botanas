using AiresEntidades;
using AiresUtilerias;
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
    public partial class PreFactura : FormBase
    {
        public PreFactura(EntEmpresa Empresa, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente
                        , string TipoComprobante, string UsoCFDI, string FormaPago, string MedioPago, string CondicionPago, string NumeroCuenta
                        , decimal CantidadIVA, decimal CantidadIVARetenido, decimal CantidadISRRetenido, decimal CantidadIEPS
                        , string Observaciones)
        {
            InitializeComponent();
            EmpresaSeleccionada = Empresa;
            PedidoSeleccionado = Pedido;
            ListaProductosSeleccionados = ListaProductos;
            ClienteSeleccionado = Cliente;

            //CargaDatosCliente(Cliente);
            //CargaProductosPedido(ListaProductos);
            //CargaDatosFactura(FormaPagoId, MedioPagoId, CondicionPago, NumeroCuenta);
            //CalculaSumaTotal(ListaProductos, txtTotal);
            this.FormaPago = FormaPago;
            this.MedioPago = MedioPago;
            this.CondicionPago = CondicionPago;
            this.NumeroCuenta = NumeroCuenta;
            this.CantidadIVA = CantidadIVA;
            this.Observaciones = Observaciones;
            
            //this.CantidadIVARetenido = CantidadIVARetenido;
            //this.CantidadISRRetenido = CantidadISRRetenido;
            //this.CantidadIEPS = CantidadIEPS;
            this.TipoComprobante = TipoComprobante;
            this.UsoCFDI = UsoCFDI;
        }
        EntCliente ClienteSeleccionado;
        EntEmpresa EmpresaSeleccionada;
        EntPedido PedidoSeleccionado;
        List<EntProducto> ListaProductosSeleccionados;
        string FormaPago, MedioPago, CondicionPago, NumeroCuenta, Observaciones,TipoComprobante, UsoCFDI;
        decimal CantidadIVA;

        EntFactura EnviarPreFactura(EntEmpresa Empresa, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente, DateTime FechaFactura,
                                    string TipoComprobante, string UsoCFDI, string FormaPago, string MedioPago, string CondicionPago, string NumeroCuenta,
                                    decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS,
                                    string Solicitud, string Observaciones)
        {
            string pathClienteDirectorio = PathPreFacturas + "\\" + Cliente.Nombre;
            if (!System.IO.Directory.Exists(pathClienteDirectorio))
                System.IO.Directory.CreateDirectory(pathClienteDirectorio);

            string pathClienteDirectorioFacturas = pathClienteDirectorio + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss");
            System.IO.Directory.CreateDirectory(pathClienteDirectorioFacturas);

            List<EntProducto> productosDetalle = ListaProductos;

            //ListaProductos = new BusProductos().ObtieneProductosPorPedido(Pedido.Id);

            List<EntProducto> ListaProductosFactura = new List<EntProducto>();
            string codigo = "";
            int cantidad = 1;
            foreach (EntProducto p in productosDetalle.OrderBy(P => P.Codigo).ToList())
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
                        ProductoId = p.ProductoId
                    };
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
            
            if (Program.UsuarioSeleccionado.Id > 1)
                ListaProductosFactura[ListaProductosFactura.Count - 1].Descripcion += "Solicitud:".PadLeft(60, '-') + Solicitud;

            UtiFacturacionPruebas factura = new UtiFacturacionPruebas();
            MessageBox.Show("MUESTRA PRE-FACTURA");

            string uuid = factura.Facturar33(Empresa, Pedido, ListaProductosFactura, Cliente, Pedido.Factura, FechaFactura,
                                           TipoComprobante, UsoCFDI, FormaPago, MedioPago, CondicionPago,
                                           NumeroCuenta, pathClienteDirectorioFacturas,
                                           CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, Observaciones);
            EntFactura fact = new EntFactura() { PedidoId = Pedido.Id, NumeroFactura = Pedido.Factura, UUID = uuid, Ruta = pathClienteDirectorioFacturas, Fecha = DateTime.Today };

            return fact;
        }
        private void PreFactura_Load(object sender, EventArgs e)
        {
            try {
                Cursor.Current = Cursors.WaitCursor;
                
                bool facturado = false;
                EntFactura factura = new EntFactura();

                try
                {
                    factura = EnviarPreFactura(EmpresaSeleccionada, PedidoSeleccionado, ListaProductosSeleccionados, ClienteSeleccionado,
                                                DateTime.Now, TipoComprobante, UsoCFDI, FormaPago, MedioPago, CondicionPago, NumeroCuenta,
                                                CantidadIVA, 0, 0, 0, ClienteSeleccionado.Banco, Observaciones);
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

                        ////COPIAR EN RED (SOLO SERDAN-MARTIN)
                        if (Program.UsuarioSeleccionado.Id == 1)
                        {
                            try
                            {
                                string pathClienteDirectorioCopia = PathPreFacturasCopia + "\\" + ClienteSeleccionado.Nombre;
                                if (!System.IO.Directory.Exists(pathClienteDirectorioCopia))
                                    System.IO.Directory.CreateDirectory(pathClienteDirectorioCopia);

                                string pathCopy = pathClienteDirectorioCopia + "\\" + factura.Ruta.Remove(0, factura.Ruta.Length - 14);
                                System.IO.Directory.CreateDirectory(pathCopy);

                                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(factura.Ruta);
                                foreach (System.IO.FileInfo file in dir.GetFiles())
                                {
                                    string nombreArchivoCopia = file.Name;//EncuentraArchivo(factura.Ruta, ".pdf");
                                    System.IO.File.Copy(file.FullName, pathCopy + "\\" + nombreArchivoCopia);
                                }

                                //System.IO.File.Copy(factura.Ruta+"\\"+nombreArchivo, pathCopy + "\\" + nombreArchivo);
                                //nombreArchivo = EncuentraArchivo(factura.Ruta, ".xml");
                                //System.IO.File.Copy(factura.Ruta + "\\" + nombreArchivo, pathCopy + "\\" + nombreArchivo);
                            }
                            catch (Exception)
                            {
                            }
                        }

                        MuestraArchivo(factura.Ruta, nombreArchivo);
                    }
                    catch (Exception ex) { MuestraExcepcion(ex, "ERROR AL MOSTRAR FACTURA"); }
                }
                Cursor.Current = Cursors.Default;

            } catch(Exception ex) { MuestraExcepcion(ex);
            }finally
            {
                //this.Close();
            }
        }
    }
}
