namespace Aires.Pantallas
{
    partial class MuestraProductosDetalleFueraDeServicio
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.txtFiltroSerie = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.gvProductosDetalle = new System.Windows.Forms.DataGridView();
            this.pnlBotones = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnMueveAConsignacion = new System.Windows.Forms.Button();
            this.btnMueveAIngreso = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.btnFueraDeServicio = new System.Windows.Forms.Button();
            this.entProductoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.estatusDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ingreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductosDetalle)).BeginInit();
            this.pnlBotones.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entProductoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(122, 25);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(292, 25);
            this.textBox3.TabIndex = 92;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // txtFiltroSerie
            // 
            this.txtFiltroSerie.Location = new System.Drawing.Point(415, 25);
            this.txtFiltroSerie.Name = "txtFiltroSerie";
            this.txtFiltroSerie.Size = new System.Drawing.Size(149, 25);
            this.txtFiltroSerie.TabIndex = 91;
            this.txtFiltroSerie.TextChanged += new System.EventHandler(this.txtFiltroSerie_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(105, 25);
            this.textBox1.TabIndex = 90;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // gvProductosDetalle
            // 
            this.gvProductosDetalle.AllowUserToAddRows = false;
            this.gvProductosDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvProductosDetalle.AutoGenerateColumns = false;
            this.gvProductosDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvProductosDetalle.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvProductosDetalle.BackgroundColor = System.Drawing.Color.White;
            this.gvProductosDetalle.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvProductosDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProductosDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.estatusDataGridViewCheckBoxColumn,
            this.dataGridViewTextBoxColumn4,
            this.Descripcion,
            this.Serie,
            this.dataGridViewTextBoxColumn5,
            this.PrecioVenta,
            this.Fecha,
            this.Ingreso,
            this.Tipo});
            this.gvProductosDetalle.DataSource = this.entProductoBindingSource;
            this.gvProductosDetalle.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvProductosDetalle.GridColor = System.Drawing.Color.DimGray;
            this.gvProductosDetalle.Location = new System.Drawing.Point(16, 52);
            this.gvProductosDetalle.Name = "gvProductosDetalle";
            this.gvProductosDetalle.RowHeadersVisible = false;
            this.gvProductosDetalle.RowHeadersWidth = 51;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.gvProductosDetalle.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvProductosDetalle.RowTemplate.Height = 27;
            this.gvProductosDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvProductosDetalle.Size = new System.Drawing.Size(1079, 603);
            this.gvProductosDetalle.TabIndex = 118;
            this.gvProductosDetalle.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProductosDetalle_CellDoubleClick);
            this.gvProductosDetalle.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProductosDetalle_CellValueChanged);
            // 
            // pnlBotones
            // 
            this.pnlBotones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBotones.Controls.Add(this.flowLayoutPanel1);
            this.pnlBotones.Location = new System.Drawing.Point(718, 6);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Size = new System.Drawing.Size(380, 45);
            this.pnlBotones.TabIndex = 121;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.btnMueveAConsignacion);
            this.flowLayoutPanel1.Controls.Add(this.btnMueveAIngreso);
            this.flowLayoutPanel1.Controls.Add(this.btnEliminar);
            this.flowLayoutPanel1.Controls.Add(this.btnRefrescar);
            this.flowLayoutPanel1.Controls.Add(this.btnFueraDeServicio);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, -4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(374, 51);
            this.flowLayoutPanel1.TabIndex = 121;
            // 
            // btnMueveAConsignacion
            // 
            this.btnMueveAConsignacion.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMueveAConsignacion.BackColor = System.Drawing.Color.White;
            this.btnMueveAConsignacion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMueveAConsignacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMueveAConsignacion.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMueveAConsignacion.Image = global::Aires.Properties.Resources.Arrow;
            this.btnMueveAConsignacion.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMueveAConsignacion.Location = new System.Drawing.Point(291, 3);
            this.btnMueveAConsignacion.Name = "btnMueveAConsignacion";
            this.btnMueveAConsignacion.Size = new System.Drawing.Size(80, 45);
            this.btnMueveAConsignacion.TabIndex = 120;
            this.btnMueveAConsignacion.Text = "Mover Consignación";
            this.btnMueveAConsignacion.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMueveAConsignacion.UseVisualStyleBackColor = false;
            this.btnMueveAConsignacion.Click += new System.EventHandler(this.btnMueveAConsignacion_Click);
            // 
            // btnMueveAIngreso
            // 
            this.btnMueveAIngreso.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMueveAIngreso.BackColor = System.Drawing.Color.White;
            this.btnMueveAIngreso.BackgroundImage = global::Aires.Properties.Resources.Arrow;
            this.btnMueveAIngreso.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMueveAIngreso.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMueveAIngreso.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMueveAIngreso.Location = new System.Drawing.Point(225, 3);
            this.btnMueveAIngreso.Name = "btnMueveAIngreso";
            this.btnMueveAIngreso.Size = new System.Drawing.Size(60, 45);
            this.btnMueveAIngreso.TabIndex = 119;
            this.btnMueveAIngreso.Text = "Mover";
            this.btnMueveAIngreso.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMueveAIngreso.UseVisualStyleBackColor = false;
            this.btnMueveAIngreso.Click += new System.EventHandler(this.btnMueveAIngreso_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnEliminar.BackColor = System.Drawing.Color.White;
            this.btnEliminar.BackgroundImage = global::Aires.Properties.Resources.flechabaja;
            this.btnEliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.Location = new System.Drawing.Point(159, 3);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(60, 45);
            this.btnEliminar.TabIndex = 94;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRefrescar.BackColor = System.Drawing.Color.White;
            this.btnRefrescar.BackgroundImage = global::Aires.Properties.Resources.Refresh;
            this.btnRefrescar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescar.Font = new System.Drawing.Font("Segoe UI", 4.25F, System.Drawing.FontStyle.Bold);
            this.btnRefrescar.Location = new System.Drawing.Point(93, 3);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(60, 45);
            this.btnRefrescar.TabIndex = 93;
            this.btnRefrescar.Text = "Actualizar";
            this.btnRefrescar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefrescar.UseVisualStyleBackColor = false;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // btnFueraDeServicio
            // 
            this.btnFueraDeServicio.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnFueraDeServicio.BackColor = System.Drawing.Color.White;
            this.btnFueraDeServicio.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFueraDeServicio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFueraDeServicio.Font = new System.Drawing.Font("Segoe UI", 5F, System.Drawing.FontStyle.Bold);
            this.btnFueraDeServicio.Image = global::Aires.Properties.Resources.Inventario2chico;
            this.btnFueraDeServicio.Location = new System.Drawing.Point(7, 3);
            this.btnFueraDeServicio.Name = "btnFueraDeServicio";
            this.btnFueraDeServicio.Size = new System.Drawing.Size(80, 45);
            this.btnFueraDeServicio.TabIndex = 121;
            this.btnFueraDeServicio.Text = "Poner en Servicio";
            this.btnFueraDeServicio.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFueraDeServicio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFueraDeServicio.UseVisualStyleBackColor = false;
            this.btnFueraDeServicio.Click += new System.EventHandler(this.btnFueraDeServicio_Click);
            // 
            // entProductoBindingSource
            // 
            this.entProductoBindingSource.DataSource = typeof(AiresEntidades.EntProducto);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.BackgroundImage = global::Aires.Properties.Resources.cruzChica;
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(587, 176);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(69, 64);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregar.BackgroundImage = global::Aires.Properties.Resources.palomitaChica;
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(287, 586);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(73, 62);
            this.btnAgregar.TabIndex = 10;
            this.btnAgregar.Text = "¡Listo!";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // estatusDataGridViewCheckBoxColumn
            // 
            this.estatusDataGridViewCheckBoxColumn.DataPropertyName = "Estatus";
            this.estatusDataGridViewCheckBoxColumn.FillWeight = 0.5F;
            this.estatusDataGridViewCheckBoxColumn.HeaderText = "Sel.";
            this.estatusDataGridViewCheckBoxColumn.MinimumWidth = 6;
            this.estatusDataGridViewCheckBoxColumn.Name = "estatusDataGridViewCheckBoxColumn";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Codigo";
            this.dataGridViewTextBoxColumn4.FillWeight = 1F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Codigo";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "Descripcion";
            this.Descripcion.FillWeight = 4F;
            this.Descripcion.HeaderText = "Descripcion";
            this.Descripcion.MinimumWidth = 6;
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            // 
            // Serie
            // 
            this.Serie.DataPropertyName = "Serie";
            this.Serie.FillWeight = 2F;
            this.Serie.HeaderText = "Serie";
            this.Serie.MinimumWidth = 6;
            this.Serie.Name = "Serie";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "PrecioCosto";
            dataGridViewCellStyle1.Format = "c2";
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn5.FillWeight = 1F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Precio Costo";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // PrecioVenta
            // 
            this.PrecioVenta.DataPropertyName = "PrecioVenta";
            dataGridViewCellStyle2.Format = "c2";
            this.PrecioVenta.DefaultCellStyle = dataGridViewCellStyle2;
            this.PrecioVenta.FillWeight = 1F;
            this.PrecioVenta.HeaderText = "Precio Venta";
            this.PrecioVenta.MinimumWidth = 6;
            this.PrecioVenta.Name = "PrecioVenta";
            this.PrecioVenta.ReadOnly = true;
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "Fecha";
            dataGridViewCellStyle3.Format = "d";
            this.Fecha.DefaultCellStyle = dataGridViewCellStyle3;
            this.Fecha.FillWeight = 1F;
            this.Fecha.HeaderText = "Fecha Ingreso";
            this.Fecha.MinimumWidth = 6;
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            // 
            // Ingreso
            // 
            this.Ingreso.DataPropertyName = "Ingreso";
            this.Ingreso.DividerWidth = 1;
            this.Ingreso.FillWeight = 1F;
            this.Ingreso.HeaderText = "Ingreso";
            this.Ingreso.MinimumWidth = 6;
            this.Ingreso.Name = "Ingreso";
            this.Ingreso.ReadOnly = true;
            // 
            // Tipo
            // 
            this.Tipo.DataPropertyName = "Tipo";
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tipo.DefaultCellStyle = dataGridViewCellStyle4;
            this.Tipo.FillWeight = 4F;
            this.Tipo.HeaderText = "Observacion";
            this.Tipo.MinimumWidth = 6;
            this.Tipo.Name = "Tipo";
            // 
            // MuestraProductosDetalleFueraDeServicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1107, 660);
            this.Controls.Add(this.pnlBotones);
            this.Controls.Add(this.gvProductosDetalle);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.txtFiltroSerie);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "MuestraProductosDetalleFueraDeServicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DETALLE PRODUCTOS";
            this.Load += new System.EventHandler(this.SeleccionaFactura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvProductosDetalle)).EndInit();
            this.pnlBotones.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.entProductoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.BindingSource entProductoBindingSource;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtFiltroSerie;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.DataGridView gvProductosDetalle;
        private System.Windows.Forms.Button btnMueveAIngreso;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.Button btnMueveAConsignacion;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnFueraDeServicio;
        private System.Windows.Forms.DataGridViewCheckBoxColumn estatusDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serie;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ingreso;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
    }
}