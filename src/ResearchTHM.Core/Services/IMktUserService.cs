using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ResearchTHM.Core.Models;
using ResearchTHM.Core.Repositories;

namespace ResearchTHM.Core.Services
{
    public interface IMktUserService : IRepository<MktUser>
    {
        Task<IEnumerable<MktContributor>> GetUserGroupAccess(Guid rGroupId);
        Task<bool> UpdatePrefContributor(string userid,string username,string language, List<string> prefContributor);
        Task<VwUserInfoDeveloper> GetUserGroupById(string userid);
        Task<VwUserInfoDeveloper> GetUserInfoByUPN(string upn);        
        Task<bool> UpdateStatus(string userid, bool status);
        Task<bool> UpdateUser(MktUser user, string userId);
        Task<object> GetViewUserInfo(bool IsDeveloper);
        Task<IEnumerable<MktLanguages>> GetLanguagesList();

    }
}
