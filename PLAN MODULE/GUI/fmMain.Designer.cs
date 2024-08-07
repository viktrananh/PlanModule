
namespace PLAN_MODULE
{
    partial class fmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmMain));
            this.timerCheckVersion = new System.Windows.Forms.Timer(this.components);
            this.tabFormControlMain = new DevComponents.DotNetBar.Controls.TabFormControl();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.mnFunction = new System.Windows.Forms.ToolStrip();
            this.mnSale = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnPlan = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnMngPO = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMngWork = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.phiếuNhậpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBillMaterial = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBillCCDC = new System.Windows.Forms.ToolStripMenuItem();
            this.phiếuXuấtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBillGood = new System.Windows.Forms.ToolStripMenuItem();
            this.linhKiệnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCheckMaterial = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelivery = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLaserPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnProduction = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnProdBillImportMaterial = new System.Windows.Forms.ToolStripMenuItem();
            this.btnProdBillExportMaterial = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton5 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnProductionPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton6 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnUser = new System.Windows.Forms.ToolStripDropDownButton();
            this.đăngXuấtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx3.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelEx4.SuspendLayout();
            this.mnFunction.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerCheckVersion
            // 
            this.timerCheckVersion.Interval = 60000;
            this.timerCheckVersion.Tick += new System.EventHandler(this.timerCheckVersion_Tick);
            // 
            // tabFormControlMain
            // 
            // 
            // 
            // 
            this.tabFormControlMain.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tabFormControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFormControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabFormControlMain.Name = "tabFormControlMain";
            this.tabFormControlMain.Size = new System.Drawing.Size(1022, 508);
            this.tabFormControlMain.TabIndex = 0;
            this.tabFormControlMain.TabStripFont = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabFormControlMain.Text = "tabFormControl1";
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.panelEx1);
            this.panelEx3.Controls.Add(this.panelEx4);
            this.panelEx3.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(0, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(1022, 551);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 13;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.tabFormControlMain);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 43);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1022, 508);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 23;
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.mnFunction);
            this.panelEx4.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx4.Location = new System.Drawing.Point(0, 0);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(1022, 43);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 13;
            // 
            // mnFunction
            // 
            this.mnFunction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(217)))), ((int)(((byte)(255)))));
            this.mnFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mnFunction.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnFunction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnSale,
            this.mnPlan,
            this.mnProduction,
            this.toolStripDropDownButton5,
            this.toolStripDropDownButton6,
            this.btnUser});
            this.mnFunction.Location = new System.Drawing.Point(0, 0);
            this.mnFunction.Name = "mnFunction";
            this.mnFunction.Size = new System.Drawing.Size(1022, 43);
            this.mnFunction.TabIndex = 0;
            this.mnFunction.Text = "toolStrip2";
            // 
            // mnSale
            // 
            this.mnSale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnSale.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCustomer});
            this.mnSale.Enabled = false;
            this.mnSale.Image = ((System.Drawing.Image)(resources.GetObject("mnSale.Image")));
            this.mnSale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnSale.Name = "mnSale";
            this.mnSale.Size = new System.Drawing.Size(82, 40);
            this.mnSale.Text = "Kinh doanh";
            // 
            // btnCustomer
            // 
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(180, 22);
            this.btnCustomer.Text = "Khách hàng";
            this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
            // 
            // mnPlan
            // 
            this.mnPlan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnPlan.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMngPO,
            this.btnMngWork,
            this.toolStripSeparator2,
            this.phiếuNhậpToolStripMenuItem,
            this.phiếuXuấtToolStripMenuItem,
            this.toolStripSeparator4,
            this.btnCheckMaterial,
            this.toolStripSeparator5,
            this.btnDelivery,
            this.btnLaserPlan});
            this.mnPlan.Enabled = false;
            this.mnPlan.Image = ((System.Drawing.Image)(resources.GetObject("mnPlan.Image")));
            this.mnPlan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnPlan.Name = "mnPlan";
            this.mnPlan.Size = new System.Drawing.Size(71, 40);
            this.mnPlan.Text = "Kế hoạch";
            // 
            // btnMngPO
            // 
            this.btnMngPO.Name = "btnMngPO";
            this.btnMngPO.Size = new System.Drawing.Size(183, 22);
            this.btnMngPO.Text = "Quản lý P.O";
            this.btnMngPO.Click += new System.EventHandler(this.btnMngPO_Click);
            // 
            // btnMngWork
            // 
            this.btnMngWork.Name = "btnMngWork";
            this.btnMngWork.Size = new System.Drawing.Size(183, 22);
            this.btnMngWork.Text = "Quản lý công lệnh";
            this.btnMngWork.Click += new System.EventHandler(this.btnMngWork_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(180, 6);
            // 
            // phiếuNhậpToolStripMenuItem
            // 
            this.phiếuNhậpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBillMaterial,
            this.btnBillCCDC});
            this.phiếuNhậpToolStripMenuItem.Name = "phiếuNhậpToolStripMenuItem";
            this.phiếuNhậpToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.phiếuNhậpToolStripMenuItem.Text = "Phiếu nhập";
            // 
            // btnBillMaterial
            // 
            this.btnBillMaterial.Name = "btnBillMaterial";
            this.btnBillMaterial.Size = new System.Drawing.Size(124, 22);
            this.btnBillMaterial.Text = "Linh kiện";
            this.btnBillMaterial.Click += new System.EventHandler(this.btnBillMaterial_Click);
            // 
            // btnBillCCDC
            // 
            this.btnBillCCDC.Name = "btnBillCCDC";
            this.btnBillCCDC.Size = new System.Drawing.Size(124, 22);
            this.btnBillCCDC.Text = "CCDC";
            this.btnBillCCDC.Click += new System.EventHandler(this.btnBillCCDC_Click);
            // 
            // phiếuXuấtToolStripMenuItem
            // 
            this.phiếuXuấtToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBillGood,
            this.linhKiệnToolStripMenuItem});
            this.phiếuXuấtToolStripMenuItem.Name = "phiếuXuấtToolStripMenuItem";
            this.phiếuXuấtToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.phiếuXuấtToolStripMenuItem.Text = "Phiếu xuất";
            // 
            // btnBillGood
            // 
            this.btnBillGood.Name = "btnBillGood";
            this.btnBillGood.Size = new System.Drawing.Size(142, 22);
            this.btnBillGood.Text = "Thành phẩm";
            this.btnBillGood.Click += new System.EventHandler(this.btnBillGood_Click);
            // 
            // linhKiệnToolStripMenuItem
            // 
            this.linhKiệnToolStripMenuItem.Name = "linhKiệnToolStripMenuItem";
            this.linhKiệnToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.linhKiệnToolStripMenuItem.Text = "Linh kiện";
            this.linhKiệnToolStripMenuItem.Click += new System.EventHandler(this.linhKiệnToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(180, 6);
            // 
            // btnCheckMaterial
            // 
            this.btnCheckMaterial.Name = "btnCheckMaterial";
            this.btnCheckMaterial.Size = new System.Drawing.Size(183, 22);
            this.btnCheckMaterial.Text = "Theo dõi liệu về";
            this.btnCheckMaterial.Click += new System.EventHandler(this.btnCheckMaterial_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(180, 6);
            // 
            // btnDelivery
            // 
            this.btnDelivery.Name = "btnDelivery";
            this.btnDelivery.Size = new System.Drawing.Size(183, 22);
            this.btnDelivery.Text = "Kế hoạch giao hàng";
            this.btnDelivery.Click += new System.EventHandler(this.btnDelivery_Click);
            // 
            // btnLaserPlan
            // 
            this.btnLaserPlan.Name = "btnLaserPlan";
            this.btnLaserPlan.Size = new System.Drawing.Size(183, 22);
            this.btnLaserPlan.Text = "Kế hoạch khắc laser";
            this.btnLaserPlan.Click += new System.EventHandler(this.btnLaserPlan_Click);
            // 
            // mnProduction
            // 
            this.mnProduction.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnProduction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnProdBillImportMaterial,
            this.btnProdBillExportMaterial});
            this.mnProduction.Enabled = false;
            this.mnProduction.Image = ((System.Drawing.Image)(resources.GetObject("mnProduction.Image")));
            this.mnProduction.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnProduction.Name = "mnProduction";
            this.mnProduction.Size = new System.Drawing.Size(68, 40);
            this.mnProduction.Text = "Sản xuất";
            // 
            // btnProdBillImportMaterial
            // 
            this.btnProdBillImportMaterial.Name = "btnProdBillImportMaterial";
            this.btnProdBillImportMaterial.Size = new System.Drawing.Size(185, 22);
            this.btnProdBillImportMaterial.Text = "Phiếu nhập linh kiện";
            this.btnProdBillImportMaterial.Click += new System.EventHandler(this.btnProdBillImportMaterial_Click);
            // 
            // btnProdBillExportMaterial
            // 
            this.btnProdBillExportMaterial.Name = "btnProdBillExportMaterial";
            this.btnProdBillExportMaterial.Size = new System.Drawing.Size(185, 22);
            this.btnProdBillExportMaterial.Text = "Phiếu xuất linh kiện";
            this.btnProdBillExportMaterial.Click += new System.EventHandler(this.btnProdBillExportMaterial_Click);
            // 
            // toolStripDropDownButton5
            // 
            this.toolStripDropDownButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolToolStripMenuItem,
            this.btnProductionPlan});
            this.toolStripDropDownButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton5.Image")));
            this.toolStripDropDownButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton5.Name = "toolStripDropDownButton5";
            this.toolStripDropDownButton5.Size = new System.Drawing.Size(64, 40);
            this.toolStripDropDownButton5.Text = "Công cụ";
            this.toolStripDropDownButton5.Click += new System.EventHandler(this.toolStripDropDownButton5_Click);
            // 
            // toolToolStripMenuItem
            // 
            this.toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            this.toolToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.toolToolStripMenuItem.Text = "Tool && Report";
            this.toolToolStripMenuItem.Click += new System.EventHandler(this.toolToolStripMenuItem_Click);
            // 
            // btnProductionPlan
            // 
            this.btnProductionPlan.Name = "btnProductionPlan";
            this.btnProductionPlan.Size = new System.Drawing.Size(180, 22);
            this.btnProductionPlan.Text = "Kế hoạch sản xuất";
            this.btnProductionPlan.Click += new System.EventHandler(this.btnProductionPlan_Click);
            // 
            // toolStripDropDownButton6
            // 
            this.toolStripDropDownButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton6.Image")));
            this.toolStripDropDownButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton6.Name = "toolStripDropDownButton6";
            this.toolStripDropDownButton6.Size = new System.Drawing.Size(66, 40);
            this.toolStripDropDownButton6.Text = "Trợ giúp";
            // 
            // btnUser
            // 
            this.btnUser.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnUser.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.đăngXuấtToolStripMenuItem});
            this.btnUser.Image = ((System.Drawing.Image)(resources.GetObject("btnUser.Image")));
            this.btnUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(88, 40);
            this.btnUser.Text = "Tài khoản";
            // 
            // đăngXuấtToolStripMenuItem
            // 
            this.đăngXuấtToolStripMenuItem.Name = "đăngXuấtToolStripMenuItem";
            this.đăngXuấtToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.đăngXuấtToolStripMenuItem.Text = "Đăng xuất";
            this.đăngXuấtToolStripMenuItem.Click += new System.EventHandler(this.đăngXuấtToolStripMenuItem_Click);
            // 
            // fmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 551);
            this.Controls.Add(this.panelEx3);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ERP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fmMain_FormClosing);
            this.Load += new System.EventHandler(this.fmMain_Load);
            this.panelEx3.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            this.panelEx4.PerformLayout();
            this.mnFunction.ResumeLayout(false);
            this.mnFunction.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerCheckVersion;
        private DevComponents.DotNetBar.Controls.TabFormControl tabFormControlMain;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private System.Windows.Forms.ToolStrip mnFunction;
        private System.Windows.Forms.ToolStripDropDownButton mnSale;
        private System.Windows.Forms.ToolStripDropDownButton mnPlan;
        private System.Windows.Forms.ToolStripDropDownButton mnProduction;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton5;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton6;
        private System.Windows.Forms.ToolStripMenuItem btnCustomer;
        private System.Windows.Forms.ToolStripMenuItem btnMngPO;
        private System.Windows.Forms.ToolStripMenuItem btnMngWork;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem phiếuNhậpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem phiếuXuấtToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem btnCheckMaterial;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem btnDelivery;
        private System.Windows.Forms.ToolStripMenuItem btnProdBillImportMaterial;
        private System.Windows.Forms.ToolStripMenuItem btnProdBillExportMaterial;
        private System.Windows.Forms.ToolStripMenuItem btnBillMaterial;
        private System.Windows.Forms.ToolStripMenuItem btnBillCCDC;
        private System.Windows.Forms.ToolStripMenuItem btnBillGood;
        private System.Windows.Forms.ToolStripMenuItem btnLaserPlan;
        private System.Windows.Forms.ToolStripDropDownButton btnUser;
        private System.Windows.Forms.ToolStripMenuItem đăngXuấtToolStripMenuItem;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.ToolStripMenuItem toolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnProductionPlan;
        private System.Windows.Forms.ToolStripMenuItem linhKiệnToolStripMenuItem;
    }
}

