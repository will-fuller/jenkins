using ResearchTHM.Core.Models;
using ResearchTHM.Core.TrkdModels.TrkdRequestModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ResearchTHM.Core.Models.RequestModels;
using ResearchTHM.Core.TrkdModels;
using Box.V2.Models;
using ResearchTHM.Core.Models.ResponseModels;
using System.IO;
using Microsoft.Extensions.Primitives;

namespace ResearchTHM.Core.Services
{
    public interface ISearchService
    {
        Task<object> GetMetadata(Guid groupId,Guid userId);
        Task<DocSearchRequest> generateDocSearchObject(DocSearchUIRequest srequest,Guid userId);
        Task<bool> CreateUserActivity(DocSearchUIRequest srequest, Guid userId, Guid useractiveId);
        Task<bool> AddBoxDocInfo(DocDownloadRequest request, BoxFile f);
        Task<(string,string)> CheckIfDocumentCached(long docId);
        Task<bool> CreateDownloadHist(DocDownloadRequest docDownloadRequest,
          string downloadSource, string boxFileId, string requestId, string requestSource);
        Task<IEnumerable<AuthorResponse>> GetAuthorsList(string authName, int rowCount);
        Task<IEnumerable> GetOrganisations(string searchText = "", string rowCount = "20", string type = "CommonName", string match = "beginswith");
        Task<object> DocumentSearch(Guid userId, DocSearchUIRequest srequest);
        Task<string> GetListOfEntitledContributors(Guid userId, bool excluseCtb, IEnumerable<string> ctbs);
        Task<TocResponse> GetToc(Guid userId, long docId);
        Task<string> GetTocContext(string docId, string searchText, string searchMethod);
        Task<DownloadDocumentResponse> GetDocument(DocDownloadRequest docDownloadRequest);
        Task<bool> SaveUsageLog(StringValues userAgent, string ip, Guid userId, Guid related, string logType, string location, DateTime logDate, string loginSource = "");
        Task<DownloadDocumentResponse> GetDownloadDocumentFromDownloadHistory(DocDownloadRequest docDownloadRequest);
        Task<IEnumerable<string>> GetProjectCodes(string searchstring, int recordscount);
        DocDownloadRequest MapDiDefEntities(DiDef request, string pages);
        Task<MktUsageLog> GetLastLogin(Guid userId);
        DocDownloadRequest MapDownloadHistoryEntities(MktDownloadHist downloadHist);

        Task<bool> updateSearchforUser(SaveSearchRequest srequest, Guid userId);
        Task<bool> saveSearchforUser(SaveSearchRequest srequest, Guid userId);
        Task<bool> AddBoxDocInfo(MktDownloadHist downloadHist, BoxFile boxFile);
    }
}
