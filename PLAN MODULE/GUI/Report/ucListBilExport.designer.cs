namespace PLAN_MODULE
{
    partial class ucListBilExport
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
            this.btnsearch = new System.Windows.Forms.Button();
            this.btnexport = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpend = new System.Windows.Forms.DateTimePicker();
            this.dtpfrom = new System.Windows.Forms.DateTimePicker();
            this.cbtimeFillter = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbdetail = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBillID = new System.Windows.Forms.TextBox();
            this.lbBillImport = new System.Windows.Forms.Label();
            this.cbtypeProduct = new System.Windows.Forms.ComboBox();
            this.dgvDetail = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.panelEx4.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnsearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnsearch.Location = new System.Drawing.Point(507, 82);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(75, 28);
            this.btnsearch.TabIndex = 135;
            this.btnsearch.Text = "Tìm Kiếm";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // btnexport
            // 
            this.btnexport.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnexport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnexport.Location = new System.Drawing.Point(605, 82);
            this.btnexport.Name = "btnexport";
            this.btnexport.Size = new System.Drawing.Size(75, 28);
            this.btnexport.TabIndex = 136;
            this.btnexport.Text = "Xuất File";
            this.btnexport.UseVisualStyleBackColor = false;
            this.btnexport.Click += new System.EventHandler(this.btnexport_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpend);
            this.panel1.Controls.Add(this.dtpfrom);
            this.panel1.Controls.Add(this.cbtimeFillter);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(375, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 70);
            this.panel1.TabIndex = 137;
            // 
            // dtpend
            // 
            this.dtpend.CalendarFont = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpend.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpend.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpend.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpend.Location = new System.Drawing.Point(217, 45);
            this.dtpend.Name = "dtpend";
            this.dtpend.Size = new System.Drawing.Size(122, 22);
            this.dtpend.TabIndex = 137;
            // 
            // dtpfrom
            // 
            this.dtpfrom.CalendarFont = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfrom.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpfrom.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpfrom.Location = new System.Drawing.Point(217, 17);
            this.dtpfrom.Name = "dtpfrom";
            this.dtpfrom.Size = new System.Drawing.Size(122, 22);
            this.dtpfrom.TabIndex = 136;
            // 
            // cbtimeFillter
            // 
            this.cbtimeFillter.AutoSize = true;
            this.cbtimeFillter.Location = new System.Drawing.Point(3, 21);
            this.cbtimeFillter.Name = "cbtimeFillter";
            this.cbtimeFillter.Size = new System.Drawing.Size(92, 18);
            this.cbtimeFillter.TabIndex = 147;
            this.cbtimeFillter.Text = "Lọc theo time";
            this.cbtimeFillter.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(129, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 141;
            this.label4.Text = "Từ Ngày :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(129, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 140;
            this.label5.Text = "Đến Ngày :";
            // 
            // cbdetail
            // 
            this.cbdetail.AutoSize = true;
            this.cbdetail.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbdetail.Location = new System.Drawing.Point(18, 70);
            this.cbdetail.Name = "cbdetail";
            this.cbdetail.Size = new System.Drawing.Size(112, 23);
            this.cbdetail.TabIndex = 145;
            this.cbdetail.Text = "Xem chi tiết ";
            this.cbdetail.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 142;
            this.label3.Text = "Loại Hàng :";
            // 
            // txtBillID
            // 
            this.txtBillID.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillID.Location = new System.Drawing.Point(81, 45);
            this.txtBillID.Name = "txtBillID";
            this.txtBillID.Size = new System.Drawing.Size(267, 20);
            this.txtBillID.TabIndex = 129;
            // 
            // lbBillImport
            // 
            this.lbBillImport.AutoSize = true;
            this.lbBillImport.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBillImport.Location = new System.Drawing.Point(15, 47);
            this.lbBillImport.Name = "lbBillImport";
            this.lbBillImport.Size = new System.Drawing.Size(64, 13);
            this.lbBillImport.TabIndex = 111;
            this.lbBillImport.Text = "Mã Phiếu :";
            // 
            // cbtypeProduct
            // 
            this.cbtypeProduct.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbtypeProduct.FormattingEnabled = true;
            this.cbtypeProduct.Items.AddRange(new object[] {
            "Công Cụ Dụng Cụ",
            "Nguyên Vật Liệu",
            "Hàng Thành Phẩm"});
            this.cbtypeProduct.Location = new System.Drawing.Point(81, 9);
            this.cbtypeProduct.Name = "cbtypeProduct";
            this.cbtypeProduct.Size = new System.Drawing.Size(267, 22);
            this.cbtypeProduct.TabIndex = 135;
            this.cbtypeProduct.SelectedIndexChanged += new System.EventHandler(this.cbtypeProduct_SelectedIndexChanged);
            // 
            // dgvDetail
            // 
            this.dgvDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetail.Location = new System.Drawing.Point(0, 0);
            this.dgvDetail.MainView = this.gridView1;
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.Size = new System.Drawing.Size(763, 266);
            this.dgvDetail.TabIndex = 137;
            this.dgvDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dgvDetail;
            this.gridView1.Name = "gridView1";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx4);
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(769, 460);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 138;
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.groupPanel1);
            this.panelEx4.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Location = new System.Drawing.Point(0, 173);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(769, 287);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 146;
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.dgvDetail);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(769, 287);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 138;
            this.groupPanel1.Text = "Thông tin chi tiết";
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.groupPanel2);
            this.panelEx3.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx3.Location = new System.Drawing.Point(0, 39);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(769, 134);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 145;
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.panel1);
            this.groupPanel2.Controls.Add(this.btnexport);
            this.groupPanel2.Controls.Add(this.btnsearch);
            this.groupPanel2.Controls.Add(this.cbdetail);
            this.groupPanel2.Controls.Add(this.cbtypeProduct);
            this.groupPanel2.Controls.Add(this.lbBillImport);
            this.groupPanel2.Controls.Add(this.label3);
            this.groupPanel2.Controls.Add(this.txtBillID);
            this.groupPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel2.Location = new System.Drawing.Point(0, 0);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(769, 134);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 146;
            this.groupPanel2.Text = "Thông tin tìm kiếm";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.label1);
            this.panelEx2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(769, 39);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 138;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 19);
            this.label1.TabIndex = 143;
            this.label1.Text = "Danh sách phiếu yêu cầu xuất kho";
            // 
            // ucListBilExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ucListBilExport";
            this.Size = new System.Drawing.Size(769, 460);
            this.Load += new System.EventHandler(this.LIST_BILL_EXPORT_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Button btnexport;
        private System.Windows.Forms.CheckBox cbdetail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpfrom;
        private System.Windows.Forms.TextBox txtBillID;
        private System.Windows.Forms.Label lbBillImport;
        private System.Windows.Forms.DateTimePicker dtpend;
        private System.Windows.Forms.ComboBox cbtypeProduct;
        private System.Windows.Forms.CheckBox cbtimeFillter;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl dgvDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private System.Windows.Forms.Label label1;
    }
}
