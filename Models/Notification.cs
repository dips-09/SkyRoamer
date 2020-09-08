using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sky_Roamer.Models
{
    public class Notification
    {
        public string CustomerName { get; set; }
        public string PackageName { get; set; }
        public string Status { get; set; }
        public string AgentId { get; set; }
    }
}