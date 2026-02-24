namespace Aires.Pantallas
{
    partial class AgregaMotivoCancelacion
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
            this.label27 = new System.Windows.Forms.Label();
            this.txtFolioSustitucion = new System.Windows.Forms.TextBox();
            this.gbFormaFechaPago = new System.Windows.Forms.GroupBox();
            this.cmbMotivoCancelacion = new System.Windows.Forms.ComboBox();
            this.txtFormaPago = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFactura = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.gbFormaFechaPago.SuspendLayout();
            this.SuspendLayout();
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label27.AutoSize = true;
            this.label27.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label27.Location = new System.Drawing.Point(7, 48);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(120, 22);
            this.label27.TabIndex = 169;
            this.label27.Text = "Folio Sustituye";
            // 
            // txtFolioSustitucion
            // 
            this.txtFolioSustitucion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolioSustitucion.Location = new System.Drawing.Point(10, 66);
            this.txtFolioSustitucion.Margin = new System.Windows.Forms.Padding(4);
            this.txtFolioSustitucion.Name = "txtFolioSustitucion";
            this.txtFolioSustitucion.ReadOnly = true;
            this.txtFolioSustitucion.Size = new System.Drawing.Size(383, 28);
            this.txtFolioSustitucion.TabIndex = 170;
            // 
            // gbFormaFechaPago
            // 
            this.gbFormaFechaPago.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFormaFechaPago.Controls.Add(this.cmbMotivoCancelacion);
            this.gbFormaFechaPago.Controls.Add(this.label27);
            this.gbFormaFechaPago.Controls.Add(this.txtFormaPago);
            this.gbFormaFechaPago.Controls.Add(this.txtFolioSustitucion);
            this.gbFormaFechaPago.Location = new System.Drawing.Point(31, 113);
            this.gbFormaFechaPago.Name = "gbFormaFechaPago";
            this.gbFormaFechaPago.Size = new System.Drawing.Size(400, 98);
            this.gbFormaFechaPago.TabIndex = 165;
            this.gbFormaFechaPago.TabStop = false;
            this.gbFormaFechaPago.Text = "Motivo Cancelación";
            this.gbFormaFechaPago.Visible = false;
            // 
            // cmbMotivoCancelacion
            // 
            this.cmbMotivoCancelacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMotivoCancelacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMotivoCancelacion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbMotivoCancelacion.FormattingEnabled = true;
            this.cmbMotivoCancelacion.Items.AddRange(new object[] {
            "01 - Comprobantes emitidos con errores con relación.",
            "02 - Comprobantes emitidos con errores sin relación.",
            "03 - No se llevó a cabo la operación.",
            "04 - Operación nominativa relacionada en una factura global."});
            this.cmbMotivoCancelacion.Location = new System.Drawing.Point(6, 19);
            this.cmbMotivoCancelacion.Name = "cmbMotivoCancelacion";
            this.cmbMotivoCancelacion.Size = new System.Drawing.Size(387, 30);
            this.cmbMotivoCancelacion.TabIndex = 163;
            this.cmbMotivoCancelacion.SelectedIndexChanged += new System.EventHandler(this.cmbMotivoCancelacion_SelectedIndexChanged);
            // 
            // txtFormaPago
            // 
            this.txtFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFormaPago.Location = new System.Drawing.Point(6, 19);
            this.txtFormaPago.Name = "txtFormaPago";
            this.txtFormaPago.Size = new System.Drawing.Size(195, 30);
            this.txtFormaPago.TabIndex = 162;
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(111, 68);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(94, 28);
            this.txtTotal.TabIndex = 85;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 22);
            this.label3.TabIndex = 84;
            this.label3.Text = "Total:";
            // 
            // txtFactura
            // 
            this.txtFactura.Location = new System.Drawing.Point(111, 24);
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.ReadOnly = true;
            this.txtFactura.Size = new System.Drawing.Size(313, 28);
            this.txtFactura.TabIndex = 83;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 22);
            this.label2.TabIndex = 82;
            this.label2.Text = "Factura(s):";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancelar.BackgroundImage = global::Aires.Properties.Resources.Cancelar;
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 6.25F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.Location = new System.Drawing.Point(247, 244);
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
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 6.25F, System.Drawing.FontStyle.Bold);
            this.btnAgregar.Location = new System.Drawing.Point(71, 244);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(120, 70);
            this.btnAgregar.TabIndex = 10;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // AgregaMotivoCancelacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(443, 322);
            this.Controls.Add(this.gbFormaFechaPago);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFactura);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AgregaMotivoCancelacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar Motivo Cancelación";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AgregaPago_FormClosing);
            this.Load += new System.EventHandler(this.AgregaPago_Load);
            this.gbFormaFechaPago.ResumeLayout(false);
            this.gbFormaFechaPago.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFactura;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbFormaFechaPago;
        private System.Windows.Forms.ComboBox cmbMotivoCancelacion;
        private System.Windows.Forms.TextBox txtFormaPago;
        private System.Windows.Forms.TextBox txtFolioSustitucion;
        private System.Windows.Forms.Label label27;
    }
}