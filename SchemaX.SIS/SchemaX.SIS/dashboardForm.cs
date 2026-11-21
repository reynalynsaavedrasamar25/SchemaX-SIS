using SchemaX.SIS.Data;
using SchemaX.SIS.Helpers;
using SchemaX.SIS.Models;

namespace SchemaX.SIS
{
    public partial class dashboardForm : Form
    {
        private readonly ScottPlot.WinForms.FormsPlot plotProgram = new();
        private readonly Button btnRefresh = new();
        public dashboardForm()
        {
            InitializeComponent();
            StyleControls();
            SetupCharts();
            SetupRefreshButton();
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

            scotPlot.BackColor = BackColor;
        }

        // move scottplot into overview panel and adds a second one
        private void SetupCharts()
        {
            foreach (var chart in new[] { scotPlot, plotProgram })
            {
                chart.BackColor = ThemeColors.Surface;
                pnlOverview.Controls.Add(chart);   // re-parents scotPlot off the form
                chart.BringToFront();
            }
            scotPlot.SetBounds(11, 118, 268, 192);
            plotProgram.SetBounds(291, 118, 268, 192);
        }

        private void SetupRefreshButton()
        {
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Text = "Refresh Dashboard";
            btnRefresh.Size = new Size(160, 30);
            btnRefresh.Location = new Point(pnlOverview.Right - btnRefresh.Width, 520);
            ButtonStyler.Apply(btnRefresh, "", ButtonKind.Solid, ThemeColors.Accent);
            btnRefresh.Click += (s, e) => RefreshStats();
            Controls.Add(btnRefresh);
        }

        // bar chart scottplot
        private static void DrawBars(ScottPlot.WinForms.FormsPlot chart, string title,
            List<KeyValuePair<string, int>> rows, string hexColor)
        {
            var plot = chart.Plot;
            plot.Clear();
            plot.Title(title);

            if (rows.Count == 0)
            {
                plot.Title(title + " (no students yet)");
                chart.Refresh();
                return;
            }

            var fill = ScottPlot.Color.FromHex(hexColor);
            var bars = new List<ScottPlot.Bar>();
            var ticks = new List<ScottPlot.Tick>();

            for (int i = 0; i < rows.Count; i++)
            {
                bars.Add(new ScottPlot.Bar
                {
                    Position = i,
                    Value = rows[i].Value,
                    FillColor = fill,
                    Size = 0.6,
                    Label = rows[i].Value.ToString()   // number shown above the bar
                });
                ticks.Add(new ScottPlot.Tick(i, rows[i].Key));
            }

            plot.Add.Bars(bars);
            plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks.ToArray());
            plot.Axes.Margins(bottom: 0, top: 0.25);
            chart.Refresh();
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

                var years = perYear
                    .Select(y => new KeyValuePair<string, int>("Year " + y.Key, y.Value)).ToList();
                var programs = perProgram
                    .Select(p => new KeyValuePair<string, int>(p.Key, p.Value)).ToList();

                DrawBars(scotPlot, "Students per year level", years, "#33658A");
                DrawBars(plotProgram, "Students per program", programs, "#2A9D8F");

                lblStatus.Text = "Connected to database. Last refreshed " +
                                 DateTime.Now.ToString("h:mm:ss tt") + ".";
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
