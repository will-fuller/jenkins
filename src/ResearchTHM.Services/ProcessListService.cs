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
    public class ProcessListService: Repository<MktProcessList>, IMktProcessListService
    {
        private readonly ResearchMktContext _context;

        public ProcessListService(ResearchMktContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> UpdateProcessList(List<MktProcessList> process)
        {           
            foreach(var item in process)
            {
                _context.Update(item);
            }
           

            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }
    }
}
