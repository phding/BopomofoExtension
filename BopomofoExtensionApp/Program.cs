using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using phding.Bopomofo;
using phding.input;

namespace BopomofoExtensionApp
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

            // Check if already running
            var title = ((AssemblyTitleAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyTitleAttribute))).Title;

            var processCount = Process.GetProcesses().Count(p => p.ProcessName == title);

            if (processCount > 1)
            {
                Console.WriteLine("Already exist");
                return;
            }

            // Install for startup
            //InstallProgramOnStartup(title);

            //Register

            HotKeyManager.RegisterHotKey(Keys.S, KeyModifiers.Alt, KeyModifiers.Control);
            HotKeyManager.HotKeyPressed += HotKeyManager_HotKeyPressed;

           // Application.Run(new Form1());

            // Show the system tray icon.					
            using (ProcessIcon pi = new ProcessIcon())
            {
                pi.Display();

                // Make sure the application runs!
                Application.Run();
            }
        }
        private static void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            if (BopomofoRegistry.IsSimplifiedEnable())
            {
                Console.WriteLine("Disable");
                BopomofoRegistry.EnableSimplified(false);
            }
            else
            {
                Console.WriteLine("Enable");
                BopomofoRegistry.EnableSimplified(true);
            }
        }
    }
}
