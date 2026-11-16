using ScottPlot.WinForms;
using SchemaX.SIS.Data;
using SchemaX.SIS.Helpers;
using SchemaX.SIS.Models;

namespace SchemaX.SIS
{
    public partial class dashboardForm : Form
    {
        private Panel? _pnlBreakdown;
        private FormsPlot? _yearPlot;
        private FormsPlot? _programPlot;

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

                if (_pnlBreakdown == null)
                {
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

                    _yearPlot = new FormsPlot
                    {
                        Location = new Point(0, 8),
                        Size = new Size(268, 180)
                    };

                    _programPlot = new FormsPlot
                    {
                        Location = new Point(280, 8),
                        Size = new Size(268, 180)
                    };

                    _pnlBreakdown.Controls.Add(_yearPlot);
                    _pnlBreakdown.Controls.Add(_programPlot);
                }

                RenderCharts(perYear, perProgram);

                lblStatus.Text = "Connected to database.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Database unavailable.";
                ExceptionHandler.Handle(ex,
                    "Could not load dashboard statistics. Check appsettings.json and that MySQL is running.");
            }
        }

        private void RenderCharts(Dictionary<int, int> perYear, Dictionary<string, int> perProgram)
        {
            if (_yearPlot == null || _programPlot == null) return;

            string[] paletteHex = ["#33658A", "#2A9D8F", "#2F4858", "#E67E22"];

            // --- CHART 1: Bar Chart (Students per Year Level) ---
            _yearPlot.Plot.Clear();
            _yearPlot.Plot.Title("Students per Year Level", size: 12);

            if (perYear.Count == 0)
            {
                _yearPlot.Plot.Add.Annotation("No students yet");
            }
            else
            {
                var bars = new List<ScottPlot.Bar>();
                var ticks = new List<ScottPlot.Tick>();
                int i = 0;

                foreach (var kv in perYear)
                {
                    bars.Add(new ScottPlot.Bar
                    {
                        Position = i,
                        Value = kv.Value,
                        FillColor = ScottPlot.Color.FromHex(paletteHex[i % paletteHex.Length]),
                        Label = kv.Value.ToString()
                    });
                    ticks.Add(new ScottPlot.Tick(i, $"Yr {kv.Key}"));
                    i++;
                }

                var barPlot = _yearPlot.Plot.Add.Bars(bars);
                barPlot.ValueLabelStyle.IsVisible = true;
                barPlot.ValueLabelStyle.FontSize = 10;

                _yearPlot.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks.ToArray());
                _yearPlot.Plot.Axes.Bottom.MajorTickStyle.Length = 0;
                _yearPlot.Plot.Axes.Margins(bottom: 0, top: 0.25);
            }
            _yearPlot.Plot.Axes.AutoScale();
            _yearPlot.Refresh();

            // --- CHART 2: Pie Chart (Students per Program) ---
            _programPlot.Plot.Clear();
            _programPlot.Plot.Title("Students per Program", size: 12);

            if (perProgram.Count == 0)
            {
                _programPlot.Plot.Add.Annotation("No students yet");
            }
            else
            {
                var slices = new List<ScottPlot.PieSlice>();
                int i = 0;

                foreach (var kv in perProgram)
                {
                    slices.Add(new ScottPlot.PieSlice
                    {
                        Value = kv.Value,
                        Label = $"{kv.Key} ({kv.Value})",
                        FillColor = ScottPlot.Color.FromHex(paletteHex[i % paletteHex.Length])
                    });
                    i++;
                }

                var pie = _programPlot.Plot.Add.Pie(slices);
                pie.ExplodeFraction = 0.05;
                _programPlot.Plot.ShowLegend();
                _programPlot.Plot.Axes.Frameless();
                _programPlot.Plot.HideGrid();
            }
            _programPlot.Plot.Axes.AutoScale();
            _programPlot.Refresh();
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