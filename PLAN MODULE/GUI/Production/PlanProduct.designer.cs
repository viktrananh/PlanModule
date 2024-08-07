
namespace PLAN_MODULE
{
    partial class PlanProduct
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanProduct));
            this.dgvView = new Bunifu.UI.WinForms.BunifuDataGridView();
            this.stt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LINE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pcb = new DevComponents.DotNetBar.Controls.DataGridViewTextBoxDropDownColumn();
            this.Panel = new DevComponents.DotNetBar.Controls.DataGridViewTextBoxDropDownColumn();
            this.lbWork = new System.Windows.Forms.Label();
            this.lbTotal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtwork = new System.Windows.Forms.TextBox();
            this.lbMarked = new System.Windows.Forms.Label();
            this.lbMark = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtqty = new System.Windows.Forms.TextBox();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnXacnhan = new System.Windows.Forms.Button();
            this.nbrLine = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dtpPlan = new Bunifu.UI.WinForms.BunifuDatePicker();
            this.bunifuSeparator1 = new Bunifu.UI.WinForms.BunifuSeparator();
            this.save = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbrLine)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvView
            // 
            this.dgvView.AllowCustomTheming = true;
            this.dgvView.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgvView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvView.ColumnHeadersHeight = 30;
            this.dgvView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stt,
            this.date,
            this.LINE,
            this.Qty,
            this.Pcb,
            this.Panel,
            this.save});
            this.dgvView.CurrentTheme.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.dgvView.CurrentTheme.AlternatingRowsStyle.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvView.CurrentTheme.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvView.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.dgvView.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvView.CurrentTheme.BackColor = System.Drawing.Color.White;
            this.dgvView.CurrentTheme.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.dgvView.CurrentTheme.HeaderStyle.BackColor = System.Drawing.Color.DodgerBlue;
            this.dgvView.CurrentTheme.HeaderStyle.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvView.CurrentTheme.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvView.CurrentTheme.HeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            this.dgvView.CurrentTheme.HeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvView.CurrentTheme.Name = null;
            this.dgvView.CurrentTheme.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvView.CurrentTheme.RowsStyle.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvView.CurrentTheme.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvView.CurrentTheme.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.dgvView.CurrentTheme.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvView.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvView.EnableHeadersVisualStyles = false;
            this.dgvView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.dgvView.HeaderBackColor = System.Drawing.Color.DodgerBlue;
            this.dgvView.HeaderBgColor = System.Drawing.Color.Empty;
            this.dgvView.HeaderForeColor = System.Drawing.Color.White;
            this.dgvView.Location = new System.Drawing.Point(0, 161);
            this.dgvView.Name = "dgvView";
            this.dgvView.RowHeadersVisible = false;
            this.dgvView.RowTemplate.Height = 40;
            this.dgvView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvView.Size = new System.Drawing.Size(616, 190);
            this.dgvView.TabIndex = 0;
            this.dgvView.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
            // 
            // stt
            // 
            this.stt.FillWeight = 50F;
            this.stt.HeaderText = "STT";
            this.stt.Name = "stt";
            // 
            // date
            // 
            this.date.HeaderText = "Ngày sản xuất";
            this.date.Name = "date";
            // 
            // LINE
            // 
            this.LINE.HeaderText = "Line";
            this.LINE.Name = "LINE";
            // 
            // Qty
            // 
            this.Qty.HeaderText = "Pcs kế hoạch";
            this.Qty.Name = "Qty";
            // 
            // Pcb
            // 
            this.Pcb.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.Pcb.BackgroundStyle.Class = "DataGridViewIpAddressBorder";
            this.Pcb.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Pcb.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Pcb.HeaderText = "Pcb khắc";
            this.Pcb.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Pcb.Name = "Pcb";
            this.Pcb.PasswordChar = '\0';
            this.Pcb.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Pcb.Text = "";
            // 
            // Panel
            // 
            this.Panel.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.Panel.BackgroundStyle.Class = "DataGridViewIpAddressBorder";
            this.Panel.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Panel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Panel.HeaderText = "Panel khắc";
            this.Panel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Panel.Name = "Panel";
            this.Panel.PasswordChar = '\0';
            this.Panel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Panel.Text = "";
            // 
            // lbWork
            // 
            this.lbWork.AutoSize = true;
            this.lbWork.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWork.Location = new System.Drawing.Point(12, 19);
            this.lbWork.Name = "lbWork";
            this.lbWork.Size = new System.Drawing.Size(59, 19);
            this.lbWork.TabIndex = 1;
            this.lbWork.Text = "Work : ";
            this.lbWork.Click += new System.EventHandler(this.lbWork_Click);
            // 
            // lbTotal
            // 
            this.lbTotal.AutoSize = true;
            this.lbTotal.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotal.Location = new System.Drawing.Point(239, 18);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(104, 19);
            this.lbTotal.TabIndex = 2;
            this.lbTotal.Text = "Total : 500000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Ngày Sản xuất";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtwork);
            this.panel1.Controls.Add(this.bunifuSeparator1);
            this.panel1.Controls.Add(this.lbMarked);
            this.panel1.Controls.Add(this.lbMark);
            this.panel1.Controls.Add(this.lbTotal);
            this.panel1.Controls.Add(this.lbWork);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(616, 63);
            this.panel1.TabIndex = 10;
            // 
            // txtwork
            // 
            this.txtwork.Location = new System.Drawing.Point(77, 19);
            this.txtwork.Name = "txtwork";
            this.txtwork.Size = new System.Drawing.Size(156, 20);
            this.txtwork.TabIndex = 5;
            this.txtwork.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtwork_KeyDown);
            // 
            // lbMarked
            // 
            this.lbMarked.AutoSize = true;
            this.lbMarked.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMarked.Location = new System.Drawing.Point(430, 19);
            this.lbMarked.Name = "lbMarked";
            this.lbMarked.Size = new System.Drawing.Size(21, 19);
            this.lbMarked.TabIndex = 2;
            this.lbMarked.Text = "...";
            // 
            // lbMark
            // 
            this.lbMark.AutoSize = true;
            this.lbMark.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMark.Location = new System.Drawing.Point(362, 19);
            this.lbMark.Name = "lbMark";
            this.lbMark.Size = new System.Drawing.Size(77, 19);
            this.lbMark.TabIndex = 2;
            this.lbMark.Text = "Marked : ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtqty);
            this.panel2.Controls.Add(this.btnXoa);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.btnXacnhan);
            this.panel2.Controls.Add(this.dtpPlan);
            this.panel2.Controls.Add(this.nbrLine);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 63);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(616, 92);
            this.panel2.TabIndex = 11;
            // 
            // txtqty
            // 
            this.txtqty.Location = new System.Drawing.Point(386, 10);
            this.txtqty.Name = "txtqty";
            this.txtqty.Size = new System.Drawing.Size(80, 20);
            this.txtqty.TabIndex = 11;
            this.txtqty.TextChanged += new System.EventHandler(this.txtqty_TextChanged);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(435, 57);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 23);
            this.btnXoa.TabIndex = 10;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(529, 57);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnXacnhan
            // 
            this.btnXacnhan.Location = new System.Drawing.Point(340, 57);
            this.btnXacnhan.Name = "btnXacnhan";
            this.btnXacnhan.Size = new System.Drawing.Size(75, 23);
            this.btnXacnhan.TabIndex = 10;
            this.btnXacnhan.Text = "Xác nhận";
            this.btnXacnhan.UseVisualStyleBackColor = true;
            this.btnXacnhan.Click += new System.EventHandler(this.btnXacnhan_Click);
            // 
            // nbrLine
            // 
            this.nbrLine.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nbrLine.Location = new System.Drawing.Point(243, 10);
            this.nbrLine.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nbrLine.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nbrLine.Name = "nbrLine";
            this.nbrLine.Size = new System.Drawing.Size(68, 26);
            this.nbrLine.TabIndex = 5;
            this.nbrLine.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(329, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "số lượng";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(211, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Line";
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.FillWeight = 50F;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::PLAN_MODULE.Properties.Resources.icons8_update_16;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Width = 66;
            // 
            // dtpPlan
            // 
            this.dtpPlan.BackColor = System.Drawing.Color.Transparent;
            this.dtpPlan.BorderRadius = 1;
            this.dtpPlan.Color = System.Drawing.Color.Silver;
            this.dtpPlan.CustomFormat = "yyyy-MM-dd";
            this.dtpPlan.DateBorderThickness = Bunifu.UI.WinForms.BunifuDatePicker.BorderThickness.Thin;
            this.dtpPlan.DateTextAlign = Bunifu.UI.WinForms.BunifuDatePicker.TextAlign.Left;
            this.dtpPlan.DisabledColor = System.Drawing.Color.Gray;
            this.dtpPlan.DisplayWeekNumbers = false;
            this.dtpPlan.DPHeight = 0;
            this.dtpPlan.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dtpPlan.FillDatePicker = false;
            this.dtpPlan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpPlan.ForeColor = System.Drawing.Color.Black;
            this.dtpPlan.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPlan.Icon = ((System.Drawing.Image)(resources.GetObject("dtpPlan.Icon")));
            this.dtpPlan.IconColor = System.Drawing.Color.Gray;
            this.dtpPlan.IconLocation = Bunifu.UI.WinForms.BunifuDatePicker.Indicator.Right;
            this.dtpPlan.LeftTextMargin = 5;
            this.dtpPlan.Location = new System.Drawing.Point(91, 5);
            this.dtpPlan.MinimumSize = new System.Drawing.Size(4, 32);
            this.dtpPlan.Name = "dtpPlan";
            this.dtpPlan.Size = new System.Drawing.Size(103, 32);
            this.dtpPlan.TabIndex = 4;
            // 
            // bunifuSeparator1
            // 
            this.bunifuSeparator1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuSeparator1.BackgroundImage")));
            this.bunifuSeparator1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuSeparator1.DashCap = Bunifu.UI.WinForms.BunifuSeparator.CapStyles.Flat;
            this.bunifuSeparator1.LineColor = System.Drawing.Color.DodgerBlue;
            this.bunifuSeparator1.LineStyle = Bunifu.UI.WinForms.BunifuSeparator.LineStyles.DoubleEdgeFaded;
            this.bunifuSeparator1.LineThickness = 1;
            this.bunifuSeparator1.Location = new System.Drawing.Point(0, 49);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Orientation = Bunifu.UI.WinForms.BunifuSeparator.LineOrientation.Horizontal;
            this.bunifuSeparator1.Size = new System.Drawing.Size(529, 14);
            this.bunifuSeparator1.TabIndex = 4;
            // 
            // save
            // 
            this.save.FillWeight = 50F;
            this.save.HeaderText = "";
            this.save.Image = global::PLAN_MODULE.Properties.Resources.icons8_update_16;
            this.save.Name = "save";
            // 
            // PlanProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(616, 351);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvView);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PlanProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kế hoạch khắc laser";
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbrLine)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.UI.WinForms.BunifuDataGridView dgvView;
        private System.Windows.Forms.Label lbWork;
        private System.Windows.Forms.Label lbTotal;
        private Bunifu.UI.WinForms.BunifuDatePicker dtpPlan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Bunifu.UI.WinForms.BunifuSeparator bunifuSeparator1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.NumericUpDown nbrLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnXacnhan;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lbMark;
        private System.Windows.Forms.Label lbMarked;
        private System.Windows.Forms.TextBox txtwork;
        private System.Windows.Forms.TextBox txtqty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn stt;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn LINE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private DevComponents.DotNetBar.Controls.DataGridViewTextBoxDropDownColumn Pcb;
        private DevComponents.DotNetBar.Controls.DataGridViewTextBoxDropDownColumn Panel;
        private System.Windows.Forms.DataGridViewImageColumn save;
    }
}