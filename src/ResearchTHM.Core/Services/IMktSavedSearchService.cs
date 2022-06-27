using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ResearchTHM.Core.Models;
using ResearchTHM.Core.Models.RequestModels;
using ResearchTHM.Core.Repositories;

namespace ResearchTHM.Core.Services
{
    public interface IMktSavedSearchService : IRepository<MktSavedSearch>
    {
        Task<bool> DeleteSaveSearch(Guid searchid, string userId);
        Task<bool> UpdateSaveSearchName(string searchid, string searchName, string userId);
        IEnumerable<MktSavedSearch> GetSavedSearch(Guid userId);
        Task<bool> SaveSearchForUser(SaveSearchRequest srequest, Guid userId);
        Task<bool> UpdateSearchForUser(SaveSearchRequest srequest, Guid userId);
  }
}
