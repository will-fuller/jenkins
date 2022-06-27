using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchTHM.API.Resources
{
    public class CountryResource
    {
        //public long Id { get; set; }
      
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
        public string GlobalCode { get; set; }
        public string LocalCode { get; set; }
        public string SetCode { get; set; }
        public string CountryUid { get; set; }
        public string GeographicCodes { get; set; }
        public string CreatedById { get; set; }
      
        public DateTime? CreatedOn { get; set; }
        public string ModifiedById { get; set; }
       
        public DateTime? ModifiedOn { get; set; }
        public string DeletedById { get; set; }
      
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool Status { get; set; }

    }
}
