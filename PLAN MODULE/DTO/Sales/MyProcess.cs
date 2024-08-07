using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Sales
{
    public class MyProcess
    {
        public string P { get; set; }
    }
    public static class LoadMyProcess
    {
        public static List<MyProcess> myProcesses = new List<MyProcess>();
        static LoadMyProcess()
        {
            myProcesses.Add(new MyProcess()
            {
                P = "SMT"
            });
            myProcesses.Add(new MyProcess()
            {
                P = "PTH"
            });
            myProcesses.Add(new MyProcess()
            {
                P = "ASSY"
            });
        }
    }
}
