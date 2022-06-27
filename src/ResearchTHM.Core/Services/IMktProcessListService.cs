using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ResearchTHM.Core.Models;
using ResearchTHM.Core.Repositories;

namespace ResearchTHM.Core.Services
{
    public interface IMktProcessListService : IRepository<MktProcessList>
    {
        Task<bool> UpdateProcessList(List<MktProcessList> processListToBeUpdated);                
    }
}
