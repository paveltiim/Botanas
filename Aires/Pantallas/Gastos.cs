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
    public partial class Gastos : FormBase
    {
        public Gastos()
        {
            InitializeComponent();
        }
        EntEmpresa EmpresaSeleccionada { get { return ObtieneEmpresaFromCmb(cmbEmpresas); } }
        List<EntEmpresa> ListaEmpresas = new List<EntEmpresa>();

        #region Metodos
            void CargaEmpresas()
            {
                ListaEmpresas = new BusEmpresas().ObtieneEmpresas();
                cmbEmpresas.DataSource = ListaEmpresas;
            }
        #endregion

        private void Gastos_Load(object sender, EventArgs e)
        {
            try {
                CargaEmpresas();
            }
            catch (Exception ex) { MuestraExcepcion(ex); }
        }
    }
}
