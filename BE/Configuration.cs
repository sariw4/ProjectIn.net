using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public static class Configuration
    {
        public static int expireRequest { get; set; }
        public static int fee = 10;
        public static long HostingUnitKeyS = 10000000;
        public static long guestRequestKeyS = 10000000;
        public static long OrderKeyS = 10000000;
        public static long HostKeyS = 10000000;
        public static long Manager = 309090;
    }
}
