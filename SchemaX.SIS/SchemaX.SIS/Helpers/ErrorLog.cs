using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace SchemaX.SIS.Helpers
{
    public static class ErrorLog
    {
        public static void LogError(Exception ex)
        {
            string logFolder = Path.Combine(
                AppContext.BaseDirectory,
                "logs"
            );

            Directory.CreateDirectory(logFolder);

            string logFile = Path.Combine(
                logFolder,
                "errors.txt"
            );

            string errorDetails =
                $"Date: {DateTime.Now}\n" +
                $"Error Type: {ex.GetType().Name}\n" +
                $"Message: {ex.Message}\n" +
                $"Stack Trace: {ex.StackTrace}\n" +
                "----------------------------------------\n";

            File.AppendAllText(logFile, errorDetails);
        }
    }
}
