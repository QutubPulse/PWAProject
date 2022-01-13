using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAProject.Models
{
    public class UserSessionDetail
    {
        public int? inId { get; set; }
        public int inUserId { get; set; }
        public string stSessionId { get; set; }
        public bool flgIsActive { get; set; }
        public string stBrowserName { get; set; }
        public string stIpAddress { get; set; }
    }
}
