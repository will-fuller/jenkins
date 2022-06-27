using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResearchTHM.Core.Models.RequestModels
{
    public class DocSearchUIRequest
    {
        public DocSearchUIRequest()
        {
            Industry = new List<searchField>();
            company = new List<company>();
            Analyst = new List<searchField>();
            contributors = new List<searchField>();
            Region = new List<searchField>();
            Country = new List<searchField>();
            ERegion = new List<searchField>();
            ECountry = new List<searchField>();
            Languages = new List<searchField>();
        }

        public DateTime LoginTime { get; set; } //This is a hack only to be used to store login time when signing in using SSO(ConnectLite). 
        public IEnumerable<company> company { get; set; }
        public bool initiatingCov { get; set; }
        [DataType(DataType.Text)]
        public string projectCode { get; set; }
        [DataType(DataType.Text)]
        public string dateFrom { get; set; }
        [DataType(DataType.Text)]
        public string dateTo { get; set; }
        [DataType(DataType.Text)]
        public string headlineSearch { get; set; }
        [DataType(DataType.Text)]
        public string textSearch { get; set; }
        [DataType(DataType.Text)]
        public string tocSearch { get; set; }
        [Required]
        public bool searchJoinCondition { get; set; }
        public IEnumerable<string> reportNo { get; set; }
        public IEnumerable<string> reportStyles { get; set; }
        public IEnumerable<searchField> Industry { get; set; }
        public IEnumerable<searchField> Country { get; set; }
        public IEnumerable<searchField> ECountry { get; set; }
        public IEnumerable<searchField> Region { get; set; }
        public IEnumerable<searchField> ERegion { get; set; }
        public IEnumerable<searchField> contributors { get; set; }
        public bool excludeCtb { get; set; }
        public IEnumerable<searchField> Analyst { get; set; }
        [DataType(DataType.Text)]
        public string pHintStr { get; set; }
        [DataType(DataType.Text)]
        public string pDocId { get; set; }
        [Range(0, 2500)]
        public int maxRows { get; set; }
        [StringLength(20)]
        public string PagesFrom { get; set; }
        [StringLength(20)]
        public string PagesTo { get; set; }
        [DataType(DataType.Text)]
        public string sortOrder { get; set; }
        [DataType(DataType.Text)]
        public string sortBy { get; set; }
        public string source { get; set; }
        public List<searchField> Languages { get; set; }
    }

    public class searchField
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string id { get; set; }
    }

    public class company
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string id { get; set; }
        public string PrimaryRIC { get; set; }
        public string isin { get; set; }
        public string ticker { get; set; }
    }

}
