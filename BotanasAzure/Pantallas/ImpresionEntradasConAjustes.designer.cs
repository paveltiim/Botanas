namespace Aires.Pantallas
{
    partial class ImpresionEntradasConAjustes
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
            this.EntReporteEntradasAjustesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rvEntradasAjustes = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.EntReporteEntradasAjustesBindingSource)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // EntReporteEntradasAjustesBindingSource
            // 
            this.EntReporteEntradasAjustesBindingSource.DataMember = "EntReporteEntradasAjustes";
            this.EntReporteEntradasAjustesBindingSource.DataSource = typeof(AiresEntidades.EntReporteEntradasAjustes);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Tai Le", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1174, 737);
            this.tabControl1.TabIndex = 42;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rvEntradasAjustes);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1166, 711);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Impresión";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rvEntradasAjustes
            // 
            this.rvEntradasAjustes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rvEntradasAjustes.DocumentMapWidth = 88;
            reportDataSource1.Name = "dsEntradasAjustes";
            reportDataSource1.Value = this.EntReporteEntradasAjustesBindingSource;
            this.rvEntradasAjustes.LocalReport.DataSources.Add(reportDataSource1);
            this.rvEntradasAjustes.LocalReport.ReportEmbeddedResource = "Aires.Reportes.rptEntradasConAjustes.rdlc";
            this.rvEntradasAjustes.Location = new System.Drawing.Point(0, 1);
            this.rvEntradasAjustes.Margin = new System.Windows.Forms.Padding(4);
            this.rvEntradasAjustes.Name = "rvEntradasAjustes";
            this.rvEntradasAjustes.ServerReport.BearerToken = null;
            this.rvEntradasAjustes.Size = new System.Drawing.Size(1166, 707);
            this.rvEntradasAjustes.TabIndex = 0;
            // 
            // ImpresionEntradasConAjustes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1200, 755);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ImpresionEntradasConAjustes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REPORTE ENTRADAS CON AJUSTES";
            this.Load += new System.EventHandler(this.ImpresionEntradasConAjustes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EntReporteEntradasAjustesBindingSource)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private Microsoft.Reporting.WinForms.ReportViewer rvEntradasAjustes;
        private System.Windows.Forms.BindingSource EntReporteEntradasAjustesBindingSource;
    }
}
