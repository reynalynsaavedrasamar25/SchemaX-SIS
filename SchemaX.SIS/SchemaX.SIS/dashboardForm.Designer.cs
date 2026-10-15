namespace SchemaX.SIS
{
    partial class dashboardForm
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
            btnEnrolledStd = new Button();
            pnlDashboardTitle = new Panel();
            labelDashboardTitle = new Label();
            pnlDashboardInfo = new Panel();
            labelDashboardInfoMain = new Label();
            labelDashboardMain = new Label();
            pnlDashboardBtns = new Panel();
            btnExit = new Button();
            btnAttendance = new Button();
            pnlDashboardStatus = new Panel();
            pnlDashboardTitle.SuspendLayout();
            pnlDashboardInfo.SuspendLayout();
            pnlDashboardBtns.SuspendLayout();
            SuspendLayout();
            // 
            // btnEnrolledStd
            // 
            btnEnrolledStd.Location = new Point(52, 41);
            btnEnrolledStd.Name = "btnEnrolledStd";
            btnEnrolledStd.Size = new Size(132, 43);
            btnEnrolledStd.TabIndex = 0;
            btnEnrolledStd.Text = "Enrolled Students";
            btnEnrolledStd.UseVisualStyleBackColor = true;
            btnEnrolledStd.Click += btnEnrolledStd_Click;
            // 
            // pnlDashboardTitle
            // 
            pnlDashboardTitle.Controls.Add(labelDashboardTitle);
            pnlDashboardTitle.Dock = DockStyle.Top;
            pnlDashboardTitle.Location = new Point(0, 0);
            pnlDashboardTitle.Name = "pnlDashboardTitle";
            pnlDashboardTitle.Size = new Size(584, 81);
            pnlDashboardTitle.TabIndex = 1;
            // 
            // labelDashboardTitle
            // 
            labelDashboardTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelDashboardTitle.AutoSize = true;
            labelDashboardTitle.Font = new Font("Segoe UI", 14F);
            labelDashboardTitle.Location = new Point(229, 28);
            labelDashboardTitle.Name = "labelDashboardTitle";
            labelDashboardTitle.Size = new Size(119, 25);
            labelDashboardTitle.TabIndex = 0;
            labelDashboardTitle.Text = "SchemaX SIS";
            // 
            // pnlDashboardInfo
            // 
            pnlDashboardInfo.Controls.Add(labelDashboardInfoMain);
            pnlDashboardInfo.Controls.Add(labelDashboardMain);
            pnlDashboardInfo.Dock = DockStyle.Top;
            pnlDashboardInfo.Location = new Point(0, 81);
            pnlDashboardInfo.Name = "pnlDashboardInfo";
            pnlDashboardInfo.Size = new Size(584, 68);
            pnlDashboardInfo.TabIndex = 2;
            // 
            // labelDashboardInfoMain
            // 
            labelDashboardInfoMain.AutoSize = true;
            labelDashboardInfoMain.Location = new Point(12, 39);
            labelDashboardInfoMain.Name = "labelDashboardInfoMain";
            labelDashboardInfoMain.Size = new Size(146, 15);
            labelDashboardInfoMain.TabIndex = 1;
            labelDashboardInfoMain.Text = "Stats breakdown go here...";
            // 
            // labelDashboardMain
            // 
            labelDashboardMain.AutoSize = true;
            labelDashboardMain.Location = new Point(12, 11);
            labelDashboardMain.Name = "labelDashboardMain";
            labelDashboardMain.Size = new Size(155, 15);
            labelDashboardMain.TabIndex = 0;
            labelDashboardMain.Text = "Student Information System";
            // 
            // pnlDashboardBtns
            // 
            pnlDashboardBtns.Controls.Add(btnExit);
            pnlDashboardBtns.Controls.Add(btnAttendance);
            pnlDashboardBtns.Controls.Add(btnEnrolledStd);
            pnlDashboardBtns.Dock = DockStyle.Top;
            pnlDashboardBtns.Location = new Point(0, 149);
            pnlDashboardBtns.Name = "pnlDashboardBtns";
            pnlDashboardBtns.Size = new Size(584, 134);
            pnlDashboardBtns.TabIndex = 3;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(382, 41);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(132, 43);
            btnExit.TabIndex = 2;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnAttendance
            // 
            btnAttendance.Location = new Point(216, 41);
            btnAttendance.Name = "btnAttendance";
            btnAttendance.Size = new Size(132, 43);
            btnAttendance.TabIndex = 1;
            btnAttendance.Text = "Attendance";
            btnAttendance.UseVisualStyleBackColor = true;
            // 
            // pnlDashboardStatus
            // 
            pnlDashboardStatus.Dock = DockStyle.Fill;
            pnlDashboardStatus.Location = new Point(0, 283);
            pnlDashboardStatus.Name = "pnlDashboardStatus";
            pnlDashboardStatus.Size = new Size(584, 28);
            pnlDashboardStatus.TabIndex = 1;
            // 
            // dashboardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 311);
            Controls.Add(pnlDashboardStatus);
            Controls.Add(pnlDashboardBtns);
            Controls.Add(pnlDashboardInfo);
            Controls.Add(pnlDashboardTitle);
            Name = "dashboardForm";
            Text = "SchemaX SIS Dashboard";
            pnlDashboardTitle.ResumeLayout(false);
            pnlDashboardTitle.PerformLayout();
            pnlDashboardInfo.ResumeLayout(false);
            pnlDashboardInfo.PerformLayout();
            pnlDashboardBtns.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnEnrolledStd;
        private Panel pnlDashboardTitle;
        private Label labelDashboardTitle;
        private Panel pnlDashboardInfo;
        private Label labelDashboardInfoMain;
        private Label labelDashboardMain;
        private Panel pnlDashboardBtns;
        private Button btnExit;
        private Button btnAttendance;
        private Panel pnlDashboardStatus;
    }
}