namespace Aires.Pantallas
{
    partial class AgregaComplementoPago
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
            this.pnlFacturacion = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTotalFactura = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSaldoAnterior = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSaldoPendiente = new System.Windows.Forms.TextBox();
            this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFolio = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUUID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRFC = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombreFiscal = new System.Windows.Forms.TextBox();
            this.cmbFormaPago = new System.Windows.Forms.ComboBox();
            this.txtFormaPago = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtCantidadPago = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.pnlFacturacion.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFacturacion
            // 
            this.pnlFacturacion.Controls.Add(this.label8);
            this.pnlFacturacion.Controls.Add(this.txtTotalFactura);
            this.pnlFacturacion.Controls.Add(this.label7);
            this.pnlFacturacion.Controls.Add(this.txtSaldoAnterior);
            this.pnlFacturacion.Controls.Add(this.label6);
            this.pnlFacturacion.Controls.Add(this.txtSaldoPendiente);
            this.pnlFacturacion.Controls.Add(this.dtpFechaPago);
            this.pnlFacturacion.Controls.Add(this.label18);
            this.pnlFacturacion.Controls.Add(this.label3);
            this.pnlFacturacion.Controls.Add(this.groupBox2);
            this.pnlFacturacion.Controls.Add(this.txtCantidadPago);
            this.pnlFacturacion.Location = new System.Drawing.Point(28, 20);
            this.pnlFacturacion.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFacturacion.Name = "pnlFacturacion";
            this.pnlFacturacion.Size = new System.Drawing.Size(410, 347);
            this.pnlFacturacion.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(224, 203);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 89;
            this.label8.Text = "Total Factura:";
            // 
            // txtTotalFactura
            // 
            this.txtTotalFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalFactura.Location = new System.Drawing.Point(307, 198);
            this.txtTotalFactura.Name = "txtTotalFactura";
            this.txtTotalFactura.ReadOnly = true;
            this.txtTotalFactura.Size = new System.Drawing.Size(91, 22);
            this.txtTotalFactura.TabIndex = 88;
            this.txtTotalFactura.Text = "$0.00";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 203);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 87;
            this.label7.Text = "Saldo Anterior:";
            // 
            // txtSaldoAnterior
            // 
            this.txtSaldoAnterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaldoAnterior.Location = new System.Drawing.Point(109, 198);
            this.txtSaldoAnterior.Name = "txtSaldoAnterior";
            this.txtSaldoAnterior.Size = new System.Drawing.Size(91, 22);
            this.txtSaldoAnterior.TabIndex = 3;
            this.txtSaldoAnterior.Text = "$0.00";
            this.txtSaldoAnterior.TextChanged += new System.EventHandler(this.txtSaldoAnterior_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 231);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 85;
            this.label6.Text = "Cantidad Pago:";
            // 
            // txtSaldoPendiente
            // 
            this.txtSaldoPendiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaldoPendiente.Location = new System.Drawing.Point(109, 254);
            this.txtSaldoPendiente.Name = "txtSaldoPendiente";
            this.txtSaldoPendiente.ReadOnly = true;
            this.txtSaldoPendiente.Size = new System.Drawing.Size(91, 22);
            this.txtSaldoPendiente.TabIndex = 84;
            this.txtSaldoPendiente.Text = "$0.00";
            // 
            // dtpFechaPago
            // 
            this.dtpFechaPago.Location = new System.Drawing.Point(109, 286);
            this.dtpFechaPago.Name = "dtpFechaPago";
            this.dtpFechaPago.Size = new System.Drawing.Size(224, 20);
            this.dtpFechaPago.TabIndex = 5;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(45, 281);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(61, 35);
            this.label18.TabIndex = 82;
            this.label18.Text = "Fecha de Pago:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 259);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Saldo Pendiente:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtFolio);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtUUID);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtRFC);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtNombreFiscal);
            this.groupBox2.Controls.Add(this.cmbFormaPago);
            this.groupBox2.Controls.Add(this.txtFormaPago);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtEmail);
            this.groupBox2.Location = new System.Drawing.Point(5, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 191);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos de Factura";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(64, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 114;
            this.label5.Text = "Folio:";
            // 
            // txtFolio
            // 
            this.txtFolio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolio.Location = new System.Drawing.Point(104, 48);
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.ReadOnly = true;
            this.txtFolio.Size = new System.Drawing.Size(91, 22);
            this.txtFolio.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 112;
            this.label4.Text = "UUID:";
            // 
            // txtUUID
            // 
            this.txtUUID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUUID.Location = new System.Drawing.Point(104, 20);
            this.txtUUID.Name = "txtUUID";
            this.txtUUID.ReadOnly = true;
            this.txtUUID.Size = new System.Drawing.Size(289, 22);
            this.txtUUID.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 110;
            this.label2.Text = "Nombre Fiscal:";
            // 
            // txtRFC
            // 
            this.txtRFC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRFC.Location = new System.Drawing.Point(104, 101);
            this.txtRFC.Name = "txtRFC";
            this.txtRFC.ReadOnly = true;
            this.txtRFC.Size = new System.Drawing.Size(145, 22);
            this.txtRFC.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 108;
            this.label1.Text = "R.F.C.:";
            // 
            // txtNombreFiscal
            // 
            this.txtNombreFiscal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreFiscal.Location = new System.Drawing.Point(104, 76);
            this.txtNombreFiscal.Name = "txtNombreFiscal";
            this.txtNombreFiscal.ReadOnly = true;
            this.txtNombreFiscal.Size = new System.Drawing.Size(224, 22);
            this.txtNombreFiscal.TabIndex = 2;
            // 
            // cmbFormaPago
            // 
            this.cmbFormaPago.FormattingEnabled = true;
            this.cmbFormaPago.Items.AddRange(new object[] {
            "01 - Efectivo",
            "02 - Cheque nominativo",
            "03 - Transferencia electrónica de fondos",
            "04 - Tarjeta de crédito",
            "05 - Monedero electrónico",
            "06 - Dinero electrónico",
            "08 - Vales de despensa",
            "12 - Dación en pago",
            "13 - Pago por subrogación",
            "14 - Pago por consignación",
            "15 - Condonación",
            "17 - Compensación",
            "23 - Novación",
            "24 - Confusión",
            "25 - Remisión de deuda",
            "26 - Prescripción o caducidad",
            "27 - A satisfacción del acreedor",
            "28 - Tarjeta de débito",
            "29 - Tarjeta de servicios",
            "30 - Aplicación de anticipos",
            "99 - Por definir"});
            this.cmbFormaPago.Location = new System.Drawing.Point(104, 129);
            this.cmbFormaPago.Name = "cmbFormaPago";
            this.cmbFormaPago.Size = new System.Drawing.Size(224, 21);
            this.cmbFormaPago.TabIndex = 4;
            // 
            // txtFormaPago
            // 
            this.txtFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFormaPago.Location = new System.Drawing.Point(104, 129);
            this.txtFormaPago.Name = "txtFormaPago";
            this.txtFormaPago.Size = new System.Drawing.Size(195, 22);
            this.txtFormaPago.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 133);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 13);
            this.label10.TabIndex = 98;
            this.label10.Text = "Forma de Pago:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(60, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 97;
            this.label9.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(104, 157);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(195, 22);
            this.txtEmail.TabIndex = 5;
            // 
            // txtCantidadPago
            // 
            this.txtCantidadPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidadPago.Location = new System.Drawing.Point(109, 226);
            this.txtCantidadPago.Name = "txtCantidadPago";
            this.txtCantidadPago.Size = new System.Drawing.Size(91, 22);
            this.txtCantidadPago.TabIndex = 4;
            this.txtCantidadPago.Text = "$0.00";
            this.txtCantidadPago.TextChanged += new System.EventHandler(this.txtSaldoAnterior_TextChanged);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancelar.BackgroundImage = global::Aires.Properties.Resources.cruzChica;
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(253, 377);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(70, 64);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAgregar.BackgroundImage = global::Aires.Properties.Resources.palomitaChica;
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 7.25F, System.Drawing.FontStyle.Bold);
            this.btnAgregar.Location = new System.Drawing.Point(141, 377);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(85, 64);
            this.btnAgregar.TabIndex = 1;
            this.btnAgregar.Text = "Facturar Complemento";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // AgregaComplementoPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(467, 463);
            this.Controls.Add(this.pnlFacturacion);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AgregaComplementoPago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AgregaComplementoPago";
            this.Load += new System.EventHandler(this.AgregaComplementoPago_Load);
            this.pnlFacturacion.ResumeLayout(false);
            this.pnlFacturacion.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFacturacion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTotalFactura;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSaldoAnterior;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSaldoPendiente;
        private System.Windows.Forms.DateTimePicker dtpFechaPago;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFolio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUUID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRFC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombreFiscal;
        private System.Windows.Forms.ComboBox cmbFormaPago;
        private System.Windows.Forms.TextBox txtFormaPago;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtCantidadPago;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAgregar;
    }
}