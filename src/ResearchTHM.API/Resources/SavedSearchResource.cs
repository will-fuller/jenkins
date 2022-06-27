using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchTHM.API.Resources
{
    public class SavedSearchResource
    {
        public Guid SearchId { get; set; }
        public Guid UserId { get; set; }
        public string SearchName { get; set; }
        public string company { get; set; }
        public bool initiatingCov { get; set; }
        public string dateFrom { get; set; }
        public string dateTo { get; set; }
        public string headlineSearch { get; set; }
        public string textSearch { get; set; }
        public string tocSearch { get; set; }
        public bool searchJoinCondition { get; set; }
        public long reportNo { get; set; }
        public string reportStyles { get; set; }
        public string Industry { get; set; }
        public string Country { get; set; }
        public string ECountry { get; set; }
        public string Region { get; set; }
        public string ERegion { get; set; }
        public string contributors { get; set; }
        public bool excludeCtb { get; set; }
        public string Analyst { get; set; }
        public string pHintStr { get; set; }
        public string docId { get; set; }
        public string CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string DeletedById { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }

    }
}
