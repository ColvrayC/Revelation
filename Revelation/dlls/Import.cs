using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Revelation.dlls
{
    public static class Import
    {
        [DllImport("Sodium.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SecretAeadAes(int var1, int var2);
    }
}
