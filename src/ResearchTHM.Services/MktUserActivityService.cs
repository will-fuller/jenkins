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
    public class MktUserActivityService : Repository<MktUserActivity>, IMktUserActivityService
    {
        private readonly ResearchMktContext _ResearchMktContext;
        public MktUserActivityService(ResearchMktContext ResearchMktContext) : base(ResearchMktContext)
        {
            _ResearchMktContext = ResearchMktContext;
        }

    }
}
