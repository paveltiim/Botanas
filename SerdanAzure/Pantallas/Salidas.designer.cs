namespace Aires.Pantallas
{
    partial class Salidas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Salidas));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label24 = new System.Windows.Forms.Label();
            this.cmbEmpresas = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbAlmacenes = new System.Windows.Forms.ComboBox();
            this.pnlProductos = new System.Windows.Forms.Panel();
            this.btnBuscarProducto = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.lbContadorSeries = new System.Windows.Forms.Label();
            this.txtBuscaProductoSerie = new System.Windows.Forms.TextBox();
            this.btnRefrescarProductos = new System.Windows.Forms.Button();
            this.txtBuscaProducto = new System.Windows.Forms.TextBox();
            this.txtBuscaProductoCodigo = new System.Windows.Forms.TextBox();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.gvProductosBusqueda = new System.Windows.Forms.DataGridView();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcionDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioCosto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Existencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entProductoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvProductosPedido = new System.Windows.Forms.DataGridView();
            this.colEliminar = new System.Windows.Forms.DataGridViewImageColumn();
            this.codigoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidadDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.pnlAgrega = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDescuento = new System.Windows.Forms.TextBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnRefrescaEmpresa = new System.Windows.Forms.Button();
            this.btnBuscaEmpresa = new System.Windows.Forms.Button();
            this.entClienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlProductos.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductosBusqueda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entProductoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductosPedido)).BeginInit();
            this.pnlAgrega.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entClienteBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label24
            // 
            this.label24.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Location = new System.Drawing.Point(361, 17);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(68, 17);
            this.label24.TabIndex = 115;
            this.label24.Text = "Empresa:";
            this.label24.Visible = false;
            // 
            // cmbEmpresas
            // 
            this.cmbEmpresas.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbEmpresas.DisplayMember = "Descripcion";
            this.cmbEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpresas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmpresas.FormattingEnabled = true;
            this.cmbEmpresas.Location = new System.Drawing.Point(429, 5);
            this.cmbEmpresas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbEmpresas.Name = "cmbEmpresas";
            this.cmbEmpresas.Size = new System.Drawing.Size(477, 33);
            this.cmbEmpresas.TabIndex = 116;
            this.cmbEmpresas.ValueMember = "Id";
            this.cmbEmpresas.Visible = false;
            this.cmbEmpresas.SelectedIndexChanged += new System.EventHandler(this.cmbEmpresas_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 41);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1240, 565);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage2
            // 
            this.tabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(1232, 533);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Salida";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.cmbAlmacenes);
            this.panel3.Controls.Add(this.pnlProductos);
            this.panel3.Controls.Add(this.pnlAgrega);
            this.panel3.Location = new System.Drawing.Point(8, 7);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1218, 526);
            this.panel3.TabIndex = 84;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(21, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 117;
            this.label1.Text = "Almacen:";
            // 
            // cmbAlmacenes
            // 
            this.cmbAlmacenes.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbAlmacenes.DisplayMember = "Descripcion";
            this.cmbAlmacenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlmacenes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbAlmacenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAlmacenes.FormattingEnabled = true;
            this.cmbAlmacenes.Items.AddRange(new object[] {
            "1 - ALMACEN MATRIZ",
            "2 - PUNTO VENTA"});
            this.cmbAlmacenes.Location = new System.Drawing.Point(108, 18);
            this.cmbAlmacenes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbAlmacenes.Name = "cmbAlmacenes";
            this.cmbAlmacenes.Size = new System.Drawing.Size(292, 33);
            this.cmbAlmacenes.TabIndex = 118;
            this.cmbAlmacenes.ValueMember = "Id";
            this.cmbAlmacenes.SelectedIndexChanged += new System.EventHandler(this.cmbAlmacenes_SelectedIndexChanged);
            // 
            // pnlProductos
            // 
            this.pnlProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlProductos.Controls.Add(this.btnRefrescarProductos);
            this.pnlProductos.Controls.Add(this.btnBuscarProducto);
            this.pnlProductos.Controls.Add(this.label22);
            this.pnlProductos.Controls.Add(this.lbContadorSeries);
            this.pnlProductos.Controls.Add(this.txtBuscaProductoSerie);
            this.pnlProductos.Controls.Add(this.txtBuscaProducto);
            this.pnlProductos.Controls.Add(this.txtBuscaProductoCodigo);
            this.pnlProductos.Controls.Add(this.tabControl3);
            this.pnlProductos.Location = new System.Drawing.Point(3, 59);
            this.pnlProductos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlProductos.Name = "pnlProductos";
            this.pnlProductos.Size = new System.Drawing.Size(1208, 326);
            this.pnlProductos.TabIndex = 2;
            // 
            // btnBuscarProducto
            // 
            this.btnBuscarProducto.BackColor = System.Drawing.Color.White;
            this.btnBuscarProducto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscarProducto.BackgroundImage")));
            this.btnBuscarProducto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBuscarProducto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarProducto.Location = new System.Drawing.Point(748, 6);
            this.btnBuscarProducto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBuscarProducto.Name = "btnBuscarProducto";
            this.btnBuscarProducto.Size = new System.Drawing.Size(60, 36);
            this.btnBuscarProducto.TabIndex = 13;
            this.btnBuscarProducto.UseVisualStyleBackColor = false;
            this.btnBuscarProducto.Click += new System.EventHandler(this.btnBuscarProducto_Click);
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(1069, 272);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(129, 20);
            this.label22.TabIndex = 109;
            this.label22.Text = "Núm. Productos";
            // 
            // lbContadorSeries
            // 
            this.lbContadorSeries.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbContadorSeries.AutoSize = true;
            this.lbContadorSeries.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbContadorSeries.Location = new System.Drawing.Point(1069, 293);
            this.lbContadorSeries.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbContadorSeries.Name = "lbContadorSeries";
            this.lbContadorSeries.Size = new System.Drawing.Size(18, 20);
            this.lbContadorSeries.TabIndex = 108;
            this.lbContadorSeries.Text = "0";
            // 
            // txtBuscaProductoSerie
            // 
            this.txtBuscaProductoSerie.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscaProductoSerie.Location = new System.Drawing.Point(748, 8);
            this.txtBuscaProductoSerie.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBuscaProductoSerie.Name = "txtBuscaProductoSerie";
            this.txtBuscaProductoSerie.Size = new System.Drawing.Size(292, 35);
            this.txtBuscaProductoSerie.TabIndex = 12;
            this.txtBuscaProductoSerie.Visible = false;
            this.txtBuscaProductoSerie.TextChanged += new System.EventHandler(this.txtBuscaProductoSerie_TextChanged);
            this.txtBuscaProductoSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProductoBusqueda_KeyPress);
            // 
            // btnRefrescarProductos
            // 
            this.btnRefrescarProductos.BackColor = System.Drawing.Color.White;
            this.btnRefrescarProductos.BackgroundImage = global::Aires.Properties.Resources.Refrescar;
            this.btnRefrescarProductos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRefrescarProductos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescarProductos.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescarProductos.Location = new System.Drawing.Point(811, 2);
            this.btnRefrescarProductos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRefrescarProductos.Name = "btnRefrescarProductos";
            this.btnRefrescarProductos.Size = new System.Drawing.Size(57, 41);
            this.btnRefrescarProductos.TabIndex = 110;
            this.btnRefrescarProductos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefrescarProductos.UseVisualStyleBackColor = false;
            this.btnRefrescarProductos.Click += new System.EventHandler(this.btnRefrescarProductos_Click);
            // 
            // txtBuscaProducto
            // 
            this.txtBuscaProducto.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscaProducto.Location = new System.Drawing.Point(260, 8);
            this.txtBuscaProducto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBuscaProducto.Name = "txtBuscaProducto";
            this.txtBuscaProducto.Size = new System.Drawing.Size(485, 35);
            this.txtBuscaProducto.TabIndex = 11;
            this.txtBuscaProducto.TextChanged += new System.EventHandler(this.txtProductoBusqueda_TextChanged);
            this.txtBuscaProducto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProductoBusqueda_KeyPress);
            // 
            // txtBuscaProductoCodigo
            // 
            this.txtBuscaProductoCodigo.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscaProductoCodigo.Location = new System.Drawing.Point(108, 8);
            this.txtBuscaProductoCodigo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBuscaProductoCodigo.Name = "txtBuscaProductoCodigo";
            this.txtBuscaProductoCodigo.Size = new System.Drawing.Size(148, 35);
            this.txtBuscaProductoCodigo.TabIndex = 10;
            this.txtBuscaProductoCodigo.TextChanged += new System.EventHandler(this.txtCodigoProductoBusqueda_TextChanged);
            this.txtBuscaProductoCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProductoBusqueda_KeyPress);
            // 
            // tabControl3
            // 
            this.tabControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl3.Controls.Add(this.tabPage4);
            this.tabControl3.Controls.Add(this.tabPage5);
            this.tabControl3.Location = new System.Drawing.Point(3, 15);
            this.tabControl3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(1062, 306);
            this.tabControl3.TabIndex = 82;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.gvProductosBusqueda);
            this.tabPage4.Controls.Add(this.gvProductosPedido);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage4.Size = new System.Drawing.Size(1054, 273);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Productos";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // gvProductosBusqueda
            // 
            this.gvProductosBusqueda.AllowUserToAddRows = false;
            this.gvProductosBusqueda.AutoGenerateColumns = false;
            this.gvProductosBusqueda.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvProductosBusqueda.BackgroundColor = System.Drawing.Color.White;
            this.gvProductosBusqueda.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvProductosBusqueda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProductosBusqueda.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.descripcionDataGridViewTextBoxColumn1,
            this.PrecioCosto,
            this.Existencia});
            this.gvProductosBusqueda.DataSource = this.entProductoBindingSource;
            this.gvProductosBusqueda.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvProductosBusqueda.GridColor = System.Drawing.Color.DimGray;
            this.gvProductosBusqueda.Location = new System.Drawing.Point(98, -1);
            this.gvProductosBusqueda.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gvProductosBusqueda.MultiSelect = false;
            this.gvProductosBusqueda.Name = "gvProductosBusqueda";
            this.gvProductosBusqueda.ReadOnly = true;
            this.gvProductosBusqueda.RowHeadersVisible = false;
            this.gvProductosBusqueda.RowHeadersWidth = 51;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvProductosBusqueda.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvProductosBusqueda.RowTemplate.Height = 27;
            this.gvProductosBusqueda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvProductosBusqueda.Size = new System.Drawing.Size(933, 214);
            this.gvProductosBusqueda.TabIndex = 92;
            this.gvProductosBusqueda.Visible = false;
            this.gvProductosBusqueda.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProductos_CellDoubleClick);
            this.gvProductosBusqueda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gvProductosBusqueda_KeyPress);
            // 
            // Codigo
            // 
            this.Codigo.DataPropertyName = "Codigo";
            this.Codigo.FillWeight = 1.5F;
            this.Codigo.HeaderText = "Codigo";
            this.Codigo.MinimumWidth = 6;
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            // 
            // descripcionDataGridViewTextBoxColumn1
            // 
            this.descripcionDataGridViewTextBoxColumn1.DataPropertyName = "Descripcion";
            this.descripcionDataGridViewTextBoxColumn1.FillWeight = 5F;
            this.descripcionDataGridViewTextBoxColumn1.HeaderText = "Descripcion";
            this.descripcionDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.descripcionDataGridViewTextBoxColumn1.Name = "descripcionDataGridViewTextBoxColumn1";
            this.descripcionDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // PrecioCosto
            // 
            this.PrecioCosto.DataPropertyName = "PrecioCosto";
            this.PrecioCosto.FillWeight = 2F;
            this.PrecioCosto.HeaderText = "Precio Costo";
            this.PrecioCosto.MinimumWidth = 8;
            this.PrecioCosto.Name = "PrecioCosto";
            this.PrecioCosto.ReadOnly = true;
            // 
            // Existencia
            // 
            this.Existencia.DataPropertyName = "Existencia";
            this.Existencia.FillWeight = 1F;
            this.Existencia.HeaderText = "Existencia";
            this.Existencia.MinimumWidth = 8;
            this.Existencia.Name = "Existencia";
            this.Existencia.ReadOnly = true;
            // 
            // entProductoBindingSource
            // 
            this.entProductoBindingSource.DataSource = typeof(AiresEntidades.EntProducto);
            // 
            // gvProductosPedido
            // 
            this.gvProductosPedido.AllowUserToAddRows = false;
            this.gvProductosPedido.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvProductosPedido.AutoGenerateColumns = false;
            this.gvProductosPedido.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvProductosPedido.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvProductosPedido.BackgroundColor = System.Drawing.Color.White;
            this.gvProductosPedido.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvProductosPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProductosPedido.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colEliminar,
            this.codigoDataGridViewTextBoxColumn,
            this.descripcionDataGridViewTextBoxColumn,
            this.cantidadDataGridViewTextBoxColumn,
            this.Precio,
            this.PrecioC});
            this.gvProductosPedido.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gvProductosPedido.DataSource = this.entProductoBindingSource;
            this.gvProductosPedido.GridColor = System.Drawing.Color.DimGray;
            this.gvProductosPedido.Location = new System.Drawing.Point(0, -1);
            this.gvProductosPedido.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gvProductosPedido.MultiSelect = false;
            this.gvProductosPedido.Name = "gvProductosPedido";
            this.gvProductosPedido.RowHeadersVisible = false;
            this.gvProductosPedido.RowHeadersWidth = 51;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvProductosPedido.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.gvProductosPedido.RowTemplate.Height = 27;
            this.gvProductosPedido.Size = new System.Drawing.Size(1055, 276);
            this.gvProductosPedido.TabIndex = 78;
            this.gvProductosPedido.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProductosPedido_CellContentClick);
            this.gvProductosPedido.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProductosPedido_CellContentDoubleClick);
            this.gvProductosPedido.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProductosPedido_CellEndEdit);
            this.gvProductosPedido.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProductosPedido_CellValueChanged);
            // 
            // colEliminar
            // 
            this.colEliminar.Description = "Elimina el registro del producto seleccionado";
            this.colEliminar.FillWeight = 0.5F;
            this.colEliminar.HeaderText = "Elim.";
            this.colEliminar.Image = ((System.Drawing.Image)(resources.GetObject("colEliminar.Image")));
            this.colEliminar.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colEliminar.MinimumWidth = 6;
            this.colEliminar.Name = "colEliminar";
            this.colEliminar.ToolTipText = "Elimina el registro del producto seleccionado";
            // 
            // codigoDataGridViewTextBoxColumn
            // 
            this.codigoDataGridViewTextBoxColumn.DataPropertyName = "Codigo";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.codigoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.codigoDataGridViewTextBoxColumn.FillWeight = 1F;
            this.codigoDataGridViewTextBoxColumn.HeaderText = "Codigo";
            this.codigoDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.codigoDataGridViewTextBoxColumn.Name = "codigoDataGridViewTextBoxColumn";
            this.codigoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descripcionDataGridViewTextBoxColumn
            // 
            this.descripcionDataGridViewTextBoxColumn.DataPropertyName = "Descripcion";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.75F);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.descripcionDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.descripcionDataGridViewTextBoxColumn.FillWeight = 5F;
            this.descripcionDataGridViewTextBoxColumn.HeaderText = "Descripcion";
            this.descripcionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.descripcionDataGridViewTextBoxColumn.Name = "descripcionDataGridViewTextBoxColumn";
            this.descripcionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cantidadDataGridViewTextBoxColumn
            // 
            this.cantidadDataGridViewTextBoxColumn.DataPropertyName = "Cantidad";
            this.cantidadDataGridViewTextBoxColumn.FillWeight = 0.6F;
            this.cantidadDataGridViewTextBoxColumn.HeaderText = "Cant.";
            this.cantidadDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.cantidadDataGridViewTextBoxColumn.Name = "cantidadDataGridViewTextBoxColumn";
            // 
            // Precio
            // 
            this.Precio.DataPropertyName = "PrecioCosto";
            dataGridViewCellStyle4.Format = "c2";
            this.Precio.DefaultCellStyle = dataGridViewCellStyle4;
            this.Precio.FillWeight = 1.1F;
            this.Precio.HeaderText = "Precio Costo";
            this.Precio.MinimumWidth = 6;
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            // 
            // PrecioC
            // 
            this.PrecioC.DataPropertyName = "PrecioC";
            dataGridViewCellStyle5.Format = "c2";
            this.PrecioC.DefaultCellStyle = dataGridViewCellStyle5;
            this.PrecioC.FillWeight = 1.5F;
            this.PrecioC.HeaderText = "Precio Total";
            this.PrecioC.MinimumWidth = 6;
            this.PrecioC.Name = "PrecioC";
            this.PrecioC.ReadOnly = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 29);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage5.Size = new System.Drawing.Size(1054, 273);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // pnlAgrega
            // 
            this.pnlAgrega.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAgrega.Controls.Add(this.btnCancelar);
            this.pnlAgrega.Controls.Add(this.btnAgregar);
            this.pnlAgrega.Controls.Add(this.txtTotal);
            this.pnlAgrega.Controls.Add(this.label7);
            this.pnlAgrega.Controls.Add(this.txtDescuento);
            this.pnlAgrega.Location = new System.Drawing.Point(712, 381);
            this.pnlAgrega.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlAgrega.Name = "pnlAgrega";
            this.pnlAgrega.Size = new System.Drawing.Size(357, 141);
            this.pnlAgrega.TabIndex = 108;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.BackgroundImage = global::Aires.Properties.Resources.Cancelar;
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(255, 55);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(99, 81);
            this.btnCancelar.TabIndex = 51;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregar.BackColor = System.Drawing.Color.White;
            this.btnAgregar.BackgroundImage = global::Aires.Properties.Resources.Aceptar;
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(122, 55);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(99, 81);
            this.btnAgregar.TabIndex = 50;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotal.Location = new System.Drawing.Point(262, 13);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(91, 26);
            this.txtTotal.TabIndex = 85;
            this.txtTotal.TabStop = false;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(153, 16);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 20);
            this.label7.TabIndex = 86;
            this.label7.Text = "Total Costo";
            // 
            // txtDescuento
            // 
            this.txtDescuento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDescuento.Location = new System.Drawing.Point(262, 13);
            this.txtDescuento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.Size = new System.Drawing.Size(91, 26);
            this.txtDescuento.TabIndex = 96;
            this.txtDescuento.Visible = false;
            this.txtDescuento.TextChanged += new System.EventHandler(this.txtDescuento_TextChanged);
            this.txtDescuento.Leave += new System.EventHandler(this.txtDescuento_Leave);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.Description = "Elimina el registro del producto seleccionado";
            this.dataGridViewImageColumn1.FillWeight = 1F;
            this.dataGridViewImageColumn1.HeaderText = "Elim.";
            this.dataGridViewImageColumn1.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn1.Image")));
            this.dataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn1.MinimumWidth = 6;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ToolTipText = "Elimina el registro del producto seleccionado";
            this.dataGridViewImageColumn1.Width = 228;
            // 
            // btnRefrescaEmpresa
            // 
            this.btnRefrescaEmpresa.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRefrescaEmpresa.BackColor = System.Drawing.Color.White;
            this.btnRefrescaEmpresa.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefrescaEmpresa.BackgroundImage")));
            this.btnRefrescaEmpresa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRefrescaEmpresa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescaEmpresa.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescaEmpresa.Location = new System.Drawing.Point(975, 1);
            this.btnRefrescaEmpresa.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRefrescaEmpresa.Name = "btnRefrescaEmpresa";
            this.btnRefrescaEmpresa.Size = new System.Drawing.Size(49, 39);
            this.btnRefrescaEmpresa.TabIndex = 133;
            this.btnRefrescaEmpresa.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefrescaEmpresa.UseVisualStyleBackColor = false;
            this.btnRefrescaEmpresa.Visible = false;
            this.btnRefrescaEmpresa.Click += new System.EventHandler(this.btnRefrescaEmpresa_Click);
            // 
            // btnBuscaEmpresa
            // 
            this.btnBuscaEmpresa.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBuscaEmpresa.BackColor = System.Drawing.Color.Transparent;
            this.btnBuscaEmpresa.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscaEmpresa.BackgroundImage")));
            this.btnBuscaEmpresa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBuscaEmpresa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscaEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscaEmpresa.Location = new System.Drawing.Point(916, 5);
            this.btnBuscaEmpresa.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBuscaEmpresa.Name = "btnBuscaEmpresa";
            this.btnBuscaEmpresa.Size = new System.Drawing.Size(53, 34);
            this.btnBuscaEmpresa.TabIndex = 114;
            this.btnBuscaEmpresa.UseVisualStyleBackColor = false;
            this.btnBuscaEmpresa.Visible = false;
            this.btnBuscaEmpresa.Click += new System.EventHandler(this.btnBuscaEmpresa_Click);
            // 
            // entClienteBindingSource
            // 
            this.entClienteBindingSource.DataSource = typeof(AiresEntidades.EntCliente);
            // 
            // Salidas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImage = global::Aires.Properties.Resources.rojo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1263, 617);
            this.Controls.Add(this.btnRefrescaEmpresa);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.cmbEmpresas);
            this.Controls.Add(this.btnBuscaEmpresa);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Salidas";
            this.Text = "Salidas";
            this.Load += new System.EventHandler(this.Ventas_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlProductos.ResumeLayout(false);
            this.pnlProductos.PerformLayout();
            this.tabControl3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvProductosBusqueda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entProductoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductosPedido)).EndInit();
            this.pnlAgrega.ResumeLayout(false);
            this.pnlAgrega.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entClienteBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.BindingSource entProductoBindingSource;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnBuscarProducto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBuscaProducto;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView gvProductosBusqueda;
        private System.Windows.Forms.DataGridView gvProductosPedido;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.BindingSource entClienteBindingSource;
        private System.Windows.Forms.Panel pnlProductos;
        private System.Windows.Forms.Panel pnlAgrega;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtBuscaProductoCodigo;
        private System.Windows.Forms.TextBox txtBuscaProductoSerie;
        private System.Windows.Forms.Label lbContadorSeries;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cmbEmpresas;
        private System.Windows.Forms.Button btnBuscaEmpresa;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.Button btnRefrescarProductos;
        private System.Windows.Forms.Button btnRefrescaEmpresa;
        private System.Windows.Forms.TextBox txtDescuento;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbAlmacenes;
        private System.Windows.Forms.DataGridViewImageColumn colEliminar;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidadDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioCosto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Existencia;
    }
}