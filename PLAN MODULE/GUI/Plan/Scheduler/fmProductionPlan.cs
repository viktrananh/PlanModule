using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Schedule;
using DevComponents.DotNetBar;
using DevComponents.Schedule.Model;
using System.Collections.Generic;
using PLAN_MODULE.DTO.Planed;
using PLAN_MODULE.DAO.PLAN;
using System.Linq;
using System.Data;
using PLAN_MODULE.BUS.PLAN;
using DocumentFormat.OpenXml.InkML;
using PLAN_MODULE.DTO.ProductionPlans;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using ROSE_Dll.DAO;
using ROSE_Dll.DTO;
using ROSE_Dll.ViewModel.ProducingProcess;

namespace PLAN_MODULE.GUI.Plan.Scheduler
{
    public partial class fmProductionPlan : Office2007Form
    {
        #region Private data
        ProductionPlanDAO _ProductionPlanDAO = new ProductionPlanDAO();
        ProductionPlanBUS _ProductionPlanBUS = new ProductionPlanBUS();
        ClusterDAO _ClusterDAO = new ClusterDAO();
        ROSE_Dll.DataLayer DataLayer = new ROSE_Dll.DataLayer();

        private bool _UserStyleSet;
        readonly string _USER = string.Empty;
        #endregion
        List<LineDefine> lines = new List<LineDefine>();
        public fmProductionPlan()
        {
            InitializeComponent();
            //this._USER = usser;

            lines = _ClusterDAO.GetAllLineDefine();
            var process = _ProductionPlanDAO.GetAllProcess();
            cboRoute.ComboBoxEx.DataSource = process;
            cboRoute.ComboBoxEx.DisplayMember = "Name";
            cboRoute.ComboBoxEx.ValueMember = "Id";
            //eCondensedViewVisibility.Hidden;
            this.cboRoute.SelectedIndexChanged += new System.EventHandler(this.cboRoute_SelectedIndexChanged);
            calendarView1.TimeLineCondensedViewVisibility
                = eCondensedViewVisibility.Hidden;
            calendarView1.AppointmentReminder += AppointmentReminder;
            calendarView1.AppointmentStartTimeReached += AppointmentStartTimeReached;

        }
        public fmProductionPlan(string usser)
        {
            InitializeComponent();
            this._USER = usser;

            lines = _ClusterDAO.GetAllLineDefine();
            var process = _ProductionPlanDAO.GetAllProcess();
            cboRoute.ComboBoxEx.DataSource = process;
            cboRoute.ComboBoxEx.DisplayMember = "Name";
            cboRoute.ComboBoxEx.ValueMember = "Id";
            //eCondensedViewVisibility.Hidden;
            this.cboRoute.SelectedIndexChanged += new System.EventHandler(this.cboRoute_SelectedIndexChanged);
            calendarView1.TimeLineCondensedViewVisibility
                = eCondensedViewVisibility.Hidden;
            calendarView1.AppointmentReminder += AppointmentReminder;
            calendarView1.AppointmentStartTimeReached += AppointmentStartTimeReached;

        }
        List<PlanDetail> plans = new List<PlanDetail>();
        private void LineAppointment_Load(object sender, EventArgs e)
        {


            //FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }
        private void cboRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            string route = cboRoute.Text;
            int processId = int.Parse(cboRoute.ComboBoxEx.SelectedValue.ToString());
            DateTime myDate = DateTime.Today;

            LoadPlans(processId);


        }
        List<StationModel> stationModels = new List<StationModel>();
        void LoadPlans(int processId)
        {
            var _line = lines.Where(x => x.ProcessId == processId).Select(x => x.Name).ToArray();

            calendarView1.DisplayedOwners.Clear();
            calendarView1.DisplayedOwners.AddRange(_line);
            Thread th = new Thread(() =>
            {
                while (true)
                {
                    DateTime timeNow = _ProductionPlanDAO.getTimeServer();
                    DateTime timeStartView = new DateTime();
                    DateTime timeEndView = new DateTime();

                    List<ProductionPlanVM> productionPlanVm = new List<ProductionPlanVM>();
                    var now = DateTime.Now;
                    var currentDay = now.DayOfWeek;
                    int days = (int)currentDay;
                    DateTime sunday = now.AddDays(-days);
                    var daysThisWeek = Enumerable.Range(0, 7)
                        .Select(d => sunday.AddDays(d))
                        .ToList();
                    foreach (var item in daysThisWeek)
                    {
                        var productPlan = _ProductionPlanDAO.GetAllProductionPlan(item.ToString("yyyy-MM-dd"));
                        productionPlanVm.AddRange(productPlan);
                    }



                    if (timeNow.Hour < 8)
                    {
                        timeStartView = new DateTime(timeNow.AddDays(-1).Year, timeNow.AddDays(-1).Month, timeNow.AddDays(-1).Day, 8, 0, 0);
                        timeEndView = new DateTime(timeNow.Year, timeNow.Month, timeNow.Day, 8, 0, 0);

                    }
                    else
                    {
                        timeStartView = new DateTime(timeNow.Year, timeNow.Month, timeNow.Day, 8, 0, 0);
                        timeEndView = new DateTime(timeNow.AddDays(1).Year, timeNow.AddDays(1).Month, timeNow.AddDays(1).Day, 8, 0, 0);
                    }

                    CalendarModel model = new CalendarModel();

                    this.Invoke((MethodInvoker)delegate
                    {

                        calendarView1.CalendarModel = model;
                        calendarView1.TimeSlotDuration = 30;
                        calendarView1.TimeLinePeriod = eTimeLinePeriod.Hours;
                        calendarView1.TimeLineInterval = 1;
                        calendarView1.TimeLineStretchRowHeight = false;
                        calendarView1.TimeLineViewStartDate = timeStartView;
                        calendarView1.TimeLineViewEndDate = timeEndView;
                    });
                   

                    string route = "";
                    var planDatRoute = plans.Where(x => x.Route == route).Select(x => x).ToList();
                    calendarView1.GetDisplayTemplateText += CalendarViewGetDisplayTemplateText;




                    foreach (var item in productionPlanVm)
                    {
                        string work = item.WorkId;
                        string modelID = item.ModelId;
                        DateTime timeStart = item.StartTime;
                        DateTime timeEnd = item.EndTime;
                        string cluster = item.ClusterName;
                        int totalQty = item.Count;
                        string line = item.LineName;
                        ViewPlan(item);


                        if (timeNow < item.StartTime) continue;
                        if (item.State == 1 || item.State == 2) continue;
                        int realQty = _ProductionPlanDAO.GetRealQtyTime(timeStart, timeNow, work, line, item.StationName);

                        int remainQty = totalQty - realQty;
                        if (remainQty <= 0)
                        { 
                            if(item.State == 0)
                            {
                                _ProductionPlanBUS.FinishPlan(item);
                            }
                            continue;
                        }
                        ProductionPlanVM planDetailReal = new ProductionPlanVM()
                        {
                            WorkId = work,
                            ModelId = modelID,
                            Count = realQty,
                            LineId = item.LineId,
                            LineName = item.LineName,
                            StartTime = item.StartTime,
                            EndTime = timeNow,
                        };
                        ViewPlanReal(planDetailReal);
                      

                        List<ClusterDetailVm> clusterDetailVms = new List<ClusterDetailVm>()
                {
                    new ClusterDetailVm() { LineId = item.LineId, LineName = item.LineName, Cycletime = item.CycleTime, Performance = item.Performance  }
                };
                        var planDetailsWaiting = _ProductionPlanDAO.CreatProductionPlan(work, modelID, remainQty, timeNow, clusterDetailVms);

                        foreach (var Subitem in planDetailsWaiting)
                        {
                            ProductionPlanVM planDetailWaiting = new ProductionPlanVM()
                            {
                                WorkId = work,
                                ModelId = modelID,
                                Count = remainQty,
                                StartTime = Subitem.StartTime,
                                EndTime = Subitem.EndTime,
                                LineName = Subitem.LineName,
                                LineId = Subitem.LineId,
                            };

                            // --------  Cập nhật kế hoạch mới ------------------


                            ViewPlanWaiting(planDetailWaiting);
                        }
                    }
                    Thread.Sleep(30000);
                }

            });
            th.IsBackground = true;
            th.Start();
        }
        private Appointment ViewPlan(ProductionPlanVM plans)
        {

            Appointment appointment = new Appointment();
            appointment.StartTime = plans.StartTime;
            appointment.EndTime = plans.EndTime;
            appointment.OwnerKey = plans.LineName;
            appointment.CategoryColor = Appointment.CategoryGreen;
            appointment.Subject = $"{plans.WorkId}-{plans.ModelId}-{plans.ClusterName}, {plans.Count} PCBS";
            appointment.TimeMarkedAs = Appointment.TimerMarkerTentative;
            appointment.Locked = true;
            appointment.Tooltip = $"{plans.WorkId}-{plans.ClusterName}-{plans.ProcessName},{plans.Count} PCBS,{plans.PlanIndex}," +
                $" {plans.StartTime.ToString("yyyy-MM-dd HH:mm:ss")},{plans.EndTime.ToString("yyyy-MM-dd HH:mm:ss")}";
            appointment.DisplayTemplate = "[Subject]" + " [Description]";
            calendarView1.CalendarModel.Appointments.Add(appointment);
            appointment.Tag = plans;
            return (appointment);
        }
        private Appointment ViewPlanReal(ProductionPlanVM plans)
        {
            Appointment appointment = new Appointment();
            appointment.StartTime = plans.StartTime;
            appointment.EndTime = plans.EndTime;
            appointment.OwnerKey = plans.LineName;
            appointment.CategoryColor = Appointment.CategoryYellow;
            appointment.Subject = $"{plans.WorkId}, {plans.Count} PCBS";
            //appointment.TimeMarkedAs = Appointment.TimerMarkerTentative;
            appointment.Locked = true;
            appointment.Description = $"Sản lượng thực tế";

            appointment.DisplayTemplate = "[Subject]" + " [Description]";
            calendarView1.CalendarModel.Appointments.Add(appointment);
            return (appointment);
        }

        private Appointment ViewPlanWaiting(ProductionPlanVM plans)
        {
            Appointment appointment = new Appointment();
            appointment.StartTime = plans.StartTime;
            appointment.EndTime = plans.EndTime;
            appointment.OwnerKey = plans.LineName;
            appointment.CategoryColor = Appointment.CategoryGreen;
            appointment.Subject = $"{plans.WorkId} , {plans.Count} PCBS";
            //appointment.TimeMarkedAs = Appointment.TimerMarkerTentative;
            appointment.Locked = true;
            appointment.Description = $"Sản lượng còn thiếu";

            appointment.DisplayTemplate = "[Subject]" + " [Description]";
            calendarView1.CalendarModel.Appointments.Add(appointment);
            return (appointment);
        }
        void CalendarViewGetDisplayTemplateText(object sender, GetDisplayTemplateTextEventArgs e)
        {
            AppointmentView view = e.CalendarItem as AppointmentView;

            if (view != null)
            {
                switch (e.DisplayTemplate)
                {
                    case "[MyStartTimeTemplate]":
                        e.DisplayText = String.Format("{0:F}", view.Appointment.StartTime);
                        break;

                    case "[MyEndTimeTemplate]":
                        e.DisplayText = String.Format("{0:T}", view.Appointment.EndTime);
                        break;
                }
            }
        }
        private void CalendarView1_GetDisplayTemplateText(object sender, GetDisplayTemplateTextEventArgs e)
        {
            AppointmentView view = e.CalendarItem as AppointmentView;

            if (view != null)
            {
                switch (e.DisplayTemplate)
                {
                    case "[MyStartTimeTemplate]":
                        e.DisplayText = String.Format("{0:F}", view.Appointment.StartTime);
                        break;

                    case "[MyEndTimeTemplate]":
                        e.DisplayText = String.Format("{0:T}", view.Appointment.EndTime);
                        break;
                }
            }
        }
        #region Handled events


        void ModelAppointmentAdded(object sender, AppointmentEventArgs e)
        {
            Console.WriteLine("{0} New Appointment Added. Appointment Subject: {1}",
                DateTime.Now, e.Appointment.Subject);
        }


        void ModelAppointmentRemoved(object sender, AppointmentEventArgs e)
        {
            Console.WriteLine("{0} Appointment Removed. Appointment Subject: {1}",
                DateTime.Now, e.Appointment.Subject);
        }

        void AppointmentViewChanged(object sender, AppointmentViewChangedEventArgs e)
        {
            AppointmentView view = e.CalendarItem as AppointmentView;

            if (view != null)
            {
                string sOperation = (e.eViewOperation == eViewOperation.AppointmentMove)
                    ? "Moved" : "Resized";

                Console.WriteLine("{0} Appointment {1}. Appointment Subject: {2}",
                    DateTime.Now, sOperation, view.Appointment.Subject);
            }
        }


        void AppointmentReminder(object sender, ReminderEventArgs e)
        {
            string s = string.Format(
                "Appointment Reminder reached for Appointment: '{0}'",
                e.Reminder.Appointment.Subject);

            MessageBox.Show(s);
        }


        void AppointmentStartTimeReached(object sender, AppointmentEventArgs e)
        {
            string s = string.Format(
                "Appointment StartTime Reached for Appointment: '{0}'",
                e.Appointment.Subject);

            MessageBox.Show(s);
        }

        #endregion




        #region View change

        private void btnDay_Click(object sender, EventArgs e)
        {

            calendarView1.SelectedView = eCalendarView.Day;
        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            calendarView1.SelectedView = eCalendarView.Week;
        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            calendarView1.SelectedView = eCalendarView.Month;
        }
        private void btnYear_Click(object sender, EventArgs e)
        {
            calendarView1.SelectedView = eCalendarView.Year;
        }


        private void btnTimeLine_Click(object sender, EventArgs e)
        {
            calendarView1.SelectedView = eCalendarView.TimeLine;
        }


        #endregion

        #region calendarView1_MouseUp

        private void calendarView1_MouseUp(object sender, MouseEventArgs e)
        {
            // Process Right MouseUp event in order to
            // present view specific ContextMenu

            if (e.Button == MouseButtons.Right)
            {
                // Main Calendar View hit

                if (sender is BaseView)
                    BaseViewMouseUp(sender, e);
                else if (sender is AppointmentView)
                    AppointmentViewMouseUp(sender);

            }
        }

        #region BaseViewMouseUp

        private void BaseViewMouseUp(object sender, MouseEventArgs e)
        {
            BaseView view = sender as BaseView;
            eViewArea area = view.GetViewAreaFromPoint(e.Location);

            if (area == eViewArea.InContent)
                InContentMouseUp(view, e);


        }
        private void AppointmentViewMouseUp(object sender)
        {
            AppointmentView view = sender as AppointmentView;

            // Select the appointment
            view.IsSelected = true;
            // Let the user delete the appointment
            btnClose.Enabled = (view.Appointment.IsRecurringInstance == false);
            AppointmentContextMenu.Tag = view;
            ShowContextMenu(AppointmentContextMenu);
        }
        #region InContentMouseUp

        /// <summary>
        /// Handles BaseView InContent MouseUp events
        /// </summary>
        /// <param name="view">BaseView</param>
        /// <param name="e">MouseEventArgs</param>
        private void InContentMouseUp(BaseView view, MouseEventArgs e)
        {
            // Get the DateSelection start and end
            // from the current mouse location

            DateTime startDate, endDate;

            if (calendarView1.GetDateSelectionFromPoint(
                e.Location, out startDate, out endDate) == true)
            {
                // If this date falls outside the currently
                // selected range (DateSelectionStart and DateSelectionEnd)
                // then select the new range

                if (IsDateSelected(startDate, endDate) == false)
                {
                    calendarView1.DateSelectionStart = startDate;
                    calendarView1.DateSelectionEnd = endDate;
                }
            }

            // Remove any previously added view specific items

            //for (int i = InContentContextMenu.SubItems.Count - 1; i > 0; i--)
            //    InContentContextMenu.SubItems.RemoveAt(i);

            // If this is a TimeLine view, then add a couple
            // of extra items

            if (view is TimeLineView)
            {
                ButtonItem bi = new ButtonItem();

                // Page Navigator

                //bi.Text = (calendarView1.TimeLineShowPageNavigation == true)
                //    ? "Hide Page Navigator" : "Show Page Navigator";

                //bi.BeginGroup = true;

                //bi.Click += BiPageNavigatorClick;

                //InContentContextMenu.SubItems.Add(bi);

                // Condensed Visibility
                //if (calendarView1.TimeLineCondensedViewVisibility ==
                //    eCondensedViewVisibility.Hidden)
                //{
                //    bi = new ButtonItem("", "Show Condensed View");
                //    bi.Click += BiCondensedClick;

                //    InContentContextMenu.SubItems.Add(bi);
                //}
            }

            ShowContextMenu(InContentContextMenu);
        }

        #region BiCondensedClick

        /// <summary>
        /// Handles InContentContextMenu Condensed selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BiCondensedClick(object sender, EventArgs e)
        {
            calendarView1.TimeLineCondensedViewVisibility =
                eCondensedViewVisibility.AllResources;
        }

        #endregion

        #region BiPageNavigatorClick

        /// <summary>
        /// Handles InContentContextMenu PageNavigator selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BiPageNavigatorClick(object sender, EventArgs e)
        {
            calendarView1.TimeLineShowPageNavigation =
                (calendarView1.TimeLineShowPageNavigation == false);
        }

        #endregion

        #region IsDateSelected

        /// <summary>
        /// Determines if the given date range is within the currently selected
        /// CalendarView selection range (DateSelectionStart to DateSelectionEnd)
        /// </summary>
        /// <param name="startDate">Start date range</param>
        /// <param name="endDate">End date range</param>
        /// <returns>True if selected</returns>
        private bool IsDateSelected(DateTime startDate, DateTime endDate)
        {
            if (calendarView1.DateSelectionStart.HasValue && calendarView1.DateSelectionEnd.HasValue)
            {
                if (calendarView1.DateSelectionStart.Value <= startDate &&
                    calendarView1.DateSelectionEnd.Value >= endDate)
                    return (true);
            }

            return (false);
        }

        #endregion

        #region miAdd_Click

        void miAdd_Click(object sender, EventArgs e)
        {
            if (!DataLayer.checkaccountGroupCanUse(_USER, "PLAN"))
            {
                MessageBox.Show("Tài khoản không đủ quyền");
                return;
            }

            DateTime startDate = calendarView1.DateSelectionStart.GetValueOrDefault();
            DateTime endDate = calendarView1.DateSelectionEnd.GetValueOrDefault();
            string line = calendarView1.SelectedOwner;
            string Router = cboRoute.Text.Trim();
            string process = cboRoute.Text.Trim();
            fmCreatPlan f = new fmCreatPlan(_USER, 0, null);
            f.ShowDialog();
            string route = cboRoute.Text;
            LoadPlans(1);

        }





        #endregion

        #endregion

        #region InDayOfWeekHeaderMouseUp


        #region miCalendarColor_Click

        /// <summary>
        /// Handles selection of a ContextMenu color item
        /// </summary>
        /// <param name="sender">MenuItem</param>
        /// <param name="e">EventArgs</param>
        void miCalendarColor_Click(object sender, EventArgs e)
        {
            ButtonItem mi = sender as ButtonItem;
            BaseView view = mi.Parent.Tag as BaseView;

            if (view != null)
                view.CalendarColor = (eCalendarColor)Enum.Parse(typeof(eCalendarColor), mi.Text);
        }

        #endregion

        #region SideBar show/hide

        /// <summary>
        /// Handles ContextMenu "Show" for the SideBar panel
        /// </summary>
        /// <param name="sender">MenuItem</param>
        /// <param name="e">EventArgs</param>
        void miShowSideBar_Click(object sender, EventArgs e)
        {
            ButtonItem bi = sender as ButtonItem;
            MonthView view = bi.Parent.Tag as MonthView;

            if (view != null)
                view.IsSideBarVisible = true;
        }

        /// <summary>
        /// Handles ContextMenu "Hide" for the SideBar panel
        /// </summary>
        /// <param name="sender">MenuItem</param>
        /// <param name="e">EventArgs</param>
        void miHideSideBar_Click(object sender, EventArgs e)
        {
            ButtonItem bi = sender as ButtonItem;
            MonthView view = bi.Parent.Tag as MonthView;

            if (view != null)
                view.IsSideBarVisible = false;
        }


        #endregion

        #endregion



        #region Gridlines show/hide

        /// <summary>
        /// Handles ContextMenu "Show GridLines" for the Year View
        /// </summary>
        /// <param name="sender">MenuItem</param>
        /// <param name="e">EventArgs</param>
        void miShowGridLines_Click(object sender, EventArgs e)
        {
            ButtonItem bi = sender as ButtonItem;
            YearView view = bi.Parent.Tag as YearView;

            if (view != null)
                calendarView1.YearViewShowGridLines = true;
        }

        /// <summary>
        /// Handles ContextMenu "Hide GridLines" for the Year View
        /// </summary>
        /// <param name="sender">MenuItem</param>
        /// <param name="e">EventArgs</param>
        void miHideGridLines_Click(object sender, EventArgs e)
        {
            ButtonItem bi = sender as ButtonItem;
            YearView view = bi.Parent.Tag as YearView;

            if (view != null)
                calendarView1.YearViewShowGridLines = false;
        }

        #endregion

        #region miCycleHighlight_Click

        /// <summary>
        /// Handles ContextMenu "Cycle Highlight" for the Year View.  This
        /// routine cycles through all the possible Day Link Highlight values
        /// </summary>
        /// <param name="sender">MenuItem</param>
        /// <param name="e">EventArgs</param>
        void miCycleHighlight_Click(object sender, EventArgs e)
        {
            ButtonItem bi = sender as ButtonItem;
            YearView view = bi.Parent.Tag as YearView;

            if (view != null)
            {
                if (calendarView1.YearViewAppointmentLinkStyle == eYearViewLinkStyle.None)
                {
                    if (_UserStyleSet == false)
                    {
                        _UserStyleSet = true;

                        calendarView1.YearViewDrawDayBackground += YearViewDrawDayBackground;

                        calendarView1.Refresh();
                    }
                    else
                    {
                        _UserStyleSet = false;

                        calendarView1.YearViewDrawDayBackground -= YearViewDrawDayBackground;

                        NextLinkStyle();
                    }
                }
                else
                {
                    NextLinkStyle();
                }
            }
        }

        #endregion

        #region NextLinkStyle

        /// <summary>
        /// Sets the next available Link style
        /// </summary>
        private void NextLinkStyle()
        {
            eYearViewLinkStyle linkStyle = calendarView1.YearViewAppointmentLinkStyle;

            linkStyle++;

            if (linkStyle > eYearViewLinkStyle.Style5)
                linkStyle = eYearViewLinkStyle.None;

            calendarView1.YearViewAppointmentLinkStyle = linkStyle;
        }

        #endregion

        #region YearViewDrawDayBackground

        /// <summary>
        /// Draws the YearView day background
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void YearViewDrawDayBackground(object sender, YearViewDrawDayBackgroundEventArgs e)
        {
            if ((e.YearMonth.DayIsSelected(e.Date.Day) == false) &&
                (e.YearMonth.DayHasAppointments(e.Date.Day) == true))
            {
                Color color;

                switch (e.Date.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                    case DayOfWeek.Wednesday:
                    case DayOfWeek.Friday:
                        color = Color.Yellow;
                        break;

                    case DayOfWeek.Tuesday:
                    case DayOfWeek.Thursday:
                        color = Color.Coral;
                        break;

                    default:
                        color = Color.LightGreen;
                        break;
                }

                using (System.Drawing.Brush br = new SolidBrush(color))
                    e.Graphics.FillRectangle(br, e.Bounds);

                e.Cancel = true;
            }
        }

        #endregion

        #endregion


        #endregion

        #region InTabMouseUp



        #region AddAltUsersToMenu

        /// <summary>
        /// Adds alternate users to the DisplayedOwner list
        /// </summary>
        /// <param name="index">Where to add them at</param>
        /// <param name="evh">Event handler</param>
        /// <returns>Count of added items</returns>


        #endregion

        #region MiAddOwnerTabClick

        /// <summary>
        /// Handles selection of ContextMenu "Add Owner Tab"
        /// </summary>
        /// <param name="sender">MenuItem</param>
        /// <param name="e">EventArgs</param>
        void MiAddOwnerTabClick(object sender, EventArgs e)
        {
            ButtonItem bi = sender as ButtonItem;

            if (bi != null)
                calendarView1.DisplayedOwners.Insert(calendarView1.SelectedOwnerIndex, bi.Text);
        }

        #region GetFreeUser

        /// <summary>
        /// Gets a user that is not currently being
        /// used in the DisplayedOwners list
        /// </summary>
        /// <returns>Owner, or null</returns>

        #endregion

        #endregion

        #region miRemoveTab_Click

        /// <summary>
        /// Handles selection of ContextMenu "Remove Tab"
        /// </summary>
        /// <param name="sender">MenuItem</param>
        /// <param name="e">EventArgs</param>
        void miRemoveTab_Click(object sender, EventArgs e)
        {
            if (calendarView1.DisplayedOwners.Count > 1)
            {
                if (calendarView1.SelectedOwnerIndex >= 0)
                    calendarView1.DisplayedOwners.RemoveAt(calendarView1.SelectedOwnerIndex);
            }
        }

        #endregion

        #region MiReplaceTabClick

        /// <summary>
        /// Handles selection of ContextMenu "Replace Tab"
        /// </summary>
        /// <param name="sender">MenuItem</param>
        /// <param name="e">EventArgs</param>
        void MiReplaceTabClick(object sender, EventArgs e)
        {
            ButtonItem bi = sender as ButtonItem;

            calendarView1.DisplayedOwners
                [calendarView1.SelectedOwnerIndex] = (string)bi.Tag;
        }

        #endregion

        #endregion





        #region miDelete_Click

        /// <summary>
        /// Handles BaseView "Delete Appointment" 
        /// ContextMenu selections
        /// </summary>
        /// <param name="sender">MenuItem</param>
        /// <param name="e">EventArgs</param>
        void miDelete_Click(object sender, EventArgs e)
        {
            ButtonItem bi = sender as ButtonItem;
            AppointmentView view = bi.Parent.Tag as AppointmentView;
            DateTime timeNow = _ProductionPlanDAO.getTimeServer();

            if (view != null)
            {
                string detail = view.Tooltip;
                DateTime timeStart = view.StartTime;
                DateTime timeEnd = view.EndTime;
                string[] arr = detail.Split(',');
                if (arr.Count() != 6) return;
                string indexPlan = arr[2].Trim();
                string idPlan = arr[3].Trim();
                DataTable dt = _ProductionPlanDAO.GetPlanGloble(indexPlan);
                if (_ProductionPlanDAO.istableNull(dt))
                {
                    MessageBox.Show("L?i ! Không tìm th?y thông tin k? ho?ch !");
                    return;
                }
                DateTime TimeStartGloble = DateTime.Parse(dt.Rows[0]["START_TIME"].ToString());
                DateTime TimeEndGloble = DateTime.Parse(dt.Rows[0]["END_TIME"].ToString());
                //PlanDetail planDetail = new PlanDetail(dt.Rows[0]);

                if (timeNow >= TimeStartGloble && timeNow < TimeEndGloble)
                {
                    PlanDetail planDetail = new PlanDetail(dt.Rows[0]);

                    bool a = planDetail.TimeFrames.Any(x => x.StartTime <= timeNow && x.EndTime > timeNow);
                    if (!a)
                    {
                        MessageBox.Show("L?i ! không th? ?óng k? h?ah này !");
                        return;
                    }
                    if (_ProductionPlanBUS.ClosePlan(planDetail, timeNow, _USER))
                    {
                        MessageBox.Show("CLOSE PASS !");
                    }
                    else
                    {
                        MessageBox.Show("CLOSE FAIL !");
                    }
                }
                else
                {
                    MessageBox.Show("L?i ! không th? ?óng k? h?ah này !");
                }
                string route = cboRoute.Text;
                LoadPlans(1);




            }
        }
        #endregion

        #region CategoryColor_Click

        void CategoryColor_Click(object sender, EventArgs e)
        {
            ButtonItem bi = sender as ButtonItem;
            AppointmentView view = bi.Parent.Tag as AppointmentView;

            if (view != null)
                view.Appointment.CategoryColor = bi.Text;
        }

        #endregion

        #region TimeMarker_Click

        void TimeMarker_Click(object sender, EventArgs e)
        {
            ButtonItem bi = sender as ButtonItem;
            AppointmentView view = bi.Parent.Tag as AppointmentView;

            if (view != null)
            {
                view.Appointment.TimeMarkedAs =
                    bi.Text.Equals("Default") ? null : bi.Text;
            }
        }

        #endregion



        #region AllDayPanelMouseUp


        /// <summary>
        /// Handles "Shrink" ContextMenu selections
        /// </summary>
        /// <param name="sender">MenuItem</param>
        /// <param name="e">EventArgs</param>
        void miShrink_Click(object sender, EventArgs e)
        {
            ButtonItem bi = sender as ButtonItem;
            AllDayPanel panel = bi.Parent.Tag as AllDayPanel;

            if (calendarView1.FixedAllDayPanelHeight == -1)
                calendarView1.FixedAllDayPanelHeight = panel.PanelHeight - 20;

            else
            {
                calendarView1.FixedAllDayPanelHeight =
                    Math.Max(20, calendarView1.FixedAllDayPanelHeight - 20);
            }
        }

        /// <summary>
        /// Handles "Grow" ContextMenu selections
        /// </summary>
        /// <param name="sender">MenuItem</param>
        /// <param name="e">EventArgs</param>
        void miGrow_Click(object sender, EventArgs e)
        {
            ButtonItem bi = sender as ButtonItem;
            AllDayPanel panel = bi.Parent.Tag as AllDayPanel;

            if (calendarView1.FixedAllDayPanelHeight == -1)
                calendarView1.FixedAllDayPanelHeight = panel.PanelHeight + 20;

            else
            {
                calendarView1.FixedAllDayPanelHeight =
                    Math.Min(500, calendarView1.FixedAllDayPanelHeight + 20);
            }
        }

        /// <summary>
        /// Handles "Reset" ContextMenu selections
        /// </summary>
        /// <param name="sender">MenuItem</param>
        /// <param name="e">EventArgs</param>
        void miReset_Click(object sender, EventArgs e)
        {
            calendarView1.FixedAllDayPanelHeight = -1;
        }

        #endregion

        #region TimeRulerPanelMouseUp

        /// <summary>
        /// Handles TimeRulerPanel MouseUp events
        /// </summary>

        void miTimeDuration_Click(object sender, EventArgs e)
        {
            ButtonItem bi = sender as ButtonItem;

            int duration;

            if (int.TryParse(bi.Text, out duration) == true)
                calendarView1.TimeSlotDuration = duration;
        }

        #endregion

        #region TimeLineHeaderPanelMouseUp



        #region InPeriodHeaderHide_Click


        private void InPeriodHeaderHide_Click(object sender, EventArgs e)
        {
            calendarView1.TimeLineShowPeriodHeader = false;
        }

        #endregion







        #region BiIntervalClick


        private void BiIntervalClick(object sender, EventArgs e)
        {
            ButtonItem bi = sender as ButtonItem;

            int n;

            if (int.TryParse(bi.Text, out n) == true)
                calendarView1.TimeLineInterval = n;
        }

        #endregion

        #endregion

        #region btnIntervalHeaderShow_Click

        private void btnIntervalHeaderShow_Click(object sender, EventArgs e)
        {
            calendarView1.TimeLineShowPeriodHeader = true;
        }

        #endregion



        #region ShowContextMenu

        private void ShowContextMenu(ButtonItem cm)
        {
            cm.Popup(MousePosition);
        }

        #endregion



        #region CondensedViewContextMenu support

        private void btnCondensedViewAll_Click(object sender, EventArgs e)
        {


            calendarView1.TimeLineCondensedViewVisibility
                = eCondensedViewVisibility.AllResources;
        }


        private void btnCondensedViewSelected_Click(object sender, EventArgs e)
        {


            calendarView1.TimeLineCondensedViewVisibility
                = eCondensedViewVisibility.SelectedResource;
        }


        private void btnCondensedViewHidden_Click(object sender, EventArgs e)
        {

            calendarView1.TimeLineCondensedViewVisibility
                = eCondensedViewVisibility.Hidden;
        }

        #endregion

        #region Color Scheme Selection


        private void Office2007Blue_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.Office2007Blue;
        }

        private void Office2007Silver_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.Office2007Silver;
        }


        private void Office2007Black_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.Office2007Black;
        }


        private void Office2007VistaGlass_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.Office2007VistaGlass;
        }


        private void Office2010Blue_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.Office2010Blue;
        }


        private void Office2010Silver_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.Office2010Silver;
        }


        private void Windows7Blue_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.Windows7Blue;
        }

        #endregion

        #region btnToday_Click

        private void btnToday_Click(object sender, EventArgs e)
        {
            calendarView1.ShowDate(DateTime.Today);
        }

        #endregion





        #region calendarView1_SelectedViewChanged

        /// <summary>
        /// Processes CalendarView SelectedViewChanged events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calendarView1_SelectedViewChanged(object sender, SelectedViewEventArgs e)
        {
            switch (e.NewValue)
            {
                case eCalendarView.Day:
                    btnDay.Checked = true;
                    break;

                case eCalendarView.Week:
                    btnWeek.Checked = true;
                    break;

                case eCalendarView.Month:
                    btnMonth.Checked = true;
                    break;

                case eCalendarView.Year:
                    btnYear.Checked = true;
                    break;

                case eCalendarView.TimeLine:
                    btnTimeline.Checked = true;
                    break;
            }
        }
        #endregion

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ButtonItem bi = sender as ButtonItem;
            AppointmentView view = bi.Parent.Tag as AppointmentView;
            string prc = cboRoute.Text.Trim();
            if (view == null) return;

            Appointment ap = view.Appointment;

            var productionPlanVm = ap.Tag as ProductionPlanVM;

            fmCreatPlan f = new fmCreatPlan(_USER, 1, productionPlanVm);
            f.ShowDialog();

        }

        private void dateNavigator1_DateChanged(object sender, DateNavigator.DateChangedEventArgs e)
        {


        }

        private void btnDeletePlan_Click(object sender, EventArgs e)
        {
            if (!DataLayer.checkaccountGroupCanUse(_USER, "PLAN"))
            {
                MessageBox.Show("Tài khoản không đủ quyền");
                return;
            }
            ButtonItem bi = sender as ButtonItem;
            AppointmentView view = bi.Parent.Tag as AppointmentView;
            string prc = cboRoute.Text.Trim();
            if (view == null) return;

            Appointment ap = view.Appointment;

            var productionPlanVm = ap.Tag as ProductionPlanVM;

            DateTime timeNow = _ProductionPlanDAO.getTimeServer();
            DateTime timeStart = productionPlanVm.StartTime;

            if((timeNow -timeStart ).TotalMinutes > 30)
            {
                MessageBox.Show("Kế hoạch sản xuất đã thực hiện không thể xóa");
                return;
            }

            if (_ProductionPlanBUS.CancelPlan(productionPlanVm, _USER))
            {
                MessageBox.Show("Hủy thành công ");
            }
            else
            {
                MessageBox.Show("Lỗi ! Hủy kế hoạch thất bại");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (!DataLayer.checkaccountGroupCanUse(_USER, "PLAN"))
            {
                MessageBox.Show("Tài khoản không đủ quyền");
                return;
            }
            ButtonItem bi = sender as ButtonItem;
            AppointmentView view = bi.Parent.Tag as AppointmentView;
            string prc = cboRoute.Text.Trim();
            if (view == null) return;

            Appointment ap = view.Appointment;

            var productionPlanVm = ap.Tag as ProductionPlanVM;

            if (_ProductionPlanBUS.ClosePlan(productionPlanVm))
            {
                MessageBox.Show("Pass");
            }
            else
            {
                MessageBox.Show("Fail");

            }
        }
    }
}