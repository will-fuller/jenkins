using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchTHM.API.Resources
{
    public class UsageLogResource
    {
        public long Id { get; set; }
        public Guid LogId { get; set; }
        public string LogType { get; set; }
        public string Related { get; set; }
        public string BrowserName { get; set; }
        public string BrowserVersion { get; set; }
        public string Location { get; set; }
        public string Device { get; set; }
        public string IP { get; set; }
        public DateTime LogDate { get; set; }
        public string UserId { get; set; }
    }
}
