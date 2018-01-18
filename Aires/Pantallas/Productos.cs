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

        EntProducto ProductoSeleccionado;
        List<EntProducto> ListaProductos = new List<EntProducto>();
        List<EntProducto> ListaProductosDetalle = new List<EntProducto>();

        int IngresoProductoId = 0;

        bool NuevoProducto;

        #region Metodos
        public void CargaProductos(int EmpresaId)
            {
                ListaProductos = new BusProductos().ObtieneProductos(EmpresaId).OrderBy(P => P.Descripcion).ToList();
                gvProductos.DataSource = ListaProductos;

                CargaProductosEnPantallas();
            }
            public void CargaProductosDetalle(int EmpresaId)
            {
                //ListaProductosDetalle = new BusProductos().ObtieneProductosDetalle().OrderBy(P => P.Descripcion).ToList();
                gvProductosDetalle.DataSource = new BusProductos().ObtieneProductosDetalle(EmpresaId).OrderBy(P => P.Descripcion).ToList();
            }
            public void CargaProductosDetalleTodos()
            {
                //SE UTILIZA PARA VERIFICAR NUMERO DE SERIE
                ListaProductosDetalle = new BusProductos().ObtieneProductosDetalleTodos().OrderBy(P => P.Descripcion).ToList();
                //gvProductosDetalle.DataSource = ListaProductosDetalle;
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

            public EntProducto AgregaProducto(int TipoProductoId, string Codigo, string Descripcion)
            {
                EntProducto producto = new EntProducto()
                {
                    TipoProductoId = TipoProductoId,
                    Codigo = Codigo,
                    Descripcion = Descripcion,
                    Fecha=DateTime.Now
                };
                producto.Id = new BusProductos().AgregaProducto(producto);

                return producto;
            }
            public void AgregaProductoDetalle(int ProductoId, int IngresoId, int EmpresaId, string Serie, decimal PrecioCosto, decimal PrecioVenta, decimal PrecioVenta2, decimal PrecioEspecial)
            {
                EntProducto producto = new EntProducto()
                {
                    Id = ProductoId,
                    IngresoId=IngresoId,
                    EmpresaId = EmpresaId,
                    Serie = Serie,
                    PrecioCosto = PrecioCosto,
                    PrecioVenta = PrecioVenta,
                    PrecioVenta2 = PrecioVenta2,
                    PrecioEspecial = PrecioEspecial,
                    Fecha = DateTime.Now
                };
                producto.Id = new BusProductos().AgregaProductoDetalle(producto);
            }
            public int AgregaIngresoProducto(int EmpresaId, string Descripcion, DateTime Fecha)
            {
                EntCatalogoGenerico ingreso = new EntCatalogoGenerico()
                {
                    Id=EmpresaId,
                    Descripcion = Descripcion,
                    Fecha = Fecha
                };
                return new BusProductos().AgregaIngreso(ingreso);
            }

            public void ActualizaProducto(int ProductoId, int TipoProductoId, string Codigo, string Descripcion)
            {
                EntProducto producto = new EntProducto()
                {
                    Id=ProductoId,
                    TipoProductoId = TipoProductoId,
                    Codigo = Codigo,
                    Descripcion = Descripcion,
                    Fecha = DateTime.Now
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
            //void AumentaProductoDetalle(EntProducto Producto, int Cantidad)
            //{
            //    new BusProductos().AumentaProductoDetalle(Producto, Cantidad);
            //}

            void CargaDatosProducto(EntProducto Producto)
            {
                txtCodigo.Text = Producto.Codigo;
                txtDescripcion.Text = Producto.Descripcion;
                txtPrecioCosto.Text = FormatoMoney(0); //FormatoMoney(Producto.PrecioCosto);
                txtPrecioVenta.Text = FormatoMoney(Producto.PrecioVenta);
                txtPrecioVenta2.Text =FormatoMoney( Producto.PrecioVenta2);
                txtPrecioEspecial.Text = FormatoMoney(Producto.PrecioEspecial);
            cmbTipoProducto.SelectedIndex = Producto.TipoProductoId - 1;    
            }

            /// <summary>
            /// Filtra Clientes por los distintos parametros, y los carga en gvClientes.
            /// </summary>
            /// <param name="Clientes"></param>
            /// <param name="Nombre"></param>
            void FiltrarProductosPorDescripcion(List<EntProducto> ListaProductos, string DescripcionProducto)
            {
                //List<EntCliente> clientes = (List<EntCliente>)gvClientes.DataSource;

                var productosFiltro = from c in ListaProductos
                                     where c.Descripcion.ToUpper().Contains(DescripcionProducto.ToUpper())
                                     select c;

                gvProductos.DataSource = null;
                gvProductos.DataSource = productosFiltro.ToList();
            }
            void FiltrarProductosPorCodigo(List<EntProducto> ListaProductos, string CodigoProducto)
            {
                var productosFiltro = from c in ListaProductos
                                      where c.Codigo.ToUpper().Contains(CodigoProducto.ToUpper())
                                      select c;

                gvProductos.DataSource = null;
                gvProductos.DataSource = productosFiltro.ToList();
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
        List<EntEmpresa> ListaEmpresas;

        void InicializaPantalla()
        {
            if (Program.UsuarioSeleccionado.Id > 1)
            {
                btnEliminar.Enabled = false;
                pnlDatos.Visible = false;
                lbTituloIngreso.Visible = false;
            }
            LimpiaTextBox(pnlDatos);
            ActivaAgregar(true);
            //if(Program.EmpresaSeleccionada!=null)
            //    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }
        public void CargaEmpresas()
        {
            if (Program.UsuarioSeleccionado.Id > 1)
                ListaEmpresas = new BusEmpresas().ObtieneEmpresas().Where(P => P.UsuarioId == Program.UsuarioSeleccionado.Id).ToList();
            else
                ListaEmpresas = new BusEmpresas().ObtieneEmpresas();

            Program.CambiaEmpresa = false;
            cmbEmpresas.DataSource = ListaEmpresas;
            Program.CambiaEmpresa = true;

            //CargaClientesEnPantallas();
        }
        private void Clientes_Load(object sender, EventArgs e)
        {
            try
            {
                InicializaPantalla();
                CargaEmpresas();

                if (Program.EmpresaSeleccionada == null)
                    Program.EmpresaSeleccionada = SeleccionaEmpresa();

                cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);

                CargaProductos(Program.EmpresaSeleccionada.Id);
                CargaProductosDetalle(Program.EmpresaSeleccionada.Id);
                CargaProductosDetalleTodos();
                CargaProveedores(Program.EmpresaSeleccionada.Id);
                gvSeries.DataSource = null;
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
                    EntEmpresa empresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                    if (cmbTipoProducto.SelectedIndex == 0)//PRODUCTO
                    {
                        List<EntCatalogoGenerico> series = new List<EntCatalogoGenerico>(); ;

                        switch (tcSeries.SelectedIndex)
                        {
                            case 0://Series Manual
                                series = ObtieneListaGenericaFromGV(gvSeries);
                                if (series == null)
                                    MandaExcepcion("Agrege Serie del Producto");
                                break;
                            case 1://Series Automatico
                                series = ObtieneListaGenericaFromGV(gvSeriesAutomatico);
                                if (series == null)
                                    MandaExcepcion("Agrege Serie Inicio y/o Serie Fin");
                                break;
                            case 2://Sin Serie
                                int cantidadProductos = ConvierteTextoAInteger(txtCantidadSinSerie.Text);
                                if (cantidadProductos == 0)
                                {
                                    txtCantidadSinSerie.Focus();
                                    throw new Exception("Ingrese Cantidad de Productos sin Serie a agregar");
                                }
                                series = new List<EntCatalogoGenerico>();
                                for (int x = 0; x < cantidadProductos; x++)
                                {
                                    EntCatalogoGenerico serie = new EntCatalogoGenerico();
                                    serie.Descripcion = "";
                                    series.Add(serie);
                                }
                                break;
                        }

                        if (NuevoProducto)
                            ProductoSeleccionado = AgregaProducto(cmbTipoProducto.SelectedIndex + 1, txtCodigo.Text, txtDescripcion.Text);

                        foreach (EntCatalogoGenerico s in series)
                        {
                            AgregaProductoDetalle(ProductoSeleccionado.Id, IngresoProductoId, empresaSeleccionada.Id, s.Descripcion, ConvierteTextoADecimal(txtPrecioCosto.Text), ConvierteTextoADecimal(txtPrecioVenta.Text), ConvierteTextoADecimal(txtPrecioVenta2.Text), ConvierteTextoADecimal(txtPrecioEspecial.Text));
                        }

                        //if(series.Count==0)
                        //    AumentaProducto(ProductoSeleccionado,1);
                        //else
                        AumentaProducto(ProductoSeleccionado.Id, series.Count);
                    }
                    else //SERVICIO
                    {
                        if (NuevoProducto)
                            ProductoSeleccionado = AgregaProducto(cmbTipoProducto.SelectedIndex + 1, txtCodigo.Text, txtDescripcion.Text);

                        AgregaProductoDetalle(ProductoSeleccionado.Id, IngresoProductoId, empresaSeleccionada.Id, "", ConvierteTextoADecimal(txtPrecioCosto.Text), ConvierteTextoADecimal(txtPrecioVenta.Text), ConvierteTextoADecimal(txtPrecioVenta2.Text), ConvierteTextoADecimal(txtPrecioEspecial.Text));
                        AumentaProducto(ProductoSeleccionado.Id, 1);
                    }
                }
              
                CargaProductos(Program.EmpresaSeleccionada.Id);
                CargaProductosDetalle(Program.EmpresaSeleccionada.Id);
                CargaProductosDetalleTodos();
                LimpiaTextBox(tcDatosProductoIngresa, true);
                chkPrecioCosto0.Checked = false;
                gvSeries.DataSource = null;
                gvSeriesAutomatico.DataSource = null;
                lbContadorSeries.Text = "0";
                ProductoSeleccionado = null;
                //NuevoProducto = true;
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
                gvSeries.DataSource = null;
                gvSeriesAutomatico.DataSource = null;
                lbContadorSeries.Text = "0";
                ProductoSeleccionado = null;
                //NuevoProducto = true;
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
                    if (cmbTipoProducto.SelectedIndex == 0)//PRODUCTO
                    {
                        List<EntCatalogoGenerico> series = new List<EntCatalogoGenerico>(); ;

                        switch (tcSeries.SelectedIndex)
                        {
                            case 0://Series Manual
                                series = ObtieneListaGenericaFromGV(gvSeries);
                                if (series == null)
                                    MandaExcepcion("Agrege Serie del Producto");
                                break;
                            case 1://Series Automatico
                                series = ObtieneListaGenericaFromGV(gvSeriesAutomatico);
                                if (series == null)
                                    MandaExcepcion("Agrege Serie Inicio y/o Serie Fin");
                                break;
                            case 2://Sin Serie
                                int cantidadProductos = ConvierteTextoAInteger(txtCantidadSinSerie.Text);
                                if (cantidadProductos == 0)
                                {
                                    txtCantidadSinSerie.Focus();
                                    throw new Exception("Ingrese Cantidad de Productos sin Serie a agregar");
                                }
                                series = new List<EntCatalogoGenerico>();
                                for (int x = 0; x < cantidadProductos; x++)
                                {
                                    EntCatalogoGenerico serie = new EntCatalogoGenerico();
                                    serie.Descripcion = "";
                                    series.Add(serie);
                                }
                                break;
                        }

                        ActualizaProducto(ProductoSeleccionado.Id, cmbTipoProducto.SelectedIndex + 1, txtCodigo.Text, txtDescripcion.Text);

                        foreach (EntCatalogoGenerico s in series)
                        {
                            AgregaProductoDetalle(ProductoSeleccionado.Id, IngresoProductoId, Program.EmpresaSeleccionada.Id, s.Descripcion, ConvierteTextoADecimal(txtPrecioCosto.Text), ConvierteTextoADecimal(txtPrecioVenta.Text), ConvierteTextoADecimal(txtPrecioVenta2.Text), ConvierteTextoADecimal(txtPrecioEspecial.Text));
                        }

                        AumentaProducto(ProductoSeleccionado.Id, series.Count);
                    }
                    else //SERVICIO
                    {
                        ActualizaProducto(ProductoSeleccionado.Id, cmbTipoProducto.SelectedIndex + 1, txtCodigo.Text, txtDescripcion.Text);

                        if (ProductoSeleccionado.Existencia == 0)
                        {
                            AgregaProductoDetalle(ProductoSeleccionado.Id, IngresoProductoId, Program.EmpresaSeleccionada.Id, "", ConvierteTextoADecimal(txtPrecioCosto.Text), ConvierteTextoADecimal(txtPrecioVenta.Text), ConvierteTextoADecimal(txtPrecioVenta2.Text), ConvierteTextoADecimal(txtPrecioEspecial.Text));
                            AumentaProducto(ProductoSeleccionado.Id, 1);
                        }
                    }
                }

                CargaProductos(Program.EmpresaSeleccionada.Id);

                CargaProductosDetalle(Program.EmpresaSeleccionada.Id);
                CargaProductosDetalleTodos();

                LimpiaTextBox(tcDatosProductoIngresa, true);
                chkPrecioCosto0.Checked = false;
                ActivaAgregar(true);

                gvSeries.DataSource = null; 
                gvSeriesAutomatico.DataSource = null;

                lbContadorSeries.Text = "0";
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

                if (MuestraMensajeYesNo(string.Format("¿Desea Eliminar el Producto: {0} - {1}? \n Se eliminará de todas las Empresas donde este registrado.", ProductoSeleccionado.Codigo, ProductoSeleccionado.Descripcion), "CONFIRMACIÓN ELIMINAR") == DialogResult.Yes)
                {
                    ActualizaEstatusProducto(ProductoSeleccionado, false);
                    ProductoSeleccionado = null;

                    CargaProductos(Program.EmpresaSeleccionada.Id);

                    CargaProductosDetalle(Program.EmpresaSeleccionada.Id);
                    CargaProductosDetalleTodos();
                    LimpiaTextBox(pnlDatos);
                    ////NuevoProducto = true;
                    ActivaAgregar(true);

                    gvSeries.DataSource = null;
                    gvSeriesAutomatico.DataSource = null;
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                ProductoSeleccionado = ObtieneProductoFromGV(gvProductos);
                ActualizaProducto vActualizaProd = new Pantallas.ActualizaProducto(ProductoSeleccionado);
                if (vActualizaProd.ShowDialog() == DialogResult.OK)
                {
                    CargaProductos(Program.EmpresaSeleccionada.Id);
                    CargaProductosDetalle(Program.EmpresaSeleccionada.Id);
                    LimpiaTextBox(pnlDatos);
                    ActivaAgregar(true);

                    gvSeries.DataSource = null;
                    gvSeriesAutomatico.DataSource = null;

                    btnFiltroProducto.PerformClick();
                }
                //CargaDatosProducto(ProductoSeleccionado);
                ////NuevoProducto = false;
                //ActivaAgregar(false);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        
        private void gvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (btnEditar.Enabled)
                //{
                if (txtCodigo.Enabled)
                {
                    ProductoSeleccionado = ObtieneProductoFromGV(gvProductos);
                    CargaDatosProducto(ProductoSeleccionado);
                    ActivaAgregar(false);
                }else
                    {
                    //EntProducto productoSeleccionado = ObtieneProductoFromGV(gvProductos);
                    //MuestraProductosDetalle vMuestraDetalle = new MuestraProductosDetalle(new BusProductos().ObtieneProductosDetalle().Where(P => P.ProductoId == productoSeleccionado.Id).ToList());
                    //vMuestraDetalle.Show();
                    btnVerSeriesProducto.PerformClick();
                }
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
                ProductoSeleccionado = ObtieneProductoPorCodigo(ObtieneListaProductosFromGV(gvProductos), txtCodigo.Text);
                if (ProductoSeleccionado != null)
                {
                    CargaDatosProducto(ProductoSeleccionado);
                    ////NuevoProducto = false;
                    //ActivaAgregar(false);
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
                    //ProductoSeleccionado = ObtieneProductoPorCodigo(ObtieneListaProductosFromGV(gvProductos), txtCodigo.Text);
                    //if (ProductoSeleccionado != null)
                    //{
                    //    CargaDatosProducto(ProductoSeleccionado);
                    //    //NuevoProducto = false;
                    //    ActivaAgregar(false);

                    //    txtPrecioCosto.Focus();
                    //}
                    //else
                    //{
                    //    string codigo = txtCodigo.Text;
                    //    LimpiaTextBox(tcDatosProductoIngresa,true);
                    //    //NuevoProducto = true;
                    //    ActivaAgregar(true);
                    //    //ProductoSeleccionado = null;

                    //    txtCodigo.Text = codigo;
                    //    txtDescripcion.Focus();
                    //}
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnAgregaSerie_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSerie.Text))
                    MandaExcepcion("Ingrese Serie del Producto");
                EntCatalogoGenerico serie = new EntCatalogoGenerico();
                serie.Descripcion = txtSerie.Text;

                List<EntCatalogoGenerico> listaSerie = ObtieneListaGenericaFromGV(gvSeries);
                if (listaSerie == null)
                    listaSerie = new List<EntCatalogoGenerico>();
                else
                {
                    if (listaSerie.Where(P => P.Descripcion == serie.Descripcion).ToList().Count > 0)
                        throw new Exception("La Serie ya se encuentra registrada en este Ingreso");
                }

                if (ListaProductosDetalle.Where(P => P.Serie == serie.Descripcion).ToList().Count > 0)
                    throw new Exception("La Serie ya se registro en Ingreso previo");

                listaSerie.Add(serie);

                gvSeries.DataSource = null;
                gvSeries.DataSource = listaSerie;
                gvSeries.Refresh();

                lbContadorSeries.Text = listaSerie.Count.ToString();
                txtSerie.Text = "";
                txtSerie.Focus();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            try {
                if ((Keys)e.KeyChar == Keys.Enter)
                    btnAgregaSerie.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                CargaProductos(Program.EmpresaSeleccionada.Id);
                CargaProductosDetalle(Program.EmpresaSeleccionada.Id);
                CargaProductosDetalleTodos();
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

        private void btnNuevoIngresoProducto_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiaTextBox(tcDatosIngreso);

                btnNuevoIngresoProducto.Visible = false;
                tcDatosIngreso.Enabled = true;

                btnCancelar.PerformClick();
                tcDatosProductoIngresa.Enabled = false;
                ////gvProductos.Enabled = false;
                //btnEditar.Enabled = false;
                //btnEliminar.Enabled = false;

                txtDescripcionIngreso.Focus();
            }
            catch (Exception ex) {
                MuestraExcepcion(ex);
            }
        }

        private void btnIngresaProducto_Click(object sender, EventArgs e)
        {
            try
            {
                IngresoProductoId=AgregaIngresoProducto(ConvierteTextoAInteger(cmbProveedores.SelectedValue.ToString()),txtDescripcionIngreso.Text, dtpFechaIngreso.Value);

                btnNuevoIngresoProducto.Visible = true;
                btnNuevoIngresoProducto.BringToFront();
                tcDatosIngreso.Enabled = false;
                tcDatosProductoIngresa.Enabled = true;
                ////gvProductos.Enabled = true;
                //btnEditar.Enabled = true;
                //btnEliminar.Enabled = true;
                
                txtCodigo.Focus();
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }

        private void txtDescripcionIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            try {
                if ((Keys)e.KeyChar == Keys.Enter)
                {
                    btnIngresaProducto.PerformClick();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnAgregaSerieAutomatico_Click(object sender, EventArgs e)
        {
            try
            {
                int serieInicio,serieFin;

                serieInicio=ConvierteTextoAInteger(txtSerieInicio.Text);
                serieFin=ConvierteTextoAInteger(txtSerieFin.Text);

                txtSerieInicio.Text = serieInicio.ToString();
                txtSerieFin.Text = serieFin.ToString();
                
                if (serieInicio >= serieFin)
                    throw new Exception("La Serie Fin debe ser mayor a la Serie Inicio.");

                List<EntCatalogoGenerico> listaSerie = new List<EntCatalogoGenerico>();
                for (int x = serieInicio; x <= serieFin; x++)
                {
                    EntCatalogoGenerico serie = new EntCatalogoGenerico();
                    serie.Descripcion = x.ToString().PadLeft(4,'0');

                    listaSerie.Add(serie);
                }

                gvSeriesAutomatico.DataSource = null;
                gvSeriesAutomatico.DataSource = listaSerie;
                gvSeriesAutomatico.Refresh();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtSerieInicio_Validated(object sender, EventArgs e)
        {
            //foreach(char c in txtSerieInicio.Text.ToCharArray()){
            //    if (char.IsLetter(c))
            //        errorProvider1.SetError((Control)sender, "Debe ser Numerico");
            //}
        }

        private void txtSerieInicio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtSerieInicio_Leave(object sender, EventArgs e)
        {
            try
            {
                ((TextBox)sender).Text=ConvierteTextoAInteger(((TextBox)sender).Text).ToString();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtSerieFin_Leave(object sender, EventArgs e)
        {
            try
            {
                ((TextBox)sender).Text = ConvierteTextoAInteger(((TextBox)sender).Text).ToString();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtProductoBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = ((TextBox)sender);
                //if (string.IsNullOrEmpty(txt.Text))
                //    CargaProductos(Program.EmpresaSeleccionada.Id);
                //else
                    FiltrarProductosPorDescripcion(ListaProductos, txt.Text);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtCodigoBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = ((TextBox)sender);
                //if (string.IsNullOrEmpty(txt.Text))
                //    CargaProductos();
                //else
                    FiltrarProductosPorCodigo(ListaProductos, txt.Text);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnVerSeriesProducto_Click(object sender, EventArgs e)
        {
            try
            {
                EntProducto productoSeleccionado = ObtieneProductoFromGV(gvProductos);
                //MuestraProductosDetalle vMuestraDetalle = new MuestraProductosDetalle(new BusProductos().ObtieneProductosDetalle().Where(P=>P.ProductoId==productoSeleccionado.Id).ToList());
                MuestraProductosDetalle vMuestraDetalle = new MuestraProductosDetalle(ObtieneListaProductosFromGV(gvProductosDetalle).Where(P => P.ProductoId == productoSeleccionado.Id).ToList());
                vMuestraDetalle.Show();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnActualizaGasto_Click(object sender, EventArgs e)
        {
            try
            {
                CargaProductos(Program.EmpresaSeleccionada.Id);
                CargaProductosDetalle(Program.EmpresaSeleccionada.Id);
                CargaProductosDetalleTodos();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        public void CargaProveedores(int EmpresaId)
        {
            cmbProveedores.DataSource = new BusProveedores().ObtieneProveedores(EmpresaId);
        }

        private void btnFiltroProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCodigoBusqueda.Text))
                    FiltrarProductosPorCodigo(ListaProductos, txtCodigoBusqueda.Text);
                else if (!string.IsNullOrEmpty(txtProductoBusqueda.Text))
                    FiltrarProductosPorDescripcion(ListaProductos, txtProductoBusqueda.Text);
                else
                    FiltrarProductosPorDescripcion(ListaProductos, "");

                //CargaProductos();
            }
            catch (Exception ex) { MuestraExcepcion(ex);}
        }

        private void txtCodigoBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((Keys)e.KeyChar == Keys.Enter)
                    FiltrarProductosPorCodigo(ListaProductos, txtCodigoBusqueda.Text);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtProductoBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            try { 
                if ((Keys)e.KeyChar == Keys.Enter)
                    FiltrarProductosPorDescripcion(ListaProductos, txtProductoBusqueda.Text);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvSeries_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if ((Keys)e.KeyChar == Keys.Enter)
                //    FiltrarProductosPorDescripcion(ListaProductos, txtProductoBusqueda.Text);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvSeries_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int index = 0;
                if ((Keys)e.KeyChar == Keys.Back)
                {
                    index = gvSeries.CurrentRow.Index;
                    List<EntCatalogoGenerico> lst=ObtieneListaGenericaFromGV(gvSeries);
                    lst.RemoveAt(index);

                    gvSeries.DataSource = null;
                    gvSeries.DataSource = lst;

                    lbContadorSeries.Text = lst.Count.ToString();
                }

            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnCambioDivisa_Click(object sender, EventArgs e)
        {
            try {
                CambioDivisa vCambioDiv = new Pantallas.CambioDivisa();
                if (vCambioDiv.ShowDialog() == DialogResult.OK)
                {
                    txtPrecioCosto.Text = FormatoMoney(vCambioDiv.Cambio);
                }
            } catch(Exception ex) { MuestraExcepcion(ex); }
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

        private void gvProductosDetalle_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                    {
                        gvProductosDetalle.DataSource = ((List<EntProducto>)((DataGridView)sender).DataSource).OrderBy(P => P.Codigo).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        gvProductosDetalle.DataSource = ((List<EntProducto>)((DataGridView)sender).DataSource).OrderByDescending(P => P.Codigo).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                    {
                        gvProductosDetalle.DataSource = ((List<EntProducto>)((DataGridView)sender).DataSource).OrderBy(P => P.Descripcion).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        gvProductosDetalle.DataSource = ((List<EntProducto>)((DataGridView)sender).DataSource).OrderByDescending(P => P.Descripcion).ToList();
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
                    CargaProveedores(Program.EmpresaSeleccionada.Id);
                    CargaProductos(Program.EmpresaSeleccionada.Id);
                    CargaProductosDetalle(Program.EmpresaSeleccionada.Id);
                    btnNuevoIngresoProducto.PerformClick();
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
                if (cmbTipoProducto.SelectedIndex == 0)
                    tcSeries.Visible = true;
                else
                    tcSeries.Visible = false;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnMateriales_Click(object sender, EventArgs e)
        {
            try
            {
                EntProducto productoSeleccionado = ObtieneProductoFromGV(gvProductos);
                AgregaMaterialesProducto vMaterialesKit = new AgregaMaterialesProducto(productoSeleccionado);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
    }
}
