using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResearchTHM.API.Resources
{
    public class GroupResource
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public Guid GroupId { get; set; }
        [StringLength(250)]
        public string GroupName { get; set; }
        [StringLength(1000)]
        public string GroupDescription { get; set; }
        [StringLength(250)]
        public string CreatedById { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }
        [StringLength(250)]
        public string ModifiedById { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
        [StringLength(250)]
        public string DeletedById { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public bool Status { get; set; }

        [DataType(DataType.Text)]
        public string ContributorId { get; set; }
        [StringLength(250)]
        public string GroupAdName { get; set; }

        [StringLength(250)]
        public string userId { get; set; }
        [StringLength(250)]
        public string userName { get; set; }
    }
}
