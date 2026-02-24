namespace Aires.Pantallas
{
    partial class FiltroProductos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FiltroProductos));
            this.txtFiltroDescripcionProducto = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gvProductosDetalle = new System.Windows.Forms.DataGridView();
            this.codigoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioCosto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Existencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entProductoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtFiltroCodigoProducto = new System.Windows.Forms.TextBox();
            this.txtFiltroSerieProducto = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSeleccionaEmpresa = new System.Windows.Forms.Button();
            this.btnBuscarProducto = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductosDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entProductoBindingSource)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFiltroDescripcionProducto
            // 
            this.txtFiltroDescripcionProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltroDescripcionProducto.Location = new System.Drawing.Point(200, 11);
            this.txtFiltroDescripcionProducto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFiltroDescripcionProducto.Name = "txtFiltroDescripcionProducto";
            this.txtFiltroDescripcionProducto.Size = new System.Drawing.Size(561, 26);
            this.txtFiltroDescripcionProducto.TabIndex = 2;
            this.txtFiltroDescripcionProducto.TextChanged += new System.EventHandler(this.btnBuscarProducto_Click);
            this.txtFiltroDescripcionProducto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFiltroEmpresas_KeyPress);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.gvProductosDetalle);
            this.panel3.Location = new System.Drawing.Point(8, 41);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1220, 470);
            this.panel3.TabIndex = 79;
            // 
            // gvProductosDetalle
            // 
            this.gvProductosDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvProductosDetalle.AutoGenerateColumns = false;
            this.gvProductosDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvProductosDetalle.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvProductosDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProductosDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigoDataGridViewTextBoxColumn,
            this.descripcionDataGridViewTextBoxColumn,
            this.PrecioCosto,
            this.Existencia});
            this.gvProductosDetalle.DataSource = this.entProductoBindingSource;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvProductosDetalle.DefaultCellStyle = dataGridViewCellStyle1;
            this.gvProductosDetalle.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.gvProductosDetalle.Location = new System.Drawing.Point(4, 0);
            this.gvProductosDetalle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gvProductosDetalle.MultiSelect = false;
            this.gvProductosDetalle.Name = "gvProductosDetalle";
            this.gvProductosDetalle.ReadOnly = true;
            this.gvProductosDetalle.RowHeadersVisible = false;
            this.gvProductosDetalle.RowHeadersWidth = 62;
            this.gvProductosDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvProductosDetalle.Size = new System.Drawing.Size(1109, 470);
            this.gvProductosDetalle.TabIndex = 0;
            this.gvProductosDetalle.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProductosDetalle_CellDoubleClick);
            this.gvProductosDetalle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gvProductosDetalle_KeyPress);
            // 
            // codigoDataGridViewTextBoxColumn
            // 
            this.codigoDataGridViewTextBoxColumn.DataPropertyName = "Codigo";
            this.codigoDataGridViewTextBoxColumn.FillWeight = 1F;
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
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(18, 16);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1239, 623);
            this.tabControl1.TabIndex = 80;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtFiltroCodigoProducto);
            this.tabPage1.Controls.Add(this.txtFiltroSerieProducto);
            this.tabPage1.Controls.Add(this.btnCancelar);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.btnSeleccionaEmpresa);
            this.tabPage1.Controls.Add(this.txtFiltroDescripcionProducto);
            this.tabPage1.Controls.Add(this.btnBuscarProducto);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(1231, 594);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Productos";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtFiltroCodigoProducto
            // 
            this.txtFiltroCodigoProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltroCodigoProducto.Location = new System.Drawing.Point(12, 11);
            this.txtFiltroCodigoProducto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFiltroCodigoProducto.Name = "txtFiltroCodigoProducto";
            this.txtFiltroCodigoProducto.Size = new System.Drawing.Size(185, 26);
            this.txtFiltroCodigoProducto.TabIndex = 1;
            this.txtFiltroCodigoProducto.TextChanged += new System.EventHandler(this.btnBuscarProducto_Click);
            this.txtFiltroCodigoProducto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFiltroEmpresas_KeyPress);
            // 
            // txtFiltroSerieProducto
            // 
            this.txtFiltroSerieProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltroSerieProducto.Location = new System.Drawing.Point(853, 11);
            this.txtFiltroSerieProducto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFiltroSerieProducto.Name = "txtFiltroSerieProducto";
            this.txtFiltroSerieProducto.Size = new System.Drawing.Size(279, 26);
            this.txtFiltroSerieProducto.TabIndex = 75;
            this.txtFiltroSerieProducto.Visible = false;
            this.txtFiltroSerieProducto.TextChanged += new System.EventHandler(this.btnBuscarProducto_Click);
            this.txtFiltroSerieProducto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFiltroEmpresas_KeyPress);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Image = global::Aires.Properties.Resources.Cancelar;
            this.btnCancelar.Location = new System.Drawing.Point(220, 523);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(200, 70);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnSeleccionaEmpresa
            // 
            this.btnSeleccionaEmpresa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSeleccionaEmpresa.BackColor = System.Drawing.Color.White;
            this.btnSeleccionaEmpresa.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSeleccionaEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionaEmpresa.Image = global::Aires.Properties.Resources.Aceptar;
            this.btnSeleccionaEmpresa.Location = new System.Drawing.Point(8, 523);
            this.btnSeleccionaEmpresa.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSeleccionaEmpresa.Name = "btnSeleccionaEmpresa";
            this.btnSeleccionaEmpresa.Size = new System.Drawing.Size(203, 70);
            this.btnSeleccionaEmpresa.TabIndex = 3;
            this.btnSeleccionaEmpresa.Text = "Seleccionar";
            this.btnSeleccionaEmpresa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSeleccionaEmpresa.UseVisualStyleBackColor = false;
            this.btnSeleccionaEmpresa.Click += new System.EventHandler(this.btnSeleccionaEmpresa_Click);
            // 
            // btnBuscarProducto
            // 
            this.btnBuscarProducto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscarProducto.BackgroundImage")));
            this.btnBuscarProducto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBuscarProducto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarProducto.Location = new System.Drawing.Point(769, 7);
            this.btnBuscarProducto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBuscarProducto.Name = "btnBuscarProducto";
            this.btnBuscarProducto.Size = new System.Drawing.Size(50, 32);
            this.btnBuscarProducto.TabIndex = 76;
            this.btnBuscarProducto.UseVisualStyleBackColor = true;
            this.btnBuscarProducto.Visible = false;
            this.btnBuscarProducto.Click += new System.EventHandler(this.btnBuscarProducto_Click);
            // 
            // FiltroProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1268, 654);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FiltroProductos";
            this.Text = "Filtro Productos";
            this.Activated += new System.EventHandler(this.FiltroProductos_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FiltroProductos_FormClosing);
            this.Load += new System.EventHandler(this.FiltroProductos_Load);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvProductosDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entProductoBindingSource)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSeleccionaEmpresa;
        private System.Windows.Forms.Button btnBuscarProducto;
        private System.Windows.Forms.TextBox txtFiltroDescripcionProducto;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView gvProductosDetalle;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.BindingSource entProductoBindingSource;
        private System.Windows.Forms.TextBox txtFiltroSerieProducto;
        private System.Windows.Forms.TextBox txtFiltroCodigoProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioCosto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Existencia;
    }
}