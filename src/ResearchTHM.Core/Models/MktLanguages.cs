using System;
using System.Collections.Generic;

namespace ResearchTHM.Core.Models
{
    public partial class MktLanguages
    {
        public long Id { get; set; }
        public Guid LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string MXLanguageMap { get; set; }
        public string RFC_1766 { get; set; }
        public string MXExistingLanguage { get; set; }
        public string CodePage { get; set; }
        public string LanguageUid { get; set; }        
        public string CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string DeletedById { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int IsDeleted { get; set; }
        public bool Status { get; set; }
        public int SortOrder  { get; set; }
    }
}
