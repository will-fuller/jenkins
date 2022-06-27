using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchTHM.Core.Models
{
    public class BoxFolderInfo
    {
        public int Id { get; set; }
        public string BoxFolderId { get; set; }
        public string FolderName { get; set; }
        public string Parent { get; set; }
        public string ParentId { get; set; }
    }
}
