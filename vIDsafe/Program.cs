using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vIDsafe
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
#pragma warning disable IDE1006 // Naming Styles
        static void Main()
#pragma warning restore IDE1006 // Naming Styles
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new vIDsafe());
        }
    }
}
