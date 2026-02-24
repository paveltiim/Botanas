namespace Aires.Pantallas
{
    partial class MuestraPagosNC
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.EntEmpresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.EntPedidoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvPagos = new System.Windows.Forms.DataGridView();
            this.btnEliminaPago = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtFactura = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel19 = new System.Windows.Forms.Panel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.cmbTipoRegistroVentas = new System.Windows.Forms.ComboBox();
            this.pnlFechasPagoVentas = new System.Windows.Forms.Panel();
            this.dtpFechaPagoHastaVentas = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaPagoDesdeVentas = new System.Windows.Forms.DateTimePicker();
            this.label28 = new System.Windows.Forms.Label();
            this.panel21 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.cmbEstatusVenta = new System.Windows.Forms.ComboBox();
            this.panel22 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbMetodoPago = new System.Windows.Forms.ComboBox();
            this.panel23 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbFormaPagoVentas = new System.Windows.Forms.ComboBox();
            this.panel24 = new System.Windows.Forms.Panel();
            this.label26 = new System.Windows.Forms.Label();
            this.cmbMonedaVentas = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbClientesVentasPorCliente = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rvNotasCreditoClientes = new Microsoft.Reporting.WinForms.ReportViewer();
            this.clienteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaCorta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstatusDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VerComplemento = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.EntEmpresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EntPedidoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPagos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.panel19.SuspendLayout();
            this.panel20.SuspendLayout();
            this.pnlFechasPagoVentas.SuspendLayout();
            this.panel21.SuspendLayout();
            this.panel22.SuspendLayout();
            this.panel23.SuspendLayout();
            this.panel24.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // EntEmpresaBindingSource
            // 
            this.EntEmpresaBindingSource.DataSource = typeof(AiresEntidades.EntEmpresa);
            // 
            // EntPedidoBindingSource
            // 
            this.EntPedidoBindingSource.DataSource = typeof(AiresEntidades.EntPedido);
            // 
            // gvPagos
            // 
            this.gvPagos.AllowUserToAddRows = false;
            this.gvPagos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvPagos.AutoGenerateColumns = false;
            this.gvPagos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvPagos.BackgroundColor = System.Drawing.Color.White;
            this.gvPagos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPagos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clienteDataGridViewTextBoxColumn,
            this.Factura,
            this.Pago,
            this.FechaPago,
            this.FechaCorta,
            this.UUID,
            this.Descripcion,
            this.EstatusDescripcion,
            this.dataGridViewTextBoxColumn1,
            this.VerComplemento});
            this.gvPagos.DataSource = this.EntPedidoBindingSource;
            this.gvPagos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvPagos.GridColor = System.Drawing.Color.DimGray;
            this.gvPagos.Location = new System.Drawing.Point(0, 0);
            this.gvPagos.Name = "gvPagos";
            this.gvPagos.ReadOnly = true;
            this.gvPagos.RowHeadersVisible = false;
            this.gvPagos.RowHeadersWidth = 51;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.gvPagos.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.gvPagos.RowTemplate.Height = 27;
            this.gvPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvPagos.Size = new System.Drawing.Size(780, 426);
            this.gvPagos.TabIndex = 89;
            this.gvPagos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPagos_CellContentClick);
            this.gvPagos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvPagos_ColumnHeaderMouseClick);
            // 
            // btnEliminaPago
            // 
            this.btnEliminaPago.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnEliminaPago.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEliminaPago.BackgroundImage = global::Aires.Properties.Resources.flechabaja;
            this.btnEliminaPago.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEliminaPago.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminaPago.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminaPago.Location = new System.Drawing.Point(796, 109);
            this.btnEliminaPago.Name = "btnEliminaPago";
            this.btnEliminaPago.Size = new System.Drawing.Size(84, 72);
            this.btnEliminaPago.TabIndex = 11;
            this.btnEliminaPago.Text = "Cancelar";
            this.btnEliminaPago.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEliminaPago.UseVisualStyleBackColor = false;
            this.btnEliminaPago.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAgregar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAgregar.BackgroundImage = global::Aires.Properties.Resources.palomitaChica;
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(346, 427);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(84, 72);
            this.btnAgregar.TabIndex = 10;
            this.btnAgregar.Text = "¡Listo!";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = false;
            // 
            // txtFactura
            // 
            this.txtFactura.Location = new System.Drawing.Point(3, 20);
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.Size = new System.Drawing.Size(92, 25);
            this.txtFactura.TabIndex = 95;
            this.txtFactura.TextChanged += new System.EventHandler(this.txtFactura_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 18);
            this.label3.TabIndex = 119;
            this.label3.Text = "Factura:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.flowLayoutPanel4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbClientesVentasPorCliente);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(858, 72);
            this.groupBox1.TabIndex = 157;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            this.groupBox1.Visible = false;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel4.Controls.Add(this.panel19);
            this.flowLayoutPanel4.Controls.Add(this.panel20);
            this.flowLayoutPanel4.Controls.Add(this.pnlFechasPagoVentas);
            this.flowLayoutPanel4.Controls.Add(this.panel21);
            this.flowLayoutPanel4.Controls.Add(this.panel22);
            this.flowLayoutPanel4.Controls.Add(this.panel23);
            this.flowLayoutPanel4.Controls.Add(this.panel24);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(6, 14);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(846, 53);
            this.flowLayoutPanel4.TabIndex = 158;
            // 
            // panel19
            // 
            this.panel19.Controls.Add(this.txtFactura);
            this.panel19.Controls.Add(this.label3);
            this.panel19.Location = new System.Drawing.Point(3, 3);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(98, 50);
            this.panel19.TabIndex = 160;
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.label24);
            this.panel20.Controls.Add(this.cmbTipoRegistroVentas);
            this.panel20.Location = new System.Drawing.Point(107, 3);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(180, 50);
            this.panel20.TabIndex = 161;
            this.panel20.Visible = false;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label24.Location = new System.Drawing.Point(4, 2);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(37, 18);
            this.label24.TabIndex = 158;
            this.label24.Text = "Tipo:";
            // 
            // cmbTipoRegistroVentas
            // 
            this.cmbTipoRegistroVentas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTipoRegistroVentas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoRegistroVentas.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbTipoRegistroVentas.FormattingEnabled = true;
            this.cmbTipoRegistroVentas.Items.AddRange(new object[] {
            "FACTURA ",
            "PAGO",
            "NOTA CRÉDITO",
            "-TODOS-"});
            this.cmbTipoRegistroVentas.Location = new System.Drawing.Point(3, 21);
            this.cmbTipoRegistroVentas.Name = "cmbTipoRegistroVentas";
            this.cmbTipoRegistroVentas.Size = new System.Drawing.Size(174, 26);
            this.cmbTipoRegistroVentas.TabIndex = 157;
            // 
            // pnlFechasPagoVentas
            // 
            this.pnlFechasPagoVentas.Controls.Add(this.dtpFechaPagoHastaVentas);
            this.pnlFechasPagoVentas.Controls.Add(this.dtpFechaPagoDesdeVentas);
            this.pnlFechasPagoVentas.Controls.Add(this.label28);
            this.pnlFechasPagoVentas.Location = new System.Drawing.Point(293, 3);
            this.pnlFechasPagoVentas.Name = "pnlFechasPagoVentas";
            this.pnlFechasPagoVentas.Size = new System.Drawing.Size(218, 50);
            this.pnlFechasPagoVentas.TabIndex = 162;
            this.pnlFechasPagoVentas.Visible = false;
            // 
            // dtpFechaPagoHastaVentas
            // 
            this.dtpFechaPagoHastaVentas.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaPagoHastaVentas.Location = new System.Drawing.Point(115, 21);
            this.dtpFechaPagoHastaVentas.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaPagoHastaVentas.Name = "dtpFechaPagoHastaVentas";
            this.dtpFechaPagoHastaVentas.Size = new System.Drawing.Size(100, 25);
            this.dtpFechaPagoHastaVentas.TabIndex = 160;
            // 
            // dtpFechaPagoDesdeVentas
            // 
            this.dtpFechaPagoDesdeVentas.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaPagoDesdeVentas.Location = new System.Drawing.Point(7, 21);
            this.dtpFechaPagoDesdeVentas.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaPagoDesdeVentas.MinDate = new System.DateTime(2019, 1, 1, 0, 0, 0, 0);
            this.dtpFechaPagoDesdeVentas.Name = "dtpFechaPagoDesdeVentas";
            this.dtpFechaPagoDesdeVentas.Size = new System.Drawing.Size(100, 25);
            this.dtpFechaPagoDesdeVentas.TabIndex = 159;
            this.dtpFechaPagoDesdeVentas.Value = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label28.Location = new System.Drawing.Point(4, 2);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(81, 18);
            this.label28.TabIndex = 158;
            this.label28.Text = "Fecha Pago:";
            // 
            // panel21
            // 
            this.panel21.Controls.Add(this.label25);
            this.panel21.Controls.Add(this.cmbEstatusVenta);
            this.panel21.Location = new System.Drawing.Point(517, 3);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(115, 50);
            this.panel21.TabIndex = 162;
            this.panel21.Visible = false;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label25.Location = new System.Drawing.Point(4, 2);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(55, 18);
            this.label25.TabIndex = 158;
            this.label25.Text = "Estatus:";
            // 
            // cmbEstatusVenta
            // 
            this.cmbEstatusVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstatusVenta.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbEstatusVenta.FormattingEnabled = true;
            this.cmbEstatusVenta.Items.AddRange(new object[] {
            "VIGENTE",
            "PAGADA",
            " CANCELADAS",
            "-TODAS-"});
            this.cmbEstatusVenta.Location = new System.Drawing.Point(3, 21);
            this.cmbEstatusVenta.Name = "cmbEstatusVenta";
            this.cmbEstatusVenta.Size = new System.Drawing.Size(109, 26);
            this.cmbEstatusVenta.TabIndex = 157;
            // 
            // panel22
            // 
            this.panel22.Controls.Add(this.label11);
            this.panel22.Controls.Add(this.cmbMetodoPago);
            this.panel22.Location = new System.Drawing.Point(3, 59);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(256, 50);
            this.panel22.TabIndex = 159;
            this.panel22.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(126, 18);
            this.label11.TabIndex = 152;
            this.label11.Text = "Condición de Pago:";
            // 
            // cmbMetodoPago
            // 
            this.cmbMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMetodoPago.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbMetodoPago.FormattingEnabled = true;
            this.cmbMetodoPago.Items.AddRange(new object[] {
            "CONTADO | PUE - Pago en una sola exhibición",
            "CRÉDITO | PPD - Pago en parcialidades o diferido",
            "-TODOS-"});
            this.cmbMetodoPago.Location = new System.Drawing.Point(6, 22);
            this.cmbMetodoPago.Name = "cmbMetodoPago";
            this.cmbMetodoPago.Size = new System.Drawing.Size(241, 26);
            this.cmbMetodoPago.TabIndex = 150;
            // 
            // panel23
            // 
            this.panel23.Controls.Add(this.label10);
            this.panel23.Controls.Add(this.cmbFormaPagoVentas);
            this.panel23.Location = new System.Drawing.Point(265, 59);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(266, 52);
            this.panel23.TabIndex = 160;
            this.panel23.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 18);
            this.label10.TabIndex = 151;
            this.label10.Text = "Forma de Pago:";
            // 
            // cmbFormaPagoVentas
            // 
            this.cmbFormaPagoVentas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormaPagoVentas.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbFormaPagoVentas.FormattingEnabled = true;
            this.cmbFormaPagoVentas.Items.AddRange(new object[] {
            "01 - Efectivo",
            "02 - Cheque nominativo",
            "03 - Transferencia electrónica de fondos",
            "04 - Tarjeta de crédito",
            "05 - Monedero electrónico",
            "06 - Dinero electrónico",
            "08 - Vales de despensa",
            "12 - Dación en pago",
            "13 - Pago por subrogación",
            "14 - Pago por consignación",
            "15 - Condonación",
            "17 - Compensación",
            "23 - Novación",
            "24 - Confusión",
            "25 - Remisión de deuda",
            "26 - Prescripción o caducidad",
            "27 - A satisfacción del acreedor",
            "28 - Tarjeta de débito",
            "29 - Tarjeta de servicios",
            "30 - Aplicación de anticipos",
            "99 - Por definir",
            "-TODOS-"});
            this.cmbFormaPagoVentas.Location = new System.Drawing.Point(9, 22);
            this.cmbFormaPagoVentas.Name = "cmbFormaPagoVentas";
            this.cmbFormaPagoVentas.Size = new System.Drawing.Size(248, 26);
            this.cmbFormaPagoVentas.TabIndex = 149;
            // 
            // panel24
            // 
            this.panel24.Controls.Add(this.label26);
            this.panel24.Controls.Add(this.cmbMonedaVentas);
            this.panel24.Location = new System.Drawing.Point(537, 59);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(145, 52);
            this.panel24.TabIndex = 158;
            this.panel24.Visible = false;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label26.Location = new System.Drawing.Point(4, 2);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(59, 18);
            this.label26.TabIndex = 154;
            this.label26.Text = "Moneda";
            // 
            // cmbMonedaVentas
            // 
            this.cmbMonedaVentas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonedaVentas.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbMonedaVentas.FormattingEnabled = true;
            this.cmbMonedaVentas.Items.AddRange(new object[] {
            "  MXN ",
            "  USD ",
            "-TODAS-"});
            this.cmbMonedaVentas.Location = new System.Drawing.Point(6, 21);
            this.cmbMonedaVentas.Name = "cmbMonedaVentas";
            this.cmbMonedaVentas.Size = new System.Drawing.Size(136, 26);
            this.cmbMonedaVentas.TabIndex = 153;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(625, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 18);
            this.label5.TabIndex = 156;
            this.label5.Text = "Clientes:";
            this.label5.Visible = false;
            // 
            // cmbClientesVentasPorCliente
            // 
            this.cmbClientesVentasPorCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbClientesVentasPorCliente.DisplayMember = "Cliente";
            this.cmbClientesVentasPorCliente.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbClientesVentasPorCliente.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbClientesVentasPorCliente.FormattingEnabled = true;
            this.cmbClientesVentasPorCliente.Location = new System.Drawing.Point(628, 25);
            this.cmbClientesVentasPorCliente.Margin = new System.Windows.Forms.Padding(4);
            this.cmbClientesVentasPorCliente.Name = "cmbClientesVentasPorCliente";
            this.cmbClientesVentasPorCliente.Size = new System.Drawing.Size(223, 33);
            this.cmbClientesVentasPorCliente.TabIndex = 155;
            this.cmbClientesVentasPorCliente.ValueMember = "ClienteId";
            this.cmbClientesVentasPorCliente.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(8, 84);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(788, 530);
            this.tabControl1.TabIndex = 158;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gvPagos);
            this.tabPage1.Controls.Add(this.btnAgregar);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(780, 499);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Notas de Crédito";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rvNotasCreditoClientes);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1075, 499);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Impresion/Exportar";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rvNotasCreditoClientes
            // 
            this.rvNotasCreditoClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rvNotasCreditoClientes.DocumentMapWidth = 88;
            reportDataSource1.Name = "dsEmpresasDeudas";
            reportDataSource1.Value = this.EntEmpresaBindingSource;
            reportDataSource2.Name = "dsPedidosNotasCredito";
            reportDataSource2.Value = this.EntPedidoBindingSource;
            this.rvNotasCreditoClientes.LocalReport.DataSources.Add(reportDataSource1);
            this.rvNotasCreditoClientes.LocalReport.DataSources.Add(reportDataSource2);
            this.rvNotasCreditoClientes.LocalReport.ReportEmbeddedResource = "Aires.Reportes.rptNotasCreditoClientes.rdlc";
            this.rvNotasCreditoClientes.Location = new System.Drawing.Point(0, 0);
            this.rvNotasCreditoClientes.Margin = new System.Windows.Forms.Padding(4);
            this.rvNotasCreditoClientes.Name = "rvNotasCreditoClientes";
            this.rvNotasCreditoClientes.ServerReport.BearerToken = null;
            this.rvNotasCreditoClientes.Size = new System.Drawing.Size(1075, 499);
            this.rvNotasCreditoClientes.TabIndex = 52;
            // 
            // clienteDataGridViewTextBoxColumn
            // 
            this.clienteDataGridViewTextBoxColumn.DataPropertyName = "Cliente";
            this.clienteDataGridViewTextBoxColumn.FillWeight = 3.5F;
            this.clienteDataGridViewTextBoxColumn.HeaderText = "Cliente";
            this.clienteDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.clienteDataGridViewTextBoxColumn.Name = "clienteDataGridViewTextBoxColumn";
            this.clienteDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Factura
            // 
            this.Factura.DataPropertyName = "NumOrden";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Factura.DefaultCellStyle = dataGridViewCellStyle1;
            this.Factura.FillWeight = 1.2F;
            this.Factura.HeaderText = "Nota Crédito";
            this.Factura.MinimumWidth = 6;
            this.Factura.Name = "Factura";
            this.Factura.ReadOnly = true;
            // 
            // Pago
            // 
            this.Pago.DataPropertyName = "Pago";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "c2";
            this.Pago.DefaultCellStyle = dataGridViewCellStyle2;
            this.Pago.FillWeight = 0.8F;
            this.Pago.HeaderText = "Cantidad Pago";
            this.Pago.MinimumWidth = 6;
            this.Pago.Name = "Pago";
            this.Pago.ReadOnly = true;
            // 
            // FechaPago
            // 
            this.FechaPago.DataPropertyName = "FechaPago";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "d";
            this.FechaPago.DefaultCellStyle = dataGridViewCellStyle3;
            this.FechaPago.FillWeight = 1F;
            this.FechaPago.HeaderText = "Fecha Pago";
            this.FechaPago.MinimumWidth = 6;
            this.FechaPago.Name = "FechaPago";
            this.FechaPago.ReadOnly = true;
            // 
            // FechaCorta
            // 
            this.FechaCorta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FechaCorta.DataPropertyName = "FechaPago";
            dataGridViewCellStyle4.Format = "t";
            dataGridViewCellStyle4.NullValue = null;
            this.FechaCorta.DefaultCellStyle = dataGridViewCellStyle4;
            this.FechaCorta.FillWeight = 0.8F;
            this.FechaCorta.HeaderText = "Hora Pago ";
            this.FechaCorta.MinimumWidth = 6;
            this.FechaCorta.Name = "FechaCorta";
            this.FechaCorta.ReadOnly = true;
            this.FechaCorta.Visible = false;
            this.FechaCorta.Width = 72;
            // 
            // UUID
            // 
            this.UUID.DataPropertyName = "UUID";
            this.UUID.DividerWidth = 1;
            this.UUID.FillWeight = 4F;
            this.UUID.HeaderText = "UUID";
            this.UUID.MinimumWidth = 6;
            this.UUID.Name = "UUID";
            this.UUID.ReadOnly = true;
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "Descripcion";
            this.Descripcion.FillWeight = 4F;
            this.Descripcion.HeaderText = "Detalle";
            this.Descripcion.MinimumWidth = 6;
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            this.Descripcion.Visible = false;
            // 
            // EstatusDescripcion
            // 
            this.EstatusDescripcion.DataPropertyName = "EstatusDescripcion";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.EstatusDescripcion.DefaultCellStyle = dataGridViewCellStyle5;
            this.EstatusDescripcion.FillWeight = 1.5F;
            this.EstatusDescripcion.HeaderText = "Estatus";
            this.EstatusDescripcion.MinimumWidth = 6;
            this.EstatusDescripcion.Name = "EstatusDescripcion";
            this.EstatusDescripcion.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Factura";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn1.FillWeight = 1F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Factura";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // VerComplemento
            // 
            this.VerComplemento.FillWeight = 0.8F;
            this.VerComplemento.HeaderText = "Ver Nota Crédito";
            this.VerComplemento.Image = global::Aires.Properties.Resources.Search;
            this.VerComplemento.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.VerComplemento.MinimumWidth = 6;
            this.VerComplemento.Name = "VerComplemento";
            this.VerComplemento.ReadOnly = true;
            // 
            // MuestraPagosNC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(882, 619);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnEliminaPago);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MuestraPagosNC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MUESTRA NOTAS CRÉDITO";
            this.Load += new System.EventHandler(this.MuestraPagos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EntEmpresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EntPedidoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPagos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.panel19.ResumeLayout(false);
            this.panel19.PerformLayout();
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.pnlFechasPagoVentas.ResumeLayout(false);
            this.pnlFechasPagoVentas.PerformLayout();
            this.panel21.ResumeLayout(false);
            this.panel21.PerformLayout();
            this.panel22.ResumeLayout(false);
            this.panel22.PerformLayout();
            this.panel23.ResumeLayout(false);
            this.panel23.PerformLayout();
            this.panel24.ResumeLayout(false);
            this.panel24.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnEliminaPago;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView gvPagos;
        private System.Windows.Forms.TextBox txtFactura;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cmbTipoRegistroVentas;
        private System.Windows.Forms.Panel pnlFechasPagoVentas;
        private System.Windows.Forms.DateTimePicker dtpFechaPagoHastaVentas;
        private System.Windows.Forms.DateTimePicker dtpFechaPagoDesdeVentas;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox cmbEstatusVenta;
        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbMetodoPago;
        private System.Windows.Forms.Panel panel23;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbFormaPagoVentas;
        private System.Windows.Forms.Panel panel24;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cmbMonedaVentas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbClientesVentasPorCliente;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.BindingSource EntEmpresaBindingSource;
        private System.Windows.Forms.BindingSource EntPedidoBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer rvNotasCreditoClientes;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoClienteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComplementoPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoPedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn Usuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn clienteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Factura;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pago;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaCorta;
        private System.Windows.Forms.DataGridViewTextBoxColumn UUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstatusDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewImageColumn VerComplemento;
    }
}