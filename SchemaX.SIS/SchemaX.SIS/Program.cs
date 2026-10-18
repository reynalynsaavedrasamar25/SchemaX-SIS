namespace SchemaX.SIS
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Configure exception handling before creating any controls
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) => Helpers.ExceptionHandler.Handle(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                try { Helpers.ErrorLog.LogError((Exception)e.ExceptionObject); } catch { }
            };

            Application.Run(new dashboardForm());
        }

    }
}