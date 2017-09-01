namespace Aires.Pantallas
{
    partial class CambioDivisa
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
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtCambio = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtValorDivisa = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDivisa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(89, 22);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(73, 21);
            this.txtCantidad.TabIndex = 0;
            this.txtCantidad.TextChanged += new System.EventHandler(this.txtCantidad_TextChanged);
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadPaga_KeyPress);
            this.txtCantidad.Leave += new System.EventHandler(this.txtCantidadPaga_Leave);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(28, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cantidad Divisa";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.BackgroundImage = global::Aires.Properties.Resources.cruzChica;
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(219, 93);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(72, 64);
            this.btnCancelar.TabIndex = 4;
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
            this.btnAgregar.Location = new System.Drawing.Point(98, 93);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(72, 64);
            this.btnAgregar.TabIndex = 3;
            this.btnAgregar.Text = "Aceptar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtCambio
            // 
            this.txtCambio.Enabled = false;
            this.txtCambio.Location = new System.Drawing.Point(89, 59);
            this.txtCambio.Name = "txtCambio";
            this.txtCambio.Size = new System.Drawing.Size(73, 21);
            this.txtCambio.TabIndex = 1;
            this.txtCambio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadPaga_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(207, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 14);
            this.label3.TabIndex = 83;
            this.label3.Text = "Valor";
            // 
            // txtValorDivisa
            // 
            this.txtValorDivisa.Enabled = false;
            this.txtValorDivisa.Location = new System.Drawing.Point(247, 59);
            this.txtValorDivisa.Name = "txtValorDivisa";
            this.txtValorDivisa.Size = new System.Drawing.Size(73, 21);
            this.txtValorDivisa.TabIndex = 82;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(204, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 14);
            this.label4.TabIndex = 85;
            this.label4.Text = "Divisa";
            // 
            // txtDivisa
            // 
            this.txtDivisa.Enabled = false;
            this.txtDivisa.Location = new System.Drawing.Point(247, 22);
            this.txtDivisa.Name = "txtDivisa";
            this.txtDivisa.Size = new System.Drawing.Size(73, 21);
            this.txtDivisa.TabIndex = 84;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(28, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 32);
            this.label2.TabIndex = 86;
            this.label2.Text = "Cambio a MXN";
            // 
            // CambioDivisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(369, 169);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDivisa);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtValorDivisa);
            this.Controls.Add(this.txtCambio);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCantidad);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CambioDivisa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cambio de Divisa";
            this.Load += new System.EventHandler(this.AgregaDeuda_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.TextBox txtCambio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtValorDivisa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDivisa;
        private System.Windows.Forms.Label label2;
    }
}