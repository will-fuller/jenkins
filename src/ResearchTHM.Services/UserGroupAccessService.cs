using System;
using System.Collections.Generic;
using System.Text;
using ResearchTHM.Core;
using ResearchTHM.Core.Models;
using ResearchTHM.Core.Services;
using System.Threading.Tasks;
using ResearchTHM.Core.Repositories;
using System.Linq;

namespace ResearchTHM.Services
{
    public class UserGroupAccessService : Repository<MktUserGroupAccess>,IMktUserGroupAccessService
    {
        private readonly ResearchMktContext _ResearchMktContext;
        public UserGroupAccessService(ResearchMktContext context):base(context)
        {
            _ResearchMktContext = context;
        }
        public async Task<bool> CreateUserGroupAccess(string userId,List<MktUserGroupAccess> UserGroupAccess)
        {

            if (userId != null)
            {
                _ResearchMktContext.MktUserGroupAccess.Where(c => c.UserId == Guid.Parse(userId)).DeleteFromQuery();
            }

            await _ResearchMktContext.MktUserGroupAccess.AddRangeAsync(UserGroupAccess);
            if (await _ResearchMktContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<IEnumerable<MktUserGroupAccess>> GetuserGroupAccessFromView()
        {
            return _ResearchMktContext.VwUserInfo.Select(e => new MktUserGroupAccess
            {
                GroupId = e.GroupId,
                UserId = e.UserId,
            });
        }


    }
}
