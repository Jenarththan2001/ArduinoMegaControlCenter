using System;
using System.Windows.Forms;

namespace TelerikWinFormsApp1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Load saved settings (schools, duration, and also last COM/baud)
            QuizConfig.Load();

            // 1) Start with CheckForm modally
            using (var check = new CheckForm())
            {
                var result = check.ShowDialog();   // user connects and clicks “Go to Main”
                if (result != DialogResult.OK)
                {
                    // user closed/cancelled => just exit app
                    return;
                }
            }

            // 2) Launch MainForm (now it will use saved COM/baud)
            Application.Run(new MainForm());
        }
    }
}
