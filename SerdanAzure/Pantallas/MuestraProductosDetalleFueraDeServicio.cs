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
    public partial class MuestraProductosDetalleFueraDeServicio : FormBase
    {
        List<AiresEntidades.EntProducto> ListaProductosDetalle;
        public MuestraProductosDetalleFueraDeServicio(List<AiresEntidades.EntProducto> ProductosDetalle)
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
            try {
                pnlBotones.Enabled = Program.UsuarioSeleccionado.Administrador;

                if (Program.UsuarioSeleccionado.Id == 0)//MARTIN
                    pnlBotones.Enabled = true; 

                //if (Program.UsuarioSeleccionado.Id > 1)
                //    gvProductosDetalle.Columns[4].Visible = false;
            } catch(Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtFiltroSerie_TextChanged(object sender, EventArgs e)
        {
            //List<EntProducto> lst= (List<EntProducto>)gvGastosEmpresa.DataSource;
            gvProductosDetalle.DataSource = ListaProductosDetalle.Where(P => P.Serie.ToUpper().Contains(txtFiltroSerie.Text.ToUpper())).ToList();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            try {
                if(MuestraMensajeYesNo("¿Desea guardar los cambios realizados?") == DialogResult.Yes)
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
                ((Productos)forma).CargaProductos(Program.EmpresaSeleccionada.Id);
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MuestraMensajeYesNo("¿Desea eliminar el(los) Producto(s) seleccionado(s)?") == DialogResult.Yes)
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
        int AgregaIngreso(EntEmpresa EmpresaNueva, string EmpresaAnterior) {
            int proveedorId;

            //VERIFICA SI YA SE AGREGO LA EMPRESANUEVA COMO PROVEEDOR. LO HACE POR NOMBRE, NO SE PUEDE BUSCAR POR ID DE PROVEEDOR(NO SE SABE).
            List<EntProveedor> provedores = new BusProveedores().ObtieneProveedores(EmpresaNueva.Id).Where(P => P.Nombre == EmpresaAnterior).ToList();
            if (provedores.Count > 0)
                proveedorId = provedores[0].Id;
            else
            {
                //Proveedores vProv = new Proveedores();
                proveedorId = new Pantallas.Proveedores().AgregaProveedor(EmpresaNueva.Id, EmpresaAnterior, EmpresaAnterior, "");
            }

            //Productos vProd = new Pantallas.Productos();

            return new Pantallas.Productos().AgregaIngresoProducto(proveedorId, "TRASPASO DE "+ EmpresaAnterior, DateTime.Today);
            //EntCatalogoGenerico ingreso = new EntCatalogoGenerico();
            //ingreso.EmpresaId = proveedorId;
            //ingreso.Descripcion = p.Ingreso;
            //ingreso.Fecha = DateTime.Today;

            ////if (ingreso.EmpresaId != p.EmpresaId)
            //p.IngresoId = new BusProductos().AgregaIngreso(ingreso);
            ////else
            ////    new BusProductos().ActualizaIngreso(ingreso);
        }
        private void btnMueveAIngreso_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntProducto> productosSeleccionados = ObtieneListaProductosFromGV(gvProductosDetalle).Where(P => P.Estatus).ToList();
                if (productosSeleccionados.Count == 0)
                    MandaExcepcion("SELECCIONE AL MENOS UN PRODUCTO");

                SeleccionaEmpresa vSeleccionaEmp = new SeleccionaEmpresa();
                if (vSeleccionaEmp.ShowDialog() == DialogResult.OK)
                {
                    EntEmpresa empresaNuevaSeleccionada = vSeleccionaEmp.EmpresaSeleccionada;
                    if (empresaNuevaSeleccionada.Id != Program.EmpresaSeleccionada.Id)
                    {
                        int ingresoId = AgregaIngreso(empresaNuevaSeleccionada, Program.EmpresaSeleccionada.Nombre);

                        foreach (EntProducto p in productosSeleccionados)
                        {
                            EntEmpresa empresaSeleccionada = vSeleccionaEmp.EmpresaSeleccionada;
                            p.EmpresaId = empresaSeleccionada.Id;
                            p.IngresoId = ingresoId;

                            //SOLO PARA ACTUALIZAR EMPRESAID e INGRESOID
                            new BusProductos().ActualizaProductoDetalle(p);

                            if (p.EstatusId == 5)//ESTATUSID: 5=CONSIGNACION.
                            {
                                p.EstatusId = 1;//ESTATUSID: 1=ACTIVO.
                                new BusProductos().ActualizaEstatusProductoDetalle(p);
                            }
                        }

                        MuestraMensaje(string.Format("PRODUCTOS TRASPASADOS A -{0}-", empresaNuevaSeleccionada.Nombre), "CONFIRMACIÓN");

                        this.Close();
                    }
                    else //PASA DE CONSIGNACION A NORMAL
                    {
                        foreach (EntProducto p in productosSeleccionados.Where(P=>P.EstatusId==5).ToList())//ESTATUSID: 5=CONSIGNACION.
                        {
                            p.EstatusId = 1;//ESTATUSID: 1=ACTIVO.
                            new BusProductos().ActualizaEstatusProductoDetalle(p);
                        }
                        this.Close();
                    }
                    //CargaProductosEnPantallas(); //NO PUEDE POR NO SER HIJA (MdiChildren)
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

      
        private void btnMueveAConsignacion_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntProducto> productosSeleccionados = ObtieneListaProductosFromGV(gvProductosDetalle).Where(P => P.Estatus).ToList();
                if (productosSeleccionados.Count == 0)
                    MandaExcepcion("SELECCIONE AL MENOS UN PRODUCTO");

                SeleccionaEmpresa vSeleccionaEmp = new SeleccionaEmpresa(true);
                if (vSeleccionaEmp.ShowDialog() == DialogResult.OK)
                {
                    EntEmpresa empresaNuevaSeleccionada = vSeleccionaEmp.EmpresaSeleccionada;
                    int ingresoId=0;

                    if (empresaNuevaSeleccionada.Id!=Program.EmpresaSeleccionada.Id)
                        ingresoId = AgregaIngreso(empresaNuevaSeleccionada, Program.EmpresaSeleccionada.Nombre);

                    foreach (EntProducto p in productosSeleccionados)
                    {
                        if (ingresoId>0)
                        {
                            new BusProductos().AgregaMovimiento(p, ingresoId, 3, DateTime.Today);//3 - TRASPASO
                            p.IngresoId = ingresoId;
                        }
                        p.EmpresaId = empresaNuevaSeleccionada.Id;
                        //SOLO PARA ACTUALIZAR EMPRESAID e INGRESOID
                        new BusProductos().ActualizaProductoDetalle(p);

                        p.EstatusId = 5;//ESTATUSID: 5=CONSIGNACION.
                        new BusProductos().ActualizaEstatusProductoDetalle(p);
                    }


                    MuestraMensaje(string.Format("PRODUCTOS EN CONSIGNACIÓN DE -{0}-",empresaNuevaSeleccionada.Nombre),"CONFIRMACIÓN");
                    this.Close();
                    //CargaProductosEnPantallas(); //NO PUEDE POR NO SER HIJA (MdiChildren)
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

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntProducto> productosSeleccionados = ObtieneListaProductosFromGV(gvProductosDetalle).Where(P => P.Estatus).ToList();
                if (productosSeleccionados.Count == 0)
                    MandaExcepcion("SELECCIONE AL MENOS UN PRODUCTO");
                Inventarios vInvent = new Inventarios();
                vInvent.ExportaProductos(productosSeleccionados, Program.EmpresaSeleccionada.Id);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductosDetalle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
                {
                    ContraseñaGeneral vContra = new Pantallas.ContraseñaGeneral(true);
                    if (vContra.ShowDialog() == DialogResult.OK)
                    {
                        gvProductosDetalle.Columns[5].ReadOnly = false;
                    }
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnFueraDeServicio_Click(object sender, EventArgs e)
        {
            try
            {
                if (MuestraMensajeYesNo("¿Desea poner 'EN SERVICIO' el(los) Producto(s) seleccionado(s)?") == DialogResult.Yes)
                {
                    List<EntProducto> productosSeleccionados = ObtieneListaProductosFromGV(gvProductosDetalle);
                    string observacion = "";
                    //AgregaObservacion vObservacion = new AgregaObservacion();
                    //if (vObservacion.ShowDialog() == DialogResult.OK)
                    //    observacion = vObservacion.Observacion;
                    foreach (EntProducto p in productosSeleccionados)
                    {
                        if (p.Estatus)
                        {
                            p.EstatusId = 1;
                            new BusProductos().ActualizaEstatusProductoDetalle(p,observacion);
                            int productoId = p.ProductoId;
                            //new BusProductos().AumentaProducto(productoId, -1);
                        }
                    }
                    this.Close();
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductosDetalle_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == gvProductosDetalle.Columns.Count - 1)
                {
                    EntProducto productoSeleccionado = ObtieneProductoFromGV(gvProductosDetalle);
                    new BusProductos().ActualizaEstatusProductoDetalle(productoSeleccionado, productoSeleccionado.Tipo);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
    }
}
