namespace Aires.Pantallas
{
    partial class MuestraPagos
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
            this.gvPagos = new System.Windows.Forms.DataGridView();
            this.entPagoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtFactura = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.btnEliminaPago = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnExporta = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.fechaPagoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidadDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FacturaColumnGvPagos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescripcionColumnGvPagos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvPagos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entPagoBindingSource)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvPagos
            // 
            this.gvPagos.AllowUserToAddRows = false;
            this.gvPagos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvPagos.AutoGenerateColumns = false;
            this.gvPagos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvPagos.BackgroundColor = System.Drawing.Color.White;
            this.gvPagos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPagos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fechaPagoDataGridViewTextBoxColumn,
            this.cantidadDataGridViewTextBoxColumn,
            this.fechaDataGridViewTextBoxColumn,
            this.FacturaColumnGvPagos,
            this.DescripcionColumnGvPagos});
            this.gvPagos.DataSource = this.entPagoBindingSource;
            this.gvPagos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvPagos.GridColor = System.Drawing.Color.DimGray;
            this.gvPagos.Location = new System.Drawing.Point(16, 48);
            this.gvPagos.Name = "gvPagos";
            this.gvPagos.RowHeadersVisible = false;
            this.gvPagos.RowHeadersWidth = 51;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.gvPagos.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvPagos.RowTemplate.Height = 27;
            this.gvPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvPagos.Size = new System.Drawing.Size(643, 346);
            this.gvPagos.TabIndex = 89;
            this.gvPagos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPagos_CellContentClick);
            this.gvPagos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvPagos_ColumnHeaderMouseClick);
            // 
            // entPagoBindingSource
            // 
            this.entPagoBindingSource.DataSource = typeof(AiresEntidades.EntPago);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(367, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "Seleccione Factura a Pagar";
            // 
            // txtFactura
            // 
            this.txtFactura.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFactura.Location = new System.Drawing.Point(273, 22);
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.Size = new System.Drawing.Size(70, 21);
            this.txtFactura.TabIndex = 95;
            this.txtFactura.TextChanged += new System.EventHandler(this.txtFactura_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(270, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 14);
            this.label3.TabIndex = 119;
            this.label3.Text = "Factura:";
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefrescar.BackColor = System.Drawing.Color.White;
            this.btnRefrescar.BackgroundImage = global::Aires.Properties.Resources.Refresh;
            this.btnRefrescar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescar.Location = new System.Drawing.Point(15, 9);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(49, 38);
            this.btnRefrescar.TabIndex = 94;
            this.btnRefrescar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefrescar.UseVisualStyleBackColor = false;
            this.btnRefrescar.Visible = false;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // btnEliminaPago
            // 
            this.btnEliminaPago.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEliminaPago.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEliminaPago.BackgroundImage = global::Aires.Properties.Resources.flechabaja;
            this.btnEliminaPago.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEliminaPago.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminaPago.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminaPago.Location = new System.Drawing.Point(3, 3);
            this.btnEliminaPago.Name = "btnEliminaPago";
            this.btnEliminaPago.Size = new System.Drawing.Size(76, 70);
            this.btnEliminaPago.TabIndex = 11;
            this.btnEliminaPago.Text = "Eliminar";
            this.btnEliminaPago.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEliminaPago.UseVisualStyleBackColor = false;
            this.btnEliminaPago.Visible = false;
            this.btnEliminaPago.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAgregar.BackgroundImage = global::Aires.Properties.Resources.palomitaChica;
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(299, 396);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(120, 70);
            this.btnAgregar.TabIndex = 10;
            this.btnAgregar.Text = "¡Listo!";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = false;
            // 
            // btnExporta
            // 
            this.btnExporta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExporta.BackColor = System.Drawing.Color.White;
            this.btnExporta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExporta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExporta.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExporta.Image = global::Aires.Properties.Resources.excel_logo;
            this.btnExporta.Location = new System.Drawing.Point(5, 79);
            this.btnExporta.Name = "btnExporta";
            this.btnExporta.Size = new System.Drawing.Size(74, 67);
            this.btnExporta.TabIndex = 120;
            this.btnExporta.Text = "Exporta";
            this.btnExporta.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExporta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExporta.UseVisualStyleBackColor = false;
            this.btnExporta.Click += new System.EventHandler(this.btnExporta_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnEliminaPago);
            this.flowLayoutPanel1.Controls.Add(this.btnExporta);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(658, 48);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(84, 208);
            this.flowLayoutPanel1.TabIndex = 121;
            // 
            // fechaPagoDataGridViewTextBoxColumn
            // 
            this.fechaPagoDataGridViewTextBoxColumn.DataPropertyName = "FechaPago";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Format = "d";
            this.fechaPagoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.fechaPagoDataGridViewTextBoxColumn.FillWeight = 2F;
            this.fechaPagoDataGridViewTextBoxColumn.HeaderText = "Fecha Pago";
            this.fechaPagoDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.fechaPagoDataGridViewTextBoxColumn.Name = "fechaPagoDataGridViewTextBoxColumn";
            // 
            // cantidadDataGridViewTextBoxColumn
            // 
            this.cantidadDataGridViewTextBoxColumn.DataPropertyName = "Cantidad";
            dataGridViewCellStyle2.Format = "c2";
            this.cantidadDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.cantidadDataGridViewTextBoxColumn.FillWeight = 1F;
            this.cantidadDataGridViewTextBoxColumn.HeaderText = "Cantidad Pago";
            this.cantidadDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.cantidadDataGridViewTextBoxColumn.Name = "cantidadDataGridViewTextBoxColumn";
            this.cantidadDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fechaDataGridViewTextBoxColumn
            // 
            this.fechaDataGridViewTextBoxColumn.DataPropertyName = "Fecha";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "d";
            this.fechaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.fechaDataGridViewTextBoxColumn.FillWeight = 1.5F;
            this.fechaDataGridViewTextBoxColumn.HeaderText = "Fecha Pedido";
            this.fechaDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.fechaDataGridViewTextBoxColumn.Name = "fechaDataGridViewTextBoxColumn";
            this.fechaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // FacturaColumnGvPagos
            // 
            this.FacturaColumnGvPagos.DataPropertyName = "Factura";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.FacturaColumnGvPagos.DefaultCellStyle = dataGridViewCellStyle4;
            this.FacturaColumnGvPagos.FillWeight = 1F;
            this.FacturaColumnGvPagos.HeaderText = "Factura";
            this.FacturaColumnGvPagos.MinimumWidth = 6;
            this.FacturaColumnGvPagos.Name = "FacturaColumnGvPagos";
            this.FacturaColumnGvPagos.ReadOnly = true;
            // 
            // DescripcionColumnGvPagos
            // 
            this.DescripcionColumnGvPagos.DataPropertyName = "Descripcion";
            this.DescripcionColumnGvPagos.FillWeight = 5F;
            this.DescripcionColumnGvPagos.HeaderText = "Pedido";
            this.DescripcionColumnGvPagos.MinimumWidth = 6;
            this.DescripcionColumnGvPagos.Name = "DescripcionColumnGvPagos";
            this.DescripcionColumnGvPagos.ReadOnly = true;
            // 
            // MuestraPagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(738, 468);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFactura);
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.gvPagos);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MuestraPagos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MUESTRA PAGOS";
            this.Load += new System.EventHandler(this.SeleccionaFactura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvPagos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entPagoBindingSource)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEliminaPago;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView gvPagos;
        private System.Windows.Forms.BindingSource entPagoBindingSource;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.TextBox txtFactura;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnExporta;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaPagoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidadDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FacturaColumnGvPagos;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescripcionColumnGvPagos;
    }
}