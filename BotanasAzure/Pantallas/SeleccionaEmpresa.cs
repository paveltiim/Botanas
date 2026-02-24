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
    public partial class SeleccionaEmpresa : Form
    {
        /// <summary>
        /// Ejecuta CargaEmpresas en constructor.
        /// </summary>
        public SeleccionaEmpresa()
        {
            InitializeComponent();
            CargaEmpresas();
        }
        /// <summary>
        /// Ejecuta CargaEmpresas en constructor.
        /// </summary>
        public SeleccionaEmpresa(bool Consignacion)
        {
            InitializeComponent();
            this.Consignacion = Consignacion;
            this.Text = "Selección Consignación de Empresa";
            CargaEmpresas();
        }
        public SeleccionaEmpresa(List<EntEmpresa> ListaEmpresas)
        {
            InitializeComponent();
            this.ListaEmpresas = ListaEmpresas;
            gvEmpresas.DataSource = ListaEmpresas;
        }
        public bool Consignacion { get; set; }

        public List<EntEmpresa> ListaEmpresas { get; set; }

        public EntEmpresa ObtieneEmpresaFromGV(DataGridView GridViewEmpresas)
        {
            if (GridViewEmpresas.CurrentRow == null)
                return null;
            return (EntEmpresa)((List<EntEmpresa>)GridViewEmpresas.DataSource)[GridViewEmpresas.CurrentRow.Index];
        }

        public EntEmpresa EmpresaSeleccionada { get { return ObtieneEmpresaFromGV(gvEmpresas); } }

        public void CargaEmpresas()
        {
            if (Program.UsuarioSeleccionado.Id > 1)
                ListaEmpresas = new BusEmpresas().ObtieneEmpresas().Where(P => P.UsuarioId == Program.UsuarioSeleccionado.Id).ToList();
            else
                ListaEmpresas = new BusEmpresas().ObtieneEmpresas();
            gvEmpresas.DataSource = ListaEmpresas;
        }


        private void SeleccionaEmpresa_Load(object sender, EventArgs e)
        {

        }
        
        private void gvEmpresas_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void gvEmpresas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            btnAgregar.PerformClick();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }
    }
}
