
namespace PLAN_MODULE
{
    partial class ucLIST_BILL_IMPORT
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnsearch = new System.Windows.Forms.Button();
            this.btnexport = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtwork = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbShowprice = new System.Windows.Forms.CheckBox();
            this.cbtimeFillter = new System.Windows.Forms.CheckBox();
            this.cbdetail = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpfrom = new System.Windows.Forms.DateTimePicker();
            this.txtBillID = new System.Windows.Forms.TextBox();
            this.lbBillImport = new System.Windows.Forms.Label();
            this.dtpend = new System.Windows.Forms.DateTimePicker();
            this.cbtypeProduct = new System.Windows.Forms.ComboBox();
            this.label41 = new System.Windows.Forms.Label();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel5.Controls.Add(this.btnsearch);
            this.panel5.Controls.Add(this.btnexport);
            this.panel5.Controls.Add(this.groupBox1);
            this.panel5.Controls.Add(this.label41);
            this.panel5.Controls.Add(this.dgvDetail);
            this.panel5.Location = new System.Drawing.Point(0, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(994, 561);
            this.panel5.TabIndex = 94;
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnsearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnsearch.Location = new System.Drawing.Point(36, 189);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(75, 23);
            this.btnsearch.TabIndex = 131;
            this.btnsearch.Text = "Tìm Kiếm";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // btnexport
            // 
            this.btnexport.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnexport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnexport.Location = new System.Drawing.Point(159, 188);
            this.btnexport.Name = "btnexport";
            this.btnexport.Size = new System.Drawing.Size(75, 23);
            this.btnexport.TabIndex = 131;
            this.btnexport.Text = "Xuất File";
            this.btnexport.UseVisualStyleBackColor = false;
            this.btnexport.Click += new System.EventHandler(this.btnexport_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtwork);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbShowprice);
            this.groupBox1.Controls.Add(this.cbtimeFillter);
            this.groupBox1.Controls.Add(this.cbdetail);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpfrom);
            this.groupBox1.Controls.Add(this.txtBillID);
            this.groupBox1.Controls.Add(this.lbBillImport);
            this.groupBox1.Controls.Add(this.dtpend);
            this.groupBox1.Controls.Add(this.cbtypeProduct);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(8, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(980, 128);
            this.groupBox1.TabIndex = 130;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin tìm kiếm";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txtwork
            // 
            this.txtwork.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtwork.Location = new System.Drawing.Point(116, 64);
            this.txtwork.Name = "txtwork";
            this.txtwork.Size = new System.Drawing.Size(274, 22);
            this.txtwork.TabIndex = 149;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(57, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 148;
            this.label1.Text = "Work :";
            // 
            // cbShowprice
            // 
            this.cbShowprice.AutoSize = true;
            this.cbShowprice.Location = new System.Drawing.Point(521, 93);
            this.cbShowprice.Name = "cbShowprice";
            this.cbShowprice.Size = new System.Drawing.Size(82, 23);
            this.cbShowprice.TabIndex = 147;
            this.cbShowprice.Text = "Hiện giá";
            this.cbShowprice.UseVisualStyleBackColor = true;
            this.cbShowprice.CheckedChanged += new System.EventHandler(this.cbShowprice_CheckedChanged);
            // 
            // cbtimeFillter
            // 
            this.cbtimeFillter.AutoSize = true;
            this.cbtimeFillter.Location = new System.Drawing.Point(392, 34);
            this.cbtimeFillter.Name = "cbtimeFillter";
            this.cbtimeFillter.Size = new System.Drawing.Size(116, 23);
            this.cbtimeFillter.TabIndex = 146;
            this.cbtimeFillter.Text = "Lọc theo time";
            this.cbtimeFillter.UseVisualStyleBackColor = true;
            // 
            // cbdetail
            // 
            this.cbdetail.AutoSize = true;
            this.cbdetail.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbdetail.Location = new System.Drawing.Point(396, 94);
            this.cbdetail.Name = "cbdetail";
            this.cbdetail.Size = new System.Drawing.Size(112, 23);
            this.cbdetail.TabIndex = 145;
            this.cbdetail.Text = "Xem chi tiết ";
            this.cbdetail.UseVisualStyleBackColor = true;
            this.cbdetail.CheckedChanged += new System.EventHandler(this.cbdetail_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(757, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 19);
            this.label5.TabIndex = 140;
            this.label5.Text = "Đến Ngày :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(517, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 19);
            this.label4.TabIndex = 141;
            this.label4.Text = "Từ Ngày :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 19);
            this.label3.TabIndex = 142;
            this.label3.Text = "Loại Hàng:";
            // 
            // dtpfrom
            // 
            this.dtpfrom.CalendarFont = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfrom.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpfrom.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpfrom.Location = new System.Drawing.Point(600, 34);
            this.dtpfrom.Name = "dtpfrom";
            this.dtpfrom.Size = new System.Drawing.Size(149, 22);
            this.dtpfrom.TabIndex = 136;
            // 
            // txtBillID
            // 
            this.txtBillID.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillID.Location = new System.Drawing.Point(116, 97);
            this.txtBillID.Name = "txtBillID";
            this.txtBillID.Size = new System.Drawing.Size(274, 22);
            this.txtBillID.TabIndex = 129;
            this.txtBillID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillImport_KeyDown);
            // 
            // lbBillImport
            // 
            this.lbBillImport.AutoSize = true;
            this.lbBillImport.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBillImport.Location = new System.Drawing.Point(25, 100);
            this.lbBillImport.Name = "lbBillImport";
            this.lbBillImport.Size = new System.Drawing.Size(80, 17);
            this.lbBillImport.TabIndex = 111;
            this.lbBillImport.Text = "Mã Phiếu :";
            this.lbBillImport.Click += new System.EventHandler(this.lbBillImport_Click);
            // 
            // dtpend
            // 
            this.dtpend.CalendarFont = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpend.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpend.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpend.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpend.Location = new System.Drawing.Point(845, 32);
            this.dtpend.Name = "dtpend";
            this.dtpend.Size = new System.Drawing.Size(129, 22);
            this.dtpend.TabIndex = 137;
            // 
            // cbtypeProduct
            // 
            this.cbtypeProduct.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbtypeProduct.FormattingEnabled = true;
            this.cbtypeProduct.Items.AddRange(new object[] {
            "Công Cụ Dụng Cụ",
            "Nguyên Vật Liệu",
            "Hàng Thành Phẩm"});
            this.cbtypeProduct.Location = new System.Drawing.Point(116, 33);
            this.cbtypeProduct.Name = "cbtypeProduct";
            this.cbtypeProduct.Size = new System.Drawing.Size(274, 23);
            this.cbtypeProduct.TabIndex = 135;
            this.cbtypeProduct.SelectedIndexChanged += new System.EventHandler(this.cbtypeProduct_SelectedIndexChanged);
            // 
            // label41
            // 
            this.label41.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label41.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label41.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ForeColor = System.Drawing.Color.Navy;
            this.label41.Location = new System.Drawing.Point(3, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(988, 32);
            this.label41.TabIndex = 92;
            this.label41.Text = "DANH SÁCH PHIẾU YÊU CẦU NHẬP KHO";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvDetail
            // 
            this.dgvDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvDetail.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Location = new System.Drawing.Point(8, 223);
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.Size = new System.Drawing.Size(980, 334);
            this.dgvDetail.TabIndex = 105;
            this.dgvDetail.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetail_CellContentClick);
            // 
            // LIST_BILL_IMPORT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.panel5);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "LIST_BILL_IMPORT";
            this.Size = new System.Drawing.Size(997, 564);
            this.Load += new System.EventHandler(this.LIST_BILL_IMPORT_Load);
            this.panel5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lbBillImport;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.DataGridView dgvDetail;
        private System.Windows.Forms.TextBox txtBillID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpfrom;
        private System.Windows.Forms.DateTimePicker dtpend;
        private System.Windows.Forms.ComboBox cbtypeProduct;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox cbdetail;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Button btnexport;
        private System.Windows.Forms.CheckBox cbtimeFillter;
        private System.Windows.Forms.CheckBox cbShowprice;
        private System.Windows.Forms.TextBox txtwork;
        private System.Windows.Forms.Label label1;
    }
}
