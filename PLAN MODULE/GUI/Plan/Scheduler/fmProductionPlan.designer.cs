namespace PLAN_MODULE.GUI.Plan.Scheduler
{
    partial class fmProductionPlan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmProductionPlan));
            this.calendarView1 = new DevComponents.DotNetBar.Schedule.CalendarView();
            this.dateNavigator1 = new DevComponents.DotNetBar.Schedule.DateNavigator();
            this.barView = new DevComponents.DotNetBar.Bar();
            this.cboRoute = new DevComponents.DotNetBar.ComboBoxItem();
            this.btnDay = new DevComponents.DotNetBar.ButtonItem();
            this.btnWeek = new DevComponents.DotNetBar.ButtonItem();
            this.btnMonth = new DevComponents.DotNetBar.ButtonItem();
            this.btnYear = new DevComponents.DotNetBar.ButtonItem();
            this.btnTimeline = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem10 = new DevComponents.DotNetBar.ButtonItem();
            this.controlContainerItem1 = new DevComponents.DotNetBar.ControlContainerItem();
            this.buttonItem14 = new DevComponents.DotNetBar.ButtonItem();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.ribbonTabItem1 = new DevComponents.DotNetBar.RibbonTabItem();
            this.contextMenuBar1 = new DevComponents.DotNetBar.ContextMenuBar();
            this.InContentContextMenu = new DevComponents.DotNetBar.ButtonItem();
            this.InContentAddAppContextItem = new DevComponents.DotNetBar.ButtonItem();
            this.AppointmentContextMenu = new DevComponents.DotNetBar.ButtonItem();
            this.btnView = new DevComponents.DotNetBar.ButtonItem();
            this.btnClose = new DevComponents.DotNetBar.ButtonItem();
            this.btnCancel = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem4 = new DevComponents.DotNetBar.LabelItem();
            this.AppMarkerDefault = new DevComponents.DotNetBar.ButtonItem();
            this.AppMarkerBusy = new DevComponents.DotNetBar.ButtonItem();
            this.AppMarkerFree = new DevComponents.DotNetBar.ButtonItem();
            this.AppMarkerOutOfOffice = new DevComponents.DotNetBar.ButtonItem();
            this.AppMarkerTentative = new DevComponents.DotNetBar.ButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // calendarView1
            // 
            this.calendarView1.AutoScrollMinSize = new System.Drawing.Size(26880, 102);
            this.calendarView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.calendarView1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.calendarView1.ContainerControlProcessDialogKey = true;
            this.calendarView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calendarView1.EnableDragDrop = false;
            this.calendarView1.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calendarView1.HighlightCurrentDay = true;
            this.calendarView1.Is24HourFormat = true;
            this.calendarView1.LabelTimeSlots = true;
            this.calendarView1.Location = new System.Drawing.Point(0, 63);
            this.calendarView1.MultiUserTabHeight = 19;
            this.calendarView1.Name = "calendarView1";
            this.calendarView1.SelectedView = DevComponents.DotNetBar.Schedule.eCalendarView.TimeLine;
            this.calendarView1.Size = new System.Drawing.Size(1092, 635);
            this.calendarView1.TabIndex = 2;
            this.calendarView1.Text = "calendarView1";
            this.calendarView1.TimeIndicator.BorderColor = System.Drawing.Color.Empty;
            this.calendarView1.TimeIndicator.IndicatorArea = DevComponents.DotNetBar.Schedule.eTimeIndicatorArea.All;
            this.calendarView1.TimeIndicator.Tag = null;
            this.calendarView1.TimeIndicator.Visibility = DevComponents.DotNetBar.Schedule.eTimeIndicatorVisibility.AllResources;
            this.calendarView1.TimeRulerFont = new System.Drawing.Font("Segoe UI", 11.7F);
            this.calendarView1.TimeRulerFontSm = new System.Drawing.Font("Segoe UI", 7.2F);
            this.calendarView1.TimeSlotDuration = 15;
            this.calendarView1.SelectedViewChanged += new System.EventHandler<DevComponents.DotNetBar.Schedule.SelectedViewEventArgs>(this.calendarView1_SelectedViewChanged);
            this.calendarView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.calendarView1_MouseUp);
            // 
            // dateNavigator1
            // 
            this.dateNavigator1.CalendarView = this.calendarView1;
            this.dateNavigator1.CanvasColor = System.Drawing.SystemColors.Control;
            this.dateNavigator1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2013;
            this.dateNavigator1.CurrentDayLabel = "yyyy-MM-dd";
            this.dateNavigator1.CurrentWeekEndLabel = "yyyy-MM-dd";
            this.dateNavigator1.CurrentWeekStartLabel = "yyyy-MM-dd";
            this.dateNavigator1.DisabledBackColor = System.Drawing.Color.Empty;
            this.dateNavigator1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dateNavigator1.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateNavigator1.Location = new System.Drawing.Point(0, 33);
            this.dateNavigator1.Name = "dateNavigator1";
            this.dateNavigator1.Size = new System.Drawing.Size(1092, 30);
            this.dateNavigator1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.dateNavigator1.TabIndex = 1;
            this.dateNavigator1.Text = "dateNavigator1";
            this.dateNavigator1.DateChanged += new System.EventHandler<DevComponents.DotNetBar.Schedule.DateNavigator.DateChangedEventArgs>(this.dateNavigator1_DateChanged);
            // 
            // barView
            // 
            this.barView.Dock = System.Windows.Forms.DockStyle.Top;
            this.barView.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barView.IsMaximized = false;
            this.barView.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.cboRoute,
            this.btnDay,
            this.btnWeek,
            this.btnMonth,
            this.btnYear,
            this.btnTimeline});
            this.barView.Location = new System.Drawing.Point(0, 0);
            this.barView.Name = "barView";
            this.barView.Size = new System.Drawing.Size(1092, 33);
            this.barView.Stretch = true;
            this.barView.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.barView.TabIndex = 0;
            this.barView.TabStop = false;
            this.barView.Text = "bar1";
            // 
            // cboRoute
            // 
            this.cboRoute.DropDownHeight = 106;
            this.cboRoute.ItemHeight = 22;
            this.cboRoute.Name = "cboRoute";
            // 
            // btnDay
            // 
            this.btnDay.Checked = true;
            this.btnDay.Name = "btnDay";
            this.btnDay.OptionGroup = "View";
            this.btnDay.Text = "Day";
            this.btnDay.Click += new System.EventHandler(this.btnDay_Click);
            // 
            // btnWeek
            // 
            this.btnWeek.Name = "btnWeek";
            this.btnWeek.OptionGroup = "View";
            this.btnWeek.Text = "Week";
            this.btnWeek.Click += new System.EventHandler(this.btnWeek_Click);
            // 
            // btnMonth
            // 
            this.btnMonth.Name = "btnMonth";
            this.btnMonth.OptionGroup = "View";
            this.btnMonth.Text = "Month";
            this.btnMonth.Click += new System.EventHandler(this.btnMonth_Click);
            // 
            // btnYear
            // 
            this.btnYear.Name = "btnYear";
            this.btnYear.OptionGroup = "View";
            this.btnYear.Text = "Year";
            this.btnYear.Click += new System.EventHandler(this.btnYear_Click);
            // 
            // btnTimeline
            // 
            this.btnTimeline.Name = "btnTimeline";
            this.btnTimeline.Text = "Time Line";
            this.btnTimeline.Click += new System.EventHandler(this.btnTimeLine_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "buttonItem1";
            // 
            // buttonItem10
            // 
            this.buttonItem10.Name = "buttonItem10";
            this.buttonItem10.Text = "&3. Customer Email.rtf";
            // 
            // controlContainerItem1
            // 
            this.controlContainerItem1.AllowItemResize = true;
            this.controlContainerItem1.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem1.Name = "controlContainerItem1";
            // 
            // buttonItem14
            // 
            this.buttonItem14.AutoCheckOnClick = true;
            this.buttonItem14.Name = "buttonItem14";
            this.buttonItem14.OptionGroup = "View";
            this.buttonItem14.SubItemsExpandWidth = 14;
            this.buttonItem14.Text = "Day";
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))), System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199))))));
            // 
            // ribbonTabItem1
            // 
            this.ribbonTabItem1.Checked = true;
            this.ribbonTabItem1.Name = "ribbonTabItem1";
            this.ribbonTabItem1.Text = "Calendar";
            // 
            // contextMenuBar1
            // 
            this.contextMenuBar1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.contextMenuBar1.IsMaximized = false;
            this.contextMenuBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.InContentContextMenu,
            this.AppointmentContextMenu});
            this.contextMenuBar1.Location = new System.Drawing.Point(209, 224);
            this.contextMenuBar1.Name = "contextMenuBar1";
            this.contextMenuBar1.Size = new System.Drawing.Size(565, 25);
            this.contextMenuBar1.Stretch = true;
            this.contextMenuBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.contextMenuBar1.TabIndex = 6;
            this.contextMenuBar1.TabStop = false;
            this.contextMenuBar1.Text = "contextMenuBar1";
            // 
            // InContentContextMenu
            // 
            this.InContentContextMenu.AutoExpandOnClick = true;
            this.InContentContextMenu.Name = "InContentContextMenu";
            this.InContentContextMenu.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.InContentAddAppContextItem});
            this.InContentContextMenu.Text = "InContent";
            // 
            // InContentAddAppContextItem
            // 
            this.InContentAddAppContextItem.Name = "InContentAddAppContextItem";
            this.InContentAddAppContextItem.Symbol = "59485";
            this.InContentAddAppContextItem.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.InContentAddAppContextItem.Text = "Tạo kế hoạch";
            this.InContentAddAppContextItem.Click += new System.EventHandler(this.miAdd_Click);
            // 
            // AppointmentContextMenu
            // 
            this.AppointmentContextMenu.AutoExpandOnClick = true;
            this.AppointmentContextMenu.Name = "AppointmentContextMenu";
            this.AppointmentContextMenu.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnView,
            this.btnClose,
            this.btnCancel});
            this.AppointmentContextMenu.Text = "Appointment";
            // 
            // btnView
            // 
            this.btnView.Name = "btnView";
            this.btnView.Symbol = "59636";
            this.btnView.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.btnView.Text = "Xem";
            this.btnView.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnClose
            // 
            this.btnClose.Name = "btnClose";
            this.btnClose.Symbol = "59528";
            this.btnClose.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Symbol = "";
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnDeletePlan_Click);
            // 
            // labelItem4
            // 
            this.labelItem4.Name = "labelItem4";
            // 
            // AppMarkerDefault
            // 
            this.AppMarkerDefault.Name = "AppMarkerDefault";
            // 
            // AppMarkerBusy
            // 
            this.AppMarkerBusy.Name = "AppMarkerBusy";
            // 
            // AppMarkerFree
            // 
            this.AppMarkerFree.Name = "AppMarkerFree";
            // 
            // AppMarkerOutOfOffice
            // 
            this.AppMarkerOutOfOffice.Name = "AppMarkerOutOfOffice";
            // 
            // AppMarkerTentative
            // 
            this.AppMarkerTentative.Name = "AppMarkerTentative";
            // 
            // fmProductionPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1092, 698);
            this.Controls.Add(this.contextMenuBar1);
            this.Controls.Add(this.calendarView1);
            this.Controls.Add(this.dateNavigator1);
            this.Controls.Add(this.barView);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fmProductionPlan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.LineAppointment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem10;
        private DevComponents.DotNetBar.ButtonItem buttonItem14;
        private DevComponents.DotNetBar.Bar barView;
        private DevComponents.DotNetBar.ButtonItem btnDay;
        private DevComponents.DotNetBar.ButtonItem btnWeek;
        private DevComponents.DotNetBar.ButtonItem btnMonth;
        private DevComponents.DotNetBar.Schedule.DateNavigator dateNavigator1;
        private DevComponents.DotNetBar.Schedule.CalendarView calendarView1;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.RibbonTabItem ribbonTabItem1;
        private DevComponents.DotNetBar.ContextMenuBar contextMenuBar1;
        private DevComponents.DotNetBar.ButtonItem InContentContextMenu;
        private DevComponents.DotNetBar.ButtonItem InContentAddAppContextItem;
        //private DevComponents.DotNetBar.ButtonItem AppCatOrange;
        //private DevComponents.DotNetBar.ButtonItem AppCatPurple;
        //private DevComponents.DotNetBar.ButtonItem AppCatRed;
        //private DevComponents.DotNetBar.ButtonItem AppCatYellow;
        private DevComponents.DotNetBar.ButtonItem AppMarkerDefault;
        private DevComponents.DotNetBar.ButtonItem AppMarkerBusy;
        private DevComponents.DotNetBar.ButtonItem AppMarkerFree;
        private DevComponents.DotNetBar.ButtonItem AppMarkerOutOfOffice;
        private DevComponents.DotNetBar.ButtonItem AppMarkerTentative;
        private DevComponents.DotNetBar.LabelItem labelItem4;
        private DevComponents.DotNetBar.ButtonItem btnYear;
        private DevComponents.DotNetBar.ComboBoxItem cboRoute;
        private DevComponents.DotNetBar.ButtonItem btnTimeline;
        private DevComponents.DotNetBar.ButtonItem AppointmentContextMenu;
        private DevComponents.DotNetBar.ButtonItem btnView;
        private DevComponents.DotNetBar.ButtonItem btnClose;
        private DevComponents.DotNetBar.ButtonItem btnCancel;
    }
}

