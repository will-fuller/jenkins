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
    public class BoxDocInfoService : Repository<MktBoxdocInfo>, IMktBoxDocInfoService
    {
        private readonly ResearchMktContext _ResearchMktContext;
        public BoxDocInfoService(ResearchMktContext ResearchMktContext) : base(ResearchMktContext)
        {
            _ResearchMktContext = ResearchMktContext;
        }

     
        public async Task<bool> CreateBoxDocInfo(MktBoxdocInfo boxinfo)
        {
            await _ResearchMktContext.MktBoxdocInfo.AddAsync(boxinfo);
            if (await _ResearchMktContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }
    }
}
