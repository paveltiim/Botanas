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
    public partial class ActualizaProducto : FormBase
    {
        public ActualizaProducto(EntProducto ProductoSeleccionado)
        {
            InitializeComponent();

            CargaProductosServicios();
            CargaUnidades();

            this.ProductoSeleccionado= ProductoSeleccionado;
            CargaDatosProducto(ProductoSeleccionado);
        }
        void CargaDatosProducto(EntProducto Producto)
        {
            txtCodigo.Text = Producto.Codigo;
            txtDescripcion.Text = Producto.Descripcion;
            txtMarca.Text = Producto.Marca;
            txtModelo.Text = Producto.Modelo;
            cmbTipoProducto.SelectedIndex = Producto.TipoProductoId - 1;

            cmbTipoProductoServicio.SelectedIndex = ((List<EntCatalogoGenerico>)cmbTipoProductoServicio.DataSource).FindIndex(P => P.Id == Producto.ProductoServicioId);
            cmbTipoUnidad.SelectedIndex = ((List<EntCatalogoGenerico>)cmbTipoUnidad.DataSource).FindIndex(P => P.Id == Producto.UnidadId);

        }

        EntProducto ProductoSeleccionado;
        //void ActualizarProducto(int ProductoId, int TipoProductoId, string Codigo, string Descripcion)
        //{
        //    EntProducto producto = new EntProducto()
        //    {
        //        Id = ProductoId,
        //        TipoProductoId = TipoProductoId,
        //        Codigo = Codigo,
        //        Descripcion = Descripcion,
        //        Fecha = DateTime.Now
        //    };
        //    new BusProductos().ActualizaProducto(producto);
        //}
        //void ActualizaProducto(int ProductoId, int TipoProductoId, string Codigo, string Descripcion,
        //                           int TipoProductoServicioId, int TipoUnidadId)
        //{
        //    EntProducto producto = new EntProducto()
        //    {
        //        Id = ProductoId,
        //        TipoProductoId = TipoProductoId,
        //        Codigo = Codigo,
        //        Descripcion = Descripcion,
        //        ProductoServicioId = TipoProductoServicioId,
        //        UnidadId = TipoUnidadId,
        //        Fecha = DateTime.Now
        //    };
        //    new BusProductos().ActualizaProducto(producto);
        //}

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
        
        private void AgregaDeposito_Load(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor; 
                
                if (ConvierteTextoADecimal(txtPrecioCosto.Text) == 0 && !chkPrecioCosto0.Checked && cmbTipoProducto.SelectedIndex == 0)
                    MandaExcepcion("Asigne un Precio de Costo mayor a $0 o seleccione la casilla 'Precio Costo $0'");
                
                EntCatalogoGenerico productoservicio = ObtieneCatalogoGenericoFromCmb(cmbTipoProductoServicio);
                EntCatalogoGenerico unidad = ObtieneCatalogoGenericoFromCmb(cmbTipoUnidad);

                new Productos().ActualizaProducto(ProductoSeleccionado.Id, cmbTipoProducto.SelectedIndex + 1, txtCodigo.Text,           
                                                    txtDescripcion.Text, txtMarca.Text, txtModelo.Text,
                                                    productoservicio.Id, unidad.Id, ConvierteTextoADecimal(txtPrecioCosto));

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}
