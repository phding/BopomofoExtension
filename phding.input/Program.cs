using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

using phding.Bopomofo;

namespace phding.input
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check if already running
            var title = ((AssemblyTitleAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyTitleAttribute))).Title;

            var processCount = Process.GetProcesses().Count(p => p.ProcessName == title);
            
            if (processCount > 1)
            {
                Console.WriteLine("Already exist");
                return;
            }

            //Register
            
            HotKeyManager.RegisterHotKey(Keys.S, KeyModifiers.Alt, KeyModifiers.Control);
            HotKeyManager.HotKeyPressed += HotKeyManager_HotKeyPressed;
            while (true)
            {
                Thread.Sleep(1000000);
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
