using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLAN_MODULE
{
    public partial class IMAGE : UserControl
    {
        private static IMAGE instacne;
        public static IMAGE Instacne
        {
            get
            {
                if (instacne == null)  instacne = new IMAGE();
                return instacne;

            }
            private set { IMAGE.instacne = value; }
        }
        public IMAGE()
        {
            InitializeComponent();
        }
    }
}
