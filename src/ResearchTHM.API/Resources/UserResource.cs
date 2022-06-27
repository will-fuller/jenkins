using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchTHM.API.Resources
{
    public class UserResource
    {
        //public long Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Range(0,long.MaxValue)]
        public long? Employeeid { get; set; }
        [StringLength(150)]
        public string FirstName { get; set; }
        [StringLength(150)]
        public string LastName { get; set; }
        [StringLength(250)]
        public string Company { get; set; }
        [StringLength(250)]
        public string DepartmentName { get; set; }
        [StringLength(250)]
        public string DepartmentNo { get; set; }
        [StringLength(250)]
        public string DisplayName { get; set; }
        [StringLength(250)]
        public string Location { get; set; }
        [StringLength(250)]
        public string Country { get; set; }
        [StringLength(250)]
        public string Title { get; set; }
        [StringLength(250)]
        public string Samaccountname { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string MobileNo { get; set; }
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
        [StringLength(150)]
        [MaxLength]
        public string PreferContributorId { get; set; }

        [NotMapped]
        [DataType(DataType.Text)]
        public string GroupId { get; set; }
    }
}
