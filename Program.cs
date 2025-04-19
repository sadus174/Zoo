namespace Zoo
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
            Application.Run(new Main());
        }
    }

    public static class Setting
    {
        public static string server = "server=10.80.1.98;port=3306;user=zoo;database=lik;password=293fh290fg9(#9fh";
    }

    public static class AuthClass
    {
        public static string Fio;        
    }
}