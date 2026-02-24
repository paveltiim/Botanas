using AiresEntidades;
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
    public partial class SeleccionaProducto : FormBase
    {
        List<EntProducto> ListaProductos;
        public EntProducto ProductoSeleccionado { get { return ObtieneProductoFromGV(gvProductosDetalle); } }

        /// <summary>
        /// SELECCIONA UN SOLO PRODUCTO
        /// </summary>
        /// <param name="Productos"></param>
        public SeleccionaProducto(List<EntProducto> Productos)
        {
            InitializeComponent();

            this.ListaProductos = Productos;
            gvProductosDetalle.DataSource = Productos;
            lbContadorSeries.Text = Productos.Count.ToString();
            //if (Program.Empresa == 2)// 2:OBREGON
            //{
            //    gvProductosDetalle.Columns["dataGridViewTextBoxColumn5"].Visible = false;
            //}
        }



        private void SeleccionaFactura_Load(object sender, EventArgs e)
        {

        }

        private void txtCodigoProducto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<EntProducto> lst = ListaProductos.Where(P => P.Codigo.ToUpper().Contains(txtCodigoProducto.Text.ToUpper())).ToList();
                gvProductosDetalle.DataSource = lst;
                lbContadorSeries.Text = lst.Count.ToString();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtDescripcionProducto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //List<EntProducto> lst= (List<EntProducto>)gvGastosEmpresa.DataSource;
                List<EntProducto> lst = ListaProductos.Where(P => P.Descripcion.ToUpper().Contains(txtDescripcionProducto.Text.ToUpper())).ToList();
                gvProductosDetalle.DataSource = lst;
                lbContadorSeries.Text = lst.Count.ToString();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void txtCodigoProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((Keys)e.KeyChar == Keys.Enter)
                    btnAgregar.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductosDetalle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnAgregar.PerformClick();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void gvProductosDetalle_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridView gvPedidos = gvProductosDetalle;
                if (e.ColumnIndex == 0)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                    {
                        gvPedidos.DataSource = ((List<EntProducto>)((DataGridView)sender).DataSource).OrderBy(P => P.Codigo).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        gvPedidos.DataSource = ((List<EntProducto>)((DataGridView)sender).DataSource).OrderByDescending(P => P.Codigo).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None || ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                    {
                        gvPedidos.DataSource = ((List<EntProducto>)((DataGridView)sender).DataSource).OrderBy(P => P.Descripcion).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        gvPedidos.DataSource = ((List<EntProducto>)((DataGridView)sender).DataSource).OrderByDescending(P => P.Descripcion).ToList();
                        ((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

    }
}
