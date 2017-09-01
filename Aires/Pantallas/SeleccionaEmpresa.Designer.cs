namespace Aires.Pantallas
{
    partial class SeleccionaEmpresa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeleccionaEmpresa));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.entEmpresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvEmpresas = new System.Windows.Forms.DataGridView();
            this.nombreDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreFiscalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RFC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.entEmpresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpresas)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.BackColor = System.Drawing.SystemColors.Window;
            this.btnCancelar.BackgroundImage = global::Aires.Properties.Resources.cruzChica;
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(370, 386);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(69, 64);
            this.btnCancelar.TabIndex = 94;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregar.BackColor = System.Drawing.SystemColors.Window;
            this.btnAgregar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAgregar.BackgroundImage")));
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(244, 386);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(73, 64);
            this.btnAgregar.TabIndex = 93;
            this.btnAgregar.Text = "Seleccionar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = false;
            // 
            // entEmpresaBindingSource
            // 
            this.entEmpresaBindingSource.DataSource = typeof(AiresEntidades.EntEmpresa);
            // 
            // gvEmpresas
            // 
            this.gvEmpresas.AllowUserToAddRows = false;
            this.gvEmpresas.AutoGenerateColumns = false;
            this.gvEmpresas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvEmpresas.BackgroundColor = System.Drawing.Color.White;
            this.gvEmpresas.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvEmpresas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvEmpresas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombreDataGridViewTextBoxColumn,
            this.nombreFiscalDataGridViewTextBoxColumn,
            this.RFC});
            this.gvEmpresas.DataSource = this.entEmpresaBindingSource;
            this.gvEmpresas.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvEmpresas.GridColor = System.Drawing.Color.DimGray;
            this.gvEmpresas.Location = new System.Drawing.Point(12, 12);
            this.gvEmpresas.MultiSelect = false;
            this.gvEmpresas.Name = "gvEmpresas";
            this.gvEmpresas.ReadOnly = true;
            this.gvEmpresas.RowHeadersVisible = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Unicode MS", 8.5F);
            this.gvEmpresas.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvEmpresas.RowTemplate.Height = 27;
            this.gvEmpresas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvEmpresas.Size = new System.Drawing.Size(648, 368);
            this.gvEmpresas.TabIndex = 95;
            this.gvEmpresas.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvEmpresas_CellContentDoubleClick_1);
            this.gvEmpresas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvEmpresas_CellDoubleClick);
            // 
            // nombreDataGridViewTextBoxColumn
            // 
            this.nombreDataGridViewTextBoxColumn.DataPropertyName = "Nombre";
            this.nombreDataGridViewTextBoxColumn.FillWeight = 3F;
            this.nombreDataGridViewTextBoxColumn.HeaderText = "Nombre";
            this.nombreDataGridViewTextBoxColumn.Name = "nombreDataGridViewTextBoxColumn";
            this.nombreDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nombreFiscalDataGridViewTextBoxColumn
            // 
            this.nombreFiscalDataGridViewTextBoxColumn.DataPropertyName = "NombreFiscal";
            this.nombreFiscalDataGridViewTextBoxColumn.FillWeight = 2.5F;
            this.nombreFiscalDataGridViewTextBoxColumn.HeaderText = "NombreFiscal";
            this.nombreFiscalDataGridViewTextBoxColumn.Name = "nombreFiscalDataGridViewTextBoxColumn";
            this.nombreFiscalDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // RFC
            // 
            this.RFC.DataPropertyName = "RFC";
            this.RFC.FillWeight = 1.5F;
            this.RFC.HeaderText = "RFC";
            this.RFC.Name = "RFC";
            this.RFC.ReadOnly = true;
            // 
            // SeleccionaEmpresa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(671, 462);
            this.Controls.Add(this.gvEmpresas);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SeleccionaEmpresa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Selección de Empresa";
            this.Load += new System.EventHandler(this.SeleccionaEmpresa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.entEmpresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpresas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.BindingSource entEmpresaBindingSource;
        private System.Windows.Forms.DataGridView gvEmpresas;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreFiscalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RFC;
    }
}