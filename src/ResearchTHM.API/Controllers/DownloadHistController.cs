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
    public class DownloadHistController : ControllerBase
    {
        private readonly IMktDownloadHistService _dwnloadHistService;
        private readonly IMapper _mapper;       
        public DownloadHistController(IMktDownloadHistService dwnloadHistService, IMapper mapper)
        {
            this._dwnloadHistService = dwnloadHistService;
            this._mapper = mapper;          
        }

        //[HttpPost("all")]
        //[EnableQuery]
        //public async Task<ActionResult<List<MktDownloadHist>>> GetDownloadHist([FromBody] DownloadHistResource queryDate)
        //{
        //    var downloadhist = _dwnloadHistService.Find(ele => ele.IsDeleted == false && ele.CreatedOn >= queryDate.From && ele.CreatedOn <= queryDate.To);
        //    var downloadhistResources = _mapper.Map<IEnumerable<MktDownloadHist>, IEnumerable<DownloadHistResource>>(downloadhist);
        //    return Ok(downloadhistResources);
        //}

        [HttpPost("dwnhistorybydoc")]
        [EnableQuery]
        public async Task<ActionResult<List<MktUsageLog>>> GetDownloadHistByDocument([FromBody] DownloadHistResource queryDate)
        {            
            var downloadhist = await _dwnloadHistService.GetDownloadHistByDocument(queryDate.From, queryDate.To);
            return Ok(downloadhist);
        }

        [HttpGet("dwnhistorybydocbyid")]
        [EnableQuery]
        public async Task<ActionResult<List<MktUsageLog>>> GetDownloadHistDocumentById(Guid downloadhistid)
        {
            var downloadhist = await _dwnloadHistService.GetDownloadHistDocumentById(downloadhistid);
            return Ok(downloadhist);
        }

        [HttpPost("dwnhistorybyuser")]
        [EnableQuery]
        public async Task<ActionResult<List<MktUsageLog>>> GetDownloadHistByUser([FromBody] DownloadHistResource queryDate)
        {
            var downloadhist = await _dwnloadHistService.GetDownloadHistByUser(queryDate.From, queryDate.To);
            return Ok(downloadhist);
        }

        [HttpPost("dwnhistorybyuserbyid")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<MktDownloadHist>>> GetDownloadHistByUserById(Guid userid, [FromBody] DownloadHistResource queryDate)
        {
            var downloadhist = await _dwnloadHistService.GetDownloadHistByUserById(userid, queryDate.From, queryDate.To);
            return Ok(downloadhist);
        }

        [HttpPost("dwnhistorybyuserbyid/count")]
        public async Task<ActionResult<IEnumerable<MktDownloadHist>>> GetDownloadHistByUserByIdCount(Guid userid, [FromBody] DownloadHistResource queryDate)
        {
            var downloadhist = await _dwnloadHistService.GetDownloadHistByUserByIdCount(userid, queryDate.From, queryDate.To);
            return Ok(downloadhist);
        }

        [HttpGet("downloadhistlog")]
        [EnableQuery]
        public async Task<ActionResult<DownloadHistResource>> GetDownloadHistById(string downloadhistid)
        {
            if (downloadhistid == "0")
                return Ok(_dwnloadHistService.Find(atb => atb.IsDeleted == false));
            else
                return Ok(await _dwnloadHistService.GetByIdAsync(new Guid(downloadhistid)));

        }

    }
}
