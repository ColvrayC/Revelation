using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Revelation
{
    public class IpAdress
    {
        public void IPAddress()
        {

        }

        public string get()
        {
            string externalIpString = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
            return externalIpString;
        }
        
    }
}
