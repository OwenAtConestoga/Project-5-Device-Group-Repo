using System;
using System.Windows.Forms;

namespace ProjectV
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); // Ensure Form1 is properly referenced here
        }
    }
}
