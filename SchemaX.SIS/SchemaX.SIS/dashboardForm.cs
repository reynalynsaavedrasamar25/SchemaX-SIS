using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SchemaX.SIS
{
    public partial class dashboardForm : Form
    {
        public dashboardForm()
        {
            InitializeComponent();
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
