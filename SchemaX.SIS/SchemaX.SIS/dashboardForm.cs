using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SchemaX.SIS.Data;
using SchemaX.SIS.Helpers;

namespace SchemaX.SIS
{
    public partial class dashboardForm : Form
    {
        // set up repository for student data access
        private readonly StudentRepository _studentRepository = new StudentRepository();

        public dashboardForm()
        {
            InitializeComponent();
            Load += (s, e) => RefreshStats();
        }

        private void RefreshStats()
        {
            try
            {
                var repo = new DashboardRepository();
                var counts = repo.GetCounts();
                var perYear = repo.GetStudentsPerYear();

                string years = perYear.Count == 0
                    ? "none"
                    : string.Join(", ", perYear.Select(y => $"Year {y.Key}: {y.Value}"));

                labelDashboardInfoMain.Text =
                    $"Students: {counts.Students}   Subjects: {counts.Subjects}   " +
                    $"Enrollments: {counts.Enrollments} ({counts.UngradedEnrollments} ungraded)\n" +
                    $"By year - {years}";
                labelDashboardStatus.Text = "Connected to database.";
            }
            catch (Exception ex)
            {
                labelDashboardStatus.Text = "Database unavailable.";
                ExceptionHandler.Handle(ex, "Could not load dashboard statistics. Check appsettings.json and that MySQL is running.");
            }
        }

        private void btnEnrolledStd_Click(object sender, EventArgs e)
        {
            var f = new studentForm();
            f.ShowDialog();   // blocks the dashboard until closed
            RefreshStats();   // counts may have changed
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); // close program
        }
    }
}
