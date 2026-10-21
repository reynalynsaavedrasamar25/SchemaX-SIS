namespace SchemaX.SIS
{
    partial class dashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dashboardForm));
            pnlHeader = new Panel();
            pictureBox1 = new PictureBox();
            lblAppName = new Label();
            btnHelp = new Button();
            pnlSidebar = new Panel();
            btnUser = new Button();
            btnEnrolledStd = new Button();
            btnStudentRecord = new Button();
            pnlUserInfo = new Panel();
            lblUserName = new Label();
            lblUserUsername = new Label();
            lblUserEmail = new Label();
            btnLogout = new Button();
            btnExit = new Button();
            lblHello = new Label();
            lblOverviewTitle = new Label();
            pnlOverview = new Panel();
            flowStats = new FlowLayoutPanel();
            lblBreakdown = new Label();
            lblStatus = new Label();
            label1 = new Label();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlSidebar.SuspendLayout();
            pnlUserInfo.SuspendLayout();
            pnlOverview.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(pictureBox1);
            pnlHeader.Controls.Add(lblAppName);
            pnlHeader.Controls.Add(btnHelp);
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(900, 70);
            pnlHeader.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(18, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(82, 70);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.Location = new Point(94, 13);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(74, 15);
            lblAppName.TabIndex = 0;
            lblAppName.Text = "SchemaX SIS";
            // 
            // btnHelp
            // 
            btnHelp.Location = new Point(836, 13);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(44, 44);
            btnHelp.TabIndex = 1;
            btnHelp.Text = "?";
            btnHelp.UseVisualStyleBackColor = true;
            // 
            // pnlSidebar
            // 
            pnlSidebar.Controls.Add(btnUser);
            pnlSidebar.Controls.Add(btnEnrolledStd);
            pnlSidebar.Controls.Add(btnStudentRecord);
            pnlSidebar.Controls.Add(pnlUserInfo);
            pnlSidebar.Controls.Add(btnExit);
            pnlSidebar.Location = new Point(0, 70);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(230, 490);
            pnlSidebar.TabIndex = 1;
            // 
            // btnUser
            // 
            btnUser.Location = new Point(10, 15);
            btnUser.Name = "btnUser";
            btnUser.Size = new Size(210, 60);
            btnUser.TabIndex = 0;
            btnUser.Text = "User";
            btnUser.UseVisualStyleBackColor = true;
            // 
            // btnEnrolledStd
            // 
            btnEnrolledStd.Location = new Point(10, 90);
            btnEnrolledStd.Name = "btnEnrolledStd";
            btnEnrolledStd.Size = new Size(210, 60);
            btnEnrolledStd.TabIndex = 1;
            btnEnrolledStd.Text = "Manage Students";
            btnEnrolledStd.UseVisualStyleBackColor = true;
            // 
            // btnStudentRecord
            // 
            btnStudentRecord.Location = new Point(10, 165);
            btnStudentRecord.Name = "btnStudentRecord";
            btnStudentRecord.Size = new Size(210, 60);
            btnStudentRecord.TabIndex = 2;
            btnStudentRecord.Text = "Student Record";
            btnStudentRecord.UseVisualStyleBackColor = true;
            // 
            // pnlUserInfo
            // 
            pnlUserInfo.Controls.Add(lblUserName);
            pnlUserInfo.Controls.Add(lblUserUsername);
            pnlUserInfo.Controls.Add(lblUserEmail);
            pnlUserInfo.Controls.Add(btnLogout);
            pnlUserInfo.Location = new Point(10, 240);
            pnlUserInfo.Name = "pnlUserInfo";
            pnlUserInfo.Size = new Size(210, 150);
            pnlUserInfo.TabIndex = 3;
            // 
            // lblUserName
            // 
            lblUserName.AutoEllipsis = true;
            lblUserName.Location = new Point(8, 8);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(192, 24);
            lblUserName.TabIndex = 0;
            lblUserName.Text = "Full Name";
            // 
            // lblUserUsername
            // 
            lblUserUsername.AutoEllipsis = true;
            lblUserUsername.Location = new Point(8, 38);
            lblUserUsername.Name = "lblUserUsername";
            lblUserUsername.Size = new Size(192, 22);
            lblUserUsername.TabIndex = 1;
            lblUserUsername.Text = "Username";
            // 
            // lblUserEmail
            // 
            lblUserEmail.AutoEllipsis = true;
            lblUserEmail.Location = new Point(8, 64);
            lblUserEmail.Name = "lblUserEmail";
            lblUserEmail.Size = new Size(192, 36);
            lblUserEmail.TabIndex = 2;
            lblUserEmail.Text = "Email";
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(8, 106);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(192, 34);
            btnLogout.TabIndex = 3;
            btnLogout.Text = "Log out";
            btnLogout.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(67, 447);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(91, 26);
            btnExit.TabIndex = 4;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            // 
            // lblHello
            // 
            lblHello.Location = new Point(260, 85);
            lblHello.Name = "lblHello";
            lblHello.Size = new Size(610, 43);
            lblHello.TabIndex = 2;
            lblHello.Text = "Hello!";
            lblHello.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblOverviewTitle
            // 
            lblOverviewTitle.Location = new Point(260, 150);
            lblOverviewTitle.Name = "lblOverviewTitle";
            lblOverviewTitle.Size = new Size(610, 35);
            lblOverviewTitle.TabIndex = 3;
            lblOverviewTitle.Text = "Overview";
            lblOverviewTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlOverview
            // 
            pnlOverview.BorderStyle = BorderStyle.FixedSingle;
            pnlOverview.Controls.Add(flowStats);
            pnlOverview.Controls.Add(lblBreakdown);
            pnlOverview.Location = new Point(280, 195);
            pnlOverview.Name = "pnlOverview";
            pnlOverview.Size = new Size(570, 320);
            pnlOverview.TabIndex = 4;
            // 
            // flowStats
            // 
            flowStats.Location = new Point(10, 10);
            flowStats.Name = "flowStats";
            flowStats.Size = new Size(548, 110);
            flowStats.TabIndex = 0;
            // 
            // lblBreakdown
            // 
            lblBreakdown.Location = new Point(14, 135);
            lblBreakdown.Name = "lblBreakdown";
            lblBreakdown.Size = new Size(540, 170);
            lblBreakdown.TabIndex = 1;
            lblBreakdown.Text = "Breakdown";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(280, 528);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(48, 15);
            lblStatus.TabIndex = 5;
            lblStatus.Text = "Status...";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(409, 131);
            label1.Name = "label1";
            label1.Size = new Size(320, 17);
            label1.TabIndex = 6;
            label1.Text = "Welcome to SchemaX Student Information System";
            // 
            // dashboardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 560);
            Controls.Add(label1);
            Controls.Add(lblStatus);
            Controls.Add(pnlOverview);
            Controls.Add(lblOverviewTitle);
            Controls.Add(lblHello);
            Controls.Add(pnlSidebar);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "dashboardForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SchemaX SIS";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlSidebar.ResumeLayout(false);
            pnlUserInfo.ResumeLayout(false);
            pnlOverview.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlHeader;
        private Label lblAppName;
        private Button btnHelp;
        private Panel pnlSidebar;
        private Button btnUser;
        private Button btnEnrolledStd;
        private Button btnStudentRecord;
        private Button btnExit;
        private Panel pnlUserInfo;
        private Label lblUserName;
        private Label lblUserUsername;
        private Label lblUserEmail;
        private Button btnLogout;
        private Label lblHello;
        private Label lblOverviewTitle;
        private Panel pnlOverview;
        private FlowLayoutPanel flowStats;
        private Label lblBreakdown;
        private Label lblStatus;
        private Label label1;
        private PictureBox pictureBox1;
    }
}
