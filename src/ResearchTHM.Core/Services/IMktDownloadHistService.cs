using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ResearchTHM.Core.Models;
using ResearchTHM.Core.Models.RequestModels;
using ResearchTHM.Core.Repositories;

namespace ResearchTHM.Core.Services
{
    public interface IMktDownloadHistService : IRepository<MktDownloadHist>
    {
        Task<object> GetDownloadHistByDocument(DateTime? From, DateTime? To);
        Task<object> GetDownloadHistByUser(DateTime? From, DateTime? To);
        Task<object> GetDownloadHistDocumentById(Guid downloadhistid);
        Task<IEnumerable<MktDownloadHist>> GetDownloadHistByUserById(Guid userid,DateTime? From,DateTime? To);
        Task<int> GetDownloadHistByUserByIdCount(Guid userid,DateTime? From,DateTime? To);
    }
}
