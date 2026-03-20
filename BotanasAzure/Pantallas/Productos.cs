using AiresEntidades;
using AiresNegocio;
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
    public partial class Productos : FormBase
    {
        public void VerificaEmpresa()
        {
            cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }

        public Productos()
        {
            InitializeComponent();
        }

        int establecimientoId = 1;
        EntProducto ProductoSeleccionado;
        List<EntProducto> ListaProductos = new List<EntProducto>();
        List<EntProducto> ListaProductosDetalle = new List<EntProducto>();

        List<EntProducto> ListaProductosC = new List<EntProducto>();
        List<EntProducto> ListaProductosDetalleC = new List<EntProducto>();

        int IngresoProductoId = 0;

        bool NuevoProducto;

        List<EntEmpresa> ListaEmpresas;
        List<EntCatalogoGenerico> ListaProductosServicios;
        List<EntCatalogoGenerico> ListaUnidades;

        int TipoProductoId { get { return cmbTipoProductoFiltro.SelectedIndex+1; } }

        public void CargaProductosServicios()
        {
            ListaProductosServicios = new BusEmpresas().ObtieneProductosServicios();
            cmbTipoProductoServicio.DataSource = ListaProductosServicios;

        }
        public void CargaUnidades()
        {
            ListaUnidades = new BusEmpresas().ObtieneUnidades();
            cmbTipoUnidad.DataSource = ListaUnidades;
        }

        #region Metodos

        public void CargaProductos(int EmpresaId, int TipoProductoId)
        {
            this.ListaProductos = new BusProductos().ObtieneProductosPorTipo(EmpresaId, TipoProductoId)
                                                                        .OrderBy(P => P.Descripcion).ToList();
            gvProductos.DataSource = this.ListaProductos;

            CargaProductosEnPantallas();
        }

        void CargaProductosEnPantallas()
        {
            //Form forma = BuscaFormaBase(new Ventas().Titulo);
            //if (forma != null)
            //{
            //    //((Ventas)forma).CargaProductos();
            //    ((Ventas)forma).CargaProductosDetalle(Program.EmpresaSeleccionada.Id);
            //}
        }

        public EntProducto AgregaProducto(int TipoProductoId, string Codigo, string CodigoBarra, string Descripcion,
                                        string Marca, string Modelo,
                                        int TipoProductoServicioId, int TipoUnidadId, decimal PrecioCosto, bool IncluyeIEPS)
        {
            EntProducto producto = new EntProducto()
            {
                EmpresaId = Program.UsuarioSeleccionado.CompañiaId,
                TipoProductoId = TipoProductoId,
                Codigo = Codigo,
                CodigoBarra = CodigoBarra,
                Descripcion = Descripcion,
                Marca = Marca,
                Modelo = Modelo,
                ProductoServicioId = TipoProductoServicioId,
                UnidadId = TipoUnidadId,
                PrecioCosto=PrecioCosto,
                IncluyeIeps=IncluyeIEPS
            };
            producto.Id = new BusProductos().AgregaProducto(producto);

            return producto;
        }
    
        public void ActualizaProducto(int ProductoId, int TipoProductoId, string Codigo, string CodigoBarra, string Descripcion,
                                        string Marca, string Modelo,
                                        int TipoProductoServicioId, int TipoUnidadId, decimal PrecioCosto, bool IncluteIEPS)
        {
            EntProducto producto = new EntProducto()
            {
                Id = ProductoId,
                TipoProductoId = TipoProductoId,
                Codigo = Codigo,
                CodigoBarra = CodigoBarra,
                Descripcion = Descripcion,
                Marca = Marca,
                Modelo = Modelo,
                ProductoServicioId = TipoProductoServicioId,
                UnidadId = TipoUnidadId,
                PrecioCosto = PrecioCosto,
                IncluyeIeps = IncluteIEPS
            };
            new BusProductos().ActualizaProducto(producto);
        }
        public void ActualizaProductoMinMax (int ProductoId, decimal ExistenciaMinima, decimal ExistenciaMaxima)
        {
            EntProducto producto = new EntProducto()
            {
                Id = ProductoId,
                ExistenciaMinima = ExistenciaMinima,
                ExistenciaMaxima = ExistenciaMaxima
            };
            new BusProductos().ActualizaProductoMinMax(producto);
        }
        public void ActualizaEstatusProducto(EntProducto Producto, bool Estatus)
        {
            Producto.Estatus = Estatus;
            new BusProductos().ActualizaEstatusProducto(Producto);
        }

        public void AumentaProducto(int ProductoId, decimal Cantidad)
        {
            new BusProductos().AumentaProducto(ProductoId, Cantidad);
        }

        void CargaDatosProducto(EntProducto Producto)
        {
            lbProducto.Text = Producto.Codigo + " - " + Producto.Descripcion;
            txtCodigo.Text = Producto.Codigo;
            txtCodigoBarra.Text = Producto.CodigoBarra;
            txtDescripcion.Text = Producto.Descripcion;
            txtPrecioCosto.Text = FormatoMoney(Producto.PrecioCosto);
            txtMarca.Text = Producto.Marca;
            txtModelo.Text = Producto.Modelo;

            cmbTipoProducto.SelectedIndex = Producto.TipoProductoId - 1;

            cmbTipoProductoServicio.SelectedIndex = ((List<EntCatalogoGenerico>)cmbTipoProductoServicio.DataSource).FindIndex(P => P.Id == Producto.ProductoServicioId);
            cmbTipoUnidad.SelectedIndex = ((List<EntCatalogoGenerico>)cmbTipoUnidad.DataSource).FindIndex(P => P.Id == Producto.UnidadId);

            txtExistenciaMinima.Text = Producto.ExistenciaMinima.ToString();
            txtExistenciaMaxima.Text = Producto.ExistenciaMaxima.ToString();

            chkIncluyeIEPS.Checked = Producto.IncluyeIeps;
            CargaListaPreciosVenta(Producto.Id);
        }

        /// <summary>
        /// Filtra Clientes por los distintos parametros, y los carga en gvClientes.
        /// </summary>
        /// <param name="Clientes"></param>
        /// <param name="Nombre"></param>
        void FiltrarProductosPorDescripcion(List<EntProducto> ListaProductos, string DescripcionProducto, bool SoloExistencia)
        {
            //List<EntCliente> clientes = (List<EntCliente>)gvClientes.DataSource;

            var productosFiltro = from c in ListaProductos
                                  where c.Descripcion.ToUpper().Contains(DescripcionProducto.ToUpper())
                                  select c;

            gvProductos.DataSource = null;
            if (SoloExistencia)
                gvProductos.DataSource = productosFiltro.Where(P => P.Existencia > 0).ToList();
            else
                gvProductos.DataSource = productosFiltro.ToList();
        }
        void FiltrarProductosPorCodigo(List<EntProducto> ListaProductos, string CodigoProducto, bool SoloExistencia)
        {
            var productosFiltro = from c in ListaProductos
                                  where c.Codigo.ToUpper().Contains(CodigoProducto.ToUpper())
                                  select c;

            gvProductos.DataSource = null;
            if (SoloExistencia)
                gvProductos.DataSource = productosFiltro.Where(P => P.Existencia > 0).ToList();
            else
                gvProductos.DataSource = productosFiltro.ToList();
        }
        void FiltrarProductos(List<EntProducto> ListaProductos, string Proveedor, string CodigoProducto, string DescripcionProducto,
                            bool SoloExistencia, DataGridView GvMuestra)
        {
            var productosFiltro = from c in ListaProductos
                                  where c.Codigo.ToUpper().Contains(CodigoProducto.ToUpper())
                                  select c;

            if (string.IsNullOrWhiteSpace(Proveedor))
                productosFiltro = from c in ListaProductos
                                  where c.Ingreso.ToUpper().Contains(Proveedor.ToUpper())
                                  select c;
            if (string.IsNullOrWhiteSpace(DescripcionProducto))
                productosFiltro = from c in ListaProductos
                                  where c.Descripcion.ToUpper().Contains(DescripcionProducto.ToUpper())
                                  select c;

            GvMuestra.DataSource = null;
            if (SoloExistencia)
                GvMuestra.DataSource = productosFiltro.Where(P => P.Existencia > 0).ToList();
            else
                GvMuestra.DataSource = productosFiltro.ToList();
        }

        /// <summary>
        /// .
        /// </summary>
        /// <param name="Clientes"></param>
        /// <param name="Codigo"></param>
        EntProducto ObtieneProductoPorCodigo(List<EntProducto> Productos, string Codigo)
        {
            var productoFiltro = from c in Productos
                                 where c.Codigo.Trim().ToUpper().Equals(Codigo.Trim().ToUpper())
                                 select c;

            if (productoFiltro.ToList().Count > 0)
                return productoFiltro.ToList()[0];
            else
                return null;
        }

        #endregion


        void InicializaPantalla()
        {
            LimpiaTextBox(pnlDatos);
            //ActivaAgregar(true);

            cmbTipoProductoFiltro.SelectedIndex = 0;
            cmbTipoProducto.SelectedIndex = 0;
            cmbTipoProductoServicio.SelectedIndex = 0;
            cmbTipoUnidad.SelectedIndex = 0;
        }

        void ActivaAgregar(bool Visible)
        {
            btnAgregar.Visible = Visible;
            btnActualizar.Visible = !Visible;
            txtCodigo.Focus();

            NuevoProducto = Visible;
        }
        void AgregaListaPrecios(List<EntProducto> listaProductos)
        {
            this.ProductoSeleccionado.PrecioVenta = listaProductos[0].PrecioVenta;
            this.ProductoSeleccionado.PrecioVenta2 = listaProductos[1].PrecioVenta;
            this.ProductoSeleccionado.PrecioVenta3 = listaProductos[2].PrecioVenta;
            this.ProductoSeleccionado.PrecioVenta4 = listaProductos[3].PrecioVenta;
            this.ProductoSeleccionado.PrecioEspecial = listaProductos[4].PrecioVenta;
            this.ProductoSeleccionado.PrecioEspecial2 = listaProductos[5].PrecioVenta;
            this.ProductoSeleccionado.PrecioEspecial3 = listaProductos[6].PrecioVenta;
            this.ProductoSeleccionado.PrecioEspecial4 = listaProductos[7].PrecioVenta;
            this.ProductoSeleccionado.PrecioEspecial5 = listaProductos.Where(P => P.Id == 14).FirstOrDefault().PrecioVenta;//HILLO
            this.ProductoSeleccionado.PrecioEspecialDetalle = listaProductos.Where(P => P.Id == 11).FirstOrDefault().PrecioVenta;//DETALLE 
            this.ProductoSeleccionado.PrecioEspecialDetalleM = listaProductos.Where(P => P.Id == 12).FirstOrDefault().PrecioVenta;//DETALLE MAYOREO//listaProductos[9].PrecioVenta;
            this.ProductoSeleccionado.PrecioEspecialComercial = listaProductos.Where(P => P.Id == 13).FirstOrDefault().PrecioVenta;//CADENA COMERCIAL//listaProductos[10].PrecioVenta;
            new BusProductos().AgregaListaPreciosProducto(this.ProductoSeleccionado,Program.UsuarioSeleccionado.Usuario);
        }
        void CargaListaPreciosVentaInicial()
        {
            List<EntProducto> listaProductos = new List<EntProducto>();
            listaProductos.Add(new EntProducto() { Id = 1, TipoId = 1, Descripcion = "Menudeo" });
            listaProductos.Add(new EntProducto() { Id = 2, TipoId = 1, Descripcion = "MayoreoLocal" });
            listaProductos.Add(new EntProducto() { Id = 3, TipoId = 1, Descripcion = "Sonora" });
            listaProductos.Add(new EntProducto() { Id = 4, TipoId = 1, Descripcion = "Culiacan" });
            listaProductos.Add(new EntProducto() { Id = 5, TipoId = 1, Descripcion = "Especial" });
            //listaProductos.Add(new EntProducto() { Id = 7, Descripcion = "A Detalle" });
            listaProductos.Add(new EntProducto() { Id = 7, TipoId = 1, Descripcion = "Cabos" });
            listaProductos.Add(new EntProducto() { Id = 14, TipoId = 1, Descripcion = "HERMOSILLO" });
            listaProductos.Add(new EntProducto() { Id = 9, TipoId = 1, Descripcion = "CONVENIENCIA" });
            listaProductos.Add(new EntProducto() { Id = 10, TipoId = 1, Descripcion = "EVENTUAL" });
            listaProductos.Add(new EntProducto() { Id = 11, TipoId = 2, Descripcion = "DETALLE" });//TIPO = 2: Venta Movil
            listaProductos.Add(new EntProducto() { Id = 12, TipoId = 2, Descripcion = "DETALLE MAYOREO" });//TIPO = 2: Venta Movil
            listaProductos.Add(new EntProducto() { Id = 13, TipoId = 2, Descripcion = "CADENA COMERCIAL" });//TIPO = 2: Venta Movil
            gvPreciosVenta.DataSource = null;
            gvPreciosVenta.DataSource = listaProductos.Where(P => P.TipoId == 1).ToList();
            gvPreciosVenta.Refresh();

            gvListaPreciosVentaDetalle.DataSource = null;
            gvListaPreciosVentaDetalle.DataSource = listaProductos.Where(P => P.TipoId == 2).ToList();
            gvListaPreciosVentaDetalle.Refresh();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductoId"></param>
        void CargaListaPreciosVenta(int ProductoId)
        {
            List<EntProducto> precios = new BusProductos().ObtieneListaPreciosProducto(0, ProductoId);
            List<EntProducto> listaProductos = new List<EntProducto>();
            listaProductos.Add(new EntProducto() { TipoId = 1, Id = 1, Descripcion = "Menudeo", PrecioVenta = precios.First().PrecioVenta });
            listaProductos.Add(new EntProducto() { TipoId = 1, Id = 2, Descripcion = "MayoreoLocal", PrecioVenta = precios.First().PrecioVenta2 });
            listaProductos.Add(new EntProducto() { TipoId = 1, Id = 3, Descripcion = "Sonora", PrecioVenta = precios.First().PrecioVenta3 });
            listaProductos.Add(new EntProducto() { TipoId = 1, Id = 4, Descripcion = "Culiacan", PrecioVenta = precios.First().PrecioVenta4 });
            listaProductos.Add(new EntProducto() { TipoId = 1, Id = 5, Descripcion = "Especial", PrecioVenta = precios.First().PrecioEspecial });
            //listaProductos.Add(new EntProducto() { Id = 7, Descripcion = "A Detalle", PrecioVenta = precios.First().PrecioEspecial2 });
            listaProductos.Add(new EntProducto() { TipoId = 1, Id = 7, Descripcion = "Cabos", PrecioVenta = precios.First().PrecioEspecial2 });
            listaProductos.Add(new EntProducto() { TipoId = 1, Id = 9, Descripcion = "CONVENIENCIA", PrecioVenta = precios.First().PrecioEspecial3 });
            listaProductos.Add(new EntProducto() { TipoId = 1, Id = 10, Descripcion = "EVENTUAL", PrecioVenta = precios.First().PrecioEspecial4 });
            listaProductos.Add(new EntProducto() { TipoId = 1, Id = 14, Descripcion = "HERMOSILLO", PrecioVenta = precios.First().PrecioEspecial5 });
            listaProductos.Add(new EntProducto() { TipoId = 2, Id = 11, Descripcion = "DETALLE", PrecioVenta = precios.First().PrecioEspecialDetalle });
            listaProductos.Add(new EntProducto() { TipoId = 2, Id = 12, Descripcion = "DETALLE MAYOREO", PrecioVenta = precios.First().PrecioEspecialDetalleM });
            listaProductos.Add(new EntProducto() { TipoId = 2, Id = 13, Descripcion = "CADENA COMERCIAL", PrecioVenta = precios.First().PrecioEspecialComercial });

            decimal ieps = 0.08m;
            if (!chkIncluyeIEPS.Checked)
                ieps = 0;
            foreach (EntProducto p in listaProductos)
            {
                p.PrecioVentaSinIVA = Math.Round(p.PrecioVenta * (1 + ieps), 2);
                p.PrecioVenta2 = p.PrecioVentaSinIVA * 1.02m;
            }
            gvPreciosVenta.DataSource = null;
            gvPreciosVenta.DataSource = listaProductos.Where(P => P.TipoId == 1).ToList(); ;
            gvPreciosVenta.Refresh();

            gvListaPreciosVentaDetalle.DataSource = null;
            gvListaPreciosVentaDetalle.DataSource = listaProductos.Where(P => P.TipoId == 2).ToList();
            gvListaPreciosVentaDetalle.Refresh();
            //CargaListaPreciosVentaDetalle(precios.First(), gvListaPreciosVentaDetalle);
        }
        //void CargaListaPreciosVentaDetalle(EntProducto Precios, DataGridView GvMuestraListaPrecios)
        //{
        //    List<EntProducto> listaProductos = new List<EntProducto>();
        //    listaProductos.Add(new EntProducto() { Id = 11, Descripcion = "DETALLE", PrecioVenta = Precios.PrecioEspecialDetalle });
        //    listaProductos.Add(new EntProducto() { Id = 12, Descripcion = "DETALLE MAYOREO", PrecioVenta = Precios.PrecioEspecialDetalleM });
        //    listaProductos.Add(new EntProducto() { Id = 13, Descripcion = "CADENA COMERCIAL", PrecioVenta = Precios.PrecioEspecialComercial });

        //    decimal ieps = 0.08m;
        //    if (!chkIncluyeIEPS.Checked)
        //        ieps = 0;
        //    foreach (EntProducto p in listaProductos)
        //    {
        //        p.PrecioVentaSinIVA = Math.Round(p.PrecioVenta * (1 + ieps), 2);
        //        p.PrecioVenta2 = p.PrecioVentaSinIVA * 1.02m;
        //    }
        //    GvMuestraListaPrecios.DataSource = null;
        //    GvMuestraListaPrecios.DataSource = listaProductos;
        //    GvMuestraListaPrecios.Refresh();
        //}


        private void Productos_Load(object sender, EventArgs e)
        {
            try
            {
                CargaProductosServicios();
                CargaUnidades();

                InicializaPantalla();

                CargaProductos(Program.UsuarioSeleccionado.CompañiaId, this.TipoProductoId);
                CargaListaPreciosVentaInicial();
                //chkSoloExistencia.Checked = true;
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCodigo.Text.Trim()))
                    MandaExcepcion("Ingrese Código de Producto");
                else if(ConvierteTextoADecimal(txtPrecioCosto.Text) == 0 && 
                        !chkPrecioCosto0.Checked && cmbTipoProducto.SelectedIndex == 0)
                {
                    MandaExcepcion("Asigne un Precio de Costo mayor a $0 o seleccione la casilla 'Precio Costo $0'");
                }
                else
                {
                    //EntEmpresa empresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                    EntCatalogoGenerico productoservicio = ObtieneCatalogoGenericoFromCmb(cmbTipoProductoServicio);
                    EntCatalogoGenerico unidad = ObtieneCatalogoGenericoFromCmb(cmbTipoUnidad);
                    this.ProductoSeleccionado = AgregaProducto(cmbTipoProducto.SelectedIndex + 1, txtCodigo.Text, txtCodigoBarra.Text, txtDescripcion.Text,                                           txtMarca.Text, txtModelo.Text,
                                                          productoservicio.Id, unidad.Id, ConvierteTextoADecimal(txtPrecioCosto),
                                                          chkIncluyeIEPS.Checked);
                    ActualizaProductoMinMax(this.ProductoSeleccionado.Id, ConvierteTextoADecimal(txtExistenciaMinima), ConvierteTextoADecimal(txtExistenciaMaxima));
                    
                    List<EntProducto> listaPrecios = ObtieneListaProductosFromGV(gvPreciosVenta);
                    listaPrecios.AddRange(ObtieneListaProductosFromGV(gvListaPreciosVentaDetalle));
                    AgregaListaPrecios(listaPrecios);
                }

                MuestraMensaje("¡Producto agregado satisfactoriamente!");
                btnCancelar.PerformClick();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCodigo.Text.Trim()))
                    MandaExcepcion("Ingrese Código de Producto");
                else if (ConvierteTextoADecimal(txtPrecioCosto.Text) == 0 && 
                                        !chkPrecioCosto0.Checked && 
                                        cmbTipoProducto.SelectedIndex == 0)
                {
                    MandaExcepcion("Asigne un Precio de Costo mayor a $0 ó seleccione \n la casilla 'Precio Costo $0'");
                }
                else
                {
                    EntCatalogoGenerico productoservicio = ObtieneCatalogoGenericoFromCmb(cmbTipoProductoServicio);
                    EntCatalogoGenerico unidad = ObtieneCatalogoGenericoFromCmb(cmbTipoUnidad);

                    ActualizaProducto(this.ProductoSeleccionado.Id, cmbTipoProducto.SelectedIndex + 1, txtCodigo.Text, txtCodigoBarra.Text,
                                        txtDescripcion.Text, txtMarca.Text, txtModelo.Text,
                                        productoservicio.Id, unidad.Id, ConvierteTextoADecimal(txtPrecioCosto), chkIncluyeIEPS.Checked);
                    ActualizaProductoMinMax(this.ProductoSeleccionado.Id, ConvierteTextoADecimal(txtExistenciaMinima), ConvierteTextoADecimal(txtExistenciaMaxima));
                    
                    List<EntProducto> listaPrecios = ObtieneListaProductosFromGV(gvPreciosVenta);
                    listaPrecios.AddRange(ObtieneListaProductosFromGV(gvListaPreciosVentaDetalle));
                    AgregaListaPrecios(listaPrecios);
                }

                MuestraMensaje("¡Producto Actualizado!");
                btnCancelar.PerformClick();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiaTextBox(tcDatosProductoIngresa, true);
                chkPrecioCosto0.Checked = false;
                this.ProductoSeleccionado = null;
                ActivaAgregar(true);
                lbProducto.Text = "*NUEVO PRODUCTO";
                CargaProductos(Program.UsuarioSeleccionado.CompañiaId, this.TipoProductoId);
                CargaListaPreciosVentaInicial();
                btnFiltroProducto.PerformClick();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ProductoSeleccionado = ObtieneProductoFromGV(gvProductos);

                if (MuestraMensajeYesNo(string.Format("¿Desea Eliminar el Producto: {0} - {1}? \n Se eliminará de todas las Empresas donde este registrado.", this.ProductoSeleccionado.Codigo, this.ProductoSeleccionado.Descripcion), "CONFIRMACIÓN ELIMINAR") == DialogResult.Yes)
                {
                    ActualizaEstatusProducto(this.ProductoSeleccionado, false);
                    this.ProductoSeleccionado = null;

                    CargaProductos(Program.UsuarioSeleccionado.CompañiaId, this.TipoProductoId);
                    LimpiaTextBox(pnlDatos);
                    ActivaAgregar(true);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                //this.ProductoSeleccionado = ObtieneProductoFromGV(gvProductos);
                //ActualizaProducto vActualizaProd = new Pantallas.ActualizaProducto(this.ProductoSeleccionado);
                //if (vActualizaProd.ShowDialog() == DialogResult.OK)
                //{
                //    CargaProductos(establecimientoId, this.TipoProductoId);
                //    LimpiaTextBox(pnlDatos);
                //    ActivaAgregar(true);

                //    btnFiltroProducto.PerformClick();
                //}
                this.ProductoSeleccionado = ObtieneProductoFromGV(gvProductos);
                CargaDatosProducto(this.ProductoSeleccionado);
                ActivaAgregar(false);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        
        private void gvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnEditar.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtDireccion_Leave(object sender, EventArgs e)
        {
            try
            {
                TextBoxDecimalMoney_Leave(sender, e);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            try
            {
                this.ProductoSeleccionado = ObtieneProductoPorCodigo(ObtieneListaProductosFromGV(gvProductos), txtCodigo.Text);
                if (this.ProductoSeleccionado != null)//actualiza
                {
                    CargaDatosProducto(this.ProductoSeleccionado);
                    btnAgregar.Visible = false;
                    btnActualizar.Visible = true;
                    NuevoProducto = false;

                    txtPrecioCosto.Focus();
                }
                else
                {
                    string codigo = txtCodigo.Text;
                    LimpiaTextBox(tcDatosProductoIngresa, true);

                    btnAgregar.Visible = true;
                    btnActualizar.Visible = false;
                    NuevoProducto = true;

                    lbProducto.Text = "*NUEVO PRODUCTO";
                    //lbProducto.Text = codigo;
                    txtCodigo.Text = codigo;
                    txtDescripcion.Focus();
                    cmbTipoProducto.SelectedIndex = cmbTipoProductoFiltro.SelectedIndex;
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((Keys)e.KeyChar == Keys.Enter) {
                    txtDescripcion.Focus();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                CargaProductos(Program.UsuarioSeleccionado.CompañiaId, this.TipoProductoId);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtPrecioCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            try {
                if ((Keys)e.KeyChar == Keys.Enter) {
                    this.GetNextControl((Control)sender, true).Focus();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void txtProductoBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = ((TextBox)sender);
                    FiltrarProductosPorDescripcion(ListaProductos, txt.Text,chkSoloExistencia.Checked);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtCodigoBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = ((TextBox)sender);
                    FiltrarProductosPorCodigo(ListaProductos, txt.Text, chkSoloExistencia.Checked);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnFiltroProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCodigoBusqueda.Text))
                    FiltrarProductosPorCodigo(this.ListaProductos, txtCodigoBusqueda.Text, chkSoloExistencia.Checked);
                else if (!string.IsNullOrEmpty(txtProductoBusqueda.Text))
                    FiltrarProductosPorDescripcion(this.ListaProductos, txtProductoBusqueda.Text, chkSoloExistencia.Checked);
                else
                    FiltrarProductosPorDescripcion(this.ListaProductos, "", chkSoloExistencia.Checked);
            }
            catch (Exception ex) { MuestraExcepcion(ex);}
        }

        private void txtCodigoBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((Keys)e.KeyChar == Keys.Enter)
                    FiltrarProductosPorCodigo(this.ListaProductos, txtCodigoBusqueda.Text, chkSoloExistencia.Checked);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtProductoBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            try { 
                if ((Keys)e.KeyChar == Keys.Enter)
                    FiltrarProductosPorDescripcion(this.ListaProductos, txtProductoBusqueda.Text, chkSoloExistencia.Checked);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                    {
                        gvProductos.DataSource = ((List<EntProducto>)((DataGridView)sender).DataSource).OrderBy(P => P.Codigo).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        gvProductos.DataSource = ((List<EntProducto>)((DataGridView)sender).DataSource).OrderByDescending(P => P.Codigo).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                    {
                        gvProductos.DataSource = ((List<EntProducto>)((DataGridView)sender).DataSource).OrderBy(P => P.Descripcion).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        gvProductos.DataSource = ((List<EntProducto>)((DataGridView)sender).DataSource).OrderByDescending(P => P.Descripcion).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                }
                else if (e.ColumnIndex == 4)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                    {
                        gvProductos.DataSource = ((List<EntProducto>)((DataGridView)sender).DataSource).OrderBy(P => P.Existencia).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        gvProductos.DataSource = ((List<EntProducto>)((DataGridView)sender).DataSource).OrderByDescending(P => P.Existencia).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

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

        private void cmbTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void tcProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnEditaPrecioVenta_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbTipoProductoFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbTipoProducto.SelectedIndex = cmbTipoProductoFiltro.SelectedIndex;
                //pnlDatosProductoVenta.Visible = !Convert.ToBoolean(cmbTipoProducto.SelectedIndex);
                CargaProductos(Program.UsuarioSeleccionado.CompañiaId, this.TipoProductoId);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvPreciosVenta_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    // Obtener el producto directamente de la fila editada (más seguro que usar el seleccionado)
                    EntProducto productoSel = null;
                    var row = gvPreciosVenta.Rows[e.RowIndex];
                    if (row != null)
                        productoSel = row.DataBoundItem as EntProducto;

                    if (productoSel == null)
                        productoSel = ObtieneProductoFromGV(gvPreciosVenta);

                    if (productoSel == null)
                    {
                        gvPreciosVenta.Refresh();
                        return;
                    }

                    string colName = gvPreciosVenta.Columns[e.ColumnIndex].Name;

                    // Si se editó la columna PrecioVenta -> calcular PrecioVentaSinIVA y PrecioVenta2
                    if (string.Equals(colName, "gvListaPrecios_PrecioVentaColumn", StringComparison.OrdinalIgnoreCase))
                    {
                        // aplicar IEPS solo si está seleccionado
                        decimal iepsFactor = chkIncluyeIEPS.Checked ? 1.08m : 1.00m;
                        productoSel.PrecioVentaSinIVA = Math.Round(productoSel.PrecioVenta * iepsFactor, 2);
                        productoSel.PrecioVenta2 = productoSel.PrecioVentaSinIVA * 1.02m;
                    }
                    // Si se editó la columna PrecioVenta2 -> proceso inverso: derivar PrecioVentaSinIVA y PrecioVenta
                    else if (string.Equals(colName, "gvListaPrecios_PrecioVenta_2PColumn", StringComparison.OrdinalIgnoreCase)
                          || string.Equals(colName, "gvListaPrecios_PrecioVenta2Column", StringComparison.OrdinalIgnoreCase))
                    {
                        // Evitar división por cero y respetar si IEPS aplica o no
                        decimal iepsFactor = chkIncluyeIEPS.Checked ? 1.08m : 1.00m;
                        if (1.02m != 0 && iepsFactor != 0)
                        {
                            productoSel.PrecioVentaSinIVA = Math.Round(productoSel.PrecioVenta2 / 1.02m, 2);
                            productoSel.PrecioVenta = Math.Round(productoSel.PrecioVentaSinIVA / iepsFactor, 2);
                        }
                    }

                    gvPreciosVenta.Refresh();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnExporta_Click(object sender, EventArgs e)
        {
            try
            {
                //ImprimeProductos vImprime = new Pantallas.ImprimeProductos(ObtieneListaProductosFromGV(gvProductos));
                //vImprime.Show();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvListaPreciosVentaDetalle_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
