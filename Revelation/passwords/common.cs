using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Revelation.common
{
   
   

    public class Cookie
    {
        public string hostname { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string expiresutc { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public string issecure { get; set; }
    }

    public class Password
    {
        public string hostname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
