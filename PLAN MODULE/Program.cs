using PLAN_MODULE.GUI.Account;
using PLAN_MODULE.GUI.Plan;
using PLAN_MODULE.GUI.Plan.Scheduler;
using PLAN_MODULE.GUI.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLAN_MODULE
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new fmProductionPlan());
            Application.Run(new fmLogin());
            ////Application.Run(new CreatPlan());
        }
    }
}
