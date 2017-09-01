namespace Aires.Pantallas
{
    partial class FiltroClientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FiltroClientes));
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSeleccionaCliente = new System.Windows.Forms.Button();
            this.txtFiltroClientes = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gvClientes = new System.Windows.Forms.DataGridView();
            this.nombreDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreFiscal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.direccionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telefonoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telefono2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.celularDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rFCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entClienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnBuscarProducto = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entClienteBindingSource)).BeginInit();
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
            this.btnCancelar.TabIndex = 77;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnSeleccionaCliente
            // 
            this.btnSeleccionaCliente.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSeleccionaCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionaCliente.Location = new System.Drawing.Point(6, 491);
            this.btnSeleccionaCliente.Name = "btnSeleccionaCliente";
            this.btnSeleccionaCliente.Size = new System.Drawing.Size(82, 27);
            this.btnSeleccionaCliente.TabIndex = 76;
            this.btnSeleccionaCliente.Text = "Seleccionar";
            this.btnSeleccionaCliente.UseVisualStyleBackColor = true;
            this.btnSeleccionaCliente.Click += new System.EventHandler(this.btnSeleccionaCliente_Click);
            // 
            // txtFiltroClientes
            // 
            this.txtFiltroClientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltroClientes.Location = new System.Drawing.Point(9, 9);
            this.txtFiltroClientes.Name = "txtFiltroClientes";
            this.txtFiltroClientes.Size = new System.Drawing.Size(288, 22);
            this.txtFiltroClientes.TabIndex = 74;
            this.txtFiltroClientes.TextChanged += new System.EventHandler(this.btnBuscarClientes_Click);
            this.txtFiltroClientes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFiltroClientes_KeyPress);
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.gvClientes);
            this.panel3.Location = new System.Drawing.Point(6, 33);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1203, 452);
            this.panel3.TabIndex = 79;
            // 
            // gvClientes
            // 
            this.gvClientes.AllowUserToAddRows = false;
            this.gvClientes.AutoGenerateColumns = false;
            this.gvClientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvClientes.BackgroundColor = System.Drawing.Color.White;
            this.gvClientes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvClientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombreDataGridViewTextBoxColumn,
            this.NombreFiscal,
            this.direccionDataGridViewTextBoxColumn,
            this.telefonoDataGridViewTextBoxColumn,
            this.telefono2DataGridViewTextBoxColumn,
            this.celularDataGridViewTextBoxColumn,
            this.rFCDataGridViewTextBoxColumn,
            this.emailDataGridViewTextBoxColumn});
            this.gvClientes.DataSource = this.entClienteBindingSource;
            this.gvClientes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvClientes.GridColor = System.Drawing.Color.DimGray;
            this.gvClientes.Location = new System.Drawing.Point(0, 0);
            this.gvClientes.Name = "gvClientes";
            this.gvClientes.ReadOnly = true;
            this.gvClientes.RowHeadersVisible = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Unicode MS", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvClientes.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvClientes.RowTemplate.Height = 27;
            this.gvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvClientes.Size = new System.Drawing.Size(1203, 448);
            this.gvClientes.TabIndex = 79;
            this.gvClientes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvClientes_CellDoubleClick);
            this.gvClientes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gvClientes_KeyPress);
            // 
            // nombreDataGridViewTextBoxColumn
            // 
            this.nombreDataGridViewTextBoxColumn.DataPropertyName = "Nombre";
            this.nombreDataGridViewTextBoxColumn.FillWeight = 3F;
            this.nombreDataGridViewTextBoxColumn.HeaderText = "Nombre";
            this.nombreDataGridViewTextBoxColumn.Name = "nombreDataGridViewTextBoxColumn";
            this.nombreDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // NombreFiscal
            // 
            this.NombreFiscal.DataPropertyName = "NombreFiscal";
            this.NombreFiscal.FillWeight = 4F;
            this.NombreFiscal.HeaderText = "NombreFiscal";
            this.NombreFiscal.Name = "NombreFiscal";
            this.NombreFiscal.ReadOnly = true;
            // 
            // direccionDataGridViewTextBoxColumn
            // 
            this.direccionDataGridViewTextBoxColumn.DataPropertyName = "Direccion";
            this.direccionDataGridViewTextBoxColumn.FillWeight = 3F;
            this.direccionDataGridViewTextBoxColumn.HeaderText = "Direccion";
            this.direccionDataGridViewTextBoxColumn.Name = "direccionDataGridViewTextBoxColumn";
            this.direccionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // telefonoDataGridViewTextBoxColumn
            // 
            this.telefonoDataGridViewTextBoxColumn.DataPropertyName = "Telefono";
            this.telefonoDataGridViewTextBoxColumn.FillWeight = 1F;
            this.telefonoDataGridViewTextBoxColumn.HeaderText = "Telefono";
            this.telefonoDataGridViewTextBoxColumn.Name = "telefonoDataGridViewTextBoxColumn";
            this.telefonoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // telefono2DataGridViewTextBoxColumn
            // 
            this.telefono2DataGridViewTextBoxColumn.DataPropertyName = "Telefono2";
            this.telefono2DataGridViewTextBoxColumn.FillWeight = 1F;
            this.telefono2DataGridViewTextBoxColumn.HeaderText = "Telefono2";
            this.telefono2DataGridViewTextBoxColumn.Name = "telefono2DataGridViewTextBoxColumn";
            this.telefono2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // celularDataGridViewTextBoxColumn
            // 
            this.celularDataGridViewTextBoxColumn.DataPropertyName = "Celular";
            this.celularDataGridViewTextBoxColumn.FillWeight = 1F;
            this.celularDataGridViewTextBoxColumn.HeaderText = "Celular";
            this.celularDataGridViewTextBoxColumn.Name = "celularDataGridViewTextBoxColumn";
            this.celularDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rFCDataGridViewTextBoxColumn
            // 
            this.rFCDataGridViewTextBoxColumn.DataPropertyName = "RFC";
            this.rFCDataGridViewTextBoxColumn.FillWeight = 2F;
            this.rFCDataGridViewTextBoxColumn.HeaderText = "RFC";
            this.rFCDataGridViewTextBoxColumn.Name = "rFCDataGridViewTextBoxColumn";
            this.rFCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // emailDataGridViewTextBoxColumn
            // 
            this.emailDataGridViewTextBoxColumn.DataPropertyName = "Email";
            this.emailDataGridViewTextBoxColumn.FillWeight = 3F;
            this.emailDataGridViewTextBoxColumn.HeaderText = "Email";
            this.emailDataGridViewTextBoxColumn.Name = "emailDataGridViewTextBoxColumn";
            this.emailDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // entClienteBindingSource
            // 
            this.entClienteBindingSource.DataSource = typeof(AiresEntidades.EntCliente);
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
            this.tabPage1.Controls.Add(this.btnCancelar);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.btnSeleccionaCliente);
            this.tabPage1.Controls.Add(this.txtFiltroClientes);
            this.tabPage1.Controls.Add(this.btnBuscarProducto);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1215, 550);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Clientes";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnBuscarProducto
            // 
            this.btnBuscarProducto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscarProducto.BackgroundImage")));
            this.btnBuscarProducto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBuscarProducto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarProducto.Location = new System.Drawing.Point(303, 7);
            this.btnBuscarProducto.Name = "btnBuscarProducto";
            this.btnBuscarProducto.Size = new System.Drawing.Size(37, 26);
            this.btnBuscarProducto.TabIndex = 75;
            this.btnBuscarProducto.UseVisualStyleBackColor = true;
            this.btnBuscarProducto.Visible = false;
            this.btnBuscarProducto.Click += new System.EventHandler(this.btnBuscarClientes_Click);
            // 
            // FiltroClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1248, 601);
            this.Controls.Add(this.tabControl1);
            this.Name = "FiltroClientes";
            this.Text = "Filtro Clientes";
            this.Activated += new System.EventHandler(this.FiltroClientes_Activated);
            this.Load += new System.EventHandler(this.FiltroClientes_Load);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entClienteBindingSource)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSeleccionaCliente;
        private System.Windows.Forms.Button btnBuscarProducto;
        private System.Windows.Forms.TextBox txtFiltroClientes;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.BindingSource entClienteBindingSource;
        private System.Windows.Forms.DataGridView gvClientes;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreFiscal;
        private System.Windows.Forms.DataGridViewTextBoxColumn direccionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn telefonoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn telefono2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn celularDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rFCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailDataGridViewTextBoxColumn;
    }
}