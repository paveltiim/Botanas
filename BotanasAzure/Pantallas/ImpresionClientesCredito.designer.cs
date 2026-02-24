namespace Aires.Pantallas
{
    partial class ImpresionClientesCredito
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
            this.EntClienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.EntPedidoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.EntProductoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.rvEntradas = new Microsoft.Reporting.WinForms.ReportViewer();
            this.entPagoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.EntClienteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EntPedidoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EntProductoBindingSource)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entPagoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // EntClienteBindingSource
            // 
            this.EntClienteBindingSource.DataSource = typeof(AiresEntidades.EntCliente);
            // 
            // EntPedidoBindingSource
            // 
            this.EntPedidoBindingSource.DataMember = "EntPedido";
            // 
            // EntProductoBindingSource
            // 
            this.EntProductoBindingSource.DataMember = "EntProducto";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Tai Le", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1163, 737);
            this.tabControl1.TabIndex = 42;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.rvEntradas);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage4.Size = new System.Drawing.Size(1155, 711);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Impresión";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // rvEntradas
            // 
            this.rvEntradas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rvEntradas.DocumentMapWidth = 88;
            reportDataSource1.Name = "dsPedidosClientesDeuda";
            reportDataSource1.Value = this.EntClienteBindingSource;
            this.rvEntradas.LocalReport.DataSources.Add(reportDataSource1);
            this.rvEntradas.LocalReport.ReportEmbeddedResource = "Aires.Reportes.rptPagosClientesCredito.rdlc";
            this.rvEntradas.Location = new System.Drawing.Point(0, 1);
            this.rvEntradas.Margin = new System.Windows.Forms.Padding(4);
            this.rvEntradas.Name = "rvEntradas";
            this.rvEntradas.ServerReport.BearerToken = null;
            this.rvEntradas.Size = new System.Drawing.Size(1155, 707);
            this.rvEntradas.TabIndex = 0;
            // 
            // entPagoBindingSource
            // 
            this.entPagoBindingSource.DataSource = typeof(AiresEntidades.EntPago);
            // 
            // ImpresionClientesCredito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1181, 755);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ImpresionClientesCredito";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IMPRESIÓN CLIENTES CRÉDITO";
            this.Load += new System.EventHandler(this.SeleccionaFactura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EntClienteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EntPedidoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EntProductoBindingSource)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.entPagoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource entPagoBindingSource;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private Microsoft.Reporting.WinForms.ReportViewer rvEntradas;
        private System.Windows.Forms.BindingSource EntProductoBindingSource;
        private System.Windows.Forms.BindingSource EntPedidoBindingSource;
        private System.Windows.Forms.BindingSource EntClienteBindingSource;
    }
}