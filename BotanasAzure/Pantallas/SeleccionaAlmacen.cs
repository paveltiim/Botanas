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
    public partial class SeleccionaAlmacen : FormBase
    {
        public int AlmacenId { get; set; }
        
        public SeleccionaAlmacen()
        {
            InitializeComponent();
            this.AlmacenId = 0;
        }
        public SeleccionaAlmacen(int AlmacenId)
        {
            InitializeComponent();
            this.AlmacenId = AlmacenId;
        }
        public EntCatalogoGenerico AlmacenSeleccionado { get { return ObtieneCatalogoGenericoFromCmb(cmbAlmacenes); } }

        void CargaAlmacenes()
        {
            List<EntCatalogoGenerico> almacenes = new BusEmpresas().ObtieneAlmacenes(Program.UsuarioSeleccionado.CompañiaId, Program.UsuarioSeleccionado.Id);
            //if (Program.UsuarioSeleccionado.Administrador)
            //    almacenes.Insert(0, new EntCatalogoGenerico() { Id = 0, Descripcion = "-TODAS-" });
            cmbAlmacenes.DataSource = almacenes;
            if(this.AlmacenId>0)
                cmbAlmacenes.SelectedIndex = ((List<EntCatalogoGenerico>)cmbAlmacenes.DataSource).FindIndex(P => P.Id == this.AlmacenId);//Cliente.TipoPersonaId - 1;
            else
                cmbAlmacenes.SelectedIndex = 0;
        }


        private void SeleccionaEmpresa_Load(object sender, EventArgs e)
        {
            try
            {
                CargaAlmacenes();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        
        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
