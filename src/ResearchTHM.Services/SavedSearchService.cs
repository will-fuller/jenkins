using System;
using System.Collections.Generic;
using System.Text;
using ResearchTHM.Core;
using ResearchTHM.Core.Models;
using ResearchTHM.Core.Services;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ResearchTHM.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ResearchTHM.Core.Models.RequestModels;
using Serilog;

namespace ResearchTHM.Services
{
    public class SavedSearchService : Repository<MktSavedSearch>, IMktSavedSearchService
    {
        private readonly ResearchMktContext _ResearchMktContext;
        private readonly ILogger _logger;
        public SavedSearchService(ResearchMktContext ResearchMktContext, ILogger logger) : base(ResearchMktContext)
        {
            _ResearchMktContext = ResearchMktContext;
            _logger = logger;
        }

        public async Task<bool> DeleteSaveSearch([FromQuery] Guid searchid, string userId)
        {
            var srch = await GetByIdAsync(searchid);
            srch.IsDeleted = true;
            srch.DeletedById = userId;
            srch.DeletedOn = DateTime.UtcNow;
            _ResearchMktContext.Update(srch);
            if (await _ResearchMktContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<bool> UpdateSaveSearchName(string searchid, string searchName, string userId)
        {
            bool status = false;
            Guid serId = Guid.Parse(searchid);

            try
            {
                _logger.Information("Saved Search Service  logging in UpdateSaveSearchName ");

                var srch = await GetByIdAsync(serId);
                srch.ModifiedOn = DateTime.UtcNow;
                srch.ModifiedById = userId;
                srch.SearchName = searchName;
                _ResearchMktContext.Update(srch);
                if (await _ResearchMktContext.SaveChangesAsync() > 0)
                    status = true;
            }
            catch (Exception ex)
            {
                status = false;
                _logger.Error(ex, $"Saved Search Service  having exception for searchId:{serId} in Update");
            }

            return status;
        }

        public IEnumerable<MktSavedSearch> GetSavedSearch(Guid userId)
        {
            return _ResearchMktContext.MktSavedSearch.Where(ele => ele.UserId == userId && (ele.IsDeleted == false)).OrderBy(ele => ele.SearchName);
        }
        public async Task<bool> SaveSearchForUser(SaveSearchRequest srequest, Guid userid)
        {
            Guid searchid = Guid.NewGuid();
            MktSavedSearch mktSavedSearch = new MktSavedSearch();
            mktSavedSearch.SearchId = searchid;
            mktSavedSearch.UserId = userid;
            mktSavedSearch.reportStyles = string.Join(",", srequest.reportStyles);
            mktSavedSearch.initiatingCov = srequest.initiatingCov;
            mktSavedSearch.PagesFrom = srequest.PagesFrom;
            mktSavedSearch.PagesTo = srequest.PagesTo;
            mktSavedSearch.dateFrom = srequest.dateFrom;
            mktSavedSearch.dateTo = srequest.dateTo;
            mktSavedSearch.dateRange = srequest.dateRange;
            mktSavedSearch.excludeCtb = srequest.excludeCtb;
            mktSavedSearch.contributors = JsonConvert.SerializeObject(srequest.contributors);
            mktSavedSearch.company = JsonConvert.SerializeObject(srequest.company);
            mktSavedSearch.Analyst = JsonConvert.SerializeObject(srequest.Analyst);
            mktSavedSearch.Industry = JsonConvert.SerializeObject(srequest.Industry);
            mktSavedSearch.Country = JsonConvert.SerializeObject(srequest.Country);
            mktSavedSearch.ECountry = JsonConvert.SerializeObject(srequest.ECountry);
            mktSavedSearch.Region = JsonConvert.SerializeObject(srequest.Region);
            mktSavedSearch.ERegion = JsonConvert.SerializeObject(srequest.ERegion);
            mktSavedSearch.CreatedOn = DateTime.UtcNow;
            mktSavedSearch.CreatedById = userid.ToString();
            mktSavedSearch.SearchName = srequest.searchName;
            mktSavedSearch.reportNo = String.Join("", srequest.reportNo);

            mktSavedSearch.keyword1Search = srequest.keyword1Search;
            mktSavedSearch.keyword2Search = srequest.keyword2Search;
            mktSavedSearch.keyword1Type = srequest.keyword1Type;
            mktSavedSearch.keyword2Type = srequest.keyword2Type;

            await _ResearchMktContext.MktSavedSearch.AddAsync(mktSavedSearch);
            if (await _ResearchMktContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }
        public async Task<bool> UpdateSearchForUser(SaveSearchRequest srequest, Guid userid)
        {
            MktSavedSearch mktSavedSearch = new MktSavedSearch();
            mktSavedSearch.SearchId = srequest.searchId;
            mktSavedSearch.UserId = userid;
            mktSavedSearch.reportStyles = string.Join(",", srequest.reportStyles);
            mktSavedSearch.initiatingCov = srequest.initiatingCov;
            mktSavedSearch.PagesFrom = srequest.PagesFrom;
            mktSavedSearch.PagesTo = srequest.PagesTo;
            mktSavedSearch.dateFrom = srequest.dateFrom;
            mktSavedSearch.dateTo = srequest.dateTo;
            mktSavedSearch.dateRange = srequest.dateRange;
            mktSavedSearch.excludeCtb = srequest.excludeCtb;
            mktSavedSearch.contributors = JsonConvert.SerializeObject(srequest.contributors);
            mktSavedSearch.company = JsonConvert.SerializeObject(srequest.company);
            mktSavedSearch.Analyst = JsonConvert.SerializeObject(srequest.Analyst);
            mktSavedSearch.Industry = JsonConvert.SerializeObject(srequest.Industry);
            mktSavedSearch.Country = JsonConvert.SerializeObject(srequest.Country);
            mktSavedSearch.ECountry = JsonConvert.SerializeObject(srequest.ECountry);
            mktSavedSearch.Region = JsonConvert.SerializeObject(srequest.Region);
            mktSavedSearch.ERegion = JsonConvert.SerializeObject(srequest.ERegion);
            mktSavedSearch.CreatedOn = DateTime.UtcNow;
            mktSavedSearch.CreatedById = userid.ToString();
            mktSavedSearch.SearchName = srequest.searchName;
            mktSavedSearch.reportNo = String.Join("", srequest.reportNo);

            mktSavedSearch.keyword1Search = srequest.keyword1Search;
            mktSavedSearch.keyword2Search = srequest.keyword2Search;
            mktSavedSearch.keyword1Type = srequest.keyword1Type;
            mktSavedSearch.keyword2Type = srequest.keyword2Type;
            mktSavedSearch.searchJoinCondition = srequest.searchJoinCondition;
            //mktSavedSearch.IsDeleted = false;

            _ResearchMktContext.MktSavedSearch.Update(mktSavedSearch);
            if (await _ResearchMktContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

    }
}
