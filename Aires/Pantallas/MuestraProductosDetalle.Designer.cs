namespace Aires.Pantallas
{
    partial class MuestraProductosDetalle
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.entProductoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnEliminar = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.txtFiltroSerie = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.gvProductosDetalle = new System.Windows.Forms.DataGridView();
            this.estatusDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnMueveAIngreso = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.entProductoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductosDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // entProductoBindingSource
            // 
            this.entProductoBindingSource.DataSource = typeof(AiresEntidades.EntProducto);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEliminar.BackColor = System.Drawing.Color.White;
            this.btnEliminar.BackgroundImage = global::Aires.Properties.Resources.flechabaja;
            this.btnEliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.Location = new System.Drawing.Point(563, 8);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(43, 33);
            this.btnEliminar.TabIndex = 94;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(89, 18);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(288, 21);
            this.textBox3.TabIndex = 92;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // txtFiltroSerie
            // 
            this.txtFiltroSerie.Location = new System.Drawing.Point(378, 18);
            this.txtFiltroSerie.Name = "txtFiltroSerie";
            this.txtFiltroSerie.Size = new System.Drawing.Size(138, 21);
            this.txtFiltroSerie.TabIndex = 91;
            this.txtFiltroSerie.TextChanged += new System.EventHandler(this.txtFiltroSerie_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(72, 21);
            this.textBox1.TabIndex = 90;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.BackgroundImage = global::Aires.Properties.Resources.cruzChica;
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(587, 122);
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
            this.btnAgregar.Location = new System.Drawing.Point(287, 532);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(73, 62);
            this.btnAgregar.TabIndex = 10;
            this.btnAgregar.Text = "¡Listo!";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
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
            this.estatusDataGridViewCheckBoxColumn,
            this.dataGridViewTextBoxColumn4,
            this.Descripcion,
            this.Serie,
            this.dataGridViewTextBoxColumn5,
            this.PrecioVenta});
            this.gvProductosDetalle.DataSource = this.entProductoBindingSource;
            this.gvProductosDetalle.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvProductosDetalle.GridColor = System.Drawing.Color.DimGray;
            this.gvProductosDetalle.Location = new System.Drawing.Point(16, 45);
            this.gvProductosDetalle.Name = "gvProductosDetalle";
            this.gvProductosDetalle.RowHeadersVisible = false;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial Unicode MS", 6.75F);
            this.gvProductosDetalle.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.gvProductosDetalle.RowTemplate.Height = 27;
            this.gvProductosDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvProductosDetalle.Size = new System.Drawing.Size(640, 556);
            this.gvProductosDetalle.TabIndex = 118;
            // 
            // estatusDataGridViewCheckBoxColumn
            // 
            this.estatusDataGridViewCheckBoxColumn.DataPropertyName = "Estatus";
            this.estatusDataGridViewCheckBoxColumn.FillWeight = 0.5F;
            this.estatusDataGridViewCheckBoxColumn.HeaderText = "Sel.";
            this.estatusDataGridViewCheckBoxColumn.Name = "estatusDataGridViewCheckBoxColumn";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Codigo";
            this.dataGridViewTextBoxColumn4.FillWeight = 1F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Codigo";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "Descripcion";
            this.Descripcion.FillWeight = 4F;
            this.Descripcion.HeaderText = "Descripcion";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            // 
            // Serie
            // 
            this.Serie.DataPropertyName = "Serie";
            this.Serie.FillWeight = 2F;
            this.Serie.HeaderText = "Serie";
            this.Serie.Name = "Serie";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "PrecioCosto";
            dataGridViewCellStyle4.Format = "c2";
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn5.FillWeight = 1F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Precio Costo";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // PrecioVenta
            // 
            this.PrecioVenta.DataPropertyName = "PrecioVenta";
            dataGridViewCellStyle5.Format = "c2";
            this.PrecioVenta.DefaultCellStyle = dataGridViewCellStyle5;
            this.PrecioVenta.FillWeight = 1F;
            this.PrecioVenta.HeaderText = "Precio Venta";
            this.PrecioVenta.Name = "PrecioVenta";
            this.PrecioVenta.ReadOnly = true;
            // 
            // btnMueveAIngreso
            // 
            this.btnMueveAIngreso.BackColor = System.Drawing.Color.White;
            this.btnMueveAIngreso.BackgroundImage = global::Aires.Properties.Resources.Arrow;
            this.btnMueveAIngreso.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMueveAIngreso.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMueveAIngreso.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMueveAIngreso.Location = new System.Drawing.Point(609, 8);
            this.btnMueveAIngreso.Name = "btnMueveAIngreso";
            this.btnMueveAIngreso.Size = new System.Drawing.Size(47, 32);
            this.btnMueveAIngreso.TabIndex = 119;
            this.btnMueveAIngreso.Text = "Mover";
            this.btnMueveAIngreso.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMueveAIngreso.UseVisualStyleBackColor = false;
            this.btnMueveAIngreso.Click += new System.EventHandler(this.btnMueveAIngreso_Click);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefrescar.BackColor = System.Drawing.Color.White;
            this.btnRefrescar.BackgroundImage = global::Aires.Properties.Resources.Refresh;
            this.btnRefrescar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescar.Location = new System.Drawing.Point(517, 8);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(43, 33);
            this.btnRefrescar.TabIndex = 93;
            this.btnRefrescar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefrescar.UseVisualStyleBackColor = false;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // MuestraProductosDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(668, 606);
            this.Controls.Add(this.btnMueveAIngreso);
            this.Controls.Add(this.gvProductosDetalle);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.txtFiltroSerie);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MuestraProductosDetalle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DETALLE PRODUCTOS";
            this.Load += new System.EventHandler(this.SeleccionaFactura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.entProductoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductosDetalle)).EndInit();
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
        private System.Windows.Forms.DataGridViewCheckBoxColumn estatusDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serie;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioVenta;
        private System.Windows.Forms.Button btnMueveAIngreso;
        private System.Windows.Forms.Button btnRefrescar;
    }
}