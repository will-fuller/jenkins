using System;
using System.Collections.Generic;

namespace ResearchTHM.Core.Models
{
    public partial class MktUserActivity
    {
        public long Id { get; set; }
        public Guid UserActivityId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
      
        public string company { get; set; }
        public bool initiatingCov { get; set; }
        public string dateFrom { get; set; }
        public string dateTo { get; set; }
        public string headlineSearch { get; set; }
        public string textSearch { get; set; }
        public string tocSearch { get; set; }
        public bool searchJoinCondition { get; set; }
        public string reportNo { get; set; }
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
        public long docId { get; set; }
        public DateTime SearchDate { get; set; }
        public string CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string DeletedById { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string ProjectCode { get; set; }
        public string PagesFrom { get; set; }
        public string PagesTo { get; set; }
        public string LanguagePreferences { get; set; }
        public string Source { get; set; }
    }
}
