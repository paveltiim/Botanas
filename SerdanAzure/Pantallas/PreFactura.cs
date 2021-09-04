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
                        , string Observaciones
                        , bool RelacionaFactura, string TipoRelacion, string UUIDRelacionado)
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
            this.RetencionIVA = CantidadIVARetenido;
            this.Observaciones = Observaciones;

            this.RelacionaFactura = RelacionaFactura;
            this.TipoRelacion = TipoRelacion;
            this.UUIDRelacionado = UUIDRelacionado;
            this.TipoComprobante = TipoComprobante;
            this.UsoCFDI = UsoCFDI;
        }
        EntCliente ClienteSeleccionado;
        EntEmpresa EmpresaSeleccionada;
        EntPedido PedidoSeleccionado;
        List<EntProducto> ListaProductosSeleccionados;
        string FormaPago, MedioPago, CondicionPago, NumeroCuenta, Observaciones,TipoComprobante, UsoCFDI, TipoRelacion, UUIDRelacionado;
        decimal CantidadIVA;
        bool RelacionaFactura;

        EntFactura EnviarPreFactura(EntEmpresa Empresa, EntPedido Pedido, List<EntProducto> ListaProductos, EntCliente Cliente, DateTime FechaFactura,
                                    string TipoComprobante, string UsoCFDI, string FormaPago, string MedioPago, 
                                    string CondicionPago, string NumeroCuenta,
                                    decimal CantidadIVA, decimal IVARetenido, decimal ISRRetenido, decimal CantidadIEPS,
                                    string Solicitud, string Observaciones, 
                                    bool RelacionaFactura, string TipoRelacion, string UUIDRelacionado)
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

            UtiFacturacionPruebas factura = new UtiFacturacionPruebas();
            MessageBox.Show("MUESTRA PRE-FACTURA");

            string uuid;
            if (RelacionaFactura)
            {
                uuid = factura.Facturar33conRelacion(Empresa, Pedido, ListaProductosFactura, Cliente, Pedido.Factura, FechaFactura,
                                               TipoComprobante, UsoCFDI, FormaPago, MedioPago, CondicionPago,
                                               NumeroCuenta, pathClienteDirectorioFacturas,
                                               CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, Observaciones, TipoRelacion, UUIDRelacionado);
            }
            else
            {
                uuid = factura.Facturar33(Empresa, Pedido, ListaProductosFactura, Cliente, Pedido.Factura, FechaFactura,
                                               TipoComprobante, UsoCFDI, FormaPago, MedioPago, CondicionPago,
                                               NumeroCuenta, pathClienteDirectorioFacturas,
                                               CantidadIVA, IVARetenido, ISRRetenido, CantidadIEPS, Observaciones);
            }
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
                                                CantidadIVA, this.RetencionIVA, 0, 0, ClienteSeleccionado.Banco, Observaciones,
                                                this.RelacionaFactura,this.TipoRelacion, this.UUIDRelacionado);
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
                        UtiPDF modiPDF = new UtiPDF();
                        string pagare = "Por medio de este PAGARÉ me obligo a pagar  incondicionalmente  a  la  orden  de " + EmpresaSeleccionada.NombreFiscal + ",  la  cantidad  de  " + FormatoMoney(PedidoSeleccionado.Total) + " correspondiente al Pedido estipulado en esta FACTURA, en esta ciudad. La cantidad que ampara este PAGARÉ causará intereses al tipo de 3 % mensual en caso de mora. Este PAGARÉ es mercantil no domiciliario y se rige por lo estipulado en la última parte del art. 173 de la ley general de títulos y operaciones de Crédito. Firma " + ClienteSeleccionado.Nombre;
                        string rutaArchivoPDF = factura.Ruta + "\\" + EncuentraArchivo(factura.Ruta, ".pdf");
                        //rutaArchivoPDF = @"c:\tiim\facturacion\prefacturas\prueba\prueba\F4BD2F40-9F10-11EB-87A4-9DD1737F0D72 (1).pdf";
                        modiPDF.ModificaPDF(pagare, rutaArchivoPDF, "1",this.ListaProductosSeleccionados.Count);
                        //factura.Ruta= @"C:\TIIM\Facturacion\PreFacturas\PRUEBA\PRUEBA\";
                        string nombreArchivo = EncuentraArchivo(factura.Ruta, ".pdf");
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
