using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ResearchTHM.Core.Models
{
    public class VwUserInfoDeveloper
    {
        public Guid UserId { get; set; }
        public long? Employeeid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentNo { get; set; }
        public string DisplayName { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
        public string Title { get; set; }
        public string Samaccountname { get; set; }
        public string userprincipalname { get; set; }
        public string PreferContributorId { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public bool? Status { get; set; }
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public int? Priority { get; set; }       
        public DateTime? CreatedOn { get; set; } = DateTime.Now;       
        public DateTime? LogDate { get; set; } = DateTime.Now;
        public string Languages { get; set; }
    }
}
