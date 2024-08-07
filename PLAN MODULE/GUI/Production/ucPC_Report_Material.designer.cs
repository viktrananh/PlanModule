namespace PLAN_MODULE.GUI.Production
{
    partial class ucPC_Report_Material
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
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grAddDetail = new System.Windows.Forms.GroupBox();
            this.btnGetData = new System.Windows.Forms.Button();
            this.cbSearch = new System.Windows.Forms.ComboBox();
            this.lbModel = new System.Windows.Forms.Label();
            this.lbBomVer = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtWork = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.grAddDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Location = new System.Drawing.Point(15, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(926, 28);
            this.label7.TabIndex = 10;
            this.label7.Text = "THỐNG KÊ TÌNH TRẠNG NHẬP- XUẤT LINH KIỆN";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.gridControl1);
            this.groupBox3.Location = new System.Drawing.Point(20, 190);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(937, 387);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thông tin chi tiết";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 16);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(931, 368);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Fast;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // grAddDetail
            // 
            this.grAddDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grAddDetail.Controls.Add(this.btnGetData);
            this.grAddDetail.Controls.Add(this.cbSearch);
            this.grAddDetail.Controls.Add(this.lbModel);
            this.grAddDetail.Controls.Add(this.lbBomVer);
            this.grAddDetail.Controls.Add(this.label10);
            this.grAddDetail.Controls.Add(this.txtWork);
            this.grAddDetail.Controls.Add(this.label2);
            this.grAddDetail.Controls.Add(this.label1);
            this.grAddDetail.Controls.Add(this.label5);
            this.grAddDetail.Location = new System.Drawing.Point(20, 49);
            this.grAddDetail.Name = "grAddDetail";
            this.grAddDetail.Size = new System.Drawing.Size(934, 122);
            this.grAddDetail.TabIndex = 11;
            this.grAddDetail.TabStop = false;
            this.grAddDetail.Text = "Thông tin tìm kiếm";
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(298, 66);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(80, 30);
            this.btnGetData.TabIndex = 22;
            this.btnGetData.Text = "Tìm kiếm";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // cbSearch
            // 
            this.cbSearch.FormattingEnabled = true;
            this.cbSearch.Items.AddRange(new object[] {
            "------------------------",
            "Liệu nhập vào sản xuất",
            "Liệu xuất trả kho",
            "Chuyển liệu"});
            this.cbSearch.Location = new System.Drawing.Point(70, 75);
            this.cbSearch.Name = "cbSearch";
            this.cbSearch.Size = new System.Drawing.Size(204, 21);
            this.cbSearch.TabIndex = 21;
            // 
            // lbModel
            // 
            this.lbModel.AutoSize = true;
            this.lbModel.Location = new System.Drawing.Point(410, 39);
            this.lbModel.Name = "lbModel";
            this.lbModel.Size = new System.Drawing.Size(16, 13);
            this.lbModel.TabIndex = 19;
            this.lbModel.Text = "...";
            // 
            // lbBomVer
            // 
            this.lbBomVer.AutoSize = true;
            this.lbBomVer.Location = new System.Drawing.Point(295, 39);
            this.lbBomVer.Name = "lbBomVer";
            this.lbBomVer.Size = new System.Drawing.Size(16, 13);
            this.lbBomVer.TabIndex = 19;
            this.lbBomVer.Text = "...";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(233, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Bom  Ver :";
            // 
            // txtWork
            // 
            this.txtWork.Location = new System.Drawing.Point(70, 36);
            this.txtWork.Name = "txtWork";
            this.txtWork.Size = new System.Drawing.Size(145, 20);
            this.txtWork.TabIndex = 14;
            this.txtWork.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWork_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Lựa chọn";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(368, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Model";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Work ID";
            // 
            // ucPC_Report_Material
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.grAddDetail);
            this.Controls.Add(this.label7);
            this.Name = "ucPC_Report_Material";
            this.Size = new System.Drawing.Size(957, 582);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.grAddDetail.ResumeLayout(false);
            this.grAddDetail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox grAddDetail;
        private System.Windows.Forms.Label lbBomVer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtWork;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Label lbModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGetData;
    }
}
