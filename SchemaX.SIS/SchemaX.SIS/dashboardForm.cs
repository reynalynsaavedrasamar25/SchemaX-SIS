using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SchemaX.SIS.Data;

namespace SchemaX.SIS
{
    public partial class dashboardForm : Form
    {
        // set up repository for student data access
        private readonly StudentRepository _studentRepository = new StudentRepository();

        public dashboardForm()
        {
            InitializeComponent();
            int count = _studentRepository.GetAll().Count; // Load all students when the dashboard is initialized
            labelDashboardInfoMain.Text = $"Total: {count}";
        }

        private void btnEnrolledStd_Click(object sender, EventArgs e)
        {
            var f = new studentForm();
            f.ShowDialog();   // blocks the dashboard until closed
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); // close program
        }
    }
}
