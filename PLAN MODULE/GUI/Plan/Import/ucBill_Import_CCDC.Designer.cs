
namespace PLAN_MODULE
{
    partial class ucTOOLS
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvView = new System.Windows.Forms.DataGridView();
            this.dtpkTimeEx = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.btnComfirm = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtShippingFee = new System.Windows.Forms.TextBox();
            this.chkShipping = new System.Windows.Forms.CheckBox();
            this.btnAddData = new System.Windows.Forms.Button();
            this.pnlFunction = new System.Windows.Forms.Panel();
            this.cbCus = new System.Windows.Forms.ComboBox();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.cboVat = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lbDescription = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotalCasch = new System.Windows.Forms.TextBox();
            this.rdoUSA = new System.Windows.Forms.RadioButton();
            this.rdoVND = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboUnit = new System.Windows.Forms.ComboBox();
            this.txtMfgPart = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNums = new System.Windows.Forms.TextBox();
            this.txtInterPart = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.bntCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbBillNumber = new System.Windows.Forms.Label();
            this.btnUpFile = new System.Windows.Forms.Button();
            this.dgvListBill = new System.Windows.Forms.DataGridView();
            this.billNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.intendDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label41 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cboVender = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCusPart = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.pnlFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListBill)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvView
            // 
            this.dgvView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvView.Location = new System.Drawing.Point(3, 16);
            this.dgvView.Name = "dgvView";
            this.dgvView.Size = new System.Drawing.Size(730, 189);
            this.dgvView.TabIndex = 0;
            this.dgvView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvView_CellContentClick);
            // 
            // dtpkTimeEx
            // 
            this.dtpkTimeEx.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtpkTimeEx.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkTimeEx.Location = new System.Drawing.Point(304, 29);
            this.dtpkTimeEx.Name = "dtpkTimeEx";
            this.dtpkTimeEx.Size = new System.Drawing.Size(158, 20);
            this.dtpkTimeEx.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(301, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 14);
            this.label10.TabIndex = 4;
            this.label10.Text = "Ngày nhập";
            // 
            // btnComfirm
            // 
            this.btnComfirm.BackColor = System.Drawing.Color.Transparent;
            this.btnComfirm.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComfirm.Location = new System.Drawing.Point(486, 17);
            this.btnComfirm.Name = "btnComfirm";
            this.btnComfirm.Size = new System.Drawing.Size(100, 33);
            this.btnComfirm.TabIndex = 3;
            this.btnComfirm.Text = "Tạo phiếu";
            this.btnComfirm.UseVisualStyleBackColor = false;
            this.btnComfirm.Click += new System.EventHandler(this.btnComfirm_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dgvView);
            this.groupBox3.Location = new System.Drawing.Point(261, 271);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(736, 208);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Chi tiết phiếu";
            // 
            // txtShippingFee
            // 
            this.txtShippingFee.Location = new System.Drawing.Point(96, 43);
            this.txtShippingFee.Name = "txtShippingFee";
            this.txtShippingFee.Size = new System.Drawing.Size(172, 20);
            this.txtShippingFee.TabIndex = 7;
            this.txtShippingFee.TextChanged += new System.EventHandler(this.txtShippingFee_TextChanged);
            this.txtShippingFee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtShippingFee_KeyPress);
            // 
            // chkShipping
            // 
            this.chkShipping.AutoSize = true;
            this.chkShipping.Location = new System.Drawing.Point(11, 45);
            this.chkShipping.Name = "chkShipping";
            this.chkShipping.Size = new System.Drawing.Size(83, 18);
            this.chkShipping.TabIndex = 6;
            this.chkShipping.Text = "Shipping fee";
            this.chkShipping.UseVisualStyleBackColor = true;
            this.chkShipping.CheckedChanged += new System.EventHandler(this.chkShipping_CheckedChanged);
            // 
            // btnAddData
            // 
            this.btnAddData.BackColor = System.Drawing.Color.Lime;
            this.btnAddData.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddData.Location = new System.Drawing.Point(360, 133);
            this.btnAddData.Name = "btnAddData";
            this.btnAddData.Size = new System.Drawing.Size(137, 27);
            this.btnAddData.TabIndex = 15;
            this.btnAddData.Text = "Thêm mã";
            this.btnAddData.UseVisualStyleBackColor = false;
            this.btnAddData.Click += new System.EventHandler(this.btnAddData_Click);
            // 
            // pnlFunction
            // 
            this.pnlFunction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFunction.Controls.Add(this.cbCus);
            this.pnlFunction.Controls.Add(this.labelX8);
            this.pnlFunction.Controls.Add(this.cboVat);
            this.pnlFunction.Controls.Add(this.label9);
            this.pnlFunction.Controls.Add(this.lbDescription);
            this.pnlFunction.Controls.Add(this.label7);
            this.pnlFunction.Controls.Add(this.txtTotalCasch);
            this.pnlFunction.Controls.Add(this.rdoUSA);
            this.pnlFunction.Controls.Add(this.rdoVND);
            this.pnlFunction.Controls.Add(this.label8);
            this.pnlFunction.Controls.Add(this.label6);
            this.pnlFunction.Controls.Add(this.txtPrice);
            this.pnlFunction.Controls.Add(this.label5);
            this.pnlFunction.Controls.Add(this.cboUnit);
            this.pnlFunction.Controls.Add(this.btnAddData);
            this.pnlFunction.Controls.Add(this.txtCusPart);
            this.pnlFunction.Controls.Add(this.txtMfgPart);
            this.pnlFunction.Controls.Add(this.label4);
            this.pnlFunction.Controls.Add(this.label1);
            this.pnlFunction.Controls.Add(this.txtNums);
            this.pnlFunction.Controls.Add(this.label12);
            this.pnlFunction.Controls.Add(this.txtInterPart);
            this.pnlFunction.Controls.Add(this.label3);
            this.pnlFunction.Location = new System.Drawing.Point(263, 58);
            this.pnlFunction.Name = "pnlFunction";
            this.pnlFunction.Size = new System.Drawing.Size(731, 168);
            this.pnlFunction.TabIndex = 23;
            this.pnlFunction.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlFunction_Paint);
            // 
            // cbCus
            // 
            this.cbCus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCus.FormattingEnabled = true;
            this.cbCus.Location = new System.Drawing.Point(101, 9);
            this.cbCus.Name = "cbCus";
            this.cbCus.Size = new System.Drawing.Size(259, 22);
            this.cbCus.TabIndex = 154;
            // 
            // labelX8
            // 
            this.labelX8.AutoSize = true;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX8.ForeColor = System.Drawing.Color.Black;
            this.labelX8.Location = new System.Drawing.Point(9, 9);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(85, 17);
            this.labelX8.TabIndex = 153;
            this.labelX8.Text = "&Khách hàng:";
            // 
            // cboVat
            // 
            this.cboVat.FormattingEnabled = true;
            this.cboVat.Items.AddRange(new object[] {
            "0%",
            "5%",
            "8%",
            "10%"});
            this.cboVat.Location = new System.Drawing.Point(360, 109);
            this.cboVat.Name = "cboVat";
            this.cboVat.Size = new System.Drawing.Size(139, 22);
            this.cboVat.TabIndex = 34;
            this.cboVat.Text = "0%";
            this.cboVat.SelectedIndexChanged += new System.EventHandler(this.cboVat_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(307, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 14);
            this.label9.TabIndex = 33;
            this.label9.Text = "VAT :";
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Font = new System.Drawing.Font("Times New Roman", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDescription.ForeColor = System.Drawing.Color.Black;
            this.lbDescription.Location = new System.Drawing.Point(104, 61);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(19, 14);
            this.lbDescription.TabIndex = 32;
            this.lbDescription.Text = "....";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 14);
            this.label7.TabIndex = 30;
            this.label7.Text = "Thành tiền :";
            // 
            // txtTotalCasch
            // 
            this.txtTotalCasch.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalCasch.Location = new System.Drawing.Point(104, 139);
            this.txtTotalCasch.Name = "txtTotalCasch";
            this.txtTotalCasch.ReadOnly = true;
            this.txtTotalCasch.Size = new System.Drawing.Size(137, 20);
            this.txtTotalCasch.TabIndex = 29;
            // 
            // rdoUSA
            // 
            this.rdoUSA.AutoSize = true;
            this.rdoUSA.Location = new System.Drawing.Point(586, 111);
            this.rdoUSA.Name = "rdoUSA";
            this.rdoUSA.Size = new System.Drawing.Size(31, 18);
            this.rdoUSA.TabIndex = 28;
            this.rdoUSA.Text = "$";
            this.rdoUSA.UseVisualStyleBackColor = true;
            this.rdoUSA.CheckedChanged += new System.EventHandler(this.rdoUSA_CheckedChanged);
            // 
            // rdoVND
            // 
            this.rdoVND.AutoSize = true;
            this.rdoVND.Checked = true;
            this.rdoVND.Location = new System.Drawing.Point(518, 111);
            this.rdoVND.Name = "rdoVND";
            this.rdoVND.Size = new System.Drawing.Size(48, 18);
            this.rdoVND.TabIndex = 27;
            this.rdoVND.TabStop = true;
            this.rdoVND.Text = "VND";
            this.rdoVND.UseVisualStyleBackColor = true;
            this.rdoVND.CheckedChanged += new System.EventHandler(this.rdoVND_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(56, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 14);
            this.label8.TabIndex = 31;
            this.label8.Text = "Mô tả :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 14);
            this.label6.TabIndex = 26;
            this.label6.Text = "Đơn Giá :";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(104, 115);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(137, 20);
            this.txtPrice.TabIndex = 25;
            this.txtPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrice_KeyDown);
            this.txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrice_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 14);
            this.label5.TabIndex = 24;
            this.label5.Text = "Đơn vị tính :";
            // 
            // cboUnit
            // 
            this.cboUnit.FormattingEnabled = true;
            this.cboUnit.Items.AddRange(new object[] {
            "PCS",
            "Kg",
            "Cái/Chiếc",
            "Gam"});
            this.cboUnit.Location = new System.Drawing.Point(104, 87);
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Size = new System.Drawing.Size(137, 22);
            this.cboUnit.TabIndex = 23;
            // 
            // txtMfgPart
            // 
            this.txtMfgPart.Location = new System.Drawing.Point(101, 38);
            this.txtMfgPart.Name = "txtMfgPart";
            this.txtMfgPart.Size = new System.Drawing.Size(140, 20);
            this.txtMfgPart.TabIndex = 16;
            this.txtMfgPart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMfgPart_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(299, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 14);
            this.label4.TabIndex = 22;
            this.label4.Text = "Số lượng :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(296, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 14);
            this.label1.TabIndex = 17;
            this.label1.Text = "Mã Nội Bộ :";
            // 
            // txtNums
            // 
            this.txtNums.Location = new System.Drawing.Point(360, 85);
            this.txtNums.Name = "txtNums";
            this.txtNums.Size = new System.Drawing.Size(137, 20);
            this.txtNums.TabIndex = 21;
            this.txtNums.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNums_KeyPress);
            // 
            // txtInterPart
            // 
            this.txtInterPart.Location = new System.Drawing.Point(362, 38);
            this.txtInterPart.Name = "txtInterPart";
            this.txtInterPart.ReadOnly = true;
            this.txtInterPart.Size = new System.Drawing.Size(137, 20);
            this.txtInterPart.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 14);
            this.label3.TabIndex = 20;
            this.label3.Text = "Mã nhà sản xuất:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 14);
            this.label11.TabIndex = 37;
            this.label11.Text = "Nhà cung cấp";
            // 
            // bntCancel
            // 
            this.bntCancel.BackColor = System.Drawing.Color.Red;
            this.bntCancel.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntCancel.Location = new System.Drawing.Point(699, 234);
            this.bntCancel.Name = "bntCancel";
            this.bntCancel.Size = new System.Drawing.Size(90, 32);
            this.bntCancel.TabIndex = 99;
            this.bntCancel.Text = "Hủy Phiếu";
            this.bntCancel.UseVisualStyleBackColor = false;
            this.bntCancel.Click += new System.EventHandler(this.bntCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 94;
            this.label2.Text = "Số Phiếu :";
            // 
            // lbBillNumber
            // 
            this.lbBillNumber.AutoSize = true;
            this.lbBillNumber.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBillNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbBillNumber.Location = new System.Drawing.Point(83, 10);
            this.lbBillNumber.Name = "lbBillNumber";
            this.lbBillNumber.Size = new System.Drawing.Size(26, 16);
            this.lbBillNumber.TabIndex = 95;
            this.lbBillNumber.Text = "......";
            // 
            // btnUpFile
            // 
            this.btnUpFile.BackColor = System.Drawing.Color.Transparent;
            this.btnUpFile.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpFile.Location = new System.Drawing.Point(608, 17);
            this.btnUpFile.Name = "btnUpFile";
            this.btnUpFile.Size = new System.Drawing.Size(88, 32);
            this.btnUpFile.TabIndex = 24;
            this.btnUpFile.Text = "Up File";
            this.btnUpFile.UseVisualStyleBackColor = false;
            this.btnUpFile.Click += new System.EventHandler(this.btnUpFile_Click);
            // 
            // dgvListBill
            // 
            this.dgvListBill.AllowUserToAddRows = false;
            this.dgvListBill.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvListBill.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvListBill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListBill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.billNumber,
            this.intendDate});
            this.dgvListBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListBill.Location = new System.Drawing.Point(3, 16);
            this.dgvListBill.Name = "dgvListBill";
            this.dgvListBill.Size = new System.Drawing.Size(251, 492);
            this.dgvListBill.TabIndex = 1;
            this.dgvListBill.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListBill_CellContentClick);
            // 
            // billNumber
            // 
            this.billNumber.DataPropertyName = "BILL_NUMBER";
            this.billNumber.HeaderText = "Số phiếu";
            this.billNumber.Name = "billNumber";
            // 
            // intendDate
            // 
            this.intendDate.DataPropertyName = "INTEND_TIME";
            this.intendDate.HeaderText = "Ngày Xuất";
            this.intendDate.Name = "intendDate";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.dgvListBill);
            this.groupBox1.Location = new System.Drawing.Point(3, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 511);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh Sách Phiếu";
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.Transparent;
            this.label41.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(3, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(989, 34);
            this.label41.TabIndex = 93;
            this.label41.Text = "TẠO PHIẾU YÊU CẦU NHẬP CCDC";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.lbBillNumber);
            this.panel4.Location = new System.Drawing.Point(261, 232);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(411, 33);
            this.panel4.TabIndex = 100;
            // 
            // cboVender
            // 
            this.cboVender.FormattingEnabled = true;
            this.cboVender.Location = new System.Drawing.Point(96, 6);
            this.cboVender.Name = "cboVender";
            this.cboVender.Size = new System.Drawing.Size(172, 22);
            this.cboVender.TabIndex = 38;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.dtpkTimeEx);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.btnComfirm);
            this.panel1.Controls.Add(this.btnUpFile);
            this.panel1.Controls.Add(this.txtShippingFee);
            this.panel1.Controls.Add(this.chkShipping);
            this.panel1.Controls.Add(this.cboVender);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Location = new System.Drawing.Point(263, 485);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(727, 73);
            this.panel1.TabIndex = 102;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(529, 37);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 14);
            this.label12.TabIndex = 20;
            this.label12.Text = "Mã khách:";
            // 
            // txtCusPart
            // 
            this.txtCusPart.Enabled = false;
            this.txtCusPart.Location = new System.Drawing.Point(586, 35);
            this.txtCusPart.Name = "txtCusPart";
            this.txtCusPart.Size = new System.Drawing.Size(140, 20);
            this.txtCusPart.TabIndex = 16;
            // 
            // ucTOOLS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pnlFunction);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.bntCancel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ucTOOLS";
            this.Size = new System.Drawing.Size(997, 564);
            this.Load += new System.EventHandler(this.TOOLS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.pnlFunction.ResumeLayout(false);
            this.pnlFunction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListBill)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvView;
        private System.Windows.Forms.DateTimePicker dtpkTimeEx;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnComfirm;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnAddData;
        private System.Windows.Forms.DataGridView dgvListBill;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNums;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtInterPart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMfgPart;
        private System.Windows.Forms.Panel pnlFunction;
        private System.Windows.Forms.DataGridViewTextBoxColumn billNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn intendDate;
        private System.Windows.Forms.Button btnUpFile;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboUnit;
        private System.Windows.Forms.Button bntCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbBillNumber;
        private System.Windows.Forms.RadioButton rdoUSA;
        private System.Windows.Forms.RadioButton rdoVND;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTotalCasch;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtShippingFee;
        private System.Windows.Forms.CheckBox chkShipping;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboVat;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox cboVender;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbCus;
        private DevComponents.DotNetBar.LabelX labelX8;
        private System.Windows.Forms.TextBox txtCusPart;
        private System.Windows.Forms.Label label12;
    }
}
