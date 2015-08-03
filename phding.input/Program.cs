using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using phding.Bopomofo;

namespace phding.input
{
    class Program
    {
        static void Main(string[] args)
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
