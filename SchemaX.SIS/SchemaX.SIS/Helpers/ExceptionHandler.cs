using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace SchemaX.SIS.Helpers
{
    public static class ExceptionHandler
    {
        public static void Handle(Exception ex, string userMessage = "Something went wrong. Please try again.")
        {
            try
            {
                ErrorLog.LogError(ex);
            }
            catch
            {
                // Logging failure should not crash the application.
            }

            MessageBox.Show(
                userMessage,
                "Application Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}
