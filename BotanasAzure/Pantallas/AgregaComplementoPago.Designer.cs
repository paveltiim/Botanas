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
            this.chkRecalcularFactura = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNumParcialidad = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTotalFactura = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSaldoAnterior = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSaldoPendiente = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnlDatosFacturacion40 = new System.Windows.Forms.Panel();
            this.label35 = new System.Windows.Forms.Label();
            this.cmbRegimenFiscal = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtCP = new System.Windows.Forms.TextBox();
            this.txtRegimenFiscal = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFolio = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUUID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRFC = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombreFiscal = new System.Windows.Forms.TextBox();
            this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
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
            this.pnlDatosFacturacion40.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFacturacion
            // 
            this.pnlFacturacion.Controls.Add(this.chkRecalcularFactura);
            this.pnlFacturacion.Controls.Add(this.label11);
            this.pnlFacturacion.Controls.Add(this.txtNumParcialidad);
            this.pnlFacturacion.Controls.Add(this.label8);
            this.pnlFacturacion.Controls.Add(this.txtTotalFactura);
            this.pnlFacturacion.Controls.Add(this.label7);
            this.pnlFacturacion.Controls.Add(this.txtSaldoAnterior);
            this.pnlFacturacion.Controls.Add(this.label6);
            this.pnlFacturacion.Controls.Add(this.txtSaldoPendiente);
            this.pnlFacturacion.Controls.Add(this.label3);
            this.pnlFacturacion.Controls.Add(this.groupBox2);
            this.pnlFacturacion.Controls.Add(this.txtCantidadPago);
            this.pnlFacturacion.Location = new System.Drawing.Point(37, 25);
            this.pnlFacturacion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlFacturacion.Name = "pnlFacturacion";
            this.pnlFacturacion.Size = new System.Drawing.Size(621, 571);
            this.pnlFacturacion.TabIndex = 0;
            // 
            // chkRecalcularFactura
            // 
            this.chkRecalcularFactura.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRecalcularFactura.AutoSize = true;
            this.chkRecalcularFactura.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRecalcularFactura.Location = new System.Drawing.Point(297, 436);
            this.chkRecalcularFactura.Margin = new System.Windows.Forms.Padding(4);
            this.chkRecalcularFactura.Name = "chkRecalcularFactura";
            this.chkRecalcularFactura.Size = new System.Drawing.Size(151, 23);
            this.chkRecalcularFactura.TabIndex = 169;
            this.chkRecalcularFactura.Text = "Recalcular Factura";
            this.chkRecalcularFactura.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(304, 492);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 47);
            this.label11.TabIndex = 93;
            this.label11.Text = "Núm Parcialidad:";
            this.label11.Visible = false;
            // 
            // txtNumParcialidad
            // 
            this.txtNumParcialidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumParcialidad.Location = new System.Drawing.Point(384, 503);
            this.txtNumParcialidad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNumParcialidad.Name = "txtNumParcialidad";
            this.txtNumParcialidad.ReadOnly = true;
            this.txtNumParcialidad.Size = new System.Drawing.Size(47, 26);
            this.txtNumParcialidad.TabIndex = 92;
            this.txtNumParcialidad.TabStop = false;
            this.txtNumParcialidad.Text = "1";
            this.txtNumParcialidad.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(288, 473);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 17);
            this.label8.TabIndex = 89;
            this.label8.Text = "Total Factura:";
            this.label8.Visible = false;
            // 
            // txtTotalFactura
            // 
            this.txtTotalFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalFactura.Location = new System.Drawing.Point(398, 466);
            this.txtTotalFactura.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotalFactura.Name = "txtTotalFactura";
            this.txtTotalFactura.ReadOnly = true;
            this.txtTotalFactura.Size = new System.Drawing.Size(120, 26);
            this.txtTotalFactura.TabIndex = 88;
            this.txtTotalFactura.TabStop = false;
            this.txtTotalFactura.Text = "$0.00";
            this.txtTotalFactura.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 441);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 17);
            this.label7.TabIndex = 87;
            this.label7.Text = "Saldo Anterior:";
            // 
            // txtSaldoAnterior
            // 
            this.txtSaldoAnterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaldoAnterior.Location = new System.Drawing.Point(145, 434);
            this.txtSaldoAnterior.Margin = new System.Windows.Forms.Padding(4);
            this.txtSaldoAnterior.Name = "txtSaldoAnterior";
            this.txtSaldoAnterior.ReadOnly = true;
            this.txtSaldoAnterior.Size = new System.Drawing.Size(120, 26);
            this.txtSaldoAnterior.TabIndex = 3;
            this.txtSaldoAnterior.TabStop = false;
            this.txtSaldoAnterior.Text = "$0.00";
            this.txtSaldoAnterior.TextChanged += new System.EventHandler(this.txtSaldoAnterior_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 475);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 17);
            this.label6.TabIndex = 85;
            this.label6.Text = "Cantidad Pago:";
            // 
            // txtSaldoPendiente
            // 
            this.txtSaldoPendiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaldoPendiente.Location = new System.Drawing.Point(145, 503);
            this.txtSaldoPendiente.Margin = new System.Windows.Forms.Padding(4);
            this.txtSaldoPendiente.Name = "txtSaldoPendiente";
            this.txtSaldoPendiente.ReadOnly = true;
            this.txtSaldoPendiente.Size = new System.Drawing.Size(120, 26);
            this.txtSaldoPendiente.TabIndex = 84;
            this.txtSaldoPendiente.TabStop = false;
            this.txtSaldoPendiente.Text = "$0.00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 510);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 17);
            this.label3.TabIndex = 37;
            this.label3.Text = "Saldo Pendiente:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pnlDatosFacturacion40);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtFolio);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtUUID);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtRFC);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtNombreFiscal);
            this.groupBox2.Controls.Add(this.dtpFechaPago);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.cmbFormaPago);
            this.groupBox2.Controls.Add(this.txtFormaPago);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtEmail);
            this.groupBox2.Location = new System.Drawing.Point(7, 4);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(611, 409);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos de Factura";
            // 
            // pnlDatosFacturacion40
            // 
            this.pnlDatosFacturacion40.Controls.Add(this.label35);
            this.pnlDatosFacturacion40.Controls.Add(this.cmbRegimenFiscal);
            this.pnlDatosFacturacion40.Controls.Add(this.label27);
            this.pnlDatosFacturacion40.Controls.Add(this.txtCP);
            this.pnlDatosFacturacion40.Controls.Add(this.txtRegimenFiscal);
            this.pnlDatosFacturacion40.Controls.Add(this.label12);
            this.pnlDatosFacturacion40.Location = new System.Drawing.Point(32, 156);
            this.pnlDatosFacturacion40.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlDatosFacturacion40.Name = "pnlDatosFacturacion40";
            this.pnlDatosFacturacion40.Size = new System.Drawing.Size(561, 76);
            this.pnlDatosFacturacion40.TabIndex = 142;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(211, 46);
            this.label35.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(17, 24);
            this.label35.TabIndex = 139;
            this.label35.Text = "*";
            // 
            // cmbRegimenFiscal
            // 
            this.cmbRegimenFiscal.BackColor = System.Drawing.SystemColors.Menu;
            this.cmbRegimenFiscal.DisplayMember = "Descripcion";
            this.cmbRegimenFiscal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegimenFiscal.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbRegimenFiscal.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRegimenFiscal.FormattingEnabled = true;
            this.cmbRegimenFiscal.Location = new System.Drawing.Point(107, 9);
            this.cmbRegimenFiscal.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRegimenFiscal.Name = "cmbRegimenFiscal";
            this.cmbRegimenFiscal.Size = new System.Drawing.Size(448, 27);
            this.cmbRegimenFiscal.TabIndex = 134;
            this.cmbRegimenFiscal.ValueMember = "Id";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(3, 14);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(96, 15);
            this.label27.TabIndex = 136;
            this.label27.Text = "Régimen Fiscal:";
            // 
            // txtCP
            // 
            this.txtCP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCP.Location = new System.Drawing.Point(107, 43);
            this.txtCP.Margin = new System.Windows.Forms.Padding(4);
            this.txtCP.Name = "txtCP";
            this.txtCP.Size = new System.Drawing.Size(101, 26);
            this.txtCP.TabIndex = 137;
            // 
            // txtRegimenFiscal
            // 
            this.txtRegimenFiscal.Font = new System.Drawing.Font("Microsoft Tai Le", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegimenFiscal.Location = new System.Drawing.Point(107, 9);
            this.txtRegimenFiscal.Margin = new System.Windows.Forms.Padding(4);
            this.txtRegimenFiscal.Name = "txtRegimenFiscal";
            this.txtRegimenFiscal.Size = new System.Drawing.Size(187, 18);
            this.txtRegimenFiscal.TabIndex = 135;
            this.txtRegimenFiscal.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(68, 46);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 17);
            this.label12.TabIndex = 138;
            this.label12.Text = "CP:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(69, 122);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 114;
            this.label5.Text = "Folio(s):";
            // 
            // txtFolio
            // 
            this.txtFolio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolio.Location = new System.Drawing.Point(139, 116);
            this.txtFolio.Margin = new System.Windows.Forms.Padding(4);
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.ReadOnly = true;
            this.txtFolio.Size = new System.Drawing.Size(448, 26);
            this.txtFolio.TabIndex = 1;
            this.txtFolio.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(83, 89);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 17);
            this.label4.TabIndex = 112;
            this.label4.Text = "UUID:";
            this.label4.Visible = false;
            // 
            // txtUUID
            // 
            this.txtUUID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUUID.Location = new System.Drawing.Point(139, 82);
            this.txtUUID.Margin = new System.Windows.Forms.Padding(4);
            this.txtUUID.Name = "txtUUID";
            this.txtUUID.ReadOnly = true;
            this.txtUUID.Size = new System.Drawing.Size(449, 26);
            this.txtUUID.TabIndex = 0;
            this.txtUUID.TabStop = false;
            this.txtUUID.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 17);
            this.label2.TabIndex = 110;
            this.label2.Text = "Nombre Fiscal:";
            // 
            // txtRFC
            // 
            this.txtRFC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRFC.Location = new System.Drawing.Point(139, 52);
            this.txtRFC.Margin = new System.Windows.Forms.Padding(4);
            this.txtRFC.Name = "txtRFC";
            this.txtRFC.ReadOnly = true;
            this.txtRFC.Size = new System.Drawing.Size(192, 26);
            this.txtRFC.TabIndex = 3;
            this.txtRFC.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 17);
            this.label1.TabIndex = 108;
            this.label1.Text = "R.F.C.:";
            // 
            // txtNombreFiscal
            // 
            this.txtNombreFiscal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreFiscal.Location = new System.Drawing.Point(139, 22);
            this.txtNombreFiscal.Margin = new System.Windows.Forms.Padding(4);
            this.txtNombreFiscal.Name = "txtNombreFiscal";
            this.txtNombreFiscal.ReadOnly = true;
            this.txtNombreFiscal.Size = new System.Drawing.Size(297, 26);
            this.txtNombreFiscal.TabIndex = 2;
            this.txtNombreFiscal.TabStop = false;
            // 
            // dtpFechaPago
            // 
            this.dtpFechaPago.Location = new System.Drawing.Point(139, 304);
            this.dtpFechaPago.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaPago.Name = "dtpFechaPago";
            this.dtpFechaPago.Size = new System.Drawing.Size(297, 22);
            this.dtpFechaPago.TabIndex = 5;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(53, 298);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(81, 43);
            this.label18.TabIndex = 82;
            this.label18.Text = "Fecha de Pago:";
            // 
            // cmbFormaPago
            // 
            this.cmbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormaPago.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.cmbFormaPago.Location = new System.Drawing.Point(139, 256);
            this.cmbFormaPago.Margin = new System.Windows.Forms.Padding(4);
            this.cmbFormaPago.Name = "cmbFormaPago";
            this.cmbFormaPago.Size = new System.Drawing.Size(448, 28);
            this.cmbFormaPago.TabIndex = 4;
            // 
            // txtFormaPago
            // 
            this.txtFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFormaPago.Location = new System.Drawing.Point(139, 256);
            this.txtFormaPago.Margin = new System.Windows.Forms.Padding(4);
            this.txtFormaPago.Name = "txtFormaPago";
            this.txtFormaPago.Size = new System.Drawing.Size(259, 26);
            this.txtFormaPago.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 261);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(109, 17);
            this.label10.TabIndex = 98;
            this.label10.Text = "Forma de Pago:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(80, 348);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 17);
            this.label9.TabIndex = 97;
            this.label9.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(139, 341);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(448, 26);
            this.txtEmail.TabIndex = 5;
            // 
            // txtCantidadPago
            // 
            this.txtCantidadPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidadPago.Location = new System.Drawing.Point(145, 469);
            this.txtCantidadPago.Margin = new System.Windows.Forms.Padding(4);
            this.txtCantidadPago.Name = "txtCantidadPago";
            this.txtCantidadPago.ReadOnly = true;
            this.txtCantidadPago.Size = new System.Drawing.Size(120, 26);
            this.txtCantidadPago.TabIndex = 4;
            this.txtCantidadPago.Text = "$0.00";
            this.txtCantidadPago.TextChanged += new System.EventHandler(this.txtSaldoAnterior_TextChanged);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Image = global::Aires.Properties.Resources.Cancelar;
            this.btnCancelar.Location = new System.Drawing.Point(369, 619);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(160, 74);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold);
            this.btnAgregar.Image = global::Aires.Properties.Resources.Aceptar;
            this.btnAgregar.Location = new System.Drawing.Point(176, 619);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(4);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(160, 74);
            this.btnAgregar.TabIndex = 1;
            this.btnAgregar.Text = "Facturar Complemento";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // AgregaComplementoPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(697, 708);
            this.Controls.Add(this.pnlFacturacion);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AgregaComplementoPago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agrega Complemento Pago";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AgregaComplementoPago_FormClosing);
            this.Load += new System.EventHandler(this.AgregaComplementoPago_Load);
            this.pnlFacturacion.ResumeLayout(false);
            this.pnlFacturacion.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pnlDatosFacturacion40.ResumeLayout(false);
            this.pnlDatosFacturacion40.PerformLayout();
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
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNumParcialidad;
        private System.Windows.Forms.Panel pnlDatosFacturacion40;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.ComboBox cmbRegimenFiscal;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtCP;
        private System.Windows.Forms.TextBox txtRegimenFiscal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkRecalcularFactura;
    }
}