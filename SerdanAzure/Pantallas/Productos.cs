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
        public void CargaProductos(int EmpresaId)
        {
            this.ListaProductos = new BusProductos().ObtieneProductos(EmpresaId).OrderBy(P => P.Descripcion).ToList();
            gvProductos.DataSource = this.ListaProductos;

            CargaProductosEnPantallas();
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

        public EntProducto AgregaProducto(int TipoProductoId, string Codigo, string Descripcion,
                                        string Marca, string Modelo,
                                        int TipoProductoServicioId, int TipoUnidadId, decimal PrecioCosto)
        {
            EntProducto producto = new EntProducto()
            {
                TipoProductoId = TipoProductoId,
                Codigo = Codigo,
                Descripcion = Descripcion,
                Marca = Marca,
                Modelo = Modelo,
                ProductoServicioId = TipoProductoServicioId,
                UnidadId = TipoUnidadId,
                PrecioCosto=PrecioCosto
            };
            producto.Id = new BusProductos().AgregaProducto(producto);

            return producto;
        }
        public int AgregaProductoDetalle(int ProductoId, int IngresoId, int EmpresaId,
                                       string Marca, string Modelo, string Serie, decimal PrecioCosto, decimal PrecioVenta, decimal PrecioVenta2, decimal PrecioEspecial)
        {
            EntProducto producto = new EntProducto()
            {
                Id = ProductoId,
                IngresoId = IngresoId,
                EmpresaId = EmpresaId,
                Marca = Marca,
                Modelo = Modelo,
                Serie = Serie,
                PrecioCosto = PrecioCosto,
                PrecioVenta = PrecioVenta,
                PrecioVenta2 = PrecioVenta2,
                PrecioEspecial = PrecioEspecial,
                Fecha = DateTime.Now
            };
            producto.Id = new BusProductos().AgregaProductoDetalle(producto);
            return producto.Id;
        }
        public int AgregaIngresoProducto(int EmpresaId, string Descripcion, DateTime Fecha)
        {
            EntCatalogoGenerico ingreso = new EntCatalogoGenerico()
            {
                Id = EmpresaId,
                Descripcion = Descripcion,
                Fecha = Fecha
            };
            return new BusProductos().AgregaIngreso(ingreso);
        }

        public void ActualizaProducto(int ProductoId, int TipoProductoId, string Codigo, string Descripcion,
                                        string Marca, string Modelo,
                                        int TipoProductoServicioId, int TipoUnidadId, decimal PrecioCosto)
        {
            EntProducto producto = new EntProducto()
            {
                Id = ProductoId,
                TipoProductoId = TipoProductoId,
                Codigo = Codigo,
                Descripcion = Descripcion,
                Marca = Marca,
                Modelo = Modelo,
                ProductoServicioId = TipoProductoServicioId,
                UnidadId = TipoUnidadId,
                PrecioCosto = PrecioCosto
            };
            new BusProductos().ActualizaProducto(producto);
        }
        public void ActualizaEstatusProducto(EntProducto Producto, bool Estatus)
        {
            Producto.Estatus = Estatus;
            new BusProductos().ActualizaEstatusProducto(Producto);
        }
        public void ActualizaEstatusProductoDetalle(EntProducto Producto, int EstatusId)
        {
            Producto.EstatusId = EstatusId;
            new BusProductos().ActualizaEstatusProductoDetalle(Producto);
        }

        public void AumentaProducto(int ProductoId, int Cantidad)
        {
            new BusProductos().AumentaProducto(ProductoId, Cantidad);
        }

        void CargaDatosProducto(EntProducto Producto)
        {
            txtCodigo.Text = Producto.Codigo;
            txtDescripcion.Text = Producto.Descripcion;
            txtPrecioCosto.Text = FormatoMoney(Producto.PrecioCosto);
            txtMarca.Text = Producto.Marca;
            txtModelo.Text = Producto.Modelo;

            cmbTipoProducto.SelectedIndex = Producto.TipoProductoId - 1;

            cmbTipoProductoServicio.SelectedIndex = ((List<EntCatalogoGenerico>)cmbTipoProductoServicio.DataSource).FindIndex(P => P.Id == Producto.ProductoServicioId);
            cmbTipoUnidad.SelectedIndex = ((List<EntCatalogoGenerico>)cmbTipoUnidad.DataSource).FindIndex(P => P.Id == Producto.UnidadId);

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

        //public void CargaEmpresas()
        //{
        //    if (Program.UsuarioSeleccionado.Id > 1)
        //        ListaEmpresas = new BusEmpresas().ObtieneEmpresas().Where(P => P.UsuarioId == Program.UsuarioSeleccionado.Id).ToList();
        //    else
        //        ListaEmpresas = new BusEmpresas().ObtieneEmpresas();

        //    Program.CambiaEmpresa = false;
        //    cmbEmpresas.DataSource = ListaEmpresas;
        //    Program.CambiaEmpresa = true;

        //    //CargaClientesEnPantallas();
        //}  
        void InicializaPantalla()
        {
            LimpiaTextBox(pnlDatos);
            //ActivaAgregar(true);

            cmbTipoProducto.SelectedIndex = 0;
            cmbTipoProductoServicio.SelectedIndex = 0;
            cmbTipoUnidad.SelectedIndex = 0;
        } 


        private void Productos_Load(object sender, EventArgs e)
        {
            try
            {
                CargaProductosServicios();
                CargaUnidades();

                InicializaPantalla();
                //CargaEmpresas();

                //if (Program.EmpresaSeleccionada == null)
                //    Program.EmpresaSeleccionada = SeleccionaEmpresa();

                //cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);

                CargaProductos(establecimientoId);
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
                else if(ConvierteTextoADecimal(txtPrecioCosto.Text)==0 && !chkPrecioCosto0.Checked && cmbTipoProducto.SelectedIndex == 0)
                {
                    MandaExcepcion("Asigne un Precio de Costo mayor a $0 o seleccione la casilla 'Precio Costo $0'");
                }
                else
                {
                    //EntEmpresa empresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                    EntCatalogoGenerico productoservicio = ObtieneCatalogoGenericoFromCmb(cmbTipoProductoServicio);
                    EntCatalogoGenerico unidad = ObtieneCatalogoGenericoFromCmb(cmbTipoUnidad);
                    ProductoSeleccionado = AgregaProducto(cmbTipoProducto.SelectedIndex + 1, txtCodigo.Text, txtDescripcion.Text,                                           txtMarca.Text, txtModelo.Text,
                                                          productoservicio.Id, unidad.Id, ConvierteTextoADecimal(txtPrecioCosto));
                }

                CargaProductos(establecimientoId);
                //chkSoloExistencia.Checked = !chkSoloExistencia.Checked;
                LimpiaTextBox(tcDatosProductoIngresa, true);
                chkPrecioCosto0.Checked = false;
                this.ProductoSeleccionado = null;
                ActivaAgregar(true);
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
                else if (ConvierteTextoADecimal(txtPrecioCosto.Text) == 0 && !chkPrecioCosto0.Checked && cmbTipoProducto.SelectedIndex == 0)
                {
                    MandaExcepcion("Asigne un Precio de Costo mayor a $0 ó seleccione \n la casilla 'Precio Costo $0'");
                }
                else
                {
                    EntCatalogoGenerico productoservicio = ObtieneCatalogoGenericoFromCmb(cmbTipoProductoServicio);
                    EntCatalogoGenerico unidad = ObtieneCatalogoGenericoFromCmb(cmbTipoUnidad);

                    ActualizaProducto(this.ProductoSeleccionado.Id, cmbTipoProducto.SelectedIndex + 1, txtCodigo.Text,          
                                        txtDescripcion.Text, txtMarca.Text, txtModelo.Text,
                                        productoservicio.Id, unidad.Id, ConvierteTextoADecimal(txtPrecioCosto));
                }

                CargaProductos(establecimientoId);

                chkSoloExistencia.Checked = !chkSoloExistencia.Checked;
                chkSoloExistencia.Checked = !chkSoloExistencia.Checked;

                LimpiaTextBox(tcDatosProductoIngresa, true);
                chkPrecioCosto0.Checked = false;
                ActivaAgregar(true);
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        void ActivaAgregar(bool Visible) {
            btnAgregar.Visible = Visible;
            btnActualizar.Visible = !Visible;
            txtCodigo.Focus();

            NuevoProducto = Visible;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiaTextBox(tcDatosProductoIngresa, true);
                chkPrecioCosto0.Checked = false;
                this.ProductoSeleccionado = null;
                ActivaAgregar(true);

                CargaProductos(this.establecimientoId);
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

                    CargaProductos(establecimientoId);
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
                this.ProductoSeleccionado = ObtieneProductoFromGV(gvProductos);
                ActualizaProducto vActualizaProd = new Pantallas.ActualizaProducto(this.ProductoSeleccionado);
                if (vActualizaProd.ShowDialog() == DialogResult.OK)
                {
                    CargaProductos(establecimientoId);
                    LimpiaTextBox(pnlDatos);
                    ActivaAgregar(true);

                    btnFiltroProducto.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        
        private void gvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.ProductoSeleccionado = ObtieneProductoFromGV(gvProductos);
                CargaDatosProducto(this.ProductoSeleccionado);
                ActivaAgregar(false);
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

                    txtCodigo.Text = codigo;
                    txtDescripcion.Focus();
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
                CargaProductos(establecimientoId);
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
                if (Program.CambiaEmpresa)
                {
                    Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                    CargaProductos(Program.EmpresaSeleccionada.Id);
                    btnFiltroProducto.PerformClick();
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

        private void label21_Click(object sender, EventArgs e)
        {

        }

    }
}
