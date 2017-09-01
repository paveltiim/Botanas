namespace Aires.Pantallas
{
    partial class EstadosDeCuentasClientes
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EstadosDeCuentasClientes));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.EntPedidoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rvPedidosDeudaPorCliente = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label9 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtAdjuntaArchivo = new System.Windows.Forms.TextBox();
            this.btnAdjuntaArchivo = new System.Windows.Forms.Button();
            this.btnEnviaCorreo = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ofdBuscaArchivo = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnBuscarCliente = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtClienteBusqueda = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.entClienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvClientes = new System.Windows.Forms.DataGridView();
            this.nombreDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreFiscalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rFCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.direccionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.EntPedidoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entClienteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvClientes)).BeginInit();
            this.SuspendLayout();
            // 
            // EntPedidoBindingSource
            // 
            this.EntPedidoBindingSource.DataSource = typeof(AiresEntidades.EntPedido);
            // 
            // rvPedidosDeudaPorCliente
            // 
            this.rvPedidosDeudaPorCliente.DocumentMapWidth = 88;
            reportDataSource1.Name = "dsPedidosClientesDeuda";
            reportDataSource1.Value = this.EntPedidoBindingSource;
            this.rvPedidosDeudaPorCliente.LocalReport.DataSources.Add(reportDataSource1);
            this.rvPedidosDeudaPorCliente.LocalReport.ReportEmbeddedResource = "Aires.Reportes.rptEstadoDeCuentaCliente.rdlc";
            this.rvPedidosDeudaPorCliente.Location = new System.Drawing.Point(34, 79);
            this.rvPedidosDeudaPorCliente.Name = "rvPedidosDeudaPorCliente";
            this.rvPedidosDeudaPorCliente.Size = new System.Drawing.Size(944, 650);
            this.rvPedidosDeudaPorCliente.TabIndex = 12;
            this.rvPedidosDeudaPorCliente.Load += new System.EventHandler(this.rvPedidosDeudaPorCliente_Load);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(507, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 14);
            this.label9.TabIndex = 122;
            this.label9.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(546, 14);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(343, 22);
            this.txtEmail.TabIndex = 121;
            this.txtEmail.TextChanged += new System.EventHandler(this.txtEmail_TextChanged);
            // 
            // txtAdjuntaArchivo
            // 
            this.txtAdjuntaArchivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdjuntaArchivo.Location = new System.Drawing.Point(546, 49);
            this.txtAdjuntaArchivo.Name = "txtAdjuntaArchivo";
            this.txtAdjuntaArchivo.Size = new System.Drawing.Size(307, 22);
            this.txtAdjuntaArchivo.TabIndex = 124;
            this.txtAdjuntaArchivo.Click += new System.EventHandler(this.btnAdjuntaArchivo_Click);
            // 
            // btnAdjuntaArchivo
            // 
            this.btnAdjuntaArchivo.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnAdjuntaArchivo.BackgroundImage = global::Aires.Properties.Resources.clipMini;
            this.btnAdjuntaArchivo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdjuntaArchivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdjuntaArchivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdjuntaArchivo.Location = new System.Drawing.Point(859, 41);
            this.btnAdjuntaArchivo.Name = "btnAdjuntaArchivo";
            this.btnAdjuntaArchivo.Size = new System.Drawing.Size(30, 32);
            this.btnAdjuntaArchivo.TabIndex = 125;
            this.btnAdjuntaArchivo.UseVisualStyleBackColor = false;
            this.btnAdjuntaArchivo.Click += new System.EventHandler(this.btnAdjuntaArchivo_Click);
            // 
            // btnEnviaCorreo
            // 
            this.btnEnviaCorreo.BackColor = System.Drawing.Color.White;
            this.btnEnviaCorreo.BackgroundImage = global::Aires.Properties.Resources.Mail_Search__chico_;
            this.btnEnviaCorreo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEnviaCorreo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnviaCorreo.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviaCorreo.Location = new System.Drawing.Point(895, 7);
            this.btnEnviaCorreo.Name = "btnEnviaCorreo";
            this.btnEnviaCorreo.Size = new System.Drawing.Size(83, 66);
            this.btnEnviaCorreo.TabIndex = 123;
            this.btnEnviaCorreo.Text = "Enviar Correo";
            this.btnEnviaCorreo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEnviaCorreo.UseVisualStyleBackColor = false;
            this.btnEnviaCorreo.Click += new System.EventHandler(this.btnEnviaCorreo_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.BackgroundImage = global::Aires.Properties.Resources.cruzChica;
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(350, 665);
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
            this.btnAgregar.Location = new System.Drawing.Point(224, 665);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(73, 64);
            this.btnAgregar.TabIndex = 10;
            this.btnAgregar.Text = "Seleccionar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(445, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 14);
            this.label1.TabIndex = 126;
            this.label1.Text = "Adjuntar Reporte:";
            // 
            // ofdBuscaArchivo
            // 
            this.ofdBuscaArchivo.FileName = "openFileDialog1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 14);
            this.label2.TabIndex = 128;
            this.label2.Text = "Cliente:";
            // 
            // comboBox1
            // 
            this.comboBox1.DisplayMember = "Nombre";
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(85, 11);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(313, 28);
            this.comboBox1.TabIndex = 127;
            this.comboBox1.ValueMember = "Id";
            this.comboBox1.Visible = false;
            // 
            // btnBuscarCliente
            // 
            this.btnBuscarCliente.BackColor = System.Drawing.Color.Transparent;
            this.btnBuscarCliente.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscarCliente.BackgroundImage")));
            this.btnBuscarCliente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBuscarCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarCliente.Location = new System.Drawing.Point(404, 12);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(37, 26);
            this.btnBuscarCliente.TabIndex = 129;
            this.btnBuscarCliente.UseVisualStyleBackColor = false;
            this.btnBuscarCliente.Click += new System.EventHandler(this.btnBuscarCliente_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 21);
            this.label3.TabIndex = 133;
            this.label3.Text = "Nombre:";
            // 
            // txtClienteBusqueda
            // 
            this.txtClienteBusqueda.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClienteBusqueda.Location = new System.Drawing.Point(100, 11);
            this.txtClienteBusqueda.Name = "txtClienteBusqueda";
            this.txtClienteBusqueda.Size = new System.Drawing.Size(298, 29);
            this.txtClienteBusqueda.TabIndex = 130;
            this.txtClienteBusqueda.TextChanged += new System.EventHandler(this.txtClienteBusqueda_TextChanged);
            this.txtClienteBusqueda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClienteBusqueda_KeyPress);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.White;
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(34, 15);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(63, 23);
            this.label23.TabIndex = 131;
            this.label23.Text = "Cliente:";
            // 
            // txtNombre
            // 
            this.txtNombre.Enabled = false;
            this.txtNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(100, 45);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(299, 22);
            this.txtNombre.TabIndex = 134;
            // 
            // entClienteBindingSource
            // 
            this.entClienteBindingSource.DataSource = typeof(AiresEntidades.EntCliente);
            // 
            // gvClientes
            // 
            this.gvClientes.AllowUserToAddRows = false;
            this.gvClientes.AllowUserToDeleteRows = false;
            this.gvClientes.AllowUserToOrderColumns = true;
            this.gvClientes.AutoGenerateColumns = false;
            this.gvClientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvClientes.BackgroundColor = System.Drawing.Color.White;
            this.gvClientes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvClientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombreDataGridViewTextBoxColumn,
            this.nombreFiscalDataGridViewTextBoxColumn,
            this.rFCDataGridViewTextBoxColumn,
            this.direccionDataGridViewTextBoxColumn,
            this.emailDataGridViewTextBoxColumn});
            this.gvClientes.DataSource = this.entClienteBindingSource;
            this.gvClientes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvClientes.GridColor = System.Drawing.Color.DimGray;
            this.gvClientes.Location = new System.Drawing.Point(100, 40);
            this.gvClientes.MultiSelect = false;
            this.gvClientes.Name = "gvClientes";
            this.gvClientes.ReadOnly = true;
            this.gvClientes.RowHeadersVisible = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Unicode MS", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvClientes.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvClientes.RowTemplate.Height = 27;
            this.gvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvClientes.Size = new System.Drawing.Size(607, 50);
            this.gvClientes.TabIndex = 135;
            this.gvClientes.Visible = false;
            this.gvClientes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvClientes_CellDoubleClick);
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
            this.nombreFiscalDataGridViewTextBoxColumn.FillWeight = 3F;
            this.nombreFiscalDataGridViewTextBoxColumn.HeaderText = "NombreFiscal";
            this.nombreFiscalDataGridViewTextBoxColumn.Name = "nombreFiscalDataGridViewTextBoxColumn";
            this.nombreFiscalDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rFCDataGridViewTextBoxColumn
            // 
            this.rFCDataGridViewTextBoxColumn.DataPropertyName = "RFC";
            this.rFCDataGridViewTextBoxColumn.FillWeight = 1F;
            this.rFCDataGridViewTextBoxColumn.HeaderText = "RFC";
            this.rFCDataGridViewTextBoxColumn.Name = "rFCDataGridViewTextBoxColumn";
            this.rFCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // direccionDataGridViewTextBoxColumn
            // 
            this.direccionDataGridViewTextBoxColumn.DataPropertyName = "Direccion";
            this.direccionDataGridViewTextBoxColumn.FillWeight = 3F;
            this.direccionDataGridViewTextBoxColumn.HeaderText = "Direccion";
            this.direccionDataGridViewTextBoxColumn.Name = "direccionDataGridViewTextBoxColumn";
            this.direccionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // emailDataGridViewTextBoxColumn
            // 
            this.emailDataGridViewTextBoxColumn.DataPropertyName = "Email";
            this.emailDataGridViewTextBoxColumn.FillWeight = 2F;
            this.emailDataGridViewTextBoxColumn.HeaderText = "Email";
            this.emailDataGridViewTextBoxColumn.Name = "emailDataGridViewTextBoxColumn";
            this.emailDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // EstadosDeCuentasClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1004, 741);
            this.Controls.Add(this.gvClientes);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtClienteBusqueda);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.btnBuscarCliente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAdjuntaArchivo);
            this.Controls.Add(this.txtAdjuntaArchivo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnEnviaCorreo);
            this.Controls.Add(this.rvPedidosDeudaPorCliente);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "EstadosDeCuentasClientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ESTADO DE CUENTA";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SeleccionaFactura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EntPedidoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entClienteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvClientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAgregar;
        private Microsoft.Reporting.WinForms.ReportViewer rvPedidosDeudaPorCliente;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnEnviaCorreo;
        private System.Windows.Forms.TextBox txtAdjuntaArchivo;
        private System.Windows.Forms.Button btnAdjuntaArchivo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog ofdBuscaArchivo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnBuscarCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtClienteBusqueda;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.BindingSource entClienteBindingSource;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.DataGridView gvClientes;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreFiscalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rFCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn direccionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource EntPedidoBindingSource;
    }
}