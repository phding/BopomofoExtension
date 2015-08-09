using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

using phding.Bopomofo;
using phding.input;

namespace BopomofoExtensionApp
{
    static class Program
    {
        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int SetActiveWindow(IntPtr hwnd);
        [DllImport("user32.dll")]
        public static extern int SetFocus(IntPtr hwnd);


        private static ProcessIcon pi;
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
            using (pi = new ProcessIcon())
            {
                pi.Display();


                // Make sure the application runs!
                Application.Run();

            }
        }

        private static void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            var foreGround = GetForegroundWindow();
            System.Diagnostics.Process me = System.Diagnostics.Process.GetCurrentProcess();
            var processes = Process.GetProcesses();
            var list = processes.Where(p => p.ProcessName == "explorer");

            var isSimplifiedEnable = BopomofoRegistry.IsSimplifiedEnable();

            if (list.Any())
            {
                var handle = list.First().MainWindowHandle;
                SwitchToThisWindow(handle, true);
                SetActiveWindow(handle);
            }

            Thread.Sleep(100);

            if (isSimplifiedEnable)
            {
                Console.WriteLine("Disable");
                BopomofoRegistry.EnableSimplified(false);
            }
            else
            {
                Console.WriteLine("Enable");
                BopomofoRegistry.EnableSimplified(true);
            }

            isSimplifiedEnable = BopomofoRegistry.IsSimplifiedEnable();
            pi.SetTextModeText(isSimplifiedEnable);

            SwitchToThisWindow(foreGround, true);
            SetActiveWindow(foreGround);
        }
    }
}
