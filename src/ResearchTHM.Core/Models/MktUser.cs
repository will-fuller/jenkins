using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ResearchTHM.Core.Models
{
    public partial class MktUser
    {
        public MktUser()
        {
            MktUserGroupAccess = new HashSet<MktUserGroupAccess>();
            
            MktUserRoleAccess = new HashSet<MktUserRoleAccess>();
        }


        [Required]
        public long Id { get; set; }
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

        [DataType(DataType.Text)]
        public string UserPrincipalName { get; set; }
        public Guid MsdsConsitencyGuid { get; set; }
        [DataType(DataType.Text)]
        public string LanguagePreferences { get; set; }

        [NotMapped]
        [DataType(DataType.Text)]
        public string GroupId { get; set; }



        public virtual ICollection<MktUserGroupAccess> MktUserGroupAccess { get; set; }
       
        public virtual ICollection<MktUserRoleAccess> MktUserRoleAccess { get; set; }
    }
}
