using System;
namespace 第十一組程式設計期末報告
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
            Form2 startForm = new Form2();
            if (startForm.ShowDialog() == DialogResult.OK)
            {
                int difficulty = startForm.getDifficulty; 
                Application.Run(new Form1(difficulty));
            }
        }
    }
}
