namespace Aires.Pantallas
{
    partial class Sincronizacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sincronizacion));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.entPedidoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tcPedidosGrids = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCantidadVentas = new System.Windows.Forms.TextBox();
            this.txtDescripcionFiltro = new System.Windows.Forms.TextBox();
            this.btnFiltrarCliente = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
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
            this.gvProductosDetalle = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.entPedidoBindingSource)).BeginInit();
            this.tcPedidosGrids.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.pnlVentasPorFechas.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.pnlVentasPorDia.SuspendLayout();
            this.pnlVentasPorMes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entCatalogoGenericoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductosDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // entPedidoBindingSource
            // 
            this.entPedidoBindingSource.DataSource = typeof(AiresEntidades.EntPedido);
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
            this.tabPage1.Controls.Add(this.gvProductosDetalle);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtCantidadVentas);
            this.tabPage1.Controls.Add(this.txtDescripcionFiltro);
            this.tabPage1.Controls.Add(this.btnFiltrarCliente);
            this.tabPage1.Controls.Add(this.btnEliminar);
            this.tabPage1.Controls.Add(this.btnRefrescar);
            this.tabPage1.Controls.Add(this.btnCancelar);
            this.tabPage1.Controls.Add(this.btnVerFactura);
            this.tabPage1.Controls.Add(this.btnFacturar);
            this.tabPage1.Controls.Add(this.btnEnviaCorreo);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Tai Le", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1548, 718);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Importar Compras";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1055, 610);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 128;
            this.label3.Text = "Cantidad:";
            // 
            // txtCantidadVentas
            // 
            this.txtCantidadVentas.Enabled = false;
            this.txtCantidadVentas.Location = new System.Drawing.Point(1103, 606);
            this.txtCantidadVentas.Name = "txtCantidadVentas";
            this.txtCantidadVentas.Size = new System.Drawing.Size(55, 19);
            this.txtCantidadVentas.TabIndex = 127;
            // 
            // txtDescripcionFiltro
            // 
            this.txtDescripcionFiltro.Font = new System.Drawing.Font("Microsoft Tai Le", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcionFiltro.Location = new System.Drawing.Point(6, 6);
            this.txtDescripcionFiltro.Name = "txtDescripcionFiltro";
            this.txtDescripcionFiltro.Size = new System.Drawing.Size(686, 21);
            this.txtDescripcionFiltro.TabIndex = 124;
            this.txtDescripcionFiltro.TextChanged += new System.EventHandler(this.btnFiltrarCliente_Click);
            // 
            // btnFiltrarCliente
            // 
            this.btnFiltrarCliente.BackColor = System.Drawing.Color.White;
            this.btnFiltrarCliente.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFiltrarCliente.BackgroundImage")));
            this.btnFiltrarCliente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFiltrarCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltrarCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrarCliente.Location = new System.Drawing.Point(694, 1);
            this.btnFiltrarCliente.Name = "btnFiltrarCliente";
            this.btnFiltrarCliente.Size = new System.Drawing.Size(37, 32);
            this.btnFiltrarCliente.TabIndex = 122;
            this.btnFiltrarCliente.UseVisualStyleBackColor = false;
            this.btnFiltrarCliente.Click += new System.EventHandler(this.btnFiltrarCliente_Click);
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
            this.btnCancelar.Location = new System.Drawing.Point(1164, 33);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(77, 66);
            this.btnCancelar.TabIndex = 119;
            this.btnCancelar.Text = "Cancelar Factura";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
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
            this.btnFacturar.Location = new System.Drawing.Point(1350, 34);
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
            this.btnEnviaCorreo.Location = new System.Drawing.Point(1252, 34);
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
            this.tabPage2.Text = "Importar Compras";
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
            // gvProductosDetalle
            // 
            this.gvProductosDetalle.AllowUserToAddRows = false;
            this.gvProductosDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvProductosDetalle.BackgroundColor = System.Drawing.Color.White;
            this.gvProductosDetalle.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvProductosDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProductosDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.Serie});
            this.gvProductosDetalle.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvProductosDetalle.GridColor = System.Drawing.Color.DimGray;
            this.gvProductosDetalle.Location = new System.Drawing.Point(6, 33);
            this.gvProductosDetalle.Name = "gvProductosDetalle";
            this.gvProductosDetalle.ReadOnly = true;
            this.gvProductosDetalle.RowHeadersVisible = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Unicode MS", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvProductosDetalle.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvProductosDetalle.RowTemplate.Height = 27;
            this.gvProductosDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvProductosDetalle.Size = new System.Drawing.Size(1152, 567);
            this.gvProductosDetalle.TabIndex = 129;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Codigo";
            this.dataGridViewTextBoxColumn2.FillWeight = 2F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Codigo";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // Serie
            // 
            this.Serie.DataPropertyName = "Serie";
            this.Serie.FillWeight = 3F;
            this.Serie.HeaderText = "Serie";
            this.Serie.Name = "Serie";
            this.Serie.ReadOnly = true;
            // 
            // Sincronizacion
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
            this.Name = "Sincronizacion";
            this.Text = "Sincronización";
            this.Load += new System.EventHandler(this.Reportes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.entPedidoBindingSource)).EndInit();
            this.tcPedidosGrids.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.pnlVentasPorFechas.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.pnlVentasPorDia.ResumeLayout(false);
            this.pnlVentasPorMes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.entCatalogoGenericoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductosDetalle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcPedidosGrids;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.BindingSource entPedidoBindingSource;
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
        private System.Windows.Forms.Button btnFiltrarCliente;
        private System.Windows.Forms.TextBox txtDescripcionFiltro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCantidadVentas;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cmbEmpresas;
        private System.Windows.Forms.Button btnBuscaEmpresa;
        private System.Windows.Forms.RadioButton rdoPorFechasVentas;
        private System.Windows.Forms.Panel pnlVentasPorFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaHastaVentas;
        private System.Windows.Forms.DateTimePicker dtpFechaDesdeVentas;
        private System.Windows.Forms.DataGridView gvProductosDetalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serie;
    }
}