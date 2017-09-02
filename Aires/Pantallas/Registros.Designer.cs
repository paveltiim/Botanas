namespace Aires.Pantallas
{
    partial class Registros
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Registros));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.entPedidoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlFacturacion = new System.Windows.Forms.Panel();
            this.chkFacturaPublicoGeneral = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRFC = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombreFiscal = new System.Windows.Forms.TextBox();
            this.cmbFormaPago = new System.Windows.Forms.ComboBox();
            this.cmbMetodoPago = new System.Windows.Forms.ComboBox();
            this.txtNumeroCuenta = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtCondicionesPago = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMetodoPago = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtFormaPago = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCP = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txtCalle = new System.Windows.Forms.TextBox();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtNoExterior = new System.Windows.Forms.TextBox();
            this.txtMunicipio = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtNoInterior = new System.Windows.Forms.TextBox();
            this.txtLocalidad = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtColonia = new System.Windows.Forms.TextBox();
            this.tcPedidosGrids = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCantidadVentas = new System.Windows.Forms.TextBox();
            this.chkVerFacturasEliminadas = new System.Windows.Forms.CheckBox();
            this.txtFacturaFiltro = new System.Windows.Forms.TextBox();
            this.txtDescripcionFiltro = new System.Windows.Forms.TextBox();
            this.txtNumClienteFiltro = new System.Windows.Forms.TextBox();
            this.btnFiltrarCliente = new System.Windows.Forms.Button();
            this.txtClienteFiltro = new System.Windows.Forms.TextBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.gvPedidos = new System.Windows.Forms.DataGridView();
            this.NumCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clienteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detalleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Facturado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FechaCorta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstatusDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnVerFactura = new System.Windows.Forms.Button();
            this.btnFacturar = new System.Windows.Forms.Button();
            this.btnEnviaCorreo = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rdoPorFechasVentas = new System.Windows.Forms.RadioButton();
            this.pnlVentasPorFechas = new System.Windows.Forms.Panel();
            this.dtpFechaHastaVentas = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaDesdeVentas = new System.Windows.Forms.DateTimePicker();
            this.btnRefrescarReporteVentas = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.rvVentas = new Microsoft.Reporting.WinForms.ReportViewer();
            this.rdoPorMesVentas = new System.Windows.Forms.RadioButton();
            this.rdoPorDiaVentas = new System.Windows.Forms.RadioButton();
            this.pnlVentasPorDia = new System.Windows.Forms.Panel();
            this.dtpFechaDiaVentas = new System.Windows.Forms.DateTimePicker();
            this.pnlVentasPorMes = new System.Windows.Forms.Panel();
            this.cmbMesesVentas = new System.Windows.Forms.ComboBox();
            this.cmbAñoVentas = new System.Windows.Forms.ComboBox();
            this.entCatalogoGenericoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label24 = new System.Windows.Forms.Label();
            this.cmbEmpresas = new System.Windows.Forms.ComboBox();
            this.btnBuscaEmpresa = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.entPedidoBindingSource)).BeginInit();
            this.pnlFacturacion.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tcPedidosGrids.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPedidos)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.pnlVentasPorFechas.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.pnlVentasPorDia.SuspendLayout();
            this.pnlVentasPorMes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entCatalogoGenericoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // entPedidoBindingSource
            // 
            this.entPedidoBindingSource.DataSource = typeof(AiresEntidades.EntPedido);
            // 
            // pnlFacturacion
            // 
            this.pnlFacturacion.Controls.Add(this.chkFacturaPublicoGeneral);
            this.pnlFacturacion.Controls.Add(this.groupBox2);
            this.pnlFacturacion.Controls.Add(this.groupBox1);
            this.pnlFacturacion.Location = new System.Drawing.Point(1211, 105);
            this.pnlFacturacion.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFacturacion.Name = "pnlFacturacion";
            this.pnlFacturacion.Size = new System.Drawing.Size(335, 411);
            this.pnlFacturacion.TabIndex = 111;
            this.pnlFacturacion.Visible = false;
            // 
            // chkFacturaPublicoGeneral
            // 
            this.chkFacturaPublicoGeneral.AutoSize = true;
            this.chkFacturaPublicoGeneral.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFacturaPublicoGeneral.Location = new System.Drawing.Point(3, 13);
            this.chkFacturaPublicoGeneral.Name = "chkFacturaPublicoGeneral";
            this.chkFacturaPublicoGeneral.Size = new System.Drawing.Size(165, 20);
            this.chkFacturaPublicoGeneral.TabIndex = 16;
            this.chkFacturaPublicoGeneral.Text = "Facturar a Público General";
            this.chkFacturaPublicoGeneral.UseVisualStyleBackColor = true;
            this.chkFacturaPublicoGeneral.CheckedChanged += new System.EventHandler(this.chkFacturaPublicoGeneral_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtRFC);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtNombreFiscal);
            this.groupBox2.Controls.Add(this.cmbFormaPago);
            this.groupBox2.Controls.Add(this.cmbMetodoPago);
            this.groupBox2.Controls.Add(this.txtNumeroCuenta);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.txtCondicionesPago);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtMetodoPago);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtFormaPago);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtEmail);
            this.groupBox2.Location = new System.Drawing.Point(1, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(328, 191);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos de Factura";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 110;
            this.label2.Text = "Nombre Fiscal:";
            // 
            // txtRFC
            // 
            this.txtRFC.Enabled = false;
            this.txtRFC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRFC.Location = new System.Drawing.Point(124, 166);
            this.txtRFC.Name = "txtRFC";
            this.txtRFC.Size = new System.Drawing.Size(195, 22);
            this.txtRFC.TabIndex = 109;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 108;
            this.label1.Text = "R.F.C.:";
            // 
            // txtNombreFiscal
            // 
            this.txtNombreFiscal.Enabled = false;
            this.txtNombreFiscal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreFiscal.Location = new System.Drawing.Point(124, 141);
            this.txtNombreFiscal.Name = "txtNombreFiscal";
            this.txtNombreFiscal.Size = new System.Drawing.Size(195, 22);
            this.txtNombreFiscal.TabIndex = 107;
            // 
            // cmbFormaPago
            // 
            this.cmbFormaPago.FormattingEnabled = true;
            this.cmbFormaPago.Items.AddRange(new object[] {
            "Pago en una sola exhibición",
            "Parcialidades"});
            this.cmbFormaPago.Location = new System.Drawing.Point(124, 20);
            this.cmbFormaPago.Name = "cmbFormaPago";
            this.cmbFormaPago.Size = new System.Drawing.Size(195, 21);
            this.cmbFormaPago.TabIndex = 106;
            this.cmbFormaPago.SelectedValueChanged += new System.EventHandler(this.cmbFormaPago_SelectedValueChanged);
            // 
            // cmbMetodoPago
            // 
            this.cmbMetodoPago.FormattingEnabled = true;
            this.cmbMetodoPago.Items.AddRange(new object[] {
            "01-Efectivo",
            "02-Cheque",
            "03-Transferencia",
            "04-Tarjetas de crédito",
            "05-Monederos electrónicos",
            "06-Dinero electrónico",
            "07-Tarjetas digitales",
            "08-Vales de despensa",
            "09-Bienes",
            "10-Servicio",
            "11-Por cuenta de tercero",
            "12-Dación en pago",
            "13-Pago por subrogación",
            "14-Pago por consignación",
            "15-Condonación",
            "16-Cancelación",
            "17-Compensación",
            "28-Tarjetas de Débito",
            "98-“NA”",
            "99-Otros"});
            this.cmbMetodoPago.Location = new System.Drawing.Point(124, 44);
            this.cmbMetodoPago.Name = "cmbMetodoPago";
            this.cmbMetodoPago.Size = new System.Drawing.Size(195, 21);
            this.cmbMetodoPago.TabIndex = 105;
            this.cmbMetodoPago.SelectedValueChanged += new System.EventHandler(this.cmbMetodoPago_SelectedValueChanged);
            // 
            // txtNumeroCuenta
            // 
            this.txtNumeroCuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroCuenta.Location = new System.Drawing.Point(124, 92);
            this.txtNumeroCuenta.Name = "txtNumeroCuenta";
            this.txtNumeroCuenta.Size = new System.Drawing.Size(195, 22);
            this.txtNumeroCuenta.TabIndex = 3;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(24, 95);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(93, 13);
            this.label20.TabIndex = 104;
            this.label20.Text = "Número de Cuenta:";
            // 
            // txtCondicionesPago
            // 
            this.txtCondicionesPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCondicionesPago.Location = new System.Drawing.Point(124, 68);
            this.txtCondicionesPago.Name = "txtCondicionesPago";
            this.txtCondicionesPago.Size = new System.Drawing.Size(195, 22);
            this.txtCondicionesPago.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 71);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 13);
            this.label12.TabIndex = 102;
            this.label12.Text = "Condiciones de Pago:";
            // 
            // txtMetodoPago
            // 
            this.txtMetodoPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMetodoPago.Location = new System.Drawing.Point(124, 44);
            this.txtMetodoPago.Name = "txtMetodoPago";
            this.txtMetodoPago.Size = new System.Drawing.Size(195, 22);
            this.txtMetodoPago.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(35, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 13);
            this.label11.TabIndex = 100;
            this.label11.Text = "Método de Pago:";
            // 
            // txtFormaPago
            // 
            this.txtFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFormaPago.Location = new System.Drawing.Point(124, 20);
            this.txtFormaPago.Name = "txtFormaPago";
            this.txtFormaPago.Size = new System.Drawing.Size(195, 22);
            this.txtFormaPago.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(42, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 98;
            this.label10.Text = "Forma de Pago:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(89, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 97;
            this.label9.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(124, 116);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(195, 22);
            this.txtEmail.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCP);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.txtCalle);
            this.groupBox1.Controls.Add(this.txtEstado);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txtNoExterior);
            this.groupBox1.Controls.Add(this.txtMunicipio);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtNoInterior);
            this.groupBox1.Controls.Add(this.txtLocalidad);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtColonia);
            this.groupBox1.Location = new System.Drawing.Point(2, 233);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 175);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dirección Fiscal";
            // 
            // txtCP
            // 
            this.txtCP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCP.Location = new System.Drawing.Point(250, 147);
            this.txtCP.Name = "txtCP";
            this.txtCP.Size = new System.Drawing.Size(77, 22);
            this.txtCP.TabIndex = 7;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(220, 152);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(21, 13);
            this.label18.TabIndex = 40;
            this.label18.Text = "CP:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(28, 150);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(38, 13);
            this.label21.TabIndex = 39;
            this.label21.Text = "Estado:";
            // 
            // txtCalle
            // 
            this.txtCalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCalle.Location = new System.Drawing.Point(82, 19);
            this.txtCalle.Name = "txtCalle";
            this.txtCalle.Size = new System.Drawing.Size(224, 22);
            this.txtCalle.TabIndex = 0;
            // 
            // txtEstado
            // 
            this.txtEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstado.Location = new System.Drawing.Point(82, 147);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.Size = new System.Drawing.Size(134, 22);
            this.txtEstado.TabIndex = 6;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(40, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 13);
            this.label16.TabIndex = 28;
            this.label16.Text = "Calle:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(14, 124);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 13);
            this.label19.TabIndex = 35;
            this.label19.Text = "Municipio:";
            // 
            // txtNoExterior
            // 
            this.txtNoExterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoExterior.Location = new System.Drawing.Point(82, 45);
            this.txtNoExterior.Name = "txtNoExterior";
            this.txtNoExterior.Size = new System.Drawing.Size(79, 22);
            this.txtNoExterior.TabIndex = 1;
            // 
            // txtMunicipio
            // 
            this.txtMunicipio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMunicipio.Location = new System.Drawing.Point(82, 121);
            this.txtMunicipio.Name = "txtMunicipio";
            this.txtMunicipio.Size = new System.Drawing.Size(167, 22);
            this.txtMunicipio.TabIndex = 5;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(2, 51);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(59, 13);
            this.label17.TabIndex = 27;
            this.label17.Text = "No. Exterior:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 99);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 13);
            this.label15.TabIndex = 33;
            this.label15.Text = "Localidad:";
            // 
            // txtNoInterior
            // 
            this.txtNoInterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoInterior.Location = new System.Drawing.Point(229, 46);
            this.txtNoInterior.Name = "txtNoInterior";
            this.txtNoInterior.Size = new System.Drawing.Size(77, 22);
            this.txtNoInterior.TabIndex = 2;
            // 
            // txtLocalidad
            // 
            this.txtLocalidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocalidad.Location = new System.Drawing.Point(82, 95);
            this.txtLocalidad.Name = "txtLocalidad";
            this.txtLocalidad.Size = new System.Drawing.Size(167, 22);
            this.txtLocalidad.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(167, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "No. Interior:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(25, 73);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(42, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "Colonia:";
            // 
            // txtColonia
            // 
            this.txtColonia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColonia.Location = new System.Drawing.Point(82, 70);
            this.txtColonia.Name = "txtColonia";
            this.txtColonia.Size = new System.Drawing.Size(167, 22);
            this.txtColonia.TabIndex = 3;
            // 
            // tcPedidosGrids
            // 
            this.tcPedidosGrids.Controls.Add(this.tabPage1);
            this.tcPedidosGrids.Controls.Add(this.tabPage2);
            this.tcPedidosGrids.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcPedidosGrids.Location = new System.Drawing.Point(45, 23);
            this.tcPedidosGrids.Name = "tcPedidosGrids";
            this.tcPedidosGrids.SelectedIndex = 0;
            this.tcPedidosGrids.Size = new System.Drawing.Size(1556, 745);
            this.tcPedidosGrids.TabIndex = 86;
            this.tcPedidosGrids.SelectedIndexChanged += new System.EventHandler(this.tcPedidosGrids_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtCantidadVentas);
            this.tabPage1.Controls.Add(this.chkVerFacturasEliminadas);
            this.tabPage1.Controls.Add(this.txtFacturaFiltro);
            this.tabPage1.Controls.Add(this.txtDescripcionFiltro);
            this.tabPage1.Controls.Add(this.txtNumClienteFiltro);
            this.tabPage1.Controls.Add(this.btnFiltrarCliente);
            this.tabPage1.Controls.Add(this.txtClienteFiltro);
            this.tabPage1.Controls.Add(this.btnEliminar);
            this.tabPage1.Controls.Add(this.btnRefrescar);
            this.tabPage1.Controls.Add(this.btnCancelar);
            this.tabPage1.Controls.Add(this.gvPedidos);
            this.tabPage1.Controls.Add(this.btnVerFactura);
            this.tabPage1.Controls.Add(this.btnFacturar);
            this.tabPage1.Controls.Add(this.btnEnviaCorreo);
            this.tabPage1.Controls.Add(this.pnlFacturacion);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Tai Le", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1548, 718);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Registros de Ventas";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1102, 647);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 128;
            this.label3.Text = "Cantidad:";
            this.label3.Visible = false;
            // 
            // txtCantidadVentas
            // 
            this.txtCantidadVentas.Enabled = false;
            this.txtCantidadVentas.Location = new System.Drawing.Point(1150, 643);
            this.txtCantidadVentas.Name = "txtCantidadVentas";
            this.txtCantidadVentas.Size = new System.Drawing.Size(55, 19);
            this.txtCantidadVentas.TabIndex = 127;
            this.txtCantidadVentas.Visible = false;
            // 
            // chkVerFacturasEliminadas
            // 
            this.chkVerFacturasEliminadas.AutoSize = true;
            this.chkVerFacturasEliminadas.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkVerFacturasEliminadas.Location = new System.Drawing.Point(1052, 9);
            this.chkVerFacturasEliminadas.Name = "chkVerFacturasEliminadas";
            this.chkVerFacturasEliminadas.Size = new System.Drawing.Size(151, 20);
            this.chkVerFacturasEliminadas.TabIndex = 126;
            this.chkVerFacturasEliminadas.Text = "Ver Facturas Eliminadas";
            this.chkVerFacturasEliminadas.UseVisualStyleBackColor = true;
            this.chkVerFacturasEliminadas.CheckedChanged += new System.EventHandler(this.chkVerFacturasEliminadas_CheckedChanged);
            // 
            // txtFacturaFiltro
            // 
            this.txtFacturaFiltro.Font = new System.Drawing.Font("Microsoft Tai Le", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacturaFiltro.Location = new System.Drawing.Point(752, 6);
            this.txtFacturaFiltro.Name = "txtFacturaFiltro";
            this.txtFacturaFiltro.Size = new System.Drawing.Size(108, 21);
            this.txtFacturaFiltro.TabIndex = 125;
            this.txtFacturaFiltro.TextChanged += new System.EventHandler(this.btnFiltrarCliente_Click);
            // 
            // txtDescripcionFiltro
            // 
            this.txtDescripcionFiltro.Font = new System.Drawing.Font("Microsoft Tai Le", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcionFiltro.Location = new System.Drawing.Point(303, 6);
            this.txtDescripcionFiltro.Name = "txtDescripcionFiltro";
            this.txtDescripcionFiltro.Size = new System.Drawing.Size(447, 21);
            this.txtDescripcionFiltro.TabIndex = 124;
            this.txtDescripcionFiltro.TextChanged += new System.EventHandler(this.btnFiltrarCliente_Click);
            // 
            // txtNumClienteFiltro
            // 
            this.txtNumClienteFiltro.Font = new System.Drawing.Font("Microsoft Tai Le", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumClienteFiltro.Location = new System.Drawing.Point(4, 6);
            this.txtNumClienteFiltro.Name = "txtNumClienteFiltro";
            this.txtNumClienteFiltro.Size = new System.Drawing.Size(73, 21);
            this.txtNumClienteFiltro.TabIndex = 123;
            this.txtNumClienteFiltro.TextChanged += new System.EventHandler(this.btnFiltrarCliente_Click);
            // 
            // btnFiltrarCliente
            // 
            this.btnFiltrarCliente.BackColor = System.Drawing.Color.Transparent;
            this.btnFiltrarCliente.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFiltrarCliente.BackgroundImage")));
            this.btnFiltrarCliente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFiltrarCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltrarCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrarCliente.Location = new System.Drawing.Point(865, 2);
            this.btnFiltrarCliente.Name = "btnFiltrarCliente";
            this.btnFiltrarCliente.Size = new System.Drawing.Size(37, 26);
            this.btnFiltrarCliente.TabIndex = 122;
            this.btnFiltrarCliente.UseVisualStyleBackColor = false;
            this.btnFiltrarCliente.Click += new System.EventHandler(this.btnFiltrarCliente_Click);
            // 
            // txtClienteFiltro
            // 
            this.txtClienteFiltro.Font = new System.Drawing.Font("Microsoft Tai Le", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClienteFiltro.Location = new System.Drawing.Point(79, 6);
            this.txtClienteFiltro.Name = "txtClienteFiltro";
            this.txtClienteFiltro.Size = new System.Drawing.Size(222, 21);
            this.txtClienteFiltro.TabIndex = 121;
            this.txtClienteFiltro.TextChanged += new System.EventHandler(this.btnFiltrarCliente_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackgroundImage = global::Aires.Properties.Resources.flechabaja;
            this.btnEliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.Location = new System.Drawing.Point(6, 644);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(77, 66);
            this.btnEliminar.TabIndex = 113;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.BackColor = System.Drawing.Color.White;
            this.btnRefrescar.BackgroundImage = global::Aires.Properties.Resources.Refresh;
            this.btnRefrescar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescar.Location = new System.Drawing.Point(110, 644);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(77, 66);
            this.btnRefrescar.TabIndex = 112;
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefrescar.UseVisualStyleBackColor = false;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.BackgroundImage = global::Aires.Properties.Resources.Paper_Cancel__chico_;
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 6.1F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(1211, 33);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(77, 66);
            this.btnCancelar.TabIndex = 119;
            this.btnCancelar.Text = "Cancelar Factura";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // gvPedidos
            // 
            this.gvPedidos.AllowUserToAddRows = false;
            this.gvPedidos.AutoGenerateColumns = false;
            this.gvPedidos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvPedidos.BackgroundColor = System.Drawing.Color.White;
            this.gvPedidos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPedidos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumCliente,
            this.clienteDataGridViewTextBoxColumn,
            this.detalleDataGridViewTextBoxColumn,
            this.Factura,
            this.Facturado,
            this.FechaCorta,
            this.totalDataGridViewTextBoxColumn,
            this.EstatusDescripcion});
            this.gvPedidos.DataSource = this.entPedidoBindingSource;
            this.gvPedidos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvPedidos.GridColor = System.Drawing.Color.DimGray;
            this.gvPedidos.Location = new System.Drawing.Point(4, 33);
            this.gvPedidos.MultiSelect = false;
            this.gvPedidos.Name = "gvPedidos";
            this.gvPedidos.ReadOnly = true;
            this.gvPedidos.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial Unicode MS", 7F);
            this.gvPedidos.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvPedidos.RowTemplate.Height = 27;
            this.gvPedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvPedidos.Size = new System.Drawing.Size(1201, 605);
            this.gvPedidos.TabIndex = 76;
            this.gvPedidos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPedidos_CellDoubleClick);
            this.gvPedidos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvPedidos_ColumnHeaderMouseClick);
            this.gvPedidos.SelectionChanged += new System.EventHandler(this.gvPedidos_SelectionChanged);
            // 
            // NumCliente
            // 
            this.NumCliente.DataPropertyName = "NumCliente";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NumCliente.DefaultCellStyle = dataGridViewCellStyle1;
            this.NumCliente.FillWeight = 1F;
            this.NumCliente.HeaderText = "Num. Cliente";
            this.NumCliente.Name = "NumCliente";
            this.NumCliente.ReadOnly = true;
            // 
            // clienteDataGridViewTextBoxColumn
            // 
            this.clienteDataGridViewTextBoxColumn.DataPropertyName = "Cliente";
            this.clienteDataGridViewTextBoxColumn.FillWeight = 3F;
            this.clienteDataGridViewTextBoxColumn.HeaderText = "Cliente";
            this.clienteDataGridViewTextBoxColumn.Name = "clienteDataGridViewTextBoxColumn";
            this.clienteDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // detalleDataGridViewTextBoxColumn
            // 
            this.detalleDataGridViewTextBoxColumn.DataPropertyName = "Detalle";
            this.detalleDataGridViewTextBoxColumn.FillWeight = 6F;
            this.detalleDataGridViewTextBoxColumn.HeaderText = "Detalle";
            this.detalleDataGridViewTextBoxColumn.Name = "detalleDataGridViewTextBoxColumn";
            this.detalleDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Factura
            // 
            this.Factura.DataPropertyName = "Factura";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Factura.DefaultCellStyle = dataGridViewCellStyle2;
            this.Factura.FillWeight = 1.5F;
            this.Factura.HeaderText = "Factura";
            this.Factura.Name = "Factura";
            this.Factura.ReadOnly = true;
            // 
            // Facturado
            // 
            this.Facturado.DataPropertyName = "Facturado";
            this.Facturado.FillWeight = 1F;
            this.Facturado.HeaderText = "Facturado";
            this.Facturado.Name = "Facturado";
            this.Facturado.ReadOnly = true;
            // 
            // FechaCorta
            // 
            this.FechaCorta.DataPropertyName = "FechaCorta";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.FechaCorta.DefaultCellStyle = dataGridViewCellStyle3;
            this.FechaCorta.FillWeight = 1.5F;
            this.FechaCorta.HeaderText = "Fecha";
            this.FechaCorta.Name = "FechaCorta";
            this.FechaCorta.ReadOnly = true;
            // 
            // totalDataGridViewTextBoxColumn
            // 
            this.totalDataGridViewTextBoxColumn.DataPropertyName = "Total";
            dataGridViewCellStyle4.Format = "c2";
            this.totalDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.totalDataGridViewTextBoxColumn.FillWeight = 2F;
            this.totalDataGridViewTextBoxColumn.HeaderText = "Total";
            this.totalDataGridViewTextBoxColumn.Name = "totalDataGridViewTextBoxColumn";
            this.totalDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // EstatusDescripcion
            // 
            this.EstatusDescripcion.DataPropertyName = "EstatusDescripcion";
            this.EstatusDescripcion.FillWeight = 1.5F;
            this.EstatusDescripcion.HeaderText = "Estatus";
            this.EstatusDescripcion.Name = "EstatusDescripcion";
            this.EstatusDescripcion.ReadOnly = true;
            // 
            // btnVerFactura
            // 
            this.btnVerFactura.BackColor = System.Drawing.Color.White;
            this.btnVerFactura.BackgroundImage = global::Aires.Properties.Resources.Paper_Search__chico_;
            this.btnVerFactura.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnVerFactura.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerFactura.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerFactura.Location = new System.Drawing.Point(214, 643);
            this.btnVerFactura.Name = "btnVerFactura";
            this.btnVerFactura.Size = new System.Drawing.Size(77, 66);
            this.btnVerFactura.TabIndex = 116;
            this.btnVerFactura.Text = "Ver Factura";
            this.btnVerFactura.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnVerFactura.UseVisualStyleBackColor = false;
            this.btnVerFactura.Click += new System.EventHandler(this.btnVerFactura_Click);
            // 
            // btnFacturar
            // 
            this.btnFacturar.BackColor = System.Drawing.Color.White;
            this.btnFacturar.BackgroundImage = global::Aires.Properties.Resources.Paper__chico_;
            this.btnFacturar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFacturar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFacturar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFacturar.Location = new System.Drawing.Point(1397, 34);
            this.btnFacturar.Name = "btnFacturar";
            this.btnFacturar.Size = new System.Drawing.Size(77, 66);
            this.btnFacturar.TabIndex = 118;
            this.btnFacturar.Text = "Facturar";
            this.btnFacturar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFacturar.UseVisualStyleBackColor = false;
            this.btnFacturar.Visible = false;
            this.btnFacturar.Click += new System.EventHandler(this.btnFacturar_Click);
            // 
            // btnEnviaCorreo
            // 
            this.btnEnviaCorreo.BackColor = System.Drawing.Color.White;
            this.btnEnviaCorreo.BackgroundImage = global::Aires.Properties.Resources.Mail_Search__chico_;
            this.btnEnviaCorreo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEnviaCorreo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnviaCorreo.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviaCorreo.Location = new System.Drawing.Point(1299, 34);
            this.btnEnviaCorreo.Name = "btnEnviaCorreo";
            this.btnEnviaCorreo.Size = new System.Drawing.Size(83, 66);
            this.btnEnviaCorreo.TabIndex = 120;
            this.btnEnviaCorreo.Text = "Enviar Correo";
            this.btnEnviaCorreo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEnviaCorreo.UseVisualStyleBackColor = false;
            this.btnEnviaCorreo.Click += new System.EventHandler(this.btnEnviaCorreo_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rdoPorFechasVentas);
            this.tabPage2.Controls.Add(this.pnlVentasPorFechas);
            this.tabPage2.Controls.Add(this.btnRefrescarReporteVentas);
            this.tabPage2.Controls.Add(this.tabControl1);
            this.tabPage2.Controls.Add(this.rdoPorMesVentas);
            this.tabPage2.Controls.Add(this.rdoPorDiaVentas);
            this.tabPage2.Controls.Add(this.pnlVentasPorDia);
            this.tabPage2.Controls.Add(this.pnlVentasPorMes);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1548, 718);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Impresión";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rdoPorFechasVentas
            // 
            this.rdoPorFechasVentas.AutoSize = true;
            this.rdoPorFechasVentas.Location = new System.Drawing.Point(11, 94);
            this.rdoPorFechasVentas.Name = "rdoPorFechasVentas";
            this.rdoPorFechasVentas.Size = new System.Drawing.Size(80, 18);
            this.rdoPorFechasVentas.TabIndex = 115;
            this.rdoPorFechasVentas.Text = "Por Fechas";
            this.rdoPorFechasVentas.UseVisualStyleBackColor = true;
            this.rdoPorFechasVentas.CheckedChanged += new System.EventHandler(this.rdoPorFechasVentas_CheckedChanged);
            // 
            // pnlVentasPorFechas
            // 
            this.pnlVentasPorFechas.Controls.Add(this.dtpFechaHastaVentas);
            this.pnlVentasPorFechas.Controls.Add(this.dtpFechaDesdeVentas);
            this.pnlVentasPorFechas.Enabled = false;
            this.pnlVentasPorFechas.Location = new System.Drawing.Point(91, 84);
            this.pnlVentasPorFechas.Name = "pnlVentasPorFechas";
            this.pnlVentasPorFechas.Size = new System.Drawing.Size(500, 32);
            this.pnlVentasPorFechas.TabIndex = 114;
            // 
            // dtpFechaHastaVentas
            // 
            this.dtpFechaHastaVentas.Location = new System.Drawing.Point(259, 7);
            this.dtpFechaHastaVentas.Name = "dtpFechaHastaVentas";
            this.dtpFechaHastaVentas.Size = new System.Drawing.Size(224, 21);
            this.dtpFechaHastaVentas.TabIndex = 15;
            this.dtpFechaHastaVentas.ValueChanged += new System.EventHandler(this.dtpFechaHastaVentas_ValueChanged);
            // 
            // dtpFechaDesdeVentas
            // 
            this.dtpFechaDesdeVentas.Location = new System.Drawing.Point(5, 7);
            this.dtpFechaDesdeVentas.Name = "dtpFechaDesdeVentas";
            this.dtpFechaDesdeVentas.Size = new System.Drawing.Size(224, 21);
            this.dtpFechaDesdeVentas.TabIndex = 15;
            this.dtpFechaDesdeVentas.ValueChanged += new System.EventHandler(this.dtpFechaDesdeVentas_ValueChanged);
            // 
            // btnRefrescarReporteVentas
            // 
            this.btnRefrescarReporteVentas.BackColor = System.Drawing.Color.White;
            this.btnRefrescarReporteVentas.BackgroundImage = global::Aires.Properties.Resources.Refresh;
            this.btnRefrescarReporteVentas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRefrescarReporteVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescarReporteVentas.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescarReporteVentas.Location = new System.Drawing.Point(350, 11);
            this.btnRefrescarReporteVentas.Name = "btnRefrescarReporteVentas";
            this.btnRefrescarReporteVentas.Size = new System.Drawing.Size(77, 66);
            this.btnRefrescarReporteVentas.TabIndex = 113;
            this.btnRefrescarReporteVentas.Text = "Refrescar";
            this.btnRefrescarReporteVentas.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefrescarReporteVentas.UseVisualStyleBackColor = false;
            this.btnRefrescarReporteVentas.Click += new System.EventHandler(this.btnRefrescarReporteVentas_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Tai Le", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(7, 115);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1328, 553);
            this.tabControl1.TabIndex = 45;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.rvVentas);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1320, 527);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Impresión";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // rvVentas
            // 
            this.rvVentas.DocumentMapWidth = 88;
            reportDataSource1.Name = "dsVentas";
            reportDataSource1.Value = this.entPedidoBindingSource;
            this.rvVentas.LocalReport.DataSources.Add(reportDataSource1);
            this.rvVentas.LocalReport.ReportEmbeddedResource = "Aires.Reportes.rptVentas.rdlc";
            this.rvVentas.Location = new System.Drawing.Point(0, 0);
            this.rvVentas.Name = "rvVentas";
            this.rvVentas.Size = new System.Drawing.Size(1088, 527);
            this.rvVentas.TabIndex = 0;
            // 
            // rdoPorMesVentas
            // 
            this.rdoPorMesVentas.AutoSize = true;
            this.rdoPorMesVentas.Checked = true;
            this.rdoPorMesVentas.Location = new System.Drawing.Point(11, 17);
            this.rdoPorMesVentas.Name = "rdoPorMesVentas";
            this.rdoPorMesVentas.Size = new System.Drawing.Size(66, 18);
            this.rdoPorMesVentas.TabIndex = 44;
            this.rdoPorMesVentas.TabStop = true;
            this.rdoPorMesVentas.Text = "Por Mes";
            this.rdoPorMesVentas.UseVisualStyleBackColor = true;
            this.rdoPorMesVentas.CheckedChanged += new System.EventHandler(this.rdoVentasPorMes_CheckedChanged);
            // 
            // rdoPorDiaVentas
            // 
            this.rdoPorDiaVentas.AutoSize = true;
            this.rdoPorDiaVentas.Location = new System.Drawing.Point(11, 56);
            this.rdoPorDiaVentas.Name = "rdoPorDiaVentas";
            this.rdoPorDiaVentas.Size = new System.Drawing.Size(62, 18);
            this.rdoPorDiaVentas.TabIndex = 43;
            this.rdoPorDiaVentas.Text = "Por Día";
            this.rdoPorDiaVentas.UseVisualStyleBackColor = true;
            this.rdoPorDiaVentas.CheckedChanged += new System.EventHandler(this.rdoVentasPorSemana_CheckedChanged);
            // 
            // pnlVentasPorDia
            // 
            this.pnlVentasPorDia.Controls.Add(this.dtpFechaDiaVentas);
            this.pnlVentasPorDia.Enabled = false;
            this.pnlVentasPorDia.Location = new System.Drawing.Point(91, 46);
            this.pnlVentasPorDia.Name = "pnlVentasPorDia";
            this.pnlVentasPorDia.Size = new System.Drawing.Size(243, 32);
            this.pnlVentasPorDia.TabIndex = 42;
            // 
            // dtpFechaDiaVentas
            // 
            this.dtpFechaDiaVentas.Location = new System.Drawing.Point(5, 7);
            this.dtpFechaDiaVentas.Name = "dtpFechaDiaVentas";
            this.dtpFechaDiaVentas.Size = new System.Drawing.Size(224, 21);
            this.dtpFechaDiaVentas.TabIndex = 15;
            this.dtpFechaDiaVentas.ValueChanged += new System.EventHandler(this.btnRefrescarReporteVentas_Click);
            // 
            // pnlVentasPorMes
            // 
            this.pnlVentasPorMes.Controls.Add(this.cmbMesesVentas);
            this.pnlVentasPorMes.Controls.Add(this.cmbAñoVentas);
            this.pnlVentasPorMes.Location = new System.Drawing.Point(91, 6);
            this.pnlVentasPorMes.Name = "pnlVentasPorMes";
            this.pnlVentasPorMes.Size = new System.Drawing.Size(243, 34);
            this.pnlVentasPorMes.TabIndex = 41;
            // 
            // cmbMesesVentas
            // 
            this.cmbMesesVentas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMesesVentas.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMesesVentas.FormattingEnabled = true;
            this.cmbMesesVentas.Items.AddRange(new object[] {
            "ENERO",
            "FEBRERO",
            "MARZO",
            "ABRIL",
            "MAYO",
            "JUNIO",
            "JULIO",
            "AGOSTO",
            "SEPTIEMBRE",
            "OCTUBRE",
            "NOVIEMBRE",
            "DICIEMBRE"});
            this.cmbMesesVentas.Location = new System.Drawing.Point(5, 6);
            this.cmbMesesVentas.Name = "cmbMesesVentas";
            this.cmbMesesVentas.Size = new System.Drawing.Size(126, 26);
            this.cmbMesesVentas.TabIndex = 19;
            this.cmbMesesVentas.SelectedIndexChanged += new System.EventHandler(this.btnRefrescarReporteVentas_Click);
            // 
            // cmbAñoVentas
            // 
            this.cmbAñoVentas.DataSource = this.entCatalogoGenericoBindingSource;
            this.cmbAñoVentas.DisplayMember = "Descripcion";
            this.cmbAñoVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAñoVentas.FormattingEnabled = true;
            this.cmbAñoVentas.Location = new System.Drawing.Point(152, 6);
            this.cmbAñoVentas.Name = "cmbAñoVentas";
            this.cmbAñoVentas.Size = new System.Drawing.Size(77, 24);
            this.cmbAñoVentas.TabIndex = 20;
            this.cmbAñoVentas.ValueMember = "Descripcion";
            this.cmbAñoVentas.SelectedIndexChanged += new System.EventHandler(this.btnRefrescarReporteVentas_Click);
            // 
            // entCatalogoGenericoBindingSource
            // 
            this.entCatalogoGenericoBindingSource.DataSource = typeof(AiresEntidades.EntCatalogoGenerico);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Location = new System.Drawing.Point(438, 21);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(51, 13);
            this.label24.TabIndex = 118;
            this.label24.Text = "Empresa:";
            // 
            // cmbEmpresas
            // 
            this.cmbEmpresas.DisplayMember = "Descripcion";
            this.cmbEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpresas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmpresas.FormattingEnabled = true;
            this.cmbEmpresas.Location = new System.Drawing.Point(489, 11);
            this.cmbEmpresas.Name = "cmbEmpresas";
            this.cmbEmpresas.Size = new System.Drawing.Size(359, 28);
            this.cmbEmpresas.TabIndex = 119;
            this.cmbEmpresas.ValueMember = "Id";
            this.cmbEmpresas.SelectedIndexChanged += new System.EventHandler(this.cmbEmpresas_SelectedIndexChanged);
            // 
            // btnBuscaEmpresa
            // 
            this.btnBuscaEmpresa.BackColor = System.Drawing.Color.Transparent;
            this.btnBuscaEmpresa.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscaEmpresa.BackgroundImage")));
            this.btnBuscaEmpresa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBuscaEmpresa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscaEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscaEmpresa.Location = new System.Drawing.Point(854, 11);
            this.btnBuscaEmpresa.Name = "btnBuscaEmpresa";
            this.btnBuscaEmpresa.Size = new System.Drawing.Size(40, 28);
            this.btnBuscaEmpresa.TabIndex = 117;
            this.btnBuscaEmpresa.UseVisualStyleBackColor = false;
            this.btnBuscaEmpresa.Click += new System.EventHandler(this.btnBuscaEmpresa_Click);
            // 
            // Reportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1362, 741);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.cmbEmpresas);
            this.Controls.Add(this.btnBuscaEmpresa);
            this.Controls.Add(this.tcPedidosGrids);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Reportes";
            this.Text = "Registros";
            this.Load += new System.EventHandler(this.Reportes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.entPedidoBindingSource)).EndInit();
            this.pnlFacturacion.ResumeLayout(false);
            this.pnlFacturacion.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tcPedidosGrids.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPedidos)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.pnlVentasPorFechas.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.pnlVentasPorDia.ResumeLayout(false);
            this.pnlVentasPorMes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.entCatalogoGenericoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcPedidosGrids;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView gvPedidos;
        private System.Windows.Forms.BindingSource entPedidoBindingSource;
        private System.Windows.Forms.Panel pnlFacturacion;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbFormaPago;
        private System.Windows.Forms.ComboBox cmbMetodoPago;
        private System.Windows.Forms.TextBox txtNumeroCuenta;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtCondicionesPago;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtMetodoPago;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtFormaPago;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCP;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtCalle;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtNoExterior;
        private System.Windows.Forms.TextBox txtMunicipio;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtNoInterior;
        private System.Windows.Forms.TextBox txtLocalidad;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtColonia;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnVerFactura;
        private System.Windows.Forms.Button btnEnviaCorreo;
        private System.Windows.Forms.Button btnFacturar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RadioButton rdoPorMesVentas;
        private System.Windows.Forms.RadioButton rdoPorDiaVentas;
        private System.Windows.Forms.Panel pnlVentasPorDia;
        private System.Windows.Forms.DateTimePicker dtpFechaDiaVentas;
        private System.Windows.Forms.Panel pnlVentasPorMes;
        private System.Windows.Forms.ComboBox cmbMesesVentas;
        private System.Windows.Forms.ComboBox cmbAñoVentas;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private Microsoft.Reporting.WinForms.ReportViewer rvVentas;
        private System.Windows.Forms.Button btnRefrescarReporteVentas;
        private System.Windows.Forms.BindingSource entCatalogoGenericoBindingSource;
        private System.Windows.Forms.CheckBox chkFacturaPublicoGeneral;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRFC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombreFiscal;
        private System.Windows.Forms.Button btnFiltrarCliente;
        private System.Windows.Forms.TextBox txtClienteFiltro;
        private System.Windows.Forms.TextBox txtNumClienteFiltro;
        private System.Windows.Forms.TextBox txtFacturaFiltro;
        private System.Windows.Forms.TextBox txtDescripcionFiltro;
        private System.Windows.Forms.CheckBox chkVerFacturasEliminadas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCantidadVentas;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cmbEmpresas;
        private System.Windows.Forms.Button btnBuscaEmpresa;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn clienteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn detalleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Factura;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Facturado;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaCorta;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstatusDescripcion;
        private System.Windows.Forms.RadioButton rdoPorFechasVentas;
        private System.Windows.Forms.Panel pnlVentasPorFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaHastaVentas;
        private System.Windows.Forms.DateTimePicker dtpFechaDesdeVentas;
    }
}