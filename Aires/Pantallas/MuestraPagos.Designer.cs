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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEliminaPago = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.gvPagos = new System.Windows.Forms.DataGridView();
            this.fechaPagoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidadDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entPagoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnRefrescar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvPagos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entPagoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(367, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "Seleccione Factura a Pagar";
            // 
            // btnEliminaPago
            // 
            this.btnEliminaPago.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEliminaPago.BackgroundImage = global::Aires.Properties.Resources.flechabaja;
            this.btnEliminaPago.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEliminaPago.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminaPago.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminaPago.Location = new System.Drawing.Point(159, 395);
            this.btnEliminaPago.Name = "btnEliminaPago";
            this.btnEliminaPago.Size = new System.Drawing.Size(73, 64);
            this.btnEliminaPago.TabIndex = 11;
            this.btnEliminaPago.Text = "Eliminar";
            this.btnEliminaPago.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEliminaPago.UseVisualStyleBackColor = true;
            this.btnEliminaPago.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregar.BackgroundImage = global::Aires.Properties.Resources.palomitaChica;
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(302, 395);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(73, 64);
            this.btnAgregar.TabIndex = 10;
            this.btnAgregar.Text = "¡Listo!";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = true;
            // 
            // gvPagos
            // 
            this.gvPagos.AllowUserToAddRows = false;
            this.gvPagos.AutoGenerateColumns = false;
            this.gvPagos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvPagos.BackgroundColor = System.Drawing.Color.White;
            this.gvPagos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPagos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fechaPagoDataGridViewTextBoxColumn,
            this.cantidadDataGridViewTextBoxColumn,
            this.fechaDataGridViewTextBoxColumn,
            this.Descripcion});
            this.gvPagos.DataSource = this.entPagoBindingSource;
            this.gvPagos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvPagos.GridColor = System.Drawing.Color.DimGray;
            this.gvPagos.Location = new System.Drawing.Point(16, 48);
            this.gvPagos.Name = "gvPagos";
            this.gvPagos.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial Unicode MS", 6.75F);
            this.gvPagos.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvPagos.RowTemplate.Height = 27;
            this.gvPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvPagos.Size = new System.Drawing.Size(505, 339);
            this.gvPagos.TabIndex = 89;
            this.gvPagos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPagos_CellContentClick);
            // 
            // fechaPagoDataGridViewTextBoxColumn
            // 
            this.fechaPagoDataGridViewTextBoxColumn.DataPropertyName = "FechaPago";
            dataGridViewCellStyle1.Format = "d";
            this.fechaPagoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.fechaPagoDataGridViewTextBoxColumn.FillWeight = 1.5F;
            this.fechaPagoDataGridViewTextBoxColumn.HeaderText = "Fecha Pago";
            this.fechaPagoDataGridViewTextBoxColumn.Name = "fechaPagoDataGridViewTextBoxColumn";
            // 
            // cantidadDataGridViewTextBoxColumn
            // 
            this.cantidadDataGridViewTextBoxColumn.DataPropertyName = "Cantidad";
            dataGridViewCellStyle2.Format = "c2";
            this.cantidadDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.cantidadDataGridViewTextBoxColumn.FillWeight = 1F;
            this.cantidadDataGridViewTextBoxColumn.HeaderText = "Cantidad Pago";
            this.cantidadDataGridViewTextBoxColumn.Name = "cantidadDataGridViewTextBoxColumn";
            this.cantidadDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fechaDataGridViewTextBoxColumn
            // 
            this.fechaDataGridViewTextBoxColumn.DataPropertyName = "Fecha";
            dataGridViewCellStyle3.Format = "d";
            this.fechaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.fechaDataGridViewTextBoxColumn.FillWeight = 1.5F;
            this.fechaDataGridViewTextBoxColumn.HeaderText = "Fecha Pedido";
            this.fechaDataGridViewTextBoxColumn.Name = "fechaDataGridViewTextBoxColumn";
            this.fechaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "Descripcion";
            this.Descripcion.FillWeight = 3F;
            this.Descripcion.HeaderText = "Pedido";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            // 
            // entPagoBindingSource
            // 
            this.entPagoBindingSource.DataSource = typeof(AiresEntidades.EntPago);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefrescar.BackColor = System.Drawing.Color.White;
            this.btnRefrescar.BackgroundImage = global::Aires.Properties.Resources.Refresh;
            this.btnRefrescar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescar.Location = new System.Drawing.Point(16, 9);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(43, 33);
            this.btnRefrescar.TabIndex = 94;
            this.btnRefrescar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefrescar.UseVisualStyleBackColor = false;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // MuestraPagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(533, 468);
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.gvPagos);
            this.Controls.Add(this.btnEliminaPago);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MuestraPagos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MUESTRA PAGOS";
            this.Load += new System.EventHandler(this.SeleccionaFactura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvPagos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entPagoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEliminaPago;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView gvPagos;
        private System.Windows.Forms.BindingSource entPagoBindingSource;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaPagoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidadDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
    }
}