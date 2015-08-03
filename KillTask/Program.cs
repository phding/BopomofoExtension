using System.Diagnostics;

namespace KillTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var processes = Process.GetProcessesByName("BopomofoExtension");
            foreach (var process in processes)
            {
                process.Kill();
            }
        }
    }
}
