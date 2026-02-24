namespace Aires.Pantallas
{
    partial class SeleccionaComplemento
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gvFacturasComplementos = new System.Windows.Forms.DataGridView();
            this.entFacturaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnAgregar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelaFactura = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.entPagoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.entPedidoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnVerVersion2 = new System.Windows.Forms.Button();
            this.NumeroFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvColumnPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PedidoId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvTextBoxNumeroComplemento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaPagoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvColumnPagoComplemento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ruta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvFacturasComplementos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entFacturaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entPagoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entPedidoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gvFacturasComplementos
            // 
            this.gvFacturasComplementos.AllowUserToAddRows = false;
            this.gvFacturasComplementos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvFacturasComplementos.AutoGenerateColumns = false;
            this.gvFacturasComplementos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvFacturasComplementos.BackgroundColor = System.Drawing.Color.White;
            this.gvFacturasComplementos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvFacturasComplementos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvFacturasComplementos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumeroFactura,
            this.Total,
            this.gvColumnPago,
            this.PedidoId,
            this.gvTextBoxNumeroComplemento,
            this.fechaPagoDataGridViewTextBoxColumn,
            this.gvColumnPagoComplemento,
            this.Ruta,
            this.Descripcion});
            this.gvFacturasComplementos.DataSource = this.entFacturaBindingSource;
            this.gvFacturasComplementos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvFacturasComplementos.GridColor = System.Drawing.Color.DimGray;
            this.gvFacturasComplementos.Location = new System.Drawing.Point(16, 12);
            this.gvFacturasComplementos.Name = "gvFacturasComplementos";
            this.gvFacturasComplementos.RowHeadersVisible = false;
            this.gvFacturasComplementos.RowHeadersWidth = 51;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.gvFacturasComplementos.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.gvFacturasComplementos.RowTemplate.Height = 27;
            this.gvFacturasComplementos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvFacturasComplementos.Size = new System.Drawing.Size(915, 375);
            this.gvFacturasComplementos.TabIndex = 89;
            this.gvFacturasComplementos.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPagos_CellContentDoubleClick);
            this.gvFacturasComplementos.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvFacturasComplementos_CellValueChanged);
            this.gvFacturasComplementos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvPagos_ColumnHeaderMouseClick);
            // 
            // entFacturaBindingSource
            // 
            this.entFacturaBindingSource.DataSource = typeof(AiresEntidades.EntFactura);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAgregar.BackgroundImage = global::Aires.Properties.Resources.Aceptar;
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 6.6F, System.Drawing.FontStyle.Bold);
            this.btnAgregar.Location = new System.Drawing.Point(338, 393);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(120, 66);
            this.btnAgregar.TabIndex = 10;
            this.btnAgregar.Text = "Seleccionar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(367, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "Seleccione Factura a Pagar";
            // 
            // btnCancelaFactura
            // 
            this.btnCancelaFactura.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelaFactura.BackColor = System.Drawing.Color.White;
            this.btnCancelaFactura.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancelaFactura.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelaFactura.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelaFactura.Image = global::Aires.Properties.Resources.Paper_Cancel__chico_;
            this.btnCancelaFactura.Location = new System.Drawing.Point(933, 11);
            this.btnCancelaFactura.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelaFactura.Name = "btnCancelaFactura";
            this.btnCancelaFactura.Size = new System.Drawing.Size(86, 90);
            this.btnCancelaFactura.TabIndex = 125;
            this.btnCancelaFactura.Text = "Cancelar Complemento";
            this.btnCancelaFactura.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelaFactura.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancelaFactura.UseVisualStyleBackColor = false;
            this.btnCancelaFactura.Click += new System.EventHandler(this.btnCancelaComplemento_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.BackColor = System.Drawing.SystemColors.Window;
            this.btnCancelar.BackgroundImage = global::Aires.Properties.Resources.Cancelar;
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 6.6F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.Location = new System.Drawing.Point(493, 393);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(120, 66);
            this.btnCancelar.TabIndex = 126;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // entPagoBindingSource
            // 
            this.entPagoBindingSource.DataSource = typeof(AiresEntidades.EntPago);
            // 
            // entPedidoBindingSource
            // 
            this.entPedidoBindingSource.DataSource = typeof(AiresEntidades.EntPedido);
            // 
            // btnVerVersion2
            // 
            this.btnVerVersion2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerVersion2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnVerVersion2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnVerVersion2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerVersion2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnVerVersion2.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold);
            this.btnVerVersion2.Image = global::Aires.Properties.Resources.Editar;
            this.btnVerVersion2.Location = new System.Drawing.Point(933, 120);
            this.btnVerVersion2.Name = "btnVerVersion2";
            this.btnVerVersion2.Size = new System.Drawing.Size(86, 83);
            this.btnVerVersion2.TabIndex = 127;
            this.btnVerVersion2.Text = "Ver PDF Resumido";
            this.btnVerVersion2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnVerVersion2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnVerVersion2.UseVisualStyleBackColor = false;
            this.btnVerVersion2.Click += new System.EventHandler(this.btnVerVersion2_Click);
            // 
            // NumeroFactura
            // 
            this.NumeroFactura.DataPropertyName = "NumeroFactura";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NumeroFactura.DefaultCellStyle = dataGridViewCellStyle1;
            this.NumeroFactura.FillWeight = 2F;
            this.NumeroFactura.HeaderText = "Número Factura";
            this.NumeroFactura.MinimumWidth = 6;
            this.NumeroFactura.Name = "NumeroFactura";
            this.NumeroFactura.ReadOnly = true;
            // 
            // Total
            // 
            this.Total.DataPropertyName = "Total";
            dataGridViewCellStyle2.Format = "C2";
            this.Total.DefaultCellStyle = dataGridViewCellStyle2;
            this.Total.FillWeight = 1.5F;
            this.Total.HeaderText = "Total Factura";
            this.Total.MinimumWidth = 6;
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            // 
            // gvColumnPago
            // 
            this.gvColumnPago.DataPropertyName = "PagoFactura";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "c2";
            this.gvColumnPago.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvColumnPago.DividerWidth = 1;
            this.gvColumnPago.FillWeight = 1.5F;
            this.gvColumnPago.HeaderText = "Pago Factura";
            this.gvColumnPago.MinimumWidth = 6;
            this.gvColumnPago.Name = "gvColumnPago";
            this.gvColumnPago.ReadOnly = true;
            // 
            // PedidoId
            // 
            this.PedidoId.DataPropertyName = "PedidoId";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.PedidoId.DefaultCellStyle = dataGridViewCellStyle4;
            this.PedidoId.FillWeight = 1F;
            this.PedidoId.HeaderText = "Núm. Parcialidad";
            this.PedidoId.MinimumWidth = 6;
            this.PedidoId.Name = "PedidoId";
            this.PedidoId.ReadOnly = true;
            // 
            // gvTextBoxNumeroComplemento
            // 
            this.gvTextBoxNumeroComplemento.DataPropertyName = "NumeroComplemento";
            this.gvTextBoxNumeroComplemento.FillWeight = 1.8F;
            this.gvTextBoxNumeroComplemento.HeaderText = "Número Complemento";
            this.gvTextBoxNumeroComplemento.Name = "gvTextBoxNumeroComplemento";
            this.gvTextBoxNumeroComplemento.ReadOnly = true;
            // 
            // fechaPagoDataGridViewTextBoxColumn
            // 
            this.fechaPagoDataGridViewTextBoxColumn.DataPropertyName = "Fecha";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "d";
            this.fechaPagoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.fechaPagoDataGridViewTextBoxColumn.FillWeight = 1.5F;
            this.fechaPagoDataGridViewTextBoxColumn.HeaderText = "Fecha Complemento";
            this.fechaPagoDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.fechaPagoDataGridViewTextBoxColumn.Name = "fechaPagoDataGridViewTextBoxColumn";
            this.fechaPagoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // gvColumnPagoComplemento
            // 
            this.gvColumnPagoComplemento.DataPropertyName = "Pago";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Format = "c2";
            this.gvColumnPagoComplemento.DefaultCellStyle = dataGridViewCellStyle6;
            this.gvColumnPagoComplemento.FillWeight = 1.5F;
            this.gvColumnPagoComplemento.HeaderText = "Pago Complemento";
            this.gvColumnPagoComplemento.MinimumWidth = 6;
            this.gvColumnPagoComplemento.Name = "gvColumnPagoComplemento";
            this.gvColumnPagoComplemento.ReadOnly = true;
            // 
            // Ruta
            // 
            this.Ruta.DataPropertyName = "Ruta";
            this.Ruta.FillWeight = 5F;
            this.Ruta.HeaderText = "Ruta Complemento";
            this.Ruta.MinimumWidth = 6;
            this.Ruta.Name = "Ruta";
            this.Ruta.ReadOnly = true;
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "Descripcion";
            this.Descripcion.FillWeight = 2F;
            this.Descripcion.HeaderText = "Estatus Complemento";
            this.Descripcion.MinimumWidth = 6;
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            // 
            // SeleccionaComplemento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1021, 468);
            this.Controls.Add(this.btnVerVersion2);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.gvFacturasComplementos);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancelaFactura);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SeleccionaComplemento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "-COMPLEMENTOS-";
            this.Load += new System.EventHandler(this.SeleccionaComplemento_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvFacturasComplementos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entFacturaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entPagoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entPedidoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView gvFacturasComplementos;
        private System.Windows.Forms.BindingSource entPagoBindingSource;
        private System.Windows.Forms.BindingSource entPedidoBindingSource;
        private System.Windows.Forms.BindingSource entFacturaBindingSource;
        private System.Windows.Forms.Button btnCancelaFactura;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnVerVersion2;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvColumnPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn PedidoId;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvTextBoxNumeroComplemento;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaPagoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvColumnPagoComplemento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ruta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
    }
}