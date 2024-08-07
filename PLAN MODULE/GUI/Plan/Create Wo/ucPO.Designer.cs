namespace PLAN_MODULE.GUI.Plan
{
    partial class ucPO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPO));
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.requiredFieldValidator1 = new DevComponents.DotNetBar.Validator.RequiredFieldValidator("Your error message here.");
            this.txtPO = new System.Windows.Forms.TextBox();
            this.buttonItem13 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem15 = new DevComponents.DotNetBar.ButtonItem();
            this.labelPosition = new DevComponents.DotNetBar.LabelItem();
            this.zoomSlider = new DevComponents.DotNetBar.SliderItem();
            this.dgvModels = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelEx6 = new DevComponents.DotNetBar.PanelEx();
            this.superTabControl1 = new DevComponents.DotNetBar.SuperTabControl();
            this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtMFG = new System.Windows.Forms.DateTimePicker();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboModel = new System.Windows.Forms.ComboBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.nbrInvoiceQty = new System.Windows.Forms.NumericUpDown();
            this.panelEx3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panelEx6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).BeginInit();
            this.superTabControl1.SuspendLayout();
            this.superTabControlPanel1.SuspendLayout();
            this.panelEx4.SuspendLayout();
            this.panelEx5.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbrInvoiceQty)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.toolStrip1);
            this.panelEx3.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx3.Location = new System.Drawing.Point(0, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(876, 34);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 4;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.toolStripSeparator1,
            this.btnClose,
            this.toolStripSeparator2,
            this.btnDelete,
            this.toolStripSeparator3,
            this.btnRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(876, 34);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(47, 31);
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(59, 31);
            this.btnClose.Text = "Đóng ";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 34);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(47, 31);
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 34);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(64, 31);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // requiredFieldValidator1
            // 
            this.requiredFieldValidator1.ErrorMessage = "Your error message here.";
            this.requiredFieldValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
            // 
            // txtPO
            // 
            this.txtPO.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPO.Location = new System.Drawing.Point(154, 42);
            this.txtPO.Name = "txtPO";
            this.txtPO.Size = new System.Drawing.Size(194, 20);
            this.txtPO.TabIndex = 4;
            this.txtPO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPO_KeyDown);
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
            // dgvModels
            // 
            this.dgvModels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvModels.Location = new System.Drawing.Point(0, 0);
            this.dgvModels.MainView = this.gridView1;
            this.dgvModels.Name = "dgvModels";
            this.dgvModels.Size = new System.Drawing.Size(862, 279);
            this.dgvModels.TabIndex = 1;
            this.dgvModels.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn4,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7});
            this.gridView1.GridControl = this.dgvModels;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.CheckBoxSelectorField = "Selected";
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Model";
            this.gridColumn1.FieldName = "ModelId";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Mã khách hàng";
            this.gridColumn4.FieldName = "CusModel";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Ngày sản xuất";
            this.gridColumn2.FieldName = "MFGDate";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Số lượng";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "Count";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Trạng thái";
            this.gridColumn5.FieldName = "State";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Số lập hóa đơn";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Ghi chú";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            // 
            // panelEx6
            // 
            this.panelEx6.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx6.Controls.Add(this.superTabControl1);
            this.panelEx6.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx6.Location = new System.Drawing.Point(0, 162);
            this.panelEx6.Name = "panelEx6";
            this.panelEx6.Padding = new System.Windows.Forms.Padding(7);
            this.panelEx6.Size = new System.Drawing.Size(876, 318);
            this.panelEx6.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx6.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx6.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx6.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx6.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx6.Style.GradientAngle = 90;
            this.panelEx6.TabIndex = 4;
            // 
            // superTabControl1
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.superTabControl1.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.superTabControl1.ControlBox.MenuBox.Name = "";
            this.superTabControl1.ControlBox.Name = "";
            this.superTabControl1.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabControl1.ControlBox.MenuBox,
            this.superTabControl1.ControlBox.CloseBox});
            this.superTabControl1.Controls.Add(this.superTabControlPanel1);
            this.superTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControl1.Location = new System.Drawing.Point(7, 7);
            this.superTabControl1.Name = "superTabControl1";
            this.superTabControl1.ReorderTabsEnabled = true;
            this.superTabControl1.SelectedTabFont = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold);
            this.superTabControl1.SelectedTabIndex = 0;
            this.superTabControl1.Size = new System.Drawing.Size(862, 304);
            this.superTabControl1.TabFont = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.superTabControl1.TabIndex = 9;
            this.superTabControl1.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItem1});
            this.superTabControl1.Text = "superTabControl1";
            // 
            // superTabControlPanel1
            // 
            this.superTabControlPanel1.Controls.Add(this.dgvModels);
            this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel1.Location = new System.Drawing.Point(0, 25);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new System.Drawing.Size(862, 279);
            this.superTabControlPanel1.TabIndex = 1;
            this.superTabControlPanel1.TabItem = this.superTabItem1;
            // 
            // superTabItem1
            // 
            this.superTabItem1.AttachedControl = this.superTabControlPanel1;
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            this.superTabItem1.Text = "&1, Thông tin P.O";
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.panelEx6);
            this.panelEx4.Controls.Add(this.panelEx5);
            this.panelEx4.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Location = new System.Drawing.Point(0, 34);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(876, 480);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 8;
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.groupPanel2);
            this.panelEx5.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx5.Location = new System.Drawing.Point(0, 0);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Padding = new System.Windows.Forms.Padding(7);
            this.panelEx5.Size = new System.Drawing.Size(876, 162);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 0;
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.nbrInvoiceQty);
            this.groupPanel2.Controls.Add(this.label7);
            this.groupPanel2.Controls.Add(this.label6);
            this.groupPanel2.Controls.Add(this.dtMFG);
            this.groupPanel2.Controls.Add(this.txtCount);
            this.groupPanel2.Controls.Add(this.label5);
            this.groupPanel2.Controls.Add(this.label3);
            this.groupPanel2.Controls.Add(this.cboModel);
            this.groupPanel2.Controls.Add(this.txtComment);
            this.groupPanel2.Controls.Add(this.label4);
            this.groupPanel2.Controls.Add(this.txtPO);
            this.groupPanel2.Controls.Add(this.label2);
            this.groupPanel2.Controls.Add(this.label1);
            this.groupPanel2.Controls.Add(this.cboCustomer);
            this.groupPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel2.DrawTitleBox = false;
            this.groupPanel2.Location = new System.Drawing.Point(7, 7);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(862, 148);
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
            this.groupPanel2.TabIndex = 1;
            this.groupPanel2.Text = "Thông tin chung";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(393, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 15);
            this.label7.TabIndex = 34;
            this.label7.Text = "&Số lập hóa đơn:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(393, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 15);
            this.label6.TabIndex = 32;
            this.label6.Text = "&Ngày sản xuất :";
            // 
            // dtMFG
            // 
            this.dtMFG.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtMFG.CustomFormat = "yyyy-MM-dd";
            this.dtMFG.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtMFG.Location = new System.Drawing.Point(504, 40);
            this.dtMFG.Name = "dtMFG";
            this.dtMFG.Size = new System.Drawing.Size(208, 20);
            this.dtMFG.TabIndex = 31;
            // 
            // txtCount
            // 
            this.txtCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtCount.Location = new System.Drawing.Point(504, 65);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(208, 20);
            this.txtCount.TabIndex = 29;
            this.txtCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCount_KeyPress);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(394, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 15);
            this.label5.TabIndex = 30;
            this.label5.Text = "&Số lượng :";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(394, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 15);
            this.label3.TabIndex = 28;
            this.label3.Text = "&Model:";
            // 
            // cboModel
            // 
            this.cboModel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboModel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboModel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboModel.FormattingEnabled = true;
            this.cboModel.Location = new System.Drawing.Point(504, 14);
            this.cboModel.Name = "cboModel";
            this.cboModel.Size = new System.Drawing.Size(208, 22);
            this.cboModel.TabIndex = 27;
            // 
            // txtComment
            // 
            this.txtComment.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtComment.Location = new System.Drawing.Point(154, 68);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(194, 43);
            this.txtComment.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(68, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 15);
            this.label4.TabIndex = 26;
            this.label4.Text = "&Ghi Chú:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(68, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "&P.O :";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(68, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "&Khách :";
            // 
            // cboCustomer
            // 
            this.cboCustomer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCustomer.FormattingEnabled = true;
            this.cboCustomer.Location = new System.Drawing.Point(154, 14);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(194, 22);
            this.cboCustomer.TabIndex = 0;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx4);
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(876, 514);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 4;
            // 
            // nbrInvoiceQty
            // 
            this.nbrInvoiceQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nbrInvoiceQty.Location = new System.Drawing.Point(504, 95);
            this.nbrInvoiceQty.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.nbrInvoiceQty.Name = "nbrInvoiceQty";
            this.nbrInvoiceQty.Size = new System.Drawing.Size(208, 20);
            this.nbrInvoiceQty.TabIndex = 35;
            // 
            // ucPO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ucPO";
            this.Size = new System.Drawing.Size(876, 514);
            this.Load += new System.EventHandler(this.ucPO_Load);
            this.panelEx3.ResumeLayout(false);
            this.panelEx3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panelEx6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).EndInit();
            this.superTabControl1.ResumeLayout(false);
            this.superTabControlPanel1.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            this.panelEx5.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nbrInvoiceQty)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.PanelEx panelEx6;
        private DevComponents.DotNetBar.ButtonItem buttonItem13;
        private DevComponents.DotNetBar.ButtonItem buttonItem15;
        internal DevComponents.DotNetBar.LabelItem labelPosition;
        private DevComponents.DotNetBar.SliderItem zoomSlider;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private System.Windows.Forms.TextBox txtPO;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Validator.RequiredFieldValidator requiredFieldValidator1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboCustomer;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraGrid.GridControl dgvModels;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboModel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtMFG;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevComponents.DotNetBar.SuperTabControl superTabControl1;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.SuperTabItem superTabItem1;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private System.Windows.Forms.NumericUpDown nbrInvoiceQty;
    }
}
