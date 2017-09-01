namespace Aires.Pantallas
{
    partial class AgregaPago
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
            this.txtCantidadPaga = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFactura = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCantidadPaga
            // 
            this.txtCantidadPaga.Location = new System.Drawing.Point(93, 67);
            this.txtCantidadPaga.Name = "txtCantidadPaga";
            this.txtCantidadPaga.Size = new System.Drawing.Size(100, 21);
            this.txtCantidadPaga.TabIndex = 0;
            this.txtCantidadPaga.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadPaga_KeyPress);
            this.txtCantidadPaga.Leave += new System.EventHandler(this.txtCantidadPaga_Leave);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(32, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cantidad a Pagar:";
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
            this.btnCancelar.Location = new System.Drawing.Point(196, 150);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(70, 65);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAgregar.BackgroundImage = global::Aires.Properties.Resources.palomitaChica;
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(93, 150);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(70, 65);
            this.btnAgregar.TabIndex = 10;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // dtpFechaPago
            // 
            this.dtpFechaPago.Location = new System.Drawing.Point(93, 112);
            this.dtpFechaPago.Name = "dtpFechaPago";
            this.dtpFechaPago.Size = new System.Drawing.Size(258, 21);
            this.dtpFechaPago.TabIndex = 81;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(29, 107);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(61, 35);
            this.label18.TabIndex = 80;
            this.label18.Text = "Fecha de Pago:";
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 14);
            this.label2.TabIndex = 82;
            this.label2.Text = "Factura(s):";
            // 
            // txtFactura
            // 
            this.txtFactura.Location = new System.Drawing.Point(93, 22);
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.ReadOnly = true;
            this.txtFactura.Size = new System.Drawing.Size(258, 21);
            this.txtFactura.TabIndex = 83;
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(269, 67);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(82, 21);
            this.txtTotal.TabIndex = 85;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(230, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 14);
            this.label3.TabIndex = 84;
            this.label3.Text = "Total:";
            // 
            // AgregaPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(392, 228);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFactura);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpFechaPago);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCantidadPaga);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AgregaPago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar Pago";
            this.Load += new System.EventHandler(this.AgregaPago_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCantidadPaga;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DateTimePicker dtpFechaPago;
        private System.Windows.Forms.Label label18;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFactura;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label3;
    }
}