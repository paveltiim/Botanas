namespace Aires.Pantallas
{
    partial class Productos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Productos));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.entProductoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.entCatalogoGenericoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.entClienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnBuscaEmpresa = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.cmbEmpresas = new System.Windows.Forms.ComboBox();
            this.tcProductos = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkSoloExistencia = new System.Windows.Forms.CheckBox();
            this.txtCodigoBusqueda = new System.Windows.Forms.TextBox();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.gvProductos = new System.Windows.Forms.DataGridView();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcionDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Marca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClaveProductoServicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductoServicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClaveUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precioCostoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precioVentaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existenciaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbTituloIngreso = new System.Windows.Forms.Label();
            this.pnlDatos = new System.Windows.Forms.Panel();
            this.tcDatosProductoIngresa = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmbTipoUnidad = new System.Windows.Forms.ComboBox();
            this.cmbTipoProductoServicio = new System.Windows.Forms.ComboBox();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnEditaPrecioVenta = new System.Windows.Forms.Button();
            this.cmbTipoProducto = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrecioCosto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkPrecioCosto0 = new System.Windows.Forms.CheckBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnFiltroProducto = new System.Windows.Forms.Button();
            this.txtProductoBusqueda = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.entProductoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entCatalogoGenericoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entClienteBindingSource)).BeginInit();
            this.tcProductos.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductos)).BeginInit();
            this.pnlDatos.SuspendLayout();
            this.tcDatosProductoIngresa.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // entProductoBindingSource
            // 
            this.entProductoBindingSource.DataSource = typeof(AiresEntidades.EntProducto);
            // 
            // entCatalogoGenericoBindingSource
            // 
            this.entCatalogoGenericoBindingSource.DataSource = typeof(AiresEntidades.EntCatalogoGenerico);
            // 
            // entClienteBindingSource
            // 
            this.entClienteBindingSource.DataSource = typeof(AiresEntidades.EntCliente);
            // 
            // btnBuscaEmpresa
            // 
            this.btnBuscaEmpresa.BackColor = System.Drawing.Color.Transparent;
            this.btnBuscaEmpresa.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscaEmpresa.BackgroundImage")));
            this.btnBuscaEmpresa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBuscaEmpresa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscaEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscaEmpresa.Location = new System.Drawing.Point(1241, 8);
            this.btnBuscaEmpresa.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBuscaEmpresa.Name = "btnBuscaEmpresa";
            this.btnBuscaEmpresa.Size = new System.Drawing.Size(60, 42);
            this.btnBuscaEmpresa.TabIndex = 125;
            this.btnBuscaEmpresa.UseVisualStyleBackColor = false;
            this.btnBuscaEmpresa.Visible = false;
            this.btnBuscaEmpresa.Click += new System.EventHandler(this.btnBuscaEmpresa_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Location = new System.Drawing.Point(616, 22);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(77, 20);
            this.label21.TabIndex = 123;
            this.label21.Text = "Empresa:";
            this.label21.Visible = false;
            this.label21.Click += new System.EventHandler(this.label21_Click);
            // 
            // cmbEmpresas
            // 
            this.cmbEmpresas.DisplayMember = "Descripcion";
            this.cmbEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpresas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmpresas.FormattingEnabled = true;
            this.cmbEmpresas.Location = new System.Drawing.Point(693, 8);
            this.cmbEmpresas.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbEmpresas.Name = "cmbEmpresas";
            this.cmbEmpresas.Size = new System.Drawing.Size(536, 37);
            this.cmbEmpresas.TabIndex = 124;
            this.cmbEmpresas.ValueMember = "Id";
            this.cmbEmpresas.Visible = false;
            this.cmbEmpresas.SelectedIndexChanged += new System.EventHandler(this.cmbEmpresas_SelectedIndexChanged);
            // 
            // tcProductos
            // 
            this.tcProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcProductos.Controls.Add(this.tabPage2);
            this.tcProductos.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcProductos.Location = new System.Drawing.Point(18, 25);
            this.tcProductos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tcProductos.Name = "tcProductos";
            this.tcProductos.SelectedIndex = 0;
            this.tcProductos.Size = new System.Drawing.Size(1357, 840);
            this.tcProductos.TabIndex = 3;
            this.tcProductos.SelectedIndexChanged += new System.EventHandler(this.tcProductos_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackgroundImage = global::Aires.Properties.Resources.verdeClaro;
            this.tabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage2.Controls.Add(this.chkSoloExistencia);
            this.tabPage2.Controls.Add(this.txtCodigoBusqueda);
            this.tabPage2.Controls.Add(this.btnRefrescar);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Controls.Add(this.lbTituloIngreso);
            this.tabPage2.Controls.Add(this.pnlDatos);
            this.tabPage2.Controls.Add(this.btnFiltroProducto);
            this.tabPage2.Controls.Add(this.txtProductoBusqueda);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 31);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Size = new System.Drawing.Size(1349, 805);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Productos";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chkSoloExistencia
            // 
            this.chkSoloExistencia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSoloExistencia.AutoSize = true;
            this.chkSoloExistencia.Location = new System.Drawing.Point(455, 26);
            this.chkSoloExistencia.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkSoloExistencia.Name = "chkSoloExistencia";
            this.chkSoloExistencia.Size = new System.Drawing.Size(175, 29);
            this.chkSoloExistencia.TabIndex = 87;
            this.chkSoloExistencia.Text = "Solo Existencias";
            this.chkSoloExistencia.UseVisualStyleBackColor = true;
            this.chkSoloExistencia.Visible = false;
            this.chkSoloExistencia.CheckedChanged += new System.EventHandler(this.btnFiltroProducto_Click);
            // 
            // txtCodigoBusqueda
            // 
            this.txtCodigoBusqueda.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoBusqueda.Location = new System.Drawing.Point(15, 18);
            this.txtCodigoBusqueda.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCodigoBusqueda.Name = "txtCodigoBusqueda";
            this.txtCodigoBusqueda.Size = new System.Drawing.Size(133, 40);
            this.txtCodigoBusqueda.TabIndex = 86;
            this.txtCodigoBusqueda.TextChanged += new System.EventHandler(this.txtCodigoBusqueda_TextChanged);
            this.txtCodigoBusqueda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoBusqueda_KeyPress);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefrescar.BackColor = System.Drawing.Color.White;
            this.btnRefrescar.BackgroundImage = global::Aires.Properties.Resources.Refresh;
            this.btnRefrescar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescar.Location = new System.Drawing.Point(945, -505);
            this.btnRefrescar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(64, 51);
            this.btnRefrescar.TabIndex = 85;
            this.btnRefrescar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefrescar.UseVisualStyleBackColor = false;
            this.btnRefrescar.Visible = false;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.flowLayoutPanel1);
            this.panel3.Controls.Add(this.tabControl3);
            this.panel3.Font = new System.Drawing.Font("Microsoft Tai Le", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(9, 71);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(620, 726);
            this.panel3.TabIndex = 84;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.btnEditar);
            this.flowLayoutPanel1.Controls.Add(this.btnEliminar);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 611);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(608, 106);
            this.flowLayoutPanel1.TabIndex = 85;
            // 
            // btnEditar
            // 
            this.btnEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditar.BackColor = System.Drawing.Color.White;
            this.btnEditar.BackgroundImage = global::Aires.Properties.Resources.Editar;
            this.btnEditar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.Location = new System.Drawing.Point(4, 5);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(111, 101);
            this.btnEditar.TabIndex = 83;
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEliminar.BackColor = System.Drawing.Color.White;
            this.btnEliminar.BackgroundImage = global::Aires.Properties.Resources.Eliminar;
            this.btnEliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.Location = new System.Drawing.Point(123, 5);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(111, 101);
            this.btnEliminar.TabIndex = 84;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // tabControl3
            // 
            this.tabControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl3.Controls.Add(this.tabPage4);
            this.tabControl3.Font = new System.Drawing.Font("Microsoft Tai Le", 7F);
            this.tabControl3.Location = new System.Drawing.Point(-3, -1);
            this.tabControl3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(624, 616);
            this.tabControl3.TabIndex = 82;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.gvProductos);
            this.tabPage4.Location = new System.Drawing.Point(4, 27);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage4.Size = new System.Drawing.Size(616, 585);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Productos Registrados";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // gvProductos
            // 
            this.gvProductos.AllowUserToAddRows = false;
            this.gvProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvProductos.AutoGenerateColumns = false;
            this.gvProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvProductos.BackgroundColor = System.Drawing.Color.White;
            this.gvProductos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.descripcionDataGridViewTextBoxColumn1,
            this.Marca,
            this.Modelo,
            this.ClaveProductoServicio,
            this.ProductoServicio,
            this.ClaveUnidad,
            this.Unidad,
            this.precioCostoDataGridViewTextBoxColumn,
            this.precioVentaDataGridViewTextBoxColumn,
            this.existenciaDataGridViewTextBoxColumn});
            this.gvProductos.DataSource = this.entProductoBindingSource;
            this.gvProductos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvProductos.GridColor = System.Drawing.Color.DimGray;
            this.gvProductos.Location = new System.Drawing.Point(0, 0);
            this.gvProductos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gvProductos.Name = "gvProductos";
            this.gvProductos.ReadOnly = true;
            this.gvProductos.RowHeadersVisible = false;
            this.gvProductos.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvProductos.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvProductos.RowTemplate.Height = 27;
            this.gvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvProductos.Size = new System.Drawing.Size(612, 575);
            this.gvProductos.TabIndex = 78;
            this.gvProductos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvClientes_CellDoubleClick);
            this.gvProductos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvProductos_ColumnHeaderMouseClick);
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
            this.descripcionDataGridViewTextBoxColumn1.FillWeight = 8F;
            this.descripcionDataGridViewTextBoxColumn1.HeaderText = "Descripcion";
            this.descripcionDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.descripcionDataGridViewTextBoxColumn1.Name = "descripcionDataGridViewTextBoxColumn1";
            this.descripcionDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // Marca
            // 
            this.Marca.DataPropertyName = "Marca";
            this.Marca.FillWeight = 2F;
            this.Marca.HeaderText = "Marca";
            this.Marca.MinimumWidth = 6;
            this.Marca.Name = "Marca";
            this.Marca.ReadOnly = true;
            // 
            // Modelo
            // 
            this.Modelo.DataPropertyName = "Modelo";
            this.Modelo.FillWeight = 2F;
            this.Modelo.HeaderText = "Modelo";
            this.Modelo.MinimumWidth = 6;
            this.Modelo.Name = "Modelo";
            this.Modelo.ReadOnly = true;
            // 
            // ClaveProductoServicio
            // 
            this.ClaveProductoServicio.DataPropertyName = "ClaveProductoServicio";
            this.ClaveProductoServicio.FillWeight = 1F;
            this.ClaveProductoServicio.HeaderText = "Clave Producto/Servicio (SAT)";
            this.ClaveProductoServicio.MinimumWidth = 6;
            this.ClaveProductoServicio.Name = "ClaveProductoServicio";
            this.ClaveProductoServicio.ReadOnly = true;
            // 
            // ProductoServicio
            // 
            this.ProductoServicio.DataPropertyName = "ProductoServicio";
            this.ProductoServicio.FillWeight = 2F;
            this.ProductoServicio.HeaderText = "Producto/Servicio (SAT)";
            this.ProductoServicio.MinimumWidth = 6;
            this.ProductoServicio.Name = "ProductoServicio";
            this.ProductoServicio.ReadOnly = true;
            // 
            // ClaveUnidad
            // 
            this.ClaveUnidad.DataPropertyName = "ClaveUnidad";
            this.ClaveUnidad.FillWeight = 1F;
            this.ClaveUnidad.HeaderText = "Clave Unidad";
            this.ClaveUnidad.MinimumWidth = 6;
            this.ClaveUnidad.Name = "ClaveUnidad";
            this.ClaveUnidad.ReadOnly = true;
            // 
            // Unidad
            // 
            this.Unidad.DataPropertyName = "Unidad";
            this.Unidad.FillWeight = 2F;
            this.Unidad.HeaderText = "Unidad";
            this.Unidad.MinimumWidth = 6;
            this.Unidad.Name = "Unidad";
            this.Unidad.ReadOnly = true;
            // 
            // precioCostoDataGridViewTextBoxColumn
            // 
            this.precioCostoDataGridViewTextBoxColumn.DataPropertyName = "PrecioCosto";
            dataGridViewCellStyle1.Format = "c2";
            this.precioCostoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.precioCostoDataGridViewTextBoxColumn.FillWeight = 1F;
            this.precioCostoDataGridViewTextBoxColumn.HeaderText = "Precio Costo (Promedio)";
            this.precioCostoDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.precioCostoDataGridViewTextBoxColumn.Name = "precioCostoDataGridViewTextBoxColumn";
            this.precioCostoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // precioVentaDataGridViewTextBoxColumn
            // 
            this.precioVentaDataGridViewTextBoxColumn.DataPropertyName = "PrecioVenta";
            dataGridViewCellStyle2.Format = "c2";
            this.precioVentaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.precioVentaDataGridViewTextBoxColumn.FillWeight = 1F;
            this.precioVentaDataGridViewTextBoxColumn.HeaderText = "Precio Venta (Promedio)";
            this.precioVentaDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.precioVentaDataGridViewTextBoxColumn.Name = "precioVentaDataGridViewTextBoxColumn";
            this.precioVentaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // existenciaDataGridViewTextBoxColumn
            // 
            this.existenciaDataGridViewTextBoxColumn.DataPropertyName = "Existencia";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.existenciaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.existenciaDataGridViewTextBoxColumn.FillWeight = 0.5F;
            this.existenciaDataGridViewTextBoxColumn.HeaderText = "Exist.";
            this.existenciaDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.existenciaDataGridViewTextBoxColumn.Name = "existenciaDataGridViewTextBoxColumn";
            this.existenciaDataGridViewTextBoxColumn.ReadOnly = true;
            this.existenciaDataGridViewTextBoxColumn.Visible = false;
            // 
            // lbTituloIngreso
            // 
            this.lbTituloIngreso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTituloIngreso.BackColor = System.Drawing.Color.White;
            this.lbTituloIngreso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTituloIngreso.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTituloIngreso.Location = new System.Drawing.Point(633, 2);
            this.lbTituloIngreso.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTituloIngreso.Name = "lbTituloIngreso";
            this.lbTituloIngreso.Size = new System.Drawing.Size(244, 42);
            this.lbTituloIngreso.TabIndex = 83;
            this.lbTituloIngreso.Text = "Datos de Producto";
            this.lbTituloIngreso.Click += new System.EventHandler(this.label23_Click);
            // 
            // pnlDatos
            // 
            this.pnlDatos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDatos.BackColor = System.Drawing.Color.White;
            this.pnlDatos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlDatos.Controls.Add(this.tcDatosProductoIngresa);
            this.pnlDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlDatos.Location = new System.Drawing.Point(633, 44);
            this.pnlDatos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlDatos.Name = "pnlDatos";
            this.pnlDatos.Size = new System.Drawing.Size(710, 752);
            this.pnlDatos.TabIndex = 81;
            // 
            // tcDatosProductoIngresa
            // 
            this.tcDatosProductoIngresa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tcDatosProductoIngresa.Controls.Add(this.tabPage1);
            this.tcDatosProductoIngresa.Location = new System.Drawing.Point(-1, 58);
            this.tcDatosProductoIngresa.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tcDatosProductoIngresa.Name = "tcDatosProductoIngresa";
            this.tcDatosProductoIngresa.SelectedIndex = 0;
            this.tcDatosProductoIngresa.Size = new System.Drawing.Size(703, 698);
            this.tcDatosProductoIngresa.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cmbTipoUnidad);
            this.tabPage1.Controls.Add(this.cmbTipoProductoServicio);
            this.tabPage1.Controls.Add(this.txtModelo);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.txtMarca);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.btnEditaPrecioVenta);
            this.tabPage1.Controls.Add(this.cmbTipoProducto);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.btnCancelar);
            this.tabPage1.Controls.Add(this.txtCodigo);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtPrecioCosto);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtDescripcion);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.chkPrecioCosto0);
            this.tabPage1.Controls.Add(this.btnAgregar);
            this.tabPage1.Controls.Add(this.btnActualizar);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(695, 660);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Producto";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // cmbTipoUnidad
            // 
            this.cmbTipoUnidad.DataSource = this.entCatalogoGenericoBindingSource;
            this.cmbTipoUnidad.DisplayMember = "Descripcion";
            this.cmbTipoUnidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoUnidad.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbTipoUnidad.Font = new System.Drawing.Font("Microsoft Tai Le", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoUnidad.FormattingEnabled = true;
            this.cmbTipoUnidad.Location = new System.Drawing.Point(129, 190);
            this.cmbTipoUnidad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbTipoUnidad.Name = "cmbTipoUnidad";
            this.cmbTipoUnidad.Size = new System.Drawing.Size(509, 29);
            this.cmbTipoUnidad.TabIndex = 4;
            this.cmbTipoUnidad.ValueMember = "Id";
            // 
            // cmbTipoProductoServicio
            // 
            this.cmbTipoProductoServicio.DataSource = this.entCatalogoGenericoBindingSource;
            this.cmbTipoProductoServicio.DisplayMember = "Descripcion";
            this.cmbTipoProductoServicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoProductoServicio.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbTipoProductoServicio.Font = new System.Drawing.Font("Microsoft Tai Le", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoProductoServicio.FormattingEnabled = true;
            this.cmbTipoProductoServicio.Location = new System.Drawing.Point(129, 134);
            this.cmbTipoProductoServicio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbTipoProductoServicio.Name = "cmbTipoProductoServicio";
            this.cmbTipoProductoServicio.Size = new System.Drawing.Size(509, 29);
            this.cmbTipoProductoServicio.TabIndex = 3;
            this.cmbTipoProductoServicio.ValueMember = "Id";
            // 
            // txtModelo
            // 
            this.txtModelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModelo.Location = new System.Drawing.Point(129, 300);
            this.txtModelo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.Size = new System.Drawing.Size(291, 30);
            this.txtModelo.TabIndex = 6;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(46, 304);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(83, 25);
            this.label17.TabIndex = 153;
            this.label17.Text = "Modelo:";
            // 
            // txtMarca
            // 
            this.txtMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMarca.Location = new System.Drawing.Point(129, 244);
            this.txtMarca.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.Size = new System.Drawing.Size(291, 30);
            this.txtMarca.TabIndex = 5;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(53, 248);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(73, 25);
            this.label16.TabIndex = 151;
            this.label16.Text = "Marca:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label11.Location = new System.Drawing.Point(47, 190);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 25);
            this.label11.TabIndex = 145;
            this.label11.Text = "Unidad:";
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label15.Location = new System.Drawing.Point(27, 104);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(150, 62);
            this.label15.TabIndex = 144;
            this.label15.Text = "Producto/Servicio: (SAT)";
            // 
            // btnEditaPrecioVenta
            // 
            this.btnEditaPrecioVenta.BackColor = System.Drawing.Color.White;
            this.btnEditaPrecioVenta.BackgroundImage = global::Aires.Properties.Resources.editar2;
            this.btnEditaPrecioVenta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEditaPrecioVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditaPrecioVenta.Font = new System.Drawing.Font("Segoe UI", 5.75F, System.Drawing.FontStyle.Bold);
            this.btnEditaPrecioVenta.Location = new System.Drawing.Point(328, 354);
            this.btnEditaPrecioVenta.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEditaPrecioVenta.Name = "btnEditaPrecioVenta";
            this.btnEditaPrecioVenta.Size = new System.Drawing.Size(92, 88);
            this.btnEditaPrecioVenta.TabIndex = 114;
            this.btnEditaPrecioVenta.Text = "Actualizar Precios Venta";
            this.btnEditaPrecioVenta.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEditaPrecioVenta.UseVisualStyleBackColor = false;
            this.btnEditaPrecioVenta.Click += new System.EventHandler(this.btnEditaPrecioVenta_Click);
            // 
            // cmbTipoProducto
            // 
            this.cmbTipoProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoProducto.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbTipoProducto.Font = new System.Drawing.Font("Microsoft Tai Le", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoProducto.FormattingEnabled = true;
            this.cmbTipoProducto.Items.AddRange(new object[] {
            "Producto",
            "Servicio"});
            this.cmbTipoProducto.Location = new System.Drawing.Point(487, 11);
            this.cmbTipoProducto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbTipoProducto.Name = "cmbTipoProducto";
            this.cmbTipoProducto.Size = new System.Drawing.Size(151, 29);
            this.cmbTipoProducto.TabIndex = 1;
            this.cmbTipoProducto.SelectedIndexChanged += new System.EventHandler(this.cmbTipoProducto_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(420, 16);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 25);
            this.label10.TabIndex = 111;
            this.label10.Text = "Tipo:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.BackgroundImage = global::Aires.Properties.Resources.Cancelar;
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(397, 545);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(111, 101);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.Location = new System.Drawing.Point(129, 9);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(210, 30);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            this.txtCodigo.Leave += new System.EventHandler(this.txtCodigo_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 25);
            this.label3.TabIndex = 102;
            this.label3.Text = "Código:";
            // 
            // txtPrecioCosto
            // 
            this.txtPrecioCosto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioCosto.Location = new System.Drawing.Point(129, 394);
            this.txtPrecioCosto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPrecioCosto.Name = "txtPrecioCosto";
            this.txtPrecioCosto.Size = new System.Drawing.Size(145, 30);
            this.txtPrecioCosto.TabIndex = 7;
            this.txtPrecioCosto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecioCosto_KeyPress);
            this.txtPrecioCosto.Leave += new System.EventHandler(this.txtDireccion_Leave);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(52, 389);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 60);
            this.label1.TabIndex = 94;
            this.label1.Text = "Precio: Costo";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(129, 60);
            this.txtDescripcion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(560, 30);
            this.txtDescripcion.TabIndex = 2;
            this.txtDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecioCosto_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 25);
            this.label2.TabIndex = 88;
            this.label2.Text = "Descripción:";
            // 
            // chkPrecioCosto0
            // 
            this.chkPrecioCosto0.AutoSize = true;
            this.chkPrecioCosto0.Location = new System.Drawing.Point(101, 354);
            this.chkPrecioCosto0.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkPrecioCosto0.Name = "chkPrecioCosto0";
            this.chkPrecioCosto0.Size = new System.Drawing.Size(177, 29);
            this.chkPrecioCosto0.TabIndex = 109;
            this.chkPrecioCosto0.Text = "Precio Costo $0";
            this.chkPrecioCosto0.UseVisualStyleBackColor = true;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAgregar.BackColor = System.Drawing.Color.White;
            this.btnAgregar.BackgroundImage = global::Aires.Properties.Resources.Aceptar;
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(214, 545);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(111, 101);
            this.btnAgregar.TabIndex = 0;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnActualizar.BackColor = System.Drawing.Color.White;
            this.btnActualizar.BackgroundImage = global::Aires.Properties.Resources.Refrescar;
            this.btnActualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.Location = new System.Drawing.Point(214, 544);
            this.btnActualizar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(111, 101);
            this.btnActualizar.TabIndex = 3;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Visible = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnFiltroProducto
            // 
            this.btnFiltroProducto.BackColor = System.Drawing.Color.Transparent;
            this.btnFiltroProducto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFiltroProducto.BackgroundImage")));
            this.btnFiltroProducto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFiltroProducto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltroProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltroProducto.Location = new System.Drawing.Point(685, 20);
            this.btnFiltroProducto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnFiltroProducto.Name = "btnFiltroProducto";
            this.btnFiltroProducto.Size = new System.Drawing.Size(55, 40);
            this.btnFiltroProducto.TabIndex = 80;
            this.btnFiltroProducto.UseVisualStyleBackColor = false;
            this.btnFiltroProducto.Click += new System.EventHandler(this.btnFiltroProducto_Click);
            // 
            // txtProductoBusqueda
            // 
            this.txtProductoBusqueda.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductoBusqueda.Location = new System.Drawing.Point(153, 18);
            this.txtProductoBusqueda.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtProductoBusqueda.Name = "txtProductoBusqueda";
            this.txtProductoBusqueda.Size = new System.Drawing.Size(525, 40);
            this.txtProductoBusqueda.TabIndex = 79;
            this.txtProductoBusqueda.TextChanged += new System.EventHandler(this.txtProductoBusqueda_TextChanged);
            this.txtProductoBusqueda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProductoBusqueda_KeyPress);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Productos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1404, 881);
            this.Controls.Add(this.btnBuscaEmpresa);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.cmbEmpresas);
            this.Controls.Add(this.tcProductos);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Productos";
            this.ShowIcon = false;
            this.Text = "Productos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Productos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.entProductoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entCatalogoGenericoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entClienteBindingSource)).EndInit();
            this.tcProductos.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvProductos)).EndInit();
            this.pnlDatos.ResumeLayout(false);
            this.tcDatosProductoIngresa.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcProductos;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel pnlDatos;
        private System.Windows.Forms.Button btnFiltroProducto;
        private System.Windows.Forms.TextBox txtProductoBusqueda;
        private System.Windows.Forms.DataGridView gvProductos;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.TabControl tcDatosProductoIngresa;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtPrecioCosto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource entClienteBindingSource;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Label lbTituloIngreso;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.BindingSource entCatalogoGenericoBindingSource;
        private System.Windows.Forms.BindingSource entProductoBindingSource;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtCodigoBusqueda;
        private System.Windows.Forms.CheckBox chkPrecioCosto0;
        private System.Windows.Forms.ComboBox cmbTipoProducto;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnBuscaEmpresa;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cmbEmpresas;
        private System.Windows.Forms.Button btnEditaPrecioVenta;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbTipoUnidad;
        private System.Windows.Forms.ComboBox cmbTipoProductoServicio;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox chkSoloExistencia;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Marca;
        private System.Windows.Forms.DataGridViewTextBoxColumn Modelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClaveProductoServicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductoServicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClaveUnidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn precioCostoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn precioVentaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn existenciaDataGridViewTextBoxColumn;
    }
}