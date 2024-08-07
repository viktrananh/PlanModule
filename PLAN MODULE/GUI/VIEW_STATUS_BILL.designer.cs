
namespace KITTING
{
    partial class VIEW_STATUS_BILL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VIEW_STATUS_BILL));
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.billNumberTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.phêDuyệtPhiếuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlAdd = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1031, 44);
            this.label2.TabIndex = 9;
            this.label2.Text = "XÁC NHẬN TRẠNG THÁI PHIẾU";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.billNumberTypeToolStripMenuItem,
            this.phêDuyệtPhiếuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1032, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // billNumberTypeToolStripMenuItem
            // 
            this.billNumberTypeToolStripMenuItem.Name = "billNumberTypeToolStripMenuItem";
            this.billNumberTypeToolStripMenuItem.Size = new System.Drawing.Size(193, 20);
            this.billNumberTypeToolStripMenuItem.Text = "Xác nhập chênh lệnh phiếu nhập";
            this.billNumberTypeToolStripMenuItem.Click += new System.EventHandler(this.billNumberTypeToolStripMenuItem_Click);
            // 
            // phêDuyệtPhiếuToolStripMenuItem
            // 
            this.phêDuyệtPhiếuToolStripMenuItem.Name = "phêDuyệtPhiếuToolStripMenuItem";
            this.phêDuyệtPhiếuToolStripMenuItem.Size = new System.Drawing.Size(105, 20);
            this.phêDuyệtPhiếuToolStripMenuItem.Text = "Phê duyệt phiếu";
            this.phêDuyệtPhiếuToolStripMenuItem.Click += new System.EventHandler(this.phêDuyệtPhiếuToolStripMenuItem_Click);
            // 
            // pnlAdd
            // 
            this.pnlAdd.Location = new System.Drawing.Point(1, 97);
            this.pnlAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlAdd.Name = "pnlAdd";
            this.pnlAdd.Size = new System.Drawing.Size(1027, 502);
            this.pnlAdd.TabIndex = 0;
            // 
            // VIEW_STATUS_BILL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1032, 600);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlAdd);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "VIEW_STATUS_BILL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Confirm Bill";
            this.Load += new System.EventHandler(this.VIEW_STATUS_BILL_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem billNumberTypeToolStripMenuItem;
        private System.Windows.Forms.Panel pnlAdd;
        private System.Windows.Forms.ToolStripMenuItem phêDuyệtPhiếuToolStripMenuItem;
    }
}