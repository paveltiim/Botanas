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
    public partial class ActualizaProducto : Form
    {
        public ActualizaProducto(EntProducto ProductoSeleccionado)
        {
            InitializeComponent();
            this.ProductoSeleccionado= ProductoSeleccionado;
            CargaDatosProducto(ProductoSeleccionado);
        }
        void CargaDatosProducto(EntProducto Producto)
        {
            txtCodigo.Text = Producto.Codigo;
            txtDescripcion.Text = Producto.Descripcion;
            cmbTipoProducto.SelectedIndex = Producto.TipoProductoId - 1;
        }
        EntProducto ProductoSeleccionado;
        void ActualizarProducto(int ProductoId, int TipoProductoId, string Codigo, string Descripcion)
        {
            EntProducto producto = new EntProducto()
            {
                Id = ProductoId,
                TipoProductoId = TipoProductoId,
                Codigo = Codigo,
                Descripcion = Descripcion,
                Fecha = DateTime.Now
            };
            new BusProductos().ActualizaProducto(producto);
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                ActualizarProducto(ProductoSeleccionado.Id, cmbTipoProducto.SelectedIndex + 1, txtCodigo.Text, txtDescripcion.Text);
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        
        private void AgregaDeposito_Load(object sender, EventArgs e)
        {

        }
    }
}
