namespace SchemaX.SIS
{
    partial class SplashForm
    {
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashForm));
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblStatus = new Label();
            progressBar = new ProgressBar();
            lblFooter = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            picLogo = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 83);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(480, 59);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "SchemaX SIS";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(201, 214, 223);
            lblSubtitle.Location = new Point(0, 131);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(480, 30);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Student Information System";
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            lblStatus.Font = new Font("Segoe UI", 9.5F);
            lblStatus.ForeColor = Color.FromArgb(201, 214, 223);
            lblStatus.Location = new Point(0, 178);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(480, 22);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Starting...";
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(109, 203);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(263, 10);
            progressBar.TabIndex = 6;
            // 
            // lblFooter
            // 
            lblFooter.Font = new Font("Segoe UI", 6.5F);
            lblFooter.ForeColor = Color.FromArgb(201, 214, 223);
            lblFooter.Location = new Point(0, 252);
            lblFooter.Name = "lblFooter";
            lblFooter.Size = new Size(480, 20);
            lblFooter.TabIndex = 4;
            lblFooter.Text = "Team SchemaX";
            lblFooter.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            timer1.Interval = 30;
            timer1.Tick += timer1_Tick;
            // 
            // picLogo
            // 
            picLogo.Image = (Image)resources.GetObject("picLogo.Image");
            picLogo.Location = new Point(198, 25);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(80, 68);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 5;
            picLogo.TabStop = false;
            // 
            // SplashForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(47, 72, 88);
            ClientSize = new Size(480, 280);
            Controls.Add(picLogo);
            Controls.Add(lblFooter);
            Controls.Add(progressBar);
            Controls.Add(lblStatus);
            Controls.Add(lblSubtitle);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SplashForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SplashForm";
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblStatus;
        private ProgressBar progressBar;
        private Label lblFooter;
        private System.Windows.Forms.Timer timer1;
        private PictureBox picLogo;
    }
}