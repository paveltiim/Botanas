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
    public partial class MuestraProductosDetalle : FormBase
    {
        List<AiresEntidades.EntProducto> ListaProductosDetalle;
        public MuestraProductosDetalle(List<AiresEntidades.EntProducto> ProductosDetalle)
        {
            InitializeComponent();

            ListaProductosDetalle = ProductosDetalle;
            gvProductosDetalle.DataSource = ProductosDetalle;
        }

        void ActualizaProductoDetalle(int ProductoId, string Serie, decimal PrecioCosto)
        {
            EntProducto prod = new EntProducto() {
                Id=ProductoId,
                Serie=Serie,
                PrecioCosto=PrecioCosto
            };
        }
        //public EntProducto ObtieneEmpresaFromGV(DataGridView GridViewEmpresas)
        //{
        //    if (GridViewEmpresas.CurrentRow == null)
        //        return null;
        //    return (EntEmpresa)((List<EntEmpresa>)GridViewEmpresas.DataSource)[GridViewEmpresas.CurrentRow.Index];
        //}

        //public AiresEntidades.EntEmpresa EmpresaGastoSeleccionada { get { return ObtieneEmpresaFromGV(gvGastosEmpresa); } }
        

        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void txtCantidadPaga_Leave(object sender, EventArgs e)
        {

        }
        
        private void SeleccionaFactura_Load(object sender, EventArgs e)
        {

        }

        private void txtFiltroSerie_TextChanged(object sender, EventArgs e)
        {
            //List<EntProducto> lst= (List<EntProducto>)gvGastosEmpresa.DataSource;
            gvProductosDetalle.DataSource = ListaProductosDetalle.Where(P => P.Serie.ToUpper().Contains(txtFiltroSerie.Text.ToUpper())).ToList();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            try {
                if(MuestraMensajeYesNo("¿Desea guardar los cambios realizdos?") == DialogResult.Yes)
                {
                    List<EntProducto> productosSeleccionados = ObtieneListaProductosFromGV(gvProductosDetalle);
                    foreach (EntProducto p in productosSeleccionados) {
                        new BusProductos().ActualizaProductoDetalle(p);
                        new BusProductos().ActualizaProductoDetallePedido(p);
                    }
                }
            }catch(Exception ex) { MuestraExcepcion(ex); }
        }
        void CargaProductosEnPantallas()
        {
            Form forma = BuscaFormaBase(new Ventas().Titulo);
            if (forma != null)
            {
                //((Ventas)forma).CargaProductos();
                ((Ventas)forma).CargaProductosDetalle(Program.EmpresaSeleccionada.Id);
            }
            forma = BuscaFormaBase(new Productos().Titulo);
            if (forma != null)
            {
                //((Ventas)forma).CargaProductos();
                ((Productos)forma).CargaProductosDetalle(Program.EmpresaSeleccionada.Id);
                ((Productos)forma).CargaProductos(Program.EmpresaSeleccionada.Id);
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MuestraMensajeYesNo("¿Desea eliminar el Producto seleccionado?") == DialogResult.Yes)
                {
                    //EntProducto productoSeleccionado = ObtieneProductoFromGV(gvProductosDetalle);
                    List<EntProducto> productosSeleccionados = ObtieneListaProductosFromGV(gvProductosDetalle);
                    foreach (EntProducto p in productosSeleccionados)
                    {
                        if (p.Estatus)
                        {
                            p.EstatusId = 0;
                            new BusProductos().ActualizaEstatusProductoDetalle(p);
                            int productoId = p.ProductoId;
                            new BusProductos().AumentaProducto(productoId, -1);
                        }
                        //ListaProductosDetalle.Remove(p);
                    }
                    ////foreach (EntProducto p in productosSeleccionados)
                    ////{
                    ////    new BusProductos().ActualizaProductoDetalle(p);
                    ////    new BusProductos().ActualizaProductoDetallePedido(p);
                    ////}
                    //gvProductosDetalle.DataSource = null;
                    //gvProductosDetalle.DataSource = ListaProductosDetalle;
                    this.Close();
                    //CargaProductosEnPantallas(); //NO PUEDE POR NO SER HIJA
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
        //void AgregaProductoDetalle(int ProductoId, int IngresoId, int EmpresaId, string Serie, decimal PrecioCosto, decimal PrecioVenta, decimal PrecioVenta2, decimal PrecioEspecial)
        //{
        //    EntProducto producto = new EntProducto()
        //    {
        //        Id = ProductoId,
        //        IngresoId = IngresoId,
        //        EmpresaId = EmpresaId,
        //        Serie = Serie,
        //        PrecioCosto = PrecioCosto,
        //        PrecioVenta = PrecioVenta,
        //        PrecioVenta2 = PrecioVenta2,
        //        PrecioEspecial = PrecioEspecial,
        //        Fecha = DateTime.Now
        //    };
        //    producto.Id = new BusProductos().AgregaProductoDetalle(producto);
        //}
        private void btnMueveAIngreso_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntProducto> productosSeleccionados = ObtieneListaProductosFromGV(gvProductosDetalle);
                SeleccionaEmpresa vSeleccionaEmp = new SeleccionaEmpresa();
                if (vSeleccionaEmp.ShowDialog() == DialogResult.OK)
                {
                    //vSeleccionaEmp.EmpresaSeleccionada;
                    foreach (EntProducto p in productosSeleccionados)
                    {
                        if (p.Estatus)
                        {
                            //ListaProductosDetalle.Remove(p);
                            p.EmpresaId = vSeleccionaEmp.EmpresaSeleccionada.Id;
                            new BusProductos().ActualizaProductoDetalle(p);
                            new BusProductos().ActualizaProductoDetallePedido(p);
                        }
                    }

                    this.Close();
                    //CargaProductosEnPantallas(); //NO PUEDE POR NO SER HIJA
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
