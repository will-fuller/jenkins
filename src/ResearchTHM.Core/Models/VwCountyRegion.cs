using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchTHM.Core.Models
{
    public class VwCountyRegion
    {
        public string CountryName { get; set; }
        public string CountryGlobalCode { get; set; }
        public string RegionName { get; set; }
        public string RegionGlobalCode { get; set; }
        public string IsCountry { get; set; }
        public string SelectedCode { get; set; }
    }
}
