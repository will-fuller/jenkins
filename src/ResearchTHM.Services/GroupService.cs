using System;
using System.Collections.Generic;
using System.Text;
using ResearchTHM.Core;
using ResearchTHM.Core.Models;
using ResearchTHM.Core.Services;
using System.Threading.Tasks;
using ResearchTHM.Core.Repositories;

namespace ResearchTHM.Services
{
    public class GroupService : Repository<MktGroup>,IMktGroupService
    {
        private readonly ResearchMktContext _context;
        public GroupService(ResearchMktContext context):base(context)
        {
            _context = context;
        }

        public async Task<bool> UpdateGroup(MktGroup group,string userId)
        {
            var toBeUpdateGroup = new MktGroup() { 
                GroupId = group.GroupId,
                GroupName = group.GroupName,
                GroupAdName = group.GroupAdName,
                GroupDescription = group.GroupDescription,
                ModifiedById = userId,
                ModifiedOn = DateTime.UtcNow
              };

            _context.MktGroup.Attach(toBeUpdateGroup);
            _context.Entry(toBeUpdateGroup).Property(x => x.GroupName).IsModified = true;
            _context.Entry(toBeUpdateGroup).Property(x => x.GroupAdName).IsModified = true;
            _context.Entry(toBeUpdateGroup).Property(x => x.GroupDescription).IsModified = true;
            _context.Entry(toBeUpdateGroup).Property(x => x.ModifiedById).IsModified = true;
            _context.Entry(toBeUpdateGroup).Property(x => x.ModifiedOn).IsModified = true;
            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<bool> DeleteGroup(Guid groupid, string userid)
        {
            var group = await GetByIdAsync(groupid);
            group.IsDeleted = true;
            group.DeletedById = userid;
            group.DeletedOn = DateTime.UtcNow;
            _context.Update(group);
            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<bool> UpdateStatus(string groupid, bool status, string userid)
        {
            var group = await GetByIdAsync(new Guid(groupid));
            group.Status = status;
            group.ModifiedById = userid;
            group.ModifiedOn = DateTime.UtcNow;
            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }

    }
}
