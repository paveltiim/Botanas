namespace Aires.Pantallas
{
    partial class EstadoDeCuenta
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.EntPedidoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtAdjuntaArchivo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ofdBuscaArchivo = new System.Windows.Forms.OpenFileDialog();
            this.btnAdjuntaArchivo = new System.Windows.Forms.Button();
            this.btnEnviaCorreo = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.rvPedidosDeudaPorCliente = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.EntPedidoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // EntPedidoBindingSource
            // 
            this.EntPedidoBindingSource.DataMember = "EntPedido";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(236, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 18);
            this.label9.TabIndex = 122;
            this.label9.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(275, 14);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(343, 26);
            this.txtEmail.TabIndex = 121;
            this.txtEmail.TextChanged += new System.EventHandler(this.txtEmail_TextChanged);
            // 
            // txtAdjuntaArchivo
            // 
            this.txtAdjuntaArchivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdjuntaArchivo.Location = new System.Drawing.Point(275, 49);
            this.txtAdjuntaArchivo.Name = "txtAdjuntaArchivo";
            this.txtAdjuntaArchivo.Size = new System.Drawing.Size(307, 26);
            this.txtAdjuntaArchivo.TabIndex = 124;
            this.txtAdjuntaArchivo.Click += new System.EventHandler(this.btnAdjuntaArchivo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(174, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 18);
            this.label1.TabIndex = 126;
            this.label1.Text = "Adjuntar Reporte:";
            // 
            // ofdBuscaArchivo
            // 
            this.ofdBuscaArchivo.FileName = "openFileDialog1";
            // 
            // btnAdjuntaArchivo
            // 
            this.btnAdjuntaArchivo.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnAdjuntaArchivo.BackgroundImage = global::Aires.Properties.Resources.clipMini;
            this.btnAdjuntaArchivo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAdjuntaArchivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdjuntaArchivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdjuntaArchivo.Location = new System.Drawing.Point(588, 41);
            this.btnAdjuntaArchivo.Name = "btnAdjuntaArchivo";
            this.btnAdjuntaArchivo.Size = new System.Drawing.Size(30, 34);
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
            this.btnEnviaCorreo.Location = new System.Drawing.Point(624, 7);
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
            // rvPedidosDeudaPorCliente
            // 
            this.rvPedidosDeudaPorCliente.DocumentMapWidth = 88;
            reportDataSource2.Name = "dsPedidosClientesDeuda";
            reportDataSource2.Value = this.EntPedidoBindingSource;
            this.rvPedidosDeudaPorCliente.LocalReport.DataSources.Add(reportDataSource2);
            this.rvPedidosDeudaPorCliente.LocalReport.ReportEmbeddedResource = "Aires.Reportes.rptEstadoDeCuentaCliente.rdlc";
            this.rvPedidosDeudaPorCliente.Location = new System.Drawing.Point(12, 79);
            this.rvPedidosDeudaPorCliente.Name = "rvPedidosDeudaPorCliente";
            this.rvPedidosDeudaPorCliente.ServerReport.BearerToken = null;
            this.rvPedidosDeudaPorCliente.Size = new System.Drawing.Size(695, 650);
            this.rvPedidosDeudaPorCliente.TabIndex = 12;
            this.rvPedidosDeudaPorCliente.Load += new System.EventHandler(this.rvPedidosDeudaPorCliente_Load);
            // 
            // EstadoDeCuenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(719, 741);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EstadoDeCuenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ESTADO DE CUENTA";
            this.Load += new System.EventHandler(this.SeleccionaFactura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EntPedidoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnEnviaCorreo;
        private System.Windows.Forms.TextBox txtAdjuntaArchivo;
        private System.Windows.Forms.Button btnAdjuntaArchivo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog ofdBuscaArchivo;
        private Microsoft.Reporting.WinForms.ReportViewer rvPedidosDeudaPorCliente;
        private System.Windows.Forms.BindingSource EntPedidoBindingSource;
    }
}