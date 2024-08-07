
namespace PLAN_MODULE
{
    partial class PRIORITY
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRIORITY));
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.did = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPartNumber = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetail.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.did,
            this.partNumber,
            this.qty});
            this.dgvDetail.Location = new System.Drawing.Point(4, 160);
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.Size = new System.Drawing.Size(633, 208);
            this.dgvDetail.TabIndex = 0;
            this.dgvDetail.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetail_CellContentClick);
            // 
            // did
            // 
            this.did.DataPropertyName = "PART_DID";
            this.did.HeaderText = "DID";
            this.did.Name = "did";
            // 
            // partNumber
            // 
            this.partNumber.DataPropertyName = "PART_NUMBER";
            this.partNumber.HeaderText = "Mã Nội Bộ";
            this.partNumber.Name = "partNumber";
            // 
            // qty
            // 
            this.qty.DataPropertyName = "QTY";
            this.qty.HeaderText = "Số lượng";
            this.qty.Name = "qty";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(169, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mã nội bộ :";
            // 
            // txtPartNumber
            // 
            this.txtPartNumber.Location = new System.Drawing.Point(235, 50);
            this.txtPartNumber.Name = "txtPartNumber";
            this.txtPartNumber.Size = new System.Drawing.Size(134, 20);
            this.txtPartNumber.TabIndex = 2;
            this.txtPartNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartNumber_KeyDown);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(235, 86);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(134, 20);
            this.textBox2.TabIndex = 4;
            this.textBox2.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "DID :";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(646, 36);
            this.label3.TabIndex = 5;
            this.label3.Text = "PRIORITY COMPONENTS";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnConfirm);
            this.panel1.Location = new System.Drawing.Point(4, 439);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(633, 36);
            this.panel1.TabIndex = 6;
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnConfirm.Location = new System.Drawing.Point(502, 1);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(101, 30);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "Xác Nhập";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(3, 3);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(622, 61);
            this.txtNote.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.txtNote);
            this.panel2.Location = new System.Drawing.Point(6, 369);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(631, 69);
            this.panel2.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnLoad);
            this.panel3.Location = new System.Drawing.Point(4, 119);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(633, 36);
            this.panel3.TabIndex = 10;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Red;
            this.btnCancel.Location = new System.Drawing.Point(314, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.Color.Lime;
            this.btnLoad.Location = new System.Drawing.Point(195, 5);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 26);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Get Data";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // PRIORITY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(645, 476);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPartNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvDetail);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PRIORITY";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Priority ";
            this.Load += new System.EventHandler(this.PRIORITY_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDetail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPartNumber;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.DataGridViewTextBoxColumn did;
        private System.Windows.Forms.DataGridViewTextBoxColumn partNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnLoad;
    }
}