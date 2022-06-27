using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResearchTHM.API.Resources
{
    public class GroupAccessResource
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string GroupId { get; set; }
        [StringLength(250)]
        public string userId { get; set; }
        [StringLength(250)]
        public string userName { get; set; }
        [MaxLength]
        public string ContributorId { get; set; }
    }
}
