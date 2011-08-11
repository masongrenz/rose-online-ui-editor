using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Rose_online_UI_Editor.Forms;

namespace Rose_online_UI_Editor
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
