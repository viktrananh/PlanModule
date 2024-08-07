
namespace PLAN_MODULE
{
    partial class CONFIRM_BILL_IMPORT
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlCheck = new System.Windows.Forms.Panel();
            this.rdoNG = new System.Windows.Forms.RadioButton();
            this.rdoOK = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbComfirm = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.gr = new System.Windows.Forms.GroupBox();
            this.dgvDetailBill = new System.Windows.Forms.DataGridView();
            this.part = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtyReal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbBillNumber = new System.Windows.Forms.Label();
            this.dgvBill = new System.Windows.Forms.DataGridView();
            this.bill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.pnlCheck.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetailBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBill)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlCheck);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtComment);
            this.panel1.Controls.Add(this.gr);
            this.panel1.Controls.Add(this.lbBillNumber);
            this.panel1.Controls.Add(this.dgvBill);
            this.panel1.Location = new System.Drawing.Point(2, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1021, 496);
            this.panel1.TabIndex = 1;
            // 
            // pnlCheck
            // 
            this.pnlCheck.Controls.Add(this.rdoNG);
            this.pnlCheck.Controls.Add(this.rdoOK);
            this.pnlCheck.Location = new System.Drawing.Point(342, 49);
            this.pnlCheck.Name = "pnlCheck";
            this.pnlCheck.Size = new System.Drawing.Size(421, 30);
            this.pnlCheck.TabIndex = 1;
            // 
            // rdoNG
            // 
            this.rdoNG.AutoSize = true;
            this.rdoNG.Location = new System.Drawing.Point(291, 9);
            this.rdoNG.Name = "rdoNG";
            this.rdoNG.Size = new System.Drawing.Size(40, 18);
            this.rdoNG.TabIndex = 9;
            this.rdoNG.TabStop = true;
            this.rdoNG.Text = "NG";
            this.rdoNG.UseVisualStyleBackColor = true;
            // 
            // rdoOK
            // 
            this.rdoOK.AutoSize = true;
            this.rdoOK.Location = new System.Drawing.Point(119, 6);
            this.rdoOK.Name = "rdoOK";
            this.rdoOK.Size = new System.Drawing.Size(41, 18);
            this.rdoOK.TabIndex = 8;
            this.rdoOK.TabStop = true;
            this.rdoOK.Text = "OK";
            this.rdoOK.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbComfirm);
            this.groupBox1.Controls.Add(this.txtBarcode);
            this.groupBox1.Location = new System.Drawing.Point(769, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 128);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Xác Nhận";
            // 
            // lbComfirm
            // 
            this.lbComfirm.AutoSize = true;
            this.lbComfirm.Location = new System.Drawing.Point(6, 51);
            this.lbComfirm.Name = "lbComfirm";
            this.lbComfirm.Size = new System.Drawing.Size(102, 14);
            this.lbComfirm.TabIndex = 8;
            this.lbComfirm.Text = "Bộ Phận Xác Nhận :";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(9, 68);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.PasswordChar = '*';
            this.txtBarcode.Size = new System.Drawing.Size(236, 20);
            this.txtBarcode.TabIndex = 7;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(347, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(268, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "Note : Vui lòng nhập rõ lý do ( sử dụng tiếng việt có dấu)";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(343, 83);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(421, 51);
            this.txtComment.TabIndex = 5;
            // 
            // gr
            // 
            this.gr.Controls.Add(this.dgvDetailBill);
            this.gr.Location = new System.Drawing.Point(343, 165);
            this.gr.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gr.Name = "gr";
            this.gr.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gr.Size = new System.Drawing.Size(674, 317);
            this.gr.TabIndex = 3;
            this.gr.TabStop = false;
            this.gr.Text = "Chi tiết phiếu";
            // 
            // dgvDetailBill
            // 
            this.dgvDetailBill.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetailBill.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvDetailBill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetailBill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.part,
            this.qty,
            this.qtyReal});
            this.dgvDetailBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetailBill.Location = new System.Drawing.Point(4, 16);
            this.dgvDetailBill.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvDetailBill.Name = "dgvDetailBill";
            this.dgvDetailBill.Size = new System.Drawing.Size(666, 298);
            this.dgvDetailBill.TabIndex = 0;
            // 
            // part
            // 
            this.part.DataPropertyName = "PART_NUMBER";
            this.part.HeaderText = "Part Number";
            this.part.Name = "part";
            // 
            // qty
            // 
            this.qty.DataPropertyName = "QTY_CUS";
            this.qty.HeaderText = "Sô lượng trên phiếu";
            this.qty.Name = "qty";
            // 
            // qtyReal
            // 
            this.qtyReal.DataPropertyName = "QTY_REAL";
            this.qtyReal.HeaderText = "Số lượng thực thế";
            this.qtyReal.Name = "qtyReal";
            // 
            // lbBillNumber
            // 
            this.lbBillNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lbBillNumber.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBillNumber.Location = new System.Drawing.Point(343, 6);
            this.lbBillNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbBillNumber.Name = "lbBillNumber";
            this.lbBillNumber.Size = new System.Drawing.Size(421, 38);
            this.lbBillNumber.TabIndex = 2;
            this.lbBillNumber.Text = "Bill Number :";
            // 
            // dgvBill
            // 
            this.dgvBill.AllowUserToAddRows = false;
            this.dgvBill.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBill.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvBill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.bill,
            this.status});
            this.dgvBill.Location = new System.Drawing.Point(2, 3);
            this.dgvBill.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvBill.Name = "dgvBill";
            this.dgvBill.Size = new System.Drawing.Size(334, 485);
            this.dgvBill.TabIndex = 0;
            this.dgvBill.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBill_CellContentClick);
            // 
            // bill
            // 
            this.bill.DataPropertyName = "BILL_NUMBER";
            this.bill.FillWeight = 98.47717F;
            this.bill.HeaderText = "Phiếu Nhập";
            this.bill.Name = "bill";
            // 
            // status
            // 
            this.status.DataPropertyName = "STATUS";
            this.status.FillWeight = 101.5229F;
            this.status.HeaderText = "Trạng Thái";
            this.status.Name = "status";
            // 
            // CONFIRM_BILL_IMPORT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CONFIRM_BILL_IMPORT";
            this.Size = new System.Drawing.Size(1027, 502);
            this.Load += new System.EventHandler(this.CONFIRM_BILL_IMPORT_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlCheck.ResumeLayout(false);
            this.pnlCheck.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gr.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetailBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBill)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlCheck;
        private System.Windows.Forms.RadioButton rdoNG;
        private System.Windows.Forms.RadioButton rdoOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbComfirm;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.GroupBox gr;
        private System.Windows.Forms.DataGridView dgvDetailBill;
        private System.Windows.Forms.DataGridViewTextBoxColumn part;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtyReal;
        private System.Windows.Forms.Label lbBillNumber;
        private System.Windows.Forms.DataGridView dgvBill;
        private System.Windows.Forms.DataGridViewTextBoxColumn bill;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
    }
}
