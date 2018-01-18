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
using System.Data.Common;
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
        
            string PathClienteDirectorioFacturas;
       
        
        #endregion
        
        int DiasPorSemana = 6;
        void InicializaPantalla()
        {

            if (Program.UsuarioSeleccionado.Id > 1)
            {
                tpImportaVentas.Hide();
                
            }
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
        EntProducto ObtieneProductoDetalleEnListaProductos(List<EntProducto> ListaProductos, int ProductoId)
        {
            List<EntProducto> lst = ListaProductos.Where(P => P.Id == ProductoId).ToList();
            if (lst.Count > 0)
                return lst[0];

            return null;
        }
        int CuentaProductosEnListaProductos(List<EntProducto> ListaProductos, int ProductoId)
        {
            //return ListaProductos.Where(P => P.ProductoId == ProductoId).Count();
            return ListaProductos.Where(P => P.Id == ProductoId).Count();
        }
        List<EntProducto> ObtieneProductosEnListaProductos(List<EntProducto> ListaProductos, int ProductoId)
        {
            return ListaProductos.Where(P => P.ProductoId == ProductoId).ToList();
        }
        void AgregaProducto(int Id, int TipoProductoId, string Codigo, string Descripcion)
        {
            EntProducto producto = new EntProducto()
            {
                Id = Id,
                TipoProductoId = TipoProductoId,
                Codigo = Codigo,
                Descripcion = Descripcion
            };
            producto.Id = new BusProductos().AgregaProducto(Id, producto);
        }
        void AgregaProductoDetalle(int Id, int ProductoId, int IngresoId, int EmpresaId, string Serie, decimal PrecioCosto, decimal PrecioVenta, decimal PrecioVenta2, decimal PrecioEspecial)
        {
            EntProducto producto = new EntProducto()
            {
                Id = Id,
                ProductoId = ProductoId,
                IngresoId = IngresoId,
                EmpresaId = EmpresaId,
                Serie = Serie,
                PrecioCosto = PrecioCosto,
                PrecioVenta = PrecioVenta,
                PrecioVenta2 = PrecioVenta2,
                PrecioEspecial = PrecioEspecial,
                Fecha = DateTime.Now
            };
            producto.Id = new BusProductos().AgregaProductoDetalle(Id, producto);
        }
        void ActualizaProductoDetalle(int Id, int ProductoId, int IngresoId, int EmpresaId, string Serie, decimal PrecioCosto, decimal PrecioVenta, decimal PrecioVenta2, decimal PrecioEspecial)
        {
            EntProducto producto = new EntProducto()
            {
                Id = Id,
                ProductoId = ProductoId,
                IngresoId = IngresoId,
                EmpresaId = EmpresaId,
                Serie = Serie,
                PrecioCosto = PrecioCosto,
                PrecioVenta = PrecioVenta,
                PrecioVenta2 = PrecioVenta2,
                PrecioEspecial = PrecioEspecial
            };
            new BusProductos().ActualizaProductoDetalle(producto);
        }
        void RevisaAgregaIngreso(EntProducto ProductoIngresa)
        {
            string descripcion = "";
            if (!string.IsNullOrWhiteSpace(ProductoIngresa.Ingreso))
                descripcion = ProductoIngresa.Ingreso;
            else
                descripcion = "IMPORTACION " + ProductoIngresa.Fecha.ToShortDateString();

            if (new BusProductos().ObtieneIngreso(ProductoIngresa.IngresoId).Id == 0)
                new BusProductos().AgregaIngreso(ProductoIngresa.IngresoId, new EntCatalogoGenerico() { Descripcion = descripcion, Fecha = ProductoIngresa.Fecha });
        }
        bool RevisaAgregaProducto(List<EntProducto> ListaProductosDetalleRevisa, EntProducto ProductoDetalleRevisa)
        {
            bool agrega = false;
            int cantidadProductos = CuentaProductosEnListaProductos(ListaProductosDetalleRevisa, ProductoDetalleRevisa.ProductoId);
            if (cantidadProductos == 0)
            {
                agrega = true;
                AgregaProducto(ProductoDetalleRevisa.ProductoId, ProductoDetalleRevisa.TipoProductoId, ProductoDetalleRevisa.Codigo, ProductoDetalleRevisa.Descripcion);
            }
            else
            {
                new Productos().ActualizaProducto(ProductoDetalleRevisa.ProductoId, ProductoDetalleRevisa.TipoProductoId, ProductoDetalleRevisa.Codigo, ProductoDetalleRevisa.Descripcion);
                //new BusProductos().ActualizaEstatusProducto(ProductoDetalleRevisa.ProductoId, true);
            }
            if (ProductoDetalleRevisa.TipoProductoId == 1)//PRODUCTO
                new Productos().AumentaProducto(ProductoDetalleRevisa.ProductoId, cantidadProductos);
            else//SERVICIO
                new Productos().AumentaProducto(ProductoDetalleRevisa.ProductoId, 1);

            return agrega;
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
        
        #region Eventos Pestaña Impresion

        private void tcPedidosGrids_SelectedIndexChanged(object sender, EventArgs e)
            {
                
            }
           
        #endregion

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


        private void btnBuscaArchivoImportarEntradas_Click(object sender, EventArgs e)
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

                    List<EntProducto> lst = new List<EntProducto>();// new UtiLinqToExcel().ExcelToListaProductos(rutaArchivo);//

                    //Microsoft.ACE.OLEDB.12.0;Data Source='" + archivo + "';Extended Properties=Excel 12.0
                    //string connectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;""", rutaArchivo);
                    string connectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source={0};Extended Properties=Excel 12.0;", rutaArchivo);

                    DbProviderFactory factory =
                       DbProviderFactories.GetFactory("System.Data.OleDb");

                    DbDataAdapter adapter = factory.CreateDataAdapter();

                    DbCommand selectCommand = factory.CreateCommand();
                    selectCommand.CommandText = "SELECT ID,PRODUCTOID,CODIGO,DESCRIPCION,TIPOPRODUCTOID,TIPOPRODUCTO,SERIE,PRECIOCOSTO,PRECIOVENTA,INGRESOID,INGRESO,FECHAINGRESO,EMPRESAID FROM[Hoja1$]";
                    //Id = row["ID"].Cast<Int32>(),
                    //             ProductoId = row["PRODUCTOID"].Cast<Int32>(),
                    //             Codigo = row["CODIGO"].Cast<string>(),
                    //             Descripcion = row["DESCRIPCION"].Cast<string>(),
                    //             TipoProductoId = row["TIPOPRODUCTOID"].Cast<Int32>(),
                    //             TipoProducto = row["TIPOPRODUCTO"].Cast<string>(),
                    //             Serie = row["SERIE"].Cast<string>(),
                    //             PrecioCosto = row["PRECIOCOSTO"].Cast<decimal>(),
                    //             PrecioVenta = row["PRECIOVENTA"].Cast<decimal>(),
                    //             IngresoId = row["INGRESOID"].Cast<Int32>(),
                    //             Ingreso = row["INGRESO"].Cast<string>(),
                    //             Fecha = row["FECHAINGRESO"].Cast<DateTime>(),
                    //             EmpresaId = row["EMPRESAID"].Cast<Int32>(),
                    DbConnection connection = factory.CreateConnection();
                    connection.ConnectionString = connectionString;

                    selectCommand.Connection = connection;

                    adapter.SelectCommand = selectCommand;

                    DataSet products = new DataSet();

                    adapter.Fill(products);

                    foreach (DataRow r in products.Tables[0].Rows)
                    {
                        EntProducto p = new EntProducto
                        {
                            Id = ConvierteTextoAInteger(r[0].ToString()),
                            ProductoId = ConvierteTextoAInteger(r[1].ToString()),
                            Codigo = r[2].ToString(),
                            Descripcion = r[3].ToString(),
                            TipoProductoId = ConvierteTextoAInteger(r[4].ToString()),
                            TipoProducto = r[5].ToString(),
                            Serie = r[6].ToString(),
                            PrecioCosto = ConvierteTextoADecimal(r[7].ToString()),
                            PrecioVenta = ConvierteTextoADecimal(r[8].ToString()),
                            IngresoId = ConvierteTextoAInteger(r[9].ToString()),
                            Ingreso = r[10].ToString(),
                            Fecha = Convert.ToDateTime(r[11].ToString()),
                            EmpresaId = ConvierteTextoAInteger(r[12].ToString()),
                            EstatusId = 1
                        };
                        lst.Add(p);
                    }

                    //IMPORTACION CON ARCHIVO CSV
                    /*/****//*/*//*/****//*/*//*/**//*/*/
                    //int counter = 0;
                    //string line;

                    //// Read the file and display it line by line.  
                    //System.IO.StreamReader file =
                    //    new System.IO.StreamReader(rutaArchivo);
                    //while ((line = file.ReadLine()) != null)
                    //{
                    //    string[] producto = line.Split(',');

                    //    EntProducto p = new EntProducto
                    //    {
                    //        Id = ConvierteTextoAInteger(producto[0]),
                    //        ProductoId = ConvierteTextoAInteger(producto[1]),
                    //        Codigo = producto[2],
                    //        Descripcion = producto[3],
                    //        TipoProductoId = ConvierteTextoAInteger(producto[4]),
                    //        TipoProducto = producto[5],
                    //        Serie = producto[6],
                    //        PrecioCosto = ConvierteTextoADecimal(producto[7]),
                    //        PrecioVenta = ConvierteTextoADecimal(producto[8]),
                    //        IngresoId = ConvierteTextoAInteger(producto[9]),
                    //        Ingreso = producto[10],
                    //        Fecha = Convert.ToDateTime(producto[11]),
                    //        EmpresaId = ConvierteTextoAInteger(producto[12]),
                    //        EstatusId = 1
                    //    };
                    //    lst.Add(p);
                    //    //System.Console.WriteLine(line);

                    //    counter++;
                    //}

                    //file.Close();
                    ////System.Console.WriteLine("There were {0} lines.", counter);
                    //// Suspend the screen.  
                    ////System.Console.ReadLine();

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
        private void btnImportarEntradas_Click(object sender, EventArgs e)
        {
            try
            {
                //REVISAR FECHA DE IMPORTACION PARA EVITAR DUPLICAR
                //PENDIENTE TABLA DE REGISTRO DE IMPORTACIONES
                EntEmpresa empresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                List<EntProducto> listaProductosTodos = new BusProductos().ObtieneProductosDetalleTodos();

                List<EntProducto> listaProductosIngresar = ObtieneListaProductosFromGV(gvProductosDetalle);
                string seriesNoImportadas = "";

                int productoId = 0;
                int IngresoId = 0;
                bool productoRepetido = false;

                foreach (EntProducto p in listaProductosIngresar)
                {
                    if (string.IsNullOrWhiteSpace(p.Serie))
                        p.Serie = "";

                    if (!EncuentraProductoDetalleEnListaProductos(listaProductosTodos, p.Id))
                    {
                        if (IngresoId != p.IngresoId)
                        {
                            IngresoId = p.IngresoId;
                            RevisaAgregaIngreso(p);
                        }
                        if (productoId != p.ProductoId)
                        {
                            productoId = p.ProductoId;

                            new BusProductos().ActualizaEstatusProducto(p.ProductoId, true);
                            List<EntProducto> listaProductos = new BusProductos().ObtieneProductos();
                            
                            if(RevisaAgregaProducto(listaProductos, p))
                                listaProductosTodos = new BusProductos().ObtieneProductosDetalleTodos();
                        }
                        if (!EncuentraProductoDetalleEnListaProductos(listaProductosTodos, p.Id))
                            AgregaProductoDetalle(p.Id, p.ProductoId, p.IngresoId, empresaSeleccionada.Id, p.Serie, p.PrecioCosto, p.PrecioVenta, p.PrecioVenta2, p.PrecioEspecial);
                    }
                    else
                    {
                        productoRepetido = true;

                        //ACTUALIZAR AL ACTUALIZAR EL EXCEL CON EMPRESAID
                        EntProducto productoEntonctrado = ObtieneProductoDetalleEnListaProductos(listaProductosTodos, p.Id);
                        if (productoEntonctrado.IngresoId != p.IngresoId)
                        {
                            RevisaAgregaIngreso(p);
                            ActualizaProductoDetalle(p.Id, p.ProductoId, productoEntonctrado.IngresoId, p.EmpresaId, p.Serie, p.PrecioCosto, p.PrecioVenta, p.PrecioVenta2, p.PrecioEspecial);
                        }else
                            ActualizaProductoDetalle(p.Id, p.ProductoId, p.IngresoId, p.EmpresaId, p.Serie, p.PrecioCosto, p.PrecioVenta, p.PrecioVenta2, p.PrecioEspecial);

                        if (productoEntonctrado.ProductoId != p.ProductoId)
                        {
                            new BusProductos().ActualizaEstatusProducto(p.ProductoId, true);
                            List<EntProducto> listaProductos = new BusProductos().ObtieneProductos();

                            RevisaAgregaProducto(listaProductos, p);
                        }

                        seriesNoImportadas += p.Serie + "\n";
                    }
                }

                if (productoRepetido)
                    MuestraMensaje(string.Format("Se encontraron Productos previamente importados:\n{0} \n¡IMPORTACIÓN COMPLETA!", seriesNoImportadas), "ALERTA");
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

        
        private void btnBuscaArchivoImportaVentas_Click(object sender, EventArgs e)
        {
            try
            {
                string rutaArchivo = SeleccionaArchivo();
                System.IO.FileInfo fi = new FileInfo(rutaArchivo);
                if (fi != null)
                {
                    if (fi.Extension != ".xls")
                        MandaExcepcion("El archivo no es el correcto. \n Debe ser archivo de Excel (.xls)");

                    txtRutaArchivoImportaVentas.Text = rutaArchivo; //fi.FullName;
                    List<EntPedido> lst = new UtiLinqToExcel().ExcelToListaPedidos(rutaArchivo);

                    gvPedidosImporta.DataSource = lst;
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
        private void btnImportaVentas_Click(object sender, EventArgs e)
        {
            try
            {
                //REVISAR FECHA DE IMPORTACION PARA EVITAR DUPLICAR
                //PENDIENTE TABLA DE REGISTRO DE IMPORTACIONES
                Cursor.Current = Cursors.WaitCursor;

                EntEmpresa empresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                
                List<EntPedido> listaPedidosIngresar = ObtieneListaPedidosFromGV(gvPedidosImporta);
                EntPedido pedidoIngresar = new EntPedido();

                int pedidoId = 0;
                
                Ventas vVentas = new Pantallas.Ventas();
                foreach (EntPedido pe in listaPedidosIngresar)
                {
                    EntProducto productoPedido = pe.ProductoPedido;
                    if (pedidoId != pe.Id)
                    {
                        pedidoId = pe.Id;

                        EntCliente clientePedido = new BusClientes().ObtieneCliente(pe.ClienteId);
                        if (clientePedido.Id == 0)
                            new Clientes().AgregaCliente(pe.ClienteId, empresaSeleccionada.Id, pe.Cliente);
                        else
                        {
                            clientePedido.EmpresaId = empresaSeleccionada.Id;
                            new Clientes().ActualizaCliente(clientePedido, pe.Cliente);
                        }
                        pedidoIngresar = vVentas.AgregarPedido(pe.ClienteId, pe.Detalle, "", pe.Total,pe.Total, pe.Fecha, pe.Fecha, 0, pe.Facturado, pe.EstatusId);

                        decimal pago = pe.Pago;
                        if (pago > 0)
                        {
                            vVentas.AgregarPago(pedidoIngresar.Id, pago);
                            vVentas.AumentaPagoPedido(pedidoIngresar.Id, 0);//SOLO PARA CAMBIAR ESTATUS. VERIFICA SI EL TOTAL DEL PEDIDO ESTA PAGADO, CAMBIA ESTATUS DE SER ASI.
                        }
                        if (pe.Facturado)
                        {
                            EntFactura factura = new EntFactura() { PedidoId = pedidoIngresar.Id, NumeroFactura = pe.Factura.Remove(0,2), UUID = pe.UUID, Ruta = pe.RutaFactura, Fecha = pe.FechaEntrega };
                            vVentas.AgregarFacturaPedido(factura);
                        }
                    }
                    vVentas.AgregarProductoDetallePedido(pedidoIngresar.Id, pe.ProductoPedido.Id, Convert.ToInt32(pe.ProductoPedido.Cantidad), pe.ProductoPedido.PrecioCosto, pe.ProductoPedido.PrecioVenta);
                    Productos vProd = new Productos();
                    if (pe.ProductoPedido.TipoProductoId == 1)
                    {
                        vProd.ActualizaEstatusProductoDetalle(pe.ProductoPedido, 2);//ESTATUS:2=ENTREGADO
                        vProd.AumentaProducto(pe.ProductoPedido.ProductoId, -Convert.ToInt32(pe.ProductoPedido.Cantidad));
                    }
                }

                MuestraMensaje("¡IMPORTACIÓN COMPLETA!", "CONFIRMACIÓN");

                txtRutaArchivoImportaVentas.Clear();
                txtCantidadPedidosImporta.Clear();
                gvPedidosImporta.DataSource = null;
                gvPedidosImporta.Refresh();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnRefrescaCargaArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                string rutaArchivo = txtDescripcionFiltro.Text;
                if (!string.IsNullOrWhiteSpace(rutaArchivo))
                {
                    List<EntProducto> lst = new List<EntProducto>();// = new UtiLinqToExcel().ExcelToListaProductos(rutaArchivo);

                    //Microsoft.ACE.OLEDB.12.0;Data Source='" + archivo + "';Extended Properties=Excel 12.0
                    //string connectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;""", rutaArchivo);
                    string connectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source={0};Extended Properties=Excel 12.0;", rutaArchivo);

                    DbProviderFactory factory =
                       DbProviderFactories.GetFactory("System.Data.OleDb");

                    DbDataAdapter adapter = factory.CreateDataAdapter();

                    DbCommand selectCommand = factory.CreateCommand();
                    selectCommand.CommandText = "SELECT ID,PRODUCTOID,CODIGO,DESCRIPCION,TIPOPRODUCTOID,TIPOPRODUCTO,SERIE,PRECIOCOSTO,PRECIOVENTA,INGRESOID,INGRESO,FECHAINGRESO,EMPRESAID FROM[Hoja1$]";
                    //Id = row["ID"].Cast<Int32>(),
                    //             ProductoId = row["PRODUCTOID"].Cast<Int32>(),
                    //             Codigo = row["CODIGO"].Cast<string>(),
                    //             Descripcion = row["DESCRIPCION"].Cast<string>(),
                    //             TipoProductoId = row["TIPOPRODUCTOID"].Cast<Int32>(),
                    //             TipoProducto = row["TIPOPRODUCTO"].Cast<string>(),
                    //             Serie = row["SERIE"].Cast<string>(),
                    //             PrecioCosto = row["PRECIOCOSTO"].Cast<decimal>(),
                    //             PrecioVenta = row["PRECIOVENTA"].Cast<decimal>(),
                    //             IngresoId = row["INGRESOID"].Cast<Int32>(),
                    //             Ingreso = row["INGRESO"].Cast<string>(),
                    //             Fecha = row["FECHAINGRESO"].Cast<DateTime>(),
                    //             EmpresaId = row["EMPRESAID"].Cast<Int32>(),
                    DbConnection connection = factory.CreateConnection();
                    connection.ConnectionString = connectionString;

                    selectCommand.Connection = connection;

                    adapter.SelectCommand = selectCommand;

                    DataSet products = new DataSet();

                    adapter.Fill(products);

                    foreach (DataRow r in products.Tables[0].Rows)
                    {
                        EntProducto p = new EntProducto
                        {
                            Id = ConvierteTextoAInteger(r[0].ToString()),
                            ProductoId = ConvierteTextoAInteger(r[1].ToString()),
                            Codigo = r[2].ToString(),
                            Descripcion = r[3].ToString(),
                            TipoProductoId = ConvierteTextoAInteger(r[4].ToString()),
                            TipoProducto = r[5].ToString(),
                            Serie = r[6].ToString(),
                            PrecioCosto = ConvierteTextoADecimal(r[7].ToString()),
                            PrecioVenta = ConvierteTextoADecimal(r[8].ToString()),
                            IngresoId = ConvierteTextoAInteger(r[9].ToString()),
                            Ingreso = r[10].ToString(),
                            Fecha = Convert.ToDateTime(r[11].ToString()),
                            EmpresaId = ConvierteTextoAInteger(r[12].ToString()),
                            EstatusId = 1
                        };
                        lst.Add(p);
                    }

                    //IMPORTACION CON ARCHIVO CSV
                    /*/****//*/*//*/****//*/*//*/**//*/*/
                    //int counter = 0;
                    //string line;

                    //// Read the file and display it line by line.  
                    //System.IO.StreamReader file =
                    //    new System.IO.StreamReader(rutaArchivo);
                    //while ((line = file.ReadLine()) != null)
                    //{
                    //    string[] producto = line.Split(',');

                    //    EntProducto p = new EntProducto
                    //    {
                    //        Id = ConvierteTextoAInteger(producto[0]),
                    //        ProductoId = ConvierteTextoAInteger(producto[1]),
                    //        Codigo = producto[2],
                    //        Descripcion = producto[3],
                    //        TipoProductoId = ConvierteTextoAInteger(producto[4]),
                    //        TipoProducto = producto[5],
                    //        Serie = producto[6],
                    //        PrecioCosto = ConvierteTextoADecimal(producto[7]),
                    //        PrecioVenta = ConvierteTextoADecimal(producto[8]),
                    //        IngresoId = ConvierteTextoAInteger(producto[9]),
                    //        Ingreso = producto[10],
                    //        Fecha = Convert.ToDateTime(producto[11]),
                    //        EmpresaId = ConvierteTextoAInteger(producto[12]),
                    //        EstatusId = 1
                    //    };
                    //    lst.Add(p);
                    //    //System.Console.WriteLine(line);

                    //    counter++;
                    //}

                    //file.Close();
                    ////System.Console.WriteLine("There were {0} lines.", counter);
                    //// Suspend the screen.  
                    ////System.Console.ReadLine();

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
    }
}
