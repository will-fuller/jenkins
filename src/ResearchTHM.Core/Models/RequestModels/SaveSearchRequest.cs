using ResearchTHM.Core.TrkdModels.TrkdRequestModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ResearchTHM.Core.Models.RequestModels
{
    public class SaveSearchRequest : DocSearchUIRequest
    {
        [DataType(DataType.Text)]
        public string dateRange { get; set; }
        public Guid searchId { get; set; }
        [DataType(DataType.Text)]
        public string searchName { get; set; }
        [DataType(DataType.Text)]
        public string keyword1Search { get; set; }
        [DataType(DataType.Text)]
        public string keyword2Search { get; set; }
        [DataType(DataType.Text)]
        public string keyword3Search { get; set; }
        [DataType(DataType.Text)]
        public string keyword1Type { get; set; }
        [DataType(DataType.Text)]
        public string keyword2Type { get; set; }
        [DataType(DataType.Text)]
        public string keyword3Type { get; set; }
    }
}
