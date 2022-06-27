using System;
using System.Collections.Generic;
using System.Text;
using ResearchTHM.Core;
using ResearchTHM.Core.Models;
using ResearchTHM.Core.Services;
using System.Threading.Tasks;
using ResearchTHM.Core.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ResearchTHM.Services
{
    public class UserService: Repository<MktUser>, IMktUserService
    {
        private readonly ResearchMktContext _ResearchMktContext;
        public UserService(ResearchMktContext ResearchMktContext) : base(ResearchMktContext)
        {
            _ResearchMktContext = ResearchMktContext;
        }

        public async Task<IEnumerable<MktContributor>> GetUserGroupAccess(Guid mGroupId)
        {

            var results = await (from ga in _ResearchMktContext.MktGroupAccess
                                 join c in _ResearchMktContext.MktContributor on ga.ContributorId equals c.ContributorId
                                 where (ga.GroupId == mGroupId && c.IsDeleted == false && c.IsExcluded == false)
                                 orderby c.ContributorName
                                 select new MktContributor
                                 {
                                     ContributorName = c.ContributorName,
                                     ContributorId = c.ContributorId,
                                     IsExcluded = c.IsExcluded,
                                     ContributorUid= c.ContributorUid
                                 }).ToListAsync();

            return results;

        }
        
        public async Task<bool> UpdatePrefContributor(string userid,string username,string language, List<string> prefContributor)
        {
            var user = _ResearchMktContext.MktUser.FirstOrDefault(c => c.UserId == new Guid(userid));

            if(user != null){
                user.LanguagePreferences = language;
                user.ModifiedById = userid;
                user.ModifiedOn = DateTime.Now;

                _ResearchMktContext.SaveChanges();
            }
            
            var currentList = await _ResearchMktContext.MktUser.Where(c => c.UserId == new Guid(userid)).Select(c => new { c.PreferContributorId }).ToListAsync();
            var sellist = currentList[0].PreferContributorId?.Split(";").AsEnumerable();
            if (sellist == null){
                sellist = new List<string>();
            }
            var IncludedIds = prefContributor.Except(sellist);  //To go in Included Id's
            var ExcludedIds = sellist.Except(prefContributor); //to go in Excluded Id's

            var processId = await _ResearchMktContext.MktProcessList.Where(c => c.ProcessName == "UserPreference" && c.ProcessType == "UI").ToListAsync();
            await _ResearchMktContext.MktAuditLog.AddAsync(new MktAuditLog
            {
                UserName = username,
                UserId = new Guid(userid.ToString()),
                IncludedIds = String.Join(";", IncludedIds),
                ExcludedIds = String.Join(";", ExcludedIds),
                UpdatedOn = DateTime.UtcNow,
                ProcessId = new Guid(processId[0].ProcessId.ToString()),
                ProcessName = processId[0].ProcessName.ToString()
            });


            String allcontrub = String.Join(";", prefContributor);
            var ctb = await GetByIdAsync(new Guid(userid));
            ctb.PreferContributorId = allcontrub;
            ctb.ModifiedById = userid;
            ctb.ModifiedOn = DateTime.UtcNow;
            if (await _ResearchMktContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<VwUserInfoDeveloper> GetUserGroupById(string userid)
        {
            VwUserInfoDeveloper viewUser = new VwUserInfoDeveloper();
            viewUser = _ResearchMktContext.VwUserInfoDeveloper.Where(e => e.UserId.ToString() == userid).FirstOrDefault();
            return viewUser;

            /*if (userid == "0")
            {
                return _ResearchMktContext.VwUserInfo.AsQueryable().OrderBy(e => e.FirstName);
            }
            else
            {

                var results = _ResearchMktContext.VwUserInfo.Where(e => e.UserId.ToString() == userid);

                //var results = await (from ga in _ResearchMktContext.MktUser
                //                     join c in _ResearchMktContext.MktUserGroupAccess on ga.UserId equals c.UserId
                //                     join g in _ResearchMktContext.MktGroup on c.GroupId equals g.GroupId
                //                     where (ga.UserId == new Guid(userid) && ga.Status == true && ga.IsDeleted == false && g.Priority == 1)
                //                     select new
                //                     {
                //                         UserId = ga.UserId,
                //                         Employeeid = ga.Employeeid,
                //                         FirstName = ga.FirstName,
                //                         LastName = ga.LastName,
                //                         Company = ga.Company,
                //                         DepartmentName = ga.DepartmentName,
                //                         DepartmentNo = ga.DepartmentNo,
                //                         DisplayName = ga.DisplayName,
                //                         Location = ga.Location,
                //                         Country = ga.Country,
                //                         Title = ga.Title,
                //                         Samaccountname = ga.Samaccountname,
                //                         EmailId = ga.EmailId,
                //                         MobileNo = ga.MobileNo,
                //                         Status = ga.Status,
                //                         GroupId = c.GroupId,
                //                         GroupName = g.GroupName,
                //                         PreferContributorId = ga.PreferContributorId

                //                     }).ToListAsync();

                return results;
            }*/

        }

        public async Task<VwUserInfoDeveloper> GetUserInfoByUPN(string upn) {
            /*var viewUser = new VwUserInfo();
            viewUser = (VwUserInfo)_ResearchMktContext.VwUserInfo.Where(e => e.userprincipalname == upn);
            //var results = await _ResearchMktContext.MktUser.Where(e => e.UserPrincipalName == upn).ToListAsync();
            return (IEnumerable<VwUserInfo>)viewUser;*/
            VwUserInfoDeveloper viewUser = new VwUserInfoDeveloper();
            viewUser = _ResearchMktContext.VwUserInfoDeveloper.Where(e => e.userprincipalname == upn).FirstOrDefault();
            return viewUser;
        }

        public async Task<object> GetViewUserInfo(bool IsDeveloper)
        {
            var result = (dynamic)null;
            if(IsDeveloper == false)
            {
                result = _ResearchMktContext.VwUserInfo.AsQueryable().OrderBy(e => e.FirstName);
            }
            else
            {
                result = _ResearchMktContext.VwUserInfoDeveloper.AsQueryable().OrderBy(e => e.FirstName);
            }
               
            return result;
        }

        public async Task<bool> UpdateStatus(string userid, bool status)
        {
            var usr = await GetByIdAsync(new Guid(userid));
            usr.Status = status;
            usr.ModifiedOn = DateTime.UtcNow;
            if (await _ResearchMktContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<bool> UpdateUser(MktUser User, string userId)
        {
            var toBeUpdateUser = new MktUser()
            {
                Employeeid = User.Employeeid,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Company = User.Company,
                DepartmentName = User.DepartmentName,
                DepartmentNo = User.DepartmentNo,
                DisplayName = User.DisplayName,
                Location = User.Location,
                Title = User.Title,
                Samaccountname = User.Samaccountname,
                EmailId = User.EmailId,
                MobileNo = User.MobileNo,
                PreferContributorId = User.PreferContributorId,
                ModifiedById = userId,
                ModifiedOn = DateTime.UtcNow
            };
            _ResearchMktContext.MktUser.Attach(toBeUpdateUser);
            _ResearchMktContext.Entry(toBeUpdateUser).Property(x => x.Id).IsModified = false;
            _ResearchMktContext.Entry(toBeUpdateUser).Property(x => x.UserId).IsModified = false;
            _ResearchMktContext.Entry(toBeUpdateUser).Property(x => x.CreatedById).IsModified = false;
            _ResearchMktContext.Entry(toBeUpdateUser).Property(x => x.CreatedOn).IsModified = false;
            _ResearchMktContext.Entry(toBeUpdateUser).Property(x => x.DeletedById).IsModified = false;
            _ResearchMktContext.Entry(toBeUpdateUser).Property(x => x.DeletedOn).IsModified = false;
            _ResearchMktContext.Entry(toBeUpdateUser).Property(x => x.Status).IsModified = false;
            _ResearchMktContext.Entry(toBeUpdateUser).Property(x => x.IsDeleted).IsModified = false;
           // _ResearchMktContext.Update(toBeUpdateUser).Property(x => { x.Id, x.UserId,}).IsModified = false;
            if (await _ResearchMktContext.SaveChangesAsync() > 0)
                return true;
            return false;

        }

        public async Task<IEnumerable<MktLanguages>> GetLanguagesList()
        {
            List<MktLanguages> languages = new List<MktLanguages>();
            languages = _ResearchMktContext.Languages.Where(e => e.IsDeleted == 0 && e.Status == true).OrderBy(x => x.SortOrder).ToList();
            return languages;            

        }


    }
}
