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
    public class DownloadHistService : Repository<MktDownloadHist>, IMktDownloadHistService
    {
        private readonly ResearchMktContext _ResearchMktContext;
        public DownloadHistService(ResearchMktContext ResearchMktContext) : base(ResearchMktContext)
        {
            _ResearchMktContext = ResearchMktContext;
        }

        public async Task<object> GetDownloadHistByDocument(DateTime? From,DateTime? To)
        {
            var results = await (from ga in _ResearchMktContext.MktDownloadHist
                                 join c in _ResearchMktContext.MktUser on ga.UserId equals c.UserId into ps
                                 from c in ps.DefaultIfEmpty()
                                 where ga.CreatedOn >= From && ga.CreatedOn <= To
                                 orderby c.FirstName, ga.CreatedOn descending
                                 select new
                                 {
                                     DownloadHistId = ga.DownloadHistId,
                                     DocId = ga.DocId,
                                     DocTitle = ga.DocTitle,
                                     DocReleaseDate = ga.DocReleaseDate,
                                     UserId = ga.UserId,
                                     //UserName = ga.UserName,
                                     Price = ga.Price,
                                     Saved = ga.Saved,
                                     Pages = ga.Pages,
                                     FileSize = ga.FileSize,
                                     ContributorName = ga.ContributorName,
                                     //ContributorId = ga.ContributorId,
                                     DownloadType = ga.DownloadType,
                                     Source = ga.Source,
                                     Name = c.FirstName + " " + c.LastName,
                                     Location = c.Location,
                                     DownloadDate = ga.CreatedOn,
                                     ga.ProjectCode
                                 }).ToListAsync();

            return results;

        }

        public async Task<object> GetDownloadHistByUser(DateTime? From, DateTime? To)
        {

            var results = await (from ga in _ResearchMktContext.MktDownloadHist
                                 join c in _ResearchMktContext.MktUser on ga.UserId equals c.UserId into ps
                                 from c in ps.DefaultIfEmpty()
                                 where ga.CreatedOn >= From && ga.CreatedOn <= To
                                 orderby c.FirstName, ga.CreatedOn descending
                                 select new
                                 {
                                     //DownloadHistId = ga.DownloadHistId,
                                     //UserId = ga.UserId,
                                     //UserName = ga.UserName,
                                     //Name = c.FirstName + " " + c.LastName,
                                     //Location =c.Location
                                     DownloadHistId = ga.DownloadHistId,
                                     DocId = ga.DocId,
                                     DocTitle = ga.DocTitle,
                                     DocReleaseDate = ga.DocReleaseDate,
                                     UserId = ga.UserId,
                                     //UserName = ga.UserName,
                                     Price = ga.Price,
                                     Saved = ga.Saved,
                                     Pages = ga.Pages,
                                     FileSize = ga.FileSize,
                                     ContributorName = ga.ContributorName,
                                     //ContributorId = ga.ContributorId,
                                     DownloadType = ga.DownloadType,
                                     Source = ga.Source,
                                     CreatedOn = ga.CreatedOn,
                                     Name = c.FirstName + " " + c.LastName,
                                     Location = c.Location,
                                     ga.ProjectCode
                                 }).ToListAsync();

            return results;
        }

        public async Task<IEnumerable<MktDownloadHist>> GetDownloadHistByUserById(Guid userid,DateTime? From,DateTime? To)
        {
            //To = To.Value.AddDays(1); //To handle the boundary condition
            var results = await (from ga in _ResearchMktContext.MktDownloadHist
                                 where ga.UserId == userid && (ga.CreatedOn >= From && ga.CreatedOn <= To) 
                                 select ga).OrderByDescending(e => e.CreatedOn).ToListAsync();

            return results;

        }
        public Task<int> GetDownloadHistByUserByIdCount(Guid userid,DateTime? From,DateTime? To)
        {
            To = To.Value.AddDays(1); //To handle the boundary condition
            return  (from ga in _ResearchMktContext.MktDownloadHist
                                 where ga.UserId == userid && (ga.CreatedOn >= From && ga.CreatedOn <= To) 
                                 select ga).OrderByDescending(e => e.CreatedOn).CountAsync();

        }

        public async Task<object> GetDownloadHistDocumentById(Guid downloadhistid)
        {
            var results = await (from ga in _ResearchMktContext.MktDownloadHist
                                 join c in _ResearchMktContext.MktUser on ga.UserId equals c.UserId into ps
                                 from c in ps.DefaultIfEmpty()
                                 where ga.DownloadHistId == downloadhistid
                                 //orderby c.FirstName, ga.CreatedOn descending
                                 select new
                                 {
                                     DownloadHistId = ga.DownloadHistId,
                                     DocId = ga.DocId,
                                     DocTitle = ga.DocTitle,
                                     DocReleaseDate = ga.DocReleaseDate,
                                     UserId = ga.UserId,
                                     //UserName = ga.UserName,
                                     Price = ga.Price,
                                     Saved = ga.Saved,
                                     Pages = ga.Pages,
                                     FileSize = ga.FileSize,
                                     ContributorName = ga.ContributorName,
                                     //ContributorId = ga.ContributorId,
                                     DownloadType = ga.DownloadType,
                                     Source = ga.Source,
                                     Name = c.FirstName + " " + c.LastName,
                                     Location = c.Location,
                                     ga.ProjectCode
                                 }).ToListAsync();

            return results;

        }
    }
}
