using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchTHM.API.Resources
{
    public class ApiConfigResource
    {
        
        public Guid AppConfigId { get; set; }
        public string AppId { get; set; }
        public string ApiName { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string EndPoint { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public bool Status { get; set; }
        public string CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string DeletedById { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
