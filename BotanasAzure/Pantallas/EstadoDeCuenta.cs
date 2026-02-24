using AiresEntidades;
using AiresNegocio;
using AiresUtilerias;
using Microsoft.Reporting.WinForms;
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
    public partial class EstadoDeCuenta : FormBase
    {
        public EstadoDeCuenta(EntCliente Cliente, int EstablecimientoId)
        {
            InitializeComponent();

            this.Cliente = Cliente;
            this.EstablecimientoId = EstablecimientoId;
            Program.EmpresaSeleccionada = new BusEmpresas().ObtieneEmpresa(Cliente.EmpresaId);
        }

        int EstablecimientoId { get; set; }
        EntCliente Cliente;
        List<EntPedido> ListaPedidos;

        public void CargaGvPedidosClientesCredito(int ClienteId)
        {
            EntCliente cliente = new BusClientes().ObtieneCliente(ClienteId);
            this.ListaPedidos = new BusPedidos().ObtienePedidosClientesDeuda(this.EstablecimientoId, ClienteId);
            EntPedidoBindingSource.DataSource = this.ListaPedidos;
            ReportParameter parm = new ReportParameter("Empresa", Program.EmpresaSeleccionada.Nombre);
            ReportParameter parm1 = new ReportParameter("DatosDireccion", Program.EmpresaSeleccionada.Direccion);
            ReportParameter parm2 = new ReportParameter("Cliente", cliente.NombreFiscal);
            ReportParameter parm3 = new ReportParameter("Fecha", DateTime.Today.ToShortDateString());
            rvPedidosDeudaPorCliente.LocalReport.SetParameters(parm);
            rvPedidosDeudaPorCliente.LocalReport.SetParameters(parm1);
            rvPedidosDeudaPorCliente.LocalReport.SetParameters(parm2);
            rvPedidosDeudaPorCliente.LocalReport.SetParameters(parm3);

            rvPedidosDeudaPorCliente.RefreshReport();
        }


        private void EstadoDeCuentaFactura_Load(object sender, EventArgs e)
        {
            try
            {
                CargaGvPedidosClientesCredito(this.Cliente.Id);
                txtEmail.Text = Cliente.Email;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void rvPedidosDeudaPorCliente_Load(object sender, EventArgs e)
        {

        }
        void EnviaCorreo(EntCliente Cliente, string PathArchivo)
        {
            base.SetWaitCursor();

            List<string> archivosAdjuntos = new List<string>();

            System.IO.FileInfo arc = new System.IO.FileInfo(PathArchivo);
            if (!arc.Exists)
                throw new Exception("La ruta del archivo seleccionado a cambiado");

            archivosAdjuntos.Add(PathArchivo);
            string asunto = "ESTADO DE CUENTA - "+ Program.EmpresaSeleccionada.NombreFiscal;

            new UtiCorreo().EnviaCorreo("" + asunto, new List<string>() { Cliente.Email }, "Envio Estado de Cuenta", archivosAdjuntos);

            MuestraMensaje("El Correo se ha Enviado correctamente, a la dirección -" + Cliente.Email + "-");
            //}
        }

        private void btnEnviaCorreo_Click(object sender, EventArgs e)
        {
            try
            {
                if (MuestraMensajeYesNo(string.Format("¿Seguro desea enviar el Estado de Cuenta al correo seleccionado? \n Cliente:{0} \n Email:{1}", Cliente.Nombre, txtEmail.Text), "CONFIRMACIÓN") == DialogResult.Yes)
                {
                    base.SetWaitCursor();
                    Cliente.Email = txtEmail.Text;
                    try
                    {
                        //throw new Exception("error");
                        //pahtArchivosFactura = PathClienteDirectorioFacturas;
                        //pahtArchivosFactura = @"C:\TIIM\Facturacion\Facturas\RAFAEL GIL ARMENTA\20170105122126";
                        EnviaCorreo(Cliente, txtAdjuntaArchivo.Text);
                    }
                    catch (Exception ex)
                    {
                        MuestraExcepcion(ex, "Correo NO enviado.");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { base.SetDefaultCursor(); }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdjuntaArchivo_Click(object sender, EventArgs e)
        {
            if (ofdBuscaArchivo.ShowDialog() == DialogResult.OK)
            {
                txtAdjuntaArchivo.Text = ofdBuscaArchivo.FileName;

                MuestraArchivo(ofdBuscaArchivo.FileName,true);
            }
        }
    }
}
