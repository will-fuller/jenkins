using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchTHM.Core.Models
{
    public partial class MktCompanyMaster
    {
        public string BusinessEntity { get; set; }
        public string PI { get; set; }
        public string CommonName { get; set; }
        public string LegalName { get; set; }
        public string AlternateLegalName { get; set; }
        public string LongName { get; set; }
        public string Ticker { get; set; }
        public string PrimaryRIC { get; set; }
        public string PrimaryIssueName { get; set; }
        public string PrimaryRICExchangeCode { get; set; }
        public string PrimaryRICExchangeName { get; set; }
        public string MXID { get; set; }
        public string PrimaryRICTickerSymbol { get; set; }
        public string PrimaryRICISIN { get; set; }
        public string PrimaryRICPI { get; set; }
        public string InsertDateTime { get; set; }
        public string CIK { get; set; }
        public string LEI { get; set; }
        public string Orgid { get; set; }
        public string IPODate { get; set; }
        public string IsPublic { get; set; }
        public string Gics { get; set; }
        public string GicsCode { get; set; }
        public string GicsIndustryID { get; set; }
        public string IsActive { get; set; }
        public string IssuerCikNumber { get; set; }
        public string IssuerId { get; set; }     
        public string ParentCompanyPI { get; set; }
        public string ParentOrganisationName { get; set; }
        public string UltimateParentOrganisationName { get; set; }
        public string UltimateParentCompanyPI { get; set; }
        public string UltimateParentIssuerEquityPrimaryRIC { get; set; }
        public string UltimateParentOrganisationOrgid { get; set; }
        public string UltimateParentOrganizationID { get; set; }
    }
}
