using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace phding.Bopomofo
{
    public class BopomofoRegistry
    {
        private static RegistryKey GetSimplifiedKey()
        {
            RegistryKey key = Registry.CurrentUser;
            key = key.OpenSubKey(@"Software\Microsoft\IME\15.0\IMETC", true);
            return key;
        }


        public static bool IsSimplifiedEnable()
        {
            var key = GetSimplifiedKey();
            if (key == null)
            {
                throw new KeyNotFoundException("Can't find IME key");
            }

            var val = key.GetValue("Enable Simplified Chinese Output");
            if ((string)val == "0x00000000")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static void EnableSimplified(bool val)
        {
            var key = GetSimplifiedKey();
            if (key == null)
            {
                throw new KeyNotFoundException("Can't find IME key");
            }

            var setValue = val ? "0x00000001" : "0x00000000";

            key.SetValue("Enable Simplified Chinese Output", setValue);

        }
    }
}
