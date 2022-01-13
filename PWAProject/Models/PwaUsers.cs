using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAProject.Models
{
    public class PwaUsers
    {
        public int? inUserId { get; set; }
        public Guid unUserId { get; set; }
        public string stFirstName { get; set; }
        public string stLastName { get; set; }
    }
}
