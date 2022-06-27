using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ResearchTHM.Core;
using ResearchTHM.Core.Models;
using ResearchTHM.Core.Services;
using ResearchTHM.API.Resources;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.OData;

namespace ResearchTHM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessListController : ControllerBase
    {
        private readonly IMktProcessListService _processService;        
        private readonly IMapper _mapper;

        public ProcessListController(IMktProcessListService processService, IMapper mapper)
        {
            this._processService = processService;           
            this._mapper = mapper;
        }

        [HttpGet("")]
        [EnableQuery]
        public async Task<ActionResult<List<MktProcessList>>> GetProcessList()
        {
            var process = await _processService.GetAllAsync();
           // var processResources = _mapper.Map<IEnumerable<MktProcessList>, IEnumerable<ProcessListResources>>(process);
            return Ok(process);
        }

        //[HttpGet("{processId}")]
        //public async Task<ActionResult<ProcessListResources>> GetProcessListById(Guid processId)
        //{
        //    var process = await _processService.GetByIdAsync(processId);
        //    var processResources = _mapper.Map<MktProcessList, ProcessListResources>(process);
        //    return Ok(processResources);
        //}               
      
        [HttpPost("update")]
        public async Task<ActionResult<MktProcessList>> UpdateProcess([FromQuery]Guid userId, [FromBody] IEnumerable<MktProcessList> saveProcessResources)
        {
            List<MktProcessList> ProcessList = new List<MktProcessList>();
            foreach(var item in saveProcessResources)
            {
                ProcessListResources processobj = new ProcessListResources();
                processobj.ID = item.ID;
                processobj.ProcessId = item.ProcessId;
                processobj.ProcessName = item.ProcessName;
                processobj.ProcessType = item.ProcessType;
                processobj.schFlag = item.schFlag;
                processobj.notifyFlag = item.notifyFlag;
                processobj.notifyList = item.notifyList;
                processobj.CreatedById = userId.ToString();
                processobj.CreatedBy = "UI";
                processobj.CreatedOn = DateTime.UtcNow;
                processobj.Status = true;

                var process = _mapper.Map<ProcessListResources, MktProcessList>(processobj);
                ProcessList.Add(process);                
            }

            await _processService.UpdateProcessList(ProcessList);

            return Ok();
        }

    }
}