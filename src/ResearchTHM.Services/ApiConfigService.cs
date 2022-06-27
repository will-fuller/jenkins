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
    public class ApiConfigService : Repository<MktApiConfig>, IMktApiConfigService
    {
        private readonly ResearchMktContext _ResearchMktContext;
        public ApiConfigService(ResearchMktContext ResearchMktContext) : base(ResearchMktContext)
        {
            _ResearchMktContext = ResearchMktContext;
        }

    }
}
