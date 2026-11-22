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
    public partial class SplashForm : Form
    {
        private int _progress;
        private int _holdTicks;
        private bool? _db0k;
        public SplashForm()
        {
            InitializeComponent();
            Opacity = 0;
            Shown += async (s, e) =>
            {
                timer1.Start();
                _db0k = await Task.Run(CheckDatabase);
            };
        }

        private static bool CheckDatabase()
        {
            try
            {
                using var con = Db.Open();
                return true;
            }
            catch (Exception ex)
            {
                try { ErrorLog.LogError(ex); } catch { }
                return false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Opacity < 1) Opacity = Math.Min(1, Opacity + 0.08);
            int cap = _db0k == null ? 70 : 100;
            if (_progress < cap) _progress += 2;
            progressBar.Value = _progress;

            lblStatus.Text =
                _progress >= 100 ? (_db0k == false ? "Opening without databse..." : "Ready!") :
                _progress < 30 ? "Starting SchemaX SIS..." :
                _db0k == null ? "Connecting to database..." :
                _db0k == true ? "Loading student records..." :
                                "Database not connected...";

            if (_progress >= 100 && ++_holdTicks >= 8)
            {
                timer1.Stop();
                Close();
            }
        }

    }
}
