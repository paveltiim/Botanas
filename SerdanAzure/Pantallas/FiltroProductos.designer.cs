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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FiltroProductos));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSeleccionaEmpresa = new System.Windows.Forms.Button();
            this.txtFiltroDescripcionProducto = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gvProductosDetalle = new System.Windows.Forms.DataGridView();
            this.entProductoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnBuscarProducto = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtFiltroSerieProducto = new System.Windows.Forms.TextBox();
            this.codigoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serieDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precioVentaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precioVenta2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precioEspecialDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtFiltroCodigoProducto = new System.Windows.Forms.TextBox();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductosDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entProductoBindingSource)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(150, 491);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(82, 27);
            this.btnCancelar.TabIndex = 78;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnSeleccionaEmpresa
            // 
            this.btnSeleccionaEmpresa.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSeleccionaEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionaEmpresa.Location = new System.Drawing.Point(6, 491);
            this.btnSeleccionaEmpresa.Name = "btnSeleccionaEmpresa";
            this.btnSeleccionaEmpresa.Size = new System.Drawing.Size(82, 27);
            this.btnSeleccionaEmpresa.TabIndex = 77;
            this.btnSeleccionaEmpresa.Text = "Seleccionar";
            this.btnSeleccionaEmpresa.UseVisualStyleBackColor = true;
            this.btnSeleccionaEmpresa.Click += new System.EventHandler(this.btnSeleccionaEmpresa_Click);
            // 
            // txtFiltroDescripcionProducto
            // 
            this.txtFiltroDescripcionProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltroDescripcionProducto.Location = new System.Drawing.Point(150, 9);
            this.txtFiltroDescripcionProducto.Name = "txtFiltroDescripcionProducto";
            this.txtFiltroDescripcionProducto.Size = new System.Drawing.Size(422, 22);
            this.txtFiltroDescripcionProducto.TabIndex = 74;
            this.txtFiltroDescripcionProducto.TextChanged += new System.EventHandler(this.btnBuscarProducto_Click);
            this.txtFiltroDescripcionProducto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFiltroEmpresas_KeyPress);
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.gvProductosDetalle);
            this.panel3.Location = new System.Drawing.Point(6, 33);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1203, 452);
            this.panel3.TabIndex = 79;
            // 
            // gvProductosDetalle
            // 
            this.gvProductosDetalle.AutoGenerateColumns = false;
            this.gvProductosDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvProductosDetalle.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvProductosDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProductosDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigoDataGridViewTextBoxColumn,
            this.descripcionDataGridViewTextBoxColumn,
            this.serieDataGridViewTextBoxColumn,
            this.precioVentaDataGridViewTextBoxColumn,
            this.precioVenta2DataGridViewTextBoxColumn,
            this.precioEspecialDataGridViewTextBoxColumn});
            this.gvProductosDetalle.DataSource = this.entProductoBindingSource;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial Unicode MS", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvProductosDetalle.DefaultCellStyle = dataGridViewCellStyle4;
            this.gvProductosDetalle.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.gvProductosDetalle.Location = new System.Drawing.Point(3, 3);
            this.gvProductosDetalle.MultiSelect = false;
            this.gvProductosDetalle.Name = "gvProductosDetalle";
            this.gvProductosDetalle.ReadOnly = true;
            this.gvProductosDetalle.RowHeadersVisible = false;
            this.gvProductosDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvProductosDetalle.Size = new System.Drawing.Size(1200, 446);
            this.gvProductosDetalle.TabIndex = 0;
            this.gvProductosDetalle.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProductosDetalle_CellDoubleClick);
            this.gvProductosDetalle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gvProductosDetalle_KeyPress);
            // 
            // entProductoBindingSource
            // 
            this.entProductoBindingSource.DataSource = typeof(AiresEntidades.EntProducto);
            // 
            // btnBuscarProducto
            // 
            this.btnBuscarProducto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscarProducto.BackgroundImage")));
            this.btnBuscarProducto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBuscarProducto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarProducto.Location = new System.Drawing.Point(784, 6);
            this.btnBuscarProducto.Name = "btnBuscarProducto";
            this.btnBuscarProducto.Size = new System.Drawing.Size(37, 26);
            this.btnBuscarProducto.TabIndex = 76;
            this.btnBuscarProducto.UseVisualStyleBackColor = true;
            this.btnBuscarProducto.Visible = false;
            this.btnBuscarProducto.Click += new System.EventHandler(this.btnBuscarProducto_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1223, 576);
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
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1215, 550);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Productos";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtFiltroSerieProducto
            // 
            this.txtFiltroSerieProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltroSerieProducto.Location = new System.Drawing.Point(573, 9);
            this.txtFiltroSerieProducto.Name = "txtFiltroSerieProducto";
            this.txtFiltroSerieProducto.Size = new System.Drawing.Size(210, 22);
            this.txtFiltroSerieProducto.TabIndex = 75;
            this.txtFiltroSerieProducto.TextChanged += new System.EventHandler(this.btnBuscarProducto_Click);
            this.txtFiltroSerieProducto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFiltroEmpresas_KeyPress);
            // 
            // codigoDataGridViewTextBoxColumn
            // 
            this.codigoDataGridViewTextBoxColumn.DataPropertyName = "Codigo";
            this.codigoDataGridViewTextBoxColumn.FillWeight = 1F;
            this.codigoDataGridViewTextBoxColumn.HeaderText = "Codigo";
            this.codigoDataGridViewTextBoxColumn.Name = "codigoDataGridViewTextBoxColumn";
            this.codigoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descripcionDataGridViewTextBoxColumn
            // 
            this.descripcionDataGridViewTextBoxColumn.DataPropertyName = "Descripcion";
            this.descripcionDataGridViewTextBoxColumn.FillWeight = 3F;
            this.descripcionDataGridViewTextBoxColumn.HeaderText = "Descripcion";
            this.descripcionDataGridViewTextBoxColumn.Name = "descripcionDataGridViewTextBoxColumn";
            this.descripcionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // serieDataGridViewTextBoxColumn
            // 
            this.serieDataGridViewTextBoxColumn.DataPropertyName = "Serie";
            this.serieDataGridViewTextBoxColumn.FillWeight = 1.5F;
            this.serieDataGridViewTextBoxColumn.HeaderText = "Serie";
            this.serieDataGridViewTextBoxColumn.Name = "serieDataGridViewTextBoxColumn";
            this.serieDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // precioVentaDataGridViewTextBoxColumn
            // 
            this.precioVentaDataGridViewTextBoxColumn.DataPropertyName = "PrecioVenta";
            dataGridViewCellStyle1.Format = "c2";
            this.precioVentaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.precioVentaDataGridViewTextBoxColumn.FillWeight = 1F;
            this.precioVentaDataGridViewTextBoxColumn.HeaderText = "Precio Venta";
            this.precioVentaDataGridViewTextBoxColumn.Name = "precioVentaDataGridViewTextBoxColumn";
            this.precioVentaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // precioVenta2DataGridViewTextBoxColumn
            // 
            this.precioVenta2DataGridViewTextBoxColumn.DataPropertyName = "PrecioVenta2";
            dataGridViewCellStyle2.Format = "c2";
            this.precioVenta2DataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.precioVenta2DataGridViewTextBoxColumn.FillWeight = 1F;
            this.precioVenta2DataGridViewTextBoxColumn.HeaderText = "Precio Mayoreo";
            this.precioVenta2DataGridViewTextBoxColumn.Name = "precioVenta2DataGridViewTextBoxColumn";
            this.precioVenta2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // precioEspecialDataGridViewTextBoxColumn
            // 
            this.precioEspecialDataGridViewTextBoxColumn.DataPropertyName = "PrecioEspecial";
            dataGridViewCellStyle3.Format = "c2";
            this.precioEspecialDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.precioEspecialDataGridViewTextBoxColumn.FillWeight = 1F;
            this.precioEspecialDataGridViewTextBoxColumn.HeaderText = "Precio Especial";
            this.precioEspecialDataGridViewTextBoxColumn.Name = "precioEspecialDataGridViewTextBoxColumn";
            this.precioEspecialDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // txtFiltroCodigoProducto
            // 
            this.txtFiltroCodigoProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltroCodigoProducto.Location = new System.Drawing.Point(9, 9);
            this.txtFiltroCodigoProducto.Name = "txtFiltroCodigoProducto";
            this.txtFiltroCodigoProducto.Size = new System.Drawing.Size(140, 22);
            this.txtFiltroCodigoProducto.TabIndex = 73;
            this.txtFiltroCodigoProducto.TextChanged += new System.EventHandler(this.btnBuscarProducto_Click);
            this.txtFiltroCodigoProducto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFiltroEmpresas_KeyPress);
            // 
            // FiltroProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1248, 601);
            this.Controls.Add(this.tabControl1);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serieDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn precioVentaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn precioVenta2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn precioEspecialDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox txtFiltroCodigoProducto;
    }
}