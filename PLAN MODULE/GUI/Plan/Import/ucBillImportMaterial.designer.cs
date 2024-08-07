
namespace PLAN_MODULE
{
    partial class ucBillImportMaterial
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
            this.bgrwUpfile = new System.ComponentModel.BackgroundWorker();
            this.tmGetListBill = new System.Windows.Forms.Timer(this.components);
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx6 = new DevComponents.DotNetBar.PanelEx();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cbWork = new System.Windows.Forms.CheckBox();
            this.cbModel = new System.Windows.Forms.CheckBox();
            this.cbInputCusOnly = new System.Windows.Forms.CheckBox();
            this.btnInputFile = new DevComponents.DotNetBar.ButtonX();
            this.cbbModel = new System.Windows.Forms.ComboBox();
            this.cbbCusID = new System.Windows.Forms.ComboBox();
            this.cbVender = new System.Windows.Forms.ComboBox();
            this.txtwork = new System.Windows.Forms.TextBox();
            this.txtPO = new System.Windows.Forms.TextBox();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.dgvDetail = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.behaviorManager = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.panelEx1.SuspendLayout();
            this.panelEx4.SuspendLayout();
            this.panelEx6.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            this.groupPanel2.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager)).BeginInit();
            this.SuspendLayout();
            // 
            // bgrwUpfile
            // 
            this.bgrwUpfile.WorkerReportsProgress = true;
            this.bgrwUpfile.WorkerSupportsCancellation = true;
            this.bgrwUpfile.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgrwUpfile_DoWork);
            // 
            // tmGetListBill
            // 
            this.tmGetListBill.Enabled = true;
            this.tmGetListBill.Interval = 30000;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx4);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1007, 560);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 95;
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.panelEx6);
            this.panelEx4.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Location = new System.Drawing.Point(0, 35);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(1007, 525);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 103;
            // 
            // panelEx6
            // 
            this.panelEx6.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx6.Controls.Add(this.toolStrip1);
            this.panelEx6.Controls.Add(this.progressBarControl1);
            this.panelEx6.Controls.Add(this.groupPanel2);
            this.panelEx6.Controls.Add(this.groupPanel3);
            this.panelEx6.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx6.Location = new System.Drawing.Point(0, 0);
            this.panelEx6.Name = "panelEx6";
            this.panelEx6.Size = new System.Drawing.Size(1007, 525);
            this.panelEx6.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx6.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx6.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx6.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx6.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx6.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx6.Style.GradientAngle = 90;
            this.panelEx6.TabIndex = 148;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1007, 25);
            this.toolStrip1.TabIndex = 158;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::PLAN_MODULE.Properties.Resources.icon_list_16;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(70, 22);
            this.toolStripButton1.Text = "Xuất file";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarControl1.Location = new System.Drawing.Point(3, 277);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Properties.ShowTitle = true;
            this.progressBarControl1.Properties.Step = 1;
            this.progressBarControl1.Size = new System.Drawing.Size(1004, 33);
            this.progressBarControl1.TabIndex = 126;
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.cbWork);
            this.groupPanel2.Controls.Add(this.cbModel);
            this.groupPanel2.Controls.Add(this.cbInputCusOnly);
            this.groupPanel2.Controls.Add(this.btnInputFile);
            this.groupPanel2.Controls.Add(this.cbbModel);
            this.groupPanel2.Controls.Add(this.cbbCusID);
            this.groupPanel2.Controls.Add(this.cbVender);
            this.groupPanel2.Controls.Add(this.txtwork);
            this.groupPanel2.Controls.Add(this.txtPO);
            this.groupPanel2.Controls.Add(this.labelX1);
            this.groupPanel2.Controls.Add(this.labelX8);
            this.groupPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel2.Location = new System.Drawing.Point(3, 37);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(1001, 234);
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
            this.groupPanel2.TabIndex = 125;
            this.groupPanel2.Text = "Thông tin tạo phiếu";
            // 
            // cbWork
            // 
            this.cbWork.AutoSize = true;
            this.cbWork.Location = new System.Drawing.Point(20, 179);
            this.cbWork.Name = "cbWork";
            this.cbWork.Size = new System.Drawing.Size(138, 19);
            this.cbWork.TabIndex = 156;
            this.cbWork.Text = "Xuất riêng cho work";
            this.cbWork.UseVisualStyleBackColor = true;
            // 
            // cbModel
            // 
            this.cbModel.AutoSize = true;
            this.cbModel.Location = new System.Drawing.Point(19, 137);
            this.cbModel.Name = "cbModel";
            this.cbModel.Size = new System.Drawing.Size(143, 19);
            this.cbModel.TabIndex = 156;
            this.cbModel.Text = "Xuất riêng cho Model";
            this.cbModel.UseVisualStyleBackColor = true;
            // 
            // cbInputCusOnly
            // 
            this.cbInputCusOnly.AutoSize = true;
            this.cbInputCusOnly.Location = new System.Drawing.Point(19, 98);
            this.cbInputCusOnly.Name = "cbInputCusOnly";
            this.cbInputCusOnly.Size = new System.Drawing.Size(174, 19);
            this.cbInputCusOnly.TabIndex = 156;
            this.cbInputCusOnly.Text = "Xuất riêng cho khách hàng";
            this.cbInputCusOnly.UseVisualStyleBackColor = true;
            // 
            // btnInputFile
            // 
            this.btnInputFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInputFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnInputFile.Location = new System.Drawing.Point(721, 179);
            this.btnInputFile.Name = "btnInputFile";
            this.btnInputFile.Size = new System.Drawing.Size(118, 23);
            this.btnInputFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnInputFile.TabIndex = 153;
            this.btnInputFile.Text = "Load File";
            this.btnInputFile.Click += new System.EventHandler(this.btnInputFile_Click);
            // 
            // cbbModel
            // 
            this.cbbModel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbbModel.FormattingEnabled = true;
            this.cbbModel.Location = new System.Drawing.Point(199, 133);
            this.cbbModel.Name = "cbbModel";
            this.cbbModel.Size = new System.Drawing.Size(259, 23);
            this.cbbModel.TabIndex = 152;
            this.cbbModel.SelectedIndexChanged += new System.EventHandler(this.cbbModel_SelectedIndexChanged);
            // 
            // cbbCusID
            // 
            this.cbbCusID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbbCusID.FormattingEnabled = true;
            this.cbbCusID.Location = new System.Drawing.Point(199, 94);
            this.cbbCusID.Name = "cbbCusID";
            this.cbbCusID.Size = new System.Drawing.Size(259, 23);
            this.cbbCusID.TabIndex = 152;
            this.cbbCusID.SelectedIndexChanged += new System.EventHandler(this.cbCus_SelectedIndexChanged);
            // 
            // cbVender
            // 
            this.cbVender.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbVender.FormattingEnabled = true;
            this.cbVender.Location = new System.Drawing.Point(107, 18);
            this.cbVender.Name = "cbVender";
            this.cbVender.Size = new System.Drawing.Size(259, 23);
            this.cbVender.TabIndex = 152;
            // 
            // txtwork
            // 
            this.txtwork.Location = new System.Drawing.Point(199, 176);
            this.txtwork.Name = "txtwork";
            this.txtwork.Size = new System.Drawing.Size(259, 22);
            this.txtwork.TabIndex = 151;
            this.txtwork.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtwork_KeyDown);
            // 
            // txtPO
            // 
            this.txtPO.Location = new System.Drawing.Point(107, 57);
            this.txtPO.Name = "txtPO";
            this.txtPO.Size = new System.Drawing.Size(259, 22);
            this.txtPO.TabIndex = 151;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.Color.Black;
            this.labelX1.Location = new System.Drawing.Point(15, 59);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(100, 17);
            this.labelX1.TabIndex = 150;
            this.labelX1.Text = "&Số PO (nếu có):";
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
            this.labelX8.Location = new System.Drawing.Point(15, 18);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(88, 17);
            this.labelX8.TabIndex = 150;
            this.labelX8.Text = "&Bên cấp NVL";
            // 
            // groupPanel3
            // 
            this.groupPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.dgvDetail);
            this.groupPanel3.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel3.Location = new System.Drawing.Point(0, 316);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(1007, 209);
            // 
            // 
            // 
            this.groupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel3.Style.BackColorGradientAngle = 90;
            this.groupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderBottomWidth = 1;
            this.groupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderLeftWidth = 1;
            this.groupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderRightWidth = 1;
            this.groupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderTopWidth = 1;
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel3.TabIndex = 123;
            this.groupPanel3.Text = "Thông tin thực nhận";
            // 
            // dgvDetail
            // 
            this.dgvDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetail.Location = new System.Drawing.Point(0, 0);
            this.dgvDetail.MainView = this.gridView1;
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.Size = new System.Drawing.Size(1001, 188);
            this.dgvDetail.TabIndex = 0;
            this.dgvDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.dgvDetail.Click += new System.EventHandler(this.dgvDetail_Click);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dgvDetail;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.labelX4);
            this.panelEx2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1007, 35);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 95;
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.ForeColor = System.Drawing.Color.Black;
            this.labelX4.Location = new System.Drawing.Point(3, 3);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(341, 26);
            this.labelX4.TabIndex = 1;
            this.labelX4.Text = "&Tạo phiếu yêu cầu nhập nguyên vật liệu";
            // 
            // ucBillImportMaterial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ucBillImportMaterial";
            this.Size = new System.Drawing.Size(1007, 560);
            this.panelEx1.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            this.panelEx6.ResumeLayout(false);
            this.panelEx6.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            this.groupPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker bgrwUpfile;
        private System.Windows.Forms.Timer tmGetListBill;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.PanelEx panelEx6;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel3;
        private DevExpress.XtraGrid.GridControl dgvDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.LabelX labelX8;
        private System.Windows.Forms.ComboBox cbVender;
        private System.Windows.Forms.TextBox txtPO;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnInputFile;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private System.Windows.Forms.CheckBox cbWork;
        private System.Windows.Forms.CheckBox cbModel;
        private System.Windows.Forms.CheckBox cbInputCusOnly;
        private System.Windows.Forms.ComboBox cbbModel;
        private System.Windows.Forms.TextBox txtwork;
        private System.Windows.Forms.ComboBox cbbCusID;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}
