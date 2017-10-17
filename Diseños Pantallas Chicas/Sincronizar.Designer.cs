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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sincronizacion));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.entPedidoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tcPedidosGrids = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnImportar = new System.Windows.Forms.Button();
            this.gvProductosDetalle = new System.Windows.Forms.DataGridView();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ingreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entProductoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.txtCantidadVentas = new System.Windows.Forms.TextBox();
            this.txtDescripcionFiltro = new System.Windows.Forms.TextBox();
            this.btnFiltrarCliente = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.btnVerFactura = new System.Windows.Forms.Button();
            this.tpImportaVentas = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCantidadPedidosImporta = new System.Windows.Forms.TextBox();
            this.gvPedidosImporta = new System.Windows.Forms.DataGridView();
            this.NumCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clienteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detalleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Facturado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FechaCorta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstatusDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnImportaVentas = new System.Windows.Forms.Button();
            this.txtRutaArchivoImportaVentas = new System.Windows.Forms.TextBox();
            this.btnBuscaArchivoImportaVentas = new System.Windows.Forms.Button();
            this.entCatalogoGenericoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label24 = new System.Windows.Forms.Label();
            this.cmbEmpresas = new System.Windows.Forms.ComboBox();
            this.btnBuscaEmpresa = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.entPedidoBindingSource)).BeginInit();
            this.tcPedidosGrids.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductosDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entProductoBindingSource)).BeginInit();
            this.tpImportaVentas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPedidosImporta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entCatalogoGenericoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // entPedidoBindingSource
            // 
            this.entPedidoBindingSource.DataSource = typeof(AiresEntidades.EntPedido);
            // 
            // tcPedidosGrids
            // 
            this.tcPedidosGrids.Controls.Add(this.tabPage1);
            this.tcPedidosGrids.Controls.Add(this.tpImportaVentas);
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
            this.tabPage1.Controls.Add(this.btnImportar);
            this.tabPage1.Controls.Add(this.gvProductosDetalle);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtCantidadVentas);
            this.tabPage1.Controls.Add(this.txtDescripcionFiltro);
            this.tabPage1.Controls.Add(this.btnFiltrarCliente);
            this.tabPage1.Controls.Add(this.btnRefrescar);
            this.tabPage1.Controls.Add(this.btnVerFactura);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Tai Le", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1548, 718);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Importar Entradas";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // btnImportar
            // 
            this.btnImportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportar.BackColor = System.Drawing.Color.White;
            this.btnImportar.BackgroundImage = global::Aires.Properties.Resources.Comprar__Chico_;
            this.btnImportar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnImportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportar.Location = new System.Drawing.Point(1164, 33);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(74, 66);
            this.btnImportar.TabIndex = 130;
            this.btnImportar.Text = "Importar";
            this.btnImportar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnImportar.UseVisualStyleBackColor = false;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // gvProductosDetalle
            // 
            this.gvProductosDetalle.AllowUserToAddRows = false;
            this.gvProductosDetalle.AutoGenerateColumns = false;
            this.gvProductosDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvProductosDetalle.BackgroundColor = System.Drawing.Color.White;
            this.gvProductosDetalle.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvProductosDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProductosDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Fecha,
            this.Ingreso,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Serie,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.gvProductosDetalle.DataSource = this.entProductoBindingSource;
            this.gvProductosDetalle.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvProductosDetalle.GridColor = System.Drawing.Color.DimGray;
            this.gvProductosDetalle.Location = new System.Drawing.Point(6, 33);
            this.gvProductosDetalle.Name = "gvProductosDetalle";
            this.gvProductosDetalle.ReadOnly = true;
            this.gvProductosDetalle.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial Unicode MS", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvProductosDetalle.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvProductosDetalle.RowTemplate.Height = 27;
            this.gvProductosDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvProductosDetalle.Size = new System.Drawing.Size(1152, 567);
            this.gvProductosDetalle.TabIndex = 129;
            this.gvProductosDetalle.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProductosDetalle_CellContentClick);
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "Fecha";
            dataGridViewCellStyle1.Format = "d";
            this.Fecha.DefaultCellStyle = dataGridViewCellStyle1;
            this.Fecha.FillWeight = 2F;
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            // 
            // Ingreso
            // 
            this.Ingreso.DataPropertyName = "Ingreso";
            this.Ingreso.FillWeight = 3F;
            this.Ingreso.HeaderText = "Ingreso";
            this.Ingreso.Name = "Ingreso";
            this.Ingreso.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Codigo";
            this.dataGridViewTextBoxColumn2.FillWeight = 2F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Codigo";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Descripcion";
            this.dataGridViewTextBoxColumn3.FillWeight = 7F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Descripcion";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // Serie
            // 
            this.Serie.DataPropertyName = "Serie";
            this.Serie.FillWeight = 3F;
            this.Serie.HeaderText = "Serie";
            this.Serie.Name = "Serie";
            this.Serie.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "PrecioCosto";
            dataGridViewCellStyle2.Format = "c2";
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn4.FillWeight = 2F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Precio Costo ";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "PrecioVenta";
            dataGridViewCellStyle3.Format = "c2";
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn5.FillWeight = 2F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Precio Venta ";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // entProductoBindingSource
            // 
            this.entProductoBindingSource.DataSource = typeof(AiresEntidades.EntProducto);
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
            // btnRefrescar
            // 
            this.btnRefrescar.BackColor = System.Drawing.Color.White;
            this.btnRefrescar.BackgroundImage = global::Aires.Properties.Resources.Refresh;
            this.btnRefrescar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescar.Location = new System.Drawing.Point(6, 610);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(77, 66);
            this.btnRefrescar.TabIndex = 112;
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefrescar.UseVisualStyleBackColor = false;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // btnVerFactura
            // 
            this.btnVerFactura.BackColor = System.Drawing.Color.White;
            this.btnVerFactura.BackgroundImage = global::Aires.Properties.Resources.Paper_Search__chico_;
            this.btnVerFactura.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnVerFactura.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerFactura.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerFactura.Location = new System.Drawing.Point(110, 609);
            this.btnVerFactura.Name = "btnVerFactura";
            this.btnVerFactura.Size = new System.Drawing.Size(77, 66);
            this.btnVerFactura.TabIndex = 116;
            this.btnVerFactura.Text = "Ver Archivo";
            this.btnVerFactura.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnVerFactura.UseVisualStyleBackColor = false;
            this.btnVerFactura.Click += new System.EventHandler(this.btnVerFactura_Click);
            // 
            // tpImportaVentas
            // 
            this.tpImportaVentas.Controls.Add(this.label1);
            this.tpImportaVentas.Controls.Add(this.txtCantidadPedidosImporta);
            this.tpImportaVentas.Controls.Add(this.gvPedidosImporta);
            this.tpImportaVentas.Controls.Add(this.btnImportaVentas);
            this.tpImportaVentas.Controls.Add(this.txtRutaArchivoImportaVentas);
            this.tpImportaVentas.Controls.Add(this.btnBuscaArchivoImportaVentas);
            this.tpImportaVentas.Location = new System.Drawing.Point(4, 23);
            this.tpImportaVentas.Name = "tpImportaVentas";
            this.tpImportaVentas.Padding = new System.Windows.Forms.Padding(3);
            this.tpImportaVentas.Size = new System.Drawing.Size(1548, 718);
            this.tpImportaVentas.TabIndex = 1;
            this.tpImportaVentas.Text = "Importar Ventas";
            this.tpImportaVentas.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1092, 648);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 14);
            this.label1.TabIndex = 137;
            this.label1.Text = "Cantidad:";
            // 
            // txtCantidadPedidosImporta
            // 
            this.txtCantidadPedidosImporta.Enabled = false;
            this.txtCantidadPedidosImporta.Location = new System.Drawing.Point(1152, 644);
            this.txtCantidadPedidosImporta.Name = "txtCantidadPedidosImporta";
            this.txtCantidadPedidosImporta.Size = new System.Drawing.Size(55, 21);
            this.txtCantidadPedidosImporta.TabIndex = 136;
            // 
            // gvPedidosImporta
            // 
            this.gvPedidosImporta.AllowUserToAddRows = false;
            this.gvPedidosImporta.AutoGenerateColumns = false;
            this.gvPedidosImporta.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvPedidosImporta.BackgroundColor = System.Drawing.Color.White;
            this.gvPedidosImporta.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvPedidosImporta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPedidosImporta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumCliente,
            this.clienteDataGridViewTextBoxColumn,
            this.detalleDataGridViewTextBoxColumn,
            this.Factura,
            this.Facturado,
            this.FechaCorta,
            this.totalDataGridViewTextBoxColumn,
            this.EstatusDescripcion});
            this.gvPedidosImporta.DataSource = this.entPedidoBindingSource;
            this.gvPedidosImporta.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvPedidosImporta.GridColor = System.Drawing.Color.DimGray;
            this.gvPedidosImporta.Location = new System.Drawing.Point(6, 33);
            this.gvPedidosImporta.MultiSelect = false;
            this.gvPedidosImporta.Name = "gvPedidosImporta";
            this.gvPedidosImporta.ReadOnly = true;
            this.gvPedidosImporta.RowHeadersVisible = false;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial Unicode MS", 7F);
            this.gvPedidosImporta.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.gvPedidosImporta.RowTemplate.Height = 27;
            this.gvPedidosImporta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvPedidosImporta.Size = new System.Drawing.Size(1201, 605);
            this.gvPedidosImporta.TabIndex = 135;
            // 
            // NumCliente
            // 
            this.NumCliente.DataPropertyName = "NumCliente";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NumCliente.DefaultCellStyle = dataGridViewCellStyle5;
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
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Factura.DefaultCellStyle = dataGridViewCellStyle6;
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
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.FechaCorta.DefaultCellStyle = dataGridViewCellStyle7;
            this.FechaCorta.FillWeight = 1.5F;
            this.FechaCorta.HeaderText = "Fecha";
            this.FechaCorta.Name = "FechaCorta";
            this.FechaCorta.ReadOnly = true;
            // 
            // totalDataGridViewTextBoxColumn
            // 
            this.totalDataGridViewTextBoxColumn.DataPropertyName = "Total";
            dataGridViewCellStyle8.Format = "c2";
            this.totalDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
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
            // btnImportaVentas
            // 
            this.btnImportaVentas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportaVentas.BackColor = System.Drawing.Color.White;
            this.btnImportaVentas.BackgroundImage = global::Aires.Properties.Resources.Comprar__Chico_;
            this.btnImportaVentas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnImportaVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportaVentas.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportaVentas.Location = new System.Drawing.Point(1213, 33);
            this.btnImportaVentas.Name = "btnImportaVentas";
            this.btnImportaVentas.Size = new System.Drawing.Size(74, 66);
            this.btnImportaVentas.TabIndex = 134;
            this.btnImportaVentas.Text = "Importar";
            this.btnImportaVentas.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnImportaVentas.UseVisualStyleBackColor = false;
            this.btnImportaVentas.Click += new System.EventHandler(this.btnImportaVentas_Click);
            // 
            // txtRutaArchivoImportaVentas
            // 
            this.txtRutaArchivoImportaVentas.Font = new System.Drawing.Font("Microsoft Tai Le", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRutaArchivoImportaVentas.Location = new System.Drawing.Point(6, 6);
            this.txtRutaArchivoImportaVentas.Name = "txtRutaArchivoImportaVentas";
            this.txtRutaArchivoImportaVentas.Size = new System.Drawing.Size(686, 21);
            this.txtRutaArchivoImportaVentas.TabIndex = 132;
            // 
            // btnBuscaArchivoImportaVentas
            // 
            this.btnBuscaArchivoImportaVentas.BackColor = System.Drawing.Color.White;
            this.btnBuscaArchivoImportaVentas.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscaArchivoImportaVentas.BackgroundImage")));
            this.btnBuscaArchivoImportaVentas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBuscaArchivoImportaVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscaArchivoImportaVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscaArchivoImportaVentas.Location = new System.Drawing.Point(694, 1);
            this.btnBuscaArchivoImportaVentas.Name = "btnBuscaArchivoImportaVentas";
            this.btnBuscaArchivoImportaVentas.Size = new System.Drawing.Size(37, 32);
            this.btnBuscaArchivoImportaVentas.TabIndex = 131;
            this.btnBuscaArchivoImportaVentas.UseVisualStyleBackColor = false;
            this.btnBuscaArchivoImportaVentas.Click += new System.EventHandler(this.btnBuscaArchivoImportaVentas_Click);
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
            // Sincronizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1379, 758);
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
            ((System.ComponentModel.ISupportInitialize)(this.gvProductosDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entProductoBindingSource)).EndInit();
            this.tpImportaVentas.ResumeLayout(false);
            this.tpImportaVentas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPedidosImporta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entCatalogoGenericoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcPedidosGrids;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.BindingSource entPedidoBindingSource;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Button btnVerFactura;
        private System.Windows.Forms.BindingSource entCatalogoGenericoBindingSource;
        private System.Windows.Forms.Button btnFiltrarCliente;
        private System.Windows.Forms.TextBox txtDescripcionFiltro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCantidadVentas;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cmbEmpresas;
        private System.Windows.Forms.Button btnBuscaEmpresa;
        private System.Windows.Forms.BindingSource entProductoBindingSource;
        private System.Windows.Forms.DataGridView gvProductosDetalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ingreso;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serie;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.TabPage tpImportaVentas;
        private System.Windows.Forms.Button btnImportaVentas;
        private System.Windows.Forms.TextBox txtRutaArchivoImportaVentas;
        private System.Windows.Forms.Button btnBuscaArchivoImportaVentas;
        private System.Windows.Forms.DataGridView gvPedidosImporta;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn clienteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn detalleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Factura;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Facturado;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaCorta;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstatusDescripcion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCantidadPedidosImporta;
    }
}