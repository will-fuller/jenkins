using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchTHM.API.Resources
{
    public class ContributorResource
    {
        public Guid ContributorId { get; set; }
        public string ContributorName { get; set; }
        public string ContributorAlias { get; set; }
        public string ContributorUid { get; set; }
        public bool IsExcluded { get; set; }
        public bool IsDeleted { get; set; }
    }
}
