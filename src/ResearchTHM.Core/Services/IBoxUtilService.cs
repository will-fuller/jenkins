using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ResearchTHM.Core.TrkdModels;
using ResearchTHM.Core.Models.RequestModels;

namespace ResearchTHM.Core.Services
{
    public interface IBoxUtilService
    {
        public Task<Stream> DownloadFile(string fileId);

        public Task<BoxFile> UploadFileAsync(Stream file,string filename, string parentId = "0");
        public Task<object> CreateFileMetadata(DocDownloadRequest request,string boxFileId, string TemplateName);
        public Task<BoxMetadataTemplate> createBoxTemplate(string templatekey);
        public string RetrieveFolderIdOrCreateNew(DocDownloadRequest request);
        Task<BoxFile> GetMetadata(string id);
    }
}
