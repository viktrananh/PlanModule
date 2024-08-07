namespace PLAN_MODULE.GUI.Plan
{
    partial class ucListPO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucListPO));
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btncreate = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.labelItem4 = new DevComponents.DotNetBar.LabelItem();
            this.lbWorkID = new DevComponents.DotNetBar.LabelItem();
            this.lbPO = new DevComponents.DotNetBar.LabelItem();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.labelItem3 = new DevComponents.DotNetBar.LabelItem();
            this.lbOP = new DevComponents.DotNetBar.LabelItem();
            this.labelItem7 = new DevComponents.DotNetBar.LabelItem();
            this.labelItem5 = new DevComponents.DotNetBar.LabelItem();
            this.lbStatusID = new DevComponents.DotNetBar.LabelItem();
            this.lbStatusName = new DevComponents.DotNetBar.LabelItem();
            this.buttonItem14 = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer9 = new DevComponents.DotNetBar.ItemContainer();
            this.buttonItem13 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem15 = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem2 = new DevComponents.DotNetBar.LabelItem();
            this.lb = new DevComponents.DotNetBar.LabelItem();
            this.lbCountBill = new DevComponents.DotNetBar.LabelItem();
            this.itemContainer13 = new DevComponents.DotNetBar.ItemContainer();
            this.labelPosition = new DevComponents.DotNetBar.LabelItem();
            this.zoomSlider = new DevComponents.DotNetBar.SliderItem();
            this.dgvPOs = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPOs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelEx5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(935, 30);
            this.panel2.TabIndex = 5;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btncreate,
            this.btnEdit,
            this.toolStripSeparator2,
            this.btnRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(935, 30);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btncreate
            // 
            this.btncreate.Image = ((System.Drawing.Image)(resources.GetObject("btncreate.Image")));
            this.btncreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btncreate.Name = "btncreate";
            this.btncreate.Size = new System.Drawing.Size(81, 27);
            this.btncreate.Text = "&Thêm mới";
            this.btncreate.Click += new System.EventHandler(this.btncreate_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(46, 27);
            this.btnEdit.Text = "Sửa";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 30);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(66, 27);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.bar1);
            this.panelEx2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.Location = new System.Drawing.Point(0, 447);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(935, 29);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 4;
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Bottom;
            this.bar1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bar1.IsMaximized = false;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem4,
            this.lbWorkID,
            this.lbPO,
            this.labelItem1,
            this.labelItem3,
            this.lbOP,
            this.labelItem7,
            this.labelItem5,
            this.lbStatusID,
            this.lbStatusName,
            this.buttonItem14,
            this.itemContainer9,
            this.labelItem2,
            this.lb,
            this.lbCountBill,
            this.itemContainer13});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(935, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 1;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // labelItem4
            // 
            this.labelItem4.Name = "labelItem4";
            this.labelItem4.Text = "P.O:";
            // 
            // lbWorkID
            // 
            this.lbWorkID.Name = "lbWorkID";
            // 
            // lbPO
            // 
            this.lbPO.Name = "lbPO";
            this.lbPO.Text = "abc";
            // 
            // labelItem1
            // 
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Stretch = true;
            // 
            // labelItem3
            // 
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.Text = "Operater:";
            // 
            // lbOP
            // 
            this.lbOP.Name = "lbOP";
            this.lbOP.Text = "MV000000";
            // 
            // labelItem7
            // 
            this.labelItem7.Name = "labelItem7";
            this.labelItem7.Stretch = true;
            // 
            // labelItem5
            // 
            this.labelItem5.Name = "labelItem5";
            this.labelItem5.Text = "Trạng thái:";
            this.labelItem5.WordWrap = true;
            // 
            // lbStatusID
            // 
            this.lbStatusID.Name = "lbStatusID";
            this.lbStatusID.Text = "1";
            // 
            // lbStatusName
            // 
            this.lbStatusName.Name = "lbStatusName";
            this.lbStatusName.Text = "Khởi tạo";
            // 
            // buttonItem14
            // 
            this.buttonItem14.ImagePaddingVertical = 9;
            this.buttonItem14.Name = "buttonItem14";
            this.buttonItem14.OptionGroup = "statusGroup";
            this.buttonItem14.Symbol = "";
            this.buttonItem14.SymbolSize = 10F;
            this.buttonItem14.Text = "Rating";
            this.buttonItem14.Tooltip = "Rating";
            // 
            // itemContainer9
            // 
            // 
            // 
            // 
            this.itemContainer9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer9.BeginGroup = true;
            this.itemContainer9.Name = "itemContainer9";
            this.itemContainer9.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem13,
            this.buttonItem15});
            // 
            // 
            // 
            this.itemContainer9.TitleMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.itemContainer9.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // buttonItem13
            // 
            this.buttonItem13.Checked = true;
            this.buttonItem13.ImagePaddingVertical = 9;
            this.buttonItem13.Name = "buttonItem13";
            this.buttonItem13.OptionGroup = "statusGroup";
            this.buttonItem13.Symbol = "";
            this.buttonItem13.SymbolSize = 10F;
            this.buttonItem13.Text = "Rating";
            this.buttonItem13.Tooltip = "Rating";
            // 
            // buttonItem15
            // 
            this.buttonItem15.ImagePaddingVertical = 9;
            this.buttonItem15.Name = "buttonItem15";
            this.buttonItem15.OptionGroup = "statusGroup";
            this.buttonItem15.Symbol = "";
            this.buttonItem15.SymbolSize = 10F;
            this.buttonItem15.Text = "Comments";
            this.buttonItem15.Tooltip = "Comments";
            // 
            // labelItem2
            // 
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.Stretch = true;
            // 
            // lb
            // 
            this.lb.Name = "lb";
            this.lb.Text = "Count: ";
            // 
            // lbCountBill
            // 
            this.lbCountBill.Name = "lbCountBill";
            this.lbCountBill.Text = "0";
            // 
            // itemContainer13
            // 
            // 
            // 
            // 
            this.itemContainer13.BackgroundStyle.Class = "Office2007StatusBarBackground2";
            this.itemContainer13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer13.Name = "itemContainer13";
            this.itemContainer13.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelPosition,
            this.zoomSlider});
            // 
            // 
            // 
            this.itemContainer13.TitleMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.itemContainer13.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // labelPosition
            // 
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.PaddingLeft = 2;
            this.labelPosition.PaddingRight = 2;
            this.labelPosition.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.labelPosition.Width = 100;
            // 
            // zoomSlider
            // 
            this.zoomSlider.Maximum = 200;
            this.zoomSlider.Name = "zoomSlider";
            this.zoomSlider.Step = 5;
            this.zoomSlider.Text = "100%";
            this.zoomSlider.Value = 100;
            // 
            // dgvPOs
            // 
            this.dgvPOs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPOs.Location = new System.Drawing.Point(0, 0);
            this.dgvPOs.MainView = this.gridView1;
            this.dgvPOs.Name = "dgvPOs";
            this.dgvPOs.Size = new System.Drawing.Size(935, 417);
            this.dgvPOs.TabIndex = 10;
            this.dgvPOs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn3,
            this.gridColumn1,
            this.gridColumn6,
            this.gridColumn2,
            this.gridColumn4});
            this.gridView1.GridControl = this.dgvPOs;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn3, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "PO";
            this.gridColumn7.FieldName = "PO";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 147;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Khách hàng";
            this.gridColumn3.FieldName = "CusId";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 162;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Ngày tạo";
            this.gridColumn1.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn1.FieldName = "DateCreat";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 166;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Trạng thái";
            this.gridColumn6.FieldName = "Status";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 143;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "OP";
            this.gridColumn2.FieldName = "OP";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 116;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Ghi chú";
            this.gridColumn4.FieldName = "Comment";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 122;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelEx3);
            this.panel1.Controls.Add(this.panelEx2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(935, 476);
            this.panel1.TabIndex = 6;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.panel3);
            this.panelEx3.Controls.Add(this.panel2);
            this.panelEx3.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(0, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(935, 447);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panelEx5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 30);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(935, 417);
            this.panel3.TabIndex = 6;
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.dgvPOs);
            this.panelEx5.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx5.Location = new System.Drawing.Point(0, 0);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(935, 417);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 7;
            // 
            // ucListPO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ucListPO";
            this.Size = new System.Drawing.Size(935, 476);
            this.Load += new System.EventHandler(this.ucListPO_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPOs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panelEx5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btncreate;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.LabelItem labelItem4;
        private DevComponents.DotNetBar.LabelItem lbWorkID;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.LabelItem labelItem3;
        private DevComponents.DotNetBar.LabelItem lbOP;
        private DevComponents.DotNetBar.LabelItem labelItem7;
        private DevComponents.DotNetBar.LabelItem labelItem5;
        private DevComponents.DotNetBar.LabelItem lbStatusID;
        private DevComponents.DotNetBar.LabelItem lbStatusName;
        private DevComponents.DotNetBar.ButtonItem buttonItem14;
        private DevComponents.DotNetBar.ItemContainer itemContainer9;
        private DevComponents.DotNetBar.ButtonItem buttonItem13;
        private DevComponents.DotNetBar.ButtonItem buttonItem15;
        private DevComponents.DotNetBar.LabelItem labelItem2;
        private DevComponents.DotNetBar.LabelItem lb;
        private DevComponents.DotNetBar.LabelItem lbCountBill;
        private DevComponents.DotNetBar.ItemContainer itemContainer13;
        internal DevComponents.DotNetBar.LabelItem labelPosition;
        private DevComponents.DotNetBar.SliderItem zoomSlider;
        private DevExpress.XtraGrid.GridControl dgvPOs;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.Panel panel3;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private DevComponents.DotNetBar.LabelItem lbPO;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}
