using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResearchTHM.Core.Models
{
    public partial class MktApiConfig
    {
        [Required]
        public long Id { get; set; }
        [StringLength(200)]
        public string AppId { get; set; }
        [StringLength(200)]
        public string ApiName { get; set; }
        [StringLength(200)]
        public string UserId { get; set; }
        [StringLength(200)]
        public string Password { get; set; }
        [StringLength(1000)]
        public string EndPoint { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
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
        public bool Status { get; set; }
        public Guid AppConfigId { get; set; }
    }
}
