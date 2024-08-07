using DevExpress.PivotGrid.OLAP.AdoWrappers;
using DevExpress.XtraLayout.Filtering.Templates;
using PLAN_MODULE.DAO;
using PLAN_MODULE.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLAN_MODULE.GUI.Tools
{
    public partial class ucRpLaser : UserControl
    {
        DAO_WorkInfor DAO_WorkInfor = new DAO_WorkInfor();
        CreateLaserPlanDao _CreateLaserPlanDao = new CreateLaserPlanDao();
        public ucRpLaser()
        {
            InitializeComponent();
        }
        WorkOrder WorkOrder = new WorkOrder();

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string workId = txtWorkId.Text.Trim();
            DateTime start = dtStartTime.Value;
            DateTime end =dtEndTime.Value;
         
         
            var laserMark = _CreateLaserPlanDao.GetLaserMarkReal(start, end);
            var planLaser = _CreateLaserPlanDao.GetPlanedNew(start, end);

            var laserDetail = (from l in laserMark
                                group l by new { l.WorkId, l.TimeMark.Date } into gr
                                select new
                                {
                                    WorkId = gr.Key.WorkId,
                                    Date = gr.Key.Date,
                                 
                                    QtyPanel = gr.Where(x=>x.TemType == 1).Count(),
                                    QtyPCB = gr.Where(x=>x.TemType == 0).Count()
                                }).ToList();


            var rs = (from ld in laserDetail
                      join lm in planLaser on new { Work = ld.WorkId, Date = ld.Date } equals new { Work = lm.WorkID, Date = lm.Date } into ldm
                      from ldms in ldm.DefaultIfEmpty()
                      select new
                      {
                          WorkId = ld.WorkId,
                          Date = ld.Date,
                          QtyPlan = ldms != null ? ldms.Qty : 0,
                          Line = ldms != null ? ldms.line : "",
                          Panel = ld.QtyPanel,
                          PCB = ld.QtyPCB
                      }).ToList();

            if (!string.IsNullOrEmpty(workId))
            {
                WorkOrder = DAO_WorkInfor.getWorkInfor(txtWorkId.Text);
                if (string.IsNullOrEmpty(WorkOrder.WorkID))
                {
                    MessageBox.Show($"Work order không tồn tại!");
                    return;
                }
                if (WorkOrder.state == WorkOrder.stateClose)
                {
                    MessageBox.Show($"Work order đã đóng!");
                    return;
                }
                lbMarked.Text = _CreateLaserPlanDao.getMarkInfor(WorkOrder.WorkID);
                rs = rs.Where(x=>x.WorkId == workId).ToList();
            }

            gridControl1.DataSource = rs;
        }
    }
}
