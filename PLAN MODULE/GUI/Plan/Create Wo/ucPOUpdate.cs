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

namespace PLAN_MODULE.GUI.Plan.Create_Wo
{
    public partial class ucPOUpdate : UserControl
    {
        private readonly string _UserId;
        private POOrder _POOrder;
        public ucPOUpdate(string userId, POOrder pOOrder)
        {
            InitializeComponent();
            _UserId = userId;
            _POOrder = pOOrder;


            this.Load += UcPOUpdate_Load;
        }

        private void UcPOUpdate_Load(object sender, EventArgs e)
        {
            
        }

        
    }
}
