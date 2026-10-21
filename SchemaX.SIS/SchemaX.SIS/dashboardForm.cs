using SchemaX.SIS.Data;
using SchemaX.SIS.Helpers;
using SchemaX.SIS.Models;

namespace SchemaX.SIS
{
    public partial class dashboardForm : Form
    {
        public dashboardForm()
        {
            InitializeComponent();
            StyleControls();
            ShowUserInfo();

            btnUser.Click += btnUser_Click;
            btnEnrolledStd.Click += btnEnrolledStd_Click;
            btnStudentRecord.Click += btnStudentRecord_Click;
            btnExit.Click += btnExit_Click;
            btnLogout.Click += btnLogout_Click;
            btnHelp.Click += btnHelp_Click;

            Load += (s, e) => RefreshStats();
        }

        private void StyleControls()
        {
            BackColor = ThemeColors.Background;

            pnlHeader.BackColor = ThemeColors.Primary;
            lblAppName.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblAppName.ForeColor = ThemeColors.TextLight;

            btnHelp.FlatStyle = FlatStyle.Flat;
            btnHelp.FlatAppearance.BorderSize = 0;
            btnHelp.BackColor = ThemeColors.Accent;
            btnHelp.ForeColor = ThemeColors.TextLight;
            btnHelp.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnHelp.Cursor = Cursors.Hand;
            var circle = new System.Drawing.Drawing2D.GraphicsPath();
            circle.AddEllipse(0, 0, btnHelp.Width, btnHelp.Height);
            btnHelp.Region = new Region(circle); //para bilog

            pnlSidebar.BackColor = ThemeColors.GridAlternateRow;
            foreach (Button b in new[] { btnUser, btnEnrolledStd, btnStudentRecord, btnExit })
            {
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderColor = ThemeColors.Accent;
                b.BackColor = ThemeColors.Surface;
                b.ForeColor = ThemeColors.TextDark;
                b.Font = new Font("Segoe UI", 12F);
                b.Cursor = Cursors.Hand;
            }

            pnlUserInfo.BackColor = ThemeColors.Surface;
            lblUserName.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.BackColor = ThemeColors.Danger;
            btnLogout.ForeColor = ThemeColors.TextLight;
            btnLogout.Cursor = Cursors.Hand;

            lblHello.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblHello.ForeColor = ThemeColors.Primary;
            lblOverviewTitle.Font = new Font("Segoe UI", 16F);
            lblOverviewTitle.ForeColor = ThemeColors.TextDark;
            pnlOverview.BackColor = ThemeColors.Surface;
            lblBreakdown.Font = new Font("Segoe UI", 11F);
            lblStatus.ForeColor = Color.Gray;

            pnlUserInfo.Location = new Point(10, btnUser.Bottom + 5);
            pnlUserInfo.Visible = false;
        }

        private void ShowUserInfo()
        {
            AppUser? user = Session.CurrentUser;
            string first = user?.FirstName ?? "there";

            lblHello.Text = "Hello, " + first + "!";
            lblUserName.Text = user?.FullName ?? "";
            lblUserUsername.Text = "Username: " + (user?.Username ?? "");
            lblUserEmail.Text = user?.Email ?? "";
        }
        private void RefreshStats()
        {
            try
            {
                var repo = new DashboardRepository();
                var counts = repo.GetCounts();
                var perYear = repo.GetStudentsPerYear();
                var perProgram = repo.GetStudentsPerProgram();

                foreach (Control old in flowStats.Controls.Cast<Control>().ToList())
                    old.Dispose();
                flowStats.Controls.Clear();
                flowStats.Controls.Add(MakeCard("Students", counts.Students));
                flowStats.Controls.Add(MakeCard("Subjects", counts.Subjects));
                flowStats.Controls.Add(MakeCard("Enrollments", counts.Enrollments));
                flowStats.Controls.Add(MakeCard("Ungraded", counts.UngradedEnrollments));

                string years = perYear.Count == 0
                    ? "none yet"
                    : string.Join("    ", perYear.Select(y => "Year " + y.Key + ": " + y.Value));
                string programs = perProgram.Count == 0
                    ? "none yet"
                    : string.Join("    ", perProgram.Select(p => p.Key + ": " + p.Value));

                lblBreakdown.Text =
                    "Students per year level\n" + years + "\n\n" +
                    "Students per program\n" + programs;
                lblStatus.Text = "Connected to database.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Database unavailable.";
                ExceptionHandler.Handle(ex,
                    "Could not load dashboard statistics. Check appsettings.json and that MySQL is running.");
            }
        }

        private static Panel MakeCard(string caption, int value)
        {
            var card = new Panel
            {
                Size = new Size(125, 90),
                BackColor = ThemeColors.Background,
                Margin = new Padding(6)
            };
            card.Controls.Add(new Label
            {
                Text = value.ToString(),
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                ForeColor = ThemeColors.Accent,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 6),
                Size = new Size(125, 50)
            });
            card.Controls.Add(new Label
            {
                Text = caption,
                Font = new Font("Segoe UI", 10F),
                ForeColor = ThemeColors.TextDark,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 58),
                Size = new Size(125, 24)
            });
            return card;
        }

        private void btnUser_Click(object? sender, EventArgs e)
        {
            bool open = !pnlUserInfo.Visible;
            pnlUserInfo.Visible = open;

            int shift = pnlUserInfo.Height + 10;
            int delta = open ? shift : -shift;
            btnEnrolledStd.Top += delta;
            btnStudentRecord.Top += delta;
        }

        private void btnEnrolledStd_Click(object? sender, EventArgs e)
        {
            using var f = new studentForm();
            f.ShowDialog(this);
            RefreshStats();
        }

        private void btnStudentRecord_Click(object? sender, EventArgs e)
        {
            using var f = new studentRecordForm();
            f.ShowDialog(this);
            RefreshStats();
        }

        private void btnLogout_Click(object? sender, EventArgs e)
        {
            var answer = MessageBox.Show("Do you want to log out?", "Log out",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer != DialogResult.Yes) return;

            Session.CurrentUser = null;
            Application.Restart();
        }

        private void btnHelp_Click(object? sender, EventArgs e)
        {
            MessageBox.Show(
                "SchemaX SIS Help\n\n" +
                "User: shows your account details and the Log out button.\n" +
                "Enrolled Students: view, search, add, edit and delete students.\n" +
                "Student Record: view profiles and manage grades.\n" +
                "Exit: closes the program.",
                "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExit_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
