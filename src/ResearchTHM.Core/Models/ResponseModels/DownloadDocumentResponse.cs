using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ResearchTHM.Core.Models.ResponseModels
{
    public class DownloadDocumentResponse
    {
        public int ResponseCode { get; set; }
        public Stream File { get; set; }
        public string RedirectURL { get; set; }
        public string FileName { get; set; }
        public string BoxFileId { get; set; }
        public string Source { get; set; }
        public string Pages { get; set; }
        public bool IsPartial { get; set; }
    }
}
