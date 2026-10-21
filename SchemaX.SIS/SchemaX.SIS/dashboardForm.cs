using SchemaX.SIS.Data;
using SchemaX.SIS.Helpers;
using SchemaX.SIS.Models;

namespace SchemaX.SIS
{
    public partial class dashboardForm : Form
    {
        private Panel? _pnlBreakdown;
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
            btnHelp.BackColor = Color.White;
            btnHelp.ForeColor = ThemeColors.Primary;
            btnHelp.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnHelp.Cursor = Cursors.Hand;
            var circle = new System.Drawing.Drawing2D.GraphicsPath();
            circle.AddEllipse(0, 0, btnHelp.Width, btnHelp.Height);
            btnHelp.Region = new Region(circle); //para bilog

            pnlSidebar.BackColor = ThemeColors.GridAlternateRow;


            pnlUserInfo.BackColor = ThemeColors.Surface;
            lblUserName.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            pnlSidebar.BackColor = Color.FromArgb(226, 234, 248);

            ButtonStyler.Apply(btnUser, "\uE77B", ButtonKind.Menu);
            ButtonStyler.Apply(btnEnrolledStd, "\uE716", ButtonKind.Menu);
            ButtonStyler.Apply(btnStudentRecord, "\uE8A5", ButtonKind.Menu);
            ButtonStyler.Apply(btnExit, "\uE7E8", ButtonKind.Danger);
            ButtonStyler.Apply(btnLogout, "", ButtonKind.Solid);

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

                flowStats.SuspendLayout();
                foreach (Control old in flowStats.Controls.Cast<Control>().ToList())
                    old.Dispose();
                flowStats.Controls.Clear();
                flowStats.WrapContents = false;
                flowStats.Padding = Padding.Empty;
                flowStats.Location = new Point(11, 12);
                flowStats.Size = new Size(548, 96);
                flowStats.Controls.Add(MakeCard("Students", counts.Students, ThemeColors.Accent));
                flowStats.Controls.Add(MakeCard("Subjects", counts.Subjects, Color.FromArgb(42, 157, 143)));
                flowStats.Controls.Add(MakeCard("Enrollments", counts.Enrollments, ThemeColors.Primary));
                flowStats.Controls.Add(MakeCard("Ungraded", counts.UngradedEnrollments, Color.FromArgb(230, 126, 34)));
                flowStats.ResumeLayout();

                lblBreakdown.Visible = false;
                _pnlBreakdown?.Dispose();
                _pnlBreakdown = new Panel
                {
                    Location = new Point(11, 118),
                    Size = new Size(548, 190)
                };
                pnlOverview.Controls.Add(_pnlBreakdown);

                _pnlBreakdown.Controls.Add(new Panel
                {
                    BackColor = Color.Gainsboro,
                    Location = new Point(0, 0),
                    Size = new Size(548, 1)
                });

                var years = perYear
                    .Select(y => new KeyValuePair<string, int>("Year " + y.Key, y.Value)).ToList();
                var programs = perProgram
                    .Select(p => new KeyValuePair<string, int>(p.Key, p.Value)).ToList();

                BuildBars(_pnlBreakdown, "Students per year level", years, 0, ThemeColors.Accent);
                BuildBars(_pnlBreakdown, "Students per program", programs, 286, Color.FromArgb(42, 157, 143));

                lblStatus.Text = "Connected to database.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Database unavailable.";
                ExceptionHandler.Handle(ex,
                    "Could not load dashboard statistics. Check appsettings.json and that MySQL is running.");
            }
        }

        private static Panel MakeCard(string caption, int value, Color accent)
        {
            var card = new Panel
            {
                Size = new Size(128, 96),
                BackColor = ThemeColors.Background,
                Margin = new Padding(0, 0, 12, 0)
            };
            card.Controls.Add(new Panel
            {
                BackColor = accent,
                Dock = DockStyle.Top,
                Height = 5
            });
            card.Controls.Add(new Label
            {
                Text = value.ToString(),
                Font = new Font("Segoe UI", 26F, FontStyle.Bold),
                ForeColor = accent,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 12),
                Size = new Size(128, 48)
            });
            card.Controls.Add(new Label
            {
                Text = caption,
                Font = new Font("Segoe UI", 10F),
                ForeColor = ThemeColors.TextDark,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 62),
                Size = new Size(128, 24)
            });
            return card;
        }

        private static void BuildBars(Panel parent, string title,
            List<KeyValuePair<string, int>> rows, int x, Color barColor)
        {
            parent.Controls.Add(new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = ThemeColors.Primary,
                Location = new Point(x, 12),
                Size = new Size(262, 26)
            });

            if (rows.Count == 0)
            {
                parent.Controls.Add(new Label
                {
                    Text = "No students yet",
                    Font = new Font("Segoe UI", 10F),
                    ForeColor = Color.Gray,
                    Location = new Point(x, 44),
                    Size = new Size(262, 24)
                });
                return;
            }

            int max = rows.Max(r => r.Value);
            int y = 44;
            foreach (var r in rows)
            {
                parent.Controls.Add(new Label
                {
                    Text = r.Key,
                    Font = new Font("Segoe UI", 10F),
                    ForeColor = ThemeColors.TextDark,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Location = new Point(x, y),
                    Size = new Size(74, 22)
                });

                int barWidth = max == 0 ? 0 : Math.Max(6, (int)(130.0 * r.Value / max));
                parent.Controls.Add(new Panel
                {
                    BackColor = barColor,
                    Location = new Point(x + 78, y + 4),
                    Size = new Size(barWidth, 14)
                });

                parent.Controls.Add(new Label
                {
                    Text = r.Value.ToString(),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = ThemeColors.TextDark,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Location = new Point(x + 78 + barWidth + 6, y),
                    Size = new Size(40, 22)
                });
                y += 26;
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
            ButtonStyler.SetActive(btnUser, open);
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
