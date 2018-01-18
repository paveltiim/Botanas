namespace Aires.Pantallas
{
    partial class SeleccionaIngreso
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
            this.entCatalogoGenericoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlEntradasPorMes = new System.Windows.Forms.Panel();
            this.cmbMesesEntradas = new System.Windows.Forms.ComboBox();
            this.cmbAñoEntradas = new System.Windows.Forms.ComboBox();
            this.gvIngresos = new System.Windows.Forms.DataGridView();
            this.fechaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.entCatalogoGenericoBindingSource)).BeginInit();
            this.pnlEntradasPorMes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvIngresos)).BeginInit();
            this.SuspendLayout();
            // 
            // entCatalogoGenericoBindingSource
            // 
            this.entCatalogoGenericoBindingSource.DataSource = typeof(AiresEntidades.EntCatalogoGenerico);
            // 
            // pnlEntradasPorMes
            // 
            this.pnlEntradasPorMes.Controls.Add(this.cmbMesesEntradas);
            this.pnlEntradasPorMes.Controls.Add(this.cmbAñoEntradas);
            this.pnlEntradasPorMes.Location = new System.Drawing.Point(7, 7);
            this.pnlEntradasPorMes.Name = "pnlEntradasPorMes";
            this.pnlEntradasPorMes.Size = new System.Drawing.Size(216, 34);
            this.pnlEntradasPorMes.TabIndex = 90;
            // 
            // cmbMesesEntradas
            // 
            this.cmbMesesEntradas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMesesEntradas.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMesesEntradas.FormattingEnabled = true;
            this.cmbMesesEntradas.Items.AddRange(new object[] {
            "ENERO",
            "FEBRERO",
            "MARZO",
            "ABRIL",
            "MAYO",
            "JUNIO",
            "JULIO",
            "AGOSTO",
            "SEPTIEMBRE",
            "OCTUBRE",
            "NOVIEMBRE",
            "DICIEMBRE"});
            this.cmbMesesEntradas.Location = new System.Drawing.Point(5, 6);
            this.cmbMesesEntradas.Name = "cmbMesesEntradas";
            this.cmbMesesEntradas.Size = new System.Drawing.Size(115, 26);
            this.cmbMesesEntradas.TabIndex = 19;
            this.cmbMesesEntradas.SelectedIndexChanged += new System.EventHandler(this.cmbMesesEntradas_SelectedIndexChanged);
            // 
            // cmbAñoEntradas
            // 
            this.cmbAñoEntradas.DisplayMember = "Descripcion";
            this.cmbAñoEntradas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAñoEntradas.FormattingEnabled = true;
            this.cmbAñoEntradas.Location = new System.Drawing.Point(130, 6);
            this.cmbAñoEntradas.Name = "cmbAñoEntradas";
            this.cmbAñoEntradas.Size = new System.Drawing.Size(77, 24);
            this.cmbAñoEntradas.TabIndex = 20;
            this.cmbAñoEntradas.ValueMember = "Descripcion";
            this.cmbAñoEntradas.SelectedIndexChanged += new System.EventHandler(this.cmbAñoEntradas_SelectedIndexChanged);
            // 
            // gvIngresos
            // 
            this.gvIngresos.AllowUserToAddRows = false;
            this.gvIngresos.AutoGenerateColumns = false;
            this.gvIngresos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvIngresos.BackgroundColor = System.Drawing.Color.White;
            this.gvIngresos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvIngresos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvIngresos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fechaDataGridViewTextBoxColumn,
            this.descripcionDataGridViewTextBoxColumn});
            this.gvIngresos.DataSource = this.entCatalogoGenericoBindingSource;
            this.gvIngresos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvIngresos.GridColor = System.Drawing.Color.DimGray;
            this.gvIngresos.Location = new System.Drawing.Point(12, 53);
            this.gvIngresos.Name = "gvIngresos";
            this.gvIngresos.ReadOnly = true;
            this.gvIngresos.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Unicode MS", 6.75F);
            this.gvIngresos.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gvIngresos.RowTemplate.Height = 27;
            this.gvIngresos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvIngresos.Size = new System.Drawing.Size(495, 320);
            this.gvIngresos.TabIndex = 89;
            this.gvIngresos.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvGastosEmpresa_CellContentDoubleClick);
            // 
            // fechaDataGridViewTextBoxColumn
            // 
            this.fechaDataGridViewTextBoxColumn.DataPropertyName = "Fecha";
            dataGridViewCellStyle1.Format = "d";
            this.fechaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.fechaDataGridViewTextBoxColumn.FillWeight = 1.5F;
            this.fechaDataGridViewTextBoxColumn.HeaderText = "Fecha";
            this.fechaDataGridViewTextBoxColumn.Name = "fechaDataGridViewTextBoxColumn";
            this.fechaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descripcionDataGridViewTextBoxColumn
            // 
            this.descripcionDataGridViewTextBoxColumn.DataPropertyName = "Descripcion";
            this.descripcionDataGridViewTextBoxColumn.FillWeight = 5F;
            this.descripcionDataGridViewTextBoxColumn.HeaderText = "Descripcion";
            this.descripcionDataGridViewTextBoxColumn.Name = "descripcionDataGridViewTextBoxColumn";
            this.descripcionDataGridViewTextBoxColumn.ReadOnly = true;
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
            // SeleccionaIngreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(519, 452);
            this.Controls.Add(this.pnlEntradasPorMes);
            this.Controls.Add(this.gvIngresos);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SeleccionaIngreso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SELECCIÓN DE ENTRADA";
            this.Load += new System.EventHandler(this.SeleccionaFactura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.entCatalogoGenericoBindingSource)).EndInit();
            this.pnlEntradasPorMes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvIngresos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView gvIngresos;
        private System.Windows.Forms.BindingSource entCatalogoGenericoBindingSource;
        private System.Windows.Forms.Panel pnlEntradasPorMes;
        private System.Windows.Forms.ComboBox cmbMesesEntradas;
        private System.Windows.Forms.ComboBox cmbAñoEntradas;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionDataGridViewTextBoxColumn;
    }
}