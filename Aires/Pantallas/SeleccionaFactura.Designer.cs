namespace Aires.Pantallas
{
    partial class SeleccionaFactura
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.gvGastosProveedor = new System.Windows.Forms.DataGridView();
            this.numeroFacturaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deudaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pagoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NotasCredito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saldoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entProveedorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gvGastosProveedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entProveedorBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "Seleccione Factura a Pagar";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.BackgroundImage = global::Aires.Properties.Resources.cruzChica;
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(286, 379);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(69, 64);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = true;
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
            this.btnAgregar.Location = new System.Drawing.Point(160, 379);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(73, 64);
            this.btnAgregar.TabIndex = 10;
            this.btnAgregar.Text = "Seleccionar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // gvGastosProveedor
            // 
            this.gvGastosProveedor.AllowUserToAddRows = false;
            this.gvGastosProveedor.AutoGenerateColumns = false;
            this.gvGastosProveedor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvGastosProveedor.BackgroundColor = System.Drawing.Color.White;
            this.gvGastosProveedor.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvGastosProveedor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvGastosProveedor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numeroFacturaDataGridViewTextBoxColumn,
            this.FechaFactura,
            this.deudaDataGridViewTextBoxColumn,
            this.pagoDataGridViewTextBoxColumn,
            this.NotasCredito,
            this.saldoDataGridViewTextBoxColumn});
            this.gvGastosProveedor.DataSource = this.entProveedorBindingSource;
            this.gvGastosProveedor.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvGastosProveedor.GridColor = System.Drawing.Color.DimGray;
            this.gvGastosProveedor.Location = new System.Drawing.Point(12, 15);
            this.gvGastosProveedor.Name = "gvGastosProveedor";
            this.gvGastosProveedor.ReadOnly = true;
            this.gvGastosProveedor.RowHeadersVisible = false;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Arial Unicode MS", 6.75F);
            this.gvGastosProveedor.RowsDefaultCellStyle = dataGridViewCellStyle14;
            this.gvGastosProveedor.RowTemplate.Height = 27;
            this.gvGastosProveedor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvGastosProveedor.Size = new System.Drawing.Size(495, 358);
            this.gvGastosProveedor.TabIndex = 89;
            this.gvGastosProveedor.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvGastosEmpresa_CellContentDoubleClick);
            // 
            // numeroFacturaDataGridViewTextBoxColumn
            // 
            this.numeroFacturaDataGridViewTextBoxColumn.DataPropertyName = "NumeroFactura";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.numeroFacturaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.numeroFacturaDataGridViewTextBoxColumn.FillWeight = 1.3F;
            this.numeroFacturaDataGridViewTextBoxColumn.HeaderText = "Factura";
            this.numeroFacturaDataGridViewTextBoxColumn.Name = "numeroFacturaDataGridViewTextBoxColumn";
            this.numeroFacturaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // FechaFactura
            // 
            this.FechaFactura.DataPropertyName = "FechaFactura";
            dataGridViewCellStyle9.Format = "d";
            this.FechaFactura.DefaultCellStyle = dataGridViewCellStyle9;
            this.FechaFactura.FillWeight = 1F;
            this.FechaFactura.HeaderText = "Fecha Factura";
            this.FechaFactura.Name = "FechaFactura";
            this.FechaFactura.ReadOnly = true;
            // 
            // deudaDataGridViewTextBoxColumn
            // 
            this.deudaDataGridViewTextBoxColumn.DataPropertyName = "Deuda";
            dataGridViewCellStyle10.Format = "c2";
            this.deudaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.deudaDataGridViewTextBoxColumn.FillWeight = 1.2F;
            this.deudaDataGridViewTextBoxColumn.HeaderText = "Total";
            this.deudaDataGridViewTextBoxColumn.Name = "deudaDataGridViewTextBoxColumn";
            this.deudaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pagoDataGridViewTextBoxColumn
            // 
            this.pagoDataGridViewTextBoxColumn.DataPropertyName = "Pago";
            dataGridViewCellStyle11.Format = "c2";
            this.pagoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.pagoDataGridViewTextBoxColumn.FillWeight = 1.2F;
            this.pagoDataGridViewTextBoxColumn.HeaderText = "Pago";
            this.pagoDataGridViewTextBoxColumn.Name = "pagoDataGridViewTextBoxColumn";
            this.pagoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // NotasCredito
            // 
            this.NotasCredito.DataPropertyName = "NotasCredito";
            dataGridViewCellStyle12.Format = "c2";
            this.NotasCredito.DefaultCellStyle = dataGridViewCellStyle12;
            this.NotasCredito.FillWeight = 1F;
            this.NotasCredito.HeaderText = "Notas Crédito";
            this.NotasCredito.Name = "NotasCredito";
            this.NotasCredito.ReadOnly = true;
            // 
            // saldoDataGridViewTextBoxColumn
            // 
            this.saldoDataGridViewTextBoxColumn.DataPropertyName = "Saldo";
            dataGridViewCellStyle13.Format = "c2";
            this.saldoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle13;
            this.saldoDataGridViewTextBoxColumn.FillWeight = 1.2F;
            this.saldoDataGridViewTextBoxColumn.HeaderText = "Saldo";
            this.saldoDataGridViewTextBoxColumn.Name = "saldoDataGridViewTextBoxColumn";
            this.saldoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // entProveedorBindingSource
            // 
            this.entProveedorBindingSource.DataSource = typeof(AiresEntidades.EntProveedor);
            // 
            // SeleccionaFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(519, 452);
            this.Controls.Add(this.gvGastosProveedor);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SeleccionaFactura";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SELECCIÓN DE FACTURA";
            this.Load += new System.EventHandler(this.SeleccionaFactura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvGastosProveedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entProveedorBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView gvGastosProveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn numeroFacturaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn deudaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pagoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NotasCredito;
        private System.Windows.Forms.DataGridViewTextBoxColumn saldoDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource entProveedorBindingSource;
    }
}