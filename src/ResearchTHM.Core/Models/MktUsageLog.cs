using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ResearchTHM.Core.Models
{
    public partial class MktUsageLog
    {
        [Required]
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
        public Guid UserId { get; set; }
        public string LoginSource { get; set; }
    }
}
