namespace Aires.Pantallas
{
    partial class SeleccionaFacturaPedidos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.gvGastosEmpresa = new System.Windows.Forms.DataGridView();
            this.pagoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entClienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gvGastosEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entClienteBindingSource)).BeginInit();
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
            this.btnCancelar.Location = new System.Drawing.Point(237, 380);
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
            this.btnAgregar.Location = new System.Drawing.Point(111, 380);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(73, 64);
            this.btnAgregar.TabIndex = 10;
            this.btnAgregar.Text = "Seleccionar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // gvGastosEmpresa
            // 
            this.gvGastosEmpresa.AllowUserToAddRows = false;
            this.gvGastosEmpresa.AutoGenerateColumns = false;
            this.gvGastosEmpresa.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvGastosEmpresa.BackgroundColor = System.Drawing.Color.White;
            this.gvGastosEmpresa.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvGastosEmpresa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvGastosEmpresa.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pagoDataGridViewTextBoxColumn});
            this.gvGastosEmpresa.DataSource = this.entClienteBindingSource;
            this.gvGastosEmpresa.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvGastosEmpresa.GridColor = System.Drawing.Color.DimGray;
            this.gvGastosEmpresa.Location = new System.Drawing.Point(16, 15);
            this.gvGastosEmpresa.Name = "gvGastosEmpresa";
            this.gvGastosEmpresa.ReadOnly = true;
            this.gvGastosEmpresa.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Unicode MS", 6.75F);
            this.gvGastosEmpresa.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gvGastosEmpresa.RowTemplate.Height = 27;
            this.gvGastosEmpresa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvGastosEmpresa.Size = new System.Drawing.Size(403, 358);
            this.gvGastosEmpresa.TabIndex = 89;
            this.gvGastosEmpresa.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvGastosEmpresa_CellContentDoubleClick);
            // 
            // pagoDataGridViewTextBoxColumn
            // 
            this.pagoDataGridViewTextBoxColumn.DataPropertyName = "Pago";
            dataGridViewCellStyle1.Format = "c2";
            this.pagoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.pagoDataGridViewTextBoxColumn.FillWeight = 1.2F;
            this.pagoDataGridViewTextBoxColumn.HeaderText = "Pago";
            this.pagoDataGridViewTextBoxColumn.Name = "pagoDataGridViewTextBoxColumn";
            this.pagoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // entClienteBindingSource
            // 
            this.entClienteBindingSource.DataSource = typeof(AiresEntidades.EntCliente);
            // 
            // SeleccionaFacturaPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(431, 452);
            this.Controls.Add(this.gvGastosEmpresa);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SeleccionaFacturaPedidos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SELECCIÓN DE FACTURA";
            this.Load += new System.EventHandler(this.SeleccionaFactura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvGastosEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entClienteBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView gvGastosEmpresa;
        private System.Windows.Forms.DataGridViewTextBoxColumn pagoDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource entClienteBindingSource;
    }
}