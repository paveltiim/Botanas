namespace Aires.Pantallas
{
    partial class ReportesGlobales
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportesGlobales));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.EntProductoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.entPedidoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.EntEmpresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.EntDepositoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tcReportes = new System.Windows.Forms.TabControl();
            this.tpVentas = new System.Windows.Forms.TabPage();
            this.tcReportesVentas = new System.Windows.Forms.TabControl();
            this.tpVentasPorProducto = new System.Windows.Forms.TabPage();
            this.chkSeleccionaTodos = new System.Windows.Forms.CheckBox();
            this.btnFiltrarProductos = new System.Windows.Forms.Button();
            this.gvProductosFiltro = new System.Windows.Forms.DataGridView();
            this.Estatus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.codigoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tcReportesGlobalesGeneral = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.rdoPorClienteGlobal = new System.Windows.Forms.RadioButton();
            this.rdoPorProductoGlobal = new System.Windows.Forms.RadioButton();
            this.rvVentasPorProductoGlobal = new Microsoft.Reporting.WinForms.ReportViewer();
            this.rvVentasPorClienteGlobal = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tpReporteAumento = new System.Windows.Forms.TabPage();
            this.chkCodigosBajos = new System.Windows.Forms.CheckBox();
            this.rvVentasPorProductoAlmacenGlobal = new Microsoft.Reporting.WinForms.ReportViewer();
            this.flpFiltroPorProducto = new System.Windows.Forms.FlowLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkSinComparativo = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlPeriodoComparativo = new System.Windows.Forms.Panel();
            this.cmbPeriodoVentas = new System.Windows.Forms.ComboBox();
            this.entCatalogoGenericoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlPeriodoConsulta = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlPorFechasVentas = new System.Windows.Forms.Panel();
            this.dtpFechaHastaVentas = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaDesdeVentas = new System.Windows.Forms.DateTimePicker();
            this.btnRefrescarVentas = new System.Windows.Forms.Button();
            this.pnlPorMesVentas = new System.Windows.Forms.Panel();
            this.cmbMesesVentas = new System.Windows.Forms.ComboBox();
            this.cmbAñosVentas = new System.Windows.Forms.ComboBox();
            this.pnlPorDiaVentas = new System.Windows.Forms.Panel();
            this.dtpFechaDiaVentas = new System.Windows.Forms.DateTimePicker();
            this.rdoPorFechasVentas = new System.Windows.Forms.RadioButton();
            this.rdoPorMesVentas = new System.Windows.Forms.RadioButton();
            this.rdoPorDiaVentas = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbAlmacenes = new System.Windows.Forms.ComboBox();
            this.tcReportesVentasPorProducto = new System.Windows.Forms.TabControl();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.chkSoloTotales = new System.Windows.Forms.CheckBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.rdoReportePorProductoGeneral = new System.Windows.Forms.RadioButton();
            this.rvVentasPorProductoTotales = new Microsoft.Reporting.WinForms.ReportViewer();
            this.rvVentasPorProducto = new Microsoft.Reporting.WinForms.ReportViewer();
            this.rvVentasPorProductoAlmacen = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tpClientesPorProducto = new System.Windows.Forms.TabPage();
            this.rvVentasClientesPorProducto = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tpProductosPorCliente = new System.Windows.Forms.TabPage();
            this.rvVentasProductosPorClientes = new Microsoft.Reporting.WinForms.ReportViewer();
            this.gbFiltrosVentas = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLimpiaFiltroProductoVentas = new System.Windows.Forms.Button();
            this.txtProductoDescripcionFiltroVentas = new System.Windows.Forms.TextBox();
            this.btnBuscaProductoVentas = new System.Windows.Forms.Button();
            this.txtProductoCodigoFiltroVentas = new System.Windows.Forms.TextBox();
            this.panel19 = new System.Windows.Forms.Panel();
            this.btnLimpiaFiltroClienteVentas = new System.Windows.Forms.Button();
            this.btnBuscaCliente = new System.Windows.Forms.Button();
            this.txtFiltroClientes = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.pnlFiltroPorTrabajador = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbTrabajadores = new System.Windows.Forms.ComboBox();
            this.entTrabajadorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel10 = new System.Windows.Forms.Panel();
            this.chkSoloDevoluciones = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel20 = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.cmbTipoRegistroVentas = new System.Windows.Forms.ComboBox();
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbClientesVentasPorCliente = new System.Windows.Forms.ComboBox();
            this.tpAnalitico = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdoPorFechasAnalitico = new System.Windows.Forms.RadioButton();
            this.pnlPorFechasAnalitico = new System.Windows.Forms.Panel();
            this.dtpFechaHastaAnalitico = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaDesdeAnalitico = new System.Windows.Forms.DateTimePicker();
            this.rdoPorMesAnalitico = new System.Windows.Forms.RadioButton();
            this.btnRefrescarAnalitico = new System.Windows.Forms.Button();
            this.pnlPorMesAnalitico = new System.Windows.Forms.Panel();
            this.cmbMesesAnalitico = new System.Windows.Forms.ComboBox();
            this.cmbAñosAnalitico = new System.Windows.Forms.ComboBox();
            this.pnlPorDiaAnalitico = new System.Windows.Forms.Panel();
            this.dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.tcReportesAnaliticos = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rvAnaliticoPorCostos = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.rvAnaliticoPorUnidad = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tpAuxiliar = new System.Windows.Forms.TabPage();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.cmbMesesAuxiliarHasta = new System.Windows.Forms.ComboBox();
            this.cmbAñosAuxiliarHasta = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbProductosHistorialPorProducto = new System.Windows.Forms.ComboBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.btnRefrescarAuxiliar = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.cmbMesesAuxiliar = new System.Windows.Forms.ComboBox();
            this.cmbAñosAuxiliar = new System.Windows.Forms.ComboBox();
            this.tcReportesAuxiliares = new System.Windows.Forms.TabControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.rvAuxiliarPorCostos = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.rvAuxiliarPorUnidad = new Microsoft.Reporting.WinForms.ReportViewer();
            this.entProductoBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.entPedidoBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.entClienteBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFiltroProductosVentas = new System.Windows.Forms.ComboBox();
            this.entClienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnBuscaEmpresa = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.cmbEmpresas = new System.Windows.Forms.ComboBox();
            this.entProductoBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.EntProductoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entPedidoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EntEmpresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EntDepositoBindingSource)).BeginInit();
            this.tcReportes.SuspendLayout();
            this.tpVentas.SuspendLayout();
            this.tcReportesVentas.SuspendLayout();
            this.tpVentasPorProducto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductosFiltro)).BeginInit();
            this.tcReportesGlobalesGeneral.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tpReporteAumento.SuspendLayout();
            this.flpFiltroPorProducto.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlPeriodoComparativo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entCatalogoGenericoBindingSource)).BeginInit();
            this.pnlPeriodoConsulta.SuspendLayout();
            this.pnlPorFechasVentas.SuspendLayout();
            this.pnlPorMesVentas.SuspendLayout();
            this.pnlPorDiaVentas.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tcReportesVentasPorProducto.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.tpClientesPorProducto.SuspendLayout();
            this.tpProductosPorCliente.SuspendLayout();
            this.gbFiltrosVentas.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel19.SuspendLayout();
            this.pnlFiltroPorTrabajador.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entTrabajadorBindingSource)).BeginInit();
            this.panel10.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel21.SuspendLayout();
            this.panel22.SuspendLayout();
            this.panel23.SuspendLayout();
            this.panel24.SuspendLayout();
            this.tpAnalitico.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlPorFechasAnalitico.SuspendLayout();
            this.pnlPorMesAnalitico.SuspendLayout();
            this.pnlPorDiaAnalitico.SuspendLayout();
            this.tcReportesAnaliticos.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tpAuxiliar.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.tcReportesAuxiliares.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entProductoBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entPedidoBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entClienteBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entClienteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entProductoBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // EntProductoBindingSource
            // 
            this.EntProductoBindingSource.DataSource = typeof(AiresEntidades.EntProducto);
            // 
            // entPedidoBindingSource
            // 
            this.entPedidoBindingSource.DataSource = typeof(AiresEntidades.EntPedido);
            // 
            // EntEmpresaBindingSource
            // 
            this.EntEmpresaBindingSource.DataSource = typeof(AiresEntidades.EntEmpresa);
            // 
            // EntDepositoBindingSource
            // 
            this.EntDepositoBindingSource.DataSource = typeof(AiresEntidades.EntDeposito);
            // 
            // tcReportes
            // 
            this.tcReportes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcReportes.Controls.Add(this.tpVentas);
            this.tcReportes.Controls.Add(this.tpAnalitico);
            this.tcReportes.Controls.Add(this.tpAuxiliar);
            this.tcReportes.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcReportes.Location = new System.Drawing.Point(12, 15);
            this.tcReportes.Name = "tcReportes";
            this.tcReportes.SelectedIndex = 0;
            this.tcReportes.Size = new System.Drawing.Size(1134, 642);
            this.tcReportes.TabIndex = 86;
            this.tcReportes.SelectedIndexChanged += new System.EventHandler(this.tcReportes_SelectedIndexChanged);
            // 
            // tpVentas
            // 
            this.tpVentas.Controls.Add(this.tcReportesVentas);
            this.tpVentas.Controls.Add(this.gbFiltrosVentas);
            this.tpVentas.Location = new System.Drawing.Point(4, 23);
            this.tpVentas.Name = "tpVentas";
            this.tpVentas.Padding = new System.Windows.Forms.Padding(3);
            this.tpVentas.Size = new System.Drawing.Size(1126, 615);
            this.tpVentas.TabIndex = 1;
            this.tpVentas.Text = "Ventas";
            this.tpVentas.UseVisualStyleBackColor = true;
            // 
            // tcReportesVentas
            // 
            this.tcReportesVentas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcReportesVentas.Controls.Add(this.tpVentasPorProducto);
            this.tcReportesVentas.Font = new System.Drawing.Font("Microsoft Tai Le", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcReportesVentas.Location = new System.Drawing.Point(5, 6);
            this.tcReportesVentas.Name = "tcReportesVentas";
            this.tcReportesVentas.SelectedIndex = 0;
            this.tcReportesVentas.Size = new System.Drawing.Size(1119, 607);
            this.tcReportesVentas.TabIndex = 45;
            this.tcReportesVentas.SelectedIndexChanged += new System.EventHandler(this.tcReportesVentas_SelectedIndexChanged);
            // 
            // tpVentasPorProducto
            // 
            this.tpVentasPorProducto.Controls.Add(this.chkSeleccionaTodos);
            this.tpVentasPorProducto.Controls.Add(this.btnFiltrarProductos);
            this.tpVentasPorProducto.Controls.Add(this.gvProductosFiltro);
            this.tpVentasPorProducto.Controls.Add(this.tcReportesGlobalesGeneral);
            this.tpVentasPorProducto.Controls.Add(this.flpFiltroPorProducto);
            this.tpVentasPorProducto.Location = new System.Drawing.Point(4, 22);
            this.tpVentasPorProducto.Margin = new System.Windows.Forms.Padding(2);
            this.tpVentasPorProducto.Name = "tpVentasPorProducto";
            this.tpVentasPorProducto.Padding = new System.Windows.Forms.Padding(2);
            this.tpVentasPorProducto.Size = new System.Drawing.Size(1111, 581);
            this.tpVentasPorProducto.TabIndex = 3;
            this.tpVentasPorProducto.Text = "Reportes";
            this.tpVentasPorProducto.UseVisualStyleBackColor = true;
            // 
            // chkSeleccionaTodos
            // 
            this.chkSeleccionaTodos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSeleccionaTodos.AutoSize = true;
            this.chkSeleccionaTodos.Checked = true;
            this.chkSeleccionaTodos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSeleccionaTodos.Location = new System.Drawing.Point(726, 180);
            this.chkSeleccionaTodos.Margin = new System.Windows.Forms.Padding(2);
            this.chkSeleccionaTodos.Name = "chkSeleccionaTodos";
            this.chkSeleccionaTodos.Size = new System.Drawing.Size(102, 17);
            this.chkSeleccionaTodos.TabIndex = 121;
            this.chkSeleccionaTodos.Text = "Selecciona Todos";
            this.chkSeleccionaTodos.UseVisualStyleBackColor = true;
            this.chkSeleccionaTodos.CheckedChanged += new System.EventHandler(this.chkSeleccionaTodos_CheckedChanged);
            // 
            // btnFiltrarProductos
            // 
            this.btnFiltrarProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFiltrarProductos.BackColor = System.Drawing.Color.White;
            this.btnFiltrarProductos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFiltrarProductos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltrarProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrarProductos.Image = global::Aires.Properties.Resources.Search;
            this.btnFiltrarProductos.Location = new System.Drawing.Point(838, 166);
            this.btnFiltrarProductos.Name = "btnFiltrarProductos";
            this.btnFiltrarProductos.Size = new System.Drawing.Size(114, 33);
            this.btnFiltrarProductos.TabIndex = 120;
            this.btnFiltrarProductos.Text = "Filtrar";
            this.btnFiltrarProductos.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnFiltrarProductos.UseVisualStyleBackColor = false;
            this.btnFiltrarProductos.Click += new System.EventHandler(this.btnFiltrarProductos_Click);
            // 
            // gvProductosFiltro
            // 
            this.gvProductosFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvProductosFiltro.AutoGenerateColumns = false;
            this.gvProductosFiltro.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvProductosFiltro.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvProductosFiltro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProductosFiltro.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Estatus,
            this.codigoDataGridViewTextBoxColumn,
            this.descripcionDataGridViewTextBoxColumn});
            this.gvProductosFiltro.DataSource = this.EntProductoBindingSource;
            this.gvProductosFiltro.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.gvProductosFiltro.Location = new System.Drawing.Point(725, 201);
            this.gvProductosFiltro.MultiSelect = false;
            this.gvProductosFiltro.Name = "gvProductosFiltro";
            this.gvProductosFiltro.RowHeadersVisible = false;
            this.gvProductosFiltro.RowHeadersWidth = 62;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvProductosFiltro.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvProductosFiltro.RowTemplate.Height = 30;
            this.gvProductosFiltro.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvProductosFiltro.Size = new System.Drawing.Size(386, 405);
            this.gvProductosFiltro.TabIndex = 119;
            this.gvProductosFiltro.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProductosFiltro_CellContentClick);
            this.gvProductosFiltro.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProductosFiltro_CellValueChanged);
            // 
            // Estatus
            // 
            this.Estatus.DataPropertyName = "Estatus";
            this.Estatus.FillWeight = 1F;
            this.Estatus.HeaderText = "Sel.";
            this.Estatus.MinimumWidth = 8;
            this.Estatus.Name = "Estatus";
            // 
            // codigoDataGridViewTextBoxColumn
            // 
            this.codigoDataGridViewTextBoxColumn.DataPropertyName = "Codigo";
            this.codigoDataGridViewTextBoxColumn.FillWeight = 2F;
            this.codigoDataGridViewTextBoxColumn.HeaderText = "Codigo";
            this.codigoDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.codigoDataGridViewTextBoxColumn.Name = "codigoDataGridViewTextBoxColumn";
            this.codigoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descripcionDataGridViewTextBoxColumn
            // 
            this.descripcionDataGridViewTextBoxColumn.DataPropertyName = "Descripcion";
            this.descripcionDataGridViewTextBoxColumn.FillWeight = 10F;
            this.descripcionDataGridViewTextBoxColumn.HeaderText = "Descripcion";
            this.descripcionDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.descripcionDataGridViewTextBoxColumn.Name = "descripcionDataGridViewTextBoxColumn";
            this.descripcionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tcReportesGlobalesGeneral
            // 
            this.tcReportesGlobalesGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcReportesGlobalesGeneral.Controls.Add(this.tabPage4);
            this.tcReportesGlobalesGeneral.Controls.Add(this.tpReporteAumento);
            this.tcReportesGlobalesGeneral.Location = new System.Drawing.Point(3, 153);
            this.tcReportesGlobalesGeneral.Margin = new System.Windows.Forms.Padding(2);
            this.tcReportesGlobalesGeneral.Name = "tcReportesGlobalesGeneral";
            this.tcReportesGlobalesGeneral.SelectedIndex = 0;
            this.tcReportesGlobalesGeneral.Size = new System.Drawing.Size(722, 457);
            this.tcReportesGlobalesGeneral.TabIndex = 118;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.rdoPorClienteGlobal);
            this.tabPage4.Controls.Add(this.rdoPorProductoGlobal);
            this.tabPage4.Controls.Add(this.rvVentasPorProductoGlobal);
            this.tabPage4.Controls.Add(this.rvVentasPorClienteGlobal);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage4.Size = new System.Drawing.Size(714, 431);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Reporte Global";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // rdoPorClienteGlobal
            // 
            this.rdoPorClienteGlobal.AutoSize = true;
            this.rdoPorClienteGlobal.Location = new System.Drawing.Point(129, 5);
            this.rdoPorClienteGlobal.Name = "rdoPorClienteGlobal";
            this.rdoPorClienteGlobal.Size = new System.Drawing.Size(72, 17);
            this.rdoPorClienteGlobal.TabIndex = 104;
            this.rdoPorClienteGlobal.Text = "Por Cliente";
            this.rdoPorClienteGlobal.UseVisualStyleBackColor = true;
            // 
            // rdoPorProductoGlobal
            // 
            this.rdoPorProductoGlobal.AutoSize = true;
            this.rdoPorProductoGlobal.Checked = true;
            this.rdoPorProductoGlobal.Location = new System.Drawing.Point(5, 5);
            this.rdoPorProductoGlobal.Name = "rdoPorProductoGlobal";
            this.rdoPorProductoGlobal.Size = new System.Drawing.Size(84, 17);
            this.rdoPorProductoGlobal.TabIndex = 103;
            this.rdoPorProductoGlobal.TabStop = true;
            this.rdoPorProductoGlobal.Text = "Por Producto";
            this.rdoPorProductoGlobal.UseVisualStyleBackColor = true;
            this.rdoPorProductoGlobal.CheckedChanged += new System.EventHandler(this.rdoPorProductoGlobal_CheckedChanged);
            // 
            // rvVentasPorProductoGlobal
            // 
            this.rvVentasPorProductoGlobal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rvVentasPorProductoGlobal.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.rvVentasPorProductoGlobal.LocalReport.ReportEmbeddedResource = "Aires.Reportes.rptVentasGlobalesTotales.rdlc";
            this.rvVentasPorProductoGlobal.Location = new System.Drawing.Point(3, 26);
            this.rvVentasPorProductoGlobal.Margin = new System.Windows.Forms.Padding(2);
            this.rvVentasPorProductoGlobal.Name = "rvVentasPorProductoGlobal";
            this.rvVentasPorProductoGlobal.ServerReport.BearerToken = null;
            this.rvVentasPorProductoGlobal.Size = new System.Drawing.Size(708, 404);
            this.rvVentasPorProductoGlobal.TabIndex = 102;
            // 
            // rvVentasPorClienteGlobal
            // 
            this.rvVentasPorClienteGlobal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rvVentasPorClienteGlobal.BackColor = System.Drawing.Color.SteelBlue;
            this.rvVentasPorClienteGlobal.LocalReport.ReportEmbeddedResource = "Aires.Reportes.rptVentasGlobalesTotalesPorClientes.rdlc";
            this.rvVentasPorClienteGlobal.Location = new System.Drawing.Point(3, 26);
            this.rvVentasPorClienteGlobal.Margin = new System.Windows.Forms.Padding(2);
            this.rvVentasPorClienteGlobal.Name = "rvVentasPorClienteGlobal";
            this.rvVentasPorClienteGlobal.ServerReport.BearerToken = null;
            this.rvVentasPorClienteGlobal.Size = new System.Drawing.Size(708, 404);
            this.rvVentasPorClienteGlobal.TabIndex = 105;
            // 
            // tpReporteAumento
            // 
            this.tpReporteAumento.Controls.Add(this.chkCodigosBajos);
            this.tpReporteAumento.Controls.Add(this.rvVentasPorProductoAlmacenGlobal);
            this.tpReporteAumento.Location = new System.Drawing.Point(4, 22);
            this.tpReporteAumento.Margin = new System.Windows.Forms.Padding(2);
            this.tpReporteAumento.Name = "tpReporteAumento";
            this.tpReporteAumento.Padding = new System.Windows.Forms.Padding(2);
            this.tpReporteAumento.Size = new System.Drawing.Size(714, 431);
            this.tpReporteAumento.TabIndex = 1;
            this.tpReporteAumento.Text = "Aumento Por Código";
            this.tpReporteAumento.UseVisualStyleBackColor = true;
            // 
            // chkCodigosBajos
            // 
            this.chkCodigosBajos.AutoSize = true;
            this.chkCodigosBajos.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCodigosBajos.Location = new System.Drawing.Point(4, 6);
            this.chkCodigosBajos.Margin = new System.Windows.Forms.Padding(2);
            this.chkCodigosBajos.Name = "chkCodigosBajos";
            this.chkCodigosBajos.Size = new System.Drawing.Size(102, 20);
            this.chkCodigosBajos.TabIndex = 122;
            this.chkCodigosBajos.Text = "Códigos Bajos";
            this.chkCodigosBajos.UseVisualStyleBackColor = true;
            this.chkCodigosBajos.CheckedChanged += new System.EventHandler(this.btnFiltrarProductos_Click);
            // 
            // rvVentasPorProductoAlmacenGlobal
            // 
            this.rvVentasPorProductoAlmacenGlobal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rvVentasPorProductoAlmacenGlobal.BackColor = System.Drawing.Color.MediumAquamarine;
            this.rvVentasPorProductoAlmacenGlobal.LocalReport.ReportEmbeddedResource = "Aires.Reportes.rptVentasGlobalesTotalesPorSucursal.rdlc";
            this.rvVentasPorProductoAlmacenGlobal.Location = new System.Drawing.Point(2, 28);
            this.rvVentasPorProductoAlmacenGlobal.Margin = new System.Windows.Forms.Padding(2);
            this.rvVentasPorProductoAlmacenGlobal.Name = "rvVentasPorProductoAlmacenGlobal";
            this.rvVentasPorProductoAlmacenGlobal.ServerReport.BearerToken = null;
            this.rvVentasPorProductoAlmacenGlobal.Size = new System.Drawing.Size(708, 402);
            this.rvVentasPorProductoAlmacenGlobal.TabIndex = 103;
            // 
            // flpFiltroPorProducto
            // 
            this.flpFiltroPorProducto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpFiltroPorProducto.Controls.Add(this.panel3);
            this.flpFiltroPorProducto.Controls.Add(this.pnlPeriodoConsulta);
            this.flpFiltroPorProducto.Controls.Add(this.panel4);
            this.flpFiltroPorProducto.Controls.Add(this.tcReportesVentasPorProducto);
            this.flpFiltroPorProducto.Location = new System.Drawing.Point(2, 0);
            this.flpFiltroPorProducto.Margin = new System.Windows.Forms.Padding(2);
            this.flpFiltroPorProducto.Name = "flpFiltroPorProducto";
            this.flpFiltroPorProducto.Size = new System.Drawing.Size(1425, 149);
            this.flpFiltroPorProducto.TabIndex = 116;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkSinComparativo);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.pnlPeriodoComparativo);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(177, 84);
            this.panel3.TabIndex = 118;
            // 
            // chkSinComparativo
            // 
            this.chkSinComparativo.AutoSize = true;
            this.chkSinComparativo.Location = new System.Drawing.Point(76, 59);
            this.chkSinComparativo.Name = "chkSinComparativo";
            this.chkSinComparativo.Size = new System.Drawing.Size(98, 17);
            this.chkSinComparativo.TabIndex = 43;
            this.chkSinComparativo.Text = "Sin Comparativo";
            this.chkSinComparativo.UseVisualStyleBackColor = true;
            this.chkSinComparativo.CheckedChanged += new System.EventHandler(this.chkSinComparativo_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Periodo Comparativo";
            // 
            // pnlPeriodoComparativo
            // 
            this.pnlPeriodoComparativo.Controls.Add(this.cmbPeriodoVentas);
            this.pnlPeriodoComparativo.Location = new System.Drawing.Point(3, 21);
            this.pnlPeriodoComparativo.Name = "pnlPeriodoComparativo";
            this.pnlPeriodoComparativo.Size = new System.Drawing.Size(171, 34);
            this.pnlPeriodoComparativo.TabIndex = 42;
            // 
            // cmbPeriodoVentas
            // 
            this.cmbPeriodoVentas.DataSource = this.entCatalogoGenericoBindingSource;
            this.cmbPeriodoVentas.DisplayMember = "Descripcion";
            this.cmbPeriodoVentas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriodoVentas.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbPeriodoVentas.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPeriodoVentas.FormattingEnabled = true;
            this.cmbPeriodoVentas.Location = new System.Drawing.Point(3, 4);
            this.cmbPeriodoVentas.Name = "cmbPeriodoVentas";
            this.cmbPeriodoVentas.Size = new System.Drawing.Size(165, 24);
            this.cmbPeriodoVentas.TabIndex = 19;
            this.cmbPeriodoVentas.ValueMember = "Id";
            // 
            // entCatalogoGenericoBindingSource
            // 
            this.entCatalogoGenericoBindingSource.DataSource = typeof(AiresEntidades.EntCatalogoGenerico);
            // 
            // pnlPeriodoConsulta
            // 
            this.pnlPeriodoConsulta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPeriodoConsulta.Controls.Add(this.label6);
            this.pnlPeriodoConsulta.Controls.Add(this.pnlPorFechasVentas);
            this.pnlPeriodoConsulta.Controls.Add(this.btnRefrescarVentas);
            this.pnlPeriodoConsulta.Controls.Add(this.pnlPorMesVentas);
            this.pnlPeriodoConsulta.Controls.Add(this.pnlPorDiaVentas);
            this.pnlPeriodoConsulta.Controls.Add(this.rdoPorFechasVentas);
            this.pnlPeriodoConsulta.Controls.Add(this.rdoPorMesVentas);
            this.pnlPeriodoConsulta.Controls.Add(this.rdoPorDiaVentas);
            this.pnlPeriodoConsulta.Location = new System.Drawing.Point(186, 3);
            this.pnlPeriodoConsulta.Name = "pnlPeriodoConsulta";
            this.pnlPeriodoConsulta.Size = new System.Drawing.Size(438, 145);
            this.pnlPeriodoConsulta.TabIndex = 114;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(84, 5);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 118;
            this.label6.Text = "Periodo a Consultar";
            // 
            // pnlPorFechasVentas
            // 
            this.pnlPorFechasVentas.Controls.Add(this.dtpFechaHastaVentas);
            this.pnlPorFechasVentas.Controls.Add(this.dtpFechaDesdeVentas);
            this.pnlPorFechasVentas.Enabled = false;
            this.pnlPorFechasVentas.Location = new System.Drawing.Point(82, 57);
            this.pnlPorFechasVentas.Name = "pnlPorFechasVentas";
            this.pnlPorFechasVentas.Size = new System.Drawing.Size(243, 61);
            this.pnlPorFechasVentas.TabIndex = 116;
            // 
            // dtpFechaHastaVentas
            // 
            this.dtpFechaHastaVentas.Location = new System.Drawing.Point(5, 33);
            this.dtpFechaHastaVentas.Name = "dtpFechaHastaVentas";
            this.dtpFechaHastaVentas.Size = new System.Drawing.Size(224, 20);
            this.dtpFechaHastaVentas.TabIndex = 15;
            // 
            // dtpFechaDesdeVentas
            // 
            this.dtpFechaDesdeVentas.Location = new System.Drawing.Point(5, 7);
            this.dtpFechaDesdeVentas.Name = "dtpFechaDesdeVentas";
            this.dtpFechaDesdeVentas.Size = new System.Drawing.Size(224, 20);
            this.dtpFechaDesdeVentas.TabIndex = 15;
            // 
            // btnRefrescarVentas
            // 
            this.btnRefrescarVentas.BackColor = System.Drawing.Color.White;
            this.btnRefrescarVentas.BackgroundImage = global::Aires.Properties.Resources.RefrescarChicoSinFondo;
            this.btnRefrescarVentas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRefrescarVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescarVentas.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescarVentas.Location = new System.Drawing.Point(343, 6);
            this.btnRefrescarVentas.Name = "btnRefrescarVentas";
            this.btnRefrescarVentas.Size = new System.Drawing.Size(77, 67);
            this.btnRefrescarVentas.TabIndex = 113;
            this.btnRefrescarVentas.Text = "Refrescar";
            this.btnRefrescarVentas.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefrescarVentas.UseVisualStyleBackColor = false;
            this.btnRefrescarVentas.Click += new System.EventHandler(this.btnRefrescarVentas_Click);
            // 
            // pnlPorMesVentas
            // 
            this.pnlPorMesVentas.Controls.Add(this.cmbMesesVentas);
            this.pnlPorMesVentas.Controls.Add(this.cmbAñosVentas);
            this.pnlPorMesVentas.Location = new System.Drawing.Point(82, 20);
            this.pnlPorMesVentas.Name = "pnlPorMesVentas";
            this.pnlPorMesVentas.Size = new System.Drawing.Size(243, 34);
            this.pnlPorMesVentas.TabIndex = 41;
            // 
            // cmbMesesVentas
            // 
            this.cmbMesesVentas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMesesVentas.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbMesesVentas.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.cmbMesesVentas.Size = new System.Drawing.Size(126, 24);
            this.cmbMesesVentas.TabIndex = 19;
            // 
            // cmbAñosVentas
            // 
            this.cmbAñosVentas.DataSource = this.entCatalogoGenericoBindingSource;
            this.cmbAñosVentas.DisplayMember = "Descripcion";
            this.cmbAñosVentas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAñosVentas.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbAñosVentas.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAñosVentas.FormattingEnabled = true;
            this.cmbAñosVentas.Location = new System.Drawing.Point(152, 8);
            this.cmbAñosVentas.Name = "cmbAñosVentas";
            this.cmbAñosVentas.Size = new System.Drawing.Size(78, 24);
            this.cmbAñosVentas.TabIndex = 20;
            this.cmbAñosVentas.ValueMember = "Descripcion";
            // 
            // pnlPorDiaVentas
            // 
            this.pnlPorDiaVentas.Controls.Add(this.dtpFechaDiaVentas);
            this.pnlPorDiaVentas.Enabled = false;
            this.pnlPorDiaVentas.Location = new System.Drawing.Point(82, 114);
            this.pnlPorDiaVentas.Name = "pnlPorDiaVentas";
            this.pnlPorDiaVentas.Size = new System.Drawing.Size(243, 31);
            this.pnlPorDiaVentas.TabIndex = 42;
            // 
            // dtpFechaDiaVentas
            // 
            this.dtpFechaDiaVentas.Location = new System.Drawing.Point(5, 7);
            this.dtpFechaDiaVentas.Name = "dtpFechaDiaVentas";
            this.dtpFechaDiaVentas.Size = new System.Drawing.Size(224, 20);
            this.dtpFechaDiaVentas.TabIndex = 15;
            // 
            // rdoPorFechasVentas
            // 
            this.rdoPorFechasVentas.AutoSize = true;
            this.rdoPorFechasVentas.Location = new System.Drawing.Point(10, 67);
            this.rdoPorFechasVentas.Name = "rdoPorFechasVentas";
            this.rdoPorFechasVentas.Size = new System.Drawing.Size(73, 17);
            this.rdoPorFechasVentas.TabIndex = 117;
            this.rdoPorFechasVentas.Text = "Por Fechas";
            this.rdoPorFechasVentas.UseVisualStyleBackColor = true;
            this.rdoPorFechasVentas.CheckedChanged += new System.EventHandler(this.rdoPorFechasVentas_CheckedChanged);
            // 
            // rdoPorMesVentas
            // 
            this.rdoPorMesVentas.AutoSize = true;
            this.rdoPorMesVentas.Checked = true;
            this.rdoPorMesVentas.Location = new System.Drawing.Point(10, 31);
            this.rdoPorMesVentas.Name = "rdoPorMesVentas";
            this.rdoPorMesVentas.Size = new System.Drawing.Size(61, 17);
            this.rdoPorMesVentas.TabIndex = 44;
            this.rdoPorMesVentas.TabStop = true;
            this.rdoPorMesVentas.Text = "Por Mes";
            this.rdoPorMesVentas.UseVisualStyleBackColor = true;
            this.rdoPorMesVentas.CheckedChanged += new System.EventHandler(this.rdoPorMesVentas_CheckedChanged);
            // 
            // rdoPorDiaVentas
            // 
            this.rdoPorDiaVentas.AutoSize = true;
            this.rdoPorDiaVentas.Location = new System.Drawing.Point(10, 123);
            this.rdoPorDiaVentas.Name = "rdoPorDiaVentas";
            this.rdoPorDiaVentas.Size = new System.Drawing.Size(57, 17);
            this.rdoPorDiaVentas.TabIndex = 43;
            this.rdoPorDiaVentas.Text = "Por Día";
            this.rdoPorDiaVentas.UseVisualStyleBackColor = true;
            this.rdoPorDiaVentas.CheckedChanged += new System.EventHandler(this.rdoPorDiaVentas_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.cmbAlmacenes);
            this.panel4.Location = new System.Drawing.Point(630, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(281, 110);
            this.panel4.TabIndex = 123;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 121;
            this.label5.Text = "Almacen:";
            // 
            // cmbAlmacenes
            // 
            this.cmbAlmacenes.DataSource = this.entCatalogoGenericoBindingSource;
            this.cmbAlmacenes.DisplayMember = "Descripcion";
            this.cmbAlmacenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlmacenes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbAlmacenes.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAlmacenes.FormattingEnabled = true;
            this.cmbAlmacenes.Location = new System.Drawing.Point(4, 28);
            this.cmbAlmacenes.Name = "cmbAlmacenes";
            this.cmbAlmacenes.Size = new System.Drawing.Size(273, 24);
            this.cmbAlmacenes.TabIndex = 122;
            this.cmbAlmacenes.ValueMember = "Id";
            // 
            // tcReportesVentasPorProducto
            // 
            this.tcReportesVentasPorProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcReportesVentasPorProducto.Controls.Add(this.tabPage9);
            this.tcReportesVentasPorProducto.Controls.Add(this.tpClientesPorProducto);
            this.tcReportesVentasPorProducto.Controls.Add(this.tpProductosPorCliente);
            this.tcReportesVentasPorProducto.Location = new System.Drawing.Point(916, 2);
            this.tcReportesVentasPorProducto.Margin = new System.Windows.Forms.Padding(2);
            this.tcReportesVentasPorProducto.Name = "tcReportesVentasPorProducto";
            this.tcReportesVentasPorProducto.SelectedIndex = 0;
            this.tcReportesVentasPorProducto.Size = new System.Drawing.Size(250, 147);
            this.tcReportesVentasPorProducto.TabIndex = 117;
            this.tcReportesVentasPorProducto.Visible = false;
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.chkSoloTotales);
            this.tabPage9.Controls.Add(this.radioButton4);
            this.tabPage9.Controls.Add(this.rdoReportePorProductoGeneral);
            this.tabPage9.Controls.Add(this.rvVentasPorProductoTotales);
            this.tabPage9.Controls.Add(this.rvVentasPorProducto);
            this.tabPage9.Controls.Add(this.rvVentasPorProductoAlmacen);
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage9.Size = new System.Drawing.Size(242, 121);
            this.tabPage9.TabIndex = 0;
            this.tabPage9.Text = "Productos";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // chkSoloTotales
            // 
            this.chkSoloTotales.AutoSize = true;
            this.chkSoloTotales.Location = new System.Drawing.Point(228, 4);
            this.chkSoloTotales.Name = "chkSoloTotales";
            this.chkSoloTotales.Size = new System.Drawing.Size(78, 17);
            this.chkSoloTotales.TabIndex = 101;
            this.chkSoloTotales.Text = "Solo Totales";
            this.chkSoloTotales.UseVisualStyleBackColor = true;
            this.chkSoloTotales.CheckedChanged += new System.EventHandler(this.rdoReportePorProductoGeneral_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Checked = true;
            this.radioButton4.Location = new System.Drawing.Point(102, 3);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(81, 17);
            this.radioButton4.TabIndex = 99;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Por Almacén";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // rdoReportePorProductoGeneral
            // 
            this.rdoReportePorProductoGeneral.AutoSize = true;
            this.rdoReportePorProductoGeneral.Location = new System.Drawing.Point(5, 3);
            this.rdoReportePorProductoGeneral.Name = "rdoReportePorProductoGeneral";
            this.rdoReportePorProductoGeneral.Size = new System.Drawing.Size(58, 17);
            this.rdoReportePorProductoGeneral.TabIndex = 98;
            this.rdoReportePorProductoGeneral.Text = "General";
            this.rdoReportePorProductoGeneral.UseVisualStyleBackColor = true;
            this.rdoReportePorProductoGeneral.CheckedChanged += new System.EventHandler(this.rdoReportePorProductoGeneral_CheckedChanged);
            // 
            // rvVentasPorProductoTotales
            // 
            this.rvVentasPorProductoTotales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rvVentasPorProductoTotales.LocalReport.ReportEmbeddedResource = "Aires.Reportes.rptVentasPorProductosNeueTotales.rdlc";
            this.rvVentasPorProductoTotales.Location = new System.Drawing.Point(0, 23);
            this.rvVentasPorProductoTotales.Margin = new System.Windows.Forms.Padding(2);
            this.rvVentasPorProductoTotales.Name = "rvVentasPorProductoTotales";
            this.rvVentasPorProductoTotales.ServerReport.BearerToken = null;
            this.rvVentasPorProductoTotales.Size = new System.Drawing.Size(244, 91);
            this.rvVentasPorProductoTotales.TabIndex = 103;
            // 
            // rvVentasPorProducto
            // 
            this.rvVentasPorProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rvVentasPorProducto.LocalReport.ReportEmbeddedResource = "Aires.Reportes.rptVentasPorProductosNeue.rdlc";
            this.rvVentasPorProducto.Location = new System.Drawing.Point(0, 23);
            this.rvVentasPorProducto.Margin = new System.Windows.Forms.Padding(2);
            this.rvVentasPorProducto.Name = "rvVentasPorProducto";
            this.rvVentasPorProducto.ServerReport.BearerToken = null;
            this.rvVentasPorProducto.Size = new System.Drawing.Size(244, 91);
            this.rvVentasPorProducto.TabIndex = 97;
            // 
            // rvVentasPorProductoAlmacen
            // 
            this.rvVentasPorProductoAlmacen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rvVentasPorProductoAlmacen.LocalReport.ReportEmbeddedResource = "Aires.Reportes.rptVentasPorAlmacenesProductos.rdlc";
            this.rvVentasPorProductoAlmacen.Location = new System.Drawing.Point(0, 23);
            this.rvVentasPorProductoAlmacen.Margin = new System.Windows.Forms.Padding(2);
            this.rvVentasPorProductoAlmacen.Name = "rvVentasPorProductoAlmacen";
            this.rvVentasPorProductoAlmacen.ServerReport.BearerToken = null;
            this.rvVentasPorProductoAlmacen.Size = new System.Drawing.Size(244, 91);
            this.rvVentasPorProductoAlmacen.TabIndex = 100;
            // 
            // tpClientesPorProducto
            // 
            this.tpClientesPorProducto.Controls.Add(this.rvVentasClientesPorProducto);
            this.tpClientesPorProducto.Location = new System.Drawing.Point(4, 22);
            this.tpClientesPorProducto.Margin = new System.Windows.Forms.Padding(2);
            this.tpClientesPorProducto.Name = "tpClientesPorProducto";
            this.tpClientesPorProducto.Padding = new System.Windows.Forms.Padding(2);
            this.tpClientesPorProducto.Size = new System.Drawing.Size(242, 121);
            this.tpClientesPorProducto.TabIndex = 1;
            this.tpClientesPorProducto.Text = "Clientes Por Producto";
            this.tpClientesPorProducto.UseVisualStyleBackColor = true;
            // 
            // rvVentasClientesPorProducto
            // 
            this.rvVentasClientesPorProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rvVentasClientesPorProducto.DocumentMapWidth = 1;
            this.rvVentasClientesPorProducto.LocalReport.ReportEmbeddedResource = "Aires.Reportes.rptVentasPorProductosNeueClientes.rdlc";
            this.rvVentasClientesPorProducto.Location = new System.Drawing.Point(0, 0);
            this.rvVentasClientesPorProducto.Margin = new System.Windows.Forms.Padding(2);
            this.rvVentasClientesPorProducto.Name = "rvVentasClientesPorProducto";
            this.rvVentasClientesPorProducto.ServerReport.BearerToken = null;
            this.rvVentasClientesPorProducto.Size = new System.Drawing.Size(114, 111);
            this.rvVentasClientesPorProducto.TabIndex = 98;
            // 
            // tpProductosPorCliente
            // 
            this.tpProductosPorCliente.Controls.Add(this.rvVentasProductosPorClientes);
            this.tpProductosPorCliente.Location = new System.Drawing.Point(4, 22);
            this.tpProductosPorCliente.Margin = new System.Windows.Forms.Padding(2);
            this.tpProductosPorCliente.Name = "tpProductosPorCliente";
            this.tpProductosPorCliente.Padding = new System.Windows.Forms.Padding(2);
            this.tpProductosPorCliente.Size = new System.Drawing.Size(242, 121);
            this.tpProductosPorCliente.TabIndex = 2;
            this.tpProductosPorCliente.Text = "Productos Por Cliente";
            this.tpProductosPorCliente.UseVisualStyleBackColor = true;
            // 
            // rvVentasProductosPorClientes
            // 
            this.rvVentasProductosPorClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rvVentasProductosPorClientes.DocumentMapWidth = 1;
            this.rvVentasProductosPorClientes.LocalReport.ReportEmbeddedResource = "Aires.Reportes.rptVentasPorClientesProductosNeue.rdlc";
            this.rvVentasProductosPorClientes.Location = new System.Drawing.Point(0, 0);
            this.rvVentasProductosPorClientes.Margin = new System.Windows.Forms.Padding(2);
            this.rvVentasProductosPorClientes.Name = "rvVentasProductosPorClientes";
            this.rvVentasProductosPorClientes.ServerReport.BearerToken = null;
            this.rvVentasProductosPorClientes.Size = new System.Drawing.Size(114, 111);
            this.rvVentasProductosPorClientes.TabIndex = 99;
            // 
            // gbFiltrosVentas
            // 
            this.gbFiltrosVentas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFiltrosVentas.Controls.Add(this.flowLayoutPanel4);
            this.gbFiltrosVentas.Controls.Add(this.label1);
            this.gbFiltrosVentas.Controls.Add(this.cmbClientesVentasPorCliente);
            this.gbFiltrosVentas.Location = new System.Drawing.Point(5, 5);
            this.gbFiltrosVentas.Margin = new System.Windows.Forms.Padding(2);
            this.gbFiltrosVentas.Name = "gbFiltrosVentas";
            this.gbFiltrosVentas.Padding = new System.Windows.Forms.Padding(2);
            this.gbFiltrosVentas.Size = new System.Drawing.Size(1278, 60);
            this.gbFiltrosVentas.TabIndex = 157;
            this.gbFiltrosVentas.TabStop = false;
            this.gbFiltrosVentas.Text = "Filtros";
            this.gbFiltrosVentas.Visible = false;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel4.Controls.Add(this.groupBox1);
            this.flowLayoutPanel4.Controls.Add(this.panel19);
            this.flowLayoutPanel4.Controls.Add(this.pnlFiltroPorTrabajador);
            this.flowLayoutPanel4.Controls.Add(this.panel10);
            this.flowLayoutPanel4.Controls.Add(this.panel20);
            this.flowLayoutPanel4.Controls.Add(this.panel21);
            this.flowLayoutPanel4.Controls.Add(this.panel22);
            this.flowLayoutPanel4.Controls.Add(this.panel23);
            this.flowLayoutPanel4.Controls.Add(this.panel24);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(4, 8);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(1502, 50);
            this.flowLayoutPanel4.TabIndex = 158;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLimpiaFiltroProductoVentas);
            this.groupBox1.Controls.Add(this.txtProductoDescripcionFiltroVentas);
            this.groupBox1.Controls.Add(this.btnBuscaProductoVentas);
            this.groupBox1.Controls.Add(this.txtProductoCodigoFiltroVentas);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(502, 45);
            this.groupBox1.TabIndex = 115;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Producto";
            // 
            // btnLimpiaFiltroProductoVentas
            // 
            this.btnLimpiaFiltroProductoVentas.BackColor = System.Drawing.Color.White;
            this.btnLimpiaFiltroProductoVentas.BackgroundImage = global::Aires.Properties.Resources.borrar_white;
            this.btnLimpiaFiltroProductoVentas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLimpiaFiltroProductoVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiaFiltroProductoVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiaFiltroProductoVentas.Location = new System.Drawing.Point(465, 11);
            this.btnLimpiaFiltroProductoVentas.Name = "btnLimpiaFiltroProductoVentas";
            this.btnLimpiaFiltroProductoVentas.Size = new System.Drawing.Size(34, 32);
            this.btnLimpiaFiltroProductoVentas.TabIndex = 17;
            this.btnLimpiaFiltroProductoVentas.UseVisualStyleBackColor = false;
            this.btnLimpiaFiltroProductoVentas.Click += new System.EventHandler(this.btnLimpiaFiltroProducto_Click);
            // 
            // txtProductoDescripcionFiltroVentas
            // 
            this.txtProductoDescripcionFiltroVentas.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductoDescripcionFiltroVentas.Location = new System.Drawing.Point(86, 13);
            this.txtProductoDescripcionFiltroVentas.Name = "txtProductoDescripcionFiltroVentas";
            this.txtProductoDescripcionFiltroVentas.ReadOnly = true;
            this.txtProductoDescripcionFiltroVentas.Size = new System.Drawing.Size(330, 29);
            this.txtProductoDescripcionFiltroVentas.TabIndex = 15;
            // 
            // btnBuscaProductoVentas
            // 
            this.btnBuscaProductoVentas.BackColor = System.Drawing.Color.White;
            this.btnBuscaProductoVentas.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscaProductoVentas.BackgroundImage")));
            this.btnBuscaProductoVentas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBuscaProductoVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscaProductoVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscaProductoVentas.Location = new System.Drawing.Point(417, 11);
            this.btnBuscaProductoVentas.Name = "btnBuscaProductoVentas";
            this.btnBuscaProductoVentas.Size = new System.Drawing.Size(45, 32);
            this.btnBuscaProductoVentas.TabIndex = 16;
            this.btnBuscaProductoVentas.UseVisualStyleBackColor = false;
            this.btnBuscaProductoVentas.Click += new System.EventHandler(this.btnBuscaProductoVentas_Click);
            // 
            // txtProductoCodigoFiltroVentas
            // 
            this.txtProductoCodigoFiltroVentas.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductoCodigoFiltroVentas.Location = new System.Drawing.Point(6, 13);
            this.txtProductoCodigoFiltroVentas.Name = "txtProductoCodigoFiltroVentas";
            this.txtProductoCodigoFiltroVentas.Size = new System.Drawing.Size(78, 29);
            this.txtProductoCodigoFiltroVentas.TabIndex = 14;
            this.txtProductoCodigoFiltroVentas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProductoCodigoFiltroVentas_KeyPress);
            // 
            // panel19
            // 
            this.panel19.Controls.Add(this.btnLimpiaFiltroClienteVentas);
            this.panel19.Controls.Add(this.btnBuscaCliente);
            this.panel19.Controls.Add(this.txtFiltroClientes);
            this.panel19.Controls.Add(this.label23);
            this.panel19.Location = new System.Drawing.Point(508, 2);
            this.panel19.Margin = new System.Windows.Forms.Padding(2);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(332, 42);
            this.panel19.TabIndex = 160;
            // 
            // btnLimpiaFiltroClienteVentas
            // 
            this.btnLimpiaFiltroClienteVentas.BackColor = System.Drawing.Color.White;
            this.btnLimpiaFiltroClienteVentas.BackgroundImage = global::Aires.Properties.Resources.borrar_white;
            this.btnLimpiaFiltroClienteVentas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLimpiaFiltroClienteVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiaFiltroClienteVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiaFiltroClienteVentas.Location = new System.Drawing.Point(296, 8);
            this.btnLimpiaFiltroClienteVentas.Name = "btnLimpiaFiltroClienteVentas";
            this.btnLimpiaFiltroClienteVentas.Size = new System.Drawing.Size(34, 32);
            this.btnLimpiaFiltroClienteVentas.TabIndex = 155;
            this.btnLimpiaFiltroClienteVentas.UseVisualStyleBackColor = false;
            this.btnLimpiaFiltroClienteVentas.Click += new System.EventHandler(this.btnLimpiaFiltroClienteVentas_Click);
            // 
            // btnBuscaCliente
            // 
            this.btnBuscaCliente.BackColor = System.Drawing.Color.White;
            this.btnBuscaCliente.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscaCliente.BackgroundImage")));
            this.btnBuscaCliente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBuscaCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscaCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscaCliente.Location = new System.Drawing.Point(248, 8);
            this.btnBuscaCliente.Name = "btnBuscaCliente";
            this.btnBuscaCliente.Size = new System.Drawing.Size(45, 32);
            this.btnBuscaCliente.TabIndex = 154;
            this.btnBuscaCliente.UseVisualStyleBackColor = false;
            this.btnBuscaCliente.Click += new System.EventHandler(this.btnBuscaCliente_Click);
            // 
            // txtFiltroClientes
            // 
            this.txtFiltroClientes.Font = new System.Drawing.Font("Microsoft Tai Le", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltroClientes.Location = new System.Drawing.Point(4, 18);
            this.txtFiltroClientes.Name = "txtFiltroClientes";
            this.txtFiltroClientes.Size = new System.Drawing.Size(241, 21);
            this.txtFiltroClientes.TabIndex = 153;
            this.txtFiltroClientes.TextChanged += new System.EventHandler(this.txtFiltroClientes_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(3, 2);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(46, 14);
            this.label23.TabIndex = 152;
            this.label23.Text = "Cliente:";
            // 
            // pnlFiltroPorTrabajador
            // 
            this.pnlFiltroPorTrabajador.Controls.Add(this.label8);
            this.pnlFiltroPorTrabajador.Controls.Add(this.cmbTrabajadores);
            this.pnlFiltroPorTrabajador.Location = new System.Drawing.Point(844, 2);
            this.pnlFiltroPorTrabajador.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFiltroPorTrabajador.Name = "pnlFiltroPorTrabajador";
            this.pnlFiltroPorTrabajador.Size = new System.Drawing.Size(185, 42);
            this.pnlFiltroPorTrabajador.TabIndex = 161;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Location = new System.Drawing.Point(3, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 14);
            this.label8.TabIndex = 158;
            this.label8.Text = "Trabajador:";
            // 
            // cmbTrabajadores
            // 
            this.cmbTrabajadores.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTrabajadores.DataSource = this.entTrabajadorBindingSource;
            this.cmbTrabajadores.DisplayMember = "Nombre";
            this.cmbTrabajadores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrabajadores.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbTrabajadores.FormattingEnabled = true;
            this.cmbTrabajadores.Location = new System.Drawing.Point(2, 17);
            this.cmbTrabajadores.Margin = new System.Windows.Forms.Padding(2);
            this.cmbTrabajadores.Name = "cmbTrabajadores";
            this.cmbTrabajadores.Size = new System.Drawing.Size(182, 22);
            this.cmbTrabajadores.TabIndex = 157;
            this.cmbTrabajadores.ValueMember = "Id";
            this.cmbTrabajadores.SelectedIndexChanged += new System.EventHandler(this.cmbTrabajadores_SelectedIndexChanged);
            // 
            // entTrabajadorBindingSource
            // 
            this.entTrabajadorBindingSource.DataSource = typeof(AiresEntidades.EntTrabajador);
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.chkSoloDevoluciones);
            this.panel10.Controls.Add(this.label7);
            this.panel10.Location = new System.Drawing.Point(1033, 2);
            this.panel10.Margin = new System.Windows.Forms.Padding(2);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(135, 42);
            this.panel10.TabIndex = 162;
            // 
            // chkSoloDevoluciones
            // 
            this.chkSoloDevoluciones.AutoSize = true;
            this.chkSoloDevoluciones.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSoloDevoluciones.Location = new System.Drawing.Point(6, 20);
            this.chkSoloDevoluciones.Name = "chkSoloDevoluciones";
            this.chkSoloDevoluciones.Size = new System.Drawing.Size(122, 18);
            this.chkSoloDevoluciones.TabIndex = 159;
            this.chkSoloDevoluciones.Text = "Solo Devoluciones";
            this.chkSoloDevoluciones.UseVisualStyleBackColor = true;
            this.chkSoloDevoluciones.CheckedChanged += new System.EventHandler(this.btnRefrescarVentas_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(3, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 14);
            this.label7.TabIndex = 158;
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.label24);
            this.panel20.Controls.Add(this.cmbTipoRegistroVentas);
            this.panel20.Location = new System.Drawing.Point(1172, 2);
            this.panel20.Margin = new System.Windows.Forms.Padding(2);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(135, 42);
            this.panel20.TabIndex = 161;
            this.panel20.Visible = false;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label24.Location = new System.Drawing.Point(3, 2);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(32, 14);
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
            this.cmbTipoRegistroVentas.Location = new System.Drawing.Point(2, 17);
            this.cmbTipoRegistroVentas.Margin = new System.Windows.Forms.Padding(2);
            this.cmbTipoRegistroVentas.Name = "cmbTipoRegistroVentas";
            this.cmbTipoRegistroVentas.Size = new System.Drawing.Size(132, 22);
            this.cmbTipoRegistroVentas.TabIndex = 157;
            // 
            // panel21
            // 
            this.panel21.Controls.Add(this.label25);
            this.panel21.Controls.Add(this.cmbEstatusVenta);
            this.panel21.Location = new System.Drawing.Point(1311, 2);
            this.panel21.Margin = new System.Windows.Forms.Padding(2);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(117, 42);
            this.panel21.TabIndex = 162;
            this.panel21.Visible = false;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label25.Location = new System.Drawing.Point(3, 2);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(47, 14);
            this.label25.TabIndex = 158;
            this.label25.Text = "Estatus:";
            // 
            // cmbEstatusVenta
            // 
            this.cmbEstatusVenta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEstatusVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstatusVenta.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbEstatusVenta.FormattingEnabled = true;
            this.cmbEstatusVenta.Items.AddRange(new object[] {
            "VIGENTE",
            "PAGADA",
            " CANCELADAS",
            "-TODAS-"});
            this.cmbEstatusVenta.Location = new System.Drawing.Point(2, 17);
            this.cmbEstatusVenta.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEstatusVenta.Name = "cmbEstatusVenta";
            this.cmbEstatusVenta.Size = new System.Drawing.Size(114, 22);
            this.cmbEstatusVenta.TabIndex = 157;
            // 
            // panel22
            // 
            this.panel22.Controls.Add(this.label11);
            this.panel22.Controls.Add(this.cmbMetodoPago);
            this.panel22.Location = new System.Drawing.Point(2, 51);
            this.panel22.Margin = new System.Windows.Forms.Padding(2);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(192, 42);
            this.panel22.TabIndex = 159;
            this.panel22.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(2, 2);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 14);
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
            this.cmbMetodoPago.Location = new System.Drawing.Point(4, 18);
            this.cmbMetodoPago.Margin = new System.Windows.Forms.Padding(2);
            this.cmbMetodoPago.Name = "cmbMetodoPago";
            this.cmbMetodoPago.Size = new System.Drawing.Size(182, 22);
            this.cmbMetodoPago.TabIndex = 150;
            // 
            // panel23
            // 
            this.panel23.Controls.Add(this.label10);
            this.panel23.Controls.Add(this.cmbFormaPagoVentas);
            this.panel23.Location = new System.Drawing.Point(198, 51);
            this.panel23.Margin = new System.Windows.Forms.Padding(2);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(200, 42);
            this.panel23.TabIndex = 160;
            this.panel23.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 2);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 14);
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
            this.cmbFormaPagoVentas.Location = new System.Drawing.Point(7, 18);
            this.cmbFormaPagoVentas.Margin = new System.Windows.Forms.Padding(2);
            this.cmbFormaPagoVentas.Name = "cmbFormaPagoVentas";
            this.cmbFormaPagoVentas.Size = new System.Drawing.Size(187, 22);
            this.cmbFormaPagoVentas.TabIndex = 149;
            // 
            // panel24
            // 
            this.panel24.Controls.Add(this.label26);
            this.panel24.Controls.Add(this.cmbMonedaVentas);
            this.panel24.Location = new System.Drawing.Point(402, 51);
            this.panel24.Margin = new System.Windows.Forms.Padding(2);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(110, 42);
            this.panel24.TabIndex = 158;
            this.panel24.Visible = false;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label26.Location = new System.Drawing.Point(3, 2);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(50, 14);
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
            this.cmbMonedaVentas.Location = new System.Drawing.Point(4, 17);
            this.cmbMonedaVentas.Margin = new System.Windows.Forms.Padding(2);
            this.cmbMonedaVentas.Name = "cmbMonedaVentas";
            this.cmbMonedaVentas.Size = new System.Drawing.Size(103, 22);
            this.cmbMonedaVentas.TabIndex = 153;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1102, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 14);
            this.label1.TabIndex = 156;
            this.label1.Text = "Clientes:";
            this.label1.Visible = false;
            // 
            // cmbClientesVentasPorCliente
            // 
            this.cmbClientesVentasPorCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbClientesVentasPorCliente.DataSource = this.entPedidoBindingSource;
            this.cmbClientesVentasPorCliente.DisplayMember = "Cliente";
            this.cmbClientesVentasPorCliente.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbClientesVentasPorCliente.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbClientesVentasPorCliente.FormattingEnabled = true;
            this.cmbClientesVentasPorCliente.Location = new System.Drawing.Point(1106, 20);
            this.cmbClientesVentasPorCliente.Name = "cmbClientesVentasPorCliente";
            this.cmbClientesVentasPorCliente.Size = new System.Drawing.Size(168, 29);
            this.cmbClientesVentasPorCliente.TabIndex = 155;
            this.cmbClientesVentasPorCliente.ValueMember = "ClienteId";
            this.cmbClientesVentasPorCliente.Visible = false;
            // 
            // tpAnalitico
            // 
            this.tpAnalitico.Controls.Add(this.panel2);
            this.tpAnalitico.Controls.Add(this.tcReportesAnaliticos);
            this.tpAnalitico.Location = new System.Drawing.Point(4, 23);
            this.tpAnalitico.Margin = new System.Windows.Forms.Padding(2);
            this.tpAnalitico.Name = "tpAnalitico";
            this.tpAnalitico.Padding = new System.Windows.Forms.Padding(2);
            this.tpAnalitico.Size = new System.Drawing.Size(1126, 615);
            this.tpAnalitico.TabIndex = 7;
            this.tpAnalitico.Text = "Analítico";
            this.tpAnalitico.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdoPorFechasAnalitico);
            this.panel2.Controls.Add(this.pnlPorFechasAnalitico);
            this.panel2.Controls.Add(this.rdoPorMesAnalitico);
            this.panel2.Controls.Add(this.btnRefrescarAnalitico);
            this.panel2.Controls.Add(this.pnlPorMesAnalitico);
            this.panel2.Controls.Add(this.pnlPorDiaAnalitico);
            this.panel2.Controls.Add(this.radioButton3);
            this.panel2.Location = new System.Drawing.Point(3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(634, 121);
            this.panel2.TabIndex = 116;
            // 
            // rdoPorFechasAnalitico
            // 
            this.rdoPorFechasAnalitico.AutoSize = true;
            this.rdoPorFechasAnalitico.Location = new System.Drawing.Point(10, 93);
            this.rdoPorFechasAnalitico.Name = "rdoPorFechasAnalitico";
            this.rdoPorFechasAnalitico.Size = new System.Drawing.Size(80, 18);
            this.rdoPorFechasAnalitico.TabIndex = 117;
            this.rdoPorFechasAnalitico.Text = "Por Fechas";
            this.rdoPorFechasAnalitico.UseVisualStyleBackColor = true;
            this.rdoPorFechasAnalitico.CheckedChanged += new System.EventHandler(this.rdoPorFechasAnalitico_CheckedChanged);
            // 
            // pnlPorFechasAnalitico
            // 
            this.pnlPorFechasAnalitico.Controls.Add(this.dtpFechaHastaAnalitico);
            this.pnlPorFechasAnalitico.Controls.Add(this.dtpFechaDesdeAnalitico);
            this.pnlPorFechasAnalitico.Enabled = false;
            this.pnlPorFechasAnalitico.Location = new System.Drawing.Point(91, 82);
            this.pnlPorFechasAnalitico.Name = "pnlPorFechasAnalitico";
            this.pnlPorFechasAnalitico.Size = new System.Drawing.Size(484, 32);
            this.pnlPorFechasAnalitico.TabIndex = 116;
            // 
            // dtpFechaHastaAnalitico
            // 
            this.dtpFechaHastaAnalitico.Location = new System.Drawing.Point(252, 7);
            this.dtpFechaHastaAnalitico.Name = "dtpFechaHastaAnalitico";
            this.dtpFechaHastaAnalitico.Size = new System.Drawing.Size(224, 21);
            this.dtpFechaHastaAnalitico.TabIndex = 15;
            this.dtpFechaHastaAnalitico.ValueChanged += new System.EventHandler(this.btnRefrescaAnalitico_Click);
            // 
            // dtpFechaDesdeAnalitico
            // 
            this.dtpFechaDesdeAnalitico.Location = new System.Drawing.Point(5, 7);
            this.dtpFechaDesdeAnalitico.Name = "dtpFechaDesdeAnalitico";
            this.dtpFechaDesdeAnalitico.Size = new System.Drawing.Size(224, 21);
            this.dtpFechaDesdeAnalitico.TabIndex = 15;
            this.dtpFechaDesdeAnalitico.ValueChanged += new System.EventHandler(this.btnRefrescaAnalitico_Click);
            // 
            // rdoPorMesAnalitico
            // 
            this.rdoPorMesAnalitico.AutoSize = true;
            this.rdoPorMesAnalitico.Checked = true;
            this.rdoPorMesAnalitico.Location = new System.Drawing.Point(10, 42);
            this.rdoPorMesAnalitico.Name = "rdoPorMesAnalitico";
            this.rdoPorMesAnalitico.Size = new System.Drawing.Size(66, 18);
            this.rdoPorMesAnalitico.TabIndex = 44;
            this.rdoPorMesAnalitico.TabStop = true;
            this.rdoPorMesAnalitico.Text = "Por Mes";
            this.rdoPorMesAnalitico.UseVisualStyleBackColor = true;
            this.rdoPorMesAnalitico.CheckedChanged += new System.EventHandler(this.rdoPorMesAnalitico_CheckedChanged);
            // 
            // btnRefrescarAnalitico
            // 
            this.btnRefrescarAnalitico.BackColor = System.Drawing.Color.White;
            this.btnRefrescarAnalitico.BackgroundImage = global::Aires.Properties.Resources.Refresh;
            this.btnRefrescarAnalitico.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRefrescarAnalitico.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescarAnalitico.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescarAnalitico.Location = new System.Drawing.Point(343, 9);
            this.btnRefrescarAnalitico.Name = "btnRefrescarAnalitico";
            this.btnRefrescarAnalitico.Size = new System.Drawing.Size(77, 67);
            this.btnRefrescarAnalitico.TabIndex = 113;
            this.btnRefrescarAnalitico.Text = "Refrescar";
            this.btnRefrescarAnalitico.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefrescarAnalitico.UseVisualStyleBackColor = false;
            this.btnRefrescarAnalitico.Click += new System.EventHandler(this.btnRefrescaAnalitico_Click);
            // 
            // pnlPorMesAnalitico
            // 
            this.pnlPorMesAnalitico.Controls.Add(this.cmbMesesAnalitico);
            this.pnlPorMesAnalitico.Controls.Add(this.cmbAñosAnalitico);
            this.pnlPorMesAnalitico.Location = new System.Drawing.Point(91, 32);
            this.pnlPorMesAnalitico.Name = "pnlPorMesAnalitico";
            this.pnlPorMesAnalitico.Size = new System.Drawing.Size(243, 34);
            this.pnlPorMesAnalitico.TabIndex = 41;
            // 
            // cmbMesesAnalitico
            // 
            this.cmbMesesAnalitico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMesesAnalitico.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMesesAnalitico.FormattingEnabled = true;
            this.cmbMesesAnalitico.Items.AddRange(new object[] {
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
            this.cmbMesesAnalitico.Location = new System.Drawing.Point(5, 6);
            this.cmbMesesAnalitico.Name = "cmbMesesAnalitico";
            this.cmbMesesAnalitico.Size = new System.Drawing.Size(126, 28);
            this.cmbMesesAnalitico.TabIndex = 19;
            this.cmbMesesAnalitico.SelectedIndexChanged += new System.EventHandler(this.btnRefrescaAnalitico_Click);
            // 
            // cmbAñosAnalitico
            // 
            this.cmbAñosAnalitico.DataSource = this.entCatalogoGenericoBindingSource;
            this.cmbAñosAnalitico.DisplayMember = "Descripcion";
            this.cmbAñosAnalitico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAñosAnalitico.FormattingEnabled = true;
            this.cmbAñosAnalitico.Location = new System.Drawing.Point(152, 6);
            this.cmbAñosAnalitico.Name = "cmbAñosAnalitico";
            this.cmbAñosAnalitico.Size = new System.Drawing.Size(78, 24);
            this.cmbAñosAnalitico.TabIndex = 20;
            this.cmbAñosAnalitico.ValueMember = "Descripcion";
            this.cmbAñosAnalitico.SelectedIndexChanged += new System.EventHandler(this.btnRefrescaAnalitico_Click);
            // 
            // pnlPorDiaAnalitico
            // 
            this.pnlPorDiaAnalitico.Controls.Add(this.dateTimePicker4);
            this.pnlPorDiaAnalitico.Enabled = false;
            this.pnlPorDiaAnalitico.Location = new System.Drawing.Point(91, 44);
            this.pnlPorDiaAnalitico.Name = "pnlPorDiaAnalitico";
            this.pnlPorDiaAnalitico.Size = new System.Drawing.Size(243, 32);
            this.pnlPorDiaAnalitico.TabIndex = 42;
            this.pnlPorDiaAnalitico.Visible = false;
            // 
            // dateTimePicker4
            // 
            this.dateTimePicker4.Location = new System.Drawing.Point(5, 7);
            this.dateTimePicker4.Name = "dateTimePicker4";
            this.dateTimePicker4.Size = new System.Drawing.Size(224, 21);
            this.dateTimePicker4.TabIndex = 15;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(10, 54);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(62, 18);
            this.radioButton3.TabIndex = 43;
            this.radioButton3.Text = "Por Día";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Visible = false;
            // 
            // tcReportesAnaliticos
            // 
            this.tcReportesAnaliticos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcReportesAnaliticos.Controls.Add(this.tabPage2);
            this.tcReportesAnaliticos.Controls.Add(this.tabPage3);
            this.tcReportesAnaliticos.Font = new System.Drawing.Font("Microsoft Tai Le", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcReportesAnaliticos.Location = new System.Drawing.Point(3, 128);
            this.tcReportesAnaliticos.Name = "tcReportesAnaliticos";
            this.tcReportesAnaliticos.SelectedIndex = 0;
            this.tcReportesAnaliticos.Size = new System.Drawing.Size(176, 297);
            this.tcReportesAnaliticos.TabIndex = 115;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rvAnaliticoPorCostos);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(168, 271);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Por Costo";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rvAnaliticoPorCostos
            // 
            this.rvAnaliticoPorCostos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "dsInventario";
            reportDataSource1.Value = this.EntProductoBindingSource;
            this.rvAnaliticoPorCostos.LocalReport.DataSources.Add(reportDataSource1);
            this.rvAnaliticoPorCostos.LocalReport.ReportEmbeddedResource = "Aires.Reportes.rptHistoricoProductosPorPrecioCosto.rdlc";
            this.rvAnaliticoPorCostos.Location = new System.Drawing.Point(0, 0);
            this.rvAnaliticoPorCostos.Margin = new System.Windows.Forms.Padding(2);
            this.rvAnaliticoPorCostos.Name = "rvAnaliticoPorCostos";
            this.rvAnaliticoPorCostos.ServerReport.BearerToken = null;
            this.rvAnaliticoPorCostos.Size = new System.Drawing.Size(170, 272);
            this.rvAnaliticoPorCostos.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.rvAnaliticoPorUnidad);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(168, 271);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Por Unidad";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // rvAnaliticoPorUnidad
            // 
            this.rvAnaliticoPorUnidad.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource2.Name = "dsInventario";
            reportDataSource2.Value = this.EntProductoBindingSource;
            this.rvAnaliticoPorUnidad.LocalReport.DataSources.Add(reportDataSource2);
            this.rvAnaliticoPorUnidad.LocalReport.ReportEmbeddedResource = "Aires.Reportes.rptHistoricoProductosPorCantidad.rdlc";
            this.rvAnaliticoPorUnidad.Location = new System.Drawing.Point(0, 0);
            this.rvAnaliticoPorUnidad.Margin = new System.Windows.Forms.Padding(2);
            this.rvAnaliticoPorUnidad.Name = "rvAnaliticoPorUnidad";
            this.rvAnaliticoPorUnidad.ServerReport.BearerToken = null;
            this.rvAnaliticoPorUnidad.Size = new System.Drawing.Size(170, 272);
            this.rvAnaliticoPorUnidad.TabIndex = 1;
            // 
            // tpAuxiliar
            // 
            this.tpAuxiliar.Controls.Add(this.panel7);
            this.tpAuxiliar.Controls.Add(this.tcReportesAuxiliares);
            this.tpAuxiliar.Location = new System.Drawing.Point(4, 23);
            this.tpAuxiliar.Margin = new System.Windows.Forms.Padding(2);
            this.tpAuxiliar.Name = "tpAuxiliar";
            this.tpAuxiliar.Padding = new System.Windows.Forms.Padding(2);
            this.tpAuxiliar.Size = new System.Drawing.Size(1126, 615);
            this.tpAuxiliar.TabIndex = 8;
            this.tpAuxiliar.Text = "Auxiliar";
            this.tpAuxiliar.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.cmbProductosHistorialPorProducto);
            this.panel7.Controls.Add(this.radioButton2);
            this.panel7.Controls.Add(this.btnRefrescarAuxiliar);
            this.panel7.Controls.Add(this.panel9);
            this.panel7.Location = new System.Drawing.Point(3, 2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(634, 121);
            this.panel7.TabIndex = 118;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.cmbMesesAuxiliarHasta);
            this.panel8.Controls.Add(this.cmbAñosAuxiliarHasta);
            this.panel8.Location = new System.Drawing.Point(99, 44);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(243, 34);
            this.panel8.TabIndex = 123;
            // 
            // cmbMesesAuxiliarHasta
            // 
            this.cmbMesesAuxiliarHasta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMesesAuxiliarHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMesesAuxiliarHasta.FormattingEnabled = true;
            this.cmbMesesAuxiliarHasta.Items.AddRange(new object[] {
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
            this.cmbMesesAuxiliarHasta.Location = new System.Drawing.Point(5, 4);
            this.cmbMesesAuxiliarHasta.Name = "cmbMesesAuxiliarHasta";
            this.cmbMesesAuxiliarHasta.Size = new System.Drawing.Size(126, 28);
            this.cmbMesesAuxiliarHasta.TabIndex = 19;
            this.cmbMesesAuxiliarHasta.SelectedIndexChanged += new System.EventHandler(this.cmbMesesAuxiliarHasta_SelectedIndexChanged);
            // 
            // cmbAñosAuxiliarHasta
            // 
            this.cmbAñosAuxiliarHasta.DataSource = this.entCatalogoGenericoBindingSource;
            this.cmbAñosAuxiliarHasta.DisplayMember = "Descripcion";
            this.cmbAñosAuxiliarHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAñosAuxiliarHasta.FormattingEnabled = true;
            this.cmbAñosAuxiliarHasta.Location = new System.Drawing.Point(152, 6);
            this.cmbAñosAuxiliarHasta.Name = "cmbAñosAuxiliarHasta";
            this.cmbAñosAuxiliarHasta.Size = new System.Drawing.Size(78, 24);
            this.cmbAñosAuxiliarHasta.TabIndex = 20;
            this.cmbAñosAuxiliarHasta.ValueMember = "Descripcion";
            this.cmbAñosAuxiliarHasta.SelectedIndexChanged += new System.EventHandler(this.cmbMesesAuxiliarHasta_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 14);
            this.label4.TabIndex = 122;
            this.label4.Text = "PRODUCTO:";
            // 
            // cmbProductosHistorialPorProducto
            // 
            this.cmbProductosHistorialPorProducto.DataSource = this.EntProductoBindingSource;
            this.cmbProductosHistorialPorProducto.DisplayMember = "Descripcion";
            this.cmbProductosHistorialPorProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductosHistorialPorProducto.Font = new System.Drawing.Font("Microsoft Tai Le", 8F);
            this.cmbProductosHistorialPorProducto.FormattingEnabled = true;
            this.cmbProductosHistorialPorProducto.Location = new System.Drawing.Point(99, 93);
            this.cmbProductosHistorialPorProducto.Name = "cmbProductosHistorialPorProducto";
            this.cmbProductosHistorialPorProducto.Size = new System.Drawing.Size(534, 22);
            this.cmbProductosHistorialPorProducto.TabIndex = 121;
            this.cmbProductosHistorialPorProducto.ValueMember = "Id";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(32, 18);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(69, 18);
            this.radioButton2.TabIndex = 44;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Por Mes:";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // btnRefrescarAuxiliar
            // 
            this.btnRefrescarAuxiliar.BackColor = System.Drawing.Color.White;
            this.btnRefrescarAuxiliar.BackgroundImage = global::Aires.Properties.Resources.Refresh;
            this.btnRefrescarAuxiliar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRefrescarAuxiliar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescarAuxiliar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescarAuxiliar.Location = new System.Drawing.Point(343, 9);
            this.btnRefrescarAuxiliar.Name = "btnRefrescarAuxiliar";
            this.btnRefrescarAuxiliar.Size = new System.Drawing.Size(77, 67);
            this.btnRefrescarAuxiliar.TabIndex = 113;
            this.btnRefrescarAuxiliar.Text = "Refrescar";
            this.btnRefrescarAuxiliar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefrescarAuxiliar.UseVisualStyleBackColor = false;
            this.btnRefrescarAuxiliar.Click += new System.EventHandler(this.btnRefrescarAuxiliar_Click);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.cmbMesesAuxiliar);
            this.panel9.Controls.Add(this.cmbAñosAuxiliar);
            this.panel9.Location = new System.Drawing.Point(99, 9);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(243, 34);
            this.panel9.TabIndex = 41;
            // 
            // cmbMesesAuxiliar
            // 
            this.cmbMesesAuxiliar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMesesAuxiliar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMesesAuxiliar.FormattingEnabled = true;
            this.cmbMesesAuxiliar.Items.AddRange(new object[] {
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
            this.cmbMesesAuxiliar.Location = new System.Drawing.Point(5, 4);
            this.cmbMesesAuxiliar.Name = "cmbMesesAuxiliar";
            this.cmbMesesAuxiliar.Size = new System.Drawing.Size(126, 28);
            this.cmbMesesAuxiliar.TabIndex = 19;
            this.cmbMesesAuxiliar.SelectedIndexChanged += new System.EventHandler(this.cmbMesesAuxiliar_SelectedIndexChanged);
            // 
            // cmbAñosAuxiliar
            // 
            this.cmbAñosAuxiliar.DataSource = this.entCatalogoGenericoBindingSource;
            this.cmbAñosAuxiliar.DisplayMember = "Descripcion";
            this.cmbAñosAuxiliar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAñosAuxiliar.FormattingEnabled = true;
            this.cmbAñosAuxiliar.Location = new System.Drawing.Point(152, 6);
            this.cmbAñosAuxiliar.Name = "cmbAñosAuxiliar";
            this.cmbAñosAuxiliar.Size = new System.Drawing.Size(78, 24);
            this.cmbAñosAuxiliar.TabIndex = 20;
            this.cmbAñosAuxiliar.ValueMember = "Descripcion";
            this.cmbAñosAuxiliar.SelectedIndexChanged += new System.EventHandler(this.cmbMesesAuxiliar_SelectedIndexChanged);
            // 
            // tcReportesAuxiliares
            // 
            this.tcReportesAuxiliares.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcReportesAuxiliares.Controls.Add(this.tabPage6);
            this.tcReportesAuxiliares.Controls.Add(this.tabPage7);
            this.tcReportesAuxiliares.Font = new System.Drawing.Font("Microsoft Tai Le", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcReportesAuxiliares.Location = new System.Drawing.Point(3, 128);
            this.tcReportesAuxiliares.Name = "tcReportesAuxiliares";
            this.tcReportesAuxiliares.SelectedIndex = 0;
            this.tcReportesAuxiliares.Size = new System.Drawing.Size(176, 297);
            this.tcReportesAuxiliares.TabIndex = 117;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.rvAuxiliarPorCostos);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(168, 271);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Por Costo";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // rvAuxiliarPorCostos
            // 
            this.rvAuxiliarPorCostos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource3.Name = "dsInventario";
            reportDataSource3.Value = this.EntProductoBindingSource;
            this.rvAuxiliarPorCostos.LocalReport.DataSources.Add(reportDataSource3);
            this.rvAuxiliarPorCostos.LocalReport.ReportEmbeddedResource = "Aires.Reportes.rptHistoricoProductosDetallePorPrecioCosto.rdlc";
            this.rvAuxiliarPorCostos.Location = new System.Drawing.Point(0, 0);
            this.rvAuxiliarPorCostos.Margin = new System.Windows.Forms.Padding(2);
            this.rvAuxiliarPorCostos.Name = "rvAuxiliarPorCostos";
            this.rvAuxiliarPorCostos.ServerReport.BearerToken = null;
            this.rvAuxiliarPorCostos.Size = new System.Drawing.Size(170, 272);
            this.rvAuxiliarPorCostos.TabIndex = 2;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.rvAuxiliarPorUnidad);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage7.Size = new System.Drawing.Size(168, 271);
            this.tabPage7.TabIndex = 2;
            this.tabPage7.Text = "Por Unidad";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // rvAuxiliarPorUnidad
            // 
            this.rvAuxiliarPorUnidad.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource4.Name = "dsInventario";
            reportDataSource4.Value = this.EntProductoBindingSource;
            this.rvAuxiliarPorUnidad.LocalReport.DataSources.Add(reportDataSource4);
            this.rvAuxiliarPorUnidad.LocalReport.ReportEmbeddedResource = "Aires.Reportes.rptHistoricoProductosDetallePorCantidad.rdlc";
            this.rvAuxiliarPorUnidad.Location = new System.Drawing.Point(0, 0);
            this.rvAuxiliarPorUnidad.Margin = new System.Windows.Forms.Padding(2);
            this.rvAuxiliarPorUnidad.Name = "rvAuxiliarPorUnidad";
            this.rvAuxiliarPorUnidad.ServerReport.BearerToken = null;
            this.rvAuxiliarPorUnidad.Size = new System.Drawing.Size(170, 272);
            this.rvAuxiliarPorUnidad.TabIndex = 3;
            // 
            // entProductoBindingSource2
            // 
            this.entProductoBindingSource2.DataSource = typeof(AiresEntidades.EntProducto);
            // 
            // entPedidoBindingSource1
            // 
            this.entPedidoBindingSource1.DataSource = typeof(AiresEntidades.EntPedido);
            // 
            // entClienteBindingSource1
            // 
            this.entClienteBindingSource1.DataSource = typeof(AiresEntidades.EntCliente);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 16);
            this.label3.TabIndex = 116;
            this.label3.Text = "PRODUCTOS";
            // 
            // cmbFiltroProductosVentas
            // 
            this.cmbFiltroProductosVentas.DataSource = this.EntProductoBindingSource;
            this.cmbFiltroProductosVentas.DisplayMember = "Descripcion";
            this.cmbFiltroProductosVentas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroProductosVentas.Font = new System.Drawing.Font("Microsoft Tai Le", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFiltroProductosVentas.FormattingEnabled = true;
            this.cmbFiltroProductosVentas.Location = new System.Drawing.Point(100, 100);
            this.cmbFiltroProductosVentas.Margin = new System.Windows.Forms.Padding(4);
            this.cmbFiltroProductosVentas.Name = "cmbFiltroProductosVentas";
            this.cmbFiltroProductosVentas.Size = new System.Drawing.Size(677, 21);
            this.cmbFiltroProductosVentas.TabIndex = 115;
            this.cmbFiltroProductosVentas.ValueMember = "Id";
            this.cmbFiltroProductosVentas.SelectedIndexChanged += new System.EventHandler(this.cmbFiltroProductosVentas_SelectedIndexChanged);
            // 
            // entClienteBindingSource
            // 
            this.entClienteBindingSource.DataSource = typeof(AiresEntidades.EntCliente);
            // 
            // btnBuscaEmpresa
            // 
            this.btnBuscaEmpresa.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBuscaEmpresa.BackColor = System.Drawing.Color.Transparent;
            this.btnBuscaEmpresa.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscaEmpresa.BackgroundImage")));
            this.btnBuscaEmpresa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBuscaEmpresa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscaEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscaEmpresa.Location = new System.Drawing.Point(948, 5);
            this.btnBuscaEmpresa.Name = "btnBuscaEmpresa";
            this.btnBuscaEmpresa.Size = new System.Drawing.Size(40, 28);
            this.btnBuscaEmpresa.TabIndex = 128;
            this.btnBuscaEmpresa.UseVisualStyleBackColor = false;
            this.btnBuscaEmpresa.Visible = false;
            this.btnBuscaEmpresa.Click += new System.EventHandler(this.btnBuscaEmpresa_Click);
            // 
            // label21
            // 
            this.label21.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Location = new System.Drawing.Point(498, 15);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(51, 13);
            this.label21.TabIndex = 126;
            this.label21.Text = "Empresa:";
            this.label21.Visible = false;
            // 
            // cmbEmpresas
            // 
            this.cmbEmpresas.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbEmpresas.DisplayMember = "Descripcion";
            this.cmbEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpresas.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbEmpresas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmpresas.FormattingEnabled = true;
            this.cmbEmpresas.Location = new System.Drawing.Point(550, 5);
            this.cmbEmpresas.Name = "cmbEmpresas";
            this.cmbEmpresas.Size = new System.Drawing.Size(359, 28);
            this.cmbEmpresas.TabIndex = 127;
            this.cmbEmpresas.ValueMember = "Id";
            this.cmbEmpresas.Visible = false;
            this.cmbEmpresas.SelectedIndexChanged += new System.EventHandler(this.cmbEmpresas_SelectedIndexChanged);
            // 
            // entProductoBindingSource1
            // 
            this.entProductoBindingSource1.DataSource = typeof(AiresEntidades.EntProducto);
            // 
            // ReportesGlobales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1155, 668);
            this.Controls.Add(this.btnBuscaEmpresa);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.cmbEmpresas);
            this.Controls.Add(this.tcReportes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ReportesGlobales";
            this.Text = "Reportes Globales";
            this.Load += new System.EventHandler(this.Reportes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EntProductoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entPedidoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EntEmpresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EntDepositoBindingSource)).EndInit();
            this.tcReportes.ResumeLayout(false);
            this.tpVentas.ResumeLayout(false);
            this.tcReportesVentas.ResumeLayout(false);
            this.tpVentasPorProducto.ResumeLayout(false);
            this.tpVentasPorProducto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductosFiltro)).EndInit();
            this.tcReportesGlobalesGeneral.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tpReporteAumento.ResumeLayout(false);
            this.tpReporteAumento.PerformLayout();
            this.flpFiltroPorProducto.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlPeriodoComparativo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.entCatalogoGenericoBindingSource)).EndInit();
            this.pnlPeriodoConsulta.ResumeLayout(false);
            this.pnlPeriodoConsulta.PerformLayout();
            this.pnlPorFechasVentas.ResumeLayout(false);
            this.pnlPorMesVentas.ResumeLayout(false);
            this.pnlPorDiaVentas.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tcReportesVentasPorProducto.ResumeLayout(false);
            this.tabPage9.ResumeLayout(false);
            this.tabPage9.PerformLayout();
            this.tpClientesPorProducto.ResumeLayout(false);
            this.tpProductosPorCliente.ResumeLayout(false);
            this.gbFiltrosVentas.ResumeLayout(false);
            this.gbFiltrosVentas.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel19.ResumeLayout(false);
            this.panel19.PerformLayout();
            this.pnlFiltroPorTrabajador.ResumeLayout(false);
            this.pnlFiltroPorTrabajador.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entTrabajadorBindingSource)).EndInit();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.panel21.ResumeLayout(false);
            this.panel21.PerformLayout();
            this.panel22.ResumeLayout(false);
            this.panel22.PerformLayout();
            this.panel23.ResumeLayout(false);
            this.panel23.PerformLayout();
            this.panel24.ResumeLayout(false);
            this.panel24.PerformLayout();
            this.tpAnalitico.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlPorFechasAnalitico.ResumeLayout(false);
            this.pnlPorMesAnalitico.ResumeLayout(false);
            this.pnlPorDiaAnalitico.ResumeLayout(false);
            this.tcReportesAnaliticos.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tpAuxiliar.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.tcReportesAuxiliares.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.entProductoBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entPedidoBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entClienteBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entClienteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entProductoBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcReportes;
        private System.Windows.Forms.BindingSource entPedidoBindingSource;
        private System.Windows.Forms.TabPage tpVentas;
        private System.Windows.Forms.RadioButton rdoPorMesVentas;
        private System.Windows.Forms.RadioButton rdoPorDiaVentas;
        private System.Windows.Forms.Panel pnlPorDiaVentas;
        private System.Windows.Forms.DateTimePicker dtpFechaDiaVentas;
        private System.Windows.Forms.Panel pnlPorMesVentas;
        private System.Windows.Forms.ComboBox cmbMesesVentas;
        private System.Windows.Forms.ComboBox cmbAñosVentas;
        private System.Windows.Forms.TabControl tcReportesVentas;
        private System.Windows.Forms.BindingSource entCatalogoGenericoBindingSource;
        private System.Windows.Forms.BindingSource EntEmpresaBindingSource;
        private System.Windows.Forms.Panel pnlPeriodoConsulta;
        private System.Windows.Forms.BindingSource EntProductoBindingSource;
        private System.Windows.Forms.Button btnRefrescarVentas;
        private System.Windows.Forms.BindingSource entClienteBindingSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbFiltroProductosVentas;
        private System.Windows.Forms.BindingSource EntDepositoBindingSource;
        private System.Windows.Forms.Button btnBuscaEmpresa;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cmbEmpresas;
        private System.Windows.Forms.RadioButton rdoPorFechasVentas;
        private System.Windows.Forms.Panel pnlPorFechasVentas;
        private System.Windows.Forms.DateTimePicker dtpFechaHastaVentas;
        private System.Windows.Forms.DateTimePicker dtpFechaDesdeVentas;
        private System.Windows.Forms.TabPage tpAnalitico;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdoPorFechasAnalitico;
        private System.Windows.Forms.Panel pnlPorFechasAnalitico;
        private System.Windows.Forms.DateTimePicker dtpFechaHastaAnalitico;
        private System.Windows.Forms.DateTimePicker dtpFechaDesdeAnalitico;
        private System.Windows.Forms.RadioButton rdoPorMesAnalitico;
        private System.Windows.Forms.Button btnRefrescarAnalitico;
        private System.Windows.Forms.Panel pnlPorMesAnalitico;
        private System.Windows.Forms.ComboBox cmbMesesAnalitico;
        private System.Windows.Forms.ComboBox cmbAñosAnalitico;
        private System.Windows.Forms.Panel pnlPorDiaAnalitico;
        private System.Windows.Forms.DateTimePicker dateTimePicker4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.TabControl tcReportesAnaliticos;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.BindingSource entClienteBindingSource1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tpAuxiliar;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button btnRefrescarAuxiliar;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.ComboBox cmbMesesAuxiliar;
        private System.Windows.Forms.ComboBox cmbAñosAuxiliar;
        private System.Windows.Forms.TabControl tcReportesAuxiliares;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private Microsoft.Reporting.WinForms.ReportViewer rvAnaliticoPorCostos;
        private Microsoft.Reporting.WinForms.ReportViewer rvAnaliticoPorUnidad;
        private Microsoft.Reporting.WinForms.ReportViewer rvAuxiliarPorCostos;
        private Microsoft.Reporting.WinForms.ReportViewer rvAuxiliarPorUnidad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbProductosHistorialPorProducto;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.ComboBox cmbMesesAuxiliarHasta;
        private System.Windows.Forms.ComboBox cmbAñosAuxiliarHasta;
        private System.Windows.Forms.BindingSource entPedidoBindingSource1;
        private System.Windows.Forms.BindingSource entProductoBindingSource1;
        private System.Windows.Forms.BindingSource entProductoBindingSource2;
        private System.Windows.Forms.TabPage tpVentasPorProducto;
        private Microsoft.Reporting.WinForms.ReportViewer rvVentasPorProducto;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtProductoDescripcionFiltroVentas;
        private System.Windows.Forms.Button btnBuscaProductoVentas;
        private System.Windows.Forms.TextBox txtProductoCodigoFiltroVentas;
        private System.Windows.Forms.FlowLayoutPanel flpFiltroPorProducto;
        private System.Windows.Forms.GroupBox gbFiltrosVentas;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.Button btnBuscaCliente;
        private System.Windows.Forms.TextBox txtFiltroClientes;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cmbTipoRegistroVentas;
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbClientesVentasPorCliente;
        private System.Windows.Forms.TabControl tcReportesVentasPorProducto;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.TabPage tpClientesPorProducto;
        private Microsoft.Reporting.WinForms.ReportViewer rvVentasClientesPorProducto;
        private System.Windows.Forms.TabPage tpProductosPorCliente;
        private Microsoft.Reporting.WinForms.ReportViewer rvVentasProductosPorClientes;
        private System.Windows.Forms.Panel pnlFiltroPorTrabajador;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbTrabajadores;
        private System.Windows.Forms.BindingSource entTrabajadorBindingSource;
        private System.Windows.Forms.Button btnLimpiaFiltroProductoVentas;
        private System.Windows.Forms.Button btnLimpiaFiltroClienteVentas;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.CheckBox chkSoloDevoluciones;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton rdoReportePorProductoGeneral;
        private Microsoft.Reporting.WinForms.ReportViewer rvVentasPorProductoAlmacen;
        private System.Windows.Forms.CheckBox chkSoloTotales;
        private Microsoft.Reporting.WinForms.ReportViewer rvVentasPorProductoGlobal;
        private Microsoft.Reporting.WinForms.ReportViewer rvVentasPorProductoTotales;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlPeriodoComparativo;
        private System.Windows.Forms.ComboBox cmbPeriodoVentas;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabControl tcReportesGlobalesGeneral;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tpReporteAumento;
        private System.Windows.Forms.DataGridView gvProductosFiltro;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Estatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnFiltrarProductos;
        private Microsoft.Reporting.WinForms.ReportViewer rvVentasPorProductoAlmacenGlobal;
        private System.Windows.Forms.CheckBox chkSeleccionaTodos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbAlmacenes;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox chkCodigosBajos;
        private System.Windows.Forms.RadioButton rdoPorClienteGlobal;
        private System.Windows.Forms.RadioButton rdoPorProductoGlobal;
        private Microsoft.Reporting.WinForms.ReportViewer rvVentasPorClienteGlobal;
        private System.Windows.Forms.CheckBox chkSinComparativo;
        private System.Windows.Forms.Label label6;
    }
}