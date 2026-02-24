using AiresEntidades;
using AiresNegocio;
using AiresUtilerias;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
//using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static iTextSharp.text.pdf.parser.LocationTextExtractionStrategy;

namespace Aires.Pantallas
{
    public partial class EntradasTraspasos : FormBase
    {
        public void VerificaEmpresa()
        {
            cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
        }

        public EntradasTraspasos()
        {
            InitializeComponent();
        }

        EntCliente ClienteSeleccionado = new EntCliente();
        EntProducto ProductoSeleccionado;
        List<EntProducto> ListaProductos = new List<EntProducto>();
        List<EntCliente> ListaClientes = new List<EntCliente>();

        int almacenId { get { return cmbAlmacenes.SelectedIndex + 1; } }

        #region Metodos

        public void CargaEmpresas()
        {
            Program.CambiaEmpresa = false;
        if (Program.UsuarioSeleccionado.Id > 1)
            cmbEmpresas.DataSource = new BusEmpresas().ObtieneEmpresas().Where(P => P.UsuarioId == Program.UsuarioSeleccionado.Id).ToList();
        else
            cmbEmpresas.DataSource = new BusEmpresas().ObtieneEmpresas();
        //cmbEmpresas.DataSource = new BusEmpresas().ObtieneEmpresas();
            Program.CambiaEmpresa = true;
        }

        void CargaAlmacenes()
        {
            List<EntCatalogoGenerico> almacenes = new BusEmpresas().ObtieneAlmacenes(Program.UsuarioSeleccionado.CompañiaId, Program.UsuarioSeleccionado.Id);
            cmbAlmacenes.DataSource = almacenes;
            cmbAlmacenes.SelectedIndex = 0;
        }

        /// <summary>
        /// Muestra el resultado de ListaProductos.Sum(P=>P.Precio) en TxtMuestraTotal.Text.
        /// </summary>
        /// <param name="CantidadSumar"></param>
        void CalculaSumaTotal(List<EntProducto> ListaProductos, TextBox TxtMuestraTotal)
        {
            decimal total, subtotal, cantidadIva, descuento;
            decimal cantidadIVARetenido = 0;

            if (this.ListaProductos == null)
            {
                total = 0;
                subtotal = 0;
                cantidadIva = 0;
            }
            else
            {
                total = ListaProductos.Sum(P => P.PrecioC);
                //subtotal = Math.Round(total, 2) / (1 + IVA); //Math.Round(total / (1 + IVA), 2);
                //cantidadIva = subtotal * this.IVA;
                //cantidadIva = Math.Round(total, 2) - subtotal;
            }
         
            TxtMuestraTotal.Text = FormatoMoney(total);
        }

        #endregion

        void VerificaProductosSeleccionados(List<EntProducto> ProductosSeleccionados)
        {
            if (ProductosSeleccionados == null || ProductosSeleccionados.Count == 0)
                throw new Exception("Agregue al menos un producto.");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EstadoId">1: EN PROCESO; 2: RECIBIDA</param>
        /// <param name="FechaDesde"></param>
        /// <param name="FechaHasta"></param>
        /// <param name="AlmacenId"></param>
        public void CargaEntradasTraspasos(int EstadoId, DateTime FechaDesde, DateTime FechaHasta, int AlmacenId)
        {
            int orientacion = 1;//RECIBE(ENTRADA)
            int estado = EstadoId;
            if (estado == 1)
            {
                gvDetalleProductosTraspaso.DataSource = null;
                gvTraspasosEntradas.DataSource = new BusProductos().ObtieneMovimientosTraspasosProductos(Program.EmpresaSeleccionada.Id, orientacion, FechaDesde, FechaHasta, estado, AlmacenId);
            }
            else if (estado == 2) {
                gvDetalleProductosTraspasoRecibidos.DataSource = null;
                gvTraspasosRecibidos.DataSource = new BusProductos().ObtieneMovimientosTraspasosProductos(Program.EmpresaSeleccionada.Id, orientacion, FechaDesde, FechaHasta, estado, AlmacenId);
            }
        }
        //public void CargaProductosMovimientoEntradaTraspaso(int TraspasoId)
        //{
        //    List<EntProducto> listaProductos = new BusProductos().ObtieneMovimientosDetalleTraspasoProductos(TraspasoId);
        //    entProductoBindingSource.DataSource = listaProductos;

        //    gvDetalleProductosTraspaso.DataSource = listaProductos;

        //    decimal cantidad = listaProductos.Sum(P => P.Cantidad);
        //    txtCantidadTotalEntradas.Text = cantidad.ToString();
        //    txtPrecioCostoTotalEntradas.Text = FormatoMoney(listaProductos.Sum(P => P.PrecioC));
        //    rvEntradas.RefreshReport();
        //}
        public void CargaProductosMovimientoEntradaTraspaso(int MovimientoId)
        {
            List<EntProducto> listaProductos = new BusProductos().ObtieneMovimientosDetalleProductos(1, MovimientoId);
            entProductoBindingSource.DataSource = listaProductos;

            gvDetalleProductosTraspaso.DataSource = listaProductos;

            decimal cantidad = listaProductos.Sum(P => P.Cantidad);
            txtCantidadTotalEntradas.Text = cantidad.ToString();
            txtPrecioCostoTotalEntradas.Text = FormatoMoney(listaProductos.Sum(P => P.PrecioC));
            rvEntradas.RefreshReport();
        }
        public void CargaProductosMovimientoEntrada(int MovimientoId)
        {
            List<EntProducto> listaProductos = new BusProductos().ObtieneMovimientosDetalleProductos(1, MovimientoId);
            //entProductoBindingSource.DataSource = listaProductos;
            gvDetalleProductosTraspasoRecibidos.DataSource = listaProductos;//.OrderByDescending(P => P.Fecha).ToList();

            decimal cantidad = listaProductos.Sum(P => P.Cantidad);
            txtCantidadTotalTraspasoRecibido.Text = cantidad.ToString();
            txtPrecioCostoTotalTraspasoRecibido.Text = FormatoMoney(listaProductos.Sum(P => P.PrecioC));
            //rvEntradas.RefreshReport();
        }
        void InicializaPantalla()
        {
            if (Program.EmpresaSeleccionada != null && cmbEmpresas.Items.Count > 0)
                cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);

            gvTraspasosEntradas.DataSource = new List<EntCatalogoGenerico>();
            cmbMesesEntradas.SelectedIndex = DateTime.Today.Month - 1;
            CargaAñosCmb(cmbAñoEntradas);

            if (Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.MASTER
             || Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.ADMINISTRADORALMACEN
             || Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.PUNTOVENTA
             || Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.PUNTOVENTAMENUDEO
             || Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.ADMINISTRADORPUNTOVENTA
             || Program.UsuarioSeleccionado.TipoUsuarioId == (int)TipoUsuario.ADMINISTRADORINSUMOS)
            {
                btnAceptaRecepcion.Visible = true;
                btnRechazar.Visible = true;
            }
        }


        private void Ventas_Load(object sender, EventArgs e)
        {
            try
            {
                InicializaPantalla();
                //CargaEmpresas();

                //if (Program.EmpresaSeleccionada == null)
                //    Program.EmpresaSeleccionada = SeleccionaEmpresa();
                //if (Program.EmpresaSeleccionada != null)
                //{
                //cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == Program.EmpresaSeleccionada.Id);
                //CargaProductos(Program.EmpresaSeleccionada.Id);

                ClienteSeleccionado = null;

                CargaAlmacenes();
                //}
            }
            catch (Exception ex)
            {
                MuestraExcepcion(ex);
            }
        }
        
        private void cmbEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Program.CambiaEmpresa)
                {
                    Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }


        private void btnRefrescaEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                Program.EmpresaSeleccionada = ObtieneEmpresaFromCmb(cmbEmpresas);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void btnBuscaEmpresa_Click(object sender, EventArgs e)
        {
            try
            {
                SeleccionaEmpresa vSeleccionaEmp = new Pantallas.SeleccionaEmpresa();
                if (vSeleccionaEmp.ShowDialog() == DialogResult.OK)
                {
                    cmbEmpresas.SelectedIndex = ((List<EntEmpresa>)cmbEmpresas.DataSource).FindIndex(P => P.Id == vSeleccionaEmp.EmpresaSeleccionada.Id);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }

        private void cmbAlmacenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                
                EntCatalogoGenerico almacen = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);
                if (cmbMesesEntradas.SelectedIndex >= 0)
                    CargaEntradasTraspasos(tcTraspasos.SelectedIndex+1, FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                                    FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1),
                                    almacen.Id);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void cmbMesesEntradas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                if (cmbMesesEntradas.Focused)
                {
                    EntCatalogoGenerico almacen = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);
                    if (cmbMesesEntradas.SelectedIndex >= 0)
                        CargaEntradasTraspasos(tcTraspasos.SelectedIndex+1, FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                                        FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1),
                                        almacen.Id);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnRefrescarEntradas_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                EntCatalogoGenerico almacen = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);
                gvDetalleProductosTraspaso.DataSource = null;
                gvDetalleProductosTraspaso.Refresh();
                if (rdoEntradasPorMes.Checked)
                {
                    if (cmbMesesEntradas.SelectedIndex >= 0)
                        CargaEntradasTraspasos(1,FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                                        FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1),
                                        almacen.Id);
                }else if (rdoEntradasTodas.Checked)
                {
                    CargaEntradasTraspasos(1,base.ObtieneFechaInicial(),DateTime.Now,almacen.Id);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void gvIngresos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                EntCatalogoGenerico entrada = ObtieneCatalogoGenericoFromGV(gvTraspasosEntradas);
                CargaProductosMovimientoEntradaTraspaso(entrada.Id);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnAceptaRecepcion_Click(object sender, EventArgs e)
        {
            try
            {
                string mensaje;

                EntEmpresa empresaSeleccionada = Program.EmpresaSeleccionada;
                empresaSeleccionada.RFC = Program.EmpresaSeleccionada.RFC;

                EntCatalogoGenerico traspasoSeleccionado = ObtieneCatalogoGenericoFromGV(gvTraspasosEntradas);
                CargaProductosMovimientoEntradaTraspaso(traspasoSeleccionado.Id);

                mensaje = "¿Desea Aceptar el Traspaso de Productos Seleccionado? \n" +
                    "\n"+traspasoSeleccionado.Descripcion;

                if (MuestraMensajeYesNo(mensaje, "CONFIRMAR") == DialogResult.Yes)
                {
                    base.SetWaitCursor();
                    string detallePedido = "";
                    
                    List<EntProducto> productosSeleccionados = ObtieneListaProductosFromGV(gvDetalleProductosTraspaso);

                    foreach (EntProducto p in productosSeleccionados)
                        detallePedido += p.Cantidad + " " + p.Descripcion + " | ";

                    int orientacion = (int)TipoOrientacion.ENTRADA;
                    int almacenRecibeId = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes).Id;
                    int movimientoId = new BusPedidos().AgregaMovimientoMaster("RECEPCIÓN TRASPASO",
                        (int)TipoMovimiento.TRASPASO, orientacion, almacenRecibeId, 0, Program.UsuarioSeleccionado.Id);

                    Productos vProd = new Productos();
                    foreach (EntProducto p in productosSeleccionados)
                    {
                        new BusPedidos().AgregaMovimientoDetalle(movimientoId, p.ProductoId, p.Cantidad, p.PrecioC);
                        vProd.AumentaProducto(p.ProductoId, p.Cantidad);
                    }

                    new BusPedidos().AgregaMovimientoLote(movimientoId, orientacion);
                    
                    new BusPedidos().AgregaMovimientoTraspaso(traspasoSeleccionado.IdSecundario, movimientoId, traspasoSeleccionado.ClaveId, almacenRecibeId, "", 2, Program.UsuarioSeleccionado.Id);

                    btnRefrescarTraspasosEnProceso.PerformClick();
                    MuestraMensaje("¡Entrada Traspaso Registrada!");
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            try
            {
                string mensaje;

                EntEmpresa empresaSeleccionada = Program.EmpresaSeleccionada;
                empresaSeleccionada.RFC = Program.EmpresaSeleccionada.RFC;

                EntCatalogoGenerico traspasoSeleccionado = ObtieneCatalogoGenericoFromGV(gvTraspasosEntradas);
                CargaProductosMovimientoEntradaTraspaso(traspasoSeleccionado.Id);

                mensaje = "¿Desea -Rechazar- el Traspaso de Productos Seleccionado? \n" +
                    "\n" + traspasoSeleccionado.Descripcion;

                if (MuestraMensajeYesNo(mensaje, "CONFIRMAR") == DialogResult.Yes)
                {
                    base.SetWaitCursor();
                    string detallePedido = "";

                    AgregaObservacion vObser = new AgregaObservacion();
                    if (vObser.ShowDialog() == DialogResult.OK)
                    {
                        //List<EntProducto> productosSeleccionados = ObtieneListaProductosFromGV(gvDetalleProductosTraspaso);

                        //foreach (EntProducto p in productosSeleccionados)
                        //    detallePedido += p.Cantidad + " " + p.Descripcion + " | ";

                        int orientacion = (int)TipoOrientacion.ENTRADA;
                        int almacenRecibeId = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes).Id;
                        int movimientoId = 0;

                        //Productos vProd = new Productos();
                        //foreach (EntProducto p in productosSeleccionados)
                        //{
                        //    new BusPedidos().AgregaMovimientoDetalle(movimientoId, p.ProductoId, p.Cantidad, p.PrecioC);
                        //    vProd.AumentaProducto(p.ProductoId, p.Cantidad);
                        //}

                        //new BusPedidos().AgregaMovimientoLote(movimientoId, orientacion);

                        new BusPedidos().AgregaMovimientoTraspaso(traspasoSeleccionado.IdSecundario, movimientoId,
                                                                    traspasoSeleccionado.ClaveId,
                                                                    almacenRecibeId, vObser.Observacion, 3, Program.UsuarioSeleccionado.Id);

                        btnRefrescarTraspasosEnProceso.PerformClick();
                        MuestraMensaje("¡Entrada Traspaso Rechazada!");
                    }
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void rdoEntradasPorMes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                btnRefrescarTraspasosEnProceso.PerformClick();
                pnlEntradasPorMes.Enabled = rdoEntradasPorMes.Checked;
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void gvTraspasosEntradas_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                EntCatalogoGenerico entrada = ObtieneCatalogoGenericoFromGV(gvTraspasosEntradas);
                CargaProductosMovimientoEntradaTraspaso(entrada.Id);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void btnRefrescarTraspasosRecibidos_Click(object sender, EventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                EntCatalogoGenerico almacen = ObtieneCatalogoGenericoFromCmb(cmbAlmacenes);
                if (rdoEntradasPorMes.Checked)
                {
                    if (cmbMesesEntradas.SelectedIndex >= 0)
                        CargaEntradasTraspasos(2, FechaDesdeFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas),
                                        FechaHastaFromComboBoxs(cmbMesesEntradas, cmbAñoEntradas).AddDays(1),
                                        almacen.Id);
                }
                else if (rdoEntradasTodas.Checked)
                {
                    CargaEntradasTraspasos(2, base.ObtieneFechaInicial(), DateTime.Now, almacen.Id);
                }
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }

        private void gvTraspasosRecibidos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                base.SetWaitCursor();
                EntCatalogoGenerico entrada = ObtieneCatalogoGenericoFromGV((DataGridView)sender);
                CargaProductosMovimientoEntrada(entrada.Id);
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
            finally { base.SetDefaultCursor(); }
        }
    }
}
