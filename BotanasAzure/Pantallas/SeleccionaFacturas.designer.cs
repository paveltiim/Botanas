namespace Aires.Pantallas
{
    partial class SeleccionaFacturas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.gvFacturasPedido = new System.Windows.Forms.DataGridView();
            this.entPedidoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtFacturaFiltro = new System.Windows.Forms.TextBox();
            this.btnBuscarCliente = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.Sel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clienteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.facturaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pagoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NotasCredito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.debeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estatus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.facturaDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pagoDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notasCreditoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iEPSDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.debeDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detalleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionCFDIDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estatusDescripcionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvFacturasPedido)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entPedidoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Seleccione Factura a Pagar";
            // 
            // gvFacturasPedido
            // 
            this.gvFacturasPedido.AllowUserToAddRows = false;
            this.gvFacturasPedido.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvFacturasPedido.AutoGenerateColumns = false;
            this.gvFacturasPedido.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvFacturasPedido.BackgroundColor = System.Drawing.Color.White;
            this.gvFacturasPedido.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvFacturasPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvFacturasPedido.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Estatus,
            this.facturaDataGridViewTextBoxColumn1,
            this.fechaDataGridViewTextBoxColumn1,
            this.totalDataGridViewTextBoxColumn1,
            this.pagoDataGridViewTextBoxColumn1,
            this.notasCreditoDataGridViewTextBoxColumn,
            this.iEPSDataGridViewTextBoxColumn,
            this.debeDataGridViewTextBoxColumn1,
            this.detalleDataGridViewTextBoxColumn,
            this.uUIDDataGridViewTextBoxColumn,
            this.versionCFDIDataGridViewTextBoxColumn,
            this.estatusDescripcionDataGridViewTextBoxColumn,
            this.Id});
            this.gvFacturasPedido.DataSource = this.entPedidoBindingSource;
            this.gvFacturasPedido.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvFacturasPedido.GridColor = System.Drawing.Color.DimGray;
            this.gvFacturasPedido.Location = new System.Drawing.Point(12, 47);
            this.gvFacturasPedido.Name = "gvFacturasPedido";
            this.gvFacturasPedido.ReadOnly = true;
            this.gvFacturasPedido.RowHeadersVisible = false;
            this.gvFacturasPedido.RowHeadersWidth = 51;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.gvFacturasPedido.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.gvFacturasPedido.RowTemplate.Height = 27;
            this.gvFacturasPedido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvFacturasPedido.Size = new System.Drawing.Size(968, 404);
            this.gvFacturasPedido.TabIndex = 90;
            this.gvFacturasPedido.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvFacturasPedido_CellClick);
            this.gvFacturasPedido.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvFacturasPedido_CellValueChanged);
            // 
            // entPedidoBindingSource
            // 
            this.entPedidoBindingSource.DataSource = typeof(AiresEntidades.EntPedido);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancelar.BackgroundImage = global::Aires.Properties.Resources.Cancelar;
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(508, 460);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(120, 70);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAgregar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAgregar.BackgroundImage = global::Aires.Properties.Resources.Aceptar;
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(355, 460);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(120, 70);
            this.btnAgregar.TabIndex = 10;
            this.btnAgregar.Text = "Seleccionar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtFacturaFiltro
            // 
            this.txtFacturaFiltro.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtFacturaFiltro.Font = new System.Drawing.Font("Microsoft Tai Le", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacturaFiltro.Location = new System.Drawing.Point(433, 19);
            this.txtFacturaFiltro.Margin = new System.Windows.Forms.Padding(4);
            this.txtFacturaFiltro.Name = "txtFacturaFiltro";
            this.txtFacturaFiltro.Size = new System.Drawing.Size(91, 24);
            this.txtFacturaFiltro.TabIndex = 128;
            this.txtFacturaFiltro.TextChanged += new System.EventHandler(this.txtFacturaFiltro_TextChanged);
            // 
            // btnBuscarCliente
            // 
            this.btnBuscarCliente.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBuscarCliente.BackColor = System.Drawing.Color.White;
            this.btnBuscarCliente.BackgroundImage = global::Aires.Properties.Resources.Search;
            this.btnBuscarCliente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBuscarCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarCliente.Location = new System.Drawing.Point(531, 15);
            this.btnBuscarCliente.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(49, 32);
            this.btnBuscarCliente.TabIndex = 127;
            this.btnBuscarCliente.UseVisualStyleBackColor = false;
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Tai Le", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(437, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(57, 19);
            this.label15.TabIndex = 164;
            this.label15.Text = "Factura";
            // 
            // Sel
            // 
            this.Sel.DataPropertyName = "Estatus";
            this.Sel.FillWeight = 0.5F;
            this.Sel.HeaderText = "Sel.";
            this.Sel.MinimumWidth = 6;
            this.Sel.Name = "Sel";
            this.Sel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Sel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Sel.Width = 23;
            // 
            // clienteDataGridViewTextBoxColumn
            // 
            this.clienteDataGridViewTextBoxColumn.DataPropertyName = "Cliente";
            this.clienteDataGridViewTextBoxColumn.FillWeight = 3F;
            this.clienteDataGridViewTextBoxColumn.HeaderText = "Cliente";
            this.clienteDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.clienteDataGridViewTextBoxColumn.Name = "clienteDataGridViewTextBoxColumn";
            this.clienteDataGridViewTextBoxColumn.Width = 136;
            // 
            // fechaDataGridViewTextBoxColumn
            // 
            this.fechaDataGridViewTextBoxColumn.DataPropertyName = "Fecha";
            this.fechaDataGridViewTextBoxColumn.FillWeight = 2F;
            this.fechaDataGridViewTextBoxColumn.HeaderText = "Fecha";
            this.fechaDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.fechaDataGridViewTextBoxColumn.Name = "fechaDataGridViewTextBoxColumn";
            this.fechaDataGridViewTextBoxColumn.Width = 90;
            // 
            // facturaDataGridViewTextBoxColumn
            // 
            this.facturaDataGridViewTextBoxColumn.DataPropertyName = "Factura";
            this.facturaDataGridViewTextBoxColumn.FillWeight = 2F;
            this.facturaDataGridViewTextBoxColumn.HeaderText = "Factura";
            this.facturaDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.facturaDataGridViewTextBoxColumn.Name = "facturaDataGridViewTextBoxColumn";
            this.facturaDataGridViewTextBoxColumn.Width = 91;
            // 
            // totalDataGridViewTextBoxColumn
            // 
            this.totalDataGridViewTextBoxColumn.DataPropertyName = "Total";
            dataGridViewCellStyle8.Format = "c2";
            this.totalDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.totalDataGridViewTextBoxColumn.FillWeight = 2F;
            this.totalDataGridViewTextBoxColumn.HeaderText = "Total";
            this.totalDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.totalDataGridViewTextBoxColumn.Name = "totalDataGridViewTextBoxColumn";
            this.totalDataGridViewTextBoxColumn.Width = 91;
            // 
            // pagoDataGridViewTextBoxColumn
            // 
            this.pagoDataGridViewTextBoxColumn.DataPropertyName = "Pago";
            dataGridViewCellStyle9.Format = "c2";
            this.pagoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.pagoDataGridViewTextBoxColumn.FillWeight = 2F;
            this.pagoDataGridViewTextBoxColumn.HeaderText = "Pago";
            this.pagoDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.pagoDataGridViewTextBoxColumn.Name = "pagoDataGridViewTextBoxColumn";
            this.pagoDataGridViewTextBoxColumn.Width = 90;
            // 
            // NotasCredito
            // 
            this.NotasCredito.DataPropertyName = "NotasCredito";
            dataGridViewCellStyle10.Format = "c2";
            this.NotasCredito.DefaultCellStyle = dataGridViewCellStyle10;
            this.NotasCredito.FillWeight = 2F;
            this.NotasCredito.HeaderText = "Notas Crédito";
            this.NotasCredito.MinimumWidth = 6;
            this.NotasCredito.Name = "NotasCredito";
            this.NotasCredito.Width = 91;
            // 
            // debeDataGridViewTextBoxColumn
            // 
            this.debeDataGridViewTextBoxColumn.DataPropertyName = "Debe";
            dataGridViewCellStyle11.Format = "c2";
            this.debeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.debeDataGridViewTextBoxColumn.FillWeight = 2F;
            this.debeDataGridViewTextBoxColumn.HeaderText = "Debe";
            this.debeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.debeDataGridViewTextBoxColumn.Name = "debeDataGridViewTextBoxColumn";
            this.debeDataGridViewTextBoxColumn.Width = 91;
            // 
            // Estatus
            // 
            this.Estatus.DataPropertyName = "Estatus";
            this.Estatus.FillWeight = 1F;
            this.Estatus.HeaderText = "Sel.";
            this.Estatus.MinimumWidth = 6;
            this.Estatus.Name = "Estatus";
            this.Estatus.ReadOnly = true;
            // 
            // facturaDataGridViewTextBoxColumn1
            // 
            this.facturaDataGridViewTextBoxColumn1.DataPropertyName = "Factura";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.facturaDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            this.facturaDataGridViewTextBoxColumn1.FillWeight = 2F;
            this.facturaDataGridViewTextBoxColumn1.HeaderText = "Factura";
            this.facturaDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.facturaDataGridViewTextBoxColumn1.Name = "facturaDataGridViewTextBoxColumn1";
            this.facturaDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // fechaDataGridViewTextBoxColumn1
            // 
            this.fechaDataGridViewTextBoxColumn1.DataPropertyName = "Fecha";
            this.fechaDataGridViewTextBoxColumn1.FillWeight = 2.5F;
            this.fechaDataGridViewTextBoxColumn1.HeaderText = "Fecha";
            this.fechaDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.fechaDataGridViewTextBoxColumn1.Name = "fechaDataGridViewTextBoxColumn1";
            this.fechaDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // totalDataGridViewTextBoxColumn1
            // 
            this.totalDataGridViewTextBoxColumn1.DataPropertyName = "Total";
            dataGridViewCellStyle2.Format = "c2";
            this.totalDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.totalDataGridViewTextBoxColumn1.FillWeight = 2F;
            this.totalDataGridViewTextBoxColumn1.HeaderText = "Total";
            this.totalDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.totalDataGridViewTextBoxColumn1.Name = "totalDataGridViewTextBoxColumn1";
            this.totalDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // pagoDataGridViewTextBoxColumn1
            // 
            this.pagoDataGridViewTextBoxColumn1.DataPropertyName = "Pago";
            dataGridViewCellStyle3.Format = "c2";
            this.pagoDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle3;
            this.pagoDataGridViewTextBoxColumn1.FillWeight = 2F;
            this.pagoDataGridViewTextBoxColumn1.HeaderText = "Pago";
            this.pagoDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.pagoDataGridViewTextBoxColumn1.Name = "pagoDataGridViewTextBoxColumn1";
            this.pagoDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // notasCreditoDataGridViewTextBoxColumn
            // 
            this.notasCreditoDataGridViewTextBoxColumn.DataPropertyName = "NotasCredito";
            dataGridViewCellStyle4.Format = "c2";
            this.notasCreditoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.notasCreditoDataGridViewTextBoxColumn.FillWeight = 2F;
            this.notasCreditoDataGridViewTextBoxColumn.HeaderText = "Notas Crédito";
            this.notasCreditoDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.notasCreditoDataGridViewTextBoxColumn.Name = "notasCreditoDataGridViewTextBoxColumn";
            this.notasCreditoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iEPSDataGridViewTextBoxColumn
            // 
            this.iEPSDataGridViewTextBoxColumn.DataPropertyName = "IEPS";
            dataGridViewCellStyle5.Format = "c2";
            this.iEPSDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.iEPSDataGridViewTextBoxColumn.FillWeight = 1.5F;
            this.iEPSDataGridViewTextBoxColumn.HeaderText = "IEPS";
            this.iEPSDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.iEPSDataGridViewTextBoxColumn.Name = "iEPSDataGridViewTextBoxColumn";
            this.iEPSDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // debeDataGridViewTextBoxColumn1
            // 
            this.debeDataGridViewTextBoxColumn1.DataPropertyName = "Debe";
            dataGridViewCellStyle6.Format = "c2";
            this.debeDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle6;
            this.debeDataGridViewTextBoxColumn1.FillWeight = 2F;
            this.debeDataGridViewTextBoxColumn1.HeaderText = "Debe";
            this.debeDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.debeDataGridViewTextBoxColumn1.Name = "debeDataGridViewTextBoxColumn1";
            this.debeDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // detalleDataGridViewTextBoxColumn
            // 
            this.detalleDataGridViewTextBoxColumn.DataPropertyName = "Detalle";
            this.detalleDataGridViewTextBoxColumn.FillWeight = 3F;
            this.detalleDataGridViewTextBoxColumn.HeaderText = "Detalle";
            this.detalleDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.detalleDataGridViewTextBoxColumn.Name = "detalleDataGridViewTextBoxColumn";
            this.detalleDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // uUIDDataGridViewTextBoxColumn
            // 
            this.uUIDDataGridViewTextBoxColumn.DataPropertyName = "UUID";
            this.uUIDDataGridViewTextBoxColumn.FillWeight = 4F;
            this.uUIDDataGridViewTextBoxColumn.HeaderText = "UUID";
            this.uUIDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.uUIDDataGridViewTextBoxColumn.Name = "uUIDDataGridViewTextBoxColumn";
            this.uUIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // versionCFDIDataGridViewTextBoxColumn
            // 
            this.versionCFDIDataGridViewTextBoxColumn.DataPropertyName = "VersionCFDI";
            this.versionCFDIDataGridViewTextBoxColumn.FillWeight = 1F;
            this.versionCFDIDataGridViewTextBoxColumn.HeaderText = "Versión CFDI";
            this.versionCFDIDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.versionCFDIDataGridViewTextBoxColumn.Name = "versionCFDIDataGridViewTextBoxColumn";
            this.versionCFDIDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // estatusDescripcionDataGridViewTextBoxColumn
            // 
            this.estatusDescripcionDataGridViewTextBoxColumn.DataPropertyName = "EstatusDescripcion";
            this.estatusDescripcionDataGridViewTextBoxColumn.FillWeight = 2F;
            this.estatusDescripcionDataGridViewTextBoxColumn.HeaderText = "Estatus";
            this.estatusDescripcionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.estatusDescripcionDataGridViewTextBoxColumn.Name = "estatusDescripcionDataGridViewTextBoxColumn";
            this.estatusDescripcionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.FillWeight = 1F;
            this.Id.HeaderText = "PedidoId";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // SeleccionaFacturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(992, 533);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtFacturaFiltro);
            this.Controls.Add(this.btnBuscarCliente);
            this.Controls.Add(this.gvFacturasPedido);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SeleccionaFacturas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SELECCIÓN DE FACTURA";
            this.Load += new System.EventHandler(this.SeleccionaFactura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvFacturasPedido)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entPedidoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.BindingSource entPedidoBindingSource;
        private System.Windows.Forms.DataGridView gvFacturasPedido;
        private System.Windows.Forms.TextBox txtFacturaFiltro;
        private System.Windows.Forms.Button btnBuscarCliente;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Sel;
        private System.Windows.Forms.DataGridViewTextBoxColumn clienteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn facturaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pagoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NotasCredito;
        private System.Windows.Forms.DataGridViewTextBoxColumn debeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Estatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn facturaDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn pagoDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn notasCreditoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iEPSDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn debeDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn detalleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uUIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionCFDIDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn estatusDescripcionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
    }
}