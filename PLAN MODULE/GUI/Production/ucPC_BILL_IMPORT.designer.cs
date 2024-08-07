
namespace PLAN_MODULE
{
    partial class ucPC_BILL_IMPORT
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
            this.components = new System.ComponentModel.Container();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRefesh = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvViewBillNumber = new System.Windows.Forms.DataGridView();
            this.did = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stogeUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subunit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.op = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbBillNumber = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvListBill = new System.Windows.Forms.DataGridView();
            this.bill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.timerLoadBIll = new System.Windows.Forms.Timer(this.components);
            this.bgwkUpfile = new System.ComponentModel.BackgroundWorker();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.label2 = new System.Windows.Forms.Label();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViewBillNumber)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListBill)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.panelEx4.SuspendLayout();
            this.panelEx5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnRefesh);
            this.panel2.Controls.Add(this.btnLoad);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Location = new System.Drawing.Point(477, 118);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(370, 51);
            this.panel2.TabIndex = 108;
            // 
            // btnRefesh
            // 
            this.btnRefesh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnRefesh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefesh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefesh.Location = new System.Drawing.Point(115, 8);
            this.btnRefesh.Name = "btnRefesh";
            this.btnRefesh.Size = new System.Drawing.Size(84, 33);
            this.btnRefesh.TabIndex = 106;
            this.btnRefesh.Text = "Refesh";
            this.btnRefesh.UseVisualStyleBackColor = false;
            this.btnRefesh.Click += new System.EventHandler(this.btnRefesh_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.Color.PaleGreen;
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLoad.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Location = new System.Drawing.Point(3, 8);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(97, 33);
            this.btnLoad.TabIndex = 106;
            this.btnLoad.Text = "LoadFIle";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Red;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(259, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 33);
            this.btnCancel.TabIndex = 106;
            this.btnCancel.Text = "Cancel Bill";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgvViewBillNumber
            // 
            this.dgvViewBillNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvViewBillNumber.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvViewBillNumber.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvViewBillNumber.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvViewBillNumber.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.did,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.stogeUnit,
            this.subunit,
            this.op,
            this.dataGridViewTextBoxColumn1});
            this.dgvViewBillNumber.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvViewBillNumber.Location = new System.Drawing.Point(183, 175);
            this.dgvViewBillNumber.Name = "dgvViewBillNumber";
            this.dgvViewBillNumber.Size = new System.Drawing.Size(664, 328);
            this.dgvViewBillNumber.TabIndex = 105;
            // 
            // did
            // 
            this.did.DataPropertyName = "DID";
            this.did.FillWeight = 131.8333F;
            this.did.HeaderText = "DID";
            this.did.Name = "did";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "WORK_ID";
            this.dataGridViewTextBoxColumn3.FillWeight = 117.4885F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Work ID";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "PART_NUMBER";
            this.dataGridViewTextBoxColumn4.FillWeight = 101.5228F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Part Number";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "QTY";
            this.dataGridViewTextBoxColumn5.FillWeight = 129.3924F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Số Lượng";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // stogeUnit
            // 
            this.stogeUnit.DataPropertyName = "STORE_UNIT";
            this.stogeUnit.FillWeight = 129.3924F;
            this.stogeUnit.HeaderText = "Stoge Unit";
            this.stogeUnit.Name = "stogeUnit";
            // 
            // subunit
            // 
            this.subunit.DataPropertyName = "SUB_UNIT";
            this.subunit.FillWeight = 129.3924F;
            this.subunit.HeaderText = "Sub Unit";
            this.subunit.Name = "subunit";
            // 
            // op
            // 
            this.op.DataPropertyName = "PC_OP";
            this.op.FillWeight = 129.3924F;
            this.op.HeaderText = "Op";
            this.op.Name = "op";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "NOTE";
            this.dataGridViewTextBoxColumn1.FillWeight = 131.5856F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Ghi Chú";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(0, 0);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(852, 23);
            this.progressBar.TabIndex = 112;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lbBillNumber);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(185, 118);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(286, 49);
            this.panel3.TabIndex = 111;
            // 
            // lbBillNumber
            // 
            this.lbBillNumber.AutoSize = true;
            this.lbBillNumber.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBillNumber.Location = new System.Drawing.Point(108, 15);
            this.lbBillNumber.Name = "lbBillNumber";
            this.lbBillNumber.Size = new System.Drawing.Size(22, 15);
            this.lbBillNumber.TabIndex = 1;
            this.lbBillNumber.Text = ".....";
            this.lbBillNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bill Number :";
            // 
            // dgvListBill
            // 
            this.dgvListBill.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvListBill.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvListBill.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvListBill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListBill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.bill});
            this.dgvListBill.Location = new System.Drawing.Point(3, 118);
            this.dgvListBill.Name = "dgvListBill";
            this.dgvListBill.Size = new System.Drawing.Size(174, 385);
            this.dgvListBill.TabIndex = 110;
            this.dgvListBill.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListBill_CellContentClick);
            // 
            // bill
            // 
            this.bill.DataPropertyName = "BILL_NUMBER";
            this.bill.HeaderText = "Số phiếu";
            this.bill.Name = "bill";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(181, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 19);
            this.label7.TabIndex = 108;
            this.label7.Text = "Thông tin xác nhận";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 19);
            this.label6.TabIndex = 108;
            this.label6.Text = "Danh sách phiếu";
            // 
            // timerLoadBIll
            // 
            this.timerLoadBIll.Interval = 60000;
            this.timerLoadBIll.Tick += new System.EventHandler(this.timerLoadBIll_Tick);
            // 
            // bgwkUpfile
            // 
            this.bgwkUpfile.WorkerReportsProgress = true;
            this.bgwkUpfile.WorkerSupportsCancellation = true;
            this.bgwkUpfile.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwkUpfile_DoWork);
            this.bgwkUpfile.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwkUpfile_ProgressChanged);
            this.bgwkUpfile.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwkUpfile_RunWorkerCompleted);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx4);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(852, 567);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 97;
            this.panelEx1.Text = "panelEx1";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.label2);
            this.panelEx2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(852, 35);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 97;
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.panelEx5);
            this.panelEx4.Controls.Add(this.panel2);
            this.panelEx4.Controls.Add(this.dgvViewBillNumber);
            this.panelEx4.Controls.Add(this.dgvListBill);
            this.panelEx4.Controls.Add(this.label6);
            this.panelEx4.Controls.Add(this.panel3);
            this.panelEx4.Controls.Add(this.label7);
            this.panelEx4.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Location = new System.Drawing.Point(0, 35);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(852, 532);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 108;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Phiếu xuất trả linh kiện";
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.progressBar);
            this.panelEx5.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx5.Location = new System.Drawing.Point(0, 509);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(852, 23);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 113;
            // 
            // ucPC_BILL_IMPORT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ucPC_BILL_IMPORT";
            this.Size = new System.Drawing.Size(852, 567);
            this.Load += new System.EventHandler(this.PC_BILL_IMPORT_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvViewBillNumber)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListBill)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.panelEx4.ResumeLayout(false);
            this.panelEx4.PerformLayout();
            this.panelEx5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRefesh;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvViewBillNumber;
        private System.Windows.Forms.DataGridView dgvListBill;
        private System.Windows.Forms.DataGridViewTextBoxColumn bill;
        private System.Windows.Forms.DataGridViewTextBoxColumn did;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn stogeUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn subunit;
        private System.Windows.Forms.DataGridViewTextBoxColumn op;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.Timer timerLoadBIll;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbBillNumber;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker bgwkUpfile;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.PanelEx panelEx5;
    }
}
