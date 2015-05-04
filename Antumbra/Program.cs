﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Antumbra.Glow.Controller;

namespace Antumbra.Glow
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, "Global\\" + appGuid)) {
                if (!mutex.WaitOne(0, false)) {
                    MessageBox.Show("Instance already running");
                    return;
                }
                if (Environment.OSVersion.Version.Major >= 6)
                    SetProcessDPIAware();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                ToolbarIconController controller = new ToolbarIconController();
                if (!controller.failed)//did setup fail?
                    Application.Run();//start independent of form
            }
        }

        private static string appGuid = "5e20e0ce-ca88-4e48-b4da-a5de166f5a3d";

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
